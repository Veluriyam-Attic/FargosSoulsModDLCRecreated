using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000658 RID: 1624
	internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
	{
		// Token: 0x06005350 RID: 21328 RVA: 0x0019AC41 File Offset: 0x00199E41
		internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
		{
			return new ConstructorOnTypeBuilderInstantiation(Constructor, type);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0019AC4A File Offset: 0x00199E4A
		internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
		{
			this.m_ctor = constructor;
			this.m_type = type;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0019AC60 File Offset: 0x00199E60
		internal override Type[] GetParameterTypes()
		{
			return this.m_ctor.GetParameterTypes();
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0019AC6D File Offset: 0x00199E6D
		internal override Type GetReturnType()
		{
			return this.m_type;
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x0019AC75 File Offset: 0x00199E75
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_ctor.MemberType;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06005355 RID: 21333 RVA: 0x0019AC82 File Offset: 0x00199E82
		public override string Name
		{
			get
			{
				return this.m_ctor.Name;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06005356 RID: 21334 RVA: 0x0019AC6D File Offset: 0x00199E6D
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0019AC6D File Offset: 0x00199E6D
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0019AC8F File Offset: 0x00199E8F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(inherit);
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0019AC9D File Offset: 0x00199E9D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0019ACAC File Offset: 0x00199EAC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_ctor.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x0019ACBC File Offset: 0x00199EBC
		internal int MetadataTokenInternal
		{
			get
			{
				ConstructorBuilder constructorBuilder = this.m_ctor as ConstructorBuilder;
				if (constructorBuilder != null)
				{
					return constructorBuilder.MetadataTokenInternal;
				}
				return this.m_ctor.MetadataToken;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600535C RID: 21340 RVA: 0x0019ACF0 File Offset: 0x00199EF0
		public override Module Module
		{
			get
			{
				return this.m_ctor.Module;
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0019ACFD File Offset: 0x00199EFD
		public override ParameterInfo[] GetParameters()
		{
			return this.m_ctor.GetParameters();
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0019AD0A File Offset: 0x00199F0A
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_ctor.GetMethodImplementationFlags();
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x0019AD17 File Offset: 0x00199F17
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_ctor.MethodHandle;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06005360 RID: 21344 RVA: 0x0019AD24 File Offset: 0x00199F24
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_ctor.Attributes;
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x000C279F File Offset: 0x000C199F
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06005362 RID: 21346 RVA: 0x0019AD31 File Offset: 0x00199F31
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_ctor.CallingConvention;
			}
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0019AD3E File Offset: 0x00199F3E
		public override Type[] GetGenericArguments()
		{
			return this.m_ctor.GetGenericArguments();
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06005364 RID: 21348 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06005365 RID: 21349 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06005366 RID: 21350 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400153A RID: 5434
		internal ConstructorInfo m_ctor;

		// Token: 0x0400153B RID: 5435
		private TypeBuilderInstantiation m_type;
	}
}
