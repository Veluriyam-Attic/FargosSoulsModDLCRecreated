using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000650 RID: 1616
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SignatureHelper
	{
		// Token: 0x060051CD RID: 20941 RVA: 0x00196CB7 File Offset: 0x00195EB7
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00196CC8 File Offset: 0x00195EC8
		internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType, int cGenericParam)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, cGenericParam, returnType, null, null, null, null, null);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00196CE3 File Offset: 0x00195EE3
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, null, null, null);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00196CF4 File Offset: 0x00195EF4
		internal static SignatureHelper GetMethodSpecSigHelper(Module scope, Type[] inst)
		{
			SignatureHelper signatureHelper = new SignatureHelper(scope, MdSigCallingConvention.GenericInst);
			signatureHelper.AddData(inst.Length);
			foreach (Type clsArgument in inst)
			{
				signatureHelper.AddArgument(clsArgument);
			}
			return signatureHelper;
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00196D30 File Offset: 0x00195F30
		internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			return SignatureHelper.GetMethodSigHelper(scope, callingConvention, 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00196D50 File Offset: 0x00195F50
		internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, int cGenericParam, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention mdSigCallingConvention = MdSigCallingConvention.Default;
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				mdSigCallingConvention = MdSigCallingConvention.Vararg;
			}
			if (cGenericParam > 0)
			{
				mdSigCallingConvention |= MdSigCallingConvention.Generic;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				mdSigCallingConvention |= MdSigCallingConvention.HasThis;
			}
			SignatureHelper signatureHelper = new SignatureHelper(scope, mdSigCallingConvention, cGenericParam, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
			signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			return signatureHelper;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00196DB0 File Offset: 0x00195FB0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention callingConvention;
			if (unmanagedCallConv == CallingConvention.Cdecl)
			{
				callingConvention = MdSigCallingConvention.C;
			}
			else if (unmanagedCallConv == CallingConvention.StdCall || unmanagedCallConv == CallingConvention.Winapi)
			{
				callingConvention = MdSigCallingConvention.StdCall;
			}
			else if (unmanagedCallConv == CallingConvention.ThisCall)
			{
				callingConvention = MdSigCallingConvention.ThisCall;
			}
			else
			{
				if (unmanagedCallConv != CallingConvention.FastCall)
				{
					throw new ArgumentException(SR.Argument_UnknownUnmanagedCallConv, "unmanagedCallConv");
				}
				callingConvention = MdSigCallingConvention.FastCall;
			}
			return new SignatureHelper(mod, callingConvention, returnType, null, null);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00196E0A File Offset: 0x0019600A
		public static SignatureHelper GetLocalVarSigHelper()
		{
			return SignatureHelper.GetLocalVarSigHelper(null);
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x00196E12 File Offset: 0x00196012
		public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, [Nullable(2)] Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, callingConvention, returnType);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x00196E1C File Offset: 0x0019601C
		public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, [Nullable(2)] Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, unmanagedCallingConvention, returnType);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x00196E26 File Offset: 0x00196026
		public static SignatureHelper GetLocalVarSigHelper([Nullable(2)] Module mod)
		{
			return new SignatureHelper(mod, MdSigCallingConvention.LocalSig);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00196E2F File Offset: 0x0019602F
		public static SignatureHelper GetFieldSigHelper([Nullable(2)] Module mod)
		{
			return new SignatureHelper(mod, MdSigCallingConvention.Field);
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00196E38 File Offset: 0x00196038
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			return SignatureHelper.GetPropertySigHelper(mod, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x00196E46 File Offset: 0x00196046
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] requiredReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] requiredParameterTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] optionalParameterTypeCustomModifiers)
		{
			return SignatureHelper.GetPropertySigHelper(mod, (CallingConventions)0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x00196E58 File Offset: 0x00196058
		[NullableContext(2)]
		[return: Nullable(1)]
		public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] requiredReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalReturnTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] requiredParameterTypeCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] optionalParameterTypeCustomModifiers)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention mdSigCallingConvention = MdSigCallingConvention.Property;
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				mdSigCallingConvention |= MdSigCallingConvention.HasThis;
			}
			SignatureHelper signatureHelper = new SignatureHelper(mod, mdSigCallingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
			signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			return signatureHelper;
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x00196EA2 File Offset: 0x001960A2
		internal static SignatureHelper GetTypeSigToken(Module module, Type type)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return new SignatureHelper(module, type);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00196ED3 File Offset: 0x001960D3
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention)
		{
			this.Init(mod, callingConvention);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00196EE3 File Offset: 0x001960E3
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention, int cGenericParameters, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			this.Init(mod, callingConvention, cGenericParameters);
			if (callingConvention == MdSigCallingConvention.Field)
			{
				throw new ArgumentException(SR.Argument_BadFieldSig);
			}
			this.AddOneArgTypeHelper(returnType, requiredCustomModifiers, optionalCustomModifiers);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00196F0F File Offset: 0x0019610F
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers) : this(mod, callingConvention, 0, returnType, requiredCustomModifiers, optionalCustomModifiers)
		{
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00196F1F File Offset: 0x0019611F
		private SignatureHelper(Module mod, Type type)
		{
			this.Init(mod);
			this.AddOneArgTypeHelper(type);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00196F38 File Offset: 0x00196138
		[MemberNotNull("m_signature")]
		private void Init(Module mod)
		{
			this.m_signature = new byte[32];
			this.m_currSig = 0;
			this.m_module = (mod as ModuleBuilder);
			this.m_argCount = 0;
			this.m_sigDone = false;
			this.m_sizeLoc = -1;
			if (this.m_module == null && mod != null)
			{
				throw new ArgumentException(SR.NotSupported_MustBeModuleBuilder);
			}
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00196F9C File Offset: 0x0019619C
		[MemberNotNull("m_signature")]
		private void Init(Module mod, MdSigCallingConvention callingConvention)
		{
			this.Init(mod, callingConvention, 0);
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x00196FA8 File Offset: 0x001961A8
		[MemberNotNull("m_signature")]
		private void Init(Module mod, MdSigCallingConvention callingConvention, int cGenericParam)
		{
			this.Init(mod);
			this.AddData((int)callingConvention);
			if (callingConvention == MdSigCallingConvention.Field || callingConvention == MdSigCallingConvention.GenericInst)
			{
				this.m_sizeLoc = -1;
				return;
			}
			if (cGenericParam > 0)
			{
				this.AddData(cGenericParam);
			}
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			this.m_sizeLoc = currSig;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00196FF6 File Offset: 0x001961F6
		private void AddOneArgTypeHelper(Type argument, bool pinned)
		{
			if (pinned)
			{
				this.AddElementType(CorElementType.ELEMENT_TYPE_PINNED);
			}
			this.AddOneArgTypeHelper(argument);
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x0019700C File Offset: 0x0019620C
		private void AddOneArgTypeHelper(Type clsArgument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			if (optionalCustomModifiers != null)
			{
				foreach (Type type in optionalCustomModifiers)
				{
					if (type == null)
					{
						throw new ArgumentNullException("optionalCustomModifiers");
					}
					if (type.HasElementType)
					{
						throw new ArgumentException(SR.Argument_ArraysInvalid, "optionalCustomModifiers");
					}
					if (type.ContainsGenericParameters)
					{
						throw new ArgumentException(SR.Argument_GenericsInvalid, "optionalCustomModifiers");
					}
					this.AddElementType(CorElementType.ELEMENT_TYPE_CMOD_OPT);
					int token = this.m_module.GetTypeToken(type).Token;
					this.AddToken(token);
				}
			}
			if (requiredCustomModifiers != null)
			{
				foreach (Type type2 in requiredCustomModifiers)
				{
					if (type2 == null)
					{
						throw new ArgumentNullException("requiredCustomModifiers");
					}
					if (type2.HasElementType)
					{
						throw new ArgumentException(SR.Argument_ArraysInvalid, "requiredCustomModifiers");
					}
					if (type2.ContainsGenericParameters)
					{
						throw new ArgumentException(SR.Argument_GenericsInvalid, "requiredCustomModifiers");
					}
					this.AddElementType(CorElementType.ELEMENT_TYPE_CMOD_REQD);
					int token2 = this.m_module.GetTypeToken(type2).Token;
					this.AddToken(token2);
				}
			}
			this.AddOneArgTypeHelper(clsArgument);
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0019712C File Offset: 0x0019632C
		private void AddOneArgTypeHelper(Type clsArgument)
		{
			this.AddOneArgTypeHelperWorker(clsArgument, false);
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00197138 File Offset: 0x00196338
		private void AddOneArgTypeHelperWorker(Type clsArgument, bool lastWasGenericInst)
		{
			if (clsArgument.IsGenericParameter)
			{
				if (clsArgument.DeclaringMethod != null)
				{
					this.AddElementType(CorElementType.ELEMENT_TYPE_MVAR);
				}
				else
				{
					this.AddElementType(CorElementType.ELEMENT_TYPE_VAR);
				}
				this.AddData(clsArgument.GenericParameterPosition);
				return;
			}
			if (clsArgument.IsGenericType && (!clsArgument.IsGenericTypeDefinition || !lastWasGenericInst))
			{
				this.AddElementType(CorElementType.ELEMENT_TYPE_GENERICINST);
				this.AddOneArgTypeHelperWorker(clsArgument.GetGenericTypeDefinition(), true);
				Type[] genericArguments = clsArgument.GetGenericArguments();
				this.AddData(genericArguments.Length);
				foreach (Type clsArgument2 in genericArguments)
				{
					this.AddOneArgTypeHelper(clsArgument2);
				}
				return;
			}
			if (clsArgument is TypeBuilder)
			{
				TypeBuilder typeBuilder = (TypeBuilder)clsArgument;
				TypeToken typeToken;
				if (typeBuilder.Module.Equals(this.m_module))
				{
					typeToken = typeBuilder.TypeToken;
				}
				else
				{
					typeToken = this.m_module.GetTypeToken(clsArgument);
				}
				if (clsArgument.IsValueType)
				{
					this.InternalAddTypeToken(typeToken, CorElementType.ELEMENT_TYPE_VALUETYPE);
					return;
				}
				this.InternalAddTypeToken(typeToken, CorElementType.ELEMENT_TYPE_CLASS);
				return;
			}
			else if (clsArgument is EnumBuilder)
			{
				TypeBuilder typeBuilder2 = ((EnumBuilder)clsArgument).m_typeBuilder;
				TypeToken typeToken2;
				if (typeBuilder2.Module.Equals(this.m_module))
				{
					typeToken2 = typeBuilder2.TypeToken;
				}
				else
				{
					typeToken2 = this.m_module.GetTypeToken(clsArgument);
				}
				if (clsArgument.IsValueType)
				{
					this.InternalAddTypeToken(typeToken2, CorElementType.ELEMENT_TYPE_VALUETYPE);
					return;
				}
				this.InternalAddTypeToken(typeToken2, CorElementType.ELEMENT_TYPE_CLASS);
				return;
			}
			else
			{
				if (clsArgument.IsByRef)
				{
					this.AddElementType(CorElementType.ELEMENT_TYPE_BYREF);
					clsArgument = clsArgument.GetElementType();
					this.AddOneArgTypeHelper(clsArgument);
					return;
				}
				if (clsArgument.IsPointer)
				{
					this.AddElementType(CorElementType.ELEMENT_TYPE_PTR);
					this.AddOneArgTypeHelper(clsArgument.GetElementType());
					return;
				}
				if (clsArgument.IsArray)
				{
					if (clsArgument.IsSZArray)
					{
						this.AddElementType(CorElementType.ELEMENT_TYPE_SZARRAY);
						this.AddOneArgTypeHelper(clsArgument.GetElementType());
						return;
					}
					this.AddElementType(CorElementType.ELEMENT_TYPE_ARRAY);
					this.AddOneArgTypeHelper(clsArgument.GetElementType());
					int arrayRank = clsArgument.GetArrayRank();
					this.AddData(arrayRank);
					this.AddData(0);
					this.AddData(arrayRank);
					for (int j = 0; j < arrayRank; j++)
					{
						this.AddData(0);
					}
					return;
				}
				else
				{
					CorElementType corElementType = CorElementType.ELEMENT_TYPE_MAX;
					if (clsArgument is RuntimeType)
					{
						corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType)clsArgument);
						if (corElementType == CorElementType.ELEMENT_TYPE_CLASS)
						{
							if (clsArgument == typeof(object))
							{
								corElementType = CorElementType.ELEMENT_TYPE_OBJECT;
							}
							else if (clsArgument == typeof(string))
							{
								corElementType = CorElementType.ELEMENT_TYPE_STRING;
							}
						}
					}
					if (SignatureHelper.IsSimpleType(corElementType))
					{
						this.AddElementType(corElementType);
						return;
					}
					if (this.m_module == null)
					{
						this.InternalAddRuntimeType(clsArgument);
						return;
					}
					if (clsArgument.IsValueType)
					{
						this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.ELEMENT_TYPE_VALUETYPE);
						return;
					}
					this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.ELEMENT_TYPE_CLASS);
					return;
				}
			}
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x001973E0 File Offset: 0x001965E0
		private void AddData(int data)
		{
			if (this.m_currSig + 4 > this.m_signature.Length)
			{
				this.m_signature = SignatureHelper.ExpandArray(this.m_signature);
			}
			if (data <= 127)
			{
				byte[] signature = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature[currSig] = (byte)data;
				return;
			}
			if (data <= 16383)
			{
				BinaryPrimitives.WriteInt16BigEndian(this.m_signature.AsSpan(this.m_currSig), (short)(data | 32768));
				this.m_currSig += 2;
				return;
			}
			if (data <= 536870911)
			{
				BinaryPrimitives.WriteInt32BigEndian(this.m_signature.AsSpan(this.m_currSig), (int)((long)data | (long)((ulong)-1073741824)));
				this.m_currSig += 4;
				return;
			}
			throw new ArgumentException(SR.Argument_LargeInteger);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x001974A8 File Offset: 0x001966A8
		private void AddElementType(CorElementType cvt)
		{
			if (this.m_currSig + 1 > this.m_signature.Length)
			{
				this.m_signature = SignatureHelper.ExpandArray(this.m_signature);
			}
			byte[] signature = this.m_signature;
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature[currSig] = cvt;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x001974F4 File Offset: 0x001966F4
		private void AddToken(int token)
		{
			int num = token & 16777215;
			MetadataTokenType metadataTokenType = (MetadataTokenType)(token & -16777216);
			if (num > 67108863)
			{
				throw new ArgumentException(SR.Argument_LargeInteger);
			}
			num <<= 2;
			if (metadataTokenType == MetadataTokenType.TypeRef)
			{
				num |= 1;
			}
			else if (metadataTokenType == MetadataTokenType.TypeSpec)
			{
				num |= 2;
			}
			this.AddData(num);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00197549 File Offset: 0x00196749
		private void InternalAddTypeToken(TypeToken clsToken, CorElementType CorType)
		{
			this.AddElementType(CorType);
			this.AddToken(clsToken.Token);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x00197560 File Offset: 0x00196760
		private unsafe void InternalAddRuntimeType(Type type)
		{
			this.AddElementType(CorElementType.ELEMENT_TYPE_INTERNAL);
			IntPtr value = type.GetTypeHandleInternal().Value;
			if (this.m_currSig + sizeof(void*) > this.m_signature.Length)
			{
				this.m_signature = SignatureHelper.ExpandArray(this.m_signature);
			}
			byte* ptr = (byte*)(&value);
			for (int i = 0; i < sizeof(void*); i++)
			{
				byte[] signature = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature[currSig] = ptr[i];
			}
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x001975E0 File Offset: 0x001967E0
		private static byte[] ExpandArray(byte[] inArray)
		{
			return SignatureHelper.ExpandArray(inArray, inArray.Length * 2);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x001975F0 File Offset: 0x001967F0
		private static byte[] ExpandArray(byte[] inArray, int requiredLength)
		{
			if (requiredLength < inArray.Length)
			{
				requiredLength = inArray.Length * 2;
			}
			byte[] array = new byte[requiredLength];
			Buffer.BlockCopy(inArray, 0, array, 0, inArray.Length);
			return array;
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0019761E File Offset: 0x0019681E
		private void IncrementArgCounts()
		{
			if (this.m_sizeLoc == -1)
			{
				return;
			}
			this.m_argCount++;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00197638 File Offset: 0x00196838
		private void SetNumberOfSignatureElements(bool forceCopy)
		{
			int currSig = this.m_currSig;
			if (this.m_sizeLoc == -1)
			{
				return;
			}
			if (this.m_argCount < 128 && !forceCopy)
			{
				this.m_signature[this.m_sizeLoc] = (byte)this.m_argCount;
				return;
			}
			int num;
			if (this.m_argCount < 128)
			{
				num = 1;
			}
			else if (this.m_argCount < 16384)
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
			byte[] array = new byte[this.m_currSig + num - 1];
			array[0] = this.m_signature[0];
			Buffer.BlockCopy(this.m_signature, this.m_sizeLoc + 1, array, this.m_sizeLoc + num, currSig - (this.m_sizeLoc + 1));
			this.m_signature = array;
			this.m_currSig = this.m_sizeLoc;
			this.AddData(this.m_argCount);
			this.m_currSig = currSig + (num - 1);
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0019770A File Offset: 0x0019690A
		internal int ArgumentCount
		{
			get
			{
				return this.m_argCount;
			}
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00197712 File Offset: 0x00196912
		internal static bool IsSimpleType(CorElementType type)
		{
			return type <= CorElementType.ELEMENT_TYPE_STRING || (type == CorElementType.ELEMENT_TYPE_TYPEDBYREF || type == CorElementType.ELEMENT_TYPE_I || type == CorElementType.ELEMENT_TYPE_U || type == CorElementType.ELEMENT_TYPE_OBJECT);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00197732 File Offset: 0x00196932
		internal byte[] InternalGetSignature(out int length)
		{
			if (!this.m_sigDone)
			{
				this.m_sigDone = true;
				this.SetNumberOfSignatureElements(false);
			}
			length = this.m_currSig;
			return this.m_signature;
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x00197758 File Offset: 0x00196958
		internal byte[] InternalGetSignatureArray()
		{
			int argCount = this.m_argCount;
			int currSig = this.m_currSig;
			int num = currSig;
			if (argCount < 127)
			{
				num++;
			}
			else if (argCount < 16383)
			{
				num += 2;
			}
			else
			{
				num += 4;
			}
			byte[] array = new byte[num];
			int dstOffset = 0;
			array[dstOffset++] = this.m_signature[0];
			if (argCount <= 127)
			{
				array[dstOffset++] = (byte)(argCount & 255);
			}
			else if (argCount <= 16383)
			{
				array[dstOffset++] = (byte)(argCount >> 8 | 128);
				array[dstOffset++] = (byte)(argCount & 255);
			}
			else
			{
				if (argCount > 536870911)
				{
					throw new ArgumentException(SR.Argument_LargeInteger);
				}
				array[dstOffset++] = (byte)(argCount >> 24 | 192);
				array[dstOffset++] = (byte)(argCount >> 16 & 255);
				array[dstOffset++] = (byte)(argCount >> 8 & 255);
				array[dstOffset++] = (byte)(argCount & 255);
			}
			Buffer.BlockCopy(this.m_signature, 2, array, dstOffset, currSig - 2);
			array[num - 1] = 0;
			return array;
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00197870 File Offset: 0x00196A70
		public void AddArgument(Type clsArgument)
		{
			this.AddArgument(clsArgument, null, null);
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0019787B File Offset: 0x00196A7B
		public void AddArgument(Type argument, bool pinned)
		{
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			this.IncrementArgCounts();
			this.AddOneArgTypeHelper(argument, pinned);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x001978A0 File Offset: 0x00196AA0
		public void AddArguments([Nullable(new byte[]
		{
			2,
			1
		})] Type[] arguments, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] requiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1,
			1
		})] Type[][] optionalCustomModifiers)
		{
			if (requiredCustomModifiers != null && (arguments == null || requiredCustomModifiers.Length != arguments.Length))
			{
				throw new ArgumentException(SR.Format(SR.Argument_MismatchedArrays, "requiredCustomModifiers", "arguments"));
			}
			if (optionalCustomModifiers != null && (arguments == null || optionalCustomModifiers.Length != arguments.Length))
			{
				throw new ArgumentException(SR.Format(SR.Argument_MismatchedArrays, "optionalCustomModifiers", "arguments"));
			}
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					this.AddArgument(arguments[i], (requiredCustomModifiers != null) ? requiredCustomModifiers[i] : null, (optionalCustomModifiers != null) ? optionalCustomModifiers[i] : null);
				}
			}
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x00197929 File Offset: 0x00196B29
		public void AddArgument(Type argument, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] requiredCustomModifiers, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalCustomModifiers)
		{
			if (this.m_sigDone)
			{
				throw new ArgumentException(SR.Argument_SigIsFinalized);
			}
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			this.IncrementArgCounts();
			this.AddOneArgTypeHelper(argument, requiredCustomModifiers, optionalCustomModifiers);
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00197961 File Offset: 0x00196B61
		public void AddSentinel()
		{
			this.AddElementType(CorElementType.ELEMENT_TYPE_SENTINEL);
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0019796C File Offset: 0x00196B6C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!(obj is SignatureHelper))
			{
				return false;
			}
			SignatureHelper signatureHelper = (SignatureHelper)obj;
			if (!signatureHelper.m_module.Equals(this.m_module) || signatureHelper.m_currSig != this.m_currSig || signatureHelper.m_sizeLoc != this.m_sizeLoc || signatureHelper.m_sigDone != this.m_sigDone)
			{
				return false;
			}
			for (int i = 0; i < this.m_currSig; i++)
			{
				if (this.m_signature[i] != signatureHelper.m_signature[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x001979F0 File Offset: 0x00196BF0
		public override int GetHashCode()
		{
			int num = this.m_module.GetHashCode() + this.m_currSig + this.m_sizeLoc;
			if (this.m_sigDone)
			{
				num++;
			}
			for (int i = 0; i < this.m_currSig; i++)
			{
				num += this.m_signature[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00197A49 File Offset: 0x00196C49
		public byte[] GetSignature()
		{
			return this.GetSignature(false);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00197A54 File Offset: 0x00196C54
		internal byte[] GetSignature(bool appendEndOfSig)
		{
			if (!this.m_sigDone)
			{
				if (appendEndOfSig)
				{
					this.AddElementType(CorElementType.ELEMENT_TYPE_END);
				}
				this.SetNumberOfSignatureElements(true);
				this.m_sigDone = true;
			}
			if (this.m_signature.Length > this.m_currSig)
			{
				byte[] array = new byte[this.m_currSig];
				Array.Copy(this.m_signature, array, this.m_currSig);
				this.m_signature = array;
			}
			return this.m_signature;
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x00197ABC File Offset: 0x00196CBC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Length: ").Append(this.m_currSig).AppendLine();
			if (this.m_sizeLoc != -1)
			{
				stringBuilder.Append("Arguments: ").Append(this.m_signature[this.m_sizeLoc]).AppendLine();
			}
			else
			{
				stringBuilder.AppendLine("Field Signature");
			}
			stringBuilder.AppendLine("Signature: ");
			for (int i = 0; i <= this.m_currSig; i++)
			{
				stringBuilder.Append(this.m_signature[i]).Append("  ");
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x040014FE RID: 5374
		private byte[] m_signature;

		// Token: 0x040014FF RID: 5375
		private int m_currSig;

		// Token: 0x04001500 RID: 5376
		private int m_sizeLoc;

		// Token: 0x04001501 RID: 5377
		private ModuleBuilder m_module;

		// Token: 0x04001502 RID: 5378
		private bool m_sigDone;

		// Token: 0x04001503 RID: 5379
		private int m_argCount;
	}
}
