using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020006C9 RID: 1737
	internal class StackFrameHelper
	{
		// Token: 0x0600585E RID: 22622 RVA: 0x001B007C File Offset: 0x001AF27C
		public StackFrameHelper(Thread target)
		{
			this.targetThread = target;
			this.rgMethodHandle = null;
			this.rgiMethodToken = null;
			this.rgiOffset = null;
			this.rgiILOffset = null;
			this.rgAssemblyPath = null;
			this.rgAssembly = null;
			this.rgLoadedPeAddress = null;
			this.rgiLoadedPeSize = null;
			this.rgInMemoryPdbAddress = null;
			this.rgiInMemoryPdbSize = null;
			this.dynamicMethods = null;
			this.rgFilename = null;
			this.rgiLineNumber = null;
			this.rgiColumnNumber = null;
			this.rgiLastFrameFromForeignExceptionStackTrace = null;
			this.iFrameCount = 0;
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x001B0108 File Offset: 0x001AF308
		[DynamicDependency("#ctor()", "System.Diagnostics.StackTraceSymbols", "System.Diagnostics.StackTrace")]
		[DynamicDependency("GetSourceLineInfo", "System.Diagnostics.StackTraceSymbols", "System.Diagnostics.StackTrace")]
		internal void InitializeSourceInfo(int iSkip, bool fNeedFileInfo, Exception exception)
		{
			StackTrace.GetStackFramesInternal(this, iSkip, fNeedFileInfo, exception);
			if (!fNeedFileInfo)
			{
				return;
			}
			if (StackFrameHelper.t_reentrancy > 0)
			{
				return;
			}
			StackFrameHelper.t_reentrancy++;
			try
			{
				if (StackFrameHelper.s_getSourceLineInfo == null)
				{
					Type type = Type.GetType("System.Diagnostics.StackTraceSymbols, System.Diagnostics.StackTrace, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false);
					if (type == null)
					{
						return;
					}
					Type[] types = new Type[]
					{
						typeof(Assembly),
						typeof(string),
						typeof(IntPtr),
						typeof(int),
						typeof(IntPtr),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(string).MakeByRefType(),
						typeof(int).MakeByRefType(),
						typeof(int).MakeByRefType()
					};
					MethodInfo method = type.GetMethod("GetSourceLineInfo", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
					if (method == null)
					{
						return;
					}
					object target = Activator.CreateInstance(type);
					StackFrameHelper.GetSourceLineInfoDelegate value = method.CreateDelegate<StackFrameHelper.GetSourceLineInfoDelegate>(target);
					Interlocked.CompareExchange<StackFrameHelper.GetSourceLineInfoDelegate>(ref StackFrameHelper.s_getSourceLineInfo, value, null);
				}
				for (int i = 0; i < this.iFrameCount; i++)
				{
					if (this.rgiMethodToken[i] != 0)
					{
						StackFrameHelper.s_getSourceLineInfo(this.rgAssembly[i], this.rgAssemblyPath[i], this.rgLoadedPeAddress[i], this.rgiLoadedPeSize[i], this.rgInMemoryPdbAddress[i], this.rgiInMemoryPdbSize[i], this.rgiMethodToken[i], this.rgiILOffset[i], out this.rgFilename[i], out this.rgiLineNumber[i], out this.rgiColumnNumber[i]);
					}
				}
			}
			catch
			{
			}
			finally
			{
				StackFrameHelper.t_reentrancy--;
			}
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x001B0328 File Offset: 0x001AF528
		public virtual MethodBase GetMethodBase(int i)
		{
			IntPtr intPtr = this.rgMethodHandle[i];
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IRuntimeMethodInfo typicalMethodDefinition = RuntimeMethodHandle.GetTypicalMethodDefinition(new RuntimeMethodInfoStub(intPtr, this));
			return RuntimeType.GetMethodBase(typicalMethodDefinition);
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x001B0360 File Offset: 0x001AF560
		public virtual int GetOffset(int i)
		{
			return this.rgiOffset[i];
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x001B036A File Offset: 0x001AF56A
		public virtual int GetILOffset(int i)
		{
			return this.rgiILOffset[i];
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x001B0374 File Offset: 0x001AF574
		public virtual string GetFilename(int i)
		{
			string[] array = this.rgFilename;
			if (array == null)
			{
				return null;
			}
			return array[i];
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x001B0384 File Offset: 0x001AF584
		public virtual int GetLineNumber(int i)
		{
			if (this.rgiLineNumber != null)
			{
				return this.rgiLineNumber[i];
			}
			return 0;
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x001B0398 File Offset: 0x001AF598
		public virtual int GetColumnNumber(int i)
		{
			if (this.rgiColumnNumber != null)
			{
				return this.rgiColumnNumber[i];
			}
			return 0;
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x001B03AC File Offset: 0x001AF5AC
		public virtual bool IsLastFrameFromForeignExceptionStackTrace(int i)
		{
			return this.rgiLastFrameFromForeignExceptionStackTrace != null && this.rgiLastFrameFromForeignExceptionStackTrace[i];
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x001B03C0 File Offset: 0x001AF5C0
		public virtual int GetNumberOfFrames()
		{
			return this.iFrameCount;
		}

		// Token: 0x04001949 RID: 6473
		private Thread targetThread;

		// Token: 0x0400194A RID: 6474
		private int[] rgiOffset;

		// Token: 0x0400194B RID: 6475
		private int[] rgiILOffset;

		// Token: 0x0400194C RID: 6476
		private object dynamicMethods;

		// Token: 0x0400194D RID: 6477
		private IntPtr[] rgMethodHandle;

		// Token: 0x0400194E RID: 6478
		private string[] rgAssemblyPath;

		// Token: 0x0400194F RID: 6479
		private Assembly[] rgAssembly;

		// Token: 0x04001950 RID: 6480
		private IntPtr[] rgLoadedPeAddress;

		// Token: 0x04001951 RID: 6481
		private int[] rgiLoadedPeSize;

		// Token: 0x04001952 RID: 6482
		private IntPtr[] rgInMemoryPdbAddress;

		// Token: 0x04001953 RID: 6483
		private int[] rgiInMemoryPdbSize;

		// Token: 0x04001954 RID: 6484
		private int[] rgiMethodToken;

		// Token: 0x04001955 RID: 6485
		private string[] rgFilename;

		// Token: 0x04001956 RID: 6486
		private int[] rgiLineNumber;

		// Token: 0x04001957 RID: 6487
		private int[] rgiColumnNumber;

		// Token: 0x04001958 RID: 6488
		private bool[] rgiLastFrameFromForeignExceptionStackTrace;

		// Token: 0x04001959 RID: 6489
		private int iFrameCount;

		// Token: 0x0400195A RID: 6490
		private static StackFrameHelper.GetSourceLineInfoDelegate s_getSourceLineInfo;

		// Token: 0x0400195B RID: 6491
		[ThreadStatic]
		private static int t_reentrancy;

		// Token: 0x020006CA RID: 1738
		// (Invoke) Token: 0x06005869 RID: 22633
		private delegate void GetSourceLineInfoDelegate(Assembly assembly, string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn);
	}
}
