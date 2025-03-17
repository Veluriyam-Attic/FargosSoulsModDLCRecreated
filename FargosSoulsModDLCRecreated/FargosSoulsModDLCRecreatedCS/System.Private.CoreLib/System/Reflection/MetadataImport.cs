using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A3 RID: 1443
	internal readonly struct MetadataImport
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x001865A0 File Offset: 0x001857A0
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.m_metadataImport2);
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x001865AD File Offset: 0x001857AD
		public override bool Equals(object obj)
		{
			return obj is MetadataImport && this.Equals((MetadataImport)obj);
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x001865C5 File Offset: 0x001857C5
		private bool Equals(MetadataImport import)
		{
			return import.m_metadataImport2 == this.m_metadataImport2;
		}

		// Token: 0x060049D6 RID: 18902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMarshalAs(IntPtr pNativeType, int cNativeType, out int unmanagedType, out int safeArraySubType, out string safeArrayUserDefinedSubType, out int arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex);

		// Token: 0x060049D7 RID: 18903 RVA: 0x001865D8 File Offset: 0x001857D8
		internal static void GetMarshalAs(ConstArray nativeType, out UnmanagedType unmanagedType, out VarEnum safeArraySubType, out string safeArrayUserDefinedSubType, out UnmanagedType arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex)
		{
			int num;
			int num2;
			int num3;
			MetadataImport._GetMarshalAs(nativeType.Signature, nativeType.Length, out num, out num2, out safeArrayUserDefinedSubType, out num3, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
			unmanagedType = (UnmanagedType)num;
			safeArraySubType = (VarEnum)num2;
			arraySubType = (UnmanagedType)num3;
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x00186613 File Offset: 0x00185813
		internal static void ThrowError(int hResult)
		{
			throw new MetadataException(hResult);
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0018661B File Offset: 0x0018581B
		internal MetadataImport(IntPtr metadataImport2, object keepalive)
		{
			this.m_metadataImport2 = metadataImport2;
			this.m_keepalive = keepalive;
		}

		// Token: 0x060049DA RID: 18906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Enum(IntPtr scope, int type, int parent, out MetadataEnumResult result);

		// Token: 0x060049DB RID: 18907 RVA: 0x0018662B File Offset: 0x0018582B
		public void Enum(MetadataTokenType type, int parent, out MetadataEnumResult result)
		{
			MetadataImport._Enum(this.m_metadataImport2, (int)type, parent, out result);
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0018663B File Offset: 0x0018583B
		public void EnumNestedTypes(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.TypeDef, mdTypeDef, out result);
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x0018664A File Offset: 0x0018584A
		public void EnumCustomAttributes(int mdToken, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.CustomAttribute, mdToken, out result);
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x00186659 File Offset: 0x00185859
		public void EnumParams(int mdMethodDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.ParamDef, mdMethodDef, out result);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x00186668 File Offset: 0x00185868
		public void EnumFields(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.FieldDef, mdTypeDef, out result);
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x00186677 File Offset: 0x00185877
		public void EnumProperties(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Property, mdTypeDef, out result);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x00186686 File Offset: 0x00185886
		public void EnumEvents(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Event, mdTypeDef, out result);
		}

		// Token: 0x060049E2 RID: 18914
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string _GetDefaultValue(IntPtr scope, int mdToken, out long value, out int length, out int corElementType);

		// Token: 0x060049E3 RID: 18915 RVA: 0x00186698 File Offset: 0x00185898
		public string GetDefaultValue(int mdToken, out long value, out int length, out CorElementType corElementType)
		{
			int num;
			string result = MetadataImport._GetDefaultValue(this.m_metadataImport2, mdToken, out value, out length, out num);
			corElementType = (CorElementType)num;
			return result;
		}

		// Token: 0x060049E4 RID: 18916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetUserString(IntPtr scope, int mdToken, void** name, out int length);

		// Token: 0x060049E5 RID: 18917 RVA: 0x001866BC File Offset: 0x001858BC
		public unsafe string GetUserString(int mdToken)
		{
			void* ptr;
			int length;
			MetadataImport._GetUserString(this.m_metadataImport2, mdToken, &ptr, out length);
			if (ptr == null)
			{
				return null;
			}
			return new string((char*)ptr, 0, length);
		}

		// Token: 0x060049E6 RID: 18918
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetName(IntPtr scope, int mdToken, void** name);

		// Token: 0x060049E7 RID: 18919 RVA: 0x001866EC File Offset: 0x001858EC
		public unsafe MdUtf8String GetName(int mdToken)
		{
			void* pStringHeap;
			MetadataImport._GetName(this.m_metadataImport2, mdToken, &pStringHeap);
			return new MdUtf8String(pStringHeap);
		}

		// Token: 0x060049E8 RID: 18920
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetNamespace(IntPtr scope, int mdToken, void** namesp);

		// Token: 0x060049E9 RID: 18921 RVA: 0x00186710 File Offset: 0x00185910
		public unsafe MdUtf8String GetNamespace(int mdToken)
		{
			void* pStringHeap;
			MetadataImport._GetNamespace(this.m_metadataImport2, mdToken, &pStringHeap);
			return new MdUtf8String(pStringHeap);
		}

		// Token: 0x060049EA RID: 18922
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetEventProps(IntPtr scope, int mdToken, void** name, out int eventAttributes);

		// Token: 0x060049EB RID: 18923 RVA: 0x00186734 File Offset: 0x00185934
		public unsafe void GetEventProps(int mdToken, out void* name, out EventAttributes eventAttributes)
		{
			void* ptr;
			int num;
			MetadataImport._GetEventProps(this.m_metadataImport2, mdToken, &ptr, out num);
			name = ptr;
			eventAttributes = (EventAttributes)num;
		}

		// Token: 0x060049EC RID: 18924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldDefProps(IntPtr scope, int mdToken, out int fieldAttributes);

		// Token: 0x060049ED RID: 18925 RVA: 0x00186758 File Offset: 0x00185958
		public void GetFieldDefProps(int mdToken, out FieldAttributes fieldAttributes)
		{
			int num;
			MetadataImport._GetFieldDefProps(this.m_metadataImport2, mdToken, out num);
			fieldAttributes = (FieldAttributes)num;
		}

		// Token: 0x060049EE RID: 18926
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyProps(IntPtr scope, int mdToken, void** name, out int propertyAttributes, out ConstArray signature);

		// Token: 0x060049EF RID: 18927 RVA: 0x00186778 File Offset: 0x00185978
		public unsafe void GetPropertyProps(int mdToken, out void* name, out PropertyAttributes propertyAttributes, out ConstArray signature)
		{
			void* ptr;
			int num;
			MetadataImport._GetPropertyProps(this.m_metadataImport2, mdToken, &ptr, out num, out signature);
			name = ptr;
			propertyAttributes = (PropertyAttributes)num;
		}

		// Token: 0x060049F0 RID: 18928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParentToken(IntPtr scope, int mdToken, out int tkParent);

		// Token: 0x060049F1 RID: 18929 RVA: 0x001867A0 File Offset: 0x001859A0
		public int GetParentToken(int tkToken)
		{
			int result;
			MetadataImport._GetParentToken(this.m_metadataImport2, tkToken, out result);
			return result;
		}

		// Token: 0x060049F2 RID: 18930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParamDefProps(IntPtr scope, int parameterToken, out int sequence, out int attributes);

		// Token: 0x060049F3 RID: 18931 RVA: 0x001867BC File Offset: 0x001859BC
		public void GetParamDefProps(int parameterToken, out int sequence, out ParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetParamDefProps(this.m_metadataImport2, parameterToken, out sequence, out num);
			attributes = (ParameterAttributes)num;
		}

		// Token: 0x060049F4 RID: 18932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetGenericParamProps(IntPtr scope, int genericParameter, out int flags);

		// Token: 0x060049F5 RID: 18933 RVA: 0x001867DC File Offset: 0x001859DC
		public void GetGenericParamProps(int genericParameter, out GenericParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetGenericParamProps(this.m_metadataImport2, genericParameter, out num);
			attributes = (GenericParameterAttributes)num;
		}

		// Token: 0x060049F6 RID: 18934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetScopeProps(IntPtr scope, out Guid mvid);

		// Token: 0x060049F7 RID: 18935 RVA: 0x001867FA File Offset: 0x001859FA
		public void GetScopeProps(out Guid mvid)
		{
			MetadataImport._GetScopeProps(this.m_metadataImport2, out mvid);
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x00186808 File Offset: 0x00185A08
		public ConstArray GetMethodSignature(MetadataToken token)
		{
			if (token.IsMemberRef)
			{
				return this.GetMemberRefProps(token);
			}
			return this.GetSigOfMethodDef(token);
		}

		// Token: 0x060049F9 RID: 18937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfMethodDef(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060049FA RID: 18938 RVA: 0x0018682C File Offset: 0x00185A2C
		public ConstArray GetSigOfMethodDef(int methodToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSigOfMethodDef(this.m_metadataImport2, methodToken, ref result);
			return result;
		}

		// Token: 0x060049FB RID: 18939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSignatureFromToken(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x060049FC RID: 18940 RVA: 0x00186850 File Offset: 0x00185A50
		public ConstArray GetSignatureFromToken(int token)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSignatureFromToken(this.m_metadataImport2, token, ref result);
			return result;
		}

		// Token: 0x060049FD RID: 18941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMemberRefProps(IntPtr scope, int memberTokenRef, out ConstArray signature);

		// Token: 0x060049FE RID: 18942 RVA: 0x00186874 File Offset: 0x00185A74
		public ConstArray GetMemberRefProps(int memberTokenRef)
		{
			ConstArray result;
			MetadataImport._GetMemberRefProps(this.m_metadataImport2, memberTokenRef, out result);
			return result;
		}

		// Token: 0x060049FF RID: 18943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetCustomAttributeProps(IntPtr scope, int customAttributeToken, out int constructorToken, out ConstArray signature);

		// Token: 0x06004A00 RID: 18944 RVA: 0x00186890 File Offset: 0x00185A90
		public void GetCustomAttributeProps(int customAttributeToken, out int constructorToken, out ConstArray signature)
		{
			MetadataImport._GetCustomAttributeProps(this.m_metadataImport2, customAttributeToken, out constructorToken, out signature);
		}

		// Token: 0x06004A01 RID: 18945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetClassLayout(IntPtr scope, int typeTokenDef, out int packSize, out int classSize);

		// Token: 0x06004A02 RID: 18946 RVA: 0x001868A0 File Offset: 0x00185AA0
		public void GetClassLayout(int typeTokenDef, out int packSize, out int classSize)
		{
			MetadataImport._GetClassLayout(this.m_metadataImport2, typeTokenDef, out packSize, out classSize);
		}

		// Token: 0x06004A03 RID: 18947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _GetFieldOffset(IntPtr scope, int typeTokenDef, int fieldTokenDef, out int offset);

		// Token: 0x06004A04 RID: 18948 RVA: 0x001868B0 File Offset: 0x00185AB0
		public bool GetFieldOffset(int typeTokenDef, int fieldTokenDef, out int offset)
		{
			return MetadataImport._GetFieldOffset(this.m_metadataImport2, typeTokenDef, fieldTokenDef, out offset);
		}

		// Token: 0x06004A05 RID: 18949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfFieldDef(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x06004A06 RID: 18950 RVA: 0x001868C0 File Offset: 0x00185AC0
		public ConstArray GetSigOfFieldDef(int fieldToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSigOfFieldDef(this.m_metadataImport2, fieldToken, ref result);
			return result;
		}

		// Token: 0x06004A07 RID: 18951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldMarshal(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x06004A08 RID: 18952 RVA: 0x001868E4 File Offset: 0x00185AE4
		public ConstArray GetFieldMarshal(int fieldToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetFieldMarshal(this.m_metadataImport2, fieldToken, ref result);
			return result;
		}

		// Token: 0x06004A09 RID: 18953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPInvokeMap(IntPtr scope, int token, out int attributes, void** importName, void** importDll);

		// Token: 0x06004A0A RID: 18954 RVA: 0x00186908 File Offset: 0x00185B08
		public unsafe void GetPInvokeMap(int token, out PInvokeAttributes attributes, out string importName, out string importDll)
		{
			int num;
			void* pStringHeap;
			void* pStringHeap2;
			MetadataImport._GetPInvokeMap(this.m_metadataImport2, token, out num, &pStringHeap, &pStringHeap2);
			importName = new MdUtf8String(pStringHeap).ToString();
			importDll = new MdUtf8String(pStringHeap2).ToString();
			attributes = (PInvokeAttributes)num;
		}

		// Token: 0x06004A0B RID: 18955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _IsValidToken(IntPtr scope, int token);

		// Token: 0x06004A0C RID: 18956 RVA: 0x00186959 File Offset: 0x00185B59
		public bool IsValidToken(int token)
		{
			return MetadataImport._IsValidToken(this.m_metadataImport2, token);
		}

		// Token: 0x0400126C RID: 4716
		private readonly IntPtr m_metadataImport2;

		// Token: 0x0400126D RID: 4717
		private readonly object m_keepalive;

		// Token: 0x0400126E RID: 4718
		internal static readonly MetadataImport EmptyImport = new MetadataImport((IntPtr)0, null);
	}
}
