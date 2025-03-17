using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x02000323 RID: 803
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(2)]
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		// Token: 0x06002B29 RID: 11049 RVA: 0x001514AF File Offset: 0x001506AF
		public TaskCanceledException() : base(SR.TaskCanceledException_ctor_DefaultMessage)
		{
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x001514BC File Offset: 0x001506BC
		public TaskCanceledException(string message) : base(message)
		{
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x001514C5 File Offset: 0x001506C5
		public TaskCanceledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x001514CF File Offset: 0x001506CF
		public TaskCanceledException(string message, Exception innerException, CancellationToken token) : base(message, innerException, token)
		{
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x001514DA File Offset: 0x001506DA
		public TaskCanceledException(Task task) : base(SR.TaskCanceledException_ctor_DefaultMessage, (task != null) ? task.CancellationToken : CancellationToken.None)
		{
			this._canceledTask = task;
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x001514FE File Offset: 0x001506FE
		[NullableContext(1)]
		protected TaskCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00151508 File Offset: 0x00150708
		public Task Task
		{
			get
			{
				return this._canceledTask;
			}
		}

		// Token: 0x04000BF9 RID: 3065
		[NonSerialized]
		private readonly Task _canceledTask;
	}
}
