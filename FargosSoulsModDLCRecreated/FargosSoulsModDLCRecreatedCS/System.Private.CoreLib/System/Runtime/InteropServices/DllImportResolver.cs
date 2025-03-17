using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000496 RID: 1174
	// (Invoke) Token: 0x060044AB RID: 17579
	public delegate IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath);
}
