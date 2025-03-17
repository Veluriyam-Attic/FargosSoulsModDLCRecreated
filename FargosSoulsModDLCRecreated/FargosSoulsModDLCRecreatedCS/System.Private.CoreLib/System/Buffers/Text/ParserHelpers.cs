using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Internal.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x0200025A RID: 602
	internal static class ParserHelpers
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x000D1D8D File Offset: 0x000D0F8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDigit(int i)
		{
			return i - 48 <= 9;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0013A364 File Offset: 0x00139564
		public static bool TryParseThrowFormatException(out int bytesConsumed)
		{
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0013BC03 File Offset: 0x0013AE03
		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed) where T : struct
		{
			value = default(T);
			return ParserHelpers.TryParseThrowFormatException(out bytesConsumed);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0013BC12 File Offset: 0x0013AE12
		[DoesNotReturn]
		[StackTraceHidden]
		public static bool TryParseThrowFormatException<T>(ReadOnlySpan<byte> source, out T value, out int bytesConsumed) where T : struct
		{
			Unsafe.SkipInit<T>(out value);
			Unsafe.SkipInit<int>(out bytesConsumed);
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}
	}
}
