using System;
using System.Diagnostics;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200044B RID: 1099
	internal static class DynamicInterfaceCastableHelpers
	{
		// Token: 0x060042C5 RID: 17093 RVA: 0x001753F0 File Offset: 0x001745F0
		[StackTraceHidden]
		internal static bool IsInterfaceImplemented(IDynamicInterfaceCastable castable, RuntimeType interfaceType, bool throwIfNotImplemented)
		{
			bool flag = castable.IsInterfaceImplemented(new RuntimeTypeHandle(interfaceType), throwIfNotImplemented);
			if (!flag && throwIfNotImplemented)
			{
				throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, castable.GetType(), interfaceType));
			}
			return flag;
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x0017542C File Offset: 0x0017462C
		[StackTraceHidden]
		internal static RuntimeType GetInterfaceImplementation(IDynamicInterfaceCastable castable, RuntimeType interfaceType)
		{
			RuntimeTypeHandle interfaceImplementation = castable.GetInterfaceImplementation(new RuntimeTypeHandle(interfaceType));
			if (interfaceImplementation.Equals(default(RuntimeTypeHandle)))
			{
				throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, castable.GetType(), interfaceType));
			}
			RuntimeType runtimeType = interfaceImplementation.GetRuntimeType();
			if (!runtimeType.IsInterface)
			{
				throw new InvalidOperationException(SR.Format(SR.IDynamicInterfaceCastable_NotInterface, runtimeType.ToString()));
			}
			if (!runtimeType.IsDefined(typeof(DynamicInterfaceCastableImplementationAttribute), false))
			{
				throw new InvalidOperationException(SR.Format(SR.IDynamicInterfaceCastable_MissingImplementationAttribute, runtimeType, "DynamicInterfaceCastableImplementationAttribute"));
			}
			if (!runtimeType.ImplementInterface(interfaceType))
			{
				throw new InvalidOperationException(SR.Format(SR.IDynamicInterfaceCastable_DoesNotImplementRequested, runtimeType, interfaceType));
			}
			return runtimeType;
		}
	}
}
