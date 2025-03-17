using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000799 RID: 1945
	internal sealed class TypeAnalysis
	{
		// Token: 0x06005DB5 RID: 23989 RVA: 0x001C4DC4 File Offset: 0x001C3FC4
		public TypeAnalysis(Type dataType, EventDataAttribute eventAttrib, List<Type> recursionCheck)
		{
			List<PropertyAnalysis> list = new List<PropertyAnalysis>();
			foreach (PropertyInfo propertyInfo in dataType.GetProperties())
			{
				if (!Statics.HasCustomAttribute(propertyInfo, typeof(EventIgnoreAttribute)) && propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0)
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (!(getMethod == null) && !getMethod.IsStatic && getMethod.IsPublic)
					{
						Type propertyType = propertyInfo.PropertyType;
						TraceLoggingTypeInfo instance = TraceLoggingTypeInfo.GetInstance(propertyType, recursionCheck);
						EventFieldAttribute customAttribute = Statics.GetCustomAttribute<EventFieldAttribute>(propertyInfo);
						string text = (customAttribute != null && customAttribute.Name != null) ? customAttribute.Name : (Statics.ShouldOverrideFieldName(propertyInfo.Name) ? instance.Name : propertyInfo.Name);
						list.Add(new PropertyAnalysis(text, propertyInfo, instance, customAttribute));
					}
				}
			}
			this.properties = list.ToArray();
			foreach (PropertyAnalysis propertyAnalysis in this.properties)
			{
				TraceLoggingTypeInfo typeInfo = propertyAnalysis.typeInfo;
				this.level = (EventLevel)Statics.Combine((int)typeInfo.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)typeInfo.Opcode, (int)this.opcode);
				this.keywords |= typeInfo.Keywords;
				this.tags |= typeInfo.Tags;
			}
			if (eventAttrib != null)
			{
				this.level = (EventLevel)Statics.Combine((int)eventAttrib.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)eventAttrib.Opcode, (int)this.opcode);
				this.keywords |= eventAttrib.Keywords;
				this.tags |= eventAttrib.Tags;
				this.name = eventAttrib.Name;
			}
			if (this.name == null)
			{
				this.name = dataType.Name;
			}
		}

		// Token: 0x04001C86 RID: 7302
		internal readonly PropertyAnalysis[] properties;

		// Token: 0x04001C87 RID: 7303
		internal readonly string name;

		// Token: 0x04001C88 RID: 7304
		internal readonly EventKeywords keywords;

		// Token: 0x04001C89 RID: 7305
		internal readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x04001C8A RID: 7306
		internal readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x04001C8B RID: 7307
		internal readonly EventTags tags;
	}
}
