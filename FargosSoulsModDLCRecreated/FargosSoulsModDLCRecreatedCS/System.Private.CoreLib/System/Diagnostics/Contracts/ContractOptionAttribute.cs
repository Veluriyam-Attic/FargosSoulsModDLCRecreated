using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006EA RID: 1770
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Nullable(0)]
	[NullableContext(1)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractOptionAttribute : Attribute
	{
		// Token: 0x06005910 RID: 22800 RVA: 0x001B1704 File Offset: 0x001B0904
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x001B1721 File Offset: 0x001B0921
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x001B173E File Offset: 0x001B093E
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x001B1746 File Offset: 0x001B0946
		public string Setting
		{
			get
			{
				return this._setting;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x001B174E File Offset: 0x001B094E
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x001B1756 File Offset: 0x001B0956
		[Nullable(2)]
		public string Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400199A RID: 6554
		private readonly string _category;

		// Token: 0x0400199B RID: 6555
		private readonly string _setting;

		// Token: 0x0400199C RID: 6556
		private readonly bool _enabled;

		// Token: 0x0400199D RID: 6557
		private readonly string _value;
	}
}
