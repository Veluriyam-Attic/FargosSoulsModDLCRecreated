using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x0200007A RID: 122
	[NullableContext(2)]
	[Nullable(0)]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x000B8678 File Offset: 0x000B7878
		internal RuntimeTypeHandle GetNativeHandle()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, SR.Arg_InvalidHandle);
			}
			return new RuntimeTypeHandle(type);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000B86A8 File Offset: 0x000B78A8
		internal RuntimeType GetTypeChecked()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, SR.Arg_InvalidHandle);
			}
			return type;
		}

		// Token: 0x0600049A RID: 1178
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, [NotNullWhen(true)] object o);

		// Token: 0x0600049B RID: 1179 RVA: 0x000B86D4 File Offset: 0x000B78D4
		internal unsafe static Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
		{
			Type type = typeStart;
			if (genericArgs != null)
			{
				type = type.MakeGenericType(genericArgs);
			}
			if (cModifiers > 0)
			{
				int* value = (int*)pModifiers.ToPointer();
				for (int i = 0; i < cModifiers; i++)
				{
					if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 15)
					{
						type = type.MakePointerType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 16)
					{
						type = type.MakeByRefType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 29)
					{
						type = type.MakeArrayType();
					}
					else
					{
						type = type.MakeArrayType(Marshal.ReadInt32((IntPtr)((void*)value), ++i * 4));
					}
				}
			}
			return type;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000B8777 File Offset: 0x000B7977
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000B8781 File Offset: 0x000B7981
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return right.Equals(left);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000B878B File Offset: 0x000B798B
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000B8798 File Offset: 0x000B7998
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return !right.Equals(left);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000B87A5 File Offset: 0x000B79A5
		public override int GetHashCode()
		{
			if (!(this.m_type != null))
			{
				return 0;
			}
			return this.m_type.GetHashCode();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000B87C4 File Offset: 0x000B79C4
		public override bool Equals(object obj)
		{
			return obj is RuntimeTypeHandle && ((RuntimeTypeHandle)obj).m_type == this.m_type;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000B87F4 File Offset: 0x000B79F4
		public bool Equals(RuntimeTypeHandle handle)
		{
			return handle.m_type == this.m_type;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000B8808 File Offset: 0x000B7A08
		public IntPtr Value
		{
			get
			{
				if (!(this.m_type != null))
				{
					return IntPtr.Zero;
				}
				return this.m_type.m_handle;
			}
		}

		// Token: 0x060004A4 RID: 1188
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

		// Token: 0x060004A5 RID: 1189 RVA: 0x000B8829 File Offset: 0x000B7A29
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this.m_type = type;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000B8834 File Offset: 0x000B7A34
		internal static bool IsTypeDefinition(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return ((corElementType >= CorElementType.ELEMENT_TYPE_VOID && corElementType < CorElementType.ELEMENT_TYPE_PTR) || corElementType == CorElementType.ELEMENT_TYPE_VALUETYPE || corElementType == CorElementType.ELEMENT_TYPE_CLASS || corElementType == CorElementType.ELEMENT_TYPE_TYPEDBYREF || corElementType == CorElementType.ELEMENT_TYPE_I || corElementType == CorElementType.ELEMENT_TYPE_U || corElementType == CorElementType.ELEMENT_TYPE_OBJECT) && (!RuntimeTypeHandle.HasInstantiation(type) || RuntimeTypeHandle.IsGenericTypeDefinition(type));
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000B8884 File Offset: 0x000B7A84
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.ELEMENT_TYPE_BOOLEAN && corElementType <= CorElementType.ELEMENT_TYPE_R8) || corElementType == CorElementType.ELEMENT_TYPE_I || corElementType == CorElementType.ELEMENT_TYPE_U;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000B88B0 File Offset: 0x000B7AB0
		internal static bool IsByRef(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ELEMENT_TYPE_BYREF;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000B88CC File Offset: 0x000B7ACC
		internal static bool IsPointer(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ELEMENT_TYPE_PTR;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000B88E8 File Offset: 0x000B7AE8
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ELEMENT_TYPE_ARRAY || corElementType == CorElementType.ELEMENT_TYPE_SZARRAY;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000B8908 File Offset: 0x000B7B08
		internal static bool IsSZArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ELEMENT_TYPE_SZARRAY;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000B8924 File Offset: 0x000B7B24
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ELEMENT_TYPE_ARRAY || corElementType == CorElementType.ELEMENT_TYPE_SZARRAY || corElementType == CorElementType.ELEMENT_TYPE_PTR || corElementType == CorElementType.ELEMENT_TYPE_BYREF;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000B8950 File Offset: 0x000B7B50
		internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000B8998 File Offset: 0x000B7B98
		internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].GetTypeHandleInternal().Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060004AF RID: 1199
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool wrapExceptions, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool hasNoDefaultCtor);

		// Token: 0x060004B0 RID: 1200
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Allocate(RuntimeType type);

		// Token: 0x060004B1 RID: 1201
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

		// Token: 0x060004B2 RID: 1202 RVA: 0x000B89E1 File Offset: 0x000B7BE1
		internal RuntimeType GetRuntimeType()
		{
			return this.m_type;
		}

		// Token: 0x060004B3 RID: 1203
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x060004B4 RID: 1204
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x060004B5 RID: 1205
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x060004B6 RID: 1206 RVA: 0x000B89E9 File Offset: 0x000B7BE9
		public ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
		}

		// Token: 0x060004B7 RID: 1207
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x060004B8 RID: 1208
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x060004B9 RID: 1209
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x060004BA RID: 1210
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

		// Token: 0x060004BB RID: 1211
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x060004BC RID: 1212
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeType type);

		// Token: 0x060004BD RID: 1213
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

		// Token: 0x060004BE RID: 1214 RVA: 0x000B89FB File Offset: 0x000B7BFB
		internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
		{
			return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
		}

		// Token: 0x060004BF RID: 1215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

		// Token: 0x060004C0 RID: 1216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

		// Token: 0x060004C1 RID: 1217
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetFields(RuntimeType type, IntPtr* result, int* count);

		// Token: 0x060004C2 RID: 1218
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetInterfaces(RuntimeType type);

		// Token: 0x060004C3 RID: 1219
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetConstraints(QCallTypeHandle handle, ObjectHandleOnStack types);

		// Token: 0x060004C4 RID: 1220 RVA: 0x000B8A04 File Offset: 0x000B7C04
		internal Type[] GetConstraints()
		{
			Type[] result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.GetConstraints(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<Type[]>(ref result));
			return result;
		}

		// Token: 0x060004C5 RID: 1221
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetGCHandle(QCallTypeHandle handle, GCHandleType type);

		// Token: 0x060004C6 RID: 1222 RVA: 0x000B8A30 File Offset: 0x000B7C30
		internal IntPtr GetGCHandle(GCHandleType type)
		{
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			return RuntimeTypeHandle.GetGCHandle(new QCallTypeHandle(ref nativeHandle), type);
		}

		// Token: 0x060004C7 RID: 1223
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr FreeGCHandle(QCallTypeHandle typeHandle, IntPtr objHandle);

		// Token: 0x060004C8 RID: 1224 RVA: 0x000B8A54 File Offset: 0x000B7C54
		internal IntPtr FreeGCHandle(IntPtr objHandle)
		{
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			return RuntimeTypeHandle.FreeGCHandle(new QCallTypeHandle(ref nativeHandle), objHandle);
		}

		// Token: 0x060004C9 RID: 1225
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumVirtuals(RuntimeType type);

		// Token: 0x060004CA RID: 1226
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void VerifyInterfaceIsImplemented(QCallTypeHandle handle, QCallTypeHandle interfaceHandle);

		// Token: 0x060004CB RID: 1227 RVA: 0x000B8A78 File Offset: 0x000B7C78
		internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
		{
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle nativeHandle2 = interfaceHandle.GetNativeHandle();
			RuntimeTypeHandle.VerifyInterfaceIsImplemented(new QCallTypeHandle(ref nativeHandle), new QCallTypeHandle(ref nativeHandle2));
		}

		// Token: 0x060004CC RID: 1228
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern RuntimeMethodHandleInternal GetInterfaceMethodImplementation(QCallTypeHandle handle, QCallTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

		// Token: 0x060004CD RID: 1229 RVA: 0x000B8AA8 File Offset: 0x000B7CA8
		internal RuntimeMethodHandleInternal GetInterfaceMethodImplementation(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
		{
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle nativeHandle2 = interfaceHandle.GetNativeHandle();
			return RuntimeTypeHandle.GetInterfaceMethodImplementation(new QCallTypeHandle(ref nativeHandle), new QCallTypeHandle(ref nativeHandle2), interfaceMethodHandle);
		}

		// Token: 0x060004CE RID: 1230
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

		// Token: 0x060004CF RID: 1231
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInterface(RuntimeType type);

		// Token: 0x060004D0 RID: 1232
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsByRefLike(RuntimeType type);

		// Token: 0x060004D1 RID: 1233
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsVisible(QCallTypeHandle typeHandle);

		// Token: 0x060004D2 RID: 1234 RVA: 0x000B8AD8 File Offset: 0x000B7CD8
		internal static bool IsVisible(RuntimeType type)
		{
			return RuntimeTypeHandle._IsVisible(new QCallTypeHandle(ref type));
		}

		// Token: 0x060004D3 RID: 1235
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValueType(RuntimeType type);

		// Token: 0x060004D4 RID: 1236
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructName(QCallTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

		// Token: 0x060004D5 RID: 1237 RVA: 0x000B8AE8 File Offset: 0x000B7CE8
		internal string ConstructName(TypeNameFormatFlags formatFlags)
		{
			string result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.ConstructName(new QCallTypeHandle(ref nativeHandle), formatFlags, new StringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x060004D6 RID: 1238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeType type);

		// Token: 0x060004D7 RID: 1239 RVA: 0x000B8B13 File Offset: 0x000B7D13
		internal static MdUtf8String GetUtf8Name(RuntimeType type)
		{
			return new MdUtf8String(RuntimeTypeHandle._GetUtf8Name(type));
		}

		// Token: 0x060004D8 RID: 1240
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

		// Token: 0x060004D9 RID: 1241
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeType type);

		// Token: 0x060004DA RID: 1242
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

		// Token: 0x060004DB RID: 1243
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, StackCrawlMarkHandle stackMark, ObjectHandleOnStack assemblyLoadContext, ObjectHandleOnStack type, ObjectHandleOnStack keepalive);

		// Token: 0x060004DC RID: 1244 RVA: 0x000B8B20 File Offset: 0x000B7D20
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, ref stackMark, AssemblyLoadContext.CurrentContextualReflectionContext);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000B8B30 File Offset: 0x000B7D30
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark, AssemblyLoadContext assemblyLoadContext)
		{
			if (!string.IsNullOrEmpty(name))
			{
				RuntimeType result = null;
				object obj = null;
				AssemblyLoadContext assemblyLoadContext2 = assemblyLoadContext;
				RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, new StackCrawlMarkHandle(ref stackMark), ObjectHandleOnStack.Create<AssemblyLoadContext>(ref assemblyLoadContext2), ObjectHandleOnStack.Create<RuntimeType>(ref result), ObjectHandleOnStack.Create<object>(ref obj));
				GC.KeepAlive(obj);
				return result;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(SR.Arg_TypeLoadNullStr);
			}
			return null;
		}

		// Token: 0x060004DE RID: 1246
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByNameUsingCARules(string name, QCallModule scope, ObjectHandleOnStack type);

		// Token: 0x060004DF RID: 1247 RVA: 0x000B8B88 File Offset: 0x000B7D88
		internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(null, "name");
			}
			RuntimeType result = null;
			RuntimeTypeHandle.GetTypeByNameUsingCARules(name, new QCallModule(ref scope), ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060004E0 RID: 1248
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetInstantiation(QCallTypeHandle type, ObjectHandleOnStack types, Interop.BOOL fAsRuntimeTypeArray);

		// Token: 0x060004E1 RID: 1249 RVA: 0x000B8BC0 File Offset: 0x000B7DC0
		internal RuntimeType[] GetInstantiationInternal()
		{
			RuntimeType[] result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.GetInstantiation(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<RuntimeType[]>(ref result), Interop.BOOL.TRUE);
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000B8BEC File Offset: 0x000B7DEC
		internal Type[] GetInstantiationPublic()
		{
			Type[] result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.GetInstantiation(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<Type[]>(ref result), Interop.BOOL.FALSE);
			return result;
		}

		// Token: 0x060004E3 RID: 1251
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void Instantiate(QCallTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

		// Token: 0x060004E4 RID: 1252 RVA: 0x000B8C18 File Offset: 0x000B7E18
		internal unsafe RuntimeType Instantiate(Type[] inst)
		{
			int numGenericArgs;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out numGenericArgs);
			IntPtr[] array2;
			IntPtr* pInst;
			if ((array2 = array) == null || array2.Length == 0)
			{
				pInst = null;
			}
			else
			{
				pInst = &array2[0];
			}
			RuntimeType result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.Instantiate(new QCallTypeHandle(ref nativeHandle), pInst, numGenericArgs, ObjectHandleOnStack.Create<RuntimeType>(ref result));
			GC.KeepAlive(inst);
			return result;
		}

		// Token: 0x060004E5 RID: 1253
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeArray(QCallTypeHandle handle, int rank, ObjectHandleOnStack type);

		// Token: 0x060004E6 RID: 1254 RVA: 0x000B8C70 File Offset: 0x000B7E70
		internal RuntimeType MakeArray(int rank)
		{
			RuntimeType result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.MakeArray(new QCallTypeHandle(ref nativeHandle), rank, ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060004E7 RID: 1255
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeSZArray(QCallTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x060004E8 RID: 1256 RVA: 0x000B8C9C File Offset: 0x000B7E9C
		internal RuntimeType MakeSZArray()
		{
			RuntimeType result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.MakeSZArray(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060004E9 RID: 1257
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeByRef(QCallTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x060004EA RID: 1258 RVA: 0x000B8CC8 File Offset: 0x000B7EC8
		internal RuntimeType MakeByRef()
		{
			RuntimeType result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.MakeByRef(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060004EB RID: 1259
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakePointer(QCallTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x060004EC RID: 1260 RVA: 0x000B8CF4 File Offset: 0x000B7EF4
		internal RuntimeType MakePointer()
		{
			RuntimeType result = null;
			RuntimeTypeHandle nativeHandle = this.GetNativeHandle();
			RuntimeTypeHandle.MakePointer(new QCallTypeHandle(ref nativeHandle), ObjectHandleOnStack.Create<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060004ED RID: 1261
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern Interop.BOOL IsCollectible(QCallTypeHandle handle);

		// Token: 0x060004EE RID: 1262
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x060004EF RID: 1263
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetGenericTypeDefinition(QCallTypeHandle type, ObjectHandleOnStack retType);

		// Token: 0x060004F0 RID: 1264 RVA: 0x000B8D20 File Offset: 0x000B7F20
		internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
		{
			RuntimeType runtimeType = type;
			if (RuntimeTypeHandle.HasInstantiation(runtimeType) && !RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType))
			{
				RuntimeTypeHandle typeHandleInternal = runtimeType.GetTypeHandleInternal();
				RuntimeTypeHandle.GetGenericTypeDefinition(new QCallTypeHandle(ref typeHandleInternal), ObjectHandleOnStack.Create<RuntimeType>(ref runtimeType));
			}
			return runtimeType;
		}

		// Token: 0x060004F1 RID: 1265
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x060004F3 RID: 1267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenericVariableIndex(RuntimeType type);

		// Token: 0x060004F4 RID: 1268 RVA: 0x000B8D5C File Offset: 0x000B7F5C
		internal int GetGenericVariableIndex()
		{
			RuntimeType typeChecked = this.GetTypeChecked();
			if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
			return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
		}

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ContainsGenericVariables(RuntimeType handle);

		// Token: 0x060004F6 RID: 1270 RVA: 0x000B8D89 File Offset: 0x000B7F89
		internal bool ContainsGenericVariables()
		{
			return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
		}

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

		// Token: 0x060004F8 RID: 1272 RVA: 0x000B8D98 File Offset: 0x000B7F98
		internal unsafe static bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
		{
			int typeContextLength;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeContext, out typeContextLength);
			int methodContextLength;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodContext, out methodContextLength);
			IntPtr[] array3;
			IntPtr* pTypeContext;
			if ((array3 = array) == null || array3.Length == 0)
			{
				pTypeContext = null;
			}
			else
			{
				pTypeContext = &array3[0];
			}
			IntPtr[] array4;
			IntPtr* pMethodContext;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				pMethodContext = null;
			}
			else
			{
				pMethodContext = &array4[0];
			}
			bool result = RuntimeTypeHandle.SatisfiesConstraints(paramType, pTypeContext, typeContextLength, pMethodContext, methodContextLength, toType);
			GC.KeepAlive(typeContext);
			GC.KeepAlive(methodContext);
			return result;
		}

		// Token: 0x060004F9 RID: 1273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeType type);

		// Token: 0x060004FA RID: 1274 RVA: 0x000B8E16 File Offset: 0x000B8016
		internal static MetadataImport GetMetadataImport(RuntimeType type)
		{
			return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), type);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000B3617 File Offset: 0x000B2817
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

		// Token: 0x0400018F RID: 399
		internal RuntimeType m_type;

		// Token: 0x0200007B RID: 123
		internal struct IntroducedMethodEnumerator
		{
			// Token: 0x060004FD RID: 1277 RVA: 0x000B8E24 File Offset: 0x000B8024
			internal IntroducedMethodEnumerator(RuntimeType type)
			{
				this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
				this._firstCall = true;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x000B8E3C File Offset: 0x000B803C
			public bool MoveNext()
			{
				if (this._firstCall)
				{
					this._firstCall = false;
				}
				else if (this._handle.Value != IntPtr.Zero)
				{
					RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
				}
				return !(this._handle.Value == IntPtr.Zero);
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x000B8E94 File Offset: 0x000B8094
			public RuntimeMethodHandleInternal Current
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x000B8E9C File Offset: 0x000B809C
			public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000190 RID: 400
			private bool _firstCall;

			// Token: 0x04000191 RID: 401
			private RuntimeMethodHandleInternal _handle;
		}
	}
}
