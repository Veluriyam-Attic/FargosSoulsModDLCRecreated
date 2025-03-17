using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FB RID: 1275
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06004646 RID: 17990 RVA: 0x0017AD36 File Offset: 0x00179F36
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x040010CD RID: 4301
		private readonly string typeName;
	}
}
