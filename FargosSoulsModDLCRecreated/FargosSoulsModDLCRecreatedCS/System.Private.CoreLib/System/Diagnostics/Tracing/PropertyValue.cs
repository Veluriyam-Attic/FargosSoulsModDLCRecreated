using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000769 RID: 1897
	internal readonly struct PropertyValue
	{
		// Token: 0x06005CC1 RID: 23745 RVA: 0x001C229D File Offset: 0x001C149D
		private PropertyValue(object value)
		{
			this._reference = value;
			this._scalar = default(PropertyValue.Scalar);
			this._scalarLength = 0;
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x001C22B9 File Offset: 0x001C14B9
		private PropertyValue(PropertyValue.Scalar scalar, int scalarLength)
		{
			this._reference = null;
			this._scalar = scalar;
			this._scalarLength = scalarLength;
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x001C22D0 File Offset: 0x001C14D0
		private PropertyValue(bool value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsBoolean = value
			}, 1);
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x001C22F8 File Offset: 0x001C14F8
		private PropertyValue(byte value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsByte = value
			}, 1);
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x001C2320 File Offset: 0x001C1520
		private PropertyValue(sbyte value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsSByte = value
			}, 1);
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x001C2348 File Offset: 0x001C1548
		private PropertyValue(char value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsChar = value
			}, 2);
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x001C2370 File Offset: 0x001C1570
		private PropertyValue(short value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsInt16 = value
			}, 2);
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x001C2398 File Offset: 0x001C1598
		private PropertyValue(ushort value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsUInt16 = value
			}, 2);
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x001C23C0 File Offset: 0x001C15C0
		private PropertyValue(int value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsInt32 = value
			}, 4);
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x001C23E8 File Offset: 0x001C15E8
		private PropertyValue(uint value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsUInt32 = value
			}, 4);
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x001C2410 File Offset: 0x001C1610
		private PropertyValue(long value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsInt64 = value
			}, 8);
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x001C2438 File Offset: 0x001C1638
		private PropertyValue(ulong value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsUInt64 = value
			}, 8);
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x001C2460 File Offset: 0x001C1660
		private PropertyValue(IntPtr value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsIntPtr = value
			}, sizeof(IntPtr));
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x001C248C File Offset: 0x001C168C
		private PropertyValue(UIntPtr value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsUIntPtr = value
			}, sizeof(UIntPtr));
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x001C24B8 File Offset: 0x001C16B8
		private PropertyValue(float value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsSingle = value
			}, 4);
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x001C24E0 File Offset: 0x001C16E0
		private PropertyValue(double value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsDouble = value
			}, 8);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x001C2508 File Offset: 0x001C1708
		private PropertyValue(Guid value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsGuid = value
			}, sizeof(Guid));
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x001C2534 File Offset: 0x001C1734
		private PropertyValue(DateTime value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsDateTime = value
			}, sizeof(DateTime));
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x001C2560 File Offset: 0x001C1760
		private PropertyValue(DateTimeOffset value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsDateTimeOffset = value
			}, sizeof(DateTimeOffset));
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x001C258C File Offset: 0x001C178C
		private PropertyValue(TimeSpan value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsTimeSpan = value
			}, sizeof(TimeSpan));
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x001C25B8 File Offset: 0x001C17B8
		private PropertyValue(decimal value)
		{
			this = new PropertyValue(new PropertyValue.Scalar
			{
				AsDecimal = value
			}, 16);
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x001C25E0 File Offset: 0x001C17E0
		public static Func<object, PropertyValue> GetFactory(Type type)
		{
			if (type == typeof(bool))
			{
				return (object value) => new PropertyValue((bool)value);
			}
			if (type == typeof(byte))
			{
				return (object value) => new PropertyValue((byte)value);
			}
			if (type == typeof(sbyte))
			{
				return (object value) => new PropertyValue((sbyte)value);
			}
			if (type == typeof(char))
			{
				return (object value) => new PropertyValue((char)value);
			}
			if (type == typeof(short))
			{
				return (object value) => new PropertyValue((short)value);
			}
			if (type == typeof(ushort))
			{
				return (object value) => new PropertyValue((ushort)value);
			}
			if (type == typeof(int))
			{
				return (object value) => new PropertyValue((int)value);
			}
			if (type == typeof(uint))
			{
				return (object value) => new PropertyValue((uint)value);
			}
			if (type == typeof(long))
			{
				return (object value) => new PropertyValue((long)value);
			}
			if (type == typeof(ulong))
			{
				return (object value) => new PropertyValue((ulong)value);
			}
			if (type == typeof(IntPtr))
			{
				return (object value) => new PropertyValue((IntPtr)value);
			}
			if (type == typeof(UIntPtr))
			{
				return (object value) => new PropertyValue((UIntPtr)value);
			}
			if (type == typeof(float))
			{
				return (object value) => new PropertyValue((float)value);
			}
			if (type == typeof(double))
			{
				return (object value) => new PropertyValue((double)value);
			}
			if (type == typeof(Guid))
			{
				return (object value) => new PropertyValue((Guid)value);
			}
			if (type == typeof(DateTime))
			{
				return (object value) => new PropertyValue((DateTime)value);
			}
			if (type == typeof(DateTimeOffset))
			{
				return (object value) => new PropertyValue((DateTimeOffset)value);
			}
			if (type == typeof(TimeSpan))
			{
				return (object value) => new PropertyValue((TimeSpan)value);
			}
			if (type == typeof(decimal))
			{
				return (object value) => new PropertyValue((decimal)value);
			}
			return (object value) => new PropertyValue(value);
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x001C29C2 File Offset: 0x001C1BC2
		public object ReferenceValue
		{
			get
			{
				return this._reference;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x001C29CA File Offset: 0x001C1BCA
		public PropertyValue.Scalar ScalarValue
		{
			get
			{
				return this._scalar;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005CD9 RID: 23769 RVA: 0x001C29D2 File Offset: 0x001C1BD2
		public int ScalarLength
		{
			get
			{
				return this._scalarLength;
			}
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x001C29DA File Offset: 0x001C1BDA
		public static Func<PropertyValue, PropertyValue> GetPropertyGetter(PropertyInfo property)
		{
			if (property.DeclaringType.GetTypeInfo().IsValueType)
			{
				return PropertyValue.GetBoxedValueTypePropertyGetter(property);
			}
			return PropertyValue.GetReferenceTypePropertyGetter(property);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x001C29FC File Offset: 0x001C1BFC
		private static Func<PropertyValue, PropertyValue> GetBoxedValueTypePropertyGetter(PropertyInfo property)
		{
			Type type = property.PropertyType;
			if (type.GetTypeInfo().IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			Func<object, PropertyValue> factory = PropertyValue.GetFactory(type);
			return (PropertyValue container) => factory(property.GetValue(container.ReferenceValue));
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x001C2A50 File Offset: 0x001C1C50
		private static Func<PropertyValue, PropertyValue> GetReferenceTypePropertyGetter(PropertyInfo property)
		{
			PropertyValue.TypeHelper typeHelper = (PropertyValue.TypeHelper)Activator.CreateInstance(typeof(PropertyValue.ReferenceTypeHelper<>).MakeGenericType(new Type[]
			{
				property.DeclaringType
			}));
			return typeHelper.GetPropertyGetter(property);
		}

		// Token: 0x04001BED RID: 7149
		private readonly object _reference;

		// Token: 0x04001BEE RID: 7150
		private readonly PropertyValue.Scalar _scalar;

		// Token: 0x04001BEF RID: 7151
		private readonly int _scalarLength;

		// Token: 0x0200076A RID: 1898
		[StructLayout(LayoutKind.Explicit)]
		public struct Scalar
		{
			// Token: 0x04001BF0 RID: 7152
			[FieldOffset(0)]
			public bool AsBoolean;

			// Token: 0x04001BF1 RID: 7153
			[FieldOffset(0)]
			public byte AsByte;

			// Token: 0x04001BF2 RID: 7154
			[FieldOffset(0)]
			public sbyte AsSByte;

			// Token: 0x04001BF3 RID: 7155
			[FieldOffset(0)]
			public char AsChar;

			// Token: 0x04001BF4 RID: 7156
			[FieldOffset(0)]
			public short AsInt16;

			// Token: 0x04001BF5 RID: 7157
			[FieldOffset(0)]
			public ushort AsUInt16;

			// Token: 0x04001BF6 RID: 7158
			[FieldOffset(0)]
			public int AsInt32;

			// Token: 0x04001BF7 RID: 7159
			[FieldOffset(0)]
			public uint AsUInt32;

			// Token: 0x04001BF8 RID: 7160
			[FieldOffset(0)]
			public long AsInt64;

			// Token: 0x04001BF9 RID: 7161
			[FieldOffset(0)]
			public ulong AsUInt64;

			// Token: 0x04001BFA RID: 7162
			[FieldOffset(0)]
			public IntPtr AsIntPtr;

			// Token: 0x04001BFB RID: 7163
			[FieldOffset(0)]
			public UIntPtr AsUIntPtr;

			// Token: 0x04001BFC RID: 7164
			[FieldOffset(0)]
			public float AsSingle;

			// Token: 0x04001BFD RID: 7165
			[FieldOffset(0)]
			public double AsDouble;

			// Token: 0x04001BFE RID: 7166
			[FieldOffset(0)]
			public Guid AsGuid;

			// Token: 0x04001BFF RID: 7167
			[FieldOffset(0)]
			public DateTime AsDateTime;

			// Token: 0x04001C00 RID: 7168
			[FieldOffset(0)]
			public DateTimeOffset AsDateTimeOffset;

			// Token: 0x04001C01 RID: 7169
			[FieldOffset(0)]
			public TimeSpan AsTimeSpan;

			// Token: 0x04001C02 RID: 7170
			[FieldOffset(0)]
			public decimal AsDecimal;
		}

		// Token: 0x0200076B RID: 1899
		private abstract class TypeHelper
		{
			// Token: 0x06005CDD RID: 23773
			public abstract Func<PropertyValue, PropertyValue> GetPropertyGetter(PropertyInfo property);

			// Token: 0x06005CDE RID: 23774 RVA: 0x001C2A8D File Offset: 0x001C1C8D
			protected Delegate GetGetMethod(PropertyInfo property, Type propertyType)
			{
				return property.GetMethod.CreateDelegate(typeof(Func<, >).MakeGenericType(new Type[]
				{
					property.DeclaringType,
					propertyType
				}));
			}
		}

		// Token: 0x0200076C RID: 1900
		private sealed class ReferenceTypeHelper<TContainer> : PropertyValue.TypeHelper where TContainer : class
		{
			// Token: 0x06005CE0 RID: 23776 RVA: 0x001C2ABC File Offset: 0x001C1CBC
			public override Func<PropertyValue, PropertyValue> GetPropertyGetter(PropertyInfo property)
			{
				Type type = property.PropertyType;
				if (!type.IsValueType)
				{
					Func<TContainer, object> getter = (Func<TContainer, object>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(getter((TContainer)((object)container.ReferenceValue)));
				}
				if (type.GetTypeInfo().IsEnum)
				{
					type = Enum.GetUnderlyingType(type);
				}
				if (type == typeof(bool))
				{
					Func<TContainer, bool> f = (Func<TContainer, bool>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(byte))
				{
					Func<TContainer, byte> f = (Func<TContainer, byte>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(sbyte))
				{
					Func<TContainer, sbyte> f = (Func<TContainer, sbyte>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(char))
				{
					Func<TContainer, char> f = (Func<TContainer, char>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(short))
				{
					Func<TContainer, short> f = (Func<TContainer, short>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(ushort))
				{
					Func<TContainer, ushort> f = (Func<TContainer, ushort>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(int))
				{
					Func<TContainer, int> f = (Func<TContainer, int>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(uint))
				{
					Func<TContainer, uint> f = (Func<TContainer, uint>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(long))
				{
					Func<TContainer, long> f = (Func<TContainer, long>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(ulong))
				{
					Func<TContainer, ulong> f = (Func<TContainer, ulong>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(IntPtr))
				{
					Func<TContainer, IntPtr> f = (Func<TContainer, IntPtr>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(UIntPtr))
				{
					Func<TContainer, UIntPtr> f = (Func<TContainer, UIntPtr>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(float))
				{
					Func<TContainer, float> f = (Func<TContainer, float>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(double))
				{
					Func<TContainer, double> f = (Func<TContainer, double>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(Guid))
				{
					Func<TContainer, Guid> f = (Func<TContainer, Guid>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(DateTime))
				{
					Func<TContainer, DateTime> f = (Func<TContainer, DateTime>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(DateTimeOffset))
				{
					Func<TContainer, DateTimeOffset> f = (Func<TContainer, DateTimeOffset>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(TimeSpan))
				{
					Func<TContainer, TimeSpan> f = (Func<TContainer, TimeSpan>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				if (type == typeof(decimal))
				{
					Func<TContainer, decimal> f = (Func<TContainer, decimal>)base.GetGetMethod(property, type);
					return (PropertyValue container) => new PropertyValue(f((TContainer)((object)container.ReferenceValue)));
				}
				return (PropertyValue container) => new PropertyValue(property.GetValue(container.ReferenceValue));
			}
		}
	}
}
