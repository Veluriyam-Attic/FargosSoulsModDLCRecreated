using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000573 RID: 1395
	public interface IResourceReader : IEnumerable, IDisposable
	{
		// Token: 0x060047DE RID: 18398
		void Close();

		// Token: 0x060047DF RID: 18399
		[NullableContext(1)]
		IDictionaryEnumerator GetEnumerator();
	}
}
