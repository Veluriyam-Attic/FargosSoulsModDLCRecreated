using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Runtime.Remoting;
using System.Threading;

namespace System
{
	// Token: 0x020000BD RID: 189
	[NullableContext(1)]
	[Nullable(0)]
	public static class Activator
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x000C720D File Offset: 0x000C640D
		[NullableContext(2)]
		[DebuggerHidden]
		[DebuggerStepThrough]
		public static object CreateInstance([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] [Nullable(1)] Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
		{
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, null);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000C721B File Offset: 0x000C641B
		[DebuggerStepThrough]
		[DebuggerHidden]
		[NullableContext(2)]
		public static object CreateInstance([Nullable(1)] [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, params object[] args)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, null);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000C722C File Offset: 0x000C642C
		[NullableContext(2)]
		[DebuggerHidden]
		[DebuggerStepThrough]
		public static object CreateInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] [Nullable(1)] Type type, object[] args, object[] activationAttributes)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, args, null, activationAttributes);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000C723D File Offset: 0x000C643D
		[DebuggerHidden]
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public static object CreateInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type type)
		{
			return Activator.CreateInstance(type, false);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000C7246 File Offset: 0x000C6446
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000C7259 File Offset: 0x000C6459
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000C726C File Offset: 0x000C646C
		[NullableContext(2)]
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		public static ObjectHandle CreateInstanceFrom([Nullable(1)] string assemblyFile, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile);
			Type type = assembly.GetType(typeName, true, ignoreCase);
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000C72A8 File Offset: 0x000C64A8
		[NullableContext(2)]
		public static object CreateInstance([Nullable(1)] Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type is TypeBuilder)
			{
				throw new NotSupportedException(SR.NotSupported_CreateInstanceWithTypeBuilder);
			}
			if ((bindingAttr & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingAttr |= (BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance);
			}
			if (activationAttributes != null && activationAttributes.Length != 0)
			{
				throw new PlatformNotSupportedException(SR.NotSupported_ActivAttr);
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return runtimeType.CreateInstanceImpl(bindingAttr, binder, args, culture);
			}
			throw new ArgumentException(SR.Arg_MustBeType, "type");
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000C7328 File Offset: 0x000C6528
		[return: Nullable(2)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstanceInternal(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, null, ref stackCrawlMark);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000C734C File Offset: 0x000C654C
		[NullableContext(2)]
		public static ObjectHandle CreateInstance([Nullable(1)] string assemblyName, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstanceInternal(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, ref stackCrawlMark);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000C7370 File Offset: 0x000C6570
		[return: Nullable(2)]
		public static ObjectHandle CreateInstance(string assemblyName, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Activator.CreateInstanceInternal(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null, activationAttributes, ref stackCrawlMark);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000C7392 File Offset: 0x000C6592
		[return: Nullable(2)]
		public static object CreateInstance([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type type, bool nonPublic)
		{
			return Activator.CreateInstance(type, nonPublic, true);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000C739C File Offset: 0x000C659C
		internal static object CreateInstance(Type type, bool nonPublic, bool wrapExceptions)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return runtimeType.CreateInstanceDefaultCtor(!nonPublic, false, true, wrapExceptions);
			}
			throw new ArgumentException(SR.Arg_MustBeType, "type");
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000C73E4 File Offset: 0x000C65E4
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2057:UnrecognizedReflectionPattern", Justification = "Implementation detail of Activator that linker intrinsically recognizes")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Implementation detail of Activator that linker intrinsically recognizes")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2072:UnrecognizedReflectionPattern", Justification = "Implementation detail of Activator that linker intrinsically recognizes")]
		private static ObjectHandle CreateInstanceInternal(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, ref StackCrawlMark stackMark)
		{
			Type type = null;
			Assembly assembly = null;
			if (assemblyString == null)
			{
				assembly = Assembly.GetExecutingAssembly(ref stackMark);
			}
			else
			{
				AssemblyName assemblyName = new AssemblyName(assemblyString);
				if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
				}
				else
				{
					assembly = RuntimeAssembly.InternalLoad(assemblyName, ref stackMark, AssemblyLoadContext.CurrentContextualReflectionContext);
				}
			}
			if (type == null)
			{
				type = assembly.GetType(typeName, true, ignoreCase);
			}
			object obj = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			if (obj == null)
			{
				return null;
			}
			return new ObjectHandle(obj);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000C7464 File Offset: 0x000C6664
		[Intrinsic]
		public static T CreateInstance<[Nullable(2), DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>()
		{
			return (T)((object)((RuntimeType)typeof(T)).CreateInstanceDefaultCtor(true, true, true, true));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000C7484 File Offset: 0x000C6684
		private static T CreateDefaultInstance<T>() where T : struct
		{
			return default(T);
		}
	}
}
