using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000788 RID: 1928
	internal sealed class ScalarArrayTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D3F RID: 23871 RVA: 0x001C364B File Offset: 0x001C284B
		private ScalarArrayTypeInfo(Type type, Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType> formatFunc, TraceLoggingDataType nativeFormat, int elementSize) : base(type)
		{
			this.formatFunc = formatFunc;
			this.nativeFormat = nativeFormat;
			this.elementSize = elementSize;
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x001C366A File Offset: 0x001C286A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, this.formatFunc(format, this.nativeFormat));
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x001C3685 File Offset: 0x001C2885
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			collector.AddArray(value, this.elementSize);
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x001C3694 File Offset: 0x001C2894
		public static TraceLoggingTypeInfo Boolean()
		{
			return new ScalarArrayTypeInfo(typeof(bool[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.Boolean8, 1);
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x001C36B7 File Offset: 0x001C28B7
		public static TraceLoggingTypeInfo Byte()
		{
			return new ScalarArrayTypeInfo(typeof(byte[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.UInt8, 1);
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x001C36D6 File Offset: 0x001C28D6
		public static TraceLoggingTypeInfo SByte()
		{
			return new ScalarArrayTypeInfo(typeof(sbyte[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format8), TraceLoggingDataType.Int8, 1);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x001C36F5 File Offset: 0x001C28F5
		public static TraceLoggingTypeInfo Char()
		{
			return new ScalarArrayTypeInfo(typeof(char[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.Char16, 2);
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x001C3718 File Offset: 0x001C2918
		public static TraceLoggingTypeInfo Int16()
		{
			return new ScalarArrayTypeInfo(typeof(short[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.Int16, 2);
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x001C3737 File Offset: 0x001C2937
		public static TraceLoggingTypeInfo UInt16()
		{
			return new ScalarArrayTypeInfo(typeof(ushort[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format16), TraceLoggingDataType.UInt16, 2);
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x001C3756 File Offset: 0x001C2956
		public static TraceLoggingTypeInfo Int32()
		{
			return new ScalarArrayTypeInfo(typeof(int[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.Int32, 4);
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x001C3775 File Offset: 0x001C2975
		public static TraceLoggingTypeInfo UInt32()
		{
			return new ScalarArrayTypeInfo(typeof(uint[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.UInt32, 4);
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x001C3794 File Offset: 0x001C2994
		public static TraceLoggingTypeInfo Int64()
		{
			return new ScalarArrayTypeInfo(typeof(long[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.Int64, 8);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x001C37B4 File Offset: 0x001C29B4
		public static TraceLoggingTypeInfo UInt64()
		{
			return new ScalarArrayTypeInfo(typeof(ulong[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.UInt64, 8);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x001C37D4 File Offset: 0x001C29D4
		public static TraceLoggingTypeInfo IntPtr()
		{
			return new ScalarArrayTypeInfo(typeof(IntPtr[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.FormatPtr), Statics.IntPtrType, System.IntPtr.Size);
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x001C37FB File Offset: 0x001C29FB
		public static TraceLoggingTypeInfo UIntPtr()
		{
			return new ScalarArrayTypeInfo(typeof(UIntPtr[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.FormatPtr), Statics.UIntPtrType, System.IntPtr.Size);
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x001C3822 File Offset: 0x001C2A22
		public static TraceLoggingTypeInfo Single()
		{
			return new ScalarArrayTypeInfo(typeof(float[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format32), TraceLoggingDataType.Float, 4);
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x001C3842 File Offset: 0x001C2A42
		public static TraceLoggingTypeInfo Double()
		{
			return new ScalarArrayTypeInfo(typeof(double[]), new Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType>(Statics.Format64), TraceLoggingDataType.Double, 8);
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001C3862 File Offset: 0x001C2A62
		public static TraceLoggingTypeInfo Guid()
		{
			return new ScalarArrayTypeInfo(typeof(Guid), (EventFieldFormat f, TraceLoggingDataType t) => Statics.MakeDataType(TraceLoggingDataType.Guid, f), TraceLoggingDataType.Guid, sizeof(Guid));
		}

		// Token: 0x04001C34 RID: 7220
		private readonly Func<EventFieldFormat, TraceLoggingDataType, TraceLoggingDataType> formatFunc;

		// Token: 0x04001C35 RID: 7221
		private readonly TraceLoggingDataType nativeFormat;

		// Token: 0x04001C36 RID: 7222
		private readonly int elementSize;
	}
}
