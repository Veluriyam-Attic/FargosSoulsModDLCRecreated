using System;

namespace System.Runtime.InteropServices.CustomMarshalers
{
	// Token: 0x020004AC RID: 1196
	internal static class ComDataHelpers
	{
		// Token: 0x06004501 RID: 17665 RVA: 0x00179D6C File Offset: 0x00178F6C
		public static TView GetOrCreateManagedViewFromComData<T, TView>(object comObject, Func<T, TView> createCallback)
		{
			object typeFromHandle = typeof(TView);
			object comObjectData = Marshal.GetComObjectData(comObject, typeFromHandle);
			if (comObjectData is TView)
			{
				return (TView)((object)comObjectData);
			}
			TView tview = createCallback((T)((object)comObject));
			if (!Marshal.SetComObjectData(comObject, typeFromHandle, tview))
			{
				tview = (TView)((object)Marshal.GetComObjectData(comObject, typeFromHandle));
			}
			return tview;
		}
	}
}
