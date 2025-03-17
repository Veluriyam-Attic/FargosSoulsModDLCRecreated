using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005F9 RID: 1529
	[Nullable(0)]
	[NullableContext(1)]
	public abstract class ReflectionContext
	{
		// Token: 0x06004D1E RID: 19742
		public abstract Assembly MapAssembly(Assembly assembly);

		// Token: 0x06004D1F RID: 19743
		public abstract TypeInfo MapType(TypeInfo type);

		// Token: 0x06004D20 RID: 19744 RVA: 0x0018C304 File Offset: 0x0018B504
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
