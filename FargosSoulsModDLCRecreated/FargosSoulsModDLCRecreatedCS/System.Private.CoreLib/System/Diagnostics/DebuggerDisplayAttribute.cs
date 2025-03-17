using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006D3 RID: 1747
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		// Token: 0x060058AF RID: 22703 RVA: 0x001B0E45 File Offset: 0x001B0045
		public DebuggerDisplayAttribute(string value)
		{
			this.Value = (value ?? "");
			this.Name = "";
			this.Type = "";
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060058B0 RID: 22704 RVA: 0x001B0E73 File Offset: 0x001B0073
		[Nullable(1)]
		public string Value { [NullableContext(1)] get; }

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060058B1 RID: 22705 RVA: 0x001B0E7B File Offset: 0x001B007B
		// (set) Token: 0x060058B2 RID: 22706 RVA: 0x001B0E83 File Offset: 0x001B0083
		public string Name { get; set; }

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060058B3 RID: 22707 RVA: 0x001B0E8C File Offset: 0x001B008C
		// (set) Token: 0x060058B4 RID: 22708 RVA: 0x001B0E94 File Offset: 0x001B0094
		public string Type { get; set; }

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x001B0E9D File Offset: 0x001B009D
		// (set) Token: 0x060058B6 RID: 22710 RVA: 0x001B0EA5 File Offset: 0x001B00A5
		public Type Target
		{
			get
			{
				return this._target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.TargetTypeName = value.AssemblyQualifiedName;
				this._target = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x001B0ECE File Offset: 0x001B00CE
		// (set) Token: 0x060058B8 RID: 22712 RVA: 0x001B0ED6 File Offset: 0x001B00D6
		public string TargetTypeName { get; set; }

		// Token: 0x04001973 RID: 6515
		private Type _target;
	}
}
