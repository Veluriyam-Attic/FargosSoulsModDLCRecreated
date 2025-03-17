using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058F RID: 1423
	[StructLayout(LayoutKind.Auto)]
	internal readonly struct CustomAttributeEncodedArgument
	{
		// Token: 0x06004940 RID: 18752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParseAttributeArguments(IntPtr pCa, int cCa, ref CustomAttributeCtorParameter[] CustomAttributeCtorParameters, ref CustomAttributeNamedParameter[] CustomAttributeTypedArgument, RuntimeAssembly assembly);

		// Token: 0x06004941 RID: 18753 RVA: 0x001845E2 File Offset: 0x001837E2
		internal static void ParseAttributeArguments(ConstArray attributeBlob, ref CustomAttributeCtorParameter[] customAttributeCtorParameters, ref CustomAttributeNamedParameter[] customAttributeNamedParameters, RuntimeModule customAttributeModule)
		{
			if (customAttributeModule == null)
			{
				throw new ArgumentNullException("customAttributeModule");
			}
			if (customAttributeCtorParameters.Length != 0 || customAttributeNamedParameters.Length != 0)
			{
				CustomAttributeEncodedArgument.ParseAttributeArguments(attributeBlob.Signature, attributeBlob.Length, ref customAttributeCtorParameters, ref customAttributeNamedParameters, (RuntimeAssembly)customAttributeModule.Assembly);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x00184622 File Offset: 0x00183822
		public CustomAttributeType CustomAttributeType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0018462A File Offset: 0x0018382A
		public long PrimitiveValue
		{
			get
			{
				return this.m_primitiveValue;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x00184632 File Offset: 0x00183832
		public CustomAttributeEncodedArgument[] ArrayValue
		{
			get
			{
				return this.m_arrayValue;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0018463A File Offset: 0x0018383A
		public string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
		}

		// Token: 0x040011F4 RID: 4596
		private readonly long m_primitiveValue;

		// Token: 0x040011F5 RID: 4597
		private readonly CustomAttributeEncodedArgument[] m_arrayValue;

		// Token: 0x040011F6 RID: 4598
		private readonly string m_stringValue;

		// Token: 0x040011F7 RID: 4599
		private readonly CustomAttributeType m_type;
	}
}
