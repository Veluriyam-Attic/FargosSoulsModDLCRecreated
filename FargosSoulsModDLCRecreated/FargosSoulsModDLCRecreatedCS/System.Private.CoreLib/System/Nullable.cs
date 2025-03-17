using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000158 RID: 344
	[NonVersionable]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x000DEF88 File Offset: 0x000DE188
		[NonVersionable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000DEF98 File Offset: 0x000DE198
		public readonly bool HasValue
		{
			[NonVersionable]
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000DEFA0 File Offset: 0x000DE1A0
		public readonly T Value
		{
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_NoValue();
				}
				return this.value;
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000DEFB5 File Offset: 0x000DE1B5
		[NonVersionable]
		public readonly T GetValueOrDefault()
		{
			return this.value;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000DEFBD File Offset: 0x000DE1BD
		[NonVersionable]
		public readonly T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000DEFCF File Offset: 0x000DE1CF
		[NullableContext(2)]
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000DEFF5 File Offset: 0x000DE1F5
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000DF012 File Offset: 0x000DE212
		[NullableContext(2)]
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000DF033 File Offset: 0x000DE233
		[NonVersionable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000DF03B File Offset: 0x000DE23B
		[NonVersionable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x04000427 RID: 1063
		private readonly bool hasValue;

		// Token: 0x04000428 RID: 1064
		internal T value;
	}
}
