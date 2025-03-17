using System;
using System.Threading.Tasks;

namespace System
{
	// Token: 0x02000125 RID: 293
	public interface IAsyncDisposable
	{
		// Token: 0x06000F50 RID: 3920
		ValueTask DisposeAsync();
	}
}
