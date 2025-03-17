using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Type : MemberInfo, IReflect
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000C25E8 File Offset: 0x000C17E8
		public bool IsInterface
		{
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsInterface(runtimeType);
				}
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000C2614 File Offset: 0x000C1814
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, ignoreCase, ref stackCrawlMark);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000C2630 File Offset: 0x000C1830
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, false, ref stackCrawlMark);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000C264C File Offset: 0x000C184C
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000C2668 File Offset: 0x000C1868
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] Func<AssemblyName, Assembly> assemblyResolver, [Nullable(new byte[]
		{
			2,
			2,
			1,
			2
		})] Func<Assembly, string, bool, Type> typeResolver)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000C2684 File Offset: 0x000C1884
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] Func<AssemblyName, Assembly> assemblyResolver, [Nullable(new byte[]
		{
			2,
			2,
			1,
			2
		})] Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackCrawlMark);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000C26A0 File Offset: 0x000C18A0
		[RequiresUnreferencedCode("The type might be removed")]
		[return: Nullable(2)]
		public static Type GetType(string typeName, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] Func<AssemblyName, Assembly> assemblyResolver, [Nullable(new byte[]
		{
			2,
			2,
			1,
			2
		})] Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackCrawlMark);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000C26BC File Offset: 0x000C18BC
		[NullableContext(2)]
		public static Type GetTypeFromProgID([Nullable(1)] string progID, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000C26C6 File Offset: 0x000C18C6
		[NullableContext(2)]
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000C26D0 File Offset: 0x000C18D0
		internal virtual RuntimeTypeHandle GetTypeHandleInternal()
		{
			return this.TypeHandle;
		}

		// Token: 0x06000782 RID: 1922
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetTypeFromHandleUnsafe(IntPtr handle);

		// Token: 0x06000783 RID: 1923
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Type GetTypeFromHandle(RuntimeTypeHandle handle);

		// Token: 0x06000784 RID: 1924
		[NullableContext(2)]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool operator ==(Type left, Type right);

		// Token: 0x06000785 RID: 1925
		[NullableContext(2)]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool operator !=(Type left, Type right);

		// Token: 0x06000786 RID: 1926 RVA: 0x000C26D8 File Offset: 0x000C18D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsRuntimeImplemented()
		{
			return this is RuntimeType;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000C26EB File Offset: 0x000C18EB
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000AF0A4 File Offset: 0x000AE2A4
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600078A RID: 1930
		[Nullable(2)]
		public abstract string Namespace { [NullableContext(2)] get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600078B RID: 1931
		[Nullable(2)]
		public abstract string AssemblyQualifiedName { [NullableContext(2)] get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600078C RID: 1932
		[Nullable(2)]
		public abstract string FullName { [NullableContext(2)] get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600078D RID: 1933
		public abstract Assembly Assembly { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600078E RID: 1934
		public new abstract Module Module { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000C26EF File Offset: 0x000C18EF
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public virtual MethodBase DeclaringMethod
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000793 RID: 1939
		public abstract Type UnderlyingSystemType { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsTypeDefinition
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000C2707 File Offset: 0x000C1907
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		// Token: 0x06000796 RID: 1942
		protected abstract bool IsArrayImpl();

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000C270F File Offset: 0x000C190F
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		// Token: 0x06000798 RID: 1944
		protected abstract bool IsByRefImpl();

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000C2717 File Offset: 0x000C1917
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		// Token: 0x0600079A RID: 1946
		protected abstract bool IsPointerImpl();

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsConstructedGenericType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000C271F File Offset: 0x000C191F
		public virtual bool IsGenericTypeParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod == null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000C2734 File Offset: 0x000C1934
		public virtual bool IsGenericMethodParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod != null;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSZArray
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x000C274C File Offset: 0x000C194C
		public virtual bool IsVariableBoundArray
		{
			get
			{
				return this.IsArray && !this.IsSZArray;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual bool IsByRefLike
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_SubclassOverride);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000C276D File Offset: 0x000C196D
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		// Token: 0x060007A5 RID: 1957
		protected abstract bool HasElementTypeImpl();

		// Token: 0x060007A6 RID: 1958
		[NullableContext(2)]
		public abstract Type GetElementType();

		// Token: 0x060007A7 RID: 1959 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x000C2775 File Offset: 0x000C1975
		public virtual Type[] GenericTypeArguments
		{
			get
			{
				if (!this.IsGenericType || this.IsGenericTypeDefinition)
				{
					return Array.Empty<Type>();
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x000C2793 File Offset: 0x000C1993
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000C279F File Offset: 0x000C199F
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000C27A6 File Offset: 0x000C19A6
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000C27C0 File Offset: 0x000C19C0
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		// Token: 0x060007AF RID: 1967
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000C27C8 File Offset: 0x000C19C8
		public bool IsAbstract
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000C27D9 File Offset: 0x000C19D9
		public bool IsImport
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000C27EA File Offset: 0x000C19EA
		public bool IsSealed
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000C27FB File Offset: 0x000C19FB
		public bool IsSpecialName
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000C280C File Offset: 0x000C1A0C
		public bool IsClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000C2824 File Offset: 0x000C1A24
		public bool IsNestedAssembly
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000C2831 File Offset: 0x000C1A31
		public bool IsNestedFamANDAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x000C283E File Offset: 0x000C1A3E
		public bool IsNestedFamily
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000C284B File Offset: 0x000C1A4B
		public bool IsNestedFamORAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000C2858 File Offset: 0x000C1A58
		public bool IsNestedPrivate
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000C2865 File Offset: 0x000C1A65
		public bool IsNestedPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000C2872 File Offset: 0x000C1A72
		public bool IsNotPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000C287F File Offset: 0x000C1A7F
		public bool IsPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000C288C File Offset: 0x000C1A8C
		public bool IsAutoLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x000C289A File Offset: 0x000C1A9A
		public bool IsExplicitLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x000C28A9 File Offset: 0x000C1AA9
		public bool IsLayoutSequential
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000C28B7 File Offset: 0x000C1AB7
		public bool IsAnsiClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000C28C8 File Offset: 0x000C1AC8
		public bool IsAutoClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000C28DD File Offset: 0x000C1ADD
		public bool IsUnicodeClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000C28F2 File Offset: 0x000C1AF2
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		// Token: 0x060007C4 RID: 1988
		protected abstract bool IsCOMObjectImpl();

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000C28FA File Offset: 0x000C1AFA
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected virtual bool IsContextfulImpl()
		{
			return false;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000C2902 File Offset: 0x000C1B02
		public virtual bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000C2914 File Offset: 0x000C1B14
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected virtual bool IsMarshalByRefImpl()
		{
			return false;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000C291C File Offset: 0x000C1B1C
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		// Token: 0x060007CB RID: 1995
		protected abstract bool IsPrimitiveImpl();

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000C2924 File Offset: 0x000C1B24
		public bool IsValueType
		{
			[Intrinsic]
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000C292C File Offset: 0x000C1B2C
		[NullableContext(2)]
		[Intrinsic]
		public bool IsAssignableTo([NotNullWhen(true)] Type targetType)
		{
			return targetType != null && targetType.IsAssignableFrom(this);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000C293A File Offset: 0x000C1B3A
		protected virtual bool IsValueTypeImpl()
		{
			return this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSignatureType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000C279F File Offset: 0x000C199F
		[Nullable(2)]
		public virtual StructLayoutAttribute StructLayoutAttribute
		{
			[NullableContext(2)]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000C294C File Offset: 0x000C1B4C
		[Nullable(2)]
		public ConstructorInfo TypeInitializer
		{
			[NullableContext(2)]
			[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
			get
			{
				return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000C295E File Offset: 0x000C1B5E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
		[return: Nullable(2)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000C296B File Offset: 0x000C1B6B
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		[NullableContext(2)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000C297C File Offset: 0x000C1B7C
		[NullableContext(2)]
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
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
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060007D8 RID: 2008
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		[NullableContext(2)]
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060007D9 RID: 2009 RVA: 0x000C29CB File Offset: 0x000C1BCB
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetConstructors(BindingFlags.Public) but this is what the body is doing")]
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x060007DA RID: 2010
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x060007DB RID: 2011 RVA: 0x000C29D5 File Offset: 0x000C1BD5
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		[return: Nullable(2)]
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007DC RID: 2012
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		[return: Nullable(2)]
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x060007DD RID: 2013 RVA: 0x000C29E0 File Offset: 0x000C1BE0
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetEvents(BindingFlags.Public) but this is what the body is doing")]
		public virtual EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007DE RID: 2014
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x060007DF RID: 2015 RVA: 0x000C29EA File Offset: 0x000C1BEA
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
		[return: Nullable(2)]
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007E0 RID: 2016
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		[return: Nullable(2)]
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060007E1 RID: 2017 RVA: 0x000C29F5 File Offset: 0x000C1BF5
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetFields(BindingFlags.Public) but this is what the body is doing")]
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007E2 RID: 2018
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060007E3 RID: 2019 RVA: 0x000C29FF File Offset: 0x000C1BFF
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetMember(BindingFlags.Public) but this is what the body is doing")]
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000C2A0A File Offset: 0x000C1C0A
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000C2761 File Offset: 0x000C1961
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000C2A19 File Offset: 0x000C1C19
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007E7 RID: 2023
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060007E8 RID: 2024 RVA: 0x000C2A23 File Offset: 0x000C1C23
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000C2A2E File Offset: 0x000C1C2E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000C2A4A File Offset: 0x000C1C4A
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, types, null);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000C2A55 File Offset: 0x000C1C55
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, Type[] types, [Nullable(2)] ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000C2A63 File Offset: 0x000C1C63
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public MethodInfo GetMethod([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000C2A74 File Offset: 0x000C1C74
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		[NullableContext(2)]
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

		// Token: 0x060007EE RID: 2030
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected abstract MethodInfo GetMethodImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060007EF RID: 2031 RVA: 0x000C2AD3 File Offset: 0x000C1CD3
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types)
		{
			return this.GetMethod(name, genericParameterCount, types, null);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000C2ADF File Offset: 0x000C1CDF
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[return: Nullable(2)]
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types, [Nullable(2)] ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000C2AEF File Offset: 0x000C1CEF
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public MethodInfo GetMethod([Nullable(1)] string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000C2B04 File Offset: 0x000C1D04
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public MethodInfo GetMethod([Nullable(1)] string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (genericParameterCount < 0)
			{
				throw new ArgumentException(SR.ArgumentOutOfRange_NeedNonNegNum, "genericParameterCount");
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
			return this.GetMethodImpl(name, genericParameterCount, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000C279F File Offset: 0x000C199F
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected virtual MethodInfo GetMethodImpl([Nullable(1)] string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000C2B79 File Offset: 0x000C1D79
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetMethods(BindingFlags.Public) but this is what the body is doing")]
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007F5 RID: 2037
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060007F6 RID: 2038 RVA: 0x000C2B83 File Offset: 0x000C1D83
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
		[return: Nullable(2)]
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007F7 RID: 2039
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		[return: Nullable(2)]
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		// Token: 0x060007F8 RID: 2040 RVA: 0x000C2B8E File Offset: 0x000C1D8E
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetNestedTypes(BindingFlags.Public) but this is what the body is doing")]
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007F9 RID: 2041
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

		// Token: 0x060007FA RID: 2042 RVA: 0x000C2B98 File Offset: 0x000C1D98
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		[return: Nullable(2)]
		public PropertyInfo GetProperty(string name)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000C2BA3 File Offset: 0x000C1DA3
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		[return: Nullable(2)]
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000C2BBF File Offset: 0x000C1DBF
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetPropertyImpl(BindingFlags.Public) but this is what the body is doing")]
		public PropertyInfo GetProperty([Nullable(1)] string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000C2BDC File Offset: 0x000C1DDC
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		[return: Nullable(2)]
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			return this.GetProperty(name, null, types);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000C2BE7 File Offset: 0x000C1DE7
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		[return: Nullable(2)]
		public PropertyInfo GetProperty(string name, [Nullable(2)] Type returnType, Type[] types)
		{
			return this.GetProperty(name, returnType, types, null);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000C2BF3 File Offset: 0x000C1DF3
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		public PropertyInfo GetProperty([Nullable(1)] string name, Type returnType, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000C2C03 File Offset: 0x000C1E03
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public PropertyInfo GetProperty([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(1)] Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06000801 RID: 2049
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected abstract PropertyInfo GetPropertyImpl([Nullable(1)] string name, BindingFlags bindingAttr, Binder binder, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06000802 RID: 2050 RVA: 0x000C2C31 File Offset: 0x000C1E31
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Linker doesn't recognize GetProperties(BindingFlags.Public) but this is what the body is doing")]
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000803 RID: 2051
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06000804 RID: 2052 RVA: 0x000C2700 File Offset: 0x000C1900
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
		public virtual MemberInfo[] GetDefaultMembers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x000C2C3C File Offset: 0x000C1E3C
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000C2C50 File Offset: 0x000C1E50
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(null, SR.Arg_InvalidHandle);
			}
			Type type = o.GetType();
			return type.TypeHandle;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000C2C7C File Offset: 0x000C1E7C
		public static Type[] GetTypeArray(object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			Type[] array = new Type[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (args[i] == null)
				{
					throw new ArgumentException(SR.ArgumentNull_ArrayValue, "args");
				}
				array[i] = args[i].GetType();
			}
			return array;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000C2CCF File Offset: 0x000C1ECF
		[NullableContext(2)]
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			return type.GetTypeCodeImpl();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000C2CE4 File Offset: 0x000C1EE4
		protected virtual TypeCode GetTypeCodeImpl()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (this != underlyingSystemType && underlyingSystemType != null)
			{
				return Type.GetTypeCode(underlyingSystemType);
			}
			return TypeCode.Object;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600080A RID: 2058
		public abstract Guid GUID { get; }

		// Token: 0x0600080B RID: 2059 RVA: 0x000C2D12 File Offset: 0x000C1F12
		[NullableContext(2)]
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return Type.GetTypeFromCLSID(clsid, null, false);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000C2D1C File Offset: 0x000C1F1C
		[NullableContext(2)]
		public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
		{
			return Type.GetTypeFromCLSID(clsid, null, throwOnError);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000C2D26 File Offset: 0x000C1F26
		[NullableContext(2)]
		public static Type GetTypeFromCLSID(Guid clsid, string server)
		{
			return Type.GetTypeFromCLSID(clsid, server, false);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000C2D30 File Offset: 0x000C1F30
		[return: Nullable(2)]
		public static Type GetTypeFromProgID(string progID)
		{
			return Type.GetTypeFromProgID(progID, null, false);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000C2D3A File Offset: 0x000C1F3A
		[return: Nullable(2)]
		public static Type GetTypeFromProgID(string progID, bool throwOnError)
		{
			return Type.GetTypeFromProgID(progID, null, throwOnError);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000C2D44 File Offset: 0x000C1F44
		[NullableContext(2)]
		public static Type GetTypeFromProgID([Nullable(1)] string progID, string server)
		{
			return Type.GetTypeFromProgID(progID, server, false);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000811 RID: 2065
		[Nullable(2)]
		public abstract Type BaseType { [NullableContext(2)] get; }

		// Token: 0x06000812 RID: 2066 RVA: 0x000C2D50 File Offset: 0x000C1F50
		[NullableContext(2)]
		[DebuggerHidden]
		[DebuggerStepThrough]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, null, null);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000C2D70 File Offset: 0x000C1F70
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		[DebuggerHidden]
		[DebuggerStepThrough]
		[NullableContext(2)]
		public object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, culture, null);
		}

		// Token: 0x06000814 RID: 2068
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public abstract object InvokeMember([Nullable(1)] string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] string[] namedParameters);

		// Token: 0x06000815 RID: 2069 RVA: 0x000C2D8E File Offset: 0x000C1F8E
		[return: Nullable(2)]
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		// Token: 0x06000816 RID: 2070
		[return: Nullable(2)]
		public abstract Type GetInterface(string name, bool ignoreCase);

		// Token: 0x06000817 RID: 2071
		public abstract Type[] GetInterfaces();

		// Token: 0x06000818 RID: 2072 RVA: 0x000C2D98 File Offset: 0x000C1F98
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000C2DAF File Offset: 0x000C1FAF
		[NullableContext(2)]
		public virtual bool IsInstanceOfType([NotNullWhen(true)] object o)
		{
			return o != null && this.IsAssignableFrom(o.GetType());
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000C2DC2 File Offset: 0x000C1FC2
		[NullableContext(2)]
		public virtual bool IsEquivalentTo([NotNullWhen(true)] Type other)
		{
			return this == other;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000C2DCC File Offset: 0x000C1FCC
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "The single instance field on enum types is never trimmed")]
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fields == null || fields.Length != 1)
			{
				throw new ArgumentException(SR.Argument_InvalidEnum, "enumType");
			}
			return fields[0].FieldType;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000C2E1B File Offset: 0x000C201B
		public virtual Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000C279F File Offset: 0x000C199F
		public virtual Type MakeArrayType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000C279F File Offset: 0x000C199F
		public virtual Type MakeArrayType(int rank)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000C279F File Offset: 0x000C199F
		public virtual Type MakeByRefType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000C279F File Offset: 0x000C199F
		public virtual Type MakePointerType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000C2E3A File Offset: 0x000C203A
		public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
		{
			return new SignatureConstructedGenericType(genericTypeDefinition, typeArguments);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000C2E43 File Offset: 0x000C2043
		public static Type MakeGenericMethodParameter(int position)
		{
			if (position < 0)
			{
				throw new ArgumentException(SR.ArgumentOutOfRange_NeedNonNegNum, "position");
			}
			return new SignatureGenericMethodParameterType(position);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000C2E60 File Offset: 0x000C2060
		internal string FormatTypeName()
		{
			Type rootElementType = this.GetRootElementType();
			if (rootElementType.IsPrimitive || rootElementType.IsNested || rootElementType == typeof(void) || rootElementType == typeof(TypedReference))
			{
				return this.Name;
			}
			return this.ToString();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000C2EB5 File Offset: 0x000C20B5
		public override string ToString()
		{
			return "Type: " + this.Name;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000C2EC7 File Offset: 0x000C20C7
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			return o != null && this.Equals(o as Type);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000C2EDC File Offset: 0x000C20DC
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000C2F01 File Offset: 0x000C2101
		[NullableContext(2)]
		public virtual bool Equals(Type o)
		{
			return !(o == null) && this.UnderlyingSystemType == o.UnderlyingSystemType;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000C2F1C File Offset: 0x000C211C
		[return: Nullable(2)]
		public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ReflectionOnly);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x000C2F28 File Offset: 0x000C2128
		public static Binder DefaultBinder
		{
			get
			{
				if (Type.s_defaultBinder == null)
				{
					DefaultBinder value = new DefaultBinder();
					Interlocked.CompareExchange<Binder>(ref Type.s_defaultBinder, value, null);
				}
				return Type.s_defaultBinder;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000C2F58 File Offset: 0x000C2158
		public virtual bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "value");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(this))
				{
					throw new ArgumentException(SR.Format(SR.Arg_EnumAndObjectMustBeSameType, type, this));
				}
				type = type.GetEnumUnderlyingType();
			}
			if (type == typeof(string))
			{
				string[] enumNames = this.GetEnumNames();
				object[] array = enumNames;
				return Array.IndexOf<object>(array, value) >= 0;
			}
			if (!Type.IsIntegerType(type))
			{
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
			Type enumUnderlyingType = this.GetEnumUnderlyingType();
			if (enumUnderlyingType.GetTypeCodeImpl() != type.GetTypeCodeImpl())
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumUnderlyingTypeAndObjectMustBeSameType, type, enumUnderlyingType));
			}
			Array enumRawConstantValues = this.GetEnumRawConstantValues();
			return Type.BinarySearch(enumRawConstantValues, value) >= 0;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000C3038 File Offset: 0x000C2238
		[return: Nullable(2)]
		public virtual string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "value");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(SR.Arg_MustBeEnumBaseTypeOrEnum, "value");
			}
			Array enumRawConstantValues = this.GetEnumRawConstantValues();
			int num = Type.BinarySearch(enumRawConstantValues, value);
			if (num >= 0)
			{
				string[] enumNames = this.GetEnumNames();
				return enumNames[num];
			}
			return null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000C30B4 File Offset: 0x000C22B4
		public virtual string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			string[] result;
			Array array;
			this.GetEnumData(out result, out array);
			return result;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000C30E4 File Offset: 0x000C22E4
		private Array GetEnumRawConstantValues()
		{
			string[] array;
			Array result;
			this.GetEnumData(out array, out result);
			return result;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000C30FC File Offset: 0x000C22FC
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Literal fields on enums can never be trimmed")]
		private void GetEnumData(out string[] enumNames, out Array enumValues)
		{
			FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			object[] array = new object[fields.Length];
			string[] array2 = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array2[i] = fields[i].Name;
				array[i] = fields[i].GetRawConstantValue();
			}
			IComparer @default = Comparer<object>.Default;
			for (int j = 1; j < array.Length; j++)
			{
				int num = j;
				string text = array2[j];
				object obj = array[j];
				bool flag = false;
				while (@default.Compare(array[num - 1], obj) > 0)
				{
					array2[num] = array2[num - 1];
					array[num] = array[num - 1];
					num--;
					flag = true;
					if (num == 0)
					{
						break;
					}
				}
				if (flag)
				{
					array2[num] = text;
					array[num] = obj;
				}
			}
			enumNames = array2;
			enumValues = array;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000C31C8 File Offset: 0x000C23C8
		private static int BinarySearch(Array array, object value)
		{
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Enum.ToUInt64(array.GetValue(i));
			}
			ulong value2 = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array2, value2);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000C3210 File Offset: 0x000C2410
		internal static bool IsIntegerType(Type t)
		{
			return t == typeof(int) || t == typeof(short) || t == typeof(ushort) || t == typeof(byte) || t == typeof(sbyte) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong) || t == typeof(char) || t == typeof(bool);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000C32D8 File Offset: 0x000C24D8
		public virtual bool IsSerializable
		{
			get
			{
				if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type.IsRuntimeImplemented())
				{
					while (!(type == typeof(Delegate)) && !(type == typeof(Enum)))
					{
						type = type.BaseType;
						if (!(type != null))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000C333C File Offset: 0x000C253C
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.HasElementType)
				{
					return this.GetRootElementType().ContainsGenericParameters;
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (!this.IsGenericType)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000C3394 File Offset: 0x000C2594
		internal Type GetRootElementType()
		{
			Type type = this;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			return type;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000C33B8 File Offset: 0x000C25B8
		public bool IsVisible
		{
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsVisible(runtimeType);
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.HasElementType)
				{
					return this.GetElementType().IsVisible;
				}
				Type type = this;
				while (type.IsNested)
				{
					if (!type.IsNestedPublic)
					{
						return false;
					}
					type = type.DeclaringType;
				}
				if (!type.IsPublic)
				{
					return false;
				}
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					foreach (Type type2 in this.GetGenericArguments())
					{
						if (!type2.IsVisible)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000C3454 File Offset: 0x000C2654
		public virtual Type[] FindInterfaces(TypeFilter filter, [Nullable(2)] object filterCriteria)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			Type[] interfaces = this.GetInterfaces();
			int num = 0;
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (!filter(interfaces[i], filterCriteria))
				{
					interfaces[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == interfaces.Length)
			{
				return interfaces;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] != null)
				{
					array[num++] = interfaces[j];
				}
			}
			return array;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000C34D8 File Offset: 0x000C26D8
		[NullableContext(2)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		[return: Nullable(1)]
		public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			MethodInfo[] array = null;
			ConstructorInfo[] array2 = null;
			FieldInfo[] array3 = null;
			PropertyInfo[] array4 = null;
			EventInfo[] array5 = null;
			Type[] array6 = null;
			int num = 0;
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				array = this.GetMethods(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!filter(array[i], filterCriteria))
						{
							array[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array.Length;
				}
			}
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				array2 = this.GetConstructors(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (!filter(array2[i], filterCriteria))
						{
							array2[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array2.Length;
				}
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				array3 = this.GetFields(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array3.Length; i++)
					{
						if (!filter(array3[i], filterCriteria))
						{
							array3[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array3.Length;
				}
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				array4 = this.GetProperties(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array4.Length; i++)
					{
						if (!filter(array4[i], filterCriteria))
						{
							array4[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array4.Length;
				}
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				array5 = this.GetEvents(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array5.Length; i++)
					{
						if (!filter(array5[i], filterCriteria))
						{
							array5[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array5.Length;
				}
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				array6 = this.GetNestedTypes(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array6.Length; i++)
					{
						if (!filter(array6[i], filterCriteria))
						{
							array6[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array6.Length;
				}
			}
			MemberInfo[] array7 = new MemberInfo[num];
			num = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array7[num++] = array[i];
					}
				}
			}
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != null)
					{
						array7[num++] = array2[i];
					}
				}
			}
			if (array3 != null)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != null)
					{
						array7[num++] = array3[i];
					}
				}
			}
			if (array4 != null)
			{
				for (int i = 0; i < array4.Length; i++)
				{
					if (array4[i] != null)
					{
						array7[num++] = array4[i];
					}
				}
			}
			if (array5 != null)
			{
				for (int i = 0; i < array5.Length; i++)
				{
					if (array5[i] != null)
					{
						array7[num++] = array5[i];
					}
				}
			}
			if (array6 != null)
			{
				for (int i = 0; i < array6.Length; i++)
				{
					if (array6[i] != null)
					{
						array7[num++] = array6[i];
					}
				}
			}
			return array7;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000C37E0 File Offset: 0x000C29E0
		public virtual bool IsSubclassOf(Type c)
		{
			Type type = this;
			if (type == c)
			{
				return false;
			}
			while (type != null)
			{
				if (type == c)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000C3818 File Offset: 0x000C2A18
		[NullableContext(2)]
		[Intrinsic]
		public virtual bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this == c)
			{
				return true;
			}
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType.IsRuntimeImplemented())
			{
				return underlyingSystemType.IsAssignableFrom(c);
			}
			if (c.IsSubclassOf(this))
			{
				return true;
			}
			if (this.IsInterface)
			{
				return c.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(c))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000C38A0 File Offset: 0x000C2AA0
		internal bool ImplementInterface(Type ifaceType)
		{
			Type type = this;
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null)
				{
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (interfaces[i] == ifaceType || (interfaces[i] != null && interfaces[i].ImplementInterface(ifaceType)))
						{
							return true;
						}
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000C3900 File Offset: 0x000C2B00
		private static bool FilterAttributeImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null)
			{
				throw new InvalidFilterCriteriaException(SR.InvalidFilterCriteriaException_CritInt);
			}
			MemberTypes memberType = m.MemberType;
			if (memberType != MemberTypes.Constructor)
			{
				if (memberType == MemberTypes.Field)
				{
					FieldAttributes fieldAttributes;
					try
					{
						int num = (int)filterCriteria;
						fieldAttributes = (FieldAttributes)num;
					}
					catch
					{
						throw new InvalidFilterCriteriaException(SR.InvalidFilterCriteriaException_CritInt);
					}
					FieldAttributes attributes = ((FieldInfo)m).Attributes;
					return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope);
				}
				if (memberType != MemberTypes.Method)
				{
					return false;
				}
			}
			MethodAttributes methodAttributes;
			try
			{
				int num2 = (int)filterCriteria;
				methodAttributes = (MethodAttributes)num2;
			}
			catch
			{
				throw new InvalidFilterCriteriaException(SR.InvalidFilterCriteriaException_CritInt);
			}
			MethodAttributes attributes2;
			if (m.MemberType == MemberTypes.Method)
			{
				attributes2 = ((MethodInfo)m).Attributes;
			}
			else
			{
				attributes2 = ((ConstructorInfo)m).Attributes;
			}
			return ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes & MethodAttributes.MemberAccessMask)) && ((methodAttributes & MethodAttributes.Static) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Final) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000C3A88 File Offset: 0x000C2C88
		private unsafe static bool FilterNameImpl(MemberInfo m, object filterCriteria, StringComparison comparison)
		{
			string text = filterCriteria as string;
			if (text == null)
			{
				throw new InvalidFilterCriteriaException(SR.InvalidFilterCriteriaException_CritString);
			}
			ReadOnlySpan<char> readOnlySpan = text.AsSpan().Trim();
			ReadOnlySpan<char> span = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				span = span.Slice(span.LastIndexOf('+') + 1);
			}
			if (readOnlySpan.Length > 0 && *readOnlySpan[readOnlySpan.Length - 1] == 42)
			{
				readOnlySpan = readOnlySpan.Slice(0, readOnlySpan.Length - 1);
				return span.StartsWith(readOnlySpan, comparison);
			}
			return span.Equals(readOnlySpan, comparison);
		}

		// Token: 0x04000214 RID: 532
		private static volatile Binder s_defaultBinder;

		// Token: 0x04000215 RID: 533
		public static readonly char Delimiter = '.';

		// Token: 0x04000216 RID: 534
		public static readonly Type[] EmptyTypes = Array.Empty<Type>();

		// Token: 0x04000217 RID: 535
		public static readonly object Missing = System.Reflection.Missing.Value;

		// Token: 0x04000218 RID: 536
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttributeImpl);

		// Token: 0x04000219 RID: 537
		public static readonly MemberFilter FilterName = (MemberInfo m, object c) => Type.FilterNameImpl(m, c, StringComparison.Ordinal);

		// Token: 0x0400021A RID: 538
		public static readonly MemberFilter FilterNameIgnoreCase = (MemberInfo m, object c) => Type.FilterNameImpl(m, c, StringComparison.OrdinalIgnoreCase);
	}
}
