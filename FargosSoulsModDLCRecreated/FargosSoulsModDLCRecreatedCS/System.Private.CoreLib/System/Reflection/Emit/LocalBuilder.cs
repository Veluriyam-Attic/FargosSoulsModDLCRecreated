using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000646 RID: 1606
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class LocalBuilder : LocalVariableInfo
	{
		// Token: 0x060050BD RID: 20669 RVA: 0x001936EA File Offset: 0x001928EA
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder) : this(localIndex, localType, methodBuilder, false)
		{
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x001936F6 File Offset: 0x001928F6
		internal LocalBuilder(int localIndex, Type localType, MethodInfo methodBuilder, bool isPinned)
		{
			this.m_isPinned = isPinned;
			this.m_localIndex = localIndex;
			this.m_localType = localType;
			this.m_methodBuilder = methodBuilder;
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0019371B File Offset: 0x0019291B
		internal int GetLocalIndex()
		{
			return this.m_localIndex;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00193723 File Offset: 0x00192923
		internal MethodInfo GetMethodBuilder()
		{
			return this.m_methodBuilder;
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x0019372B File Offset: 0x0019292B
		public override bool IsPinned
		{
			get
			{
				return this.m_isPinned;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x00193733 File Offset: 0x00192933
		public override Type LocalType
		{
			get
			{
				return this.m_localType;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x0019371B File Offset: 0x0019291B
		public override int LocalIndex
		{
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0019373B File Offset: 0x0019293B
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x00193748 File Offset: 0x00192948
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)methodBuilder.Module;
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(SR.InvalidOperation_TypeHasBeenCreated);
			}
			if (moduleBuilder.GetSymWriter() == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotADebugModule);
			}
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(moduleBuilder);
			fieldSigHelper.AddArgument(this.m_localType);
			int num;
			byte[] src = fieldSigHelper.InternalGetSignature(out num);
			byte[] array = new byte[num - 1];
			Buffer.BlockCopy(src, 1, array, 0, num - 1);
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddLocalSymInfo(name, array, this.m_localIndex, startOffset, endOffset);
				return;
			}
			methodBuilder.GetILGenerator().m_ScopeTree.AddLocalSymInfoToCurrentScope(name, array, this.m_localIndex, startOffset, endOffset);
		}

		// Token: 0x040014B8 RID: 5304
		private int m_localIndex;

		// Token: 0x040014B9 RID: 5305
		private Type m_localType;

		// Token: 0x040014BA RID: 5306
		private MethodInfo m_methodBuilder;

		// Token: 0x040014BB RID: 5307
		private bool m_isPinned;
	}
}
