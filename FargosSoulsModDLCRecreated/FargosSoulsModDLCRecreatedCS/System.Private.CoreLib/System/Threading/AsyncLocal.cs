using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200027D RID: 637
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class AsyncLocal<[Nullable(2)] T> : IAsyncLocal
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x000ABD27 File Offset: 0x000AAF27
		public AsyncLocal()
		{
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00143417 File Offset: 0x00142617
		public AsyncLocal([Nullable(new byte[]
		{
			2,
			0,
			1
		})] Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x00143428 File Offset: 0x00142628
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x0014344F File Offset: 0x0014264F
		public T Value
		{
			[return: MaybeNull]
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00143468 File Offset: 0x00142668
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T previousValue = (previousValueObj == null) ? default(T) : ((T)((object)previousValueObj));
			T currentValue = (currentValueObj == null) ? default(T) : ((T)((object)currentValueObj));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(previousValue, currentValue, contextChanged));
		}

		// Token: 0x04000A28 RID: 2600
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
