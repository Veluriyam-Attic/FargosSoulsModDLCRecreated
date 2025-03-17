using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x0200009E RID: 158
	internal sealed class TypeNameParser : IDisposable
	{
		// Token: 0x06000859 RID: 2137
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _CreateTypeNameParser(string typeName, ObjectHandleOnStack retHandle, bool throwOnError);

		// Token: 0x0600085A RID: 2138
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetNames(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x0600085B RID: 2139
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetTypeArguments(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x0600085C RID: 2140
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetModifiers(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x0600085D RID: 2141
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetAssemblyName(SafeTypeNameParserHandle pTypeNameParser, StringHandleOnStack retString);

		// Token: 0x0600085E RID: 2142 RVA: 0x000C3F6C File Offset: 0x000C316C
		internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (typeName.Length > 0 && typeName[0] == '\0')
			{
				throw new ArgumentException(SR.Format_StringZeroLength);
			}
			Type result = null;
			SafeTypeNameParserHandle safeTypeNameParserHandle = TypeNameParser.CreateTypeNameParser(typeName, throwOnError);
			if (safeTypeNameParserHandle != null)
			{
				using (TypeNameParser typeNameParser = new TypeNameParser(safeTypeNameParserHandle))
				{
					result = typeNameParser.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
				}
			}
			return result;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000C3FE4 File Offset: 0x000C31E4
		private TypeNameParser(SafeTypeNameParserHandle handle)
		{
			this.m_NativeParser = handle;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000C3FF3 File Offset: 0x000C31F3
		public void Dispose()
		{
			this.m_NativeParser.Dispose();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000C4000 File Offset: 0x000C3200
		private unsafe Type ConstructType(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			Assembly assembly = null;
			string assemblyName = this.GetAssemblyName();
			if (assemblyName.Length > 0)
			{
				assembly = TypeNameParser.ResolveAssembly(assemblyName, assemblyResolver, throwOnError, ref stackMark);
				if (assembly == null)
				{
					return null;
				}
			}
			string[] names = this.GetNames();
			if (names == null)
			{
				if (throwOnError)
				{
					throw new TypeLoadException(SR.Arg_TypeLoadNullStr);
				}
				return null;
			}
			else
			{
				Type type = TypeNameParser.ResolveType(assembly, names, typeResolver, throwOnError, ignoreCase, ref stackMark);
				if (type == null)
				{
					return null;
				}
				SafeTypeNameParserHandle[] typeArguments = this.GetTypeArguments();
				Type[] array = null;
				if (typeArguments != null)
				{
					array = new Type[typeArguments.Length];
					for (int i = 0; i < typeArguments.Length; i++)
					{
						using (TypeNameParser typeNameParser = new TypeNameParser(typeArguments[i]))
						{
							array[i] = typeNameParser.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
						}
						if (array[i] == null)
						{
							return null;
						}
					}
				}
				int[] modifiers = this.GetModifiers();
				int[] array2;
				int* value;
				if ((array2 = modifiers) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				IntPtr pModifiers = new IntPtr((void*)value);
				return RuntimeTypeHandle.GetTypeHelper(type, array, pModifiers, (modifiers == null) ? 0 : modifiers.Length);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000C4128 File Offset: 0x000C3328
		private static Assembly ResolveAssembly(string asmName, Func<AssemblyName, Assembly> assemblyResolver, bool throwOnError, ref StackCrawlMark stackMark)
		{
			if (assemblyResolver == null)
			{
				if (throwOnError)
				{
					return RuntimeAssembly.InternalLoad(asmName, ref stackMark, AssemblyLoadContext.CurrentContextualReflectionContext);
				}
				try
				{
					return RuntimeAssembly.InternalLoad(asmName, ref stackMark, AssemblyLoadContext.CurrentContextualReflectionContext);
				}
				catch (FileNotFoundException)
				{
					return null;
				}
			}
			Assembly assembly = assemblyResolver(new AssemblyName(asmName));
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(SR.Format(SR.FileNotFound_ResolveAssembly, asmName));
			}
			return assembly;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000C419C File Offset: 0x000C339C
		private static Type ResolveType(Assembly assembly, string[] names, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			string text = TypeNameParser.EscapeTypeName(names[0]);
			Type type;
			if (typeResolver != null)
			{
				type = typeResolver(assembly, text, ignoreCase);
				if (type == null && throwOnError)
				{
					string message = (assembly == null) ? SR.Format(SR.TypeLoad_ResolveType, text) : SR.Format(SR.TypeLoad_ResolveTypeFromAssembly, text, assembly.FullName);
					throw new TypeLoadException(message);
				}
			}
			else if (assembly == null)
			{
				type = RuntimeType.GetType(text, throwOnError, ignoreCase, ref stackMark);
			}
			else
			{
				type = assembly.GetType(text, throwOnError, ignoreCase);
			}
			if (type != null)
			{
				BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
				if (ignoreCase)
				{
					bindingFlags |= BindingFlags.IgnoreCase;
				}
				int i = 1;
				while (i < names.Length)
				{
					type = type.GetNestedType(names[i], bindingFlags);
					if (type == null)
					{
						if (throwOnError)
						{
							throw new TypeLoadException(SR.Format(SR.TypeLoad_ResolveNestedType, names[i], names[i - 1]));
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return type;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000C4278 File Offset: 0x000C3478
		private unsafe static string EscapeTypeName(string name)
		{
			if (name.IndexOfAny(TypeNameParser.SPECIAL_CHARS) < 0)
			{
				return name;
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			foreach (char c in name)
			{
				if (Array.IndexOf<char>(TypeNameParser.SPECIAL_CHARS, c) >= 0)
				{
					valueStringBuilder.Append('\\');
				}
				valueStringBuilder.Append(c);
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000C42F8 File Offset: 0x000C34F8
		private static SafeTypeNameParserHandle CreateTypeNameParser(string typeName, bool throwOnError)
		{
			SafeTypeNameParserHandle result = null;
			TypeNameParser._CreateTypeNameParser(typeName, ObjectHandleOnStack.Create<SafeTypeNameParserHandle>(ref result), throwOnError);
			return result;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000C4318 File Offset: 0x000C3518
		private string[] GetNames()
		{
			string[] result = null;
			TypeNameParser._GetNames(this.m_NativeParser, ObjectHandleOnStack.Create<string[]>(ref result));
			return result;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000C433C File Offset: 0x000C353C
		private SafeTypeNameParserHandle[] GetTypeArguments()
		{
			SafeTypeNameParserHandle[] result = null;
			TypeNameParser._GetTypeArguments(this.m_NativeParser, ObjectHandleOnStack.Create<SafeTypeNameParserHandle[]>(ref result));
			return result;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000C4360 File Offset: 0x000C3560
		private int[] GetModifiers()
		{
			int[] result = null;
			TypeNameParser._GetModifiers(this.m_NativeParser, ObjectHandleOnStack.Create<int[]>(ref result));
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000C4384 File Offset: 0x000C3584
		private string GetAssemblyName()
		{
			string result = null;
			TypeNameParser._GetAssemblyName(this.m_NativeParser, new StringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x04000222 RID: 546
		private readonly SafeTypeNameParserHandle m_NativeParser;

		// Token: 0x04000223 RID: 547
		private static readonly char[] SPECIAL_CHARS = new char[]
		{
			',',
			'[',
			']',
			'&',
			'*',
			'+',
			'\\'
		};
	}
}
