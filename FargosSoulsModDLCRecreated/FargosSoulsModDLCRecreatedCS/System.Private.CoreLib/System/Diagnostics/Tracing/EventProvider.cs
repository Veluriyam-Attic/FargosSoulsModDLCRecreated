using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Win32;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000722 RID: 1826
	internal class EventProvider : IDisposable
	{
		// Token: 0x06005A50 RID: 23120 RVA: 0x001B5244 File Offset: 0x001B4444
		internal EventProvider(EventProviderType providerType)
		{
			IEventProvider eventProvider;
			if (providerType != EventProviderType.ETW)
			{
				if (providerType != EventProviderType.EventPipe)
				{
					eventProvider = new NoOpEventProvider();
				}
				else
				{
					eventProvider = new EventPipeEventProvider();
				}
			}
			else
			{
				eventProvider = new EtwEventProvider();
			}
			this.m_eventProvider = eventProvider;
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x001B5280 File Offset: 0x001B4480
		internal void Register(EventSource eventSource)
		{
			this.m_etwCallback = new Interop.Advapi32.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = this.EventRegister(eventSource, this.m_etwCallback);
			if (num != 0U)
			{
				throw new ArgumentException(Interop.Kernel32.GetMessage((int)num));
			}
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x001B52BC File Offset: 0x001B44BC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x001B52CC File Offset: 0x001B44CC
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_enabled = false;
			long num = 0L;
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_disposed)
				{
					return;
				}
				num = this.m_regHandle;
				this.m_regHandle = 0L;
				this.m_disposed = true;
			}
			if (num != 0L)
			{
				this.EventUnregister(num);
			}
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x001B5344 File Offset: 0x001B4544
		~EventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x001B5374 File Offset: 0x001B4574
		private unsafe void EtwEnableCallBack(in Guid sourceId, int controlCode, byte setLevel, long anyKeyword, long allKeyword, Interop.Advapi32.EVENT_FILTER_DESCRIPTOR* filterData, void* callbackContext)
		{
			try
			{
				ControllerCommand command = ControllerCommand.Update;
				IDictionary<string, string> dictionary = null;
				bool flag = false;
				if (controlCode == 1)
				{
					this.m_enabled = true;
					this.m_level = setLevel;
					this.m_anyKeywordMask = anyKeyword;
					this.m_allKeywordMask = allKeyword;
					List<Tuple<EventProvider.SessionInfo, bool>> sessions = this.GetSessions();
					if (sessions.Count == 0)
					{
						sessions.Add(new Tuple<EventProvider.SessionInfo, bool>(new EventProvider.SessionInfo(0, 0), true));
					}
					using (List<Tuple<EventProvider.SessionInfo, bool>>.Enumerator enumerator = sessions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Tuple<EventProvider.SessionInfo, bool> tuple = enumerator.Current;
							int sessionIdBit = tuple.Item1.sessionIdBit;
							int etwSessionId = tuple.Item1.etwSessionId;
							bool item = tuple.Item2;
							flag = true;
							dictionary = null;
							if (sessions.Count > 1)
							{
								filterData = null;
							}
							byte[] array;
							int i;
							if (item && this.GetDataFromController(etwSessionId, filterData, out command, out array, out i))
							{
								dictionary = new Dictionary<string, string>(4);
								if (array != null)
								{
									while (i < array.Length)
									{
										int num = EventProvider.FindNull(array, i);
										int num2 = num + 1;
										int num3 = EventProvider.FindNull(array, num2);
										if (num3 < array.Length)
										{
											string @string = Encoding.UTF8.GetString(array, i, num - i);
											string string2 = Encoding.UTF8.GetString(array, num2, num3 - num2);
											dictionary[@string] = string2;
										}
										i = num3 + 1;
									}
								}
							}
							this.OnControllerCommand(command, dictionary, item ? sessionIdBit : (-sessionIdBit), etwSessionId);
						}
						goto IL_18A;
					}
				}
				if (controlCode == 0)
				{
					this.m_enabled = false;
					this.m_level = 0;
					this.m_anyKeywordMask = 0L;
					this.m_allKeywordMask = 0L;
					this.m_liveSessions = null;
				}
				else
				{
					if (controlCode != 2)
					{
						return;
					}
					command = ControllerCommand.SendManifest;
				}
				IL_18A:
				if (!flag)
				{
					this.OnControllerCommand(command, dictionary, 0, 0);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x000AB30B File Offset: 0x000AA50B
		protected virtual void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int sessionId, int etwSessionId)
		{
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x001B5554 File Offset: 0x001B4754
		protected EventLevel Level
		{
			get
			{
				return (EventLevel)this.m_level;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x001B555C File Offset: 0x001B475C
		protected EventKeywords MatchAnyKeyword
		{
			get
			{
				return (EventKeywords)this.m_anyKeywordMask;
			}
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x001B5564 File Offset: 0x001B4764
		private static int FindNull(byte[] buffer, int idx)
		{
			while (idx < buffer.Length && buffer[idx] != 0)
			{
				idx++;
			}
			return idx;
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x001B557C File Offset: 0x001B477C
		private List<Tuple<EventProvider.SessionInfo, bool>> GetSessions()
		{
			List<EventProvider.SessionInfo> list = null;
			this.GetSessionInfo(delegate(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList)
			{
				EventProvider.GetSessionInfoCallback(etwSessionId, matchAllKeywords, ref sessionList);
			}, ref list);
			List<Tuple<EventProvider.SessionInfo, bool>> list2 = new List<Tuple<EventProvider.SessionInfo, bool>>();
			if (this.m_liveSessions != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo in this.m_liveSessions)
				{
					int index;
					if ((index = EventProvider.IndexOfSessionInList(list, sessionInfo.etwSessionId)) < 0 || list[index].sessionIdBit != sessionInfo.sessionIdBit)
					{
						list2.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo, false));
					}
				}
			}
			if (list != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo2 in list)
				{
					int index2;
					if ((index2 = EventProvider.IndexOfSessionInList(this.m_liveSessions, sessionInfo2.etwSessionId)) < 0 || this.m_liveSessions[index2].sessionIdBit != sessionInfo2.sessionIdBit)
					{
						list2.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo2, true));
					}
				}
			}
			this.m_liveSessions = list;
			return list2;
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x001B56B8 File Offset: 0x001B48B8
		private static void GetSessionInfoCallback(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList)
		{
			uint value = (uint)SessionMask.FromEventKeywords((ulong)matchAllKeywords);
			int num = BitOperations.PopCount(value);
			if (num > 1)
			{
				return;
			}
			if (sessionList == null)
			{
				sessionList = new List<EventProvider.SessionInfo>(8);
			}
			if (num == 1)
			{
				num = BitOperations.TrailingZeroCount(value);
			}
			else
			{
				num = BitOperations.PopCount((uint)SessionMask.All);
			}
			sessionList.Add(new EventProvider.SessionInfo(num + 1, etwSessionId));
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x001B5718 File Offset: 0x001B4918
		private unsafe void GetSessionInfo(EventProvider.SessionInfoCallback action, ref List<EventProvider.SessionInfo> sessionList)
		{
			int num = 256;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			byte* ptr2 = ptr;
			try
			{
				for (;;)
				{
					int num2 = 0;
					try
					{
						fixed (Guid* ptr3 = &this.m_providerId)
						{
							Guid* inBuffer = ptr3;
							num2 = Interop.Advapi32.EnumerateTraceGuidsEx(Interop.Advapi32.TRACE_QUERY_INFO_CLASS.TraceGuidQueryInfo, (void*)inBuffer, sizeof(Guid), (void*)ptr2, num, out num);
						}
					}
					finally
					{
						Guid* ptr3 = null;
					}
					if (num2 == 0)
					{
						goto IL_6B;
					}
					if (num2 != 122)
					{
						break;
					}
					if (ptr2 != ptr)
					{
						byte* value = ptr2;
						ptr2 = null;
						Marshal.FreeHGlobal((IntPtr)((void*)value));
					}
					ptr2 = (byte*)((void*)Marshal.AllocHGlobal(num));
				}
				return;
				IL_6B:
				Interop.Advapi32.TRACE_GUID_INFO* ptr4 = (Interop.Advapi32.TRACE_GUID_INFO*)ptr2;
				Interop.Advapi32.TRACE_PROVIDER_INSTANCE_INFO* ptr5 = (Interop.Advapi32.TRACE_PROVIDER_INSTANCE_INFO*)(ptr4 + 1);
				int currentProcessId = (int)Interop.Kernel32.GetCurrentProcessId();
				for (int i = 0; i < ptr4->InstanceCount; i++)
				{
					if (ptr5->Pid == currentProcessId)
					{
						Interop.Advapi32.TRACE_ENABLE_INFO* ptr6 = (Interop.Advapi32.TRACE_ENABLE_INFO*)(ptr5 + 1);
						for (int j = 0; j < ptr5->EnableCount; j++)
						{
							action((int)ptr6[j].LoggerId, ptr6[j].MatchAllKeyword, ref sessionList);
						}
					}
					if (ptr5->NextOffset == 0)
					{
						break;
					}
					byte* ptr7 = (byte*)ptr5;
					ptr5 = (Interop.Advapi32.TRACE_PROVIDER_INSTANCE_INFO*)(ptr7 + ptr5->NextOffset);
				}
			}
			finally
			{
				if (ptr2 != null && ptr2 != ptr)
				{
					Marshal.FreeHGlobal((IntPtr)((void*)ptr2));
				}
			}
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x001B587C File Offset: 0x001B4A7C
		private static int IndexOfSessionInList(List<EventProvider.SessionInfo> sessions, int etwSessionId)
		{
			if (sessions == null)
			{
				return -1;
			}
			for (int i = 0; i < sessions.Count; i++)
			{
				if (sessions[i].etwSessionId == etwSessionId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x001B58B4 File Offset: 0x001B4AB4
		private unsafe bool GetDataFromController(int etwSessionId, Interop.Advapi32.EVENT_FILTER_DESCRIPTOR* filterData, out ControllerCommand command, out byte[] data, out int dataStart)
		{
			data = null;
			dataStart = 0;
			if (filterData == null)
			{
				string str = "\\Microsoft\\Windows\\CurrentVersion\\Winevt\\Publishers\\{";
				Guid providerId = this.m_providerId;
				string text = str + providerId.ToString() + "}";
				int size = IntPtr.Size;
				text = "Software\\Wow6432Node" + text;
				string name = "ControllerData_Session_" + etwSessionId.ToString(CultureInfo.InvariantCulture);
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text))
				{
					data = (((registryKey != null) ? registryKey.GetValue(name, null) : null) as byte[]);
					if (data != null)
					{
						command = ControllerCommand.Update;
						return true;
					}
					goto IL_EA;
				}
				goto IL_9A;
				IL_EA:
				command = ControllerCommand.Update;
				return false;
			}
			IL_9A:
			if (filterData->Ptr != 0L && 0 < filterData->Size && filterData->Size <= 102400)
			{
				data = new byte[filterData->Size];
				Marshal.Copy((IntPtr)filterData->Ptr, data, 0, data.Length);
			}
			command = (ControllerCommand)filterData->Type;
			return true;
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x001B59C4 File Offset: 0x001B4BC4
		public bool IsEnabled()
		{
			return this.m_enabled;
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x001B59CC File Offset: 0x001B4BCC
		public bool IsEnabled(byte level, long keywords)
		{
			return this.m_enabled && ((level <= this.m_level || this.m_level == 0) && (keywords == 0L || ((keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask)));
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x001B5A09 File Offset: 0x001B4C09
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			return EventProvider.s_returnCode;
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x001B5A10 File Offset: 0x001B4C10
		private static void SetLastError(EventProvider.WriteEventErrorCode error)
		{
			EventProvider.s_returnCode = error;
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x001B5A18 File Offset: 0x001B4C18
		private unsafe static object EncodeObject(ref object data, ref EventProvider.EventData* dataDescriptor, ref byte* dataBuffer, ref uint totalEventSize)
		{
			string text;
			byte[] array;
			for (;;)
			{
				dataDescriptor.Reserved = 0U;
				text = (data as string);
				array = null;
				if (text != null)
				{
					break;
				}
				if ((array = (data as byte[])) != null)
				{
					goto Block_1;
				}
				if (data is IntPtr)
				{
					goto Block_2;
				}
				if (data is int)
				{
					goto Block_3;
				}
				if (data is long)
				{
					goto Block_4;
				}
				if (data is uint)
				{
					goto Block_5;
				}
				if (data is ulong)
				{
					goto Block_6;
				}
				if (data is char)
				{
					goto Block_7;
				}
				if (data is byte)
				{
					goto Block_8;
				}
				if (data is short)
				{
					goto Block_9;
				}
				if (data is sbyte)
				{
					goto Block_10;
				}
				if (data is ushort)
				{
					goto Block_11;
				}
				if (data is float)
				{
					goto Block_12;
				}
				if (data is double)
				{
					goto Block_13;
				}
				if (data is bool)
				{
					goto Block_14;
				}
				if (data is Guid)
				{
					goto Block_16;
				}
				if (data is decimal)
				{
					goto Block_17;
				}
				if (data is DateTime)
				{
					goto Block_18;
				}
				if (data is Enum)
				{
					try
					{
						Type underlyingType = Enum.GetUnderlyingType(data.GetType());
						if (underlyingType == typeof(ulong))
						{
							data = (ulong)data;
						}
						else if (underlyingType == typeof(long))
						{
							data = (long)data;
						}
						else
						{
							data = (int)Convert.ToInt64(data);
						}
						continue;
					}
					catch
					{
					}
					goto IL_411;
				}
				goto IL_411;
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			goto IL_436;
			Block_1:
			*dataBuffer = array.Length;
			dataDescriptor.Ptr = (ulong)dataBuffer;
			dataDescriptor.Size = 4U;
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			dataDescriptor.Size = (uint)array.Length;
			goto IL_436;
			Block_2:
			dataDescriptor.Size = (uint)sizeof(IntPtr);
			IntPtr* ptr = dataBuffer;
			*ptr = (IntPtr)data;
			dataDescriptor.Ptr = ptr;
			goto IL_436;
			Block_3:
			dataDescriptor.Size = 4U;
			int* ptr2 = dataBuffer;
			*ptr2 = (int)data;
			dataDescriptor.Ptr = ptr2;
			goto IL_436;
			Block_4:
			dataDescriptor.Size = 8U;
			long* ptr3 = dataBuffer;
			*ptr3 = (long)data;
			dataDescriptor.Ptr = ptr3;
			goto IL_436;
			Block_5:
			dataDescriptor.Size = 4U;
			uint* ptr4 = dataBuffer;
			*ptr4 = (uint)data;
			dataDescriptor.Ptr = ptr4;
			goto IL_436;
			Block_6:
			dataDescriptor.Size = 8U;
			ulong* ptr5 = dataBuffer;
			*ptr5 = (ulong)data;
			dataDescriptor.Ptr = ptr5;
			goto IL_436;
			Block_7:
			dataDescriptor.Size = 2U;
			char* ptr6 = dataBuffer;
			*ptr6 = (char)data;
			dataDescriptor.Ptr = ptr6;
			goto IL_436;
			Block_8:
			dataDescriptor.Size = 1U;
			byte* ptr7 = dataBuffer;
			*ptr7 = (byte)data;
			dataDescriptor.Ptr = ptr7;
			goto IL_436;
			Block_9:
			dataDescriptor.Size = 2U;
			short* ptr8 = dataBuffer;
			*ptr8 = (short)data;
			dataDescriptor.Ptr = ptr8;
			goto IL_436;
			Block_10:
			dataDescriptor.Size = 1U;
			sbyte* ptr9 = dataBuffer;
			*ptr9 = (sbyte)data;
			dataDescriptor.Ptr = ptr9;
			goto IL_436;
			Block_11:
			dataDescriptor.Size = 2U;
			ushort* ptr10 = dataBuffer;
			*ptr10 = (ushort)data;
			dataDescriptor.Ptr = ptr10;
			goto IL_436;
			Block_12:
			dataDescriptor.Size = 4U;
			float* ptr11 = dataBuffer;
			*ptr11 = (float)data;
			dataDescriptor.Ptr = ptr11;
			goto IL_436;
			Block_13:
			dataDescriptor.Size = 8U;
			double* ptr12 = dataBuffer;
			*ptr12 = (double)data;
			dataDescriptor.Ptr = ptr12;
			goto IL_436;
			Block_14:
			dataDescriptor.Size = 4U;
			int* ptr13 = dataBuffer;
			if ((bool)data)
			{
				*ptr13 = 1;
			}
			else
			{
				*ptr13 = 0;
			}
			dataDescriptor.Ptr = ptr13;
			goto IL_436;
			Block_16:
			dataDescriptor.Size = (uint)sizeof(Guid);
			Guid* ptr14 = dataBuffer;
			*ptr14 = (Guid)data;
			dataDescriptor.Ptr = ptr14;
			goto IL_436;
			Block_17:
			dataDescriptor.Size = 16U;
			decimal* ptr15 = dataBuffer;
			*ptr15 = (decimal)data;
			dataDescriptor.Ptr = ptr15;
			goto IL_436;
			Block_18:
			long num = 0L;
			if (((DateTime)data).Ticks > 504911232000000000L)
			{
				num = ((DateTime)data).ToFileTimeUtc();
			}
			dataDescriptor.Size = 8U;
			long* ptr16 = dataBuffer;
			*ptr16 = num;
			dataDescriptor.Ptr = ptr16;
			goto IL_436;
			IL_411:
			if (data == null)
			{
				text = "";
			}
			else
			{
				text = data.ToString();
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			IL_436:
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			return text ?? array;
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x001B5E90 File Offset: 0x001B5090
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityID, Guid* childActivityID, params object[] eventPayload)
		{
			EventProvider.WriteEventErrorCode writeEventErrorCode = EventProvider.WriteEventErrorCode.NoError;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				int num = eventPayload.Length;
				if (num > 128)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
					return false;
				}
				uint num2 = 0U;
				int i = 0;
				List<int> list = new List<int>(8);
				List<object> list2 = new List<object>(8);
				EventProvider.EventData* ptr = stackalloc EventProvider.EventData[checked(unchecked((UIntPtr)(2 * num)) * (UIntPtr)sizeof(EventProvider.EventData))];
				for (int j = 0; j < 2 * num; j++)
				{
					ptr[j] = default(EventProvider.EventData);
				}
				EventProvider.EventData* ptr2 = ptr;
				byte* ptr3 = stackalloc byte[(UIntPtr)(32 * num)];
				byte* ptr4 = ptr3;
				bool flag = false;
				for (int k = 0; k < eventPayload.Length; k++)
				{
					if (eventPayload[k] == null)
					{
						EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NullInput;
						return false;
					}
					object obj = EventProvider.EncodeObject(ref eventPayload[k], ref ptr2, ref ptr4, ref num2);
					if (obj != null)
					{
						int num3 = (int)((long)(ptr2 - ptr) - 1L);
						if (!(obj is string))
						{
							if (eventPayload.Length + num3 + 1 - k > 128)
							{
								EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
								return false;
							}
							flag = true;
						}
						list2.Add(obj);
						list.Add(num3);
						i++;
					}
				}
				num = (int)((long)(ptr2 - ptr));
				if (num2 > 65482U)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				if (!flag && i < 8)
				{
					while (i < 8)
					{
						list2.Add(null);
						i++;
					}
					string text = (string)list2[0];
					char* ptr5;
					if (text == null)
					{
						ptr5 = null;
					}
					else
					{
						fixed (char* ptr6 = text.GetPinnableReference())
						{
							ptr5 = ptr6;
						}
					}
					char* ptr7 = ptr5;
					string text2 = (string)list2[1];
					char* ptr8;
					if (text2 == null)
					{
						ptr8 = null;
					}
					else
					{
						fixed (char* ptr9 = text2.GetPinnableReference())
						{
							ptr8 = ptr9;
						}
					}
					char* ptr10 = ptr8;
					string text3 = (string)list2[2];
					char* ptr11;
					if (text3 == null)
					{
						ptr11 = null;
					}
					else
					{
						fixed (char* ptr12 = text3.GetPinnableReference())
						{
							ptr11 = ptr12;
						}
					}
					char* ptr13 = ptr11;
					string text4 = (string)list2[3];
					char* ptr14;
					if (text4 == null)
					{
						ptr14 = null;
					}
					else
					{
						fixed (char* ptr15 = text4.GetPinnableReference())
						{
							ptr14 = ptr15;
						}
					}
					char* ptr16 = ptr14;
					string text5 = (string)list2[4];
					char* ptr17;
					if (text5 == null)
					{
						ptr17 = null;
					}
					else
					{
						fixed (char* ptr18 = text5.GetPinnableReference())
						{
							ptr17 = ptr18;
						}
					}
					char* ptr19 = ptr17;
					string text6 = (string)list2[5];
					char* ptr20;
					if (text6 == null)
					{
						ptr20 = null;
					}
					else
					{
						fixed (char* ptr21 = text6.GetPinnableReference())
						{
							ptr20 = ptr21;
						}
					}
					char* ptr22 = ptr20;
					string text7 = (string)list2[6];
					char* ptr23;
					if (text7 == null)
					{
						ptr23 = null;
					}
					else
					{
						fixed (char* ptr24 = text7.GetPinnableReference())
						{
							ptr23 = ptr24;
						}
					}
					char* ptr25 = ptr23;
					string text8 = (string)list2[7];
					char* ptr26;
					if (text8 == null)
					{
						ptr26 = null;
					}
					else
					{
						fixed (char* ptr27 = text8.GetPinnableReference())
						{
							ptr26 = ptr27;
						}
					}
					char* ptr28 = ptr26;
					ptr2 = ptr;
					if (list2[0] != null)
					{
						ptr2[list[0]].Ptr = ptr7;
					}
					if (list2[1] != null)
					{
						ptr2[list[1]].Ptr = ptr10;
					}
					if (list2[2] != null)
					{
						ptr2[list[2]].Ptr = ptr13;
					}
					if (list2[3] != null)
					{
						ptr2[list[3]].Ptr = ptr16;
					}
					if (list2[4] != null)
					{
						ptr2[list[4]].Ptr = ptr19;
					}
					if (list2[5] != null)
					{
						ptr2[list[5]].Ptr = ptr22;
					}
					if (list2[6] != null)
					{
						ptr2[list[6]].Ptr = ptr25;
					}
					if (list2[7] != null)
					{
						ptr2[list[7]].Ptr = ptr28;
					}
					writeEventErrorCode = this.m_eventProvider.EventWriteTransfer(this.m_regHandle, eventDescriptor, eventHandle, activityID, childActivityID, num, ptr);
					char* ptr6 = null;
					char* ptr9 = null;
					char* ptr12 = null;
					char* ptr15 = null;
					char* ptr18 = null;
					char* ptr21 = null;
					char* ptr24 = null;
					char* ptr27 = null;
				}
				else
				{
					ptr2 = ptr;
					GCHandle[] array = new GCHandle[i];
					for (int l = 0; l < i; l++)
					{
						array[l] = GCHandle.Alloc(list2[l], GCHandleType.Pinned);
						if (list2[l] is string)
						{
							string text9 = (string)list2[l];
							char* ptr29;
							if (text9 == null)
							{
								ptr29 = null;
							}
							else
							{
								fixed (char* ptr30 = text9.GetPinnableReference())
								{
									ptr29 = ptr30;
								}
							}
							char* ptr31 = ptr29;
							ptr2[list[l]].Ptr = ptr31;
							char* ptr30 = null;
						}
						else
						{
							byte[] array2;
							byte* ptr32;
							if ((array2 = (byte[])list2[l]) == null || array2.Length == 0)
							{
								ptr32 = null;
							}
							else
							{
								ptr32 = &array2[0];
							}
							ptr2[list[l]].Ptr = ptr32;
							array2 = null;
						}
					}
					writeEventErrorCode = this.m_eventProvider.EventWriteTransfer(this.m_regHandle, eventDescriptor, eventHandle, activityID, childActivityID, num, ptr);
					for (int m = 0; m < i; m++)
					{
						array[m].Free();
					}
				}
			}
			if (writeEventErrorCode != EventProvider.WriteEventErrorCode.NoError)
			{
				EventProvider.SetLastError(writeEventErrorCode);
				return false;
			}
			return true;
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x001B6388 File Offset: 0x001B5588
		protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityID, Guid* childActivityID, int dataCount, IntPtr data)
		{
			UIntPtr uintPtr = (UIntPtr)0;
			EventProvider.WriteEventErrorCode writeEventErrorCode = this.m_eventProvider.EventWriteTransfer(this.m_regHandle, eventDescriptor, eventHandle, activityID, childActivityID, dataCount, (EventProvider.EventData*)((void*)data));
			if (writeEventErrorCode != EventProvider.WriteEventErrorCode.NoError)
			{
				EventProvider.SetLastError(writeEventErrorCode);
				return false;
			}
			return true;
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x001B63C8 File Offset: 0x001B55C8
		internal unsafe bool WriteEventRaw(ref EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			EventProvider.WriteEventErrorCode writeEventErrorCode = this.m_eventProvider.EventWriteTransfer(this.m_regHandle, eventDescriptor, eventHandle, activityID, relatedActivityID, dataCount, (EventProvider.EventData*)((void*)data));
			if (writeEventErrorCode != EventProvider.WriteEventErrorCode.NoError)
			{
				EventProvider.SetLastError(writeEventErrorCode);
				return false;
			}
			return true;
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x001B6401 File Offset: 0x001B5601
		private uint EventRegister(EventSource eventSource, Interop.Advapi32.EtwEnableCallback enableCallback)
		{
			this.m_providerName = eventSource.Name;
			this.m_providerId = eventSource.Guid;
			this.m_etwCallback = enableCallback;
			return this.m_eventProvider.EventRegister(eventSource, enableCallback, null, ref this.m_regHandle);
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x001B6437 File Offset: 0x001B5637
		private void EventUnregister(long registrationHandle)
		{
			this.m_eventProvider.EventUnregister(registrationHandle);
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x001B6448 File Offset: 0x001B5648
		internal unsafe int SetInformation(Interop.Advapi32.EVENT_INFO_CLASS eventInfoClass, IntPtr data, uint dataSize)
		{
			int result = 50;
			if (!EventProvider.m_setInformationMissing)
			{
				try
				{
					result = Interop.Advapi32.EventSetInformation(this.m_regHandle, eventInfoClass, (void*)data, (int)dataSize);
				}
				catch (TypeLoadException)
				{
					EventProvider.m_setInformationMissing = true;
				}
			}
			return result;
		}

		// Token: 0x04001A6A RID: 6762
		internal IEventProvider m_eventProvider;

		// Token: 0x04001A6B RID: 6763
		private Interop.Advapi32.EtwEnableCallback m_etwCallback;

		// Token: 0x04001A6C RID: 6764
		private long m_regHandle;

		// Token: 0x04001A6D RID: 6765
		private byte m_level;

		// Token: 0x04001A6E RID: 6766
		private long m_anyKeywordMask;

		// Token: 0x04001A6F RID: 6767
		private long m_allKeywordMask;

		// Token: 0x04001A70 RID: 6768
		private List<EventProvider.SessionInfo> m_liveSessions;

		// Token: 0x04001A71 RID: 6769
		private bool m_enabled;

		// Token: 0x04001A72 RID: 6770
		private string m_providerName;

		// Token: 0x04001A73 RID: 6771
		private Guid m_providerId;

		// Token: 0x04001A74 RID: 6772
		internal bool m_disposed;

		// Token: 0x04001A75 RID: 6773
		[ThreadStatic]
		private static EventProvider.WriteEventErrorCode s_returnCode;

		// Token: 0x04001A76 RID: 6774
		private static bool m_setInformationMissing;

		// Token: 0x02000723 RID: 1827
		public struct EventData
		{
			// Token: 0x04001A77 RID: 6775
			internal ulong Ptr;

			// Token: 0x04001A78 RID: 6776
			internal uint Size;

			// Token: 0x04001A79 RID: 6777
			internal uint Reserved;
		}

		// Token: 0x02000724 RID: 1828
		public struct SessionInfo
		{
			// Token: 0x06005A6A RID: 23146 RVA: 0x001B6490 File Offset: 0x001B5690
			internal SessionInfo(int sessionIdBit_, int etwSessionId_)
			{
				this.sessionIdBit = sessionIdBit_;
				this.etwSessionId = etwSessionId_;
			}

			// Token: 0x04001A7A RID: 6778
			internal int sessionIdBit;

			// Token: 0x04001A7B RID: 6779
			internal int etwSessionId;
		}

		// Token: 0x02000725 RID: 1829
		public enum WriteEventErrorCode
		{
			// Token: 0x04001A7D RID: 6781
			NoError,
			// Token: 0x04001A7E RID: 6782
			NoFreeBuffers,
			// Token: 0x04001A7F RID: 6783
			EventTooBig,
			// Token: 0x04001A80 RID: 6784
			NullInput,
			// Token: 0x04001A81 RID: 6785
			TooManyArgs,
			// Token: 0x04001A82 RID: 6786
			Other
		}

		// Token: 0x02000726 RID: 1830
		// (Invoke) Token: 0x06005A6C RID: 23148
		private delegate void SessionInfoCallback(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList);
	}
}
