using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200064A RID: 1610
	internal sealed class MethodBuilderInstantiation : MethodInfo
	{
		// Token: 0x06005115 RID: 20757 RVA: 0x001949D9 File Offset: 0x00193BD9
		internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
		{
			if (!method.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return new MethodBuilderInstantiation(method, inst);
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x001949F0 File Offset: 0x00193BF0
		internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
		{
			this.m_method = method;
			this.m_inst = inst;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00194A06 File Offset: 0x00193C06
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x00194A13 File Offset: 0x00193C13
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x00194A20 File Offset: 0x00193C20
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600511A RID: 20762 RVA: 0x00194A2D File Offset: 0x00193C2D
		public override Type DeclaringType
		{
			get
			{
				return this.m_method.DeclaringType;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x00194A3A File Offset: 0x00193C3A
		public override Type ReflectedType
		{
			get
			{
				return this.m_method.ReflectedType;
			}
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x00194A47 File Offset: 0x00193C47
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00194A55 File Offset: 0x00193C55
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00194A64 File Offset: 0x00193C64
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x00194A73 File Offset: 0x00193C73
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x000C279F File Offset: 0x000C199F
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x00194A80 File Offset: 0x00193C80
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x00193F4C File Offset: 0x0019314C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicModule);
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x00194A8D File Offset: 0x00193C8D
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x000C279F File Offset: 0x000C199F
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x00194A9A File Offset: 0x00193C9A
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00194AA7 File Offset: 0x00193CA7
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x00194AAF File Offset: 0x00193CAF
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x00194AB8 File Offset: 0x00193CB8
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00194B07 File Offset: 0x00193D07
		public override MethodInfo MakeGenericMethod(params Type[] arguments)
		{
			throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericMethodDefinition, this));
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsGenericMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x00194B19 File Offset: 0x00193D19
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x000C279F File Offset: 0x000C199F
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x000C279F File Offset: 0x000C199F
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x000C279F File Offset: 0x000C199F
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040014E5 RID: 5349
		internal MethodInfo m_method;

		// Token: 0x040014E6 RID: 5350
		private Type[] m_inst;
	}
}
