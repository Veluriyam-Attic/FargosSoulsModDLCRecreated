using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001B3 RID: 435
	[CLSCompliant(false)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct UIntPtr : IEquatable<UIntPtr>, IComparable, IComparable<UIntPtr>, IFormattable, ISerializable
	{
		// Token: 0x06001AA9 RID: 6825 RVA: 0x000FCC38 File Offset: 0x000FBE38
		[NonVersionable]
		public UIntPtr(uint value)
		{
			this._value = value;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000FCC38 File Offset: 0x000FBE38
		[NonVersionable]
		public UIntPtr(ulong value)
		{
			this._value = value;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000FCC42 File Offset: 0x000FBE42
		[NonVersionable]
		public unsafe UIntPtr(void* value)
		{
			this._value = value;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000FCC4C File Offset: 0x000FBE4C
		private UIntPtr(SerializationInfo info, StreamingContext context)
		{
			ulong @uint = info.GetUInt64("value");
			if (UIntPtr.Size == 4)
			{
			}
			this._value = @uint;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000FCC75 File Offset: 0x000FBE75
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToUInt64());
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000FCC96 File Offset: 0x000FBE96
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is UIntPtr && this._value == ((UIntPtr)obj)._value;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000FCCB8 File Offset: 0x000FBEB8
		public override int GetHashCode()
		{
			ulong num = this._value;
			return (int)num ^ (int)(num >> 32);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000FCCD5 File Offset: 0x000FBED5
		[NonVersionable]
		public uint ToUInt32()
		{
			return this._value;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000FCCDE File Offset: 0x000FBEDE
		[NonVersionable]
		public ulong ToUInt64()
		{
			return this._value;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000FCCE7 File Offset: 0x000FBEE7
		[NonVersionable]
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000FCCEF File Offset: 0x000FBEEF
		[NonVersionable]
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000FCCF7 File Offset: 0x000FBEF7
		[NonVersionable]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000FCCFF File Offset: 0x000FBEFF
		[NonVersionable]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value._value;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000FCD08 File Offset: 0x000FBF08
		[NonVersionable]
		public static explicit operator uint(UIntPtr value)
		{
			return value._value;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000FCD12 File Offset: 0x000FBF12
		[NonVersionable]
		public static explicit operator ulong(UIntPtr value)
		{
			return value._value;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000FCD1C File Offset: 0x000FBF1C
		[NonVersionable]
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._value == value2._value;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000FCD2E File Offset: 0x000FBF2E
		[NonVersionable]
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._value != value2._value;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000FCD43 File Offset: 0x000FBF43
		[NonVersionable]
		public static UIntPtr Add(UIntPtr pointer, int offset)
		{
			return pointer + offset;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000FCD4C File Offset: 0x000FBF4C
		[NonVersionable]
		public unsafe static UIntPtr operator +(UIntPtr pointer, int offset)
		{
			return (byte*)pointer._value + offset;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000FCD58 File Offset: 0x000FBF58
		[NonVersionable]
		public static UIntPtr Subtract(UIntPtr pointer, int offset)
		{
			return pointer - offset;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000FCD61 File Offset: 0x000FBF61
		[NonVersionable]
		public unsafe static UIntPtr operator -(UIntPtr pointer, int offset)
		{
			return (byte*)pointer._value - offset;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		public static int Size
		{
			[NonVersionable]
			get
			{
				return 8;
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000FCD6D File Offset: 0x000FBF6D
		[NonVersionable]
		public unsafe void* ToPointer()
		{
			return this._value;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x000FCD75 File Offset: 0x000FBF75
		public static UIntPtr MaxValue
		{
			[NonVersionable]
			get
			{
				return (UIntPtr)ulong.MaxValue;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000FCD7E File Offset: 0x000FBF7E
		public static UIntPtr MinValue
		{
			[NonVersionable]
			get
			{
				return (UIntPtr)0UL;
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000FCD88 File Offset: 0x000FBF88
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is UIntPtr))
			{
				throw new ArgumentException(SR.Arg_MustBeUIntPtr);
			}
			UIntPtr uintPtr = (UIntPtr)value;
			if (this._value < uintPtr)
			{
				return -1;
			}
			if (this._value != uintPtr)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000FCDCC File Offset: 0x000FBFCC
		public int CompareTo(UIntPtr value)
		{
			return this._value.CompareTo((ulong)value);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000FCDEE File Offset: 0x000FBFEE
		[NonVersionable]
		public bool Equals(UIntPtr other)
		{
			return this._value == other;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000FCDFC File Offset: 0x000FBFFC
		[NullableContext(1)]
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000FCE18 File Offset: 0x000FC018
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this._value.ToString(format);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000FCE38 File Offset: 0x000FC038
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return this._value.ToString(provider);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000FCE58 File Offset: 0x000FC058
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return this._value.ToString(format, provider);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000FCE76 File Offset: 0x000FC076
		[NullableContext(1)]
		public static UIntPtr Parse(string s)
		{
			return (UIntPtr)ulong.Parse(s);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000FCE83 File Offset: 0x000FC083
		[NullableContext(1)]
		public static UIntPtr Parse(string s, NumberStyles style)
		{
			return (UIntPtr)ulong.Parse(s, style);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000FCE91 File Offset: 0x000FC091
		[NullableContext(1)]
		public static UIntPtr Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			return (UIntPtr)ulong.Parse(s, provider);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000FCE9F File Offset: 0x000FC09F
		[NullableContext(1)]
		public static UIntPtr Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			return (UIntPtr)ulong.Parse(s, style, provider);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000FCEAE File Offset: 0x000FC0AE
		[NullableContext(2)]
		public static bool TryParse(string s, out UIntPtr result)
		{
			Unsafe.SkipInit<UIntPtr>(out result);
			return ulong.TryParse(s, Unsafe.As<UIntPtr, ulong>(ref result));
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000FCEC2 File Offset: 0x000FC0C2
		[NullableContext(2)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out UIntPtr result)
		{
			Unsafe.SkipInit<UIntPtr>(out result);
			return ulong.TryParse(s, style, provider, Unsafe.As<UIntPtr, ulong>(ref result));
		}

		// Token: 0x040005D4 RID: 1492
		private unsafe readonly void* _value;

		// Token: 0x040005D5 RID: 1493
		[Intrinsic]
		public static readonly UIntPtr Zero;
	}
}
