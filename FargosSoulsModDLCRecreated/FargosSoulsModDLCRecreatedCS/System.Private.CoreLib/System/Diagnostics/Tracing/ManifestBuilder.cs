using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200073E RID: 1854
	internal class ManifestBuilder
	{
		// Token: 0x06005B61 RID: 23393 RVA: 0x001BC684 File Offset: 0x001BB884
		public ManifestBuilder(string providerName, Guid providerGuid, string dllName, ResourceManager resources, EventManifestOptions flags)
		{
			this.providerName = providerName;
			this.flags = flags;
			this.resources = resources;
			this.sb = new StringBuilder();
			this.events = new StringBuilder();
			this.templates = new StringBuilder();
			this.opcodeTab = new Dictionary<int, string>();
			this.stringTab = new Dictionary<string, string>();
			this.errors = new List<string>();
			this.perEventByteArrayArgIndices = new Dictionary<string, List<int>>();
			this.sb.AppendLine("<instrumentationManifest xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
			this.sb.AppendLine(" <instrumentation xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:win=\"http://manifests.microsoft.com/win/2004/08/windows/events\">");
			this.sb.AppendLine("  <events xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
			this.sb.Append("<provider name=\"").Append(providerName).Append("\" guid=\"{").Append(providerGuid.ToString()).Append('}');
			if (dllName != null)
			{
				this.sb.Append("\" resourceFileName=\"").Append(dllName).Append("\" messageFileName=\"").Append(dllName);
			}
			string value = providerName.Replace("-", "").Replace('.', '_');
			this.sb.Append("\" symbol=\"").Append(value);
			this.sb.AppendLine("\">");
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x001BC7E4 File Offset: 0x001BB9E4
		public void AddOpcode(string name, int value)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value <= 10 || value >= 239)
				{
					this.ManifestError(SR.Format(SR.EventSource_IllegalOpcodeValue, name, value), false);
				}
				string text;
				if (this.opcodeTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(SR.Format(SR.EventSource_OpcodeCollision, name, text, value), false);
				}
			}
			this.opcodeTab[value] = name;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x001BC860 File Offset: 0x001BBA60
		public void AddTask(string name, int value)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value <= 0 || value >= 65535)
				{
					this.ManifestError(SR.Format(SR.EventSource_IllegalTaskValue, name, value), false);
				}
				string text;
				if (this.taskTab != null && this.taskTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(SR.Format(SR.EventSource_TaskCollision, name, text, value), false);
				}
			}
			if (this.taskTab == null)
			{
				this.taskTab = new Dictionary<int, string>();
			}
			this.taskTab[value] = name;
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x001BC8F8 File Offset: 0x001BBAF8
		public void AddKeyword(string name, ulong value)
		{
			if ((value & value - 1UL) != 0UL)
			{
				this.ManifestError(SR.Format(SR.EventSource_KeywordNeedPowerOfTwo, "0x" + value.ToString("x", CultureInfo.CurrentCulture), name), true);
			}
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				if (value >= 17592186044416UL && !name.StartsWith("Session", StringComparison.Ordinal))
				{
					this.ManifestError(SR.Format(SR.EventSource_IllegalKeywordsValue, name, "0x" + value.ToString("x", CultureInfo.CurrentCulture)), false);
				}
				string text;
				if (this.keywordTab != null && this.keywordTab.TryGetValue(value, out text) && !name.Equals(text, StringComparison.Ordinal))
				{
					this.ManifestError(SR.Format(SR.EventSource_KeywordCollision, name, text, "0x" + value.ToString("x", CultureInfo.CurrentCulture)), false);
				}
			}
			if (this.keywordTab == null)
			{
				this.keywordTab = new Dictionary<ulong, string>();
			}
			this.keywordTab[value] = name;
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x001BCA00 File Offset: 0x001BBC00
		public void AddChannel(string name, int value, EventChannelAttribute channelAttribute)
		{
			EventChannel eventChannel = (EventChannel)value;
			if (value < 16 || value > 255)
			{
				this.ManifestError(SR.Format(SR.EventSource_EventChannelOutOfRange, name, value), false);
			}
			else if (eventChannel >= EventChannel.Admin && eventChannel <= EventChannel.Debug && channelAttribute != null && ManifestBuilder.EventChannelToChannelType(eventChannel) != channelAttribute.EventChannelType)
			{
				this.ManifestError(SR.Format(SR.EventSource_ChannelTypeDoesNotMatchEventChannelValue, name, ((EventChannel)value).ToString()), false);
			}
			ulong channelKeyword = this.GetChannelKeyword(eventChannel, 0UL);
			if (this.channelTab == null)
			{
				this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
			}
			this.channelTab[value] = new ManifestBuilder.ChannelInfo
			{
				Name = name,
				Keywords = channelKeyword,
				Attribs = channelAttribute
			};
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x001BCAB8 File Offset: 0x001BBCB8
		private static EventChannelType EventChannelToChannelType(EventChannel channel)
		{
			return (EventChannelType)(channel - 16 + 1);
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x001BCAC0 File Offset: 0x001BBCC0
		private static EventChannelAttribute GetDefaultChannelAttribute(EventChannel channel)
		{
			EventChannelAttribute eventChannelAttribute = new EventChannelAttribute();
			eventChannelAttribute.EventChannelType = ManifestBuilder.EventChannelToChannelType(channel);
			if (eventChannelAttribute.EventChannelType <= EventChannelType.Operational)
			{
				eventChannelAttribute.Enabled = true;
			}
			return eventChannelAttribute;
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x001BCAF0 File Offset: 0x001BBCF0
		public ulong[] GetChannelData()
		{
			if (this.channelTab == null)
			{
				return Array.Empty<ulong>();
			}
			int num = -1;
			foreach (int num2 in this.channelTab.Keys)
			{
				if (num2 > num)
				{
					num = num2;
				}
			}
			ulong[] array = new ulong[num + 1];
			foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in this.channelTab)
			{
				array[keyValuePair.Key] = keyValuePair.Value.Keywords;
			}
			return array;
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x001BCBB4 File Offset: 0x001BBDB4
		public void StartEvent(string eventName, EventAttribute eventAttribute)
		{
			this.eventName = eventName;
			this.numParams = 0;
			this.byteArrArgIndices = null;
			this.events.Append("  <event").Append(" value=\"").Append(eventAttribute.EventId).Append('"').Append(" version=\"").Append(eventAttribute.Version).Append('"').Append(" level=\"").Append(ManifestBuilder.GetLevelName(eventAttribute.Level)).Append('"').Append(" symbol=\"").Append(eventName).Append('"');
			this.WriteMessageAttrib(this.events, "event", eventName, eventAttribute.Message);
			if (eventAttribute.Keywords != EventKeywords.None)
			{
				this.events.Append(" keywords=\"").Append(this.GetKeywords((ulong)eventAttribute.Keywords, eventName)).Append('"');
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				this.events.Append(" opcode=\"").Append(this.GetOpcodeName(eventAttribute.Opcode, eventName)).Append('"');
			}
			if (eventAttribute.Task != EventTask.None)
			{
				this.events.Append(" task=\"").Append(this.GetTaskName(eventAttribute.Task, eventName)).Append('"');
			}
			if (eventAttribute.Channel != EventChannel.None)
			{
				this.events.Append(" channel=\"").Append(this.GetChannelName(eventAttribute.Channel, eventName, eventAttribute.Message)).Append('"');
			}
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x001BCD40 File Offset: 0x001BBF40
		public void AddEventParameter(Type type, string name)
		{
			if (this.numParams == 0)
			{
				this.templates.Append("  <template tid=\"").Append(this.eventName).AppendLine("Args\">");
			}
			if (type == typeof(byte[]))
			{
				if (this.byteArrArgIndices == null)
				{
					this.byteArrArgIndices = new List<int>(4);
				}
				this.byteArrArgIndices.Add(this.numParams);
				this.numParams++;
				this.templates.Append("   <data name=\"").Append(name).AppendLine("Size\" inType=\"win:UInt32\"/>");
			}
			this.numParams++;
			this.templates.Append("   <data name=\"").Append(name).Append("\" inType=\"").Append(this.GetTypeName(type)).Append('"');
			if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof(byte))
			{
				this.templates.Append(" length=\"").Append(name).Append("Size\"");
			}
			if (type.IsEnum && Enum.GetUnderlyingType(type) != typeof(ulong) && Enum.GetUnderlyingType(type) != typeof(long))
			{
				this.templates.Append(" map=\"").Append(type.Name).Append('"');
				if (this.mapsTab == null)
				{
					this.mapsTab = new Dictionary<string, Type>();
				}
				if (!this.mapsTab.ContainsKey(type.Name))
				{
					this.mapsTab.Add(type.Name, type);
				}
			}
			this.templates.AppendLine("/>");
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x001BCF10 File Offset: 0x001BC110
		public void EndEvent()
		{
			if (this.numParams > 0)
			{
				this.templates.AppendLine("  </template>");
				this.events.Append(" template=\"").Append(this.eventName).Append("Args\"");
			}
			this.events.AppendLine("/>");
			if (this.byteArrArgIndices != null)
			{
				this.perEventByteArrayArgIndices[this.eventName] = this.byteArrArgIndices;
			}
			string text;
			if (this.stringTab.TryGetValue("event_" + this.eventName, out text))
			{
				text = this.TranslateToManifestConvention(text, this.eventName);
				this.stringTab["event_" + this.eventName] = text;
			}
			this.eventName = null;
			this.numParams = 0;
			this.byteArrArgIndices = null;
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x001BCFEC File Offset: 0x001BC1EC
		public ulong GetChannelKeyword(EventChannel channel, ulong channelKeyword = 0UL)
		{
			channelKeyword &= 17293822569102704640UL;
			if (this.channelTab == null)
			{
				this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
			}
			if (this.channelTab.Count == 8)
			{
				this.ManifestError(SR.EventSource_MaxChannelExceeded, false);
			}
			ManifestBuilder.ChannelInfo channelInfo;
			if (!this.channelTab.TryGetValue((int)channel, out channelInfo))
			{
				if (channelKeyword != 0UL)
				{
					channelKeyword = this.nextChannelKeywordBit;
					this.nextChannelKeywordBit >>= 1;
				}
			}
			else
			{
				channelKeyword = channelInfo.Keywords;
			}
			return channelKeyword;
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x001BD068 File Offset: 0x001BC268
		public byte[] CreateManifest()
		{
			string s = this.CreateManifestString();
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x001BD087 File Offset: 0x001BC287
		public IList<string> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x001BD08F File Offset: 0x001BC28F
		public void ManifestError(string msg, bool runtimeCritical = false)
		{
			if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
			{
				this.errors.Add(msg);
				return;
			}
			if (runtimeCritical)
			{
				throw new ArgumentException(msg);
			}
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x001BD0B4 File Offset: 0x001BC2B4
		private string CreateManifestString()
		{
			if (this.channelTab != null)
			{
				this.sb.AppendLine(" <channels>");
				List<KeyValuePair<int, ManifestBuilder.ChannelInfo>> list = new List<KeyValuePair<int, ManifestBuilder.ChannelInfo>>();
				foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> item in this.channelTab)
				{
					list.Add(item);
				}
				list.Sort((KeyValuePair<int, ManifestBuilder.ChannelInfo> p1, KeyValuePair<int, ManifestBuilder.ChannelInfo> p2) => -Comparer<ulong>.Default.Compare(p1.Value.Keywords, p2.Value.Keywords));
				foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in list)
				{
					int key = keyValuePair.Key;
					ManifestBuilder.ChannelInfo value = keyValuePair.Value;
					string text = null;
					bool flag = false;
					string text2 = null;
					if (value.Attribs != null)
					{
						EventChannelAttribute attribs = value.Attribs;
						if (Enum.IsDefined(typeof(EventChannelType), attribs.EventChannelType))
						{
							text = attribs.EventChannelType.ToString();
						}
						flag = attribs.Enabled;
					}
					if (text2 == null)
					{
						text2 = this.providerName + "/" + value.Name;
					}
					this.sb.Append("  <").Append("channel");
					this.sb.Append(" chid=\"").Append(value.Name).Append('"');
					this.sb.Append(" name=\"").Append(text2).Append('"');
					this.WriteMessageAttrib(this.sb, "channel", value.Name, null);
					this.sb.Append(" value=\"").Append(key).Append('"');
					if (text != null)
					{
						this.sb.Append(" type=\"").Append(text).Append('"');
					}
					this.sb.Append(" enabled=\"").Append(flag ? "true" : "false").Append('"');
					this.sb.AppendLine("/>");
				}
				this.sb.AppendLine(" </channels>");
			}
			if (this.taskTab != null)
			{
				this.sb.AppendLine(" <tasks>");
				List<int> list2 = new List<int>(this.taskTab.Keys);
				list2.Sort();
				foreach (int num in list2)
				{
					this.sb.Append("  <task");
					this.WriteNameAndMessageAttribs(this.sb, "task", this.taskTab[num]);
					this.sb.Append(" value=\"").Append(num).AppendLine("\"/>");
				}
				this.sb.AppendLine(" </tasks>");
			}
			if (this.mapsTab != null)
			{
				this.sb.AppendLine(" <maps>");
				foreach (Type type in this.mapsTab.Values)
				{
					bool flag2 = EventSource.GetCustomAttributeHelper(type, typeof(FlagsAttribute), this.flags) != null;
					string value2 = flag2 ? "bitMap" : "valueMap";
					this.sb.Append("  <").Append(value2).Append(" name=\"").Append(type.Name).AppendLine("\">");
					FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
					bool flag3 = false;
					foreach (FieldInfo fieldInfo in fields)
					{
						object rawConstantValue = fieldInfo.GetRawConstantValue();
						if (rawConstantValue != null)
						{
							ulong num2;
							if (rawConstantValue is ulong)
							{
								num2 = (ulong)rawConstantValue;
							}
							else
							{
								num2 = (ulong)Convert.ToInt64(rawConstantValue);
							}
							if (!flag2 || ((num2 & num2 - 1UL) == 0UL && num2 != 0UL))
							{
								this.sb.Append("   <map value=\"0x").Append(num2.ToString("x", CultureInfo.InvariantCulture)).Append('"');
								this.WriteMessageAttrib(this.sb, "map", type.Name + "." + fieldInfo.Name, fieldInfo.Name);
								this.sb.AppendLine("/>");
								flag3 = true;
							}
						}
					}
					if (!flag3)
					{
						this.sb.Append("   <map value=\"0x0\"");
						this.WriteMessageAttrib(this.sb, "map", type.Name + ".None", "None");
						this.sb.AppendLine("/>");
					}
					this.sb.Append("  </").Append(value2).AppendLine(">");
				}
				this.sb.AppendLine(" </maps>");
			}
			this.sb.AppendLine(" <opcodes>");
			List<int> list3 = new List<int>(this.opcodeTab.Keys);
			list3.Sort();
			foreach (int num3 in list3)
			{
				this.sb.Append("  <opcode");
				this.WriteNameAndMessageAttribs(this.sb, "opcode", this.opcodeTab[num3]);
				this.sb.Append(" value=\"").Append(num3).AppendLine("\"/>");
			}
			this.sb.AppendLine(" </opcodes>");
			if (this.keywordTab != null)
			{
				this.sb.AppendLine(" <keywords>");
				List<ulong> list4 = new List<ulong>(this.keywordTab.Keys);
				list4.Sort();
				foreach (ulong key2 in list4)
				{
					this.sb.Append("  <keyword");
					this.WriteNameAndMessageAttribs(this.sb, "keyword", this.keywordTab[key2]);
					this.sb.Append(" mask=\"0x").Append(key2.ToString("x", CultureInfo.InvariantCulture)).AppendLine("\"/>");
				}
				this.sb.AppendLine(" </keywords>");
			}
			this.sb.AppendLine(" <events>");
			this.sb.Append(this.events);
			this.sb.AppendLine(" </events>");
			this.sb.AppendLine(" <templates>");
			if (this.templates.Length > 0)
			{
				this.sb.Append(this.templates);
			}
			else
			{
				this.sb.AppendLine("    <template tid=\"_empty\"></template>");
			}
			this.sb.AppendLine(" </templates>");
			this.sb.AppendLine("</provider>");
			this.sb.AppendLine("</events>");
			this.sb.AppendLine("</instrumentation>");
			this.sb.AppendLine("<localization>");
			List<CultureInfo> list5;
			if (this.resources == null || (this.flags & EventManifestOptions.AllCultures) == EventManifestOptions.None)
			{
				(list5 = new List<CultureInfo>()).Add(CultureInfo.CurrentUICulture);
			}
			else
			{
				list5 = ManifestBuilder.GetSupportedCultures();
			}
			List<CultureInfo> list6 = list5;
			string[] array2 = new string[this.stringTab.Keys.Count];
			this.stringTab.Keys.CopyTo(array2, 0);
			Array.Sort<string>(array2, 0, array2.Length);
			foreach (CultureInfo cultureInfo in list6)
			{
				this.sb.Append(" <resources culture=\"").Append(cultureInfo.Name).AppendLine("\">");
				this.sb.AppendLine("  <stringTable>");
				foreach (string text3 in array2)
				{
					string localizedMessage = this.GetLocalizedMessage(text3, cultureInfo, true);
					this.sb.Append("   <string id=\"").Append(text3).Append("\" value=\"").Append(localizedMessage).AppendLine("\"/>");
				}
				this.sb.AppendLine("  </stringTable>");
				this.sb.AppendLine(" </resources>");
			}
			this.sb.AppendLine("</localization>");
			this.sb.AppendLine("</instrumentationManifest>");
			return this.sb.ToString();
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001BDA5C File Offset: 0x001BCC5C
		private void WriteNameAndMessageAttribs(StringBuilder stringBuilder, string elementName, string name)
		{
			stringBuilder.Append(" name=\"").Append(name).Append('"');
			this.WriteMessageAttrib(this.sb, elementName, name, name);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x001BDA88 File Offset: 0x001BCC88
		private void WriteMessageAttrib(StringBuilder stringBuilder, string elementName, string name, string value)
		{
			string text = elementName + "_" + name;
			if (this.resources != null)
			{
				string @string = this.resources.GetString(text, CultureInfo.InvariantCulture);
				if (@string != null)
				{
					value = @string;
				}
			}
			if (value == null)
			{
				return;
			}
			stringBuilder.Append(" message=\"$(string.").Append(text).Append(")\"");
			string text2;
			if (this.stringTab.TryGetValue(text, out text2) && !text2.Equals(value))
			{
				this.ManifestError(SR.Format(SR.EventSource_DuplicateStringKey, text), true);
				return;
			}
			this.stringTab[text] = value;
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x001BDB20 File Offset: 0x001BCD20
		internal string GetLocalizedMessage(string key, CultureInfo ci, bool etwFormat)
		{
			string text = null;
			if (this.resources != null)
			{
				string @string = this.resources.GetString(key, ci);
				if (@string != null)
				{
					text = @string;
					if (etwFormat && key.StartsWith("event_", StringComparison.Ordinal))
					{
						string evtName = key.Substring("event_".Length);
						text = this.TranslateToManifestConvention(text, evtName);
					}
				}
			}
			if (etwFormat && text == null)
			{
				this.stringTab.TryGetValue(key, out text);
			}
			return text;
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x001BDB8C File Offset: 0x001BCD8C
		private static List<CultureInfo> GetSupportedCultures()
		{
			List<CultureInfo> list = new List<CultureInfo>();
			if (!list.Contains(CultureInfo.CurrentUICulture))
			{
				list.Insert(0, CultureInfo.CurrentUICulture);
			}
			return list;
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x001BDBB9 File Offset: 0x001BCDB9
		private static string GetLevelName(EventLevel level)
		{
			return ((level >= (EventLevel)16) ? "" : "win:") + level.ToString();
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x001BDBE0 File Offset: 0x001BCDE0
		private string GetChannelName(EventChannel channel, string eventName, string eventMessage)
		{
			ManifestBuilder.ChannelInfo channelInfo;
			if (this.channelTab == null || !this.channelTab.TryGetValue((int)channel, out channelInfo))
			{
				if (channel < EventChannel.Admin)
				{
					this.ManifestError(SR.Format(SR.EventSource_UndefinedChannel, channel, eventName), false);
				}
				if (this.channelTab == null)
				{
					this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
				}
				string text = channel.ToString();
				if (EventChannel.Debug < channel)
				{
					text = "Channel" + text;
				}
				this.AddChannel(text, (int)channel, ManifestBuilder.GetDefaultChannelAttribute(channel));
				if (!this.channelTab.TryGetValue((int)channel, out channelInfo))
				{
					this.ManifestError(SR.Format(SR.EventSource_UndefinedChannel, channel, eventName), false);
				}
			}
			if (this.resources != null && eventMessage == null)
			{
				eventMessage = this.resources.GetString("event_" + eventName, CultureInfo.InvariantCulture);
			}
			if (channelInfo.Attribs.EventChannelType == EventChannelType.Admin && eventMessage == null)
			{
				this.ManifestError(SR.Format(SR.EventSource_EventWithAdminChannelMustHaveMessage, eventName, channelInfo.Name), false);
			}
			return channelInfo.Name;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x001BDCE8 File Offset: 0x001BCEE8
		private string GetTaskName(EventTask task, string eventName)
		{
			if (task == EventTask.None)
			{
				return "";
			}
			if (this.taskTab == null)
			{
				this.taskTab = new Dictionary<int, string>();
			}
			string result;
			if (!this.taskTab.TryGetValue((int)task, out result))
			{
				this.taskTab[(int)task] = eventName;
				result = eventName;
			}
			return result;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x001BDD34 File Offset: 0x001BCF34
		private string GetOpcodeName(EventOpcode opcode, string eventName)
		{
			switch (opcode)
			{
			case EventOpcode.Info:
				return "win:Info";
			case EventOpcode.Start:
				return "win:Start";
			case EventOpcode.Stop:
				return "win:Stop";
			case EventOpcode.DataCollectionStart:
				return "win:DC_Start";
			case EventOpcode.DataCollectionStop:
				return "win:DC_Stop";
			case EventOpcode.Extension:
				return "win:Extension";
			case EventOpcode.Reply:
				return "win:Reply";
			case EventOpcode.Resume:
				return "win:Resume";
			case EventOpcode.Suspend:
				return "win:Suspend";
			case EventOpcode.Send:
				return "win:Send";
			default:
				if (opcode != EventOpcode.Receive)
				{
					string result;
					if (this.opcodeTab == null || !this.opcodeTab.TryGetValue((int)opcode, out result))
					{
						this.ManifestError(SR.Format(SR.EventSource_UndefinedOpcode, opcode, eventName), true);
						result = null;
					}
					return result;
				}
				return "win:Receive";
			}
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x001BDDF0 File Offset: 0x001BCFF0
		private string GetKeywords(ulong keywords, string eventName)
		{
			keywords &= 1152921504606846975UL;
			string text = "";
			for (ulong num = 1UL; num != 0UL; num <<= 1)
			{
				if ((keywords & num) != 0UL)
				{
					string text2 = null;
					if ((this.keywordTab == null || !this.keywordTab.TryGetValue(num, out text2)) && num >= 281474976710656UL)
					{
						text2 = string.Empty;
					}
					if (text2 == null)
					{
						this.ManifestError(SR.Format(SR.EventSource_UndefinedKeyword, "0x" + num.ToString("x", CultureInfo.CurrentCulture), eventName), true);
						text2 = string.Empty;
					}
					if (text.Length != 0 && text2.Length != 0)
					{
						text += " ";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x001BDEB4 File Offset: 0x001BD0B4
		private string GetTypeName(Type type)
		{
			if (type.IsEnum)
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				string typeName = this.GetTypeName(fields[0].FieldType);
				return typeName.Replace("win:Int", "win:UInt");
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return "win:Boolean";
			case TypeCode.Char:
			case TypeCode.UInt16:
				return "win:UInt16";
			case TypeCode.SByte:
				return "win:Int8";
			case TypeCode.Byte:
				return "win:UInt8";
			case TypeCode.Int16:
				return "win:Int16";
			case TypeCode.Int32:
				return "win:Int32";
			case TypeCode.UInt32:
				return "win:UInt32";
			case TypeCode.Int64:
				return "win:Int64";
			case TypeCode.UInt64:
				return "win:UInt64";
			case TypeCode.Single:
				return "win:Float";
			case TypeCode.Double:
				return "win:Double";
			case TypeCode.DateTime:
				return "win:FILETIME";
			case TypeCode.String:
				return "win:UnicodeString";
			}
			if (type == typeof(Guid))
			{
				return "win:GUID";
			}
			if (type == typeof(IntPtr))
			{
				return "win:Pointer";
			}
			if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof(byte))
			{
				return "win:Binary";
			}
			this.ManifestError(SR.Format(SR.EventSource_UnsupportedEventTypeInManifest, type.Name), true);
			return string.Empty;
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x001BE00A File Offset: 0x001BD20A
		private static void UpdateStringBuilder([NotNull] ref StringBuilder stringBuilder, string eventMessage, int startIndex, int count)
		{
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder();
			}
			stringBuilder.Append(eventMessage, startIndex, count);
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x001BE024 File Offset: 0x001BD224
		private string TranslateToManifestConvention(string eventMessage, string evtName)
		{
			StringBuilder stringBuilder = null;
			int num = 0;
			int i = 0;
			while (i < eventMessage.Length)
			{
				int num4;
				if (eventMessage[i] == '%')
				{
					ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, num, i - num);
					stringBuilder.Append("%%");
					i++;
					num = i;
				}
				else if (i < eventMessage.Length - 1 && ((eventMessage[i] == '{' && eventMessage[i + 1] == '{') || (eventMessage[i] == '}' && eventMessage[i + 1] == '}')))
				{
					ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, num, i - num);
					stringBuilder.Append(eventMessage[i]);
					i++;
					i++;
					num = i;
				}
				else if (eventMessage[i] == '{')
				{
					int num2 = i;
					i++;
					int num3 = 0;
					while (i < eventMessage.Length && char.IsDigit(eventMessage[i]))
					{
						num3 = num3 * 10 + (int)eventMessage[i] - 48;
						i++;
					}
					if (i < eventMessage.Length && eventMessage[i] == '}')
					{
						i++;
						ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, num, num2 - num);
						int value = this.TranslateIndexToManifestConvention(num3, evtName);
						stringBuilder.Append('%').Append(value);
						if (i < eventMessage.Length && eventMessage[i] == '!')
						{
							i++;
							stringBuilder.Append("%!");
						}
						num = i;
					}
					else
					{
						this.ManifestError(SR.Format(SR.EventSource_UnsupportedMessageProperty, evtName, eventMessage), false);
					}
				}
				else if ((num4 = "&<>'\"\r\n\t".IndexOf(eventMessage[i])) >= 0)
				{
					ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, num, i - num);
					i++;
					stringBuilder.Append(ManifestBuilder.s_escapes[num4]);
					num = i;
				}
				else
				{
					i++;
				}
			}
			if (stringBuilder == null)
			{
				return eventMessage;
			}
			ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, num, i - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x001BE1F4 File Offset: 0x001BD3F4
		private int TranslateIndexToManifestConvention(int idx, string evtName)
		{
			List<int> list;
			if (this.perEventByteArrayArgIndices.TryGetValue(evtName, out list))
			{
				foreach (int num in list)
				{
					if (idx < num)
					{
						break;
					}
					idx++;
				}
			}
			return idx + 1;
		}

		// Token: 0x04001B03 RID: 6915
		private static readonly string[] s_escapes = new string[]
		{
			"&amp;",
			"&lt;",
			"&gt;",
			"&apos;",
			"&quot;",
			"%r",
			"%n",
			"%t"
		};

		// Token: 0x04001B04 RID: 6916
		private readonly Dictionary<int, string> opcodeTab;

		// Token: 0x04001B05 RID: 6917
		private Dictionary<int, string> taskTab;

		// Token: 0x04001B06 RID: 6918
		private Dictionary<int, ManifestBuilder.ChannelInfo> channelTab;

		// Token: 0x04001B07 RID: 6919
		private Dictionary<ulong, string> keywordTab;

		// Token: 0x04001B08 RID: 6920
		private Dictionary<string, Type> mapsTab;

		// Token: 0x04001B09 RID: 6921
		private readonly Dictionary<string, string> stringTab;

		// Token: 0x04001B0A RID: 6922
		private ulong nextChannelKeywordBit = 9223372036854775808UL;

		// Token: 0x04001B0B RID: 6923
		private readonly StringBuilder sb;

		// Token: 0x04001B0C RID: 6924
		private readonly StringBuilder events;

		// Token: 0x04001B0D RID: 6925
		private readonly StringBuilder templates;

		// Token: 0x04001B0E RID: 6926
		private readonly string providerName;

		// Token: 0x04001B0F RID: 6927
		private readonly ResourceManager resources;

		// Token: 0x04001B10 RID: 6928
		private readonly EventManifestOptions flags;

		// Token: 0x04001B11 RID: 6929
		private readonly IList<string> errors;

		// Token: 0x04001B12 RID: 6930
		private readonly Dictionary<string, List<int>> perEventByteArrayArgIndices;

		// Token: 0x04001B13 RID: 6931
		private string eventName;

		// Token: 0x04001B14 RID: 6932
		private int numParams;

		// Token: 0x04001B15 RID: 6933
		private List<int> byteArrArgIndices;

		// Token: 0x0200073F RID: 1855
		private class ChannelInfo
		{
			// Token: 0x04001B16 RID: 6934
			public string Name;

			// Token: 0x04001B17 RID: 6935
			public ulong Keywords;

			// Token: 0x04001B18 RID: 6936
			public EventChannelAttribute Attribs;
		}
	}
}
