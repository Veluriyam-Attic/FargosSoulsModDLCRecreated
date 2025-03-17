using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x020000BE RID: 190
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class AggregateException : Exception
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x000C749A File Offset: 0x000C669A
		public AggregateException() : base(SR.AggregateException_ctor_DefaultMessage)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000C74B7 File Offset: 0x000C66B7
		[NullableContext(2)]
		public AggregateException(string message) : base(message)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000C74D0 File Offset: 0x000C66D0
		public AggregateException([Nullable(2)] string message, Exception innerException) : base(message, innerException)
		{
			if (innerException == null)
			{
				throw new ArgumentNullException("innerException");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[]
			{
				innerException
			});
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000C74FD File Offset: 0x000C66FD
		public AggregateException(IEnumerable<Exception> innerExceptions) : this(SR.AggregateException_ctor_DefaultMessage, innerExceptions)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000C750B File Offset: 0x000C670B
		public AggregateException(params Exception[] innerExceptions) : this(SR.AggregateException_ctor_DefaultMessage, innerExceptions)
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000C7519 File Offset: 0x000C6719
		public AggregateException([Nullable(2)] string message, IEnumerable<Exception> innerExceptions) : this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000C7538 File Offset: 0x000C6738
		public AggregateException([Nullable(2)] string message, params Exception[] innerExceptions) : this(message, innerExceptions)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000C7544 File Offset: 0x000C6744
		private AggregateException(string message, IList<Exception> innerExceptions) : base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException(SR.AggregateException_ctor_InnerExceptionNull);
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000C75BC File Offset: 0x000C67BC
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this(SR.AggregateException_ctor_DefaultMessage, innerExceptionInfos)
		{
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000C75CA File Offset: 0x000C67CA
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000C75EC File Offset: 0x000C67EC
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos) : base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException(SR.AggregateException_ctor_InnerExceptionNull);
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000C767C File Offset: 0x000C687C
		protected AggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException(SR.AggregateException_DeserializationFailure);
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000C76D4 File Offset: 0x000C68D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000C7720 File Offset: 0x000C6920
		public override Exception GetBaseException()
		{
			Exception ex = this;
			AggregateException ex2 = this;
			while (ex2 != null && ex2.InnerExceptions.Count == 1)
			{
				ex = ex.InnerException;
				ex2 = (ex as AggregateException);
			}
			return ex;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000C7753 File Offset: 0x000C6953
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			get
			{
				return this.m_innerExceptions;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000C775C File Offset: 0x000C695C
		public void Handle(Func<Exception, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<Exception> list = null;
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (!predicate(this.m_innerExceptions[i]))
				{
					if (list == null)
					{
						list = new List<Exception>();
					}
					list.Add(this.m_innerExceptions[i]);
				}
			}
			if (list != null)
			{
				throw new AggregateException(this.Message, list);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000C77D0 File Offset: 0x000C69D0
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>
			{
				this
			};
			int num = 0;
			while (list2.Count > num)
			{
				ReadOnlyCollection<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException((base.GetType() == typeof(AggregateException)) ? base.Message : this.Message, list);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000C787C File Offset: 0x000C6A7C
		public override string Message
		{
			get
			{
				if (this.m_innerExceptions.Count == 0)
				{
					return base.Message;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(base.Message);
				stringBuilder.Append(' ');
				for (int i = 0; i < this.m_innerExceptions.Count; i++)
				{
					stringBuilder.Append('(');
					stringBuilder.Append(this.m_innerExceptions[i].Message);
					stringBuilder.Append(") ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				int length = stringBuilder2.Length;
				stringBuilder2.Length = length - 1;
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000C7914 File Offset: 0x000C6B14
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (this.m_innerExceptions[i] != base.InnerException)
				{
					stringBuilder.Append("\r\n ---> ");
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, SR.AggregateException_InnerException, i);
					stringBuilder.Append(this.m_innerExceptions[i].ToString());
					stringBuilder.Append("<---");
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000C79B2 File Offset: 0x000C6BB2
		private int InnerExceptionCount
		{
			get
			{
				return this.InnerExceptions.Count;
			}
		}

		// Token: 0x0400025F RID: 607
		private readonly ReadOnlyCollection<Exception> m_innerExceptions;
	}
}
