using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System
{
	// Token: 0x0200009D RID: 157
	internal class SafeTypeNameParserHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000856 RID: 2134
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _ReleaseTypeNameParser(IntPtr pTypeNameParser);

		// Token: 0x06000857 RID: 2135 RVA: 0x000AB1EC File Offset: 0x000AA3EC
		public SafeTypeNameParserHandle() : base(true)
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000C3F53 File Offset: 0x000C3153
		protected override bool ReleaseHandle()
		{
			SafeTypeNameParserHandle._ReleaseTypeNameParser(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
