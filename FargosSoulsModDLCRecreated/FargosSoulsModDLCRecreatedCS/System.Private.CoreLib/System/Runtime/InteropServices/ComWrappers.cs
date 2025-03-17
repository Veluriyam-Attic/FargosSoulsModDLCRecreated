using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000455 RID: 1109
	[SupportedOSPlatform("windows")]
	[Nullable(0)]
	[NullableContext(1)]
	[CLSCompliant(false)]
	public abstract class ComWrappers
	{
		// Token: 0x060043D5 RID: 17365 RVA: 0x00177828 File Offset: 0x00176A28
		public IntPtr GetOrCreateComInterfaceForObject(object instance, CreateComInterfaceFlags flags)
		{
			IntPtr result;
			if (!ComWrappers.TryGetOrCreateComInterfaceForObjectInternal(this, instance, flags, out result))
			{
				throw new ArgumentException(null, "instance");
			}
			return result;
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x0017784E File Offset: 0x00176A4E
		private static bool TryGetOrCreateComInterfaceForObjectInternal(ComWrappers impl, object instance, CreateComInterfaceFlags flags, out IntPtr retValue)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return ComWrappers.TryGetOrCreateComInterfaceForObjectInternal(ObjectHandleOnStack.Create<ComWrappers>(ref impl), impl.id, ObjectHandleOnStack.Create<object>(ref instance), flags, out retValue);
		}

		// Token: 0x060043D7 RID: 17367
		[DllImport("QCall")]
		private static extern bool TryGetOrCreateComInterfaceForObjectInternal(ObjectHandleOnStack comWrappersImpl, long wrapperId, ObjectHandleOnStack instance, CreateComInterfaceFlags flags, out IntPtr retValue);

		// Token: 0x060043D8 RID: 17368
		[NullableContext(0)]
		protected unsafe abstract ComWrappers.ComInterfaceEntry* ComputeVtables([Nullable(1)] object obj, CreateComInterfaceFlags flags, out int count);

		// Token: 0x060043D9 RID: 17369 RVA: 0x0017787C File Offset: 0x00176A7C
		internal unsafe static void* CallComputeVtables(ComWrappersScenario scenario, ComWrappers comWrappersImpl, object obj, CreateComInterfaceFlags flags, out int count)
		{
			ComWrappers comWrappers = null;
			switch (scenario)
			{
			case ComWrappersScenario.Instance:
				comWrappers = comWrappersImpl;
				break;
			case ComWrappersScenario.TrackerSupportGlobalInstance:
				comWrappers = ComWrappers.s_globalInstanceForTrackerSupport;
				break;
			case ComWrappersScenario.MarshallingGlobalInstance:
				comWrappers = ComWrappers.s_globalInstanceForMarshalling;
				break;
			}
			if (comWrappers == null)
			{
				count = -1;
				return null;
			}
			return (void*)comWrappers.ComputeVtables(obj, flags, out count);
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x001778C8 File Offset: 0x00176AC8
		public object GetOrCreateObjectForComInstance(IntPtr externalComObject, CreateObjectFlags flags)
		{
			object result;
			if (!ComWrappers.TryGetOrCreateObjectForComInstanceInternal(this, externalComObject, flags, null, out result))
			{
				throw new ArgumentNullException("externalComObject");
			}
			return result;
		}

		// Token: 0x060043DB RID: 17371
		[NullableContext(2)]
		protected abstract object CreateObject(IntPtr externalComObject, CreateObjectFlags flags);

		// Token: 0x060043DC RID: 17372 RVA: 0x001778F0 File Offset: 0x00176AF0
		internal static object CallCreateObject(ComWrappersScenario scenario, ComWrappers comWrappersImpl, IntPtr externalComObject, CreateObjectFlags flags)
		{
			ComWrappers comWrappers = null;
			switch (scenario)
			{
			case ComWrappersScenario.Instance:
				comWrappers = comWrappersImpl;
				break;
			case ComWrappersScenario.TrackerSupportGlobalInstance:
				comWrappers = ComWrappers.s_globalInstanceForTrackerSupport;
				break;
			case ComWrappersScenario.MarshallingGlobalInstance:
				comWrappers = ComWrappers.s_globalInstanceForMarshalling;
				break;
			}
			if (comWrappers == null)
			{
				return null;
			}
			return comWrappers.CreateObject(externalComObject, flags);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x00177934 File Offset: 0x00176B34
		public object GetOrRegisterObjectForComInstance(IntPtr externalComObject, CreateObjectFlags flags, object wrapper)
		{
			if (wrapper == null)
			{
				throw new ArgumentNullException("externalComObject");
			}
			object result;
			if (!ComWrappers.TryGetOrCreateObjectForComInstanceInternal(this, externalComObject, flags, wrapper, out result))
			{
				throw new ArgumentNullException("externalComObject");
			}
			return result;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x00177968 File Offset: 0x00176B68
		private static bool TryGetOrCreateObjectForComInstanceInternal(ComWrappers impl, IntPtr externalComObject, CreateObjectFlags flags, object wrapperMaybe, out object retValue)
		{
			if (externalComObject == IntPtr.Zero)
			{
				throw new ArgumentNullException("externalComObject");
			}
			object obj = wrapperMaybe;
			retValue = null;
			return ComWrappers.TryGetOrCreateObjectForComInstanceInternal(ObjectHandleOnStack.Create<ComWrappers>(ref impl), impl.id, externalComObject, flags, ObjectHandleOnStack.Create<object>(ref obj), ObjectHandleOnStack.Create<object>(ref retValue));
		}

		// Token: 0x060043DF RID: 17375
		[DllImport("QCall")]
		private static extern bool TryGetOrCreateObjectForComInstanceInternal(ObjectHandleOnStack comWrappersImpl, long wrapperId, IntPtr externalComObject, CreateObjectFlags flags, ObjectHandleOnStack wrapper, ObjectHandleOnStack retValue);

		// Token: 0x060043E0 RID: 17376
		protected abstract void ReleaseObjects(IEnumerable objects);

		// Token: 0x060043E1 RID: 17377 RVA: 0x001779B5 File Offset: 0x00176BB5
		internal static void CallReleaseObjects(ComWrappers comWrappersImpl, IEnumerable objects)
		{
			(comWrappersImpl ?? ComWrappers.s_globalInstanceForTrackerSupport).ReleaseObjects(objects);
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x001779C7 File Offset: 0x00176BC7
		public static void RegisterForTrackerSupport(ComWrappers instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (Interlocked.CompareExchange<ComWrappers>(ref ComWrappers.s_globalInstanceForTrackerSupport, instance, null) != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ResetGlobalComWrappersInstance);
			}
			ComWrappers.SetGlobalInstanceRegisteredForTrackerSupport(instance.id);
		}

		// Token: 0x060043E3 RID: 17379
		[SuppressGCTransition]
		[DllImport("QCall")]
		private static extern void SetGlobalInstanceRegisteredForTrackerSupport(long id);

		// Token: 0x060043E4 RID: 17380 RVA: 0x001779FB File Offset: 0x00176BFB
		public static void RegisterForMarshalling(ComWrappers instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (Interlocked.CompareExchange<ComWrappers>(ref ComWrappers.s_globalInstanceForMarshalling, instance, null) != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ResetGlobalComWrappersInstance);
			}
			ComWrappers.SetGlobalInstanceRegisteredForMarshalling(instance.id);
		}

		// Token: 0x060043E5 RID: 17381
		[SuppressGCTransition]
		[DllImport("QCall")]
		private static extern void SetGlobalInstanceRegisteredForMarshalling(long id);

		// Token: 0x060043E6 RID: 17382 RVA: 0x00177A2F File Offset: 0x00176C2F
		protected static void GetIUnknownImpl(out IntPtr fpQueryInterface, out IntPtr fpAddRef, out IntPtr fpRelease)
		{
			ComWrappers.GetIUnknownImplInternal(out fpQueryInterface, out fpAddRef, out fpRelease);
		}

		// Token: 0x060043E7 RID: 17383
		[DllImport("QCall")]
		private static extern void GetIUnknownImplInternal(out IntPtr fpQueryInterface, out IntPtr fpAddRef, out IntPtr fpRelease);

		// Token: 0x060043E8 RID: 17384 RVA: 0x00177A3C File Offset: 0x00176C3C
		internal static int CallICustomQueryInterface(object customQueryInterfaceMaybe, ref Guid iid, out IntPtr ppObject)
		{
			ICustomQueryInterface customQueryInterface = customQueryInterfaceMaybe as ICustomQueryInterface;
			if (customQueryInterface == null)
			{
				ppObject = IntPtr.Zero;
				return -1;
			}
			return (int)customQueryInterface.GetInterface(ref iid, out ppObject);
		}

		// Token: 0x04000EC4 RID: 3780
		private static ComWrappers s_globalInstanceForTrackerSupport;

		// Token: 0x04000EC5 RID: 3781
		private static ComWrappers s_globalInstanceForMarshalling;

		// Token: 0x04000EC6 RID: 3782
		private static long s_instanceCounter;

		// Token: 0x04000EC7 RID: 3783
		private readonly long id = Interlocked.Increment(ref ComWrappers.s_instanceCounter);

		// Token: 0x02000456 RID: 1110
		[NullableContext(0)]
		public struct ComInterfaceEntry
		{
			// Token: 0x04000EC8 RID: 3784
			public Guid IID;

			// Token: 0x04000EC9 RID: 3785
			public IntPtr Vtable;
		}

		// Token: 0x02000457 RID: 1111
		[NullableContext(0)]
		public struct ComInterfaceDispatch
		{
			// Token: 0x060043EA RID: 17386 RVA: 0x00177A7C File Offset: 0x00176C7C
			[NullableContext(1)]
			public unsafe static T GetInstance<T>([Nullable(0)] ComWrappers.ComInterfaceDispatch* dispatchPtr) where T : class
			{
				ComWrappers.ComInterfaceDispatch.ComInterfaceInstance* ptr = *((IntPtr*)dispatchPtr & -16L);
				return Unsafe.As<T>(GCHandle.InternalGet(ptr->GcHandle));
			}

			// Token: 0x04000ECA RID: 3786
			public IntPtr Vtable;

			// Token: 0x02000458 RID: 1112
			private struct ComInterfaceInstance
			{
				// Token: 0x04000ECB RID: 3787
				public IntPtr GcHandle;
			}
		}
	}
}
