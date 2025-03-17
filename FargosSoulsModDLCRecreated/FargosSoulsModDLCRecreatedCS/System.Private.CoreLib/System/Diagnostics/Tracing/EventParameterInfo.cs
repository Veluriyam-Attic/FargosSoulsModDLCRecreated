using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200071E RID: 1822
	internal struct EventParameterInfo
	{
		// Token: 0x06005A42 RID: 23106 RVA: 0x001B46F3 File Offset: 0x001B38F3
		internal void SetInfo(string name, Type type, TraceLoggingTypeInfo typeInfo = null)
		{
			this.ParameterName = name;
			this.ParameterType = type;
			this.TypeInfo = typeInfo;
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x001B470C File Offset: 0x001B390C
		internal unsafe bool GenerateMetadata(byte* pMetadataBlob, ref uint offset, uint blobSize)
		{
			TypeCode typeCodeExtended = EventParameterInfo.GetTypeCodeExtended(this.ParameterType);
			if (typeCodeExtended == TypeCode.Object)
			{
				EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 1U);
				InvokeTypeInfo invokeTypeInfo = this.TypeInfo as InvokeTypeInfo;
				if (invokeTypeInfo == null)
				{
					return false;
				}
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)properties.Length);
					foreach (PropertyAnalysis property in properties)
					{
						if (!EventParameterInfo.GenerateMetadataForProperty(property, pMetadataBlob, ref offset, blobSize))
						{
							return false;
						}
					}
				}
				else
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 0U);
				}
				EventPipeMetadataGenerator.WriteToBuffer<char>(pMetadataBlob, blobSize, ref offset, '\0');
			}
			else
			{
				EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)typeCodeExtended);
				string parameterName = this.ParameterName;
				char* ptr;
				if (parameterName == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = parameterName.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* src = ptr;
				EventPipeMetadataGenerator.WriteToBuffer(pMetadataBlob, blobSize, ref offset, (byte*)src, (uint)((this.ParameterName.Length + 1) * 2));
				char* ptr2 = null;
			}
			return true;
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x001B47D8 File Offset: 0x001B39D8
		private unsafe static bool GenerateMetadataForProperty(PropertyAnalysis property, byte* pMetadataBlob, ref uint offset, uint blobSize)
		{
			InvokeTypeInfo invokeTypeInfo = property.typeInfo as InvokeTypeInfo;
			if (invokeTypeInfo != null)
			{
				EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 1U);
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)properties.Length);
					foreach (PropertyAnalysis property2 in properties)
					{
						if (!EventParameterInfo.GenerateMetadataForProperty(property2, pMetadataBlob, ref offset, blobSize))
						{
							return false;
						}
					}
				}
				else
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 0U);
				}
				string name = property.name;
				char* ptr;
				if (name == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = name.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* src = ptr;
				EventPipeMetadataGenerator.WriteToBuffer(pMetadataBlob, blobSize, ref offset, (byte*)src, (uint)((property.name.Length + 1) * 2));
				char* ptr2 = null;
			}
			else
			{
				TypeCode typeCodeExtended = EventParameterInfo.GetTypeCodeExtended(property.typeInfo.DataType);
				if (typeCodeExtended == TypeCode.Object)
				{
					return false;
				}
				EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)typeCodeExtended);
				string name2 = property.name;
				char* ptr3;
				if (name2 == null)
				{
					ptr3 = null;
				}
				else
				{
					fixed (char* ptr4 = name2.GetPinnableReference())
					{
						ptr3 = ptr4;
					}
				}
				char* src2 = ptr3;
				EventPipeMetadataGenerator.WriteToBuffer(pMetadataBlob, blobSize, ref offset, (byte*)src2, (uint)((property.name.Length + 1) * 2));
				char* ptr4 = null;
			}
			return true;
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x001B48D7 File Offset: 0x001B3AD7
		internal unsafe bool GenerateMetadataV2(byte* pMetadataBlob, ref uint offset, uint blobSize)
		{
			return this.TypeInfo != null && EventParameterInfo.GenerateMetadataForNamedTypeV2(this.ParameterName, this.TypeInfo, pMetadataBlob, ref offset, blobSize);
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x001B48F8 File Offset: 0x001B3AF8
		private unsafe static bool GenerateMetadataForNamedTypeV2(string name, TraceLoggingTypeInfo typeInfo, byte* pMetadataBlob, ref uint offset, uint blobSize)
		{
			uint value;
			if (!EventParameterInfo.GetMetadataLengthForNamedTypeV2(name, typeInfo, out value))
			{
				return false;
			}
			EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, value);
			char* ptr;
			if (name == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = name.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* src = ptr;
			EventPipeMetadataGenerator.WriteToBuffer(pMetadataBlob, blobSize, ref offset, (byte*)src, (uint)((name.Length + 1) * 2));
			char* ptr2 = null;
			return EventParameterInfo.GenerateMetadataForTypeV2(typeInfo, pMetadataBlob, ref offset, blobSize);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x001B4950 File Offset: 0x001B3B50
		private unsafe static bool GenerateMetadataForTypeV2(TraceLoggingTypeInfo typeInfo, byte* pMetadataBlob, ref uint offset, uint blobSize)
		{
			InvokeTypeInfo invokeTypeInfo = typeInfo as InvokeTypeInfo;
			if (invokeTypeInfo != null)
			{
				EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 1U);
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)properties.Length);
					foreach (PropertyAnalysis propertyAnalysis in properties)
					{
						if (!EventParameterInfo.GenerateMetadataForNamedTypeV2(propertyAnalysis.name, propertyAnalysis.typeInfo, pMetadataBlob, ref offset, blobSize))
						{
							return false;
						}
					}
				}
				else
				{
					EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, 0U);
				}
			}
			else
			{
				EnumerableTypeInfo enumerableTypeInfo = typeInfo as EnumerableTypeInfo;
				if (enumerableTypeInfo != null)
				{
					EventPipeMetadataGenerator.WriteToBuffer<int>(pMetadataBlob, blobSize, ref offset, 19);
					EventParameterInfo.GenerateMetadataForTypeV2(enumerableTypeInfo.ElementInfo, pMetadataBlob, ref offset, blobSize);
				}
				else
				{
					ScalarArrayTypeInfo scalarArrayTypeInfo = typeInfo as ScalarArrayTypeInfo;
					if (scalarArrayTypeInfo != null)
					{
						if (!scalarArrayTypeInfo.DataType.HasElementType)
						{
							return false;
						}
						TraceLoggingTypeInfo typeInfo2;
						if (!EventParameterInfo.GetTypeInfoFromType(scalarArrayTypeInfo.DataType.GetElementType(), out typeInfo2))
						{
							return false;
						}
						EventPipeMetadataGenerator.WriteToBuffer<int>(pMetadataBlob, blobSize, ref offset, 19);
						EventParameterInfo.GenerateMetadataForTypeV2(typeInfo2, pMetadataBlob, ref offset, blobSize);
					}
					else
					{
						TypeCode typeCodeExtended = EventParameterInfo.GetTypeCodeExtended(typeInfo.DataType);
						if (typeCodeExtended == TypeCode.Object)
						{
							return false;
						}
						EventPipeMetadataGenerator.WriteToBuffer<uint>(pMetadataBlob, blobSize, ref offset, (uint)typeCodeExtended);
					}
				}
			}
			return true;
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x001B4A58 File Offset: 0x001B3C58
		internal static bool GetTypeInfoFromType(Type type, out TraceLoggingTypeInfo typeInfo)
		{
			if (type == typeof(bool))
			{
				typeInfo = ScalarTypeInfo.Boolean();
				return true;
			}
			if (type == typeof(byte))
			{
				typeInfo = ScalarTypeInfo.Byte();
				return true;
			}
			if (type == typeof(sbyte))
			{
				typeInfo = ScalarTypeInfo.SByte();
				return true;
			}
			if (type == typeof(char))
			{
				typeInfo = ScalarTypeInfo.Char();
				return true;
			}
			if (type == typeof(short))
			{
				typeInfo = ScalarTypeInfo.Int16();
				return true;
			}
			if (type == typeof(ushort))
			{
				typeInfo = ScalarTypeInfo.UInt16();
				return true;
			}
			if (type == typeof(int))
			{
				typeInfo = ScalarTypeInfo.Int32();
				return true;
			}
			if (type == typeof(uint))
			{
				typeInfo = ScalarTypeInfo.UInt32();
				return true;
			}
			if (type == typeof(long))
			{
				typeInfo = ScalarTypeInfo.Int64();
				return true;
			}
			if (type == typeof(ulong))
			{
				typeInfo = ScalarTypeInfo.UInt64();
				return true;
			}
			if (type == typeof(IntPtr))
			{
				typeInfo = ScalarTypeInfo.IntPtr();
				return true;
			}
			if (type == typeof(UIntPtr))
			{
				typeInfo = ScalarTypeInfo.UIntPtr();
				return true;
			}
			if (type == typeof(float))
			{
				typeInfo = ScalarTypeInfo.Single();
				return true;
			}
			if (type == typeof(double))
			{
				typeInfo = ScalarTypeInfo.Double();
				return true;
			}
			if (type == typeof(Guid))
			{
				typeInfo = ScalarTypeInfo.Guid();
				return true;
			}
			typeInfo = null;
			return false;
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001B4C00 File Offset: 0x001B3E00
		internal bool GetMetadataLength(out uint size)
		{
			size = 0U;
			TypeCode typeCodeExtended = EventParameterInfo.GetTypeCodeExtended(this.ParameterType);
			if (typeCodeExtended == TypeCode.Object)
			{
				InvokeTypeInfo invokeTypeInfo = this.TypeInfo as InvokeTypeInfo;
				if (invokeTypeInfo == null)
				{
					return false;
				}
				size += 8U;
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					foreach (PropertyAnalysis property in properties)
					{
						size += EventParameterInfo.GetMetadataLengthForProperty(property);
					}
				}
				size += 2U;
			}
			else
			{
				size += (uint)(4 + (this.ParameterName.Length + 1) * 2);
			}
			return true;
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x001B4C88 File Offset: 0x001B3E88
		private static uint GetMetadataLengthForProperty(PropertyAnalysis property)
		{
			uint num = 0U;
			InvokeTypeInfo invokeTypeInfo = property.typeInfo as InvokeTypeInfo;
			if (invokeTypeInfo != null)
			{
				num += 8U;
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					foreach (PropertyAnalysis property2 in properties)
					{
						num += EventParameterInfo.GetMetadataLengthForProperty(property2);
					}
				}
				num += (uint)((property.name.Length + 1) * 2);
			}
			else
			{
				num += (uint)(4 + (property.name.Length + 1) * 2);
			}
			return num;
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x001B4D04 File Offset: 0x001B3F04
		private static TypeCode GetTypeCodeExtended(Type parameterType)
		{
			if (parameterType == typeof(Guid))
			{
				return (TypeCode)17;
			}
			if (parameterType == typeof(IntPtr))
			{
				int size = IntPtr.Size;
				return TypeCode.Int64;
			}
			if (parameterType == typeof(UIntPtr))
			{
				int size2 = UIntPtr.Size;
				return TypeCode.UInt64;
			}
			return Type.GetTypeCode(parameterType);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x001B4D62 File Offset: 0x001B3F62
		internal bool GetMetadataLengthV2(out uint size)
		{
			return EventParameterInfo.GetMetadataLengthForNamedTypeV2(this.ParameterName, this.TypeInfo, out size);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x001B4D78 File Offset: 0x001B3F78
		private static bool GetMetadataLengthForTypeV2(TraceLoggingTypeInfo typeInfo, out uint size)
		{
			size = 0U;
			if (typeInfo == null)
			{
				return false;
			}
			InvokeTypeInfo invokeTypeInfo = typeInfo as InvokeTypeInfo;
			if (invokeTypeInfo != null)
			{
				size += 8U;
				PropertyAnalysis[] properties = invokeTypeInfo.properties;
				if (properties != null)
				{
					foreach (PropertyAnalysis propertyAnalysis in properties)
					{
						uint num;
						if (!EventParameterInfo.GetMetadataLengthForNamedTypeV2(propertyAnalysis.name, propertyAnalysis.typeInfo, out num))
						{
							return false;
						}
						size += num;
					}
				}
			}
			else
			{
				EnumerableTypeInfo enumerableTypeInfo = typeInfo as EnumerableTypeInfo;
				if (enumerableTypeInfo != null)
				{
					size += 4U;
					uint num2;
					if (!EventParameterInfo.GetMetadataLengthForTypeV2(enumerableTypeInfo.ElementInfo, out num2))
					{
						return false;
					}
					size += num2;
				}
				else
				{
					ScalarArrayTypeInfo scalarArrayTypeInfo = typeInfo as ScalarArrayTypeInfo;
					if (scalarArrayTypeInfo != null)
					{
						TraceLoggingTypeInfo typeInfo2;
						if (!scalarArrayTypeInfo.DataType.HasElementType || !EventParameterInfo.GetTypeInfoFromType(scalarArrayTypeInfo.DataType.GetElementType(), out typeInfo2))
						{
							return false;
						}
						size += 4U;
						uint num3;
						if (!EventParameterInfo.GetMetadataLengthForTypeV2(typeInfo2, out num3))
						{
							return false;
						}
						size += num3;
					}
					else
					{
						size += 4U;
					}
				}
			}
			return true;
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x001B4E68 File Offset: 0x001B4068
		private static bool GetMetadataLengthForNamedTypeV2(string name, TraceLoggingTypeInfo typeInfo, out uint size)
		{
			size = (uint)(4 + (name.Length + 1) * 2);
			uint num;
			if (!EventParameterInfo.GetMetadataLengthForTypeV2(typeInfo, out num))
			{
				return false;
			}
			size += num;
			return true;
		}

		// Token: 0x04001A5E RID: 6750
		internal string ParameterName;

		// Token: 0x04001A5F RID: 6751
		internal Type ParameterType;

		// Token: 0x04001A60 RID: 6752
		internal TraceLoggingTypeInfo TypeInfo;
	}
}
