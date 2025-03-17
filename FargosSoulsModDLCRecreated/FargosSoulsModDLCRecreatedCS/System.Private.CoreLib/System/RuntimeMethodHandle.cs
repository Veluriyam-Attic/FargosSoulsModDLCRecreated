using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x0200007F RID: 127
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000B8F16 File Offset: 0x000B8116
		internal static IRuntimeMethodInfo EnsureNonNullMethodInfo(IRuntimeMethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(null, SR.Arg_InvalidHandle);
			}
			return method;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000B8F28 File Offset: 0x000B8128
		internal RuntimeMethodHandle(IRuntimeMethodInfo method)
		{
			this.m_value = method;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000B8F31 File Offset: 0x000B8131
		internal IRuntimeMethodInfo GetMethodInfo()
		{
			return this.m_value;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000B8F39 File Offset: 0x000B8139
		private static IntPtr GetValueInternal(RuntimeMethodHandle rmh)
		{
			return rmh.Value;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000B3617 File Offset: 0x000B2817
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000B8F44 File Offset: 0x000B8144
		public IntPtr Value
		{
			get
			{
				if (this.m_value == null)
				{
					return IntPtr.Zero;
				}
				return this.m_value.Value.Value;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000B8F72 File Offset: 0x000B8172
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.Value);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000B8F80 File Offset: 0x000B8180
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is RuntimeMethodHandle && ((RuntimeMethodHandle)obj).Value == this.Value;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000B8FB0 File Offset: 0x000B81B0
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000B8FBA File Offset: 0x000B81BA
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000B8FC7 File Offset: 0x000B81C7
		public bool Equals(RuntimeMethodHandle handle)
		{
			return handle.Value == this.Value;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000B8FDB File Offset: 0x000B81DB
		internal bool IsNullHandle()
		{
			return this.m_value == null;
		}

		// Token: 0x06000515 RID: 1301
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetFunctionPointer(RuntimeMethodHandleInternal handle);

		// Token: 0x06000516 RID: 1302 RVA: 0x000B8FE8 File Offset: 0x000B81E8
		public IntPtr GetFunctionPointer()
		{
			IntPtr functionPointer = RuntimeMethodHandle.GetFunctionPointer(RuntimeMethodHandle.EnsureNonNullMethodInfo(this.m_value).Value);
			GC.KeepAlive(this.m_value);
			return functionPointer;
		}

		// Token: 0x06000517 RID: 1303
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern Interop.BOOL GetIsCollectible(RuntimeMethodHandleInternal handle);

		// Token: 0x06000518 RID: 1304
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern Interop.BOOL IsCAVisibleFromDecoratedType(QCallTypeHandle attrTypeHandle, RuntimeMethodHandleInternal attrCtor, QCallTypeHandle sourceTypeHandle, QCallModule sourceModule);

		// Token: 0x06000519 RID: 1305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo _GetCurrentMethod(ref StackCrawlMark stackMark);

		// Token: 0x0600051A RID: 1306 RVA: 0x000B9017 File Offset: 0x000B8217
		internal static IRuntimeMethodInfo GetCurrentMethod(ref StackCrawlMark stackMark)
		{
			return RuntimeMethodHandle._GetCurrentMethod(ref stackMark);
		}

		// Token: 0x0600051B RID: 1307
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodAttributes GetAttributes(RuntimeMethodHandleInternal method);

		// Token: 0x0600051C RID: 1308 RVA: 0x000B9020 File Offset: 0x000B8220
		internal static MethodAttributes GetAttributes(IRuntimeMethodInfo method)
		{
			MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method.Value);
			GC.KeepAlive(method);
			return attributes;
		}

		// Token: 0x0600051D RID: 1309
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodImplAttributes GetImplAttributes(IRuntimeMethodInfo method);

		// Token: 0x0600051E RID: 1310
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructInstantiation(RuntimeMethodHandleInternal method, TypeNameFormatFlags format, StringHandleOnStack retString);

		// Token: 0x0600051F RID: 1311 RVA: 0x000B9040 File Offset: 0x000B8240
		internal static string ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			string result = null;
			IRuntimeMethodInfo runtimeMethodInfo = RuntimeMethodHandle.EnsureNonNullMethodInfo(method);
			RuntimeMethodHandle.ConstructInstantiation(runtimeMethodInfo.Value, format, new StringHandleOnStack(ref result));
			GC.KeepAlive(runtimeMethodInfo);
			return result;
		}

		// Token: 0x06000520 RID: 1312
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeMethodHandleInternal method);

		// Token: 0x06000521 RID: 1313 RVA: 0x000B9070 File Offset: 0x000B8270
		internal static RuntimeType GetDeclaringType(IRuntimeMethodInfo method)
		{
			RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.Value);
			GC.KeepAlive(method);
			return declaringType;
		}

		// Token: 0x06000522 RID: 1314
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSlot(RuntimeMethodHandleInternal method);

		// Token: 0x06000523 RID: 1315 RVA: 0x000B9090 File Offset: 0x000B8290
		internal static int GetSlot(IRuntimeMethodInfo method)
		{
			int slot = RuntimeMethodHandle.GetSlot(method.Value);
			GC.KeepAlive(method);
			return slot;
		}

		// Token: 0x06000524 RID: 1316
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMethodDef(IRuntimeMethodInfo method);

		// Token: 0x06000525 RID: 1317
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetName(RuntimeMethodHandleInternal method);

		// Token: 0x06000526 RID: 1318 RVA: 0x000B90B0 File Offset: 0x000B82B0
		internal static string GetName(IRuntimeMethodInfo method)
		{
			string name = RuntimeMethodHandle.GetName(method.Value);
			GC.KeepAlive(method);
			return name;
		}

		// Token: 0x06000527 RID: 1319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeMethodHandleInternal method);

		// Token: 0x06000528 RID: 1320 RVA: 0x000B90D0 File Offset: 0x000B82D0
		internal static MdUtf8String GetUtf8Name(RuntimeMethodHandleInternal method)
		{
			return new MdUtf8String(RuntimeMethodHandle._GetUtf8Name(method));
		}

		// Token: 0x06000529 RID: 1321
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool MatchesNameHash(RuntimeMethodHandleInternal method, uint hash);

		// Token: 0x0600052A RID: 1322
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InvokeMethod(object target, object[] arguments, Signature sig, bool constructor, bool wrapExceptions);

		// Token: 0x0600052B RID: 1323
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack types, Interop.BOOL fAsRuntimeTypeArray);

		// Token: 0x0600052C RID: 1324 RVA: 0x000B90E0 File Offset: 0x000B82E0
		internal static RuntimeType[] GetMethodInstantiationInternal(IRuntimeMethodInfo method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, ObjectHandleOnStack.Create<RuntimeType[]>(ref result), Interop.BOOL.TRUE);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000B9110 File Offset: 0x000B8310
		internal static RuntimeType[] GetMethodInstantiationInternal(RuntimeMethodHandleInternal method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(method, ObjectHandleOnStack.Create<RuntimeType[]>(ref result), Interop.BOOL.TRUE);
			return result;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000B9130 File Offset: 0x000B8330
		internal static Type[] GetMethodInstantiationPublic(IRuntimeMethodInfo method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, ObjectHandleOnStack.Create<RuntimeType[]>(ref result), Interop.BOOL.FALSE);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600052F RID: 1327
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMethodInstantiation(RuntimeMethodHandleInternal method);

		// Token: 0x06000530 RID: 1328 RVA: 0x000B9160 File Offset: 0x000B8360
		internal static bool HasMethodInstantiation(IRuntimeMethodInfo method)
		{
			bool result = RuntimeMethodHandle.HasMethodInstantiation(method.Value);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x06000531 RID: 1329
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetStubIfNeeded(RuntimeMethodHandleInternal method, RuntimeType declaringType, RuntimeType[] methodInstantiation);

		// Token: 0x06000532 RID: 1330
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodFromCanonical(RuntimeMethodHandleInternal method, RuntimeType declaringType);

		// Token: 0x06000533 RID: 1331
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericMethodDefinition(RuntimeMethodHandleInternal method);

		// Token: 0x06000534 RID: 1332 RVA: 0x000B9180 File Offset: 0x000B8380
		internal static bool IsGenericMethodDefinition(IRuntimeMethodInfo method)
		{
			bool result = RuntimeMethodHandle.IsGenericMethodDefinition(method.Value);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x06000535 RID: 1333
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTypicalMethodDefinition(IRuntimeMethodInfo method);

		// Token: 0x06000536 RID: 1334
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypicalMethodDefinition(RuntimeMethodHandleInternal method, ObjectHandleOnStack outMethod);

		// Token: 0x06000537 RID: 1335 RVA: 0x000B91A0 File Offset: 0x000B83A0
		internal static IRuntimeMethodInfo GetTypicalMethodDefinition(IRuntimeMethodInfo method)
		{
			if (!RuntimeMethodHandle.IsTypicalMethodDefinition(method))
			{
				RuntimeMethodHandle.GetTypicalMethodDefinition(method.Value, ObjectHandleOnStack.Create<IRuntimeMethodInfo>(ref method));
				GC.KeepAlive(method);
			}
			return method;
		}

		// Token: 0x06000538 RID: 1336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenericParameterCount(RuntimeMethodHandleInternal method);

		// Token: 0x06000539 RID: 1337 RVA: 0x000B91C3 File Offset: 0x000B83C3
		internal static int GetGenericParameterCount(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle.GetGenericParameterCount(method.Value);
		}

		// Token: 0x0600053A RID: 1338
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void StripMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack outMethod);

		// Token: 0x0600053B RID: 1339 RVA: 0x000B91D0 File Offset: 0x000B83D0
		internal static IRuntimeMethodInfo StripMethodInstantiation(IRuntimeMethodInfo method)
		{
			IRuntimeMethodInfo result = method;
			RuntimeMethodHandle.StripMethodInstantiation(method.Value, ObjectHandleOnStack.Create<IRuntimeMethodInfo>(ref result));
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600053C RID: 1340
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDynamicMethod(RuntimeMethodHandleInternal method);

		// Token: 0x0600053D RID: 1341
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void Destroy(RuntimeMethodHandleInternal method);

		// Token: 0x0600053E RID: 1342
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Resolver GetResolver(RuntimeMethodHandleInternal method);

		// Token: 0x0600053F RID: 1343
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodBody GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType);

		// Token: 0x06000540 RID: 1344
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsConstructor(RuntimeMethodHandleInternal method);

		// Token: 0x06000541 RID: 1345
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LoaderAllocator GetLoaderAllocator(RuntimeMethodHandleInternal method);

		// Token: 0x0400019C RID: 412
		private readonly IRuntimeMethodInfo m_value;
	}
}
