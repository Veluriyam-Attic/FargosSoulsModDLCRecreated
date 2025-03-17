using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200008B RID: 139
	internal sealed class RuntimeType : TypeInfo, ICloneable
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x000B9813 File Offset: 0x000B8A13
		internal static RuntimeType GetType(string typeName, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			return RuntimeTypeHandle.GetTypeByName(typeName, throwOnError, ignoreCase, ref stackMark);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000B982C File Offset: 0x000B8A2C
		internal static MethodBase GetMethodBase(RuntimeModule scope, int typeMetadataToken)
		{
			return RuntimeType.GetMethodBase(ModuleHandle.ResolveMethodHandleInternal(scope, typeMetadataToken));
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000B983A File Offset: 0x000B8A3A
		internal static MethodBase GetMethodBase(IRuntimeMethodInfo methodHandle)
		{
			return RuntimeType.GetMethodBase(null, methodHandle);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000B9844 File Offset: 0x000B8A44
		internal static MethodBase GetMethodBase(RuntimeType reflectedType, IRuntimeMethodInfo methodHandle)
		{
			MethodBase methodBase = RuntimeType.GetMethodBase(reflectedType, methodHandle.Value);
			GC.KeepAlive(methodHandle);
			return methodBase;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000B9868 File Offset: 0x000B8A68
		internal static MethodBase GetMethodBase(RuntimeType reflectedType, RuntimeMethodHandleInternal methodHandle)
		{
			if (!RuntimeMethodHandle.IsDynamicMethod(methodHandle))
			{
				RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
				RuntimeType[] array = null;
				if (reflectedType == null)
				{
					reflectedType = runtimeType;
				}
				if (reflectedType != runtimeType && !reflectedType.IsSubclassOf(runtimeType))
				{
					if (reflectedType.IsArray)
					{
						MethodBase[] array2 = reflectedType.GetMember(RuntimeMethodHandle.GetName(methodHandle), MemberTypes.Constructor | MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) as MethodBase[];
						bool flag = false;
						foreach (IRuntimeMethodInfo runtimeMethodInfo in array2)
						{
							if (runtimeMethodInfo.Value.Value == methodHandle.Value)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							throw new ArgumentException(SR.Format(SR.Argument_ResolveMethodHandle, reflectedType, runtimeType));
						}
					}
					else if (runtimeType.IsGenericType)
					{
						RuntimeType right = (RuntimeType)runtimeType.GetGenericTypeDefinition();
						RuntimeType runtimeType2 = reflectedType;
						while (runtimeType2 != null)
						{
							RuntimeType runtimeType3 = runtimeType2;
							if (runtimeType3.IsGenericType && !runtimeType2.IsGenericTypeDefinition)
							{
								runtimeType3 = (RuntimeType)runtimeType3.GetGenericTypeDefinition();
							}
							if (runtimeType3 == right)
							{
								break;
							}
							runtimeType2 = runtimeType2.GetBaseType();
						}
						if (runtimeType2 == null)
						{
							throw new ArgumentException(SR.Format(SR.Argument_ResolveMethodHandle, reflectedType, runtimeType));
						}
						runtimeType = runtimeType2;
						if (!RuntimeMethodHandle.IsGenericMethodDefinition(methodHandle))
						{
							array = RuntimeMethodHandle.GetMethodInstantiationInternal(methodHandle);
						}
						methodHandle = RuntimeMethodHandle.GetMethodFromCanonical(methodHandle, runtimeType);
					}
					else if (!runtimeType.IsAssignableFrom(reflectedType))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.Argument_ResolveMethodHandle, reflectedType.ToString(), runtimeType.ToString()));
					}
				}
				methodHandle = RuntimeMethodHandle.GetStubIfNeeded(methodHandle, runtimeType, array);
				MethodBase result;
				if (RuntimeMethodHandle.IsConstructor(methodHandle))
				{
					result = reflectedType.Cache.GetConstructor(runtimeType, methodHandle);
				}
				else if (RuntimeMethodHandle.HasMethodInstantiation(methodHandle) && !RuntimeMethodHandle.IsGenericMethodDefinition(methodHandle))
				{
					result = reflectedType.Cache.GetGenericMethodInfo(methodHandle);
				}
				else
				{
					result = reflectedType.Cache.GetMethod(runtimeType, methodHandle);
				}
				GC.KeepAlive(array);
				return result;
			}
			Resolver resolver = RuntimeMethodHandle.GetResolver(methodHandle);
			if (resolver != null)
			{
				return resolver.GetDynamicMethod();
			}
			return null;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000B9A4A File Offset: 0x000B8C4A
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000B9A5D File Offset: 0x000B8C5D
		internal object GenericCache
		{
			get
			{
				RuntimeType.RuntimeTypeCache cacheIfExists = this.CacheIfExists;
				if (cacheIfExists == null)
				{
					return null;
				}
				return cacheIfExists.GenericCache;
			}
			set
			{
				this.Cache.GenericCache = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000B9A6B File Offset: 0x000B8C6B
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000B9A78 File Offset: 0x000B8C78
		internal bool DomainInitialized
		{
			get
			{
				return this.Cache.DomainInitialized;
			}
			set
			{
				this.Cache.DomainInitialized = value;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000B9A86 File Offset: 0x000B8C86
		internal static FieldInfo GetFieldInfo(IRuntimeFieldInfo fieldHandle)
		{
			return RuntimeType.GetFieldInfo(RuntimeFieldHandle.GetApproxDeclaringType(fieldHandle), fieldHandle);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000B9A94 File Offset: 0x000B8C94
		internal static FieldInfo GetFieldInfo(RuntimeType reflectedType, IRuntimeFieldInfo field)
		{
			RuntimeFieldHandleInternal value = field.Value;
			if (reflectedType == null)
			{
				reflectedType = RuntimeFieldHandle.GetApproxDeclaringType(value);
			}
			else
			{
				RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(value);
				if (reflectedType != approxDeclaringType && (!RuntimeFieldHandle.AcquiresContextFromThis(value) || !RuntimeTypeHandle.CompareCanonicalHandles(approxDeclaringType, reflectedType)))
				{
					throw new ArgumentException(SR.Format(SR.Argument_ResolveFieldHandle, reflectedType, approxDeclaringType));
				}
			}
			FieldInfo field2 = reflectedType.Cache.GetField(value);
			GC.KeepAlive(field);
			return field2;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000B9B04 File Offset: 0x000B8D04
		private static PropertyInfo GetPropertyInfo(RuntimeType reflectedType, int tkProperty)
		{
			foreach (RuntimePropertyInfo runtimePropertyInfo in reflectedType.Cache.GetPropertyList(RuntimeType.MemberListType.All, null))
			{
				if (runtimePropertyInfo.MetadataToken == tkProperty)
				{
					return runtimePropertyInfo;
				}
			}
			throw new SystemException();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000B9B41 File Offset: 0x000B8D41
		private static void ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
		{
			if (type.IsPointer || type.IsByRef || type == typeof(void))
			{
				throw new ArgumentException(SR.Format(SR.Argument_NeverValidGenericArgument, type));
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000B9B78 File Offset: 0x000B8D78
		internal static void SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
		{
			if (genericArguments == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType.ThrowIfTypeNeverValidGenericArgument(genericArguments[i]);
			}
			if (genericArguments.Length != genericParamters.Length)
			{
				throw new ArgumentException(SR.Format(SR.Argument_NotEnoughGenArguments, genericArguments.Length, genericParamters.Length));
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000B9BE0 File Offset: 0x000B8DE0
		internal static void ValidateGenericArguments(MemberInfo definition, RuntimeType[] genericArguments, Exception e)
		{
			RuntimeType[] typeContext = null;
			RuntimeType[] methodContext = null;
			RuntimeType[] genericArgumentsInternal;
			if (definition is Type)
			{
				RuntimeType runtimeType = (RuntimeType)definition;
				genericArgumentsInternal = runtimeType.GetGenericArgumentsInternal();
				typeContext = genericArguments;
			}
			else
			{
				RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)definition;
				genericArgumentsInternal = runtimeMethodInfo.GetGenericArgumentsInternal();
				methodContext = genericArguments;
				RuntimeType runtimeType2 = (RuntimeType)runtimeMethodInfo.DeclaringType;
				if (runtimeType2 != null)
				{
					typeContext = runtimeType2.GetTypeHandleInternal().GetInstantiationInternal();
				}
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type = genericArguments[i];
				Type type2 = genericArgumentsInternal[i];
				if (!RuntimeTypeHandle.SatisfiesConstraints(type2.GetTypeHandleInternal().GetTypeChecked(), typeContext, methodContext, type.GetTypeHandleInternal().GetTypeChecked()))
				{
					throw new ArgumentException(SR.Format(SR.Argument_GenConstraintViolation, new object[]
					{
						i.ToString(),
						type,
						definition,
						type2
					}), e);
				}
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000B9CBC File Offset: 0x000B8EBC
		private static void SplitName(string fullname, out string name, out string ns)
		{
			name = null;
			ns = null;
			if (fullname == null)
			{
				return;
			}
			int num = fullname.LastIndexOf(".", StringComparison.Ordinal);
			if (num == -1)
			{
				name = fullname;
				return;
			}
			ns = fullname.Substring(0, num);
			int num2 = fullname.Length - ns.Length - 1;
			if (num2 != 0)
			{
				name = fullname.Substring(num + 1, num2);
				return;
			}
			name = "";
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000B9D1C File Offset: 0x000B8F1C
		internal static BindingFlags FilterPreCalculate(bool isPublic, bool isInherited, bool isStatic)
		{
			BindingFlags bindingFlags = isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
			if (isInherited)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
				if (isStatic)
				{
					bindingFlags |= (BindingFlags.Static | BindingFlags.FlattenHierarchy);
				}
				else
				{
					bindingFlags |= BindingFlags.Instance;
				}
			}
			else if (isStatic)
			{
				bindingFlags |= BindingFlags.Static;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			return bindingFlags;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000B9D58 File Offset: 0x000B8F58
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, bool allowPrefixLookup, out bool prefixLookup, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			prefixLookup = false;
			ignoreCase = false;
			if (name != null)
			{
				if ((bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default)
				{
					name = name.ToLowerInvariant();
					ignoreCase = true;
					listType = RuntimeType.MemberListType.CaseInsensitive;
				}
				else
				{
					listType = RuntimeType.MemberListType.CaseSensitive;
				}
				if (allowPrefixLookup && name.EndsWith("*", StringComparison.Ordinal))
				{
					string text = name;
					int length = text.Length;
					int num = 0;
					int length2 = length - 1 - num;
					name = text.Substring(num, length2);
					prefixLookup = true;
					listType = RuntimeType.MemberListType.All;
					return;
				}
			}
			else
			{
				listType = RuntimeType.MemberListType.All;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000B9DC4 File Offset: 0x000B8FC4
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			bool flag;
			RuntimeType.FilterHelper(bindingFlags, ref name, false, out flag, out ignoreCase, out listType);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000B9DDD File Offset: 0x000B8FDD
		private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				if (!memberInfo.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000B9E08 File Offset: 0x000B9008
		private static bool FilterApplyBase(MemberInfo memberInfo, BindingFlags bindingFlags, bool isPublic, bool isNonProtectedInternal, bool isStatic, string name, bool prefixLookup)
		{
			if (isPublic)
			{
				if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
				{
					return false;
				}
			}
			else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
			{
				return false;
			}
			bool flag = memberInfo.DeclaringType != memberInfo.ReflectedType;
			if ((bindingFlags & BindingFlags.DeclaredOnly) > BindingFlags.Default && flag)
			{
				return false;
			}
			if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
			{
				if (isStatic)
				{
					if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default && flag)
					{
						return false;
					}
					if ((bindingFlags & BindingFlags.Static) == BindingFlags.Default)
					{
						return false;
					}
				}
				else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
				{
					return false;
				}
			}
			if (prefixLookup && !RuntimeType.FilterApplyPrefixLookup(memberInfo, name, (bindingFlags & BindingFlags.IgnoreCase) > BindingFlags.Default))
			{
				return false;
			}
			if ((bindingFlags & BindingFlags.DeclaredOnly) == BindingFlags.Default && flag && isNonProtectedInternal && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.Default && !isStatic && (bindingFlags & BindingFlags.Instance) != BindingFlags.Default)
			{
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo == null)
				{
					return false;
				}
				if (!methodInfo.IsVirtual && !methodInfo.IsAbstract)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000B9ED4 File Offset: 0x000B90D4
		private static bool FilterApplyType(Type type, BindingFlags bindingFlags, string name, bool prefixLookup, string ns)
		{
			bool isPublic = type.IsNestedPublic || type.IsPublic;
			return RuntimeType.FilterApplyBase(type, bindingFlags, isPublic, type.IsNestedAssembly, false, name, prefixLookup) && (ns == null || !(ns != type.Namespace));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000B9F1E File Offset: 0x000B911E
		private static bool FilterApplyMethodInfo(RuntimeMethodInfo method, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(method, method.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000B9F2F File Offset: 0x000B912F
		private static bool FilterApplyConstructorInfo(RuntimeConstructorInfo constructor, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(constructor, constructor.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000B9F40 File Offset: 0x000B9140
		private static bool FilterApplyMethodBase(MethodBase methodBase, BindingFlags methodFlags, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			bindingFlags ^= BindingFlags.DeclaredOnly;
			if ((bindingFlags & methodFlags) != methodFlags)
			{
				return false;
			}
			if ((callConv & CallingConventions.Any) == (CallingConventions)0)
			{
				if ((callConv & CallingConventions.VarArgs) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
				{
					return false;
				}
				if ((callConv & CallingConventions.Standard) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.Standard) == (CallingConventions)0)
				{
					return false;
				}
			}
			if (argumentTypes != null)
			{
				ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
				if (argumentTypes.Length != parametersNoCopy.Length)
				{
					if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
					{
						return false;
					}
					bool flag = false;
					bool flag2 = argumentTypes.Length > parametersNoCopy.Length;
					if (flag2)
					{
						if ((methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
						{
							flag = true;
						}
					}
					else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						flag = true;
					}
					else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
					{
						flag = true;
					}
					if (flag)
					{
						if (parametersNoCopy.Length == 0)
						{
							return false;
						}
						bool flag3 = argumentTypes.Length < parametersNoCopy.Length - 1;
						if (flag3)
						{
							return false;
						}
						ParameterInfo[] array = parametersNoCopy;
						ParameterInfo parameterInfo = array[array.Length - 1];
						if (!parameterInfo.ParameterType.IsArray)
						{
							return false;
						}
						if (!parameterInfo.IsDefined(typeof(ParamArrayAttribute), false))
						{
							return false;
						}
					}
				}
				else if ((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default && (bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default)
				{
					for (int i = 0; i < parametersNoCopy.Length; i++)
					{
						if (argumentTypes[i] != null && !argumentTypes[i].MatchesParameterTypeExactly(parametersNoCopy[i]))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000BA069 File Offset: 0x000B9269
		internal RuntimeType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000BA076 File Offset: 0x000B9276
		internal IntPtr GetUnderlyingNativeHandle()
		{
			return this.m_handle;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000BA080 File Offset: 0x000B9280
		internal override bool CacheEquals(object o)
		{
			RuntimeType runtimeType = o as RuntimeType;
			return runtimeType != null && runtimeType.m_handle == this.m_handle;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000BA0AC File Offset: 0x000B92AC
		private RuntimeType.RuntimeTypeCache CacheIfExists
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.m_cache != IntPtr.Zero)
				{
					object value = GCHandle.InternalGet(this.m_cache);
					return Unsafe.As<RuntimeType.RuntimeTypeCache>(value);
				}
				return null;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000BA0E0 File Offset: 0x000B92E0
		private RuntimeType.RuntimeTypeCache Cache
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this.m_cache != IntPtr.Zero)
				{
					object obj = GCHandle.InternalGet(this.m_cache);
					if (obj != null)
					{
						return Unsafe.As<RuntimeType.RuntimeTypeCache>(obj);
					}
				}
				return this.InitializeCache();
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000BA11C File Offset: 0x000B931C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeType.RuntimeTypeCache InitializeCache()
		{
			if (this.m_cache == IntPtr.Zero)
			{
				RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(this);
				IntPtr gchandle = runtimeTypeHandle.GetGCHandle(GCHandleType.WeakTrackResurrection);
				IntPtr value = Interlocked.CompareExchange(ref this.m_cache, gchandle, IntPtr.Zero);
				if (value != IntPtr.Zero)
				{
					runtimeTypeHandle.FreeGCHandle(gchandle);
				}
			}
			RuntimeType.RuntimeTypeCache runtimeTypeCache = (RuntimeType.RuntimeTypeCache)GCHandle.InternalGet(this.m_cache);
			if (runtimeTypeCache == null)
			{
				runtimeTypeCache = new RuntimeType.RuntimeTypeCache(this);
				RuntimeType.RuntimeTypeCache runtimeTypeCache2 = (RuntimeType.RuntimeTypeCache)GCHandle.InternalCompareExchange(this.m_cache, runtimeTypeCache, null);
				if (runtimeTypeCache2 != null)
				{
					runtimeTypeCache = runtimeTypeCache2;
				}
			}
			return runtimeTypeCache;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000BA1AB File Offset: 0x000B93AB
		private string GetDefaultMemberName()
		{
			return this.Cache.GetDefaultMemberName();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000BA1B8 File Offset: 0x000B93B8
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, int genericParameterCount, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeMethodInfo[] methodList = this.Cache.GetMethodList(listType, name);
			RuntimeType.ListBuilder<MethodInfo> result = new RuntimeType.ListBuilder<MethodInfo>(methodList.Length);
			foreach (RuntimeMethodInfo runtimeMethodInfo in methodList)
			{
				if ((genericParameterCount == -1 || genericParameterCount == runtimeMethodInfo.GenericParameterCount) && RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, ignoreCase)))
				{
					result.Add(runtimeMethodInfo);
				}
			}
			return result;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000BA23C File Offset: 0x000B943C
		private RuntimeType.ListBuilder<ConstructorInfo> GetConstructorCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeConstructorInfo[] constructorList = this.Cache.GetConstructorList(listType, name);
			RuntimeType.ListBuilder<ConstructorInfo> result = new RuntimeType.ListBuilder<ConstructorInfo>(constructorList.Length);
			foreach (RuntimeConstructorInfo runtimeConstructorInfo in constructorList)
			{
				if (RuntimeType.FilterApplyConstructorInfo(runtimeConstructorInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeConstructorInfo, name, ignoreCase)))
				{
					result.Add(runtimeConstructorInfo);
				}
			}
			return result;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000BA2B0 File Offset: 0x000B94B0
		private RuntimeType.ListBuilder<PropertyInfo> GetPropertyCandidates(string name, BindingFlags bindingAttr, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimePropertyInfo[] propertyList = this.Cache.GetPropertyList(listType, name);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<PropertyInfo> result = new RuntimeType.ListBuilder<PropertyInfo>(propertyList.Length);
			foreach (RuntimePropertyInfo runtimePropertyInfo in propertyList)
			{
				if ((bindingAttr & runtimePropertyInfo.BindingFlags) == runtimePropertyInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimePropertyInfo, name, ignoreCase)) && (types == null || runtimePropertyInfo.GetIndexParameters().Length == types.Length))
				{
					result.Add(runtimePropertyInfo);
				}
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000BA340 File Offset: 0x000B9540
		private RuntimeType.ListBuilder<EventInfo> GetEventCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeEventInfo[] eventList = this.Cache.GetEventList(listType, name);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<EventInfo> result = new RuntimeType.ListBuilder<EventInfo>(eventList.Length);
			foreach (RuntimeEventInfo runtimeEventInfo in eventList)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeEventInfo, name, ignoreCase)))
				{
					result.Add(runtimeEventInfo);
				}
			}
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000BA3BC File Offset: 0x000B95BC
		private RuntimeType.ListBuilder<FieldInfo> GetFieldCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeFieldInfo[] fieldList = this.Cache.GetFieldList(listType, name);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<FieldInfo> result = new RuntimeType.ListBuilder<FieldInfo>(fieldList.Length);
			foreach (RuntimeFieldInfo runtimeFieldInfo in fieldList)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeFieldInfo, name, ignoreCase)))
				{
					result.Add(runtimeFieldInfo);
				}
			}
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000BA438 File Offset: 0x000B9638
		private RuntimeType.ListBuilder<Type> GetNestedTypeCandidates(string fullname, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bindingAttr &= ~BindingFlags.Static;
			string name;
			string ns;
			RuntimeType.SplitName(fullname, out name, out ns);
			bool prefixLookup;
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out prefixLookup, out flag, out listType);
			RuntimeType[] nestedTypeList = this.Cache.GetNestedTypeList(listType, name);
			RuntimeType.ListBuilder<Type> result = new RuntimeType.ListBuilder<Type>(nestedTypeList.Length);
			foreach (RuntimeType runtimeType in nestedTypeList)
			{
				if (RuntimeType.FilterApplyType(runtimeType, bindingAttr, name, prefixLookup, ns))
				{
					result.Add(runtimeType);
				}
			}
			return result;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000BA4B0 File Offset: 0x000B96B0
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodCandidates(null, -1, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000BA4D4 File Offset: 0x000B96D4
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000BA4F4 File Offset: 0x000B96F4
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertyCandidates(null, bindingAttr, null, false).ToArray();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000BA514 File Offset: 0x000B9714
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEventCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000BA534 File Offset: 0x000B9734
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFieldCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000BA554 File Offset: 0x000B9754
		public override Type[] GetInterfaces()
		{
			RuntimeType[] interfaceList = this.Cache.GetInterfaceList(RuntimeType.MemberListType.All, null);
			Type[] array = interfaceList;
			return new ReadOnlySpan<Type>(array).ToArray();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000BA580 File Offset: 0x000B9780
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.GetNestedTypeCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000BA5A0 File Offset: 0x000B97A0
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(null, -1, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(null, bindingAttr, null, false);
			RuntimeType.ListBuilder<EventInfo> eventCandidates = this.GetEventCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<FieldInfo> fieldCandidates = this.GetFieldCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<Type> nestedTypeCandidates = this.GetNestedTypeCandidates(null, bindingAttr, false);
			MemberInfo[] array = new MemberInfo[methodCandidates.Count + constructorCandidates.Count + propertyCandidates.Count + eventCandidates.Count + fieldCandidates.Count + nestedTypeCandidates.Count];
			int num = 0;
			object[] array2 = array;
			methodCandidates.CopyTo(array2, num);
			num += methodCandidates.Count;
			array2 = array;
			constructorCandidates.CopyTo(array2, num);
			num += constructorCandidates.Count;
			array2 = array;
			propertyCandidates.CopyTo(array2, num);
			num += propertyCandidates.Count;
			array2 = array;
			eventCandidates.CopyTo(array2, num);
			num += eventCandidates.Count;
			array2 = array;
			fieldCandidates.CopyTo(array2, num);
			num += fieldCandidates.Count;
			array2 = array;
			nestedTypeCandidates.CopyTo(array2, num);
			num += nestedTypeCandidates.Count;
			return array;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000BA6D0 File Offset: 0x000B98D0
		public override InterfaceMapping GetInterfaceMap(Type ifaceType)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(SR.Arg_GenericParameter);
			}
			if (ifaceType == null)
			{
				throw new ArgumentNullException("ifaceType");
			}
			RuntimeType runtimeType = ifaceType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType, "ifaceType");
			}
			RuntimeTypeHandle typeHandleInternal = runtimeType.GetTypeHandleInternal();
			this.GetTypeHandleInternal().VerifyInterfaceIsImplemented(typeHandleInternal);
			if (this.IsSZArray && ifaceType.IsGenericType)
			{
				throw new ArgumentException(SR.Argument_ArrayGetInterfaceMap);
			}
			int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
			InterfaceMapping interfaceMapping;
			interfaceMapping.InterfaceType = ifaceType;
			interfaceMapping.TargetType = this;
			interfaceMapping.InterfaceMethods = new MethodInfo[numVirtuals];
			interfaceMapping.TargetMethods = new MethodInfo[numVirtuals];
			for (int i = 0; i < numVirtuals; i++)
			{
				RuntimeMethodHandleInternal methodAt = RuntimeTypeHandle.GetMethodAt(runtimeType, i);
				MethodBase methodBase = RuntimeType.GetMethodBase(runtimeType, methodAt);
				interfaceMapping.InterfaceMethods[i] = (MethodInfo)methodBase;
				RuntimeMethodHandleInternal interfaceMethodImplementation = this.GetTypeHandleInternal().GetInterfaceMethodImplementation(typeHandleInternal, methodAt);
				if (!interfaceMethodImplementation.IsNullHandle())
				{
					RuntimeType runtimeType2 = RuntimeMethodHandle.GetDeclaringType(interfaceMethodImplementation);
					if (!runtimeType2.IsInterface)
					{
						runtimeType2 = this;
					}
					MethodBase methodBase2 = RuntimeType.GetMethodBase(runtimeType2, interfaceMethodImplementation);
					interfaceMapping.TargetMethods[i] = (MethodInfo)methodBase2;
				}
			}
			return interfaceMapping;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000BA803 File Offset: 0x000B9A03
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImplCommon(name, -1, bindingAttr, binder, callConv, types, modifiers);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000BA815 File Offset: 0x000B9A15
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
		protected override MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImplCommon(name, genericParameterCount, bindingAttr, binder, callConv, types, modifiers);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000BA828 File Offset: 0x000B9A28
		private MethodInfo GetMethodImplCommon(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(name, genericParameterCount, bindingAttr, callConv, types, false);
			if (methodCandidates.Count == 0)
			{
				return null;
			}
			MethodBase[] match;
			if (types == null || types.Length == 0)
			{
				MethodInfo methodInfo = methodCandidates[0];
				if (methodCandidates.Count == 1)
				{
					return methodInfo;
				}
				if (types == null)
				{
					for (int i = 1; i < methodCandidates.Count; i++)
					{
						MethodInfo m = methodCandidates[i];
						if (!System.DefaultBinder.CompareMethodSig(m, methodInfo))
						{
							throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
						}
					}
					match = methodCandidates.ToArray();
					return System.DefaultBinder.FindMostDerivedNewSlotMeth(match, methodCandidates.Count) as MethodInfo;
				}
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			match = methodCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, match, types, modifiers) as MethodInfo;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000BA8E4 File Offset: 0x000B9AE4
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, types, false);
			if (constructorCandidates.Count == 0)
			{
				return null;
			}
			if (types.Length == 0 && constructorCandidates.Count == 1)
			{
				ConstructorInfo constructorInfo = constructorCandidates[0];
				ParameterInfo[] parametersNoCopy = constructorInfo.GetParametersNoCopy();
				if (parametersNoCopy == null || parametersNoCopy.Length == 0)
				{
					return constructorInfo;
				}
			}
			MethodBase[] match;
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				match = constructorCandidates.ToArray();
				return System.DefaultBinder.ExactBinding(match, types, modifiers) as ConstructorInfo;
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			match = constructorCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, match, types, modifiers) as ConstructorInfo;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000BA978 File Offset: 0x000B9B78
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(name, bindingAttr, types, false);
			if (propertyCandidates.Count == 0)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				if (propertyCandidates.Count == 1)
				{
					PropertyInfo propertyInfo = propertyCandidates[0];
					if (returnType != null && !returnType.IsEquivalentTo(propertyInfo.PropertyType))
					{
						return null;
					}
					return propertyInfo;
				}
				else if (returnType == null)
				{
					throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
				}
			}
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				return System.DefaultBinder.ExactPropertyBinding(propertyCandidates.ToArray(), returnType, types, modifiers);
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertyCandidates.ToArray(), returnType, types, modifiers);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000BAA28 File Offset: 0x000B9C28
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out listType);
			RuntimeEventInfo[] eventList = this.Cache.GetEventList(listType, name);
			EventInfo eventInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			foreach (RuntimeEventInfo runtimeEventInfo in eventList)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags)
				{
					if (eventInfo != null)
					{
						throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
					}
					eventInfo = runtimeEventInfo;
				}
			}
			return eventInfo;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000BAAA8 File Offset: 0x000B9CA8
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out listType);
			RuntimeFieldInfo[] fieldList = this.Cache.GetFieldList(listType, name);
			FieldInfo fieldInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			bool flag2 = false;
			foreach (RuntimeFieldInfo runtimeFieldInfo in fieldList)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags)
				{
					if (fieldInfo != null)
					{
						if (runtimeFieldInfo.DeclaringType == fieldInfo.DeclaringType)
						{
							throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
						}
						if (fieldInfo.DeclaringType.IsInterface && runtimeFieldInfo.DeclaringType.IsInterface)
						{
							flag2 = true;
						}
					}
					if (fieldInfo == null || runtimeFieldInfo.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) || fieldInfo.DeclaringType.IsInterface)
					{
						fieldInfo = runtimeFieldInfo;
					}
				}
			}
			if (flag2 && fieldInfo.DeclaringType.IsInterface)
			{
				throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
			}
			return fieldInfo;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000BAB9C File Offset: 0x000B9D9C
		public override Type GetInterface(string fullname, bool ignoreCase)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			bindingFlags &= ~BindingFlags.Static;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			string name;
			string ns;
			RuntimeType.SplitName(fullname, out name, out ns);
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingFlags, ref name, out flag, out listType);
			RuntimeType[] interfaceList = this.Cache.GetInterfaceList(listType, name);
			RuntimeType runtimeType = null;
			foreach (RuntimeType runtimeType2 in interfaceList)
			{
				if (RuntimeType.FilterApplyType(runtimeType2, bindingFlags, name, false, ns))
				{
					if (runtimeType != null)
					{
						throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
					}
					runtimeType = runtimeType2;
				}
			}
			return runtimeType;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000BAC30 File Offset: 0x000B9E30
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
		public override Type GetNestedType(string fullname, BindingFlags bindingAttr)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			bindingAttr &= ~BindingFlags.Static;
			string name;
			string ns;
			RuntimeType.SplitName(fullname, out name, out ns);
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out listType);
			RuntimeType[] nestedTypeList = this.Cache.GetNestedTypeList(listType, name);
			RuntimeType runtimeType = null;
			foreach (RuntimeType runtimeType2 in nestedTypeList)
			{
				if (RuntimeType.FilterApplyType(runtimeType2, bindingAttr, name, false, ns))
				{
					if (runtimeType != null)
					{
						throw new AmbiguousMatchException(SR.Arg_AmbiguousMatchException);
					}
					runtimeType = runtimeType2;
				}
			}
			return runtimeType;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000BACB8 File Offset: 0x000B9EB8
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			RuntimeType.ListBuilder<MethodInfo> listBuilder = default(RuntimeType.ListBuilder<MethodInfo>);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder2 = default(RuntimeType.ListBuilder<ConstructorInfo>);
			RuntimeType.ListBuilder<PropertyInfo> listBuilder3 = default(RuntimeType.ListBuilder<PropertyInfo>);
			RuntimeType.ListBuilder<EventInfo> listBuilder4 = default(RuntimeType.ListBuilder<EventInfo>);
			RuntimeType.ListBuilder<FieldInfo> listBuilder5 = default(RuntimeType.ListBuilder<FieldInfo>);
			RuntimeType.ListBuilder<Type> listBuilder6 = default(RuntimeType.ListBuilder<Type>);
			int num = 0;
			if ((type & MemberTypes.Method) != (MemberTypes)0)
			{
				listBuilder = this.GetMethodCandidates(name, -1, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Method)
				{
					return listBuilder.ToArray();
				}
				num += listBuilder.Count;
			}
			if ((type & MemberTypes.Constructor) != (MemberTypes)0)
			{
				listBuilder2 = this.GetConstructorCandidates(name, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Constructor)
				{
					return listBuilder2.ToArray();
				}
				num += listBuilder2.Count;
			}
			if ((type & MemberTypes.Property) != (MemberTypes)0)
			{
				listBuilder3 = this.GetPropertyCandidates(name, bindingAttr, null, true);
				if (type == MemberTypes.Property)
				{
					return listBuilder3.ToArray();
				}
				num += listBuilder3.Count;
			}
			if ((type & MemberTypes.Event) != (MemberTypes)0)
			{
				listBuilder4 = this.GetEventCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Event)
				{
					return listBuilder4.ToArray();
				}
				num += listBuilder4.Count;
			}
			if ((type & MemberTypes.Field) != (MemberTypes)0)
			{
				listBuilder5 = this.GetFieldCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Field)
				{
					return listBuilder5.ToArray();
				}
				num += listBuilder5.Count;
			}
			if ((type & (MemberTypes.TypeInfo | MemberTypes.NestedType)) != (MemberTypes)0)
			{
				listBuilder6 = this.GetNestedTypeCandidates(name, bindingAttr, true);
				if (type == MemberTypes.NestedType || type == MemberTypes.TypeInfo)
				{
					return listBuilder6.ToArray();
				}
				num += listBuilder6.Count;
			}
			MemberInfo[] array;
			if (type != (MemberTypes.Constructor | MemberTypes.Method))
			{
				array = new MemberInfo[num];
			}
			else
			{
				MemberInfo[] array2 = new MethodBase[num];
				array = array2;
			}
			MemberInfo[] array3 = array;
			int num2 = 0;
			object[] array4 = array3;
			listBuilder.CopyTo(array4, num2);
			num2 += listBuilder.Count;
			array4 = array3;
			listBuilder2.CopyTo(array4, num2);
			num2 += listBuilder2.Count;
			array4 = array3;
			listBuilder3.CopyTo(array4, num2);
			num2 += listBuilder3.Count;
			array4 = array3;
			listBuilder4.CopyTo(array4, num2);
			num2 += listBuilder4.Count;
			array4 = array3;
			listBuilder5.CopyTo(array4, num2);
			num2 += listBuilder5.Count;
			array4 = array3;
			listBuilder6.CopyTo(array4, num2);
			num2 += listBuilder6.Count;
			return array3;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000BAEE4 File Offset: 0x000BA0E4
		public sealed override bool IsCollectible
		{
			get
			{
				RuntimeType runtimeType = this;
				return RuntimeTypeHandle.IsCollectible(new QCallTypeHandle(ref runtimeType)) > Interop.BOOL.FALSE;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000BAF04 File Offset: 0x000BA104
		public override MethodBase DeclaringMethod
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(SR.Arg_NotGenericParameter);
				}
				IRuntimeMethodInfo declaringMethod = RuntimeTypeHandle.GetDeclaringMethod(this);
				if (declaringMethod == null)
				{
					return null;
				}
				return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetDeclaringType(declaringMethod), declaringMethod);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000BAF3C File Offset: 0x000BA13C
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				return false;
			}
			RuntimeType baseType = this.GetBaseType();
			while (baseType != null)
			{
				if (baseType == runtimeType)
				{
					return true;
				}
				baseType = baseType.GetBaseType();
			}
			return runtimeType == RuntimeType.ObjectType && runtimeType != this;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000BAFA8 File Offset: 0x000BA1A8
		public override bool IsEquivalentTo([NotNullWhen(true)] Type other)
		{
			RuntimeType runtimeType = other as RuntimeType;
			return runtimeType != null && (runtimeType == this || RuntimeTypeHandle.IsEquivalentTo(this, runtimeType));
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000BAFD3 File Offset: 0x000BA1D3
		public override string FullName
		{
			get
			{
				return this.GetCachedName(TypeNameKind.FullName);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000BAFDC File Offset: 0x000BA1DC
		public override string AssemblyQualifiedName
		{
			get
			{
				string fullName = this.FullName;
				if (fullName == null)
				{
					return null;
				}
				return Assembly.CreateQualifiedName(this.Assembly.FullName, fullName);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000BB008 File Offset: 0x000BA208
		public override string Namespace
		{
			get
			{
				string nameSpace = this.Cache.GetNameSpace();
				if (string.IsNullOrEmpty(nameSpace))
				{
					return null;
				}
				return nameSpace;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000BB02C File Offset: 0x000BA22C
		public override Guid GUID
		{
			get
			{
				Guid result = default(Guid);
				this.GetGUID(ref result);
				return result;
			}
		}

		// Token: 0x060005DE RID: 1502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGUID(ref Guid result);

		// Token: 0x060005DF RID: 1503 RVA: 0x000BB04A File Offset: 0x000BA24A
		internal bool IsDelegate()
		{
			return this.GetBaseType() == typeof(MulticastDelegate);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000BB064 File Offset: 0x000BA264
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(SR.Arg_NotGenericParameter);
				}
				GenericParameterAttributes result;
				RuntimeTypeHandle.GetMetadataImport(this).GetGenericParamProps(this.MetadataToken, out result);
				return result;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000BB09B File Offset: 0x000BA29B
		public sealed override bool IsSZArray
		{
			get
			{
				return RuntimeTypeHandle.IsSZArray(this);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000BB0A3 File Offset: 0x000BA2A3
		internal object[] GetEmptyArray()
		{
			return this.Cache.GetEmptyArray();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000BB0B0 File Offset: 0x000BA2B0
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return base.GetRootElementType().GetTypeHandleInternal().GetInstantiationInternal();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000BB0D0 File Offset: 0x000BA2D0
		public override Type[] GetGenericArguments()
		{
			Type[] instantiationPublic = base.GetRootElementType().GetTypeHandleInternal().GetInstantiationPublic();
			return instantiationPublic ?? Array.Empty<Type>();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000BB0FC File Offset: 0x000BA2FC
		public override Type MakeGenericType(params Type[] instantiation)
		{
			if (instantiation == null)
			{
				throw new ArgumentNullException("instantiation");
			}
			RuntimeType[] array = new RuntimeType[instantiation.Length];
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException(SR.Format(SR.Arg_NotGenericTypeDefinition, this));
			}
			if (this.GetGenericArguments().Length != instantiation.Length)
			{
				throw new ArgumentException(SR.Argument_GenericArgsCount, "instantiation");
			}
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < instantiation.Length; i++)
			{
				Type type = instantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					flag2 = true;
					if (type.IsSignatureType)
					{
						flag = true;
					}
				}
				array[i] = runtimeType;
			}
			if (!flag2)
			{
				RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
				RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
				Type result;
				try
				{
					RuntimeTypeHandle runtimeTypeHandle = new RuntimeTypeHandle(this);
					Type[] inst = array;
					result = runtimeTypeHandle.Instantiate(inst);
				}
				catch (TypeLoadException e)
				{
					RuntimeType.ValidateGenericArguments(this, array, e);
					throw;
				}
				return result;
			}
			if (flag)
			{
				return new SignatureConstructedGenericType(this, instantiation);
			}
			return TypeBuilderInstantiation.MakeGenericType(this, (Type[])instantiation.Clone());
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000BB210 File Offset: 0x000BA410
		public override int GenericParameterPosition
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(SR.Arg_NotGenericParameter);
				}
				return new RuntimeTypeHandle(this).GetGenericVariableIndex();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000BB240 File Offset: 0x000BA440
		public override bool ContainsGenericParameters
		{
			get
			{
				return base.GetRootElementType().GetTypeHandleInternal().ContainsGenericVariables();
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000BB260 File Offset: 0x000BA460
		public override Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(SR.Arg_NotGenericParameter);
			}
			Type[] constraints = new RuntimeTypeHandle(this).GetConstraints();
			return constraints ?? Array.Empty<Type>();
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000BB299 File Offset: 0x000BA499
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeType>(other);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000BB2A4 File Offset: 0x000BA4A4
		public override Type MakePointerType()
		{
			return new RuntimeTypeHandle(this).MakePointer();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000BB2C0 File Offset: 0x000BA4C0
		public override Type MakeByRefType()
		{
			return new RuntimeTypeHandle(this).MakeByRef();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000BB2DC File Offset: 0x000BA4DC
		public override Type MakeArrayType()
		{
			return new RuntimeTypeHandle(this).MakeSZArray();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000BB2F8 File Offset: 0x000BA4F8
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			return new RuntimeTypeHandle(this).MakeArray(rank);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000BB31E File Offset: 0x000BA51E
		public override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				return PseudoCustomAttribute.GetStructLayoutCustomAttribute(this);
			}
		}

		// Token: 0x060005EF RID: 1519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanValueSpecialCast(RuntimeType valueType, RuntimeType targetType);

		// Token: 0x060005F0 RID: 1520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object AllocateValueType(RuntimeType type, object value, bool fForceTypeChange);

		// Token: 0x060005F1 RID: 1521 RVA: 0x000BB328 File Offset: 0x000BA528
		internal object CheckValue(object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (this.IsInstanceOfType(value))
			{
				Type type = value.GetType();
				if (type != this && RuntimeTypeHandle.IsValueType(this))
				{
					return RuntimeType.AllocateValueType(this, value, true);
				}
				return value;
			}
			else
			{
				bool isByRef = base.IsByRef;
				if (isByRef)
				{
					RuntimeType elementType = RuntimeTypeHandle.GetElementType(this);
					if (elementType.IsInstanceOfType(value) || value == null)
					{
						return RuntimeType.AllocateValueType(elementType, value, false);
					}
				}
				else
				{
					if (value == null)
					{
						return value;
					}
					if (this == RuntimeType.s_typedRef)
					{
						return value;
					}
				}
				bool flag = base.IsPointer || this.IsEnum || base.IsPrimitive;
				if (flag)
				{
					Pointer pointer = value as Pointer;
					RuntimeType valueType;
					if (pointer != null)
					{
						valueType = (RuntimeType)pointer.GetPointerType();
					}
					else
					{
						valueType = (RuntimeType)value.GetType();
					}
					if (RuntimeType.CanValueSpecialCast(valueType, this))
					{
						if (pointer != null)
						{
							return pointer.GetPointerValue();
						}
						return value;
					}
				}
				if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.ExactBinding)
				{
					throw new ArgumentException(SR.Format(SR.Arg_ObjObjEx, value.GetType(), this));
				}
				return this.TryChangeType(value, binder, culture, flag);
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000BB42C File Offset: 0x000BA62C
		private object TryChangeType(object value, Binder binder, CultureInfo culture, bool needsSpecialCast)
		{
			if (binder != null && binder != Type.DefaultBinder)
			{
				value = binder.ChangeType(value, this, culture);
				if (this.IsInstanceOfType(value))
				{
					return value;
				}
				if (base.IsByRef)
				{
					RuntimeType elementType = RuntimeTypeHandle.GetElementType(this);
					if (elementType.IsInstanceOfType(value) || value == null)
					{
						return RuntimeType.AllocateValueType(elementType, value, false);
					}
				}
				else if (value == null)
				{
					return value;
				}
				if (needsSpecialCast)
				{
					Pointer pointer = value as Pointer;
					RuntimeType valueType;
					if (pointer != null)
					{
						valueType = (RuntimeType)pointer.GetPointerType();
					}
					else
					{
						valueType = (RuntimeType)value.GetType();
					}
					if (RuntimeType.CanValueSpecialCast(valueType, this))
					{
						if (pointer != null)
						{
							return pointer.GetPointerValue();
						}
						return value;
					}
				}
			}
			throw new ArgumentException(SR.Format(SR.Arg_ObjObjEx, value.GetType(), this));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000BB4E4 File Offset: 0x000BA6E4
		[DebuggerStepThrough]
		[DebuggerHidden]
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
		public override object InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, object[] providedArgs, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParams)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(SR.Arg_GenericParameter);
			}
			if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException(SR.Arg_NoAccessSpec, "bindingFlags");
			}
			if ((bindingFlags & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingFlags |= (BindingFlags.Instance | BindingFlags.Public);
				if ((bindingFlags & BindingFlags.CreateInstance) == BindingFlags.Default)
				{
					bindingFlags |= BindingFlags.Static;
				}
			}
			if (namedParams != null)
			{
				if (providedArgs != null)
				{
					if (namedParams.Length > providedArgs.Length)
					{
						throw new ArgumentException(SR.Arg_NamedParamTooBig, "namedParams");
					}
				}
				else if (namedParams.Length != 0)
				{
					throw new ArgumentException(SR.Arg_NamedParamTooBig, "namedParams");
				}
			}
			if (target != null && target.GetType().IsCOMObject)
			{
				if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_COMAccess, "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_PropSetGet, "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_PropSetInvoke, "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_COMPropSetPut, "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_COMPropSetPut, "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutRefDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutRefDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_COMPropSetPut, "bindingFlags");
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				bool[] byrefModifiers = (modifiers != null) ? modifiers[0].IsByRefArray : null;
				int culture2 = (culture == null) ? 1033 : culture.LCID;
				bool flag = (bindingFlags & BindingFlags.DoNotWrapExceptions) > BindingFlags.Default;
				try
				{
					return this.InvokeDispMethod(name, bindingFlags, target, providedArgs, byrefModifiers, culture2, namedParams);
				}
				catch (TargetInvocationException ex) when (flag)
				{
					throw ex.InnerException;
				}
			}
			if (namedParams != null && Array.IndexOf<string>(namedParams, null) != -1)
			{
				throw new ArgumentException(SR.Arg_NamedParamNull, "namedParams");
			}
			int num = (providedArgs != null) ? providedArgs.Length : 0;
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default)
			{
				if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(SR.Arg_CreatInstAccess, "bindingFlags");
				}
				return Activator.CreateInstance(this, bindingFlags, binder, providedArgs, culture);
			}
			else
			{
				if ((bindingFlags & (BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) != BindingFlags.Default)
				{
					bindingFlags |= BindingFlags.SetProperty;
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (name.Length == 0 || name.Equals("[DISPID=0]"))
				{
					name = (this.GetDefaultMemberName() ?? "ToString");
				}
				bool flag2 = (bindingFlags & BindingFlags.GetField) > BindingFlags.Default;
				bool flag3 = (bindingFlags & BindingFlags.SetField) > BindingFlags.Default;
				if (flag2 || flag3)
				{
					if (flag2)
					{
						if (flag3)
						{
							throw new ArgumentException(SR.Arg_FldSetGet, "bindingFlags");
						}
						if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default)
						{
							throw new ArgumentException(SR.Arg_FldGetPropSet, "bindingFlags");
						}
					}
					else
					{
						if (providedArgs == null)
						{
							throw new ArgumentNullException("providedArgs");
						}
						if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default)
						{
							throw new ArgumentException(SR.Arg_FldSetPropGet, "bindingFlags");
						}
						if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
						{
							throw new ArgumentException(SR.Arg_FldSetInvoke, "bindingFlags");
						}
					}
					FieldInfo fieldInfo = null;
					FieldInfo[] array = this.GetMember(name, MemberTypes.Field, bindingFlags) as FieldInfo[];
					if (array.Length == 1)
					{
						fieldInfo = array[0];
					}
					else if (array.Length != 0)
					{
						fieldInfo = binder.BindToField(bindingFlags, array, flag2 ? Empty.Value : providedArgs[0], culture);
					}
					if (fieldInfo != null)
					{
						if (fieldInfo.FieldType.IsArray || fieldInfo.FieldType == typeof(Array))
						{
							int num2;
							if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
							{
								num2 = num;
							}
							else
							{
								num2 = num - 1;
							}
							if (num2 > 0)
							{
								int[] array2 = new int[num2];
								for (int i = 0; i < num2; i++)
								{
									try
									{
										array2[i] = ((IConvertible)providedArgs[i]).ToInt32(null);
									}
									catch (InvalidCastException)
									{
										throw new ArgumentException(SR.Arg_IndexMustBeInt);
									}
								}
								Array array3 = (Array)fieldInfo.GetValue(target);
								if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
								{
									return array3.GetValue(array2);
								}
								array3.SetValue(providedArgs[num2], array2);
								return null;
							}
						}
						if (flag2)
						{
							if (num != 0)
							{
								throw new ArgumentException(SR.Arg_FldGetArgErr, "bindingFlags");
							}
							return fieldInfo.GetValue(target);
						}
						else
						{
							if (num != 1)
							{
								throw new ArgumentException(SR.Arg_FldSetArgErr, "bindingFlags");
							}
							fieldInfo.SetValue(target, providedArgs[0], bindingFlags, binder, culture);
							return null;
						}
					}
					else if ((bindingFlags & (BindingFlags)16773888) == BindingFlags.Default)
					{
						throw new MissingFieldException(this.FullName, name);
					}
				}
				bool flag4 = (bindingFlags & BindingFlags.GetProperty) > BindingFlags.Default;
				bool flag5 = (bindingFlags & BindingFlags.SetProperty) > BindingFlags.Default;
				if (flag4 || flag5)
				{
					if (flag4)
					{
						if (flag5)
						{
							throw new ArgumentException(SR.Arg_PropSetGet, "bindingFlags");
						}
					}
					else if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
					{
						throw new ArgumentException(SR.Arg_PropSetInvoke, "bindingFlags");
					}
				}
				MethodInfo[] array4 = null;
				MethodInfo methodInfo = null;
				if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
				{
					MethodInfo[] array5 = this.GetMember(name, MemberTypes.Method, bindingFlags) as MethodInfo[];
					List<MethodInfo> list = null;
					foreach (MethodInfo methodInfo2 in array5)
					{
						if (RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo2, bindingFlags, CallingConventions.Any, new Type[num]))
						{
							if (methodInfo == null)
							{
								methodInfo = methodInfo2;
							}
							else
							{
								if (list == null)
								{
									list = new List<MethodInfo>(array5.Length)
									{
										methodInfo
									};
								}
								list.Add(methodInfo2);
							}
						}
					}
					if (list != null)
					{
						array4 = list.ToArray();
					}
				}
				if ((methodInfo == null && flag4) || flag5)
				{
					PropertyInfo[] array6 = this.GetMember(name, MemberTypes.Property, bindingFlags) as PropertyInfo[];
					List<MethodInfo> list2 = null;
					for (int k = 0; k < array6.Length; k++)
					{
						MethodInfo methodInfo3;
						if (flag5)
						{
							methodInfo3 = array6[k].GetSetMethod(true);
						}
						else
						{
							methodInfo3 = array6[k].GetGetMethod(true);
						}
						if (!(methodInfo3 == null) && RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo3, bindingFlags, CallingConventions.Any, new Type[num]))
						{
							if (methodInfo == null)
							{
								methodInfo = methodInfo3;
							}
							else
							{
								if (list2 == null)
								{
									list2 = new List<MethodInfo>(array6.Length)
									{
										methodInfo
									};
								}
								list2.Add(methodInfo3);
							}
						}
					}
					if (list2 != null)
					{
						array4 = list2.ToArray();
					}
				}
				if (!(methodInfo != null))
				{
					throw new MissingMethodException(this.FullName, name);
				}
				if (array4 == null && num == 0 && methodInfo.GetParametersNoCopy().Length == 0 && (bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
				{
					return methodInfo.Invoke(target, bindingFlags, binder, providedArgs, culture);
				}
				if (array4 == null)
				{
					array4 = new MethodInfo[]
					{
						methodInfo
					};
				}
				if (providedArgs == null)
				{
					providedArgs = Array.Empty<object>();
				}
				object obj2 = null;
				MethodBase methodBase = null;
				try
				{
					Binder binder2 = binder;
					BindingFlags bindingAttr = bindingFlags;
					MethodBase[] match = array4;
					methodBase = binder2.BindToMethod(bindingAttr, match, ref providedArgs, modifiers, culture, namedParams, out obj2);
				}
				catch (MissingMethodException)
				{
				}
				if (methodBase == null)
				{
					throw new MissingMethodException(this.FullName, name);
				}
				object result = ((MethodInfo)methodBase).Invoke(target, bindingFlags, binder, providedArgs, culture);
				if (obj2 != null)
				{
					binder.ReorderArgumentArray(ref providedArgs, obj2);
				}
				return result;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000BBC08 File Offset: 0x000BAE08
		public override string ToString()
		{
			return this.GetCachedName(TypeNameKind.ToString);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000BBC11 File Offset: 0x000BAE11
		public override string Name
		{
			get
			{
				return this.GetCachedName(TypeNameKind.Name);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000BBC1A File Offset: 0x000BAE1A
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string GetCachedName(TypeNameKind kind)
		{
			return this.Cache.GetName(kind);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000BBC28 File Offset: 0x000BAE28
		public override Type DeclaringType
		{
			get
			{
				return this.Cache.GetEnclosingType();
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000BBC38 File Offset: 0x000BAE38
		private void CreateInstanceCheckThis()
		{
			if (this.ContainsGenericParameters)
			{
				throw new ArgumentException(SR.Format(SR.Acc_CreateGenericEx, this));
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType == typeof(ArgIterator))
			{
				throw new NotSupportedException(SR.Acc_CreateArgIterator);
			}
			if (rootElementType == typeof(void))
			{
				throw new NotSupportedException(SR.Acc_CreateVoid);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000BBC98 File Offset: 0x000BAE98
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2082:UnrecognizedReflectionPattern", Justification = "Implementation detail of Activator that linker intrinsically recognizes")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2085:UnrecognizedReflectionPattern", Justification = "Implementation detail of Activator that linker intrinsically recognizes")]
		internal object CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
		{
			this.CreateInstanceCheckThis();
			if (args == null)
			{
				args = Array.Empty<object>();
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			bool publicOnly = (bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default;
			bool wrapExceptions = (bindingAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
			object result;
			if (args.Length == 0 && (bindingAttr & BindingFlags.Public) != BindingFlags.Default && (bindingAttr & BindingFlags.Instance) != BindingFlags.Default && (this.IsGenericCOMObjectImpl() || base.IsValueType))
			{
				result = this.CreateInstanceDefaultCtor(publicOnly, false, true, wrapExceptions);
			}
			else
			{
				ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
				List<MethodBase> list = new List<MethodBase>(constructors.Length);
				Type[] array = new Type[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i] != null)
					{
						array[i] = args[i].GetType();
					}
				}
				for (int j = 0; j < constructors.Length; j++)
				{
					if (RuntimeType.FilterApplyConstructorInfo((RuntimeConstructorInfo)constructors[j], bindingAttr, CallingConventions.Any, array))
					{
						list.Add(constructors[j]);
					}
				}
				if (list.Count == 0)
				{
					throw new MissingMethodException(SR.Format(SR.MissingConstructor_Name, this.FullName));
				}
				MethodBase[] match = list.ToArray();
				object obj = null;
				MethodBase methodBase;
				try
				{
					methodBase = binder.BindToMethod(bindingAttr, match, ref args, null, culture, null, out obj);
				}
				catch (MissingMethodException)
				{
					methodBase = null;
				}
				if (methodBase == null)
				{
					throw new MissingMethodException(SR.Format(SR.MissingConstructor_Name, this.FullName));
				}
				if (methodBase.GetParametersNoCopy().Length == 0)
				{
					if (args.Length != 0)
					{
						throw new NotSupportedException(SR.NotSupported_CallToVarArg);
					}
					result = Activator.CreateInstance(this, true, wrapExceptions);
				}
				else
				{
					result = ((ConstructorInfo)methodBase).Invoke(bindingAttr, binder, args, culture);
					if (obj != null)
					{
						binder.ReorderArgumentArray(ref args, obj);
					}
				}
			}
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000BBE2C File Offset: 0x000BB02C
		private object CreateInstanceDefaultCtorSlow(bool publicOnly, bool wrapExceptions, bool fillCache)
		{
			RuntimeMethodHandleInternal rmh = default(RuntimeMethodHandleInternal);
			bool flag = false;
			bool flag2 = false;
			object result = RuntimeTypeHandle.CreateInstance(this, publicOnly, wrapExceptions, ref flag, ref rmh, ref flag2);
			if (flag2)
			{
				throw new MissingMethodException(SR.Format(SR.Arg_NoDefCTor, this));
			}
			if (flag && fillCache)
			{
				this.GenericCache = new RuntimeType.ActivatorCache(rmh);
			}
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000BBE7C File Offset: 0x000BB07C
		[DebuggerHidden]
		[DebuggerStepThrough]
		internal object CreateInstanceDefaultCtor(bool publicOnly, bool skipCheckThis, bool fillCache, bool wrapExceptions)
		{
			RuntimeType.ActivatorCache activatorCache = this.GenericCache as RuntimeType.ActivatorCache;
			if (activatorCache == null)
			{
				if (!skipCheckThis)
				{
					this.CreateInstanceCheckThis();
				}
				return this.CreateInstanceDefaultCtorSlow(publicOnly, wrapExceptions, fillCache);
			}
			activatorCache.EnsureInitialized();
			if (publicOnly && activatorCache._ctor != null && (activatorCache._ctorAttributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
			{
				throw new MissingMethodException(SR.Format(SR.Arg_NoDefCTor, this));
			}
			object obj = RuntimeTypeHandle.Allocate(this);
			if (activatorCache._ctor != null)
			{
				try
				{
					activatorCache._ctor(obj);
				}
				catch (Exception inner) when (wrapExceptions)
				{
					throw new TargetInvocationException(inner);
				}
			}
			return obj;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000BBF24 File Offset: 0x000BB124
		internal void InvalidateCachedNestedType()
		{
			this.Cache.InvalidateCachedNestedType();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000BBF31 File Offset: 0x000BB131
		internal bool IsGenericCOMObjectImpl()
		{
			return RuntimeTypeHandle.IsComObject(this, true);
		}

		// Token: 0x060005FE RID: 1534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object _CreateEnum(RuntimeType enumType, long value);

		// Token: 0x060005FF RID: 1535 RVA: 0x000BBF3A File Offset: 0x000BB13A
		internal static object CreateEnum(RuntimeType enumType, long value)
		{
			return RuntimeType._CreateEnum(enumType, value);
		}

		// Token: 0x06000600 RID: 1536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object InvokeDispMethod(string name, BindingFlags invokeAttr, object target, object[] args, bool[] byrefModifiers, int culture, string[] namedParameters);

		// Token: 0x06000601 RID: 1537
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetTypeFromProgIDImpl(string progID, string server, bool throwOnError);

		// Token: 0x06000602 RID: 1538
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetTypeFromCLSIDImpl(Guid clsid, string server, bool throwOnError);

		// Token: 0x06000603 RID: 1539 RVA: 0x000BBF44 File Offset: 0x000BB144
		private object ForwardCallToInvokeMember(string memberName, BindingFlags flags, object target, object[] aArgs, bool[] aArgsIsByRef, int[] aArgsWrapperTypes, Type[] aArgsTypes, Type retType)
		{
			int num = aArgs.Length;
			ParameterModifier[] array = null;
			if (num > 0)
			{
				ParameterModifier parameterModifier = new ParameterModifier(num);
				for (int i = 0; i < num; i++)
				{
					parameterModifier[i] = aArgsIsByRef[i];
				}
				array = new ParameterModifier[]
				{
					parameterModifier
				};
				if (aArgsWrapperTypes != null)
				{
					RuntimeType.WrapArgsForInvokeCall(aArgs, aArgsWrapperTypes);
				}
			}
			flags |= BindingFlags.DoNotWrapExceptions;
			object obj = this.InvokeMember(memberName, flags, null, target, aArgs, array, null, null);
			for (int j = 0; j < num; j++)
			{
				if (array[0][j] && aArgs[j] != null)
				{
					Type type = aArgsTypes[j];
					if (type != aArgs[j].GetType())
					{
						aArgs[j] = RuntimeType.ForwardCallBinder.ChangeType(aArgs[j], type, null);
					}
				}
			}
			if (obj != null && retType != obj.GetType())
			{
				obj = RuntimeType.ForwardCallBinder.ChangeType(obj, retType, null);
			}
			return obj;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000BC028 File Offset: 0x000BB228
		private static void WrapArgsForInvokeCall(object[] aArgs, int[] aArgsWrapperTypes)
		{
			int num = aArgs.Length;
			for (int i = 0; i < num; i++)
			{
				if (aArgsWrapperTypes[i] != 0)
				{
					if (((RuntimeType.DispatchWrapperType)aArgsWrapperTypes[i]).HasFlag(RuntimeType.DispatchWrapperType.SafeArray))
					{
						Type type = null;
						bool flag = false;
						RuntimeType.DispatchWrapperType dispatchWrapperType = (RuntimeType.DispatchWrapperType)(aArgsWrapperTypes[i] & -65537);
						if (dispatchWrapperType <= RuntimeType.DispatchWrapperType.Dispatch)
						{
							if (dispatchWrapperType != RuntimeType.DispatchWrapperType.Unknown)
							{
								if (dispatchWrapperType == RuntimeType.DispatchWrapperType.Dispatch)
								{
									type = typeof(DispatchWrapper);
								}
							}
							else
							{
								type = typeof(UnknownWrapper);
							}
						}
						else if (dispatchWrapperType != RuntimeType.DispatchWrapperType.Error)
						{
							if (dispatchWrapperType != RuntimeType.DispatchWrapperType.Currency)
							{
								if (dispatchWrapperType == RuntimeType.DispatchWrapperType.BStr)
								{
									type = typeof(BStrWrapper);
									flag = true;
								}
							}
							else
							{
								type = typeof(CurrencyWrapper);
							}
						}
						else
						{
							type = typeof(ErrorWrapper);
						}
						Array array = (Array)aArgs[i];
						int length = array.Length;
						object[] array2 = (object[])Array.CreateInstance(type, length);
						ConstructorInfo constructor;
						if (flag)
						{
							constructor = type.GetConstructor(new Type[]
							{
								typeof(string)
							});
						}
						else
						{
							constructor = type.GetConstructor(new Type[]
							{
								typeof(object)
							});
						}
						for (int j = 0; j < length; j++)
						{
							if (flag)
							{
								array2[j] = constructor.Invoke(new object[]
								{
									(string)array.GetValue(j)
								});
							}
							else
							{
								array2[j] = constructor.Invoke(new object[]
								{
									array.GetValue(j)
								});
							}
						}
						aArgs[i] = array2;
					}
					else
					{
						RuntimeType.DispatchWrapperType dispatchWrapperType2 = (RuntimeType.DispatchWrapperType)aArgsWrapperTypes[i];
						if (dispatchWrapperType2 <= RuntimeType.DispatchWrapperType.Dispatch)
						{
							if (dispatchWrapperType2 != RuntimeType.DispatchWrapperType.Unknown)
							{
								if (dispatchWrapperType2 == RuntimeType.DispatchWrapperType.Dispatch)
								{
									aArgs[i] = new DispatchWrapper(aArgs[i]);
								}
							}
							else
							{
								aArgs[i] = new UnknownWrapper(aArgs[i]);
							}
						}
						else if (dispatchWrapperType2 != RuntimeType.DispatchWrapperType.Error)
						{
							if (dispatchWrapperType2 != RuntimeType.DispatchWrapperType.Currency)
							{
								if (dispatchWrapperType2 == RuntimeType.DispatchWrapperType.BStr)
								{
									aArgs[i] = new BStrWrapper((string)aArgs[i]);
								}
							}
							else
							{
								aArgs[i] = new CurrencyWrapper(aArgs[i]);
							}
						}
						else
						{
							aArgs[i] = new ErrorWrapper(aArgs[i]);
						}
					}
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x000BC20B File Offset: 0x000BB40B
		private static OleAutBinder ForwardCallBinder
		{
			get
			{
				OleAutBinder result;
				if ((result = RuntimeType.s_ForwardCallBinder) == null)
				{
					result = (RuntimeType.s_ForwardCallBinder = new OleAutBinder());
				}
				return result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000BC221 File Offset: 0x000BB421
		public override Assembly Assembly
		{
			get
			{
				return RuntimeTypeHandle.GetAssembly(this);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000BC229 File Offset: 0x000BB429
		public override Type BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000BC231 File Offset: 0x000BB431
		public override bool IsByRefLike
		{
			get
			{
				return RuntimeTypeHandle.IsByRefLike(this);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x000BC239 File Offset: 0x000BB439
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.IsGenericType && !this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000BC24E File Offset: 0x000BB44E
		public override bool IsGenericType
		{
			get
			{
				return RuntimeTypeHandle.HasInstantiation(this);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000BC256 File Offset: 0x000BB456
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsGenericTypeDefinition(this);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000BC25E File Offset: 0x000BB45E
		public override bool IsGenericParameter
		{
			get
			{
				return RuntimeTypeHandle.IsGenericVariable(this);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000BC266 File Offset: 0x000BB466
		public override bool IsTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsTypeDefinition(this);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000BC26E File Offset: 0x000BB46E
		public override MemberTypes MemberType
		{
			get
			{
				if (!base.IsPublic && !base.IsNotPublic)
				{
					return MemberTypes.NestedType;
				}
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000BC288 File Offset: 0x000BB488
		public override int MetadataToken
		{
			get
			{
				return RuntimeTypeHandle.GetToken(this);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000BC290 File Offset: 0x000BB490
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000BC298 File Offset: 0x000BB498
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000BC2A0 File Offset: 0x000BB4A0
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return new RuntimeTypeHandle(this);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000AC098 File Offset: 0x000AB298
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000AC098 File Offset: 0x000AB298
		public object Clone()
		{
			return this;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000BC2A8 File Offset: 0x000BB4A8
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000BC2AE File Offset: 0x000BB4AE
		public override int GetArrayRank()
		{
			if (!this.IsArrayImpl())
			{
				throw new ArgumentException(SR.Argument_HasToBeArrayClass);
			}
			return RuntimeTypeHandle.GetArrayRank(this);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000BC2C9 File Offset: 0x000BB4C9
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return RuntimeTypeHandle.GetAttributes(this);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000BC2D1 File Offset: 0x000BB4D1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, RuntimeType.ObjectType, inherit);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000BC2E0 File Offset: 0x000BB4E0
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000BC328 File Offset: 0x000BB528
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000BC330 File Offset: 0x000BB530
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
		public override MemberInfo[] GetDefaultMembers()
		{
			MemberInfo[] array = null;
			string defaultMemberName = this.GetDefaultMemberName();
			if (defaultMemberName != null)
			{
				array = base.GetMember(defaultMemberName);
			}
			return array ?? Array.Empty<MemberInfo>();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000BC35B File Offset: 0x000BB55B
		public override Type GetElementType()
		{
			return RuntimeTypeHandle.GetElementType(this);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000BC364 File Offset: 0x000BB564
		public override string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(SR.Arg_MustBeEnumBaseTypeOrEnum, "value");
			}
			ulong ulValue = Enum.ToUInt64(value);
			return Enum.GetEnumName(this, ulValue);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000BC3B4 File Offset: 0x000BB5B4
		public override string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			string[] array = Enum.InternalGetNames(this);
			return new ReadOnlySpan<string>(array).ToArray();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000BC3F0 File Offset: 0x000BB5F0
		public override Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			ulong[] array = Enum.InternalGetValues(this);
			Array array2 = Array.CreateInstance(this, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				object value = Enum.ToObject(this, array[i]);
				array2.SetValue(value, i);
			}
			return array2;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000BC447 File Offset: 0x000BB647
		public override Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			return Enum.InternalGetUnderlyingType(this);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000BC467 File Offset: 0x000BB667
		public override Type GetGenericTypeDefinition()
		{
			if (!this.IsGenericType)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NotGenericType);
			}
			return RuntimeTypeHandle.GetGenericTypeDefinition(this);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000B8668 File Offset: 0x000B7868
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000BC482 File Offset: 0x000BB682
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000BC48C File Offset: 0x000BB68C
		protected override TypeCode GetTypeCodeImpl()
		{
			TypeCode typeCode = this.Cache.TypeCode;
			if (typeCode != TypeCode.Empty)
			{
				return typeCode;
			}
			switch (RuntimeTypeHandle.GetCorElementType(this))
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				typeCode = TypeCode.Boolean;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_CHAR:
				typeCode = TypeCode.Char;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_I1:
				typeCode = TypeCode.SByte;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_U1:
				typeCode = TypeCode.Byte;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_I2:
				typeCode = TypeCode.Int16;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_U2:
				typeCode = TypeCode.UInt16;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_I4:
				typeCode = TypeCode.Int32;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_U4:
				typeCode = TypeCode.UInt32;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_I8:
				typeCode = TypeCode.Int64;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_U8:
				typeCode = TypeCode.UInt64;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_R4:
				typeCode = TypeCode.Single;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_R8:
				typeCode = TypeCode.Double;
				goto IL_121;
			case CorElementType.ELEMENT_TYPE_VALUETYPE:
				if (this == Convert.ConvertTypes[15])
				{
					typeCode = TypeCode.Decimal;
					goto IL_121;
				}
				if (this == Convert.ConvertTypes[16])
				{
					typeCode = TypeCode.DateTime;
					goto IL_121;
				}
				if (this.IsEnum)
				{
					typeCode = Type.GetTypeCode(Enum.GetUnderlyingType(this));
					goto IL_121;
				}
				typeCode = TypeCode.Object;
				goto IL_121;
			}
			if (this == Convert.ConvertTypes[2])
			{
				typeCode = TypeCode.DBNull;
			}
			else if (this == Convert.ConvertTypes[18])
			{
				typeCode = TypeCode.String;
			}
			else
			{
				typeCode = TypeCode.Object;
			}
			IL_121:
			this.Cache.TypeCode = typeCode;
			return typeCode;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000BC5C7 File Offset: 0x000BB7C7
		protected override bool HasElementTypeImpl()
		{
			return RuntimeTypeHandle.HasElementType(this);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000BC5CF File Offset: 0x000BB7CF
		protected override bool IsArrayImpl()
		{
			return RuntimeTypeHandle.IsArray(this);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000AC09B File Offset: 0x000AB29B
		protected override bool IsContextfulImpl()
		{
			return false;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000BC5D8 File Offset: 0x000BB7D8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000BC620 File Offset: 0x000BB820
		public override bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			RuntimeType runtimeType = (RuntimeType)value.GetType();
			if (runtimeType.IsEnum)
			{
				if (!runtimeType.IsEquivalentTo(this))
				{
					throw new ArgumentException(SR.Format(SR.Arg_EnumAndObjectMustBeSameType, runtimeType, this));
				}
				runtimeType = (RuntimeType)runtimeType.GetEnumUnderlyingType();
			}
			if (runtimeType == RuntimeType.StringType)
			{
				string[] array = Enum.InternalGetNames(this);
				object[] array2 = array;
				return Array.IndexOf<object>(array2, value) >= 0;
			}
			if (!Type.IsIntegerType(runtimeType))
			{
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
			RuntimeType runtimeType2 = Enum.InternalGetUnderlyingType(this);
			if (runtimeType2 != runtimeType)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumUnderlyingTypeAndObjectMustBeSameType, runtimeType, runtimeType2));
			}
			ulong[] array3 = Enum.InternalGetValues(this);
			ulong value2 = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array3, value2) >= 0;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000BC706 File Offset: 0x000BB906
		protected override bool IsValueTypeImpl()
		{
			return !(this == typeof(ValueType)) && !(this == typeof(Enum)) && this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000BC73E File Offset: 0x000BB93E
		protected override bool IsByRefImpl()
		{
			return RuntimeTypeHandle.IsByRef(this);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000BC746 File Offset: 0x000BB946
		protected override bool IsPrimitiveImpl()
		{
			return RuntimeTypeHandle.IsPrimitive(this);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000BC74E File Offset: 0x000BB94E
		protected override bool IsPointerImpl()
		{
			return RuntimeTypeHandle.IsPointer(this);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000BC756 File Offset: 0x000BB956
		protected override bool IsCOMObjectImpl()
		{
			return RuntimeTypeHandle.IsComObject(this, false);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000BC75F File Offset: 0x000BB95F
		public override bool IsInstanceOfType([NotNullWhen(true)] object o)
		{
			return RuntimeTypeHandle.IsInstanceOfType(this, o);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000BC768 File Offset: 0x000BB968
		public override bool IsAssignableFrom([NotNullWhen(true)] TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000BC784 File Offset: 0x000BB984
		public override bool IsAssignableFrom([NotNullWhen(true)] Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return true;
			}
			RuntimeType runtimeType = c.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return RuntimeTypeHandle.CanCastTo(runtimeType, this);
			}
			if (c is TypeBuilder)
			{
				if (c.IsSubclassOf(this))
				{
					return true;
				}
				if (base.IsInterface)
				{
					return c.ImplementInterface(this);
				}
				if (this.IsGenericParameter)
				{
					Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
					for (int i = 0; i < genericParameterConstraints.Length; i++)
					{
						if (!genericParameterConstraints[i].IsAssignableFrom(c))
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000BC804 File Offset: 0x000BBA04
		private RuntimeType GetBaseType()
		{
			if (base.IsInterface)
			{
				return null;
			}
			if (RuntimeTypeHandle.IsGenericVariable(this))
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				RuntimeType runtimeType = RuntimeType.ObjectType;
				foreach (RuntimeType runtimeType2 in genericParameterConstraints)
				{
					if (!runtimeType2.IsInterface)
					{
						if (runtimeType2.IsGenericParameter)
						{
							GenericParameterAttributes genericParameterAttributes = runtimeType2.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
							if ((genericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.None && (genericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.None)
							{
								goto IL_55;
							}
						}
						runtimeType = runtimeType2;
					}
					IL_55:;
				}
				if (runtimeType == RuntimeType.ObjectType)
				{
					GenericParameterAttributes genericParameterAttributes2 = this.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
					if ((genericParameterAttributes2 & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
					{
						runtimeType = RuntimeType.ValueType;
					}
				}
				return runtimeType;
			}
			return RuntimeTypeHandle.GetBaseType(this);
		}

		// Token: 0x040001C4 RID: 452
		private readonly object m_keepalive;

		// Token: 0x040001C5 RID: 453
		private IntPtr m_cache;

		// Token: 0x040001C6 RID: 454
		internal IntPtr m_handle;

		// Token: 0x040001C7 RID: 455
		internal static readonly RuntimeType ValueType = (RuntimeType)typeof(ValueType);

		// Token: 0x040001C8 RID: 456
		private static readonly RuntimeType ObjectType = (RuntimeType)typeof(object);

		// Token: 0x040001C9 RID: 457
		private static readonly RuntimeType StringType = (RuntimeType)typeof(string);

		// Token: 0x040001CA RID: 458
		private const int GenericParameterCountAny = -1;

		// Token: 0x040001CB RID: 459
		private const BindingFlags MemberBindingMask = (BindingFlags)255;

		// Token: 0x040001CC RID: 460
		private const BindingFlags InvocationMask = BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x040001CD RID: 461
		private const BindingFlags BinderNonCreateInstance = BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x040001CE RID: 462
		private const BindingFlags BinderGetSetProperty = BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x040001CF RID: 463
		private const BindingFlags BinderGetSetField = BindingFlags.GetField | BindingFlags.SetField;

		// Token: 0x040001D0 RID: 464
		private const BindingFlags BinderNonFieldGetSet = (BindingFlags)16773888;

		// Token: 0x040001D1 RID: 465
		private static readonly RuntimeType s_typedRef = (RuntimeType)typeof(TypedReference);

		// Token: 0x040001D2 RID: 466
		private static OleAutBinder s_ForwardCallBinder;

		// Token: 0x0200008C RID: 140
		internal enum MemberListType
		{
			// Token: 0x040001D4 RID: 468
			All,
			// Token: 0x040001D5 RID: 469
			CaseSensitive,
			// Token: 0x040001D6 RID: 470
			CaseInsensitive,
			// Token: 0x040001D7 RID: 471
			HandleToInfo
		}

		// Token: 0x0200008D RID: 141
		internal struct ListBuilder<T> where T : class
		{
			// Token: 0x06000637 RID: 1591 RVA: 0x000BC8F9 File Offset: 0x000BBAF9
			public ListBuilder(int capacity)
			{
				this._items = null;
				this._item = default(T);
				this._count = 0;
				this._capacity = capacity;
			}

			// Token: 0x17000083 RID: 131
			public T this[int index]
			{
				get
				{
					if (this._items == null)
					{
						return this._item;
					}
					return this._items[index];
				}
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x000BC93C File Offset: 0x000BBB3C
			public T[] ToArray()
			{
				if (this._count == 0)
				{
					return Array.Empty<T>();
				}
				if (this._count == 1)
				{
					return new T[]
					{
						this._item
					};
				}
				Array.Resize<T>(ref this._items, this._count);
				this._capacity = this._count;
				return this._items;
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x000BC997 File Offset: 0x000BBB97
			public void CopyTo(object[] array, int index)
			{
				if (this._count == 0)
				{
					return;
				}
				if (this._count == 1)
				{
					array[index] = this._item;
					return;
				}
				Array.Copy(this._items, 0, array, index, this._count);
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x0600063B RID: 1595 RVA: 0x000BC9CE File Offset: 0x000BBBCE
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x000BC9D8 File Offset: 0x000BBBD8
			public void Add(T item)
			{
				if (this._count == 0)
				{
					this._item = item;
				}
				else
				{
					if (this._count == 1)
					{
						if (this._capacity < 2)
						{
							this._capacity = 4;
						}
						this._items = new T[this._capacity];
						this._items[0] = this._item;
					}
					else if (this._capacity == this._count)
					{
						int num = 2 * this._capacity;
						Array.Resize<T>(ref this._items, num);
						this._capacity = num;
					}
					this._items[this._count] = item;
				}
				this._count++;
			}

			// Token: 0x040001D8 RID: 472
			private T[] _items;

			// Token: 0x040001D9 RID: 473
			private T _item;

			// Token: 0x040001DA RID: 474
			private int _count;

			// Token: 0x040001DB RID: 475
			private int _capacity;
		}

		// Token: 0x0200008E RID: 142
		internal class RuntimeTypeCache
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x000BCA7E File Offset: 0x000BBC7E
			internal RuntimeTypeCache(RuntimeType runtimeType)
			{
				this.m_typeCode = TypeCode.Empty;
				this.m_runtimeType = runtimeType;
				this.m_isGlobal = (RuntimeTypeHandle.GetModule(runtimeType).RuntimeType == runtimeType);
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x000BCAAC File Offset: 0x000BBCAC
			private string ConstructName([NotNull] ref string name, TypeNameFormatFlags formatFlags)
			{
				string result;
				if ((result = name) == null)
				{
					string text;
					name = (text = new RuntimeTypeHandle(this.m_runtimeType).ConstructName(formatFlags));
					result = text;
				}
				return result;
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x000BCAD8 File Offset: 0x000BBCD8
			private T[] GetMemberList<T>(ref RuntimeType.RuntimeTypeCache.MemberInfoCache<T> m_cache, RuntimeType.MemberListType listType, string name, RuntimeType.RuntimeTypeCache.CacheType cacheType) where T : MemberInfo
			{
				RuntimeType.RuntimeTypeCache.MemberInfoCache<T> memberCache = this.GetMemberCache<T>(ref m_cache);
				return memberCache.GetMemberList(listType, name, cacheType);
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x000BCAF8 File Offset: 0x000BBCF8
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<T> GetMemberCache<T>(ref RuntimeType.RuntimeTypeCache.MemberInfoCache<T> m_cache) where T : MemberInfo
			{
				RuntimeType.RuntimeTypeCache.MemberInfoCache<T> memberInfoCache = m_cache;
				if (memberInfoCache == null)
				{
					RuntimeType.RuntimeTypeCache.MemberInfoCache<T> memberInfoCache2 = new RuntimeType.RuntimeTypeCache.MemberInfoCache<T>(this);
					memberInfoCache = Interlocked.CompareExchange<RuntimeType.RuntimeTypeCache.MemberInfoCache<T>>(ref m_cache, memberInfoCache2, null);
					if (memberInfoCache == null)
					{
						memberInfoCache = memberInfoCache2;
					}
				}
				return memberInfoCache;
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000641 RID: 1601 RVA: 0x000BCB21 File Offset: 0x000BBD21
			// (set) Token: 0x06000642 RID: 1602 RVA: 0x000BCB29 File Offset: 0x000BBD29
			internal object GenericCache
			{
				get
				{
					return this.m_genericCache;
				}
				set
				{
					this.m_genericCache = value;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x000BCB32 File Offset: 0x000BBD32
			// (set) Token: 0x06000644 RID: 1604 RVA: 0x000BCB3A File Offset: 0x000BBD3A
			internal bool DomainInitialized
			{
				get
				{
					return this.m_bIsDomainInitialized;
				}
				set
				{
					this.m_bIsDomainInitialized = value;
				}
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x000BCB44 File Offset: 0x000BBD44
			internal string GetName(TypeNameKind kind)
			{
				switch (kind)
				{
				case TypeNameKind.Name:
					return this.ConstructName(ref this.m_name, TypeNameFormatFlags.FormatBasic);
				case TypeNameKind.ToString:
					return this.ConstructName(ref this.m_toString, TypeNameFormatFlags.FormatNamespace);
				case TypeNameKind.FullName:
					if (!this.m_runtimeType.GetRootElementType().IsGenericTypeDefinition && this.m_runtimeType.ContainsGenericParameters)
					{
						return null;
					}
					return this.ConstructName(ref this.m_fullname, (TypeNameFormatFlags)3);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x000BCBB8 File Offset: 0x000BBDB8
			internal string GetNameSpace()
			{
				if (this.m_namespace == null)
				{
					Type type = this.m_runtimeType;
					type = type.GetRootElementType();
					while (type.IsNested)
					{
						type = type.DeclaringType;
					}
					this.m_namespace = RuntimeTypeHandle.GetMetadataImport((RuntimeType)type).GetNamespace(type.MetadataToken).ToString();
				}
				return this.m_namespace;
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000647 RID: 1607 RVA: 0x000BCC1F File Offset: 0x000BBE1F
			// (set) Token: 0x06000648 RID: 1608 RVA: 0x000BCC27 File Offset: 0x000BBE27
			internal TypeCode TypeCode
			{
				get
				{
					return this.m_typeCode;
				}
				set
				{
					this.m_typeCode = value;
				}
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x000BCC30 File Offset: 0x000BBE30
			internal RuntimeType GetEnclosingType()
			{
				if (this.m_enclosingType == null)
				{
					RuntimeType declaringType = RuntimeTypeHandle.GetDeclaringType(this.GetRuntimeType());
					this.m_enclosingType = (declaringType ?? ((RuntimeType)typeof(void)));
				}
				if (!(this.m_enclosingType == typeof(void)))
				{
					return this.m_enclosingType;
				}
				return null;
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x000BCC90 File Offset: 0x000BBE90
			internal RuntimeType GetRuntimeType()
			{
				return this.m_runtimeType;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600064B RID: 1611 RVA: 0x000BCC98 File Offset: 0x000BBE98
			internal bool IsGlobal
			{
				get
				{
					return this.m_isGlobal;
				}
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x000BCCA0 File Offset: 0x000BBEA0
			internal void InvalidateCachedNestedType()
			{
				this.m_nestedClassesCache = null;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x000BCCAC File Offset: 0x000BBEAC
			internal string GetDefaultMemberName()
			{
				if (this.m_defaultMemberName == null)
				{
					CustomAttributeData customAttributeData = null;
					Type typeFromHandle = typeof(DefaultMemberAttribute);
					RuntimeType runtimeType = this.m_runtimeType;
					while (runtimeType != null)
					{
						IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(runtimeType);
						for (int i = 0; i < customAttributes.Count; i++)
						{
							if (customAttributes[i].Constructor.DeclaringType == typeFromHandle)
							{
								customAttributeData = customAttributes[i];
								break;
							}
						}
						if (customAttributeData != null)
						{
							this.m_defaultMemberName = (customAttributeData.ConstructorArguments[0].Value as string);
							break;
						}
						runtimeType = runtimeType.GetBaseType();
					}
				}
				return this.m_defaultMemberName;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x000BCD54 File Offset: 0x000BBF54
			internal object[] GetEmptyArray()
			{
				object[] result;
				if ((result = this._emptyArray) == null)
				{
					result = (this._emptyArray = (object[])Array.CreateInstance(this.m_runtimeType, 0));
				}
				return result;
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x000BCD88 File Offset: 0x000BBF88
			internal MethodInfo GetGenericMethodInfo(RuntimeMethodHandleInternal genericMethod)
			{
				LoaderAllocator loaderAllocator = RuntimeMethodHandle.GetLoaderAllocator(genericMethod);
				RuntimeMethodInfo runtimeMethodInfo = new RuntimeMethodInfo(genericMethod, RuntimeMethodHandle.GetDeclaringType(genericMethod), this, RuntimeMethodHandle.GetAttributes(genericMethod), (BindingFlags)(-1), loaderAllocator);
				RuntimeMethodInfo runtimeMethodInfo2;
				if (loaderAllocator != null)
				{
					runtimeMethodInfo2 = loaderAllocator.m_methodInstantiations[runtimeMethodInfo];
				}
				else
				{
					runtimeMethodInfo2 = RuntimeType.RuntimeTypeCache.s_methodInstantiations[runtimeMethodInfo];
				}
				if (runtimeMethodInfo2 != null)
				{
					return runtimeMethodInfo2;
				}
				if (RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock == null)
				{
					Interlocked.CompareExchange(ref RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock, new object(), null);
				}
				lock (RuntimeType.RuntimeTypeCache.s_methodInstantiationsLock)
				{
					if (loaderAllocator != null)
					{
						runtimeMethodInfo2 = loaderAllocator.m_methodInstantiations[runtimeMethodInfo];
						if (runtimeMethodInfo2 != null)
						{
							return runtimeMethodInfo2;
						}
						loaderAllocator.m_methodInstantiations[runtimeMethodInfo] = runtimeMethodInfo;
					}
					else
					{
						runtimeMethodInfo2 = RuntimeType.RuntimeTypeCache.s_methodInstantiations[runtimeMethodInfo];
						if (runtimeMethodInfo2 != null)
						{
							return runtimeMethodInfo2;
						}
						RuntimeType.RuntimeTypeCache.s_methodInstantiations[runtimeMethodInfo] = runtimeMethodInfo;
					}
				}
				return runtimeMethodInfo;
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x000BCE78 File Offset: 0x000BC078
			internal RuntimeMethodInfo[] GetMethodList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeMethodInfo>(ref this.m_methodInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Method);
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x000BCE89 File Offset: 0x000BC089
			internal RuntimeConstructorInfo[] GetConstructorList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeConstructorInfo>(ref this.m_constructorInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Constructor);
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x000BCE9A File Offset: 0x000BC09A
			internal RuntimePropertyInfo[] GetPropertyList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimePropertyInfo>(ref this.m_propertyInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Property);
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x000BCEAB File Offset: 0x000BC0AB
			internal RuntimeEventInfo[] GetEventList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeEventInfo>(ref this.m_eventInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Event);
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x000BCEBC File Offset: 0x000BC0BC
			internal RuntimeFieldInfo[] GetFieldList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeFieldInfo>(ref this.m_fieldInfoCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Field);
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x000BCECD File Offset: 0x000BC0CD
			internal RuntimeType[] GetInterfaceList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeType>(ref this.m_interfaceCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.Interface);
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x000BCEDE File Offset: 0x000BC0DE
			internal RuntimeType[] GetNestedTypeList(RuntimeType.MemberListType listType, string name)
			{
				return this.GetMemberList<RuntimeType>(ref this.m_nestedClassesCache, listType, name, RuntimeType.RuntimeTypeCache.CacheType.NestedType);
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x000BCEEF File Offset: 0x000BC0EF
			internal MethodBase GetMethod(RuntimeType declaringType, RuntimeMethodHandleInternal method)
			{
				this.GetMemberCache<RuntimeMethodInfo>(ref this.m_methodInfoCache);
				return this.m_methodInfoCache.AddMethod(declaringType, method, RuntimeType.RuntimeTypeCache.CacheType.Method);
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x000BCF0C File Offset: 0x000BC10C
			internal MethodBase GetConstructor(RuntimeType declaringType, RuntimeMethodHandleInternal constructor)
			{
				this.GetMemberCache<RuntimeConstructorInfo>(ref this.m_constructorInfoCache);
				return this.m_constructorInfoCache.AddMethod(declaringType, constructor, RuntimeType.RuntimeTypeCache.CacheType.Constructor);
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x000BCF29 File Offset: 0x000BC129
			internal FieldInfo GetField(RuntimeFieldHandleInternal field)
			{
				this.GetMemberCache<RuntimeFieldInfo>(ref this.m_fieldInfoCache);
				return this.m_fieldInfoCache.AddField(field);
			}

			// Token: 0x040001DC RID: 476
			private readonly RuntimeType m_runtimeType;

			// Token: 0x040001DD RID: 477
			private RuntimeType m_enclosingType;

			// Token: 0x040001DE RID: 478
			private TypeCode m_typeCode;

			// Token: 0x040001DF RID: 479
			private string m_name;

			// Token: 0x040001E0 RID: 480
			private string m_fullname;

			// Token: 0x040001E1 RID: 481
			private string m_toString;

			// Token: 0x040001E2 RID: 482
			private string m_namespace;

			// Token: 0x040001E3 RID: 483
			private readonly bool m_isGlobal;

			// Token: 0x040001E4 RID: 484
			private bool m_bIsDomainInitialized;

			// Token: 0x040001E5 RID: 485
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeMethodInfo> m_methodInfoCache;

			// Token: 0x040001E6 RID: 486
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeConstructorInfo> m_constructorInfoCache;

			// Token: 0x040001E7 RID: 487
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeFieldInfo> m_fieldInfoCache;

			// Token: 0x040001E8 RID: 488
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeType> m_interfaceCache;

			// Token: 0x040001E9 RID: 489
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeType> m_nestedClassesCache;

			// Token: 0x040001EA RID: 490
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimePropertyInfo> m_propertyInfoCache;

			// Token: 0x040001EB RID: 491
			private RuntimeType.RuntimeTypeCache.MemberInfoCache<RuntimeEventInfo> m_eventInfoCache;

			// Token: 0x040001EC RID: 492
			private static CerHashtable<RuntimeMethodInfo, RuntimeMethodInfo> s_methodInstantiations;

			// Token: 0x040001ED RID: 493
			private static object s_methodInstantiationsLock;

			// Token: 0x040001EE RID: 494
			private string m_defaultMemberName;

			// Token: 0x040001EF RID: 495
			private object m_genericCache;

			// Token: 0x040001F0 RID: 496
			private object[] _emptyArray;

			// Token: 0x0200008F RID: 143
			internal enum CacheType
			{
				// Token: 0x040001F2 RID: 498
				Method,
				// Token: 0x040001F3 RID: 499
				Constructor,
				// Token: 0x040001F4 RID: 500
				Field,
				// Token: 0x040001F5 RID: 501
				Property,
				// Token: 0x040001F6 RID: 502
				Event,
				// Token: 0x040001F7 RID: 503
				Interface,
				// Token: 0x040001F8 RID: 504
				NestedType
			}

			// Token: 0x02000090 RID: 144
			private readonly struct Filter
			{
				// Token: 0x0600065A RID: 1626 RVA: 0x000BCF44 File Offset: 0x000BC144
				public unsafe Filter(byte* pUtf8Name, int cUtf8Name, RuntimeType.MemberListType listType)
				{
					this.m_name = new MdUtf8String(pUtf8Name, cUtf8Name);
					this.m_listType = listType;
					this.m_nameHash = 0U;
					if (this.RequiresStringComparison())
					{
						this.m_nameHash = this.m_name.HashCaseInsensitive();
					}
				}

				// Token: 0x0600065B RID: 1627 RVA: 0x000BCF7C File Offset: 0x000BC17C
				public bool Match(MdUtf8String name)
				{
					bool result = true;
					if (this.m_listType == RuntimeType.MemberListType.CaseSensitive)
					{
						result = this.m_name.Equals(name);
					}
					else if (this.m_listType == RuntimeType.MemberListType.CaseInsensitive)
					{
						result = this.m_name.EqualsCaseInsensitive(name);
					}
					return result;
				}

				// Token: 0x0600065C RID: 1628 RVA: 0x000BCFBA File Offset: 0x000BC1BA
				public bool RequiresStringComparison()
				{
					return this.m_listType == RuntimeType.MemberListType.CaseSensitive || this.m_listType == RuntimeType.MemberListType.CaseInsensitive;
				}

				// Token: 0x0600065D RID: 1629 RVA: 0x000BCFD0 File Offset: 0x000BC1D0
				public bool CaseSensitive()
				{
					return this.m_listType == RuntimeType.MemberListType.CaseSensitive;
				}

				// Token: 0x0600065E RID: 1630 RVA: 0x000BCFDB File Offset: 0x000BC1DB
				public uint GetHashToMatch()
				{
					return this.m_nameHash;
				}

				// Token: 0x040001F9 RID: 505
				private readonly MdUtf8String m_name;

				// Token: 0x040001FA RID: 506
				private readonly RuntimeType.MemberListType m_listType;

				// Token: 0x040001FB RID: 507
				private readonly uint m_nameHash;
			}

			// Token: 0x02000091 RID: 145
			private class MemberInfoCache<T> where T : MemberInfo
			{
				// Token: 0x0600065F RID: 1631 RVA: 0x000BCFE3 File Offset: 0x000BC1E3
				internal MemberInfoCache(RuntimeType.RuntimeTypeCache runtimeTypeCache)
				{
					this.m_runtimeTypeCache = runtimeTypeCache;
				}

				// Token: 0x06000660 RID: 1632 RVA: 0x000BCFF4 File Offset: 0x000BC1F4
				internal MethodBase AddMethod(RuntimeType declaringType, RuntimeMethodHandleInternal method, RuntimeType.RuntimeTypeCache.CacheType cacheType)
				{
					T[] allMembers = this.m_allMembers;
					if (allMembers != null)
					{
						if (cacheType == RuntimeType.RuntimeTypeCache.CacheType.Method)
						{
							foreach (T t in allMembers)
							{
								if (t == null)
								{
									break;
								}
								RuntimeMethodInfo runtimeMethodInfo = t as RuntimeMethodInfo;
								if (runtimeMethodInfo != null && runtimeMethodInfo.MethodHandle.Value == method.Value)
								{
									return runtimeMethodInfo;
								}
							}
						}
						else if (cacheType == RuntimeType.RuntimeTypeCache.CacheType.Constructor)
						{
							foreach (T t2 in allMembers)
							{
								if (t2 == null)
								{
									break;
								}
								RuntimeConstructorInfo runtimeConstructorInfo = t2 as RuntimeConstructorInfo;
								if (runtimeConstructorInfo != null && runtimeConstructorInfo.MethodHandle.Value == method.Value)
								{
									return runtimeConstructorInfo;
								}
							}
						}
					}
					T[] array3 = null;
					MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method);
					bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
					bool isStatic = (attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
					bool isInherited = declaringType != this.ReflectedType;
					BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
					if (cacheType != RuntimeType.RuntimeTypeCache.CacheType.Method)
					{
						if (cacheType == RuntimeType.RuntimeTypeCache.CacheType.Constructor)
						{
							array3 = (T[])new RuntimeConstructorInfo[]
							{
								new RuntimeConstructorInfo(method, declaringType, this.m_runtimeTypeCache, attributes, bindingFlags)
							};
						}
					}
					else
					{
						array3 = (T[])new RuntimeMethodInfo[]
						{
							new RuntimeMethodInfo(method, declaringType, this.m_runtimeTypeCache, attributes, bindingFlags, null)
						};
					}
					this.Insert(ref array3, null, RuntimeType.MemberListType.HandleToInfo);
					return (MethodBase)((object)array3[0]);
				}

				// Token: 0x06000661 RID: 1633 RVA: 0x000BD16C File Offset: 0x000BC36C
				internal FieldInfo AddField(RuntimeFieldHandleInternal field)
				{
					T[] allMembers = this.m_allMembers;
					if (allMembers != null)
					{
						foreach (T t in allMembers)
						{
							if (t == null)
							{
								break;
							}
							RtFieldInfo rtFieldInfo = t as RtFieldInfo;
							if (rtFieldInfo != null && rtFieldInfo.GetFieldHandle() == field.Value)
							{
								return rtFieldInfo;
							}
						}
					}
					FieldAttributes attributes = RuntimeFieldHandle.GetAttributes(field);
					bool isPublic = (attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
					bool isStatic = (attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
					RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field);
					bool isInherited = RuntimeFieldHandle.AcquiresContextFromThis(field) ? (!RuntimeTypeHandle.CompareCanonicalHandles(approxDeclaringType, this.ReflectedType)) : (approxDeclaringType != this.ReflectedType);
					BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, isInherited, isStatic);
					T[] array2 = (T[])new RuntimeFieldInfo[]
					{
						new RtFieldInfo(field, this.ReflectedType, this.m_runtimeTypeCache, bindingFlags)
					};
					this.Insert(ref array2, null, RuntimeType.MemberListType.HandleToInfo);
					return (FieldInfo)((object)array2[0]);
				}

				// Token: 0x06000662 RID: 1634 RVA: 0x000BD26C File Offset: 0x000BC46C
				private unsafe T[] Populate(string name, RuntimeType.MemberListType listType, RuntimeType.RuntimeTypeCache.CacheType cacheType)
				{
					T[] listByName;
					if (string.IsNullOrEmpty(name) || (cacheType == RuntimeType.RuntimeTypeCache.CacheType.Constructor && name[0] != '.' && name[0] != '*'))
					{
						listByName = this.GetListByName(null, 0, null, 0, listType, cacheType);
					}
					else
					{
						int length = name.Length;
						char* ptr;
						if (name == null)
						{
							ptr = null;
						}
						else
						{
							fixed (char* ptr2 = name.GetPinnableReference())
							{
								ptr = ptr2;
							}
						}
						char* ptr3 = ptr;
						int byteCount = Encoding.UTF8.GetByteCount(ptr3, length);
						if (byteCount > 1024)
						{
							byte[] array = new byte[byteCount];
							fixed (byte* ptr4 = &array[0])
							{
								byte* pUtf8Name = ptr4;
								listByName = this.GetListByName(ptr3, length, pUtf8Name, byteCount, listType, cacheType);
							}
						}
						else
						{
							byte* pUtf8Name2 = stackalloc byte[(UIntPtr)byteCount];
							listByName = this.GetListByName(ptr3, length, pUtf8Name2, byteCount, listType, cacheType);
						}
						char* ptr2 = null;
					}
					this.Insert(ref listByName, name, listType);
					return listByName;
				}

				// Token: 0x06000663 RID: 1635 RVA: 0x000BD32C File Offset: 0x000BC52C
				private unsafe T[] GetListByName(char* pName, int cNameLen, byte* pUtf8Name, int cUtf8Name, RuntimeType.MemberListType listType, RuntimeType.RuntimeTypeCache.CacheType cacheType)
				{
					if (cNameLen != 0)
					{
						Encoding.UTF8.GetBytes(pName, cNameLen, pUtf8Name, cUtf8Name);
					}
					RuntimeType.RuntimeTypeCache.Filter filter = new RuntimeType.RuntimeTypeCache.Filter(pUtf8Name, cUtf8Name, listType);
					object obj = null;
					switch (cacheType)
					{
					case RuntimeType.RuntimeTypeCache.CacheType.Method:
						obj = this.PopulateMethods(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.Constructor:
						obj = this.PopulateConstructors(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.Field:
						obj = this.PopulateFields(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.Property:
						obj = this.PopulateProperties(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.Event:
						obj = this.PopulateEvents(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.Interface:
						obj = this.PopulateInterfaces(filter);
						break;
					case RuntimeType.RuntimeTypeCache.CacheType.NestedType:
						obj = this.PopulateNestedClasses(filter);
						break;
					}
					return (T[])obj;
				}

				// Token: 0x06000664 RID: 1636 RVA: 0x000BD3CC File Offset: 0x000BC5CC
				internal void Insert(ref T[] list, string name, RuntimeType.MemberListType listType)
				{
					lock (this)
					{
						switch (listType)
						{
						case RuntimeType.MemberListType.All:
							if (!this.m_cacheComplete)
							{
								this.MergeWithGlobalList(list);
								int num = this.m_allMembers.Length;
								while (num > 0 && !(this.m_allMembers[num - 1] != null))
								{
									num--;
								}
								Array.Resize<T>(ref this.m_allMembers, num);
								Volatile.Write(ref this.m_cacheComplete, true);
							}
							list = this.m_allMembers;
							break;
						case RuntimeType.MemberListType.CaseSensitive:
						{
							T[] array = this.m_csMemberInfos[name];
							if (array == null)
							{
								this.MergeWithGlobalList(list);
								this.m_csMemberInfos[name] = list;
							}
							else
							{
								list = array;
							}
							break;
						}
						case RuntimeType.MemberListType.CaseInsensitive:
						{
							T[] array2 = this.m_cisMemberInfos[name];
							if (array2 == null)
							{
								this.MergeWithGlobalList(list);
								this.m_cisMemberInfos[name] = list;
							}
							else
							{
								list = array2;
							}
							break;
						}
						default:
							this.MergeWithGlobalList(list);
							break;
						}
					}
				}

				// Token: 0x06000665 RID: 1637 RVA: 0x000BD4E0 File Offset: 0x000BC6E0
				private void MergeWithGlobalList(T[] list)
				{
					T[] array = this.m_allMembers;
					if (array == null)
					{
						this.m_allMembers = list;
						return;
					}
					int num = array.Length;
					int num2 = 0;
					for (int i = 0; i < list.Length; i++)
					{
						T t = list[i];
						bool flag = false;
						int j;
						for (j = 0; j < num; j++)
						{
							T t2 = array[j];
							if (t2 == null)
							{
								break;
							}
							if (t.CacheEquals(t2))
							{
								list[i] = t2;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (num2 == 0)
							{
								num2 = j;
							}
							if (num2 >= array.Length)
							{
								int newSize;
								if (this.m_cacheComplete)
								{
									newSize = array.Length + 1;
								}
								else
								{
									newSize = Math.Max(Math.Max(4, 2 * array.Length), list.Length);
								}
								T[] array2 = array;
								Array.Resize<T>(ref array2, newSize);
								array = array2;
							}
							Volatile.Write<T>(ref array[num2], t);
							num2++;
						}
					}
					this.m_allMembers = array;
				}

				// Token: 0x06000666 RID: 1638 RVA: 0x000BD5D0 File Offset: 0x000BC7D0
				private unsafe RuntimeMethodInfo[] PopulateMethods(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					RuntimeType.ListBuilder<RuntimeMethodInfo> listBuilder = default(RuntimeType.ListBuilder<RuntimeMethodInfo>);
					RuntimeType runtimeType = this.ReflectedType;
					if (RuntimeTypeHandle.IsInterface(runtimeType))
					{
						foreach (RuntimeMethodHandleInternal method in RuntimeTypeHandle.GetIntroducedMethods(runtimeType))
						{
							if (!filter.RequiresStringComparison() || (RuntimeMethodHandle.MatchesNameHash(method, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(method))))
							{
								MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method);
								if ((attributes & MethodAttributes.RTSpecialName) == MethodAttributes.PrivateScope)
								{
									bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
									bool isStatic = (attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
									BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, false, isStatic);
									RuntimeMethodHandleInternal stubIfNeeded = RuntimeMethodHandle.GetStubIfNeeded(method, runtimeType, null);
									RuntimeMethodInfo item = new RuntimeMethodInfo(stubIfNeeded, runtimeType, this.m_runtimeTypeCache, attributes, bindingFlags, null);
									listBuilder.Add(item);
								}
							}
						}
					}
					else
					{
						while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
						{
							runtimeType = runtimeType.GetBaseType();
						}
						int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
						bool* ptr = stackalloc bool[(UIntPtr)numVirtuals];
						new Span<bool>((void*)ptr, numVirtuals).Clear();
						bool isValueType = runtimeType.IsValueType;
						do
						{
							int numVirtuals2 = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
							foreach (RuntimeMethodHandleInternal method2 in RuntimeTypeHandle.GetIntroducedMethods(runtimeType))
							{
								if (!filter.RequiresStringComparison() || (RuntimeMethodHandle.MatchesNameHash(method2, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(method2))))
								{
									MethodAttributes attributes2 = RuntimeMethodHandle.GetAttributes(method2);
									MethodAttributes methodAttributes = attributes2 & MethodAttributes.MemberAccessMask;
									if ((attributes2 & MethodAttributes.RTSpecialName) == MethodAttributes.PrivateScope)
									{
										bool flag = false;
										int num = 0;
										if ((attributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
										{
											num = RuntimeMethodHandle.GetSlot(method2);
											flag = (num < numVirtuals2);
										}
										bool flag2 = runtimeType != this.ReflectedType;
										bool flag3 = methodAttributes == MethodAttributes.Private;
										if (!flag2 || !flag3 || flag)
										{
											if (flag)
											{
												if (ptr[num])
												{
													continue;
												}
												ptr[num] = true;
											}
											else if (isValueType && (attributes2 & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != MethodAttributes.PrivateScope)
											{
												continue;
											}
											bool isPublic2 = methodAttributes == MethodAttributes.Public;
											bool isStatic2 = (attributes2 & MethodAttributes.Static) > MethodAttributes.PrivateScope;
											BindingFlags bindingFlags2 = RuntimeType.FilterPreCalculate(isPublic2, flag2, isStatic2);
											RuntimeMethodHandleInternal stubIfNeeded2 = RuntimeMethodHandle.GetStubIfNeeded(method2, runtimeType, null);
											RuntimeMethodInfo item2 = new RuntimeMethodInfo(stubIfNeeded2, runtimeType, this.m_runtimeTypeCache, attributes2, bindingFlags2, null);
											listBuilder.Add(item2);
										}
									}
								}
							}
							runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
						}
						while (runtimeType != null);
					}
					return listBuilder.ToArray();
				}

				// Token: 0x06000667 RID: 1639 RVA: 0x000BD820 File Offset: 0x000BCA20
				private RuntimeConstructorInfo[] PopulateConstructors(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					if (this.ReflectedType.IsGenericParameter)
					{
						return Array.Empty<RuntimeConstructorInfo>();
					}
					RuntimeType.ListBuilder<RuntimeConstructorInfo> listBuilder = default(RuntimeType.ListBuilder<RuntimeConstructorInfo>);
					RuntimeType reflectedType = this.ReflectedType;
					foreach (RuntimeMethodHandleInternal method in RuntimeTypeHandle.GetIntroducedMethods(reflectedType))
					{
						if (!filter.RequiresStringComparison() || (RuntimeMethodHandle.MatchesNameHash(method, filter.GetHashToMatch()) && filter.Match(RuntimeMethodHandle.GetUtf8Name(method))))
						{
							MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method);
							if ((attributes & MethodAttributes.RTSpecialName) != MethodAttributes.PrivateScope)
							{
								bool isPublic = (attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
								bool isStatic = (attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
								BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, false, isStatic);
								RuntimeMethodHandleInternal stubIfNeeded = RuntimeMethodHandle.GetStubIfNeeded(method, reflectedType, null);
								RuntimeConstructorInfo item = new RuntimeConstructorInfo(stubIfNeeded, this.ReflectedType, this.m_runtimeTypeCache, attributes, bindingFlags);
								listBuilder.Add(item);
							}
						}
					}
					return listBuilder.ToArray();
				}

				// Token: 0x06000668 RID: 1640 RVA: 0x000BD908 File Offset: 0x000BCB08
				private RuntimeFieldInfo[] PopulateFields(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					RuntimeType.ListBuilder<RuntimeFieldInfo> listBuilder = default(RuntimeType.ListBuilder<RuntimeFieldInfo>);
					RuntimeType runtimeType = this.ReflectedType;
					while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
					{
						runtimeType = runtimeType.GetBaseType();
					}
					while (runtimeType != null)
					{
						this.PopulateRtFields(filter, runtimeType, ref listBuilder);
						this.PopulateLiteralFields(filter, runtimeType, ref listBuilder);
						runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
					}
					if (this.ReflectedType.IsGenericParameter)
					{
						Type[] interfaces = this.ReflectedType.BaseType.GetInterfaces();
						for (int i = 0; i < interfaces.Length; i++)
						{
							this.PopulateLiteralFields(filter, (RuntimeType)interfaces[i], ref listBuilder);
							this.PopulateRtFields(filter, (RuntimeType)interfaces[i], ref listBuilder);
						}
					}
					else
					{
						Type[] interfaces2 = RuntimeTypeHandle.GetInterfaces(this.ReflectedType);
						if (interfaces2 != null)
						{
							for (int j = 0; j < interfaces2.Length; j++)
							{
								this.PopulateLiteralFields(filter, (RuntimeType)interfaces2[j], ref listBuilder);
								this.PopulateRtFields(filter, (RuntimeType)interfaces2[j], ref listBuilder);
							}
						}
					}
					return listBuilder.ToArray();
				}

				// Token: 0x06000669 RID: 1641 RVA: 0x000BD9FC File Offset: 0x000BCBFC
				private unsafe void PopulateRtFields(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
				{
					IntPtr* ptr = stackalloc IntPtr[checked(unchecked((UIntPtr)64) * (UIntPtr)sizeof(IntPtr))];
					int num = 64;
					if (!RuntimeTypeHandle.GetFields(declaringType, ptr, &num))
					{
						IntPtr[] array;
						IntPtr* ptr2;
						if ((array = new IntPtr[num]) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						RuntimeTypeHandle.GetFields(declaringType, ptr2, &num);
						this.PopulateRtFields(filter, ptr2, num, declaringType, ref list);
						array = null;
						return;
					}
					if (num > 0)
					{
						this.PopulateRtFields(filter, ptr, num, declaringType, ref list);
					}
				}

				// Token: 0x0600066A RID: 1642 RVA: 0x000BDA6C File Offset: 0x000BCC6C
				private unsafe void PopulateRtFields(RuntimeType.RuntimeTypeCache.Filter filter, IntPtr* ppFieldHandles, int count, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
				{
					bool flag = RuntimeTypeHandle.HasInstantiation(declaringType) && !RuntimeTypeHandle.ContainsGenericVariables(declaringType);
					bool flag2 = declaringType != this.ReflectedType;
					for (int i = 0; i < count; i++)
					{
						RuntimeFieldHandleInternal staticFieldForGenericType = new RuntimeFieldHandleInternal(ppFieldHandles[i]);
						if (!filter.RequiresStringComparison() || (RuntimeFieldHandle.MatchesNameHash(staticFieldForGenericType, filter.GetHashToMatch()) && filter.Match(RuntimeFieldHandle.GetUtf8Name(staticFieldForGenericType))))
						{
							FieldAttributes attributes = RuntimeFieldHandle.GetAttributes(staticFieldForGenericType);
							FieldAttributes fieldAttributes = attributes & FieldAttributes.FieldAccessMask;
							if (!flag2 || fieldAttributes != FieldAttributes.Private)
							{
								bool isPublic = fieldAttributes == FieldAttributes.Public;
								bool flag3 = (attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
								BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, flag2, flag3);
								if (flag && flag3)
								{
									staticFieldForGenericType = RuntimeFieldHandle.GetStaticFieldForGenericType(staticFieldForGenericType, declaringType);
								}
								RuntimeFieldInfo item = new RtFieldInfo(staticFieldForGenericType, declaringType, this.m_runtimeTypeCache, bindingFlags);
								list.Add(item);
							}
						}
					}
				}

				// Token: 0x0600066B RID: 1643 RVA: 0x000BDB48 File Offset: 0x000BCD48
				private void PopulateLiteralFields(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, ref RuntimeType.ListBuilder<RuntimeFieldInfo> list)
				{
					int token = RuntimeTypeHandle.GetToken(declaringType);
					if (System.Reflection.MetadataToken.IsNullToken(token))
					{
						return;
					}
					MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(declaringType);
					MetadataEnumResult metadataEnumResult;
					metadataImport.EnumFields(token, out metadataEnumResult);
					for (int i = 0; i < metadataEnumResult.Length; i++)
					{
						int num = metadataEnumResult[i];
						FieldAttributes fieldAttributes;
						metadataImport.GetFieldDefProps(num, out fieldAttributes);
						FieldAttributes fieldAttributes2 = fieldAttributes & FieldAttributes.FieldAccessMask;
						if ((fieldAttributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope)
						{
							bool flag = declaringType != this.ReflectedType;
							if (!flag || fieldAttributes2 != FieldAttributes.Private)
							{
								if (filter.RequiresStringComparison())
								{
									MdUtf8String name = metadataImport.GetName(num);
									if (!filter.Match(name))
									{
										goto IL_C5;
									}
								}
								bool isPublic = fieldAttributes2 == FieldAttributes.Public;
								bool isStatic = (fieldAttributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
								BindingFlags bindingFlags = RuntimeType.FilterPreCalculate(isPublic, flag, isStatic);
								RuntimeFieldInfo item = new MdFieldInfo(num, fieldAttributes, declaringType.GetTypeHandleInternal(), this.m_runtimeTypeCache, bindingFlags);
								list.Add(item);
							}
						}
						IL_C5:;
					}
				}

				// Token: 0x0600066C RID: 1644 RVA: 0x000BDC2C File Offset: 0x000BCE2C
				private void AddSpecialInterface(ref RuntimeType.ListBuilder<RuntimeType> list, RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType iList, bool addSubInterface)
				{
					if (iList.IsAssignableFrom(this.ReflectedType))
					{
						if (filter.Match(RuntimeTypeHandle.GetUtf8Name(iList)))
						{
							list.Add(iList);
						}
						if (addSubInterface)
						{
							foreach (RuntimeType runtimeType in iList.GetInterfaces())
							{
								if (runtimeType.IsGenericType && filter.Match(RuntimeTypeHandle.GetUtf8Name(runtimeType)))
								{
									list.Add(runtimeType);
								}
							}
						}
					}
				}

				// Token: 0x0600066D RID: 1645 RVA: 0x000BDCA0 File Offset: 0x000BCEA0
				private RuntimeType[] PopulateInterfaces(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					RuntimeType.ListBuilder<RuntimeType> listBuilder = default(RuntimeType.ListBuilder<RuntimeType>);
					RuntimeType reflectedType = this.ReflectedType;
					if (!RuntimeTypeHandle.IsGenericVariable(reflectedType))
					{
						Type[] interfaces = RuntimeTypeHandle.GetInterfaces(reflectedType);
						if (interfaces != null)
						{
							foreach (RuntimeType runtimeType in interfaces)
							{
								if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(runtimeType)))
								{
									listBuilder.Add(runtimeType);
								}
							}
						}
						if (this.ReflectedType.IsSZArray)
						{
							RuntimeType runtimeType2 = (RuntimeType)this.ReflectedType.GetElementType();
							if (!runtimeType2.IsPointer)
							{
								this.AddSpecialInterface(ref listBuilder, filter, (RuntimeType)typeof(IList<>).MakeGenericType(new Type[]
								{
									runtimeType2
								}), true);
								this.AddSpecialInterface(ref listBuilder, filter, (RuntimeType)typeof(IReadOnlyList<>).MakeGenericType(new Type[]
								{
									runtimeType2
								}), false);
								this.AddSpecialInterface(ref listBuilder, filter, (RuntimeType)typeof(IReadOnlyCollection<>).MakeGenericType(new Type[]
								{
									runtimeType2
								}), false);
							}
						}
					}
					else
					{
						HashSet<RuntimeType> hashSet = new HashSet<RuntimeType>();
						foreach (RuntimeType runtimeType3 in reflectedType.GetGenericParameterConstraints())
						{
							if (runtimeType3.IsInterface)
							{
								hashSet.Add(runtimeType3);
							}
							Type[] interfaces2 = runtimeType3.GetInterfaces();
							for (int k = 0; k < interfaces2.Length; k++)
							{
								hashSet.Add((RuntimeType)interfaces2[k]);
							}
						}
						foreach (RuntimeType runtimeType4 in hashSet)
						{
							if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(runtimeType4)))
							{
								listBuilder.Add(runtimeType4);
							}
						}
					}
					return listBuilder.ToArray();
				}

				// Token: 0x0600066E RID: 1646 RVA: 0x000BDE90 File Offset: 0x000BD090
				private RuntimeType[] PopulateNestedClasses(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					RuntimeType runtimeType = this.ReflectedType;
					while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
					{
						runtimeType = runtimeType.GetBaseType();
					}
					int token = RuntimeTypeHandle.GetToken(runtimeType);
					if (System.Reflection.MetadataToken.IsNullToken(token))
					{
						return Array.Empty<RuntimeType>();
					}
					RuntimeType.ListBuilder<RuntimeType> listBuilder = default(RuntimeType.ListBuilder<RuntimeType>);
					RuntimeModule module = RuntimeTypeHandle.GetModule(runtimeType);
					MetadataEnumResult metadataEnumResult;
					ModuleHandle.GetMetadataImport(module).EnumNestedTypes(token, out metadataEnumResult);
					int i = 0;
					while (i < metadataEnumResult.Length)
					{
						RuntimeType runtimeType2;
						try
						{
							runtimeType2 = ModuleHandle.ResolveTypeHandleInternal(module, metadataEnumResult[i], null, null);
						}
						catch (TypeLoadException)
						{
							goto IL_8E;
						}
						goto IL_6C;
						IL_8E:
						i++;
						continue;
						IL_6C:
						if (!filter.RequiresStringComparison() || filter.Match(RuntimeTypeHandle.GetUtf8Name(runtimeType2)))
						{
							listBuilder.Add(runtimeType2);
							goto IL_8E;
						}
						goto IL_8E;
					}
					return listBuilder.ToArray();
				}

				// Token: 0x0600066F RID: 1647 RVA: 0x000BDF54 File Offset: 0x000BD154
				private RuntimeEventInfo[] PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					Dictionary<string, RuntimeEventInfo> csEventInfos = filter.CaseSensitive() ? null : new Dictionary<string, RuntimeEventInfo>();
					RuntimeType runtimeType = this.ReflectedType;
					RuntimeType.ListBuilder<RuntimeEventInfo> listBuilder = default(RuntimeType.ListBuilder<RuntimeEventInfo>);
					if (!RuntimeTypeHandle.IsInterface(runtimeType))
					{
						while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
						{
							runtimeType = runtimeType.GetBaseType();
						}
						while (runtimeType != null)
						{
							this.PopulateEvents(filter, runtimeType, csEventInfos, ref listBuilder);
							runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
						}
					}
					else
					{
						this.PopulateEvents(filter, runtimeType, csEventInfos, ref listBuilder);
					}
					return listBuilder.ToArray();
				}

				// Token: 0x06000670 RID: 1648 RVA: 0x000BDFCC File Offset: 0x000BD1CC
				private void PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, Dictionary<string, RuntimeEventInfo> csEventInfos, ref RuntimeType.ListBuilder<RuntimeEventInfo> list)
				{
					int token = RuntimeTypeHandle.GetToken(declaringType);
					if (System.Reflection.MetadataToken.IsNullToken(token))
					{
						return;
					}
					MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(declaringType);
					MetadataEnumResult metadataEnumResult;
					metadataImport.EnumEvents(token, out metadataEnumResult);
					int i = 0;
					while (i < metadataEnumResult.Length)
					{
						int num = metadataEnumResult[i];
						if (!filter.RequiresStringComparison())
						{
							goto IL_51;
						}
						MdUtf8String name = metadataImport.GetName(num);
						if (filter.Match(name))
						{
							goto IL_51;
						}
						IL_AE:
						i++;
						continue;
						IL_51:
						bool flag;
						RuntimeEventInfo runtimeEventInfo = new RuntimeEventInfo(num, declaringType, this.m_runtimeTypeCache, ref flag);
						if (!(declaringType != this.m_runtimeTypeCache.GetRuntimeType()) || !flag)
						{
							if (csEventInfos != null)
							{
								string name2 = runtimeEventInfo.Name;
								if (csEventInfos.ContainsKey(name2))
								{
									goto IL_AE;
								}
								csEventInfos[name2] = runtimeEventInfo;
							}
							else if (list.Count > 0)
							{
								break;
							}
							list.Add(runtimeEventInfo);
							goto IL_AE;
						}
						goto IL_AE;
					}
				}

				// Token: 0x06000671 RID: 1649 RVA: 0x000BE098 File Offset: 0x000BD298
				private RuntimePropertyInfo[] PopulateProperties(RuntimeType.RuntimeTypeCache.Filter filter)
				{
					RuntimeType runtimeType = this.ReflectedType;
					RuntimeType.ListBuilder<RuntimePropertyInfo> listBuilder = default(RuntimeType.ListBuilder<RuntimePropertyInfo>);
					if (!RuntimeTypeHandle.IsInterface(runtimeType))
					{
						while (RuntimeTypeHandle.IsGenericVariable(runtimeType))
						{
							runtimeType = runtimeType.GetBaseType();
						}
						Dictionary<string, List<RuntimePropertyInfo>> csPropertyInfos = filter.CaseSensitive() ? null : new Dictionary<string, List<RuntimePropertyInfo>>();
						bool[] usedSlots = new bool[RuntimeTypeHandle.GetNumVirtuals(runtimeType)];
						do
						{
							this.PopulateProperties(filter, runtimeType, csPropertyInfos, usedSlots, ref listBuilder);
							runtimeType = RuntimeTypeHandle.GetBaseType(runtimeType);
						}
						while (runtimeType != null);
					}
					else
					{
						this.PopulateProperties(filter, runtimeType, null, null, ref listBuilder);
					}
					return listBuilder.ToArray();
				}

				// Token: 0x06000672 RID: 1650 RVA: 0x000BE11C File Offset: 0x000BD31C
				private void PopulateProperties(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeType declaringType, Dictionary<string, List<RuntimePropertyInfo>> csPropertyInfos, bool[] usedSlots, ref RuntimeType.ListBuilder<RuntimePropertyInfo> list)
				{
					int token = RuntimeTypeHandle.GetToken(declaringType);
					if (System.Reflection.MetadataToken.IsNullToken(token))
					{
						return;
					}
					MetadataEnumResult metadataEnumResult;
					RuntimeTypeHandle.GetMetadataImport(declaringType).EnumProperties(token, out metadataEnumResult);
					RuntimeModule module = RuntimeTypeHandle.GetModule(declaringType);
					int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(declaringType);
					int i = 0;
					while (i < metadataEnumResult.Length)
					{
						int num = metadataEnumResult[i];
						if (!filter.RequiresStringComparison())
						{
							goto IL_86;
						}
						if (ModuleHandle.ContainsPropertyMatchingHash(module, num, filter.GetHashToMatch()))
						{
							MdUtf8String name = declaringType.GetRuntimeModule().MetadataImport.GetName(num);
							if (filter.Match(name))
							{
								goto IL_86;
							}
						}
						IL_198:
						i++;
						continue;
						IL_86:
						bool flag;
						RuntimePropertyInfo runtimePropertyInfo = new RuntimePropertyInfo(num, declaringType, this.m_runtimeTypeCache, ref flag);
						if (usedSlots != null)
						{
							if (declaringType != this.ReflectedType && flag)
							{
								goto IL_198;
							}
							MethodInfo methodInfo = runtimePropertyInfo.GetGetMethod() ?? runtimePropertyInfo.GetSetMethod();
							if (methodInfo != null)
							{
								int slot = RuntimeMethodHandle.GetSlot((RuntimeMethodInfo)methodInfo);
								if (slot < numVirtuals)
								{
									if (usedSlots[slot])
									{
										goto IL_198;
									}
									usedSlots[slot] = true;
								}
							}
							if (csPropertyInfos != null)
							{
								string name2 = runtimePropertyInfo.Name;
								List<RuntimePropertyInfo> list2;
								if (!csPropertyInfos.TryGetValue(name2, out list2))
								{
									list2 = new List<RuntimePropertyInfo>(1);
									csPropertyInfos[name2] = list2;
								}
								for (int j = 0; j < list2.Count; j++)
								{
									if (runtimePropertyInfo.EqualsSig(list2[j]))
									{
										list2 = null;
										break;
									}
								}
								if (list2 == null)
								{
									goto IL_198;
								}
								list2.Add(runtimePropertyInfo);
							}
							else
							{
								bool flag2 = false;
								for (int k = 0; k < list.Count; k++)
								{
									if (runtimePropertyInfo.EqualsSig(list[k]))
									{
										flag2 = true;
										break;
									}
								}
								if (flag2)
								{
									goto IL_198;
								}
							}
						}
						list.Add(runtimePropertyInfo);
						goto IL_198;
					}
				}

				// Token: 0x06000673 RID: 1651 RVA: 0x000BE2D8 File Offset: 0x000BD4D8
				internal T[] GetMemberList(RuntimeType.MemberListType listType, string name, RuntimeType.RuntimeTypeCache.CacheType cacheType)
				{
					if (listType == RuntimeType.MemberListType.CaseSensitive)
					{
						return this.m_csMemberInfos[name] ?? this.Populate(name, listType, cacheType);
					}
					if (listType == RuntimeType.MemberListType.CaseInsensitive)
					{
						return this.m_cisMemberInfos[name] ?? this.Populate(name, listType, cacheType);
					}
					if (Volatile.Read(ref this.m_cacheComplete))
					{
						return this.m_allMembers;
					}
					return this.Populate(null, listType, cacheType);
				}

				// Token: 0x17000089 RID: 137
				// (get) Token: 0x06000674 RID: 1652 RVA: 0x000BE340 File Offset: 0x000BD540
				internal RuntimeType ReflectedType
				{
					get
					{
						return this.m_runtimeTypeCache.GetRuntimeType();
					}
				}

				// Token: 0x040001FC RID: 508
				private CerHashtable<string, T[]> m_csMemberInfos;

				// Token: 0x040001FD RID: 509
				private CerHashtable<string, T[]> m_cisMemberInfos;

				// Token: 0x040001FE RID: 510
				private T[] m_allMembers;

				// Token: 0x040001FF RID: 511
				private bool m_cacheComplete;

				// Token: 0x04000200 RID: 512
				private readonly RuntimeType.RuntimeTypeCache m_runtimeTypeCache;
			}
		}

		// Token: 0x02000092 RID: 146
		private sealed class ActivatorCache
		{
			// Token: 0x06000675 RID: 1653 RVA: 0x000BE34D File Offset: 0x000BD54D
			internal ActivatorCache(RuntimeMethodHandleInternal rmh)
			{
				this._hCtorMethodHandle = rmh;
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x000BE35C File Offset: 0x000BD55C
			private void Initialize()
			{
				if (!this._hCtorMethodHandle.IsNullHandle())
				{
					this._ctorAttributes = RuntimeMethodHandle.GetAttributes(this._hCtorMethodHandle);
					ConstructorInfo constructorInfo;
					if ((constructorInfo = RuntimeType.ActivatorCache.s_delegateCtorInfo) == null)
					{
						constructorInfo = (RuntimeType.ActivatorCache.s_delegateCtorInfo = typeof(CtorDelegate).GetConstructor(new Type[]
						{
							typeof(object),
							typeof(IntPtr)
						}));
					}
					ConstructorInfo constructorInfo2 = constructorInfo;
					this._ctor = (CtorDelegate)constructorInfo2.Invoke(new object[]
					{
						null,
						RuntimeMethodHandle.GetFunctionPointer(this._hCtorMethodHandle)
					});
				}
				this._isFullyInitialized = true;
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x000BE3FC File Offset: 0x000BD5FC
			public void EnsureInitialized()
			{
				if (!this._isFullyInitialized)
				{
					this.Initialize();
				}
			}

			// Token: 0x04000201 RID: 513
			internal readonly RuntimeMethodHandleInternal _hCtorMethodHandle;

			// Token: 0x04000202 RID: 514
			internal MethodAttributes _ctorAttributes;

			// Token: 0x04000203 RID: 515
			internal CtorDelegate _ctor;

			// Token: 0x04000204 RID: 516
			internal volatile bool _isFullyInitialized;

			// Token: 0x04000205 RID: 517
			private static ConstructorInfo s_delegateCtorInfo;
		}

		// Token: 0x02000093 RID: 147
		[Flags]
		private enum DispatchWrapperType
		{
			// Token: 0x04000207 RID: 519
			Unknown = 1,
			// Token: 0x04000208 RID: 520
			Dispatch = 2,
			// Token: 0x04000209 RID: 521
			Error = 8,
			// Token: 0x0400020A RID: 522
			Currency = 16,
			// Token: 0x0400020B RID: 523
			BStr = 32,
			// Token: 0x0400020C RID: 524
			SafeArray = 65536
		}
	}
}
