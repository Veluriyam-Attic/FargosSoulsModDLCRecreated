using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x02000818 RID: 2072
	public static class ComponentActivator
	{
		// Token: 0x0600626F RID: 25199 RVA: 0x001D3F54 File Offset: 0x001D3154
		private static string MarshalToString(IntPtr arg, string argName)
		{
			string text = Marshal.PtrToStringAuto(arg);
			if (text == null)
			{
				throw new ArgumentNullException(argName);
			}
			return text;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x001D3F74 File Offset: 0x001D3174
		[UnmanagedCallersOnly]
		public unsafe static int LoadAssemblyAndGetFunctionPointer(IntPtr assemblyPathNative, IntPtr typeNameNative, IntPtr methodNameNative, IntPtr delegateTypeNative, IntPtr reserved, IntPtr functionHandle)
		{
			try
			{
				string assemblyPath = ComponentActivator.MarshalToString(assemblyPathNative, "assemblyPathNative");
				string typeName = ComponentActivator.MarshalToString(typeNameNative, "typeNameNative");
				string methodName = ComponentActivator.MarshalToString(methodNameNative, "methodNameNative");
				if (reserved != IntPtr.Zero)
				{
					throw new ArgumentOutOfRangeException("reserved");
				}
				if (functionHandle == IntPtr.Zero)
				{
					throw new ArgumentNullException("functionHandle");
				}
				AssemblyLoadContext isolatedComponentLoadContext = ComponentActivator.GetIsolatedComponentLoadContext(assemblyPath);
				*(IntPtr*)((void*)functionHandle) = ComponentActivator.InternalGetFunctionPointer(isolatedComponentLoadContext, typeName, methodName, delegateTypeNative);
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x001D4014 File Offset: 0x001D3214
		[UnmanagedCallersOnly]
		public unsafe static int GetFunctionPointer(IntPtr typeNameNative, IntPtr methodNameNative, IntPtr delegateTypeNative, IntPtr loadContext, IntPtr reserved, IntPtr functionHandle)
		{
			try
			{
				string typeName = ComponentActivator.MarshalToString(typeNameNative, "typeNameNative");
				string methodName = ComponentActivator.MarshalToString(methodNameNative, "methodNameNative");
				if (loadContext != IntPtr.Zero)
				{
					throw new ArgumentOutOfRangeException("loadContext");
				}
				if (reserved != IntPtr.Zero)
				{
					throw new ArgumentOutOfRangeException("reserved");
				}
				if (functionHandle == IntPtr.Zero)
				{
					throw new ArgumentNullException("functionHandle");
				}
				*(IntPtr*)((void*)functionHandle) = ComponentActivator.InternalGetFunctionPointer(AssemblyLoadContext.Default, typeName, methodName, delegateTypeNative);
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x001D40B8 File Offset: 0x001D32B8
		private static IsolatedComponentLoadContext GetIsolatedComponentLoadContext(string assemblyPath)
		{
			Dictionary<string, IsolatedComponentLoadContext> obj = ComponentActivator.s_assemblyLoadContexts;
			IsolatedComponentLoadContext isolatedComponentLoadContext;
			lock (obj)
			{
				if (!ComponentActivator.s_assemblyLoadContexts.TryGetValue(assemblyPath, out isolatedComponentLoadContext))
				{
					isolatedComponentLoadContext = new IsolatedComponentLoadContext(assemblyPath);
					ComponentActivator.s_assemblyLoadContexts.Add(assemblyPath, isolatedComponentLoadContext);
				}
			}
			return isolatedComponentLoadContext;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x001D4114 File Offset: 0x001D3314
		private static IntPtr InternalGetFunctionPointer(AssemblyLoadContext alc, string typeName, string methodName, IntPtr delegateTypeNative)
		{
			Func<AssemblyName, Assembly> assemblyResolver = (AssemblyName name) => alc.LoadFromAssemblyName(name);
			Type type;
			if (delegateTypeNative == IntPtr.Zero)
			{
				type = typeof(ComponentActivator.ComponentEntryPoint);
			}
			else if (delegateTypeNative == (IntPtr)(-1))
			{
				type = null;
			}
			else
			{
				string typeName2 = ComponentActivator.MarshalToString(delegateTypeNative, "delegateTypeNative");
				type = Type.GetType(typeName2, assemblyResolver, null, true);
			}
			Type type2 = Type.GetType(typeName, assemblyResolver, null, true);
			IntPtr intPtr;
			if (type == null)
			{
				BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
				MethodInfo method = type2.GetMethod(methodName, bindingAttr);
				if (method == null)
				{
					throw new MissingMethodException(typeName, methodName);
				}
				if (method.GetCustomAttribute<UnmanagedCallersOnlyAttribute>() == null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_FunctionMissingUnmanagedCallersOnly);
				}
				intPtr = method.MethodHandle.GetFunctionPointer();
			}
			else
			{
				Delegate @delegate = Delegate.CreateDelegate(type, type2, methodName);
				intPtr = Marshal.GetFunctionPointerForDelegate(@delegate);
				Dictionary<IntPtr, Delegate> obj = ComponentActivator.s_delegates;
				lock (obj)
				{
					ComponentActivator.s_delegates[intPtr] = @delegate;
				}
			}
			return intPtr;
		}

		// Token: 0x04001D55 RID: 7509
		private static readonly Dictionary<string, IsolatedComponentLoadContext> s_assemblyLoadContexts = new Dictionary<string, IsolatedComponentLoadContext>(StringComparer.InvariantCulture);

		// Token: 0x04001D56 RID: 7510
		private static readonly Dictionary<IntPtr, Delegate> s_delegates = new Dictionary<IntPtr, Delegate>();

		// Token: 0x02000819 RID: 2073
		// (Invoke) Token: 0x06006276 RID: 25206
		public delegate int ComponentEntryPoint(IntPtr args, int sizeBytes);
	}
}
