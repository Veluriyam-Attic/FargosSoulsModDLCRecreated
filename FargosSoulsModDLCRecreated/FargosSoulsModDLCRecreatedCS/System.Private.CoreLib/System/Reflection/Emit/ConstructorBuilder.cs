using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000615 RID: 1557
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConstructorBuilder : ConstructorInfo
	{
		// Token: 0x06004EBF RID: 20159 RVA: 0x0018DCAC File Offset: 0x0018CEAC
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TypeBuilder type)
		{
			this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type);
			type.m_listMethods.Add(this.m_methodBuilder);
			int num;
			this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			this.m_methodBuilder.GetToken();
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x0018DD0C File Offset: 0x0018CF0C
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type) : this(name, attributes, callingConvention, parameterTypes, null, null, mod, type)
		{
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x0018DD2A File Offset: 0x0018CF2A
		internal override Type[] GetParameterTypes()
		{
			return this.m_methodBuilder.GetParameterTypes();
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x0018DD37 File Offset: 0x0018CF37
		[return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		private TypeBuilder GetTypeBuilder()
		{
			return this.m_methodBuilder.GetTypeBuilder();
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x0018DD44 File Offset: 0x0018CF44
		public override string ToString()
		{
			return this.m_methodBuilder.ToString();
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x0018DD51 File Offset: 0x0018CF51
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_methodBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x0018DD5E File Offset: 0x0018CF5E
		public override Module Module
		{
			get
			{
				return this.m_methodBuilder.Module;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x0018DD6B File Offset: 0x0018CF6B
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this.m_methodBuilder.ReflectedType;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x0018DD78 File Offset: 0x0018CF78
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this.m_methodBuilder.DeclaringType;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x0018DD85 File Offset: 0x0018CF85
		public override string Name
		{
			get
			{
				return this.m_methodBuilder.Name;
			}
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x0018DDA0 File Offset: 0x0018CFA0
		public override ParameterInfo[] GetParameters()
		{
			ConstructorInfo constructor = this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes);
			return constructor.GetParameters();
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x0018DDCA File Offset: 0x0018CFCA
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodBuilder.Attributes;
			}
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x0018DDD7 File Offset: 0x0018CFD7
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_methodBuilder.GetMethodImplementationFlags();
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x0018DDE4 File Offset: 0x0018CFE4
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_methodBuilder.MethodHandle;
			}
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x0018DD92 File Offset: 0x0018CF92
		[NullableContext(2)]
		[return: Nullable(1)]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicModule);
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x0018DDF1 File Offset: 0x0018CFF1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x0018DDFF File Offset: 0x0018CFFF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0018DE0E File Offset: 0x0018D00E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0018DE1D File Offset: 0x0018D01D
		public MethodToken GetToken()
		{
			return this.m_methodBuilder.GetToken();
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0018DE2A File Offset: 0x0018D02A
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, [Nullable(2)] string strParamName)
		{
			attributes &= ~(ParameterAttributes.HasDefault | ParameterAttributes.HasFieldMarshal | ParameterAttributes.Reserved3 | ParameterAttributes.Reserved4);
			return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x0018DE43 File Offset: 0x0018D043
		public ILGenerator GetILGenerator()
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(SR.InvalidOperation_DefaultConstructorILGen);
			}
			return this.m_methodBuilder.GetILGenerator();
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0018DE63 File Offset: 0x0018D063
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(SR.InvalidOperation_DefaultConstructorILGen);
			}
			return this.m_methodBuilder.GetILGenerator(streamSize);
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x0018DE84 File Offset: 0x0018D084
		public override CallingConventions CallingConvention
		{
			get
			{
				if (this.DeclaringType.IsGenericType)
				{
					return CallingConventions.HasThis;
				}
				return CallingConventions.Standard;
			}
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0018DE97 File Offset: 0x0018D097
		public Module GetModule()
		{
			return this.m_methodBuilder.GetModule();
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x0018DEA4 File Offset: 0x0018D0A4
		internal override Type GetReturnType()
		{
			return this.m_methodBuilder.ReturnType;
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0018DEB1 File Offset: 0x0018D0B1
		public string Signature
		{
			get
			{
				return this.m_methodBuilder.Signature;
			}
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x0018DEBE File Offset: 0x0018D0BE
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x0018DECD File Offset: 0x0018D0CD
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_methodBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0018DEDB File Offset: 0x0018D0DB
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.m_methodBuilder.SetImplementationFlags(attributes);
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x0018DEE9 File Offset: 0x0018D0E9
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x0018DEF6 File Offset: 0x0018D0F6
		public bool InitLocals
		{
			get
			{
				return this.m_methodBuilder.InitLocals;
			}
			set
			{
				this.m_methodBuilder.InitLocals = value;
			}
		}

		// Token: 0x0400141A RID: 5146
		private readonly MethodBuilder m_methodBuilder;

		// Token: 0x0400141B RID: 5147
		internal bool m_isDefaultConstructor;
	}
}
