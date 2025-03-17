using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	internal sealed class OrdinalCaseSensitiveComparer : OrdinalComparer, ISerializable
	{
		// Token: 0x060017F8 RID: 6136 RVA: 0x000F2914 File Offset: 0x000F1B14
		private OrdinalCaseSensitiveComparer() : base(false)
		{
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000F291D File Offset: 0x000F1B1D
		public override int Compare(string x, string y)
		{
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000F2926 File Offset: 0x000F1B26
		public override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000F292F File Offset: 0x000F1B2F
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000F2940 File Offset: 0x000F1B40
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(OrdinalComparer));
			info.AddValue("_ignoreCase", false);
		}

		// Token: 0x040004A6 RID: 1190
		internal static readonly OrdinalCaseSensitiveComparer Instance = new OrdinalCaseSensitiveComparer();
	}
}
