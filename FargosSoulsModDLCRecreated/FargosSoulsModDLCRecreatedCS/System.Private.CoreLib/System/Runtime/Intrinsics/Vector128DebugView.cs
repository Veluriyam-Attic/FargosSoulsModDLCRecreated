using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000413 RID: 1043
	internal readonly struct Vector128DebugView<T> where T : struct
	{
		// Token: 0x060033A4 RID: 13220 RVA: 0x0016E8E0 File Offset: 0x0016DAE0
		public Vector128DebugView(Vector128<T> value)
		{
			this._value = value;
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x0016E8EC File Offset: 0x0016DAEC
		public byte[] ByteView
		{
			get
			{
				byte[] array = new byte[16];
				Unsafe.WriteUnaligned<Vector128<T>>(ref array[0], this._value);
				return array;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x0016E914 File Offset: 0x0016DB14
		public double[] DoubleView
		{
			get
			{
				double[] array = new double[2];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<double, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x0016E940 File Offset: 0x0016DB40
		public short[] Int16View
		{
			get
			{
				short[] array = new short[8];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<short, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x0016E96C File Offset: 0x0016DB6C
		public int[] Int32View
		{
			get
			{
				int[] array = new int[4];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<int, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x0016E998 File Offset: 0x0016DB98
		public long[] Int64View
		{
			get
			{
				long[] array = new long[2];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<long, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x0016E9C4 File Offset: 0x0016DBC4
		public sbyte[] SByteView
		{
			get
			{
				sbyte[] array = new sbyte[16];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<sbyte, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x0016E9F4 File Offset: 0x0016DBF4
		public float[] SingleView
		{
			get
			{
				float[] array = new float[4];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<float, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x0016EA20 File Offset: 0x0016DC20
		public ushort[] UInt16View
		{
			get
			{
				ushort[] array = new ushort[8];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<ushort, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x0016EA4C File Offset: 0x0016DC4C
		public uint[] UInt32View
		{
			get
			{
				uint[] array = new uint[4];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<uint, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x0016EA78 File Offset: 0x0016DC78
		public ulong[] UInt64View
		{
			get
			{
				ulong[] array = new ulong[2];
				Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<ulong, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x04000E7B RID: 3707
		private readonly Vector128<T> _value;
	}
}
