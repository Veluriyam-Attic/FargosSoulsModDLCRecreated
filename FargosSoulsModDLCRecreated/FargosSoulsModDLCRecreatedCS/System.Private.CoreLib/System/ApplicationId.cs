using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020000C4 RID: 196
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ApplicationId
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x000C84EC File Offset: 0x000C76EC
		public ApplicationId(byte[] publicKeyToken, string name, Version version, [Nullable(2)] string processorArchitecture, [Nullable(2)] string culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyApplicationName);
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (publicKeyToken == null)
			{
				throw new ArgumentNullException("publicKeyToken");
			}
			this._publicKeyToken = (byte[])publicKeyToken.Clone();
			this.Name = name;
			this.Version = version;
			this.ProcessorArchitecture = processorArchitecture;
			this.Culture = culture;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000C8571 File Offset: 0x000C7771
		[Nullable(2)]
		public string Culture { [NullableContext(2)] get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000C8579 File Offset: 0x000C7779
		public string Name { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000C8581 File Offset: 0x000C7781
		[Nullable(2)]
		public string ProcessorArchitecture { [NullableContext(2)] get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000C8589 File Offset: 0x000C7789
		public Version Version { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000C8591 File Offset: 0x000C7791
		public byte[] PublicKeyToken
		{
			get
			{
				return (byte[])this._publicKeyToken.Clone();
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000C85A3 File Offset: 0x000C77A3
		public ApplicationId Copy()
		{
			return new ApplicationId(this._publicKeyToken, this.Name, this.Version, this.ProcessorArchitecture, this.Culture);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000C85C8 File Offset: 0x000C77C8
		public unsafe override string ToString()
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.Append(this.Name);
			if (this.Culture != null)
			{
				valueStringBuilder.Append(", culture=\"");
				valueStringBuilder.Append(this.Culture);
				valueStringBuilder.Append('"');
			}
			valueStringBuilder.Append(", version=\"");
			valueStringBuilder.Append(this.Version.ToString());
			valueStringBuilder.Append('"');
			if (this._publicKeyToken != null)
			{
				valueStringBuilder.Append(", publicKeyToken=\"");
				ApplicationId.EncodeHexString(this._publicKeyToken, ref valueStringBuilder);
				valueStringBuilder.Append('"');
			}
			if (this.ProcessorArchitecture != null)
			{
				valueStringBuilder.Append(", processorArchitecture =\"");
				valueStringBuilder.Append(this.ProcessorArchitecture);
				valueStringBuilder.Append('"');
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000C86B0 File Offset: 0x000C78B0
		private static void EncodeHexString(byte[] sArray, ref ValueStringBuilder stringBuilder)
		{
			for (int i = 0; i < sArray.Length; i++)
			{
				HexConverter.ToCharsBuffer(sArray[i], stringBuilder.AppendSpan(2), 0, HexConverter.Casing.Upper);
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000C86DC File Offset: 0x000C78DC
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			ApplicationId applicationId = o as ApplicationId;
			if (applicationId == null)
			{
				return false;
			}
			if (!object.Equals(this.Name, applicationId.Name) || !object.Equals(this.Version, applicationId.Version) || !object.Equals(this.ProcessorArchitecture, applicationId.ProcessorArchitecture) || !object.Equals(this.Culture, applicationId.Culture))
			{
				return false;
			}
			if (this._publicKeyToken.Length != applicationId._publicKeyToken.Length)
			{
				return false;
			}
			for (int i = 0; i < this._publicKeyToken.Length; i++)
			{
				if (this._publicKeyToken[i] != applicationId._publicKeyToken[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000C877F File Offset: 0x000C797F
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}

		// Token: 0x0400026E RID: 622
		private readonly byte[] _publicKeyToken;
	}
}
