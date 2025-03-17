using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Security;
using System.StubHelpers;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200044D RID: 1101
	[NullableContext(1)]
	[Nullable(0)]
	public static class Marshal
	{
		// Token: 0x060042E0 RID: 17120
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int SizeOfHelper(Type t, bool throwIfNotMarshalable);

		// Token: 0x060042E1 RID: 17121 RVA: 0x00175744 File Offset: 0x00174944
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "Trimming doesn't affect types eligible for marshalling. Different exception for invalid inputs doesn't matter.")]
		public static IntPtr OffsetOf(Type t, string fieldName)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			FieldInfo field = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentException(SR.Format(SR.Argument_OffsetOfFieldNotFound, t.FullName), "fieldName");
			}
			RtFieldInfo rtFieldInfo = field as RtFieldInfo;
			if (rtFieldInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeFieldInfo, "fieldName");
			}
			return Marshal.OffsetOfHelper(rtFieldInfo);
		}

		// Token: 0x060042E2 RID: 17122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OffsetOfHelper(IRuntimeFieldInfo f);

		// Token: 0x060042E3 RID: 17123 RVA: 0x001757A7 File Offset: 0x001749A7
		public static byte ReadByte(object ptr, int ofs)
		{
			return Marshal.ReadValueSlow<byte>(ptr, ofs, (IntPtr nativeHome, int offset) => Marshal.ReadByte(nativeHome, offset));
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x001757CF File Offset: 0x001749CF
		public static short ReadInt16(object ptr, int ofs)
		{
			return Marshal.ReadValueSlow<short>(ptr, ofs, (IntPtr nativeHome, int offset) => Marshal.ReadInt16(nativeHome, offset));
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x001757F7 File Offset: 0x001749F7
		public static int ReadInt32(object ptr, int ofs)
		{
			return Marshal.ReadValueSlow<int>(ptr, ofs, (IntPtr nativeHome, int offset) => Marshal.ReadInt32(nativeHome, offset));
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0017581F File Offset: 0x00174A1F
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			return Marshal.ReadValueSlow<long>(ptr, ofs, (IntPtr nativeHome, int offset) => Marshal.ReadInt64(nativeHome, offset));
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x00175848 File Offset: 0x00174A48
		private unsafe static T ReadValueSlow<T>(object ptr, int ofs, Func<IntPtr, int, T> readValueHelper)
		{
			if (ptr == null)
			{
				throw new AccessViolationException();
			}
			MngdNativeArrayMarshaler.MarshalerState marshalerState = default(MngdNativeArrayMarshaler.MarshalerState);
			AsAnyMarshaler asAnyMarshaler = new AsAnyMarshaler(new IntPtr((void*)(&marshalerState)));
			IntPtr intPtr = IntPtr.Zero;
			T result;
			try
			{
				intPtr = asAnyMarshaler.ConvertToNative(ptr, 285147391);
				result = readValueHelper(intPtr, ofs);
			}
			finally
			{
				asAnyMarshaler.ClearNative(intPtr);
			}
			return result;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x001758B0 File Offset: 0x00174AB0
		public static void WriteByte(object ptr, int ofs, byte val)
		{
			Marshal.WriteValueSlow<byte>(ptr, ofs, val, delegate(IntPtr nativeHome, int offset, byte value)
			{
				Marshal.WriteByte(nativeHome, offset, value);
			});
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x001758D9 File Offset: 0x00174AD9
		public static void WriteInt16(object ptr, int ofs, short val)
		{
			Marshal.WriteValueSlow<short>(ptr, ofs, val, delegate(IntPtr nativeHome, int offset, short value)
			{
				Marshal.WriteInt16(nativeHome, offset, value);
			});
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x00175902 File Offset: 0x00174B02
		public static void WriteInt32(object ptr, int ofs, int val)
		{
			Marshal.WriteValueSlow<int>(ptr, ofs, val, delegate(IntPtr nativeHome, int offset, int value)
			{
				Marshal.WriteInt32(nativeHome, offset, value);
			});
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0017592B File Offset: 0x00174B2B
		public static void WriteInt64(object ptr, int ofs, long val)
		{
			Marshal.WriteValueSlow<long>(ptr, ofs, val, delegate(IntPtr nativeHome, int offset, long value)
			{
				Marshal.WriteInt64(nativeHome, offset, value);
			});
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x00175954 File Offset: 0x00174B54
		private unsafe static void WriteValueSlow<T>(object ptr, int ofs, T val, Action<IntPtr, int, T> writeValueHelper)
		{
			if (ptr == null)
			{
				throw new AccessViolationException();
			}
			MngdNativeArrayMarshaler.MarshalerState marshalerState = default(MngdNativeArrayMarshaler.MarshalerState);
			AsAnyMarshaler asAnyMarshaler = new AsAnyMarshaler(new IntPtr((void*)(&marshalerState)));
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = asAnyMarshaler.ConvertToNative(ptr, 822018303);
				writeValueHelper(intPtr, ofs, val);
				asAnyMarshaler.ConvertToManaged(ptr, intPtr);
			}
			finally
			{
				asAnyMarshaler.ClearNative(intPtr);
			}
		}

		// Token: 0x060042ED RID: 17133
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		// Token: 0x060042EE RID: 17134
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastWin32Error(int error);

		// Token: 0x060042EF RID: 17135 RVA: 0x001759C4 File Offset: 0x00174BC4
		private static void PrelinkCore(MethodInfo m)
		{
			RuntimeMethodInfo runtimeMethodInfo = m as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "m");
			}
			Marshal.InternalPrelink(((IRuntimeMethodInfo)runtimeMethodInfo).Value);
			GC.KeepAlive(runtimeMethodInfo);
		}

		// Token: 0x060042F0 RID: 17136
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InternalPrelink(RuntimeMethodHandleInternal m);

		// Token: 0x060042F1 RID: 17137
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetExceptionPointers();

		// Token: 0x060042F2 RID: 17138
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetExceptionCode();

		// Token: 0x060042F3 RID: 17139
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		// Token: 0x060042F4 RID: 17140 RVA: 0x001759FC File Offset: 0x00174BFC
		private static object PtrToStructureHelper(IntPtr ptr, Type structureType)
		{
			RuntimeType runtimeType = (RuntimeType)structureType;
			object obj = runtimeType.CreateInstanceDefaultCtor(false, false, false, true);
			Marshal.PtrToStructureHelper(ptr, obj, true);
			return obj;
		}

		// Token: 0x060042F5 RID: 17141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PtrToStructureHelper(IntPtr ptr, object structure, bool allowValueClasses);

		// Token: 0x060042F6 RID: 17142
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		// Token: 0x060042F7 RID: 17143
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsPinnable(object obj);

		// Token: 0x060042F8 RID: 17144 RVA: 0x00175A24 File Offset: 0x00174C24
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeModule runtimeModule = m as RuntimeModule;
			if (runtimeModule != null)
			{
				return Marshal.GetHINSTANCE(new QCallModule(ref runtimeModule));
			}
			return (IntPtr)(-1);
		}

		// Token: 0x060042F9 RID: 17145
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetHINSTANCE(QCallModule m);

		// Token: 0x060042FA RID: 17146
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception GetExceptionForHRInternal(int errorCode, IntPtr errorInfo);

		// Token: 0x060042FB RID: 17147 RVA: 0x00175A5C File Offset: 0x00174C5C
		public static IntPtr AllocHGlobal(IntPtr cb)
		{
			UIntPtr uBytes = new UIntPtr((ulong)cb.ToInt64());
			IntPtr intPtr = Interop.Kernel32.LocalAlloc(0U, uBytes);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x00175A93 File Offset: 0x00174C93
		public static void FreeHGlobal(IntPtr hglobal)
		{
			if (!Marshal.IsNullOrWin32Atom(hglobal) && IntPtr.Zero != Interop.Kernel32.LocalFree(hglobal))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x00175ABC File Offset: 0x00174CBC
		public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
		{
			IntPtr intPtr = Interop.Kernel32.LocalReAlloc(pv, cb, 2U);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x060042FE RID: 17150
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHRForException(Exception e);

		// Token: 0x060042FF RID: 17151 RVA: 0x00175AE8 File Offset: 0x00174CE8
		[SupportedOSPlatform("windows")]
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			string result;
			string text;
			int num;
			string text2;
			typeInfo.GetDocumentation(-1, out result, out text, out num, out text2);
			return result;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x00175B13 File Offset: 0x00174D13
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, null, false);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x00175B1D File Offset: 0x00174D1D
		[SupportedOSPlatform("windows")]
		public static IntPtr GetIUnknownForObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			return Marshal.GetIUnknownForObjectNative(o, false);
		}

		// Token: 0x06004302 RID: 17154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectNative(object o, bool onlyInContext);

		// Token: 0x06004303 RID: 17155 RVA: 0x00175B34 File Offset: 0x00174D34
		[SupportedOSPlatform("windows")]
		public static IntPtr GetIDispatchForObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			return Marshal.GetIDispatchForObjectNative(o, false);
		}

		// Token: 0x06004304 RID: 17156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectNative(object o, bool onlyInContext);

		// Token: 0x06004305 RID: 17157 RVA: 0x00175B4B File Offset: 0x00174D4B
		[SupportedOSPlatform("windows")]
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			if (T == null)
			{
				throw new ArgumentNullException("T");
			}
			return Marshal.GetComInterfaceForObjectNative(o, T, false, true);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x00175B72 File Offset: 0x00174D72
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		public static IntPtr GetComInterfaceForObject<T, TInterface>([DisallowNull] [Nullable(1)] T o)
		{
			return Marshal.GetComInterfaceForObject(o, typeof(TInterface));
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x00175B8C File Offset: 0x00174D8C
		[SupportedOSPlatform("windows")]
		public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			if (T == null)
			{
				throw new ArgumentNullException("T");
			}
			bool fEnableCustomizedQueryInterface = mode == CustomQueryInterfaceMode.Allow;
			return Marshal.GetComInterfaceForObjectNative(o, T, false, fEnableCustomizedQueryInterface);
		}

		// Token: 0x06004308 RID: 17160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetComInterfaceForObjectNative(object o, Type t, bool onlyInContext, bool fEnableCustomizedQueryInterface);

		// Token: 0x06004309 RID: 17161 RVA: 0x00175BC7 File Offset: 0x00174DC7
		[SupportedOSPlatform("windows")]
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.GetObjectForIUnknownNative(pUnk);
		}

		// Token: 0x0600430A RID: 17162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetObjectForIUnknownNative(IntPtr pUnk);

		// Token: 0x0600430B RID: 17163 RVA: 0x00175BE7 File Offset: 0x00174DE7
		[SupportedOSPlatform("windows")]
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			if (unknown == IntPtr.Zero)
			{
				throw new ArgumentNullException("unknown");
			}
			return Marshal.GetUniqueObjectForIUnknownNative(unknown);
		}

		// Token: 0x0600430C RID: 17164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetUniqueObjectForIUnknownNative(IntPtr unknown);

		// Token: 0x0600430D RID: 17165
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetTypedObjectForIUnknown(IntPtr pUnk, Type t);

		// Token: 0x0600430E RID: 17166
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateAggregatedObject(IntPtr pOuter, object o);

		// Token: 0x0600430F RID: 17167 RVA: 0x00175C07 File Offset: 0x00174E07
		[SupportedOSPlatform("windows")]
		public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
		{
			return Marshal.CreateAggregatedObject(pOuter, o);
		}

		// Token: 0x06004310 RID: 17168
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CleanupUnusedObjectsInCurrentContext();

		// Token: 0x06004311 RID: 17169
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool AreComObjectsAvailableForCleanup();

		// Token: 0x06004312 RID: 17170
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		// Token: 0x06004313 RID: 17171 RVA: 0x00175C18 File Offset: 0x00174E18
		public static IntPtr AllocCoTaskMem(int cb)
		{
			IntPtr intPtr = Interop.Ole32.CoTaskMemAlloc(new UIntPtr((uint)cb));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x00175C45 File Offset: 0x00174E45
		public static void FreeCoTaskMem(IntPtr ptr)
		{
			if (!Marshal.IsNullOrWin32Atom(ptr))
			{
				Interop.Ole32.CoTaskMemFree(ptr);
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x00175C58 File Offset: 0x00174E58
		public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
		{
			IntPtr intPtr = Interop.Ole32.CoTaskMemRealloc(pv, new UIntPtr((uint)cb));
			if (intPtr == IntPtr.Zero && cb != 0)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x00175C8C File Offset: 0x00174E8C
		internal static IntPtr AllocBSTR(int length)
		{
			IntPtr intPtr = Interop.OleAut32.SysAllocStringLen(null, length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x00175CB5 File Offset: 0x00174EB5
		public static void FreeBSTR(IntPtr ptr)
		{
			if (!Marshal.IsNullOrWin32Atom(ptr))
			{
				Interop.OleAut32.SysFreeString(ptr);
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x00175CC8 File Offset: 0x00174EC8
		[NullableContext(2)]
		public static IntPtr StringToBSTR(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = Interop.OleAut32.SysAllocStringLen(s, s.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			return intPtr;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x00175CFF File Offset: 0x00174EFF
		public static string PtrToStringBSTR(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.PtrToStringUni(ptr, (int)(Marshal.SysStringByteLen(ptr) / 2U));
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x00175D28 File Offset: 0x00174F28
		[SupportedOSPlatform("windows")]
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new NullReferenceException();
			}
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException(SR.Argument_ObjNotComObject, "o");
			}
			return _ComObject.ReleaseSelf();
		}

		// Token: 0x0600431B RID: 17179
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalReleaseComObject(object o);

		// Token: 0x0600431C RID: 17180 RVA: 0x00175D60 File Offset: 0x00174F60
		[SupportedOSPlatform("windows")]
		public static int FinalReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException(SR.Argument_ObjNotComObject, "o");
			}
			_ComObject.FinalReleaseSelf();
			return 0;
		}

		// Token: 0x0600431D RID: 17181
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalFinalReleaseComObject(object o);

		// Token: 0x0600431E RID: 17182 RVA: 0x00175D9C File Offset: 0x00174F9C
		[SupportedOSPlatform("windows")]
		[return: Nullable(2)]
		public static object GetComObjectData(object obj, object key)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			__ComObject _ComObject = obj as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException(SR.Argument_ObjNotComObject, "obj");
			}
			return _ComObject.GetData(key);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x00175DE8 File Offset: 0x00174FE8
		[SupportedOSPlatform("windows")]
		public static bool SetComObjectData(object obj, object key, [Nullable(2)] object data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			__ComObject _ComObject = obj as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException(SR.Argument_ObjNotComObject, "obj");
			}
			return _ComObject.SetData(key, data);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x00175E34 File Offset: 0x00175034
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		[return: NotNullIfNotNull("o")]
		public static object CreateWrapperOfType(object o, [Nullable(1)] Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsCOMObject)
			{
				throw new ArgumentException(SR.Argument_TypeNotComObject, "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "t");
			}
			if (o == null)
			{
				return null;
			}
			if (!o.GetType().IsCOMObject)
			{
				throw new ArgumentException(SR.Argument_ObjNotComObject, "o");
			}
			if (o.GetType() == t)
			{
				return o;
			}
			object obj = Marshal.GetComObjectData(o, t);
			if (obj == null)
			{
				obj = Marshal.InternalCreateWrapperOfType(o, t);
				if (!Marshal.SetComObjectData(o, t, obj))
				{
					obj = Marshal.GetComObjectData(o, t);
				}
			}
			return obj;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x00175ED7 File Offset: 0x001750D7
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		[return: Nullable(1)]
		public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
		{
			return (TWrapper)((object)Marshal.CreateWrapperOfType(o, typeof(TWrapper)));
		}

		// Token: 0x06004322 RID: 17186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalCreateWrapperOfType(object o, Type t);

		// Token: 0x06004323 RID: 17187
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTypeVisibleFromCom(Type t);

		// Token: 0x06004324 RID: 17188 RVA: 0x00175EF4 File Offset: 0x001750F4
		[SupportedOSPlatform("windows")]
		public unsafe static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			fixed (Guid* ptr = &iid)
			{
				Guid* ptr2 = ptr;
				fixed (IntPtr* ptr3 = &ppv)
				{
					IntPtr* ptr4 = ptr3;
					method <<<NULL>>> = *(*(IntPtr*)((void*)pUnk));
					pUnk;
					ptr2;
					ptr4;
					int result = <<<NULL>>>;
					calli();
					return result;
				}
			}
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x00175F38 File Offset: 0x00175138
		[SupportedOSPlatform("windows")]
		public unsafe static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			method <<<NULL>>> = *(*(IntPtr*)((void*)pUnk) + (IntPtr)sizeof(void*));
			pUnk;
			int result = <<<NULL>>>;
			calli();
			return result;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x00175F74 File Offset: 0x00175174
		[SupportedOSPlatform("windows")]
		public unsafe static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			method <<<NULL>>> = *(*(IntPtr*)((void*)pUnk) + (IntPtr)2 * (IntPtr)sizeof(void*));
			pUnk;
			int result = <<<NULL>>>;
			calli();
			return result;
		}

		// Token: 0x06004327 RID: 17191
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant);

		// Token: 0x06004328 RID: 17192 RVA: 0x00175FB3 File Offset: 0x001751B3
		[SupportedOSPlatform("windows")]
		[NullableContext(2)]
		public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
		{
			Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
		}

		// Token: 0x06004329 RID: 17193
		[SupportedOSPlatform("windows")]
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectForNativeVariant(IntPtr pSrcNativeVariant);

		// Token: 0x0600432A RID: 17194 RVA: 0x00175FC1 File Offset: 0x001751C1
		[NullableContext(2)]
		[SupportedOSPlatform("windows")]
		public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
		{
			return (T)((object)Marshal.GetObjectForNativeVariant(pSrcNativeVariant));
		}

		// Token: 0x0600432B RID: 17195
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public static extern object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars);

		// Token: 0x0600432C RID: 17196 RVA: 0x00175FD0 File Offset: 0x001751D0
		[SupportedOSPlatform("windows")]
		public static T[] GetObjectsForNativeVariants<[Nullable(2)] T>(IntPtr aSrcNativeVariant, int cVars)
		{
			object[] objectsForNativeVariants = Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
			T[] array = new T[objectsForNativeVariants.Length];
			Array.Copy(objectsForNativeVariants, array, objectsForNativeVariants.Length);
			return array;
		}

		// Token: 0x0600432D RID: 17197
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStartComSlot(Type t);

		// Token: 0x0600432E RID: 17198
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetEndComSlot(Type t);

		// Token: 0x0600432F RID: 17199 RVA: 0x00175FFC File Offset: 0x001751FC
		[SupportedOSPlatform("windows")]
		public static object BindToMoniker(string monikerName)
		{
			IBindCtx pbc;
			Marshal.CreateBindCtx(0U, out pbc);
			uint num;
			IMoniker pmk;
			Marshal.MkParseDisplayName(pbc, monikerName, out num, out pmk);
			object result;
			Marshal.BindMoniker(pmk, 0U, ref Marshal.IID_IUnknown, out result);
			return result;
		}

		// Token: 0x06004330 RID: 17200
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void CreateBindCtx(uint reserved, out IBindCtx ppbc);

		// Token: 0x06004331 RID: 17201
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void MkParseDisplayName(IBindCtx pbc, [MarshalAs(UnmanagedType.LPWStr)] string szUserName, out uint pchEaten, out IMoniker ppmk);

		// Token: 0x06004332 RID: 17202
		[DllImport("ole32.dll", PreserveSig = false)]
		private static extern void BindMoniker(IMoniker pmk, uint grfOpt, ref Guid iidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x06004333 RID: 17203
		[SupportedOSPlatform("windows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ChangeWrapperHandleStrength(object otp, bool fIsWeak);

		// Token: 0x06004334 RID: 17204
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		// Token: 0x06004335 RID: 17205
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		// Token: 0x06004336 RID: 17206 RVA: 0x0017602B File Offset: 0x0017522B
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x00176038 File Offset: 0x00175238
		[NullableContext(2)]
		public unsafe static string PtrToStringAnsi(IntPtr ptr)
		{
			if (Marshal.IsNullOrWin32Atom(ptr))
			{
				return null;
			}
			return new string((sbyte*)((void*)ptr));
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x00176050 File Offset: 0x00175250
		public unsafe static string PtrToStringAnsi(IntPtr ptr, int len)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len", len, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return new string((sbyte*)((void*)ptr), 0, len);
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0017609C File Offset: 0x0017529C
		[NullableContext(2)]
		public unsafe static string PtrToStringUni(IntPtr ptr)
		{
			if (Marshal.IsNullOrWin32Atom(ptr))
			{
				return null;
			}
			return new string((char*)((void*)ptr));
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x001760B4 File Offset: 0x001752B4
		public unsafe static string PtrToStringUni(IntPtr ptr, int len)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len", len, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return new string((char*)((void*)ptr), 0, len);
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x00176100 File Offset: 0x00175300
		[NullableContext(2)]
		public unsafe static string PtrToStringUTF8(IntPtr ptr)
		{
			if (Marshal.IsNullOrWin32Atom(ptr))
			{
				return null;
			}
			int byteLength = string.strlen((byte*)((void*)ptr));
			return string.CreateStringFromEncoding((byte*)((void*)ptr), byteLength, Encoding.UTF8);
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x00176134 File Offset: 0x00175334
		public unsafe static string PtrToStringUTF8(IntPtr ptr, int byteLen)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (byteLen < 0)
			{
				throw new ArgumentOutOfRangeException("byteLen", byteLen, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return string.CreateStringFromEncoding((byte*)((void*)ptr), byteLen, Encoding.UTF8);
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x00176184 File Offset: 0x00175384
		public static int SizeOf(object structure)
		{
			if (structure == null)
			{
				throw new ArgumentNullException("structure");
			}
			return Marshal.SizeOfHelper(structure.GetType(), true);
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x001761A0 File Offset: 0x001753A0
		public static int SizeOf<[Nullable(2)] T>(T structure)
		{
			if (structure == null)
			{
				throw new ArgumentNullException("structure");
			}
			return Marshal.SizeOfHelper(structure.GetType(), true);
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x001761C8 File Offset: 0x001753C8
		public static int SizeOf(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "t");
			}
			return Marshal.SizeOfHelper(t, true);
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x0017621A File Offset: 0x0017541A
		[NullableContext(2)]
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x0017622C File Offset: 0x0017542C
		public unsafe static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index)
		{
			if (arr == null)
			{
				throw new ArgumentNullException("arr");
			}
			void* ptr = Unsafe.AsPointer<byte>(arr.GetRawArrayData());
			return (IntPtr)((void*)((byte*)ptr + (ulong)((UIntPtr)index * (UIntPtr)arr.GetElementSize())));
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x00176268 File Offset: 0x00175468
		public unsafe static IntPtr UnsafeAddrOfPinnedArrayElement<[Nullable(2)] T>(T[] arr, int index)
		{
			if (arr == null)
			{
				throw new ArgumentNullException("arr");
			}
			void* ptr = Unsafe.AsPointer<T>(MemoryMarshal.GetArrayDataReference<T>(arr));
			return (IntPtr)((void*)((byte*)ptr + (ulong)((UIntPtr)index * (UIntPtr)((IntPtr)Unsafe.SizeOf<T>()))));
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x001762A1 File Offset: 0x001754A1
		public static IntPtr OffsetOf<[Nullable(2)] T>(string fieldName)
		{
			return Marshal.OffsetOf(typeof(T), fieldName);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x001762B3 File Offset: 0x001754B3
		public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<int>(source, startIndex, destination, length);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x001762BE File Offset: 0x001754BE
		public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<char>(source, startIndex, destination, length);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x001762C9 File Offset: 0x001754C9
		public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<short>(source, startIndex, destination, length);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x001762D4 File Offset: 0x001754D4
		public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<long>(source, startIndex, destination, length);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x001762DF File Offset: 0x001754DF
		public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<float>(source, startIndex, destination, length);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x001762EA File Offset: 0x001754EA
		public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<double>(source, startIndex, destination, length);
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x001762F5 File Offset: 0x001754F5
		public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<byte>(source, startIndex, destination, length);
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x00176300 File Offset: 0x00175500
		public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.CopyToNative<IntPtr>(source, startIndex, destination, length);
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x0017630C File Offset: 0x0017550C
		private unsafe static void CopyToNative<T>(T[] source, int startIndex, IntPtr destination, int length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == IntPtr.Zero)
			{
				throw new ArgumentNullException("destination");
			}
			new Span<T>(source, startIndex, length).CopyTo(new Span<T>((void*)destination, length));
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x0017635B File Offset: 0x0017555B
		public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<int>(source, destination, startIndex, length);
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x00176366 File Offset: 0x00175566
		public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<char>(source, destination, startIndex, length);
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x00176371 File Offset: 0x00175571
		public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<short>(source, destination, startIndex, length);
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x0017637C File Offset: 0x0017557C
		public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<long>(source, destination, startIndex, length);
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x00176387 File Offset: 0x00175587
		public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<float>(source, destination, startIndex, length);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x00176392 File Offset: 0x00175592
		public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<double>(source, destination, startIndex, length);
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x0017639D File Offset: 0x0017559D
		public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<byte>(source, destination, startIndex, length);
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x001763A8 File Offset: 0x001755A8
		public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			Marshal.CopyToManaged<IntPtr>(source, destination, startIndex, length);
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x001763B4 File Offset: 0x001755B4
		private unsafe static void CopyToManaged<T>(IntPtr source, T[] destination, int startIndex, int length)
		{
			if (source == IntPtr.Zero)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			new Span<T>((void*)source, length).CopyTo(new Span<T>(destination, startIndex, length));
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x0017642C File Offset: 0x0017562C
		public unsafe static byte ReadByte(IntPtr ptr, int ofs)
		{
			byte result;
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				result = *ptr2;
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return result;
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x00176460 File Offset: 0x00175660
		public static byte ReadByte(IntPtr ptr)
		{
			return Marshal.ReadByte(ptr, 0);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x0017646C File Offset: 0x0017566C
		public unsafe static short ReadInt16(IntPtr ptr, int ofs)
		{
			short result;
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 1) == 0)
				{
					result = *(short*)ptr2;
				}
				else
				{
					result = Unsafe.ReadUnaligned<short>((void*)ptr2);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return result;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x001764B0 File Offset: 0x001756B0
		public static short ReadInt16(IntPtr ptr)
		{
			return Marshal.ReadInt16(ptr, 0);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x001764BC File Offset: 0x001756BC
		public unsafe static int ReadInt32(IntPtr ptr, int ofs)
		{
			int result;
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 3) == 0)
				{
					result = *(int*)ptr2;
				}
				else
				{
					result = Unsafe.ReadUnaligned<int>((void*)ptr2);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return result;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x00176500 File Offset: 0x00175700
		public static int ReadInt32(IntPtr ptr)
		{
			return Marshal.ReadInt32(ptr, 0);
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x00176509 File Offset: 0x00175709
		public static IntPtr ReadIntPtr(object ptr, int ofs)
		{
			return (IntPtr)Marshal.ReadInt64(ptr, ofs);
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x00176517 File Offset: 0x00175717
		public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
		{
			return (IntPtr)Marshal.ReadInt64(ptr, ofs);
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x00176525 File Offset: 0x00175725
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			return Marshal.ReadIntPtr(ptr, 0);
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x00176530 File Offset: 0x00175730
		public unsafe static long ReadInt64(IntPtr ptr, int ofs)
		{
			long result;
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 7) == 0)
				{
					result = *(long*)ptr2;
				}
				else
				{
					result = Unsafe.ReadUnaligned<long>((void*)ptr2);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
			return result;
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x00176574 File Offset: 0x00175774
		public static long ReadInt64(IntPtr ptr)
		{
			return Marshal.ReadInt64(ptr, 0);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x00176580 File Offset: 0x00175780
		public unsafe static void WriteByte(IntPtr ptr, int ofs, byte val)
		{
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				*ptr2 = val;
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x001765B4 File Offset: 0x001757B4
		public static void WriteByte(IntPtr ptr, byte val)
		{
			Marshal.WriteByte(ptr, 0, val);
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x001765C0 File Offset: 0x001757C0
		public unsafe static void WriteInt16(IntPtr ptr, int ofs, short val)
		{
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 1) == 0)
				{
					*(short*)ptr2 = val;
				}
				else
				{
					Unsafe.WriteUnaligned<short>((void*)ptr2, val);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x00176604 File Offset: 0x00175804
		public static void WriteInt16(IntPtr ptr, short val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0017660E File Offset: 0x0017580E
		public static void WriteInt16(IntPtr ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x00176619 File Offset: 0x00175819
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x00176624 File Offset: 0x00175824
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, (short)val);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x00176630 File Offset: 0x00175830
		public unsafe static void WriteInt32(IntPtr ptr, int ofs, int val)
		{
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 3) == 0)
				{
					*(int*)ptr2 = val;
				}
				else
				{
					Unsafe.WriteUnaligned<int>((void*)ptr2, val);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x00176674 File Offset: 0x00175874
		public static void WriteInt32(IntPtr ptr, int val)
		{
			Marshal.WriteInt32(ptr, 0, val);
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x0017667E File Offset: 0x0017587E
		public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
		{
			Marshal.WriteInt64(ptr, ofs, (long)val);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x0017668D File Offset: 0x0017588D
		public static void WriteIntPtr(object ptr, int ofs, IntPtr val)
		{
			Marshal.WriteInt64(ptr, ofs, (long)val);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x0017669C File Offset: 0x0017589C
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			Marshal.WriteIntPtr(ptr, 0, val);
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x001766A8 File Offset: 0x001758A8
		public unsafe static void WriteInt64(IntPtr ptr, int ofs, long val)
		{
			try
			{
				byte* ptr2 = (byte*)((void*)ptr) + ofs;
				if ((ptr2 & 7) == 0)
				{
					*(long*)ptr2 = val;
				}
				else
				{
					Unsafe.WriteUnaligned<long>((void*)ptr2, val);
				}
			}
			catch (NullReferenceException)
			{
				throw new AccessViolationException();
			}
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x001766EC File Offset: 0x001758EC
		public static void WriteInt64(IntPtr ptr, long val)
		{
			Marshal.WriteInt64(ptr, 0, val);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x001766F6 File Offset: 0x001758F6
		public static void Prelink(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			Marshal.PrelinkCore(m);
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x0017670C File Offset: 0x0017590C
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "This only needs to prelink methods that are actually used")]
		public static void PrelinkAll(Type c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			MethodInfo[] methods = c.GetMethods();
			for (int i = 0; i < methods.Length; i++)
			{
				Marshal.Prelink(methods[i]);
			}
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x00176744 File Offset: 0x00175944
		public static void StructureToPtr<[Nullable(2)] T>([DisallowNull] T structure, IntPtr ptr, bool fDeleteOld)
		{
			Marshal.StructureToPtr(structure, ptr, fDeleteOld);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x00176754 File Offset: 0x00175954
		[return: Nullable(2)]
		public static object PtrToStructure(IntPtr ptr, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type structureType)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			if (structureType == null)
			{
				throw new ArgumentNullException("structureType");
			}
			if (structureType.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "structureType");
			}
			if (!structureType.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "structureType");
			}
			return Marshal.PtrToStructureHelper(ptr, structureType);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x001767B5 File Offset: 0x001759B5
		public static void PtrToStructure(IntPtr ptr, object structure)
		{
			Marshal.PtrToStructureHelper(ptr, structure, false);
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x001767BF File Offset: 0x001759BF
		public static void PtrToStructure<[Nullable(2)] T>(IntPtr ptr, [DisallowNull] T structure)
		{
			Marshal.PtrToStructure(ptr, structure);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x001767CD File Offset: 0x001759CD
		[NullableContext(2)]
		public static T PtrToStructure<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x001767E4 File Offset: 0x001759E4
		[NullableContext(2)]
		public static void DestroyStructure<T>(IntPtr ptr)
		{
			Marshal.DestroyStructure(ptr, typeof(T));
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001767F6 File Offset: 0x001759F6
		[NullableContext(2)]
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x00176803 File Offset: 0x00175A03
		[NullableContext(2)]
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode >= 0)
			{
				return null;
			}
			return Marshal.GetExceptionForHRInternal(errorCode, errorInfo);
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x00176812 File Offset: 0x00175A12
		public static void ThrowExceptionForHR(int errorCode)
		{
			if (errorCode < 0)
			{
				throw Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x00176824 File Offset: 0x00175A24
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode < 0)
			{
				throw Marshal.GetExceptionForHR(errorCode, errorInfo);
			}
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x00176832 File Offset: 0x00175A32
		public static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.MarshalToBSTR();
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x00176848 File Offset: 0x00175A48
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.MarshalToString(false, false);
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x00176860 File Offset: 0x00175A60
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.MarshalToString(false, true);
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x00176878 File Offset: 0x00175A78
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.MarshalToString(true, false);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00176890 File Offset: 0x00175A90
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return s.MarshalToString(true, true);
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x001768A8 File Offset: 0x00175AA8
		[NullableContext(2)]
		public unsafe static IntPtr StringToHGlobalAnsi(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			long num = (long)(s.Length + 1) * (long)Marshal.SystemMaxDBCSCharSize;
			int num2 = (int)num;
			if ((long)num2 != num)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocHGlobal((IntPtr)num2);
			Marshal.StringToAnsiString(s, (byte*)((void*)intPtr), num2, false, false);
			return intPtr;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x00176900 File Offset: 0x00175B00
		[NullableContext(2)]
		public unsafe static IntPtr StringToHGlobalUni(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 2;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocHGlobal((IntPtr)num);
			char* ptr;
			if (s == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = s.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* smem = ptr;
			string.wstrcpy((char*)((void*)intPtr), smem, s.Length + 1);
			char* ptr2 = null;
			return intPtr;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x0017696C File Offset: 0x00175B6C
		[NullableContext(2)]
		public unsafe static IntPtr StringToCoTaskMemUni(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 2;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			char* ptr;
			if (s == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = s.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* smem = ptr;
			string.wstrcpy((char*)((void*)intPtr), smem, s.Length + 1);
			char* ptr2 = null;
			return intPtr;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x001769D4 File Offset: 0x00175BD4
		[NullableContext(2)]
		public unsafe static IntPtr StringToCoTaskMemUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int maxByteCount = Encoding.UTF8.GetMaxByteCount(s.Length);
			IntPtr intPtr = Marshal.AllocCoTaskMem(maxByteCount + 1);
			byte* ptr = (byte*)((void*)intPtr);
			char* ptr2;
			if (s == null)
			{
				ptr2 = null;
			}
			else
			{
				fixed (char* ptr3 = s.GetPinnableReference())
				{
					ptr2 = ptr3;
				}
			}
			char* chars = ptr2;
			int bytes = Encoding.UTF8.GetBytes(chars, s.Length, ptr, maxByteCount);
			char* ptr3 = null;
			ptr[bytes] = 0;
			return intPtr;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00176A40 File Offset: 0x00175C40
		[NullableContext(2)]
		public unsafe static IntPtr StringToCoTaskMemAnsi(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			long num = (long)(s.Length + 1) * (long)Marshal.SystemMaxDBCSCharSize;
			int num2 = (int)num;
			if ((long)num2 != num)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocCoTaskMem(num2);
			Marshal.StringToAnsiString(s, (byte*)((void*)intPtr), num2, false, false);
			return intPtr;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00176A92 File Offset: 0x00175C92
		public static Guid GenerateGuidForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			return type.GUID;
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00176AC0 File Offset: 0x00175CC0
		[return: Nullable(2)]
		public static string GenerateProgIdForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsImport)
			{
				throw new ArgumentException(SR.Argument_TypeMustNotBeComImport, "type");
			}
			if (type.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "type");
			}
			ProgIdAttribute customAttribute = type.GetCustomAttribute<ProgIdAttribute>();
			if (customAttribute != null)
			{
				return customAttribute.Value ?? string.Empty;
			}
			return type.FullName;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00176B2C File Offset: 0x00175D2C
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_NeedNonGenericType, "t");
			}
			Type baseType = t.BaseType;
			if (baseType != typeof(Delegate) && baseType != typeof(MulticastDelegate))
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "t");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00176BD1 File Offset: 0x00175DD1
		public static TDelegate GetDelegateForFunctionPointer<[Nullable(2)] TDelegate>(IntPtr ptr)
		{
			return (TDelegate)((object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate)));
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00176BE8 File Offset: 0x00175DE8
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00176BFE File Offset: 0x00175DFE
		public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
		{
			return Marshal.GetFunctionPointerForDelegate((Delegate)((object)d));
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00176C10 File Offset: 0x00175E10
		public static int GetHRForLastWin32Error()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (((long)lastWin32Error & (long)((ulong)-2147483648)) == (long)((ulong)-2147483648))
			{
				return lastWin32Error;
			}
			return (lastWin32Error & 65535) | -2147024896;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00176C43 File Offset: 0x00175E43
		public unsafe static void ZeroFreeBSTR(IntPtr s)
		{
			if (s == IntPtr.Zero)
			{
				return;
			}
			Buffer.ZeroMemory((byte*)((void*)s), (UIntPtr)Marshal.SysStringByteLen(s));
			Marshal.FreeBSTR(s);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00176C6B File Offset: 0x00175E6B
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ZeroFreeCoTaskMemUTF8(s);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00176C73 File Offset: 0x00175E73
		public unsafe static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			if (s == IntPtr.Zero)
			{
				return;
			}
			Buffer.ZeroMemory((byte*)((void*)s), (UIntPtr)((IntPtr)string.wcslen((char*)((void*)s)) * (IntPtr)2));
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00176CA3 File Offset: 0x00175EA3
		public unsafe static void ZeroFreeCoTaskMemUTF8(IntPtr s)
		{
			if (s == IntPtr.Zero)
			{
				return;
			}
			Buffer.ZeroMemory((byte*)((void*)s), (UIntPtr)((IntPtr)string.strlen((byte*)((void*)s))));
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x00176CD0 File Offset: 0x00175ED0
		public unsafe static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			if (s == IntPtr.Zero)
			{
				return;
			}
			Buffer.ZeroMemory((byte*)((void*)s), (UIntPtr)((IntPtr)string.strlen((byte*)((void*)s))));
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x00176CFD File Offset: 0x00175EFD
		public unsafe static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			if (s == IntPtr.Zero)
			{
				return;
			}
			Buffer.ZeroMemory((byte*)((void*)s), (UIntPtr)((IntPtr)string.wcslen((char*)((void*)s)) * (IntPtr)2));
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x00176D2D File Offset: 0x00175F2D
		internal unsafe static uint SysStringByteLen(IntPtr s)
		{
			return *(uint*)((byte*)((void*)s) - 4);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00176D38 File Offset: 0x00175F38
		[NullableContext(2)]
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			return Marshal.PtrToStringUni(ptr, len);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00176D41 File Offset: 0x00175F41
		[NullableContext(2)]
		public static string PtrToStringAuto(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00176D49 File Offset: 0x00175F49
		[NullableContext(2)]
		public static IntPtr StringToHGlobalAuto(string s)
		{
			return Marshal.StringToHGlobalUni(s);
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00176D51 File Offset: 0x00175F51
		[NullableContext(2)]
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			return Marshal.StringToCoTaskMemUni(s);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x00176D5C File Offset: 0x00175F5C
		private unsafe static int GetSystemMaxDBCSCharSize()
		{
			Interop.Kernel32.CPINFO cpinfo = default(Interop.Kernel32.CPINFO);
			if (Interop.Kernel32.GetCPInfo(0U, &cpinfo) == Interop.BOOL.FALSE)
			{
				return 2;
			}
			return cpinfo.MaxCharSize;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x00176D84 File Offset: 0x00175F84
		private static bool IsNullOrWin32Atom(IntPtr ptr)
		{
			long num = (long)ptr;
			return (num & -65536L) == 0L;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x00176DA4 File Offset: 0x00175FA4
		internal unsafe static int StringToAnsiString(string s, byte* buffer, int bufferLength, bool bestFit = false, bool throwOnUnmappableChar = false)
		{
			uint dwFlags = bestFit ? 0U : 1024U;
			uint num = 0U;
			char* ptr;
			if (s == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = s.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* lpWideCharStr = ptr;
			int num2 = Interop.Kernel32.WideCharToMultiByte(0U, dwFlags, lpWideCharStr, s.Length, buffer, bufferLength, IntPtr.Zero, throwOnUnmappableChar ? new IntPtr((void*)(&num)) : IntPtr.Zero);
			char* ptr2 = null;
			if (num != 0U)
			{
				throw new ArgumentException(SR.Interop_Marshal_Unmappable_Char);
			}
			buffer[num2] = 0;
			return num2;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x00176E14 File Offset: 0x00176014
		internal unsafe static int GetAnsiStringByteCount(ReadOnlySpan<char> chars)
		{
			int num;
			if (chars.Length == 0)
			{
				num = 0;
			}
			else
			{
				fixed (char* pinnableReference = chars.GetPinnableReference())
				{
					char* lpWideCharStr = pinnableReference;
					num = Interop.Kernel32.WideCharToMultiByte(0U, 1024U, lpWideCharStr, chars.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
					if (num <= 0)
					{
						throw new ArgumentException();
					}
				}
			}
			return checked(num + 1);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00176E6C File Offset: 0x0017606C
		internal unsafe static void GetAnsiStringBytes(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			int num;
			if (chars.Length == 0)
			{
				num = 0;
			}
			else
			{
				fixed (char* pinnableReference = chars.GetPinnableReference())
				{
					char* lpWideCharStr = pinnableReference;
					fixed (byte* pinnableReference2 = bytes.GetPinnableReference())
					{
						byte* lpMultiByteStr = pinnableReference2;
						num = Interop.Kernel32.WideCharToMultiByte(0U, 1024U, lpWideCharStr, chars.Length, lpMultiByteStr, bytes.Length, IntPtr.Zero, IntPtr.Zero);
						if (num <= 0)
						{
							throw new ArgumentException();
						}
					}
				}
			}
			*bytes[num] = 0;
		}

		// Token: 0x04000EA5 RID: 3749
		internal static Guid IID_IUnknown = new Guid(0, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

		// Token: 0x04000EA6 RID: 3750
		public static readonly int SystemDefaultCharSize = 2;

		// Token: 0x04000EA7 RID: 3751
		public static readonly int SystemMaxDBCSCharSize = Marshal.GetSystemMaxDBCSCharSize();
	}
}
