using System;
using System.Runtime.CompilerServices;

namespace System.Security.Permissions
{
	// Token: 0x020003D0 RID: 976
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060031AF RID: 12719 RVA: 0x00169FB1 File Offset: 0x001691B1
		public SecurityPermissionAttribute(SecurityAction action) : base((SecurityAction)0)
		{
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x00169FBA File Offset: 0x001691BA
		// (set) Token: 0x060031B1 RID: 12721 RVA: 0x00169FC2 File Offset: 0x001691C2
		public bool Assertion { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x00169FCB File Offset: 0x001691CB
		// (set) Token: 0x060031B3 RID: 12723 RVA: 0x00169FD3 File Offset: 0x001691D3
		public bool BindingRedirects { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x00169FDC File Offset: 0x001691DC
		// (set) Token: 0x060031B5 RID: 12725 RVA: 0x00169FE4 File Offset: 0x001691E4
		public bool ControlAppDomain { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x00169FED File Offset: 0x001691ED
		// (set) Token: 0x060031B7 RID: 12727 RVA: 0x00169FF5 File Offset: 0x001691F5
		public bool ControlDomainPolicy { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x00169FFE File Offset: 0x001691FE
		// (set) Token: 0x060031B9 RID: 12729 RVA: 0x0016A006 File Offset: 0x00169206
		public bool ControlEvidence { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x0016A00F File Offset: 0x0016920F
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x0016A017 File Offset: 0x00169217
		public bool ControlPolicy { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x0016A020 File Offset: 0x00169220
		// (set) Token: 0x060031BD RID: 12733 RVA: 0x0016A028 File Offset: 0x00169228
		public bool ControlPrincipal { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x0016A031 File Offset: 0x00169231
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x0016A039 File Offset: 0x00169239
		public bool ControlThread { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x0016A042 File Offset: 0x00169242
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x0016A04A File Offset: 0x0016924A
		public bool Execution { get; set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x0016A053 File Offset: 0x00169253
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x0016A05B File Offset: 0x0016925B
		public SecurityPermissionFlag Flags { get; set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x0016A064 File Offset: 0x00169264
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x0016A06C File Offset: 0x0016926C
		public bool Infrastructure { get; set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x0016A075 File Offset: 0x00169275
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x0016A07D File Offset: 0x0016927D
		public bool RemotingConfiguration { get; set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x0016A086 File Offset: 0x00169286
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x0016A08E File Offset: 0x0016928E
		public bool SerializationFormatter { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x0016A097 File Offset: 0x00169297
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x0016A09F File Offset: 0x0016929F
		public bool SkipVerification { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x0016A0A8 File Offset: 0x001692A8
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x0016A0B0 File Offset: 0x001692B0
		public bool UnmanagedCode { get; set; }

		// Token: 0x060031CE RID: 12750 RVA: 0x000C26FD File Offset: 0x000C18FD
		[NullableContext(2)]
		public override IPermission CreatePermission()
		{
			return null;
		}
	}
}
