using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal sealed class OrdinalIgnoreCaseComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x000F296A File Offset: 0x000F1B6A
		private OrdinalIgnoreCaseComparer() : base(true)
		{
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000F2973 File Offset: 0x000F1B73
		public override int Compare(string x, string y)
		{
			return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000F297D File Offset: 0x000F1B7D
		public override bool Equals(string x, string y)
		{
			return x == y || (x != null && y != null && x.Length == y.Length && System.Globalization.Ordinal.EqualsIgnoreCase(x.GetRawStringData(), y.GetRawStringData(), x.Length));
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000F29B4 File Offset: 0x000F1BB4
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return obj.GetHashCodeOrdinalIgnoreCase();
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000F29C5 File Offset: 0x000F1BC5
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", true);
		}

		// Token: 0x040004A7 RID: 1191
		internal static readonly OrdinalIgnoreCaseComparer Instance = new OrdinalIgnoreCaseComparer();
	}
}
