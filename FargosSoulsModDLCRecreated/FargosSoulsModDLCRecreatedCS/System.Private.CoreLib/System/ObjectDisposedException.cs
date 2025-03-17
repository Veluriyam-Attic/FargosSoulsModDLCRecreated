using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000164 RID: 356
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x000E65F4 File Offset: 0x000E57F4
		private ObjectDisposedException() : this(null, SR.ObjectDisposed_Generic)
		{
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x000E6602 File Offset: 0x000E5802
		[NullableContext(2)]
		public ObjectDisposedException(string objectName) : this(objectName, SR.ObjectDisposed_Generic)
		{
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000E6610 File Offset: 0x000E5810
		[NullableContext(2)]
		public ObjectDisposedException(string objectName, string message) : base(message)
		{
			base.HResult = -2146232798;
			this._objectName = objectName;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000E662B File Offset: 0x000E582B
		[NullableContext(2)]
		public ObjectDisposedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232798;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000E6640 File Offset: 0x000E5840
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._objectName = info.GetString("ObjectName");
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000E665B File Offset: 0x000E585B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x000E6680 File Offset: 0x000E5880
		public override string Message
		{
			get
			{
				string objectName = this.ObjectName;
				if (string.IsNullOrEmpty(objectName))
				{
					return base.Message;
				}
				string str = SR.Format(SR.ObjectDisposed_ObjectName_Name, objectName);
				return base.Message + "\r\n" + str;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000E66C0 File Offset: 0x000E58C0
		public string ObjectName
		{
			get
			{
				return this._objectName ?? string.Empty;
			}
		}

		// Token: 0x0400045A RID: 1114
		private readonly string _objectName;
	}
}
