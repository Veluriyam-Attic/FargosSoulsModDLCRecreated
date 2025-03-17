using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200065B RID: 1627
	internal class EmptyCAHolder : ICustomAttributeProvider
	{
		// Token: 0x0600537D RID: 21373 RVA: 0x000ABD27 File Offset: 0x000AAF27
		internal EmptyCAHolder()
		{
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0018C06F File Offset: 0x0018B26F
		object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
		{
			return Array.Empty<object>();
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0018C06F File Offset: 0x0018B26F
		object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
		{
			return Array.Empty<object>();
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x000AC09B File Offset: 0x000AB29B
		bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
		{
			return false;
		}
	}
}
