using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x0200023E RID: 574
	[AttributeUsage(AttributeTargets.All)]
	[Nullable(0)]
	[NullableContext(2)]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x001388D8 File Offset: 0x00137AD8
		[DynamicDependency("ConvertFromInvariantString", "System.ComponentModel.TypeConverter", "System.ComponentModel.TypeConverter")]
		[NullableContext(1)]
		public DefaultValueAttribute(Type type, [Nullable(2)] string value)
		{
			if (type == null)
			{
				return;
			}
			try
			{
				object value2;
				if (DefaultValueAttribute.<.ctor>g__TryConvertFromInvariantString|2_0(type, value, out value2))
				{
					this._value = value2;
				}
				else if (type.IsSubclassOf(typeof(Enum)) && value != null)
				{
					this._value = Enum.Parse(type, value, true);
				}
				else if (type == typeof(TimeSpan) && value != null)
				{
					this._value = TimeSpan.Parse(value);
				}
				else
				{
					this._value = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00138980 File Offset: 0x00137B80
		public DefaultValueAttribute(char value)
		{
			this._value = value;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00138994 File Offset: 0x00137B94
		public DefaultValueAttribute(byte value)
		{
			this._value = value;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x001389A8 File Offset: 0x00137BA8
		public DefaultValueAttribute(short value)
		{
			this._value = value;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x001389BC File Offset: 0x00137BBC
		public DefaultValueAttribute(int value)
		{
			this._value = value;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x001389D0 File Offset: 0x00137BD0
		public DefaultValueAttribute(long value)
		{
			this._value = value;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x001389E4 File Offset: 0x00137BE4
		public DefaultValueAttribute(float value)
		{
			this._value = value;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x001389F8 File Offset: 0x00137BF8
		public DefaultValueAttribute(double value)
		{
			this._value = value;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00138A0C File Offset: 0x00137C0C
		public DefaultValueAttribute(bool value)
		{
			this._value = value;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00138A20 File Offset: 0x00137C20
		public DefaultValueAttribute(string value)
		{
			this._value = value;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00138A20 File Offset: 0x00137C20
		public DefaultValueAttribute(object value)
		{
			this._value = value;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00138A2F File Offset: 0x00137C2F
		[CLSCompliant(false)]
		public DefaultValueAttribute(sbyte value)
		{
			this._value = value;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00138A43 File Offset: 0x00137C43
		[CLSCompliant(false)]
		public DefaultValueAttribute(ushort value)
		{
			this._value = value;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00138A57 File Offset: 0x00137C57
		[CLSCompliant(false)]
		public DefaultValueAttribute(uint value)
		{
			this._value = value;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00138A6B File Offset: 0x00137C6B
		[CLSCompliant(false)]
		public DefaultValueAttribute(ulong value)
		{
			this._value = value;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x00138A7F File Offset: 0x00137C7F
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00138A88 File Offset: 0x00137C88
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.Value.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00138ACA File Offset: 0x00137CCA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00138AD2 File Offset: 0x00137CD2
		protected void SetValue(object value)
		{
			this._value = value;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00138ADC File Offset: 0x00137CDC
		[CompilerGenerated]
		internal static bool <.ctor>g__TryConvertFromInvariantString|2_0(Type typeToConvert, string stringValue, out object conversionResult)
		{
			conversionResult = null;
			if (DefaultValueAttribute.s_convertFromInvariantString == null)
			{
				Type type = Type.GetType("System.ComponentModel.TypeDescriptor, System.ComponentModel.TypeConverter", false);
				MethodInfo methodInfo = (type != null) ? type.GetMethod("ConvertFromInvariantString", BindingFlags.Static | BindingFlags.NonPublic) : null;
				Volatile.Write<object>(ref DefaultValueAttribute.s_convertFromInvariantString, (methodInfo == null) ? new object() : methodInfo.CreateDelegate(typeof(Func<Type, string, object>)));
			}
			Func<Type, string, object> func = DefaultValueAttribute.s_convertFromInvariantString as Func<Type, string, object>;
			if (func == null)
			{
				return false;
			}
			try
			{
				conversionResult = func(typeToConvert, stringValue);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000963 RID: 2403
		private object _value;

		// Token: 0x04000964 RID: 2404
		private static object s_convertFromInvariantString;
	}
}
