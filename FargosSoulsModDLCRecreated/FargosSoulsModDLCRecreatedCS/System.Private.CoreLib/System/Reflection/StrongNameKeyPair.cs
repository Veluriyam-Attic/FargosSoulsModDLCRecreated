using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000607 RID: 1543
	[NullableContext(1)]
	[Nullable(0)]
	public class StrongNameKeyPair : IDeserializationCallback, ISerializable
	{
		// Token: 0x06004E05 RID: 19973 RVA: 0x0018CC84 File Offset: 0x0018BE84
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			int num = (int)keyPairFile.Length;
			byte[] buffer = new byte[num];
			keyPairFile.Read(buffer, 0, num);
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0018CCBE File Offset: 0x0018BEBE
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x0018CCD4 File Offset: 0x0018BED4
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0018CCE1 File Offset: 0x0018BEE1
		public StrongNameKeyPair(string keyPairContainer)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_StrongNameSigning);
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0018CCF3 File Offset: 0x0018BEF3
		public byte[] PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException(SR.PlatformNotSupported_StrongNameSigning);
			}
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x000B3617 File Offset: 0x000B2817
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x000B3617 File Offset: 0x000B2817
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
