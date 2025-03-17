using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005B1 RID: 1457
	internal class RuntimeModule : Module
	{
		// Token: 0x06004B50 RID: 19280 RVA: 0x0018926F File Offset: 0x0018846F
		internal RuntimeModule()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004B51 RID: 19281
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(QCallModule module, string className, bool throwOnError, bool ignoreCase, ObjectHandleOnStack type, ObjectHandleOnStack keepAlive);

		// Token: 0x06004B52 RID: 19282
		[DllImport("QCall")]
		private static extern bool nIsTransientInternal(QCallModule module);

		// Token: 0x06004B53 RID: 19283
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetScopeName(QCallModule module, StringHandleOnStack retString);

		// Token: 0x06004B54 RID: 19284
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullyQualifiedName(QCallModule module, StringHandleOnStack retString);

		// Token: 0x06004B55 RID: 19285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType[] GetTypes(RuntimeModule module);

		// Token: 0x06004B56 RID: 19286 RVA: 0x0018927C File Offset: 0x0018847C
		internal RuntimeType[] GetDefinedTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x06004B57 RID: 19287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsResource(RuntimeModule module);

		// Token: 0x06004B58 RID: 19288 RVA: 0x0018928C File Offset: 0x0018848C
		private static RuntimeTypeHandle[] ConvertToTypeHandleArray(Type[] genericArguments)
		{
			if (genericArguments == null)
			{
				return null;
			}
			int num = genericArguments.Length;
			RuntimeTypeHandle[] array = new RuntimeTypeHandle[num];
			for (int i = 0; i < num; i++)
			{
				Type type = genericArguments[i];
				if (type == null)
				{
					throw new ArgumentException(SR.Argument_InvalidGenericInstArray);
				}
				type = type.UnderlyingSystemType;
				if (type == null)
				{
					throw new ArgumentException(SR.Argument_InvalidGenericInstArray);
				}
				if (!(type is RuntimeType))
				{
					throw new ArgumentException(SR.Argument_InvalidGenericInstArray);
				}
				array[i] = type.GetTypeHandleInternal();
			}
			return array;
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x0018930C File Offset: 0x0018850C
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override byte[] ResolveSignature(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			if (!metadataToken2.IsMemberRef && !metadataToken2.IsMethodDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsSignature && !metadataToken2.IsFieldDef)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidToken, metadataToken2, this), "metadataToken");
			}
			ConstArray constArray;
			if (metadataToken2.IsMemberRef)
			{
				constArray = this.MetadataImport.GetMemberRefProps(metadataToken);
			}
			else
			{
				constArray = this.MetadataImport.GetSignatureFromToken(metadataToken);
			}
			byte[] array = new byte[constArray.Length];
			for (int i = 0; i < constArray.Length; i++)
			{
				array[i] = constArray[i];
			}
			return array;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x001893F8 File Offset: 0x001885F8
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public unsafe override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			MethodBase methodBase;
			try
			{
				if (!metadataToken2.IsMethodDef && !metadataToken2.IsMethodSpec)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException(SR.Format(SR.Argument_ResolveMethod, metadataToken2, this), "metadataToken");
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
					{
						throw new ArgumentException(SR.Format(SR.Argument_ResolveMethod, metadataToken2, this), "metadataToken");
					}
				}
				IRuntimeMethodInfo runtimeMethodInfo = ModuleHandle.ResolveMethodHandleInternal(this.GetNativeHandle(), metadataToken2, typeInstantiationContext, methodInstantiationContext);
				Type type = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if (type.IsGenericType || type.IsArray)
				{
					MetadataToken token = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken2));
					if (metadataToken2.IsMethodSpec)
					{
						token = new MetadataToken(this.MetadataImport.GetParentToken(token));
					}
					type = this.ResolveType(token, genericTypeArguments, genericMethodArguments);
				}
				methodBase = RuntimeType.GetMethodBase(type as RuntimeType, runtimeMethodInfo);
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(SR.Argument_BadImageFormatExceptionResolve, innerException);
			}
			return methodBase;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0018958C File Offset: 0x0018878C
		private FieldInfo ResolveLiteralField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2) || !metadataToken2.IsFieldDef)
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			string name = this.MetadataImport.GetName(metadataToken2).ToString();
			int parentToken = this.MetadataImport.GetParentToken(metadataToken2);
			Type type = this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
			type.GetFields();
			FieldInfo field;
			try
			{
				field = type.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			catch
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveField, metadataToken2, this), "metadataToken");
			}
			return field;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00189668 File Offset: 0x00188868
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public unsafe override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			FieldInfo result;
			try
			{
				IRuntimeFieldInfo runtimeFieldInfo;
				if (!metadataToken2.IsFieldDef)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException(SR.Format(SR.Argument_ResolveField, metadataToken2, this), "metadataToken");
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() != 6)
					{
						throw new ArgumentException(SR.Format(SR.Argument_ResolveField, metadataToken2, this), "metadataToken");
					}
					runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken2, typeInstantiationContext, methodInstantiationContext);
				}
				runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken, typeInstantiationContext, methodInstantiationContext);
				RuntimeType runtimeType = RuntimeFieldHandle.GetApproxDeclaringType(runtimeFieldInfo.Value);
				if (runtimeType.IsGenericType || runtimeType.IsArray)
				{
					int parentToken = ModuleHandle.GetMetadataImport(this.GetNativeHandle()).GetParentToken(metadataToken);
					runtimeType = (RuntimeType)this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
				}
				result = RuntimeType.GetFieldInfo(runtimeType, runtimeFieldInfo);
			}
			catch (MissingFieldException)
			{
				result = this.ResolveLiteralField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(SR.Argument_BadImageFormatExceptionResolve, innerException);
			}
			return result;
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x001897F0 File Offset: 0x001889F0
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsGlobalTypeDefToken)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveModuleType, metadataToken2), "metadataToken");
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			if (!metadataToken2.IsTypeDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsTypeRef)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveType, metadataToken2, this), "metadataToken");
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			Type result;
			try
			{
				Type runtimeType = this.GetModuleHandleImpl().ResolveTypeHandle(metadataToken, typeInstantiationContext, methodInstantiationContext).GetRuntimeType();
				if (runtimeType == null)
				{
					throw new ArgumentException(SR.Format(SR.Argument_ResolveType, metadataToken2, this), "metadataToken");
				}
				result = runtimeType;
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(SR.Argument_BadImageFormatExceptionResolve, innerException);
			}
			return result;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x0018990C File Offset: 0x00188B0C
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public unsafe override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsProperty)
			{
				throw new ArgumentException(SR.InvalidOperation_PropertyInfoNotAvailable);
			}
			if (metadataToken2.IsEvent)
			{
				throw new ArgumentException(SR.InvalidOperation_EventInfoNotAvailable);
			}
			if (metadataToken2.IsMethodSpec || metadataToken2.IsMethodDef)
			{
				return this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsFieldDef)
			{
				return this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsTypeRef || metadataToken2.IsTypeDef || metadataToken2.IsTypeSpec)
			{
				return this.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (!metadataToken2.IsMemberRef)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveMember, metadataToken2, this), "metadataToken");
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
			{
				return this.ResolveField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			return this.ResolveMethod(metadataToken2, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00189A3C File Offset: 0x00188C3C
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public override string ResolveString(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!metadataToken2.IsString)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveString, metadataToken, this));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", SR.Format(SR.Argument_InvalidToken, metadataToken2, this));
			}
			string userString = this.MetadataImport.GetUserString(metadataToken);
			if (userString == null)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResolveString, metadataToken, this));
			}
			return userString;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00189ACE File Offset: 0x00188CCE
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			ModuleHandle.GetPEKind(this.GetNativeHandle(), out peKind, out machine);
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x00189ADD File Offset: 0x00188CDD
		public override int MDStreamVersion
		{
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetNativeHandle());
			}
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00189AEA File Offset: 0x00188CEA
		[RequiresUnreferencedCode("Methods might be removed")]
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00189AFB File Offset: 0x00188CFB
		internal MethodInfo GetMethodInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.RuntimeType == null)
			{
				return null;
			}
			if (types == null)
			{
				return this.RuntimeType.GetMethod(name, bindingAttr);
			}
			return this.RuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00189B34 File Offset: 0x00188D34
		internal RuntimeType RuntimeType
		{
			get
			{
				RuntimeType result;
				if ((result = this.m_runtimeType) == null)
				{
					result = (this.m_runtimeType = ModuleHandle.GetModuleType(this));
				}
				return result;
			}
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x00189B5C File Offset: 0x00188D5C
		internal bool IsTransientInternal()
		{
			RuntimeModule runtimeModule = this;
			return RuntimeModule.nIsTransientInternal(new QCallModule(ref runtimeModule));
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004B66 RID: 19302 RVA: 0x00189B77 File Offset: 0x00188D77
		internal MetadataImport MetadataImport
		{
			get
			{
				return ModuleHandle.GetMetadataImport(this);
			}
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00189B7F File Offset: 0x00188D7F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00189B98 File Offset: 0x00188D98
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00189BE8 File Offset: 0x00188DE8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x00189C35 File Offset: 0x00188E35
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000B3617 File Offset: 0x000B2817
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00189C40 File Offset: 0x00188E40
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			RuntimeType result = null;
			object obj = null;
			RuntimeModule runtimeModule = this;
			RuntimeModule.GetType(new QCallModule(ref runtimeModule), className, throwOnError, ignoreCase, ObjectHandleOnStack.Create<RuntimeType>(ref result), ObjectHandleOnStack.Create<object>(ref obj));
			GC.KeepAlive(obj);
			return result;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00189C88 File Offset: 0x00188E88
		internal string GetFullyQualifiedName()
		{
			string result = null;
			RuntimeModule runtimeModule = this;
			RuntimeModule.GetFullyQualifiedName(new QCallModule(ref runtimeModule), new StringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00189CAD File Offset: 0x00188EAD
		public override string FullyQualifiedName
		{
			get
			{
				return this.GetFullyQualifiedName();
			}
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00189CB8 File Offset: 0x00188EB8
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type[] GetTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00189CD4 File Offset: 0x00188ED4
		public override Guid ModuleVersionId
		{
			get
			{
				Guid result;
				this.MetadataImport.GetScopeProps(out result);
				return result;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x00189CF2 File Offset: 0x00188EF2
		public override int MetadataToken
		{
			get
			{
				return ModuleHandle.GetToken(this.GetNativeHandle());
			}
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00189CFF File Offset: 0x00188EFF
		public override bool IsResource()
		{
			return RuntimeModule.IsResource(this.GetNativeHandle());
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00189D0C File Offset: 0x00188F0C
		[RequiresUnreferencedCode("Fields might be removed")]
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return Array.Empty<FieldInfo>();
			}
			return this.RuntimeType.GetFields(bindingFlags);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00189D2E File Offset: 0x00188F2E
		[RequiresUnreferencedCode("Fields might be removed")]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.RuntimeType == null)
			{
				return null;
			}
			return this.RuntimeType.GetField(name, bindingAttr);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00189D5B File Offset: 0x00188F5B
		[RequiresUnreferencedCode("Methods might be removed")]
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return Array.Empty<MethodInfo>();
			}
			return this.RuntimeType.GetMethods(bindingFlags);
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x00189D80 File Offset: 0x00188F80
		public override string ScopeName
		{
			get
			{
				string result = null;
				RuntimeModule runtimeModule = this;
				RuntimeModule.GetScopeName(new QCallModule(ref runtimeModule), new StringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x00189DA8 File Offset: 0x00188FA8
		public override string Name
		{
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				int num = fullyQualifiedName.LastIndexOf(Path.DirectorySeparatorChar);
				if (num == -1)
				{
					return fullyQualifiedName;
				}
				return fullyQualifiedName.Substring(num + 1);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004B78 RID: 19320 RVA: 0x00189DD7 File Offset: 0x00188FD7
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00189DDF File Offset: 0x00188FDF
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_runtimeAssembly;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00189DE7 File Offset: 0x00188FE7
		protected override ModuleHandle GetModuleHandleImpl()
		{
			return new ModuleHandle(this);
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x000AC098 File Offset: 0x000AB298
		internal RuntimeModule GetNativeHandle()
		{
			return this;
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00189DEF File Offset: 0x00188FEF
		internal IntPtr GetUnderlyingNativeHandle()
		{
			return this.m_pData;
		}

		// Token: 0x040012B2 RID: 4786
		private RuntimeType m_runtimeType;

		// Token: 0x040012B3 RID: 4787
		private RuntimeAssembly m_runtimeAssembly;

		// Token: 0x040012B4 RID: 4788
		private IntPtr m_pRefClass;

		// Token: 0x040012B5 RID: 4789
		private IntPtr m_pData;

		// Token: 0x040012B6 RID: 4790
		private IntPtr m_pGlobals;

		// Token: 0x040012B7 RID: 4791
		private IntPtr m_pFields;
	}
}
