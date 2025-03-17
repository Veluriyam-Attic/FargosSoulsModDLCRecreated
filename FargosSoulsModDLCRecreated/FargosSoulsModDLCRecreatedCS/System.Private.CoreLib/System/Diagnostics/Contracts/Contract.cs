using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006EB RID: 1771
	[NullableContext(1)]
	[Nullable(0)]
	public static class Contract
	{
		// Token: 0x06005916 RID: 22806 RVA: 0x001B175E File Offset: 0x001B095E
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		public static void Assume([DoesNotReturnIf(false)] bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, null, null, null);
			}
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x001B176C File Offset: 0x001B096C
		[Conditional("CONTRACTS_FULL")]
		[Conditional("DEBUG")]
		[NullableContext(2)]
		public static void Assume([DoesNotReturnIf(false)] bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assume, userMessage, null, null);
			}
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x001B177A File Offset: 0x001B097A
		[Conditional("CONTRACTS_FULL")]
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool condition)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, null, null, null);
			}
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x001B1788 File Offset: 0x001B0988
		[NullableContext(2)]
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		public static void Assert([DoesNotReturnIf(false)] bool condition, string userMessage)
		{
			if (!condition)
			{
				Contract.ReportFailure(ContractFailureKind.Assert, userMessage, null, null);
			}
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x001B1796 File Offset: 0x001B0996
		[Conditional("CONTRACTS_FULL")]
		public static void Requires(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x001B1796 File Offset: 0x001B0996
		[NullableContext(2)]
		[Conditional("CONTRACTS_FULL")]
		public static void Requires(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires");
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x001B17A3 File Offset: 0x001B09A3
		[NullableContext(0)]
		public static void Requires<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x001B17A3 File Offset: 0x001B09A3
		[NullableContext(0)]
		public static void Requires<TException>(bool condition, [Nullable(2)] string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Precondition, "Requires<TException>");
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x001B17B0 File Offset: 0x001B09B0
		[Conditional("CONTRACTS_FULL")]
		public static void Ensures(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x001B17B0 File Offset: 0x001B09B0
		[NullableContext(2)]
		[Conditional("CONTRACTS_FULL")]
		public static void Ensures(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Postcondition, "Ensures");
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x001B17BD File Offset: 0x001B09BD
		[Conditional("CONTRACTS_FULL")]
		[NullableContext(0)]
		public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x001B17BD File Offset: 0x001B09BD
		[NullableContext(0)]
		[Conditional("CONTRACTS_FULL")]
		public static void EnsuresOnThrow<TException>(bool condition, [Nullable(2)] string userMessage) where TException : Exception
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.PostconditionOnException, "EnsuresOnThrow");
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x000C7484 File Offset: 0x000C6684
		public static T Result<[Nullable(2)] T>()
		{
			return default(T);
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x001B17CA File Offset: 0x001B09CA
		public static T ValueAtReturn<[Nullable(2)] T>(out T value)
		{
			value = default(T);
			return value;
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x000C7484 File Offset: 0x000C6684
		public static T OldValue<[Nullable(2)] T>(T value)
		{
			return default(T);
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x001B17D9 File Offset: 0x001B09D9
		[Conditional("CONTRACTS_FULL")]
		public static void Invariant(bool condition)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x001B17D9 File Offset: 0x001B09D9
		[NullableContext(2)]
		[Conditional("CONTRACTS_FULL")]
		public static void Invariant(bool condition, string userMessage)
		{
			Contract.AssertMustUseRewriter(ContractFailureKind.Invariant, "Invariant");
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x001B17E8 File Offset: 0x001B09E8
		public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException(SR.Argument_ToExclusiveLessThanFromExclusive);
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (!predicate(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x001B182C File Offset: 0x001B0A2C
		public static bool ForAll<[Nullable(2)] T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T obj in collection)
			{
				if (!predicate(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x001B189C File Offset: 0x001B0A9C
		public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
		{
			if (fromInclusive > toExclusive)
			{
				throw new ArgumentException(SR.Argument_ToExclusiveLessThanFromExclusive);
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				if (predicate(i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x001B18E0 File Offset: 0x001B0AE0
		public static bool Exists<[Nullable(2)] T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T obj in collection)
			{
				if (predicate(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Conditional("CONTRACTS_FULL")]
		public static void EndContractBlock()
		{
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x001B1950 File Offset: 0x001B0B50
		private static void AssertMustUseRewriter(ContractFailureKind kind, string contractKind)
		{
			Assembly assembly = typeof(Contract).Assembly;
			StackTrace stackTrace = new StackTrace();
			Assembly assembly2 = null;
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				MethodBase method = stackTrace.GetFrame(i).GetMethod();
				Assembly assembly3;
				if (method == null)
				{
					assembly3 = null;
				}
				else
				{
					Type declaringType = method.DeclaringType;
					assembly3 = ((declaringType != null) ? declaringType.Assembly : null);
				}
				Assembly assembly4 = assembly3;
				if (assembly4 != null && assembly4 != assembly)
				{
					assembly2 = assembly4;
					break;
				}
			}
			if (assembly2 == null)
			{
				assembly2 = assembly;
			}
			string name = assembly2.GetName().Name;
			ContractHelper.TriggerFailure(kind, SR.Format(SR.MustUseCCRewrite, contractKind, name), null, null, null);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x001B19F0 File Offset: 0x001B0BF0
		[DebuggerNonUserCode]
		private static void ReportFailure(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, failureKind), "failureKind");
			}
			string text = ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);
			if (text == null)
			{
				return;
			}
			ContractHelper.TriggerFailure(failureKind, text, userMessage, conditionText, innerException);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600592E RID: 22830 RVA: 0x001B1A38 File Offset: 0x001B0C38
		// (remove) Token: 0x0600592F RID: 22831 RVA: 0x001B1A40 File Offset: 0x001B0C40
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public static event EventHandler<ContractFailedEventArgs> ContractFailed
		{
			add
			{
				ContractHelper.InternalContractFailed += value;
			}
			remove
			{
				ContractHelper.InternalContractFailed -= value;
			}
		}
	}
}
