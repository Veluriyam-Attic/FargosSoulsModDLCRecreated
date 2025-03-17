using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x0200028B RID: 651
	public readonly struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable, IAsyncDisposable
	{
		// Token: 0x06002721 RID: 10017 RVA: 0x00143FA1 File Offset: 0x001431A1
		internal CancellationTokenRegistration(long id, CancellationTokenSource.CallbackNode node)
		{
			this._id = id;
			this._node = node;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00143FB4 File Offset: 0x001431B4
		public void Dispose()
		{
			CancellationTokenSource.CallbackNode node = this._node;
			if (node != null && !node.Partition.Unregister(this._id, node))
			{
				this.WaitForCallbackIfNecessary();
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00143FE8 File Offset: 0x001431E8
		public ValueTask DisposeAsync()
		{
			CancellationTokenSource.CallbackNode node = this._node;
			if (node == null || node.Partition.Unregister(this._id, node))
			{
				return default(ValueTask);
			}
			return this.WaitForCallbackIfNecessaryAsync();
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x00144024 File Offset: 0x00143224
		public CancellationToken Token
		{
			get
			{
				CancellationTokenSource.CallbackNode node = this._node;
				if (node == null)
				{
					return default(CancellationToken);
				}
				return new CancellationToken(node.Partition.Source);
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00144058 File Offset: 0x00143258
		public bool Unregister()
		{
			CancellationTokenSource.CallbackNode node = this._node;
			return node != null && node.Partition.Unregister(this._id, node);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00144084 File Offset: 0x00143284
		private void WaitForCallbackIfNecessary()
		{
			CancellationTokenSource source = this._node.Partition.Source;
			if (source.IsCancellationRequested && !source.IsCancellationCompleted && source.ThreadIDExecutingCallbacks != Environment.CurrentManagedThreadId)
			{
				source.WaitForCallbackToComplete(this._id);
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x001440CC File Offset: 0x001432CC
		private ValueTask WaitForCallbackIfNecessaryAsync()
		{
			CancellationTokenSource source = this._node.Partition.Source;
			if (source.IsCancellationRequested && !source.IsCancellationCompleted && source.ThreadIDExecutingCallbacks != Environment.CurrentManagedThreadId)
			{
				return source.WaitForCallbackToCompleteAsync(this._id);
			}
			return default(ValueTask);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0014411D File Offset: 0x0014331D
		public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00144127 File Offset: 0x00143327
		public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00144134 File Offset: 0x00143334
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CancellationTokenRegistration && this.Equals((CancellationTokenRegistration)obj);
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0014414C File Offset: 0x0014334C
		public bool Equals(CancellationTokenRegistration other)
		{
			return this._node == other._node && this._id == other._id;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0014416C File Offset: 0x0014336C
		public override int GetHashCode()
		{
			if (this._node == null)
			{
				return this._id.GetHashCode();
			}
			return this._node.GetHashCode() ^ this._id.GetHashCode();
		}

		// Token: 0x04000A3D RID: 2621
		private readonly long _id;

		// Token: 0x04000A3E RID: 2622
		private readonly CancellationTokenSource.CallbackNode _node;
	}
}
