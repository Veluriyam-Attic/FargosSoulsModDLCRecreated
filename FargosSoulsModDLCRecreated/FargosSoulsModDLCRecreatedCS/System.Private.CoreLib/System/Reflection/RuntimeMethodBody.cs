using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005AF RID: 1455
	internal sealed class RuntimeMethodBody : MethodBody
	{
		// Token: 0x06004B0E RID: 19214 RVA: 0x001887D9 File Offset: 0x001879D9
		private RuntimeMethodBody()
		{
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x001887E1 File Offset: 0x001879E1
		public override int LocalSignatureMetadataToken
		{
			get
			{
				return this._localSignatureMetadataToken;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x001887E9 File Offset: 0x001879E9
		public override IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this._localVariables);
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x001887F6 File Offset: 0x001879F6
		public override int MaxStackSize
		{
			get
			{
				return this._maxStackSize;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x001887FE File Offset: 0x001879FE
		public override bool InitLocals
		{
			get
			{
				return this._initLocals;
			}
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x00188806 File Offset: 0x00187A06
		public override byte[] GetILAsByteArray()
		{
			return this._IL;
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x0018880E File Offset: 0x00187A0E
		public override IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this._exceptionHandlingClauses);
			}
		}

		// Token: 0x0400129F RID: 4767
		private byte[] _IL;

		// Token: 0x040012A0 RID: 4768
		private ExceptionHandlingClause[] _exceptionHandlingClauses;

		// Token: 0x040012A1 RID: 4769
		private LocalVariableInfo[] _localVariables;

		// Token: 0x040012A2 RID: 4770
		internal MethodBase _methodBase;

		// Token: 0x040012A3 RID: 4771
		private int _localSignatureMetadataToken;

		// Token: 0x040012A4 RID: 4772
		private int _maxStackSize;

		// Token: 0x040012A5 RID: 4773
		private bool _initLocals;
	}
}
