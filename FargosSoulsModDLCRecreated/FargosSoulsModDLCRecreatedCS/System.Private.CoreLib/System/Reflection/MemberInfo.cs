using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005A5 RID: 1445
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MemberInfo : ICustomAttributeProvider
	{
		// Token: 0x06004A10 RID: 18960 RVA: 0x00177FCF File Offset: 0x001771CF
		internal virtual bool CacheEquals(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x001869A0 File Offset: 0x00185BA0
		internal bool HasSameMetadataDefinitionAsCore<TOther>(MemberInfo other) where TOther : MemberInfo
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			return other is TOther && this.MetadataToken == other.MetadataToken && this.Module.Equals(other.Module);
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06004A13 RID: 18963
		public abstract MemberTypes MemberType { get; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004A14 RID: 18964
		public abstract string Name { get; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004A15 RID: 18965
		[Nullable(2)]
		public abstract Type DeclaringType { [NullableContext(2)] get; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004A16 RID: 18966
		[Nullable(2)]
		public abstract Type ReflectedType { [NullableContext(2)] get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x001869E0 File Offset: 0x00185BE0
		public virtual Module Module
		{
			get
			{
				Type type = this as Type;
				if (type != null)
				{
					return type.Module;
				}
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004A19 RID: 18969
		public abstract bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004A1A RID: 18970
		public abstract object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004A1B RID: 18971
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x00186A03 File Offset: 0x00185C03
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x000AC09E File Offset: 0x000AB29E
		public virtual bool IsCollectible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public virtual int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x001686B3 File Offset: 0x001678B3
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x001686D0 File Offset: 0x001678D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0018222B File Offset: 0x0018142B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(MemberInfo left, MemberInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00186A0B File Offset: 0x00185C0B
		[NullableContext(2)]
		public static bool operator !=(MemberInfo left, MemberInfo right)
		{
			return !(left == right);
		}
	}
}
