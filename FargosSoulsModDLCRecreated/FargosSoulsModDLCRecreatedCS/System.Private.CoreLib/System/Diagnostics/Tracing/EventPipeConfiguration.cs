using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000717 RID: 1815
	internal sealed class EventPipeConfiguration
	{
		// Token: 0x06005A1B RID: 23067 RVA: 0x001B3C30 File Offset: 0x001B2E30
		internal EventPipeConfiguration(string outputFile, EventPipeSerializationFormat format, uint circularBufferSizeInMB)
		{
			if (string.IsNullOrEmpty(outputFile))
			{
				throw new ArgumentNullException("outputFile");
			}
			if (circularBufferSizeInMB == 0U)
			{
				throw new ArgumentOutOfRangeException("circularBufferSizeInMB");
			}
			this.m_outputFile = outputFile;
			this.m_format = format;
			this.m_circularBufferSizeInMB = circularBufferSizeInMB;
			this.m_providers = new List<EventPipeProviderConfiguration>();
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x001B3C98 File Offset: 0x001B2E98
		internal string OutputFile
		{
			get
			{
				return this.m_outputFile;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x001B3CA0 File Offset: 0x001B2EA0
		internal EventPipeSerializationFormat Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x001B3CA8 File Offset: 0x001B2EA8
		internal uint CircularBufferSizeInMB
		{
			get
			{
				return this.m_circularBufferSizeInMB;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x001B3CB0 File Offset: 0x001B2EB0
		internal EventPipeProviderConfiguration[] Providers
		{
			get
			{
				return this.m_providers.ToArray();
			}
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x001B3CBD File Offset: 0x001B2EBD
		internal void EnableProvider(string providerName, ulong keywords, uint loggingLevel)
		{
			this.EnableProviderWithFilter(providerName, keywords, loggingLevel, null);
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x001B3CC9 File Offset: 0x001B2EC9
		internal void EnableProviderWithFilter(string providerName, ulong keywords, uint loggingLevel, string filterData)
		{
			this.m_providers.Add(new EventPipeProviderConfiguration(providerName, keywords, loggingLevel, filterData));
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x001B3CE0 File Offset: 0x001B2EE0
		private void EnableProviderConfiguration(EventPipeProviderConfiguration providerConfig)
		{
			this.m_providers.Add(providerConfig);
		}

		// Token: 0x06005A23 RID: 23075 RVA: 0x001B3CF0 File Offset: 0x001B2EF0
		internal void EnableProviderRange(EventPipeProviderConfiguration[] providerConfigs)
		{
			foreach (EventPipeProviderConfiguration providerConfig in providerConfigs)
			{
				this.EnableProviderConfiguration(providerConfig);
			}
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x001B3D1C File Offset: 0x001B2F1C
		internal void SetProfilerSamplingRate(TimeSpan minTimeBetweenSamples)
		{
			if (minTimeBetweenSamples.Ticks <= 0L)
			{
				throw new ArgumentOutOfRangeException("minTimeBetweenSamples");
			}
			this.m_minTimeBetweenSamples = minTimeBetweenSamples;
		}

		// Token: 0x04001A45 RID: 6725
		private readonly string m_outputFile;

		// Token: 0x04001A46 RID: 6726
		private readonly EventPipeSerializationFormat m_format;

		// Token: 0x04001A47 RID: 6727
		private readonly uint m_circularBufferSizeInMB;

		// Token: 0x04001A48 RID: 6728
		private readonly List<EventPipeProviderConfiguration> m_providers;

		// Token: 0x04001A49 RID: 6729
		private TimeSpan m_minTimeBetweenSamples = TimeSpan.FromMilliseconds(1.0);
	}
}
