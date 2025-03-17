using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FA RID: 1018
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x0016B3E9 File Offset: 0x0016A5E9
		public string Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x0016B3F1 File Offset: 0x0016A5F1
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x0016B3F9 File Offset: 0x0016A5F9
		public string Profile
		{
			get
			{
				return this._profile;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x0016B404 File Offset: 0x0016A604
		public string FullName
		{
			get
			{
				if (this._fullName == null)
				{
					if (string.IsNullOrEmpty(this.Profile))
					{
						this._fullName = this.Identifier + ",Version=v" + this.Version.ToString();
					}
					else
					{
						this._fullName = string.Concat(new string[]
						{
							this.Identifier,
							",Version=v",
							this.Version.ToString(),
							",Profile=",
							this.Profile
						});
					}
				}
				return this._fullName;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0016B490 File Offset: 0x0016A690
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x0016B49E File Offset: 0x0016A69E
		[NullableContext(2)]
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x0016B4DE File Offset: 0x0016A6DE
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x0016B503 File Offset: 0x0016A703
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x0016B50B File Offset: 0x0016A70B
		public FrameworkName(string identifier, Version version) : this(identifier, version, null)
		{
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x0016B518 File Offset: 0x0016A718
		public FrameworkName(string identifier, Version version, [Nullable(2)] string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			identifier = identifier.Trim();
			if (identifier.Length == 0)
			{
				throw new ArgumentException(SR.Format(SR.net_emptystringcall, "identifier"), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._identifier = identifier;
			this._version = version;
			this._profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x0016B59C File Offset: 0x0016A79C
		public unsafe FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(SR.Format(SR.net_emptystringcall, "frameworkName"), "frameworkName");
			}
			string[] array = frameworkName.Split(',', StringSplitOptions.None);
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(SR.Argument_FrameworkNameTooShort, "frameworkName");
			}
			this._identifier = array[0].Trim();
			if (this._identifier.Length == 0)
			{
				throw new ArgumentException(SR.Argument_FrameworkNameInvalid, "frameworkName");
			}
			bool flag = false;
			this._profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string text = array[i];
				int num = text.IndexOf('=');
				if (num == -1 || num != text.LastIndexOf('='))
				{
					throw new ArgumentException(SR.Argument_FrameworkNameInvalid, "frameworkName");
				}
				ReadOnlySpan<char> span = text.AsSpan(0, num).Trim();
				ReadOnlySpan<char> input = text.AsSpan(num + 1).Trim();
				if (span.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (input.Length > 0 && (*input[0] == 118 || *input[0] == 86))
					{
						input = input.Slice(1);
					}
					try
					{
						this._version = Version.Parse(input);
						goto IL_196;
					}
					catch (Exception innerException)
					{
						throw new ArgumentException(SR.Argument_FrameworkNameInvalidVersion, "frameworkName", innerException);
					}
					goto IL_153;
				}
				goto IL_153;
				IL_196:
				i++;
				continue;
				IL_153:
				if (!span.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.Argument_FrameworkNameInvalid, "frameworkName");
				}
				if (input.Length > 0)
				{
					this._profile = input.ToString();
					goto IL_196;
				}
				goto IL_196;
			}
			if (!flag)
			{
				throw new ArgumentException(SR.Argument_FrameworkNameMissingVersion, "frameworkName");
			}
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x0016B770 File Offset: 0x0016A970
		[NullableContext(2)]
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x0016B781 File Offset: 0x0016A981
		[NullableContext(2)]
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x04000E2B RID: 3627
		private readonly string _identifier;

		// Token: 0x04000E2C RID: 3628
		private readonly Version _version;

		// Token: 0x04000E2D RID: 3629
		private readonly string _profile;

		// Token: 0x04000E2E RID: 3630
		private string _fullName;
	}
}
