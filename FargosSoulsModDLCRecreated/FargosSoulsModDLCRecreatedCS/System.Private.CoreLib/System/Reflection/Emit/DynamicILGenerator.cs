using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000617 RID: 1559
	internal class DynamicILGenerator : ILGenerator
	{
		// Token: 0x06004EEA RID: 20202 RVA: 0x0018EA3A File Offset: 0x0018DC3A
		internal DynamicILGenerator(DynamicMethod method, byte[] methodSignature, int size) : base(method, size)
		{
			this.m_scope = new DynamicScope();
			this.m_methodSigToken = this.m_scope.GetTokenFor(methodSignature);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0018EA61 File Offset: 0x0018DC61
		internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
		{
			dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_methodBuilder.Name, (byte[])this.m_scope[this.m_methodSigToken], new DynamicResolver(this));
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0018EA98 File Offset: 0x0018DC98
		public override LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			RuntimeType left = localType as RuntimeType;
			if (left == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType);
			}
			LocalBuilder result = new LocalBuilder(this.m_localCount, localType, this.m_methodBuilder);
			this.m_localSignature.AddArgument(localType, pinned);
			this.m_localCount++;
			return result;
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0018EB04 File Offset: 0x0018DD04
		public override void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			int num = 0;
			DynamicMethod dynamicMethod = meth as DynamicMethod;
			int tokenFor;
			if (dynamicMethod == null)
			{
				RuntimeMethodInfo runtimeMethodInfo = meth as RuntimeMethodInfo;
				if (runtimeMethodInfo == null)
				{
					throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "meth");
				}
				RuntimeType runtimeType = runtimeMethodInfo.GetRuntimeType();
				if (runtimeType != null && (runtimeType.IsGenericType || runtimeType.IsArray))
				{
					tokenFor = this.GetTokenFor(runtimeMethodInfo, runtimeType);
				}
				else
				{
					tokenFor = this.GetTokenFor(runtimeMethodInfo);
				}
			}
			else
			{
				if (opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn))
				{
					throw new ArgumentException(SR.Argument_InvalidOpCodeOnDynamicMethod);
				}
				tokenFor = this.GetTokenFor(dynamicMethod);
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush && meth.ReturnType != typeof(void))
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= meth.GetParametersNoCopy().Length;
			}
			if (!meth.IsStatic && !opcode.Equals(OpCodes.Newobj) && !opcode.Equals(OpCodes.Ldtoken) && !opcode.Equals(OpCodes.Ldftn))
			{
				num--;
			}
			base.UpdateStackSize(opcode, num);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0018EC60 File Offset: 0x0018DE60
		public override void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			RuntimeConstructorInfo runtimeConstructorInfo = con as RuntimeConstructorInfo;
			if (runtimeConstructorInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "con");
			}
			RuntimeType runtimeType = runtimeConstructorInfo.GetRuntimeType();
			int tokenFor;
			if (runtimeType != null && (runtimeType.IsGenericType || runtimeType.IsArray))
			{
				tokenFor = this.GetTokenFor(runtimeConstructorInfo, runtimeType);
			}
			else
			{
				tokenFor = this.GetTokenFor(runtimeConstructorInfo);
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.UpdateStackSize(opcode, 1);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0018ECF4 File Offset: 0x0018DEF4
		public override void Emit(OpCode opcode, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType);
			}
			int tokenFor = this.GetTokenFor(runtimeType);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0018ED50 File Offset: 0x0018DF50
		public override void Emit(OpCode opcode, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			RuntimeFieldInfo runtimeFieldInfo = field as RuntimeFieldInfo;
			if (runtimeFieldInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeFieldInfo, "field");
			}
			int tokenFor;
			if (field.DeclaringType == null)
			{
				tokenFor = this.GetTokenFor(runtimeFieldInfo);
			}
			else
			{
				tokenFor = this.GetTokenFor(runtimeFieldInfo, runtimeFieldInfo.GetRuntimeType());
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0018EDCC File Offset: 0x0018DFCC
		public override void Emit(OpCode opcode, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int tokenForString = this.GetTokenForString(str);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenForString);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0018EE04 File Offset: 0x0018E004
		public override void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotAVarArgCallingConvention);
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			base.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				num--;
			}
			num--;
			base.UpdateStackSize(OpCodes.Calli, num);
			int tokenForSig = this.GetTokenForSig(memberRefSignature.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0018EEA4 File Offset: 0x0018E0A4
		public override void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(unmanagedCallConv, returnType);
			if (parameterTypes != null)
			{
				for (int i = 0; i < num2; i++)
				{
					methodSigHelper.AddArgument(parameterTypes[i]);
				}
			}
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= num2;
			}
			num--;
			base.UpdateStackSize(OpCodes.Calli, num);
			base.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			int tokenForSig = this.GetTokenForSig(methodSigHelper.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x0018EF38 File Offset: 0x0018E138
		public override void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(SR.Argument_NotMethodCallOpcode, "opcode");
			}
			if (methodInfo.ContainsGenericParameters)
			{
				throw new ArgumentException(SR.Argument_GenericsInvalid, "methodInfo");
			}
			if (methodInfo.DeclaringType != null && methodInfo.DeclaringType.ContainsGenericParameters)
			{
				throw new ArgumentException(SR.Argument_GenericsInvalid, "methodInfo");
			}
			int num = 0;
			int memberRefToken = this.GetMemberRefToken(methodInfo, optionalParameterTypes);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			num -= methodInfo.GetParameterTypes().Length;
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			base.UpdateStackSize(opcode, num);
			base.PutInteger4(memberRefToken);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0018F050 File Offset: 0x0018E250
		public override void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				base.UpdateStackSize(opcode, num);
			}
			int tokenForSig = this.GetTokenForSig(signature.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0018F0B0 File Offset: 0x0018E2B0
		public override void BeginExceptFilterBlock()
		{
			if (base.CurrExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = base.CurrExcStack[base.CurrExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			base.UpdateStackSize(OpCodes.Nop, 1);
			_ExceptionInfo.MarkFilterAddr(this.ILOffset);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0018F10C File Offset: 0x0018E30C
		public override void BeginCatchBlock(Type exceptionType)
		{
			if (base.CurrExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = base.CurrExcStack[base.CurrExcStackCount - 1];
			RuntimeType runtimeType = exceptionType as RuntimeType;
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(SR.Argument_ShouldNotSpecifyExceptionType);
				}
				this.Emit(OpCodes.Endfilter);
				_ExceptionInfo.MarkCatchAddr(this.ILOffset, null);
				return;
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				if (runtimeType == null)
				{
					throw new ArgumentException(SR.Argument_MustBeRuntimeType);
				}
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
				base.UpdateStackSize(OpCodes.Nop, 1);
				_ExceptionInfo.MarkCatchAddr(this.ILOffset, exceptionType);
				_ExceptionInfo.m_filterAddr[_ExceptionInfo.m_currentCatch - 1] = this.GetTokenFor(runtimeType);
				return;
			}
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x0018F1E3 File Offset: 0x0018E3E3
		public override void UsingNamespace(string ns)
		{
			throw new NotSupportedException(SR.InvalidOperation_NotAllowedInDynamicMethod);
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0018F1E3 File Offset: 0x0018E3E3
		public override void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			throw new NotSupportedException(SR.InvalidOperation_NotAllowedInDynamicMethod);
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x0018F1E3 File Offset: 0x0018E3E3
		public override void BeginScope()
		{
			throw new NotSupportedException(SR.InvalidOperation_NotAllowedInDynamicMethod);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0018F1E3 File Offset: 0x0018E3E3
		public override void EndScope()
		{
			throw new NotSupportedException(SR.InvalidOperation_NotAllowedInDynamicMethod);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0018F1F0 File Offset: 0x0018E3F0
		private int GetMemberRefToken(MethodBase methodInfo, Type[] optionalParameterTypes)
		{
			if (optionalParameterTypes != null && (methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotAVarArgCallingConvention);
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			DynamicMethod dynamicMethod = methodInfo as DynamicMethod;
			if (runtimeMethodInfo == null && dynamicMethod == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "methodInfo");
			}
			ParameterInfo[] parametersNoCopy = methodInfo.GetParametersNoCopy();
			Type[] array;
			if (parametersNoCopy != null && parametersNoCopy.Length != 0)
			{
				array = new Type[parametersNoCopy.Length];
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					array[i] = parametersNoCopy[i].ParameterType;
				}
			}
			else
			{
				array = null;
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(methodInfo.CallingConvention, MethodBuilder.GetMethodBaseReturnType(methodInfo), array, optionalParameterTypes);
			if (runtimeMethodInfo != null)
			{
				return this.GetTokenForVarArgMethod(runtimeMethodInfo, memberRefSignature);
			}
			return this.GetTokenForVarArgMethod(dynamicMethod, memberRefSignature);
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x0018F2B4 File Offset: 0x0018E4B4
		internal override SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(call, returnType);
			if (parameterTypes != null)
			{
				foreach (Type clsArgument in parameterTypes)
				{
					methodSigHelper.AddArgument(clsArgument);
				}
			}
			if (optionalParameterTypes != null && optionalParameterTypes.Length != 0)
			{
				methodSigHelper.AddSentinel();
				foreach (Type clsArgument2 in optionalParameterTypes)
				{
					methodSigHelper.AddArgument(clsArgument2);
				}
			}
			return methodSigHelper;
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x000AB30B File Offset: 0x000AA50B
		internal override void RecordTokenFixup()
		{
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x0018F31D File Offset: 0x0018E51D
		private int GetTokenFor(RuntimeType rtType)
		{
			return this.m_scope.GetTokenFor(rtType.TypeHandle);
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0018F330 File Offset: 0x0018E530
		private int GetTokenFor(RuntimeFieldInfo runtimeField)
		{
			return this.m_scope.GetTokenFor(runtimeField.FieldHandle);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0018F343 File Offset: 0x0018E543
		private int GetTokenFor(RuntimeFieldInfo runtimeField, RuntimeType rtType)
		{
			return this.m_scope.GetTokenFor(runtimeField.FieldHandle, rtType.TypeHandle);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0018F35C File Offset: 0x0018E55C
		private int GetTokenFor(RuntimeConstructorInfo rtMeth)
		{
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0018F36F File Offset: 0x0018E56F
		private int GetTokenFor(RuntimeConstructorInfo rtMeth, RuntimeType rtType)
		{
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0018F35C File Offset: 0x0018E55C
		private int GetTokenFor(RuntimeMethodInfo rtMeth)
		{
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0018F36F File Offset: 0x0018E56F
		private int GetTokenFor(RuntimeMethodInfo rtMeth, RuntimeType rtType)
		{
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0018F388 File Offset: 0x0018E588
		private int GetTokenFor(DynamicMethod dm)
		{
			return this.m_scope.GetTokenFor(dm);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0018F398 File Offset: 0x0018E598
		private int GetTokenForVarArgMethod(RuntimeMethodInfo rtMeth, SignatureHelper sig)
		{
			VarArgMethod varArgMethod = new VarArgMethod(rtMeth, sig);
			return this.m_scope.GetTokenFor(varArgMethod);
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0018F3BC File Offset: 0x0018E5BC
		private int GetTokenForVarArgMethod(DynamicMethod dm, SignatureHelper sig)
		{
			VarArgMethod varArgMethod = new VarArgMethod(dm, sig);
			return this.m_scope.GetTokenFor(varArgMethod);
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0018F3DD File Offset: 0x0018E5DD
		private int GetTokenForString(string s)
		{
			return this.m_scope.GetTokenFor(s);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0018F3EB File Offset: 0x0018E5EB
		private int GetTokenForSig(byte[] sig)
		{
			return this.m_scope.GetTokenFor(sig);
		}

		// Token: 0x0400141F RID: 5151
		internal DynamicScope m_scope;

		// Token: 0x04001420 RID: 5152
		private int m_methodSigToken;
	}
}
