using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020006C8 RID: 1736
	[NullableContext(2)]
	[Nullable(0)]
	public class StackFrame
	{
		// Token: 0x0600584C RID: 22604 RVA: 0x001AFD60 File Offset: 0x001AEF60
		internal StackFrame(StackFrameHelper stackFrameHelper, int skipFrames, bool needFileInfo)
		{
			this._method = stackFrameHelper.GetMethodBase(skipFrames);
			this._nativeOffset = stackFrameHelper.GetOffset(skipFrames);
			this._ilOffset = stackFrameHelper.GetILOffset(skipFrames);
			this._isLastFrameFromForeignExceptionStackTrace = stackFrameHelper.IsLastFrameFromForeignExceptionStackTrace(skipFrames);
			if (needFileInfo)
			{
				this._fileName = stackFrameHelper.GetFilename(skipFrames);
				this._lineNumber = stackFrameHelper.GetLineNumber(skipFrames);
				this._columnNumber = stackFrameHelper.GetColumnNumber(skipFrames);
			}
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x001AFDD4 File Offset: 0x001AEFD4
		private void BuildStackFrame(int skipFrames, bool needFileInfo)
		{
			StackFrameHelper stackFrameHelper = new StackFrameHelper(null);
			stackFrameHelper.InitializeSourceInfo(0, needFileInfo, null);
			int numberOfFrames = stackFrameHelper.GetNumberOfFrames();
			skipFrames += StackTrace.CalculateFramesToSkip(stackFrameHelper, numberOfFrames);
			if (numberOfFrames - skipFrames > 0)
			{
				this._method = stackFrameHelper.GetMethodBase(skipFrames);
				this._nativeOffset = stackFrameHelper.GetOffset(skipFrames);
				this._ilOffset = stackFrameHelper.GetILOffset(skipFrames);
				if (needFileInfo)
				{
					this._fileName = stackFrameHelper.GetFilename(skipFrames);
					this._lineNumber = stackFrameHelper.GetLineNumber(skipFrames);
					this._columnNumber = stackFrameHelper.GetColumnNumber(skipFrames);
				}
			}
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x000AC09B File Offset: 0x000AB29B
		private bool AppendStackFrameWithoutMethodBase(StringBuilder sb)
		{
			return false;
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x001AFE5A File Offset: 0x001AF05A
		private void InitMembers()
		{
			this._nativeOffset = -1;
			this._ilOffset = -1;
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x001AFE6A File Offset: 0x001AF06A
		public StackFrame()
		{
			this.InitMembers();
			this.BuildStackFrame(0, false);
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x001AFE80 File Offset: 0x001AF080
		public StackFrame(bool needFileInfo)
		{
			this.InitMembers();
			this.BuildStackFrame(0, needFileInfo);
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x001AFE96 File Offset: 0x001AF096
		public StackFrame(int skipFrames)
		{
			this.InitMembers();
			this.BuildStackFrame(skipFrames, false);
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x001AFEAC File Offset: 0x001AF0AC
		public StackFrame(int skipFrames, bool needFileInfo)
		{
			this.InitMembers();
			this.BuildStackFrame(skipFrames, needFileInfo);
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x001AFEC2 File Offset: 0x001AF0C2
		public StackFrame(string fileName, int lineNumber)
		{
			this.InitMembers();
			this.BuildStackFrame(0, false);
			this._fileName = fileName;
			this._lineNumber = lineNumber;
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x001AFEE6 File Offset: 0x001AF0E6
		public StackFrame(string fileName, int lineNumber, int colNumber) : this(fileName, lineNumber)
		{
			this._columnNumber = colNumber;
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x001AFEF7 File Offset: 0x001AF0F7
		internal bool IsLastFrameFromForeignExceptionStackTrace
		{
			get
			{
				return this._isLastFrameFromForeignExceptionStackTrace;
			}
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x001AFEFF File Offset: 0x001AF0FF
		public virtual MethodBase GetMethod()
		{
			return this._method;
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x001AFF07 File Offset: 0x001AF107
		public virtual int GetNativeOffset()
		{
			return this._nativeOffset;
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x001AFF0F File Offset: 0x001AF10F
		public virtual int GetILOffset()
		{
			return this._ilOffset;
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x001AFF17 File Offset: 0x001AF117
		public virtual string GetFileName()
		{
			return this._fileName;
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001AFF1F File Offset: 0x001AF11F
		public virtual int GetFileLineNumber()
		{
			return this._lineNumber;
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x001AFF27 File Offset: 0x001AF127
		public virtual int GetFileColumnNumber()
		{
			return this._columnNumber;
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x001AFF30 File Offset: 0x001AF130
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			bool flag2;
			if (this._method != null)
			{
				stringBuilder.Append(this._method.Name);
				MethodInfo methodInfo = this._method as MethodInfo;
				if (methodInfo != null && methodInfo.IsGenericMethod)
				{
					Type[] genericArguments = methodInfo.GetGenericArguments();
					stringBuilder.Append('<');
					int i = 0;
					bool flag = true;
					while (i < genericArguments.Length)
					{
						if (!flag)
						{
							stringBuilder.Append(',');
						}
						else
						{
							flag = false;
						}
						stringBuilder.Append(genericArguments[i].Name);
						i++;
					}
					stringBuilder.Append('>');
				}
				flag2 = true;
			}
			else
			{
				flag2 = this.AppendStackFrameWithoutMethodBase(stringBuilder);
			}
			if (flag2)
			{
				stringBuilder.Append(" at offset ");
				if (this._nativeOffset == -1)
				{
					stringBuilder.Append("<offset unknown>");
				}
				else
				{
					stringBuilder.Append(this._nativeOffset);
				}
				stringBuilder.Append(" in file:line:column ");
				stringBuilder.Append(this._fileName ?? "<filename unknown>");
				stringBuilder.Append(':');
				stringBuilder.Append(this._lineNumber);
				stringBuilder.Append(':');
				stringBuilder.Append(this._columnNumber);
			}
			else
			{
				stringBuilder.Append("<null>");
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x04001941 RID: 6465
		private MethodBase _method;

		// Token: 0x04001942 RID: 6466
		private int _nativeOffset;

		// Token: 0x04001943 RID: 6467
		private int _ilOffset;

		// Token: 0x04001944 RID: 6468
		private string _fileName;

		// Token: 0x04001945 RID: 6469
		private int _lineNumber;

		// Token: 0x04001946 RID: 6470
		private int _columnNumber;

		// Token: 0x04001947 RID: 6471
		private bool _isLastFrameFromForeignExceptionStackTrace;

		// Token: 0x04001948 RID: 6472
		public const int OFFSET_UNKNOWN = -1;
	}
}
