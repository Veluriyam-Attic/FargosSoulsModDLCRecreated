using System;

namespace System.Threading
{
	// Token: 0x0200027E RID: 638
	internal interface IAsyncLocal
	{
		// Token: 0x060026EC RID: 9964
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
