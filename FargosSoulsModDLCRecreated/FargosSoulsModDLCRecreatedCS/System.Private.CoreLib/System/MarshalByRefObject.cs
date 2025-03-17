using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000149 RID: 329
	[NullableContext(1)]
	[Nullable(0)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public abstract class MarshalByRefObject
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x000DB824 File Offset: 0x000DAA24
		[Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public object GetLifetimeService()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_Remoting);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000DB824 File Offset: 0x000DAA24
		[Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public virtual object InitializeLifetimeService()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_Remoting);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000DB830 File Offset: 0x000DAA30
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			return (MarshalByRefObject)base.MemberwiseClone();
		}
	}
}
