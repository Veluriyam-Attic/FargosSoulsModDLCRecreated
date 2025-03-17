using System;
using System.Collections.Generic;

namespace System.Reflection.Emit
{
	// Token: 0x02000614 RID: 1556
	internal class AssemblyBuilderData
	{
		// Token: 0x06004EBD RID: 20157 RVA: 0x0018DC53 File Offset: 0x0018CE53
		internal AssemblyBuilderData(InternalAssemblyBuilder assembly, AssemblyBuilderAccess access)
		{
			this._assembly = assembly;
			this._access = access;
			this._moduleBuilderList = new List<ModuleBuilder>();
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0018DC74 File Offset: 0x0018CE74
		public void CheckTypeNameConflict(string strTypeName, TypeBuilder enclosingType)
		{
			for (int i = 0; i < this._moduleBuilderList.Count; i++)
			{
				ModuleBuilder moduleBuilder = this._moduleBuilderList[i];
				moduleBuilder.CheckTypeNameConflict(strTypeName, enclosingType);
			}
		}

		// Token: 0x04001416 RID: 5142
		public readonly List<ModuleBuilder> _moduleBuilderList;

		// Token: 0x04001417 RID: 5143
		public readonly AssemblyBuilderAccess _access;

		// Token: 0x04001418 RID: 5144
		public MethodInfo _entryPointMethod;

		// Token: 0x04001419 RID: 5145
		private readonly InternalAssemblyBuilder _assembly;
	}
}
