using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006D8 RID: 1752
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		// Token: 0x060058BD RID: 22717 RVA: 0x001B0EDF File Offset: 0x001B00DF
		[NullableContext(1)]
		public DebuggerTypeProxyAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.ProxyTypeName = type.AssemblyQualifiedName;
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001B0F07 File Offset: 0x001B0107
		[NullableContext(1)]
		public DebuggerTypeProxyAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] string typeName)
		{
			this.ProxyTypeName = typeName;
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x060058BF RID: 22719 RVA: 0x001B0F16 File Offset: 0x001B0116
		[Nullable(1)]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public string ProxyTypeName { [NullableContext(1)] get; }

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x001B0F1E File Offset: 0x001B011E
		// (set) Token: 0x060058C1 RID: 22721 RVA: 0x001B0F26 File Offset: 0x001B0126
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

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x001B0F4F File Offset: 0x001B014F
		// (set) Token: 0x060058C3 RID: 22723 RVA: 0x001B0F57 File Offset: 0x001B0157
		public string TargetTypeName { get; set; }

		// Token: 0x04001978 RID: 6520
		private Type _target;
	}
}
