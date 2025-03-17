using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000651 RID: 1617
	internal sealed class SymbolMethod : MethodInfo
	{
		// Token: 0x060051FF RID: 20991 RVA: 0x00197B68 File Offset: 0x00196D68
		internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.m_mdMethod = token;
			this.m_returnType = (returnType ?? typeof(void));
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = Array.Empty<Type>();
			}
			this.m_module = mod;
			this.m_containingType = arrayClass;
			this.m_name = methodName;
			this.m_callingConvention = callingConvention;
			this.m_signature = SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00197BFD File Offset: 0x00196DFD
		internal override Type[] GetParameterTypes()
		{
			return this.m_parameterTypes;
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00197C05 File Offset: 0x00196E05
		internal MethodToken GetToken(ModuleBuilder mod)
		{
			return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06005202 RID: 20994 RVA: 0x00197C2B File Offset: 0x00196E2B
		public override Module Module
		{
			get
			{
				return this.m_module;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x00197C33 File Offset: 0x00196E33
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x00197C3B File Offset: 0x00196E3B
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x00197C33 File Offset: 0x00196E33
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00197C43 File Offset: 0x00196E43
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00197C43 File Offset: 0x00196E43
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x00197C43 File Offset: 0x00196E43
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SymbolMethod);
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x00197C4F File Offset: 0x00196E4F
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x00197C58 File Offset: 0x00196E58
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SymbolMethod);
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x00197C6F File Offset: 0x00196E6F
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x001908F3 File Offset: 0x0018FAF3
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return new EmptyCAHolder();
			}
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00197C43 File Offset: 0x00196E43
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x000AC098 File Offset: 0x000AB298
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00197C43 File Offset: 0x00196E43
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00197C43 File Offset: 0x00196E43
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00197C43 File Offset: 0x00196E43
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(SR.NotSupported_SymbolMethod);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00197C2B File Offset: 0x00196E2B
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00197C77 File Offset: 0x00196E77
		public MethodToken GetToken()
		{
			return this.m_mdMethod;
		}

		// Token: 0x04001504 RID: 5380
		private ModuleBuilder m_module;

		// Token: 0x04001505 RID: 5381
		private Type m_containingType;

		// Token: 0x04001506 RID: 5382
		private string m_name;

		// Token: 0x04001507 RID: 5383
		private CallingConventions m_callingConvention;

		// Token: 0x04001508 RID: 5384
		private Type m_returnType;

		// Token: 0x04001509 RID: 5385
		private MethodToken m_mdMethod;

		// Token: 0x0400150A RID: 5386
		private Type[] m_parameterTypes;

		// Token: 0x0400150B RID: 5387
		private SignatureHelper m_signature;
	}
}
