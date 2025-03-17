using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000611 RID: 1553
	public static class AssemblyExtensions
	{
		// Token: 0x06004E76 RID: 20086
		[DllImport("QCall")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private unsafe static extern bool InternalTryGetRawMetadata(QCallAssembly assembly, ref byte* blob, ref int length);

		// Token: 0x06004E77 RID: 20087 RVA: 0x0018D450 File Offset: 0x0018C650
		[CLSCompliant(false)]
		public unsafe static bool TryGetRawMetadata([Nullable(1)] this Assembly assembly, out byte* blob, out int length)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			blob = (IntPtr)((UIntPtr)0);
			length = 0;
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (runtimeAssembly == null)
			{
				return false;
			}
			RuntimeAssembly runtimeAssembly2 = runtimeAssembly;
			return AssemblyExtensions.InternalTryGetRawMetadata(new QCallAssembly(ref runtimeAssembly2), ref blob, ref length);
		}
	}
}
