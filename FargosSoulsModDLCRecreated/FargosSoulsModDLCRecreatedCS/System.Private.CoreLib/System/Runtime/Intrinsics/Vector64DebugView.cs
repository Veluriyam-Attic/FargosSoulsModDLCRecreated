using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000419 RID: 1049
	internal readonly struct Vector64DebugView<T> where T : struct
	{
		// Token: 0x0600348B RID: 13451 RVA: 0x001711CE File Offset: 0x001703CE
		public Vector64DebugView(Vector64<T> value)
		{
			this._value = value;
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x001711D8 File Offset: 0x001703D8
		public byte[] ByteView
		{
			get
			{
				byte[] array = new byte[8];
				Unsafe.WriteUnaligned<Vector64<T>>(ref array[0], this._value);
				return array;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x00171200 File Offset: 0x00170400
		public double[] DoubleView
		{
			get
			{
				double[] array = new double[1];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<double, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x0017122C File Offset: 0x0017042C
		public short[] Int16View
		{
			get
			{
				short[] array = new short[4];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<short, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x00171258 File Offset: 0x00170458
		public int[] Int32View
		{
			get
			{
				int[] array = new int[2];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<int, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x00171284 File Offset: 0x00170484
		public long[] Int64View
		{
			get
			{
				long[] array = new long[1];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<long, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x001712B0 File Offset: 0x001704B0
		public sbyte[] SByteView
		{
			get
			{
				sbyte[] array = new sbyte[8];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<sbyte, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x001712DC File Offset: 0x001704DC
		public float[] SingleView
		{
			get
			{
				float[] array = new float[2];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<float, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x00171308 File Offset: 0x00170508
		public ushort[] UInt16View
		{
			get
			{
				ushort[] array = new ushort[4];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<ushort, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x00171334 File Offset: 0x00170534
		public uint[] UInt32View
		{
			get
			{
				uint[] array = new uint[2];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<uint, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x00171360 File Offset: 0x00170560
		public ulong[] UInt64View
		{
			get
			{
				ulong[] array = new ulong[1];
				Unsafe.WriteUnaligned<Vector64<T>>(Unsafe.As<ulong, byte>(ref array[0]), this._value);
				return array;
			}
		}

		// Token: 0x04000E82 RID: 3714
		private readonly Vector64<T> _value;
	}
}
