using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005E1 RID: 1505
	[NullableContext(1)]
	public interface IReflect
	{
		// Token: 0x06004C60 RID: 19552
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		MethodInfo GetMethod([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, [Nullable(1)] Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004C61 RID: 19553
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[return: Nullable(2)]
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06004C62 RID: 19554
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06004C63 RID: 19555
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06004C64 RID: 19556
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06004C65 RID: 19557
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[return: Nullable(2)]
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06004C66 RID: 19558
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[NullableContext(2)]
		PropertyInfo GetProperty([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(1)] Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004C67 RID: 19559
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06004C68 RID: 19560
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06004C69 RID: 19561
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06004C6A RID: 19562
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters);

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004C6B RID: 19563
		Type UnderlyingSystemType { get; }
	}
}
