using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x0200016E RID: 366
	public enum PlatformID
	{
		// Token: 0x0400046C RID: 1132
		[EditorBrowsable(EditorBrowsableState.Never)]
		Win32S,
		// Token: 0x0400046D RID: 1133
		[EditorBrowsable(EditorBrowsableState.Never)]
		Win32Windows,
		// Token: 0x0400046E RID: 1134
		Win32NT,
		// Token: 0x0400046F RID: 1135
		[EditorBrowsable(EditorBrowsableState.Never)]
		WinCE,
		// Token: 0x04000470 RID: 1136
		Unix,
		// Token: 0x04000471 RID: 1137
		[EditorBrowsable(EditorBrowsableState.Never)]
		Xbox,
		// Token: 0x04000472 RID: 1138
		[EditorBrowsable(EditorBrowsableState.Never)]
		MacOSX,
		// Token: 0x04000473 RID: 1139
		Other
	}
}
