using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000768 RID: 1896
	internal sealed class PropertyAnalysis
	{
		// Token: 0x06005CC0 RID: 23744 RVA: 0x001C226C File Offset: 0x001C146C
		public PropertyAnalysis(string name, PropertyInfo propertyInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
		{
			this.name = name;
			this.propertyInfo = propertyInfo;
			this.getter = PropertyValue.GetPropertyGetter(propertyInfo);
			this.typeInfo = typeInfo;
			this.fieldAttribute = fieldAttribute;
		}

		// Token: 0x04001BE8 RID: 7144
		internal readonly string name;

		// Token: 0x04001BE9 RID: 7145
		internal readonly PropertyInfo propertyInfo;

		// Token: 0x04001BEA RID: 7146
		internal readonly Func<PropertyValue, PropertyValue> getter;

		// Token: 0x04001BEB RID: 7147
		internal readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04001BEC RID: 7148
		internal readonly EventFieldAttribute fieldAttribute;
	}
}
