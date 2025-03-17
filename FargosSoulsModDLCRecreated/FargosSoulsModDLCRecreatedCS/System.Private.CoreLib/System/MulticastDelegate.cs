using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000077 RID: 119
	[ClassInterface(ClassInterfaceType.None)]
	[Nullable(0)]
	[NullableContext(1)]
	[ComVisible(true)]
	public abstract class MulticastDelegate : Delegate
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x000B7CDC File Offset: 0x000B6EDC
		protected MulticastDelegate(object target, string method) : base(target, method)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000B7CE6 File Offset: 0x000B6EE6
		protected MulticastDelegate(Type target, string method) : base(target, method)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000B7CF0 File Offset: 0x000B6EF0
		internal bool IsUnmanagedFunctionPtr()
		{
			return this._invocationCount == (IntPtr)(-1);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000B7D03 File Offset: 0x000B6F03
		internal bool InvocationListLogicallyNull()
		{
			return this._invocationList == null || this._invocationList is LoaderAllocator || this._invocationList is DynamicResolver;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000B7D2A File Offset: 0x000B6F2A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new SerializationException(SR.Serialization_DelegatesNotSupported);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000B7D38 File Offset: 0x000B6F38
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			if (!Delegate.InternalEqualTypes(this, obj))
			{
				return false;
			}
			MulticastDelegate multicastDelegate = Unsafe.As<MulticastDelegate>(obj);
			if (this._invocationCount != (IntPtr)0)
			{
				if (this.InvocationListLogicallyNull())
				{
					if (this.IsUnmanagedFunctionPtr())
					{
						return multicastDelegate.IsUnmanagedFunctionPtr() && Delegate.CompareUnmanagedFunctionPtrs(this, multicastDelegate);
					}
					if (multicastDelegate._invocationList is Delegate)
					{
						return this.Equals(multicastDelegate._invocationList);
					}
					return base.Equals(obj);
				}
				else
				{
					Delegate @delegate = this._invocationList as Delegate;
					if (@delegate != null)
					{
						return @delegate.Equals(obj);
					}
					return this.InvocationListEquals(multicastDelegate);
				}
			}
			else
			{
				if (!this.InvocationListLogicallyNull())
				{
					return this._invocationList.Equals(multicastDelegate._invocationList) && base.Equals(multicastDelegate);
				}
				if (multicastDelegate._invocationList is Delegate)
				{
					return this.Equals(multicastDelegate._invocationList);
				}
				return base.Equals(multicastDelegate);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000B7E20 File Offset: 0x000B7020
		private bool InvocationListEquals(MulticastDelegate d)
		{
			object[] array = (object[])this._invocationList;
			if (d._invocationCount != this._invocationCount)
			{
				return false;
			}
			int num = (int)this._invocationCount;
			for (int i = 0; i < num; i++)
			{
				Delegate @delegate = (Delegate)array[i];
				object[] array2 = d._invocationList as object[];
				if (!@delegate.Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000B7E8C File Offset: 0x000B708C
		private bool TrySetSlot(object[] a, int index, object o)
		{
			if (a[index] == null && Interlocked.CompareExchange<object>(ref a[index], o, null) == null)
			{
				return true;
			}
			if (a[index] != null)
			{
				MulticastDelegate multicastDelegate = (MulticastDelegate)o;
				MulticastDelegate multicastDelegate2 = (MulticastDelegate)a[index];
				if (multicastDelegate2._methodPtr == multicastDelegate._methodPtr && multicastDelegate2._target == multicastDelegate._target && multicastDelegate2._methodPtrAux == multicastDelegate._methodPtrAux)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000B7EFC File Offset: 0x000B70FC
		private MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount, bool thisIsMultiCastAlready)
		{
			MulticastDelegate multicastDelegate = Delegate.InternalAllocLike(this);
			if (thisIsMultiCastAlready)
			{
				multicastDelegate._methodPtr = this._methodPtr;
				multicastDelegate._methodPtrAux = this._methodPtrAux;
			}
			else
			{
				multicastDelegate._methodPtr = base.GetMulticastInvoke();
				multicastDelegate._methodPtrAux = base.GetInvokeMethod();
			}
			multicastDelegate._target = multicastDelegate;
			multicastDelegate._invocationList = invocationList;
			multicastDelegate._invocationCount = (IntPtr)invocationCount;
			return multicastDelegate;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000B7F60 File Offset: 0x000B7160
		internal MulticastDelegate NewMulticastDelegate(object[] invocationList, int invocationCount)
		{
			return this.NewMulticastDelegate(invocationList, invocationCount, false);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000B7F6C File Offset: 0x000B716C
		internal void StoreDynamicMethod(MethodInfo dynamicMethod)
		{
			if (this._invocationCount != (IntPtr)0)
			{
				MulticastDelegate multicastDelegate = (MulticastDelegate)this._invocationList;
				multicastDelegate._methodBase = dynamicMethod;
				return;
			}
			this._methodBase = dynamicMethod;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000B7FA8 File Offset: 0x000B71A8
		protected sealed override Delegate CombineImpl([Nullable(2)] Delegate follow)
		{
			if (follow == null)
			{
				return this;
			}
			if (!Delegate.InternalEqualTypes(this, follow))
			{
				throw new ArgumentException(SR.Arg_DlgtTypeMis);
			}
			MulticastDelegate multicastDelegate = (MulticastDelegate)follow;
			int num = 1;
			object[] array = multicastDelegate._invocationList as object[];
			if (array != null)
			{
				num = (int)multicastDelegate._invocationCount;
			}
			object[] array2 = this._invocationList as object[];
			int num2;
			object[] array3;
			if (array2 == null)
			{
				num2 = 1 + num;
				array3 = new object[num2];
				array3[0] = this;
				if (array == null)
				{
					array3[1] = multicastDelegate;
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						array3[1 + i] = array[i];
					}
				}
				return this.NewMulticastDelegate(array3, num2);
			}
			int num3 = (int)this._invocationCount;
			num2 = num3 + num;
			array3 = null;
			if (num2 <= array2.Length)
			{
				array3 = array2;
				if (array == null)
				{
					if (!this.TrySetSlot(array3, num3, multicastDelegate))
					{
						array3 = null;
					}
				}
				else
				{
					for (int j = 0; j < num; j++)
					{
						if (!this.TrySetSlot(array3, num3 + j, array[j]))
						{
							array3 = null;
							break;
						}
					}
				}
			}
			if (array3 == null)
			{
				int k;
				for (k = array2.Length; k < num2; k *= 2)
				{
				}
				array3 = new object[k];
				for (int l = 0; l < num3; l++)
				{
					array3[l] = array2[l];
				}
				if (array == null)
				{
					array3[num3] = multicastDelegate;
				}
				else
				{
					for (int m = 0; m < num; m++)
					{
						array3[num3 + m] = array[m];
					}
				}
			}
			return this.NewMulticastDelegate(array3, num2, true);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000B8104 File Offset: 0x000B7304
		private object[] DeleteFromInvocationList(object[] invocationList, int invocationCount, int deleteIndex, int deleteCount)
		{
			object[] array = (object[])this._invocationList;
			int num = array.Length;
			while (num / 2 >= invocationCount - deleteCount)
			{
				num /= 2;
			}
			object[] array2 = new object[num];
			for (int i = 0; i < deleteIndex; i++)
			{
				array2[i] = invocationList[i];
			}
			for (int j = deleteIndex + deleteCount; j < invocationCount; j++)
			{
				array2[j - deleteCount] = invocationList[j];
			}
			return array2;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000B8168 File Offset: 0x000B7368
		private bool EqualInvocationLists(object[] a, object[] b, int start, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (!a[start + i].Equals(b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000B8194 File Offset: 0x000B7394
		[return: Nullable(2)]
		protected sealed override Delegate RemoveImpl(Delegate value)
		{
			MulticastDelegate multicastDelegate = value as MulticastDelegate;
			if (multicastDelegate == null)
			{
				return this;
			}
			if (!(multicastDelegate._invocationList is object[]))
			{
				object[] array = this._invocationList as object[];
				if (array == null)
				{
					if (this.Equals(value))
					{
						return null;
					}
				}
				else
				{
					int num = (int)this._invocationCount;
					int num2 = num;
					while (--num2 >= 0)
					{
						if (value.Equals(array[num2]))
						{
							if (num == 2)
							{
								return (Delegate)array[1 - num2];
							}
							object[] invocationList = this.DeleteFromInvocationList(array, num, num2, 1);
							return this.NewMulticastDelegate(invocationList, num - 1, true);
						}
					}
				}
			}
			else
			{
				object[] array2 = this._invocationList as object[];
				if (array2 != null)
				{
					int num3 = (int)this._invocationCount;
					int num4 = (int)multicastDelegate._invocationCount;
					int i = num3 - num4;
					while (i >= 0)
					{
						if (this.EqualInvocationLists(array2, multicastDelegate._invocationList as object[], i, num4))
						{
							if (num3 - num4 == 0)
							{
								return null;
							}
							if (num3 - num4 == 1)
							{
								return (Delegate)array2[(i != 0) ? 0 : (num3 - 1)];
							}
							object[] invocationList2 = this.DeleteFromInvocationList(array2, num3, i, num4);
							return this.NewMulticastDelegate(invocationList2, num3 - num4, true);
						}
						else
						{
							i--;
						}
					}
				}
			}
			return this;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000B82C8 File Offset: 0x000B74C8
		public sealed override Delegate[] GetInvocationList()
		{
			object[] array = this._invocationList as object[];
			Delegate[] array2;
			if (array == null)
			{
				array2 = new Delegate[]
				{
					this
				};
			}
			else
			{
				array2 = new Delegate[(int)this._invocationCount];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (Delegate)array[i];
				}
			}
			return array2;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000B3672 File Offset: 0x000B2872
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d2 == null)
			{
				return d1 == null;
			}
			return d2 == d1 || d2.Equals(d1);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000B368B File Offset: 0x000B288B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(MulticastDelegate d1, MulticastDelegate d2)
		{
			if (d2 == null)
			{
				return d1 != null;
			}
			return d2 != d1 && !d2.Equals(d1);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000B831C File Offset: 0x000B751C
		public sealed override int GetHashCode()
		{
			if (this.IsUnmanagedFunctionPtr())
			{
				return ValueType.GetHashCodeOfPtr(this._methodPtr) ^ ValueType.GetHashCodeOfPtr(this._methodPtrAux);
			}
			if (this._invocationCount != (IntPtr)0)
			{
				Delegate @delegate = this._invocationList as Delegate;
				if (@delegate != null)
				{
					return @delegate.GetHashCode();
				}
			}
			object[] array = this._invocationList as object[];
			if (array == null)
			{
				return base.GetHashCode();
			}
			int num = 0;
			for (int i = 0; i < (int)this._invocationCount; i++)
			{
				num = num * 33 + array[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000B83B0 File Offset: 0x000B75B0
		internal override object GetTarget()
		{
			if (this._invocationCount != (IntPtr)0)
			{
				if (this.InvocationListLogicallyNull())
				{
					return null;
				}
				object[] array = this._invocationList as object[];
				if (array != null)
				{
					int num = (int)this._invocationCount;
					return ((Delegate)array[num - 1]).GetTarget();
				}
				Delegate @delegate = this._invocationList as Delegate;
				if (@delegate != null)
				{
					return @delegate.GetTarget();
				}
			}
			return base.GetTarget();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000B8424 File Offset: 0x000B7624
		protected override MethodInfo GetMethodImpl()
		{
			if (this._invocationCount != (IntPtr)0 && this._invocationList != null)
			{
				object[] array = this._invocationList as object[];
				if (array != null)
				{
					int num = (int)this._invocationCount - 1;
					return ((Delegate)array[num]).Method;
				}
				MulticastDelegate multicastDelegate = this._invocationList as MulticastDelegate;
				if (multicastDelegate != null)
				{
					return multicastDelegate.GetMethodImpl();
				}
			}
			else if (this.IsUnmanagedFunctionPtr())
			{
				if (this._methodBase == null || !(this._methodBase is MethodInfo))
				{
					IRuntimeMethodInfo runtimeMethodInfo = base.FindMethodHandle();
					RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
					if (RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType))
					{
						RuntimeType runtimeType2 = (RuntimeType)base.GetType();
						runtimeType = runtimeType2;
					}
					this._methodBase = (MethodInfo)RuntimeType.GetMethodBase(runtimeType, runtimeMethodInfo);
				}
				return (MethodInfo)this._methodBase;
			}
			return base.GetMethodImpl();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000B8501 File Offset: 0x000B7701
		[DoesNotReturn]
		[DebuggerNonUserCode]
		private static void ThrowNullThisInDelegateToInstance()
		{
			throw new ArgumentException(SR.Arg_DlgtNullInst);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000B850D File Offset: 0x000B770D
		[DebuggerNonUserCode]
		private void CtorClosed(object target, IntPtr methodPtr)
		{
			if (target == null)
			{
				MulticastDelegate.ThrowNullThisInDelegateToInstance();
			}
			this._target = target;
			this._methodPtr = methodPtr;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000B8525 File Offset: 0x000B7725
		[DebuggerNonUserCode]
		private void CtorClosedStatic(object target, IntPtr methodPtr)
		{
			this._target = target;
			this._methodPtr = methodPtr;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000B8535 File Offset: 0x000B7735
		[DebuggerNonUserCode]
		private void CtorRTClosed(object target, IntPtr methodPtr)
		{
			this._target = target;
			this._methodPtr = base.AdjustTarget(target, methodPtr);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000B854C File Offset: 0x000B774C
		[DebuggerNonUserCode]
		private void CtorOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk)
		{
			this._target = this;
			this._methodPtr = shuffleThunk;
			this._methodPtrAux = methodPtr;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000B8563 File Offset: 0x000B7763
		[DebuggerNonUserCode]
		private void CtorVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk)
		{
			this._target = this;
			this._methodPtr = shuffleThunk;
			this._methodPtrAux = base.GetCallStub(methodPtr);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000B8580 File Offset: 0x000B7780
		[DebuggerNonUserCode]
		private void CtorCollectibleClosedStatic(object target, IntPtr methodPtr, IntPtr gchandle)
		{
			this._target = target;
			this._methodPtr = methodPtr;
			this._methodBase = GCHandle.InternalGet(gchandle);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000B859C File Offset: 0x000B779C
		[DebuggerNonUserCode]
		private void CtorCollectibleOpened(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
		{
			this._target = this;
			this._methodPtr = shuffleThunk;
			this._methodPtrAux = methodPtr;
			this._methodBase = GCHandle.InternalGet(gchandle);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000B85C0 File Offset: 0x000B77C0
		[DebuggerNonUserCode]
		private void CtorCollectibleVirtualDispatch(object target, IntPtr methodPtr, IntPtr shuffleThunk, IntPtr gchandle)
		{
			this._target = this;
			this._methodPtr = shuffleThunk;
			this._methodPtrAux = base.GetCallStub(methodPtr);
			this._methodBase = GCHandle.InternalGet(gchandle);
		}

		// Token: 0x0400018C RID: 396
		private object _invocationList;

		// Token: 0x0400018D RID: 397
		private IntPtr _invocationCount;
	}
}
