using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000144 RID: 324
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	public class Lazy<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T, [Nullable(2)] TMetadata> : Lazy<T>
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x000DB668 File Offset: 0x000DA868
		public Lazy(Func<T> valueFactory, TMetadata metadata) : base(valueFactory)
		{
			this._metadata = metadata;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000DB678 File Offset: 0x000DA878
		public Lazy(TMetadata metadata)
		{
			this._metadata = metadata;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000DB687 File Offset: 0x000DA887
		public Lazy(TMetadata metadata, bool isThreadSafe) : base(isThreadSafe)
		{
			this._metadata = metadata;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000DB697 File Offset: 0x000DA897
		public Lazy(Func<T> valueFactory, TMetadata metadata, bool isThreadSafe) : base(valueFactory, isThreadSafe)
		{
			this._metadata = metadata;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000DB6A8 File Offset: 0x000DA8A8
		public Lazy(TMetadata metadata, LazyThreadSafetyMode mode) : base(mode)
		{
			this._metadata = metadata;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000DB6B8 File Offset: 0x000DA8B8
		public Lazy(Func<T> valueFactory, TMetadata metadata, LazyThreadSafetyMode mode) : base(valueFactory, mode)
		{
			this._metadata = metadata;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000DB6C9 File Offset: 0x000DA8C9
		public TMetadata Metadata
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x0400040B RID: 1035
		private readonly TMetadata _metadata;
	}
}
