using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000626 RID: 1574
	[Nullable(0)]
	[NullableContext(1)]
	public class ILGenerator
	{
		// Token: 0x06005027 RID: 20519 RVA: 0x001912BA File Offset: 0x001904BA
		internal static T[] EnlargeArray<T>(T[] incoming)
		{
			return ILGenerator.EnlargeArray<T>(incoming, incoming.Length * 2);
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x001912C8 File Offset: 0x001904C8
		internal static T[] EnlargeArray<T>(T[] incoming, int requiredSize)
		{
			T[] array = new T[requiredSize];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x001912E7 File Offset: 0x001904E7
		internal int CurrExcStackCount
		{
			get
			{
				return this.m_currExcStackCount;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x001912EF File Offset: 0x001904EF
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal __ExceptionInfo[] CurrExcStack
		{
			get
			{
				return this.m_currExcStack;
			}
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x001912F7 File Offset: 0x001904F7
		internal ILGenerator(MethodInfo methodBuilder) : this(methodBuilder, 64)
		{
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x00191304 File Offset: 0x00190504
		internal ILGenerator(MethodInfo methodBuilder, int size)
		{
			this.m_ILStream = new byte[Math.Max(size, 16)];
			this.m_ScopeTree = new ScopeTree();
			this.m_LineNumberInfo = new LineNumberInfo();
			this.m_methodBuilder = methodBuilder;
			MethodBuilder methodBuilder2 = this.m_methodBuilder as MethodBuilder;
			this.m_localSignature = SignatureHelper.GetLocalVarSigHelper((methodBuilder2 != null) ? methodBuilder2.GetTypeBuilder().Module : null);
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x00191370 File Offset: 0x00190570
		internal virtual void RecordTokenFixup()
		{
			if (this.m_RelocFixupList == null)
			{
				this.m_RelocFixupList = new int[8];
			}
			else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
			{
				this.m_RelocFixupList = ILGenerator.EnlargeArray<int>(this.m_RelocFixupList);
			}
			int[] relocFixupList = this.m_RelocFixupList;
			int relocFixupCount = this.m_RelocFixupCount;
			this.m_RelocFixupCount = relocFixupCount + 1;
			relocFixupList[relocFixupCount] = this.m_length;
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x001913D4 File Offset: 0x001905D4
		internal void InternalEmit(OpCode opcode)
		{
			short value = opcode.Value;
			if (opcode.Size != 1)
			{
				BinaryPrimitives.WriteInt16BigEndian(this.m_ILStream.AsSpan(this.m_length), value);
				this.m_length += 2;
			}
			else
			{
				byte[] ilstream = this.m_ILStream;
				int length = this.m_length;
				this.m_length = length + 1;
				ilstream[length] = (byte)value;
			}
			this.UpdateStackSize(opcode, opcode.StackChange());
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00191444 File Offset: 0x00190644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void UpdateStackSize(OpCode opcode, int stackchange)
		{
			this.m_maxMidStackCur += stackchange;
			if (this.m_maxMidStackCur > this.m_maxMidStack)
			{
				this.m_maxMidStack = this.m_maxMidStackCur;
			}
			else if (this.m_maxMidStackCur < 0)
			{
				this.m_maxMidStackCur = 0;
			}
			if (opcode.EndsUncondJmpBlk())
			{
				this.m_maxStackSize += this.m_maxMidStack;
				this.m_maxMidStack = 0;
				this.m_maxMidStackCur = 0;
			}
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x001914B5 File Offset: 0x001906B5
		private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMethodTokenInternal(method, optionalParameterTypes, useMethodDef);
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x001914CF File Offset: 0x001906CF
		internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x001914DD File Offset: 0x001906DD
		private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, cGenericParameters);
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x001914FC File Offset: 0x001906FC
		internal byte[] BakeByteArray()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new ArgumentException(SR.Argument_UnclosedExceptionBlock);
			}
			if (this.m_length == 0)
			{
				return null;
			}
			byte[] array = new byte[this.m_length];
			Array.Copy(this.m_ILStream, array, this.m_length);
			for (int i = 0; i < this.m_fixupCount; i++)
			{
				__FixupData _FixupData = this.m_fixupData[i];
				int num = this.GetLabelPos(_FixupData.m_fixupLabel) - (_FixupData.m_fixupPos + _FixupData.m_fixupInstSize);
				if (_FixupData.m_fixupInstSize == 1)
				{
					if (num < -128 || num > 127)
					{
						throw new NotSupportedException(SR.Format(SR.NotSupported_IllegalOneByteBranch, _FixupData.m_fixupPos, num));
					}
					array[_FixupData.m_fixupPos] = (byte)num;
				}
				else
				{
					BinaryPrimitives.WriteInt32LittleEndian(array.AsSpan(_FixupData.m_fixupPos), num);
				}
			}
			return array;
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x001915D4 File Offset: 0x001907D4
		internal __ExceptionInfo[] GetExceptions()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new NotSupportedException(SR.Argument_UnclosedExceptionBlock);
			}
			if (this.m_exceptionCount == 0)
			{
				return null;
			}
			__ExceptionInfo[] array = new __ExceptionInfo[this.m_exceptionCount];
			Array.Copy(this.m_exceptions, array, this.m_exceptionCount);
			ILGenerator.SortExceptions(array);
			return array;
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x00191623 File Offset: 0x00190823
		internal void EnsureCapacity(int size)
		{
			if (this.m_length + size >= this.m_ILStream.Length)
			{
				this.IncreaseCapacity(size);
			}
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00191640 File Offset: 0x00190840
		private void IncreaseCapacity(int size)
		{
			byte[] array = new byte[Math.Max(this.m_ILStream.Length * 2, this.m_length + size)];
			Array.Copy(this.m_ILStream, array, this.m_ILStream.Length);
			this.m_ILStream = array;
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00191685 File Offset: 0x00190885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void PutInteger4(int value)
		{
			BinaryPrimitives.WriteInt32LittleEndian(this.m_ILStream.AsSpan(this.m_length), value);
			this.m_length += 4;
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x001916AC File Offset: 0x001908AC
		private int GetLabelPos(Label lbl)
		{
			int labelValue = lbl.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelCount || this.m_labelList == null)
			{
				throw new ArgumentException(SR.Argument_BadLabel);
			}
			if (this.m_labelList[labelValue] < 0)
			{
				throw new ArgumentException(SR.Argument_BadLabelContent);
			}
			return this.m_labelList[labelValue];
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x00191700 File Offset: 0x00190900
		private void AddFixup(Label lbl, int pos, int instSize)
		{
			if (this.m_fixupData == null)
			{
				this.m_fixupData = new __FixupData[8];
			}
			else if (this.m_fixupData.Length <= this.m_fixupCount)
			{
				this.m_fixupData = ILGenerator.EnlargeArray<__FixupData>(this.m_fixupData);
			}
			__FixupData[] fixupData = this.m_fixupData;
			int fixupCount = this.m_fixupCount;
			this.m_fixupCount = fixupCount + 1;
			fixupData[fixupCount] = new __FixupData
			{
				m_fixupPos = pos,
				m_fixupLabel = lbl,
				m_fixupInstSize = instSize
			};
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00191781 File Offset: 0x00190981
		internal int GetMaxStackSize()
		{
			return this.m_maxStackSize;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0019178C File Offset: 0x0019098C
		private static void SortExceptions(__ExceptionInfo[] exceptions)
		{
			for (int i = 0; i < exceptions.Length; i++)
			{
				int num = i;
				for (int j = i + 1; j < exceptions.Length; j++)
				{
					if (exceptions[num].IsInner(exceptions[j]))
					{
						num = j;
					}
				}
				__ExceptionInfo _ExceptionInfo = exceptions[i];
				exceptions[i] = exceptions[num];
				exceptions[num] = _ExceptionInfo;
			}
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x001917D8 File Offset: 0x001909D8
		internal int[] GetTokenFixups()
		{
			if (this.m_RelocFixupCount == 0)
			{
				return null;
			}
			int[] array = new int[this.m_RelocFixupCount];
			Array.Copy(this.m_RelocFixupList, array, this.m_RelocFixupCount);
			return array;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0019180E File Offset: 0x00190A0E
		public virtual void Emit(OpCode opcode)
		{
			this.EnsureCapacity(3);
			this.InternalEmit(opcode);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00191820 File Offset: 0x00190A20
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = arg;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00191854 File Offset: 0x00190A54
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)arg;
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x00191889 File Offset: 0x00190A89
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.EnsureCapacity(5);
			this.InternalEmit(opcode);
			BinaryPrimitives.WriteInt16LittleEndian(this.m_ILStream.AsSpan(this.m_length), arg);
			this.m_length += 2;
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x001918C0 File Offset: 0x00190AC0
		public virtual void Emit(OpCode opcode, int arg)
		{
			if (opcode.Equals(OpCodes.Ldc_I4))
			{
				if (arg >= -1 && arg <= 8)
				{
					OpCode opCode;
					switch (arg)
					{
					case -1:
						opCode = OpCodes.Ldc_I4_M1;
						break;
					case 0:
						opCode = OpCodes.Ldc_I4_0;
						break;
					case 1:
						opCode = OpCodes.Ldc_I4_1;
						break;
					case 2:
						opCode = OpCodes.Ldc_I4_2;
						break;
					case 3:
						opCode = OpCodes.Ldc_I4_3;
						break;
					case 4:
						opCode = OpCodes.Ldc_I4_4;
						break;
					case 5:
						opCode = OpCodes.Ldc_I4_5;
						break;
					case 6:
						opCode = OpCodes.Ldc_I4_6;
						break;
					case 7:
						opCode = OpCodes.Ldc_I4_7;
						break;
					default:
						opCode = OpCodes.Ldc_I4_8;
						break;
					}
					opcode = opCode;
					this.Emit(opcode);
					return;
				}
				if (arg >= -128 && arg <= 127)
				{
					this.Emit(OpCodes.Ldc_I4_S, (sbyte)arg);
					return;
				}
			}
			else if (opcode.Equals(OpCodes.Ldarg))
			{
				if (arg <= 3)
				{
					OpCode opCode;
					switch (arg)
					{
					case 0:
						opCode = OpCodes.Ldarg_0;
						break;
					case 1:
						opCode = OpCodes.Ldarg_1;
						break;
					case 2:
						opCode = OpCodes.Ldarg_2;
						break;
					default:
						opCode = OpCodes.Ldarg_3;
						break;
					}
					this.Emit(opCode);
					return;
				}
				if (arg <= 255)
				{
					this.Emit(OpCodes.Ldarg_S, (byte)arg);
					return;
				}
				if (arg <= 65535)
				{
					this.Emit(OpCodes.Ldarg, (short)arg);
					return;
				}
			}
			else if (opcode.Equals(OpCodes.Ldarga))
			{
				if (arg <= 255)
				{
					this.Emit(OpCodes.Ldarga_S, (byte)arg);
					return;
				}
				if (arg <= 65535)
				{
					this.Emit(OpCodes.Ldarga, (short)arg);
					return;
				}
			}
			else if (opcode.Equals(OpCodes.Starg))
			{
				if (arg <= 255)
				{
					this.Emit(OpCodes.Starg_S, (byte)arg);
					return;
				}
				if (arg <= 65535)
				{
					this.Emit(OpCodes.Starg, (short)arg);
					return;
				}
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(arg);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x00191A98 File Offset: 0x00190C98
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
			{
				this.EmitCall(opcode, meth, null);
				return;
			}
			bool useMethodDef = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
			int methodToken = this.GetMethodToken(meth, null, useMethodDef);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.UpdateStackSize(opcode, 0);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x00191B48 File Offset: 0x00190D48
		[NullableContext(2)]
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotAVarArgCallingConvention);
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			this.EnsureCapacity(7);
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
			this.UpdateStackSize(OpCodes.Calli, num);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(memberRefSignature).Token);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x00191C00 File Offset: 0x00190E00
		[NullableContext(2)]
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(moduleBuilder, unmanagedCallConv, returnType);
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
			this.UpdateStackSize(OpCodes.Calli, num);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(methodSigHelper).Token);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00191CB0 File Offset: 0x00190EB0
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, [Nullable(new byte[]
		{
			2,
			1
		})] Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(SR.Argument_NotMethodCallOpcode, "opcode");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(methodInfo, optionalParameterTypes, false);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			Type[] parameterTypes = methodInfo.GetParameterTypes();
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00191D90 File Offset: 0x00190F90
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetSignatureToken(signature).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				this.UpdateStackSize(opcode, num);
			}
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00191E0C File Offset: 0x0019100C
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(con, null, true);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				Type[] parameterTypes = con.GetParameterTypes();
				if (parameterTypes != null)
				{
					num -= parameterTypes.Length;
				}
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00191E88 File Offset: 0x00191088
		public virtual void Emit(OpCode opcode, Type cls)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token;
			if (opcode == OpCodes.Ldtoken && cls != null && cls.IsGenericTypeDefinition)
			{
				token = moduleBuilder.GetTypeToken(cls).Token;
			}
			else
			{
				token = moduleBuilder.GetTypeTokenInternal(cls).Token;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x00191F01 File Offset: 0x00191101
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			BinaryPrimitives.WriteInt64LittleEndian(this.m_ILStream.AsSpan(this.m_length), arg);
			this.m_length += 8;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x00191F37 File Offset: 0x00191137
		public virtual void Emit(OpCode opcode, float arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			BinaryPrimitives.WriteInt32LittleEndian(this.m_ILStream.AsSpan(this.m_length), BitConverter.SingleToInt32Bits(arg));
			this.m_length += 4;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x00191F71 File Offset: 0x00191171
		public virtual void Emit(OpCode opcode, double arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			BinaryPrimitives.WriteInt64LittleEndian(this.m_ILStream.AsSpan(this.m_length), BitConverter.DoubleToInt64Bits(arg));
			this.m_length += 8;
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x00191FAC File Offset: 0x001911AC
		public virtual void Emit(OpCode opcode, Label label)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (OpCodes.TakesSingleByteArgument(opcode))
			{
				int length = this.m_length;
				this.m_length = length + 1;
				this.AddFixup(label, length, 1);
				return;
			}
			this.AddFixup(label, this.m_length, 4);
			this.m_length += 4;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00192008 File Offset: 0x00191208
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.EnsureCapacity(num * 4 + 7);
			this.InternalEmit(opcode);
			this.PutInteger4(num);
			int i = num * 4;
			int num2 = 0;
			while (i > 0)
			{
				this.AddFixup(labels[num2], this.m_length, i);
				this.m_length += 4;
				i -= 4;
				num2++;
			}
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00192078 File Offset: 0x00191278
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetFieldToken(field).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x001920C4 File Offset: 0x001912C4
		public virtual void Emit(OpCode opcode, string str)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetStringConstant(str).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(token);
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00192108 File Offset: 0x00191308
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			int localIndex = local.GetLocalIndex();
			if (local.GetMethodBuilder() != this.m_methodBuilder)
			{
				throw new ArgumentException(SR.Argument_UnmatchedMethodForLocal, "local");
			}
			if (opcode.Equals(OpCodes.Ldloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Ldloc_0;
					break;
				case 1:
					opcode = OpCodes.Ldloc_1;
					break;
				case 2:
					opcode = OpCodes.Ldloc_2;
					break;
				case 3:
					opcode = OpCodes.Ldloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Ldloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Stloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Stloc_0;
					break;
				case 1:
					opcode = OpCodes.Stloc_1;
					break;
				case 2:
					opcode = OpCodes.Stloc_2;
					break;
				case 3:
					opcode = OpCodes.Stloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Stloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= 255)
			{
				opcode = OpCodes.Ldloca_S;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.OperandType == OperandType.InlineNone)
			{
				return;
			}
			if (!OpCodes.TakesSingleByteArgument(opcode))
			{
				BinaryPrimitives.WriteInt16LittleEndian(this.m_ILStream.AsSpan(this.m_length), (short)localIndex);
				this.m_length += 2;
				return;
			}
			if (localIndex > 255)
			{
				throw new InvalidOperationException(SR.InvalidOperation_BadInstructionOrIndexOutOfBound);
			}
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)localIndex;
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x001922A4 File Offset: 0x001914A4
		public virtual Label BeginExceptionBlock()
		{
			if (this.m_exceptions == null)
			{
				this.m_exceptions = new __ExceptionInfo[2];
			}
			if (this.m_currExcStack == null)
			{
				this.m_currExcStack = new __ExceptionInfo[2];
			}
			if (this.m_exceptionCount >= this.m_exceptions.Length)
			{
				this.m_exceptions = ILGenerator.EnlargeArray<__ExceptionInfo>(this.m_exceptions);
			}
			if (this.m_currExcStackCount >= this.m_currExcStack.Length)
			{
				this.m_currExcStack = ILGenerator.EnlargeArray<__ExceptionInfo>(this.m_currExcStack);
			}
			Label label = this.DefineLabel();
			__ExceptionInfo _ExceptionInfo = new __ExceptionInfo(this.m_length, label);
			__ExceptionInfo[] exceptions = this.m_exceptions;
			int num = this.m_exceptionCount;
			this.m_exceptionCount = num + 1;
			exceptions[num] = _ExceptionInfo;
			__ExceptionInfo[] currExcStack = this.m_currExcStack;
			num = this.m_currExcStackCount;
			this.m_currExcStackCount = num + 1;
			currExcStack[num] = _ExceptionInfo;
			return label;
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00192364 File Offset: 0x00191564
		public virtual void EndExceptionBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			__ExceptionInfo[] currExcStack = this.m_currExcStack;
			int num = this.m_currExcStackCount - 1;
			this.m_currExcStackCount = num;
			currExcStack[num] = null;
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int currentState = _ExceptionInfo.GetCurrentState();
			if (currentState == 1 || currentState == 0)
			{
				throw new InvalidOperationException(SR.Argument_BadExceptionCodeGen);
			}
			if (currentState == 2)
			{
				this.Emit(OpCodes.Leave, endLabel);
			}
			else if (currentState == 3 || currentState == 4)
			{
				this.Emit(OpCodes.Endfinally);
			}
			Label loc = (this.m_labelList[endLabel.GetLabelValue()] != -1) ? _ExceptionInfo.m_finallyEndLabel : endLabel;
			this.MarkLabel(loc);
			_ExceptionInfo.Done(this.m_length);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x00192424 File Offset: 0x00191624
		public virtual void BeginExceptFilterBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			this.Emit(OpCodes.Leave, _ExceptionInfo.GetEndLabel());
			_ExceptionInfo.MarkFilterAddr(this.m_length);
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00192474 File Offset: 0x00191674
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(SR.Argument_ShouldNotSpecifyExceptionType);
				}
				this.Emit(OpCodes.Endfilter);
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				this.Emit(OpCodes.Leave, _ExceptionInfo.GetEndLabel());
			}
			_ExceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00192500 File Offset: 0x00191700
		public virtual void BeginFaultBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			this.Emit(OpCodes.Leave, _ExceptionInfo.GetEndLabel());
			_ExceptionInfo.MarkFaultAddr(this.m_length);
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x00192550 File Offset: 0x00191750
		public virtual void BeginFinallyBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(SR.Argument_NotInExceptionBlock);
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			int currentState = _ExceptionInfo.GetCurrentState();
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int num = 0;
			if (currentState != 0)
			{
				this.Emit(OpCodes.Leave, endLabel);
				num = this.m_length;
			}
			this.MarkLabel(endLabel);
			Label label = this.DefineLabel();
			_ExceptionInfo.SetFinallyEndLabel(label);
			this.Emit(OpCodes.Leave, label);
			if (num == 0)
			{
				num = this.m_length;
			}
			_ExceptionInfo.MarkFinallyAddr(this.m_length, num);
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x001925E4 File Offset: 0x001917E4
		public virtual Label DefineLabel()
		{
			if (this.m_labelList == null)
			{
				this.m_labelList = new int[4];
			}
			if (this.m_labelCount >= this.m_labelList.Length)
			{
				this.m_labelList = ILGenerator.EnlargeArray<int>(this.m_labelList);
			}
			this.m_labelList[this.m_labelCount] = -1;
			int labelCount = this.m_labelCount;
			this.m_labelCount = labelCount + 1;
			return new Label(labelCount);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0019264C File Offset: 0x0019184C
		public virtual void MarkLabel(Label loc)
		{
			int labelValue = loc.GetLabelValue();
			if (this.m_labelList == null || labelValue < 0 || labelValue >= this.m_labelList.Length)
			{
				throw new ArgumentException(SR.Argument_InvalidLabel);
			}
			if (this.m_labelList[labelValue] != -1)
			{
				throw new ArgumentException(SR.Argument_RedefinedLabel);
			}
			this.m_labelList[labelValue] = this.m_length;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x001926A8 File Offset: 0x001918A8
		public virtual void ThrowException([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!excType.IsSubclassOf(typeof(Exception)) && excType != typeof(Exception))
			{
				throw new ArgumentException(SR.Argument_NotExceptionType);
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException(SR.Argument_MissingDefaultConstructor);
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00192730 File Offset: 0x00191930
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			Type[] types = new Type[]
			{
				typeof(string)
			};
			Type type = Type.GetType("System.Console, System.Console", true);
			MethodInfo method = type.GetMethod("WriteLine", types);
			this.Emit(OpCodes.Call, method);
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00192784 File Offset: 0x00191984
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (this.m_methodBuilder == null)
			{
				throw new ArgumentException(SR.InvalidOperation_BadILGeneratorUsage);
			}
			Type type = Type.GetType("System.Console, System.Console", true);
			MethodInfo method = type.GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			this.Emit(OpCodes.Ldloc, localBuilder);
			Type[] array = new Type[1];
			Type localType = localBuilder.LocalType;
			if (localType is TypeBuilder || localType is EnumBuilder)
			{
				throw new ArgumentException(SR.NotSupported_OutputStreamUsingTypeBuilder);
			}
			array[0] = localType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(SR.Argument_EmitWriteLineType, "localBuilder");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00192848 File Offset: 0x00191A48
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			Type type = Type.GetType("System.Console, System.Console", true);
			MethodInfo method = type.GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg, 0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			Type[] array = new Type[1];
			Type fieldType = fld.FieldType;
			if (fieldType is TypeBuilder || fieldType is EnumBuilder)
			{
				throw new NotSupportedException(SR.NotSupported_OutputStreamUsingTypeBuilder);
			}
			array[0] = fieldType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(SR.Argument_EmitWriteLineType, "fld");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x00192929 File Offset: 0x00191B29
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x00192934 File Offset: 0x00191B34
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(SR.InvalidOperation_TypeHasBeenCreated);
			}
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (methodBuilder.m_bIsBaked)
			{
				throw new InvalidOperationException(SR.InvalidOperation_MethodBaked);
			}
			this.m_localSignature.AddArgument(localType, pinned);
			int localCount = this.m_localCount;
			this.m_localCount = localCount + 1;
			return new LocalBuilder(localCount, localType, methodBuilder, pinned);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001929BC File Offset: 0x00191BBC
		public virtual void UsingNamespace(string usingNamespace)
		{
			if (usingNamespace == null)
			{
				throw new ArgumentNullException("usingNamespace");
			}
			if (usingNamespace.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "usingNamespace");
			}
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
				return;
			}
			this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00192A38 File Offset: 0x00191C38
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine == 0 || startLine < 0 || endLine == 0 || endLine < 0)
			{
				throw new ArgumentOutOfRangeException("startLine");
			}
			this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00192A6D File Offset: 0x00191C6D
		public virtual void BeginScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00192A81 File Offset: 0x00191C81
		public virtual void EndScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06005063 RID: 20579 RVA: 0x00192A95 File Offset: 0x00191C95
		public virtual int ILOffset
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x0400145F RID: 5215
		private int m_length;

		// Token: 0x04001460 RID: 5216
		private byte[] m_ILStream;

		// Token: 0x04001461 RID: 5217
		private int[] m_labelList;

		// Token: 0x04001462 RID: 5218
		private int m_labelCount;

		// Token: 0x04001463 RID: 5219
		private __FixupData[] m_fixupData;

		// Token: 0x04001464 RID: 5220
		private int m_fixupCount;

		// Token: 0x04001465 RID: 5221
		private int[] m_RelocFixupList;

		// Token: 0x04001466 RID: 5222
		private int m_RelocFixupCount;

		// Token: 0x04001467 RID: 5223
		private int m_exceptionCount;

		// Token: 0x04001468 RID: 5224
		private int m_currExcStackCount;

		// Token: 0x04001469 RID: 5225
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x0400146A RID: 5226
		private __ExceptionInfo[] m_currExcStack;

		// Token: 0x0400146B RID: 5227
		internal ScopeTree m_ScopeTree;

		// Token: 0x0400146C RID: 5228
		internal LineNumberInfo m_LineNumberInfo;

		// Token: 0x0400146D RID: 5229
		internal MethodInfo m_methodBuilder;

		// Token: 0x0400146E RID: 5230
		internal int m_localCount;

		// Token: 0x0400146F RID: 5231
		internal SignatureHelper m_localSignature;

		// Token: 0x04001470 RID: 5232
		private int m_maxStackSize;

		// Token: 0x04001471 RID: 5233
		private int m_maxMidStack;

		// Token: 0x04001472 RID: 5234
		private int m_maxMidStackCur;
	}
}
