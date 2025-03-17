using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x02000587 RID: 1415
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class AssemblyName : ICloneable, IDeserializationCallback, ISerializable
	{
		// Token: 0x060048CF RID: 18639 RVA: 0x001825B1 File Offset: 0x001817B1
		[NullableContext(1)]
		public AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0 || assemblyName[0] == '\0')
			{
				throw new ArgumentException(SR.Format_StringZeroLength);
			}
			this._name = assemblyName;
			this.nInit();
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x001825F0 File Offset: 0x001817F0
		internal AssemblyName(string name, byte[] publicKey, byte[] publicKeyToken, Version version, CultureInfo cultureInfo, AssemblyHashAlgorithm hashAlgorithm, AssemblyVersionCompatibility versionCompatibility, string codeBase, AssemblyNameFlags flags, StrongNameKeyPair keyPair)
		{
			this._name = name;
			this._publicKey = publicKey;
			this._publicKeyToken = publicKeyToken;
			this._version = version;
			this._cultureInfo = cultureInfo;
			this._hashAlgorithm = hashAlgorithm;
			this._versionCompatibility = versionCompatibility;
			this._codeBase = codeBase;
			this._flags = flags;
			this._strongNameKeyPair = keyPair;
		}

		// Token: 0x060048D1 RID: 18641
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void nInit();

		// Token: 0x060048D2 RID: 18642
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssemblyName nGetFileInformation(string s);

		// Token: 0x060048D3 RID: 18643 RVA: 0x00182650 File Offset: 0x00181850
		internal static AssemblyName GetFileInformationCore(string assemblyFile)
		{
			string fullPath = Path.GetFullPath(assemblyFile);
			return AssemblyName.nGetFileInformation(fullPath);
		}

		// Token: 0x060048D4 RID: 18644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] ComputePublicKeyToken();

		// Token: 0x060048D5 RID: 18645 RVA: 0x0018266A File Offset: 0x0018186A
		internal void SetProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm)
		{
			this.ProcessorArchitecture = AssemblyName.CalculateProcArchIndex(pek, ifm, this._flags);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00182680 File Offset: 0x00181880
		internal static ProcessorArchitecture CalculateProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm, AssemblyNameFlags flags)
		{
			if ((flags & (AssemblyNameFlags)240) == (AssemblyNameFlags)112)
			{
				return ProcessorArchitecture.None;
			}
			if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
			{
				if (ifm != ImageFileMachine.I386)
				{
					if (ifm == ImageFileMachine.IA64)
					{
						return ProcessorArchitecture.IA64;
					}
					if (ifm == ImageFileMachine.AMD64)
					{
						return ProcessorArchitecture.Amd64;
					}
				}
				else if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
			}
			else if (ifm == ImageFileMachine.I386)
			{
				if ((pek & PortableExecutableKinds.Required32Bit) == PortableExecutableKinds.Required32Bit)
				{
					return ProcessorArchitecture.X86;
				}
				if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
				return ProcessorArchitecture.X86;
			}
			else if (ifm == ImageFileMachine.ARM)
			{
				return ProcessorArchitecture.Arm;
			}
			return ProcessorArchitecture.None;
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x001826EB File Offset: 0x001818EB
		public AssemblyName()
		{
			this._versionCompatibility = AssemblyVersionCompatibility.SameMachine;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x001826FA File Offset: 0x001818FA
		// (set) Token: 0x060048D9 RID: 18649 RVA: 0x00182702 File Offset: 0x00181902
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060048DA RID: 18650 RVA: 0x0018270B File Offset: 0x0018190B
		// (set) Token: 0x060048DB RID: 18651 RVA: 0x00182713 File Offset: 0x00181913
		public Version Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x0018271C File Offset: 0x0018191C
		// (set) Token: 0x060048DD RID: 18653 RVA: 0x00182724 File Offset: 0x00181924
		public CultureInfo CultureInfo
		{
			get
			{
				return this._cultureInfo;
			}
			set
			{
				this._cultureInfo = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x0018272D File Offset: 0x0018192D
		// (set) Token: 0x060048DF RID: 18655 RVA: 0x00182740 File Offset: 0x00181940
		public string CultureName
		{
			get
			{
				CultureInfo cultureInfo = this._cultureInfo;
				if (cultureInfo == null)
				{
					return null;
				}
				return cultureInfo.Name;
			}
			set
			{
				this._cultureInfo = ((value == null) ? null : new CultureInfo(value));
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x00182754 File Offset: 0x00181954
		// (set) Token: 0x060048E1 RID: 18657 RVA: 0x0018275C File Offset: 0x0018195C
		public string CodeBase
		{
			get
			{
				return this._codeBase;
			}
			set
			{
				this._codeBase = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x00182765 File Offset: 0x00181965
		public string EscapedCodeBase
		{
			get
			{
				if (this._codeBase == null)
				{
					return null;
				}
				return AssemblyName.EscapeCodeBase(this._codeBase);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0018277C File Offset: 0x0018197C
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x0018279C File Offset: 0x0018199C
		public ProcessorArchitecture ProcessorArchitecture
		{
			get
			{
				int num = (int)((this._flags & (AssemblyNameFlags)112) >> 4);
				if (num > 5)
				{
					num = 0;
				}
				return (ProcessorArchitecture)num;
			}
			set
			{
				int num = (int)(value & (ProcessorArchitecture)7);
				if (num <= 5)
				{
					this._flags = (AssemblyNameFlags)((long)this._flags & (long)((ulong)-241));
					this._flags |= (AssemblyNameFlags)(num << 4);
				}
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x001827D8 File Offset: 0x001819D8
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x001827FC File Offset: 0x001819FC
		public AssemblyContentType ContentType
		{
			get
			{
				int num = (int)((this._flags & (AssemblyNameFlags)3584) >> 9);
				if (num > 1)
				{
					num = 0;
				}
				return (AssemblyContentType)num;
			}
			set
			{
				int num = (int)(value & (AssemblyContentType)7);
				if (num <= 1)
				{
					this._flags = (AssemblyNameFlags)((long)this._flags & (long)((ulong)-3585));
					this._flags |= (AssemblyNameFlags)(num << 9);
				}
			}
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x00182838 File Offset: 0x00181A38
		[NullableContext(1)]
		public object Clone()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName._name = this._name;
			byte[] publicKey = this._publicKey;
			assemblyName._publicKey = (byte[])((publicKey != null) ? publicKey.Clone() : null);
			byte[] publicKeyToken = this._publicKeyToken;
			assemblyName._publicKeyToken = (byte[])((publicKeyToken != null) ? publicKeyToken.Clone() : null);
			assemblyName._cultureInfo = this._cultureInfo;
			Version version = this._version;
			assemblyName._version = (Version)((version != null) ? version.Clone() : null);
			assemblyName._flags = this._flags;
			assemblyName._codeBase = this._codeBase;
			assemblyName._hashAlgorithm = this._hashAlgorithm;
			assemblyName._versionCompatibility = this._versionCompatibility;
			return assemblyName;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x001828EB File Offset: 0x00181AEB
		[NullableContext(1)]
		public static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return AssemblyName.GetFileInformationCore(assemblyFile);
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00182901 File Offset: 0x00181B01
		public byte[] GetPublicKey()
		{
			return this._publicKey;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x00182909 File Offset: 0x00181B09
		public void SetPublicKey(byte[] publicKey)
		{
			this._publicKey = publicKey;
			if (publicKey == null)
			{
				this._flags &= ~AssemblyNameFlags.PublicKey;
				return;
			}
			this._flags |= AssemblyNameFlags.PublicKey;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00182934 File Offset: 0x00181B34
		public byte[] GetPublicKeyToken()
		{
			byte[] result;
			if ((result = this._publicKeyToken) == null)
			{
				result = (this._publicKeyToken = this.ComputePublicKeyToken());
			}
			return result;
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x0018295A File Offset: 0x00181B5A
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this._publicKeyToken = publicKeyToken;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00182963 File Offset: 0x00181B63
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x00182971 File Offset: 0x00181B71
		public AssemblyNameFlags Flags
		{
			get
			{
				return this._flags & (AssemblyNameFlags)(-3825);
			}
			set
			{
				this._flags &= (AssemblyNameFlags)3824;
				this._flags |= (value & (AssemblyNameFlags)(-3825));
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00182999 File Offset: 0x00181B99
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x001829A1 File Offset: 0x00181BA1
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this._hashAlgorithm;
			}
			set
			{
				this._hashAlgorithm = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x001829AA File Offset: 0x00181BAA
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x001829B2 File Offset: 0x00181BB2
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this._versionCompatibility;
			}
			set
			{
				this._versionCompatibility = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x001829BB File Offset: 0x00181BBB
		// (set) Token: 0x060048F4 RID: 18676 RVA: 0x001829C3 File Offset: 0x00181BC3
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this._strongNameKeyPair;
			}
			set
			{
				this._strongNameKeyPair = value;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x001829CC File Offset: 0x00181BCC
		[Nullable(1)]
		public string FullName
		{
			[NullableContext(1)]
			get
			{
				if (this.Name == null)
				{
					return string.Empty;
				}
				byte[] pkt = this._publicKeyToken ?? this.ComputePublicKeyToken();
				return AssemblyNameFormatter.ComputeDisplayName(this.Name, this.Version, this.CultureName, pkt, this.Flags, this.ContentType);
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00182A1C File Offset: 0x00181C1C
		[NullableContext(1)]
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x000B3617 File Offset: 0x000B2817
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x000B3617 File Offset: 0x000B2817
		public void OnDeserialization(object sender)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00182A3C File Offset: 0x00181C3C
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			if (reference == definition)
			{
				return true;
			}
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			string text = reference.Name ?? string.Empty;
			string value = definition.Name ?? string.Empty;
			return text.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x00182A94 File Offset: 0x00181C94
		internal static string EscapeCodeBase(string codebase)
		{
			if (codebase == null)
			{
				return string.Empty;
			}
			int length = 0;
			char[] array = AssemblyName.EscapeString(codebase, 0, codebase.Length, null, ref length, true, char.MaxValue, char.MaxValue, char.MaxValue);
			if (array == null)
			{
				return codebase;
			}
			return new string(array, 0, length);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x00182ADC File Offset: 0x00181CDC
		internal unsafe static char[] EscapeString(string input, int start, int end, char[] dest, ref int destPos, bool isUriString, char force1, char force2, char rsvd)
		{
			int i = start;
			int num = start;
			byte* ptr = stackalloc byte[(UIntPtr)160];
			char* ptr2;
			if (input == null)
			{
				ptr2 = null;
			}
			else
			{
				fixed (char* ptr3 = input.GetPinnableReference())
				{
					ptr2 = ptr3;
				}
			}
			char* ptr4 = ptr2;
			while (i < end)
			{
				char c = ptr4[i];
				if (c > '\u007f')
				{
					short num2 = (short)Math.Min(end - i, 39);
					short num3 = 1;
					while (num3 < num2 && ptr4[i + (int)num3] > '\u007f')
					{
						num3 += 1;
					}
					if (ptr4[i + (int)num3 - 1] >= '\ud800' && ptr4[i + (int)num3 - 1] <= '\udbff')
					{
						if (num3 == 1 || (int)num3 == end - i)
						{
							throw new FormatException(SR.Arg_FormatException);
						}
						num3 += 1;
					}
					dest = AssemblyName.EnsureDestinationSize(ptr4, dest, i, num3 * 4 * 3, 480, ref destPos, num);
					short num4 = (short)Encoding.UTF8.GetBytes(ptr4 + i, (int)num3, ptr, 160);
					if (num4 == 0)
					{
						throw new FormatException(SR.Arg_FormatException);
					}
					i += (int)(num3 - 1);
					for (num3 = 0; num3 < num4; num3 += 1)
					{
						AssemblyName.EscapeAsciiChar((char)ptr[num3], dest, ref destPos);
					}
					num = i + 1;
				}
				else if (c == '%' && rsvd == '%')
				{
					dest = AssemblyName.EnsureDestinationSize(ptr4, dest, i, 3, 120, ref destPos, num);
					if (i + 2 < end && HexConverter.IsHexChar((int)ptr4[i + 1]) && HexConverter.IsHexChar((int)ptr4[i + 2]))
					{
						char[] array = dest;
						int num5 = destPos;
						destPos = num5 + 1;
						array[num5] = 37;
						char[] array2 = dest;
						num5 = destPos;
						destPos = num5 + 1;
						array2[num5] = ptr4[i + 1];
						char[] array3 = dest;
						num5 = destPos;
						destPos = num5 + 1;
						array3[num5] = ptr4[i + 2];
						i += 2;
					}
					else
					{
						AssemblyName.EscapeAsciiChar('%', dest, ref destPos);
					}
					num = i + 1;
				}
				else if (c == force1 || c == force2 || (c != rsvd && (isUriString ? (!AssemblyName.IsReservedUnreservedOrHash(c)) : (!AssemblyName.IsUnreserved(c)))))
				{
					dest = AssemblyName.EnsureDestinationSize(ptr4, dest, i, 3, 120, ref destPos, num);
					AssemblyName.EscapeAsciiChar(c, dest, ref destPos);
					num = i + 1;
				}
				i++;
			}
			if (num != i && (num != start || dest != null))
			{
				dest = AssemblyName.EnsureDestinationSize(ptr4, dest, i, 0, 0, ref destPos, num);
			}
			char* ptr3 = null;
			return dest;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00182D1C File Offset: 0x00181F1C
		private unsafe static char[] EnsureDestinationSize(char* pStr, char[] dest, int currentInputPos, short charsToAdd, short minReallocateChars, ref int destPos, int prevInputPos)
		{
			if (dest == null || dest.Length < destPos + (currentInputPos - prevInputPos) + (int)charsToAdd)
			{
				char[] array = new char[destPos + (currentInputPos - prevInputPos) + (int)minReallocateChars];
				if (dest != null && destPos != 0)
				{
					Buffer.BlockCopy(dest, 0, array, 0, destPos << 1);
				}
				dest = array;
			}
			while (prevInputPos != currentInputPos)
			{
				char[] array2 = dest;
				int num = destPos;
				destPos = num + 1;
				array2[num] = pStr[prevInputPos++];
			}
			return dest;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x00182D88 File Offset: 0x00181F88
		internal static void EscapeAsciiChar(char ch, char[] to, ref int pos)
		{
			int num = pos;
			pos = num + 1;
			to[num] = '%';
			num = pos;
			pos = num + 1;
			to[num] = HexConverter.ToCharUpper((int)(ch >> 4));
			num = pos;
			pos = num + 1;
			to[num] = HexConverter.ToCharUpper((int)ch);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00182DC6 File Offset: 0x00181FC6
		private static bool IsReservedUnreservedOrHash(char c)
		{
			return AssemblyName.IsUnreserved(c) || ":/?#[]@!$&'()*+,;=".Contains(c);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00182DDD File Offset: 0x00181FDD
		internal static bool IsUnreserved(char c)
		{
			return AssemblyName.IsAsciiLetterOrDigit(c) || "-._~".Contains(c);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00182DF4 File Offset: 0x00181FF4
		internal static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x00182E11 File Offset: 0x00182011
		internal static bool IsAsciiLetterOrDigit(char character)
		{
			return AssemblyName.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x040011BC RID: 4540
		private string _name;

		// Token: 0x040011BD RID: 4541
		private byte[] _publicKey;

		// Token: 0x040011BE RID: 4542
		private byte[] _publicKeyToken;

		// Token: 0x040011BF RID: 4543
		private CultureInfo _cultureInfo;

		// Token: 0x040011C0 RID: 4544
		private string _codeBase;

		// Token: 0x040011C1 RID: 4545
		private Version _version;

		// Token: 0x040011C2 RID: 4546
		private StrongNameKeyPair _strongNameKeyPair;

		// Token: 0x040011C3 RID: 4547
		private AssemblyHashAlgorithm _hashAlgorithm;

		// Token: 0x040011C4 RID: 4548
		private AssemblyVersionCompatibility _versionCompatibility;

		// Token: 0x040011C5 RID: 4549
		private AssemblyNameFlags _flags;

		// Token: 0x040011C6 RID: 4550
		internal const char c_DummyChar = '￿';

		// Token: 0x040011C7 RID: 4551
		private const short c_MaxAsciiCharsReallocate = 40;

		// Token: 0x040011C8 RID: 4552
		private const short c_MaxUnicodeCharsReallocate = 40;

		// Token: 0x040011C9 RID: 4553
		private const short c_MaxUTF_8BytesPerUnicodeChar = 4;

		// Token: 0x040011CA RID: 4554
		private const short c_EncodedCharsPerByte = 3;

		// Token: 0x040011CB RID: 4555
		private const string RFC3986ReservedMarks = ":/?#[]@!$&'()*+,;=";

		// Token: 0x040011CC RID: 4556
		private const string RFC3986UnreservedMarks = "-._~";
	}
}
