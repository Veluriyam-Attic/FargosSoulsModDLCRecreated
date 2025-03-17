using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Resources
{
	// Token: 0x02000570 RID: 1392
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x060047CD RID: 18381 RVA: 0x0017EB04 File Offset: 0x0017DD04
		public int GetHashCode(object key)
		{
			string key2 = (string)key;
			return FastResourceComparer.HashFunction(key2);
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0017EB1E File Offset: 0x0017DD1E
		public int GetHashCode([DisallowNull] string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0017EB28 File Offset: 0x0017DD28
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num ^ (uint)key[i]);
			}
			return (int)num;
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0017EB5C File Offset: 0x0017DD5C
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string strA = (string)a;
			string strB = (string)b;
			return string.CompareOrdinal(strA, strB);
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x000F291D File Offset: 0x000F1B1D
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000F2926 File Offset: 0x000F1B26
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0017EB84 File Offset: 0x0017DD84
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string a2 = (string)a;
			string b2 = (string)b;
			return string.Equals(a2, b2);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0017EBAC File Offset: 0x0017DDAC
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | (int)ptr2[1] << 8;
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0017EC32 File Offset: 0x0017DE32
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0017EC40 File Offset: 0x0017DE40
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				char c = (char)((int)(*(a++)) | (int)(*(a++)) << 8);
				num = (int)(c - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x0400115A RID: 4442
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
