using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005CA RID: 1482
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		// Token: 0x06004BF2 RID: 19442 RVA: 0x0018B598 File Offset: 0x0018A798
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this.PublicKey = publicKey;
			this.Countersignature = countersignature;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x0018B5AE File Offset: 0x0018A7AE
		public string PublicKey { get; }

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0018B5B6 File Offset: 0x0018A7B6
		public string Countersignature { get; }
	}
}
