using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000221 RID: 545
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class SortKey
	{
		// Token: 0x060022BC RID: 8892 RVA: 0x00133ACF File Offset: 0x00132CCF
		internal SortKey(CompareInfo compareInfo, string str, CompareOptions options, byte[] keyData)
		{
			this._keyData = keyData;
			this._compareInfo = compareInfo;
			this._options = options;
			this._string = str;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00133AF4 File Offset: 0x00132CF4
		public string OriginalString
		{
			get
			{
				return this._string;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x00133AFC File Offset: 0x00132CFC
		public byte[] KeyData
		{
			get
			{
				return (byte[])this._keyData.Clone();
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00133B10 File Offset: 0x00132D10
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			byte[] keyData = sortkey1._keyData;
			byte[] keyData2 = sortkey2._keyData;
			return new ReadOnlySpan<byte>(keyData).SequenceCompareTo(keyData2);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x00133B58 File Offset: 0x00132D58
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && new ReadOnlySpan<byte>(this._keyData).SequenceEqual(sortKey._keyData);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x00133B8C File Offset: 0x00132D8C
		public override int GetHashCode()
		{
			return this._compareInfo.GetHashCode(this._string, this._options);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00133BA8 File Offset: 0x00132DA8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"SortKey - ",
				this._compareInfo.Name,
				", ",
				this._options.ToString(),
				", ",
				this._string
			});
		}

		// Token: 0x040008CB RID: 2251
		private readonly CompareInfo _compareInfo;

		// Token: 0x040008CC RID: 2252
		private readonly CompareOptions _options;

		// Token: 0x040008CD RID: 2253
		private readonly string _string;

		// Token: 0x040008CE RID: 2254
		private readonly byte[] _keyData;
	}
}
