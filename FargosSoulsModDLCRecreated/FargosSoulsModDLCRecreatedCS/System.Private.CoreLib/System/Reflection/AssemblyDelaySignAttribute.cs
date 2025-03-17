using System;

namespace System.Reflection
{
	// Token: 0x020005BE RID: 1470
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		// Token: 0x06004BD6 RID: 19414 RVA: 0x0018B145 File Offset: 0x0018A345
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.DelaySign = delaySign;
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x0018B154 File Offset: 0x0018A354
		public bool DelaySign { get; }
	}
}
