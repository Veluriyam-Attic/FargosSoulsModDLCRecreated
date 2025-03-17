using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005A6 RID: 1446
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class MethodBase : MemberInfo
	{
		// Token: 0x06004A24 RID: 18980 RVA: 0x00186A18 File Offset: 0x00185C18
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(SR.Argument_InvalidHandle);
			}
			MethodBase methodBase = RuntimeType.GetMethodBase(handle.GetMethodInfo());
			Type type = (methodBase != null) ? methodBase.DeclaringType : null;
			if (type != null && type.IsGenericType)
			{
				throw new ArgumentException(SR.Format(SR.Argument_MethodDeclaringTypeGeneric, methodBase, type.GetGenericTypeDefinition()));
			}
			return methodBase;
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x00186A7C File Offset: 0x00185C7C
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(SR.Argument_InvalidHandle);
			}
			return RuntimeType.GetMethodBase(declaringType.GetRuntimeType(), handle.GetMethodInfo());
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x00186AA8 File Offset: 0x00185CA8
		public static MethodBase GetCurrentMethod()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeMethodInfo.InternalGetCurrentMethod(ref stackCrawlMark);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00186AC0 File Offset: 0x00185CC0
		private IntPtr GetMethodDesc()
		{
			return this.MethodHandle.Value;
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00186ADB File Offset: 0x00185CDB
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00186AE4 File Offset: 0x00185CE4
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00186B1C File Offset: 0x00185D1C
		internal object[] CheckArguments(object[] parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
		{
			object[] array = new object[parameters.Length];
			ParameterInfo[] array2 = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				object obj = parameters[i];
				RuntimeType runtimeType = sig.Arguments[i];
				if (obj == Type.Missing)
				{
					if (array2 == null)
					{
						array2 = this.GetParametersNoCopy();
					}
					if (array2[i].DefaultValue == DBNull.Value)
					{
						throw new ArgumentException(SR.Arg_VarMissNull, "parameters");
					}
					obj = array2[i].DefaultValue;
				}
				array[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
			}
			return array;
		}

		// Token: 0x06004A2C RID: 18988
		[NullableContext(1)]
		public abstract ParameterInfo[] GetParameters();

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004A2D RID: 18989
		public abstract MethodAttributes Attributes { get; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x00186B9B File Offset: 0x00185D9B
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		// Token: 0x06004A2F RID: 18991
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x06004A30 RID: 18992 RVA: 0x000F2BDF File Offset: 0x000F1DDF
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x000AC09E File Offset: 0x000AB29E
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x00186BA3 File Offset: 0x00185DA3
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x00186BB4 File Offset: 0x00185DB4
		public bool IsConstructor
		{
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x00186BDB File Offset: 0x00185DDB
		public bool IsFinal
		{
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06004A35 RID: 18997 RVA: 0x00186BE9 File Offset: 0x00185DE9
		public bool IsHideBySig
		{
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x00186BFA File Offset: 0x00185DFA
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x00186C0B File Offset: 0x00185E0B
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x00186C19 File Offset: 0x00185E19
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00186C27 File Offset: 0x00185E27
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x00186C34 File Offset: 0x00185E34
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x00186C41 File Offset: 0x00185E41
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x00186C4E File Offset: 0x00185E4E
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x00186C5B File Offset: 0x00185E5B
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x00186C68 File Offset: 0x00185E68
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x00186C75 File Offset: 0x00185E75
		public virtual bool IsConstructedGenericMethod
		{
			get
			{
				return this.IsGenericMethod && !this.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x000C2761 File Offset: 0x000C1961
		[NullableContext(1)]
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00186C8A File Offset: 0x00185E8A
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x06004A45 RID: 19013
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004A46 RID: 19014
		public abstract RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x00185EF0 File Offset: 0x001850F0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00185EF9 File Offset: 0x001850F9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0018222B File Offset: 0x0018142B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00186C97 File Offset: 0x00185E97
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00186CA4 File Offset: 0x00185EA4
		internal static void AppendParameters(ref ValueStringBuilder sbParamList, Type[] parameterTypes, CallingConventions callingConvention)
		{
			string s = "";
			foreach (Type type in parameterTypes)
			{
				sbParamList.Append(s);
				string text = type.FormatTypeName();
				if (type.IsByRef)
				{
					sbParamList.Append(text.AsSpan().TrimEnd('&'));
					sbParamList.Append(" ByRef");
				}
				else
				{
					sbParamList.Append(text);
				}
				s = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				sbParamList.Append(s);
				sbParamList.Append("...");
			}
		}
	}
}
