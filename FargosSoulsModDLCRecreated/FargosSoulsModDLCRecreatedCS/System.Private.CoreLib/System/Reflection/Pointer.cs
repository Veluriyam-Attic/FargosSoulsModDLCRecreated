using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F4 RID: 1524
	[CLSCompliant(false)]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x06004CF9 RID: 19705 RVA: 0x0018C1DF File Offset: 0x0018B3DF
		private unsafe Pointer(void* ptr, Type ptrType)
		{
			this._ptr = ptr;
			this._ptrType = ptrType;
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0018C1F8 File Offset: 0x0018B3F8
		[NullableContext(1)]
		public unsafe static object Box([Nullable(0)] void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException(SR.Arg_MustBePointer, "ptr");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Arg_MustBeType, "ptr");
			}
			return new Pointer(ptr, type);
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0018C250 File Offset: 0x0018B450
		public unsafe static void* Unbox([Nullable(1)] object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException(SR.Arg_MustBePointer, "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x000B3617 File Offset: 0x000B2817
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0018C275 File Offset: 0x0018B475
		internal Type GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0018C27D File Offset: 0x0018B47D
		internal IntPtr GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		// Token: 0x040013B9 RID: 5049
		private unsafe readonly void* _ptr;

		// Token: 0x040013BA RID: 5050
		private readonly Type _ptrType;
	}
}
