using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200055F RID: 1375
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x0017DC03 File Offset: 0x0017CE03
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.AssemblyFullName = assemblyFullName;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x0017DC25 File Offset: 0x0017CE25
		public string AssemblyFullName { get; }
	}
}
