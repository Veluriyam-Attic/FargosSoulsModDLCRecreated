using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000094 RID: 148
	internal readonly struct MdUtf8String
	{
		// Token: 0x06000678 RID: 1656
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern bool EqualsCaseInsensitive(void* szLhs, void* szRhs, int cSz);

		// Token: 0x06000679 RID: 1657
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern uint HashCaseInsensitive(void* sz, int cSz);

		// Token: 0x0600067A RID: 1658 RVA: 0x000BE410 File Offset: 0x000BD610
		internal unsafe MdUtf8String(void* pStringHeap)
		{
			if (pStringHeap != null)
			{
				this.m_StringHeapByteLength = string.strlen((byte*)pStringHeap);
			}
			else
			{
				this.m_StringHeapByteLength = 0;
			}
			this.m_pStringHeap = (byte*)pStringHeap;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000BE440 File Offset: 0x000BD640
		internal unsafe MdUtf8String(byte* pUtf8String, int cUtf8String)
		{
			this.m_pStringHeap = pUtf8String;
			this.m_StringHeapByteLength = cUtf8String;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000BE450 File Offset: 0x000BD650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool Equals(MdUtf8String s)
		{
			return s.m_StringHeapByteLength == this.m_StringHeapByteLength && SpanHelpers.SequenceEqual(ref *s.m_pStringHeap, ref *this.m_pStringHeap, (UIntPtr)this.m_StringHeapByteLength);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000BE47A File Offset: 0x000BD67A
		internal unsafe bool EqualsCaseInsensitive(MdUtf8String s)
		{
			return s.m_StringHeapByteLength == this.m_StringHeapByteLength && (this.m_StringHeapByteLength == 0 || MdUtf8String.EqualsCaseInsensitive((void*)s.m_pStringHeap, (void*)this.m_pStringHeap, this.m_StringHeapByteLength));
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000BE4AD File Offset: 0x000BD6AD
		internal unsafe uint HashCaseInsensitive()
		{
			return MdUtf8String.HashCaseInsensitive((void*)this.m_pStringHeap, this.m_StringHeapByteLength);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000BE4C0 File Offset: 0x000BD6C0
		public unsafe override string ToString()
		{
			return Encoding.UTF8.GetString(new ReadOnlySpan<byte>((void*)this.m_pStringHeap, this.m_StringHeapByteLength));
		}

		// Token: 0x0400020D RID: 525
		private unsafe readonly byte* m_pStringHeap;

		// Token: 0x0400020E RID: 526
		private readonly int m_StringHeapByteLength;
	}
}
