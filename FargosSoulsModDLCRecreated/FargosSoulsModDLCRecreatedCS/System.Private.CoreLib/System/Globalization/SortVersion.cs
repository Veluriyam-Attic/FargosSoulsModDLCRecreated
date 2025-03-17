using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000222 RID: 546
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class SortVersion : IEquatable<SortVersion>
	{
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00133C03 File Offset: 0x00132E03
		public int FullVersion
		{
			get
			{
				return this.m_NlsVersion;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x00133C0B File Offset: 0x00132E0B
		public Guid SortId
		{
			get
			{
				return this.m_SortId;
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00133C13 File Offset: 0x00132E13
		public SortVersion(int fullVersion, Guid sortId)
		{
			this.m_SortId = sortId;
			this.m_NlsVersion = fullVersion;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00133C2C File Offset: 0x00132E2C
		internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
		{
			this.m_NlsVersion = nlsVersion;
			if (customVersion == Guid.Empty)
			{
				byte h = (byte)(effectiveId >> 24);
				byte i = (byte)((effectiveId & 16711680) >> 16);
				byte j = (byte)((effectiveId & 65280) >> 8);
				byte k = (byte)(effectiveId & 255);
				customVersion = new Guid(0, 0, 0, 0, 0, 0, 0, h, i, j, k);
			}
			this.m_SortId = customVersion;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x00133C94 File Offset: 0x00132E94
		public override bool Equals(object obj)
		{
			SortVersion sortVersion = obj as SortVersion;
			return sortVersion != null && this.Equals(sortVersion);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00133CB4 File Offset: 0x00132EB4
		public bool Equals(SortVersion other)
		{
			return !(other == null) && this.m_NlsVersion == other.m_NlsVersion && this.m_SortId == other.m_SortId;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00133CE2 File Offset: 0x00132EE2
		public override int GetHashCode()
		{
			return this.m_NlsVersion * 7 | this.m_SortId.GetHashCode();
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00133CFE File Offset: 0x00132EFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(SortVersion left, SortVersion right)
		{
			if (right == null)
			{
				return left == null;
			}
			return right.Equals(left);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00133D11 File Offset: 0x00132F11
		public static bool operator !=(SortVersion left, SortVersion right)
		{
			return !(left == right);
		}

		// Token: 0x040008CF RID: 2255
		private readonly int m_NlsVersion;

		// Token: 0x040008D0 RID: 2256
		private Guid m_SortId;
	}
}
