using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A1 RID: 161
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class WeakReference<[Nullable(2)] T> : ISerializable where T : class
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x000C44FF File Offset: 0x000C36FF
		public void SetTarget(T target)
		{
			this.Target = target;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600087F RID: 2175
		// (set) Token: 0x06000880 RID: 2176
		private extern T Target { [MethodImpl(MethodImplOptions.InternalCall)] [return: MaybeNull] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000881 RID: 2177
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern void Finalize();

		// Token: 0x06000882 RID: 2178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Create(T target, bool trackResurrection);

		// Token: 0x06000883 RID: 2179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTrackResurrection();

		// Token: 0x06000884 RID: 2180 RVA: 0x000C4508 File Offset: 0x000C3708
		public WeakReference(T target) : this(target, false)
		{
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000C4512 File Offset: 0x000C3712
		public WeakReference(T target, bool trackResurrection)
		{
			this.Create(target, trackResurrection);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000C4524 File Offset: 0x000C3724
		private WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			T target = (T)((object)info.GetValue("TrackedObject", typeof(T)));
			bool boolean = info.GetBoolean("TrackResurrection");
			this.Create(target, boolean);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000C4574 File Offset: 0x000C3774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryGetTarget([NotNullWhen(true)] [MaybeNullWhen(false)] out T target)
		{
			T target2 = this.Target;
			target = target2;
			return target2 != null;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000C4598 File Offset: 0x000C3798
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackedObject", this.Target, typeof(T));
			info.AddValue("TrackResurrection", this.IsTrackResurrection());
		}

		// Token: 0x04000225 RID: 549
		internal IntPtr m_handle;
	}
}
