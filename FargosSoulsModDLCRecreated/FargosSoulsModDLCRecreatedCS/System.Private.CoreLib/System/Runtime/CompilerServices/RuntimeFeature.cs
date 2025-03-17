using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004F2 RID: 1266
	[NullableContext(1)]
	[Nullable(0)]
	public static class RuntimeFeature
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000AC09E File Offset: 0x000AB29E
		public static bool IsDynamicCodeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000AC09E File Offset: 0x000AB29E
		public static bool IsDynamicCodeCompiled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x0017A89C File Offset: 0x00179A9C
		public static bool IsSupported(string feature)
		{
			if (feature == "PortablePdb" || feature == "CovariantReturnsOfClasses" || feature == "UnmanagedSignatureCallingConvention" || feature == "DefaultImplementationsOfInterfaces")
			{
				return true;
			}
			if (!(feature == "IsDynamicCodeSupported"))
			{
				return feature == "IsDynamicCodeCompiled" && RuntimeFeature.IsDynamicCodeCompiled;
			}
			return RuntimeFeature.IsDynamicCodeSupported;
		}

		// Token: 0x040010B9 RID: 4281
		public const string PortablePdb = "PortablePdb";

		// Token: 0x040010BA RID: 4282
		public const string DefaultImplementationsOfInterfaces = "DefaultImplementationsOfInterfaces";

		// Token: 0x040010BB RID: 4283
		public const string UnmanagedSignatureCallingConvention = "UnmanagedSignatureCallingConvention";

		// Token: 0x040010BC RID: 4284
		public const string CovariantReturnsOfClasses = "CovariantReturnsOfClasses";
	}
}
