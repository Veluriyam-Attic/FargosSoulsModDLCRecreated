using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C8 RID: 1480
	public class AssemblyNameProxy : MarshalByRefObject
	{
		// Token: 0x06004BEE RID: 19438 RVA: 0x0018B579 File Offset: 0x0018A779
		[NullableContext(1)]
		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}
	}
}
