using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200060E RID: 1550
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x06004E51 RID: 20049 RVA: 0x000AC098 File Offset: 0x000AB298
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x000AC098 File Offset: 0x000AB298
		public virtual Type AsType()
		{
			return this;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x0018D0BF File Offset: 0x0018C2BF
		public virtual Type[] GenericTypeParameters
		{
			get
			{
				if (!this.IsGenericTypeDefinition)
				{
					return Type.EmptyTypes;
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x0018D0D5 File Offset: 0x0018C2D5
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		[return: Nullable(2)]
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0018D0E0 File Offset: 0x0018C2E0
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0018D0EB File Offset: 0x0018C2EB
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[return: Nullable(2)]
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x0018D0F6 File Offset: 0x0018C2F6
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		[return: Nullable(2)]
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x0018D10C File Offset: 0x0018C30C
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[return: Nullable(2)]
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x0018D117 File Offset: 0x0018C317
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in TypeInfo.<GetDeclaredMethods>g__GetDeclaredOnlyMethods|10_0(this))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x0018D12E File Offset: 0x0018C32E
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x0018D138 File Offset: 0x0018C338
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x0018D142 File Offset: 0x0018C342
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x0018D14C File Offset: 0x0018C34C
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x0018D156 File Offset: 0x0018C356
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x0018D160 File Offset: 0x0018C360
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
			get
			{
				foreach (Type type in TypeInfo.<get_DeclaredNestedTypes>g__GetDeclaredOnlyNestedTypes|22_0(this))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0018D17D File Offset: 0x0018C37D
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x0018D187 File Offset: 0x0018C387
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.GetInterfaces();
			}
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x0018D190 File Offset: 0x0018C390
		[NullableContext(2)]
		public virtual bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x0018D1FB File Offset: 0x0018C3FB
		internal static string GetRankString(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			if (rank != 1)
			{
				return "[" + new string(',', rank - 1) + "]";
			}
			return "[*]";
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x0018D156 File Offset: 0x0018C356
		[CompilerGenerated]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "The yield return state machine doesn't propagate annotations")]
		internal static MethodInfo[] <GetDeclaredMethods>g__GetDeclaredOnlyMethods|10_0(Type type)
		{
			return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x0018D22A File Offset: 0x0018C42A
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "The yield return state machine doesn't propagate annotations")]
		[CompilerGenerated]
		internal static Type[] <get_DeclaredNestedTypes>g__GetDeclaredOnlyNestedTypes|22_0(Type type)
		{
			return type.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}
	}
}
