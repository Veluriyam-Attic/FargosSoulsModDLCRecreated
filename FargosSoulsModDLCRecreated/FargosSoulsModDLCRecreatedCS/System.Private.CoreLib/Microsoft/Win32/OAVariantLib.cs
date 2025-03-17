using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Win32
{
	// Token: 0x02000046 RID: 70
	internal static class OAVariantLib
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x000AAF64 File Offset: 0x000AA164
		internal static Variant ChangeType(Variant source, Type targetClass, short options, CultureInfo culture)
		{
			if (targetClass == null)
			{
				throw new ArgumentNullException("targetClass");
			}
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Variant result = default(Variant);
			OAVariantLib.ChangeTypeEx(ref result, ref source, culture.LCID, targetClass.TypeHandle.Value, OAVariantLib.GetCVTypeFromClass(targetClass), options);
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000AAFC0 File Offset: 0x000AA1C0
		private static int GetCVTypeFromClass(Type ctype)
		{
			int num = -1;
			for (int i = 0; i < OAVariantLib.ClassTypes.Length; i++)
			{
				if (ctype.Equals(OAVariantLib.ClassTypes[i]))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				num = 18;
			}
			return num;
		}

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ChangeTypeEx(ref Variant result, ref Variant source, int lcid, IntPtr typeHandle, int cvType, short flags);

		// Token: 0x040000CA RID: 202
		internal static readonly Type[] ClassTypes = new Type[]
		{
			typeof(Empty),
			typeof(void),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(string),
			typeof(void),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(object),
			typeof(decimal),
			null,
			typeof(Missing),
			typeof(DBNull)
		};
	}
}
