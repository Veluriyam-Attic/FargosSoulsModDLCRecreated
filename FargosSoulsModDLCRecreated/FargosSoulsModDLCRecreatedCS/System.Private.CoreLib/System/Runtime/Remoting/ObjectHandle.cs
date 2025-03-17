using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Remoting
{
	// Token: 0x020003EF RID: 1007
	[NullableContext(2)]
	[Nullable(0)]
	public class ObjectHandle : MarshalByRefObject
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x0016B308 File Offset: 0x0016A508
		public ObjectHandle(object o)
		{
			this._wrappedObject = o;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x0016B317 File Offset: 0x0016A517
		public object Unwrap()
		{
			return this._wrappedObject;
		}

		// Token: 0x04000E16 RID: 3606
		private readonly object _wrappedObject;
	}
}
