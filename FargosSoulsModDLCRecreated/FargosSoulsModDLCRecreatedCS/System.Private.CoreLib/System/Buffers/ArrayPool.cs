using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000246 RID: 582
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ArrayPool<[Nullable(2)] T>
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00138F70 File Offset: 0x00138170
		public static ArrayPool<T> Shared
		{
			get
			{
				return ArrayPool<T>.s_shared;
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00138F77 File Offset: 0x00138177
		public static ArrayPool<T> Create()
		{
			return new ConfigurableArrayPool<T>();
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00138F7E File Offset: 0x0013817E
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new ConfigurableArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x06002425 RID: 9253
		public abstract T[] Rent(int minimumLength);

		// Token: 0x06002426 RID: 9254
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x04000971 RID: 2417
		private static readonly TlsOverPerCoreLockedStacksArrayPool<T> s_shared = new TlsOverPerCoreLockedStacksArrayPool<T>();
	}
}
