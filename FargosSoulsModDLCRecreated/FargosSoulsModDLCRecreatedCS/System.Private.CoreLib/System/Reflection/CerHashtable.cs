using System;
using System.Collections;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005B4 RID: 1460
	internal struct CerHashtable<K, V> where K : class
	{
		// Token: 0x06004BBF RID: 19391 RVA: 0x0018AE5C File Offset: 0x0018A05C
		private static int GetHashCodeHelper(K key)
		{
			string text = key as string;
			if (text == null)
			{
				return key.GetHashCode();
			}
			return text.GetNonRandomizedHashCode();
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0018AE8C File Offset: 0x0018A08C
		private void Rehash(int newSize)
		{
			CerHashtable<K, V>.Table table = new CerHashtable<K, V>.Table(newSize);
			CerHashtable<K, V>.Table table2 = this.m_Table;
			if (table2 != null)
			{
				K[] keys = table2.m_keys;
				V[] values = table2.m_values;
				for (int i = 0; i < keys.Length; i++)
				{
					K k = keys[i];
					if (k != null)
					{
						table.Insert(k, values[i]);
					}
				}
			}
			Volatile.Write<CerHashtable<K, V>.Table>(ref this.m_Table, table);
		}

		// Token: 0x17000BE3 RID: 3043
		internal V this[K key]
		{
			get
			{
				CerHashtable<K, V>.Table table = Volatile.Read<CerHashtable<K, V>.Table>(ref this.m_Table);
				if (table == null)
				{
					return default(V);
				}
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = table.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = Volatile.Read<K>(ref keys[num2]);
					if (k == null)
					{
						goto IL_7F;
					}
					if (k.Equals(key))
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				return table.m_values[num2];
				IL_7F:
				return default(V);
			}
			set
			{
				CerHashtable<K, V>.Table table = this.m_Table;
				if (table != null)
				{
					int num = 2 * (table.m_count + 1);
					if (num >= table.m_keys.Length)
					{
						this.Rehash(num);
					}
				}
				else
				{
					this.Rehash(7);
				}
				this.m_Table.Insert(key, value);
			}
		}

		// Token: 0x040012CD RID: 4813
		private CerHashtable<K, V>.Table m_Table;

		// Token: 0x020005B5 RID: 1461
		private class Table
		{
			// Token: 0x06004BC3 RID: 19395 RVA: 0x0018AFDE File Offset: 0x0018A1DE
			internal Table(int size)
			{
				size = HashHelpers.GetPrime(size);
				this.m_keys = new K[size];
				this.m_values = new V[size];
			}

			// Token: 0x06004BC4 RID: 19396 RVA: 0x0018B008 File Offset: 0x0018A208
			internal void Insert(K key, V value)
			{
				int num = CerHashtable<K, V>.GetHashCodeHelper(key);
				if (num < 0)
				{
					num = ~num;
				}
				K[] keys = this.m_keys;
				int num2 = num % keys.Length;
				for (;;)
				{
					K k = keys[num2];
					if (k == null)
					{
						break;
					}
					num2++;
					if (num2 >= keys.Length)
					{
						num2 -= keys.Length;
					}
				}
				this.m_count++;
				this.m_values[num2] = value;
				Volatile.Write<K>(ref keys[num2], key);
			}

			// Token: 0x040012CE RID: 4814
			internal K[] m_keys;

			// Token: 0x040012CF RID: 4815
			internal V[] m_values;

			// Token: 0x040012D0 RID: 4816
			internal int m_count;
		}
	}
}
