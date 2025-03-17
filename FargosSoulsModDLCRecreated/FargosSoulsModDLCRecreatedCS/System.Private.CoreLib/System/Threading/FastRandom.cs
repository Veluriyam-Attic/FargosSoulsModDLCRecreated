using System;

namespace System.Threading
{
	// Token: 0x020002C7 RID: 711
	internal struct FastRandom
	{
		// Token: 0x060028BB RID: 10427 RVA: 0x00149DC1 File Offset: 0x00148FC1
		public FastRandom(int seed)
		{
			this._x = (uint)seed;
			this._w = 88675123U;
			this._y = 362436069U;
			this._z = 521288629U;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x00149DEC File Offset: 0x00148FEC
		public int Next(int maxValue)
		{
			uint num = this._x ^ this._x << 11;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 19 ^ (num ^ num >> 8));
			return (int)(this._w % (uint)maxValue);
		}

		// Token: 0x04000AE9 RID: 2793
		private uint _w;

		// Token: 0x04000AEA RID: 2794
		private uint _x;

		// Token: 0x04000AEB RID: 2795
		private uint _y;

		// Token: 0x04000AEC RID: 2796
		private uint _z;
	}
}
