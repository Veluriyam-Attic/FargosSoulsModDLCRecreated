using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002BE RID: 702
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[NullableContext(1)]
	public class ThreadLocal<[Nullable(2)] T> : IDisposable
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x00148B4F File Offset: 0x00147D4F
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00148B6B File Offset: 0x00147D6B
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00148B87 File Offset: 0x00147D87
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00148BB1 File Offset: 0x00147DB1
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00148BDC File Offset: 0x00147DDC
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this._valueFactory = valueFactory;
			this._trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this._idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this._initialized = true;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x00148C24 File Offset: 0x00147E24
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x00148C54 File Offset: 0x00147E54
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00148C64 File Offset: 0x00147E64
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			int num;
			lock (obj)
			{
				num = ~this._idComplement;
				this._idComplement = 0;
				if (num < 0 || !this._initialized)
				{
					return;
				}
				this._initialized = false;
				for (ThreadLocal<T>.LinkedSlot next = this._linkedSlot._next; next != null; next = next._next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = next._slotArray;
					if (slotArray != null)
					{
						next._slotArray = null;
						slotArray[num].Value._value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this._linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00148D38 File Offset: 0x00147F38
		[NullableContext(2)]
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x00148D5C File Offset: 0x00147F5C
		// (set) Token: 0x0600288D RID: 10381 RVA: 0x00148DB0 File Offset: 0x00147FB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			[return: MaybeNull]
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this._idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this._initialized)
				{
					return value._value;
				}
				return this.GetValueSlow();
			}
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this._idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this._initialized)
				{
					value2._value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00148E04 File Offset: 0x00148004
		private T GetValueSlow()
		{
			int num = ~this._idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this._valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this._valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(SR.ThreadLocal_Value_RecursiveCallsToValue);
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00148E68 File Offset: 0x00148068
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this._idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this._trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				ThreadLocal<T>.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this._initialized)
			{
				throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
			}
			value2._value = value;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00148F1C File Offset: 0x0014811C
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			lock (obj)
			{
				if (!this._initialized)
				{
					throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
				}
				ThreadLocal<T>.LinkedSlot next = this._linkedSlot._next;
				linkedSlot._next = next;
				linkedSlot._previous = this._linkedSlot;
				linkedSlot._value = value;
				if (next != null)
				{
					next._previous = linkedSlot;
				}
				this._linkedSlot._next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x00148FC8 File Offset: 0x001481C8
		public IList<T> Values
		{
			get
			{
				if (!this._trackAllValues)
				{
					throw new InvalidOperationException(SR.ThreadLocal_ValuesNotAvailable);
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
				}
				return valuesAsList;
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00149000 File Offset: 0x00148200
		private List<T> GetValuesAsList()
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = this._linkedSlot;
			int num = ~this._idComplement;
			if (num == -1 || linkedSlot == null)
			{
				return null;
			}
			List<T> list = new List<T>();
			for (linkedSlot = linkedSlot._next; linkedSlot != null; linkedSlot = linkedSlot._next)
			{
				list.Add(linkedSlot._value);
			}
			return list;
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x00149050 File Offset: 0x00148250
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				ThreadLocal<T>.LinkedSlot linkedSlot = this._linkedSlot;
				for (ThreadLocal<T>.LinkedSlot linkedSlot2 = (linkedSlot != null) ? linkedSlot._next : null; linkedSlot2 != null; linkedSlot2 = linkedSlot2._next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x00149088 File Offset: 0x00148288
		public bool IsValueCreated
		{
			get
			{
				int num = ~this._idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(SR.ThreadLocal_Disposed);
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x001490D0 File Offset: 0x001482D0
		[Nullable(2)]
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this._idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this._initialized)
				{
					return default(T);
				}
				return value._value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x00149120 File Offset: 0x00148320
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x00149128 File Offset: 0x00148328
		private static void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			int newTableSize = ThreadLocal<T>.GetNewTableSize(minLength);
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[newTableSize];
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			lock (obj)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value._slotArray != null)
					{
						value._slotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x001491C4 File Offset: 0x001483C4
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x04000ACC RID: 2764
		private Func<T> _valueFactory;

		// Token: 0x04000ACD RID: 2765
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x04000ACE RID: 2766
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x04000ACF RID: 2767
		private int _idComplement;

		// Token: 0x04000AD0 RID: 2768
		private volatile bool _initialized;

		// Token: 0x04000AD1 RID: 2769
		private static readonly ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x04000AD2 RID: 2770
		private ThreadLocal<T>.LinkedSlot _linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x04000AD3 RID: 2771
		private bool _trackAllValues;

		// Token: 0x020002BF RID: 703
		private struct LinkedSlotVolatile
		{
			// Token: 0x04000AD4 RID: 2772
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x020002C0 RID: 704
		private sealed class LinkedSlot
		{
			// Token: 0x0600289A RID: 10394 RVA: 0x00149223 File Offset: 0x00148423
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this._slotArray = slotArray;
			}

			// Token: 0x04000AD5 RID: 2773
			internal volatile ThreadLocal<T>.LinkedSlot _next;

			// Token: 0x04000AD6 RID: 2774
			internal volatile ThreadLocal<T>.LinkedSlot _previous;

			// Token: 0x04000AD7 RID: 2775
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] _slotArray;

			// Token: 0x04000AD8 RID: 2776
			internal T _value;
		}

		// Token: 0x020002C1 RID: 705
		private class IdManager
		{
			// Token: 0x0600289B RID: 10395 RVA: 0x00149234 File Offset: 0x00148434
			internal int GetId()
			{
				List<bool> freeIds = this._freeIds;
				int result;
				lock (freeIds)
				{
					int num = this._nextIdToTry;
					while (num < this._freeIds.Count && !this._freeIds[num])
					{
						num++;
					}
					if (num == this._freeIds.Count)
					{
						this._freeIds.Add(false);
					}
					else
					{
						this._freeIds[num] = false;
					}
					this._nextIdToTry = num + 1;
					result = num;
				}
				return result;
			}

			// Token: 0x0600289C RID: 10396 RVA: 0x001492CC File Offset: 0x001484CC
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this._freeIds;
				lock (freeIds)
				{
					this._freeIds[id] = true;
					if (id < this._nextIdToTry)
					{
						this._nextIdToTry = id;
					}
				}
			}

			// Token: 0x04000AD9 RID: 2777
			private int _nextIdToTry;

			// Token: 0x04000ADA RID: 2778
			private readonly List<bool> _freeIds = new List<bool>();
		}

		// Token: 0x020002C2 RID: 706
		private class FinalizationHelper
		{
			// Token: 0x0600289E RID: 10398 RVA: 0x00149337 File Offset: 0x00148537
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this._trackAllValues = trackAllValues;
			}

			// Token: 0x0600289F RID: 10399 RVA: 0x00149350 File Offset: 0x00148550
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this._trackAllValues)
							{
								value._slotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value._next != null)
									{
										value._next._previous = value._previous;
									}
									value._previous._next = value._next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04000ADB RID: 2779
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04000ADC RID: 2780
			private readonly bool _trackAllValues;
		}
	}
}
