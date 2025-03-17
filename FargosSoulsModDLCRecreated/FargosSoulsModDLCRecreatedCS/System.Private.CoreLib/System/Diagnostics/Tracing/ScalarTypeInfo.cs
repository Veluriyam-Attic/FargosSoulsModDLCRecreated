using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000786 RID: 1926
	internal sealed class ScalarTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D2A RID: 23850 RVA: 0x001C3410 File Offset: 0x001C2610
		private ScalarTypeInfo(Type type, Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType> formatFunc, TraceLoggingDataType nativeFormat) : base(type)
		{
			this.formatFunc = formatFunc;
			this.nativeFormat = nativeFormat;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001C3427 File Offset: 0x001C2627
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, this.formatFunc(format, this.nativeFormat));
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x001C3442 File Offset: 0x001C2642
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x001C344B File Offset: 0x001C264B
		public static TraceLoggingTypeInfo Boolean()
		{
			return new ScalarTypeInfo(typeof(bool), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.Boolean8);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x001C346D File Offset: 0x001C266D
		public static TraceLoggingTypeInfo Byte()
		{
			return new ScalarTypeInfo(typeof(byte), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.UInt8);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x001C348B File Offset: 0x001C268B
		public static TraceLoggingTypeInfo SByte()
		{
			return new ScalarTypeInfo(typeof(sbyte), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.Int8);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x001C34A9 File Offset: 0x001C26A9
		public static TraceLoggingTypeInfo Char()
		{
			return new ScalarTypeInfo(typeof(char), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.Char16);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x001C34CB File Offset: 0x001C26CB
		public static TraceLoggingTypeInfo Int16()
		{
			return new ScalarTypeInfo(typeof(short), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.Int16);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001C34E9 File Offset: 0x001C26E9
		public static TraceLoggingTypeInfo UInt16()
		{
			return new ScalarTypeInfo(typeof(ushort), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.UInt16);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001C3507 File Offset: 0x001C2707
		public static TraceLoggingTypeInfo Int32()
		{
			return new ScalarTypeInfo(typeof(int), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.Int32);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001C3525 File Offset: 0x001C2725
		public static TraceLoggingTypeInfo UInt32()
		{
			return new ScalarTypeInfo(typeof(uint), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.UInt32);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x001C3543 File Offset: 0x001C2743
		public static TraceLoggingTypeInfo Int64()
		{
			return new ScalarTypeInfo(typeof(long), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.Int64);
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x001C3562 File Offset: 0x001C2762
		public static TraceLoggingTypeInfo UInt64()
		{
			return new ScalarTypeInfo(typeof(ulong), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.UInt64);
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x001C3581 File Offset: 0x001C2781
		public static TraceLoggingTypeInfo IntPtr()
		{
			return new ScalarTypeInfo(typeof(IntPtr), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.FormatPtr), Statics.IntPtrType);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001C35A3 File Offset: 0x001C27A3
		public static TraceLoggingTypeInfo UIntPtr()
		{
			return new ScalarTypeInfo(typeof(UIntPtr), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.FormatPtr), Statics.UIntPtrType);
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x001C35C5 File Offset: 0x001C27C5
		public static TraceLoggingTypeInfo Single()
		{
			return new ScalarTypeInfo(typeof(float), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.Float);
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x001C35E4 File Offset: 0x001C27E4
		public static TraceLoggingTypeInfo Double()
		{
			return new ScalarTypeInfo(typeof(double), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.Double);
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x001C3603 File Offset: 0x001C2803
		public static TraceLoggingTypeInfo Guid()
		{
			return new ScalarTypeInfo(typeof(Guid), (EventFieldFormat f, TraceLoggingDataType t) => Statics.MakeDataType(TraceLoggingDataType.Guid, f), TraceLoggingDataType.Guid);
		}

		// Token: 0x04001C30 RID: 7216
		private readonly Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType> formatFunc;

		// Token: 0x04001C31 RID: 7217
		private readonly TraceLoggingDataType nativeFormat;
	}
}
