using System;

namespace System.Reflection
{
	// Token: 0x020005CF RID: 1487
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x040012F1 RID: 4849
		Default = 0,
		// Token: 0x040012F2 RID: 4850
		IgnoreCase = 1,
		// Token: 0x040012F3 RID: 4851
		DeclaredOnly = 2,
		// Token: 0x040012F4 RID: 4852
		Instance = 4,
		// Token: 0x040012F5 RID: 4853
		Static = 8,
		// Token: 0x040012F6 RID: 4854
		Public = 16,
		// Token: 0x040012F7 RID: 4855
		NonPublic = 32,
		// Token: 0x040012F8 RID: 4856
		FlattenHierarchy = 64,
		// Token: 0x040012F9 RID: 4857
		InvokeMethod = 256,
		// Token: 0x040012FA RID: 4858
		CreateInstance = 512,
		// Token: 0x040012FB RID: 4859
		GetField = 1024,
		// Token: 0x040012FC RID: 4860
		SetField = 2048,
		// Token: 0x040012FD RID: 4861
		GetProperty = 4096,
		// Token: 0x040012FE RID: 4862
		SetProperty = 8192,
		// Token: 0x040012FF RID: 4863
		PutDispProperty = 16384,
		// Token: 0x04001300 RID: 4864
		PutRefDispProperty = 32768,
		// Token: 0x04001301 RID: 4865
		ExactBinding = 65536,
		// Token: 0x04001302 RID: 4866
		SuppressChangeType = 131072,
		// Token: 0x04001303 RID: 4867
		OptionalParamBinding = 262144,
		// Token: 0x04001304 RID: 4868
		IgnoreReturn = 16777216,
		// Token: 0x04001305 RID: 4869
		DoNotWrapExceptions = 33554432
	}
}
