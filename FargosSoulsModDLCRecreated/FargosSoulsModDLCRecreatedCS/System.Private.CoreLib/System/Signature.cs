using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000085 RID: 133
	internal class Signature
	{
		// Token: 0x06000586 RID: 1414
		[MemberNotNull("m_arguments")]
		[MemberNotNull("m_returnTypeORfieldType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void GetSignature(void* pCorSig, int cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType);

		// Token: 0x06000587 RID: 1415 RVA: 0x000B9740 File Offset: 0x000B8940
		public Signature(IRuntimeMethodInfo method, RuntimeType[] arguments, RuntimeType returnType, CallingConventions callingConvention)
		{
			this.m_pMethod = method.Value;
			this.m_arguments = arguments;
			this.m_returnTypeORfieldType = returnType;
			this.m_managedCallingConventionAndArgIteratorFlags = (int)((byte)callingConvention);
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), method, null);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000B978C File Offset: 0x000B898C
		public Signature(IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), methodHandle, declaringType);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000B97B3 File Offset: 0x000B89B3
		public Signature(IRuntimeFieldInfo fieldHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, fieldHandle.Value, null, declaringType);
			GC.KeepAlive(fieldHandle);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000B97D4 File Offset: 0x000B89D4
		public unsafe Signature(void* pCorSig, int cCorSig, RuntimeType declaringType)
		{
			this.GetSignature(pCorSig, cCorSig, default(RuntimeFieldHandleInternal), null, declaringType);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000B97FA File Offset: 0x000B89FA
		internal CallingConventions CallingConvention
		{
			get
			{
				return (CallingConventions)((byte)this.m_managedCallingConventionAndArgIteratorFlags);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000B9803 File Offset: 0x000B8A03
		internal RuntimeType[] Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000B980B File Offset: 0x000B8A0B
		internal RuntimeType ReturnType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000B980B File Offset: 0x000B8A0B
		internal RuntimeType FieldType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x0600058F RID: 1423
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareSig(Signature sig1, Signature sig2);

		// Token: 0x06000590 RID: 1424
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] GetCustomModifiers(int position, bool required);

		// Token: 0x040001A7 RID: 423
		internal RuntimeType[] m_arguments;

		// Token: 0x040001A8 RID: 424
		internal RuntimeType m_declaringType;

		// Token: 0x040001A9 RID: 425
		internal RuntimeType m_returnTypeORfieldType;

		// Token: 0x040001AA RID: 426
		internal object m_keepalive;

		// Token: 0x040001AB RID: 427
		internal unsafe void* m_sig;

		// Token: 0x040001AC RID: 428
		internal int m_managedCallingConventionAndArgIteratorFlags;

		// Token: 0x040001AD RID: 429
		internal int m_nSizeOfArgStack;

		// Token: 0x040001AE RID: 430
		internal int m_csig;

		// Token: 0x040001AF RID: 431
		internal RuntimeMethodHandleInternal m_pMethod;
	}
}
