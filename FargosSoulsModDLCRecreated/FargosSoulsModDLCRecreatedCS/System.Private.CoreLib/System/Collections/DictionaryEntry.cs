using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007AC RID: 1964
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public struct DictionaryEntry
	{
		// Token: 0x06005F1B RID: 24347 RVA: 0x001C8996 File Offset: 0x001C7B96
		public DictionaryEntry(object key, [Nullable(2)] object value)
		{
			this._key = key;
			this._value = value;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x001C89A6 File Offset: 0x001C7BA6
		// (set) Token: 0x06005F1D RID: 24349 RVA: 0x001C89AE File Offset: 0x001C7BAE
		public object Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x001C89B7 File Offset: 0x001C7BB7
		// (set) Token: 0x06005F1F RID: 24351 RVA: 0x001C89BF File Offset: 0x001C7BBF
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
			[NullableContext(2)]
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x001C89C8 File Offset: 0x001C7BC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out object key, [Nullable(2)] out object value)
		{
			key = this.Key;
			value = this.Value;
		}

		// Token: 0x04001CB7 RID: 7351
		private object _key;

		// Token: 0x04001CB8 RID: 7352
		private object _value;
	}
}
