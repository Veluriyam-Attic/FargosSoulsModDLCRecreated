using System;

namespace System.Security.Permissions
{
	// Token: 0x020003D1 RID: 977
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[Flags]
	public enum SecurityPermissionFlag
	{
		// Token: 0x04000DCC RID: 3532
		AllFlags = 16383,
		// Token: 0x04000DCD RID: 3533
		Assertion = 1,
		// Token: 0x04000DCE RID: 3534
		BindingRedirects = 8192,
		// Token: 0x04000DCF RID: 3535
		ControlAppDomain = 1024,
		// Token: 0x04000DD0 RID: 3536
		ControlDomainPolicy = 256,
		// Token: 0x04000DD1 RID: 3537
		ControlEvidence = 32,
		// Token: 0x04000DD2 RID: 3538
		ControlPolicy = 64,
		// Token: 0x04000DD3 RID: 3539
		ControlPrincipal = 512,
		// Token: 0x04000DD4 RID: 3540
		ControlThread = 16,
		// Token: 0x04000DD5 RID: 3541
		Execution = 8,
		// Token: 0x04000DD6 RID: 3542
		Infrastructure = 4096,
		// Token: 0x04000DD7 RID: 3543
		NoFlags = 0,
		// Token: 0x04000DD8 RID: 3544
		RemotingConfiguration = 2048,
		// Token: 0x04000DD9 RID: 3545
		SerializationFormatter = 128,
		// Token: 0x04000DDA RID: 3546
		SkipVerification = 4,
		// Token: 0x04000DDB RID: 3547
		UnmanagedCode = 2
	}
}
