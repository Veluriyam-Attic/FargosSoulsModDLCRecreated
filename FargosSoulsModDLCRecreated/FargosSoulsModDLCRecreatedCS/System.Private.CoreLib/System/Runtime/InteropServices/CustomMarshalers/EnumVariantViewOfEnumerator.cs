using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004AD RID: 1197
	internal class EnumVariantViewOfEnumerator : IEnumVARIANT, ICustomAdapter
	{
		// Token: 0x06004502 RID: 17666 RVA: 0x00179DC7 File Offset: 0x00178FC7
		public EnumVariantViewOfEnumerator(System.Collections.IEnumerator enumerator)
		{
			if (enumerator == null)
			{
				throw new ArgumentNullException("enumerator");
			}
			this.Enumerator = enumerator;
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x00179DE4 File Offset: 0x00178FE4
		public System.Collections.IEnumerator Enumerator { get; }

		// Token: 0x06004504 RID: 17668 RVA: 0x00179DEC File Offset: 0x00178FEC
		public IEnumVARIANT Clone()
		{
			ICloneable cloneable = this.Enumerator as ICloneable;
			if (cloneable != null)
			{
				return new EnumVariantViewOfEnumerator((System.Collections.IEnumerator)cloneable.Clone());
			}
			throw new COMException(SR.Arg_EnumNotCloneable, -2147467259);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x00179E28 File Offset: 0x00179028
		public int Next(int celt, object[] rgVar, IntPtr pceltFetched)
		{
			int num = 0;
			try
			{
				if (celt > 0 && rgVar == null)
				{
					return -2147024809;
				}
				while (num < celt && this.Enumerator.MoveNext())
				{
					rgVar[num++] = this.Enumerator.Current;
				}
				if (pceltFetched != IntPtr.Zero)
				{
					Marshal.WriteInt32(pceltFetched, num);
				}
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			if (num != celt)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00179EA4 File Offset: 0x001790A4
		public int Reset()
		{
			try
			{
				this.Enumerator.Reset();
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			return 0;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00179EDC File Offset: 0x001790DC
		public int Skip(int celt)
		{
			try
			{
				while (celt > 0 && this.Enumerator.MoveNext())
				{
					celt--;
				}
			}
			catch (Exception ex)
			{
				return ex.HResult;
			}
			if (celt != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00179F28 File Offset: 0x00179128
		public object GetUnderlyingObject()
		{
			return this.Enumerator;
		}
	}
}
