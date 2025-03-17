using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200055E RID: 1374
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x06004790 RID: 18320 RVA: 0x0017DBDE File Offset: 0x0017CDDE
		public TupleElementNamesAttribute([Nullable(new byte[]
		{
			1,
			2
		})] string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x0017DBFB File Offset: 0x0017CDFB
		[Nullable(new byte[]
		{
			1,
			2
		})]
		public IList<string> TransformNames
		{
			[return: Nullable(new byte[]
			{
				1,
				2
			})]
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x04001143 RID: 4419
		private readonly string[] _transformNames;
	}
}
