using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200045E RID: 1118
	[StructLayout(LayoutKind.Explicit)]
	internal struct Variant
	{
		// Token: 0x0600440E RID: 17422 RVA: 0x001782BC File Offset: 0x001774BC
		public unsafe void CopyFromIndirect(object value)
		{
			VarEnum varEnum = this.VariantType & (VarEnum)(-16385);
			if (value == null)
			{
				if (varEnum == VarEnum.VT_DISPATCH || varEnum == VarEnum.VT_UNKNOWN || varEnum == VarEnum.VT_BSTR)
				{
					*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = IntPtr.Zero;
				}
				return;
			}
			if ((varEnum & VarEnum.VT_ARRAY) != VarEnum.VT_EMPTY)
			{
				Variant variant;
				Marshal.GetNativeVariantForObject(value, (IntPtr)((void*)(&variant)));
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = variant._typeUnion._unionTypes._byref;
				return;
			}
			switch (varEnum)
			{
			case VarEnum.VT_I2:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (short)value;
				return;
			case VarEnum.VT_I4:
			case VarEnum.VT_INT:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = (int)value;
				return;
			case VarEnum.VT_R4:
				*(float*)((void*)this._typeUnion._unionTypes._byref) = (float)value;
				return;
			case VarEnum.VT_R8:
				*(double*)((void*)this._typeUnion._unionTypes._byref) = (double)value;
				return;
			case VarEnum.VT_CY:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = decimal.ToOACurrency((decimal)value);
				return;
			case VarEnum.VT_DATE:
				*(double*)((void*)this._typeUnion._unionTypes._byref) = ((DateTime)value).ToOADate();
				return;
			case VarEnum.VT_BSTR:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.StringToBSTR((string)value);
				return;
			case VarEnum.VT_DISPATCH:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.GetComInterfaceForObject<object, IDispatch>(value);
				return;
			case VarEnum.VT_ERROR:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = ((ErrorWrapper)value).ErrorCode;
				return;
			case VarEnum.VT_BOOL:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (((bool)value) ? -1 : 0);
				return;
			case VarEnum.VT_VARIANT:
				Marshal.GetNativeVariantForObject(value, this._typeUnion._unionTypes._byref);
				return;
			case VarEnum.VT_UNKNOWN:
				*(IntPtr*)((void*)this._typeUnion._unionTypes._byref) = Marshal.GetIUnknownForObject(value);
				return;
			case VarEnum.VT_DECIMAL:
				*(decimal*)((void*)this._typeUnion._unionTypes._byref) = (decimal)value;
				return;
			case VarEnum.VT_I1:
				*(byte*)((void*)this._typeUnion._unionTypes._byref) = (byte)((sbyte)value);
				return;
			case VarEnum.VT_UI1:
				*(byte*)((void*)this._typeUnion._unionTypes._byref) = (byte)value;
				return;
			case VarEnum.VT_UI2:
				*(short*)((void*)this._typeUnion._unionTypes._byref) = (short)((ushort)value);
				return;
			case VarEnum.VT_UI4:
			case VarEnum.VT_UINT:
				*(int*)((void*)this._typeUnion._unionTypes._byref) = (int)((uint)value);
				return;
			case VarEnum.VT_I8:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = (long)value;
				return;
			case VarEnum.VT_UI8:
				*(long*)((void*)this._typeUnion._unionTypes._byref) = (long)((ulong)value);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x001785F0 File Offset: 0x001777F0
		public unsafe object ToObject()
		{
			if (this.IsEmpty)
			{
				return null;
			}
			switch (this.VariantType)
			{
			case VarEnum.VT_NULL:
				return DBNull.Value;
			case VarEnum.VT_I2:
				return this.AsI2;
			case VarEnum.VT_I4:
				return this.AsI4;
			case VarEnum.VT_R4:
				return this.AsR4;
			case VarEnum.VT_R8:
				return this.AsR8;
			case VarEnum.VT_CY:
				return this.AsCy;
			case VarEnum.VT_DATE:
				return this.AsDate;
			case VarEnum.VT_BSTR:
				return this.AsBstr;
			case VarEnum.VT_DISPATCH:
				return this.AsDispatch;
			case VarEnum.VT_ERROR:
				return this.AsError;
			case VarEnum.VT_BOOL:
				return this.AsBool;
			case VarEnum.VT_UNKNOWN:
				return this.AsUnknown;
			case VarEnum.VT_DECIMAL:
				return this.AsDecimal;
			case VarEnum.VT_I1:
				return this.AsI1;
			case VarEnum.VT_UI1:
				return this.AsUi1;
			case VarEnum.VT_UI2:
				return this.AsUi2;
			case VarEnum.VT_UI4:
				return this.AsUi4;
			case VarEnum.VT_I8:
				return this.AsI8;
			case VarEnum.VT_UI8:
				return this.AsUi8;
			case VarEnum.VT_INT:
				return this.AsInt;
			case VarEnum.VT_UINT:
				return this.AsUint;
			}
			fixed (Variant* ptr = &this)
			{
				void* value = (void*)ptr;
				return Marshal.GetObjectForNativeVariant((IntPtr)value);
			}
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x00178770 File Offset: 0x00177970
		public unsafe void Clear()
		{
			VarEnum variantType = this.VariantType;
			if ((variantType & VarEnum.VT_BYREF) != VarEnum.VT_EMPTY)
			{
				this.VariantType = VarEnum.VT_EMPTY;
				return;
			}
			if ((variantType & VarEnum.VT_ARRAY) != VarEnum.VT_EMPTY || variantType == VarEnum.VT_BSTR || variantType == VarEnum.VT_UNKNOWN || variantType == VarEnum.VT_DISPATCH || variantType == VarEnum.VT_VARIANT || variantType == VarEnum.VT_RECORD)
			{
				fixed (Variant* ptr = &this)
				{
					void* value = (void*)ptr;
					Interop.OleAut32.VariantClear((IntPtr)value);
				}
				return;
			}
			this.VariantType = VarEnum.VT_EMPTY;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x001787D1 File Offset: 0x001779D1
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x001787DE File Offset: 0x001779DE
		public VarEnum VariantType
		{
			get
			{
				return (VarEnum)this._typeUnion._vt;
			}
			set
			{
				this._typeUnion._vt = (ushort)value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x001787ED File Offset: 0x001779ED
		public bool IsEmpty
		{
			get
			{
				return this._typeUnion._vt == 0;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x001787FD File Offset: 0x001779FD
		public bool IsByRef
		{
			get
			{
				return (this._typeUnion._vt & 16384) > 0;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x00178813 File Offset: 0x00177A13
		public sbyte AsI1
		{
			get
			{
				return this._typeUnion._unionTypes._i1;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x00178825 File Offset: 0x00177A25
		public short AsI2
		{
			get
			{
				return this._typeUnion._unionTypes._i2;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x00178837 File Offset: 0x00177A37
		public int AsI4
		{
			get
			{
				return this._typeUnion._unionTypes._i4;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x00178849 File Offset: 0x00177A49
		public long AsI8
		{
			get
			{
				return this._typeUnion._unionTypes._i8;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x0017885B File Offset: 0x00177A5B
		public byte AsUi1
		{
			get
			{
				return this._typeUnion._unionTypes._ui1;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x0017886D File Offset: 0x00177A6D
		public ushort AsUi2
		{
			get
			{
				return this._typeUnion._unionTypes._ui2;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x0017887F File Offset: 0x00177A7F
		public uint AsUi4
		{
			get
			{
				return this._typeUnion._unionTypes._ui4;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x00178891 File Offset: 0x00177A91
		public ulong AsUi8
		{
			get
			{
				return this._typeUnion._unionTypes._ui8;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x001788A3 File Offset: 0x00177AA3
		public int AsInt
		{
			get
			{
				return this._typeUnion._unionTypes._int;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x001788B5 File Offset: 0x00177AB5
		public uint AsUint
		{
			get
			{
				return this._typeUnion._unionTypes._uint;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x001788C7 File Offset: 0x00177AC7
		public bool AsBool
		{
			get
			{
				return this._typeUnion._unionTypes._bool != 0;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x001788DC File Offset: 0x00177ADC
		public int AsError
		{
			get
			{
				return this._typeUnion._unionTypes._error;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x001788EE File Offset: 0x00177AEE
		public float AsR4
		{
			get
			{
				return this._typeUnion._unionTypes._r4;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x00178900 File Offset: 0x00177B00
		public double AsR8
		{
			get
			{
				return this._typeUnion._unionTypes._r8;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x00178914 File Offset: 0x00177B14
		public decimal AsDecimal
		{
			get
			{
				Variant variant = this;
				variant._typeUnion._vt = 0;
				return variant._decimal;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004424 RID: 17444 RVA: 0x0017893B File Offset: 0x00177B3B
		public decimal AsCy
		{
			get
			{
				return decimal.FromOACurrency(this._typeUnion._unionTypes._cy);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x00178952 File Offset: 0x00177B52
		public DateTime AsDate
		{
			get
			{
				return DateTime.FromOADate(this._typeUnion._unionTypes._date);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x00178969 File Offset: 0x00177B69
		public string AsBstr
		{
			get
			{
				return Marshal.PtrToStringBSTR(this._typeUnion._unionTypes._bstr);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x00178980 File Offset: 0x00177B80
		public object AsUnknown
		{
			get
			{
				if (this._typeUnion._unionTypes._unknown == IntPtr.Zero)
				{
					return null;
				}
				return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._unknown);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x001789B5 File Offset: 0x00177BB5
		public object AsDispatch
		{
			get
			{
				if (this._typeUnion._unionTypes._dispatch == IntPtr.Zero)
				{
					return null;
				}
				return Marshal.GetObjectForIUnknown(this._typeUnion._unionTypes._dispatch);
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x001789EA File Offset: 0x00177BEA
		public IntPtr AsByRefVariant
		{
			get
			{
				return this._typeUnion._unionTypes._pvarVal;
			}
		}

		// Token: 0x04000EDE RID: 3806
		[FieldOffset(0)]
		private Variant.TypeUnion _typeUnion;

		// Token: 0x04000EDF RID: 3807
		[FieldOffset(0)]
		private decimal _decimal;

		// Token: 0x0200045F RID: 1119
		private struct TypeUnion
		{
			// Token: 0x04000EE0 RID: 3808
			public ushort _vt;

			// Token: 0x04000EE1 RID: 3809
			public ushort _wReserved1;

			// Token: 0x04000EE2 RID: 3810
			public ushort _wReserved2;

			// Token: 0x04000EE3 RID: 3811
			public ushort _wReserved3;

			// Token: 0x04000EE4 RID: 3812
			public Variant.UnionTypes _unionTypes;
		}

		// Token: 0x02000460 RID: 1120
		private struct Record
		{
			// Token: 0x04000EE5 RID: 3813
			public IntPtr _record;

			// Token: 0x04000EE6 RID: 3814
			public IntPtr _recordInfo;
		}

		// Token: 0x02000461 RID: 1121
		[StructLayout(LayoutKind.Explicit)]
		private struct UnionTypes
		{
			// Token: 0x04000EE7 RID: 3815
			[FieldOffset(0)]
			public sbyte _i1;

			// Token: 0x04000EE8 RID: 3816
			[FieldOffset(0)]
			public short _i2;

			// Token: 0x04000EE9 RID: 3817
			[FieldOffset(0)]
			public int _i4;

			// Token: 0x04000EEA RID: 3818
			[FieldOffset(0)]
			public long _i8;

			// Token: 0x04000EEB RID: 3819
			[FieldOffset(0)]
			public byte _ui1;

			// Token: 0x04000EEC RID: 3820
			[FieldOffset(0)]
			public ushort _ui2;

			// Token: 0x04000EED RID: 3821
			[FieldOffset(0)]
			public uint _ui4;

			// Token: 0x04000EEE RID: 3822
			[FieldOffset(0)]
			public ulong _ui8;

			// Token: 0x04000EEF RID: 3823
			[FieldOffset(0)]
			public int _int;

			// Token: 0x04000EF0 RID: 3824
			[FieldOffset(0)]
			public uint _uint;

			// Token: 0x04000EF1 RID: 3825
			[FieldOffset(0)]
			public short _bool;

			// Token: 0x04000EF2 RID: 3826
			[FieldOffset(0)]
			public int _error;

			// Token: 0x04000EF3 RID: 3827
			[FieldOffset(0)]
			public float _r4;

			// Token: 0x04000EF4 RID: 3828
			[FieldOffset(0)]
			public double _r8;

			// Token: 0x04000EF5 RID: 3829
			[FieldOffset(0)]
			public long _cy;

			// Token: 0x04000EF6 RID: 3830
			[FieldOffset(0)]
			public double _date;

			// Token: 0x04000EF7 RID: 3831
			[FieldOffset(0)]
			public IntPtr _bstr;

			// Token: 0x04000EF8 RID: 3832
			[FieldOffset(0)]
			public IntPtr _unknown;

			// Token: 0x04000EF9 RID: 3833
			[FieldOffset(0)]
			public IntPtr _dispatch;

			// Token: 0x04000EFA RID: 3834
			[FieldOffset(0)]
			public IntPtr _pvarVal;

			// Token: 0x04000EFB RID: 3835
			[FieldOffset(0)]
			public IntPtr _byref;

			// Token: 0x04000EFC RID: 3836
			[FieldOffset(0)]
			public Variant.Record _record;
		}
	}
}
