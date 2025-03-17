using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200064E RID: 1614
	[NullableContext(2)]
	[Nullable(0)]
	public class ParameterBuilder
	{
		// Token: 0x060051A6 RID: 20902 RVA: 0x00196874 File Offset: 0x00195A74
		public virtual void SetConstant(object defaultValue)
		{
			TypeBuilder.SetConstantValue(this._methodBuilder.GetModuleBuilder(), this._token.Token, (this._position == 0) ? this._methodBuilder.ReturnType : this._methodBuilder.m_parameterTypes[this._position - 1], defaultValue);
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x001968C8 File Offset: 0x00195AC8
		[NullableContext(1)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.DefineCustomAttribute(this._methodBuilder.GetModuleBuilder(), this._token.Token, ((ModuleBuilder)this._methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00196933 File Offset: 0x00195B33
		[NullableContext(1)]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute((ModuleBuilder)this._methodBuilder.GetModule(), this._token.Token);
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x00196964 File Offset: 0x00195B64
		internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string paramName)
		{
			this._position = sequence;
			this._name = paramName;
			this._methodBuilder = methodBuilder;
			this._attributes = attributes;
			ModuleBuilder moduleBuilder = this._methodBuilder.GetModuleBuilder();
			this._token = new ParameterToken(TypeBuilder.SetParamInfo(new QCallModule(ref moduleBuilder), this._methodBuilder.GetToken().Token, sequence, attributes, paramName));
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x001969CE File Offset: 0x00195BCE
		public virtual ParameterToken GetToken()
		{
			return this._token;
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x001969D6 File Offset: 0x00195BD6
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x001969DE File Offset: 0x00195BDE
		public virtual int Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x001969E6 File Offset: 0x00195BE6
		public virtual int Attributes
		{
			get
			{
				return (int)this._attributes;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x001969EE File Offset: 0x00195BEE
		public bool IsIn
		{
			get
			{
				return (this._attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x001969FB File Offset: 0x00195BFB
		public bool IsOut
		{
			get
			{
				return (this._attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x00196A08 File Offset: 0x00195C08
		public bool IsOptional
		{
			get
			{
				return (this._attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x040014EF RID: 5359
		private readonly string _name;

		// Token: 0x040014F0 RID: 5360
		private readonly int _position;

		// Token: 0x040014F1 RID: 5361
		private readonly ParameterAttributes _attributes;

		// Token: 0x040014F2 RID: 5362
		private MethodBuilder _methodBuilder;

		// Token: 0x040014F3 RID: 5363
		private ParameterToken _token;
	}
}
