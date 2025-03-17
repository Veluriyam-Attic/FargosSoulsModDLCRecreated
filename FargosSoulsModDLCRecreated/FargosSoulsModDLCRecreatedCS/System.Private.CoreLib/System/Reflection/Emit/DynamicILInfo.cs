using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200061B RID: 1563
	[NullableContext(1)]
	[Nullable(0)]
	public class DynamicILInfo
	{
		// Token: 0x06004F1A RID: 20250 RVA: 0x0018F94C File Offset: 0x0018EB4C
		internal DynamicILInfo(DynamicMethod method, byte[] methodSignature)
		{
			this.m_scope = new DynamicScope();
			this.m_method = method;
			this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
			this.m_exceptions = Array.Empty<byte>();
			this.m_code = Array.Empty<byte>();
			this.m_localSignature = Array.Empty<byte>();
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0018F9A4 File Offset: 0x0018EBA4
		internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
		{
			dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_method.Name, (byte[])this.m_scope[this.m_methodSignature], new DynamicResolver(this));
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0018F9DC File Offset: 0x0018EBDC
		internal byte[] LocalSignature
		{
			get
			{
				byte[] result;
				if ((result = this.m_localSignature) == null)
				{
					result = (this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray());
				}
				return result;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x0018FA06 File Offset: 0x0018EC06
		internal byte[] Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0018FA0E File Offset: 0x0018EC0E
		internal byte[] Code
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0018FA16 File Offset: 0x0018EC16
		internal int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x0018FA1E File Offset: 0x0018EC1E
		public DynamicMethod DynamicMethod
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0018FA26 File Offset: 0x0018EC26
		internal DynamicScope DynamicScope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0018FA2E File Offset: 0x0018EC2E
		[NullableContext(2)]
		public void SetCode(byte[] code, int maxStackSize)
		{
			this.m_code = ((code != null) ? ((byte[])code.Clone()) : Array.Empty<byte>());
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x0018FA54 File Offset: 0x0018EC54
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
		{
			if (codeSize < 0)
			{
				throw new ArgumentOutOfRangeException("codeSize", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (codeSize > 0 && code == null)
			{
				throw new ArgumentNullException("code");
			}
			this.m_code = new Span<byte>((void*)code, codeSize).ToArray();
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x0018FAA5 File Offset: 0x0018ECA5
		[NullableContext(2)]
		public void SetExceptions(byte[] exceptions)
		{
			this.m_exceptions = ((exceptions != null) ? ((byte[])exceptions.Clone()) : Array.Empty<byte>());
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x0018FAC4 File Offset: 0x0018ECC4
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
		{
			if (exceptionsSize < 0)
			{
				throw new ArgumentOutOfRangeException("exceptionsSize", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (exceptionsSize > 0 && exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			this.m_exceptions = new Span<byte>((void*)exceptions, exceptionsSize).ToArray();
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x0018FB0E File Offset: 0x0018ED0E
		[NullableContext(2)]
		public void SetLocalSignature(byte[] localSignature)
		{
			this.m_localSignature = ((localSignature != null) ? ((byte[])localSignature.Clone()) : Array.Empty<byte>());
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0018FB2C File Offset: 0x0018ED2C
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
		{
			if (signatureSize < 0)
			{
				throw new ArgumentOutOfRangeException("signatureSize", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (signatureSize > 0 && localSignature == null)
			{
				throw new ArgumentNullException("localSignature");
			}
			this.m_localSignature = new Span<byte>((void*)localSignature, signatureSize).ToArray();
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0018FB76 File Offset: 0x0018ED76
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x0018FB84 File Offset: 0x0018ED84
		public int GetTokenFor(DynamicMethod method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x0018FB92 File Offset: 0x0018ED92
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(method, contextType);
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0018FBA1 File Offset: 0x0018EDA1
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.DynamicScope.GetTokenFor(field);
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x0018FBAF File Offset: 0x0018EDAF
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(field, contextType);
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0018FBBE File Offset: 0x0018EDBE
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			return this.DynamicScope.GetTokenFor(type);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x0018FBCC File Offset: 0x0018EDCC
		public int GetTokenFor(string literal)
		{
			return this.DynamicScope.GetTokenFor(literal);
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x0018FBDA File Offset: 0x0018EDDA
		public int GetTokenFor(byte[] signature)
		{
			return this.DynamicScope.GetTokenFor(signature);
		}

		// Token: 0x0400142F RID: 5167
		private DynamicMethod m_method;

		// Token: 0x04001430 RID: 5168
		private DynamicScope m_scope;

		// Token: 0x04001431 RID: 5169
		private byte[] m_exceptions;

		// Token: 0x04001432 RID: 5170
		private byte[] m_code;

		// Token: 0x04001433 RID: 5171
		private byte[] m_localSignature;

		// Token: 0x04001434 RID: 5172
		private int m_maxStackSize;

		// Token: 0x04001435 RID: 5173
		private int m_methodSignature;
	}
}
