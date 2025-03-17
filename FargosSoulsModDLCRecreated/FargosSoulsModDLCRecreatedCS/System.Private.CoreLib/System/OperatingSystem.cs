using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000166 RID: 358
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class OperatingSystem : ISerializable, ICloneable
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x000E6728 File Offset: 0x000E5928
		public OperatingSystem(PlatformID platform, Version version) : this(platform, version, null)
		{
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000E6734 File Offset: 0x000E5934
		internal OperatingSystem(PlatformID platform, Version version, string servicePack)
		{
			if (platform < PlatformID.Win32S || platform > PlatformID.Other)
			{
				throw new ArgumentOutOfRangeException("platform", platform, SR.Format(SR.Arg_EnumIllegalVal, platform));
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = version;
			this._servicePack = servicePack;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000B3617 File Offset: 0x000B2817
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x000E6799 File Offset: 0x000E5999
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x000E67A1 File Offset: 0x000E59A1
		public string ServicePack
		{
			get
			{
				return this._servicePack ?? string.Empty;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000E67B2 File Offset: 0x000E59B2
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000E67BA File Offset: 0x000E59BA
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version, this._servicePack);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000E67D3 File Offset: 0x000E59D3
		public override string ToString()
		{
			return this.VersionString;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x000E67DC File Offset: 0x000E59DC
		public string VersionString
		{
			get
			{
				if (this._versionString == null)
				{
					string str;
					switch (this._platform)
					{
					case PlatformID.Win32S:
						str = "Microsoft Win32S ";
						break;
					case PlatformID.Win32Windows:
						str = ((this._version.Major > 4 || (this._version.Major == 4 && this._version.Minor > 0)) ? "Microsoft Windows 98 " : "Microsoft Windows 95 ");
						break;
					case PlatformID.Win32NT:
						str = "Microsoft Windows NT ";
						break;
					case PlatformID.WinCE:
						str = "Microsoft Windows CE ";
						break;
					case PlatformID.Unix:
						str = "Unix ";
						break;
					case PlatformID.Xbox:
						str = "Xbox ";
						break;
					case PlatformID.MacOSX:
						str = "Mac OS X ";
						break;
					case PlatformID.Other:
						str = "Other ";
						break;
					default:
						str = "<unknown> ";
						break;
					}
					this._versionString = (string.IsNullOrEmpty(this._servicePack) ? (str + this._version.ToString()) : (str + this._version.ToString(3) + " " + this._servicePack));
				}
				return this._versionString;
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000E68E3 File Offset: 0x000E5AE3
		public static bool IsOSPlatform(string platform)
		{
			if (platform == null)
			{
				throw new ArgumentNullException("platform");
			}
			return platform.Equals("WINDOWS", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000E68FF File Offset: 0x000E5AFF
		public static bool IsOSPlatformVersionAtLeast(string platform, int major, int minor = 0, int build = 0, int revision = 0)
		{
			return OperatingSystem.IsOSPlatform(platform) && OperatingSystem.IsOSVersionAtLeast(major, minor, build, revision);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsBrowser()
		{
			return false;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsLinux()
		{
			return false;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsFreeBSD()
		{
			return false;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000E6915 File Offset: 0x000E5B15
		public static bool IsFreeBSDVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
		{
			if (OperatingSystem.IsFreeBSD())
			{
			}
			return false;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsAndroid()
		{
			return false;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000E691F File Offset: 0x000E5B1F
		public static bool IsAndroidVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
		{
			if (OperatingSystem.IsAndroid())
			{
			}
			return false;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsIOS()
		{
			return false;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000E6929 File Offset: 0x000E5B29
		public static bool IsIOSVersionAtLeast(int major, int minor = 0, int build = 0)
		{
			if (OperatingSystem.IsIOS())
			{
			}
			return false;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsMacOS()
		{
			return false;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000E6933 File Offset: 0x000E5B33
		public static bool IsMacOSVersionAtLeast(int major, int minor = 0, int build = 0)
		{
			if (OperatingSystem.IsMacOS())
			{
			}
			return false;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsTvOS()
		{
			return false;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000E693D File Offset: 0x000E5B3D
		public static bool IsTvOSVersionAtLeast(int major, int minor = 0, int build = 0)
		{
			if (OperatingSystem.IsTvOS())
			{
			}
			return false;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsWatchOS()
		{
			return false;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000E6947 File Offset: 0x000E5B47
		public static bool IsWatchOSVersionAtLeast(int major, int minor = 0, int build = 0)
		{
			if (OperatingSystem.IsWatchOS())
			{
			}
			return false;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000AC09E File Offset: 0x000AB29E
		public static bool IsWindows()
		{
			return true;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000E6951 File Offset: 0x000E5B51
		public static bool IsWindowsVersionAtLeast(int major, int minor = 0, int build = 0, int revision = 0)
		{
			OperatingSystem.IsWindows();
			return OperatingSystem.IsOSVersionAtLeast(major, minor, build, revision);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000E6964 File Offset: 0x000E5B64
		private static bool IsOSVersionAtLeast(int major, int minor, int build, int revision)
		{
			Version version = Environment.OSVersion.Version;
			if (version.Major != major)
			{
				return version.Major > major;
			}
			if (version.Minor != minor)
			{
				return version.Minor > minor;
			}
			if (version.Build != build)
			{
				return version.Build > build;
			}
			return version.Revision >= revision || (version.Revision == -1 && revision == 0);
		}

		// Token: 0x0400045F RID: 1119
		private readonly Version _version;

		// Token: 0x04000460 RID: 1120
		private readonly PlatformID _platform;

		// Token: 0x04000461 RID: 1121
		private readonly string _servicePack;

		// Token: 0x04000462 RID: 1122
		private string _versionString;
	}
}
