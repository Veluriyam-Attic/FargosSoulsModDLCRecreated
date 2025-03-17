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
	// Token: 0x02000415 RID: 1045
	[DebuggerDisplay("{DisplayString,nq}")]
	[Intrinsic]
	[DebuggerTypeProxy(typeof(Vector256DebugView<>))]
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	public readonly struct Vector256<T> : IEquatable<Vector256<T>> where T : struct
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x001702A9 File Offset: 0x0016F4A9
		public static int Count
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return 32 / Unsafe.SizeOf<T>();
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x001702B8 File Offset: 0x0016F4B8
		public static Vector256<T> Zero
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return default(Vector256<T>);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x001702D3 File Offset: 0x0016F4D3
		public static Vector256<T> AllBitsSet
		{
			[Intrinsic]
			get
			{
				ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
				return Vector256.Create(uint.MaxValue).As<uint, T>();
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x001702E5 File Offset: 0x0016F4E5
		[Nullable(1)]
		internal string DisplayString
		{
			get
			{
				if (Vector256<T>.IsSupported)
				{
					return this.ToString();
				}
				return SR.NotSupported_Type;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x0016E570 File Offset: 0x0016D770
		internal static bool IsSupported
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort) || typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(float) || typeof(T) == typeof(double);
			}
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00170300 File Offset: 0x0016F500
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector256<T> other)
		{
			if (Avx.IsSupported)
			{
				if (typeof(T) == typeof(float))
				{
					Vector256<float> value = Avx.Compare(this.AsSingle<T>(), other.AsSingle<T>(), FloatComparisonMode.OrderedEqualNonSignaling);
					return Avx.MoveMask(value) == 255;
				}
				if (typeof(T) == typeof(double))
				{
					Vector256<double> value2 = Avx.Compare(this.AsDouble<T>(), other.AsDouble<T>(), FloatComparisonMode.OrderedEqualNonSignaling);
					return Avx.MoveMask(value2) == 15;
				}
			}
			if (Avx2.IsSupported)
			{
				Vector256<byte> value3 = Avx2.CompareEqual(this.AsByte<T>(), other.AsByte<T>());
				return Avx2.MoveMask(value3) == -1;
			}
			return Vector256<T>.<Equals>g__SoftwareFallback|14_0(this, other);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x001703C2 File Offset: 0x0016F5C2
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is Vector256<T> && this.Equals((Vector256<T>)obj);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x001703DC File Offset: 0x0016F5DC
		public override int GetHashCode()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			HashCode hashCode = default(HashCode);
			for (int i = 0; i < Vector256<T>.Count; i++)
			{
				T element = this.GetElement(i);
				hashCode.Add<int>(element.GetHashCode());
			}
			return hashCode.ToHashCode();
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00170430 File Offset: 0x0016F630
		[NullableContext(1)]
		public unsafe override string ToString()
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			int num = Vector256<T>.Count - 1;
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

		// Token: 0x06003429 RID: 13353 RVA: 0x001704FC File Offset: 0x0016F6FC
		[CompilerGenerated]
		internal static bool <Equals>g__SoftwareFallback|14_0(in Vector256<T> vector, Vector256<T> other)
		{
			for (int i = 0; i < Vector256<T>.Count; i++)
			{
				if (!((IEquatable<T>)((object)vector.GetElement(i))).Equals(other.GetElement(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000E7C RID: 3708
		private readonly ulong _00;

		// Token: 0x04000E7D RID: 3709
		private readonly ulong _01;

		// Token: 0x04000E7E RID: 3710
		private readonly ulong _02;

		// Token: 0x04000E7F RID: 3711
		private readonly ulong _03;
	}
}
