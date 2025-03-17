using System;

namespace System.Reflection
{
	// Token: 0x020005C1 RID: 1473
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		// Token: 0x06004BDC RID: 19420 RVA: 0x0018B199 File Offset: 0x0018A399
		[CLSCompliant(false)]
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(uint flags)
		{
			this._flags = (AssemblyNameFlags)flags;
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x0018B1A8 File Offset: 0x0018A3A8
		[Obsolete("This property has been deprecated. Please use AssemblyFlags instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public uint Flags
		{
			get
			{
				return (uint)this._flags;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0018B1A8 File Offset: 0x0018A3A8
		public int AssemblyFlags
		{
			get
			{
				return (int)this._flags;
			}
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0018B199 File Offset: 0x0018A399
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this._flags = (AssemblyNameFlags)assemblyFlags;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0018B199 File Offset: 0x0018A399
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this._flags = assemblyFlags;
		}

		// Token: 0x040012DD RID: 4829
		private readonly AssemblyNameFlags _flags;
	}
}
