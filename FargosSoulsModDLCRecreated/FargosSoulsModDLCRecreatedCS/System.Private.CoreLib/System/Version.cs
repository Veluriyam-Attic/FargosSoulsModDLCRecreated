using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001C2 RID: 450
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>, ISpanFormattable
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x001018A4 File Offset: 0x00100AA4
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", SR.ArgumentOutOfRange_Version);
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", SR.ArgumentOutOfRange_Version);
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", SR.ArgumentOutOfRange_Version);
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", SR.ArgumentOutOfRange_Version);
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00101928 File Offset: 0x00100B28
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", SR.ArgumentOutOfRange_Version);
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", SR.ArgumentOutOfRange_Version);
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", SR.ArgumentOutOfRange_Version);
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = -1;
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00101994 File Offset: 0x00100B94
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", SR.ArgumentOutOfRange_Version);
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", SR.ArgumentOutOfRange_Version);
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = -1;
			this._Revision = -1;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x001019EC File Offset: 0x00100BEC
		[NullableContext(1)]
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00101A36 File Offset: 0x00100C36
		public Version()
		{
			this._Build = -1;
			this._Revision = -1;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00101A4C File Offset: 0x00100C4C
		private Version(Version version)
		{
			this._Major = version._Major;
			this._Minor = version._Minor;
			this._Build = version._Build;
			this._Revision = version._Revision;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00101A84 File Offset: 0x00100C84
		[NullableContext(1)]
		public object Clone()
		{
			return new Version(this);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x00101A8C File Offset: 0x00100C8C
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00101A94 File Offset: 0x00100C94
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00101A9C File Offset: 0x00100C9C
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00101AA4 File Offset: 0x00100CA4
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00101AAC File Offset: 0x00100CAC
		public short MajorRevision
		{
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00101AB8 File Offset: 0x00100CB8
		public short MinorRevision
		{
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00101AC8 File Offset: 0x00100CC8
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 != null)
			{
				return this.CompareTo(version2);
			}
			throw new ArgumentException(SR.Arg_MustBeVersion);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00101AF8 File Offset: 0x00100CF8
		public int CompareTo(Version value)
		{
			if (value == this)
			{
				return 0;
			}
			if (value == null)
			{
				return 1;
			}
			if (this._Major == value._Major)
			{
				if (this._Minor == value._Minor)
				{
					if (this._Build == value._Build)
					{
						if (this._Revision == value._Revision)
						{
							return 0;
						}
						if (this._Revision <= value._Revision)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						if (this._Build <= value._Build)
						{
							return -1;
						}
						return 1;
					}
				}
				else
				{
					if (this._Minor <= value._Minor)
					{
						return -1;
					}
					return 1;
				}
			}
			else
			{
				if (this._Major <= value._Major)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00101B97 File Offset: 0x00100D97
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00101BA8 File Offset: 0x00100DA8
		public bool Equals(Version obj)
		{
			return obj == this || (obj != null && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00101BF8 File Offset: 0x00100DF8
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this._Major & 15) << 28;
			num |= (this._Minor & 255) << 20;
			num |= (this._Build & 255) << 12;
			return num | (this._Revision & 4095);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00101C4A File Offset: 0x00100E4A
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(this.DefaultFormatFieldCount);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00101C58 File Offset: 0x00100E58
		[NullableContext(1)]
		public string ToString(int fieldCount)
		{
			if (fieldCount == 0)
			{
				return string.Empty;
			}
			if (fieldCount != 1)
			{
				return StringBuilderCache.GetStringAndRelease(this.ToCachedStringBuilder(fieldCount));
			}
			return this._Major.ToString();
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00101C7F File Offset: 0x00100E7F
		[NullableContext(0)]
		public bool TryFormat(Span<char> destination, out int charsWritten)
		{
			return this.TryFormat(destination, this.DefaultFormatFieldCount, out charsWritten);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00101C90 File Offset: 0x00100E90
		[NullableContext(0)]
		public bool TryFormat(Span<char> destination, int fieldCount, out int charsWritten)
		{
			if (fieldCount == 0)
			{
				charsWritten = 0;
				return true;
			}
			if (fieldCount == 1)
			{
				return this._Major.TryFormat(destination, out charsWritten, default(ReadOnlySpan<char>), null);
			}
			StringBuilder stringBuilder = this.ToCachedStringBuilder(fieldCount);
			if (stringBuilder.Length <= destination.Length)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				StringBuilderCache.Release(stringBuilder);
				charsWritten = stringBuilder.Length;
				return true;
			}
			StringBuilderCache.Release(stringBuilder);
			charsWritten = 0;
			return false;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00101D00 File Offset: 0x00100F00
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00101D0A File Offset: 0x00100F0A
		private int DefaultFormatFieldCount
		{
			get
			{
				if (this._Build == -1)
				{
					return 2;
				}
				if (this._Revision != -1)
				{
					return 4;
				}
				return 3;
			}
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00101D24 File Offset: 0x00100F24
		private StringBuilder ToCachedStringBuilder(int fieldCount)
		{
			if (fieldCount == 2)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(this._Major);
				stringBuilder.Append('.');
				stringBuilder.Append(this._Minor);
				return stringBuilder;
			}
			if (this._Build == -1)
			{
				throw new ArgumentException(SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, "0", "2"), "fieldCount");
			}
			if (fieldCount == 3)
			{
				StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
				stringBuilder2.Append(this._Major);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Minor);
				stringBuilder2.Append('.');
				stringBuilder2.Append(this._Build);
				return stringBuilder2;
			}
			if (this._Revision == -1)
			{
				throw new ArgumentException(SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, "0", "3"), "fieldCount");
			}
			if (fieldCount == 4)
			{
				StringBuilder stringBuilder3 = StringBuilderCache.Acquire(16);
				stringBuilder3.Append(this._Major);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Minor);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Build);
				stringBuilder3.Append('.');
				stringBuilder3.Append(this._Revision);
				return stringBuilder3;
			}
			throw new ArgumentException(SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, "0", "4"), "fieldCount");
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00101E74 File Offset: 0x00101074
		[NullableContext(1)]
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Version.ParseVersion(input.AsSpan(), true);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00101E90 File Offset: 0x00101090
		[NullableContext(0)]
		[return: Nullable(1)]
		public static Version Parse(ReadOnlySpan<char> input)
		{
			return Version.ParseVersion(input, true);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00101E9C File Offset: 0x0010109C
		public static bool TryParse([NotNullWhen(true)] string input, [NotNullWhen(true)] out Version result)
		{
			if (input == null)
			{
				result = null;
				return false;
			}
			Version v;
			result = (v = Version.ParseVersion(input.AsSpan(), false));
			return v != null;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00101EC8 File Offset: 0x001010C8
		[NullableContext(0)]
		public static bool TryParse(ReadOnlySpan<char> input, [Nullable(2)] [NotNullWhen(true)] out Version result)
		{
			Version v;
			result = (v = Version.ParseVersion(input, false));
			return v != null;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00101EE8 File Offset: 0x001010E8
		private static Version ParseVersion(ReadOnlySpan<char> input, bool throwOnFailure)
		{
			int num = input.IndexOf('.');
			if (num < 0)
			{
				if (throwOnFailure)
				{
					throw new ArgumentException(SR.Arg_VersionString, "input");
				}
				return null;
			}
			else
			{
				int num2 = -1;
				int num3 = input.Slice(num + 1).IndexOf('.');
				if (num3 != -1)
				{
					num3 += num + 1;
					num2 = input.Slice(num3 + 1).IndexOf('.');
					if (num2 != -1)
					{
						num2 += num3 + 1;
						if (input.Slice(num2 + 1).Contains('.'))
						{
							if (throwOnFailure)
							{
								throw new ArgumentException(SR.Arg_VersionString, "input");
							}
							return null;
						}
					}
				}
				int major;
				if (!Version.TryParseComponent(input.Slice(0, num), "input", throwOnFailure, out major))
				{
					return null;
				}
				if (num3 != -1)
				{
					int minor;
					if (!Version.TryParseComponent(input.Slice(num + 1, num3 - num - 1), "input", throwOnFailure, out minor))
					{
						return null;
					}
					if (num2 != -1)
					{
						int build;
						int revision;
						if (!Version.TryParseComponent(input.Slice(num3 + 1, num2 - num3 - 1), "build", throwOnFailure, out build) || !Version.TryParseComponent(input.Slice(num2 + 1), "revision", throwOnFailure, out revision))
						{
							return null;
						}
						return new Version(major, minor, build, revision);
					}
					else
					{
						int build;
						if (!Version.TryParseComponent(input.Slice(num3 + 1), "build", throwOnFailure, out build))
						{
							return null;
						}
						return new Version(major, minor, build);
					}
				}
				else
				{
					int minor;
					if (!Version.TryParseComponent(input.Slice(num + 1), "input", throwOnFailure, out minor))
					{
						return null;
					}
					return new Version(major, minor);
				}
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00102050 File Offset: 0x00101250
		private static bool TryParseComponent(ReadOnlySpan<char> component, string componentName, bool throwOnFailure, out int parsedComponent)
		{
			if (!throwOnFailure)
			{
				return int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent) && parsedComponent >= 0;
			}
			if ((parsedComponent = int.Parse(component, NumberStyles.Integer, CultureInfo.InvariantCulture)) < 0)
			{
				throw new ArgumentOutOfRangeException(componentName, SR.ArgumentOutOfRange_Version);
			}
			return true;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0010209B File Offset: 0x0010129B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Version v1, Version v2)
		{
			if (v2 == null)
			{
				return v1 == null;
			}
			return v2 == v1 || v2.Equals(v1);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x001020B4 File Offset: 0x001012B4
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x001020C0 File Offset: 0x001012C0
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 != null;
			}
			return v1.CompareTo(v2) < 0;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x001020D4 File Offset: 0x001012D4
		public static bool operator <=(Version v1, Version v2)
		{
			return v1 == null || v1.CompareTo(v2) <= 0;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x001020E8 File Offset: 0x001012E8
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x001020F1 File Offset: 0x001012F1
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x040005FE RID: 1534
		private readonly int _Major;

		// Token: 0x040005FF RID: 1535
		private readonly int _Minor;

		// Token: 0x04000600 RID: 1536
		private readonly int _Build;

		// Token: 0x04000601 RID: 1537
		private readonly int _Revision;
	}
}
