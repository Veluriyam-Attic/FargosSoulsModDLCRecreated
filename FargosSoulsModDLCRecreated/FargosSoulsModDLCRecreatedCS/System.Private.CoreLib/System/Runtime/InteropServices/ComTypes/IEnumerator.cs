using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004B8 RID: 1208
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x0600453B RID: 17723
		bool MoveNext();

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600453C RID: 17724
		object Current { get; }

		// Token: 0x0600453D RID: 17725
		void Reset();
	}
}
