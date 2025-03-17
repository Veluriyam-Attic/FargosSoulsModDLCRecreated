﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Reflection
{
	// Token: 0x02000606 RID: 1542
	internal static class SignatureTypeExtensions
	{
		// Token: 0x06004DFC RID: 19964 RVA: 0x0018C900 File Offset: 0x0018BB00
		public static bool MatchesParameterTypeExactly(this Type pattern, ParameterInfo parameter)
		{
			SignatureType signatureType = pattern as SignatureType;
			if (signatureType != null)
			{
				return signatureType.MatchesExactly(parameter.ParameterType);
			}
			return pattern == parameter.ParameterType;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0018C930 File Offset: 0x0018BB30
		internal static bool MatchesExactly(this SignatureType pattern, Type actual)
		{
			if (pattern.IsSZArray)
			{
				return actual.IsSZArray && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsVariableBoundArray)
			{
				return actual.IsVariableBoundArray && pattern.GetArrayRank() == actual.GetArrayRank() && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsByRef)
			{
				return actual.IsByRef && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsPointer)
			{
				return actual.IsPointer && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (!pattern.IsConstructedGenericType)
			{
				return pattern.IsGenericMethodParameter && actual.IsGenericMethodParameter && pattern.GenericParameterPosition == actual.GenericParameterPosition;
			}
			if (!actual.IsConstructedGenericType)
			{
				return false;
			}
			if (!(pattern.GetGenericTypeDefinition() == actual.GetGenericTypeDefinition()))
			{
				return false;
			}
			Type[] genericTypeArguments = pattern.GenericTypeArguments;
			Type[] genericTypeArguments2 = actual.GenericTypeArguments;
			int num = genericTypeArguments.Length;
			if (num != genericTypeArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				Type type = genericTypeArguments[i];
				SignatureType signatureType = type as SignatureType;
				if (signatureType != null)
				{
					if (!signatureType.MatchesExactly(genericTypeArguments2[i]))
					{
						return false;
					}
				}
				else if (type != genericTypeArguments2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0018CA7D File Offset: 0x0018BC7D
		internal static Type TryResolveAgainstGenericMethod(this SignatureType signatureType, MethodInfo genericMethod)
		{
			return signatureType.TryResolve(genericMethod.GetGenericArguments());
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x0018CA8C File Offset: 0x0018BC8C
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2055:UnrecognizedReflectionPattern", Justification = "Used to find matching method overloads. Only used for assignability checks.")]
		private static Type TryResolve(this SignatureType signatureType, Type[] genericMethodParameters)
		{
			if (signatureType.IsSZArray)
			{
				Type type = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type == null)
				{
					return null;
				}
				return type.TryMakeArrayType();
			}
			else if (signatureType.IsVariableBoundArray)
			{
				Type type2 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type2 == null)
				{
					return null;
				}
				return type2.TryMakeArrayType(signatureType.GetArrayRank());
			}
			else if (signatureType.IsByRef)
			{
				Type type3 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type3 == null)
				{
					return null;
				}
				return type3.TryMakeByRefType();
			}
			else if (signatureType.IsPointer)
			{
				Type type4 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type4 == null)
				{
					return null;
				}
				return type4.TryMakePointerType();
			}
			else
			{
				if (signatureType.IsConstructedGenericType)
				{
					Type[] genericTypeArguments = signatureType.GenericTypeArguments;
					int num = genericTypeArguments.Length;
					Type[] array = new Type[num];
					for (int i = 0; i < num; i++)
					{
						Type type5 = genericTypeArguments[i];
						SignatureType signatureType2 = type5 as SignatureType;
						if (signatureType2 != null)
						{
							array[i] = signatureType2.TryResolve(genericMethodParameters);
							if (array[i] == null)
							{
								return null;
							}
						}
						else
						{
							array[i] = type5;
						}
					}
					return signatureType.GetGenericTypeDefinition().TryMakeGenericType(array);
				}
				if (!signatureType.IsGenericMethodParameter)
				{
					return null;
				}
				int genericParameterPosition = signatureType.GenericParameterPosition;
				if (genericParameterPosition >= genericMethodParameters.Length)
				{
					return null;
				}
				return genericMethodParameters[genericParameterPosition];
			}
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0018CBA0 File Offset: 0x0018BDA0
		private static Type TryMakeArrayType(this Type type)
		{
			Type result;
			try
			{
				result = type.MakeArrayType();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x0018CBCC File Offset: 0x0018BDCC
		private static Type TryMakeArrayType(this Type type, int rank)
		{
			Type result;
			try
			{
				result = type.MakeArrayType(rank);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x0018CBFC File Offset: 0x0018BDFC
		private static Type TryMakeByRefType(this Type type)
		{
			Type result;
			try
			{
				result = type.MakeByRefType();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x0018CC28 File Offset: 0x0018BE28
		private static Type TryMakePointerType(this Type type)
		{
			Type result;
			try
			{
				result = type.MakePointerType();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x0018CC54 File Offset: 0x0018BE54
		private static Type TryMakeGenericType(this Type type, Type[] instantiation)
		{
			Type result;
			try
			{
				result = type.MakeGenericType(instantiation);
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
