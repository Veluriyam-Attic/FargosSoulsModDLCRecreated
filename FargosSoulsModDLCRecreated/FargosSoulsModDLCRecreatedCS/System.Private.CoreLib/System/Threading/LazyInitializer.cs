using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200029F RID: 671
	[NullableContext(1)]
	[Nullable(0)]
	public static class LazyInitializer
	{
		// Token: 0x060027A0 RID: 10144 RVA: 0x0014573F File Offset: 0x0014493F
		public static T EnsureInitialized<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([Nullable(2)] [NotNull] ref T target) where T : class
		{
			T result;
			if ((result = Volatile.Read<T>(ref target)) == null)
			{
				result = LazyInitializer.EnsureInitializedCore<T>(ref target);
			}
			return result;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00145758 File Offset: 0x00144958
		private static T EnsureInitializedCore<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([NotNull] ref T target) where T : class
		{
			try
			{
				Interlocked.CompareExchange<T>(ref target, Activator.CreateInstance<T>(), default(T));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException(SR.Lazy_CreateValue_NoParameterlessCtorForT);
			}
			return target;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x001457A0 File Offset: 0x001449A0
		public static T EnsureInitialized<T>([Nullable(2)] [NotNull] ref T target, Func<T> valueFactory) where T : class
		{
			T result;
			if ((result = Volatile.Read<T>(ref target)) == null)
			{
				result = LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
			}
			return result;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x001457B8 File Offset: 0x001449B8
		private static T EnsureInitializedCore<T>([NotNull] ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException(SR.Lazy_StaticInit_InvalidOperation);
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x001457F6 File Offset: 0x001449F6
		public static T EnsureInitialized<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([AllowNull] ref T target, ref bool initialized, [Nullable(2)] [NotNullIfNotNull("syncLock")] ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00145810 File Offset: 0x00144A10
		private static T EnsureInitializedCore<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>([AllowNull] ref T target, ref bool initialized, [NotNull] ref object syncLock)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					try
					{
						target = Activator.CreateInstance<T>();
					}
					catch (MissingMethodException)
					{
						throw new MissingMemberException(SR.Lazy_CreateValue_NoParameterlessCtorForT);
					}
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00145884 File Offset: 0x00144A84
		public static T EnsureInitialized<[Nullable(2)] T>([AllowNull] ref T target, ref bool initialized, [NotNullIfNotNull("syncLock")] [Nullable(2)] ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x001458A0 File Offset: 0x00144AA0
		private static T EnsureInitializedCore<T>([AllowNull] ref T target, ref bool initialized, [NotNull] ref object syncLock, Func<T> valueFactory)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x001458FC File Offset: 0x00144AFC
		public static T EnsureInitialized<T>([NotNull] [Nullable(2)] ref T target, [NotNullIfNotNull("syncLock")] [Nullable(2)] ref object syncLock, Func<T> valueFactory) where T : class
		{
			T result;
			if ((result = Volatile.Read<T>(ref target)) == null)
			{
				result = LazyInitializer.EnsureInitializedCore<T>(ref target, ref syncLock, valueFactory);
			}
			return result;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x00145918 File Offset: 0x00144B18
		private static T EnsureInitializedCore<T>([NotNull] ref T target, [NotNull] ref object syncLock, Func<T> valueFactory) where T : class
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (Volatile.Read<T>(ref target) == null)
				{
					Volatile.Write<T>(ref target, valueFactory());
					if (target == null)
					{
						throw new InvalidOperationException(SR.Lazy_StaticInit_InvalidOperation);
					}
				}
			}
			return target;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0014598C File Offset: 0x00144B8C
		private static object EnsureLockInitialized([NotNull] ref object syncLock)
		{
			object result;
			if ((result = syncLock) == null)
			{
				result = (Interlocked.CompareExchange(ref syncLock, new object(), null) ?? syncLock);
			}
			return result;
		}
	}
}
