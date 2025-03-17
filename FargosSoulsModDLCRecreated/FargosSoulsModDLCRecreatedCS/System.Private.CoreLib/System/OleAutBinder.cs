using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System
{
	// Token: 0x020000A3 RID: 163
	internal class OleAutBinder : DefaultBinder
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x000C47D0 File Offset: 0x000C39D0
		public override object ChangeType(object value, Type type, CultureInfo cultureInfo)
		{
			System.Variant source = new System.Variant(value);
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			if (!type.IsPrimitive && type.IsInstanceOfType(value))
			{
				return value;
			}
			Type type2 = value.GetType();
			if (type.IsEnum && type2.IsPrimitive)
			{
				return Enum.Parse(type, value.ToString());
			}
			object result;
			try
			{
				object obj = OAVariantLib.ChangeType(source, type, 16, cultureInfo).ToObject();
				result = obj;
			}
			catch (NotSupportedException)
			{
				throw new COMException(SR.Interop_COM_TypeMismatch, -2147352571);
			}
			return result;
		}
	}
}
