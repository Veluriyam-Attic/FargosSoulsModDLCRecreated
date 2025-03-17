using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System
{
	// Token: 0x02000141 RID: 321
	internal class LazyHelper
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x000DB12A File Offset: 0x000DA32A
		internal LazyState State { get; }

		// Token: 0x06001032 RID: 4146 RVA: 0x000DB132 File Offset: 0x000DA332
		internal LazyHelper(LazyState state)
		{
			this.State = state;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000DB144 File Offset: 0x000DA344
		internal LazyHelper(LazyThreadSafetyMode mode, Exception exception)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				this.State = 2;
				break;
			case LazyThreadSafetyMode.PublicationOnly:
				this.State = 6;
				break;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				this.State = 9;
				break;
			}
			this._exceptionDispatch = ExceptionDispatchInfo.Capture(exception);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000DB191 File Offset: 0x000DA391
		[DoesNotReturn]
		internal void ThrowException()
		{
			this._exceptionDispatch.Throw();
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000DB1A0 File Offset: 0x000DA3A0
		private LazyThreadSafetyMode GetMode()
		{
			switch (this.State)
			{
			case LazyState.NoneViaConstructor:
			case LazyState.NoneViaFactory:
			case LazyState.NoneException:
				return LazyThreadSafetyMode.None;
			case LazyState.PublicationOnlyViaConstructor:
			case LazyState.PublicationOnlyViaFactory:
			case LazyState.PublicationOnlyWait:
			case LazyState.PublicationOnlyException:
				return LazyThreadSafetyMode.PublicationOnly;
			case LazyState.ExecutionAndPublicationViaConstructor:
			case LazyState.ExecutionAndPublicationViaFactory:
			case LazyState.ExecutionAndPublicationException:
				return LazyThreadSafetyMode.ExecutionAndPublication;
			default:
				return LazyThreadSafetyMode.None;
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000DB1EC File Offset: 0x000DA3EC
		internal static LazyThreadSafetyMode? GetMode(LazyHelper state)
		{
			if (state == null)
			{
				return null;
			}
			return new LazyThreadSafetyMode?(state.GetMode());
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000DB211 File Offset: 0x000DA411
		internal static bool GetIsValueFaulted(LazyHelper state)
		{
			return ((state != null) ? state._exceptionDispatch : null) != null;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000DB224 File Offset: 0x000DA424
		internal static LazyHelper Create(LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				if (!useDefaultConstructor)
				{
					return LazyHelper.NoneViaFactory;
				}
				return LazyHelper.NoneViaConstructor;
			case LazyThreadSafetyMode.PublicationOnly:
				if (!useDefaultConstructor)
				{
					return LazyHelper.PublicationOnlyViaFactory;
				}
				return LazyHelper.PublicationOnlyViaConstructor;
			case LazyThreadSafetyMode.ExecutionAndPublication:
			{
				LazyState state = useDefaultConstructor ? LazyState.ExecutionAndPublicationViaConstructor : LazyState.ExecutionAndPublicationViaFactory;
				return new LazyHelper(state);
			}
			default:
				throw new ArgumentOutOfRangeException("mode", SR.Lazy_ctor_ModeInvalid);
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000DB284 File Offset: 0x000DA484
		internal static T CreateViaDefaultConstructor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>()
		{
			T result;
			try
			{
				result = Activator.CreateInstance<T>();
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(SR.Lazy_CreateValue_NoParameterlessCtorForT);
			}
			return result;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000DB2B8 File Offset: 0x000DA4B8
		internal static LazyThreadSafetyMode GetModeFromIsThreadSafe(bool isThreadSafe)
		{
			if (!isThreadSafe)
			{
				return LazyThreadSafetyMode.None;
			}
			return LazyThreadSafetyMode.ExecutionAndPublication;
		}

		// Token: 0x04000400 RID: 1024
		internal static readonly LazyHelper NoneViaConstructor = new LazyHelper(LazyState.NoneViaConstructor);

		// Token: 0x04000401 RID: 1025
		internal static readonly LazyHelper NoneViaFactory = new LazyHelper(LazyState.NoneViaFactory);

		// Token: 0x04000402 RID: 1026
		internal static readonly LazyHelper PublicationOnlyViaConstructor = new LazyHelper(LazyState.PublicationOnlyViaConstructor);

		// Token: 0x04000403 RID: 1027
		internal static readonly LazyHelper PublicationOnlyViaFactory = new LazyHelper(LazyState.PublicationOnlyViaFactory);

		// Token: 0x04000404 RID: 1028
		internal static readonly LazyHelper PublicationOnlyWaitForOtherThreadToPublish = new LazyHelper(LazyState.PublicationOnlyWait);

		// Token: 0x04000406 RID: 1030
		private readonly ExceptionDispatchInfo _exceptionDispatch;
	}
}
