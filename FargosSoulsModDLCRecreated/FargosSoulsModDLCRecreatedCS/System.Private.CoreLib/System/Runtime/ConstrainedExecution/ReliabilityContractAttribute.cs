using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020003F7 RID: 1015
	[Obsolete("The Constrained Execution Region (CER) feature is not supported.", DiagnosticId = "SYSLIB0004", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		// Token: 0x0600327B RID: 12923 RVA: 0x0016B3AC File Offset: 0x0016A5AC
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this.ConsistencyGuarantee = consistencyGuarantee;
			this.Cer = cer;
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x0016B3C2 File Offset: 0x0016A5C2
		public Consistency ConsistencyGuarantee { get; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x0016B3CA File Offset: 0x0016A5CA
		public Cer Cer { get; }
	}
}
