using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x0200024B RID: 587
	public interface IMemoryOwner<[Nullable(2)] T> : IDisposable
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002439 RID: 9273
		[Nullable(new byte[]
		{
			0,
			1
		})]
		Memory<T> Memory { [return: Nullable(new byte[]
		{
			0,
			1
		})] get; }
	}
}
