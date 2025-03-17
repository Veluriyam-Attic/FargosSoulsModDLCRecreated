using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006DE RID: 1758
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ContractException : Exception
	{
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x060058F1 RID: 22769 RVA: 0x001B1556 File Offset: 0x001B0756
		public ContractFailureKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x001B155E File Offset: 0x001B075E
		[Nullable(1)]
		public string Failure
		{
			[NullableContext(1)]
			get
			{
				return this.Message;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060058F3 RID: 22771 RVA: 0x001B1566 File Offset: 0x001B0766
		public string UserMessage
		{
			get
			{
				return this._userMessage;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060058F4 RID: 22772 RVA: 0x001B156E File Offset: 0x001B076E
		public string Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x001B1576 File Offset: 0x001B0776
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x001B1589 File Offset: 0x001B0789
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException) : base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._kind = kind;
			this._userMessage = userMessage;
			this._condition = condition;
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x001B15B5 File Offset: 0x001B07B5
		private ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._kind = (ContractFailureKind)info.GetInt32("Kind");
			this._userMessage = info.GetString("UserMessage");
			this._condition = info.GetString("Condition");
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x001B15F4 File Offset: 0x001B07F4
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._kind);
			info.AddValue("UserMessage", this._userMessage);
			info.AddValue("Condition", this._condition);
		}

		// Token: 0x0400198C RID: 6540
		private readonly ContractFailureKind _kind;

		// Token: 0x0400198D RID: 6541
		private readonly string _userMessage;

		// Token: 0x0400198E RID: 6542
		private readonly string _condition;
	}
}
