using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000159 RID: 345
	public static class Nullable
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x000DF044 File Offset: 0x000DE244
		public static int Compare<T>(T? n1, T? n2) where T : struct
		{
			if (n1 != null)
			{
				if (n2 != null)
				{
					return Comparer<T>.Default.Compare(n1.value, n2.value);
				}
				return 1;
			}
			else
			{
				if (n2 != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000DF07D File Offset: 0x000DE27D
		public static bool Equals<T>(T? n1, T? n2) where T : struct
		{
			if (n1 != null)
			{
				return n2 != null && EqualityComparer<T>.Default.Equals(n1.value, n2.value);
			}
			return n2 == null;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000DF0B8 File Offset: 0x000DE2B8
		[NullableContext(1)]
		[return: Nullable(2)]
		public static Type GetUnderlyingType(Type nullableType)
		{
			if (nullableType == null)
			{
				throw new ArgumentNullException("nullableType");
			}
			if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition)
			{
				Type genericTypeDefinition = nullableType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(Nullable<>))
				{
					return nullableType.GetGenericArguments()[0];
				}
			}
			return null;
		}
	}
}
