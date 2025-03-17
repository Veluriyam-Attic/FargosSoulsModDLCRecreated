using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005D7 RID: 1495
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class EventInfo : MemberInfo
	{
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x000CE630 File Offset: 0x000CD830
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004C3A RID: 19514
		public abstract EventAttributes Attributes { get; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x0018B9DA File Offset: 0x0018ABDA
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0018B9EB File Offset: 0x0018ABEB
		[NullableContext(1)]
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x000C2700 File Offset: 0x000C1900
		[NullableContext(1)]
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x0018B9F4 File Offset: 0x0018ABF4
		public virtual MethodInfo AddMethod
		{
			get
			{
				return this.GetAddMethod(true);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x0018B9FD File Offset: 0x0018ABFD
		public virtual MethodInfo RemoveMethod
		{
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x0018BA06 File Offset: 0x0018AC06
		public virtual MethodInfo RaiseMethod
		{
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0018BA0F File Offset: 0x0018AC0F
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0018BA18 File Offset: 0x0018AC18
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0018BA21 File Offset: 0x0018AC21
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		// Token: 0x06004C44 RID: 19524
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x06004C45 RID: 19525
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x06004C46 RID: 19526
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x0018BA2C File Offset: 0x0018AC2C
		public virtual bool IsMulticast
		{
			get
			{
				Type eventHandlerType = this.EventHandlerType;
				Type typeFromHandle = typeof(MulticastDelegate);
				return typeFromHandle.IsAssignableFrom(eventHandlerType);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x0018BA54 File Offset: 0x0018AC54
		public virtual Type EventHandlerType
		{
			get
			{
				MethodInfo addMethod = this.GetAddMethod(true);
				ParameterInfo[] parametersNoCopy = addMethod.GetParametersNoCopy();
				Type typeFromHandle = typeof(Delegate);
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					Type parameterType = parametersNoCopy[i].ParameterType;
					if (parameterType.IsSubclassOf(typeFromHandle))
					{
						return parameterType;
					}
				}
				return null;
			}
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0018BAA4 File Offset: 0x0018ACA4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			MethodInfo addMethod = this.GetAddMethod(false);
			if (addMethod == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NoPublicAddMethod);
			}
			addMethod.Invoke(target, new object[]
			{
				handler
			});
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0018BAE0 File Offset: 0x0018ACE0
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod(false);
			if (removeMethod == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NoPublicRemoveMethod);
			}
			removeMethod.Invoke(target, new object[]
			{
				handler
			});
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00185EF0 File Offset: 0x001850F0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x00185EF9 File Offset: 0x001850F9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0018222B File Offset: 0x0018142B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0018BB1B File Offset: 0x0018AD1B
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return !(left == right);
		}
	}
}
