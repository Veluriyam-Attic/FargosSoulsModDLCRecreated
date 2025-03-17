using System;

namespace System.Diagnostics
{
	// Token: 0x020006CF RID: 1743
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	public sealed class DebuggableAttribute : Attribute
	{
		// Token: 0x060058A8 RID: 22696 RVA: 0x001B0DB1 File Offset: 0x001AFFB1
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.DebuggingFlags = 0;
			if (isJITTrackingEnabled)
			{
				this.DebuggingFlags |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.DebuggingFlags |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x001B0DE6 File Offset: 0x001AFFE6
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.DebuggingFlags = modes;
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x001B0DF5 File Offset: 0x001AFFF5
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.DebuggingFlags & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x001B0E02 File Offset: 0x001B0002
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.DebuggingFlags & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060058AC RID: 22700 RVA: 0x001B0E13 File Offset: 0x001B0013
		public DebuggableAttribute.DebuggingModes DebuggingFlags { get; }

		// Token: 0x020006D0 RID: 1744
		[Flags]
		public enum DebuggingModes
		{
			// Token: 0x04001969 RID: 6505
			None = 0,
			// Token: 0x0400196A RID: 6506
			Default = 1,
			// Token: 0x0400196B RID: 6507
			DisableOptimizations = 256,
			// Token: 0x0400196C RID: 6508
			IgnoreSymbolStoreSequencePoints = 2,
			// Token: 0x0400196D RID: 6509
			EnableEditAndContinue = 4
		}
	}
}
