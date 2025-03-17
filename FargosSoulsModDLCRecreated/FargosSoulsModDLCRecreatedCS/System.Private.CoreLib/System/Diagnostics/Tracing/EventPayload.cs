using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000762 RID: 1890
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06005C8E RID: 23694 RVA: 0x001C1945 File Offset: 0x001C0B45
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x001C195B File Offset: 0x001C0B5B
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x001C1963 File Offset: 0x001C0B63
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17000F1E RID: 3870
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				foreach (string a in this.m_names)
				{
					if (a == key)
					{
						return this.m_values[num];
					}
					num++;
				}
				throw new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key));
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x000C279F File Offset: 0x000C199F
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x000C279F File Offset: 0x000C199F
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x000C279F File Offset: 0x000C199F
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x001C19F8 File Offset: 0x001C0BF8
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x001C1A08 File Offset: 0x001C0C08
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			foreach (string a in this.m_names)
			{
				if (a == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x001C1A74 File Offset: 0x001C0C74
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005C99 RID: 23705 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x001C1A81 File Offset: 0x001C0C81
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Keys.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, object>(this.m_names[i], this.m_values[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x001C1A90 File Offset: 0x001C0C90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x000C279F File Offset: 0x000C199F
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x000C279F File Offset: 0x000C199F
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x000C279F File Offset: 0x000C199F
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x001C1AA8 File Offset: 0x001C0CA8
		public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			foreach (string a in this.m_names)
			{
				if (a == key)
				{
					value = this.m_values[num];
					return true;
				}
				num++;
			}
			value = null;
			return false;
		}

		// Token: 0x04001BCF RID: 7119
		private readonly List<string> m_names;

		// Token: 0x04001BD0 RID: 7120
		private readonly List<object> m_values;
	}
}
