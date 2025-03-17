using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000703 RID: 1795
	internal static class EventPipeInternal
	{
		// Token: 0x0600597E RID: 22910
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern ulong Enable(char* outputFile, EventPipeSerializationFormat format, uint circularBufferSizeInMB, EventPipeInternal.EventPipeProviderConfigurationNative* providers, uint numProviders);

		// Token: 0x0600597F RID: 22911
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void Disable(ulong sessionID);

		// Token: 0x06005980 RID: 22912
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr CreateProvider(string providerName, Interop.Advapi32.EtwEnableCallback callbackFunc);

		// Token: 0x06005981 RID: 22913
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal unsafe static extern IntPtr DefineEvent(IntPtr provHandle, uint eventID, long keywords, uint eventVersion, uint level, void* pMetadata, uint metadataLength);

		// Token: 0x06005982 RID: 22914
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetProvider(string providerName);

		// Token: 0x06005983 RID: 22915
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void DeleteProvider(IntPtr provHandle);

		// Token: 0x06005984 RID: 22916
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int EventActivityIdControl(uint controlCode, ref Guid activityId);

		// Token: 0x06005985 RID: 22917
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal unsafe static extern void WriteEventData(IntPtr eventHandle, EventProvider.EventData* pEventData, uint dataCount, Guid* activityId, Guid* relatedActivityId);

		// Token: 0x06005986 RID: 22918
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool GetSessionInfo(ulong sessionID, EventPipeSessionInfo* pSessionInfo);

		// Token: 0x06005987 RID: 22919
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool GetNextEvent(ulong sessionID, EventPipeEventInstanceData* pInstance);

		// Token: 0x06005988 RID: 22920
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetWaitHandle(ulong sessionID);

		// Token: 0x06005989 RID: 22921 RVA: 0x001B1D24 File Offset: 0x001B0F24
		internal unsafe static ulong Enable(string outputFile, EventPipeSerializationFormat format, uint circularBufferSizeInMB, EventPipeProviderConfiguration[] providers)
		{
			Span<EventPipeInternal.EventPipeProviderConfigurationNative> span = new Span<EventPipeInternal.EventPipeProviderConfigurationNative>((void*)Marshal.AllocCoTaskMem(sizeof(EventPipeInternal.EventPipeProviderConfigurationNative) * providers.Length), providers.Length);
			span.Clear();
			ulong result;
			try
			{
				for (int i = 0; i < providers.Length; i++)
				{
					EventPipeInternal.EventPipeProviderConfigurationNative.MarshalToNative(providers[i], span[i]);
				}
				try
				{
					char* ptr;
					if (outputFile == null)
					{
						ptr = null;
					}
					else
					{
						fixed (char* ptr2 = outputFile.GetPinnableReference())
						{
							ptr = ptr2;
						}
					}
					char* outputFile2 = ptr;
					try
					{
						fixed (EventPipeInternal.EventPipeProviderConfigurationNative* ptr3 = span.GetPinnableReference())
						{
							EventPipeInternal.EventPipeProviderConfigurationNative* providers2 = ptr3;
							result = EventPipeInternal.Enable(outputFile2, format, circularBufferSizeInMB, providers2, (uint)span.Length);
						}
					}
					finally
					{
						EventPipeInternal.EventPipeProviderConfigurationNative* ptr3 = null;
					}
				}
				finally
				{
					char* ptr2 = null;
				}
			}
			finally
			{
				for (int j = 0; j < providers.Length; j++)
				{
					span[j].Release();
				}
				fixed (EventPipeInternal.EventPipeProviderConfigurationNative* pinnableReference = span.GetPinnableReference())
				{
					EventPipeInternal.EventPipeProviderConfigurationNative* value = pinnableReference;
					Marshal.FreeCoTaskMem((IntPtr)((void*)value));
				}
			}
			return result;
		}

		// Token: 0x02000704 RID: 1796
		private struct EventPipeProviderConfigurationNative
		{
			// Token: 0x0600598A RID: 22922 RVA: 0x001B1E20 File Offset: 0x001B1020
			internal unsafe static void MarshalToNative(EventPipeProviderConfiguration managed, ref EventPipeInternal.EventPipeProviderConfigurationNative native)
			{
				native.m_pProviderName = (char*)((void*)Marshal.StringToCoTaskMemUni(managed.ProviderName));
				native.m_keywords = managed.Keywords;
				native.m_loggingLevel = managed.LoggingLevel;
				native.m_pFilterData = (char*)((void*)Marshal.StringToCoTaskMemUni(managed.FilterData));
			}

			// Token: 0x0600598B RID: 22923 RVA: 0x001B1E75 File Offset: 0x001B1075
			internal unsafe void Release()
			{
				if (this.m_pProviderName != null)
				{
					Marshal.FreeCoTaskMem((IntPtr)((void*)this.m_pProviderName));
				}
				if (this.m_pFilterData != null)
				{
					Marshal.FreeCoTaskMem((IntPtr)((void*)this.m_pFilterData));
				}
			}

			// Token: 0x040019DE RID: 6622
			private unsafe char* m_pProviderName;

			// Token: 0x040019DF RID: 6623
			private ulong m_keywords;

			// Token: 0x040019E0 RID: 6624
			private uint m_loggingLevel;

			// Token: 0x040019E1 RID: 6625
			private unsafe char* m_pFilterData;
		}
	}
}
