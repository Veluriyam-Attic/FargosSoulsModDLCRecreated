using System;

namespace System
{
	// Token: 0x02000192 RID: 402
	internal enum ExceptionResource
	{
		// Token: 0x0400050F RID: 1295
		ArgumentOutOfRange_Index,
		// Token: 0x04000510 RID: 1296
		ArgumentOutOfRange_IndexCount,
		// Token: 0x04000511 RID: 1297
		ArgumentOutOfRange_IndexCountBuffer,
		// Token: 0x04000512 RID: 1298
		ArgumentOutOfRange_Count,
		// Token: 0x04000513 RID: 1299
		ArgumentOutOfRange_Year,
		// Token: 0x04000514 RID: 1300
		Arg_ArrayPlusOffTooSmall,
		// Token: 0x04000515 RID: 1301
		NotSupported_ReadOnlyCollection,
		// Token: 0x04000516 RID: 1302
		Arg_RankMultiDimNotSupported,
		// Token: 0x04000517 RID: 1303
		Arg_NonZeroLowerBound,
		// Token: 0x04000518 RID: 1304
		ArgumentOutOfRange_GetCharCountOverflow,
		// Token: 0x04000519 RID: 1305
		ArgumentOutOfRange_ListInsert,
		// Token: 0x0400051A RID: 1306
		ArgumentOutOfRange_NeedNonNegNum,
		// Token: 0x0400051B RID: 1307
		ArgumentOutOfRange_SmallCapacity,
		// Token: 0x0400051C RID: 1308
		Argument_InvalidOffLen,
		// Token: 0x0400051D RID: 1309
		Argument_CannotExtractScalar,
		// Token: 0x0400051E RID: 1310
		ArgumentOutOfRange_BiggerThanCollection,
		// Token: 0x0400051F RID: 1311
		Serialization_MissingKeys,
		// Token: 0x04000520 RID: 1312
		Serialization_NullKey,
		// Token: 0x04000521 RID: 1313
		NotSupported_KeyCollectionSet,
		// Token: 0x04000522 RID: 1314
		NotSupported_ValueCollectionSet,
		// Token: 0x04000523 RID: 1315
		InvalidOperation_NullArray,
		// Token: 0x04000524 RID: 1316
		TaskT_TransitionToFinal_AlreadyCompleted,
		// Token: 0x04000525 RID: 1317
		TaskCompletionSourceT_TrySetException_NullException,
		// Token: 0x04000526 RID: 1318
		TaskCompletionSourceT_TrySetException_NoExceptions,
		// Token: 0x04000527 RID: 1319
		NotSupported_StringComparison,
		// Token: 0x04000528 RID: 1320
		ConcurrentCollection_SyncRoot_NotSupported,
		// Token: 0x04000529 RID: 1321
		Task_MultiTaskContinuation_NullTask,
		// Token: 0x0400052A RID: 1322
		InvalidOperation_WrongAsyncResultOrEndCalledMultiple,
		// Token: 0x0400052B RID: 1323
		Task_MultiTaskContinuation_EmptyTaskList,
		// Token: 0x0400052C RID: 1324
		Task_Start_TaskCompleted,
		// Token: 0x0400052D RID: 1325
		Task_Start_Promise,
		// Token: 0x0400052E RID: 1326
		Task_Start_ContinuationTask,
		// Token: 0x0400052F RID: 1327
		Task_Start_AlreadyStarted,
		// Token: 0x04000530 RID: 1328
		Task_RunSynchronously_Continuation,
		// Token: 0x04000531 RID: 1329
		Task_RunSynchronously_Promise,
		// Token: 0x04000532 RID: 1330
		Task_RunSynchronously_TaskCompleted,
		// Token: 0x04000533 RID: 1331
		Task_RunSynchronously_AlreadyStarted,
		// Token: 0x04000534 RID: 1332
		AsyncMethodBuilder_InstanceNotInitialized,
		// Token: 0x04000535 RID: 1333
		Task_ContinueWith_ESandLR,
		// Token: 0x04000536 RID: 1334
		Task_ContinueWith_NotOnAnything,
		// Token: 0x04000537 RID: 1335
		Task_Delay_InvalidDelay,
		// Token: 0x04000538 RID: 1336
		Task_Delay_InvalidMillisecondsDelay,
		// Token: 0x04000539 RID: 1337
		Task_Dispose_NotCompleted,
		// Token: 0x0400053A RID: 1338
		Task_ThrowIfDisposed,
		// Token: 0x0400053B RID: 1339
		Task_WaitMulti_NullTask,
		// Token: 0x0400053C RID: 1340
		ArgumentException_OtherNotArrayOfCorrectLength,
		// Token: 0x0400053D RID: 1341
		ArgumentNull_Array,
		// Token: 0x0400053E RID: 1342
		ArgumentNull_SafeHandle,
		// Token: 0x0400053F RID: 1343
		ArgumentOutOfRange_EndIndexStartIndex,
		// Token: 0x04000540 RID: 1344
		ArgumentOutOfRange_Enum,
		// Token: 0x04000541 RID: 1345
		ArgumentOutOfRange_HugeArrayNotSupported,
		// Token: 0x04000542 RID: 1346
		Argument_AddingDuplicate,
		// Token: 0x04000543 RID: 1347
		Argument_InvalidArgumentForComparison,
		// Token: 0x04000544 RID: 1348
		Arg_LowerBoundsMustMatch,
		// Token: 0x04000545 RID: 1349
		Arg_MustBeType,
		// Token: 0x04000546 RID: 1350
		Arg_Need1DArray,
		// Token: 0x04000547 RID: 1351
		Arg_Need2DArray,
		// Token: 0x04000548 RID: 1352
		Arg_Need3DArray,
		// Token: 0x04000549 RID: 1353
		Arg_NeedAtLeast1Rank,
		// Token: 0x0400054A RID: 1354
		Arg_RankIndices,
		// Token: 0x0400054B RID: 1355
		Arg_RanksAndBounds,
		// Token: 0x0400054C RID: 1356
		InvalidOperation_IComparerFailed,
		// Token: 0x0400054D RID: 1357
		NotSupported_FixedSizeCollection,
		// Token: 0x0400054E RID: 1358
		Rank_MultiDimNotSupported,
		// Token: 0x0400054F RID: 1359
		Arg_TypeNotSupported,
		// Token: 0x04000550 RID: 1360
		Argument_SpansMustHaveSameLength,
		// Token: 0x04000551 RID: 1361
		Argument_InvalidFlag
	}
}
