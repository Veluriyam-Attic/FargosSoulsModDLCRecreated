using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x020003D8 RID: 984
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x0016A193 File Offset: 0x00169393
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x0016A19F File Offset: 0x0016939F
		private static long LastKnownFreeAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.s_hiddenLastKnownFreeAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.s_hiddenLastKnownFreeAddressSpace, value);
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x0016A1AC File Offset: 0x001693AC
		private static void AddToLastKnownFreeAddressSpace(long addend)
		{
			Interlocked.Add(ref MemoryFailPoint.s_hiddenLastKnownFreeAddressSpace, addend);
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x0016A1BA File Offset: 0x001693BA
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x0016A1C6 File Offset: 0x001693C6
		private static long LastTimeCheckingAddressSpace
		{
			get
			{
				return Volatile.Read(ref MemoryFailPoint.s_hiddenLastTimeCheckingAddressSpace);
			}
			set
			{
				Volatile.Write(ref MemoryFailPoint.s_hiddenLastTimeCheckingAddressSpace, value);
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x0016A1D4 File Offset: 0x001693D4
		public MemoryFailPoint(int sizeInMegabytes)
		{
			if (sizeInMegabytes <= 0)
			{
				throw new ArgumentOutOfRangeException("sizeInMegabytes", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			ulong num = (ulong)((ulong)((long)sizeInMegabytes) << 20);
			this._reservedMemory = num;
			ulong num2 = (ulong)(Math.Ceiling(num / MemoryFailPoint.s_GCSegmentSize) * MemoryFailPoint.s_GCSegmentSize);
			if (num2 >= MemoryFailPoint.s_topOfMemory)
			{
				throw new InsufficientMemoryException(SR.InsufficientMemory_MemFailPoint_TooBig);
			}
			ulong num3 = (ulong)(Math.Ceiling((double)sizeInMegabytes / 16.0) * 16.0);
			num3 <<= 20;
			for (int i = 0; i < 3; i++)
			{
				ulong num4;
				ulong num5;
				if (!MemoryFailPoint.CheckForAvailableMemory(out num4, out num5))
				{
					return;
				}
				ulong memoryFailPointReservedMemory = MemoryFailPoint.MemoryFailPointReservedMemory;
				ulong num6 = num2 + memoryFailPointReservedMemory;
				bool flag = num6 < num2 || num6 < memoryFailPointReservedMemory;
				bool flag2 = num4 < num3 + memoryFailPointReservedMemory + 16777216UL || flag;
				bool flag3 = num5 < num6 || flag;
				long num7 = (long)Environment.TickCount;
				if (num7 > MemoryFailPoint.LastTimeCheckingAddressSpace + 10000L || num7 < MemoryFailPoint.LastTimeCheckingAddressSpace || MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2)
				{
					MemoryFailPoint.CheckForFreeAddressSpace(num2, false);
				}
				bool flag4 = MemoryFailPoint.LastKnownFreeAddressSpace < (long)num2;
				if (!flag2 && !flag3 && !flag4)
				{
					break;
				}
				switch (i)
				{
				case 0:
					GC.Collect();
					break;
				case 1:
					if (flag2)
					{
						UIntPtr numBytes = new UIntPtr(num2);
						MemoryFailPoint.GrowPageFileIfNecessaryAndPossible(numBytes);
					}
					break;
				case 2:
					if (flag2 || flag3)
					{
						InsufficientMemoryException ex = new InsufficientMemoryException(SR.InsufficientMemory_MemFailPoint);
						throw ex;
					}
					if (flag4)
					{
						InsufficientMemoryException ex2 = new InsufficientMemoryException(SR.InsufficientMemory_MemFailPoint_VAFrag);
						throw ex2;
					}
					break;
				}
			}
			MemoryFailPoint.AddToLastKnownFreeAddressSpace((long)(-(long)num));
			if (MemoryFailPoint.LastKnownFreeAddressSpace < 0L)
			{
				MemoryFailPoint.CheckForFreeAddressSpace(num2, true);
			}
			MemoryFailPoint.AddMemoryFailPointReservation((long)num);
			this._mustSubtractReservation = true;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x0016A378 File Offset: 0x00169578
		~MemoryFailPoint()
		{
			this.Dispose(false);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x0016A3A8 File Offset: 0x001695A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x0016A3B7 File Offset: 0x001695B7
		private void Dispose(bool disposing)
		{
			if (this._mustSubtractReservation)
			{
				MemoryFailPoint.AddMemoryFailPointReservation((long)(-(long)this._reservedMemory));
				this._mustSubtractReservation = false;
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x0016A3D5 File Offset: 0x001695D5
		internal static long AddMemoryFailPointReservation(long size)
		{
			return Interlocked.Add(ref MemoryFailPoint.s_failPointReservedMemory, size);
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0016A3E2 File Offset: 0x001695E2
		internal static ulong MemoryFailPointReservedMemory
		{
			get
			{
				return (ulong)Volatile.Read(ref MemoryFailPoint.s_failPointReservedMemory);
			}
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0016A3F0 File Offset: 0x001695F0
		private static ulong GetTopOfMemory()
		{
			Interop.Kernel32.SYSTEM_INFO system_INFO;
			Interop.Kernel32.GetSystemInfo(out system_INFO);
			return (ulong)((long)system_INFO.lpMaximumApplicationAddress);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0016A410 File Offset: 0x00169610
		private static bool CheckForAvailableMemory(out ulong availPageFile, out ulong totalAddressSpaceFree)
		{
			Interop.Kernel32.MEMORYSTATUSEX memorystatusex = default(Interop.Kernel32.MEMORYSTATUSEX);
			memorystatusex.dwLength = (uint)sizeof(Interop.Kernel32.MEMORYSTATUSEX);
			if (!Interop.Kernel32.GlobalMemoryStatusEx(ref memorystatusex))
			{
				availPageFile = 0UL;
				totalAddressSpaceFree = 0UL;
				return false;
			}
			availPageFile = memorystatusex.ullAvailPageFile;
			totalAddressSpaceFree = memorystatusex.ullAvailVirtual;
			return true;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x0016A458 File Offset: 0x00169658
		private static void CheckForFreeAddressSpace(ulong size, bool shouldThrow)
		{
			ulong num = MemoryFailPoint.MemFreeAfterAddress(null, size);
			MemoryFailPoint.LastKnownFreeAddressSpace = (long)num;
			MemoryFailPoint.LastTimeCheckingAddressSpace = (long)Environment.TickCount;
			if (num < size && shouldThrow)
			{
				throw new InsufficientMemoryException(SR.InsufficientMemory_MemFailPoint_VAFrag);
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x0016A494 File Offset: 0x00169694
		private unsafe static ulong MemFreeAfterAddress(void* address, ulong size)
		{
			if (size >= MemoryFailPoint.s_topOfMemory)
			{
				return 0UL;
			}
			ulong num = 0UL;
			Interop.Kernel32.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION = default(Interop.Kernel32.MEMORY_BASIC_INFORMATION);
			UIntPtr dwLength = (UIntPtr)((ulong)((long)sizeof(Interop.Kernel32.MEMORY_BASIC_INFORMATION)));
			while ((byte*)address + size < MemoryFailPoint.s_topOfMemory)
			{
				UIntPtr value = Interop.Kernel32.VirtualQuery(address, ref memory_BASIC_INFORMATION, dwLength);
				if (value == UIntPtr.Zero)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error("");
				}
				ulong num2 = memory_BASIC_INFORMATION.RegionSize.ToUInt64();
				if (memory_BASIC_INFORMATION.State == 65536U)
				{
					if (num2 >= size)
					{
						return num2;
					}
					num = Math.Max(num, num2);
				}
				address = (void*)((byte*)address + num2);
			}
			return num;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0016A528 File Offset: 0x00169728
		private unsafe static void GrowPageFileIfNecessaryAndPossible(UIntPtr numBytes)
		{
			void* ptr = Interop.Kernel32.VirtualAlloc(null, numBytes, 4096, 4);
			if (ptr != null && !Interop.Kernel32.VirtualFree(ptr, UIntPtr.Zero, 32768))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
		}

		// Token: 0x04000DE8 RID: 3560
		private static readonly ulong s_topOfMemory = MemoryFailPoint.GetTopOfMemory();

		// Token: 0x04000DE9 RID: 3561
		private static long s_hiddenLastKnownFreeAddressSpace;

		// Token: 0x04000DEA RID: 3562
		private static long s_hiddenLastTimeCheckingAddressSpace;

		// Token: 0x04000DEB RID: 3563
		private static readonly ulong s_GCSegmentSize = GC.GetSegmentSize();

		// Token: 0x04000DEC RID: 3564
		private static long s_failPointReservedMemory;

		// Token: 0x04000DED RID: 3565
		private readonly ulong _reservedMemory;

		// Token: 0x04000DEE RID: 3566
		private bool _mustSubtractReservation;
	}
}
