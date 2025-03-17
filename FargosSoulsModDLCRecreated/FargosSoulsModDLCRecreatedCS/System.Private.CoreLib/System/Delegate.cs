using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	[Nullable(0)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public abstract class Delegate : ICloneable, ISerializable
	{
		// Token: 0x060002AA RID: 682 RVA: 0x000B2E74 File Offset: 0x000B2074
		protected Delegate(object target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!this.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)10))
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000B2EC8 File Offset: 0x000B20C8
		protected Delegate(Type target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.ContainsGenericParameters)
			{
				throw new ArgumentException(SR.Arg_UnboundGenParam, "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = target as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "target");
			}
			this.BindToMethodName(null, runtimeType, method, (DelegateBindingFlags)37);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000B2F3C File Offset: 0x000B213C
		[NullableContext(2)]
		protected virtual object DynamicInvokeImpl(object[] args)
		{
			RuntimeMethodHandleInternal methodHandle = new RuntimeMethodHandleInternal(this.GetInvokeMethod());
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)RuntimeType.GetMethodBase((RuntimeType)base.GetType(), methodHandle);
			return runtimeMethodInfo.Invoke(this, BindingFlags.Default, null, args, null);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000B2F78 File Offset: 0x000B2178
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj == null || !Delegate.InternalEqualTypes(this, obj))
			{
				return false;
			}
			Delegate @delegate = (Delegate)obj;
			if (this._target == @delegate._target && this._methodPtr == @delegate._methodPtr && this._methodPtrAux == @delegate._methodPtrAux)
			{
				return true;
			}
			if (this._methodPtrAux == IntPtr.Zero)
			{
				if (@delegate._methodPtrAux != IntPtr.Zero)
				{
					return false;
				}
				if (this._target != @delegate._target)
				{
					return false;
				}
			}
			else
			{
				if (@delegate._methodPtrAux == IntPtr.Zero)
				{
					return false;
				}
				if (this._methodPtrAux == @delegate._methodPtrAux)
				{
					return true;
				}
			}
			if (this._methodBase == null || @delegate._methodBase == null || !(this._methodBase is MethodInfo) || !(@delegate._methodBase is MethodInfo))
			{
				return Delegate.InternalEqualMethodHandles(this, @delegate);
			}
			return this._methodBase.Equals(@delegate._methodBase);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000B3074 File Offset: 0x000B2274
		public override int GetHashCode()
		{
			if (this._methodPtrAux == IntPtr.Zero)
			{
				return ((this._target != null) ? (RuntimeHelpers.GetHashCode(this._target) * 33) : 0) + base.GetType().GetHashCode();
			}
			return base.GetType().GetHashCode();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000B30C4 File Offset: 0x000B22C4
		protected virtual MethodInfo GetMethodImpl()
		{
			if (this._methodBase == null || !(this._methodBase is MethodInfo))
			{
				IRuntimeMethodInfo runtimeMethodInfo = this.FindMethodHandle();
				RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if ((RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType)) && (RuntimeMethodHandle.GetAttributes(runtimeMethodInfo) & MethodAttributes.Static) <= MethodAttributes.PrivateScope)
				{
					if (this._methodPtrAux == IntPtr.Zero)
					{
						Type type = this._target.GetType();
						Type genericTypeDefinition = runtimeType.GetGenericTypeDefinition();
						while (type != null)
						{
							if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
							{
								runtimeType = (type as RuntimeType);
								break;
							}
							type = type.BaseType;
						}
					}
					else
					{
						MethodInfo method = base.GetType().GetMethod("Invoke");
						runtimeType = (RuntimeType)method.GetParameters()[0].ParameterType;
					}
				}
				this._methodBase = (MethodInfo)RuntimeType.GetMethodBase(runtimeType, runtimeMethodInfo);
			}
			return (MethodInfo)this._methodBase;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000B31BA File Offset: 0x000B23BA
		[Nullable(2)]
		public object Target
		{
			[NullableContext(2)]
			get
			{
				return this.GetTarget();
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000B31C4 File Offset: 0x000B23C4
		[RequiresUnreferencedCode("The target method might be removed")]
		[return: Nullable(2)]
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (@delegate.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)26 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				return @delegate;
			}
			if (throwOnBindFailure)
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return null;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000B326C File Offset: 0x000B246C
		[RequiresUnreferencedCode("The target method might be removed")]
		[return: Nullable(2)]
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.ContainsGenericParameters)
			{
				throw new ArgumentException(SR.Arg_UnboundGenParam, "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			RuntimeType runtimeType2 = target as RuntimeType;
			if (runtimeType2 == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "target");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (@delegate.BindToMethodName(null, runtimeType2, method, (DelegateBindingFlags)5 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				return @delegate;
			}
			if (throwOnBindFailure)
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return null;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000B3344 File Offset: 0x000B2544
		[return: Nullable(2)]
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "type");
			}
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, null, (DelegateBindingFlags)68);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return @delegate;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000B33E4 File Offset: 0x000B25E4
		[return: Nullable(2)]
		public static Delegate CreateDelegate(Type type, [Nullable(2)] object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeMethodInfo, "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "type");
			}
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, firstArgument, DelegateBindingFlags.RelaxedSignature);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return @delegate;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000B3484 File Offset: 0x000B2684
		internal static Delegate CreateDelegateNoSecurityCheck(Type type, object target, RuntimeMethodHandle method)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method.IsNullHandle())
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(SR.Arg_MustBeDelegate, "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodInfo(target, method.GetMethodInfo(), RuntimeMethodHandle.GetDeclaringType(method.GetMethodInfo()), DelegateBindingFlags.RelaxedSignature))
			{
				throw new ArgumentException(SR.Arg_DlgtTargMeth);
			}
			return @delegate;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000B351C File Offset: 0x000B271C
		internal static Delegate CreateDelegateInternal(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags)
		{
			Delegate @delegate = Delegate.InternalAlloc(rtType);
			if (@delegate.BindToMethodInfo(firstArgument, rtMethod, rtMethod.GetDeclaringTypeInternal(), flags))
			{
				return @delegate;
			}
			return null;
		}

		// Token: 0x060002B7 RID: 695
		[RequiresUnreferencedCode("The target method might be removed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodName(object target, RuntimeType methodType, string method, DelegateBindingFlags flags);

		// Token: 0x060002B8 RID: 696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodInfo(object target, IRuntimeMethodInfo method, RuntimeType methodType, DelegateBindingFlags flags);

		// Token: 0x060002B9 RID: 697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MulticastDelegate InternalAlloc(RuntimeType type);

		// Token: 0x060002BA RID: 698
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MulticastDelegate InternalAllocLike(Delegate d);

		// Token: 0x060002BB RID: 699
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualTypes(object a, object b);

		// Token: 0x060002BC RID: 700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DelegateConstruct(object target, IntPtr slot);

		// Token: 0x060002BD RID: 701
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMulticastInvoke();

		// Token: 0x060002BE RID: 702
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInvokeMethod();

		// Token: 0x060002BF RID: 703
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IRuntimeMethodInfo FindMethodHandle();

		// Token: 0x060002C0 RID: 704
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualMethodHandles(Delegate left, Delegate right);

		// Token: 0x060002C1 RID: 705
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr AdjustTarget(object target, IntPtr methodPtr);

		// Token: 0x060002C2 RID: 706
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetCallStub(IntPtr methodPtr);

		// Token: 0x060002C3 RID: 707 RVA: 0x000B3544 File Offset: 0x000B2744
		internal virtual object GetTarget()
		{
			if (!(this._methodPtrAux == IntPtr.Zero))
			{
				return null;
			}
			return this._target;
		}

		// Token: 0x060002C4 RID: 708
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareUnmanagedFunctionPtrs(Delegate d1, Delegate d2);

		// Token: 0x060002C5 RID: 709 RVA: 0x000AC0FA File Offset: 0x000AB2FA
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000B3560 File Offset: 0x000B2760
		[NullableContext(2)]
		[return: NotNullIfNotNull("a")]
		[return: NotNullIfNotNull("b")]
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			return a.CombineImpl(b);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000B3570 File Offset: 0x000B2770
		[NullableContext(2)]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null || delegates.Length == 0)
			{
				return null;
			}
			Delegate @delegate = delegates[0];
			for (int i = 1; i < delegates.Length; i++)
			{
				@delegate = Delegate.Combine(@delegate, delegates[i]);
			}
			return @delegate;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000B35A3 File Offset: 0x000B27A3
		public static Delegate CreateDelegate(Type type, [Nullable(2)] object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000B35AE File Offset: 0x000B27AE
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000B35B8 File Offset: 0x000B27B8
		[RequiresUnreferencedCode("The target method might be removed")]
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000B35C4 File Offset: 0x000B27C4
		[RequiresUnreferencedCode("The target method might be removed")]
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000B35D0 File Offset: 0x000B27D0
		[RequiresUnreferencedCode("The target method might be removed")]
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000B35DC File Offset: 0x000B27DC
		[RequiresUnreferencedCode("The target method might be removed")]
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000B35E8 File Offset: 0x000B27E8
		protected virtual Delegate CombineImpl([Nullable(2)] Delegate d)
		{
			throw new MulticastNotSupportedException(SR.Multicast_Combine);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000B35F4 File Offset: 0x000B27F4
		[return: Nullable(2)]
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (!d.Equals(this))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000B3602 File Offset: 0x000B2802
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[]
			{
				this
			};
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000B360E File Offset: 0x000B280E
		[NullableContext(2)]
		public object DynamicInvoke(params object[] args)
		{
			return this.DynamicInvokeImpl(args);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000B3617 File Offset: 0x000B2817
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000B361E File Offset: 0x000B281E
		public MethodInfo Method
		{
			get
			{
				return this.GetMethodImpl();
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000B3626 File Offset: 0x000B2826
		[NullableContext(2)]
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			if (!Delegate.InternalEqualTypes(source, value))
			{
				throw new ArgumentException(SR.Arg_DlgtTypeMis);
			}
			return source.RemoveImpl(value);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000B3650 File Offset: 0x000B2850
		[NullableContext(2)]
		public static Delegate RemoveAll(Delegate source, Delegate value)
		{
			Delegate @delegate;
			do
			{
				@delegate = source;
				source = Delegate.Remove(source, value);
			}
			while (@delegate != source);
			return @delegate;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000B3672 File Offset: 0x000B2872
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Delegate d1, Delegate d2)
		{
			if (d2 == null)
			{
				return d1 == null;
			}
			return d2 == d1 || d2.Equals(d1);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000B368B File Offset: 0x000B288B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Delegate d1, Delegate d2)
		{
			if (d2 == null)
			{
				return d1 != null;
			}
			return d2 != d1 && !d2.Equals(d1);
		}

		// Token: 0x04000105 RID: 261
		internal object _target;

		// Token: 0x04000106 RID: 262
		internal object _methodBase;

		// Token: 0x04000107 RID: 263
		internal IntPtr _methodPtr;

		// Token: 0x04000108 RID: 264
		internal IntPtr _methodPtrAux;
	}
}
