using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TypeLoadException : SystemException, ISerializable
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x000C3D5B File Offset: 0x000C2F5B
		private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId) : base(null)
		{
			base.HResult = -2146233054;
			this._className = className;
			this._assemblyName = assemblyName;
			this._messageArg = messageArg;
			this._resourceId = resourceId;
			this.SetMessageField();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000C3D94 File Offset: 0x000C2F94
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._className == null && this._resourceId == 0)
				{
					this._message = SR.Arg_TypeLoadException;
					return;
				}
				if (this._assemblyName == null)
				{
					this._assemblyName = SR.IO_UnknownFileName;
				}
				if (this._className == null)
				{
					this._className = SR.IO_UnknownFileName;
				}
				string format = null;
				TypeLoadException.GetTypeLoadExceptionMessage(this._resourceId, new StringHandleOnStack(ref format));
				this._message = string.Format(format, this._className, this._assemblyName, this._messageArg);
			}
		}

		// Token: 0x0600084E RID: 2126
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeLoadExceptionMessage(int resourceId, StringHandleOnStack retString);

		// Token: 0x0600084F RID: 2127 RVA: 0x000C3E1D File Offset: 0x000C301D
		public TypeLoadException() : base(SR.Arg_TypeLoadException)
		{
			base.HResult = -2146233054;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000C3E35 File Offset: 0x000C3035
		[NullableContext(2)]
		public TypeLoadException(string message) : base(message)
		{
			base.HResult = -2146233054;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000C3E49 File Offset: 0x000C3049
		[NullableContext(2)]
		public TypeLoadException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233054;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000C3E5E File Offset: 0x000C305E
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000C3E6C File Offset: 0x000C306C
		public string TypeName
		{
			get
			{
				return this._className ?? string.Empty;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000C3E80 File Offset: 0x000C3080
		protected TypeLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._className = info.GetString("TypeLoadClassName");
			this._assemblyName = info.GetString("TypeLoadAssemblyName");
			this._messageArg = info.GetString("TypeLoadMessageArg");
			this._resourceId = info.GetInt32("TypeLoadResourceID");
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000C3EDC File Offset: 0x000C30DC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this._className, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this._assemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", this._messageArg, typeof(string));
			info.AddValue("TypeLoadResourceID", this._resourceId);
		}

		// Token: 0x0400021E RID: 542
		private string _className;

		// Token: 0x0400021F RID: 543
		private string _assemblyName;

		// Token: 0x04000220 RID: 544
		private readonly string _messageArg;

		// Token: 0x04000221 RID: 545
		private readonly int _resourceId;
	}
}
