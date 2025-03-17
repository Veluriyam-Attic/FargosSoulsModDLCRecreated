using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002A9 RID: 681
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x001461EF File Offset: 0x001453EF
		private void InitializeThreadCounts()
		{
			this._upgradeLockOwnerId = -1;
			this._writeLockOwnerId = -1;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x001461FF File Offset: 0x001453FF
		public ReaderWriterLockSlim() : this(LockRecursionPolicy.NoRecursion)
		{
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x00146208 File Offset: 0x00145408
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == LockRecursionPolicy.SupportsRecursion)
			{
				this._fIsReentrant = true;
			}
			this.InitializeThreadCounts();
			this._waiterStates = ReaderWriterLockSlim.WaiterStates.NoWaiters;
			this._lockID = Interlocked.Increment(ref ReaderWriterLockSlim.s_nextLockID);
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x00146238 File Offset: 0x00145438
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x00146245 File Offset: 0x00145445
		private bool HasNoWaiters
		{
			get
			{
				return (this._waiterStates & ReaderWriterLockSlim.WaiterStates.NoWaiters) > ReaderWriterLockSlim.WaiterStates.None;
			}
			set
			{
				if (value)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.NoWaiters;
					return;
				}
				this._waiterStates &= ~ReaderWriterLockSlim.WaiterStates.NoWaiters;
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0014626B File Offset: 0x0014546B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
		{
			return rwc.lockID == 0L || (rwc.readercount == 0 && rwc.writercount == 0 && rwc.upgradecount == 0);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00146292 File Offset: 0x00145492
		private bool IsRwHashEntryChanged(ReaderWriterCount lrwc)
		{
			return lrwc.lockID != this._lockID;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x001462A8 File Offset: 0x001454A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReaderWriterCount GetThreadRWCount(bool dontAllocate)
		{
			ReaderWriterCount next = ReaderWriterLockSlim.t_rwc;
			ReaderWriterCount readerWriterCount = null;
			while (next != null)
			{
				if (next.lockID == this._lockID)
				{
					return next;
				}
				if (!dontAllocate && readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(next))
				{
					readerWriterCount = next;
				}
				next = next.next;
			}
			if (dontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				readerWriterCount = new ReaderWriterCount();
				readerWriterCount.next = ReaderWriterLockSlim.t_rwc;
				ReaderWriterLockSlim.t_rwc = readerWriterCount;
			}
			readerWriterCount.lockID = this._lockID;
			return readerWriterCount;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x00146315 File Offset: 0x00145515
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0014631F File Offset: 0x0014551F
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0014632D File Offset: 0x0014552D
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0014633B File Offset: 0x0014553B
		private bool TryEnterReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterReadLockCore(timeout);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00146344 File Offset: 0x00145544
		private bool TryEnterReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.LockRecursionException_ReadAfterWriteNotAllowed);
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.LockRecursionException_RecursiveReadNotAllowed);
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					return true;
				}
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					threadRWCount.readercount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					this._fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					threadRWCount.readercount++;
					this._owners += 1U;
					this._spinLock.Exit();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this._owners >= 268435454U)
			{
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyRead())
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else if (this._readEvent == null)
				{
					this.LazyCreateEvent(ref this._readEvent, ReaderWriterLockSlim.EnterLockType.Read);
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this._readEvent, ref this._numReadWaiters, timeout, ReaderWriterLockSlim.EnterLockType.Read);
					if (!flag)
					{
						return false;
					}
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
			}
			this._owners += 1U;
			threadRWCount.readercount++;
			this._spinLock.Exit();
			return flag;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00146586 File Offset: 0x00145786
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00146590 File Offset: 0x00145790
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0014659E File Offset: 0x0014579E
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x001465AC File Offset: 0x001457AC
		private bool TryEnterWriteLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterWriteLockCore(timeout);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x001465B8 File Offset: 0x001457B8
		private bool TryEnterWriteLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			bool flag = false;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.LockRecursionException_RecursiveWriteNotAllowed);
				}
				ReaderWriterLockSlim.EnterSpinLockReason reason;
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					flag = true;
					reason = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				}
				else
				{
					reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				}
				this._spinLock.Enter(reason);
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.LockRecursionException_WriteAfterReadNotAllowed);
				}
			}
			else
			{
				ReaderWriterLockSlim.EnterSpinLockReason reason2;
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.EnterRecursiveWrite;
				}
				else if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				}
				else
				{
					reason2 = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				}
				this._spinLock.Enter(reason2);
				threadRWCount = this.GetThreadRWCount(false);
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					threadRWCount.writercount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.LockRecursionException_WriteAfterReadNotAllowed);
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || threadRWCount == null)
						{
							goto IL_167;
						}
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						if (threadRWCount.readercount <= 0)
						{
							goto IL_167;
						}
						this.SetWriterAcquired();
					}
					IL_22C:
					if (this._fIsReentrant)
					{
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						threadRWCount.writercount++;
					}
					this._spinLock.Exit();
					this._writeLockOwnerId = currentManagedThreadId;
					return true;
				}
				IL_167:
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyWrite(flag))
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(flag ? ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite : ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite);
				}
				else if (flag)
				{
					if (this._waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this._waitUpgradeEvent, ReaderWriterLockSlim.EnterLockType.UpgradeToWrite);
					}
					else if (!this.WaitOnEvent(this._waitUpgradeEvent, ref this._numWriteUpgradeWaiters, timeout, ReaderWriterLockSlim.EnterLockType.UpgradeToWrite))
					{
						return false;
					}
				}
				else if (this._writeEvent == null)
				{
					this.LazyCreateEvent(ref this._writeEvent, ReaderWriterLockSlim.EnterLockType.Write);
				}
				else if (!this.WaitOnEvent(this._writeEvent, ref this._numWriteWaiters, timeout, ReaderWriterLockSlim.EnterLockType.Write))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_22C;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0014682B File Offset: 0x00145A2B
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00146835 File Offset: 0x00145A35
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00146843 File Offset: 0x00145A43
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00146851 File Offset: 0x00145A51
		private bool TryEnterUpgradeableReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterUpgradeableReadLockCore(timeout);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0014685C File Offset: 0x00145A5C
		private bool TryEnterUpgradeableReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this._fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this._fIsReentrant)
			{
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					throw new LockRecursionException(SR.LockRecursionException_RecursiveUpgradeNotAllowed);
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					throw new LockRecursionException(SR.LockRecursionException_UpgradeAfterWriteNotAllowed);
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.LockRecursionException_UpgradeAfterReadNotAllowed);
				}
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				threadRWCount = this.GetThreadRWCount(false);
				if (currentManagedThreadId == this._upgradeLockOwnerId)
				{
					threadRWCount.upgradecount++;
					this._spinLock.Exit();
					return true;
				}
				if (currentManagedThreadId == this._writeLockOwnerId)
				{
					this._owners += 1U;
					this._upgradeLockOwnerId = currentManagedThreadId;
					threadRWCount.upgradecount++;
					if (threadRWCount.readercount > 0)
					{
						this._fUpgradeThreadHoldingRead = true;
					}
					this._spinLock.Exit();
					return true;
				}
				if (threadRWCount.readercount > 0)
				{
					this._spinLock.Exit();
					throw new LockRecursionException(SR.LockRecursionException_UpgradeAfterReadNotAllowed);
				}
			}
			int num = 0;
			while (this._upgradeLockOwnerId != -1 || this._owners >= 268435454U)
			{
				if (timeout.IsExpired)
				{
					this._spinLock.Exit();
					return false;
				}
				if (num < 20 && this.ShouldSpinForEnterAnyRead())
				{
					this._spinLock.Exit();
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead);
				}
				else if (this._upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this._upgradeEvent, ReaderWriterLockSlim.EnterLockType.UpgradeableRead);
				}
				else if (!this.WaitOnEvent(this._upgradeEvent, ref this._numUpgradeWaiters, timeout, ReaderWriterLockSlim.EnterLockType.UpgradeableRead))
				{
					return false;
				}
			}
			this._owners += 1U;
			this._upgradeLockOwnerId = currentManagedThreadId;
			if (this._fIsReentrant)
			{
				if (this.IsRwHashEntryChanged(threadRWCount))
				{
					threadRWCount = this.GetThreadRWCount(false);
				}
				threadRWCount.upgradecount++;
			}
			this._spinLock.Exit();
			return true;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00146A6C File Offset: 0x00145C6C
		public void ExitReadLock()
		{
			this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
			ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
			if (threadRWCount == null || threadRWCount.readercount < 1)
			{
				this._spinLock.Exit();
				throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedRead);
			}
			if (this._fIsReentrant)
			{
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this._spinLock.Exit();
					return;
				}
				if (Environment.CurrentManagedThreadId == this._upgradeLockOwnerId)
				{
					this._fUpgradeThreadHoldingRead = false;
				}
			}
			this._owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00146B10 File Offset: 0x00145D10
		public void ExitWriteLock()
		{
			if (!this._fIsReentrant)
			{
				if (Environment.CurrentManagedThreadId != this._writeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedWrite);
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyWrite);
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyWrite);
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount == null)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedWrite);
				}
				if (threadRWCount.writercount < 1)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedWrite);
				}
				threadRWCount.writercount--;
				if (threadRWCount.writercount > 0)
				{
					this._spinLock.Exit();
					return;
				}
			}
			this.ClearWriterAcquired();
			this._writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00146BD0 File Offset: 0x00145DD0
		public void ExitUpgradeableReadLock()
		{
			if (!this._fIsReentrant)
			{
				if (Environment.CurrentManagedThreadId != this._upgradeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedUpgrade);
				}
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
			}
			else
			{
				this._spinLock.Enter(ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead);
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount == null)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedUpgrade);
				}
				if (threadRWCount.upgradecount < 1)
				{
					this._spinLock.Exit();
					throw new SynchronizationLockException(SR.SynchronizationLockException_MisMatchedUpgrade);
				}
				threadRWCount.upgradecount--;
				if (threadRWCount.upgradecount > 0)
				{
					this._spinLock.Exit();
					return;
				}
				this._fUpgradeThreadHoldingRead = false;
			}
			this._owners -= 1U;
			this._upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00146C9C File Offset: 0x00145E9C
		private void LazyCreateEvent([NotNull] ref EventWaitHandle waitEvent, ReaderWriterLockSlim.EnterLockType enterLockType)
		{
			this._spinLock.Exit();
			EventWaitHandle eventWaitHandle = new EventWaitHandle(false, (enterLockType == ReaderWriterLockSlim.EnterLockType.Read) ? EventResetMode.ManualReset : EventResetMode.AutoReset);
			ReaderWriterLockSlim.EnterSpinLockReason reason;
			if (enterLockType > ReaderWriterLockSlim.EnterLockType.UpgradeableRead)
			{
				if (enterLockType != ReaderWriterLockSlim.EnterLockType.Write)
				{
					reason = (ReaderWriterLockSlim.EnterSpinLockReason)11;
				}
				else
				{
					reason = (ReaderWriterLockSlim.EnterSpinLockReason)10;
				}
			}
			else
			{
				reason = ReaderWriterLockSlim.EnterSpinLockReason.Wait;
			}
			this._spinLock.Enter(reason);
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Dispose();
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00146CF4 File Offset: 0x00145EF4
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, ReaderWriterLockSlim.TimeoutTracker timeout, ReaderWriterLockSlim.EnterLockType enterLockType)
		{
			ReaderWriterLockSlim.WaiterStates waiterStates = ReaderWriterLockSlim.WaiterStates.None;
			ReaderWriterLockSlim.EnterSpinLockReason reason;
			switch (enterLockType)
			{
			case ReaderWriterLockSlim.EnterLockType.Read:
				break;
			case ReaderWriterLockSlim.EnterLockType.UpgradeableRead:
				waiterStates = ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled;
				break;
			case ReaderWriterLockSlim.EnterLockType.Write:
				waiterStates = ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite;
				goto IL_25;
			default:
				reason = ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite;
				goto IL_25;
			}
			reason = ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead;
			IL_25:
			if (waiterStates != ReaderWriterLockSlim.WaiterStates.None && (this._waiterStates & waiterStates) != ReaderWriterLockSlim.WaiterStates.None)
			{
				this._waiterStates &= ~waiterStates;
			}
			waitEvent.Reset();
			numWaiters += 1U;
			this.HasNoWaiters = false;
			if (this._numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this._numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this._spinLock.Exit();
			try
			{
				flag = waitEvent.WaitOne(timeout.RemainingMilliseconds);
			}
			finally
			{
				this._spinLock.Enter(reason);
				numWaiters -= 1U;
				if (flag && waiterStates != ReaderWriterLockSlim.WaiterStates.None && (this._waiterStates & waiterStates) != ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates &= ~waiterStates;
				}
				if (this._numWriteWaiters == 0U && this._numWriteUpgradeWaiters == 0U && this._numUpgradeWaiters == 0U && this._numReadWaiters == 0U)
				{
					this.HasNoWaiters = true;
				}
				if (this._numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this._numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					if (enterLockType >= ReaderWriterLockSlim.EnterLockType.Write)
					{
						this.ExitAndWakeUpAppropriateReadWaiters();
					}
					else
					{
						this._spinLock.Exit();
					}
				}
			}
			return flag;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00146E38 File Offset: 0x00146038
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.HasNoWaiters)
			{
				this._spinLock.Exit();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00146E54 File Offset: 0x00146054
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			uint numReaders = this.GetNumReaders();
			if (this._fIsReentrant && this._numWriteUpgradeWaiters > 0U && this._fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this._spinLock.Exit();
				this._waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this._numWriteUpgradeWaiters > 0U)
			{
				this._spinLock.Exit();
				this._waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this._numWriteWaiters > 0U)
			{
				ReaderWriterLockSlim.WaiterStates waiterStates = this._waiterStates & ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				if (waiterStates == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.WriteWaiterSignaled;
				}
				this._spinLock.Exit();
				if (waiterStates == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._writeEvent.Set();
					return;
				}
			}
			else
			{
				this.ExitAndWakeUpAppropriateReadWaiters();
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00146F0C File Offset: 0x0014610C
		private void ExitAndWakeUpAppropriateReadWaiters()
		{
			if (this._numWriteWaiters != 0U || this._numWriteUpgradeWaiters != 0U || this.HasNoWaiters)
			{
				this._spinLock.Exit();
				return;
			}
			bool flag = this._numReadWaiters > 0U;
			bool flag2 = this._numUpgradeWaiters != 0U && this._upgradeLockOwnerId == -1;
			if (flag2)
			{
				if ((this._waiterStates & ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled) == ReaderWriterLockSlim.WaiterStates.None)
				{
					this._waiterStates |= ReaderWriterLockSlim.WaiterStates.UpgradeableReadWaiterSignaled;
				}
				else
				{
					flag2 = false;
				}
			}
			this._spinLock.Exit();
			if (flag)
			{
				this._readEvent.Set();
			}
			if (flag2)
			{
				this._upgradeEvent.Set();
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00146FA4 File Offset: 0x001461A4
		private bool IsWriterAcquired()
		{
			return (this._owners & 3221225471U) == 0U;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00146FB5 File Offset: 0x001461B5
		private void SetWriterAcquired()
		{
			this._owners |= 2147483648U;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00146FC9 File Offset: 0x001461C9
		private void ClearWriterAcquired()
		{
			this._owners &= 2147483647U;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00146FDD File Offset: 0x001461DD
		private void SetWritersWaiting()
		{
			this._owners |= 1073741824U;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00146FF1 File Offset: 0x001461F1
		private void ClearWritersWaiting()
		{
			this._owners &= 3221225471U;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x00147005 File Offset: 0x00146205
		private void SetUpgraderWaiting()
		{
			this._owners |= 536870912U;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x00147019 File Offset: 0x00146219
		private void ClearUpgraderWaiting()
		{
			this._owners &= 3758096383U;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0014702D File Offset: 0x0014622D
		private uint GetNumReaders()
		{
			return this._owners & 268435455U;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0014703B File Offset: 0x0014623B
		private bool ShouldSpinForEnterAnyRead()
		{
			return this.HasNoWaiters || (this._numWriteWaiters == 0U && this._numWriteUpgradeWaiters == 0U);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0014705A File Offset: 0x0014625A
		private bool ShouldSpinForEnterAnyWrite(bool isUpgradeToWrite)
		{
			return isUpgradeToWrite || this._numWriteUpgradeWaiters == 0U;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0014706A File Offset: 0x0014626A
		private static void SpinWait(int spinCount)
		{
			if (spinCount < 5 && Environment.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * spinCount);
				return;
			}
			Thread.Sleep(0);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00147088 File Offset: 0x00146288
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x00147094 File Offset: 0x00146294
		private void Dispose(bool disposing)
		{
			if (disposing && !this._fDisposed)
			{
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new SynchronizationLockException(SR.SynchronizationLockException_IncorrectDispose);
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new SynchronizationLockException(SR.SynchronizationLockException_IncorrectDispose);
				}
				if (this._writeEvent != null)
				{
					this._writeEvent.Dispose();
					this._writeEvent = null;
				}
				if (this._readEvent != null)
				{
					this._readEvent.Dispose();
					this._readEvent = null;
				}
				if (this._upgradeEvent != null)
				{
					this._upgradeEvent.Dispose();
					this._upgradeEvent = null;
				}
				if (this._waitUpgradeEvent != null)
				{
					this._waitUpgradeEvent.Dispose();
					this._waitUpgradeEvent = null;
				}
				this._fDisposed = true;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x0014716A File Offset: 0x0014636A
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x00147178 File Offset: 0x00146378
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x00147186 File Offset: 0x00146386
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00147194 File Offset: 0x00146394
		public LockRecursionPolicy RecursionPolicy
		{
			get
			{
				if (this._fIsReentrant)
				{
					return LockRecursionPolicy.SupportsRecursion;
				}
				return LockRecursionPolicy.NoRecursion;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x001471A4 File Offset: 0x001463A4
		public int CurrentReadCount
		{
			get
			{
				int numReaders = (int)this.GetNumReaders();
				if (this._upgradeLockOwnerId != -1)
				{
					return numReaders - 1;
				}
				return numReaders;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x001471C8 File Offset: 0x001463C8
		public int RecursiveReadCount
		{
			get
			{
				int result = 0;
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null)
				{
					result = threadRWCount.readercount;
				}
				return result;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x001471EC File Offset: 0x001463EC
		public int RecursiveUpgradeCount
		{
			get
			{
				if (this._fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.upgradecount;
					}
					return result;
				}
				if (Environment.CurrentManagedThreadId == this._upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x00147228 File Offset: 0x00146428
		public int RecursiveWriteCount
		{
			get
			{
				if (this._fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.writercount;
					}
					return result;
				}
				if (Environment.CurrentManagedThreadId == this._writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00147263 File Offset: 0x00146463
		public int WaitingReadCount
		{
			get
			{
				return (int)this._numReadWaiters;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0014726B File Offset: 0x0014646B
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this._numUpgradeWaiters;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x00147273 File Offset: 0x00146473
		public int WaitingWriteCount
		{
			get
			{
				return (int)this._numWriteWaiters;
			}
		}

		// Token: 0x04000A83 RID: 2691
		private readonly bool _fIsReentrant;

		// Token: 0x04000A84 RID: 2692
		private ReaderWriterLockSlim.SpinLock _spinLock;

		// Token: 0x04000A85 RID: 2693
		private uint _numWriteWaiters;

		// Token: 0x04000A86 RID: 2694
		private uint _numReadWaiters;

		// Token: 0x04000A87 RID: 2695
		private uint _numWriteUpgradeWaiters;

		// Token: 0x04000A88 RID: 2696
		private uint _numUpgradeWaiters;

		// Token: 0x04000A89 RID: 2697
		private ReaderWriterLockSlim.WaiterStates _waiterStates;

		// Token: 0x04000A8A RID: 2698
		private int _upgradeLockOwnerId;

		// Token: 0x04000A8B RID: 2699
		private int _writeLockOwnerId;

		// Token: 0x04000A8C RID: 2700
		private EventWaitHandle _writeEvent;

		// Token: 0x04000A8D RID: 2701
		private EventWaitHandle _readEvent;

		// Token: 0x04000A8E RID: 2702
		private EventWaitHandle _upgradeEvent;

		// Token: 0x04000A8F RID: 2703
		private EventWaitHandle _waitUpgradeEvent;

		// Token: 0x04000A90 RID: 2704
		private static long s_nextLockID;

		// Token: 0x04000A91 RID: 2705
		private readonly long _lockID;

		// Token: 0x04000A92 RID: 2706
		[ThreadStatic]
		private static ReaderWriterCount t_rwc;

		// Token: 0x04000A93 RID: 2707
		private bool _fUpgradeThreadHoldingRead;

		// Token: 0x04000A94 RID: 2708
		private uint _owners;

		// Token: 0x04000A95 RID: 2709
		private bool _fDisposed;

		// Token: 0x020002AA RID: 682
		private struct TimeoutTracker
		{
			// Token: 0x06002814 RID: 10260 RVA: 0x0014727C File Offset: 0x0014647C
			public TimeoutTracker(TimeSpan timeout)
			{
				long num = (long)timeout.TotalMilliseconds;
				if (num < -1L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("timeout");
				}
				this._total = (int)num;
				if (this._total != -1 && this._total != 0)
				{
					this._start = Environment.TickCount;
					return;
				}
				this._start = 0;
			}

			// Token: 0x06002815 RID: 10261 RVA: 0x001472D7 File Offset: 0x001464D7
			public TimeoutTracker(int millisecondsTimeout)
			{
				if (millisecondsTimeout < -1)
				{
					throw new ArgumentOutOfRangeException("millisecondsTimeout");
				}
				this._total = millisecondsTimeout;
				if (this._total != -1 && this._total != 0)
				{
					this._start = Environment.TickCount;
					return;
				}
				this._start = 0;
			}

			// Token: 0x1700085F RID: 2143
			// (get) Token: 0x06002816 RID: 10262 RVA: 0x00147314 File Offset: 0x00146514
			public int RemainingMilliseconds
			{
				get
				{
					if (this._total == -1 || this._total == 0)
					{
						return this._total;
					}
					int num = Environment.TickCount - this._start;
					if (num < 0 || num >= this._total)
					{
						return 0;
					}
					return this._total - num;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x06002817 RID: 10263 RVA: 0x0014735D File Offset: 0x0014655D
			public bool IsExpired
			{
				get
				{
					return this.RemainingMilliseconds == 0;
				}
			}

			// Token: 0x04000A96 RID: 2710
			private readonly int _total;

			// Token: 0x04000A97 RID: 2711
			private readonly int _start;
		}

		// Token: 0x020002AB RID: 683
		private struct SpinLock
		{
			// Token: 0x06002818 RID: 10264 RVA: 0x00147368 File Offset: 0x00146568
			private static int GetEnterDeprioritizationStateChange(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				switch (reason & ReaderWriterLockSlim.EnterSpinLockReason.OperationMask)
				{
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead:
					return 0;
				case ReaderWriterLockSlim.EnterSpinLockReason.ExitAnyRead:
					return 1;
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite:
					return 65536;
				default:
					return 65537;
				}
			}

			// Token: 0x17000861 RID: 2145
			// (get) Token: 0x06002819 RID: 10265 RVA: 0x0014739C File Offset: 0x0014659C
			private ushort EnterForEnterAnyReadDeprioritizedCount
			{
				get
				{
					return (ushort)((uint)this._enterDeprioritizationState >> 16);
				}
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x0600281A RID: 10266 RVA: 0x001473A8 File Offset: 0x001465A8
			private ushort EnterForEnterAnyWriteDeprioritizedCount
			{
				get
				{
					return (ushort)this._enterDeprioritizationState;
				}
			}

			// Token: 0x0600281B RID: 10267 RVA: 0x001473B1 File Offset: 0x001465B1
			private bool IsEnterDeprioritized(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				switch (reason)
				{
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterAnyRead:
					return this.EnterForEnterAnyReadDeprioritizedCount > 0;
				default:
					return false;
				case ReaderWriterLockSlim.EnterSpinLockReason.EnterWrite:
					return this.EnterForEnterAnyWriteDeprioritizedCount > 0;
				case ReaderWriterLockSlim.EnterSpinLockReason.UpgradeToWrite:
					return this.EnterForEnterAnyWriteDeprioritizedCount > 1;
				}
			}

			// Token: 0x0600281C RID: 10268 RVA: 0x001473E8 File Offset: 0x001465E8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private bool TryEnter()
			{
				return Interlocked.CompareExchange(ref this._isLocked, 1, 0) == 0;
			}

			// Token: 0x0600281D RID: 10269 RVA: 0x001473FA File Offset: 0x001465FA
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Enter(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				if (!this.TryEnter())
				{
					this.EnterSpin(reason);
				}
			}

			// Token: 0x0600281E RID: 10270 RVA: 0x0014740C File Offset: 0x0014660C
			private void EnterSpin(ReaderWriterLockSlim.EnterSpinLockReason reason)
			{
				int enterDeprioritizationStateChange = ReaderWriterLockSlim.SpinLock.GetEnterDeprioritizationStateChange(reason);
				if (enterDeprioritizationStateChange != 0)
				{
					Interlocked.Add(ref this._enterDeprioritizationState, enterDeprioritizationStateChange);
				}
				int processorCount = Environment.ProcessorCount;
				int num = 0;
				for (;;)
				{
					if (num < 10 && processorCount > 1)
					{
						Thread.SpinWait(20 * (num + 1));
					}
					else if (num < 15)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Sleep(1);
					}
					if (!this.IsEnterDeprioritized(reason))
					{
						if (this._isLocked == 0 && this.TryEnter())
						{
							break;
						}
					}
					else if (num >= 20)
					{
						reason |= ReaderWriterLockSlim.EnterSpinLockReason.Wait;
						num = -1;
					}
					num++;
				}
				if (enterDeprioritizationStateChange != 0)
				{
					Interlocked.Add(ref this._enterDeprioritizationState, -enterDeprioritizationStateChange);
				}
			}

			// Token: 0x0600281F RID: 10271 RVA: 0x0014749D File Offset: 0x0014669D
			public void Exit()
			{
				Volatile.Write(ref this._isLocked, 0);
			}

			// Token: 0x04000A98 RID: 2712
			private int _isLocked;

			// Token: 0x04000A99 RID: 2713
			private int _enterDeprioritizationState;
		}

		// Token: 0x020002AC RID: 684
		[Flags]
		private enum WaiterStates : byte
		{
			// Token: 0x04000A9B RID: 2715
			None = 0,
			// Token: 0x04000A9C RID: 2716
			NoWaiters = 1,
			// Token: 0x04000A9D RID: 2717
			WriteWaiterSignaled = 2,
			// Token: 0x04000A9E RID: 2718
			UpgradeableReadWaiterSignaled = 4
		}

		// Token: 0x020002AD RID: 685
		private enum EnterSpinLockReason
		{
			// Token: 0x04000AA0 RID: 2720
			EnterAnyRead,
			// Token: 0x04000AA1 RID: 2721
			ExitAnyRead,
			// Token: 0x04000AA2 RID: 2722
			EnterWrite,
			// Token: 0x04000AA3 RID: 2723
			UpgradeToWrite,
			// Token: 0x04000AA4 RID: 2724
			EnterRecursiveWrite,
			// Token: 0x04000AA5 RID: 2725
			ExitAnyWrite,
			// Token: 0x04000AA6 RID: 2726
			OperationMask = 7,
			// Token: 0x04000AA7 RID: 2727
			Wait
		}

		// Token: 0x020002AE RID: 686
		private enum EnterLockType
		{
			// Token: 0x04000AA9 RID: 2729
			Read,
			// Token: 0x04000AAA RID: 2730
			UpgradeableRead,
			// Token: 0x04000AAB RID: 2731
			Write,
			// Token: 0x04000AAC RID: 2732
			UpgradeToWrite
		}
	}
}
