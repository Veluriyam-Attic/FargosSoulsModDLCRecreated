using System;
using System.Buffers.Binary;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200071F RID: 1823
	internal static class EventPipePayloadDecoder
	{
		// Token: 0x06005A4F RID: 23119 RVA: 0x001B4E98 File Offset: 0x001B4098
		internal unsafe static object[] DecodePayload(ref EventSource.EventMetadata metadata, ReadOnlySpan<byte> payload)
		{
			ParameterInfo[] parameters = metadata.Parameters;
			object[] array = new object[parameters.Length];
			int num = 0;
			while (num < parameters.Length && payload.Length > 0)
			{
				Type parameterType = parameters[num].ParameterType;
				if (parameterType == typeof(IntPtr))
				{
					int size = IntPtr.Size;
					array[num] = (IntPtr)BinaryPrimitives.ReadInt64LittleEndian(payload);
					payload = payload.Slice(IntPtr.Size);
				}
				else if (parameterType == typeof(int))
				{
					array[num] = BinaryPrimitives.ReadInt32LittleEndian(payload);
					payload = payload.Slice(4);
				}
				else if (parameterType == typeof(uint))
				{
					array[num] = BinaryPrimitives.ReadUInt32LittleEndian(payload);
					payload = payload.Slice(4);
				}
				else if (parameterType == typeof(long))
				{
					array[num] = BinaryPrimitives.ReadInt64LittleEndian(payload);
					payload = payload.Slice(8);
				}
				else if (parameterType == typeof(ulong))
				{
					array[num] = BinaryPrimitives.ReadUInt64LittleEndian(payload);
					payload = payload.Slice(8);
				}
				else if (parameterType == typeof(byte))
				{
					array[num] = MemoryMarshal.Read<byte>(payload);
					payload = payload.Slice(1);
				}
				else if (parameterType == typeof(sbyte))
				{
					array[num] = MemoryMarshal.Read<sbyte>(payload);
					payload = payload.Slice(1);
				}
				else if (parameterType == typeof(short))
				{
					array[num] = BinaryPrimitives.ReadInt16LittleEndian(payload);
					payload = payload.Slice(2);
				}
				else if (parameterType == typeof(ushort))
				{
					array[num] = BinaryPrimitives.ReadUInt16LittleEndian(payload);
					payload = payload.Slice(2);
				}
				else if (parameterType == typeof(float))
				{
					array[num] = BitConverter.Int32BitsToSingle(BinaryPrimitives.ReadInt32LittleEndian(payload));
					payload = payload.Slice(4);
				}
				else if (parameterType == typeof(double))
				{
					array[num] = BitConverter.Int64BitsToDouble(BinaryPrimitives.ReadInt64LittleEndian(payload));
					payload = payload.Slice(8);
				}
				else if (parameterType == typeof(bool))
				{
					array[num] = (BinaryPrimitives.ReadInt32LittleEndian(payload) == 1);
					payload = payload.Slice(4);
				}
				else if (parameterType == typeof(Guid))
				{
					array[num] = new Guid(payload.Slice(0, 16));
					payload = payload.Slice(16);
				}
				else if (parameterType == typeof(char))
				{
					array[num] = (char)BinaryPrimitives.ReadUInt16LittleEndian(payload);
					payload = payload.Slice(2);
				}
				else if (parameterType == typeof(string))
				{
					int num2 = -1;
					for (int i = 1; i < payload.Length; i += 2)
					{
						if (*payload[i - 1] == 0 && *payload[i] == 0)
						{
							num2 = i + 1;
							break;
						}
					}
					ReadOnlySpan<char> readOnlySpan;
					if (num2 < 0)
					{
						readOnlySpan = MemoryMarshal.Cast<byte, char>(payload);
						payload = default(ReadOnlySpan<byte>);
					}
					else
					{
						readOnlySpan = MemoryMarshal.Cast<byte, char>(payload.Slice(0, num2 - 2));
						payload = payload.Slice(num2);
					}
					array[num] = (BitConverter.IsLittleEndian ? new string(readOnlySpan) : Encoding.Unicode.GetString(MemoryMarshal.Cast<char, byte>(readOnlySpan)));
				}
				num++;
			}
			return array;
		}
	}
}
