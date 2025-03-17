using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace Internal.Runtime.CompilerServices
{
	// Token: 0x02000817 RID: 2071
	[NullableContext(1)]
	[Nullable(0)]
	[CLSCompliant(false)]
	public static class Unsafe
	{
		// Token: 0x06006254 RID: 25172 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		[NullableContext(0)]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AsPointer<[Nullable(2)] T>([Nullable(1)] ref T value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NullableContext(2)]
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SizeOf<T>()
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x000B3617 File Offset: 0x000B2817
		[NonVersionable]
		[Intrinsic]
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(1)]
		[return: NotNullIfNotNull("value")]
		public static T As<T>(object value) where T : class
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref TTo As<[Nullable(2)] TFrom, [Nullable(2)] TTo>(ref TFrom source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Add<[Nullable(2)] T>(ref T source, int elementOffset)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T Add<[Nullable(2)] T>(ref T source, IntPtr elementOffset)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NullableContext(0)]
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* Add<[Nullable(2)] T>(void* source, int elementOffset)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x001D3EE7 File Offset: 0x001D30E7
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T AddByteOffset<T>(ref T source, [NativeInteger] UIntPtr byteOffset)
		{
			return Unsafe.AddByteOffset<T>(ref source, (IntPtr)byteOffset);
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x000B3617 File Offset: 0x000B2817
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AreSame<[Nullable(2)] T>([AllowNull] ref T left, [AllowNull] ref T right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAddressGreaterThan<[Nullable(2)] T>([AllowNull] ref T left, [AllowNull] ref T right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAddressLessThan<[Nullable(2)] T>([AllowNull] ref T left, [AllowNull] ref T right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001D3EF8 File Offset: 0x001D30F8
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			for (uint num = 0U; num < byteCount; num += 1U)
			{
				*Unsafe.AddByteOffset<byte>(ref startAddress, (UIntPtr)num) = value;
			}
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NullableContext(0)]
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(1)]
		public unsafe static T ReadUnaligned<[Nullable(2)] T>(void* source)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T ReadUnaligned<[Nullable(2)] T>(ref byte source)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[NonVersionable]
		[Intrinsic]
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteUnaligned<[Nullable(2)] T>(void* destination, [Nullable(1)] T value)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x001D3ED0 File Offset: 0x001D30D0
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUnaligned<[Nullable(2)] T>(ref byte destination, T value)
		{
			typeof(T).ToString();
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x000B3617 File Offset: 0x000B2817
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AddByteOffset<[Nullable(2)] T>(ref T source, IntPtr byteOffset)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x001D3F1B File Offset: 0x001D311B
		[NonVersionable]
		[Intrinsic]
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(1)]
		public unsafe static T Read<[Nullable(2)] T>(void* source)
		{
			return *Unsafe.As<byte, T>(ref *(byte*)source);
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x001D3F1B File Offset: 0x001D311B
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T Read<[Nullable(2)] T>(ref byte source)
		{
			return *Unsafe.As<byte, T>(ref source);
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x001D3F28 File Offset: 0x001D3128
		[Intrinsic]
		[NullableContext(0)]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Write<[Nullable(2)] T>(void* destination, [Nullable(1)] T value)
		{
			*Unsafe.As<byte, T>(ref *(byte*)destination) = value;
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x001D3F28 File Offset: 0x001D3128
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Write<[Nullable(2)] T>(ref byte destination, T value)
		{
			*Unsafe.As<byte, T>(ref destination) = value;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x001D3F36 File Offset: 0x001D3136
		[NullableContext(0)]
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(1)]
		public unsafe static ref T AsRef<[Nullable(2)] T>(void* source)
		{
			return Unsafe.As<byte, T>(ref *(byte*)source);
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x000B3617 File Offset: 0x000B2817
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<[Nullable(2)] T>(in T source)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x000B3617 File Offset: 0x000B2817
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IntPtr ByteOffset<[Nullable(2)] T>([AllowNull] ref T origin, [AllowNull] ref T target)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x001D3F3E File Offset: 0x001D313E
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T NullRef<[Nullable(2)] T>()
		{
			return Unsafe.AsRef<T>(null);
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x001D3F47 File Offset: 0x001D3147
		[Intrinsic]
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNullRef<[Nullable(2)] T>(ref T source)
		{
			return Unsafe.AsPointer<T>(ref source) == null;
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x000B3617 File Offset: 0x000B2817
		[NonVersionable]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SkipInit<[Nullable(2)] T>(out T value)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
