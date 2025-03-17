using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x02000612 RID: 1554
	internal sealed class InternalAssemblyBuilder : RuntimeAssembly
	{
		// Token: 0x06004E78 RID: 20088 RVA: 0x0018D49A File Offset: 0x0018C69A
		private InternalAssemblyBuilder()
		{
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0018D4A2 File Offset: 0x0018C6A2
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalAssemblyBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0018D4BD File Offset: 0x0018C6BD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override string[] GetManifestResourceNames()
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override FileStream GetFile(string name)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override Stream GetManifestResourceStream(string name)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override string Location
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		public override string CodeBase
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
			}
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0018D4C5 File Offset: 0x0018C6C5
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type[] GetExportedTypes()
		{
			throw new NotSupportedException(SR.NotSupported_DynamicAssembly);
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x0018D4D1 File Offset: 0x0018C6D1
		public override string ImageRuntimeVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().ImageRuntimeVersion;
			}
		}
	}
}
