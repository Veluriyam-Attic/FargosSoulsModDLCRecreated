using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x020003EA RID: 1002
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class SerializationInfo
	{
		// Token: 0x06003220 RID: 12832 RVA: 0x0016A6D0 File Offset: 0x001698D0
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this._rootType = type;
			this._rootTypeName = type.FullName;
			this._rootTypeAssemblyName = type.Module.Assembly.FullName;
			this._names = new string[4];
			this._values = new object[4];
			this._types = new Type[4];
			this._nameToIndex = new Dictionary<string, int>();
			this._converter = converter;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x0016A75E File Offset: 0x0016995E
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust) : this(type, converter)
		{
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0016A768 File Offset: 0x00169968
		// (set) Token: 0x06003223 RID: 12835 RVA: 0x0016A770 File Offset: 0x00169970
		public string FullTypeName
		{
			get
			{
				return this._rootTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._rootTypeName = value;
				this.IsFullTypeNameSetExplicit = true;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x0016A78E File Offset: 0x0016998E
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x0016A796 File Offset: 0x00169996
		public string AssemblyName
		{
			get
			{
				return this._rootTypeAssemblyName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._rootTypeAssemblyName = value;
				this.IsAssemblyNameSetExplicit = true;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x0016A7B4 File Offset: 0x001699B4
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x0016A7BC File Offset: 0x001699BC
		public bool IsFullTypeNameSetExplicit { get; private set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x0016A7C5 File Offset: 0x001699C5
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x0016A7CD File Offset: 0x001699CD
		public bool IsAssemblyNameSetExplicit { get; private set; }

		// Token: 0x0600322A RID: 12842 RVA: 0x0016A7D8 File Offset: 0x001699D8
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this._rootType != type)
			{
				this._rootType = type;
				this._rootTypeName = type.FullName;
				this._rootTypeAssemblyName = type.Module.Assembly.FullName;
				this.IsFullTypeNameSetExplicit = false;
				this.IsAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x0016A833 File Offset: 0x00169A33
		public int MemberCount
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x0016A83B File Offset: 0x00169A3B
		public Type ObjectType
		{
			get
			{
				return this._rootType;
			}
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0016A843 File Offset: 0x00169A43
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this._names, this._values, this._types, this._count);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x0016A864 File Offset: 0x00169A64
		private void ExpandArrays()
		{
			int num = this._count * 2;
			if (num < this._count && 2147483647 > this._count)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this._names, array, this._count);
			Array.Copy(this._values, array2, this._count);
			Array.Copy(this._types, array3, this._count);
			this._names = array;
			this._values = array2;
			this._types = array3;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x0016A8F6 File Offset: 0x00169AF6
		public void AddValue(string name, [Nullable(2)] object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x0016A91D File Offset: 0x00169B1D
		public void AddValue(string name, [Nullable(2)] object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x0016A943 File Offset: 0x00169B43
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x0016A95C File Offset: 0x00169B5C
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x0016A975 File Offset: 0x00169B75
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x0016A98E File Offset: 0x00169B8E
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x0016A9A7 File Offset: 0x00169BA7
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0016A9C0 File Offset: 0x00169BC0
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x0016A9D9 File Offset: 0x00169BD9
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x0016A9F2 File Offset: 0x00169BF2
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x0016AA0B File Offset: 0x00169C0B
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x0016AA24 File Offset: 0x00169C24
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x0016AA3D File Offset: 0x00169C3D
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x0016AA56 File Offset: 0x00169C56
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0016AA6F File Offset: 0x00169C6F
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0016AA88 File Offset: 0x00169C88
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x0016AAA4 File Offset: 0x00169CA4
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this._nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(SR.Serialization_SameNameTwice);
			}
			this._nameToIndex.Add(name, this._count);
			if (this._count >= this._names.Length)
			{
				this.ExpandArrays();
			}
			this._names[this._count] = name;
			this._values[this._count] = value;
			this._types[this._count] = type;
			this._count++;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0016AB2C File Offset: 0x00169D2C
		public void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this._values[num] = value;
			this._types[num] = type;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x0016AB64 File Offset: 0x00169D64
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int result;
			if (this._nameToIndex.TryGetValue(name, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0016AB94 File Offset: 0x00169D94
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(SR.Format(SR.Serialization_NotFound, name));
			}
			foundType = this._types[num];
			return this._values[num];
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0016ABD0 File Offset: 0x00169DD0
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this._types[num];
			return this._values[num];
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x0016AC00 File Offset: 0x00169E00
		[return: Nullable(2)]
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType);
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this._converter.Convert(element, type);
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0016AC58 File Offset: 0x00169E58
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (type2 == type || type.IsAssignableFrom(type2))
			{
				return elementNoThrow;
			}
			return this._converter.Convert(elementNoThrow, type);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x0016AC90 File Offset: 0x00169E90
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(bool))
			{
				return this._converter.ToBoolean(element);
			}
			return (bool)element;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x0016ACC8 File Offset: 0x00169EC8
		public char GetChar(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(char))
			{
				return this._converter.ToChar(element);
			}
			return (char)element;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x0016AD00 File Offset: 0x00169F00
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(sbyte))
			{
				return this._converter.ToSByte(element);
			}
			return (sbyte)element;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x0016AD38 File Offset: 0x00169F38
		public byte GetByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(byte))
			{
				return this._converter.ToByte(element);
			}
			return (byte)element;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x0016AD70 File Offset: 0x00169F70
		public short GetInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(short))
			{
				return this._converter.ToInt16(element);
			}
			return (short)element;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0016ADA8 File Offset: 0x00169FA8
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(ushort))
			{
				return this._converter.ToUInt16(element);
			}
			return (ushort)element;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0016ADE0 File Offset: 0x00169FE0
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(int))
			{
				return this._converter.ToInt32(element);
			}
			return (int)element;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0016AE18 File Offset: 0x0016A018
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(uint))
			{
				return this._converter.ToUInt32(element);
			}
			return (uint)element;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x0016AE50 File Offset: 0x0016A050
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(long))
			{
				return this._converter.ToInt64(element);
			}
			return (long)element;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x0016AE88 File Offset: 0x0016A088
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(ulong))
			{
				return this._converter.ToUInt64(element);
			}
			return (ulong)element;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x0016AEC0 File Offset: 0x0016A0C0
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(float))
			{
				return this._converter.ToSingle(element);
			}
			return (float)element;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x0016AEF8 File Offset: 0x0016A0F8
		public double GetDouble(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(double))
			{
				return this._converter.ToDouble(element);
			}
			return (double)element;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0016AF30 File Offset: 0x0016A130
		public decimal GetDecimal(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(decimal))
			{
				return this._converter.ToDecimal(element);
			}
			return (decimal)element;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x0016AF68 File Offset: 0x0016A168
		public DateTime GetDateTime(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(DateTime))
			{
				return this._converter.ToDateTime(element);
			}
			return (DateTime)element;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x0016AFA0 File Offset: 0x0016A1A0
		[return: Nullable(2)]
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type != typeof(string) && element != null)
			{
				return this._converter.ToString(element);
			}
			return (string)element;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x0016AFDA File Offset: 0x0016A1DA
		internal static AsyncLocal<bool> AsyncDeserializationInProgress { get; } = new AsyncLocal<bool>();

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x0016AFE4 File Offset: 0x0016A1E4
		public static bool DeserializationInProgress
		{
			get
			{
				if (SerializationInfo.AsyncDeserializationInProgress.Value)
				{
					return true;
				}
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				DeserializationTracker threadDeserializationTracker = Thread.GetThreadDeserializationTracker(ref stackCrawlMark);
				return threadDeserializationTracker.DeserializationInProgress;
			}
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x0016B011 File Offset: 0x0016A211
		public static void ThrowIfDeserializationInProgress()
		{
			if (SerializationInfo.DeserializationInProgress)
			{
				throw new SerializationException(SR.Serialization_DangerousDeserialization);
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x0016B028 File Offset: 0x0016A228
		public static void ThrowIfDeserializationInProgress(string switchSuffix, ref int cachedValue)
		{
			if (switchSuffix == null)
			{
				throw new ArgumentNullException("switchSuffix");
			}
			if (string.IsNullOrWhiteSpace(switchSuffix))
			{
				throw new ArgumentException(SR.Argument_EmptyName, "switchSuffix");
			}
			if (cachedValue == 0)
			{
				bool flag;
				if (AppContext.TryGetSwitch("Switch.System.Runtime.Serialization.SerializationGuard." + switchSuffix, out flag) && flag)
				{
					cachedValue = 1;
				}
				else
				{
					cachedValue = -1;
				}
			}
			if (cachedValue == 1)
			{
				return;
			}
			if (cachedValue != -1)
			{
				throw new ArgumentOutOfRangeException("cachedValue");
			}
			if (SerializationInfo.DeserializationInProgress)
			{
				throw new SerializationException(SR.Format(SR.Serialization_DangerousDeserialization_Switch, "Switch.System.Runtime.Serialization.SerializationGuard." + switchSuffix));
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x0016B0B8 File Offset: 0x0016A2B8
		public static DeserializationToken StartDeserialization()
		{
			if (LocalAppContextSwitches.SerializationGuard)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
				DeserializationTracker threadDeserializationTracker = Thread.GetThreadDeserializationTracker(ref stackCrawlMark);
				if (!threadDeserializationTracker.DeserializationInProgress)
				{
					DeserializationTracker obj = threadDeserializationTracker;
					lock (obj)
					{
						if (!threadDeserializationTracker.DeserializationInProgress)
						{
							SerializationInfo.AsyncDeserializationInProgress.Value = true;
							threadDeserializationTracker.DeserializationInProgress = true;
							return new DeserializationToken(threadDeserializationTracker);
						}
					}
				}
			}
			return new DeserializationToken(null);
		}

		// Token: 0x04000DF5 RID: 3573
		private string[] _names;

		// Token: 0x04000DF6 RID: 3574
		private object[] _values;

		// Token: 0x04000DF7 RID: 3575
		private Type[] _types;

		// Token: 0x04000DF8 RID: 3576
		private int _count;

		// Token: 0x04000DF9 RID: 3577
		private readonly Dictionary<string, int> _nameToIndex;

		// Token: 0x04000DFA RID: 3578
		private readonly IFormatterConverter _converter;

		// Token: 0x04000DFB RID: 3579
		private string _rootTypeName;

		// Token: 0x04000DFC RID: 3580
		private string _rootTypeAssemblyName;

		// Token: 0x04000DFD RID: 3581
		private Type _rootType;
	}
}
