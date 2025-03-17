using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200054C RID: 1356
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06004756 RID: 18262 RVA: 0x0017D671 File Offset: 0x0017C871
		public RuntimeWrappedException(object thrownObject) : base(SR.RuntimeWrappedException)
		{
			base.HResult = -2146233026;
			this._wrappedException = thrownObject;
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x0017D690 File Offset: 0x0017C890
		private RuntimeWrappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0017D6B5 File Offset: 0x0017C8B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this._wrappedException, typeof(object));
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x0017D6DA File Offset: 0x0017C8DA
		public object WrappedException
		{
			get
			{
				return this._wrappedException;
			}
		}

		// Token: 0x04001135 RID: 4405
		private object _wrappedException;
	}
}
