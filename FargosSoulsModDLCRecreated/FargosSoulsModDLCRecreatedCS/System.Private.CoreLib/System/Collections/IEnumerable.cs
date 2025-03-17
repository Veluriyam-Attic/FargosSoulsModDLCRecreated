using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections
{
	// Token: 0x020007B9 RID: 1977
	[ComVisible(true)]
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	[NullableContext(1)]
	public interface IEnumerable
	{
		// Token: 0x06005FA1 RID: 24481
		IEnumerator GetEnumerator();
	}
}
