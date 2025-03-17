using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004B3 RID: 1203
	internal class EnumeratorViewOfEnumVariant : ICustomAdapter, System.Collections.IEnumerator
	{
		// Token: 0x06004523 RID: 17699 RVA: 0x0017A18A File Offset: 0x0017938A
		public EnumeratorViewOfEnumVariant(IEnumVARIANT enumVariantObject)
		{
			this._enumVariantObject = enumVariantObject;
			this._fetchedLastObject = false;
			this._current = null;
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0017A1B3 File Offset: 0x001793B3
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0017A1BC File Offset: 0x001793BC
		public unsafe bool MoveNext()
		{
			if (this._fetchedLastObject)
			{
				this._current = null;
				return false;
			}
			int num = 0;
			if (this._enumVariantObject.Next(1, this._nextArray, (IntPtr)((void*)(&num))) == 1)
			{
				this._fetchedLastObject = true;
				if (num == 0)
				{
					this._current = null;
					return false;
				}
			}
			this._current = this._nextArray[0];
			return true;
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0017A21C File Offset: 0x0017941C
		public void Reset()
		{
			int num = this._enumVariantObject.Reset();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			this._fetchedLastObject = false;
			this._current = null;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x0017A24D File Offset: 0x0017944D
		public object GetUnderlyingObject()
		{
			return this._enumVariantObject;
		}

		// Token: 0x04000FD2 RID: 4050
		private readonly IEnumVARIANT _enumVariantObject;

		// Token: 0x04000FD3 RID: 4051
		private bool _fetchedLastObject;

		// Token: 0x04000FD4 RID: 4052
		private readonly object[] _nextArray = new object[1];

		// Token: 0x04000FD5 RID: 4053
		private object _current;
	}
}
