using System;
using System.Configuration.Assemblies;

namespace System.Reflection
{
	// Token: 0x020005B7 RID: 1463
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		// Token: 0x06004BC9 RID: 19401 RVA: 0x0018B0BB File Offset: 0x0018A2BB
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0018B0BB File Offset: 0x0018A2BB
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x0018B0CA File Offset: 0x0018A2CA
		[CLSCompliant(false)]
		public uint AlgorithmId { get; }
	}
}
