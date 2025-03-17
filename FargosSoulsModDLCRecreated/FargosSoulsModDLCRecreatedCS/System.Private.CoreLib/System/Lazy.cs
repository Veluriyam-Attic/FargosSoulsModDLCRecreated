using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000142 RID: 322
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerTypeProxy(typeof(LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	public class Lazy<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x000DB2F9 File Offset: 0x000DA4F9
		private static T CreateViaDefaultConstructor()
		{
			return LazyHelper.CreateViaDefaultConstructor<T>();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000DB300 File Offset: 0x000DA500
		public Lazy() : this(null, LazyThreadSafetyMode.ExecutionAndPublication, true)
		{
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000DB30B File Offset: 0x000DA50B
		public Lazy(T value)
		{
			this._value = value;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000DB31A File Offset: 0x000DA51A
		public Lazy(Func<T> valueFactory) : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication, false)
		{
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000DB325 File Offset: 0x000DA525
		public Lazy(bool isThreadSafe) : this(null, LazyHelper.GetModeFromIsThreadSafe(isThreadSafe), true)
		{
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000DB335 File Offset: 0x000DA535
		public Lazy(LazyThreadSafetyMode mode) : this(null, mode, true)
		{
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000DB340 File Offset: 0x000DA540
		public Lazy(Func<T> valueFactory, bool isThreadSafe) : this(valueFactory, LazyHelper.GetModeFromIsThreadSafe(isThreadSafe), false)
		{
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000DB350 File Offset: 0x000DA550
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode) : this(valueFactory, mode, false)
		{
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000DB35B File Offset: 0x000DA55B
		private Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			if (valueFactory == null && !useDefaultConstructor)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this._factory = valueFactory;
			this._state = LazyHelper.Create(mode, useDefaultConstructor);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000DB38A File Offset: 0x000DA58A
		private void ViaConstructor()
		{
			this._value = Lazy<T>.CreateViaDefaultConstructor();
			this._state = null;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000DB3A0 File Offset: 0x000DA5A0
		private void ViaFactory(LazyThreadSafetyMode mode)
		{
			try
			{
				Func<T> factory = this._factory;
				if (factory == null)
				{
					throw new InvalidOperationException(SR.Lazy_Value_RecursiveCallsToValue);
				}
				this._factory = null;
				this._value = factory();
				this._state = null;
			}
			catch (Exception exception)
			{
				this._state = new LazyHelper(mode, exception);
				throw;
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000DB404 File Offset: 0x000DA604
		private void ExecutionAndPublication(LazyHelper executionAndPublication, bool useDefaultConstructor)
		{
			lock (executionAndPublication)
			{
				if (this._state == executionAndPublication)
				{
					if (useDefaultConstructor)
					{
						this.ViaConstructor();
					}
					else
					{
						this.ViaFactory(LazyThreadSafetyMode.ExecutionAndPublication);
					}
				}
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000DB458 File Offset: 0x000DA658
		private void PublicationOnly(LazyHelper publicationOnly, T possibleValue)
		{
			LazyHelper lazyHelper = Interlocked.CompareExchange<LazyHelper>(ref this._state, LazyHelper.PublicationOnlyWaitForOtherThreadToPublish, publicationOnly);
			if (lazyHelper == publicationOnly)
			{
				this._factory = null;
				this._value = possibleValue;
				this._state = null;
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000DB492 File Offset: 0x000DA692
		private void PublicationOnlyViaConstructor(LazyHelper initializer)
		{
			this.PublicationOnly(initializer, Lazy<T>.CreateViaDefaultConstructor());
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000DB4A0 File Offset: 0x000DA6A0
		private void PublicationOnlyViaFactory(LazyHelper initializer)
		{
			Func<T> factory = this._factory;
			if (factory == null)
			{
				this.PublicationOnlyWaitForOtherThreadToPublish();
				return;
			}
			this.PublicationOnly(initializer, factory());
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000DB4CC File Offset: 0x000DA6CC
		private void PublicationOnlyWaitForOtherThreadToPublish()
		{
			SpinWait spinWait = default(SpinWait);
			while (this._state != null)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000DB4F4 File Offset: 0x000DA6F4
		private T CreateValue()
		{
			LazyHelper state = this._state;
			if (state != null)
			{
				switch (state.State)
				{
				case LazyState.NoneViaConstructor:
					this.ViaConstructor();
					goto IL_84;
				case LazyState.NoneViaFactory:
					this.ViaFactory(LazyThreadSafetyMode.None);
					goto IL_84;
				case LazyState.PublicationOnlyViaConstructor:
					this.PublicationOnlyViaConstructor(state);
					goto IL_84;
				case LazyState.PublicationOnlyViaFactory:
					this.PublicationOnlyViaFactory(state);
					goto IL_84;
				case LazyState.PublicationOnlyWait:
					this.PublicationOnlyWaitForOtherThreadToPublish();
					goto IL_84;
				case LazyState.ExecutionAndPublicationViaConstructor:
					this.ExecutionAndPublication(state, true);
					goto IL_84;
				case LazyState.ExecutionAndPublicationViaFactory:
					this.ExecutionAndPublication(state, false);
					goto IL_84;
				}
				state.ThrowException();
			}
			IL_84:
			return this.Value;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000DB58C File Offset: 0x000DA78C
		[NullableContext(2)]
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return SR.Lazy_ToString_ValueNotCreated;
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000DB5BC File Offset: 0x000DA7BC
		[Nullable(2)]
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return this._value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000DB5E1 File Offset: 0x000DA7E1
		internal LazyThreadSafetyMode? Mode
		{
			get
			{
				return LazyHelper.GetMode(this._state);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x000DB5F0 File Offset: 0x000DA7F0
		internal bool IsValueFaulted
		{
			get
			{
				return LazyHelper.GetIsValueFaulted(this._state);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000DB5FF File Offset: 0x000DA7FF
		public bool IsValueCreated
		{
			get
			{
				return this._state == null;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000DB60C File Offset: 0x000DA80C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				if (this._state != null)
				{
					return this.CreateValue();
				}
				return this._value;
			}
		}

		// Token: 0x04000407 RID: 1031
		private volatile LazyHelper _state;

		// Token: 0x04000408 RID: 1032
		private Func<T> _factory;

		// Token: 0x04000409 RID: 1033
		private T _value;
	}
}
