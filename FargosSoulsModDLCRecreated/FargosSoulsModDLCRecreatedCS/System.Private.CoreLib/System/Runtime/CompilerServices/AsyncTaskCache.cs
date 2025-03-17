using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000503 RID: 1283
	internal static class AsyncTaskCache
	{
		// Token: 0x0600465C RID: 18012 RVA: 0x0017AFEC File Offset: 0x0017A1EC
		private static bool GetPoolAsyncValueTasksSwitch()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("DOTNET_SYSTEM_THREADING_POOLASYNCVALUETASKS");
			return environmentVariable != null && (bool.IsTrueStringIgnoreCase(environmentVariable) || environmentVariable.Equals("1"));
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x0017B024 File Offset: 0x0017A224
		private static int GetPoolAsyncValueTasksLimitValue()
		{
			int num;
			if (!int.TryParse(Environment.GetEnvironmentVariable("DOTNET_SYSTEM_THREADING_POOLASYNCVALUETASKSLIMIT"), out num) || num <= 0)
			{
				return Environment.ProcessorCount * 4;
			}
			return num;
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x0017B054 File Offset: 0x0017A254
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x0017B078 File Offset: 0x0017A278
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x040010D4 RID: 4308
		internal static readonly Task<bool> s_trueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x040010D5 RID: 4309
		internal static readonly Task<bool> s_falseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x040010D6 RID: 4310
		internal static readonly Task<int>[] s_int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x040010D7 RID: 4311
		internal static readonly bool s_valueTaskPoolingEnabled = AsyncTaskCache.GetPoolAsyncValueTasksSwitch();

		// Token: 0x040010D8 RID: 4312
		internal static readonly int s_valueTaskPoolingCacheSize = AsyncTaskCache.GetPoolAsyncValueTasksLimitValue();
	}
}
