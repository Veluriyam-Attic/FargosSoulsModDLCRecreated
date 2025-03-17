using System;
using System.Collections.Generic;

namespace System.Reflection.Emit
{
	// Token: 0x0200061C RID: 1564
	internal class DynamicScope
	{
		// Token: 0x17000CEB RID: 3307
		internal object this[int token]
		{
			get
			{
				token &= 16777215;
				if (token < 0 || token > this.m_tokens.Count)
				{
					return null;
				}
				return this.m_tokens[token];
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x0018FC13 File Offset: 0x0018EE13
		internal int GetTokenFor(VarArgMethod varArgMethod)
		{
			this.m_tokens.Add(varArgMethod);
			return this.m_tokens.Count - 1 | 167772160;
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x0018FC34 File Offset: 0x0018EE34
		internal string GetString(int token)
		{
			return this[token] as string;
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x0018FC44 File Offset: 0x0018EE44
		internal byte[] ResolveSignature(int token, int fromMethod)
		{
			if (fromMethod == 0)
			{
				return (byte[])this[token];
			}
			VarArgMethod varArgMethod = this[token] as VarArgMethod;
			if (varArgMethod == null)
			{
				return null;
			}
			return varArgMethod.m_signature.GetSignature(true);
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x0018FC80 File Offset: 0x0018EE80
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
			if (methodInfo != null)
			{
				RuntimeMethodHandleInternal value = methodInfo.Value;
				if (!RuntimeMethodHandle.IsDynamicMethod(value))
				{
					RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(value);
					if (declaringType != null && RuntimeTypeHandle.HasInstantiation(declaringType))
					{
						MethodBase methodBase = RuntimeType.GetMethodBase(methodInfo);
						Type genericTypeDefinition = methodBase.DeclaringType.GetGenericTypeDefinition();
						throw new ArgumentException(SR.Format(SR.Argument_MethodDeclaringTypeGenericLcg, methodBase, genericTypeDefinition));
					}
				}
			}
			this.m_tokens.Add(method);
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x0018FD0A File Offset: 0x0018EF0A
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericMethodInfo(method, typeContext));
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x0018FD31 File Offset: 0x0018EF31
		public int GetTokenFor(DynamicMethod method)
		{
			this.m_tokens.Add(method);
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0018FD52 File Offset: 0x0018EF52
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			this.m_tokens.Add(field);
			return this.m_tokens.Count - 1 | 67108864;
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x0018FD78 File Offset: 0x0018EF78
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericFieldInfo(field, typeContext));
			return this.m_tokens.Count - 1 | 67108864;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x0018FD9F File Offset: 0x0018EF9F
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			this.m_tokens.Add(type);
			return this.m_tokens.Count - 1 | 33554432;
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x0018FDC5 File Offset: 0x0018EFC5
		public int GetTokenFor(string literal)
		{
			this.m_tokens.Add(literal);
			return this.m_tokens.Count - 1 | 1879048192;
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0018FDE6 File Offset: 0x0018EFE6
		public int GetTokenFor(byte[] signature)
		{
			this.m_tokens.Add(signature);
			return this.m_tokens.Count - 1 | 285212672;
		}

		// Token: 0x04001436 RID: 5174
		internal readonly List<object> m_tokens = new List<object>
		{
			null
		};
	}
}
