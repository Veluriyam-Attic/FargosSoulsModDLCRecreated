using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000190 RID: 400
	[StackTraceHidden]
	internal static class ThrowHelper
	{
		// Token: 0x0600180F RID: 6159 RVA: 0x000F2A70 File Offset: 0x000F1C70
		[DoesNotReturn]
		internal static void ThrowArrayTypeMismatchException()
		{
			throw new ArrayTypeMismatchException();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000F2A77 File Offset: 0x000F1C77
		[DoesNotReturn]
		internal static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
		{
			throw new ArgumentException(SR.Format(SR.Argument_InvalidTypeWithPointersNotSupported, targetType));
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000F2A89 File Offset: 0x000F1C89
		[DoesNotReturn]
		internal static void ThrowIndexOutOfRangeException()
		{
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000F2A90 File Offset: 0x000F1C90
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000F2A97 File Offset: 0x000F1C97
		[DoesNotReturn]
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw new ArgumentException(SR.Argument_DestinationTooShort, "destination");
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x000F2AA8 File Offset: 0x000F1CA8
		[DoesNotReturn]
		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw new ArgumentException(SR.Argument_OverlapAlignmentMismatch);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000F2AB4 File Offset: 0x000F1CB4
		[DoesNotReturn]
		internal static void ThrowArgumentException_CannotExtractScalar(ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentException(ExceptionResource.Argument_CannotExtractScalar, argument);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000F2ABE File Offset: 0x000F1CBE
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_IndexException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x000F2AC8 File Offset: 0x000F1CC8
		[DoesNotReturn]
		internal static void ThrowArgumentException_BadComparer(object comparer)
		{
			throw new ArgumentException(SR.Format(SR.Arg_BogusIComparer, comparer));
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000F2ADA File Offset: 0x000F1CDA
		[DoesNotReturn]
		internal static void ThrowIndexArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000F2AE5 File Offset: 0x000F1CE5
		[DoesNotReturn]
		internal static void ThrowValueArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000F2AEF File Offset: 0x000F1CEF
		[DoesNotReturn]
		internal static void ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.length, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000F2AFA File Offset: 0x000F1CFA
		[DoesNotReturn]
		internal static void ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000F2B03 File Offset: 0x000F1D03
		[DoesNotReturn]
		internal static void ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x000F2B0D File Offset: 0x000F1D0D
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_Year()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.year, ExceptionResource.ArgumentOutOfRange_Year);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000F2B17 File Offset: 0x000F1D17
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_BadYearMonthDay()
		{
			throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000F2B24 File Offset: 0x000F1D24
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_BadHourMinuteSecond()
		{
			throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000F2B31 File Offset: 0x000F1D31
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRange_TimeSpanTooLong()
		{
			throw new ArgumentOutOfRangeException(null, SR.Overflow_TimeSpanTooLong);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000F2B3E File Offset: 0x000F1D3E
		[DoesNotReturn]
		internal static void ThrowWrongKeyTypeArgumentException<T>(T key, Type targetType)
		{
			throw ThrowHelper.GetWrongKeyTypeArgumentException(key, targetType);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000F2B4C File Offset: 0x000F1D4C
		[DoesNotReturn]
		internal static void ThrowWrongValueTypeArgumentException<T>(T value, Type targetType)
		{
			throw ThrowHelper.GetWrongValueTypeArgumentException(value, targetType);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000F2B5A File Offset: 0x000F1D5A
		private static ArgumentException GetAddingDuplicateWithKeyArgumentException(object key)
		{
			return new ArgumentException(SR.Format(SR.Argument_AddingDuplicateWithKey, key));
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000F2B6C File Offset: 0x000F1D6C
		[DoesNotReturn]
		internal static void ThrowAddingDuplicateWithKeyArgumentException<T>(T key)
		{
			throw ThrowHelper.GetAddingDuplicateWithKeyArgumentException(key);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000F2B79 File Offset: 0x000F1D79
		[DoesNotReturn]
		internal static void ThrowKeyNotFoundException<T>(T key)
		{
			throw ThrowHelper.GetKeyNotFoundException(key);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000F2B86 File Offset: 0x000F1D86
		[DoesNotReturn]
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw ThrowHelper.GetArgumentException(resource);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000F2B8E File Offset: 0x000F1D8E
		[DoesNotReturn]
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentException(resource, argument);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000F2B97 File Offset: 0x000F1D97
		private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000F2BA4 File Offset: 0x000F1DA4
		[DoesNotReturn]
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.GetArgumentNullException(argument);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000F2BAC File Offset: 0x000F1DAC
		[DoesNotReturn]
		internal static void ThrowArgumentNullException(ExceptionArgument argument, ExceptionResource resource)
		{
			throw new ArgumentNullException(ThrowHelper.GetArgumentName(argument), ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000F2BBF File Offset: 0x000F1DBF
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000F2BCC File Offset: 0x000F1DCC
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(argument, resource);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000F2BD5 File Offset: 0x000F1DD5
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, int paramNumber, ExceptionResource resource)
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(argument, paramNumber, resource);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000F2BE6 File Offset: 0x000F1DE6
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw ThrowHelper.GetInvalidOperationException(resource);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000F2BEE File Offset: 0x000F1DEE
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException(ExceptionResource resource, Exception e)
		{
			throw new InvalidOperationException(ThrowHelper.GetResourceString(resource), e);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000F2BFC File Offset: 0x000F1DFC
		[DoesNotReturn]
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000F2C09 File Offset: 0x000F1E09
		[DoesNotReturn]
		internal static void ThrowRankException(ExceptionResource resource)
		{
			throw new RankException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000F2C16 File Offset: 0x000F1E16
		[DoesNotReturn]
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000F2C23 File Offset: 0x000F1E23
		[DoesNotReturn]
		internal static void ThrowObjectDisposedException(ExceptionResource resource)
		{
			throw new ObjectDisposedException(null, ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000C279F File Offset: 0x000C199F
		[DoesNotReturn]
		internal static void ThrowNotSupportedException()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000F2C31 File Offset: 0x000F1E31
		[DoesNotReturn]
		internal static void ThrowAggregateException(List<Exception> exceptions)
		{
			throw new AggregateException(exceptions);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000F2C39 File Offset: 0x000F1E39
		[DoesNotReturn]
		internal static void ThrowArgumentException_Argument_InvalidArrayType()
		{
			throw new ArgumentException(SR.Argument_InvalidArrayType);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000F2C45 File Offset: 0x000F1E45
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumNotStarted()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumNotStarted);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000F2C51 File Offset: 0x000F1E51
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumEnded()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumEnded);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000F2C5D File Offset: 0x000F1E5D
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_EnumCurrent(int index)
		{
			throw ThrowHelper.GetInvalidOperationException_EnumCurrent(index);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000F2C65 File Offset: 0x000F1E65
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumFailedVersion);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000F2C71 File Offset: 0x000F1E71
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
		{
			throw new InvalidOperationException(SR.InvalidOperation_EnumOpCantHappen);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000F2C7D File Offset: 0x000F1E7D
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_InvalidOperation_NoValue()
		{
			throw new InvalidOperationException(SR.InvalidOperation_NoValue);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000F2C89 File Offset: 0x000F1E89
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_ConcurrentOperationsNotSupported()
		{
			throw new InvalidOperationException(SR.InvalidOperation_ConcurrentOperationsNotSupported);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000F2C95 File Offset: 0x000F1E95
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_HandleIsNotInitialized()
		{
			throw new InvalidOperationException(SR.InvalidOperation_HandleIsNotInitialized);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000F2CA1 File Offset: 0x000F1EA1
		[DoesNotReturn]
		internal static void ThrowInvalidOperationException_HandleIsNotPinned()
		{
			throw new InvalidOperationException(SR.InvalidOperation_HandleIsNotPinned);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000F2CAD File Offset: 0x000F1EAD
		[DoesNotReturn]
		internal static void ThrowArraySegmentCtorValidationFailedExceptions(Array array, int offset, int count)
		{
			throw ThrowHelper.GetArraySegmentCtorValidationFailedException(array, offset, count);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000F2CB7 File Offset: 0x000F1EB7
		[DoesNotReturn]
		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw new FormatException(SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000F2CC3 File Offset: 0x000F1EC3
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw new ArgumentOutOfRangeException("precision", SR.Format(SR.Argument_PrecisionTooLarge, 99));
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000F2CE0 File Offset: 0x000F1EE0
		[DoesNotReturn]
		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw new ArgumentOutOfRangeException("symbol", SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000F2CF4 File Offset: 0x000F1EF4
		private static Exception GetArraySegmentCtorValidationFailedException(Array array, int offset, int count)
		{
			if (array == null)
			{
				return new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				return new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				return new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return new ArgumentException(SR.Argument_InvalidOffLen);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000F2D41 File Offset: 0x000F1F41
		private static ArgumentException GetArgumentException(ExceptionResource resource)
		{
			return new ArgumentException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000F2D4E File Offset: 0x000F1F4E
		private static InvalidOperationException GetInvalidOperationException(ExceptionResource resource)
		{
			return new InvalidOperationException(ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000F2D5B File Offset: 0x000F1F5B
		private static ArgumentException GetWrongKeyTypeArgumentException(object key, Type targetType)
		{
			return new ArgumentException(SR.Format(SR.Arg_WrongType, key, targetType), "key");
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000F2D73 File Offset: 0x000F1F73
		private static ArgumentException GetWrongValueTypeArgumentException(object value, Type targetType)
		{
			return new ArgumentException(SR.Format(SR.Arg_WrongType, value, targetType), "value");
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000F2D8B File Offset: 0x000F1F8B
		private static KeyNotFoundException GetKeyNotFoundException(object key)
		{
			return new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key));
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000F2D9D File Offset: 0x000F1F9D
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000F2DB0 File Offset: 0x000F1FB0
		private static ArgumentException GetArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			return new ArgumentException(ThrowHelper.GetResourceString(resource), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000F2DC3 File Offset: 0x000F1FC3
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, int paramNumber, ExceptionResource resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument) + "[" + paramNumber.ToString() + "]", ThrowHelper.GetResourceString(resource));
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000F2DEC File Offset: 0x000F1FEC
		private static InvalidOperationException GetInvalidOperationException_EnumCurrent(int index)
		{
			return new InvalidOperationException((index < 0) ? SR.InvalidOperation_EnumNotStarted : SR.InvalidOperation_EnumEnded);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000F2E04 File Offset: 0x000F2004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (default(T) != null && value == null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000F2E2C File Offset: 0x000F202C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ThrowForUnsupportedVectorBaseType<T>() where T : struct
		{
			if (typeof(T) != typeof(byte) && typeof(T) != typeof(sbyte) && typeof(T) != typeof(short) && typeof(T) != typeof(ushort) && typeof(T) != typeof(int) && typeof(T) != typeof(uint) && typeof(T) != typeof(long) && typeof(T) != typeof(ulong) && typeof(T) != typeof(float) && typeof(T) != typeof(double))
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.Arg_TypeNotSupported);
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000F2F60 File Offset: 0x000F2160
		private static string GetArgumentName(ExceptionArgument argument)
		{
			switch (argument)
			{
			case ExceptionArgument.obj:
				return "obj";
			case ExceptionArgument.dictionary:
				return "dictionary";
			case ExceptionArgument.array:
				return "array";
			case ExceptionArgument.info:
				return "info";
			case ExceptionArgument.key:
				return "key";
			case ExceptionArgument.text:
				return "text";
			case ExceptionArgument.values:
				return "values";
			case ExceptionArgument.value:
				return "value";
			case ExceptionArgument.startIndex:
				return "startIndex";
			case ExceptionArgument.task:
				return "task";
			case ExceptionArgument.bytes:
				return "bytes";
			case ExceptionArgument.byteIndex:
				return "byteIndex";
			case ExceptionArgument.byteCount:
				return "byteCount";
			case ExceptionArgument.ch:
				return "ch";
			case ExceptionArgument.chars:
				return "chars";
			case ExceptionArgument.charIndex:
				return "charIndex";
			case ExceptionArgument.charCount:
				return "charCount";
			case ExceptionArgument.s:
				return "s";
			case ExceptionArgument.input:
				return "input";
			case ExceptionArgument.ownedMemory:
				return "ownedMemory";
			case ExceptionArgument.list:
				return "list";
			case ExceptionArgument.index:
				return "index";
			case ExceptionArgument.capacity:
				return "capacity";
			case ExceptionArgument.collection:
				return "collection";
			case ExceptionArgument.item:
				return "item";
			case ExceptionArgument.converter:
				return "converter";
			case ExceptionArgument.match:
				return "match";
			case ExceptionArgument.count:
				return "count";
			case ExceptionArgument.action:
				return "action";
			case ExceptionArgument.comparison:
				return "comparison";
			case ExceptionArgument.exceptions:
				return "exceptions";
			case ExceptionArgument.exception:
				return "exception";
			case ExceptionArgument.pointer:
				return "pointer";
			case ExceptionArgument.start:
				return "start";
			case ExceptionArgument.format:
				return "format";
			case ExceptionArgument.culture:
				return "culture";
			case ExceptionArgument.comparer:
				return "comparer";
			case ExceptionArgument.comparable:
				return "comparable";
			case ExceptionArgument.source:
				return "source";
			case ExceptionArgument.state:
				return "state";
			case ExceptionArgument.length:
				return "length";
			case ExceptionArgument.comparisonType:
				return "comparisonType";
			case ExceptionArgument.manager:
				return "manager";
			case ExceptionArgument.sourceBytesToCopy:
				return "sourceBytesToCopy";
			case ExceptionArgument.callBack:
				return "callBack";
			case ExceptionArgument.creationOptions:
				return "creationOptions";
			case ExceptionArgument.function:
				return "function";
			case ExceptionArgument.scheduler:
				return "scheduler";
			case ExceptionArgument.continuationAction:
				return "continuationAction";
			case ExceptionArgument.continuationFunction:
				return "continuationFunction";
			case ExceptionArgument.tasks:
				return "tasks";
			case ExceptionArgument.asyncResult:
				return "asyncResult";
			case ExceptionArgument.beginMethod:
				return "beginMethod";
			case ExceptionArgument.endMethod:
				return "endMethod";
			case ExceptionArgument.endFunction:
				return "endFunction";
			case ExceptionArgument.cancellationToken:
				return "cancellationToken";
			case ExceptionArgument.continuationOptions:
				return "continuationOptions";
			case ExceptionArgument.delay:
				return "delay";
			case ExceptionArgument.millisecondsDelay:
				return "millisecondsDelay";
			case ExceptionArgument.millisecondsTimeout:
				return "millisecondsTimeout";
			case ExceptionArgument.stateMachine:
				return "stateMachine";
			case ExceptionArgument.timeout:
				return "timeout";
			case ExceptionArgument.type:
				return "type";
			case ExceptionArgument.sourceIndex:
				return "sourceIndex";
			case ExceptionArgument.sourceArray:
				return "sourceArray";
			case ExceptionArgument.destinationIndex:
				return "destinationIndex";
			case ExceptionArgument.destinationArray:
				return "destinationArray";
			case ExceptionArgument.pHandle:
				return "pHandle";
			case ExceptionArgument.other:
				return "other";
			case ExceptionArgument.newSize:
				return "newSize";
			case ExceptionArgument.lowerBounds:
				return "lowerBounds";
			case ExceptionArgument.lengths:
				return "lengths";
			case ExceptionArgument.len:
				return "len";
			case ExceptionArgument.keys:
				return "keys";
			case ExceptionArgument.indices:
				return "indices";
			case ExceptionArgument.index1:
				return "index1";
			case ExceptionArgument.index2:
				return "index2";
			case ExceptionArgument.index3:
				return "index3";
			case ExceptionArgument.length1:
				return "length1";
			case ExceptionArgument.length2:
				return "length2";
			case ExceptionArgument.length3:
				return "length3";
			case ExceptionArgument.endIndex:
				return "endIndex";
			case ExceptionArgument.elementType:
				return "elementType";
			case ExceptionArgument.arrayIndex:
				return "arrayIndex";
			case ExceptionArgument.year:
				return "year";
			case ExceptionArgument.codePoint:
				return "codePoint";
			case ExceptionArgument.str:
				return "str";
			case ExceptionArgument.options:
				return "options";
			case ExceptionArgument.prefix:
				return "prefix";
			case ExceptionArgument.suffix:
				return "suffix";
			default:
				return "";
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000F3304 File Offset: 0x000F2504
		private static string GetResourceString(ExceptionResource resource)
		{
			switch (resource)
			{
			case ExceptionResource.ArgumentOutOfRange_Index:
				return SR.ArgumentOutOfRange_Index;
			case ExceptionResource.ArgumentOutOfRange_IndexCount:
				return SR.ArgumentOutOfRange_IndexCount;
			case ExceptionResource.ArgumentOutOfRange_IndexCountBuffer:
				return SR.ArgumentOutOfRange_IndexCountBuffer;
			case ExceptionResource.ArgumentOutOfRange_Count:
				return SR.ArgumentOutOfRange_Count;
			case ExceptionResource.ArgumentOutOfRange_Year:
				return SR.ArgumentOutOfRange_Year;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				return SR.Arg_ArrayPlusOffTooSmall;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				return SR.NotSupported_ReadOnlyCollection;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				return SR.Arg_RankMultiDimNotSupported;
			case ExceptionResource.Arg_NonZeroLowerBound:
				return SR.Arg_NonZeroLowerBound;
			case ExceptionResource.ArgumentOutOfRange_GetCharCountOverflow:
				return SR.ArgumentOutOfRange_GetCharCountOverflow;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				return SR.ArgumentOutOfRange_ListInsert;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				return SR.ArgumentOutOfRange_NeedNonNegNum;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				return SR.ArgumentOutOfRange_SmallCapacity;
			case ExceptionResource.Argument_InvalidOffLen:
				return SR.Argument_InvalidOffLen;
			case ExceptionResource.Argument_CannotExtractScalar:
				return SR.Argument_CannotExtractScalar;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				return SR.ArgumentOutOfRange_BiggerThanCollection;
			case ExceptionResource.Serialization_MissingKeys:
				return SR.Serialization_MissingKeys;
			case ExceptionResource.Serialization_NullKey:
				return SR.Serialization_NullKey;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				return SR.NotSupported_KeyCollectionSet;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				return SR.NotSupported_ValueCollectionSet;
			case ExceptionResource.InvalidOperation_NullArray:
				return SR.InvalidOperation_NullArray;
			case ExceptionResource.TaskT_TransitionToFinal_AlreadyCompleted:
				return SR.TaskT_TransitionToFinal_AlreadyCompleted;
			case ExceptionResource.TaskCompletionSourceT_TrySetException_NullException:
				return SR.TaskCompletionSourceT_TrySetException_NullException;
			case ExceptionResource.TaskCompletionSourceT_TrySetException_NoExceptions:
				return SR.TaskCompletionSourceT_TrySetException_NoExceptions;
			case ExceptionResource.NotSupported_StringComparison:
				return SR.NotSupported_StringComparison;
			case ExceptionResource.ConcurrentCollection_SyncRoot_NotSupported:
				return SR.ConcurrentCollection_SyncRoot_NotSupported;
			case ExceptionResource.Task_MultiTaskContinuation_NullTask:
				return SR.Task_MultiTaskContinuation_NullTask;
			case ExceptionResource.InvalidOperation_WrongAsyncResultOrEndCalledMultiple:
				return SR.InvalidOperation_WrongAsyncResultOrEndCalledMultiple;
			case ExceptionResource.Task_MultiTaskContinuation_EmptyTaskList:
				return SR.Task_MultiTaskContinuation_EmptyTaskList;
			case ExceptionResource.Task_Start_TaskCompleted:
				return SR.Task_Start_TaskCompleted;
			case ExceptionResource.Task_Start_Promise:
				return SR.Task_Start_Promise;
			case ExceptionResource.Task_Start_ContinuationTask:
				return SR.Task_Start_ContinuationTask;
			case ExceptionResource.Task_Start_AlreadyStarted:
				return SR.Task_Start_AlreadyStarted;
			case ExceptionResource.Task_RunSynchronously_Continuation:
				return SR.Task_RunSynchronously_Continuation;
			case ExceptionResource.Task_RunSynchronously_Promise:
				return SR.Task_RunSynchronously_Promise;
			case ExceptionResource.Task_RunSynchronously_TaskCompleted:
				return SR.Task_RunSynchronously_TaskCompleted;
			case ExceptionResource.Task_RunSynchronously_AlreadyStarted:
				return SR.Task_RunSynchronously_AlreadyStarted;
			case ExceptionResource.AsyncMethodBuilder_InstanceNotInitialized:
				return SR.AsyncMethodBuilder_InstanceNotInitialized;
			case ExceptionResource.Task_ContinueWith_ESandLR:
				return SR.Task_ContinueWith_ESandLR;
			case ExceptionResource.Task_ContinueWith_NotOnAnything:
				return SR.Task_ContinueWith_NotOnAnything;
			case ExceptionResource.Task_Delay_InvalidDelay:
				return SR.Task_Delay_InvalidDelay;
			case ExceptionResource.Task_Delay_InvalidMillisecondsDelay:
				return SR.Task_Delay_InvalidMillisecondsDelay;
			case ExceptionResource.Task_Dispose_NotCompleted:
				return SR.Task_Dispose_NotCompleted;
			case ExceptionResource.Task_ThrowIfDisposed:
				return SR.Task_ThrowIfDisposed;
			case ExceptionResource.Task_WaitMulti_NullTask:
				return SR.Task_WaitMulti_NullTask;
			case ExceptionResource.ArgumentException_OtherNotArrayOfCorrectLength:
				return SR.ArgumentException_OtherNotArrayOfCorrectLength;
			case ExceptionResource.ArgumentNull_Array:
				return SR.ArgumentNull_Array;
			case ExceptionResource.ArgumentNull_SafeHandle:
				return SR.ArgumentNull_SafeHandle;
			case ExceptionResource.ArgumentOutOfRange_EndIndexStartIndex:
				return SR.ArgumentOutOfRange_EndIndexStartIndex;
			case ExceptionResource.ArgumentOutOfRange_Enum:
				return SR.ArgumentOutOfRange_Enum;
			case ExceptionResource.ArgumentOutOfRange_HugeArrayNotSupported:
				return SR.ArgumentOutOfRange_HugeArrayNotSupported;
			case ExceptionResource.Argument_AddingDuplicate:
				return SR.Argument_AddingDuplicate;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				return SR.Argument_InvalidArgumentForComparison;
			case ExceptionResource.Arg_LowerBoundsMustMatch:
				return SR.Arg_LowerBoundsMustMatch;
			case ExceptionResource.Arg_MustBeType:
				return SR.Arg_MustBeType;
			case ExceptionResource.Arg_Need1DArray:
				return SR.Arg_Need1DArray;
			case ExceptionResource.Arg_Need2DArray:
				return SR.Arg_Need2DArray;
			case ExceptionResource.Arg_Need3DArray:
				return SR.Arg_Need3DArray;
			case ExceptionResource.Arg_NeedAtLeast1Rank:
				return SR.Arg_NeedAtLeast1Rank;
			case ExceptionResource.Arg_RankIndices:
				return SR.Arg_RankIndices;
			case ExceptionResource.Arg_RanksAndBounds:
				return SR.Arg_RanksAndBounds;
			case ExceptionResource.InvalidOperation_IComparerFailed:
				return SR.InvalidOperation_IComparerFailed;
			case ExceptionResource.NotSupported_FixedSizeCollection:
				return SR.NotSupported_FixedSizeCollection;
			case ExceptionResource.Rank_MultiDimNotSupported:
				return SR.Rank_MultiDimNotSupported;
			case ExceptionResource.Arg_TypeNotSupported:
				return SR.Arg_TypeNotSupported;
			case ExceptionResource.Argument_SpansMustHaveSameLength:
				return SR.Argument_SpansMustHaveSameLength;
			case ExceptionResource.Argument_InvalidFlag:
				return SR.Argument_InvalidFlag;
			default:
				return "";
			}
		}
	}
}
