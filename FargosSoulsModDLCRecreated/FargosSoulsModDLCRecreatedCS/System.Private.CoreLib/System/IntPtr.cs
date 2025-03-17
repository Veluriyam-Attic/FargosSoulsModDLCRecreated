using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000137 RID: 311
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct IntPtr : IEquatable<IntPtr>, IComparable, IComparable<IntPtr>, IFormattable, ISerializable
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x000DAD49 File Offset: 0x000D9F49
		[NonVersionable]
		public IntPtr(int value)
		{
			this._value = value;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000DAD53 File Offset: 0x000D9F53
		[NonVersionable]
		public IntPtr(long value)
		{
			this._value = value;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000DAD5D File Offset: 0x000D9F5D
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe IntPtr(void* value)
		{
			this._value = value;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000DAD68 File Offset: 0x000D9F68
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			if (IntPtr.Size == 4)
			{
			}
			this._value = @int;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000DAD91 File Offset: 0x000D9F91
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToInt64());
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000DADB4 File Offset: 0x000D9FB4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is IntPtr)
			{
				IntPtr intPtr = (IntPtr)obj;
				return this._value == intPtr._value;
			}
			return false;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000DADE4 File Offset: 0x000D9FE4
		public override int GetHashCode()
		{
			long num = this._value;
			return (int)num ^ (int)(num >> 32);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000DAE04 File Offset: 0x000DA004
		[NonVersionable]
		public int ToInt32()
		{
			long num = this._value;
			return checked((int)num);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000DAE1B File Offset: 0x000DA01B
		[NonVersionable]
		public long ToInt64()
		{
			return this._value;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000DAE24 File Offset: 0x000DA024
		[NonVersionable]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000DAE2C File Offset: 0x000DA02C
		[NonVersionable]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000DAE34 File Offset: 0x000DA034
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000DAE3C File Offset: 0x000DA03C
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value._value;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000DAE48 File Offset: 0x000DA048
		[NonVersionable]
		public static explicit operator int(IntPtr value)
		{
			long num = value._value;
			return checked((int)num);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000DAE60 File Offset: 0x000DA060
		[NonVersionable]
		public static explicit operator long(IntPtr value)
		{
			return value._value;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000DAE6A File Offset: 0x000DA06A
		[NonVersionable]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1._value == value2._value;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000DAE7C File Offset: 0x000DA07C
		[NonVersionable]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1._value != value2._value;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000DAE91 File Offset: 0x000DA091
		[NonVersionable]
		public static IntPtr Add(IntPtr pointer, int offset)
		{
			return pointer + offset;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000DAE9A File Offset: 0x000DA09A
		[NonVersionable]
		public unsafe static IntPtr operator +(IntPtr pointer, int offset)
		{
			return (byte*)pointer._value + offset;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000DAEA6 File Offset: 0x000DA0A6
		[NonVersionable]
		public static IntPtr Subtract(IntPtr pointer, int offset)
		{
			return pointer - offset;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000DAEAF File Offset: 0x000DA0AF
		[NonVersionable]
		public unsafe static IntPtr operator -(IntPtr pointer, int offset)
		{
			return (byte*)pointer._value - offset;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		public static int Size
		{
			[NonVersionable]
			get
			{
				return 8;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000DAEBE File Offset: 0x000DA0BE
		[CLSCompliant(false)]
		[NonVersionable]
		public unsafe void* ToPointer()
		{
			return this._value;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000DAEC6 File Offset: 0x000DA0C6
		public static IntPtr MaxValue
		{
			[NonVersionable]
			get
			{
				return (IntPtr)long.MaxValue;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000DAED6 File Offset: 0x000DA0D6
		public static IntPtr MinValue
		{
			[NonVersionable]
			get
			{
				return (IntPtr)long.MinValue;
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000DAEE8 File Offset: 0x000DA0E8
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is IntPtr))
			{
				throw new ArgumentException(SR.Arg_MustBeIntPtr);
			}
			IntPtr intPtr = (IntPtr)value;
			if (this._value < intPtr)
			{
				return -1;
			}
			if (this._value > intPtr)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000DAF2C File Offset: 0x000DA12C
		public int CompareTo(IntPtr value)
		{
			return this._value.CompareTo((long)value);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000DAF4E File Offset: 0x000DA14E
		[NonVersionable]
		public bool Equals(IntPtr other)
		{
			return this._value == (long)other;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000DAF60 File Offset: 0x000DA160
		[NullableContext(1)]
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000DAF7C File Offset: 0x000DA17C
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000DAF9C File Offset: 0x000DA19C
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return this._value.ToString(provider);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000DAFBC File Offset: 0x000DA1BC
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return this._value.ToString(format, provider);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000DAFDA File Offset: 0x000DA1DA
		[NullableContext(1)]
		public static IntPtr Parse(string s)
		{
			return (IntPtr)long.Parse(s);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000DAFE7 File Offset: 0x000DA1E7
		[NullableContext(1)]
		public static IntPtr Parse(string s, NumberStyles style)
		{
			return (IntPtr)long.Parse(s, style);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000DAFF5 File Offset: 0x000DA1F5
		[NullableContext(1)]
		public static IntPtr Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			return (IntPtr)long.Parse(s, provider);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000DB003 File Offset: 0x000DA203
		[NullableContext(1)]
		public static IntPtr Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			return (IntPtr)long.Parse(s, style, provider);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000DB012 File Offset: 0x000DA212
		[NullableContext(2)]
		public static bool TryParse(string s, out IntPtr result)
		{
			Unsafe.SkipInit<IntPtr>(out result);
			return long.TryParse(s, Unsafe.As<IntPtr, long>(ref result));
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000DB026 File Offset: 0x000DA226
		[NullableContext(2)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out IntPtr result)
		{
			Unsafe.SkipInit<IntPtr>(out result);
			return long.TryParse(s, style, provider, Unsafe.As<IntPtr, long>(ref result));
		}

		// Token: 0x040003F3 RID: 1011
		private unsafe readonly void* _value;

		// Token: 0x040003F4 RID: 1012
		[Intrinsic]
		public static readonly IntPtr Zero;
	}
}
