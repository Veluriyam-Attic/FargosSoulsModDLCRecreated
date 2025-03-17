using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000A4 RID: 164
	internal struct Variant
	{
		// Token: 0x06000893 RID: 2195
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetFieldsObject(object val);

		// Token: 0x06000894 RID: 2196 RVA: 0x000C487C File Offset: 0x000C3A7C
		internal Variant(int flags, object or, long data)
		{
			this._flags = flags;
			this._objref = or;
			this._data = data;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000C4893 File Offset: 0x000C3A93
		public Variant(bool val)
		{
			this._objref = null;
			this._flags = 2;
			this._data = (val ? 1L : 0L);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000C48B1 File Offset: 0x000C3AB1
		public Variant(sbyte val)
		{
			this._objref = null;
			this._flags = 4;
			this._data = (long)val;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000C48C9 File Offset: 0x000C3AC9
		public Variant(byte val)
		{
			this._objref = null;
			this._flags = 5;
			this._data = (long)((ulong)val);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000C48E1 File Offset: 0x000C3AE1
		public Variant(short val)
		{
			this._objref = null;
			this._flags = 6;
			this._data = (long)val;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000C48F9 File Offset: 0x000C3AF9
		public Variant(ushort val)
		{
			this._objref = null;
			this._flags = 7;
			this._data = (long)((ulong)val);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000C4911 File Offset: 0x000C3B11
		public Variant(char val)
		{
			this._objref = null;
			this._flags = 3;
			this._data = (long)((ulong)val);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000C4929 File Offset: 0x000C3B29
		public Variant(int val)
		{
			this._objref = null;
			this._flags = 8;
			this._data = (long)val;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000C4941 File Offset: 0x000C3B41
		public Variant(uint val)
		{
			this._objref = null;
			this._flags = 9;
			this._data = (long)((ulong)val);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000C495A File Offset: 0x000C3B5A
		public Variant(long val)
		{
			this._objref = null;
			this._flags = 10;
			this._data = val;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000C4972 File Offset: 0x000C3B72
		public Variant(ulong val)
		{
			this._objref = null;
			this._flags = 11;
			this._data = (long)val;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000C498A File Offset: 0x000C3B8A
		public Variant(float val)
		{
			this._objref = null;
			this._flags = 12;
			this._data = (long)((ulong)BitConverter.SingleToInt32Bits(val));
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000C49A8 File Offset: 0x000C3BA8
		public Variant(double val)
		{
			this._objref = null;
			this._flags = 13;
			this._data = BitConverter.DoubleToInt64Bits(val);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000C49C5 File Offset: 0x000C3BC5
		public Variant(DateTime val)
		{
			this._objref = null;
			this._flags = 16;
			this._data = val.Ticks;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000C49E3 File Offset: 0x000C3BE3
		public Variant(decimal val)
		{
			this._objref = val;
			this._flags = 19;
			this._data = 0L;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000C4A04 File Offset: 0x000C3C04
		public Variant(object obj)
		{
			this._data = 0L;
			VarEnum varEnum = VarEnum.VT_EMPTY;
			if (obj is DateTime)
			{
				this._objref = null;
				this._flags = 16;
				this._data = ((DateTime)obj).Ticks;
				return;
			}
			if (obj is string)
			{
				this._flags = 14;
				this._objref = obj;
				return;
			}
			if (obj == null)
			{
				this = System.Variant.Empty;
				return;
			}
			if (obj == System.DBNull.Value)
			{
				this = System.Variant.DBNull;
				return;
			}
			if (obj == Type.Missing)
			{
				this = System.Variant.Missing;
				return;
			}
			if (obj is Array)
			{
				this._flags = 65554;
				this._objref = obj;
				return;
			}
			this._flags = 0;
			this._objref = null;
			if (obj is UnknownWrapper)
			{
				varEnum = VarEnum.VT_UNKNOWN;
				obj = ((UnknownWrapper)obj).WrappedObject;
			}
			else if (obj is DispatchWrapper)
			{
				varEnum = VarEnum.VT_DISPATCH;
				obj = ((DispatchWrapper)obj).WrappedObject;
			}
			else if (obj is ErrorWrapper)
			{
				varEnum = VarEnum.VT_ERROR;
				obj = ((ErrorWrapper)obj).ErrorCode;
			}
			else if (obj is CurrencyWrapper)
			{
				varEnum = VarEnum.VT_CY;
				obj = ((CurrencyWrapper)obj).WrappedObject;
			}
			else if (obj is BStrWrapper)
			{
				varEnum = VarEnum.VT_BSTR;
				obj = ((BStrWrapper)obj).WrappedObject;
			}
			if (obj != null)
			{
				this.SetFieldsObject(obj);
			}
			if (varEnum != VarEnum.VT_EMPTY)
			{
				this._flags |= (int)((int)varEnum << 24);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x000C4B65 File Offset: 0x000C3D65
		internal int CVType
		{
			get
			{
				return this._flags & 65535;
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000C4B74 File Offset: 0x000C3D74
		public object ToObject()
		{
			switch (this.CVType)
			{
			case 0:
				return null;
			case 2:
				return (int)this._data != 0;
			case 3:
				return (char)this._data;
			case 4:
				return (sbyte)this._data;
			case 5:
				return (byte)this._data;
			case 6:
				return (short)this._data;
			case 7:
				return (ushort)this._data;
			case 8:
				return (int)this._data;
			case 9:
				return (uint)this._data;
			case 10:
				return this._data;
			case 11:
				return (ulong)this._data;
			case 12:
				return BitConverter.Int32BitsToSingle((int)this._data);
			case 13:
				return BitConverter.Int64BitsToDouble(this._data);
			case 16:
				return new DateTime(this._data);
			case 17:
				return new TimeSpan(this._data);
			case 21:
				return this.BoxEnum();
			case 22:
				return Type.Missing;
			case 23:
				return System.DBNull.Value;
			}
			return this._objref;
		}

		// Token: 0x060008A6 RID: 2214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object BoxEnum();

		// Token: 0x060008A7 RID: 2215 RVA: 0x000C4D18 File Offset: 0x000C3F18
		internal static void MarshalHelperConvertObjectToVariant(object o, ref System.Variant v)
		{
			if (o == null)
			{
				v = System.Variant.Empty;
				return;
			}
			IConvertible convertible = o as IConvertible;
			if (convertible != null)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				System.Variant variant;
				switch (convertible.GetTypeCode())
				{
				case TypeCode.Empty:
					variant = System.Variant.Empty;
					goto IL_1B6;
				case TypeCode.Object:
					variant = new System.Variant(o);
					goto IL_1B6;
				case TypeCode.DBNull:
					variant = System.Variant.DBNull;
					goto IL_1B6;
				case TypeCode.Boolean:
					variant = new System.Variant(convertible.ToBoolean(invariantCulture));
					goto IL_1B6;
				case TypeCode.Char:
					variant = new System.Variant(convertible.ToChar(invariantCulture));
					goto IL_1B6;
				case TypeCode.SByte:
					variant = new System.Variant(convertible.ToSByte(invariantCulture));
					goto IL_1B6;
				case TypeCode.Byte:
					variant = new System.Variant(convertible.ToByte(invariantCulture));
					goto IL_1B6;
				case TypeCode.Int16:
					variant = new System.Variant(convertible.ToInt16(invariantCulture));
					goto IL_1B6;
				case TypeCode.UInt16:
					variant = new System.Variant(convertible.ToUInt16(invariantCulture));
					goto IL_1B6;
				case TypeCode.Int32:
					variant = new System.Variant(convertible.ToInt32(invariantCulture));
					goto IL_1B6;
				case TypeCode.UInt32:
					variant = new System.Variant(convertible.ToUInt32(invariantCulture));
					goto IL_1B6;
				case TypeCode.Int64:
					variant = new System.Variant(convertible.ToInt64(invariantCulture));
					goto IL_1B6;
				case TypeCode.UInt64:
					variant = new System.Variant(convertible.ToUInt64(invariantCulture));
					goto IL_1B6;
				case TypeCode.Single:
					variant = new System.Variant(convertible.ToSingle(invariantCulture));
					goto IL_1B6;
				case TypeCode.Double:
					variant = new System.Variant(convertible.ToDouble(invariantCulture));
					goto IL_1B6;
				case TypeCode.Decimal:
					variant = new System.Variant(convertible.ToDecimal(invariantCulture));
					goto IL_1B6;
				case TypeCode.DateTime:
					variant = new System.Variant(convertible.ToDateTime(invariantCulture));
					goto IL_1B6;
				case TypeCode.String:
					variant = new System.Variant(convertible.ToString(invariantCulture));
					goto IL_1B6;
				}
				throw new NotSupportedException(SR.Format(SR.NotSupported_UnknownTypeCode, convertible.GetTypeCode()));
				IL_1B6:
				v = variant;
				return;
			}
			v = new System.Variant(o);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000C4EEF File Offset: 0x000C40EF
		internal static object MarshalHelperConvertVariantToObject(ref System.Variant v)
		{
			return v.ToObject();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000C4EF8 File Offset: 0x000C40F8
		internal static void MarshalHelperCastVariant(object pValue, int vt, ref System.Variant v)
		{
			IConvertible convertible = pValue as IConvertible;
			System.Variant variant;
			if (convertible == null)
			{
				switch (vt)
				{
				case 8:
					if (pValue == null)
					{
						variant = new System.Variant(null)
						{
							_flags = 14
						};
						v = variant;
						return;
					}
					break;
				case 9:
					v = new System.Variant(new DispatchWrapper(pValue));
					return;
				case 10:
				case 11:
					break;
				case 12:
					v = new System.Variant(pValue);
					return;
				case 13:
					v = new System.Variant(new UnknownWrapper(pValue));
					return;
				default:
					if (vt == 36)
					{
						v = new System.Variant(pValue);
						return;
					}
					break;
				}
				throw new InvalidCastException(SR.InvalidCast_CannotCoerceByRefVariant);
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			switch (vt)
			{
			case 0:
				variant = System.Variant.Empty;
				goto IL_28F;
			case 1:
				variant = System.Variant.DBNull;
				goto IL_28F;
			case 2:
				variant = new System.Variant(convertible.ToInt16(invariantCulture));
				goto IL_28F;
			case 3:
				variant = new System.Variant(convertible.ToInt32(invariantCulture));
				goto IL_28F;
			case 4:
				variant = new System.Variant(convertible.ToSingle(invariantCulture));
				goto IL_28F;
			case 5:
				variant = new System.Variant(convertible.ToDouble(invariantCulture));
				goto IL_28F;
			case 6:
				variant = new System.Variant(new CurrencyWrapper(convertible.ToDecimal(invariantCulture)));
				goto IL_28F;
			case 7:
				variant = new System.Variant(convertible.ToDateTime(invariantCulture));
				goto IL_28F;
			case 8:
				variant = new System.Variant(convertible.ToString(invariantCulture));
				goto IL_28F;
			case 9:
				variant = new System.Variant(new DispatchWrapper(convertible));
				goto IL_28F;
			case 10:
				variant = new System.Variant(new ErrorWrapper(convertible.ToInt32(invariantCulture)));
				goto IL_28F;
			case 11:
				variant = new System.Variant(convertible.ToBoolean(invariantCulture));
				goto IL_28F;
			case 12:
				variant = new System.Variant(convertible);
				goto IL_28F;
			case 13:
				variant = new System.Variant(new UnknownWrapper(convertible));
				goto IL_28F;
			case 14:
				variant = new System.Variant(convertible.ToDecimal(invariantCulture));
				goto IL_28F;
			case 16:
				variant = new System.Variant(convertible.ToSByte(invariantCulture));
				goto IL_28F;
			case 17:
				variant = new System.Variant(convertible.ToByte(invariantCulture));
				goto IL_28F;
			case 18:
				variant = new System.Variant(convertible.ToUInt16(invariantCulture));
				goto IL_28F;
			case 19:
				variant = new System.Variant(convertible.ToUInt32(invariantCulture));
				goto IL_28F;
			case 20:
				variant = new System.Variant(convertible.ToInt64(invariantCulture));
				goto IL_28F;
			case 21:
				variant = new System.Variant(convertible.ToUInt64(invariantCulture));
				goto IL_28F;
			case 22:
				variant = new System.Variant(convertible.ToInt32(invariantCulture));
				goto IL_28F;
			case 23:
				variant = new System.Variant(convertible.ToUInt32(invariantCulture));
				goto IL_28F;
			}
			throw new InvalidCastException(SR.InvalidCast_CannotCoerceByRefVariant);
			IL_28F:
			v = variant;
		}

		// Token: 0x04000227 RID: 551
		private object _objref;

		// Token: 0x04000228 RID: 552
		private long _data;

		// Token: 0x04000229 RID: 553
		private int _flags;

		// Token: 0x0400022A RID: 554
		internal const int CV_EMPTY = 0;

		// Token: 0x0400022B RID: 555
		internal const int CV_VOID = 1;

		// Token: 0x0400022C RID: 556
		internal const int CV_BOOLEAN = 2;

		// Token: 0x0400022D RID: 557
		internal const int CV_CHAR = 3;

		// Token: 0x0400022E RID: 558
		internal const int CV_I1 = 4;

		// Token: 0x0400022F RID: 559
		internal const int CV_U1 = 5;

		// Token: 0x04000230 RID: 560
		internal const int CV_I2 = 6;

		// Token: 0x04000231 RID: 561
		internal const int CV_U2 = 7;

		// Token: 0x04000232 RID: 562
		internal const int CV_I4 = 8;

		// Token: 0x04000233 RID: 563
		internal const int CV_U4 = 9;

		// Token: 0x04000234 RID: 564
		internal const int CV_I8 = 10;

		// Token: 0x04000235 RID: 565
		internal const int CV_U8 = 11;

		// Token: 0x04000236 RID: 566
		internal const int CV_R4 = 12;

		// Token: 0x04000237 RID: 567
		internal const int CV_R8 = 13;

		// Token: 0x04000238 RID: 568
		internal const int CV_STRING = 14;

		// Token: 0x04000239 RID: 569
		internal const int CV_PTR = 15;

		// Token: 0x0400023A RID: 570
		internal const int CV_DATETIME = 16;

		// Token: 0x0400023B RID: 571
		internal const int CV_TIMESPAN = 17;

		// Token: 0x0400023C RID: 572
		internal const int CV_OBJECT = 18;

		// Token: 0x0400023D RID: 573
		internal const int CV_DECIMAL = 19;

		// Token: 0x0400023E RID: 574
		internal const int CV_ENUM = 21;

		// Token: 0x0400023F RID: 575
		internal const int CV_MISSING = 22;

		// Token: 0x04000240 RID: 576
		internal const int CV_NULL = 23;

		// Token: 0x04000241 RID: 577
		internal const int CV_LAST = 24;

		// Token: 0x04000242 RID: 578
		internal const int TypeCodeBitMask = 65535;

		// Token: 0x04000243 RID: 579
		internal const int VTBitMask = -16777216;

		// Token: 0x04000244 RID: 580
		internal const int VTBitShift = 24;

		// Token: 0x04000245 RID: 581
		internal const int ArrayBitMask = 65536;

		// Token: 0x04000246 RID: 582
		internal const int EnumI1 = 1048576;

		// Token: 0x04000247 RID: 583
		internal const int EnumU1 = 2097152;

		// Token: 0x04000248 RID: 584
		internal const int EnumI2 = 3145728;

		// Token: 0x04000249 RID: 585
		internal const int EnumU2 = 4194304;

		// Token: 0x0400024A RID: 586
		internal const int EnumI4 = 5242880;

		// Token: 0x0400024B RID: 587
		internal const int EnumU4 = 6291456;

		// Token: 0x0400024C RID: 588
		internal const int EnumI8 = 7340032;

		// Token: 0x0400024D RID: 589
		internal const int EnumU8 = 8388608;

		// Token: 0x0400024E RID: 590
		internal const int EnumMask = 15728640;

		// Token: 0x0400024F RID: 591
		internal static readonly Type[] ClassTypes = new Type[]
		{
			typeof(Empty),
			typeof(void),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(string),
			typeof(void),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(object),
			typeof(decimal),
			typeof(object),
			typeof(Missing),
			typeof(DBNull)
		};

		// Token: 0x04000250 RID: 592
		internal static readonly System.Variant Empty;

		// Token: 0x04000251 RID: 593
		internal static readonly System.Variant Missing = new System.Variant(22, Type.Missing, 0L);

		// Token: 0x04000252 RID: 594
		internal static readonly System.Variant DBNull = new System.Variant(23, System.DBNull.Value, 0L);
	}
}
