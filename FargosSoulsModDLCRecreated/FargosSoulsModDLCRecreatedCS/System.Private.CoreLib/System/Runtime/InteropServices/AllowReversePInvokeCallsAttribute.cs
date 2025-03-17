using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000464 RID: 1124
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class AllowReversePInvokeCallsAttribute : Attribute
	{
	}
}
