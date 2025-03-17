using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000412 RID: 1042
	[Intrinsic]
	[DebuggerDisplay("{DisplayString,nq}")]
	[DebuggerTypeProxy(typeof(Vector128DebugView<>))]
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	public readonly struct Vector128<T> : IEquatable<Vector128<T>> where T : struct
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x0016E516 File Offset: 0x0016D716
		public static int Count
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return 16 / Unsafe.SizeOf<T>();
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x0016E528 File Offset: 0x0016D728
		public static Vector128<T> Zero
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return default(Vector128<T>);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x0016E543 File Offset: 0x0016D743
		public static Vector128<T> AllBitsSet
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return Vector128.Create(uint.MaxValue).As<uint, T>();
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x0016E555 File Offset: 0x0016D755
		[Nullable(1)]
		internal string DisplayString
		{
			get
			{
				if (Vector128<T>.IsSupported)
				{
					return this.ToString();
				}
				return SR.NotSupported_Type;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x0016E570 File Offset: 0x0016D770
		internal static bool IsSupported
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(float) || typeof(T) == typeof(double);
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x0016E69C File Offset: 0x0016D89C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector128<T> other)
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (Sse.IsSupported && typeof(T) == typeof(float))
			{
				Vector128<float> value = Sse.CompareEqual(this.AsSingle<T>(), other.AsSingle<T>());
				return Sse.MoveMask(value) == 15;
			}
			if (!Sse2.IsSupported)
			{
				return Vector128<T>.<Equals>g__SoftwareFallback|12_0(this, other);
			}
			if (typeof(T) == typeof(double))
			{
				Vector128<double> value2 = Sse2.CompareEqual(this.AsDouble<T>(), other.AsDouble<T>());
				return Sse2.MoveMask(value2) == 3;
			}
			Vector128<byte> value3 = Sse2.CompareEqual(this.AsByte<T>(), other.AsByte<T>());
			return Sse2.MoveMask(value3) == 65535;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0016E761 File Offset: 0x0016D961
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is Vector128<T> && this.Equals((Vector128<T>)obj);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x0016E77C File Offset: 0x0016D97C
		public override int GetHashCode()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			HashCode hashCode = default(HashCode);
			for (int i = 0; i < Vector128<T>.Count; i++)
			{
				T element = this.GetElement(i);
				hashCode.Add<int>(element.GetHashCode());
			}
			return hashCode.ToHashCode();
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x0016E7D0 File Offset: 0x0016D9D0
		[NullableContext(1)]
		public unsafe override string ToString()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			int num = Vector128<T>.Count - 1;
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

		// Token: 0x060033A3 RID: 13219 RVA: 0x0016E89C File Offset: 0x0016DA9C
		[CompilerGenerated]
		internal static bool <Equals>g__SoftwareFallback|12_0(in Vector128<T> vector, Vector128<T> other)
		{
			for (int i = 0; i < Vector128<T>.Count; i++)
			{
				if (!((IEquatable<T>)((object)vector.GetElement(i))).Equals(other.GetElement(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000E79 RID: 3705
		private readonly ulong _00;

		// Token: 0x04000E7A RID: 3706
		private readonly ulong _01;
	}
}
