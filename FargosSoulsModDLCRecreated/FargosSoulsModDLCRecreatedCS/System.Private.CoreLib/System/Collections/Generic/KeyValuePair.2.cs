using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000804 RID: 2052
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct KeyValuePair<[Nullable(2)] TKey, [Nullable(2)] TValue>
	{
		// Token: 0x060061C1 RID: 25025 RVA: 0x001D2354 File Offset: 0x001D1554
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060061C2 RID: 25026 RVA: 0x001D2364 File Offset: 0x001D1564
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060061C3 RID: 25027 RVA: 0x001D236C File Offset: 0x001D156C
		public TValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x001D2374 File Offset: 0x001D1574
		public override string ToString()
		{
			return KeyValuePair.PairToString(this.Key, this.Value);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x001D2391 File Offset: 0x001D1591
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out TKey key, out TValue value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04001D3A RID: 7482
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TKey key;

		// Token: 0x04001D3B RID: 7483
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly TValue value;
	}
}
