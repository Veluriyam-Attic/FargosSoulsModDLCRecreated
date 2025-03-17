using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000657 RID: 1623
	internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
	{
		// Token: 0x06005335 RID: 21301 RVA: 0x0019AB0E File Offset: 0x00199D0E
		internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
		{
			return new MethodOnTypeBuilderInstantiation(method, type);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0019AB17 File Offset: 0x00199D17
		internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
		{
			this.m_method = method;
			this.m_type = type;
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0019AB2D File Offset: 0x00199D2D
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x0019AB3A File Offset: 0x00199D3A
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x0019AB47 File Offset: 0x00199D47
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x0019AB54 File Offset: 0x00199D54
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x0019AB54 File Offset: 0x00199D54
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0019AB5C File Offset: 0x00199D5C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0019AB6A File Offset: 0x00199D6A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0019AB79 File Offset: 0x00199D79
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x0019AB88 File Offset: 0x00199D88
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0019AB95 File Offset: 0x00199D95
		public override ParameterInfo[] GetParameters()
		{
			return this.m_method.GetParameters();
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0019ABA2 File Offset: 0x00199DA2
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06005342 RID: 21314 RVA: 0x0019ABAF File Offset: 0x00199DAF
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_method.MethodHandle;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x0019ABBC File Offset: 0x00199DBC
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x000C279F File Offset: 0x000C199F
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x0019ABC9 File Offset: 0x00199DC9
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0019ABD6 File Offset: 0x00199DD6
		public override Type[] GetGenericArguments()
		{
			return this.m_method.GetGenericArguments();
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0019ABE3 File Offset: 0x00199DE3
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x0019ABEB File Offset: 0x00199DEB
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x0019ABF8 File Offset: 0x00199DF8
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_method.ContainsGenericParameters;
			}
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0019AC05 File Offset: 0x00199E05
		public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericMethodDefinition, this));
			}
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArgs);
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x0019AC27 File Offset: 0x00199E27
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_method.IsGenericMethod;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x0019AC34 File Offset: 0x00199E34
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x000C279F File Offset: 0x000C199F
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600534E RID: 21326 RVA: 0x000C279F File Offset: 0x000C199F
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x000C279F File Offset: 0x000C199F
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001538 RID: 5432
		internal MethodInfo m_method;

		// Token: 0x04001539 RID: 5433
		private TypeBuilderInstantiation m_type;
	}
}
