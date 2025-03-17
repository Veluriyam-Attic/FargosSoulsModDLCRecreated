using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020003BF RID: 959
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SecurityException : SystemException
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x00169C5C File Offset: 0x00168E5C
		public SecurityException() : base(SR.Arg_SecurityException)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x00169C74 File Offset: 0x00168E74
		public SecurityException(string message) : base(message)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00169C88 File Offset: 0x00168E88
		public SecurityException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x00169C9D File Offset: 0x00168E9D
		public SecurityException(string message, Type type) : base(message)
		{
			base.HResult = -2146233078;
			this.PermissionType = type;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x00169CB8 File Offset: 0x00168EB8
		public SecurityException(string message, Type type, string state) : base(message)
		{
			base.HResult = -2146233078;
			this.PermissionType = type;
			this.PermissionState = state;
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x00169CDC File Offset: 0x00168EDC
		[NullableContext(1)]
		protected SecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Demanded = (string)info.GetValueNoThrow("Demanded", typeof(string));
			this.GrantedSet = (string)info.GetValueNoThrow("GrantedSet", typeof(string));
			this.RefusedSet = (string)info.GetValueNoThrow("RefusedSet", typeof(string));
			this.DenySetInstance = (string)info.GetValueNoThrow("Denied", typeof(string));
			this.PermitOnlySetInstance = (string)info.GetValueNoThrow("PermitOnly", typeof(string));
			this.Url = (string)info.GetValueNoThrow("Url", typeof(string));
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00169DB1 File Offset: 0x00168FB1
		[NullableContext(1)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00169DBC File Offset: 0x00168FBC
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Demanded", this.Demanded, typeof(string));
			info.AddValue("GrantedSet", this.GrantedSet, typeof(string));
			info.AddValue("RefusedSet", this.RefusedSet, typeof(string));
			info.AddValue("Denied", this.DenySetInstance, typeof(string));
			info.AddValue("PermitOnly", this.PermitOnlySetInstance, typeof(string));
			info.AddValue("Url", this.Url, typeof(string));
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x00169E73 File Offset: 0x00169073
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x00169E7B File Offset: 0x0016907B
		public object Demanded { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x00169E84 File Offset: 0x00169084
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x00169E8C File Offset: 0x0016908C
		public object DenySetInstance { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x00169E95 File Offset: 0x00169095
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x00169E9D File Offset: 0x0016909D
		public AssemblyName FailedAssemblyInfo { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x00169EA6 File Offset: 0x001690A6
		// (set) Token: 0x06003189 RID: 12681 RVA: 0x00169EAE File Offset: 0x001690AE
		public string GrantedSet { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x00169EB7 File Offset: 0x001690B7
		// (set) Token: 0x0600318B RID: 12683 RVA: 0x00169EBF File Offset: 0x001690BF
		public MethodInfo Method { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x00169EC8 File Offset: 0x001690C8
		// (set) Token: 0x0600318D RID: 12685 RVA: 0x00169ED0 File Offset: 0x001690D0
		public string PermissionState { get; set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x00169ED9 File Offset: 0x001690D9
		// (set) Token: 0x0600318F RID: 12687 RVA: 0x00169EE1 File Offset: 0x001690E1
		public Type PermissionType { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x00169EEA File Offset: 0x001690EA
		// (set) Token: 0x06003191 RID: 12689 RVA: 0x00169EF2 File Offset: 0x001690F2
		public object PermitOnlySetInstance { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x00169EFB File Offset: 0x001690FB
		// (set) Token: 0x06003193 RID: 12691 RVA: 0x00169F03 File Offset: 0x00169103
		public string RefusedSet { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x00169F0C File Offset: 0x0016910C
		// (set) Token: 0x06003195 RID: 12693 RVA: 0x00169F14 File Offset: 0x00169114
		public string Url { get; set; }
	}
}
