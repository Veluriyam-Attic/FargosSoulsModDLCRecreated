using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200071C RID: 1820
	internal sealed class EventPipeMetadataGenerator
	{
		// Token: 0x06005A3B RID: 23099 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private EventPipeMetadataGenerator()
		{
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x001B42AC File Offset: 0x001B34AC
		public byte[] GenerateEventMetadata(EventSource.EventMetadata eventMetadata)
		{
			ParameterInfo[] parameters = eventMetadata.Parameters;
			EventParameterInfo[] array = new EventParameterInfo[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				TraceLoggingTypeInfo typeInfo;
				EventParameterInfo.GetTypeInfoFromType(parameters[i].ParameterType, out typeInfo);
				array[i].SetInfo(parameters[i].Name, parameters[i].ParameterType, typeInfo);
			}
			return this.GenerateMetadata(eventMetadata.Descriptor.EventId, eventMetadata.Name, eventMetadata.Descriptor.Keywords, (uint)eventMetadata.Descriptor.Level, (uint)eventMetadata.Descriptor.Version, (EventOpcode)eventMetadata.Descriptor.Opcode, array);
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x001B4350 File Offset: 0x001B3550
		public byte[] GenerateEventMetadata(int eventId, string eventName, EventKeywords keywords, EventLevel level, uint version, EventOpcode opcode, TraceLoggingEventTypes eventTypes)
		{
			TraceLoggingTypeInfo[] typeInfos = eventTypes.typeInfos;
			string[] paramNames = eventTypes.paramNames;
			EventParameterInfo[] array = new EventParameterInfo[typeInfos.Length];
			for (int i = 0; i < typeInfos.Length; i++)
			{
				string name = string.Empty;
				if (paramNames != null)
				{
					name = paramNames[i];
				}
				array[i].SetInfo(name, typeInfos[i].DataType, typeInfos[i]);
			}
			return this.GenerateMetadata(eventId, eventName, (long)keywords, (uint)level, version, opcode, array);
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x001B43BC File Offset: 0x001B35BC
		internal unsafe byte[] GenerateMetadata(int eventId, string eventName, long keywords, uint level, uint version, EventOpcode opcode, EventParameterInfo[] parameters)
		{
			byte[] array = null;
			bool flag = false;
			try
			{
				uint num = (uint)(24 + (eventName.Length + 1) * 2);
				uint num2 = 0U;
				uint num3 = num;
				if (parameters.Length == 1 && parameters[0].ParameterType == typeof(EmptyStruct))
				{
					parameters = Array.Empty<EventParameterInfo>();
				}
				foreach (EventParameterInfo eventParameterInfo in parameters)
				{
					uint num4;
					if (!eventParameterInfo.GetMetadataLength(out num4))
					{
						flag = true;
						break;
					}
					num += num4;
				}
				if (flag)
				{
					num = num3;
					num2 = 4U;
					foreach (EventParameterInfo eventParameterInfo2 in parameters)
					{
						uint num5;
						if (!eventParameterInfo2.GetMetadataLengthV2(out num5))
						{
							parameters = Array.Empty<EventParameterInfo>();
							num = num3;
							num2 = 0U;
							flag = false;
							break;
						}
						num2 += num5;
					}
				}
				uint num6 = (opcode == EventOpcode.Info) ? 0U : 6U;
				uint num7 = (num2 == 0U) ? 0U : (num2 + 5U);
				uint num8 = num7 + num6;
				uint num9 = num + num8;
				array = new byte[num9];
				try
				{
					byte[] array4;
					byte* ptr;
					if ((array4 = array) == null || array4.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array4[0];
					}
					uint num10 = 0U;
					EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, (uint)eventId);
					try
					{
						char* ptr2;
						if (eventName == null)
						{
							ptr2 = null;
						}
						else
						{
							fixed (char* ptr3 = eventName.GetPinnableReference())
							{
								ptr2 = ptr3;
							}
						}
						char* src = ptr2;
						EventPipeMetadataGenerator.WriteToBuffer(ptr, num9, ref num10, (byte*)src, (uint)((eventName.Length + 1) * 2));
					}
					finally
					{
						char* ptr3 = null;
					}
					EventPipeMetadataGenerator.WriteToBuffer<long>(ptr, num9, ref num10, keywords);
					EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, version);
					EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, level);
					if (flag)
					{
						EventPipeMetadataGenerator.WriteToBuffer<int>(ptr, num9, ref num10, 0);
					}
					else
					{
						EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, (uint)parameters.Length);
						foreach (EventParameterInfo eventParameterInfo3 in parameters)
						{
							if (!eventParameterInfo3.GenerateMetadata(ptr, ref num10, num9))
							{
								return this.GenerateMetadata(eventId, eventName, keywords, level, version, opcode, Array.Empty<EventParameterInfo>());
							}
						}
					}
					if (opcode != EventOpcode.Info)
					{
						EventPipeMetadataGenerator.WriteToBuffer<int>(ptr, num9, ref num10, 1);
						EventPipeMetadataGenerator.WriteToBuffer<byte>(ptr, num9, ref num10, 1);
						EventPipeMetadataGenerator.WriteToBuffer<byte>(ptr, num9, ref num10, (byte)opcode);
					}
					if (flag)
					{
						EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, num2);
						EventPipeMetadataGenerator.WriteToBuffer<byte>(ptr, num9, ref num10, 2);
						EventPipeMetadataGenerator.WriteToBuffer<uint>(ptr, num9, ref num10, (uint)parameters.Length);
						foreach (EventParameterInfo eventParameterInfo4 in parameters)
						{
							if (!eventParameterInfo4.GenerateMetadataV2(ptr, ref num10, num9))
							{
								return this.GenerateMetadata(eventId, eventName, keywords, level, version, opcode, Array.Empty<EventParameterInfo>());
							}
						}
					}
				}
				finally
				{
					byte[] array4 = null;
				}
			}
			catch
			{
				array = null;
			}
			return array;
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x001B46A0 File Offset: 0x001B38A0
		internal unsafe static void WriteToBuffer(byte* buffer, uint bufferLength, ref uint offset, byte* src, uint srcLength)
		{
			int num = 0;
			while ((long)num < (long)((ulong)srcLength))
			{
				(buffer + offset)[num] = src[num];
				num++;
			}
			offset += srcLength;
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x001B46CF File Offset: 0x001B38CF
		internal unsafe static void WriteToBuffer<[IsUnmanaged] T>(byte* buffer, uint bufferLength, ref uint offset, T value) where T : struct, ValueType
		{
			*(T*)(buffer + offset) = value;
			offset += (uint)sizeof(T);
		}

		// Token: 0x04001A5A RID: 6746
		public static EventPipeMetadataGenerator Instance = new EventPipeMetadataGenerator();

		// Token: 0x0200071D RID: 1821
		private enum MetadataTag
		{
			// Token: 0x04001A5C RID: 6748
			Opcode = 1,
			// Token: 0x04001A5D RID: 6749
			ParameterPayload
		}
	}
}
