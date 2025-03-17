using System;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x0200081F RID: 2079
	[CLSCompliant(false)]
	public struct ComActivationContextInternal
	{
		// Token: 0x04001D5C RID: 7516
		public Guid ClassId;

		// Token: 0x04001D5D RID: 7517
		public Guid InterfaceId;

		// Token: 0x04001D5E RID: 7518
		public unsafe char* AssemblyPathBuffer;

		// Token: 0x04001D5F RID: 7519
		public unsafe char* AssemblyNameBuffer;

		// Token: 0x04001D60 RID: 7520
		public unsafe char* TypeNameBuffer;

		// Token: 0x04001D61 RID: 7521
		public IntPtr ClassFactoryDest;
	}
}
