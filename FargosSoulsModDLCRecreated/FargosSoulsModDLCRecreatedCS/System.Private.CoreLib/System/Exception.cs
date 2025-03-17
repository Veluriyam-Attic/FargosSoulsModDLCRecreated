using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x0200006B RID: 107
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Exception : ISerializable
	{
		// Token: 0x0600036D RID: 877 RVA: 0x000B60F7 File Offset: 0x000B52F7
		private IDictionary CreateDataContainer()
		{
			if (Exception.IsImmutableAgileException(this))
			{
				return new EmptyReadOnlyDictionaryInternal();
			}
			return new ListDictionaryInternal();
		}

		// Token: 0x0600036E RID: 878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsImmutableAgileException(Exception e);

		// Token: 0x0600036F RID: 879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo GetMethodFromStackTrace(object stackTrace);

		// Token: 0x06000370 RID: 880 RVA: 0x000B610C File Offset: 0x000B530C
		private MethodBase GetExceptionMethodFromStackTrace()
		{
			IRuntimeMethodInfo methodFromStackTrace = Exception.GetMethodFromStackTrace(this._stackTrace);
			if (methodFromStackTrace == null)
			{
				return null;
			}
			return RuntimeType.GetMethodBase(methodFromStackTrace);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000B6130 File Offset: 0x000B5330
		public MethodBase TargetSite
		{
			get
			{
				if (this._exceptionMethod != null)
				{
					return this._exceptionMethod;
				}
				if (this._stackTrace == null)
				{
					return null;
				}
				this._exceptionMethod = this.GetExceptionMethodFromStackTrace();
				return this._exceptionMethod;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000B6164 File Offset: 0x000B5364
		public virtual string StackTrace
		{
			get
			{
				string stackTraceString = this._stackTraceString;
				string remoteStackTraceString = this._remoteStackTraceString;
				if (stackTraceString != null)
				{
					return remoteStackTraceString + stackTraceString;
				}
				if (this._stackTrace == null)
				{
					return remoteStackTraceString;
				}
				return remoteStackTraceString + Exception.GetStackTrace(this);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000B61A0 File Offset: 0x000B53A0
		private static string GetStackTrace(Exception e)
		{
			return new StackTrace(e, true).ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000B61B0 File Offset: 0x000B53B0
		private string CreateSourceName()
		{
			StackTrace stackTrace = new StackTrace(this, false);
			if (stackTrace.FrameCount > 0)
			{
				StackFrame frame = stackTrace.GetFrame(0);
				MethodBase method = frame.GetMethod();
				Module module = method.Module;
				RuntimeModule runtimeModule = module as RuntimeModule;
				if (runtimeModule == null)
				{
					ModuleBuilder moduleBuilder = module as ModuleBuilder;
					if (moduleBuilder == null)
					{
						throw new ArgumentException(SR.Argument_MustBeRuntimeReflectionObject);
					}
					runtimeModule = moduleBuilder.InternalModule;
				}
				return runtimeModule.GetRuntimeAssembly().GetSimpleName();
			}
			return null;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000B6220 File Offset: 0x000B5420
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			this._ipForWatsonBuckets = UIntPtr.Zero;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000B6234 File Offset: 0x000B5434
		internal void InternalPreserveStackTrace()
		{
			string source = this.Source;
			string stackTrace = this.StackTrace;
			if (!string.IsNullOrEmpty(stackTrace))
			{
				this._remoteStackTraceString = stackTrace + "\r\n";
			}
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		// Token: 0x06000377 RID: 887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareForForeignExceptionRaise();

		// Token: 0x06000378 RID: 888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStackTracesDeepCopy(Exception exception, out byte[] currentStackTrace, out object[] dynamicMethodArray);

		// Token: 0x06000379 RID: 889
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SaveStackTracesFromDeepCopy(Exception exception, byte[] currentStackTrace, object[] dynamicMethodArray);

		// Token: 0x0600037A RID: 890
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetExceptionCount();

		// Token: 0x0600037B RID: 891 RVA: 0x000B6278 File Offset: 0x000B5478
		internal void RestoreDispatchState(in Exception.DispatchState dispatchState)
		{
			if (!Exception.IsImmutableAgileException(this))
			{
				byte[] stackTrace = dispatchState.StackTrace;
				byte[] currentStackTrace = (byte[])((stackTrace != null) ? stackTrace.Clone() : null);
				object[] dynamicMethods = dispatchState.DynamicMethods;
				object[] dynamicMethodArray = (object[])((dynamicMethods != null) ? dynamicMethods.Clone() : null);
				this._watsonBuckets = dispatchState.WatsonBuckets;
				this._ipForWatsonBuckets = dispatchState.IpForWatsonBuckets;
				this._remoteStackTraceString = dispatchState.RemoteStackTrace;
				Exception.SaveStackTracesFromDeepCopy(this, currentStackTrace, dynamicMethodArray);
				this._stackTraceString = null;
				Exception.PrepareForForeignExceptionRaise();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000B62F5 File Offset: 0x000B54F5
		private string SerializationRemoteStackTraceString
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000B62FD File Offset: 0x000B54FD
		private object SerializationWatsonBuckets
		{
			get
			{
				return this._watsonBuckets;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000B6308 File Offset: 0x000B5508
		private string SerializationStackTraceString
		{
			get
			{
				string text = this._stackTraceString;
				if (text == null && this._stackTrace != null)
				{
					text = Exception.GetStackTrace(this);
				}
				return text;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000B6330 File Offset: 0x000B5530
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			string result = null;
			Exception.GetMessageFromNativeResources(kind, new StringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x06000380 RID: 896
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMessageFromNativeResources(Exception.ExceptionMessageKind kind, StringHandleOnStack retMesg);

		// Token: 0x06000381 RID: 897 RVA: 0x000B6350 File Offset: 0x000B5550
		internal Exception.DispatchState CaptureDispatchState()
		{
			byte[] stackTrace;
			object[] dynamicMethods;
			Exception.GetStackTracesDeepCopy(this, out stackTrace, out dynamicMethods);
			return new Exception.DispatchState(stackTrace, dynamicMethods, this._remoteStackTraceString, this._ipForWatsonBuckets, this._watsonBuckets);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000B6380 File Offset: 0x000B5580
		[StackTraceHidden]
		internal void SetCurrentStackTrace()
		{
			if (Exception.IsImmutableAgileException(this))
			{
				return;
			}
			if (this._stackTrace != null || this._stackTraceString != null || this._remoteStackTraceString != null)
			{
				ThrowHelper.ThrowInvalidOperationException();
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			new StackTrace(true).ToString(System.Diagnostics.StackTrace.TraceFormat.TrailingNewLine, stringBuilder);
			stringBuilder.AppendLine(SR.Exception_EndStackTraceFromPreviousThrow);
			this._remoteStackTraceString = stringBuilder.ToString();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000B63E3 File Offset: 0x000B55E3
		public Exception()
		{
			this._HResult = -2146233088;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000B6401 File Offset: 0x000B5601
		public Exception(string message) : this()
		{
			this._message = message;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000B6410 File Offset: 0x000B5610
		public Exception(string message, Exception innerException) : this()
		{
			this._message = message;
			this._innerException = innerException;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000B6428 File Offset: 0x000B5628
		[NullableContext(1)]
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this.RestoreRemoteStackTrace(info, context);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000B64F1 File Offset: 0x000B56F1
		[Nullable(1)]
		public virtual string Message
		{
			[NullableContext(1)]
			get
			{
				return this._message ?? SR.Format(SR.Exception_WasThrown, this.GetClassName());
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000B6510 File Offset: 0x000B5710
		[Nullable(1)]
		public virtual IDictionary Data
		{
			[NullableContext(1)]
			get
			{
				IDictionary result;
				if ((result = this._data) == null)
				{
					result = (this._data = this.CreateDataContainer());
				}
				return result;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000B6536 File Offset: 0x000B5736
		private string GetClassName()
		{
			return this.GetType().ToString();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000B6544 File Offset: 0x000B5744
		[NullableContext(1)]
		public virtual Exception GetBaseException()
		{
			Exception innerException = this.InnerException;
			Exception result = this;
			while (innerException != null)
			{
				result = innerException;
				innerException = innerException.InnerException;
			}
			return result;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000B6569 File Offset: 0x000B5769
		public Exception InnerException
		{
			get
			{
				return this._innerException;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000B6571 File Offset: 0x000B5771
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000B6579 File Offset: 0x000B5779
		public virtual string HelpLink
		{
			get
			{
				return this._helpURL;
			}
			set
			{
				this._helpURL = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000B6584 File Offset: 0x000B5784
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000B65AA File Offset: 0x000B57AA
		public virtual string Source
		{
			get
			{
				string result;
				if ((result = this._source) == null)
				{
					result = (this._source = this.CreateSourceName());
				}
				return result;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000B65B4 File Offset: 0x000B57B4
		[NullableContext(1)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", this.SerializationStackTraceString, typeof(string));
			info.AddValue("RemoteStackTraceString", this.SerializationRemoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", 0, typeof(int));
			info.AddValue("ExceptionMethod", null, typeof(string));
			info.AddValue("HResult", this._HResult);
			info.AddValue("Source", this._source, typeof(string));
			info.AddValue("WatsonBuckets", this.SerializationWatsonBuckets, typeof(byte[]));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000B6718 File Offset: 0x000B5918
		[NullableContext(1)]
		public override string ToString()
		{
			string className = this.GetClassName();
			string message = this.Message;
			Exception innerException = this._innerException;
			string text = ((innerException != null) ? innerException.ToString() : null) ?? "";
			string exception_EndOfInnerExceptionStack = SR.Exception_EndOfInnerExceptionStack;
			string stackTrace = this.StackTrace;
			int num = className.Length;
			checked
			{
				if (!string.IsNullOrEmpty(message))
				{
					num += 2 + message.Length;
				}
				if (this._innerException != null)
				{
					num += "\r\n".Length + " ---> ".Length + text.Length + "\r\n".Length + 3 + exception_EndOfInnerExceptionStack.Length;
				}
				if (stackTrace != null)
				{
					num += "\r\n".Length + stackTrace.Length;
				}
				string text2 = string.FastAllocateString(num);
				Span<char> span = new Span<char>(text2.GetRawStringData(), text2.Length);
				Exception.<ToString>g__Write|65_0(className, ref span);
				if (!string.IsNullOrEmpty(message))
				{
					Exception.<ToString>g__Write|65_0(": ", ref span);
					Exception.<ToString>g__Write|65_0(message, ref span);
				}
				if (this._innerException != null)
				{
					Exception.<ToString>g__Write|65_0("\r\n", ref span);
					Exception.<ToString>g__Write|65_0(" ---> ", ref span);
					Exception.<ToString>g__Write|65_0(text, ref span);
					Exception.<ToString>g__Write|65_0("\r\n", ref span);
					Exception.<ToString>g__Write|65_0("   ", ref span);
					Exception.<ToString>g__Write|65_0(exception_EndOfInnerExceptionStack, ref span);
				}
				if (stackTrace != null)
				{
					Exception.<ToString>g__Write|65_0("\r\n", ref span);
					Exception.<ToString>g__Write|65_0(stackTrace, ref span);
				}
				return text2;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000392 RID: 914 RVA: 0x000B6877 File Offset: 0x000B5A77
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x000B6877 File Offset: 0x000B5A77
		[Nullable(new byte[]
		{
			2,
			1
		})]
		protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState
		{
			add
			{
				throw new PlatformNotSupportedException(SR.PlatformNotSupported_SecureBinarySerialization);
			}
			remove
			{
				throw new PlatformNotSupportedException(SR.PlatformNotSupported_SecureBinarySerialization);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000B6883 File Offset: 0x000B5A83
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000B688B File Offset: 0x000B5A8B
		public int HResult
		{
			get
			{
				return this._HResult;
			}
			set
			{
				this._HResult = value;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000AF0A4 File Offset: 0x000AE2A4
		[NullableContext(1)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000B6894 File Offset: 0x000B5A94
		private void RestoreRemoteStackTrace(SerializationInfo info, StreamingContext context)
		{
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._watsonBuckets = (byte[])info.GetValueNoThrow("WatsonBuckets", typeof(byte[]));
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000B6900 File Offset: 0x000B5B00
		[CompilerGenerated]
		internal static void <ToString>g__Write|65_0(string source, ref Span<char> dest)
		{
			source.AsSpan().CopyTo(dest);
			dest = dest.Slice(source.Length);
		}

		// Token: 0x0400014F RID: 335
		private MethodBase _exceptionMethod;

		// Token: 0x04000150 RID: 336
		internal string _message;

		// Token: 0x04000151 RID: 337
		private IDictionary _data;

		// Token: 0x04000152 RID: 338
		private readonly Exception _innerException;

		// Token: 0x04000153 RID: 339
		private string _helpURL;

		// Token: 0x04000154 RID: 340
		private byte[] _stackTrace;

		// Token: 0x04000155 RID: 341
		private byte[] _watsonBuckets;

		// Token: 0x04000156 RID: 342
		private string _stackTraceString;

		// Token: 0x04000157 RID: 343
		private string _remoteStackTraceString;

		// Token: 0x04000158 RID: 344
		private readonly object[] _dynamicMethods;

		// Token: 0x04000159 RID: 345
		private string _source;

		// Token: 0x0400015A RID: 346
		private UIntPtr _ipForWatsonBuckets;

		// Token: 0x0400015B RID: 347
		private readonly IntPtr _xptrs;

		// Token: 0x0400015C RID: 348
		private readonly int _xcode = -532462766;

		// Token: 0x0400015D RID: 349
		private int _HResult;

		// Token: 0x0400015E RID: 350
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x0400015F RID: 351
		private protected const string InnerExceptionPrefix = " ---> ";

		// Token: 0x0200006C RID: 108
		internal enum ExceptionMessageKind
		{
			// Token: 0x04000161 RID: 353
			ThreadAbort = 1,
			// Token: 0x04000162 RID: 354
			ThreadInterrupted,
			// Token: 0x04000163 RID: 355
			OutOfMemory
		}

		// Token: 0x0200006D RID: 109
		internal readonly struct DispatchState
		{
			// Token: 0x06000399 RID: 921 RVA: 0x000B6933 File Offset: 0x000B5B33
			public DispatchState(byte[] stackTrace, object[] dynamicMethods, string remoteStackTrace, UIntPtr ipForWatsonBuckets, byte[] watsonBuckets)
			{
				this.StackTrace = stackTrace;
				this.DynamicMethods = dynamicMethods;
				this.RemoteStackTrace = remoteStackTrace;
				this.IpForWatsonBuckets = ipForWatsonBuckets;
				this.WatsonBuckets = watsonBuckets;
			}

			// Token: 0x04000164 RID: 356
			public readonly byte[] StackTrace;

			// Token: 0x04000165 RID: 357
			public readonly object[] DynamicMethods;

			// Token: 0x04000166 RID: 358
			public readonly string RemoteStackTrace;

			// Token: 0x04000167 RID: 359
			public readonly UIntPtr IpForWatsonBuckets;

			// Token: 0x04000168 RID: 360
			public readonly byte[] WatsonBuckets;
		}
	}
}
