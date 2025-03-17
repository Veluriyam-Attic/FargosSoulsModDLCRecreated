using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x02000820 RID: 2080
	[Nullable(0)]
	[NullableContext(1)]
	public struct ComActivationContext
	{
		// Token: 0x06006285 RID: 25221 RVA: 0x001D42D4 File Offset: 0x001D34D4
		[CLSCompliant(false)]
		public unsafe static ComActivationContext Create(ref ComActivationContextInternal cxtInt)
		{
			return new ComActivationContext
			{
				ClassId = cxtInt.ClassId,
				InterfaceId = cxtInt.InterfaceId,
				AssemblyPath = Marshal.PtrToStringUni(new IntPtr((void*)cxtInt.AssemblyPathBuffer)),
				AssemblyName = Marshal.PtrToStringUni(new IntPtr((void*)cxtInt.AssemblyNameBuffer)),
				TypeName = Marshal.PtrToStringUni(new IntPtr((void*)cxtInt.TypeNameBuffer))
			};
		}

		// Token: 0x04001D62 RID: 7522
		public Guid ClassId;

		// Token: 0x04001D63 RID: 7523
		public Guid InterfaceId;

		// Token: 0x04001D64 RID: 7524
		public string AssemblyPath;

		// Token: 0x04001D65 RID: 7525
		public string AssemblyName;

		// Token: 0x04001D66 RID: 7526
		public string TypeName;
	}
}
