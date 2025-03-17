using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000623 RID: 1571
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class EventBuilder
	{
		// Token: 0x06004FC0 RID: 20416 RVA: 0x00190CAC File Offset: 0x0018FEAC
		internal EventBuilder(ModuleBuilder mod, string name, EventAttributes attr, TypeBuilder type, EventToken evToken)
		{
			this.m_name = name;
			this.m_module = mod;
			this.m_attributes = attr;
			this.m_evToken = evToken;
			this.m_type = type;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00190CD9 File Offset: 0x0018FED9
		public EventToken GetEventToken()
		{
			return this.m_evToken;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00190CE4 File Offset: 0x0018FEE4
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			ModuleBuilder module = this.m_module;
			TypeBuilder.DefineMethodSemantics(new QCallModule(ref module), this.m_evToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00190D3D File Offset: 0x0018FF3D
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.AddOn);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x00190D47 File Offset: 0x0018FF47
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.RemoveOn);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00190D52 File Offset: 0x0018FF52
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Fire);
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x00190D5D File Offset: 0x0018FF5D
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x00190D68 File Offset: 0x0018FF68
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
			this.m_type.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.m_evToken.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00190DCF File Offset: 0x0018FFCF
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_type.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_module, this.m_evToken.Token);
		}

		// Token: 0x04001453 RID: 5203
		private string m_name;

		// Token: 0x04001454 RID: 5204
		private EventToken m_evToken;

		// Token: 0x04001455 RID: 5205
		private ModuleBuilder m_module;

		// Token: 0x04001456 RID: 5206
		private EventAttributes m_attributes;

		// Token: 0x04001457 RID: 5207
		private TypeBuilder m_type;
	}
}
