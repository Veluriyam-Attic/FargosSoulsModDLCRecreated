using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200064D RID: 1613
	internal class ModuleBuilderData
	{
		// Token: 0x060051A5 RID: 20901 RVA: 0x00196856 File Offset: 0x00195A56
		internal ModuleBuilderData(ModuleBuilder module, string moduleName)
		{
			this._globalTypeBuilder = new TypeBuilder(module);
			this._moduleName = moduleName;
		}

		// Token: 0x040014EC RID: 5356
		public readonly TypeBuilder _globalTypeBuilder;

		// Token: 0x040014ED RID: 5357
		public readonly string _moduleName;

		// Token: 0x040014EE RID: 5358
		public bool _hasGlobalBeenCreated;
	}
}
