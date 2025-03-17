using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200057C RID: 1404
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x06004825 RID: 18469 RVA: 0x0017FCF0 File Offset: 0x0017EEF0
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache, bool permitDeserialization)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this._permitDeserialization = permitDeserialization;
			this.ReadResources();
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0017FD2C File Offset: 0x0017EF2C
		private object DeserializeObject(int typeIndex)
		{
			if (!this._permitDeserialization)
			{
				throw new NotSupportedException(SR.NotSupported_ResourceObjectSerialization);
			}
			if (this._binaryFormatter == null && !this.InitializeBinaryFormatter())
			{
				throw new NotSupportedException(SR.BinaryFormatter_SerializationDisallowed);
			}
			Type type = this.FindType(typeIndex);
			object obj = ResourceReader.s_deserializeMethod(this._binaryFormatter, this._store.BaseStream);
			if (obj.GetType() != type)
			{
				throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResType_SerBlobMismatch, type.FullName, obj.GetType().FullName));
			}
			return obj;
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0017FDBC File Offset: 0x0017EFBC
		private bool InitializeBinaryFormatter()
		{
			LazyInitializer.EnsureInitialized<Type>(ref ResourceReader.s_binaryFormatterType, () => Type.GetType("System.Runtime.Serialization.Formatters.Binary.BinaryFormatter, System.Runtime.Serialization.Formatters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true));
			LazyInitializer.EnsureInitialized<Func<object, Stream, object>>(ref ResourceReader.s_deserializeMethod, delegate()
			{
				MethodInfo method = ResourceReader.s_binaryFormatterType.GetMethod("Deserialize", new Type[]
				{
					typeof(Stream)
				});
				return (Func<object, Stream, object>)typeof(ResourceReader).GetMethod("CreateUntypedDelegate", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					ResourceReader.s_binaryFormatterType
				}).Invoke(null, new object[]
				{
					method
				});
			});
			this._binaryFormatter = Activator.CreateInstance(ResourceReader.s_binaryFormatterType);
			return true;
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0017FE30 File Offset: 0x0017F030
		private static Func<object, Stream, object> CreateUntypedDelegate<TInstance>(MethodInfo method)
		{
			Func<TInstance, Stream, object> typedDelegate = (Func<TInstance, Stream, object>)Delegate.CreateDelegate(typeof(Func<TInstance, Stream, object>), null, method);
			return (object obj, Stream stream) => typedDelegate((TInstance)((object)obj), stream);
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x0017FE6B File Offset: 0x0017F06B
		private static bool ValidateReaderType(string readerType)
		{
			return ResourceManager.IsDefaultType(readerType, "System.Resources.ResourceReader");
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x0017FE78 File Offset: 0x0017F078
		[NullableContext(1)]
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(SR.ResourceReaderIsClosed);
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ResourceNameNotExist, resourceName));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesNameInvalidOffset, num2));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesDataInvalidOffset, num3));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				long num5 = (num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length;
				int num6 = (int)(num5 - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(SR.BadImageFormat_InvalidType);
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num6 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num6);
				if (array2.Length != num6)
				{
					throw new FormatException(SR.BadImageFormat_ResourceNameCorrupted);
				}
				resourceData = array2;
			}
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x001800B0 File Offset: 0x0017F2B0
		[NullableContext(1)]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0018011C File Offset: 0x0017F31C
		[NullableContext(1)]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(SR.Argument_StreamNotReadable);
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = (stream as UnmanagedMemoryStream);
			this.ReadResources();
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00180183 File Offset: 0x0017F383
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x0018018C File Offset: 0x0017F38C
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00180194 File Offset: 0x0017F394
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x001801F8 File Offset: 0x0017F3F8
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | (int)((byte*)p)[1] << 8 | (int)((byte*)p)[2] << 16 | (int)((byte*)p)[3] << 24;
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00180220 File Offset: 0x0017F420
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_NegativeStringLength);
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x0018025C File Offset: 0x0017F45C
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00180280 File Offset: 0x0017F480
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesNameInvalidOffset, num));
			}
			return num;
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x001802DE File Offset: 0x0017F4DE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x001802E6 File Offset: 0x0017F4E6
		[NullableContext(1)]
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(SR.ResourceReaderIsClosed);
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x00180301 File Offset: 0x0017F501
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0018030C File Offset: 0x0017F50C
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesDataInvalidOffset, num5));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00180474 File Offset: 0x0017F674
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_NegativeStringLength);
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(SR.BadImageFormat_ResourceNameCorrupted);
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesNameTooLong);
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00180530 File Offset: 0x0017F730
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(SR.BadImageFormat_NegativeStringLength);
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourcesIndexTooLong, index));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string result = new string(positionPointer, 0, num2 / 2);
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesDataInvalidOffset, dataOffset));
					}
					return result;
				}
				else
				{
					array = new byte[num2];
					int num3;
					for (int i = num2; i > 0; i -= num3)
					{
						num3 = this._store.Read(array, num2 - i, i);
						if (num3 == 0)
						{
							throw new EndOfStreamException(SR.Format(SR.BadImageFormat_ResourceNameCorrupted_NameIndex, index));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesDataInvalidOffset, dataOffset));
					}
				}
			}
			return Encoding.Unicode.GetString(array, 0, num2);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00180708 File Offset: 0x0017F908
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object result;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(SR.Format(SR.BadImageFormat_ResourcesDataInvalidOffset, num2));
				}
				if (this._version == 1)
				{
					result = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					result = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return result;
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x001807C8 File Offset: 0x0017F9C8
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string result = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotString_Type, this.FindType(num).FullName));
				}
				result = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string p;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						p = resourceTypeCode.ToString();
					}
					else
					{
						p = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotString_Type, p));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					result = this._store.ReadString();
				}
			}
			return result;
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x001808A0 File Offset: 0x0017FAA0
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x001808C8 File Offset: 0x0017FAC8
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00180900 File Offset: 0x0017FB00
		internal object LoadObjectV1(int pos)
		{
			object result;
			try
			{
				result = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(SR.BadImageFormat_TypeMismatch, inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(SR.BadImageFormat_TypeMismatch, inner2);
			}
			return result;
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00180950 File Offset: 0x0017FB50
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			Type left = this.FindType(num);
			if (left == typeof(string))
			{
				return this._store.ReadString();
			}
			if (left == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (left == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (left == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (left == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (left == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (left == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (left == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (left == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (left == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (left == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (left == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (left == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (left == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00180BA8 File Offset: 0x0017FDA8
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object result;
			try
			{
				result = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(SR.BadImageFormat_TypeMismatch, inner);
			}
			catch (ArgumentOutOfRangeException inner2)
			{
				throw new BadImageFormatException(SR.BadImageFormat_TypeMismatch, inner2);
			}
			return result;
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00180BF8 File Offset: 0x0017FDF8
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
			{
				long dateData = this._store.ReadInt64();
				return DateTime.FromBinary(dateData);
			}
			case ResourceTypeCode.TimeSpan:
			{
				long ticks = this._store.ReadInt64();
				return new TimeSpan(ticks);
			}
			case ResourceTypeCode.ByteArray:
			{
				int num = this._store.ReadInt32();
				if (num < 0)
				{
					throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourceDataLengthInvalid, num));
				}
				if (this._ums == null)
				{
					if ((long)num > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourceDataLengthInvalid, num));
					}
					return this._store.ReadBytes(num);
				}
				else
				{
					if ((long)num > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourceDataLengthInvalid, num));
					}
					byte[] array = new byte[num];
					int num2 = this._ums.Read(array, 0, num);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num3 = this._store.ReadInt32();
				if (num3 < 0)
				{
					throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourceDataLengthInvalid, num3));
				}
				if (this._ums == null)
				{
					byte[] array2 = this._store.ReadBytes(num3);
					return new PinnedBufferMemoryStream(array2);
				}
				if ((long)num3 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(SR.Format(SR.BadImageFormat_ResourceDataLengthInvalid, num3));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num3, (long)num3, FileAccess.Read);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(SR.BadImageFormat_TypeMismatch);
			}
			int typeIndex = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(typeIndex);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00180F4C File Offset: 0x0018014C
		[MemberNotNull("_typeTable")]
		[MemberNotNull("_typeNamePositions")]
		private void ReadResources()
		{
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted, inner);
			}
			catch (IndexOutOfRangeException inner2)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted, inner2);
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00180F98 File Offset: 0x00180198
		[MemberNotNull("_typeTable")]
		[MemberNotNull("_typeNamePositions")]
		private unsafe void _ReadResources()
		{
			int num = this._store.ReadInt32();
			if (num != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(SR.Resources_StreamNotValid);
			}
			int num2 = this._store.ReadInt32();
			int num3 = this._store.ReadInt32();
			if (num3 < 0 || num2 < 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
			}
			if (num2 > 1)
			{
				this._store.BaseStream.Seek((long)num3, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				if (!ResourceReader.ValidateReaderType(text))
				{
					throw new NotSupportedException(SR.Format(SR.NotSupported_WrongResourceReader_Type, text));
				}
				this.SkipString();
			}
			int num4 = this._store.ReadInt32();
			if (num4 != 2 && num4 != 1)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ResourceFileUnsupportedVersion, 2, num4));
			}
			this._version = num4;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
			}
			int num5 = this._store.ReadInt32();
			if (num5 < 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
			}
			this._typeTable = new Type[num5];
			this._typeNamePositions = new int[num5];
			for (int i = 0; i < num5; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			long position = this._store.BaseStream.Position;
			int num6 = (int)position & 7;
			if (num6 != 0)
			{
				for (int j = 0; j < 8 - num6; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				int num7 = 4 * this._numResources;
				if (num7 < 0)
				{
					throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
				}
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num7, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num8 = this._store.ReadInt32();
					if (num8 < 0)
					{
						throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
					}
					this._namePositions[l] = num8;
				}
			}
			else
			{
				int num9 = 4 * this._numResources;
				if (num9 < 0)
				{
					throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
				}
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num9, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(SR.BadImageFormat_ResourcesHeaderCorrupted);
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x001812B8 File Offset: 0x001804B8
		private Type FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(SR.BadImageFormat_InvalidType);
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string typeName = this._store.ReadString();
					this._typeTable[typeIndex] = Type.GetType(typeName, true);
				}
				catch (FileNotFoundException)
				{
					throw new NotSupportedException(SR.NotSupported_ResourceObjectSerialization);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00181378 File Offset: 0x00180578
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string result;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				result = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return result;
		}

		// Token: 0x0400117B RID: 4475
		private readonly bool _permitDeserialization;

		// Token: 0x0400117C RID: 4476
		private object _binaryFormatter;

		// Token: 0x0400117D RID: 4477
		private static Type s_binaryFormatterType;

		// Token: 0x0400117E RID: 4478
		private static Func<object, Stream, object> s_deserializeMethod;

		// Token: 0x0400117F RID: 4479
		private BinaryReader _store;

		// Token: 0x04001180 RID: 4480
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x04001181 RID: 4481
		private long _nameSectionOffset;

		// Token: 0x04001182 RID: 4482
		private long _dataSectionOffset;

		// Token: 0x04001183 RID: 4483
		private int[] _nameHashes;

		// Token: 0x04001184 RID: 4484
		private unsafe int* _nameHashesPtr;

		// Token: 0x04001185 RID: 4485
		private int[] _namePositions;

		// Token: 0x04001186 RID: 4486
		private unsafe int* _namePositionsPtr;

		// Token: 0x04001187 RID: 4487
		private Type[] _typeTable;

		// Token: 0x04001188 RID: 4488
		private int[] _typeNamePositions;

		// Token: 0x04001189 RID: 4489
		private int _numResources;

		// Token: 0x0400118A RID: 4490
		private UnmanagedMemoryStream _ums;

		// Token: 0x0400118B RID: 4491
		private int _version;

		// Token: 0x0200057D RID: 1405
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06004846 RID: 18502 RVA: 0x00181404 File Offset: 0x00180604
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06004847 RID: 18503 RVA: 0x00181424 File Offset: 0x00180624
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x17000AE1 RID: 2785
			// (get) Token: 0x06004848 RID: 18504 RVA: 0x00181480 File Offset: 0x00180680
			public object Key
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(SR.ResourceReaderIsClosed);
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x17000AE2 RID: 2786
			// (get) Token: 0x06004849 RID: 18505 RVA: 0x001814E7 File Offset: 0x001806E7
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000AE3 RID: 2787
			// (get) Token: 0x0600484A RID: 18506 RVA: 0x001814F4 File Offset: 0x001806F4
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x17000AE4 RID: 2788
			// (get) Token: 0x0600484B RID: 18507 RVA: 0x001814FC File Offset: 0x001806FC
			public DictionaryEntry Entry
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(SR.ResourceReaderIsClosed);
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string key;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							key = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(key, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(key, obj);
				}
			}

			// Token: 0x17000AE5 RID: 2789
			// (get) Token: 0x0600484C RID: 18508 RVA: 0x0018161C File Offset: 0x0018081C
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(SR.ResourceReaderIsClosed);
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x0600484D RID: 18509 RVA: 0x0018167D File Offset: 0x0018087D
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(SR.ResourceReaderIsClosed);
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x0400118C RID: 4492
			private readonly ResourceReader _reader;

			// Token: 0x0400118D RID: 4493
			private bool _currentIsValid;

			// Token: 0x0400118E RID: 4494
			private int _currentName;

			// Token: 0x0400118F RID: 4495
			private int _dataPosition;
		}
	}
}
