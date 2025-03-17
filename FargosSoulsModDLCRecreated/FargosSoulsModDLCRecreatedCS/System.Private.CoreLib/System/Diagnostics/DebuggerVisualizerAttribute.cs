using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006D9 RID: 1753
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		// Token: 0x060058C4 RID: 22724 RVA: 0x001B0F60 File Offset: 0x001B0160
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.VisualizerTypeName = visualizerTypeName;
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x001B0F6F File Offset: 0x001B016F
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(string visualizerTypeName, [Nullable(2)] string visualizerObjectSourceTypeName)
		{
			this.VisualizerTypeName = visualizerTypeName;
			this.VisualizerObjectSourceTypeName = visualizerObjectSourceTypeName;
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x001B0F85 File Offset: 0x001B0185
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.VisualizerTypeName = visualizerTypeName;
			this.VisualizerObjectSourceTypeName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x001B0FB4 File Offset: 0x001B01B4
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.VisualizerTypeName = visualizer.AssemblyQualifiedName;
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x001B0FDC File Offset: 0x001B01DC
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.VisualizerTypeName = visualizer.AssemblyQualifiedName;
			this.VisualizerObjectSourceTypeName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x001B102F File Offset: 0x001B022F
		[NullableContext(1)]
		public DebuggerVisualizerAttribute(Type visualizer, [Nullable(2)] string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.VisualizerTypeName = visualizer.AssemblyQualifiedName;
			this.VisualizerObjectSourceTypeName = visualizerObjectSourceTypeName;
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060058CA RID: 22730 RVA: 0x001B105E File Offset: 0x001B025E
		public string VisualizerObjectSourceTypeName { get; }

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060058CB RID: 22731 RVA: 0x001B1066 File Offset: 0x001B0266
		[Nullable(1)]
		public string VisualizerTypeName { [NullableContext(1)] get; }

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x001B106E File Offset: 0x001B026E
		// (set) Token: 0x060058CD RID: 22733 RVA: 0x001B1076 File Offset: 0x001B0276
		public string Description { get; set; }

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x001B107F File Offset: 0x001B027F
		// (set) Token: 0x060058CF RID: 22735 RVA: 0x001B1087 File Offset: 0x001B0287
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

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x001B10B0 File Offset: 0x001B02B0
		// (set) Token: 0x060058D1 RID: 22737 RVA: 0x001B10B8 File Offset: 0x001B02B8
		public string TargetTypeName { get; set; }

		// Token: 0x0400197B RID: 6523
		private Type _target;
	}
}
