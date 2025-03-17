using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200058C RID: 1420
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeTypedArgument
	{
		// Token: 0x06004931 RID: 18737 RVA: 0x00183F20 File Offset: 0x00183120
		private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
		{
			if (encodedType <= CustomAttributeEncoding.Array)
			{
				switch (encodedType)
				{
				case CustomAttributeEncoding.Boolean:
					return typeof(bool);
				case CustomAttributeEncoding.Char:
					return typeof(char);
				case CustomAttributeEncoding.SByte:
					return typeof(sbyte);
				case CustomAttributeEncoding.Byte:
					return typeof(byte);
				case CustomAttributeEncoding.Int16:
					return typeof(short);
				case CustomAttributeEncoding.UInt16:
					return typeof(ushort);
				case CustomAttributeEncoding.Int32:
					return typeof(int);
				case CustomAttributeEncoding.UInt32:
					return typeof(uint);
				case CustomAttributeEncoding.Int64:
					return typeof(long);
				case CustomAttributeEncoding.UInt64:
					return typeof(ulong);
				case CustomAttributeEncoding.Float:
					return typeof(float);
				case CustomAttributeEncoding.Double:
					return typeof(double);
				case CustomAttributeEncoding.String:
					return typeof(string);
				default:
					if (encodedType == CustomAttributeEncoding.Array)
					{
						return typeof(Array);
					}
					break;
				}
			}
			else
			{
				if (encodedType == CustomAttributeEncoding.Type)
				{
					return typeof(Type);
				}
				if (encodedType == CustomAttributeEncoding.Object)
				{
					return typeof(object);
				}
				if (encodedType == CustomAttributeEncoding.Enum)
				{
					return typeof(Enum);
				}
			}
			throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)encodedType), "encodedType");
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x001840A4 File Offset: 0x001832A4
		private unsafe static object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
		{
			switch (encodedType)
			{
			case CustomAttributeEncoding.Boolean:
				return (byte)val > 0;
			case CustomAttributeEncoding.Char:
				return (char)val;
			case CustomAttributeEncoding.SByte:
				return (sbyte)val;
			case CustomAttributeEncoding.Byte:
				return (byte)val;
			case CustomAttributeEncoding.Int16:
				return (short)val;
			case CustomAttributeEncoding.UInt16:
				return (ushort)val;
			case CustomAttributeEncoding.Int32:
				return (int)val;
			case CustomAttributeEncoding.UInt32:
				return (uint)val;
			case CustomAttributeEncoding.Int64:
				return val;
			case CustomAttributeEncoding.UInt64:
				return (ulong)val;
			case CustomAttributeEncoding.Float:
				return *(float*)(&val);
			case CustomAttributeEncoding.Double:
				return *(double*)(&val);
			default:
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)val), "val");
			}
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x0018416C File Offset: 0x0018336C
		private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
		{
			RuntimeType typeByNameUsingCARules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
			if (typeByNameUsingCARules == null)
			{
				throw new InvalidOperationException(SR.Format(SR.Arg_CATypeResolutionFailed, typeName));
			}
			return typeByNameUsingCARules;
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x0018419C File Offset: 0x0018339C
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x001841B8 File Offset: 0x001833B8
		internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
		{
			CustomAttributeEncoding customAttributeEncoding = encodedArg.CustomAttributeType.EncodedType;
			if (customAttributeEncoding == CustomAttributeEncoding.Undefined)
			{
				throw new ArgumentException(null, "encodedArg");
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum)
			{
				this.m_argumentType = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedArg.CustomAttributeType.EncodedEnumType);
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.String)
			{
				this.m_argumentType = typeof(string);
				this.m_value = encodedArg.StringValue;
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Type)
			{
				this.m_argumentType = typeof(Type);
				this.m_value = null;
				if (encodedArg.StringValue != null)
				{
					this.m_value = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
					return;
				}
			}
			else if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				customAttributeEncoding = encodedArg.CustomAttributeType.EncodedArrayType;
				Type type;
				if (customAttributeEncoding == CustomAttributeEncoding.Enum)
				{
					type = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				}
				else
				{
					type = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				}
				this.m_argumentType = type.MakeArrayType();
				if (encodedArg.ArrayValue == null)
				{
					this.m_value = null;
					return;
				}
				CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[i]);
				}
				this.m_value = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				return;
			}
			else
			{
				this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, customAttributeEncoding);
			}
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x00184347 File Offset: 0x00183547
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0018435C File Offset: 0x0018355C
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00184374 File Offset: 0x00183574
		public CustomAttributeTypedArgument(Type argumentType, [Nullable(2)] object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.m_value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.m_argumentType = argumentType;
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x001843A3 File Offset: 0x001835A3
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.m_argumentType = value.GetType();
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001843CB File Offset: 0x001835CB
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001843D4 File Offset: 0x001835D4
		internal string ToString(bool typed)
		{
			if (this.m_argumentType == null)
			{
				return base.ToString();
			}
			if (this.ArgumentType.IsEnum)
			{
				return string.Format(typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullName);
			}
			if (this.Value == null)
			{
				return string.Format(typed ? "null" : "({0})null", this.ArgumentType.Name);
			}
			if (this.ArgumentType == typeof(string))
			{
				return string.Format("\"{0}\"", this.Value);
			}
			if (this.ArgumentType == typeof(char))
			{
				return string.Format("'{0}'", this.Value);
			}
			if (this.ArgumentType == typeof(Type))
			{
				return string.Format("typeof({0})", ((Type)this.Value).FullName);
			}
			if (this.ArgumentType.IsArray)
			{
				IList<CustomAttributeTypedArgument> list = (IList<CustomAttributeTypedArgument>)this.Value;
				Type elementType = this.ArgumentType.GetElementType();
				string str = string.Format("new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullName : elementType.Name, list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					str += string.Format((i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
				}
				return str + " }";
			}
			return string.Format(typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.Name);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001845B0 File Offset: 0x001837B0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x001845C2 File Offset: 0x001837C2
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x001845D2 File Offset: 0x001837D2
		public Type ArgumentType
		{
			get
			{
				return this.m_argumentType;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x001845DA File Offset: 0x001837DA
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x040011DB RID: 4571
		private readonly object m_value;

		// Token: 0x040011DC RID: 4572
		private readonly Type m_argumentType;
	}
}
