using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000792 RID: 1938
	internal enum TraceLoggingDataType
	{
		// Token: 0x04001C40 RID: 7232
		Nil,
		// Token: 0x04001C41 RID: 7233
		Utf16String,
		// Token: 0x04001C42 RID: 7234
		MbcsString,
		// Token: 0x04001C43 RID: 7235
		Int8,
		// Token: 0x04001C44 RID: 7236
		UInt8,
		// Token: 0x04001C45 RID: 7237
		Int16,
		// Token: 0x04001C46 RID: 7238
		UInt16,
		// Token: 0x04001C47 RID: 7239
		Int32,
		// Token: 0x04001C48 RID: 7240
		UInt32,
		// Token: 0x04001C49 RID: 7241
		Int64,
		// Token: 0x04001C4A RID: 7242
		UInt64,
		// Token: 0x04001C4B RID: 7243
		Float,
		// Token: 0x04001C4C RID: 7244
		Double,
		// Token: 0x04001C4D RID: 7245
		Boolean32,
		// Token: 0x04001C4E RID: 7246
		Binary,
		// Token: 0x04001C4F RID: 7247
		Guid,
		// Token: 0x04001C50 RID: 7248
		FileTime = 17,
		// Token: 0x04001C51 RID: 7249
		SystemTime,
		// Token: 0x04001C52 RID: 7250
		HexInt32 = 20,
		// Token: 0x04001C53 RID: 7251
		HexInt64,
		// Token: 0x04001C54 RID: 7252
		CountedUtf16String,
		// Token: 0x04001C55 RID: 7253
		CountedMbcsString,
		// Token: 0x04001C56 RID: 7254
		Struct,
		// Token: 0x04001C57 RID: 7255
		Char16 = 518,
		// Token: 0x04001C58 RID: 7256
		Char8 = 516,
		// Token: 0x04001C59 RID: 7257
		Boolean8 = 772,
		// Token: 0x04001C5A RID: 7258
		HexInt8 = 1028,
		// Token: 0x04001C5B RID: 7259
		HexInt16 = 1030,
		// Token: 0x04001C5C RID: 7260
		Utf16Xml = 2817,
		// Token: 0x04001C5D RID: 7261
		MbcsXml,
		// Token: 0x04001C5E RID: 7262
		CountedUtf16Xml = 2838,
		// Token: 0x04001C5F RID: 7263
		CountedMbcsXml,
		// Token: 0x04001C60 RID: 7264
		Utf16Json = 3073,
		// Token: 0x04001C61 RID: 7265
		MbcsJson,
		// Token: 0x04001C62 RID: 7266
		CountedUtf16Json = 3094,
		// Token: 0x04001C63 RID: 7267
		CountedMbcsJson,
		// Token: 0x04001C64 RID: 7268
		HResult = 3847
	}
}
