using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001AE RID: 430
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x000FC472 File Offset: 0x000FB672
		private TypeInitializationException() : base(SR.TypeInitialization_Default)
		{
			base.HResult = -2146233036;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000FC48A File Offset: 0x000FB68A
		[NullableContext(2)]
		public TypeInitializationException(string fullTypeName, Exception innerException) : this(fullTypeName, SR.Format(SR.TypeInitialization_Type, fullTypeName), innerException)
		{
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000FC49F File Offset: 0x000FB69F
		internal TypeInitializationException(string message) : base(message)
		{
			base.HResult = -2146233036;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000FC4B3 File Offset: 0x000FB6B3
		internal TypeInitializationException(string fullTypeName, string message, Exception innerException) : base(message, innerException)
		{
			this._typeName = fullTypeName;
			base.HResult = -2146233036;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000FC4CF File Offset: 0x000FB6CF
		private TypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x000FC4EA File Offset: 0x000FB6EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x000FC50F File Offset: 0x000FB70F
		public string TypeName
		{
			get
			{
				return this._typeName ?? string.Empty;
			}
		}

		// Token: 0x040005CA RID: 1482
		private readonly string _typeName;
	}
}
