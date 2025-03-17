using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000416 RID: 1046
	internal readonly struct Vector256DebugView<T> where T : struct
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x00170540 File Offset: 0x0016F740
		public Vector256DebugView(Vector256<T> value)
		{
			this._value = value;
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x0017054C File Offset: 0x0016F74C
		public byte[] ByteView
		{
			get
			{
				byte[] array = new byte[32];
				Unsafe.WriteUnaligned<Vector256<T>>(ref array[0], this._value);
				return array;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x00170574 File Offset: 0x0016F774
		public double[] DoubleView
		{
			get
			{
				double[] array = new double[4];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<double, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x001705A0 File Offset: 0x0016F7A0
		public short[] Int16View
		{
			get
			{
				short[] array = new short[16];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<short, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x001705D0 File Offset: 0x0016F7D0
		public int[] Int32View
		{
			get
			{
				int[] array = new int[8];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<int, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x001705FC File Offset: 0x0016F7FC
		public long[] Int64View
		{
			get
			{
				long[] array = new long[4];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<long, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x00170628 File Offset: 0x0016F828
		public sbyte[] SByteView
		{
			get
			{
				sbyte[] array = new sbyte[32];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<sbyte, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x00170658 File Offset: 0x0016F858
		public float[] SingleView
		{
			get
			{
				float[] array = new float[8];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<float, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x00170684 File Offset: 0x0016F884
		public ushort[] UInt16View
		{
			get
			{
				ushort[] array = new ushort[16];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<ushort, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x001706B4 File Offset: 0x0016F8B4
		public uint[] UInt32View
		{
			get
			{
				uint[] array = new uint[8];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<uint, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x001706E0 File Offset: 0x0016F8E0
		public ulong[] UInt64View
		{
			get
			{
				ulong[] array = new ulong[4];
				Unsafe.WriteUnaligned<Vector256<T>>(Unsafe.As<ulong, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x04000E80 RID: 3712
		private readonly Vector256<T> _value;
	}
}
