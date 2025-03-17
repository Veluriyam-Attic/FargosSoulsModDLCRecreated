using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000795 RID: 1941
	public class TraceLoggingEventTypes
	{
		// Token: 0x06005D87 RID: 23943 RVA: 0x001C44E7 File Offset: 0x001C36E7
		internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types) : this(tags, name, TraceLoggingEventTypes.MakeArray(types))
		{
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x001C44F7 File Offset: 0x001C36F7
		internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos) : this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
		{
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x001C4508 File Offset: 0x001C3708
		internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.typeInfos = this.MakeArray(paramInfos);
			this.paramNames = TraceLoggingEventTypes.MakeParamNameArray(paramInfos);
			this.name = name;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			for (int i = 0; i < this.typeInfos.Length; i++)
			{
				TraceLoggingTypeInfo traceLoggingTypeInfo = this.typeInfos[i];
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				string fieldName = paramInfos[i].Name;
				if (Statics.ShouldOverrideFieldName(fieldName))
				{
					fieldName = traceLoggingTypeInfo.Name;
				}
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, fieldName, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x001C460C File Offset: 0x001C380C
		private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
		{
			if (defaultName == null)
			{
				throw new ArgumentNullException("defaultName");
			}
			this.typeInfos = typeInfos;
			this.name = defaultName;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			foreach (TraceLoggingTypeInfo traceLoggingTypeInfo in typeInfos)
			{
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, null, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x001C46DD File Offset: 0x001C38DD
		[Nullable(1)]
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x001C46E5 File Offset: 0x001C38E5
		internal EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001C46ED File Offset: 0x001C38ED
		internal NameInfo GetNameInfo(string name, EventTags tags)
		{
			return this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags)) ?? this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001C4720 File Offset: 0x001C3920
		private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
		{
			if (paramInfos == null)
			{
				throw new ArgumentNullException("paramInfos");
			}
			List<Type> recursionCheck = new List<Type>(paramInfos.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				array[i] = TraceLoggingTypeInfo.GetInstance(paramInfos[i].ParameterType, recursionCheck);
			}
			return array;
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001C4770 File Offset: 0x001C3970
		private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			List<Type> recursionCheck = new List<Type>(types.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = TraceLoggingTypeInfo.GetInstance(types[i], recursionCheck);
			}
			return array;
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001C47B8 File Offset: 0x001C39B8
		private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
		{
			if (typeInfos == null)
			{
				throw new ArgumentNullException("typeInfos");
			}
			return (TraceLoggingTypeInfo[])typeInfos.Clone();
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x001C47D4 File Offset: 0x001C39D4
		private static string[] MakeParamNameArray(ParameterInfo[] paramInfos)
		{
			string[] array = new string[paramInfos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = paramInfos[i].Name;
			}
			return array;
		}

		// Token: 0x04001C68 RID: 7272
		internal readonly TraceLoggingTypeInfo[] typeInfos;

		// Token: 0x04001C69 RID: 7273
		internal readonly string[] paramNames;

		// Token: 0x04001C6A RID: 7274
		internal readonly string name;

		// Token: 0x04001C6B RID: 7275
		internal readonly EventTags tags;

		// Token: 0x04001C6C RID: 7276
		internal readonly byte level;

		// Token: 0x04001C6D RID: 7277
		internal readonly byte opcode;

		// Token: 0x04001C6E RID: 7278
		internal readonly EventKeywords keywords;

		// Token: 0x04001C6F RID: 7279
		internal readonly byte[] typeMetadata;

		// Token: 0x04001C70 RID: 7280
		internal readonly int scratchSize;

		// Token: 0x04001C71 RID: 7281
		internal readonly int dataCount;

		// Token: 0x04001C72 RID: 7282
		internal readonly int pinCount;

		// Token: 0x04001C73 RID: 7283
		private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;
	}
}
