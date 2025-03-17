using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000465 RID: 1125
	[Nullable(0)]
	[NullableContext(2)]
	public struct ArrayWithOffset
	{
		// Token: 0x06004434 RID: 17460 RVA: 0x00178C10 File Offset: 0x00177E10
		public ArrayWithOffset(object array, int offset)
		{
			int num = 0;
			if (array != null)
			{
				Array array2 = array as Array;
				if (array2 == null || array2.Rank != 1 || !Marshal.IsPinnable(array2))
				{
					throw new ArgumentException(SR.ArgumentException_NotIsomorphic);
				}
				UIntPtr uintPtr = (UIntPtr)array2.LongLength * (UIntPtr)array2.GetElementSize();
				if (uintPtr > (UIntPtr)((IntPtr)2147483632))
				{
					throw new ArgumentException(SR.Argument_StructArrayTooLarge);
				}
				num = (int)uintPtr;
			}
			if (offset > num)
			{
				throw new IndexOutOfRangeException(SR.IndexOutOfRange_ArrayWithOffset);
			}
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = num - offset;
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x00178C95 File Offset: 0x00177E95
		public object GetArray()
		{
			return this.m_array;
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x00178C9D File Offset: 0x00177E9D
		public int GetOffset()
		{
			return this.m_offset;
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00178CA5 File Offset: 0x00177EA5
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00178CB4 File Offset: 0x00177EB4
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00178CCC File Offset: 0x00177ECC
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00178CFA File Offset: 0x00177EFA
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00178D04 File Offset: 0x00177F04
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x04000EFF RID: 3839
		private readonly object m_array;

		// Token: 0x04000F00 RID: 3840
		private readonly int m_offset;

		// Token: 0x04000F01 RID: 3841
		private readonly int m_count;
	}
}
