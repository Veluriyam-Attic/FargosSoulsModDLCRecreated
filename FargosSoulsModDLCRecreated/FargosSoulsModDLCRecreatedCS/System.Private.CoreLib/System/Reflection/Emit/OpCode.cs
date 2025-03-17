using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000661 RID: 1633
	[Nullable(0)]
	[NullableContext(2)]
	public readonly struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x0600539D RID: 21405 RVA: 0x0019B010 File Offset: 0x0019A210
		internal OpCode(OpCodeValues value, int flags)
		{
			this.m_value = value;
			this.m_flags = flags;
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0019B020 File Offset: 0x0019A220
		internal bool EndsUncondJmpBlk()
		{
			return (this.m_flags & 16777216) != 0;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0019B031 File Offset: 0x0019A231
		internal int StackChange()
		{
			return this.m_flags >> 28;
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060053A0 RID: 21408 RVA: 0x0019B03C File Offset: 0x0019A23C
		public OperandType OperandType
		{
			get
			{
				return (OperandType)(this.m_flags & 31);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x0019B047 File Offset: 0x0019A247
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)(this.m_flags >> 5 & 15);
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060053A2 RID: 21410 RVA: 0x0019B054 File Offset: 0x0019A254
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)(this.m_flags >> 9 & 7);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x0019B061 File Offset: 0x0019A261
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)(this.m_flags >> 12 & 31);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060053A4 RID: 21412 RVA: 0x0019B06F File Offset: 0x0019A26F
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)(this.m_flags >> 17 & 31);
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060053A5 RID: 21413 RVA: 0x0019B07D File Offset: 0x0019A27D
		public int Size
		{
			get
			{
				return this.m_flags >> 22 & 3;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060053A6 RID: 21414 RVA: 0x0019B08A File Offset: 0x0019A28A
		public short Value
		{
			get
			{
				return (short)this.m_value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x0019B094 File Offset: 0x0019A294
		public string Name
		{
			get
			{
				if (this.Size == 0)
				{
					return null;
				}
				string[] array = OpCode.g_nameCache;
				if (array == null)
				{
					array = new string[287];
					OpCode.g_nameCache = array;
				}
				OpCodeValues opCodeValues = (OpCodeValues)((ushort)this.Value);
				int num = (int)opCodeValues;
				if (num > 255)
				{
					if (num < 65024 || num > 65054)
					{
						return null;
					}
					num = 256 + (num - 65024);
				}
				string text = Volatile.Read<string>(ref array[num]);
				if (text != null)
				{
					return text;
				}
				text = Enum.GetName(typeof(OpCodeValues), opCodeValues).ToLowerInvariant().Replace('_', '.');
				Volatile.Write<string>(ref array[num], text);
				return text;
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0019B144 File Offset: 0x0019A344
		public override bool Equals(object obj)
		{
			if (obj is OpCode)
			{
				OpCode obj2 = (OpCode)obj;
				return this.Equals(obj2);
			}
			return false;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x0019B169 File Offset: 0x0019A369
		public bool Equals(OpCode obj)
		{
			return obj.Value == this.Value;
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x0019B17A File Offset: 0x0019A37A
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.Equals(b);
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0019B184 File Offset: 0x0019A384
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0019B190 File Offset: 0x0019A390
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0019B198 File Offset: 0x0019A398
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04001553 RID: 5459
		private readonly OpCodeValues m_value;

		// Token: 0x04001554 RID: 5460
		private readonly int m_flags;

		// Token: 0x04001555 RID: 5461
		private static volatile string[] g_nameCache;
	}
}
