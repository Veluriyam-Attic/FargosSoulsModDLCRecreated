using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005DF RID: 1503
	public static class IntrospectionExtensions
	{
		// Token: 0x06004C5B RID: 19547 RVA: 0x0018BBA8 File Offset: 0x0018ADA8
		[NullableContext(1)]
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = type as IReflectableType;
			if (reflectableType != null)
			{
				return reflectableType.GetTypeInfo();
			}
			return new TypeDelegator(type);
		}
	}
}
