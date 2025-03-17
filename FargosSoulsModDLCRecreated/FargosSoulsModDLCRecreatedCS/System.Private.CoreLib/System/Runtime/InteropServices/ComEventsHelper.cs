using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000462 RID: 1122
	[SupportedOSPlatform("windows")]
	[Nullable(0)]
	[NullableContext(1)]
	public static class ComEventsHelper
	{
		// Token: 0x0600442A RID: 17450 RVA: 0x001789FC File Offset: 0x00177BFC
		public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
		{
			lock (rcw)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.FromObject(rcw);
				ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid) ?? comEventsInfo.AddSink(ref iid);
				ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid) ?? comEventsSink.AddMethod(dispid);
				comEventsMethod.AddDelegate(d, false);
			}
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x00178A6C File Offset: 0x00177C6C
		[return: Nullable(2)]
		public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
		{
			Delegate result;
			lock (rcw)
			{
				ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
				if (comEventsInfo == null)
				{
					result = null;
				}
				else
				{
					ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid);
					if (comEventsSink == null)
					{
						result = null;
					}
					else
					{
						ComEventsMethod comEventsMethod = comEventsSink.FindMethod(dispid);
						if (comEventsMethod == null)
						{
							result = null;
						}
						else
						{
							comEventsMethod.RemoveDelegate(d, false);
							if (comEventsMethod.Empty)
							{
								comEventsMethod = comEventsSink.RemoveMethod(comEventsMethod);
							}
							if (comEventsMethod == null)
							{
								comEventsSink = comEventsInfo.RemoveSink(comEventsSink);
							}
							if (comEventsSink == null)
							{
								Marshal.SetComObjectData(rcw, typeof(ComEventsInfo), null);
								GC.SuppressFinalize(comEventsInfo);
							}
							result = d;
						}
					}
				}
			}
			return result;
		}
	}
}
