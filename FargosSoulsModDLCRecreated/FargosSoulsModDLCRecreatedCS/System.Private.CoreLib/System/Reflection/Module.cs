using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005EC RID: 1516
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Module : ICustomAttributeProvider, ISerializable
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004C97 RID: 19607 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Assembly Assembly
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string FullyQualifiedName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string Name
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual int MDStreamVersion
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0018BCEC File Offset: 0x0018AEEC
		public virtual Guid ModuleVersionId
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string ScopeName
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x0018BCFE File Offset: 0x0018AEFE
		public ModuleHandle ModuleHandle
		{
			get
			{
				return this.GetModuleHandleImpl();
			}
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0018BD06 File Offset: 0x0018AF06
		protected virtual ModuleHandle GetModuleHandleImpl()
		{
			return ModuleHandle.EmptyHandle;
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsResource()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x0018BD0D File Offset: 0x0018AF0D
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0018BD15 File Offset: 0x0018AF15
		[RequiresUnreferencedCode("Methods might be removed")]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0018BD32 File Offset: 0x0018AF32
		[RequiresUnreferencedCode("Methods might be removed")]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0018BD44 File Offset: 0x0018AF44
		[NullableContext(2)]
		[RequiresUnreferencedCode("Methods might be removed")]
		public MethodInfo GetMethod([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Methods might be removed")]
		[NullableContext(2)]
		protected virtual MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0018BDA3 File Offset: 0x0018AFA3
		[RequiresUnreferencedCode("Methods might be removed")]
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Methods might be removed")]
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0018BDAD File Offset: 0x0018AFAD
		[RequiresUnreferencedCode("Fields might be removed")]
		[return: Nullable(2)]
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Fields might be removed")]
		[return: Nullable(2)]
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0018BDB8 File Offset: 0x0018AFB8
		[RequiresUnreferencedCode("Fields might be removed")]
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Fields might be removed")]
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types might be removed")]
		public virtual Type[] GetTypes()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0018BDC2 File Offset: 0x0018AFC2
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0018BDCD File Offset: 0x0018AFCD
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0018BDD8 File Offset: 0x0018AFD8
		[RequiresUnreferencedCode("Types might be removed")]
		[NullableContext(2)]
		[return: Nullable(1)]
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			Type[] types = this.GetTypes();
			int num = 0;
			for (int i = 0; i < types.Length; i++)
			{
				if (filter != null && !filter(types[i], filterCriteria))
				{
					types[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == types.Length)
			{
				return types;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j] != null)
				{
					array[num++] = types[j];
				}
			}
			return array;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06004CB5 RID: 19637 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual int MetadataToken
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0018BE50 File Offset: 0x0018B050
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		[NullableContext(2)]
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x000C2700 File Offset: 0x000C1900
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public virtual FieldInfo ResolveField(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0018BE5B File Offset: 0x0018B05B
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		[NullableContext(2)]
		public virtual MemberInfo ResolveMember(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0018BE66 File Offset: 0x0018B066
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x000C2700 File Offset: 0x000C1900
		[NullableContext(2)]
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public virtual MethodBase ResolveMethod(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public virtual string ResolveString(int metadataToken)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0018BE71 File Offset: 0x0018B071
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Trimming changes metadata tokens")]
		public virtual Type ResolveType(int metadataToken, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericTypeArguments, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] genericMethodArguments)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x001686B3 File Offset: 0x001678B3
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x001686D0 File Offset: 0x001678D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0018222B File Offset: 0x0018142B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Module left, Module right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0018BE7C File Offset: 0x0018B07C
		[NullableContext(2)]
		public static bool operator !=(Module left, Module right)
		{
			return !(left == right);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0018BE88 File Offset: 0x0018B088
		public override string ToString()
		{
			return this.ScopeName;
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0018BE90 File Offset: 0x0018B090
		private static bool FilterTypeNameImpl(Type cls, object filterCriteria, StringComparison comparison)
		{
			string text = filterCriteria as string;
			if (text == null)
			{
				throw new InvalidFilterCriteriaException(SR.InvalidFilterCriteriaException_CritString);
			}
			if (text.Length > 0)
			{
				string text2 = text;
				int index = text2.Length - 1;
				if (text2[index] == '*')
				{
					ReadOnlySpan<char> value = text.AsSpan(0, text.Length - 1);
					return cls.Name.AsSpan().StartsWith(value, comparison);
				}
			}
			return cls.Name.Equals(text, comparison);
		}

		// Token: 0x0400139D RID: 5021
		public static readonly TypeFilter FilterTypeName = (Type m, object c) => Module.FilterTypeNameImpl(m, c, StringComparison.Ordinal);

		// Token: 0x0400139E RID: 5022
		public static readonly TypeFilter FilterTypeNameIgnoreCase = (Type m, object c) => Module.FilterTypeNameImpl(m, c, StringComparison.OrdinalIgnoreCase);
	}
}
