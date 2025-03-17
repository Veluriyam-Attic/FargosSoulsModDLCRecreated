using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000616 RID: 1558
	[NullableContext(1)]
	[Nullable(0)]
	public class CustomAttributeBuilder
	{
		// Token: 0x06004EDF RID: 20191 RVA: 0x0018DF04 File Offset: 0x0018D104
		public CustomAttributeBuilder(ConstructorInfo con, [Nullable(new byte[]
		{
			1,
			2
		})] object[] constructorArgs) : this(con, constructorArgs, Array.Empty<PropertyInfo>(), Array.Empty<object>(), Array.Empty<FieldInfo>(), Array.Empty<object>())
		{
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0018DF22 File Offset: 0x0018D122
		public CustomAttributeBuilder(ConstructorInfo con, [Nullable(new byte[]
		{
			1,
			2
		})] object[] constructorArgs, PropertyInfo[] namedProperties, [Nullable(new byte[]
		{
			1,
			2
		})] object[] propertyValues) : this(con, constructorArgs, namedProperties, propertyValues, Array.Empty<FieldInfo>(), Array.Empty<object>())
		{
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0018DF39 File Offset: 0x0018D139
		public CustomAttributeBuilder(ConstructorInfo con, [Nullable(new byte[]
		{
			1,
			2
		})] object[] constructorArgs, FieldInfo[] namedFields, [Nullable(new byte[]
		{
			1,
			2
		})] object[] fieldValues) : this(con, constructorArgs, Array.Empty<PropertyInfo>(), Array.Empty<object>(), namedFields, fieldValues)
		{
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0018DF50 File Offset: 0x0018D150
		public CustomAttributeBuilder(ConstructorInfo con, [Nullable(new byte[]
		{
			1,
			2
		})] object[] constructorArgs, PropertyInfo[] namedProperties, [Nullable(new byte[]
		{
			1,
			2
		})] object[] propertyValues, FieldInfo[] namedFields, [Nullable(new byte[]
		{
			1,
			2
		})] object[] fieldValues)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException(SR.Arg_ArrayLengthsDiffer, "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException(SR.Arg_ArrayLengthsDiffer, "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException(SR.Argument_BadConstructor);
			}
			if ((con.CallingConvention & CallingConventions.Standard) != CallingConventions.Standard)
			{
				throw new ArgumentException(SR.Argument_BadConstructorCallConv);
			}
			this.m_con = con;
			this.m_constructorArgs = new object[constructorArgs.Length];
			Array.Copy(constructorArgs, this.m_constructorArgs, constructorArgs.Length);
			Type[] parameterTypes = con.GetParameterTypes();
			if (parameterTypes.Length != constructorArgs.Length)
			{
				throw new ArgumentException(SR.Argument_BadParameterCountsForConstructor);
			}
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				if (!this.ValidateType(parameterTypes[i]))
				{
					throw new ArgumentException(SR.Argument_BadTypeInCustomAttribute);
				}
			}
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				object obj = constructorArgs[i];
				if (obj == null)
				{
					if (parameterTypes[i].IsValueType)
					{
						throw new ArgumentNullException(string.Format("{0}[{1}]", "constructorArgs", i));
					}
				}
				else
				{
					CustomAttributeBuilder.VerifyTypeAndPassedObjectType(parameterTypes[i], obj.GetType(), string.Format("{0}[{1}]", "constructorArgs", i));
				}
			}
			MemoryStream output = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write(1);
			for (int i = 0; i < constructorArgs.Length; i++)
			{
				CustomAttributeBuilder.EmitValue(binaryWriter, parameterTypes[i], constructorArgs[i]);
			}
			binaryWriter.Write((ushort)(namedProperties.Length + namedFields.Length));
			for (int i = 0; i < namedProperties.Length; i++)
			{
				PropertyInfo propertyInfo = namedProperties[i];
				if (propertyInfo == null)
				{
					throw new ArgumentNullException("namedProperties[" + i.ToString() + "]");
				}
				Type propertyType = propertyInfo.PropertyType;
				object obj2 = propertyValues[i];
				if (obj2 == null && propertyType.IsValueType)
				{
					throw new ArgumentNullException("propertyValues[" + i.ToString() + "]");
				}
				if (!this.ValidateType(propertyType))
				{
					throw new ArgumentException(SR.Argument_BadTypeInCustomAttribute);
				}
				if (!propertyInfo.CanWrite)
				{
					throw new ArgumentException(SR.Argument_NotAWritableProperty);
				}
				if (propertyInfo.DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && !con.DeclaringType.IsSubclassOf(propertyInfo.DeclaringType) && !TypeBuilder.IsTypeEqual(propertyInfo.DeclaringType, con.DeclaringType) && (!(propertyInfo.DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf(((TypeBuilder)propertyInfo.DeclaringType).BakedRuntimeType)))
				{
					throw new ArgumentException(SR.Argument_BadPropertyForConstructorBuilder);
				}
				if (obj2 != null)
				{
					CustomAttributeBuilder.VerifyTypeAndPassedObjectType(propertyType, obj2.GetType(), string.Format("{0}[{1}]", "propertyValues", i));
				}
				binaryWriter.Write(84);
				CustomAttributeBuilder.EmitType(binaryWriter, propertyType);
				CustomAttributeBuilder.EmitString(binaryWriter, namedProperties[i].Name);
				CustomAttributeBuilder.EmitValue(binaryWriter, propertyType, obj2);
			}
			for (int i = 0; i < namedFields.Length; i++)
			{
				FieldInfo fieldInfo = namedFields[i];
				if (fieldInfo == null)
				{
					throw new ArgumentNullException("namedFields[" + i.ToString() + "]");
				}
				Type fieldType = fieldInfo.FieldType;
				object obj3 = fieldValues[i];
				if (obj3 == null && fieldType.IsValueType)
				{
					throw new ArgumentNullException("fieldValues[" + i.ToString() + "]");
				}
				if (!this.ValidateType(fieldType))
				{
					throw new ArgumentException(SR.Argument_BadTypeInCustomAttribute);
				}
				if (fieldInfo.DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && !con.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) && !TypeBuilder.IsTypeEqual(fieldInfo.DeclaringType, con.DeclaringType) && (!(fieldInfo.DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf(((TypeBuilder)namedFields[i].DeclaringType).BakedRuntimeType)))
				{
					throw new ArgumentException(SR.Argument_BadFieldForConstructorBuilder);
				}
				if (obj3 != null)
				{
					CustomAttributeBuilder.VerifyTypeAndPassedObjectType(fieldType, obj3.GetType(), string.Format("{0}[{1}]", "fieldValues", i));
				}
				binaryWriter.Write(83);
				CustomAttributeBuilder.EmitType(binaryWriter, fieldType);
				CustomAttributeBuilder.EmitString(binaryWriter, fieldInfo.Name);
				CustomAttributeBuilder.EmitValue(binaryWriter, fieldType, obj3);
			}
			this.m_blob = ((MemoryStream)binaryWriter.BaseStream).ToArray();
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0018E410 File Offset: 0x0018D610
		private bool ValidateType(Type t)
		{
			if (t.IsPrimitive)
			{
				return t != typeof(IntPtr) && t != typeof(UIntPtr);
			}
			if (t == typeof(string) || t == typeof(Type))
			{
				return true;
			}
			if (t.IsEnum)
			{
				TypeCode typeCode = Type.GetTypeCode(Enum.GetUnderlyingType(t));
				return typeCode - TypeCode.SByte <= 7;
			}
			if (t.IsArray)
			{
				return t.GetArrayRank() == 1 && this.ValidateType(t.GetElementType());
			}
			return t == typeof(object);
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0018E4C0 File Offset: 0x0018D6C0
		private static void VerifyTypeAndPassedObjectType(Type type, Type passedType, string paramName)
		{
			if (type != typeof(object) && Type.GetTypeCode(passedType) != Type.GetTypeCode(type))
			{
				throw new ArgumentException(SR.Argument_ConstantDoesntMatch);
			}
			if (passedType == typeof(IntPtr) || passedType == typeof(UIntPtr))
			{
				throw new ArgumentException(SR.Format(SR.Argument_BadParameterTypeForCAB, passedType), paramName);
			}
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0018E530 File Offset: 0x0018D730
		private static void EmitType(BinaryWriter writer, Type type)
		{
			if (type.IsPrimitive)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					writer.Write(2);
					return;
				case TypeCode.Char:
					writer.Write(3);
					return;
				case TypeCode.SByte:
					writer.Write(4);
					return;
				case TypeCode.Byte:
					writer.Write(5);
					return;
				case TypeCode.Int16:
					writer.Write(6);
					return;
				case TypeCode.UInt16:
					writer.Write(7);
					return;
				case TypeCode.Int32:
					writer.Write(8);
					return;
				case TypeCode.UInt32:
					writer.Write(9);
					return;
				case TypeCode.Int64:
					writer.Write(10);
					return;
				case TypeCode.UInt64:
					writer.Write(11);
					return;
				case TypeCode.Single:
					writer.Write(12);
					return;
				case TypeCode.Double:
					writer.Write(13);
					return;
				default:
					return;
				}
			}
			else
			{
				if (type.IsEnum)
				{
					writer.Write(85);
					CustomAttributeBuilder.EmitString(writer, type.AssemblyQualifiedName);
					return;
				}
				if (type == typeof(string))
				{
					writer.Write(14);
					return;
				}
				if (type == typeof(Type))
				{
					writer.Write(80);
					return;
				}
				if (type.IsArray)
				{
					writer.Write(29);
					CustomAttributeBuilder.EmitType(writer, type.GetElementType());
					return;
				}
				writer.Write(81);
				return;
			}
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0018E668 File Offset: 0x0018D868
		private static void EmitString(BinaryWriter writer, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			uint num = (uint)bytes.Length;
			if (num <= 127U)
			{
				writer.Write((byte)num);
			}
			else if (num <= 16383U)
			{
				writer.Write(BinaryPrimitives.ReverseEndianness((short)(num | 32768U)));
			}
			else
			{
				writer.Write(BinaryPrimitives.ReverseEndianness(num | 3221225472U));
			}
			writer.Write(bytes);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0018E6CC File Offset: 0x0018D8CC
		private static void EmitValue(BinaryWriter writer, Type type, object value)
		{
			if (type.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(type)))
				{
				case TypeCode.SByte:
					writer.Write((sbyte)value);
					return;
				case TypeCode.Byte:
					writer.Write((byte)value);
					return;
				case TypeCode.Int16:
					writer.Write((short)value);
					return;
				case TypeCode.UInt16:
					writer.Write((ushort)value);
					return;
				case TypeCode.Int32:
					writer.Write((int)value);
					return;
				case TypeCode.UInt32:
					writer.Write((uint)value);
					return;
				case TypeCode.Int64:
					writer.Write((long)value);
					return;
				case TypeCode.UInt64:
					writer.Write((ulong)value);
					return;
				default:
					return;
				}
			}
			else if (type == typeof(string))
			{
				if (value == null)
				{
					writer.Write(byte.MaxValue);
					return;
				}
				CustomAttributeBuilder.EmitString(writer, (string)value);
				return;
			}
			else if (type == typeof(Type))
			{
				if (value == null)
				{
					writer.Write(byte.MaxValue);
					return;
				}
				string text = TypeNameBuilder.ToString((Type)value, TypeNameBuilder.Format.AssemblyQualifiedName);
				if (text == null)
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidTypeForCA, value.GetType()));
				}
				CustomAttributeBuilder.EmitString(writer, text);
				return;
			}
			else if (type.IsArray)
			{
				if (value == null)
				{
					writer.Write(uint.MaxValue);
					return;
				}
				Array array = (Array)value;
				Type elementType = type.GetElementType();
				writer.Write(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					CustomAttributeBuilder.EmitValue(writer, elementType, array.GetValue(i));
				}
				return;
			}
			else if (type.IsPrimitive)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					writer.Write(((bool)value) ? 1 : 0);
					return;
				case TypeCode.Char:
					writer.Write(Convert.ToUInt16((char)value));
					return;
				case TypeCode.SByte:
					writer.Write((sbyte)value);
					return;
				case TypeCode.Byte:
					writer.Write((byte)value);
					return;
				case TypeCode.Int16:
					writer.Write((short)value);
					return;
				case TypeCode.UInt16:
					writer.Write((ushort)value);
					return;
				case TypeCode.Int32:
					writer.Write((int)value);
					return;
				case TypeCode.UInt32:
					writer.Write((uint)value);
					return;
				case TypeCode.Int64:
					writer.Write((long)value);
					return;
				case TypeCode.UInt64:
					writer.Write((ulong)value);
					return;
				case TypeCode.Single:
					writer.Write((float)value);
					return;
				case TypeCode.Double:
					writer.Write((double)value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (!(type == typeof(object)))
				{
					string p = "null";
					if (value != null)
					{
						p = value.GetType().ToString();
					}
					throw new ArgumentException(SR.Format(SR.Argument_BadParameterTypeForCAB, p));
				}
				Type type2 = (value == null) ? typeof(string) : ((value is Type) ? typeof(Type) : value.GetType());
				if (type2 == typeof(object))
				{
					throw new ArgumentException(SR.Format(SR.Argument_BadParameterTypeForCAB, type2));
				}
				CustomAttributeBuilder.EmitType(writer, type2);
				CustomAttributeBuilder.EmitValue(writer, type2, value);
				return;
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0018E9E4 File Offset: 0x0018DBE4
		internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner)
		{
			this.CreateCustomAttribute(mod, tkOwner, mod.GetConstructorToken(this.m_con).Token, false);
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0018EA0E File Offset: 0x0018DC0E
		internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner, int tkAttrib, bool toDisk)
		{
			TypeBuilder.DefineCustomAttribute(mod, tkOwner, tkAttrib, this.m_blob, toDisk, typeof(DebuggableAttribute) == this.m_con.DeclaringType);
		}

		// Token: 0x0400141C RID: 5148
		internal ConstructorInfo m_con;

		// Token: 0x0400141D RID: 5149
		private object[] m_constructorArgs;

		// Token: 0x0400141E RID: 5150
		private byte[] m_blob;
	}
}
