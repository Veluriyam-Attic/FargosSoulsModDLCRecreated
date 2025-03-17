using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x02000575 RID: 1397
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		// Token: 0x060047E4 RID: 18404 RVA: 0x0017EE81 File Offset: 0x0017E081
		public MissingSatelliteAssemblyException() : base(SR.MissingSatelliteAssembly_Default)
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0017EE99 File Offset: 0x0017E099
		public MissingSatelliteAssemblyException(string message) : base(message)
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0017EEAD File Offset: 0x0017E0AD
		public MissingSatelliteAssemblyException(string message, string cultureName) : base(message)
		{
			base.HResult = -2146233034;
			this._cultureName = cultureName;
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x0017EEC8 File Offset: 0x0017E0C8
		public MissingSatelliteAssemblyException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x0017EEDD File Offset: 0x0017E0DD
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x0400115C RID: 4444
		private readonly string _cultureName;
	}
}
