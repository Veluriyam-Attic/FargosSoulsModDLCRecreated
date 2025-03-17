using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200009F RID: 159
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x000C43C0 File Offset: 0x000C35C0
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075:UnrecognizedReflectionPattern", Justification = "Trimmed fields don't make a difference for equality")]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Type type = base.GetType();
			Type type2 = obj.GetType();
			if (type2 != type)
			{
				return false;
			}
			if (ValueType.CanCompareBits(this))
			{
				return ValueType.FastEqualsCheck(this, obj);
			}
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				object value = fields[i].GetValue(this);
				object value2 = fields[i].GetValue(obj);
				if (value == null)
				{
					if (value2 != null)
					{
						return false;
					}
				}
				else if (!value.Equals(value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600086C RID: 2156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanCompareBits(object obj);

		// Token: 0x0600086D RID: 2157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FastEqualsCheck(object a, object b);

		// Token: 0x0600086E RID: 2158
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern int GetHashCode();

		// Token: 0x0600086F RID: 2159
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetHashCodeOfPtr(IntPtr ptr);

		// Token: 0x06000870 RID: 2160 RVA: 0x000B8635 File Offset: 0x000B7835
		public override string ToString()
		{
			return base.GetType().ToString();
		}
	}
}
