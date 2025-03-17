using System;
using Internal.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000807 RID: 2055
	internal abstract class RandomizedStringEqualityComparer : EqualityComparer<string>, IInternalStringEqualityComparer, IEqualityComparer<string>
	{
		// Token: 0x06006216 RID: 25110 RVA: 0x001D33D0 File Offset: 0x001D25D0
		private unsafe RandomizedStringEqualityComparer(IEqualityComparer<string> underlyingComparer)
		{
			this._underlyingComparer = underlyingComparer;
			fixed (RandomizedStringEqualityComparer.MarvinSeed* ptr = &this._seed)
			{
				RandomizedStringEqualityComparer.MarvinSeed* buffer = ptr;
				Interop.GetRandomBytes((byte*)buffer, sizeof(RandomizedStringEqualityComparer.MarvinSeed));
			}
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x001D3403 File Offset: 0x001D2603
		internal static RandomizedStringEqualityComparer Create(IEqualityComparer<string> underlyingComparer, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return new RandomizedStringEqualityComparer.OrdinalComparer(underlyingComparer);
			}
			return new RandomizedStringEqualityComparer.OrdinalIgnoreCaseComparer(underlyingComparer);
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x001D3415 File Offset: 0x001D2615
		public IEqualityComparer<string> GetUnderlyingEqualityComparer()
		{
			return this._underlyingComparer;
		}

		// Token: 0x04001D44 RID: 7492
		private readonly RandomizedStringEqualityComparer.MarvinSeed _seed;

		// Token: 0x04001D45 RID: 7493
		private readonly IEqualityComparer<string> _underlyingComparer;

		// Token: 0x02000808 RID: 2056
		private struct MarvinSeed
		{
			// Token: 0x04001D46 RID: 7494
			internal uint p0;

			// Token: 0x04001D47 RID: 7495
			internal uint p1;
		}

		// Token: 0x02000809 RID: 2057
		private sealed class OrdinalComparer : RandomizedStringEqualityComparer
		{
			// Token: 0x06006219 RID: 25113 RVA: 0x001D341D File Offset: 0x001D261D
			internal OrdinalComparer(IEqualityComparer<string> wrappedComparer) : base(wrappedComparer)
			{
			}

			// Token: 0x0600621A RID: 25114 RVA: 0x000F2926 File Offset: 0x000F1B26
			public override bool Equals(string x, string y)
			{
				return string.Equals(x, y);
			}

			// Token: 0x0600621B RID: 25115 RVA: 0x001D3426 File Offset: 0x001D2626
			public override int GetHashCode(string obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return Marvin.ComputeHash32(Unsafe.As<char, byte>(obj.GetRawStringData()), (uint)(obj.Length * 2), this._seed.p0, this._seed.p1);
			}
		}

		// Token: 0x0200080A RID: 2058
		private sealed class OrdinalIgnoreCaseComparer : RandomizedStringEqualityComparer
		{
			// Token: 0x0600621C RID: 25116 RVA: 0x001D341D File Offset: 0x001D261D
			internal OrdinalIgnoreCaseComparer(IEqualityComparer<string> wrappedComparer) : base(wrappedComparer)
			{
			}

			// Token: 0x0600621D RID: 25117 RVA: 0x001D345B File Offset: 0x001D265B
			public override bool Equals(string x, string y)
			{
				return string.EqualsOrdinalIgnoreCase(x, y);
			}

			// Token: 0x0600621E RID: 25118 RVA: 0x001D3426 File Offset: 0x001D2626
			public override int GetHashCode(string obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return Marvin.ComputeHash32(Unsafe.As<char, byte>(obj.GetRawStringData()), (uint)(obj.Length * 2), this._seed.p0, this._seed.p1);
			}
		}
	}
}
