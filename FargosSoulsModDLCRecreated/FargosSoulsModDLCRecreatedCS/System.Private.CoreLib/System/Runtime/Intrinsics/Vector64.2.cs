using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000418 RID: 1048
	[Intrinsic]
	[DebuggerDisplay("{DisplayString,nq}")]
	[DebuggerTypeProxy(typeof(Vector64DebugView<>))]
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	public readonly struct Vector64<T> : IEquatable<Vector64<T>> where T : struct
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x00170FF5 File Offset: 0x001701F5
		public static int Count
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return 8 / Unsafe.SizeOf<T>();
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x00171004 File Offset: 0x00170204
		public static Vector64<T> Zero
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return default(Vector64<T>);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x0017101F File Offset: 0x0017021F
		public static Vector64<T> AllBitsSet
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return Vector64.Create(uint.MaxValue).As<uint, T>();
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x00171031 File Offset: 0x00170231
		[Nullable(1)]
		internal string DisplayString
		{
			get
			{
				if (Vector64<T>.IsSupported)
				{
					return this.ToString();
				}
				return SR.NotSupported_Type;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x0016E570 File Offset: 0x0016D770
		internal static bool IsSupported
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(float) || typeof(T) == typeof(double);
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x0017104C File Offset: 0x0017024C
		public bool Equals(Vector64<T> other)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			for (int i = 0; i < Vector64<T>.Count; i++)
			{
				if (!((IEquatable<T>)((object)this.GetElement(i))).Equals(other.GetElement(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x00171095 File Offset: 0x00170295
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is Vector64<T> && this.Equals((Vector64<T>)obj);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x001710B0 File Offset: 0x001702B0
		public override int GetHashCode()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			HashCode hashCode = default(HashCode);
			for (int i = 0; i < Vector64<T>.Count; i++)
			{
				T element = this.GetElement(i);
				hashCode.Add<int>(element.GetHashCode());
			}
			return hashCode.ToHashCode();
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00171104 File Offset: 0x00170304
		[NullableContext(1)]
		public unsafe override string ToString()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			int num = Vector64<T>.Count - 1;
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			valueStringBuilder.Append('<');
			for (int i = 0; i < num; i++)
			{
				valueStringBuilder.Append(((IFormattable)((object)this.GetElement(i))).ToString("G", invariantCulture));
				valueStringBuilder.Append(',');
				valueStringBuilder.Append(' ');
			}
			valueStringBuilder.Append(((IFormattable)((object)this.GetElement(num))).ToString("G", invariantCulture));
			valueStringBuilder.Append('>');
			return valueStringBuilder.ToString();
		}

		// Token: 0x04000E81 RID: 3713
		private readonly ulong _00;
	}
}
