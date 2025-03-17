using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200027B RID: 635
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		// Token: 0x060026DD RID: 9949 RVA: 0x0014331C File Offset: 0x0014251C
		public AbandonedMutexException() : base(SR.Threading_AbandonedMutexException)
		{
			base.HResult = -2146233043;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0014333B File Offset: 0x0014253B
		public AbandonedMutexException(string message) : base(message)
		{
			base.HResult = -2146233043;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00143356 File Offset: 0x00142556
		public AbandonedMutexException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233043;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00143372 File Offset: 0x00142572
		public AbandonedMutexException(int location, WaitHandle handle) : base(SR.Threading_AbandonedMutexException)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00143399 File Offset: 0x00142599
		public AbandonedMutexException(string message, int location, WaitHandle handle) : base(message)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x001433BC File Offset: 0x001425BC
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle) : base(message, inner)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x001433E1 File Offset: 0x001425E1
		[NullableContext(1)]
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x001433F2 File Offset: 0x001425F2
		private void SetupException(int location, WaitHandle handle)
		{
			this._mutexIndex = location;
			this._mutex = (handle as Mutex);
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00143407 File Offset: 0x00142607
		public Mutex Mutex
		{
			get
			{
				return this._mutex;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x0014340F File Offset: 0x0014260F
		public int MutexIndex
		{
			get
			{
				return this._mutexIndex;
			}
		}

		// Token: 0x04000A22 RID: 2594
		private int _mutexIndex = -1;

		// Token: 0x04000A23 RID: 2595
		private Mutex _mutex;
	}
}
