using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000172 RID: 370
	public class Random
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x000E7A0B File Offset: 0x000E6C0B
		public Random() : this(Random.GenerateSeed())
		{
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000E7A18 File Offset: 0x000E6C18
		public Random(int Seed)
		{
			int num = 0;
			int num2 = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
			int num3 = 161803398 - num2;
			this._seedArray[55] = num3;
			int num4 = 1;
			for (int i = 1; i < 55; i++)
			{
				if ((num += 21) >= 55)
				{
					num -= 55;
				}
				this._seedArray[num] = num4;
				num4 = num3 - num4;
				if (num4 < 0)
				{
					num4 += int.MaxValue;
				}
				num3 = this._seedArray[num];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					int num5 = k + 30;
					if (num5 >= 55)
					{
						num5 -= 55;
					}
					this._seedArray[k] -= this._seedArray[1 + num5];
					if (this._seedArray[k] < 0)
					{
						this._seedArray[k] += int.MaxValue;
					}
				}
			}
			this._inextp = 21;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000E7B21 File Offset: 0x000E6D21
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000E7B34 File Offset: 0x000E6D34
		private int InternalSample()
		{
			int num = this._inext;
			int num2 = this._inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this._seedArray[num] - this._seedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this._seedArray[num] = num3;
			this._inext = num;
			this._inextp = num2;
			return num3;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000E7BA8 File Offset: 0x000E6DA8
		private static int GenerateSeed()
		{
			Random random = Random.t_threadRandom;
			if (random == null)
			{
				Random obj = Random.s_globalRandom;
				int seed;
				lock (obj)
				{
					seed = Random.s_globalRandom.Next();
				}
				random = new Random(seed);
				Random.t_threadRandom = random;
			}
			return random.Next();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000E7C08 File Offset: 0x000E6E08
		private unsafe static int GenerateGlobalSeed()
		{
			int result;
			Interop.GetRandomBytes((byte*)(&result), 4);
			return result;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000E7C1F File Offset: 0x000E6E1F
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000E7C28 File Offset: 0x000E6E28
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			bool flag = this.InternalSample() % 2 == 0;
			if (flag)
			{
				num = -num;
			}
			double num2 = (double)num;
			num2 += 2147483646.0;
			return num2 / 4294967293.0;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000E7C70 File Offset: 0x000E6E70
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", SR.Format(SR.Argument_MinMaxValue, "minValue", "maxValue"));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000E7CCA File Offset: 0x000E6ECA
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", SR.Format(SR.ArgumentOutOfRange_MustBePositive, "maxValue"));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000E7CF4 File Offset: 0x000E6EF4
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000E7CFC File Offset: 0x000E6EFC
		[NullableContext(1)]
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)this.InternalSample();
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000E7D30 File Offset: 0x000E6F30
		public unsafe virtual void NextBytes(Span<byte> buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				*buffer[i] = (byte)this.Next();
			}
		}

		// Token: 0x04000479 RID: 1145
		private int _inext;

		// Token: 0x0400047A RID: 1146
		private int _inextp;

		// Token: 0x0400047B RID: 1147
		private readonly int[] _seedArray = new int[56];

		// Token: 0x0400047C RID: 1148
		[ThreadStatic]
		private static Random t_threadRandom;

		// Token: 0x0400047D RID: 1149
		private static readonly Random s_globalRandom = new Random(Random.GenerateGlobalSeed());
	}
}
