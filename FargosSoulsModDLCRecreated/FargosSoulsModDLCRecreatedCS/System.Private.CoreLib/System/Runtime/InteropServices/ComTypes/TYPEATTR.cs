using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004CF RID: 1231
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04001014 RID: 4116
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04001015 RID: 4117
		public Guid guid;

		// Token: 0x04001016 RID: 4118
		public int lcid;

		// Token: 0x04001017 RID: 4119
		public int dwReserved;

		// Token: 0x04001018 RID: 4120
		public int memidConstructor;

		// Token: 0x04001019 RID: 4121
		public int memidDestructor;

		// Token: 0x0400101A RID: 4122
		public IntPtr lpstrSchema;

		// Token: 0x0400101B RID: 4123
		public int cbSizeInstance;

		// Token: 0x0400101C RID: 4124
		public TYPEKIND typekind;

		// Token: 0x0400101D RID: 4125
		public short cFuncs;

		// Token: 0x0400101E RID: 4126
		public short cVars;

		// Token: 0x0400101F RID: 4127
		public short cImplTypes;

		// Token: 0x04001020 RID: 4128
		public short cbSizeVft;

		// Token: 0x04001021 RID: 4129
		public short cbAlignment;

		// Token: 0x04001022 RID: 4130
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04001023 RID: 4131
		public short wMajorVerNum;

		// Token: 0x04001024 RID: 4132
		public short wMinorVerNum;

		// Token: 0x04001025 RID: 4133
		public TYPEDESC tdescAlias;

		// Token: 0x04001026 RID: 4134
		public IDLDESC idldescType;
	}
}
