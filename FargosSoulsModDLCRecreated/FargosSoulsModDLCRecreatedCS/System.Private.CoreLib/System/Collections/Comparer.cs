using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections
{
	// Token: 0x020007AA RID: 1962
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06005F10 RID: 24336 RVA: 0x001C87DC File Offset: 0x001C79DC
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x001C87FE File Offset: 0x001C79FE
		private Comparer(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._compareInfo = (CompareInfo)info.GetValue("CompareInfo", typeof(CompareInfo));
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x001C8834 File Offset: 0x001C7A34
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("CompareInfo", this._compareInfo);
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x001C8858 File Offset: 0x001C7A58
		[NullableContext(2)]
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			string text = a as string;
			if (text != null)
			{
				string text2 = b as string;
				if (text2 != null)
				{
					return this._compareInfo.Compare(text, text2);
				}
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException(SR.Argument_ImplementIComparable);
		}

		// Token: 0x04001CB2 RID: 7346
		private readonly CompareInfo _compareInfo;

		// Token: 0x04001CB3 RID: 7347
		public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);

		// Token: 0x04001CB4 RID: 7348
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);
	}
}
