using System;

namespace System.Reflection
{
	// Token: 0x020005DB RID: 1499
	[Flags]
	public enum GenericParameterAttributes
	{
		// Token: 0x04001353 RID: 4947
		None = 0,
		// Token: 0x04001354 RID: 4948
		VarianceMask = 3,
		// Token: 0x04001355 RID: 4949
		Covariant = 1,
		// Token: 0x04001356 RID: 4950
		Contravariant = 2,
		// Token: 0x04001357 RID: 4951
		SpecialConstraintMask = 28,
		// Token: 0x04001358 RID: 4952
		ReferenceTypeConstraint = 4,
		// Token: 0x04001359 RID: 4953
		NotNullableValueTypeConstraint = 8,
		// Token: 0x0400135A RID: 4954
		DefaultConstructorConstraint = 16
	}
}
