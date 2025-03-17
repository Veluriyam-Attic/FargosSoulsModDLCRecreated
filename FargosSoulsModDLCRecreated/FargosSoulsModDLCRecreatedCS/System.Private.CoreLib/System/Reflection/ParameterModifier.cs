using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005F3 RID: 1523
	public readonly struct ParameterModifier
	{
		// Token: 0x06004CF5 RID: 19701 RVA: 0x0018C1A5 File Offset: 0x0018B3A5
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException(SR.Arg_ParmArraySize);
			}
			this._byRef = new bool[parameterCount];
		}

		// Token: 0x17000C3E RID: 3134
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x0018C1D7 File Offset: 0x0018B3D7
		[Nullable(1)]
		internal bool[] IsByRefArray
		{
			get
			{
				return this._byRef;
			}
		}

		// Token: 0x040013B8 RID: 5048
		private readonly bool[] _byRef;
	}
}
