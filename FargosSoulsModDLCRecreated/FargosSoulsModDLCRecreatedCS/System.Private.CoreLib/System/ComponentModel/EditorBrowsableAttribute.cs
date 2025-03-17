using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	// Token: 0x0200023F RID: 575
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		// Token: 0x060023E8 RID: 9192 RVA: 0x00138B74 File Offset: 0x00137D74
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.State = state;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00138B83 File Offset: 0x00137D83
		public EditorBrowsableAttribute() : this(EditorBrowsableState.Always)
		{
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x00138B8C File Offset: 0x00137D8C
		public EditorBrowsableState State { get; }

		// Token: 0x060023EB RID: 9195 RVA: 0x00138B94 File Offset: 0x00137D94
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorBrowsableAttribute editorBrowsableAttribute = obj as EditorBrowsableAttribute;
			return editorBrowsableAttribute != null && editorBrowsableAttribute.State == this.State;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00138ACA File Offset: 0x00137CCA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
