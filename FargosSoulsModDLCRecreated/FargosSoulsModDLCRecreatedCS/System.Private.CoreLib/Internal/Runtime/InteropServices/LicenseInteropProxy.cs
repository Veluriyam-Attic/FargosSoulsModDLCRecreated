using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x02000824 RID: 2084
	internal sealed class LicenseInteropProxy
	{
		// Token: 0x0600629C RID: 25244 RVA: 0x001D4A24 File Offset: 0x001D3C24
		public LicenseInteropProxy()
		{
			Type type = Type.GetType("System.ComponentModel.LicenseManager, System.ComponentModel.TypeConverter", true);
			Type type2 = Type.GetType("System.ComponentModel.LicenseContext, System.ComponentModel.TypeConverter", true);
			this._setSavedLicenseKey = type2.GetMethod("SetSavedLicenseKey", BindingFlags.Instance | BindingFlags.Public);
			this._createWithContext = type.GetMethod("CreateWithContext", new Type[]
			{
				typeof(Type),
				type2
			});
			Type nestedType = type.GetNestedType("LicenseInteropHelper", BindingFlags.NonPublic);
			this._validateTypeAndReturnDetails = nestedType.GetMethod("ValidateAndRetrieveLicenseDetails", BindingFlags.Static | BindingFlags.Public);
			this._getCurrentContextInfo = nestedType.GetMethod("GetCurrentContextInfo", BindingFlags.Static | BindingFlags.Public);
			Type nestedType2 = type.GetNestedType("CLRLicenseContext", BindingFlags.NonPublic);
			this._createDesignContext = nestedType2.GetMethod("CreateDesignContext", BindingFlags.Static | BindingFlags.Public);
			this._createRuntimeContext = nestedType2.GetMethod("CreateRuntimeContext", BindingFlags.Static | BindingFlags.Public);
			this._licInfoHelper = type.GetNestedType("LicInfoHelperLicenseContext", BindingFlags.NonPublic);
			this._licInfoHelperContains = this._licInfoHelper.GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x001D4B1D File Offset: 0x001D3D1D
		public static object Create()
		{
			return new LicenseInteropProxy();
		}

		// Token: 0x0600629E RID: 25246 RVA: 0x001D4B24 File Offset: 0x001D3D24
		public static bool HasLicense(Type type)
		{
			return !(LicenseInteropProxy.s_licenseAttrType == null) && type.IsDefined(LicenseInteropProxy.s_licenseAttrType, true);
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x001D4B44 File Offset: 0x001D3D44
		public void GetLicInfo(Type type, out bool runtimeKeyAvail, out bool licVerified)
		{
			runtimeKeyAvail = false;
			licVerified = false;
			object obj = Activator.CreateInstance(this._licInfoHelper);
			object[] array = new object[4];
			array[0] = obj;
			array[1] = type;
			object[] array2 = array;
			if (!(bool)this._validateTypeAndReturnDetails.Invoke(null, BindingFlags.DoNotWrapExceptions, null, array2, null))
			{
				return;
			}
			IDisposable disposable = (IDisposable)array2[2];
			if (disposable != null)
			{
				disposable.Dispose();
				licVerified = true;
			}
			array2 = new object[]
			{
				type.AssemblyQualifiedName
			};
			runtimeKeyAvail = (bool)this._licInfoHelperContains.Invoke(obj, BindingFlags.DoNotWrapExceptions, null, array2, null);
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x001D4BD0 File Offset: 0x001D3DD0
		public string RequestLicKey(Type type)
		{
			object[] array = new object[4];
			array[1] = type;
			object[] array2 = array;
			if (!(bool)this._validateTypeAndReturnDetails.Invoke(null, BindingFlags.DoNotWrapExceptions, null, array2, null))
			{
				throw new COMException();
			}
			IDisposable disposable = (IDisposable)array2[2];
			if (disposable != null)
			{
				disposable.Dispose();
			}
			string text = (string)array2[3];
			if (text == null)
			{
				throw new COMException();
			}
			return text;
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x001D4C30 File Offset: 0x001D3E30
		public object AllocateAndValidateLicense(Type type, string key, bool isDesignTime)
		{
			object obj;
			if (isDesignTime)
			{
				object[] parameters = new object[]
				{
					type
				};
				obj = this._createDesignContext.Invoke(null, BindingFlags.DoNotWrapExceptions, null, parameters, null);
			}
			else
			{
				object[] parameters = new object[]
				{
					type,
					key
				};
				obj = this._createRuntimeContext.Invoke(null, BindingFlags.DoNotWrapExceptions, null, parameters, null);
			}
			object result;
			try
			{
				object[] parameters = new object[]
				{
					type,
					obj
				};
				result = this._createWithContext.Invoke(null, BindingFlags.DoNotWrapExceptions, null, parameters, null);
			}
			catch (Exception ex) when (ex.GetType() == LicenseInteropProxy.s_licenseExceptionType)
			{
				throw new COMException(ex.Message, -2147221230);
			}
			return result;
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x001D4CF4 File Offset: 0x001D3EF4
		public void GetCurrentContextInfo(RuntimeTypeHandle rth, out bool isDesignTime, out IntPtr bstrKey)
		{
			Type typeFromHandle = Type.GetTypeFromHandle(rth);
			object[] array = new object[3];
			array[0] = typeFromHandle;
			object[] array2 = array;
			this._licContext = this._getCurrentContextInfo.Invoke(null, BindingFlags.DoNotWrapExceptions, null, array2, null);
			this._targetRcwType = typeFromHandle;
			isDesignTime = (bool)array2[1];
			bstrKey = Marshal.StringToBSTR((string)array2[2]);
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x001D4D50 File Offset: 0x001D3F50
		public void SaveKeyInCurrentContext(IntPtr bstrKey)
		{
			if (bstrKey == IntPtr.Zero)
			{
				return;
			}
			string text = Marshal.PtrToStringBSTR(bstrKey);
			object[] parameters = new object[]
			{
				this._targetRcwType,
				text
			};
			this._setSavedLicenseKey.Invoke(this._licContext, BindingFlags.DoNotWrapExceptions, null, parameters, null);
		}

		// Token: 0x04001D6D RID: 7533
		private static readonly Type s_licenseAttrType = Type.GetType("System.ComponentModel.LicenseProviderAttribute, System.ComponentModel.TypeConverter", false);

		// Token: 0x04001D6E RID: 7534
		private static readonly Type s_licenseExceptionType = Type.GetType("System.ComponentModel.LicenseException, System.ComponentModel.TypeConverter", false);

		// Token: 0x04001D6F RID: 7535
		private readonly MethodInfo _createWithContext;

		// Token: 0x04001D70 RID: 7536
		private readonly MethodInfo _validateTypeAndReturnDetails;

		// Token: 0x04001D71 RID: 7537
		private readonly MethodInfo _getCurrentContextInfo;

		// Token: 0x04001D72 RID: 7538
		private readonly MethodInfo _createDesignContext;

		// Token: 0x04001D73 RID: 7539
		private readonly MethodInfo _createRuntimeContext;

		// Token: 0x04001D74 RID: 7540
		private readonly MethodInfo _setSavedLicenseKey;

		// Token: 0x04001D75 RID: 7541
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicMethods)]
		private readonly Type _licInfoHelper;

		// Token: 0x04001D76 RID: 7542
		private readonly MethodInfo _licInfoHelperContains;

		// Token: 0x04001D77 RID: 7543
		private object _licContext;

		// Token: 0x04001D78 RID: 7544
		private Type _targetRcwType;
	}
}
