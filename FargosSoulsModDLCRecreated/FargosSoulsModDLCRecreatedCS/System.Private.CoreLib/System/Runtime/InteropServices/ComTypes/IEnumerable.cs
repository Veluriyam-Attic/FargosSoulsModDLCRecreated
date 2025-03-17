using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004B7 RID: 1207
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x0600453A RID: 17722
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
