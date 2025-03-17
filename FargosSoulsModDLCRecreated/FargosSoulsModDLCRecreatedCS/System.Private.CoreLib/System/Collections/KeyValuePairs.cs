using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x020007C0 RID: 1984
	[DebuggerDisplay("{_value}", Name = "[{_key}]")]
	internal class KeyValuePairs
	{
		// Token: 0x06005FB6 RID: 24502 RVA: 0x001CA5A3 File Offset: 0x001C97A3
		public KeyValuePairs(object key, object value)
		{
			this._value = value;
			this._key = key;
		}

		// Token: 0x04001CD3 RID: 7379
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _key;

		// Token: 0x04001CD4 RID: 7380
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _value;
	}
}
