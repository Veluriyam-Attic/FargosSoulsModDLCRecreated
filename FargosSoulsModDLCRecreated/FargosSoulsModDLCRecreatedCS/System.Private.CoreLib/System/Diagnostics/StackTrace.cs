using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020006CB RID: 1739
	[NullableContext(1)]
	[Nullable(0)]
	public class StackTrace
	{
		// Token: 0x0600586A RID: 22634
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetStackFramesInternal(StackFrameHelper sfh, int iSkip, bool fNeedFileInfo, Exception e);

		// Token: 0x0600586B RID: 22635 RVA: 0x001B03C8 File Offset: 0x001AF5C8
		internal static int CalculateFramesToSkip(StackFrameHelper StackF, int iNumFrames)
		{
			int num = 0;
			for (int i = 0; i < iNumFrames; i++)
			{
				MethodBase methodBase = StackF.GetMethodBase(i);
				if (methodBase != null)
				{
					Type declaringType = methodBase.DeclaringType;
					if (declaringType == null)
					{
						break;
					}
					string @namespace = declaringType.Namespace;
					if (@namespace == null || !string.Equals(@namespace, "System.Diagnostics", StringComparison.Ordinal))
					{
						break;
					}
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x001B0424 File Offset: 0x001AF624
		private void InitializeForException(Exception exception, int skipFrames, bool fNeedFileInfo)
		{
			this.CaptureStackTrace(skipFrames, fNeedFileInfo, exception);
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x001B042F File Offset: 0x001AF62F
		private void InitializeForCurrentThread(int skipFrames, bool fNeedFileInfo)
		{
			this.CaptureStackTrace(skipFrames, fNeedFileInfo, null);
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x001B043C File Offset: 0x001AF63C
		private void CaptureStackTrace(int skipFrames, bool fNeedFileInfo, Exception e)
		{
			this._methodsToSkip = skipFrames;
			StackFrameHelper stackFrameHelper = new StackFrameHelper(null);
			stackFrameHelper.InitializeSourceInfo(0, fNeedFileInfo, e);
			this._numOfFrames = stackFrameHelper.GetNumberOfFrames();
			if (this._methodsToSkip > this._numOfFrames)
			{
				this._methodsToSkip = this._numOfFrames;
			}
			if (this._numOfFrames != 0)
			{
				this._stackFrames = new StackFrame[this._numOfFrames];
				for (int i = 0; i < this._numOfFrames; i++)
				{
					this._stackFrames[i] = new StackFrame(stackFrameHelper, i, fNeedFileInfo);
				}
				if (e == null)
				{
					this._methodsToSkip += StackTrace.CalculateFramesToSkip(stackFrameHelper, this._numOfFrames);
				}
				this._numOfFrames -= this._methodsToSkip;
				if (this._numOfFrames < 0)
				{
					this._numOfFrames = 0;
				}
			}
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x001B04FF File Offset: 0x001AF6FF
		public StackTrace()
		{
			this.InitializeForCurrentThread(0, false);
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x001B050F File Offset: 0x001AF70F
		public StackTrace(bool fNeedFileInfo)
		{
			this.InitializeForCurrentThread(0, fNeedFileInfo);
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x001B051F File Offset: 0x001AF71F
		public StackTrace(int skipFrames)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.InitializeForCurrentThread(skipFrames, false);
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x001B0543 File Offset: 0x001AF743
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.InitializeForCurrentThread(skipFrames, fNeedFileInfo);
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x001B0567 File Offset: 0x001AF767
		public StackTrace(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.InitializeForException(e, 0, false);
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x001B0586 File Offset: 0x001AF786
		public StackTrace(Exception e, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			this.InitializeForException(e, 0, fNeedFileInfo);
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x001B05A5 File Offset: 0x001AF7A5
		public StackTrace(Exception e, int skipFrames)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.InitializeForException(e, skipFrames, false);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x001B05D8 File Offset: 0x001AF7D8
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("skipFrames", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this.InitializeForException(e, skipFrames, fNeedFileInfo);
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x001B060B File Offset: 0x001AF80B
		public StackTrace(StackFrame frame)
		{
			this._stackFrames = new StackFrame[]
			{
				frame
			};
			this._numOfFrames = 1;
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06005878 RID: 22648 RVA: 0x001B062A File Offset: 0x001AF82A
		public virtual int FrameCount
		{
			get
			{
				return this._numOfFrames;
			}
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x001B0632 File Offset: 0x001AF832
		[NullableContext(2)]
		public virtual StackFrame GetFrame(int index)
		{
			if (this._stackFrames != null && index < this._numOfFrames && index >= 0)
			{
				return this._stackFrames[index + this._methodsToSkip];
			}
			return null;
		}

		// Token: 0x0600587A RID: 22650 RVA: 0x001B065C File Offset: 0x001AF85C
		public virtual StackFrame[] GetFrames()
		{
			if (this._stackFrames == null || this._numOfFrames <= 0)
			{
				return Array.Empty<StackFrame>();
			}
			StackFrame[] array = new StackFrame[this._numOfFrames];
			Array.Copy(this._stackFrames, this._methodsToSkip, array, 0, this._numOfFrames);
			return array;
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x001B06A6 File Offset: 0x001AF8A6
		public override string ToString()
		{
			return this.ToString(StackTrace.TraceFormat.TrailingNewLine);
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x001B06B0 File Offset: 0x001AF8B0
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.ToString(traceFormat, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600587D RID: 22653 RVA: 0x001B06D8 File Offset: 0x001AF8D8
		internal void ToString(StackTrace.TraceFormat traceFormat, StringBuilder sb)
		{
			string resourceString = SR.GetResourceString("Word_At", "at");
			string resourceString2 = SR.GetResourceString("StackTrace_InFileLineNumber", "in {0}:line {1}");
			bool flag = true;
			for (int i = 0; i < this._numOfFrames; i++)
			{
				StackFrame frame = this.GetFrame(i);
				MethodBase methodBase = (frame != null) ? frame.GetMethod() : null;
				if (methodBase != null && (StackTrace.ShowInStackTrace(methodBase) || i == this._numOfFrames - 1))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						sb.AppendLine();
					}
					sb.AppendFormat(CultureInfo.InvariantCulture, "   {0} ", resourceString);
					bool flag2 = false;
					Type declaringType = methodBase.DeclaringType;
					string name = methodBase.Name;
					bool flag3 = false;
					if (declaringType != null && declaringType.IsDefined(typeof(CompilerGeneratedAttribute), false))
					{
						flag2 = declaringType.IsAssignableTo(typeof(IAsyncStateMachine));
						if (flag2 || declaringType.IsAssignableTo(typeof(IEnumerator)))
						{
							flag3 = StackTrace.TryResolveStateMachineMethod(ref methodBase, out declaringType);
						}
					}
					if (declaringType != null)
					{
						foreach (char c in declaringType.FullName)
						{
							sb.Append((c == '+') ? '.' : c);
						}
						sb.Append('.');
					}
					sb.Append(methodBase.Name);
					MethodInfo methodInfo = methodBase as MethodInfo;
					if (methodInfo != null && methodInfo.IsGenericMethod)
					{
						Type[] genericArguments = methodInfo.GetGenericArguments();
						sb.Append('[');
						int k = 0;
						bool flag4 = true;
						while (k < genericArguments.Length)
						{
							if (!flag4)
							{
								sb.Append(',');
							}
							else
							{
								flag4 = false;
							}
							sb.Append(genericArguments[k].Name);
							k++;
						}
						sb.Append(']');
					}
					ParameterInfo[] array = null;
					try
					{
						array = methodBase.GetParameters();
					}
					catch
					{
					}
					if (array != null)
					{
						sb.Append('(');
						bool flag5 = true;
						for (int l = 0; l < array.Length; l++)
						{
							if (!flag5)
							{
								sb.Append(", ");
							}
							else
							{
								flag5 = false;
							}
							string value = "<UnknownType>";
							if (array[l].ParameterType != null)
							{
								value = array[l].ParameterType.Name;
							}
							sb.Append(value);
							sb.Append(' ');
							sb.Append(array[l].Name);
						}
						sb.Append(')');
					}
					if (flag3)
					{
						sb.Append('+');
						sb.Append(name);
						sb.Append('(').Append(')');
					}
					if (frame.GetILOffset() != -1)
					{
						string fileName = frame.GetFileName();
						if (fileName != null)
						{
							sb.Append(' ');
							sb.AppendFormat(CultureInfo.InvariantCulture, resourceString2, fileName, frame.GetFileLineNumber());
						}
					}
					if (frame.IsLastFrameFromForeignExceptionStackTrace && !flag2)
					{
						sb.AppendLine();
						sb.Append(SR.GetResourceString("Exception_EndStackTraceFromPreviousThrow", "--- End of stack trace from previous location ---"));
					}
				}
			}
			if (traceFormat == StackTrace.TraceFormat.TrailingNewLine)
			{
				sb.AppendLine();
			}
		}

		// Token: 0x0600587E RID: 22654 RVA: 0x001B09F8 File Offset: 0x001AFBF8
		private static bool ShowInStackTrace(MethodBase mb)
		{
			if ((mb.MethodImplementationFlags & MethodImplAttributes.AggressiveInlining) != MethodImplAttributes.IL)
			{
				return false;
			}
			if (mb.IsDefined(typeof(StackTraceHiddenAttribute), false))
			{
				return false;
			}
			Type declaringType = mb.DeclaringType;
			return !(declaringType != null) || !declaringType.IsDefined(typeof(StackTraceHiddenAttribute), false);
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x001B0A50 File Offset: 0x001AFC50
		private static bool TryResolveStateMachineMethod(ref MethodBase method, out Type declaringType)
		{
			declaringType = method.DeclaringType;
			Type declaringType2 = declaringType.DeclaringType;
			if (declaringType2 == null)
			{
				return false;
			}
			MethodInfo[] methods = declaringType2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return false;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				IEnumerable<StateMachineAttribute> customAttributes = methodInfo.GetCustomAttributes(false);
				if (customAttributes != null)
				{
					bool flag = false;
					bool flag2 = false;
					foreach (StateMachineAttribute stateMachineAttribute in customAttributes)
					{
						if (stateMachineAttribute.StateMachineType == declaringType)
						{
							flag = true;
							flag2 |= (stateMachineAttribute is IteratorStateMachineAttribute || stateMachineAttribute is AsyncIteratorStateMachineAttribute);
						}
					}
					if (flag)
					{
						method = methodInfo;
						declaringType = methodInfo.DeclaringType;
						return flag2;
					}
				}
			}
			return false;
		}

		// Token: 0x0400195C RID: 6492
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x0400195D RID: 6493
		private int _numOfFrames;

		// Token: 0x0400195E RID: 6494
		private int _methodsToSkip;

		// Token: 0x0400195F RID: 6495
		private StackFrame[] _stackFrames;

		// Token: 0x020006CC RID: 1740
		internal enum TraceFormat
		{
			// Token: 0x04001961 RID: 6497
			Normal,
			// Token: 0x04001962 RID: 6498
			TrailingNewLine
		}
	}
}
