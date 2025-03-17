using System;

namespace System.Reflection
{
	// Token: 0x02000599 RID: 1433
	internal static class MdConstant
	{
		// Token: 0x060049A6 RID: 18854 RVA: 0x00185F9C File Offset: 0x0018519C
		public unsafe static object GetValue(MetadataImport scope, int token, RuntimeTypeHandle fieldTypeHandle, bool raw)
		{
			long num;
			int num2;
			CorElementType corElementType;
			string defaultValue = scope.GetDefaultValue(token, out num, out num2, out corElementType);
			RuntimeType runtimeType = fieldTypeHandle.GetRuntimeType();
			if (runtimeType.IsEnum && !raw)
			{
				long value;
				switch (corElementType)
				{
				case CorElementType.ELEMENT_TYPE_VOID:
					return DBNull.Value;
				case CorElementType.ELEMENT_TYPE_CHAR:
					value = (long)((ulong)(*(ushort*)(&num)));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_I1:
					value = (long)(*(sbyte*)(&num));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_U1:
					value = (long)((ulong)(*(byte*)(&num)));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_I2:
					value = (long)(*(short*)(&num));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_U2:
					value = (long)((ulong)(*(ushort*)(&num)));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_I4:
					value = (long)(*(int*)(&num));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_U4:
					value = (long)((ulong)(*(uint*)(&num)));
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_I8:
					value = num;
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_U8:
					value = num;
					goto IL_DC;
				case CorElementType.ELEMENT_TYPE_CLASS:
					return null;
				}
				throw new FormatException(SR.Arg_BadLiteralFormat);
				IL_DC:
				return RuntimeType.CreateEnum(runtimeType, value);
			}
			if (runtimeType == typeof(DateTime))
			{
				long ticks;
				if (corElementType <= CorElementType.ELEMENT_TYPE_I8)
				{
					if (corElementType == CorElementType.ELEMENT_TYPE_VOID)
					{
						return DBNull.Value;
					}
					if (corElementType == CorElementType.ELEMENT_TYPE_I8)
					{
						ticks = num;
						goto IL_136;
					}
				}
				else
				{
					if (corElementType == CorElementType.ELEMENT_TYPE_U8)
					{
						ticks = num;
						goto IL_136;
					}
					if (corElementType == CorElementType.ELEMENT_TYPE_CLASS)
					{
						return null;
					}
				}
				throw new FormatException(SR.Arg_BadLiteralFormat);
				IL_136:
				return new DateTime(ticks);
			}
			switch (corElementType)
			{
			case CorElementType.ELEMENT_TYPE_VOID:
				return DBNull.Value;
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return *(int*)(&num) != 0;
			case CorElementType.ELEMENT_TYPE_CHAR:
				return (char)(*(ushort*)(&num));
			case CorElementType.ELEMENT_TYPE_I1:
				return *(sbyte*)(&num);
			case CorElementType.ELEMENT_TYPE_U1:
				return *(byte*)(&num);
			case CorElementType.ELEMENT_TYPE_I2:
				return *(short*)(&num);
			case CorElementType.ELEMENT_TYPE_U2:
				return *(ushort*)(&num);
			case CorElementType.ELEMENT_TYPE_I4:
				return *(int*)(&num);
			case CorElementType.ELEMENT_TYPE_U4:
				return *(uint*)(&num);
			case CorElementType.ELEMENT_TYPE_I8:
				return num;
			case CorElementType.ELEMENT_TYPE_U8:
				return (ulong)num;
			case CorElementType.ELEMENT_TYPE_R4:
				return *(float*)(&num);
			case CorElementType.ELEMENT_TYPE_R8:
				return *(double*)(&num);
			case CorElementType.ELEMENT_TYPE_STRING:
				return defaultValue ?? string.Empty;
			case CorElementType.ELEMENT_TYPE_CLASS:
				return null;
			}
			throw new FormatException(SR.Arg_BadLiteralFormat);
		}
	}
}
