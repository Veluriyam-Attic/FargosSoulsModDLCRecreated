using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000718 RID: 1816
	internal static class EventPipe
	{
		// Token: 0x06005A25 RID: 23077 RVA: 0x001B3D3C File Offset: 0x001B2F3C
		internal static void Enable(EventPipeConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (configuration.Providers == null)
			{
				throw new ArgumentNullException("Providers");
			}
			EventPipeProviderConfiguration[] providers = configuration.Providers;
			EventPipe.s_sessionID = EventPipeInternal.Enable(configuration.OutputFile, configuration.Format, configuration.CircularBufferSizeInMB, providers);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x001B3D8E File Offset: 0x001B2F8E
		internal static void Disable()
		{
			EventPipeInternal.Disable(EventPipe.s_sessionID);
		}

		// Token: 0x04001A4A RID: 6730
		private static ulong s_sessionID;
	}
}
