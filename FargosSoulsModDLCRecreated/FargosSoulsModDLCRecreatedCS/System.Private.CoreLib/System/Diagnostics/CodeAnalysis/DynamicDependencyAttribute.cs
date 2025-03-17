using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F0 RID: 1776
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public sealed class DynamicDependencyAttribute : Attribute
	{
		// Token: 0x06005936 RID: 22838 RVA: 0x001B1A87 File Offset: 0x001B0C87
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature)
		{
			this.MemberSignature = memberSignature;
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x001B1A96 File Offset: 0x001B0C96
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			this.MemberSignature = memberSignature;
			this.Type = type;
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x001B1AAC File Offset: 0x001B0CAC
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			this.MemberSignature = memberSignature;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x001B1AC9 File Offset: 0x001B0CC9
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			this.MemberTypes = memberTypes;
			this.Type = type;
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x001B1ADF File Offset: 0x001B0CDF
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			this.MemberTypes = memberTypes;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x001B1AFC File Offset: 0x001B0CFC
		public string MemberSignature { get; }

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x001B1B04 File Offset: 0x001B0D04
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600593D RID: 22845 RVA: 0x001B1B0C File Offset: 0x001B0D0C
		public Type Type { get; }

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600593E RID: 22846 RVA: 0x001B1B14 File Offset: 0x001B0D14
		public string TypeName { get; }

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600593F RID: 22847 RVA: 0x001B1B1C File Offset: 0x001B0D1C
		public string AssemblyName { get; }

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x001B1B24 File Offset: 0x001B0D24
		// (set) Token: 0x06005941 RID: 22849 RVA: 0x001B1B2C File Offset: 0x001B0D2C
		public string Condition { get; set; }
	}
}
