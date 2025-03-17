using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000078 RID: 120
	[NullableContext(2)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Object
	{
		// Token: 0x0600048E RID: 1166
		[NullableContext(1)]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Type GetType();

		// Token: 0x0600048F RID: 1167 RVA: 0x000B85EC File Offset: 0x000B77EC
		[NullableContext(1)]
		protected unsafe object MemberwiseClone()
		{
			object obj = RuntimeHelpers.AllocateUninitializedClone(this);
			UIntPtr rawObjectDataSize = RuntimeHelpers.GetRawObjectDataSize(obj);
			ref byte rawData = ref this.GetRawData();
			ref byte rawData2 = ref obj.GetRawData();
			if (RuntimeHelpers.GetMethodTable(obj)->ContainsGCPointers)
			{
				Buffer.BulkMoveWithWriteBarrier(ref rawData2, ref rawData, rawObjectDataSize);
			}
			else
			{
				Buffer.Memmove<byte>(ref rawData2, ref rawData, rawObjectDataSize);
			}
			return obj;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NonVersionable]
		public Object()
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NonVersionable]
		protected virtual void Finalize()
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000B8635 File Offset: 0x000B7835
		public virtual string ToString()
		{
			return this.GetType().ToString();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000B8642 File Offset: 0x000B7842
		public virtual bool Equals(object obj)
		{
			return RuntimeHelpers.Equals(this, obj);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000B864B File Offset: 0x000B784B
		public static bool Equals(object objA, object objB)
		{
			return objA == objB || (objA != null && objB != null && objA.Equals(objB));
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000B8662 File Offset: 0x000B7862
		[NonVersionable]
		public static bool ReferenceEquals(object objA, object objB)
		{
			return objA == objB;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000B8668 File Offset: 0x000B7868
		public virtual int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}
	}
}
