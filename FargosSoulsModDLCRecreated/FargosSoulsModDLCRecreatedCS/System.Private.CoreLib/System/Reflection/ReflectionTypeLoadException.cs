using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020005FA RID: 1530
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		// Token: 0x06004D21 RID: 19745 RVA: 0x0018C325 File Offset: 0x0018B525
		[NullableContext(2)]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions) : this(classes, exceptions, null)
		{
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0018C330 File Offset: 0x0018B530
		[NullableContext(2)]
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message) : base(message)
		{
			this.Types = (classes ?? Type.EmptyTypes);
			this.LoaderExceptions = (exceptions ?? Array.Empty<Exception>());
			base.HResult = -2146232830;
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0018C364 File Offset: 0x0018B564
		private ReflectionTypeLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Types = Type.EmptyTypes;
			this.LoaderExceptions = (((Exception[])info.GetValue("Exceptions", typeof(Exception[]))) ?? Array.Empty<Exception>());
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0018C3A2 File Offset: 0x0018B5A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Types", null, typeof(Type[]));
			info.AddValue("Exceptions", this.LoaderExceptions, typeof(Exception[]));
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x0018C3DD File Offset: 0x0018B5DD
		[Nullable(new byte[]
		{
			1,
			2
		})]
		public Type[] Types { [return: Nullable(new byte[]
		{
			1,
			2
		})] get; }

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x0018C3E5 File Offset: 0x0018B5E5
		[Nullable(new byte[]
		{
			1,
			2
		})]
		public Exception[] LoaderExceptions { [return: Nullable(new byte[]
		{
			1,
			2
		})] get; }

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x0018C3ED File Offset: 0x0018B5ED
		public override string Message
		{
			get
			{
				return this.CreateString(true);
			}
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0018C3F6 File Offset: 0x0018B5F6
		public override string ToString()
		{
			return this.CreateString(false);
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0018C400 File Offset: 0x0018B600
		private string CreateString(bool isMessage)
		{
			string text = isMessage ? base.Message : base.ToString();
			Exception[] loaderExceptions = this.LoaderExceptions;
			if (loaderExceptions.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			foreach (Exception ex in loaderExceptions)
			{
				if (ex != null)
				{
					stringBuilder.AppendLine().Append(isMessage ? ex.Message : ex.ToString());
				}
			}
			return stringBuilder.ToString();
		}
	}
}
