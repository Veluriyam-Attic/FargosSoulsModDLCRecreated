using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000463 RID: 1123
	internal class ComEventsInfo
	{
		// Token: 0x0600442C RID: 17452 RVA: 0x00178B1C File Offset: 0x00177D1C
		private ComEventsInfo(object rcw)
		{
			this._rcw = rcw;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x00178B2C File Offset: 0x00177D2C
		~ComEventsInfo()
		{
			this._sinks = ComEventsSink.RemoveAll(this._sinks);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x00178B64 File Offset: 0x00177D64
		public static ComEventsInfo Find(object rcw)
		{
			return (ComEventsInfo)Marshal.GetComObjectData(rcw, typeof(ComEventsInfo));
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x00178B7C File Offset: 0x00177D7C
		public static ComEventsInfo FromObject(object rcw)
		{
			ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
			if (comEventsInfo == null)
			{
				comEventsInfo = new ComEventsInfo(rcw);
				Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), comEventsInfo);
			}
			return comEventsInfo;
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x00178BAD File Offset: 0x00177DAD
		public ComEventsSink FindSink(ref Guid iid)
		{
			return ComEventsSink.Find(this._sinks, ref iid);
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x00178BBC File Offset: 0x00177DBC
		public ComEventsSink AddSink(ref Guid iid)
		{
			ComEventsSink sink = new ComEventsSink(this._rcw, iid);
			this._sinks = ComEventsSink.Add(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x00178BF3 File Offset: 0x00177DF3
		internal ComEventsSink RemoveSink(ComEventsSink sink)
		{
			this._sinks = ComEventsSink.Remove(this._sinks, sink);
			return this._sinks;
		}

		// Token: 0x04000EFD RID: 3837
		private ComEventsSink _sinks;

		// Token: 0x04000EFE RID: 3838
		private readonly object _rcw;
	}
}
