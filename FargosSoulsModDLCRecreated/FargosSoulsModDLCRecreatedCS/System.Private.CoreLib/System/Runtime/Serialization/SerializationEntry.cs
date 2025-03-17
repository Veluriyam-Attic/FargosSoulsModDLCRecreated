using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003EB RID: 1003
	[Nullable(0)]
	[NullableContext(1)]
	public readonly struct SerializationEntry
	{
		// Token: 0x0600325B RID: 12891 RVA: 0x0016B140 File Offset: 0x0016A340
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this._name = entryName;
			this._value = entryValue;
			this._type = entryType;
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x0016B157 File Offset: 0x0016A357
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x0016B15F File Offset: 0x0016A35F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x0016B167 File Offset: 0x0016A367
		public Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000E01 RID: 3585
		private readonly string _name;

		// Token: 0x04000E02 RID: 3586
		private readonly object _value;

		// Token: 0x04000E03 RID: 3587
		private readonly Type _type;
	}
}
