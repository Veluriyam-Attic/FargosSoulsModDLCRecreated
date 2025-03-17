using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000084 RID: 132
	[Nullable(0)]
	[NullableContext(2)]
	public struct ModuleHandle
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x000B92F4 File Offset: 0x000B84F4
		private static ModuleHandle GetEmptyMH()
		{
			return default(ModuleHandle);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000B930A File Offset: 0x000B850A
		internal ModuleHandle(RuntimeModule module)
		{
			this.m_ptr = module;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000B9313 File Offset: 0x000B8513
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_ptr;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000B931B File Offset: 0x000B851B
		public override int GetHashCode()
		{
			if (!(this.m_ptr != null))
			{
				return 0;
			}
			return this.m_ptr.GetHashCode();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000B9338 File Offset: 0x000B8538
		public override bool Equals(object obj)
		{
			if (!(obj is ModuleHandle))
			{
				return false;
			}
			ModuleHandle moduleHandle = (ModuleHandle)obj;
			return moduleHandle.m_ptr == this.m_ptr;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000B9367 File Offset: 0x000B8567
		public bool Equals(ModuleHandle handle)
		{
			return handle.m_ptr == this.m_ptr;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000B937A File Offset: 0x000B857A
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000B9384 File Offset: 0x000B8584
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000567 RID: 1383
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDynamicMethod(DynamicMethod method, RuntimeModule module, string name, byte[] sig, Resolver resolver);

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeModule module);

		// Token: 0x06000569 RID: 1385 RVA: 0x000B9391 File Offset: 0x000B8591
		private static void ValidateModulePointer(RuntimeModule module)
		{
			if (module == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NullModuleHandle);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000B93A7 File Offset: 0x000B85A7
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000B93B0 File Offset: 0x000B85B0
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, null, null));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000B93C5 File Offset: 0x000B85C5
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000B93DC File Offset: 0x000B85DC
		internal unsafe static RuntimeType ResolveTypeHandleInternal(RuntimeModule module, int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module).IsValidToken(typeToken))
			{
				throw new ArgumentOutOfRangeException("typeToken", SR.Format(SR.Argument_InvalidToken, typeToken, new ModuleHandle(module)));
			}
			int typeInstCount;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out typeInstCount);
			int methodInstCount;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out methodInstCount);
			IntPtr[] array3;
			IntPtr* typeInstArgs;
			if ((array3 = array) == null || array3.Length == 0)
			{
				typeInstArgs = null;
			}
			else
			{
				typeInstArgs = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* methodInstArgs;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				methodInstArgs = null;
			}
			else
			{
				methodInstArgs = &array4[0];
			}
			RuntimeType result = null;
			ModuleHandle.ResolveType(new QCallModule(ref module), typeToken, typeInstArgs, typeInstCount, methodInstArgs, methodInstCount, ObjectHandleOnStack.Create<RuntimeType>(ref result));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return result;
		}

		// Token: 0x0600056E RID: 1390
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void ResolveType(QCallModule module, int typeToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack type);

		// Token: 0x0600056F RID: 1391 RVA: 0x000B949E File Offset: 0x000B869E
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000B94A7 File Offset: 0x000B86A7
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000B94B2 File Offset: 0x000B86B2
		internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken)
		{
			return ModuleHandle.ResolveMethodHandleInternal(module, methodToken, null, null);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000B94BD File Offset: 0x000B86BD
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeMethodHandle(ModuleHandle.ResolveMethodHandleInternal(this.GetRuntimeModule(), methodToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000B94D4 File Offset: 0x000B86D4
		internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			int typeInstCount;
			IntPtr[] typeInstantiationContext2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out typeInstCount);
			int methodInstCount;
			IntPtr[] methodInstantiationContext2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out methodInstCount);
			RuntimeMethodHandleInternal runtimeMethodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(module, methodToken, typeInstantiationContext2, typeInstCount, methodInstantiationContext2, methodInstCount);
			IRuntimeMethodInfo result = new RuntimeMethodInfoStub(runtimeMethodHandleInternal, RuntimeMethodHandle.GetLoaderAllocator(runtimeMethodHandleInternal));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return result;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000B9520 File Offset: 0x000B8720
		internal unsafe static RuntimeMethodHandleInternal ResolveMethodHandleInternalCore(RuntimeModule module, int methodToken, IntPtr[] typeInstantiationContext, int typeInstCount, IntPtr[] methodInstantiationContext, int methodInstCount)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(methodToken))
			{
				throw new ArgumentOutOfRangeException("methodToken", SR.Format(SR.Argument_InvalidToken, methodToken, new ModuleHandle(module)));
			}
			IntPtr* typeInstArgs;
			if (typeInstantiationContext == null || typeInstantiationContext.Length == 0)
			{
				typeInstArgs = null;
			}
			else
			{
				typeInstArgs = &typeInstantiationContext[0];
			}
			IntPtr* methodInstArgs;
			if (methodInstantiationContext == null || methodInstantiationContext.Length == 0)
			{
				methodInstArgs = null;
			}
			else
			{
				methodInstArgs = &methodInstantiationContext[0];
			}
			return ModuleHandle.ResolveMethod(new QCallModule(ref module), methodToken, typeInstArgs, typeInstCount, methodInstArgs, methodInstCount);
		}

		// Token: 0x06000575 RID: 1397
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern RuntimeMethodHandleInternal ResolveMethod(QCallModule module, int methodToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount);

		// Token: 0x06000576 RID: 1398 RVA: 0x000B95B5 File Offset: 0x000B87B5
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000B95BE File Offset: 0x000B87BE
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, null, null));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000B95D3 File Offset: 0x000B87D3
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, typeInstantiationContext, methodInstantiationContext));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000B95E8 File Offset: 0x000B87E8
		internal unsafe static IRuntimeFieldInfo ResolveFieldHandleInternal(RuntimeModule module, int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			ModuleHandle.ValidateModulePointer(module);
			if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(fieldToken))
			{
				throw new ArgumentOutOfRangeException("fieldToken", SR.Format(SR.Argument_InvalidToken, fieldToken, new ModuleHandle(module)));
			}
			int typeInstCount;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out typeInstCount);
			int methodInstCount;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out methodInstCount);
			IntPtr[] array3;
			IntPtr* typeInstArgs;
			if ((array3 = array) == null || array3.Length == 0)
			{
				typeInstArgs = null;
			}
			else
			{
				typeInstArgs = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* methodInstArgs;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				methodInstArgs = null;
			}
			else
			{
				methodInstArgs = &array4[0];
			}
			IRuntimeFieldInfo result = null;
			ModuleHandle.ResolveField(new QCallModule(ref module), fieldToken, typeInstArgs, typeInstCount, methodInstArgs, methodInstCount, ObjectHandleOnStack.Create<IRuntimeFieldInfo>(ref result));
			GC.KeepAlive(typeInstantiationContext);
			GC.KeepAlive(methodInstantiationContext);
			return result;
		}

		// Token: 0x0600057A RID: 1402
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void ResolveField(QCallModule module, int fieldToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack retField);

		// Token: 0x0600057B RID: 1403
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern Interop.BOOL _ContainsPropertyMatchingHash(QCallModule module, int propertyToken, uint hash);

		// Token: 0x0600057C RID: 1404 RVA: 0x000B96AF File Offset: 0x000B88AF
		internal static bool ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash)
		{
			return ModuleHandle._ContainsPropertyMatchingHash(new QCallModule(ref module), propertyToken, hash) > Interop.BOOL.FALSE;
		}

		// Token: 0x0600057D RID: 1405
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetModuleType(QCallModule handle, ObjectHandleOnStack type);

		// Token: 0x0600057E RID: 1406 RVA: 0x000B96C4 File Offset: 0x000B88C4
		internal static RuntimeType GetModuleType(RuntimeModule module)
		{
			RuntimeType result = null;
			ModuleHandle.GetModuleType(new QCallModule(ref module), ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x0600057F RID: 1407
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void GetPEKind(QCallModule handle, int* peKind, int* machine);

		// Token: 0x06000580 RID: 1408 RVA: 0x000B96E8 File Offset: 0x000B88E8
		internal unsafe static void GetPEKind(RuntimeModule module, out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			int num;
			int num2;
			ModuleHandle.GetPEKind(new QCallModule(ref module), &num, &num2);
			peKind = (PortableExecutableKinds)num;
			machine = (ImageFileMachine)num2;
		}

		// Token: 0x06000581 RID: 1409
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMDStreamVersion(RuntimeModule module);

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000B970D File Offset: 0x000B890D
		public int MDStreamVersion
		{
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetRuntimeModule().GetNativeHandle());
			}
		}

		// Token: 0x06000583 RID: 1411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeModule module);

		// Token: 0x06000584 RID: 1412 RVA: 0x000B971F File Offset: 0x000B891F
		internal static MetadataImport GetMetadataImport(RuntimeModule module)
		{
			return new MetadataImport(ModuleHandle._GetMetadataImport(module.GetNativeHandle()), module);
		}

		// Token: 0x040001A5 RID: 421
		public static readonly ModuleHandle EmptyHandle = ModuleHandle.GetEmptyMH();

		// Token: 0x040001A6 RID: 422
		private readonly RuntimeModule m_ptr;
	}
}
