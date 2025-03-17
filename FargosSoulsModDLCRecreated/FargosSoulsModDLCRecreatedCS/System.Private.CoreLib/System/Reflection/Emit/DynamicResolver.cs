using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000618 RID: 1560
	internal class DynamicResolver : Resolver
	{
		// Token: 0x06004F0B RID: 20235 RVA: 0x0018F3FC File Offset: 0x0018E5FC
		internal DynamicResolver(DynamicILGenerator ilGenerator)
		{
			this.m_stackSize = ilGenerator.GetMaxStackSize();
			this.m_exceptions = ilGenerator.GetExceptions();
			this.m_code = ilGenerator.BakeByteArray();
			this.m_localSignature = ilGenerator.m_localSignature.InternalGetSignatureArray();
			this.m_scope = ilGenerator.m_scope;
			this.m_method = (DynamicMethod)ilGenerator.m_methodBuilder;
			this.m_method.m_resolver = this;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0018F470 File Offset: 0x0018E670
		internal DynamicResolver(DynamicILInfo dynamicILInfo)
		{
			this.m_stackSize = dynamicILInfo.MaxStackSize;
			this.m_code = dynamicILInfo.Code;
			this.m_localSignature = dynamicILInfo.LocalSignature;
			this.m_exceptionHeader = dynamicILInfo.Exceptions;
			this.m_scope = dynamicILInfo.DynamicScope;
			this.m_method = dynamicILInfo.DynamicMethod;
			this.m_method.m_resolver = this;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0018F4D8 File Offset: 0x0018E6D8
		protected override void Finalize()
		{
			try
			{
				DynamicMethod method = this.m_method;
				if (!(method == null))
				{
					if (method.m_methodHandle != null)
					{
						DynamicResolver.DestroyScout destroyScout;
						try
						{
							destroyScout = new DynamicResolver.DestroyScout();
						}
						catch
						{
							GC.ReRegisterForFinalize(this);
							return;
						}
						destroyScout.m_methodHandle = method.m_methodHandle.Value;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x0018F54C File Offset: 0x0018E74C
		internal override RuntimeType GetJitContext(out int securityControlFlags)
		{
			DynamicResolver.SecurityControlFlags securityControlFlags2 = DynamicResolver.SecurityControlFlags.Default;
			if (this.m_method.m_restrictedSkipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.RestrictedSkipVisibilityChecks;
			}
			else if (this.m_method.m_skipVisibility)
			{
				securityControlFlags2 |= DynamicResolver.SecurityControlFlags.SkipVisibilityChecks;
			}
			RuntimeType typeOwner = this.m_method.m_typeOwner;
			securityControlFlags = (int)securityControlFlags2;
			return typeOwner;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x0018F590 File Offset: 0x0018E790
		private static int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x0018F5C0 File Offset: 0x0018E7C0
		internal override byte[] GetCodeInfo(out int stackSize, out int initLocals, out int EHCount)
		{
			stackSize = this.m_stackSize;
			if (this.m_exceptionHeader != null && this.m_exceptionHeader.Length != 0)
			{
				if (this.m_exceptionHeader.Length < 4)
				{
					throw new FormatException();
				}
				byte b = this.m_exceptionHeader[0];
				if ((b & 64) != 0)
				{
					int num = (int)this.m_exceptionHeader[3] << 16;
					num |= (int)this.m_exceptionHeader[2] << 8;
					num |= (int)this.m_exceptionHeader[1];
					EHCount = (num - 4) / 24;
				}
				else
				{
					EHCount = (int)((this.m_exceptionHeader[1] - 2) / 12);
				}
			}
			else
			{
				EHCount = DynamicResolver.CalculateNumberOfExceptions(this.m_exceptions);
			}
			initLocals = (this.m_method.InitLocals ? 1 : 0);
			return this.m_code;
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x0018F66B File Offset: 0x0018E86B
		internal override byte[] GetLocalsSignature()
		{
			return this.m_localSignature;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0018F673 File Offset: 0x0018E873
		internal override byte[] GetRawEHInfo()
		{
			return this.m_exceptionHeader;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0018F67C File Offset: 0x0018E87C
		internal unsafe override void GetEHInfo(int excNumber, void* exc)
		{
			for (int i = 0; i < this.m_exceptions.Length; i++)
			{
				int numberOfCatches = this.m_exceptions[i].GetNumberOfCatches();
				if (excNumber < numberOfCatches)
				{
					((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags = this.m_exceptions[i].GetExceptionTypes()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset = this.m_exceptions[i].GetStartAddress();
					if ((((Resolver.CORINFO_EH_CLAUSE*)exc)->Flags & 2) != 2)
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					else
					{
						((Resolver.CORINFO_EH_CLAUSE*)exc)->TryLength = this.m_exceptions[i].GetFinallyEndAddress() - ((Resolver.CORINFO_EH_CLAUSE*)exc)->TryOffset;
					}
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset = this.m_exceptions[i].GetCatchAddresses()[excNumber];
					((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerLength = this.m_exceptions[i].GetCatchEndAddresses()[excNumber] - ((Resolver.CORINFO_EH_CLAUSE*)exc)->HandlerOffset;
					((Resolver.CORINFO_EH_CLAUSE*)exc)->ClassTokenOrFilterOffset = this.m_exceptions[i].GetFilterAddresses()[excNumber];
					return;
				}
				excNumber -= numberOfCatches;
			}
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0018F76E File Offset: 0x0018E96E
		internal override string GetStringLiteral(int token)
		{
			return this.m_scope.GetString(token);
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x0018F77C File Offset: 0x0018E97C
		internal override void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle)
		{
			typeHandle = 0;
			methodHandle = 0;
			fieldHandle = 0;
			object obj = this.m_scope[token];
			if (obj == null)
			{
				throw new InvalidProgramException();
			}
			if (obj is RuntimeTypeHandle)
			{
				typeHandle = ((RuntimeTypeHandle)obj).Value;
				return;
			}
			if (obj is RuntimeMethodHandle)
			{
				methodHandle = ((RuntimeMethodHandle)obj).Value;
				return;
			}
			if (obj is RuntimeFieldHandle)
			{
				fieldHandle = ((RuntimeFieldHandle)obj).Value;
				return;
			}
			DynamicMethod dynamicMethod = obj as DynamicMethod;
			if (dynamicMethod != null)
			{
				methodHandle = dynamicMethod.GetMethodDescriptor().Value;
				return;
			}
			GenericMethodInfo genericMethodInfo = obj as GenericMethodInfo;
			if (genericMethodInfo != null)
			{
				methodHandle = genericMethodInfo.m_methodHandle.Value;
				typeHandle = genericMethodInfo.m_context.Value;
				return;
			}
			GenericFieldInfo genericFieldInfo = obj as GenericFieldInfo;
			if (genericFieldInfo != null)
			{
				fieldHandle = genericFieldInfo.m_fieldHandle.Value;
				typeHandle = genericFieldInfo.m_context.Value;
				return;
			}
			VarArgMethod varArgMethod = obj as VarArgMethod;
			if (varArgMethod == null)
			{
				return;
			}
			if (varArgMethod.m_dynamicMethod == null)
			{
				methodHandle = varArgMethod.m_method.MethodHandle.Value;
				typeHandle = varArgMethod.m_method.GetDeclaringTypeInternal().GetTypeHandleInternal().Value;
				return;
			}
			methodHandle = varArgMethod.m_dynamicMethod.GetMethodDescriptor().Value;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0018F8D8 File Offset: 0x0018EAD8
		internal override byte[] ResolveSignature(int token, int fromMethod)
		{
			return this.m_scope.ResolveSignature(token, fromMethod);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x0018F8E7 File Offset: 0x0018EAE7
		internal override MethodInfo GetDynamicMethod()
		{
			return this.m_method.GetMethodInfo();
		}

		// Token: 0x04001421 RID: 5153
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001422 RID: 5154
		private byte[] m_exceptionHeader;

		// Token: 0x04001423 RID: 5155
		private DynamicMethod m_method;

		// Token: 0x04001424 RID: 5156
		private byte[] m_code;

		// Token: 0x04001425 RID: 5157
		private byte[] m_localSignature;

		// Token: 0x04001426 RID: 5158
		private int m_stackSize;

		// Token: 0x04001427 RID: 5159
		private DynamicScope m_scope;

		// Token: 0x02000619 RID: 1561
		private class DestroyScout
		{
			// Token: 0x06004F18 RID: 20248 RVA: 0x0018F8F4 File Offset: 0x0018EAF4
			~DestroyScout()
			{
				if (!this.m_methodHandle.IsNullHandle())
				{
					if (RuntimeMethodHandle.GetResolver(this.m_methodHandle) != null)
					{
						GC.ReRegisterForFinalize(this);
					}
					else
					{
						RuntimeMethodHandle.Destroy(this.m_methodHandle);
					}
				}
			}

			// Token: 0x04001428 RID: 5160
			internal RuntimeMethodHandleInternal m_methodHandle;
		}

		// Token: 0x0200061A RID: 1562
		[Flags]
		internal enum SecurityControlFlags
		{
			// Token: 0x0400142A RID: 5162
			Default = 0,
			// Token: 0x0400142B RID: 5163
			SkipVisibilityChecks = 1,
			// Token: 0x0400142C RID: 5164
			RestrictedSkipVisibilityChecks = 2,
			// Token: 0x0400142D RID: 5165
			HasCreationContext = 4,
			// Token: 0x0400142E RID: 5166
			CanSkipCSEvaluation = 8
		}
	}
}
