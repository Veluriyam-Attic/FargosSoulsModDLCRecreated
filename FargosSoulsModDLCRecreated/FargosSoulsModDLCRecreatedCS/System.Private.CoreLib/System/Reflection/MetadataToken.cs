using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005A0 RID: 1440
	internal struct MetadataToken
	{
		// Token: 0x060049BD RID: 18877 RVA: 0x0018643D File Offset: 0x0018563D
		public static implicit operator int(MetadataToken token)
		{
			return token.Value;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00186445 File Offset: 0x00185645
		public static implicit operator MetadataToken(int token)
		{
			return new MetadataToken(token);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0018644D File Offset: 0x0018564D
		public static bool IsNullToken(int token)
		{
			return (token & 16777215) == 0;
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00186459 File Offset: 0x00185659
		public MetadataToken(int token)
		{
			this.Value = token;
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x00186462 File Offset: 0x00185662
		public bool IsGlobalTypeDefToken
		{
			get
			{
				return this.Value == 33554433;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x00186471 File Offset: 0x00185671
		public MetadataTokenType TokenType
		{
			get
			{
				return (MetadataTokenType)((long)this.Value & (long)((ulong)-16777216));
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x00186482 File Offset: 0x00185682
		public bool IsTypeRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeRef;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x00186491 File Offset: 0x00185691
		public bool IsTypeDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeDef;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x001864A0 File Offset: 0x001856A0
		public bool IsFieldDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.FieldDef;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x001864AF File Offset: 0x001856AF
		public bool IsMethodDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodDef;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x001864BE File Offset: 0x001856BE
		public bool IsMemberRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MemberRef;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x001864CD File Offset: 0x001856CD
		public bool IsEvent
		{
			get
			{
				return this.TokenType == MetadataTokenType.Event;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x001864DC File Offset: 0x001856DC
		public bool IsProperty
		{
			get
			{
				return this.TokenType == MetadataTokenType.Property;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x001864EB File Offset: 0x001856EB
		public bool IsParamDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.ParamDef;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x001864FA File Offset: 0x001856FA
		public bool IsTypeSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeSpec;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x00186509 File Offset: 0x00185709
		public bool IsMethodSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodSpec;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x00186518 File Offset: 0x00185718
		public bool IsString
		{
			get
			{
				return this.TokenType == MetadataTokenType.String;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x00186527 File Offset: 0x00185727
		public bool IsSignature
		{
			get
			{
				return this.TokenType == MetadataTokenType.Signature;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x00186536 File Offset: 0x00185736
		public bool IsGenericPar
		{
			get
			{
				return this.TokenType == MetadataTokenType.GenericPar;
			}
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x00186545 File Offset: 0x00185745
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x8}", this.Value);
		}

		// Token: 0x04001267 RID: 4711
		public int Value;
	}
}
