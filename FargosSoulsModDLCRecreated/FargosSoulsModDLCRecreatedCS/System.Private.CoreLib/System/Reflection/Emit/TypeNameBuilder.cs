using System;
using System.Collections.Generic;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x0200066D RID: 1645
	internal class TypeNameBuilder
	{
		// Token: 0x060053CC RID: 21452 RVA: 0x0019C398 File Offset: 0x0019B598
		private TypeNameBuilder()
		{
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0019C3B6 File Offset: 0x0019B5B6
		private void OpenGenericArguments()
		{
			this._instNesting++;
			this._firstInstArg = true;
			if (this._useAngleBracketsForGenerics)
			{
				this.Append('<');
				return;
			}
			this.Append('[');
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x0019C3E8 File Offset: 0x0019B5E8
		private void CloseGenericArguments()
		{
			this._instNesting--;
			if (this._firstInstArg)
			{
				this._str.Remove(this._str.Length - 1, 1);
				return;
			}
			if (this._useAngleBracketsForGenerics)
			{
				this.Append('>');
				return;
			}
			this.Append(']');
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0019C43F File Offset: 0x0019B63F
		private void OpenGenericArgument()
		{
			this._nestedName = false;
			if (!this._firstInstArg)
			{
				this.Append(',');
			}
			this._firstInstArg = false;
			if (this._useAngleBracketsForGenerics)
			{
				this.Append('<');
			}
			else
			{
				this.Append('[');
			}
			this.PushOpenGenericArgument();
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0019C47F File Offset: 0x0019B67F
		private void CloseGenericArgument()
		{
			if (this._hasAssemblySpec)
			{
				if (this._useAngleBracketsForGenerics)
				{
					this.Append('>');
				}
				else
				{
					this.Append(']');
				}
			}
			this.PopOpenGenericArgument();
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x0019C4A9 File Offset: 0x0019B6A9
		private void AddName(string name)
		{
			if (this._nestedName)
			{
				this.Append('+');
			}
			this._nestedName = true;
			this.EscapeName(name);
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0019C4CC File Offset: 0x0019B6CC
		private void AddArray(int rank)
		{
			if (rank == 1)
			{
				this.Append("[*]");
				return;
			}
			if (rank > 64)
			{
				this._str.Append('[').Append(rank).Append(']');
				return;
			}
			this.Append('[');
			for (int i = 1; i < rank; i++)
			{
				this.Append(',');
			}
			this.Append(']');
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0019C52E File Offset: 0x0019B72E
		private void AddAssemblySpec(string assemblySpec)
		{
			if (assemblySpec != null && !assemblySpec.Equals(""))
			{
				this.Append(", ");
				if (this._instNesting > 0)
				{
					this.EscapeEmbeddedAssemblyName(assemblySpec);
				}
				else
				{
					this.EscapeAssemblyName(assemblySpec);
				}
				this._hasAssemblySpec = true;
			}
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0019C56B File Offset: 0x0019B76B
		public override string ToString()
		{
			return this._str.ToString();
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0019C578 File Offset: 0x0019B778
		private static bool ContainsReservedChar(string name)
		{
			foreach (char c in name)
			{
				if (c == '\0')
				{
					break;
				}
				if (TypeNameBuilder.IsTypeNameReservedChar(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x0019C5B0 File Offset: 0x0019B7B0
		private static bool IsTypeNameReservedChar(char ch)
		{
			switch (ch)
			{
			case '&':
			case '*':
			case '+':
			case ',':
				break;
			case '\'':
			case '(':
			case ')':
				return false;
			default:
				switch (ch)
				{
				case '[':
				case '\\':
				case ']':
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0019C5FC File Offset: 0x0019B7FC
		private void EscapeName(string name)
		{
			if (TypeNameBuilder.ContainsReservedChar(name))
			{
				foreach (char c in name)
				{
					if (c == '\0')
					{
						return;
					}
					if (TypeNameBuilder.IsTypeNameReservedChar(c))
					{
						this._str.Append('\\');
					}
					this._str.Append(c);
				}
				return;
			}
			this.Append(name);
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0019C65A File Offset: 0x0019B85A
		private void EscapeAssemblyName(string name)
		{
			this.Append(name);
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x0019C664 File Offset: 0x0019B864
		private void EscapeEmbeddedAssemblyName(string name)
		{
			if (name.Contains(']'))
			{
				foreach (char c in name)
				{
					if (c == ']')
					{
						this.Append('\\');
					}
					this.Append(c);
				}
				return;
			}
			this.Append(name);
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x0019C6B2 File Offset: 0x0019B8B2
		private void PushOpenGenericArgument()
		{
			this._stack.Add(this._str.Length);
			this._stackIdx++;
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0019C6D8 File Offset: 0x0019B8D8
		private void PopOpenGenericArgument()
		{
			List<int> stack = this._stack;
			int num = this._stackIdx - 1;
			this._stackIdx = num;
			int num2 = stack[num];
			this._stack.RemoveAt(this._stackIdx);
			if (!this._hasAssemblySpec)
			{
				this._str.Remove(num2 - 1, 1);
			}
			this._hasAssemblySpec = false;
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0019C734 File Offset: 0x0019B934
		private void Append(string pStr)
		{
			foreach (char c in pStr)
			{
				if (c == '\0')
				{
					break;
				}
				this._str.Append(c);
			}
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0019C76C File Offset: 0x0019B96C
		private void Append(char c)
		{
			this._str.Append(c);
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0019C77C File Offset: 0x0019B97C
		internal static string ToString(Type type, TypeNameBuilder.Format format)
		{
			if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && !type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				return null;
			}
			TypeNameBuilder typeNameBuilder = new TypeNameBuilder();
			typeNameBuilder.AddAssemblyQualifiedName(type, format);
			return typeNameBuilder.ToString();
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0019C7B8 File Offset: 0x0019B9B8
		private void AddElementType(Type type)
		{
			if (!type.HasElementType)
			{
				return;
			}
			this.AddElementType(type.GetElementType());
			if (type.IsPointer)
			{
				this.Append('*');
				return;
			}
			if (type.IsByRef)
			{
				this.Append('&');
				return;
			}
			if (type.IsSZArray)
			{
				this.Append("[]");
				return;
			}
			if (type.IsArray)
			{
				this.AddArray(type.GetArrayRank());
			}
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0019C824 File Offset: 0x0019BA24
		private void AddAssemblyQualifiedName(Type type, TypeNameBuilder.Format format)
		{
			Type type2 = type;
			while (type2.HasElementType)
			{
				type2 = type2.GetElementType();
			}
			List<Type> list = new List<Type>();
			Type type3 = type2;
			while (type3 != null)
			{
				list.Add(type3);
				type3 = (type3.IsGenericParameter ? null : type3.DeclaringType);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Type type4 = list[i];
				string text = type4.Name;
				if (i == list.Count - 1 && type4.Namespace != null && type4.Namespace.Length != 0)
				{
					text = type4.Namespace + "." + text;
				}
				this.AddName(text);
			}
			if (type2.IsGenericType && (!type2.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
			{
				Type[] genericArguments = type2.GetGenericArguments();
				this.OpenGenericArguments();
				for (int j = 0; j < genericArguments.Length; j++)
				{
					TypeNameBuilder.Format format2 = (format == TypeNameBuilder.Format.FullName) ? TypeNameBuilder.Format.AssemblyQualifiedName : format;
					this.OpenGenericArgument();
					this.AddAssemblyQualifiedName(genericArguments[j], format2);
					this.CloseGenericArgument();
				}
				this.CloseGenericArguments();
			}
			this.AddElementType(type);
			if (format == TypeNameBuilder.Format.AssemblyQualifiedName)
			{
				this.AddAssemblySpec(type.Module.Assembly.FullName);
			}
		}

		// Token: 0x04001768 RID: 5992
		private StringBuilder _str = new StringBuilder();

		// Token: 0x04001769 RID: 5993
		private int _instNesting;

		// Token: 0x0400176A RID: 5994
		private bool _firstInstArg;

		// Token: 0x0400176B RID: 5995
		private bool _nestedName;

		// Token: 0x0400176C RID: 5996
		private bool _hasAssemblySpec;

		// Token: 0x0400176D RID: 5997
		private bool _useAngleBracketsForGenerics;

		// Token: 0x0400176E RID: 5998
		private List<int> _stack = new List<int>();

		// Token: 0x0400176F RID: 5999
		private int _stackIdx;

		// Token: 0x0200066E RID: 1646
		internal enum Format
		{
			// Token: 0x04001771 RID: 6001
			ToString,
			// Token: 0x04001772 RID: 6002
			FullName,
			// Token: 0x04001773 RID: 6003
			AssemblyQualifiedName
		}
	}
}
