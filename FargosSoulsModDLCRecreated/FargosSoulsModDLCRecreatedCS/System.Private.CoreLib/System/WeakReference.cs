using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A0 RID: 160
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class WeakReference : ISerializable
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x000C4449 File Offset: 0x000C3649
		protected WeakReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000873 RID: 2163
		public virtual extern bool IsAlive { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000C4456 File Offset: 0x000C3656
		public virtual bool TrackResurrection
		{
			get
			{
				return this.IsTrackResurrection();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000875 RID: 2165
		// (set) Token: 0x06000876 RID: 2166
		public virtual extern object Target { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000877 RID: 2167
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern void Finalize();

		// Token: 0x06000878 RID: 2168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Create(object target, bool trackResurrection);

		// Token: 0x06000879 RID: 2169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsTrackResurrection();

		// Token: 0x0600087A RID: 2170 RVA: 0x000C445E File Offset: 0x000C365E
		public WeakReference(object target) : this(target, false)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000C4468 File Offset: 0x000C3668
		public WeakReference(object target, bool trackResurrection)
		{
			this.Create(target, trackResurrection);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000C4478 File Offset: 0x000C3678
		[NullableContext(1)]
		protected WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object value = info.GetValue("TrackedObject", typeof(object));
			bool boolean = info.GetBoolean("TrackResurrection");
			this.Create(value, boolean);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000C44C3 File Offset: 0x000C36C3
		[NullableContext(1)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackedObject", this.Target, typeof(object));
			info.AddValue("TrackResurrection", this.IsTrackResurrection());
		}

		// Token: 0x04000224 RID: 548
		internal IntPtr m_handle;
	}
}
