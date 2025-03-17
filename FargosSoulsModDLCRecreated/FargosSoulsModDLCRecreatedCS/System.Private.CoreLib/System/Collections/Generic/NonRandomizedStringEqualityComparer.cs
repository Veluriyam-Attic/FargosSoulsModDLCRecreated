using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200080B RID: 2059
	[Serializable]
	public class NonRandomizedStringEqualityComparer : IEqualityComparer<string>, IInternalStringEqualityComparer, ISerializable
	{
		// Token: 0x0600621F RID: 25119 RVA: 0x001D3464 File Offset: 0x001D2664
		private NonRandomizedStringEqualityComparer(IEqualityComparer<string> underlyingComparer)
		{
			this._underlyingComparer = underlyingComparer;
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x001D3473 File Offset: 0x001D2673
		[NullableContext(1)]
		protected NonRandomizedStringEqualityComparer(SerializationInfo information, StreamingContext context) : this(EqualityComparer<string>.Default)
		{
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x000F2926 File Offset: 0x000F1B26
		[NullableContext(2)]
		public virtual bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x001D3480 File Offset: 0x001D2680
		[NullableContext(2)]
		public virtual int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetNonRandomizedHashCode();
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x001D348D File Offset: 0x001D268D
		internal virtual RandomizedStringEqualityComparer GetRandomizedEqualityComparer()
		{
			return RandomizedStringEqualityComparer.Create(this._underlyingComparer, false);
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x001D349B File Offset: 0x001D269B
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual IEqualityComparer<string> GetUnderlyingEqualityComparer()
		{
			return this._underlyingComparer;
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x001D34A3 File Offset: 0x001D26A3
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(GenericEqualityComparer<string>));
		}

		// Token: 0x04001D48 RID: 7496
		internal static readonly NonRandomizedStringEqualityComparer WrappedAroundDefaultComparer = new NonRandomizedStringEqualityComparer.OrdinalComparer(EqualityComparer<string>.Default);

		// Token: 0x04001D49 RID: 7497
		internal static readonly NonRandomizedStringEqualityComparer WrappedAroundStringComparerOrdinal = new NonRandomizedStringEqualityComparer.OrdinalComparer(StringComparer.Ordinal);

		// Token: 0x04001D4A RID: 7498
		internal static readonly NonRandomizedStringEqualityComparer WrappedAroundStringComparerOrdinalIgnoreCase = new NonRandomizedStringEqualityComparer.OrdinalIgnoreCaseComparer(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001D4B RID: 7499
		private readonly IEqualityComparer<string> _underlyingComparer;

		// Token: 0x0200080C RID: 2060
		private sealed class OrdinalComparer : NonRandomizedStringEqualityComparer
		{
			// Token: 0x06006227 RID: 25127 RVA: 0x001D34E4 File Offset: 0x001D26E4
			internal OrdinalComparer(IEqualityComparer<string> wrappedComparer) : base(wrappedComparer)
			{
			}

			// Token: 0x06006228 RID: 25128 RVA: 0x000F2926 File Offset: 0x000F1B26
			public override bool Equals(string x, string y)
			{
				return string.Equals(x, y);
			}

			// Token: 0x06006229 RID: 25129 RVA: 0x001D34ED File Offset: 0x001D26ED
			public override int GetHashCode(string obj)
			{
				return obj.GetNonRandomizedHashCode();
			}
		}

		// Token: 0x0200080D RID: 2061
		private sealed class OrdinalIgnoreCaseComparer : NonRandomizedStringEqualityComparer
		{
			// Token: 0x0600622A RID: 25130 RVA: 0x001D34E4 File Offset: 0x001D26E4
			internal OrdinalIgnoreCaseComparer(IEqualityComparer<string> wrappedComparer) : base(wrappedComparer)
			{
			}

			// Token: 0x0600622B RID: 25131 RVA: 0x001D345B File Offset: 0x001D265B
			public override bool Equals(string x, string y)
			{
				return string.EqualsOrdinalIgnoreCase(x, y);
			}

			// Token: 0x0600622C RID: 25132 RVA: 0x001D34F5 File Offset: 0x001D26F5
			public override int GetHashCode(string obj)
			{
				return obj.GetNonRandomizedHashCodeOrdinalIgnoreCase();
			}

			// Token: 0x0600622D RID: 25133 RVA: 0x001D34FD File Offset: 0x001D26FD
			internal override RandomizedStringEqualityComparer GetRandomizedEqualityComparer()
			{
				return RandomizedStringEqualityComparer.Create(this._underlyingComparer, true);
			}
		}
	}
}
