using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000185 RID: 389
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class CultureAwareComparer : StringComparer, ISerializable
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x000F263F File Offset: 0x000F183F
		internal CultureAwareComparer(CultureInfo culture, CompareOptions options) : this(culture.CompareInfo, options)
		{
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000F264E File Offset: 0x000F184E
		internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
		{
			this._compareInfo = compareInfo;
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(SR.Argument_InvalidFlag, "options");
			}
			this._options = options;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000F2680 File Offset: 0x000F1880
		private CultureAwareComparer(SerializationInfo info, StreamingContext context)
		{
			this._compareInfo = (CompareInfo)info.GetValue("_compareInfo", typeof(CompareInfo));
			bool boolean = info.GetBoolean("_ignoreCase");
			object valueNoThrow = info.GetValueNoThrow("_options", typeof(CompareOptions));
			if (valueNoThrow != null)
			{
				this._options = (CompareOptions)valueNoThrow;
			}
			this._options |= (boolean ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000F26F8 File Offset: 0x000F18F8
		public override int Compare(string x, string y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return this._compareInfo.Compare(x, y, this._options);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000F271D File Offset: 0x000F191D
		public override bool Equals(string x, string y)
		{
			return x == y || (x != null && y != null && this._compareInfo.Compare(x, y, this._options) == 0);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000F2743 File Offset: 0x000F1943
		[NullableContext(1)]
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this._compareInfo.GetHashCode(obj, this._options);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000F2768 File Offset: 0x000F1968
		public override bool Equals(object obj)
		{
			CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
			return cultureAwareComparer != null && this._options == cultureAwareComparer._options && this._compareInfo.Equals(cultureAwareComparer._compareInfo);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000F27A0 File Offset: 0x000F19A0
		public override int GetHashCode()
		{
			return this._compareInfo.GetHashCode() ^ (int)(this._options & (CompareOptions)2147483647);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000F27BA File Offset: 0x000F19BA
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_compareInfo", this._compareInfo);
			info.AddValue("_options", this._options);
			info.AddValue("_ignoreCase", (this._options & CompareOptions.IgnoreCase) > CompareOptions.None);
		}

		// Token: 0x040004A1 RID: 1185
		internal static readonly CultureAwareComparer InvariantCaseSensitiveInstance = new CultureAwareComparer(CompareInfo.Invariant, CompareOptions.None);

		// Token: 0x040004A2 RID: 1186
		internal static readonly CultureAwareComparer InvariantIgnoreCaseInstance = new CultureAwareComparer(CompareInfo.Invariant, CompareOptions.IgnoreCase);

		// Token: 0x040004A3 RID: 1187
		private readonly CompareInfo _compareInfo;

		// Token: 0x040004A4 RID: 1188
		private readonly CompareOptions _options;
	}
}
