using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x02000821 RID: 2081
	public static class ComActivator
	{
		// Token: 0x06006286 RID: 25222 RVA: 0x001D434C File Offset: 0x001D354C
		[NullableContext(1)]
		public static object GetClassFactoryForType(ComActivationContext cxt)
		{
			if (cxt.InterfaceId != typeof(IClassFactory).GUID && cxt.InterfaceId != typeof(IClassFactory2).GUID)
			{
				throw new NotSupportedException();
			}
			if (!Path.IsPathRooted(cxt.AssemblyPath))
			{
				throw new ArgumentException(null, "cxt");
			}
			Type type = ComActivator.FindClassType(cxt.ClassId, cxt.AssemblyPath, cxt.AssemblyName, cxt.TypeName);
			if (LicenseInteropProxy.HasLicense(type))
			{
				return new ComActivator.LicenseClassFactory(cxt.ClassId, type);
			}
			return new ComActivator.BasicClassFactory(cxt.ClassId, type);
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x001D43F0 File Offset: 0x001D35F0
		public static void ClassRegistrationScenarioForType(ComActivationContext cxt, bool register)
		{
			string str = register ? "ComRegisterFunctionAttribute" : "ComUnregisterFunctionAttribute";
			Type type = Type.GetType("System.Runtime.InteropServices." + str + ", System.Runtime.InteropServices", false);
			if (type == null)
			{
				return;
			}
			if (!Path.IsPathRooted(cxt.AssemblyPath))
			{
				throw new ArgumentException(null, "cxt");
			}
			Type type2 = ComActivator.FindClassType(cxt.ClassId, cxt.AssemblyPath, cxt.AssemblyName, cxt.TypeName);
			Type type3 = type2;
			bool flag = false;
			while (type3 != null && !flag)
			{
				MethodInfo[] methods = type3.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.GetCustomAttributes(type, true).Length != 0)
					{
						if (!methodInfo.IsStatic)
						{
							string resourceFormat = register ? SR.InvalidOperation_NonStaticComRegFunction : SR.InvalidOperation_NonStaticComUnRegFunction;
							throw new InvalidOperationException(SR.Format(resourceFormat, Array.Empty<object>()));
						}
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (methodInfo.ReturnType != typeof(void) || parameters == null || parameters.Length != 1 || (parameters[0].ParameterType != typeof(string) && parameters[0].ParameterType != typeof(Type)))
						{
							string resourceFormat2 = register ? SR.InvalidOperation_InvalidComRegFunctionSig : SR.InvalidOperation_InvalidComUnRegFunctionSig;
							throw new InvalidOperationException(SR.Format(resourceFormat2, Array.Empty<object>()));
						}
						if (flag)
						{
							string resourceFormat3 = register ? SR.InvalidOperation_MultipleComRegFunctions : SR.InvalidOperation_MultipleComUnRegFunctions;
							throw new InvalidOperationException(SR.Format(resourceFormat3, Array.Empty<object>()));
						}
						object[] array2 = new object[1];
						if (parameters[0].ParameterType == typeof(string))
						{
							array2[0] = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Classes\\CLSID\\" + cxt.ClassId.ToString("B");
						}
						else
						{
							array2[0] = type2;
						}
						methodInfo.Invoke(null, array2);
						flag = true;
					}
				}
				type3 = type3.BaseType;
			}
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x001D45F0 File Offset: 0x001D37F0
		[UnmanagedCallersOnly]
		[CLSCompliant(false)]
		public unsafe static int GetClassFactoryForTypeInternal(ComActivationContextInternal* pCxtInt)
		{
			ref ComActivationContextInternal ptr = ref *pCxtInt;
			if (ComActivator.IsLoggingEnabled())
			{
			}
			try
			{
				ComActivationContext cxt = ComActivationContext.Create(ref ptr);
				object classFactoryForType = ComActivator.GetClassFactoryForType(cxt);
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(classFactoryForType);
				Marshal.WriteIntPtr(ptr.ClassFactoryDest, iunknownForObject);
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x001D464C File Offset: 0x001D384C
		[CLSCompliant(false)]
		[UnmanagedCallersOnly]
		public unsafe static int RegisterClassForTypeInternal(ComActivationContextInternal* pCxtInt)
		{
			ref ComActivationContextInternal ptr = ref *pCxtInt;
			if (ComActivator.IsLoggingEnabled())
			{
			}
			if (ptr.InterfaceId != Guid.Empty || ptr.ClassFactoryDest != IntPtr.Zero)
			{
				throw new ArgumentException(null, "pCxtInt");
			}
			try
			{
				ComActivationContext cxt = ComActivationContext.Create(ref ptr);
				ComActivator.ClassRegistrationScenarioForType(cxt, true);
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x001D46C0 File Offset: 0x001D38C0
		[CLSCompliant(false)]
		[UnmanagedCallersOnly]
		public unsafe static int UnregisterClassForTypeInternal(ComActivationContextInternal* pCxtInt)
		{
			ref ComActivationContextInternal ptr = ref *pCxtInt;
			if (ComActivator.IsLoggingEnabled())
			{
			}
			if (ptr.InterfaceId != Guid.Empty || ptr.ClassFactoryDest != IntPtr.Zero)
			{
				throw new ArgumentException(null, "pCxtInt");
			}
			try
			{
				ComActivationContext cxt = ComActivationContext.Create(ref ptr);
				ComActivator.ClassRegistrationScenarioForType(cxt, false);
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x000AC09B File Offset: 0x000AB29B
		private static bool IsLoggingEnabled()
		{
			return false;
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001D4734 File Offset: 0x001D3934
		private static Type FindClassType(Guid clsid, string assemblyPath, string assemblyName, string typeName)
		{
			try
			{
				AssemblyLoadContext alc = ComActivator.GetALC(assemblyPath);
				AssemblyName assemblyName2 = new AssemblyName(assemblyName);
				Assembly assembly = alc.LoadFromAssemblyName(assemblyName2);
				Type type = assembly.GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}
			catch (Exception ex)
			{
				if (ComActivator.IsLoggingEnabled())
				{
				}
			}
			throw new COMException(string.Empty, -2147221231);
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x001D479C File Offset: 0x001D399C
		private static AssemblyLoadContext GetALC(string assemblyPath)
		{
			Dictionary<string, AssemblyLoadContext> obj = ComActivator.s_assemblyLoadContexts;
			AssemblyLoadContext assemblyLoadContext;
			lock (obj)
			{
				if (!ComActivator.s_assemblyLoadContexts.TryGetValue(assemblyPath, out assemblyLoadContext))
				{
					assemblyLoadContext = new IsolatedComponentLoadContext(assemblyPath);
					ComActivator.s_assemblyLoadContexts.Add(assemblyPath, assemblyLoadContext);
				}
			}
			return assemblyLoadContext;
		}

		// Token: 0x04001D67 RID: 7527
		private static readonly Dictionary<string, AssemblyLoadContext> s_assemblyLoadContexts = new Dictionary<string, AssemblyLoadContext>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x02000822 RID: 2082
		[ComVisible(true)]
		private class BasicClassFactory : IClassFactory
		{
			// Token: 0x0600628F RID: 25231 RVA: 0x001D4809 File Offset: 0x001D3A09
			public BasicClassFactory(Guid clsid, Type classType)
			{
				this._classId = clsid;
				this._classType = classType;
			}

			// Token: 0x06006290 RID: 25232 RVA: 0x001D4820 File Offset: 0x001D3A20
			public static Type GetValidatedInterfaceType(Type classType, ref Guid riid, object outer)
			{
				if (riid == Marshal.IID_IUnknown)
				{
					return typeof(object);
				}
				if (outer != null)
				{
					throw new COMException(string.Empty, -2147221232);
				}
				foreach (Type type in classType.GetInterfaces())
				{
					if (type.GUID == riid)
					{
						return type;
					}
				}
				throw new InvalidCastException();
			}

			// Token: 0x06006291 RID: 25233 RVA: 0x001D4890 File Offset: 0x001D3A90
			public static IntPtr GetObjectAsInterface(object obj, Type interfaceType)
			{
				if (interfaceType == typeof(object))
				{
					return Marshal.GetIUnknownForObject(obj);
				}
				IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(obj, interfaceType, CustomQueryInterfaceMode.Ignore);
				if (comInterfaceForObject == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
				return comInterfaceForObject;
			}

			// Token: 0x06006292 RID: 25234 RVA: 0x001D48D4 File Offset: 0x001D3AD4
			public static object CreateAggregatedObject(object pUnkOuter, object comObject)
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(pUnkOuter);
				object objectForIUnknown;
				try
				{
					IntPtr pUnk = Marshal.CreateAggregatedObject(iunknownForObject, comObject);
					objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
				}
				finally
				{
					Marshal.Release(iunknownForObject);
				}
				return objectForIUnknown;
			}

			// Token: 0x06006293 RID: 25235 RVA: 0x001D4914 File Offset: 0x001D3B14
			public void CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, ref Guid riid, out IntPtr ppvObject)
			{
				Type validatedInterfaceType = ComActivator.BasicClassFactory.GetValidatedInterfaceType(this._classType, ref riid, pUnkOuter);
				object obj = Activator.CreateInstance(this._classType);
				if (pUnkOuter != null)
				{
					obj = ComActivator.BasicClassFactory.CreateAggregatedObject(pUnkOuter, obj);
				}
				ppvObject = ComActivator.BasicClassFactory.GetObjectAsInterface(obj, validatedInterfaceType);
			}

			// Token: 0x06006294 RID: 25236 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock)
			{
			}

			// Token: 0x04001D68 RID: 7528
			private readonly Guid _classId;

			// Token: 0x04001D69 RID: 7529
			private readonly Type _classType;
		}

		// Token: 0x02000823 RID: 2083
		[ComVisible(true)]
		private class LicenseClassFactory : IClassFactory2, IClassFactory
		{
			// Token: 0x06006295 RID: 25237 RVA: 0x001D494F File Offset: 0x001D3B4F
			public LicenseClassFactory(Guid clsid, Type classType)
			{
				this._classId = clsid;
				this._classType = classType;
			}

			// Token: 0x06006296 RID: 25238 RVA: 0x001D4970 File Offset: 0x001D3B70
			public void CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, ref Guid riid, out IntPtr ppvObject)
			{
				this.CreateInstanceInner(pUnkOuter, ref riid, null, true, out ppvObject);
			}

			// Token: 0x06006297 RID: 25239 RVA: 0x000AB30B File Offset: 0x000AA50B
			public void LockServer([MarshalAs(UnmanagedType.Bool)] bool fLock)
			{
			}

			// Token: 0x06006298 RID: 25240 RVA: 0x001D4980 File Offset: 0x001D3B80
			public void GetLicInfo(ref LICINFO licInfo)
			{
				bool fRuntimeKeyAvail;
				bool fLicVerified;
				this._licenseProxy.GetLicInfo(this._classType, out fRuntimeKeyAvail, out fLicVerified);
				licInfo.cbLicInfo = 12;
				licInfo.fRuntimeKeyAvail = fRuntimeKeyAvail;
				licInfo.fLicVerified = fLicVerified;
			}

			// Token: 0x06006299 RID: 25241 RVA: 0x001D49B8 File Offset: 0x001D3BB8
			public void RequestLicKey(int dwReserved, [MarshalAs(UnmanagedType.BStr)] out string pBstrKey)
			{
				pBstrKey = this._licenseProxy.RequestLicKey(this._classType);
			}

			// Token: 0x0600629A RID: 25242 RVA: 0x001D49CD File Offset: 0x001D3BCD
			public void CreateInstanceLic([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, [MarshalAs(UnmanagedType.Interface)] object pUnkReserved, ref Guid riid, [MarshalAs(UnmanagedType.BStr)] string bstrKey, out IntPtr ppvObject)
			{
				this.CreateInstanceInner(pUnkOuter, ref riid, bstrKey, false, out ppvObject);
			}

			// Token: 0x0600629B RID: 25243 RVA: 0x001D49DC File Offset: 0x001D3BDC
			private void CreateInstanceInner(object pUnkOuter, ref Guid riid, string key, bool isDesignTime, out IntPtr ppvObject)
			{
				Type validatedInterfaceType = ComActivator.BasicClassFactory.GetValidatedInterfaceType(this._classType, ref riid, pUnkOuter);
				object obj = this._licenseProxy.AllocateAndValidateLicense(this._classType, key, isDesignTime);
				if (pUnkOuter != null)
				{
					obj = ComActivator.BasicClassFactory.CreateAggregatedObject(pUnkOuter, obj);
				}
				ppvObject = ComActivator.BasicClassFactory.GetObjectAsInterface(obj, validatedInterfaceType);
			}

			// Token: 0x04001D6A RID: 7530
			private readonly LicenseInteropProxy _licenseProxy = new LicenseInteropProxy();

			// Token: 0x04001D6B RID: 7531
			private readonly Guid _classId;

			// Token: 0x04001D6C RID: 7532
			private readonly Type _classType;
		}
	}
}
