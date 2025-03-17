using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200042D RID: 1069
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Aes : Sse2
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x001713EF File Offset: 0x001705EF
		public new static bool IsSupported
		{
			get
			{
				return Aes.IsSupported;
			}
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x001713F6 File Offset: 0x001705F6
		public static Vector128<byte> Decrypt(Vector128<byte> value, Vector128<byte> roundKey)
		{
			return Aes.Decrypt(value, roundKey);
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x001713FF File Offset: 0x001705FF
		public static Vector128<byte> DecryptLast(Vector128<byte> value, Vector128<byte> roundKey)
		{
			return Aes.DecryptLast(value, roundKey);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00171408 File Offset: 0x00170608
		public static Vector128<byte> Encrypt(Vector128<byte> value, Vector128<byte> roundKey)
		{
			return Aes.Encrypt(value, roundKey);
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00171411 File Offset: 0x00170611
		public static Vector128<byte> EncryptLast(Vector128<byte> value, Vector128<byte> roundKey)
		{
			return Aes.EncryptLast(value, roundKey);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x0017141A File Offset: 0x0017061A
		public static Vector128<byte> InverseMixColumns(Vector128<byte> value)
		{
			return Aes.InverseMixColumns(value);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00171422 File Offset: 0x00170622
		public static Vector128<byte> KeygenAssist(Vector128<byte> value, byte control)
		{
			return Aes.KeygenAssist(value, control);
		}

		// Token: 0x0200042E RID: 1070
		[Intrinsic]
		public new abstract class X64 : Sse2.X64
		{
			// Token: 0x17000A22 RID: 2594
			// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x0017142B File Offset: 0x0017062B
			public new static bool IsSupported
			{
				get
				{
					return Aes.X64.IsSupported;
				}
			}
		}
	}
}
