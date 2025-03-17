using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000798 RID: 1944
	internal abstract class TraceLoggingTypeInfo
	{
		// Token: 0x06005DA8 RID: 23976 RVA: 0x001C4C54 File Offset: 0x001C3E54
		internal TraceLoggingTypeInfo(Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			this.name = dataType.Name;
			this.dataType = dataType;
			this.propertyValueFactory = PropertyValue.GetFactory(dataType);
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x001C4CA8 File Offset: 0x001C3EA8
		internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Statics.CheckName(name);
			this.name = name;
			this.keywords = keywords;
			this.level = level;
			this.opcode = opcode;
			this.tags = tags;
			this.dataType = dataType;
			this.propertyValueFactory = PropertyValue.GetFactory(dataType);
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x001C4D2A File Offset: 0x001C3F2A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x001C4D32 File Offset: 0x001C3F32
		public EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x001C4D3A File Offset: 0x001C3F3A
		public EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x001C4D42 File Offset: 0x001C3F42
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x001C4D4A File Offset: 0x001C3F4A
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x001C4D52 File Offset: 0x001C3F52
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x001C4D5A File Offset: 0x001C3F5A
		internal Func<object, PropertyValue> PropertyValueFactory
		{
			get
			{
				return this.propertyValueFactory;
			}
		}

		// Token: 0x06005DB1 RID: 23985
		public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

		// Token: 0x06005DB2 RID: 23986
		public abstract void WriteData(TraceLoggingDataCollector collector, PropertyValue value);

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0011FF72 File Offset: 0x0011F172
		public virtual object GetData(object value)
		{
			return value;
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x001C4D64 File Offset: 0x001C3F64
		public static TraceLoggingTypeInfo GetInstance(Type type, List<Type> recursionCheck)
		{
			Dictionary<Type, TraceLoggingTypeInfo> dictionary;
			if ((dictionary = TraceLoggingTypeInfo.threadCache) == null)
			{
				dictionary = (TraceLoggingTypeInfo.threadCache = new Dictionary<Type, TraceLoggingTypeInfo>());
			}
			Dictionary<Type, TraceLoggingTypeInfo> dictionary2 = dictionary;
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (!dictionary2.TryGetValue(type, out traceLoggingTypeInfo))
			{
				if (recursionCheck == null)
				{
					recursionCheck = new List<Type>();
				}
				int count = recursionCheck.Count;
				traceLoggingTypeInfo = Statics.CreateDefaultTypeInfo(type, recursionCheck);
				dictionary2[type] = traceLoggingTypeInfo;
				recursionCheck.RemoveRange(count, recursionCheck.Count - count);
			}
			return traceLoggingTypeInfo;
		}

		// Token: 0x04001C7E RID: 7294
		private readonly string name;

		// Token: 0x04001C7F RID: 7295
		private readonly EventKeywords keywords;

		// Token: 0x04001C80 RID: 7296
		private readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x04001C81 RID: 7297
		private readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x04001C82 RID: 7298
		private readonly EventTags tags;

		// Token: 0x04001C83 RID: 7299
		private readonly Type dataType;

		// Token: 0x04001C84 RID: 7300
		private readonly Func<object, PropertyValue> propertyValueFactory;

		// Token: 0x04001C85 RID: 7301
		[ThreadStatic]
		private static Dictionary<Type, TraceLoggingTypeInfo> threadCache;
	}
}
