using System;
using System.Collections.Generic;
using System.IO;
using System.Private.CoreLib;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000182 RID: 386
	internal static class SR
	{
		// Token: 0x0600139D RID: 5021 RVA: 0x000EEF28 File Offset: 0x000EE128
		internal static string GetResourceString(string resourceKey)
		{
			return SR.GetResourceString(resourceKey, null);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000EEF34 File Offset: 0x000EE134
		private static string InternalGetResourceString(string key)
		{
			if (key.Length == 0)
			{
				return key;
			}
			bool flag = false;
			string result;
			try
			{
				Monitor.Enter(SR._lock, ref flag);
				if (SR._currentlyLoading != null && SR._currentlyLoading.Count > 0 && SR._currentlyLoading.LastIndexOf(key) != -1)
				{
					if (SR._infinitelyRecursingCount > 0)
					{
						return key;
					}
					SR._infinitelyRecursingCount++;
					string message = "Infinite recursion during resource lookup within System.Private.CoreLib.  This may be a bug in System.Private.CoreLib, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: " + key;
					Environment.FailFast(message);
				}
				if (SR._currentlyLoading == null)
				{
					SR._currentlyLoading = new List<string>();
				}
				if (!SR._resourceManagerInited)
				{
					RuntimeHelpers.RunClassConstructor(typeof(ResourceManager).TypeHandle);
					RuntimeHelpers.RunClassConstructor(typeof(ResourceReader).TypeHandle);
					RuntimeHelpers.RunClassConstructor(typeof(RuntimeResourceSet).TypeHandle);
					RuntimeHelpers.RunClassConstructor(typeof(BinaryReader).TypeHandle);
					SR._resourceManagerInited = true;
				}
				SR._currentlyLoading.Add(key);
				string @string = SR.ResourceManager.GetString(key, null);
				SR._currentlyLoading.RemoveAt(SR._currentlyLoading.Count - 1);
				result = (@string ?? key);
			}
			catch
			{
				if (flag)
				{
					SR.s_resourceManager = null;
					SR._currentlyLoading = null;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SR._lock);
				}
			}
			return result;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000EF0A4 File Offset: 0x000EE2A4
		private static bool UsingResourceKeys()
		{
			return SR.s_usingResourceKeys;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000EF0AC File Offset: 0x000EE2AC
		internal static string GetResourceString(string resourceKey, string defaultString = null)
		{
			if (SR.UsingResourceKeys())
			{
				return defaultString ?? resourceKey;
			}
			string text = null;
			try
			{
				text = SR.InternalGetResourceString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000EF0F4 File Offset: 0x000EE2F4
		internal static string Format(string resourceFormat, object p1)
		{
			if (SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000EF11D File Offset: 0x000EE31D
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000EF14B File Offset: 0x000EE34B
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000EF17E File Offset: 0x000EE37E
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000EF1AA File Offset: 0x000EE3AA
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000EF1D4 File Offset: 0x000EE3D4
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000EF203 File Offset: 0x000EE403
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager result;
				if ((result = SR.s_resourceManager) == null)
				{
					result = (SR.s_resourceManager = new ResourceManager(typeof(Strings)));
				}
				return result;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000EF223 File Offset: 0x000EE423
		internal static string Acc_CreateAbstEx
		{
			get
			{
				return SR.GetResourceString("Acc_CreateAbstEx");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000EF22F File Offset: 0x000EE42F
		internal static string Acc_CreateArgIterator
		{
			get
			{
				return SR.GetResourceString("Acc_CreateArgIterator");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000EF23B File Offset: 0x000EE43B
		internal static string Acc_CreateGenericEx
		{
			get
			{
				return SR.GetResourceString("Acc_CreateGenericEx");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x000EF247 File Offset: 0x000EE447
		internal static string Acc_CreateInterfaceEx
		{
			get
			{
				return SR.GetResourceString("Acc_CreateInterfaceEx");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000EF253 File Offset: 0x000EE453
		internal static string Acc_CreateVoid
		{
			get
			{
				return SR.GetResourceString("Acc_CreateVoid");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000EF25F File Offset: 0x000EE45F
		internal static string Acc_NotClassInit
		{
			get
			{
				return SR.GetResourceString("Acc_NotClassInit");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x000EF26B File Offset: 0x000EE46B
		internal static string Acc_ReadOnly
		{
			get
			{
				return SR.GetResourceString("Acc_ReadOnly");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000EF277 File Offset: 0x000EE477
		internal static string Access_Void
		{
			get
			{
				return SR.GetResourceString("Access_Void");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000EF283 File Offset: 0x000EE483
		internal static string AggregateException_ctor_DefaultMessage
		{
			get
			{
				return SR.GetResourceString("AggregateException_ctor_DefaultMessage");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000EF28F File Offset: 0x000EE48F
		internal static string AggregateException_ctor_InnerExceptionNull
		{
			get
			{
				return SR.GetResourceString("AggregateException_ctor_InnerExceptionNull");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000EF29B File Offset: 0x000EE49B
		internal static string AggregateException_DeserializationFailure
		{
			get
			{
				return SR.GetResourceString("AggregateException_DeserializationFailure");
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000EF2A7 File Offset: 0x000EE4A7
		internal static string AggregateException_InnerException
		{
			get
			{
				return SR.GetResourceString("AggregateException_InnerException");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000EF2B3 File Offset: 0x000EE4B3
		internal static string AppDomain_Name
		{
			get
			{
				return SR.GetResourceString("AppDomain_Name");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x000EF2BF File Offset: 0x000EE4BF
		internal static string AppDomain_NoContextPolicies
		{
			get
			{
				return SR.GetResourceString("AppDomain_NoContextPolicies");
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000EF2CB File Offset: 0x000EE4CB
		internal static string AppDomain_Policy_PrincipalTwice
		{
			get
			{
				return SR.GetResourceString("AppDomain_Policy_PrincipalTwice");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x000EF2D7 File Offset: 0x000EE4D7
		internal static string AmbiguousImplementationException_NullMessage
		{
			get
			{
				return SR.GetResourceString("AmbiguousImplementationException_NullMessage");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000EF2E3 File Offset: 0x000EE4E3
		internal static string Arg_AccessException
		{
			get
			{
				return SR.GetResourceString("Arg_AccessException");
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000EF2EF File Offset: 0x000EE4EF
		internal static string Arg_AccessViolationException
		{
			get
			{
				return SR.GetResourceString("Arg_AccessViolationException");
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000EF2FB File Offset: 0x000EE4FB
		internal static string Arg_AmbiguousMatchException
		{
			get
			{
				return SR.GetResourceString("Arg_AmbiguousMatchException");
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x000EF307 File Offset: 0x000EE507
		internal static string Arg_ApplicationException
		{
			get
			{
				return SR.GetResourceString("Arg_ApplicationException");
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000EF313 File Offset: 0x000EE513
		internal static string Arg_ArgumentException
		{
			get
			{
				return SR.GetResourceString("Arg_ArgumentException");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x000EF31F File Offset: 0x000EE51F
		internal static string Arg_ArgumentOutOfRangeException
		{
			get
			{
				return SR.GetResourceString("Arg_ArgumentOutOfRangeException");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x000EF32B File Offset: 0x000EE52B
		internal static string Arg_ArithmeticException
		{
			get
			{
				return SR.GetResourceString("Arg_ArithmeticException");
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000EF337 File Offset: 0x000EE537
		internal static string Arg_ArrayLengthsDiffer
		{
			get
			{
				return SR.GetResourceString("Arg_ArrayLengthsDiffer");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x000EF343 File Offset: 0x000EE543
		internal static string Arg_ArrayPlusOffTooSmall
		{
			get
			{
				return SR.GetResourceString("Arg_ArrayPlusOffTooSmall");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000EF34F File Offset: 0x000EE54F
		internal static string Arg_ArrayTypeMismatchException
		{
			get
			{
				return SR.GetResourceString("Arg_ArrayTypeMismatchException");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000EF35B File Offset: 0x000EE55B
		internal static string Arg_ArrayZeroError
		{
			get
			{
				return SR.GetResourceString("Arg_ArrayZeroError");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000EF367 File Offset: 0x000EE567
		internal static string Arg_BadDecimal
		{
			get
			{
				return SR.GetResourceString("Arg_BadDecimal");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x000EF373 File Offset: 0x000EE573
		internal static string Arg_BadImageFormatException
		{
			get
			{
				return SR.GetResourceString("Arg_BadImageFormatException");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000EF37F File Offset: 0x000EE57F
		internal static string Arg_BadLiteralFormat
		{
			get
			{
				return SR.GetResourceString("Arg_BadLiteralFormat");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x000EF38B File Offset: 0x000EE58B
		internal static string Arg_BogusIComparer
		{
			get
			{
				return SR.GetResourceString("Arg_BogusIComparer");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000EF397 File Offset: 0x000EE597
		internal static string Arg_BufferTooSmall
		{
			get
			{
				return SR.GetResourceString("Arg_BufferTooSmall");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000EF3A3 File Offset: 0x000EE5A3
		internal static string Arg_CannotBeNaN
		{
			get
			{
				return SR.GetResourceString("Arg_CannotBeNaN");
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000EF3AF File Offset: 0x000EE5AF
		internal static string Arg_CannotHaveNegativeValue
		{
			get
			{
				return SR.GetResourceString("Arg_CannotHaveNegativeValue");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x000EF3BB File Offset: 0x000EE5BB
		internal static string Arg_CannotMixComparisonInfrastructure
		{
			get
			{
				return SR.GetResourceString("Arg_CannotMixComparisonInfrastructure");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000EF3C7 File Offset: 0x000EE5C7
		internal static string Arg_CannotUnloadAppDomainException
		{
			get
			{
				return SR.GetResourceString("Arg_CannotUnloadAppDomainException");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x000EF3D3 File Offset: 0x000EE5D3
		internal static string Arg_CATypeResolutionFailed
		{
			get
			{
				return SR.GetResourceString("Arg_CATypeResolutionFailed");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x000EF3DF File Offset: 0x000EE5DF
		internal static string Arg_COMAccess
		{
			get
			{
				return SR.GetResourceString("Arg_COMAccess");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x000EF3EB File Offset: 0x000EE5EB
		internal static string Arg_COMException
		{
			get
			{
				return SR.GetResourceString("Arg_COMException");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x000EF3F7 File Offset: 0x000EE5F7
		internal static string Arg_COMPropSetPut
		{
			get
			{
				return SR.GetResourceString("Arg_COMPropSetPut");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000EF403 File Offset: 0x000EE603
		internal static string Arg_CreatInstAccess
		{
			get
			{
				return SR.GetResourceString("Arg_CreatInstAccess");
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x000EF40F File Offset: 0x000EE60F
		internal static string Arg_CryptographyException
		{
			get
			{
				return SR.GetResourceString("Arg_CryptographyException");
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000EF41B File Offset: 0x000EE61B
		internal static string Arg_CustomAttributeFormatException
		{
			get
			{
				return SR.GetResourceString("Arg_CustomAttributeFormatException");
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x000EF427 File Offset: 0x000EE627
		internal static string Arg_DataMisalignedException
		{
			get
			{
				return SR.GetResourceString("Arg_DataMisalignedException");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000EF433 File Offset: 0x000EE633
		internal static string Arg_DateTimeRange
		{
			get
			{
				return SR.GetResourceString("Arg_DateTimeRange");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000EF43F File Offset: 0x000EE63F
		internal static string Arg_DecBitCtor
		{
			get
			{
				return SR.GetResourceString("Arg_DecBitCtor");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000EF44B File Offset: 0x000EE64B
		internal static string Arg_DirectoryNotFoundException
		{
			get
			{
				return SR.GetResourceString("Arg_DirectoryNotFoundException");
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x000EF457 File Offset: 0x000EE657
		internal static string Arg_DivideByZero
		{
			get
			{
				return SR.GetResourceString("Arg_DivideByZero");
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x000EF463 File Offset: 0x000EE663
		internal static string Arg_DlgtNullInst
		{
			get
			{
				return SR.GetResourceString("Arg_DlgtNullInst");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000EF46F File Offset: 0x000EE66F
		internal static string Arg_DlgtTargMeth
		{
			get
			{
				return SR.GetResourceString("Arg_DlgtTargMeth");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x000EF47B File Offset: 0x000EE67B
		internal static string Arg_DlgtTypeMis
		{
			get
			{
				return SR.GetResourceString("Arg_DlgtTypeMis");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000EF487 File Offset: 0x000EE687
		internal static string Arg_DllNotFoundException
		{
			get
			{
				return SR.GetResourceString("Arg_DllNotFoundException");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x000EF493 File Offset: 0x000EE693
		internal static string Arg_DuplicateWaitObjectException
		{
			get
			{
				return SR.GetResourceString("Arg_DuplicateWaitObjectException");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000EF49F File Offset: 0x000EE69F
		internal static string Arg_EHClauseNotClause
		{
			get
			{
				return SR.GetResourceString("Arg_EHClauseNotClause");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000EF4AB File Offset: 0x000EE6AB
		internal static string Arg_EHClauseNotFilter
		{
			get
			{
				return SR.GetResourceString("Arg_EHClauseNotFilter");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x000EF4B7 File Offset: 0x000EE6B7
		internal static string Arg_EmptyArray
		{
			get
			{
				return SR.GetResourceString("Arg_EmptyArray");
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x000EF4C3 File Offset: 0x000EE6C3
		internal static string Arg_EndOfStreamException
		{
			get
			{
				return SR.GetResourceString("Arg_EndOfStreamException");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x000EF4CF File Offset: 0x000EE6CF
		internal static string Arg_EntryPointNotFoundException
		{
			get
			{
				return SR.GetResourceString("Arg_EntryPointNotFoundException");
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000EF4DB File Offset: 0x000EE6DB
		internal static string Arg_EnumAndObjectMustBeSameType
		{
			get
			{
				return SR.GetResourceString("Arg_EnumAndObjectMustBeSameType");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x000EF4E7 File Offset: 0x000EE6E7
		internal static string Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType
		{
			get
			{
				return SR.GetResourceString("Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000EF4F3 File Offset: 0x000EE6F3
		internal static string Arg_EnumIllegalVal
		{
			get
			{
				return SR.GetResourceString("Arg_EnumIllegalVal");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x000EF4FF File Offset: 0x000EE6FF
		internal static string Arg_EnumLitValueNotFound
		{
			get
			{
				return SR.GetResourceString("Arg_EnumLitValueNotFound");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000EF50B File Offset: 0x000EE70B
		internal static string Arg_EnumUnderlyingTypeAndObjectMustBeSameType
		{
			get
			{
				return SR.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType");
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000EF517 File Offset: 0x000EE717
		internal static string Arg_EnumValueNotFound
		{
			get
			{
				return SR.GetResourceString("Arg_EnumValueNotFound");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000EF523 File Offset: 0x000EE723
		internal static string Arg_ExecutionEngineException
		{
			get
			{
				return SR.GetResourceString("Arg_ExecutionEngineException");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000EF52F File Offset: 0x000EE72F
		internal static string Arg_ExternalException
		{
			get
			{
				return SR.GetResourceString("Arg_ExternalException");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x000EF53B File Offset: 0x000EE73B
		internal static string Arg_FieldAccessException
		{
			get
			{
				return SR.GetResourceString("Arg_FieldAccessException");
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x000EF547 File Offset: 0x000EE747
		internal static string Arg_FieldDeclTarget
		{
			get
			{
				return SR.GetResourceString("Arg_FieldDeclTarget");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x000EF553 File Offset: 0x000EE753
		internal static string Arg_FldGetArgErr
		{
			get
			{
				return SR.GetResourceString("Arg_FldGetArgErr");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x000EF55F File Offset: 0x000EE75F
		internal static string Arg_FldGetPropSet
		{
			get
			{
				return SR.GetResourceString("Arg_FldGetPropSet");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x000EF56B File Offset: 0x000EE76B
		internal static string Arg_FldSetArgErr
		{
			get
			{
				return SR.GetResourceString("Arg_FldSetArgErr");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x000EF577 File Offset: 0x000EE777
		internal static string Arg_FldSetGet
		{
			get
			{
				return SR.GetResourceString("Arg_FldSetGet");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x000EF583 File Offset: 0x000EE783
		internal static string Arg_FldSetInvoke
		{
			get
			{
				return SR.GetResourceString("Arg_FldSetInvoke");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x000EF58F File Offset: 0x000EE78F
		internal static string Arg_FldSetPropGet
		{
			get
			{
				return SR.GetResourceString("Arg_FldSetPropGet");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x000EF59B File Offset: 0x000EE79B
		internal static string Arg_FormatException
		{
			get
			{
				return SR.GetResourceString("Arg_FormatException");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x000EF5A7 File Offset: 0x000EE7A7
		internal static string Arg_GenericParameter
		{
			get
			{
				return SR.GetResourceString("Arg_GenericParameter");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x000EF5B3 File Offset: 0x000EE7B3
		internal static string Arg_GetMethNotFnd
		{
			get
			{
				return SR.GetResourceString("Arg_GetMethNotFnd");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000EF5BF File Offset: 0x000EE7BF
		internal static string Arg_GuidArrayCtor
		{
			get
			{
				return SR.GetResourceString("Arg_GuidArrayCtor");
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000EF5CB File Offset: 0x000EE7CB
		internal static string Arg_HandleNotAsync
		{
			get
			{
				return SR.GetResourceString("Arg_HandleNotAsync");
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x000EF5D7 File Offset: 0x000EE7D7
		internal static string Arg_HandleNotSync
		{
			get
			{
				return SR.GetResourceString("Arg_HandleNotSync");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000EF5E3 File Offset: 0x000EE7E3
		internal static string Arg_HexStyleNotSupported
		{
			get
			{
				return SR.GetResourceString("Arg_HexStyleNotSupported");
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000EF5EF File Offset: 0x000EE7EF
		internal static string Arg_HTCapacityOverflow
		{
			get
			{
				return SR.GetResourceString("Arg_HTCapacityOverflow");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000EF5FB File Offset: 0x000EE7FB
		internal static string Arg_IndexMustBeInt
		{
			get
			{
				return SR.GetResourceString("Arg_IndexMustBeInt");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000EF607 File Offset: 0x000EE807
		internal static string Arg_IndexOutOfRangeException
		{
			get
			{
				return SR.GetResourceString("Arg_IndexOutOfRangeException");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000EF613 File Offset: 0x000EE813
		internal static string Arg_InsufficientExecutionStackException
		{
			get
			{
				return SR.GetResourceString("Arg_InsufficientExecutionStackException");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000EF61F File Offset: 0x000EE81F
		internal static string Arg_InvalidANSIString
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidANSIString");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000EF62B File Offset: 0x000EE82B
		internal static string Arg_InvalidBase
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidBase");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x000EF637 File Offset: 0x000EE837
		internal static string Arg_InvalidCastException
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidCastException");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000EF643 File Offset: 0x000EE843
		internal static string Arg_InvalidComObjectException
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidComObjectException");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000EF64F File Offset: 0x000EE84F
		internal static string Arg_InvalidFilterCriteriaException
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidFilterCriteriaException");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x000EF65B File Offset: 0x000EE85B
		internal static string Arg_InvalidHandle
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidHandle");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x000EF667 File Offset: 0x000EE867
		internal static string Arg_InvalidHexStyle
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidHexStyle");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000EF673 File Offset: 0x000EE873
		internal static string Arg_InvalidNeutralResourcesLanguage_Asm_Culture
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidNeutralResourcesLanguage_Asm_Culture");
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x000EF67F File Offset: 0x000EE87F
		internal static string Arg_InvalidNeutralResourcesLanguage_FallbackLoc
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000EF68B File Offset: 0x000EE88B
		internal static string Arg_InvalidSatelliteContract_Asm_Ver
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidSatelliteContract_Asm_Ver");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x000EF697 File Offset: 0x000EE897
		internal static string Arg_InvalidOleVariantTypeException
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidOleVariantTypeException");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000EF6A3 File Offset: 0x000EE8A3
		internal static string Arg_InvalidOperationException
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidOperationException");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000EF6AF File Offset: 0x000EE8AF
		internal static string Arg_InvalidTypeInRetType
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidTypeInRetType");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000EF6BB File Offset: 0x000EE8BB
		internal static string Arg_InvalidTypeInSignature
		{
			get
			{
				return SR.GetResourceString("Arg_InvalidTypeInSignature");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000EF6C7 File Offset: 0x000EE8C7
		internal static string Arg_IOException
		{
			get
			{
				return SR.GetResourceString("Arg_IOException");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000EF6D3 File Offset: 0x000EE8D3
		internal static string Arg_KeyNotFound
		{
			get
			{
				return SR.GetResourceString("Arg_KeyNotFound");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000EF6DF File Offset: 0x000EE8DF
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return SR.GetResourceString("Arg_KeyNotFoundWithKey");
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x000EF6EB File Offset: 0x000EE8EB
		internal static string Arg_LongerThanDestArray
		{
			get
			{
				return SR.GetResourceString("Arg_LongerThanDestArray");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000EF6F7 File Offset: 0x000EE8F7
		internal static string Arg_LongerThanSrcArray
		{
			get
			{
				return SR.GetResourceString("Arg_LongerThanSrcArray");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000EF703 File Offset: 0x000EE903
		internal static string Arg_LongerThanSrcString
		{
			get
			{
				return SR.GetResourceString("Arg_LongerThanSrcString");
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x000EF70F File Offset: 0x000EE90F
		internal static string Arg_LowerBoundsMustMatch
		{
			get
			{
				return SR.GetResourceString("Arg_LowerBoundsMustMatch");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x000EF71B File Offset: 0x000EE91B
		internal static string Arg_MarshalAsAnyRestriction
		{
			get
			{
				return SR.GetResourceString("Arg_MarshalAsAnyRestriction");
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000EF727 File Offset: 0x000EE927
		internal static string Arg_MarshalDirectiveException
		{
			get
			{
				return SR.GetResourceString("Arg_MarshalDirectiveException");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000EF733 File Offset: 0x000EE933
		internal static string Arg_MethodAccessException
		{
			get
			{
				return SR.GetResourceString("Arg_MethodAccessException");
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x000EF73F File Offset: 0x000EE93F
		internal static string Arg_MissingFieldException
		{
			get
			{
				return SR.GetResourceString("Arg_MissingFieldException");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000EF74B File Offset: 0x000EE94B
		internal static string Arg_MissingManifestResourceException
		{
			get
			{
				return SR.GetResourceString("Arg_MissingManifestResourceException");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x000EF757 File Offset: 0x000EE957
		internal static string Arg_MissingMemberException
		{
			get
			{
				return SR.GetResourceString("Arg_MissingMemberException");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x000EF763 File Offset: 0x000EE963
		internal static string Arg_MissingMethodException
		{
			get
			{
				return SR.GetResourceString("Arg_MissingMethodException");
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x000EF76F File Offset: 0x000EE96F
		internal static string Arg_MulticastNotSupportedException
		{
			get
			{
				return SR.GetResourceString("Arg_MulticastNotSupportedException");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x000EF77B File Offset: 0x000EE97B
		internal static string Arg_MustBeBoolean
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeBoolean");
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x000EF787 File Offset: 0x000EE987
		internal static string Arg_MustBeByte
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeByte");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x000EF793 File Offset: 0x000EE993
		internal static string Arg_MustBeChar
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeChar");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x000EF79F File Offset: 0x000EE99F
		internal static string Arg_MustBeDateTime
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeDateTime");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x000EF7AB File Offset: 0x000EE9AB
		internal static string Arg_MustBeDateTimeOffset
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeDateTimeOffset");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x000EF7B7 File Offset: 0x000EE9B7
		internal static string Arg_MustBeDecimal
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeDecimal");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000EF7C3 File Offset: 0x000EE9C3
		internal static string Arg_MustBeDelegate
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeDelegate");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x000EF7CF File Offset: 0x000EE9CF
		internal static string Arg_MustBeDouble
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeDouble");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x000EF7DB File Offset: 0x000EE9DB
		internal static string Arg_MustBeEnum
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeEnum");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000EF7E7 File Offset: 0x000EE9E7
		internal static string Arg_MustBeEnumBaseTypeOrEnum
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum");
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000EF7F3 File Offset: 0x000EE9F3
		internal static string Arg_MustBeGuid
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeGuid");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000EF7FF File Offset: 0x000EE9FF
		internal static string Arg_MustBeInt16
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeInt16");
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000EF80B File Offset: 0x000EEA0B
		internal static string Arg_MustBeInt32
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeInt32");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x000EF817 File Offset: 0x000EEA17
		internal static string Arg_MustBeInt64
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeInt64");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000EF823 File Offset: 0x000EEA23
		internal static string Arg_MustBeIntPtr
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeIntPtr");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000EF82F File Offset: 0x000EEA2F
		internal static string Arg_MustBePointer
		{
			get
			{
				return SR.GetResourceString("Arg_MustBePointer");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000EF83B File Offset: 0x000EEA3B
		internal static string Arg_MustBePrimArray
		{
			get
			{
				return SR.GetResourceString("Arg_MustBePrimArray");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x000EF847 File Offset: 0x000EEA47
		internal static string Arg_MustBeRuntimeAssembly
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeRuntimeAssembly");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x000EF853 File Offset: 0x000EEA53
		internal static string Arg_MustBeSByte
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeSByte");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x000EF85F File Offset: 0x000EEA5F
		internal static string Arg_MustBeSingle
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeSingle");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000EF86B File Offset: 0x000EEA6B
		internal static string Arg_MustBeString
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeString");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000EF877 File Offset: 0x000EEA77
		internal static string Arg_MustBeTimeSpan
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeTimeSpan");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000EF883 File Offset: 0x000EEA83
		internal static string Arg_MustBeType
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeType");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x000EF88F File Offset: 0x000EEA8F
		internal static string Arg_MustBeTrue
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeTrue");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000EF89B File Offset: 0x000EEA9B
		internal static string Arg_MustBeUInt16
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeUInt16");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x000EF8A7 File Offset: 0x000EEAA7
		internal static string Arg_MustBeUInt32
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeUInt32");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x000EF8B3 File Offset: 0x000EEAB3
		internal static string Arg_MustBeUInt64
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeUInt64");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x000EF8BF File Offset: 0x000EEABF
		internal static string Arg_MustBeUIntPtr
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeUIntPtr");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x000EF8CB File Offset: 0x000EEACB
		internal static string Arg_MustBeVersion
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeVersion");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x000EF8D7 File Offset: 0x000EEAD7
		internal static string Arg_MustContainEnumInfo
		{
			get
			{
				return SR.GetResourceString("Arg_MustContainEnumInfo");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x000EF8E3 File Offset: 0x000EEAE3
		internal static string Arg_NamedParamNull
		{
			get
			{
				return SR.GetResourceString("Arg_NamedParamNull");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x000EF8EF File Offset: 0x000EEAEF
		internal static string Arg_NamedParamTooBig
		{
			get
			{
				return SR.GetResourceString("Arg_NamedParamTooBig");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x000EF8FB File Offset: 0x000EEAFB
		internal static string Arg_NDirectBadObject
		{
			get
			{
				return SR.GetResourceString("Arg_NDirectBadObject");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x000EF907 File Offset: 0x000EEB07
		internal static string Arg_Need1DArray
		{
			get
			{
				return SR.GetResourceString("Arg_Need1DArray");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000EF913 File Offset: 0x000EEB13
		internal static string Arg_Need2DArray
		{
			get
			{
				return SR.GetResourceString("Arg_Need2DArray");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x000EF91F File Offset: 0x000EEB1F
		internal static string Arg_Need3DArray
		{
			get
			{
				return SR.GetResourceString("Arg_Need3DArray");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000EF92B File Offset: 0x000EEB2B
		internal static string Arg_NeedAtLeast1Rank
		{
			get
			{
				return SR.GetResourceString("Arg_NeedAtLeast1Rank");
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000EF937 File Offset: 0x000EEB37
		internal static string Arg_NegativeArgCount
		{
			get
			{
				return SR.GetResourceString("Arg_NegativeArgCount");
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000EF943 File Offset: 0x000EEB43
		internal static string Arg_NoAccessSpec
		{
			get
			{
				return SR.GetResourceString("Arg_NoAccessSpec");
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x000EF94F File Offset: 0x000EEB4F
		internal static string Arg_NoDefCTor
		{
			get
			{
				return SR.GetResourceString("Arg_NoDefCTor");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000EF95B File Offset: 0x000EEB5B
		internal static string Arg_NonZeroLowerBound
		{
			get
			{
				return SR.GetResourceString("Arg_NonZeroLowerBound");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x000EF967 File Offset: 0x000EEB67
		internal static string Arg_NoStaticVirtual
		{
			get
			{
				return SR.GetResourceString("Arg_NoStaticVirtual");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000EF973 File Offset: 0x000EEB73
		internal static string Arg_NotFiniteNumberException
		{
			get
			{
				return SR.GetResourceString("Arg_NotFiniteNumberException");
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x000EF97F File Offset: 0x000EEB7F
		internal static string Arg_NotGenericMethodDefinition
		{
			get
			{
				return SR.GetResourceString("Arg_NotGenericMethodDefinition");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000EF98B File Offset: 0x000EEB8B
		internal static string Arg_NotGenericParameter
		{
			get
			{
				return SR.GetResourceString("Arg_NotGenericParameter");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000EF997 File Offset: 0x000EEB97
		internal static string Arg_NotGenericTypeDefinition
		{
			get
			{
				return SR.GetResourceString("Arg_NotGenericTypeDefinition");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000EF9A3 File Offset: 0x000EEBA3
		internal static string Arg_NotImplementedException
		{
			get
			{
				return SR.GetResourceString("Arg_NotImplementedException");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000EF9AF File Offset: 0x000EEBAF
		internal static string Arg_NotSupportedException
		{
			get
			{
				return SR.GetResourceString("Arg_NotSupportedException");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000EF9BB File Offset: 0x000EEBBB
		internal static string Arg_NullReferenceException
		{
			get
			{
				return SR.GetResourceString("Arg_NullReferenceException");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x000EF9C7 File Offset: 0x000EEBC7
		internal static string Arg_ObjObjEx
		{
			get
			{
				return SR.GetResourceString("Arg_ObjObjEx");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x000EF9D3 File Offset: 0x000EEBD3
		internal static string Arg_OleAutDateInvalid
		{
			get
			{
				return SR.GetResourceString("Arg_OleAutDateInvalid");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x000EF9DF File Offset: 0x000EEBDF
		internal static string Arg_OleAutDateScale
		{
			get
			{
				return SR.GetResourceString("Arg_OleAutDateScale");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x000EF9EB File Offset: 0x000EEBEB
		internal static string Arg_OverflowException
		{
			get
			{
				return SR.GetResourceString("Arg_OverflowException");
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000EF9F7 File Offset: 0x000EEBF7
		internal static string Arg_ParamName_Name
		{
			get
			{
				return SR.GetResourceString("Arg_ParamName_Name");
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000EFA03 File Offset: 0x000EEC03
		internal static string Arg_ParmArraySize
		{
			get
			{
				return SR.GetResourceString("Arg_ParmArraySize");
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000EFA0F File Offset: 0x000EEC0F
		internal static string Arg_ParmCnt
		{
			get
			{
				return SR.GetResourceString("Arg_ParmCnt");
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000EFA1B File Offset: 0x000EEC1B
		internal static string Arg_PathEmpty
		{
			get
			{
				return SR.GetResourceString("Arg_PathEmpty");
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000EFA27 File Offset: 0x000EEC27
		internal static string Arg_PlatformNotSupported
		{
			get
			{
				return SR.GetResourceString("Arg_PlatformNotSupported");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x000EFA33 File Offset: 0x000EEC33
		internal static string Arg_PropSetGet
		{
			get
			{
				return SR.GetResourceString("Arg_PropSetGet");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000EFA3F File Offset: 0x000EEC3F
		internal static string Arg_PropSetInvoke
		{
			get
			{
				return SR.GetResourceString("Arg_PropSetInvoke");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x000EFA4B File Offset: 0x000EEC4B
		internal static string Arg_RankException
		{
			get
			{
				return SR.GetResourceString("Arg_RankException");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000EFA57 File Offset: 0x000EEC57
		internal static string Arg_RankIndices
		{
			get
			{
				return SR.GetResourceString("Arg_RankIndices");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000EFA63 File Offset: 0x000EEC63
		internal static string Arg_RankMultiDimNotSupported
		{
			get
			{
				return SR.GetResourceString("Arg_RankMultiDimNotSupported");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x000EFA6F File Offset: 0x000EEC6F
		internal static string Arg_RanksAndBounds
		{
			get
			{
				return SR.GetResourceString("Arg_RanksAndBounds");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x000EFA7B File Offset: 0x000EEC7B
		internal static string Arg_RegGetOverflowBug
		{
			get
			{
				return SR.GetResourceString("Arg_RegGetOverflowBug");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x000EFA87 File Offset: 0x000EEC87
		internal static string Arg_RegKeyNotFound
		{
			get
			{
				return SR.GetResourceString("Arg_RegKeyNotFound");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x000EFA93 File Offset: 0x000EEC93
		internal static string Arg_RegSubKeyValueAbsent
		{
			get
			{
				return SR.GetResourceString("Arg_RegSubKeyValueAbsent");
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x000EFA9F File Offset: 0x000EEC9F
		internal static string Arg_RegValStrLenBug
		{
			get
			{
				return SR.GetResourceString("Arg_RegValStrLenBug");
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x000EFAAB File Offset: 0x000EECAB
		internal static string Arg_ResMgrNotResSet
		{
			get
			{
				return SR.GetResourceString("Arg_ResMgrNotResSet");
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000EFAB7 File Offset: 0x000EECB7
		internal static string Arg_ResourceFileUnsupportedVersion
		{
			get
			{
				return SR.GetResourceString("Arg_ResourceFileUnsupportedVersion");
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000EFAC3 File Offset: 0x000EECC3
		internal static string Arg_ResourceNameNotExist
		{
			get
			{
				return SR.GetResourceString("Arg_ResourceNameNotExist");
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000EFACF File Offset: 0x000EECCF
		internal static string Arg_SafeArrayRankMismatchException
		{
			get
			{
				return SR.GetResourceString("Arg_SafeArrayRankMismatchException");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x000EFADB File Offset: 0x000EECDB
		internal static string Arg_SafeArrayTypeMismatchException
		{
			get
			{
				return SR.GetResourceString("Arg_SafeArrayTypeMismatchException");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000EFAE7 File Offset: 0x000EECE7
		internal static string Arg_SecurityException
		{
			get
			{
				return SR.GetResourceString("Arg_SecurityException");
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000EFAF3 File Offset: 0x000EECF3
		internal static string SerializationException
		{
			get
			{
				return SR.GetResourceString("SerializationException");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x000EFAFF File Offset: 0x000EECFF
		internal static string Arg_SetMethNotFnd
		{
			get
			{
				return SR.GetResourceString("Arg_SetMethNotFnd");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x000EFB0B File Offset: 0x000EED0B
		internal static string Arg_StackOverflowException
		{
			get
			{
				return SR.GetResourceString("Arg_StackOverflowException");
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000EFB17 File Offset: 0x000EED17
		internal static string Arg_SurrogatesNotAllowedAsSingleChar
		{
			get
			{
				return SR.GetResourceString("Arg_SurrogatesNotAllowedAsSingleChar");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000EFB23 File Offset: 0x000EED23
		internal static string Arg_SynchronizationLockException
		{
			get
			{
				return SR.GetResourceString("Arg_SynchronizationLockException");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x000EFB2F File Offset: 0x000EED2F
		internal static string Arg_SystemException
		{
			get
			{
				return SR.GetResourceString("Arg_SystemException");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x000EFB3B File Offset: 0x000EED3B
		internal static string Arg_TargetInvocationException
		{
			get
			{
				return SR.GetResourceString("Arg_TargetInvocationException");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x000EFB47 File Offset: 0x000EED47
		internal static string Arg_TargetParameterCountException
		{
			get
			{
				return SR.GetResourceString("Arg_TargetParameterCountException");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000EFB53 File Offset: 0x000EED53
		internal static string Arg_ThreadStartException
		{
			get
			{
				return SR.GetResourceString("Arg_ThreadStartException");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000EFB5F File Offset: 0x000EED5F
		internal static string Arg_ThreadStateException
		{
			get
			{
				return SR.GetResourceString("Arg_ThreadStateException");
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x000EFB6B File Offset: 0x000EED6B
		internal static string Arg_TimeoutException
		{
			get
			{
				return SR.GetResourceString("Arg_TimeoutException");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000EFB77 File Offset: 0x000EED77
		internal static string Arg_TypeAccessException
		{
			get
			{
				return SR.GetResourceString("Arg_TypeAccessException");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000EFB83 File Offset: 0x000EED83
		internal static string Arg_TypedReference_Null
		{
			get
			{
				return SR.GetResourceString("Arg_TypedReference_Null");
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x000EFB8F File Offset: 0x000EED8F
		internal static string Arg_TypeLoadException
		{
			get
			{
				return SR.GetResourceString("Arg_TypeLoadException");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x000EFB9B File Offset: 0x000EED9B
		internal static string Arg_TypeLoadNullStr
		{
			get
			{
				return SR.GetResourceString("Arg_TypeLoadNullStr");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x000EFBA7 File Offset: 0x000EEDA7
		internal static string Arg_TypeRefPrimitve
		{
			get
			{
				return SR.GetResourceString("Arg_TypeRefPrimitve");
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x000EFBB3 File Offset: 0x000EEDB3
		internal static string Arg_TypeUnloadedException
		{
			get
			{
				return SR.GetResourceString("Arg_TypeUnloadedException");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x000EFBBF File Offset: 0x000EEDBF
		internal static string Arg_UnauthorizedAccessException
		{
			get
			{
				return SR.GetResourceString("Arg_UnauthorizedAccessException");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x000EFBCB File Offset: 0x000EEDCB
		internal static string Arg_UnboundGenField
		{
			get
			{
				return SR.GetResourceString("Arg_UnboundGenField");
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x000EFBD7 File Offset: 0x000EEDD7
		internal static string Arg_UnboundGenParam
		{
			get
			{
				return SR.GetResourceString("Arg_UnboundGenParam");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000EFBE3 File Offset: 0x000EEDE3
		internal static string Arg_UnknownTypeCode
		{
			get
			{
				return SR.GetResourceString("Arg_UnknownTypeCode");
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x000EFBEF File Offset: 0x000EEDEF
		internal static string Arg_VarMissNull
		{
			get
			{
				return SR.GetResourceString("Arg_VarMissNull");
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x000EFBFB File Offset: 0x000EEDFB
		internal static string Arg_VersionString
		{
			get
			{
				return SR.GetResourceString("Arg_VersionString");
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x000EFC07 File Offset: 0x000EEE07
		internal static string Arg_WrongAsyncResult
		{
			get
			{
				return SR.GetResourceString("Arg_WrongAsyncResult");
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000EFC13 File Offset: 0x000EEE13
		internal static string Arg_WrongType
		{
			get
			{
				return SR.GetResourceString("Arg_WrongType");
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x000EFC1F File Offset: 0x000EEE1F
		internal static string Argument_AbsolutePathRequired
		{
			get
			{
				return SR.GetResourceString("Argument_AbsolutePathRequired");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x000EFC2B File Offset: 0x000EEE2B
		internal static string Argument_AddingDuplicate
		{
			get
			{
				return SR.GetResourceString("Argument_AddingDuplicate");
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x000EFC37 File Offset: 0x000EEE37
		internal static string Argument_AddingDuplicate__
		{
			get
			{
				return SR.GetResourceString("Argument_AddingDuplicate__");
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x000EFC43 File Offset: 0x000EEE43
		internal static string Argument_AddingDuplicateWithKey
		{
			get
			{
				return SR.GetResourceString("Argument_AddingDuplicateWithKey");
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000EFC4F File Offset: 0x000EEE4F
		internal static string Argument_AdjustmentRulesNoNulls
		{
			get
			{
				return SR.GetResourceString("Argument_AdjustmentRulesNoNulls");
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x000EFC5B File Offset: 0x000EEE5B
		internal static string Argument_AdjustmentRulesOutOfOrder
		{
			get
			{
				return SR.GetResourceString("Argument_AdjustmentRulesOutOfOrder");
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000EFC67 File Offset: 0x000EEE67
		internal static string Argument_AlreadyBoundOrSyncHandle
		{
			get
			{
				return SR.GetResourceString("Argument_AlreadyBoundOrSyncHandle");
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x000EFC73 File Offset: 0x000EEE73
		internal static string Argument_ArrayGetInterfaceMap
		{
			get
			{
				return SR.GetResourceString("Argument_ArrayGetInterfaceMap");
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x000EFC7F File Offset: 0x000EEE7F
		internal static string Argument_ArraysInvalid
		{
			get
			{
				return SR.GetResourceString("Argument_ArraysInvalid");
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x000EFC8B File Offset: 0x000EEE8B
		internal static string Argument_AttributeNamesMustBeUnique
		{
			get
			{
				return SR.GetResourceString("Argument_AttributeNamesMustBeUnique");
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x000EFC97 File Offset: 0x000EEE97
		internal static string Argument_BadConstructor
		{
			get
			{
				return SR.GetResourceString("Argument_BadConstructor");
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x000EFCA3 File Offset: 0x000EEEA3
		internal static string Argument_BadConstructorCallConv
		{
			get
			{
				return SR.GetResourceString("Argument_BadConstructorCallConv");
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x000EFCAF File Offset: 0x000EEEAF
		internal static string Argument_BadExceptionCodeGen
		{
			get
			{
				return SR.GetResourceString("Argument_BadExceptionCodeGen");
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x000EFCBB File Offset: 0x000EEEBB
		internal static string Argument_BadFieldForConstructorBuilder
		{
			get
			{
				return SR.GetResourceString("Argument_BadFieldForConstructorBuilder");
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x000EFCC7 File Offset: 0x000EEEC7
		internal static string Argument_BadFieldSig
		{
			get
			{
				return SR.GetResourceString("Argument_BadFieldSig");
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x000EFCD3 File Offset: 0x000EEED3
		internal static string Argument_BadFieldType
		{
			get
			{
				return SR.GetResourceString("Argument_BadFieldType");
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000EFCDF File Offset: 0x000EEEDF
		internal static string Argument_BadFormatSpecifier
		{
			get
			{
				return SR.GetResourceString("Argument_BadFormatSpecifier");
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000EFCEB File Offset: 0x000EEEEB
		internal static string Argument_BadImageFormatExceptionResolve
		{
			get
			{
				return SR.GetResourceString("Argument_BadImageFormatExceptionResolve");
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000EFCF7 File Offset: 0x000EEEF7
		internal static string Argument_BadLabel
		{
			get
			{
				return SR.GetResourceString("Argument_BadLabel");
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000EFD03 File Offset: 0x000EEF03
		internal static string Argument_BadLabelContent
		{
			get
			{
				return SR.GetResourceString("Argument_BadLabelContent");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x000EFD0F File Offset: 0x000EEF0F
		internal static string Argument_BadNestedTypeFlags
		{
			get
			{
				return SR.GetResourceString("Argument_BadNestedTypeFlags");
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x000EFD1B File Offset: 0x000EEF1B
		internal static string Argument_BadParameterCountsForConstructor
		{
			get
			{
				return SR.GetResourceString("Argument_BadParameterCountsForConstructor");
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000EFD27 File Offset: 0x000EEF27
		internal static string Argument_BadParameterTypeForCAB
		{
			get
			{
				return SR.GetResourceString("Argument_BadParameterTypeForCAB");
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x000EFD33 File Offset: 0x000EEF33
		internal static string Argument_BadPropertyForConstructorBuilder
		{
			get
			{
				return SR.GetResourceString("Argument_BadPropertyForConstructorBuilder");
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000EFD3F File Offset: 0x000EEF3F
		internal static string Argument_BadSigFormat
		{
			get
			{
				return SR.GetResourceString("Argument_BadSigFormat");
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x000EFD4B File Offset: 0x000EEF4B
		internal static string Argument_BadSizeForData
		{
			get
			{
				return SR.GetResourceString("Argument_BadSizeForData");
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x000EFD57 File Offset: 0x000EEF57
		internal static string Argument_BadTypeAttrInvalidLayout
		{
			get
			{
				return SR.GetResourceString("Argument_BadTypeAttrInvalidLayout");
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x000EFD63 File Offset: 0x000EEF63
		internal static string Argument_BadTypeAttrNestedVisibilityOnNonNestedType
		{
			get
			{
				return SR.GetResourceString("Argument_BadTypeAttrNestedVisibilityOnNonNestedType");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x000EFD6F File Offset: 0x000EEF6F
		internal static string Argument_BadTypeAttrNonNestedVisibilityNestedType
		{
			get
			{
				return SR.GetResourceString("Argument_BadTypeAttrNonNestedVisibilityNestedType");
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x000EFD7B File Offset: 0x000EEF7B
		internal static string Argument_BadTypeAttrReservedBitsSet
		{
			get
			{
				return SR.GetResourceString("Argument_BadTypeAttrReservedBitsSet");
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x000EFD87 File Offset: 0x000EEF87
		internal static string Argument_BadTypeInCustomAttribute
		{
			get
			{
				return SR.GetResourceString("Argument_BadTypeInCustomAttribute");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000EFD93 File Offset: 0x000EEF93
		internal static string Argument_CannotGetTypeTokenForByRef
		{
			get
			{
				return SR.GetResourceString("Argument_CannotGetTypeTokenForByRef");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x000EFD9F File Offset: 0x000EEF9F
		internal static string Argument_CannotSetParentToInterface
		{
			get
			{
				return SR.GetResourceString("Argument_CannotSetParentToInterface");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x000EFDAB File Offset: 0x000EEFAB
		internal static string Argument_CodepageNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_CodepageNotSupported");
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x000EFDB7 File Offset: 0x000EEFB7
		internal static string Argument_CompareOptionOrdinal
		{
			get
			{
				return SR.GetResourceString("Argument_CompareOptionOrdinal");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x000EFDC3 File Offset: 0x000EEFC3
		internal static string Argument_ConflictingDateTimeRoundtripStyles
		{
			get
			{
				return SR.GetResourceString("Argument_ConflictingDateTimeRoundtripStyles");
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000EFDCF File Offset: 0x000EEFCF
		internal static string Argument_ConflictingDateTimeStyles
		{
			get
			{
				return SR.GetResourceString("Argument_ConflictingDateTimeStyles");
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000EFDDB File Offset: 0x000EEFDB
		internal static string Argument_ConstantDoesntMatch
		{
			get
			{
				return SR.GetResourceString("Argument_ConstantDoesntMatch");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x000EFDE7 File Offset: 0x000EEFE7
		internal static string Argument_ConstantNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_ConstantNotSupported");
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000EFDF3 File Offset: 0x000EEFF3
		internal static string Argument_ConstantNull
		{
			get
			{
				return SR.GetResourceString("Argument_ConstantNull");
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x000EFDFF File Offset: 0x000EEFFF
		internal static string Argument_ConstructorNeedGenericDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_ConstructorNeedGenericDeclaringType");
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000EFE0B File Offset: 0x000EF00B
		internal static string Argument_ConversionOverflow
		{
			get
			{
				return SR.GetResourceString("Argument_ConversionOverflow");
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000EFE17 File Offset: 0x000EF017
		internal static string Argument_ConvertMismatch
		{
			get
			{
				return SR.GetResourceString("Argument_ConvertMismatch");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000EFE23 File Offset: 0x000EF023
		internal static string Argument_CultureIetfNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_CultureIetfNotSupported");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x000EFE2F File Offset: 0x000EF02F
		internal static string Argument_CultureInvalidIdentifier
		{
			get
			{
				return SR.GetResourceString("Argument_CultureInvalidIdentifier");
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x000EFE3B File Offset: 0x000EF03B
		internal static string Argument_CultureIsNeutral
		{
			get
			{
				return SR.GetResourceString("Argument_CultureIsNeutral");
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x000EFE47 File Offset: 0x000EF047
		internal static string Argument_CultureNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_CultureNotSupported");
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x000EFE53 File Offset: 0x000EF053
		internal static string Argument_CustomAssemblyLoadContextRequestedNameMismatch
		{
			get
			{
				return SR.GetResourceString("Argument_CustomAssemblyLoadContextRequestedNameMismatch");
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x000EFE5F File Offset: 0x000EF05F
		internal static string Argument_CustomCultureCannotBePassedByNumber
		{
			get
			{
				return SR.GetResourceString("Argument_CustomCultureCannotBePassedByNumber");
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x000EFE6B File Offset: 0x000EF06B
		internal static string Argument_DateTimeBadBinaryData
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeBadBinaryData");
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000EFE77 File Offset: 0x000EF077
		internal static string Argument_DateTimeHasTicks
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeHasTicks");
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000EFE83 File Offset: 0x000EF083
		internal static string Argument_DateTimeHasTimeOfDay
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeHasTimeOfDay");
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x000EFE8F File Offset: 0x000EF08F
		internal static string Argument_DateTimeIsInvalid
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeIsInvalid");
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000EFE9B File Offset: 0x000EF09B
		internal static string Argument_DateTimeIsNotAmbiguous
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeIsNotAmbiguous");
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x000EFEA7 File Offset: 0x000EF0A7
		internal static string Argument_DateTimeKindMustBeUnspecified
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeKindMustBeUnspecified");
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x000EFEB3 File Offset: 0x000EF0B3
		internal static string Argument_DateTimeKindMustBeUnspecifiedOrUtc
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeKindMustBeUnspecifiedOrUtc");
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000EFEBF File Offset: 0x000EF0BF
		internal static string Argument_DateTimeOffsetInvalidDateTimeStyles
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeOffsetInvalidDateTimeStyles");
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000EFECB File Offset: 0x000EF0CB
		internal static string Argument_DateTimeOffsetIsNotAmbiguous
		{
			get
			{
				return SR.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous");
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000EFED7 File Offset: 0x000EF0D7
		internal static string Argument_DestinationTooShort
		{
			get
			{
				return SR.GetResourceString("Argument_DestinationTooShort");
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000EFEE3 File Offset: 0x000EF0E3
		internal static string Argument_DuplicateTypeName
		{
			get
			{
				return SR.GetResourceString("Argument_DuplicateTypeName");
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x000EFEEF File Offset: 0x000EF0EF
		internal static string Argument_EmitWriteLineType
		{
			get
			{
				return SR.GetResourceString("Argument_EmitWriteLineType");
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x000EFEFB File Offset: 0x000EF0FB
		internal static string Argument_EmptyDecString
		{
			get
			{
				return SR.GetResourceString("Argument_EmptyDecString");
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x000EFF07 File Offset: 0x000EF107
		internal static string Argument_EmptyName
		{
			get
			{
				return SR.GetResourceString("Argument_EmptyName");
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000EFF13 File Offset: 0x000EF113
		internal static string Argument_EmptyPath
		{
			get
			{
				return SR.GetResourceString("Argument_EmptyPath");
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x000EFF1F File Offset: 0x000EF11F
		internal static string Argument_EmptyWaithandleArray
		{
			get
			{
				return SR.GetResourceString("Argument_EmptyWaithandleArray");
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x000EFF2B File Offset: 0x000EF12B
		internal static string Argument_EncoderFallbackNotEmpty
		{
			get
			{
				return SR.GetResourceString("Argument_EncoderFallbackNotEmpty");
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000EFF37 File Offset: 0x000EF137
		internal static string Argument_EncodingConversionOverflowBytes
		{
			get
			{
				return SR.GetResourceString("Argument_EncodingConversionOverflowBytes");
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x000EFF43 File Offset: 0x000EF143
		internal static string Argument_EncodingConversionOverflowChars
		{
			get
			{
				return SR.GetResourceString("Argument_EncodingConversionOverflowChars");
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x000EFF4F File Offset: 0x000EF14F
		internal static string Argument_EncodingNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_EncodingNotSupported");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x000EFF5B File Offset: 0x000EF15B
		internal static string Argument_EnumTypeDoesNotMatch
		{
			get
			{
				return SR.GetResourceString("Argument_EnumTypeDoesNotMatch");
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x000EFF67 File Offset: 0x000EF167
		internal static string Argument_FallbackBufferNotEmpty
		{
			get
			{
				return SR.GetResourceString("Argument_FallbackBufferNotEmpty");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000EFF73 File Offset: 0x000EF173
		internal static string Argument_FieldDeclaringTypeGeneric
		{
			get
			{
				return SR.GetResourceString("Argument_FieldDeclaringTypeGeneric");
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000EFF7F File Offset: 0x000EF17F
		internal static string Argument_FieldNeedGenericDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_FieldNeedGenericDeclaringType");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000EFF8B File Offset: 0x000EF18B
		internal static string Argument_GenConstraintViolation
		{
			get
			{
				return SR.GetResourceString("Argument_GenConstraintViolation");
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x000EFF97 File Offset: 0x000EF197
		internal static string Argument_GenericArgsCount
		{
			get
			{
				return SR.GetResourceString("Argument_GenericArgsCount");
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000EFFA3 File Offset: 0x000EF1A3
		internal static string Argument_GenericsInvalid
		{
			get
			{
				return SR.GetResourceString("Argument_GenericsInvalid");
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000EFFAF File Offset: 0x000EF1AF
		internal static string Argument_GlobalFunctionHasToBeStatic
		{
			get
			{
				return SR.GetResourceString("Argument_GlobalFunctionHasToBeStatic");
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x000EFFBB File Offset: 0x000EF1BB
		internal static string Argument_HasToBeArrayClass
		{
			get
			{
				return SR.GetResourceString("Argument_HasToBeArrayClass");
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x000EFFC7 File Offset: 0x000EF1C7
		internal static string Argument_IdnBadBidi
		{
			get
			{
				return SR.GetResourceString("Argument_IdnBadBidi");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x000EFFD3 File Offset: 0x000EF1D3
		internal static string Argument_IdnBadLabelSize
		{
			get
			{
				return SR.GetResourceString("Argument_IdnBadLabelSize");
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000EFFDF File Offset: 0x000EF1DF
		internal static string Argument_IdnBadNameSize
		{
			get
			{
				return SR.GetResourceString("Argument_IdnBadNameSize");
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x000EFFEB File Offset: 0x000EF1EB
		internal static string Argument_IdnBadPunycode
		{
			get
			{
				return SR.GetResourceString("Argument_IdnBadPunycode");
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x000EFFF7 File Offset: 0x000EF1F7
		internal static string Argument_IdnBadStd3
		{
			get
			{
				return SR.GetResourceString("Argument_IdnBadStd3");
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000F0003 File Offset: 0x000EF203
		internal static string Argument_IdnIllegalName
		{
			get
			{
				return SR.GetResourceString("Argument_IdnIllegalName");
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x000F000F File Offset: 0x000EF20F
		internal static string Argument_IllegalEnvVarName
		{
			get
			{
				return SR.GetResourceString("Argument_IllegalEnvVarName");
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000F001B File Offset: 0x000EF21B
		internal static string Argument_IllegalName
		{
			get
			{
				return SR.GetResourceString("Argument_IllegalName");
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x000F0027 File Offset: 0x000EF227
		internal static string Argument_ImplementIComparable
		{
			get
			{
				return SR.GetResourceString("Argument_ImplementIComparable");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000F0033 File Offset: 0x000EF233
		internal static string Argument_InvalidAppendMode
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidAppendMode");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x000F003F File Offset: 0x000EF23F
		internal static string Argument_InvalidArgumentForComparison
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidArgumentForComparison");
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000F004B File Offset: 0x000EF24B
		internal static string Argument_InvalidArrayLength
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidArrayLength");
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000F0057 File Offset: 0x000EF257
		internal static string Argument_InvalidArrayType
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidArrayType");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x000F0063 File Offset: 0x000EF263
		internal static string Argument_InvalidCalendar
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCalendar");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x000F006F File Offset: 0x000EF26F
		internal static string Argument_InvalidCharSequence
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCharSequence");
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x000F007B File Offset: 0x000EF27B
		internal static string Argument_InvalidCharSequenceNoIndex
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCharSequenceNoIndex");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x000F0087 File Offset: 0x000EF287
		internal static string Argument_InvalidCodePageBytesIndex
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCodePageBytesIndex");
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x000F0093 File Offset: 0x000EF293
		internal static string Argument_InvalidCodePageConversionIndex
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCodePageConversionIndex");
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x000F009F File Offset: 0x000EF29F
		internal static string Argument_InvalidConstructorDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidConstructorDeclaringType");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x000F00AB File Offset: 0x000EF2AB
		internal static string Argument_InvalidConstructorInfo
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidConstructorInfo");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x000F00B7 File Offset: 0x000EF2B7
		internal static string Argument_InvalidCultureName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidCultureName");
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000F00C3 File Offset: 0x000EF2C3
		internal static string Argument_InvalidPredefinedCultureName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidPredefinedCultureName");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000F00CF File Offset: 0x000EF2CF
		internal static string Argument_InvalidDateTimeKind
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidDateTimeKind");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000F00DB File Offset: 0x000EF2DB
		internal static string Argument_InvalidDateTimeStyles
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidDateTimeStyles");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000F00E7 File Offset: 0x000EF2E7
		internal static string Argument_InvalidDigitSubstitution
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidDigitSubstitution");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x000F00F3 File Offset: 0x000EF2F3
		internal static string Argument_InvalidElementName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidElementName");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000F00FF File Offset: 0x000EF2FF
		internal static string Argument_InvalidElementTag
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidElementTag");
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000F010B File Offset: 0x000EF30B
		internal static string Argument_InvalidElementText
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidElementText");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000F0117 File Offset: 0x000EF317
		internal static string Argument_InvalidElementValue
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidElementValue");
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000F0123 File Offset: 0x000EF323
		internal static string Argument_InvalidEnum
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidEnum");
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000F012F File Offset: 0x000EF32F
		internal static string Argument_InvalidEnumValue
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidEnumValue");
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x000F013B File Offset: 0x000EF33B
		internal static string Argument_InvalidFieldDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidFieldDeclaringType");
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000F0147 File Offset: 0x000EF347
		internal static string Argument_InvalidFileModeAndAccessCombo
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidFileModeAndAccessCombo");
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x000F0153 File Offset: 0x000EF353
		internal static string Argument_InvalidFlag
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidFlag");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000F015F File Offset: 0x000EF35F
		internal static string Argument_InvalidGenericInstArray
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidGenericInstArray");
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x000F016B File Offset: 0x000EF36B
		internal static string Argument_InvalidGroupSize
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidGroupSize");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x000F0177 File Offset: 0x000EF377
		internal static string Argument_InvalidHandle
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidHandle");
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000F0183 File Offset: 0x000EF383
		internal static string Argument_InvalidHighSurrogate
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidHighSurrogate");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000F018F File Offset: 0x000EF38F
		internal static string Argument_InvalidId
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidId");
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000F019B File Offset: 0x000EF39B
		internal static string Argument_InvalidKindOfTypeForCA
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidKindOfTypeForCA");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000F01A7 File Offset: 0x000EF3A7
		internal static string Argument_InvalidLabel
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidLabel");
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000F01B3 File Offset: 0x000EF3B3
		internal static string Argument_InvalidLowSurrogate
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidLowSurrogate");
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000F01BF File Offset: 0x000EF3BF
		internal static string Argument_InvalidMemberForNamedArgument
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidMemberForNamedArgument");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x000F01CB File Offset: 0x000EF3CB
		internal static string Argument_InvalidMethodDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidMethodDeclaringType");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000F01D7 File Offset: 0x000EF3D7
		internal static string Argument_InvalidName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidName");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000F01E3 File Offset: 0x000EF3E3
		internal static string Argument_InvalidNativeDigitCount
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidNativeDigitCount");
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000F01EF File Offset: 0x000EF3EF
		internal static string Argument_InvalidNativeDigitValue
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidNativeDigitValue");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x000F01FB File Offset: 0x000EF3FB
		internal static string Argument_InvalidNeutralRegionName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidNeutralRegionName");
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000F0207 File Offset: 0x000EF407
		internal static string Argument_InvalidNormalizationForm
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidNormalizationForm");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x000F0213 File Offset: 0x000EF413
		internal static string Argument_InvalidNumberStyles
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidNumberStyles");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000F021F File Offset: 0x000EF41F
		internal static string Argument_InvalidOffLen
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidOffLen");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x000F022B File Offset: 0x000EF42B
		internal static string Argument_InvalidOpCodeOnDynamicMethod
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidOpCodeOnDynamicMethod");
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x000F0237 File Offset: 0x000EF437
		internal static string Argument_InvalidParameterInfo
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidParameterInfo");
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000F0243 File Offset: 0x000EF443
		internal static string Argument_InvalidParamInfo
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidParamInfo");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x000F024F File Offset: 0x000EF44F
		internal static string Argument_InvalidPathChars
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidPathChars");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000F025B File Offset: 0x000EF45B
		internal static string Argument_InvalidResourceCultureName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidResourceCultureName");
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x000F0267 File Offset: 0x000EF467
		internal static string Argument_InvalidSafeBufferOffLen
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidSafeBufferOffLen");
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x000F0273 File Offset: 0x000EF473
		internal static string Argument_InvalidSeekOrigin
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidSeekOrigin");
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x000F027F File Offset: 0x000EF47F
		internal static string Argument_InvalidSerializedString
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidSerializedString");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x000F028B File Offset: 0x000EF48B
		internal static string Argument_InvalidStartupHookSignature
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidStartupHookSignature");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x000F0297 File Offset: 0x000EF497
		internal static string Argument_InvalidTimeSpanStyles
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidTimeSpanStyles");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x000F02A3 File Offset: 0x000EF4A3
		internal static string Argument_InvalidToken
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidToken");
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000F02AF File Offset: 0x000EF4AF
		internal static string Argument_InvalidTypeForCA
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidTypeForCA");
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x000F02BB File Offset: 0x000EF4BB
		internal static string Argument_InvalidTypeForDynamicMethod
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidTypeForDynamicMethod");
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000F02C7 File Offset: 0x000EF4C7
		internal static string Argument_InvalidTypeName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidTypeName");
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x000F02D3 File Offset: 0x000EF4D3
		internal static string Argument_InvalidTypeWithPointersNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidTypeWithPointersNotSupported");
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x000F02DF File Offset: 0x000EF4DF
		internal static string Argument_InvalidUnity
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidUnity");
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x000F02EB File Offset: 0x000EF4EB
		internal static string Argument_LargeInteger
		{
			get
			{
				return SR.GetResourceString("Argument_LargeInteger");
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000F02F7 File Offset: 0x000EF4F7
		internal static string Argument_LongEnvVarValue
		{
			get
			{
				return SR.GetResourceString("Argument_LongEnvVarValue");
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000F0303 File Offset: 0x000EF503
		internal static string Argument_MethodDeclaringTypeGeneric
		{
			get
			{
				return SR.GetResourceString("Argument_MethodDeclaringTypeGeneric");
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000F030F File Offset: 0x000EF50F
		internal static string Argument_MethodDeclaringTypeGenericLcg
		{
			get
			{
				return SR.GetResourceString("Argument_MethodDeclaringTypeGenericLcg");
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x000F031B File Offset: 0x000EF51B
		internal static string Argument_MethodNeedGenericDeclaringType
		{
			get
			{
				return SR.GetResourceString("Argument_MethodNeedGenericDeclaringType");
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000F0327 File Offset: 0x000EF527
		internal static string Argument_MinMaxValue
		{
			get
			{
				return SR.GetResourceString("Argument_MinMaxValue");
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000F0333 File Offset: 0x000EF533
		internal static string Argument_MismatchedArrays
		{
			get
			{
				return SR.GetResourceString("Argument_MismatchedArrays");
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000F033F File Offset: 0x000EF53F
		internal static string Argument_MissingDefaultConstructor
		{
			get
			{
				return SR.GetResourceString("Argument_MissingDefaultConstructor");
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x000F034B File Offset: 0x000EF54B
		internal static string Argument_MustBeFalse
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeFalse");
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000F0357 File Offset: 0x000EF557
		internal static string Argument_MustBeRuntimeAssembly
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeRuntimeAssembly");
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x000F0363 File Offset: 0x000EF563
		internal static string Argument_MustBeRuntimeFieldInfo
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeRuntimeFieldInfo");
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000F036F File Offset: 0x000EF56F
		internal static string Argument_MustBeRuntimeMethodInfo
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeRuntimeMethodInfo");
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x000F037B File Offset: 0x000EF57B
		internal static string Argument_MustBeRuntimeReflectionObject
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeRuntimeReflectionObject");
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x000F0387 File Offset: 0x000EF587
		internal static string Argument_MustBeRuntimeType
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeRuntimeType");
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x000F0393 File Offset: 0x000EF593
		internal static string Argument_MustBeTypeBuilder
		{
			get
			{
				return SR.GetResourceString("Argument_MustBeTypeBuilder");
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000F039F File Offset: 0x000EF59F
		internal static string Argument_MustHaveAttributeBaseClass
		{
			get
			{
				return SR.GetResourceString("Argument_MustHaveAttributeBaseClass");
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x000F03AB File Offset: 0x000EF5AB
		internal static string Argument_NativeOverlappedAlreadyFree
		{
			get
			{
				return SR.GetResourceString("Argument_NativeOverlappedAlreadyFree");
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000F03B7 File Offset: 0x000EF5B7
		internal static string Argument_NativeOverlappedWrongBoundHandle
		{
			get
			{
				return SR.GetResourceString("Argument_NativeOverlappedWrongBoundHandle");
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x000F03C3 File Offset: 0x000EF5C3
		internal static string Argument_NeedGenericMethodDefinition
		{
			get
			{
				return SR.GetResourceString("Argument_NeedGenericMethodDefinition");
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000F03CF File Offset: 0x000EF5CF
		internal static string Argument_NeedNonGenericType
		{
			get
			{
				return SR.GetResourceString("Argument_NeedNonGenericType");
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x000F03DB File Offset: 0x000EF5DB
		internal static string Argument_NeedStructWithNoRefs
		{
			get
			{
				return SR.GetResourceString("Argument_NeedStructWithNoRefs");
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x000F03E7 File Offset: 0x000EF5E7
		internal static string Argument_NeverValidGenericArgument
		{
			get
			{
				return SR.GetResourceString("Argument_NeverValidGenericArgument");
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x000F03F3 File Offset: 0x000EF5F3
		internal static string Argument_NoEra
		{
			get
			{
				return SR.GetResourceString("Argument_NoEra");
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x000F03FF File Offset: 0x000EF5FF
		internal static string Argument_NoRegionInvariantCulture
		{
			get
			{
				return SR.GetResourceString("Argument_NoRegionInvariantCulture");
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x000F040B File Offset: 0x000EF60B
		internal static string Argument_NotAWritableProperty
		{
			get
			{
				return SR.GetResourceString("Argument_NotAWritableProperty");
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x000F0417 File Offset: 0x000EF617
		internal static string Argument_NotEnoughBytesToRead
		{
			get
			{
				return SR.GetResourceString("Argument_NotEnoughBytesToRead");
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000F0423 File Offset: 0x000EF623
		internal static string Argument_NotEnoughBytesToWrite
		{
			get
			{
				return SR.GetResourceString("Argument_NotEnoughBytesToWrite");
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000F042F File Offset: 0x000EF62F
		internal static string Argument_NotEnoughGenArguments
		{
			get
			{
				return SR.GetResourceString("Argument_NotEnoughGenArguments");
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x000F043B File Offset: 0x000EF63B
		internal static string Argument_NotExceptionType
		{
			get
			{
				return SR.GetResourceString("Argument_NotExceptionType");
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x000F0447 File Offset: 0x000EF647
		internal static string Argument_NotInExceptionBlock
		{
			get
			{
				return SR.GetResourceString("Argument_NotInExceptionBlock");
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000F0453 File Offset: 0x000EF653
		internal static string Argument_NotMethodCallOpcode
		{
			get
			{
				return SR.GetResourceString("Argument_NotMethodCallOpcode");
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x000F045F File Offset: 0x000EF65F
		internal static string Argument_NotSerializable
		{
			get
			{
				return SR.GetResourceString("Argument_NotSerializable");
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000F046B File Offset: 0x000EF66B
		internal static string Argument_ObjNotComObject
		{
			get
			{
				return SR.GetResourceString("Argument_ObjNotComObject");
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000F0477 File Offset: 0x000EF677
		internal static string Argument_OffsetAndCapacityOutOfBounds
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetAndCapacityOutOfBounds");
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000F0483 File Offset: 0x000EF683
		internal static string Argument_OffsetLocalMismatch
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetLocalMismatch");
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000F048F File Offset: 0x000EF68F
		internal static string Argument_OffsetOfFieldNotFound
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetOfFieldNotFound");
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000F049B File Offset: 0x000EF69B
		internal static string Argument_OffsetOutOfRange
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetOutOfRange");
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x000F04A7 File Offset: 0x000EF6A7
		internal static string Argument_OffsetPrecision
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetPrecision");
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x000F04B3 File Offset: 0x000EF6B3
		internal static string Argument_OffsetUtcMismatch
		{
			get
			{
				return SR.GetResourceString("Argument_OffsetUtcMismatch");
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x000F04BF File Offset: 0x000EF6BF
		internal static string Argument_OneOfCulturesNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_OneOfCulturesNotSupported");
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x000F04CB File Offset: 0x000EF6CB
		internal static string Argument_OnlyMscorlib
		{
			get
			{
				return SR.GetResourceString("Argument_OnlyMscorlib");
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x000F04D7 File Offset: 0x000EF6D7
		internal static string Argument_OutOfOrderDateTimes
		{
			get
			{
				return SR.GetResourceString("Argument_OutOfOrderDateTimes");
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x000F04E3 File Offset: 0x000EF6E3
		internal static string Argument_PathEmpty
		{
			get
			{
				return SR.GetResourceString("Argument_PathEmpty");
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000F04EF File Offset: 0x000EF6EF
		internal static string Argument_PreAllocatedAlreadyAllocated
		{
			get
			{
				return SR.GetResourceString("Argument_PreAllocatedAlreadyAllocated");
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x000F04FB File Offset: 0x000EF6FB
		internal static string Argument_RecursiveFallback
		{
			get
			{
				return SR.GetResourceString("Argument_RecursiveFallback");
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000F0507 File Offset: 0x000EF707
		internal static string Argument_RecursiveFallbackBytes
		{
			get
			{
				return SR.GetResourceString("Argument_RecursiveFallbackBytes");
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000F0513 File Offset: 0x000EF713
		internal static string Argument_RedefinedLabel
		{
			get
			{
				return SR.GetResourceString("Argument_RedefinedLabel");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000F051F File Offset: 0x000EF71F
		internal static string Argument_ResolveField
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveField");
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000F052B File Offset: 0x000EF72B
		internal static string Argument_ResolveFieldHandle
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveFieldHandle");
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000F0537 File Offset: 0x000EF737
		internal static string Argument_ResolveMember
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveMember");
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000F0543 File Offset: 0x000EF743
		internal static string Argument_ResolveMethod
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveMethod");
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000F054F File Offset: 0x000EF74F
		internal static string Argument_ResolveMethodHandle
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveMethodHandle");
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x000F055B File Offset: 0x000EF75B
		internal static string Argument_ResolveModuleType
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveModuleType");
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000F0567 File Offset: 0x000EF767
		internal static string Argument_ResolveString
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveString");
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x000F0573 File Offset: 0x000EF773
		internal static string Argument_ResolveType
		{
			get
			{
				return SR.GetResourceString("Argument_ResolveType");
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x000F057F File Offset: 0x000EF77F
		internal static string Argument_ResultCalendarRange
		{
			get
			{
				return SR.GetResourceString("Argument_ResultCalendarRange");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x000F058B File Offset: 0x000EF78B
		internal static string Argument_SemaphoreInitialMaximum
		{
			get
			{
				return SR.GetResourceString("Argument_SemaphoreInitialMaximum");
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x000F0597 File Offset: 0x000EF797
		internal static string Argument_ShouldNotSpecifyExceptionType
		{
			get
			{
				return SR.GetResourceString("Argument_ShouldNotSpecifyExceptionType");
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000F05A3 File Offset: 0x000EF7A3
		internal static string Argument_ShouldOnlySetVisibilityFlags
		{
			get
			{
				return SR.GetResourceString("Argument_ShouldOnlySetVisibilityFlags");
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000F05AF File Offset: 0x000EF7AF
		internal static string Argument_SigIsFinalized
		{
			get
			{
				return SR.GetResourceString("Argument_SigIsFinalized");
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000F05BB File Offset: 0x000EF7BB
		internal static string Argument_StreamNotReadable
		{
			get
			{
				return SR.GetResourceString("Argument_StreamNotReadable");
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000F05C7 File Offset: 0x000EF7C7
		internal static string Argument_StreamNotWritable
		{
			get
			{
				return SR.GetResourceString("Argument_StreamNotWritable");
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000F05D3 File Offset: 0x000EF7D3
		internal static string Argument_StringFirstCharIsZero
		{
			get
			{
				return SR.GetResourceString("Argument_StringFirstCharIsZero");
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000F05DF File Offset: 0x000EF7DF
		internal static string Argument_StringZeroLength
		{
			get
			{
				return SR.GetResourceString("Argument_StringZeroLength");
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000F05EB File Offset: 0x000EF7EB
		internal static string Argument_TimeSpanHasSeconds
		{
			get
			{
				return SR.GetResourceString("Argument_TimeSpanHasSeconds");
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000F05F7 File Offset: 0x000EF7F7
		internal static string Argument_ToExclusiveLessThanFromExclusive
		{
			get
			{
				return SR.GetResourceString("Argument_ToExclusiveLessThanFromExclusive");
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000F0603 File Offset: 0x000EF803
		internal static string Argument_TooManyFinallyClause
		{
			get
			{
				return SR.GetResourceString("Argument_TooManyFinallyClause");
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000F060F File Offset: 0x000EF80F
		internal static string Argument_TransitionTimesAreIdentical
		{
			get
			{
				return SR.GetResourceString("Argument_TransitionTimesAreIdentical");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000F061B File Offset: 0x000EF81B
		internal static string Argument_TypedReferenceInvalidField
		{
			get
			{
				return SR.GetResourceString("Argument_TypedReferenceInvalidField");
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000F0627 File Offset: 0x000EF827
		internal static string Argument_TypeMustNotBeComImport
		{
			get
			{
				return SR.GetResourceString("Argument_TypeMustNotBeComImport");
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000F0633 File Offset: 0x000EF833
		internal static string Argument_TypeNameTooLong
		{
			get
			{
				return SR.GetResourceString("Argument_TypeNameTooLong");
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000F063F File Offset: 0x000EF83F
		internal static string Argument_TypeNotComObject
		{
			get
			{
				return SR.GetResourceString("Argument_TypeNotComObject");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x000F064B File Offset: 0x000EF84B
		internal static string Argument_TypeNotValid
		{
			get
			{
				return SR.GetResourceString("Argument_TypeNotValid");
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x000F0657 File Offset: 0x000EF857
		internal static string Argument_UnclosedExceptionBlock
		{
			get
			{
				return SR.GetResourceString("Argument_UnclosedExceptionBlock");
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x000F0663 File Offset: 0x000EF863
		internal static string Argument_UnknownUnmanagedCallConv
		{
			get
			{
				return SR.GetResourceString("Argument_UnknownUnmanagedCallConv");
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x000F066F File Offset: 0x000EF86F
		internal static string Argument_UnmanagedMemAccessorWrapAround
		{
			get
			{
				return SR.GetResourceString("Argument_UnmanagedMemAccessorWrapAround");
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000F067B File Offset: 0x000EF87B
		internal static string Argument_UnmatchedMethodForLocal
		{
			get
			{
				return SR.GetResourceString("Argument_UnmatchedMethodForLocal");
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000F0687 File Offset: 0x000EF887
		internal static string Argument_UnmatchingSymScope
		{
			get
			{
				return SR.GetResourceString("Argument_UnmatchingSymScope");
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000F0693 File Offset: 0x000EF893
		internal static string Argument_UTCOutOfRange
		{
			get
			{
				return SR.GetResourceString("Argument_UTCOutOfRange");
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000F069F File Offset: 0x000EF89F
		internal static string ArgumentException_BadMethodImplBody
		{
			get
			{
				return SR.GetResourceString("ArgumentException_BadMethodImplBody");
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000F06AB File Offset: 0x000EF8AB
		internal static string ArgumentException_BufferNotFromPool
		{
			get
			{
				return SR.GetResourceString("ArgumentException_BufferNotFromPool");
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x000F06B7 File Offset: 0x000EF8B7
		internal static string ArgumentException_OtherNotArrayOfCorrectLength
		{
			get
			{
				return SR.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength");
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x000F06C3 File Offset: 0x000EF8C3
		internal static string ArgumentException_NotIsomorphic
		{
			get
			{
				return SR.GetResourceString("ArgumentException_NotIsomorphic");
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000F06CF File Offset: 0x000EF8CF
		internal static string ArgumentException_TupleIncorrectType
		{
			get
			{
				return SR.GetResourceString("ArgumentException_TupleIncorrectType");
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000F06DB File Offset: 0x000EF8DB
		internal static string ArgumentException_TupleLastArgumentNotATuple
		{
			get
			{
				return SR.GetResourceString("ArgumentException_TupleLastArgumentNotATuple");
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000F06E7 File Offset: 0x000EF8E7
		internal static string ArgumentException_ValueTupleIncorrectType
		{
			get
			{
				return SR.GetResourceString("ArgumentException_ValueTupleIncorrectType");
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000F06F3 File Offset: 0x000EF8F3
		internal static string ArgumentException_ValueTupleLastArgumentNotAValueTuple
		{
			get
			{
				return SR.GetResourceString("ArgumentException_ValueTupleLastArgumentNotAValueTuple");
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000F06FF File Offset: 0x000EF8FF
		internal static string ArgumentNull_Array
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Array");
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000F070B File Offset: 0x000EF90B
		internal static string ArgumentNull_ArrayElement
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_ArrayElement");
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000F0717 File Offset: 0x000EF917
		internal static string ArgumentNull_ArrayValue
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_ArrayValue");
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000F0723 File Offset: 0x000EF923
		internal static string ArgumentNull_Assembly
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Assembly");
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000F072F File Offset: 0x000EF92F
		internal static string ArgumentNull_AssemblyNameName
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_AssemblyNameName");
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000F073B File Offset: 0x000EF93B
		internal static string ArgumentNull_Buffer
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Buffer");
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x000F0747 File Offset: 0x000EF947
		internal static string ArgumentNull_Child
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Child");
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000F0753 File Offset: 0x000EF953
		internal static string ArgumentNull_Collection
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Collection");
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000F075F File Offset: 0x000EF95F
		internal static string ArgumentNull_Dictionary
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Dictionary");
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000F076B File Offset: 0x000EF96B
		internal static string ArgumentNull_Generic
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Generic");
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000F0777 File Offset: 0x000EF977
		internal static string ArgumentNull_Key
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Key");
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000F0783 File Offset: 0x000EF983
		internal static string ArgumentNull_Path
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Path");
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x000F078F File Offset: 0x000EF98F
		internal static string ArgumentNull_SafeHandle
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_SafeHandle");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000F079B File Offset: 0x000EF99B
		internal static string ArgumentNull_Stream
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Stream");
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000F07A7 File Offset: 0x000EF9A7
		internal static string ArgumentNull_String
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_String");
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000F07B3 File Offset: 0x000EF9B3
		internal static string ArgumentNull_Type
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Type");
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000F07BF File Offset: 0x000EF9BF
		internal static string ArgumentNull_Waithandles
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_Waithandles");
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x000F07CB File Offset: 0x000EF9CB
		internal static string ArgumentOutOfRange_ActualValue
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ActualValue");
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000F07D7 File Offset: 0x000EF9D7
		internal static string ArgumentOutOfRange_AddValue
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_AddValue");
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000F07E3 File Offset: 0x000EF9E3
		internal static string ArgumentOutOfRange_ArrayLB
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ArrayLB");
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x000F07EF File Offset: 0x000EF9EF
		internal static string ArgumentOutOfRange_BadHourMinuteSecond
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond");
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000F07FB File Offset: 0x000EF9FB
		internal static string ArgumentOutOfRange_BadYearMonthDay
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_BadYearMonthDay");
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x000F0807 File Offset: 0x000EFA07
		internal static string ArgumentOutOfRange_BiggerThanCollection
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_BiggerThanCollection");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000F0813 File Offset: 0x000EFA13
		internal static string ArgumentOutOfRange_BinaryReaderFillBuffer
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_BinaryReaderFillBuffer");
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x000F081F File Offset: 0x000EFA1F
		internal static string ArgumentOutOfRange_Bounds_Lower_Upper
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper");
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x000F082B File Offset: 0x000EFA2B
		internal static string ArgumentOutOfRange_CalendarRange
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_CalendarRange");
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000F0837 File Offset: 0x000EFA37
		internal static string ArgumentOutOfRange_Capacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Capacity");
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000F0843 File Offset: 0x000EFA43
		internal static string ArgumentOutOfRange_Count
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Count");
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000F084F File Offset: 0x000EFA4F
		internal static string ArgumentOutOfRange_DateArithmetic
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DateArithmetic");
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000F085B File Offset: 0x000EFA5B
		internal static string ArgumentOutOfRange_DateTimeBadMonths
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DateTimeBadMonths");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x000F0867 File Offset: 0x000EFA67
		internal static string ArgumentOutOfRange_DateTimeBadTicks
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks");
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x000F0873 File Offset: 0x000EFA73
		internal static string ArgumentOutOfRange_DateTimeBadYears
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DateTimeBadYears");
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000F087F File Offset: 0x000EFA7F
		internal static string ArgumentOutOfRange_Day
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Day");
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000F088B File Offset: 0x000EFA8B
		internal static string ArgumentOutOfRange_DayOfWeek
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DayOfWeek");
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x000F0897 File Offset: 0x000EFA97
		internal static string ArgumentOutOfRange_DayParam
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DayParam");
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x000F08A3 File Offset: 0x000EFAA3
		internal static string ArgumentOutOfRange_DecimalRound
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DecimalRound");
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x000F08AF File Offset: 0x000EFAAF
		internal static string ArgumentOutOfRange_DecimalScale
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_DecimalScale");
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x000F08BB File Offset: 0x000EFABB
		internal static string ArgumentOutOfRange_EndIndexStartIndex
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_EndIndexStartIndex");
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x000F08C7 File Offset: 0x000EFAC7
		internal static string ArgumentOutOfRange_Enum
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Enum");
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000F08D3 File Offset: 0x000EFAD3
		internal static string ArgumentOutOfRange_Era
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Era");
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x000F08DF File Offset: 0x000EFADF
		internal static string ArgumentOutOfRange_FileLengthTooBig
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_FileLengthTooBig");
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000F08EB File Offset: 0x000EFAEB
		internal static string ArgumentOutOfRange_FileTimeInvalid
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_FileTimeInvalid");
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x000F08F7 File Offset: 0x000EFAF7
		internal static string ArgumentOutOfRange_GenericPositive
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_GenericPositive");
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x000F0903 File Offset: 0x000EFB03
		internal static string ArgumentOutOfRange_GetByteCountOverflow
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow");
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x000F090F File Offset: 0x000EFB0F
		internal static string ArgumentOutOfRange_GetCharCountOverflow
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow");
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x000F091B File Offset: 0x000EFB1B
		internal static string ArgumentOutOfRange_HashtableLoadFactor
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_HashtableLoadFactor");
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x000F0927 File Offset: 0x000EFB27
		internal static string ArgumentOutOfRange_HugeArrayNotSupported
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_HugeArrayNotSupported");
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x000F0933 File Offset: 0x000EFB33
		internal static string ArgumentOutOfRange_Index
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Index");
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000F093F File Offset: 0x000EFB3F
		internal static string ArgumentOutOfRange_IndexCount
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_IndexCount");
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000F094B File Offset: 0x000EFB4B
		internal static string ArgumentOutOfRange_IndexCountBuffer
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_IndexCountBuffer");
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000F0957 File Offset: 0x000EFB57
		internal static string ArgumentOutOfRange_IndexLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_IndexLength");
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000F0963 File Offset: 0x000EFB63
		internal static string ArgumentOutOfRange_IndexString
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_IndexString");
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000F096F File Offset: 0x000EFB6F
		internal static string ArgumentOutOfRange_InputTooLarge
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_InputTooLarge");
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x000F097B File Offset: 0x000EFB7B
		internal static string ArgumentOutOfRange_InvalidEraValue
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_InvalidEraValue");
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000F0987 File Offset: 0x000EFB87
		internal static string ArgumentOutOfRange_InvalidHighSurrogate
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate");
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x000F0993 File Offset: 0x000EFB93
		internal static string ArgumentOutOfRange_InvalidLowSurrogate
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate");
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x000F099F File Offset: 0x000EFB9F
		internal static string ArgumentOutOfRange_InvalidUTF32
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_InvalidUTF32");
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000F09AB File Offset: 0x000EFBAB
		internal static string ArgumentOutOfRange_Length
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Length");
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x000F09B7 File Offset: 0x000EFBB7
		internal static string ArgumentOutOfRange_LengthGreaterThanCapacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_LengthGreaterThanCapacity");
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000F09C3 File Offset: 0x000EFBC3
		internal static string ArgumentOutOfRange_LengthTooLarge
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_LengthTooLarge");
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000F09CF File Offset: 0x000EFBCF
		internal static string ArgumentOutOfRange_LessEqualToIntegerMaxVal
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_LessEqualToIntegerMaxVal");
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000F09DB File Offset: 0x000EFBDB
		internal static string ArgumentOutOfRange_ListInsert
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ListInsert");
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x000F09E7 File Offset: 0x000EFBE7
		internal static string ArgumentOutOfRange_Month
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Month");
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000F09F3 File Offset: 0x000EFBF3
		internal static string ArgumentOutOfRange_MonthParam
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_MonthParam");
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000F09FF File Offset: 0x000EFBFF
		internal static string ArgumentOutOfRange_MustBeNonNegInt32
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32");
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000F0A0B File Offset: 0x000EFC0B
		internal static string ArgumentOutOfRange_MustBeNonNegNum
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum");
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000F0A17 File Offset: 0x000EFC17
		internal static string ArgumentOutOfRange_MustBePositive
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_MustBePositive");
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000F0A23 File Offset: 0x000EFC23
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NeedNonNegNum");
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000F0A2F File Offset: 0x000EFC2F
		internal static string ArgumentOutOfRange_NeedNonNegOrNegative1
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1");
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000F0A3B File Offset: 0x000EFC3B
		internal static string ArgumentOutOfRange_NeedPosNum
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NeedPosNum");
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x000F0A47 File Offset: 0x000EFC47
		internal static string ArgumentOutOfRange_NeedValidId
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NeedValidId");
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x000F0A53 File Offset: 0x000EFC53
		internal static string ArgumentOutOfRange_NegativeCapacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NegativeCapacity");
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x000F0A5F File Offset: 0x000EFC5F
		internal static string ArgumentOutOfRange_NegativeCount
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NegativeCount");
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x000F0A6B File Offset: 0x000EFC6B
		internal static string ArgumentOutOfRange_NegativeLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NegativeLength");
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x000F0A77 File Offset: 0x000EFC77
		internal static string ArgumentOutOfRange_OffsetLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_OffsetLength");
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000F0A83 File Offset: 0x000EFC83
		internal static string ArgumentOutOfRange_OffsetOut
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_OffsetOut");
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000F0A8F File Offset: 0x000EFC8F
		internal static string ArgumentOutOfRange_ParamSequence
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ParamSequence");
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000F0A9B File Offset: 0x000EFC9B
		internal static string ArgumentOutOfRange_PartialWCHAR
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_PartialWCHAR");
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000F0AA7 File Offset: 0x000EFCA7
		internal static string ArgumentOutOfRange_PeriodTooLarge
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_PeriodTooLarge");
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000F0AB3 File Offset: 0x000EFCB3
		internal static string ArgumentOutOfRange_PositionLessThanCapacityRequired
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_PositionLessThanCapacityRequired");
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000F0ABF File Offset: 0x000EFCBF
		internal static string ArgumentOutOfRange_Range
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Range");
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x000F0ACB File Offset: 0x000EFCCB
		internal static string ArgumentOutOfRange_RoundingDigits
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_RoundingDigits");
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000F0AD7 File Offset: 0x000EFCD7
		internal static string ArgumentOutOfRange_SmallCapacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_SmallCapacity");
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000F0AE3 File Offset: 0x000EFCE3
		internal static string ArgumentOutOfRange_SmallMaxCapacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity");
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000F0AEF File Offset: 0x000EFCEF
		internal static string ArgumentOutOfRange_StartIndex
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_StartIndex");
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x000F0AFB File Offset: 0x000EFCFB
		internal static string ArgumentOutOfRange_StartIndexLargerThanLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength");
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x000F0B07 File Offset: 0x000EFD07
		internal static string ArgumentOutOfRange_StartIndexLessThanLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_StartIndexLessThanLength");
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x000F0B13 File Offset: 0x000EFD13
		internal static string ArgumentOutOfRange_StreamLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_StreamLength");
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x000F0B1F File Offset: 0x000EFD1F
		internal static string ArgumentOutOfRange_TimeoutTooLarge
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_TimeoutTooLarge");
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x000F0B2B File Offset: 0x000EFD2B
		internal static string ArgumentOutOfRange_UIntPtrMax
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_UIntPtrMax");
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x000F0B37 File Offset: 0x000EFD37
		internal static string ArgumentOutOfRange_UnmanagedMemStreamLength
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength");
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000F0B43 File Offset: 0x000EFD43
		internal static string ArgumentOutOfRange_UnmanagedMemStreamWrapAround
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround");
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000F0B4F File Offset: 0x000EFD4F
		internal static string ArgumentOutOfRange_UtcOffset
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_UtcOffset");
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000F0B5B File Offset: 0x000EFD5B
		internal static string ArgumentOutOfRange_UtcOffsetAndDaylightDelta
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_UtcOffsetAndDaylightDelta");
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000F0B67 File Offset: 0x000EFD67
		internal static string ArgumentOutOfRange_Version
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Version");
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000F0B73 File Offset: 0x000EFD73
		internal static string ArgumentOutOfRange_Week
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Week");
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000F0B7F File Offset: 0x000EFD7F
		internal static string ArgumentOutOfRange_Year
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Year");
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000F0B8B File Offset: 0x000EFD8B
		internal static string Arithmetic_NaN
		{
			get
			{
				return SR.GetResourceString("Arithmetic_NaN");
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000F0B97 File Offset: 0x000EFD97
		internal static string ArrayTypeMismatch_ConstrainedCopy
		{
			get
			{
				return SR.GetResourceString("ArrayTypeMismatch_ConstrainedCopy");
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000F0BA3 File Offset: 0x000EFDA3
		internal static string AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible
		{
			get
			{
				return SR.GetResourceString("AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible");
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000F0BAF File Offset: 0x000EFDAF
		internal static string AssemblyLoadContext_Verify_NotUnloading
		{
			get
			{
				return SR.GetResourceString("AssemblyLoadContext_Verify_NotUnloading");
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000F0BBB File Offset: 0x000EFDBB
		internal static string AssertionFailed
		{
			get
			{
				return SR.GetResourceString("AssertionFailed");
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x000F0BC7 File Offset: 0x000EFDC7
		internal static string AssertionFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("AssertionFailed_Cnd");
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000F0BD3 File Offset: 0x000EFDD3
		internal static string AssumptionFailed
		{
			get
			{
				return SR.GetResourceString("AssumptionFailed");
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x000F0BDF File Offset: 0x000EFDDF
		internal static string AssumptionFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("AssumptionFailed_Cnd");
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x000F0BEB File Offset: 0x000EFDEB
		internal static string AsyncMethodBuilder_InstanceNotInitialized
		{
			get
			{
				return SR.GetResourceString("AsyncMethodBuilder_InstanceNotInitialized");
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000F0BF7 File Offset: 0x000EFDF7
		internal static string BadImageFormat_BadILFormat
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_BadILFormat");
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000F0C03 File Offset: 0x000EFE03
		internal static string BadImageFormat_InvalidType
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_InvalidType");
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000F0C0F File Offset: 0x000EFE0F
		internal static string BadImageFormat_NegativeStringLength
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_NegativeStringLength");
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000F0C1B File Offset: 0x000EFE1B
		internal static string BadImageFormat_ParameterSignatureMismatch
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ParameterSignatureMismatch");
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000F0C27 File Offset: 0x000EFE27
		internal static string BadImageFormat_ResType_SerBlobMismatch
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResType_SerBlobMismatch");
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000F0C33 File Offset: 0x000EFE33
		internal static string BadImageFormat_ResourceDataLengthInvalid
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourceDataLengthInvalid");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000F0C3F File Offset: 0x000EFE3F
		internal static string BadImageFormat_ResourceNameCorrupted
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourceNameCorrupted");
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x000F0C4B File Offset: 0x000EFE4B
		internal static string BadImageFormat_ResourceNameCorrupted_NameIndex
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex");
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000F0C57 File Offset: 0x000EFE57
		internal static string BadImageFormat_ResourcesDataInvalidOffset
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000F0C63 File Offset: 0x000EFE63
		internal static string BadImageFormat_ResourcesHeaderCorrupted
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted");
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000F0C6F File Offset: 0x000EFE6F
		internal static string BadImageFormat_ResourcesIndexTooLong
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourcesIndexTooLong");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x000F0C7B File Offset: 0x000EFE7B
		internal static string BadImageFormat_ResourcesNameInvalidOffset
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000F0C87 File Offset: 0x000EFE87
		internal static string BadImageFormat_ResourcesNameTooLong
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_ResourcesNameTooLong");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000F0C93 File Offset: 0x000EFE93
		internal static string BadImageFormat_TypeMismatch
		{
			get
			{
				return SR.GetResourceString("BadImageFormat_TypeMismatch");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000F0C9F File Offset: 0x000EFE9F
		internal static string CancellationToken_CreateLinkedToken_TokensIsEmpty
		{
			get
			{
				return SR.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty");
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x000F0CAB File Offset: 0x000EFEAB
		internal static string CancellationTokenSource_Disposed
		{
			get
			{
				return SR.GetResourceString("CancellationTokenSource_Disposed");
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000F0CB7 File Offset: 0x000EFEB7
		internal static string ConcurrentCollection_SyncRoot_NotSupported
		{
			get
			{
				return SR.GetResourceString("ConcurrentCollection_SyncRoot_NotSupported");
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000F0CC3 File Offset: 0x000EFEC3
		internal static string EventSource_AbstractMustNotDeclareEventMethods
		{
			get
			{
				return SR.GetResourceString("EventSource_AbstractMustNotDeclareEventMethods");
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000F0CCF File Offset: 0x000EFECF
		internal static string EventSource_AbstractMustNotDeclareKTOC
		{
			get
			{
				return SR.GetResourceString("EventSource_AbstractMustNotDeclareKTOC");
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000F0CDB File Offset: 0x000EFEDB
		internal static string EventSource_AddScalarOutOfRange
		{
			get
			{
				return SR.GetResourceString("EventSource_AddScalarOutOfRange");
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000F0CE7 File Offset: 0x000EFEE7
		internal static string EventSource_BadHexDigit
		{
			get
			{
				return SR.GetResourceString("EventSource_BadHexDigit");
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000F0CF3 File Offset: 0x000EFEF3
		internal static string EventSource_ChannelTypeDoesNotMatchEventChannelValue
		{
			get
			{
				return SR.GetResourceString("EventSource_ChannelTypeDoesNotMatchEventChannelValue");
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000F0CFF File Offset: 0x000EFEFF
		internal static string EventSource_DataDescriptorsOutOfRange
		{
			get
			{
				return SR.GetResourceString("EventSource_DataDescriptorsOutOfRange");
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000F0D0B File Offset: 0x000EFF0B
		internal static string EventSource_DuplicateStringKey
		{
			get
			{
				return SR.GetResourceString("EventSource_DuplicateStringKey");
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000F0D17 File Offset: 0x000EFF17
		internal static string EventSource_EnumKindMismatch
		{
			get
			{
				return SR.GetResourceString("EventSource_EnumKindMismatch");
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000F0D23 File Offset: 0x000EFF23
		internal static string EventSource_EvenHexDigits
		{
			get
			{
				return SR.GetResourceString("EventSource_EvenHexDigits");
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000F0D2F File Offset: 0x000EFF2F
		internal static string EventSource_EventChannelOutOfRange
		{
			get
			{
				return SR.GetResourceString("EventSource_EventChannelOutOfRange");
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x000F0D3B File Offset: 0x000EFF3B
		internal static string EventSource_EventIdReused
		{
			get
			{
				return SR.GetResourceString("EventSource_EventIdReused");
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x000F0D47 File Offset: 0x000EFF47
		internal static string EventSource_EventMustHaveTaskIfNonDefaultOpcode
		{
			get
			{
				return SR.GetResourceString("EventSource_EventMustHaveTaskIfNonDefaultOpcode");
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000F0D53 File Offset: 0x000EFF53
		internal static string EventSource_EventMustNotBeExplicitImplementation
		{
			get
			{
				return SR.GetResourceString("EventSource_EventMustNotBeExplicitImplementation");
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x000F0D5F File Offset: 0x000EFF5F
		internal static string EventSource_EventNameReused
		{
			get
			{
				return SR.GetResourceString("EventSource_EventNameReused");
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000F0D6B File Offset: 0x000EFF6B
		internal static string EventSource_EventParametersMismatch
		{
			get
			{
				return SR.GetResourceString("EventSource_EventParametersMismatch");
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x000F0D77 File Offset: 0x000EFF77
		internal static string EventSource_EventSourceGuidInUse
		{
			get
			{
				return SR.GetResourceString("EventSource_EventSourceGuidInUse");
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000F0D83 File Offset: 0x000EFF83
		internal static string EventSource_EventTooBig
		{
			get
			{
				return SR.GetResourceString("EventSource_EventTooBig");
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000F0D8F File Offset: 0x000EFF8F
		internal static string EventSource_EventWithAdminChannelMustHaveMessage
		{
			get
			{
				return SR.GetResourceString("EventSource_EventWithAdminChannelMustHaveMessage");
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000F0D9B File Offset: 0x000EFF9B
		internal static string EventSource_IllegalKeywordsValue
		{
			get
			{
				return SR.GetResourceString("EventSource_IllegalKeywordsValue");
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x000F0DA7 File Offset: 0x000EFFA7
		internal static string EventSource_IllegalOpcodeValue
		{
			get
			{
				return SR.GetResourceString("EventSource_IllegalOpcodeValue");
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000F0DB3 File Offset: 0x000EFFB3
		internal static string EventSource_IllegalTaskValue
		{
			get
			{
				return SR.GetResourceString("EventSource_IllegalTaskValue");
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000F0DBF File Offset: 0x000EFFBF
		internal static string EventSource_IllegalValue
		{
			get
			{
				return SR.GetResourceString("EventSource_IllegalValue");
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000F0DCB File Offset: 0x000EFFCB
		internal static string EventSource_IncorrentlyAuthoredTypeInfo
		{
			get
			{
				return SR.GetResourceString("EventSource_IncorrentlyAuthoredTypeInfo");
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000F0DD7 File Offset: 0x000EFFD7
		internal static string EventSource_InvalidCommand
		{
			get
			{
				return SR.GetResourceString("EventSource_InvalidCommand");
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000F0DE3 File Offset: 0x000EFFE3
		internal static string EventSource_InvalidEventFormat
		{
			get
			{
				return SR.GetResourceString("EventSource_InvalidEventFormat");
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000F0DEF File Offset: 0x000EFFEF
		internal static string EventSource_KeywordCollision
		{
			get
			{
				return SR.GetResourceString("EventSource_KeywordCollision");
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000F0DFB File Offset: 0x000EFFFB
		internal static string EventSource_KeywordNeedPowerOfTwo
		{
			get
			{
				return SR.GetResourceString("EventSource_KeywordNeedPowerOfTwo");
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x000F0E07 File Offset: 0x000F0007
		internal static string EventSource_ListenerCreatedInsideCallback
		{
			get
			{
				return SR.GetResourceString("EventSource_ListenerCreatedInsideCallback");
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000F0E13 File Offset: 0x000F0013
		internal static string EventSource_ListenerNotFound
		{
			get
			{
				return SR.GetResourceString("EventSource_ListenerNotFound");
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x000F0E1F File Offset: 0x000F001F
		internal static string EventSource_ListenerWriteFailure
		{
			get
			{
				return SR.GetResourceString("EventSource_ListenerWriteFailure");
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x000F0E2B File Offset: 0x000F002B
		internal static string EventSource_MaxChannelExceeded
		{
			get
			{
				return SR.GetResourceString("EventSource_MaxChannelExceeded");
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x000F0E37 File Offset: 0x000F0037
		internal static string EventSource_MismatchIdToWriteEvent
		{
			get
			{
				return SR.GetResourceString("EventSource_MismatchIdToWriteEvent");
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x000F0E43 File Offset: 0x000F0043
		internal static string EventSource_NeedGuid
		{
			get
			{
				return SR.GetResourceString("EventSource_NeedGuid");
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000F0E4F File Offset: 0x000F004F
		internal static string EventSource_NeedName
		{
			get
			{
				return SR.GetResourceString("EventSource_NeedName");
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x000F0E5B File Offset: 0x000F005B
		internal static string EventSource_NeedPositiveId
		{
			get
			{
				return SR.GetResourceString("EventSource_NeedPositiveId");
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x000F0E67 File Offset: 0x000F0067
		internal static string EventSource_NoFreeBuffers
		{
			get
			{
				return SR.GetResourceString("EventSource_NoFreeBuffers");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x000F0E73 File Offset: 0x000F0073
		internal static string EventSource_NonCompliantTypeError
		{
			get
			{
				return SR.GetResourceString("EventSource_NonCompliantTypeError");
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x000F0E7F File Offset: 0x000F007F
		internal static string EventSource_NoRelatedActivityId
		{
			get
			{
				return SR.GetResourceString("EventSource_NoRelatedActivityId");
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x000F0E8B File Offset: 0x000F008B
		internal static string EventSource_NotSupportedArrayOfBinary
		{
			get
			{
				return SR.GetResourceString("EventSource_NotSupportedArrayOfBinary");
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x000F0E97 File Offset: 0x000F0097
		internal static string EventSource_NotSupportedArrayOfNil
		{
			get
			{
				return SR.GetResourceString("EventSource_NotSupportedArrayOfNil");
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000F0EA3 File Offset: 0x000F00A3
		internal static string EventSource_NotSupportedArrayOfNullTerminatedString
		{
			get
			{
				return SR.GetResourceString("EventSource_NotSupportedArrayOfNullTerminatedString");
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x000F0EAF File Offset: 0x000F00AF
		internal static string EventSource_NotSupportedNestedArraysEnums
		{
			get
			{
				return SR.GetResourceString("EventSource_NotSupportedNestedArraysEnums");
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x000F0EBB File Offset: 0x000F00BB
		internal static string EventSource_NullInput
		{
			get
			{
				return SR.GetResourceString("EventSource_NullInput");
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x000F0EC7 File Offset: 0x000F00C7
		internal static string EventSource_OpcodeCollision
		{
			get
			{
				return SR.GetResourceString("EventSource_OpcodeCollision");
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x000F0ED3 File Offset: 0x000F00D3
		internal static string EventSource_PinArrayOutOfRange
		{
			get
			{
				return SR.GetResourceString("EventSource_PinArrayOutOfRange");
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000F0EDF File Offset: 0x000F00DF
		internal static string EventSource_RecursiveTypeDefinition
		{
			get
			{
				return SR.GetResourceString("EventSource_RecursiveTypeDefinition");
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000F0EEB File Offset: 0x000F00EB
		internal static string EventSource_StopsFollowStarts
		{
			get
			{
				return SR.GetResourceString("EventSource_StopsFollowStarts");
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000F0EF7 File Offset: 0x000F00F7
		internal static string EventSource_TaskCollision
		{
			get
			{
				return SR.GetResourceString("EventSource_TaskCollision");
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x000F0F03 File Offset: 0x000F0103
		internal static string EventSource_TaskOpcodePairReused
		{
			get
			{
				return SR.GetResourceString("EventSource_TaskOpcodePairReused");
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x000F0F0F File Offset: 0x000F010F
		internal static string EventSource_TooManyArgs
		{
			get
			{
				return SR.GetResourceString("EventSource_TooManyArgs");
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000F0F1B File Offset: 0x000F011B
		internal static string EventSource_TooManyFields
		{
			get
			{
				return SR.GetResourceString("EventSource_TooManyFields");
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x000F0F27 File Offset: 0x000F0127
		internal static string EventSource_ToString
		{
			get
			{
				return SR.GetResourceString("EventSource_ToString");
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x000F0F33 File Offset: 0x000F0133
		internal static string EventSource_TraitEven
		{
			get
			{
				return SR.GetResourceString("EventSource_TraitEven");
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x000F0F3F File Offset: 0x000F013F
		internal static string EventSource_TypeMustBeSealedOrAbstract
		{
			get
			{
				return SR.GetResourceString("EventSource_TypeMustBeSealedOrAbstract");
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x000F0F4B File Offset: 0x000F014B
		internal static string EventSource_TypeMustDeriveFromEventSource
		{
			get
			{
				return SR.GetResourceString("EventSource_TypeMustDeriveFromEventSource");
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x000F0F57 File Offset: 0x000F0157
		internal static string EventSource_UndefinedChannel
		{
			get
			{
				return SR.GetResourceString("EventSource_UndefinedChannel");
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x000F0F63 File Offset: 0x000F0163
		internal static string EventSource_UndefinedKeyword
		{
			get
			{
				return SR.GetResourceString("EventSource_UndefinedKeyword");
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x000F0F6F File Offset: 0x000F016F
		internal static string EventSource_UndefinedOpcode
		{
			get
			{
				return SR.GetResourceString("EventSource_UndefinedOpcode");
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x000F0F7B File Offset: 0x000F017B
		internal static string EventSource_UnknownEtwTrait
		{
			get
			{
				return SR.GetResourceString("EventSource_UnknownEtwTrait");
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x000F0F87 File Offset: 0x000F0187
		internal static string EventSource_UnsupportedEventTypeInManifest
		{
			get
			{
				return SR.GetResourceString("EventSource_UnsupportedEventTypeInManifest");
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x000F0F93 File Offset: 0x000F0193
		internal static string EventSource_UnsupportedMessageProperty
		{
			get
			{
				return SR.GetResourceString("EventSource_UnsupportedMessageProperty");
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x000F0F9F File Offset: 0x000F019F
		internal static string EventSource_VarArgsParameterMismatch
		{
			get
			{
				return SR.GetResourceString("EventSource_VarArgsParameterMismatch");
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x000F0FAB File Offset: 0x000F01AB
		internal static string Exception_EndOfInnerExceptionStack
		{
			get
			{
				return SR.GetResourceString("Exception_EndOfInnerExceptionStack");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x000F0FB7 File Offset: 0x000F01B7
		internal static string Exception_EndStackTraceFromPreviousThrow
		{
			get
			{
				return SR.GetResourceString("Exception_EndStackTraceFromPreviousThrow");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x000F0FC3 File Offset: 0x000F01C3
		internal static string Exception_WasThrown
		{
			get
			{
				return SR.GetResourceString("Exception_WasThrown");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x000F0FCF File Offset: 0x000F01CF
		internal static string ExecutionContext_ExceptionInAsyncLocalNotification
		{
			get
			{
				return SR.GetResourceString("ExecutionContext_ExceptionInAsyncLocalNotification");
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x000F0FDB File Offset: 0x000F01DB
		internal static string FileNotFound_ResolveAssembly
		{
			get
			{
				return SR.GetResourceString("FileNotFound_ResolveAssembly");
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x000F0FE7 File Offset: 0x000F01E7
		internal static string Format_AttributeUsage
		{
			get
			{
				return SR.GetResourceString("Format_AttributeUsage");
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000F0FF3 File Offset: 0x000F01F3
		internal static string Format_Bad7BitInt
		{
			get
			{
				return SR.GetResourceString("Format_Bad7BitInt");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x000F0FFF File Offset: 0x000F01FF
		internal static string Format_BadBase64Char
		{
			get
			{
				return SR.GetResourceString("Format_BadBase64Char");
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x000F100B File Offset: 0x000F020B
		internal static string Format_BadBoolean
		{
			get
			{
				return SR.GetResourceString("Format_BadBoolean");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x000F1017 File Offset: 0x000F0217
		internal static string Format_BadFormatSpecifier
		{
			get
			{
				return SR.GetResourceString("Format_BadFormatSpecifier");
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000F1023 File Offset: 0x000F0223
		internal static string Format_NoFormatSpecifier
		{
			get
			{
				return SR.GetResourceString("Format_NoFormatSpecifier");
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x000F102F File Offset: 0x000F022F
		internal static string Format_BadHexChar
		{
			get
			{
				return SR.GetResourceString("Format_BadHexChar");
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000F103B File Offset: 0x000F023B
		internal static string Format_BadHexLength
		{
			get
			{
				return SR.GetResourceString("Format_BadHexLength");
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x000F1047 File Offset: 0x000F0247
		internal static string Format_BadQuote
		{
			get
			{
				return SR.GetResourceString("Format_BadQuote");
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x000F1053 File Offset: 0x000F0253
		internal static string Format_BadTimeSpan
		{
			get
			{
				return SR.GetResourceString("Format_BadTimeSpan");
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x000F105F File Offset: 0x000F025F
		internal static string Format_EmptyInputString
		{
			get
			{
				return SR.GetResourceString("Format_EmptyInputString");
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000F106B File Offset: 0x000F026B
		internal static string Format_ExtraJunkAtEnd
		{
			get
			{
				return SR.GetResourceString("Format_ExtraJunkAtEnd");
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x000F1077 File Offset: 0x000F0277
		internal static string Format_GuidUnrecognized
		{
			get
			{
				return SR.GetResourceString("Format_GuidUnrecognized");
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000F1083 File Offset: 0x000F0283
		internal static string Format_IndexOutOfRange
		{
			get
			{
				return SR.GetResourceString("Format_IndexOutOfRange");
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000F108F File Offset: 0x000F028F
		internal static string Format_InvalidEnumFormatSpecification
		{
			get
			{
				return SR.GetResourceString("Format_InvalidEnumFormatSpecification");
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x000F109B File Offset: 0x000F029B
		internal static string Format_InvalidGuidFormatSpecification
		{
			get
			{
				return SR.GetResourceString("Format_InvalidGuidFormatSpecification");
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000F10A7 File Offset: 0x000F02A7
		internal static string Format_InvalidString
		{
			get
			{
				return SR.GetResourceString("Format_InvalidString");
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000F10B3 File Offset: 0x000F02B3
		internal static string Format_NeedSingleChar
		{
			get
			{
				return SR.GetResourceString("Format_NeedSingleChar");
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x000F10BF File Offset: 0x000F02BF
		internal static string Format_NoParsibleDigits
		{
			get
			{
				return SR.GetResourceString("Format_NoParsibleDigits");
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000F10CB File Offset: 0x000F02CB
		internal static string Format_StringZeroLength
		{
			get
			{
				return SR.GetResourceString("Format_StringZeroLength");
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x000F10D7 File Offset: 0x000F02D7
		internal static string IndexOutOfRange_ArrayRankIndex
		{
			get
			{
				return SR.GetResourceString("IndexOutOfRange_ArrayRankIndex");
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000F10E3 File Offset: 0x000F02E3
		internal static string IndexOutOfRange_UMSPosition
		{
			get
			{
				return SR.GetResourceString("IndexOutOfRange_UMSPosition");
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x000F10EF File Offset: 0x000F02EF
		internal static string InsufficientMemory_MemFailPoint
		{
			get
			{
				return SR.GetResourceString("InsufficientMemory_MemFailPoint");
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x000F10FB File Offset: 0x000F02FB
		internal static string InsufficientMemory_MemFailPoint_TooBig
		{
			get
			{
				return SR.GetResourceString("InsufficientMemory_MemFailPoint_TooBig");
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000F1107 File Offset: 0x000F0307
		internal static string InsufficientMemory_MemFailPoint_VAFrag
		{
			get
			{
				return SR.GetResourceString("InsufficientMemory_MemFailPoint_VAFrag");
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000F1113 File Offset: 0x000F0313
		internal static string Interop_COM_TypeMismatch
		{
			get
			{
				return SR.GetResourceString("Interop_COM_TypeMismatch");
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000F111F File Offset: 0x000F031F
		internal static string Interop_Marshal_Unmappable_Char
		{
			get
			{
				return SR.GetResourceString("Interop_Marshal_Unmappable_Char");
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000F112B File Offset: 0x000F032B
		internal static string Interop_Marshal_SafeHandle_InvalidOperation
		{
			get
			{
				return SR.GetResourceString("Interop_Marshal_SafeHandle_InvalidOperation");
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000F1137 File Offset: 0x000F0337
		internal static string Interop_Marshal_CannotCreateSafeHandleField
		{
			get
			{
				return SR.GetResourceString("Interop_Marshal_CannotCreateSafeHandleField");
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000F1143 File Offset: 0x000F0343
		internal static string Interop_Marshal_CannotCreateCriticalHandleField
		{
			get
			{
				return SR.GetResourceString("Interop_Marshal_CannotCreateCriticalHandleField");
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x000F114F File Offset: 0x000F034F
		internal static string InvalidCast_CannotCastNullToValueType
		{
			get
			{
				return SR.GetResourceString("InvalidCast_CannotCastNullToValueType");
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000F115B File Offset: 0x000F035B
		internal static string InvalidCast_CannotCoerceByRefVariant
		{
			get
			{
				return SR.GetResourceString("InvalidCast_CannotCoerceByRefVariant");
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x000F1167 File Offset: 0x000F0367
		internal static string InvalidCast_DBNull
		{
			get
			{
				return SR.GetResourceString("InvalidCast_DBNull");
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x000F1173 File Offset: 0x000F0373
		internal static string InvalidCast_Empty
		{
			get
			{
				return SR.GetResourceString("InvalidCast_Empty");
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000F117F File Offset: 0x000F037F
		internal static string InvalidCast_FromDBNull
		{
			get
			{
				return SR.GetResourceString("InvalidCast_FromDBNull");
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000F118B File Offset: 0x000F038B
		internal static string InvalidCast_FromTo
		{
			get
			{
				return SR.GetResourceString("InvalidCast_FromTo");
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000F1197 File Offset: 0x000F0397
		internal static string InvalidCast_IConvertible
		{
			get
			{
				return SR.GetResourceString("InvalidCast_IConvertible");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000F11A3 File Offset: 0x000F03A3
		internal static string InvalidOperation_AsyncFlowCtrlCtxMismatch
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch");
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x000F11AF File Offset: 0x000F03AF
		internal static string InvalidOperation_AsyncIOInProgress
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_AsyncIOInProgress");
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x000F11BB File Offset: 0x000F03BB
		internal static string InvalidOperation_BadEmptyMethodBody
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadEmptyMethodBody");
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000F11C7 File Offset: 0x000F03C7
		internal static string InvalidOperation_BadILGeneratorUsage
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadILGeneratorUsage");
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x000F11D3 File Offset: 0x000F03D3
		internal static string InvalidOperation_BadInstructionOrIndexOutOfBound
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound");
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000F11DF File Offset: 0x000F03DF
		internal static string InvalidOperation_BadInterfaceNotAbstract
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadInterfaceNotAbstract");
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000F11EB File Offset: 0x000F03EB
		internal static string InvalidOperation_BadMethodBody
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadMethodBody");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000F11F7 File Offset: 0x000F03F7
		internal static string InvalidOperation_BadTypeAttributesNotAbstract
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_BadTypeAttributesNotAbstract");
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000F1203 File Offset: 0x000F0403
		internal static string InvalidOperation_CalledTwice
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CalledTwice");
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000F120F File Offset: 0x000F040F
		internal static string InvalidOperation_CannotImportGlobalFromDifferentModule
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000F121B File Offset: 0x000F041B
		internal static string InvalidOperation_CannotRegisterSecondResolver
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotRegisterSecondResolver");
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000F1227 File Offset: 0x000F0427
		internal static string InvalidOperation_CannotRestoreUnsupressedFlow
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow");
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x000F1233 File Offset: 0x000F0433
		internal static string InvalidOperation_CannotSupressFlowMultipleTimes
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes");
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x000F123F File Offset: 0x000F043F
		internal static string InvalidOperation_CannotUseAFCMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotUseAFCMultiple");
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x000F124B File Offset: 0x000F044B
		internal static string InvalidOperation_CannotUseAFCOtherThread
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CannotUseAFCOtherThread");
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x000F1257 File Offset: 0x000F0457
		internal static string InvalidOperation_CollectionCorrupted
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_CollectionCorrupted");
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x000F1263 File Offset: 0x000F0463
		internal static string InvalidOperation_ComputerName
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ComputerName");
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000F126F File Offset: 0x000F046F
		internal static string InvalidOperation_ConcurrentOperationsNotSupported
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ConcurrentOperationsNotSupported");
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x000F127B File Offset: 0x000F047B
		internal static string InvalidOperation_ConstructorNotAllowedOnInterface
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface");
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x000F1287 File Offset: 0x000F0487
		internal static string InvalidOperation_DateTimeParsing
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_DateTimeParsing");
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x000F1293 File Offset: 0x000F0493
		internal static string InvalidOperation_DefaultConstructorILGen
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_DefaultConstructorILGen");
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x000F129F File Offset: 0x000F049F
		internal static string InvalidOperation_EndReadCalledMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EndReadCalledMultiple");
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x000F12AB File Offset: 0x000F04AB
		internal static string InvalidOperation_EndWriteCalledMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EndWriteCalledMultiple");
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x000F12B7 File Offset: 0x000F04B7
		internal static string InvalidOperation_EnumEnded
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumEnded");
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x000F12C3 File Offset: 0x000F04C3
		internal static string InvalidOperation_EnumFailedVersion
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumFailedVersion");
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x000F12CF File Offset: 0x000F04CF
		internal static string InvalidOperation_EnumNotStarted
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumNotStarted");
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000F12DB File Offset: 0x000F04DB
		internal static string InvalidOperation_EnumOpCantHappen
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumOpCantHappen");
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x000F12E7 File Offset: 0x000F04E7
		internal static string InvalidOperation_EventInfoNotAvailable
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EventInfoNotAvailable");
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x000F12F3 File Offset: 0x000F04F3
		internal static string InvalidOperation_GenericParametersAlreadySet
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_GenericParametersAlreadySet");
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x000F12FF File Offset: 0x000F04FF
		internal static string InvalidOperation_GetVersion
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_GetVersion");
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x000F130B File Offset: 0x000F050B
		internal static string InvalidOperation_GlobalsHaveBeenCreated
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated");
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x000F1317 File Offset: 0x000F0517
		internal static string InvalidOperation_HandleIsNotInitialized
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_HandleIsNotInitialized");
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x000F1323 File Offset: 0x000F0523
		internal static string InvalidOperation_HandleIsNotPinned
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_HandleIsNotPinned");
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x000F132F File Offset: 0x000F052F
		internal static string InvalidOperation_HashInsertFailed
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_HashInsertFailed");
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x000F133B File Offset: 0x000F053B
		internal static string InvalidOperation_IComparerFailed
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_IComparerFailed");
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000F1347 File Offset: 0x000F0547
		internal static string InvalidOperation_MethodBaked
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MethodBaked");
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x000F1353 File Offset: 0x000F0553
		internal static string InvalidOperation_MethodBuilderBaked
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MethodBuilderBaked");
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000F135F File Offset: 0x000F055F
		internal static string InvalidOperation_MethodHasBody
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MethodHasBody");
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x000F136B File Offset: 0x000F056B
		internal static string InvalidOperation_MustCallInitialize
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MustCallInitialize");
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x000F1377 File Offset: 0x000F0577
		internal static string InvalidOperation_NativeOverlappedReused
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NativeOverlappedReused");
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x000F1383 File Offset: 0x000F0583
		internal static string InvalidOperation_NoMultiModuleAssembly
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NoMultiModuleAssembly");
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x000F138F File Offset: 0x000F058F
		internal static string InvalidOperation_NoPublicAddMethod
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NoPublicAddMethod");
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x000F139B File Offset: 0x000F059B
		internal static string InvalidOperation_NoPublicRemoveMethod
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NoPublicRemoveMethod");
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x000F13A7 File Offset: 0x000F05A7
		internal static string InvalidOperation_NotADebugModule
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NotADebugModule");
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000F13B3 File Offset: 0x000F05B3
		internal static string InvalidOperation_NotAllowedInDynamicMethod
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod");
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000F13BF File Offset: 0x000F05BF
		internal static string InvalidOperation_NotAVarArgCallingConvention
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NotAVarArgCallingConvention");
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000F13CB File Offset: 0x000F05CB
		internal static string InvalidOperation_NotGenericType
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NotGenericType");
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000F13D7 File Offset: 0x000F05D7
		internal static string InvalidOperation_NotWithConcurrentGC
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NotWithConcurrentGC");
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000F13E3 File Offset: 0x000F05E3
		internal static string InvalidOperation_NoUnderlyingTypeOnEnum
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NoUnderlyingTypeOnEnum");
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000F13EF File Offset: 0x000F05EF
		internal static string InvalidOperation_NoValue
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NoValue");
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x000F13FB File Offset: 0x000F05FB
		internal static string InvalidOperation_NullArray
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NullArray");
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000F1407 File Offset: 0x000F0607
		internal static string InvalidOperation_NullContext
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NullContext");
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x000F1413 File Offset: 0x000F0613
		internal static string InvalidOperation_NullModuleHandle
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NullModuleHandle");
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x000F141F File Offset: 0x000F061F
		internal static string InvalidOperation_OpenLocalVariableScope
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_OpenLocalVariableScope");
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x000F142B File Offset: 0x000F062B
		internal static string InvalidOperation_Overlapped_Pack
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_Overlapped_Pack");
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x000F1437 File Offset: 0x000F0637
		internal static string InvalidOperation_PropertyInfoNotAvailable
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_PropertyInfoNotAvailable");
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x000F1443 File Offset: 0x000F0643
		internal static string InvalidOperation_ReadOnly
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ReadOnly");
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000F144F File Offset: 0x000F064F
		internal static string InvalidOperation_ResMgrBadResSet_Type
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ResMgrBadResSet_Type");
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x000F145B File Offset: 0x000F065B
		internal static string InvalidOperation_ResourceNotStream_Name
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ResourceNotStream_Name");
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000F1467 File Offset: 0x000F0667
		internal static string InvalidOperation_ResourceNotString_Name
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ResourceNotString_Name");
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000F1473 File Offset: 0x000F0673
		internal static string InvalidOperation_ResourceNotString_Type
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ResourceNotString_Type");
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x000F147F File Offset: 0x000F067F
		internal static string InvalidOperation_SetLatencyModeNoGC
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_SetLatencyModeNoGC");
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x000F148B File Offset: 0x000F068B
		internal static string InvalidOperation_ShouldNotHaveMethodBody
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody");
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x000F1497 File Offset: 0x000F0697
		internal static string InvalidOperation_ThreadWrongThreadStart
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ThreadWrongThreadStart");
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x000F14A3 File Offset: 0x000F06A3
		internal static string InvalidOperation_TimeoutsNotSupported
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_TimeoutsNotSupported");
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x000F14AF File Offset: 0x000F06AF
		internal static string InvalidOperation_TimerAlreadyClosed
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_TimerAlreadyClosed");
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x000F14BB File Offset: 0x000F06BB
		internal static string InvalidOperation_TypeHasBeenCreated
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_TypeHasBeenCreated");
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x000F14C7 File Offset: 0x000F06C7
		internal static string InvalidOperation_TypeNotCreated
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_TypeNotCreated");
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000F14D3 File Offset: 0x000F06D3
		internal static string InvalidOperation_UnderlyingArrayListChanged
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_UnderlyingArrayListChanged");
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x000F14DF File Offset: 0x000F06DF
		internal static string InvalidOperation_UnknownEnumType
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_UnknownEnumType");
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000F14EB File Offset: 0x000F06EB
		internal static string InvalidOperation_WriteOnce
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_WriteOnce");
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000F14F7 File Offset: 0x000F06F7
		internal static string InvalidOperation_WrongAsyncResultOrEndCalledMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple");
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x000F1503 File Offset: 0x000F0703
		internal static string InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple");
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000F150F File Offset: 0x000F070F
		internal static string InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple");
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x000F151B File Offset: 0x000F071B
		internal static string InvalidProgram_Default
		{
			get
			{
				return SR.GetResourceString("InvalidProgram_Default");
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x000F1527 File Offset: 0x000F0727
		internal static string InvalidTimeZone_InvalidRegistryData
		{
			get
			{
				return SR.GetResourceString("InvalidTimeZone_InvalidRegistryData");
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x000F1533 File Offset: 0x000F0733
		internal static string InvariantFailed
		{
			get
			{
				return SR.GetResourceString("InvariantFailed");
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000F153F File Offset: 0x000F073F
		internal static string InvariantFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("InvariantFailed_Cnd");
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000F154B File Offset: 0x000F074B
		internal static string IO_NoFileTableInInMemoryAssemblies
		{
			get
			{
				return SR.GetResourceString("IO_NoFileTableInInMemoryAssemblies");
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000F1557 File Offset: 0x000F0757
		internal static string IO_EOF_ReadBeyondEOF
		{
			get
			{
				return SR.GetResourceString("IO_EOF_ReadBeyondEOF");
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000F1563 File Offset: 0x000F0763
		internal static string IO_FileLoad
		{
			get
			{
				return SR.GetResourceString("IO_FileLoad");
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000F156F File Offset: 0x000F076F
		internal static string IO_FileName_Name
		{
			get
			{
				return SR.GetResourceString("IO_FileName_Name");
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000F157B File Offset: 0x000F077B
		internal static string IO_FileNotFound
		{
			get
			{
				return SR.GetResourceString("IO_FileNotFound");
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000F1587 File Offset: 0x000F0787
		internal static string IO_FileNotFound_FileName
		{
			get
			{
				return SR.GetResourceString("IO_FileNotFound_FileName");
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000F1593 File Offset: 0x000F0793
		internal static string IO_AlreadyExists_Name
		{
			get
			{
				return SR.GetResourceString("IO_AlreadyExists_Name");
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x000F159F File Offset: 0x000F079F
		internal static string IO_BindHandleFailed
		{
			get
			{
				return SR.GetResourceString("IO_BindHandleFailed");
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x000F15AB File Offset: 0x000F07AB
		internal static string IO_FileExists_Name
		{
			get
			{
				return SR.GetResourceString("IO_FileExists_Name");
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000F15B7 File Offset: 0x000F07B7
		internal static string IO_FileStreamHandlePosition
		{
			get
			{
				return SR.GetResourceString("IO_FileStreamHandlePosition");
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x000F15C3 File Offset: 0x000F07C3
		internal static string IO_FileTooLongOrHandleNotSync
		{
			get
			{
				return SR.GetResourceString("IO_FileTooLongOrHandleNotSync");
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000F15CF File Offset: 0x000F07CF
		internal static string IO_FixedCapacity
		{
			get
			{
				return SR.GetResourceString("IO_FixedCapacity");
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000F15DB File Offset: 0x000F07DB
		internal static string IO_InvalidStringLen_Len
		{
			get
			{
				return SR.GetResourceString("IO_InvalidStringLen_Len");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000F15E7 File Offset: 0x000F07E7
		internal static string IO_SeekAppendOverwrite
		{
			get
			{
				return SR.GetResourceString("IO_SeekAppendOverwrite");
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000F15F3 File Offset: 0x000F07F3
		internal static string IO_SeekBeforeBegin
		{
			get
			{
				return SR.GetResourceString("IO_SeekBeforeBegin");
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000F15FF File Offset: 0x000F07FF
		internal static string IO_SetLengthAppendTruncate
		{
			get
			{
				return SR.GetResourceString("IO_SetLengthAppendTruncate");
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000F160B File Offset: 0x000F080B
		internal static string IO_SharingViolation_File
		{
			get
			{
				return SR.GetResourceString("IO_SharingViolation_File");
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000F1617 File Offset: 0x000F0817
		internal static string IO_SharingViolation_NoFileName
		{
			get
			{
				return SR.GetResourceString("IO_SharingViolation_NoFileName");
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000F1623 File Offset: 0x000F0823
		internal static string IO_StreamTooLong
		{
			get
			{
				return SR.GetResourceString("IO_StreamTooLong");
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000F162F File Offset: 0x000F082F
		internal static string IO_PathNotFound_NoPathName
		{
			get
			{
				return SR.GetResourceString("IO_PathNotFound_NoPathName");
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x000F163B File Offset: 0x000F083B
		internal static string IO_PathNotFound_Path
		{
			get
			{
				return SR.GetResourceString("IO_PathNotFound_Path");
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000F1647 File Offset: 0x000F0847
		internal static string IO_PathTooLong
		{
			get
			{
				return SR.GetResourceString("IO_PathTooLong");
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000F1653 File Offset: 0x000F0853
		internal static string IO_PathTooLong_Path
		{
			get
			{
				return SR.GetResourceString("IO_PathTooLong_Path");
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000F165F File Offset: 0x000F085F
		internal static string IO_UnknownFileName
		{
			get
			{
				return SR.GetResourceString("IO_UnknownFileName");
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000F166B File Offset: 0x000F086B
		internal static string Lazy_CreateValue_NoParameterlessCtorForT
		{
			get
			{
				return SR.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT");
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000F1677 File Offset: 0x000F0877
		internal static string Lazy_ctor_ModeInvalid
		{
			get
			{
				return SR.GetResourceString("Lazy_ctor_ModeInvalid");
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000F1683 File Offset: 0x000F0883
		internal static string Lazy_StaticInit_InvalidOperation
		{
			get
			{
				return SR.GetResourceString("Lazy_StaticInit_InvalidOperation");
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000F168F File Offset: 0x000F088F
		internal static string Lazy_ToString_ValueNotCreated
		{
			get
			{
				return SR.GetResourceString("Lazy_ToString_ValueNotCreated");
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000F169B File Offset: 0x000F089B
		internal static string Lazy_Value_RecursiveCallsToValue
		{
			get
			{
				return SR.GetResourceString("Lazy_Value_RecursiveCallsToValue");
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000F16A7 File Offset: 0x000F08A7
		internal static string ManualResetEventSlim_ctor_SpinCountOutOfRange
		{
			get
			{
				return SR.GetResourceString("ManualResetEventSlim_ctor_SpinCountOutOfRange");
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000F16B3 File Offset: 0x000F08B3
		internal static string ManualResetEventSlim_ctor_TooManyWaiters
		{
			get
			{
				return SR.GetResourceString("ManualResetEventSlim_ctor_TooManyWaiters");
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000F16BF File Offset: 0x000F08BF
		internal static string ManualResetEventSlim_Disposed
		{
			get
			{
				return SR.GetResourceString("ManualResetEventSlim_Disposed");
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000F16CB File Offset: 0x000F08CB
		internal static string Marshaler_StringTooLong
		{
			get
			{
				return SR.GetResourceString("Marshaler_StringTooLong");
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000F16D7 File Offset: 0x000F08D7
		internal static string MissingConstructor_Name
		{
			get
			{
				return SR.GetResourceString("MissingConstructor_Name");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000F16E3 File Offset: 0x000F08E3
		internal static string MissingField
		{
			get
			{
				return SR.GetResourceString("MissingField");
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000F16EF File Offset: 0x000F08EF
		internal static string MissingField_Name
		{
			get
			{
				return SR.GetResourceString("MissingField_Name");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x000F16FB File Offset: 0x000F08FB
		internal static string MissingManifestResource_MultipleBlobs
		{
			get
			{
				return SR.GetResourceString("MissingManifestResource_MultipleBlobs");
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000F1707 File Offset: 0x000F0907
		internal static string MissingManifestResource_NoNeutralAsm
		{
			get
			{
				return SR.GetResourceString("MissingManifestResource_NoNeutralAsm");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000F1713 File Offset: 0x000F0913
		internal static string MissingManifestResource_NoNeutralDisk
		{
			get
			{
				return SR.GetResourceString("MissingManifestResource_NoNeutralDisk");
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000F171F File Offset: 0x000F091F
		internal static string MissingMember
		{
			get
			{
				return SR.GetResourceString("MissingMember");
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000F172B File Offset: 0x000F092B
		internal static string MissingMember_Name
		{
			get
			{
				return SR.GetResourceString("MissingMember_Name");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000F1737 File Offset: 0x000F0937
		internal static string MissingMemberNestErr
		{
			get
			{
				return SR.GetResourceString("MissingMemberNestErr");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x000F1743 File Offset: 0x000F0943
		internal static string MissingMemberTypeRef
		{
			get
			{
				return SR.GetResourceString("MissingMemberTypeRef");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000F174F File Offset: 0x000F094F
		internal static string MissingMethod_Name
		{
			get
			{
				return SR.GetResourceString("MissingMethod_Name");
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x000F175B File Offset: 0x000F095B
		internal static string MissingSatelliteAssembly_Culture_Name
		{
			get
			{
				return SR.GetResourceString("MissingSatelliteAssembly_Culture_Name");
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000F1767 File Offset: 0x000F0967
		internal static string MissingSatelliteAssembly_Default
		{
			get
			{
				return SR.GetResourceString("MissingSatelliteAssembly_Default");
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000F1773 File Offset: 0x000F0973
		internal static string Multicast_Combine
		{
			get
			{
				return SR.GetResourceString("Multicast_Combine");
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000F177F File Offset: 0x000F097F
		internal static string MustUseCCRewrite
		{
			get
			{
				return SR.GetResourceString("MustUseCCRewrite");
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000F178B File Offset: 0x000F098B
		internal static string NotSupported_AbstractNonCLS
		{
			get
			{
				return SR.GetResourceString("NotSupported_AbstractNonCLS");
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000F1797 File Offset: 0x000F0997
		internal static string NotSupported_ActivAttr
		{
			get
			{
				return SR.GetResourceString("NotSupported_ActivAttr");
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000F17A3 File Offset: 0x000F09A3
		internal static string NotSupported_AssemblyLoadFromHash
		{
			get
			{
				return SR.GetResourceString("NotSupported_AssemblyLoadFromHash");
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000F17AF File Offset: 0x000F09AF
		internal static string NotSupported_ByRefToByRefLikeReturn
		{
			get
			{
				return SR.GetResourceString("NotSupported_ByRefToByRefLikeReturn");
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000F17BB File Offset: 0x000F09BB
		internal static string NotSupported_ByRefToVoidReturn
		{
			get
			{
				return SR.GetResourceString("NotSupported_ByRefToVoidReturn");
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000F17C7 File Offset: 0x000F09C7
		internal static string NotSupported_CallToVarArg
		{
			get
			{
				return SR.GetResourceString("NotSupported_CallToVarArg");
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000F17D3 File Offset: 0x000F09D3
		internal static string NotSupported_CannotCallEqualsOnSpan
		{
			get
			{
				return SR.GetResourceString("NotSupported_CannotCallEqualsOnSpan");
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000F17DF File Offset: 0x000F09DF
		internal static string NotSupported_CannotCallGetHashCodeOnSpan
		{
			get
			{
				return SR.GetResourceString("NotSupported_CannotCallGetHashCodeOnSpan");
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x000F17EB File Offset: 0x000F09EB
		internal static string NotSupported_ChangeType
		{
			get
			{
				return SR.GetResourceString("NotSupported_ChangeType");
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000F17F7 File Offset: 0x000F09F7
		internal static string NotSupported_CreateInstanceWithTypeBuilder
		{
			get
			{
				return SR.GetResourceString("NotSupported_CreateInstanceWithTypeBuilder");
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000F1803 File Offset: 0x000F0A03
		internal static string NotSupported_DBNullSerial
		{
			get
			{
				return SR.GetResourceString("NotSupported_DBNullSerial");
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000F180F File Offset: 0x000F0A0F
		internal static string NotSupported_DynamicAssembly
		{
			get
			{
				return SR.GetResourceString("NotSupported_DynamicAssembly");
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x000F181B File Offset: 0x000F0A1B
		internal static string NotSupported_DynamicMethodFlags
		{
			get
			{
				return SR.GetResourceString("NotSupported_DynamicMethodFlags");
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000F1827 File Offset: 0x000F0A27
		internal static string NotSupported_DynamicModule
		{
			get
			{
				return SR.GetResourceString("NotSupported_DynamicModule");
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x000F1833 File Offset: 0x000F0A33
		internal static string NotSupported_FileStreamOnNonFiles
		{
			get
			{
				return SR.GetResourceString("NotSupported_FileStreamOnNonFiles");
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000F183F File Offset: 0x000F0A3F
		internal static string NotSupported_FixedSizeCollection
		{
			get
			{
				return SR.GetResourceString("NotSupported_FixedSizeCollection");
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x000F184B File Offset: 0x000F0A4B
		internal static string InvalidOperation_SpanOverlappedOperation
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_SpanOverlappedOperation");
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000F1857 File Offset: 0x000F0A57
		internal static string NotSupported_IllegalOneByteBranch
		{
			get
			{
				return SR.GetResourceString("NotSupported_IllegalOneByteBranch");
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x000F1863 File Offset: 0x000F0A63
		internal static string NotSupported_KeyCollectionSet
		{
			get
			{
				return SR.GetResourceString("NotSupported_KeyCollectionSet");
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000F186F File Offset: 0x000F0A6F
		internal static string NotSupported_MaxWaitHandles
		{
			get
			{
				return SR.GetResourceString("NotSupported_MaxWaitHandles");
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000F187B File Offset: 0x000F0A7B
		internal static string NotSupported_MemStreamNotExpandable
		{
			get
			{
				return SR.GetResourceString("NotSupported_MemStreamNotExpandable");
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x000F1887 File Offset: 0x000F0A87
		internal static string NotSupported_MustBeModuleBuilder
		{
			get
			{
				return SR.GetResourceString("NotSupported_MustBeModuleBuilder");
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000F1893 File Offset: 0x000F0A93
		internal static string NotSupported_NoCodepageData
		{
			get
			{
				return SR.GetResourceString("NotSupported_NoCodepageData");
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000F189F File Offset: 0x000F0A9F
		internal static string InvalidOperation_FunctionMissingUnmanagedCallersOnly
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_FunctionMissingUnmanagedCallersOnly");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000F18AB File Offset: 0x000F0AAB
		internal static string NotSupported_NonReflectedType
		{
			get
			{
				return SR.GetResourceString("NotSupported_NonReflectedType");
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x000F18B7 File Offset: 0x000F0AB7
		internal static string NotSupported_NoParentDefaultConstructor
		{
			get
			{
				return SR.GetResourceString("NotSupported_NoParentDefaultConstructor");
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x000F18C3 File Offset: 0x000F0AC3
		internal static string NotSupported_NoTypeInfo
		{
			get
			{
				return SR.GetResourceString("NotSupported_NoTypeInfo");
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x000F18CF File Offset: 0x000F0ACF
		internal static string NotSupported_NYI
		{
			get
			{
				return SR.GetResourceString("NotSupported_NYI");
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000F18DB File Offset: 0x000F0ADB
		internal static string NotSupported_ObsoleteResourcesFile
		{
			get
			{
				return SR.GetResourceString("NotSupported_ObsoleteResourcesFile");
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x000F18E7 File Offset: 0x000F0AE7
		internal static string NotSupported_OutputStreamUsingTypeBuilder
		{
			get
			{
				return SR.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder");
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x000F18F3 File Offset: 0x000F0AF3
		internal static string NotSupported_RangeCollection
		{
			get
			{
				return SR.GetResourceString("NotSupported_RangeCollection");
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000F18FF File Offset: 0x000F0AFF
		internal static string NotSupported_Reading
		{
			get
			{
				return SR.GetResourceString("NotSupported_Reading");
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000F190B File Offset: 0x000F0B0B
		internal static string NotSupported_ReadOnlyCollection
		{
			get
			{
				return SR.GetResourceString("NotSupported_ReadOnlyCollection");
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000F1917 File Offset: 0x000F0B17
		internal static string NotSupported_ResourceObjectSerialization
		{
			get
			{
				return SR.GetResourceString("NotSupported_ResourceObjectSerialization");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x000F1923 File Offset: 0x000F0B23
		internal static string NotSupported_StringComparison
		{
			get
			{
				return SR.GetResourceString("NotSupported_StringComparison");
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x000F192F File Offset: 0x000F0B2F
		internal static string NotSupported_SubclassOverride
		{
			get
			{
				return SR.GetResourceString("NotSupported_SubclassOverride");
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x000F193B File Offset: 0x000F0B3B
		internal static string NotSupported_SymbolMethod
		{
			get
			{
				return SR.GetResourceString("NotSupported_SymbolMethod");
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x000F1947 File Offset: 0x000F0B47
		internal static string NotSupported_Type
		{
			get
			{
				return SR.GetResourceString("NotSupported_Type");
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x000F1953 File Offset: 0x000F0B53
		internal static string NotSupported_TypeNotYetCreated
		{
			get
			{
				return SR.GetResourceString("NotSupported_TypeNotYetCreated");
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x000F195F File Offset: 0x000F0B5F
		internal static string NotSupported_UmsSafeBuffer
		{
			get
			{
				return SR.GetResourceString("NotSupported_UmsSafeBuffer");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x000F196B File Offset: 0x000F0B6B
		internal static string NotSupported_UnitySerHolder
		{
			get
			{
				return SR.GetResourceString("NotSupported_UnitySerHolder");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x000F1977 File Offset: 0x000F0B77
		internal static string NotSupported_UnknownTypeCode
		{
			get
			{
				return SR.GetResourceString("NotSupported_UnknownTypeCode");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000F1983 File Offset: 0x000F0B83
		internal static string NotSupported_UnreadableStream
		{
			get
			{
				return SR.GetResourceString("NotSupported_UnreadableStream");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x000F198F File Offset: 0x000F0B8F
		internal static string NotSupported_UnseekableStream
		{
			get
			{
				return SR.GetResourceString("NotSupported_UnseekableStream");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x000F199B File Offset: 0x000F0B9B
		internal static string NotSupported_UnwritableStream
		{
			get
			{
				return SR.GetResourceString("NotSupported_UnwritableStream");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000F19A7 File Offset: 0x000F0BA7
		internal static string NotSupported_ValueCollectionSet
		{
			get
			{
				return SR.GetResourceString("NotSupported_ValueCollectionSet");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000F19B3 File Offset: 0x000F0BB3
		internal static string NotSupported_Writing
		{
			get
			{
				return SR.GetResourceString("NotSupported_Writing");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000F19BF File Offset: 0x000F0BBF
		internal static string NotSupported_WrongResourceReader_Type
		{
			get
			{
				return SR.GetResourceString("NotSupported_WrongResourceReader_Type");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x000F19CB File Offset: 0x000F0BCB
		internal static string ObjectDisposed_FileClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_FileClosed");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000F19D7 File Offset: 0x000F0BD7
		internal static string ObjectDisposed_Generic
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_Generic");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000F19E3 File Offset: 0x000F0BE3
		internal static string ObjectDisposed_ObjectName_Name
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_ObjectName_Name");
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000F19EF File Offset: 0x000F0BEF
		internal static string ObjectDisposed_WriterClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_WriterClosed");
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000F19FB File Offset: 0x000F0BFB
		internal static string ObjectDisposed_ReaderClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_ReaderClosed");
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x000F1A07 File Offset: 0x000F0C07
		internal static string ObjectDisposed_ResourceSet
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_ResourceSet");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x000F1A13 File Offset: 0x000F0C13
		internal static string ObjectDisposed_StreamClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_StreamClosed");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x000F1A1F File Offset: 0x000F0C1F
		internal static string ObjectDisposed_ViewAccessorClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_ViewAccessorClosed");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000F1A2B File Offset: 0x000F0C2B
		internal static string ObjectDisposed_SafeHandleClosed
		{
			get
			{
				return SR.GetResourceString("ObjectDisposed_SafeHandleClosed");
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000F1A37 File Offset: 0x000F0C37
		internal static string OperationCanceled
		{
			get
			{
				return SR.GetResourceString("OperationCanceled");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000F1A43 File Offset: 0x000F0C43
		internal static string Overflow_Byte
		{
			get
			{
				return SR.GetResourceString("Overflow_Byte");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x000F1A4F File Offset: 0x000F0C4F
		internal static string Overflow_Char
		{
			get
			{
				return SR.GetResourceString("Overflow_Char");
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x000F1A5B File Offset: 0x000F0C5B
		internal static string Overflow_Currency
		{
			get
			{
				return SR.GetResourceString("Overflow_Currency");
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000F1A67 File Offset: 0x000F0C67
		internal static string Overflow_Decimal
		{
			get
			{
				return SR.GetResourceString("Overflow_Decimal");
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000F1A73 File Offset: 0x000F0C73
		internal static string Overflow_Duration
		{
			get
			{
				return SR.GetResourceString("Overflow_Duration");
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x000F1A7F File Offset: 0x000F0C7F
		internal static string Overflow_Int16
		{
			get
			{
				return SR.GetResourceString("Overflow_Int16");
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000F1A8B File Offset: 0x000F0C8B
		internal static string Overflow_Int32
		{
			get
			{
				return SR.GetResourceString("Overflow_Int32");
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x000F1A97 File Offset: 0x000F0C97
		internal static string Overflow_Int64
		{
			get
			{
				return SR.GetResourceString("Overflow_Int64");
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x000F1AA3 File Offset: 0x000F0CA3
		internal static string Overflow_NegateTwosCompNum
		{
			get
			{
				return SR.GetResourceString("Overflow_NegateTwosCompNum");
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x000F1AAF File Offset: 0x000F0CAF
		internal static string Overflow_NegativeUnsigned
		{
			get
			{
				return SR.GetResourceString("Overflow_NegativeUnsigned");
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x000F1ABB File Offset: 0x000F0CBB
		internal static string Overflow_SByte
		{
			get
			{
				return SR.GetResourceString("Overflow_SByte");
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x000F1AC7 File Offset: 0x000F0CC7
		internal static string Overflow_TimeSpanElementTooLarge
		{
			get
			{
				return SR.GetResourceString("Overflow_TimeSpanElementTooLarge");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x000F1AD3 File Offset: 0x000F0CD3
		internal static string Overflow_TimeSpanTooLong
		{
			get
			{
				return SR.GetResourceString("Overflow_TimeSpanTooLong");
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x000F1ADF File Offset: 0x000F0CDF
		internal static string Overflow_UInt16
		{
			get
			{
				return SR.GetResourceString("Overflow_UInt16");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x000F1AEB File Offset: 0x000F0CEB
		internal static string Overflow_UInt32
		{
			get
			{
				return SR.GetResourceString("Overflow_UInt32");
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x000F1AF7 File Offset: 0x000F0CF7
		internal static string Overflow_UInt64
		{
			get
			{
				return SR.GetResourceString("Overflow_UInt64");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x000F1B03 File Offset: 0x000F0D03
		internal static string PlatformNotSupported_ReflectionOnly
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_ReflectionOnly");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x000F1B0F File Offset: 0x000F0D0F
		internal static string PlatformNotSupported_Remoting
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_Remoting");
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x000F1B1B File Offset: 0x000F0D1B
		internal static string PlatformNotSupported_SecureBinarySerialization
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_SecureBinarySerialization");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x000F1B27 File Offset: 0x000F0D27
		internal static string PlatformNotSupported_StrongNameSigning
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_StrongNameSigning");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x000F1B33 File Offset: 0x000F0D33
		internal static string PlatformNotSupported_ITypeInfo
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_ITypeInfo");
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x000F1B3F File Offset: 0x000F0D3F
		internal static string PlatformNotSupported_IExpando
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_IExpando");
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x000F1B4B File Offset: 0x000F0D4B
		internal static string PlatformNotSupported_AppDomains
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_AppDomains");
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x000F1B57 File Offset: 0x000F0D57
		internal static string PlatformNotSupported_CAS
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_CAS");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x000F1B63 File Offset: 0x000F0D63
		internal static string PlatformNotSupported_Principal
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_Principal");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x000F1B6F File Offset: 0x000F0D6F
		internal static string PlatformNotSupported_ThreadAbort
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_ThreadAbort");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x000F1B7B File Offset: 0x000F0D7B
		internal static string PlatformNotSupported_ThreadSuspend
		{
			get
			{
				return SR.GetResourceString("PlatformNotSupported_ThreadSuspend");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x000F1B87 File Offset: 0x000F0D87
		internal static string PostconditionFailed
		{
			get
			{
				return SR.GetResourceString("PostconditionFailed");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000F1B93 File Offset: 0x000F0D93
		internal static string PostconditionFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("PostconditionFailed_Cnd");
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000F1B9F File Offset: 0x000F0D9F
		internal static string PostconditionOnExceptionFailed
		{
			get
			{
				return SR.GetResourceString("PostconditionOnExceptionFailed");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000F1BAB File Offset: 0x000F0DAB
		internal static string PostconditionOnExceptionFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("PostconditionOnExceptionFailed_Cnd");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x000F1BB7 File Offset: 0x000F0DB7
		internal static string PreconditionFailed
		{
			get
			{
				return SR.GetResourceString("PreconditionFailed");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000F1BC3 File Offset: 0x000F0DC3
		internal static string PreconditionFailed_Cnd
		{
			get
			{
				return SR.GetResourceString("PreconditionFailed_Cnd");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x000F1BCF File Offset: 0x000F0DCF
		internal static string Rank_MultiDimNotSupported
		{
			get
			{
				return SR.GetResourceString("Rank_MultiDimNotSupported");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000F1BDB File Offset: 0x000F0DDB
		internal static string Rank_MustMatch
		{
			get
			{
				return SR.GetResourceString("Rank_MustMatch");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000F1BE7 File Offset: 0x000F0DE7
		internal static string ResourceReaderIsClosed
		{
			get
			{
				return SR.GetResourceString("ResourceReaderIsClosed");
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x000F1BF3 File Offset: 0x000F0DF3
		internal static string Resources_StreamNotValid
		{
			get
			{
				return SR.GetResourceString("Resources_StreamNotValid");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x000F1BFF File Offset: 0x000F0DFF
		internal static string RFLCT_AmbigCust
		{
			get
			{
				return SR.GetResourceString("RFLCT_AmbigCust");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000F1C0B File Offset: 0x000F0E0B
		internal static string RFLCT_Ambiguous
		{
			get
			{
				return SR.GetResourceString("RFLCT_Ambiguous");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000F1C17 File Offset: 0x000F0E17
		internal static string InvalidFilterCriteriaException_CritInt
		{
			get
			{
				return SR.GetResourceString("InvalidFilterCriteriaException_CritInt");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000F1C23 File Offset: 0x000F0E23
		internal static string InvalidFilterCriteriaException_CritString
		{
			get
			{
				return SR.GetResourceString("InvalidFilterCriteriaException_CritString");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000F1C2F File Offset: 0x000F0E2F
		internal static string RFLCT_InvalidFieldFail
		{
			get
			{
				return SR.GetResourceString("RFLCT_InvalidFieldFail");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000F1C3B File Offset: 0x000F0E3B
		internal static string RFLCT_InvalidPropFail
		{
			get
			{
				return SR.GetResourceString("RFLCT_InvalidPropFail");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000F1C47 File Offset: 0x000F0E47
		internal static string RFLCT_Targ_ITargMismatch
		{
			get
			{
				return SR.GetResourceString("RFLCT_Targ_ITargMismatch");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000F1C53 File Offset: 0x000F0E53
		internal static string RFLCT_Targ_StatFldReqTarg
		{
			get
			{
				return SR.GetResourceString("RFLCT_Targ_StatFldReqTarg");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000F1C5F File Offset: 0x000F0E5F
		internal static string RFLCT_Targ_StatMethReqTarg
		{
			get
			{
				return SR.GetResourceString("RFLCT_Targ_StatMethReqTarg");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000F1C6B File Offset: 0x000F0E6B
		internal static string RuntimeWrappedException
		{
			get
			{
				return SR.GetResourceString("RuntimeWrappedException");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x000F1C77 File Offset: 0x000F0E77
		internal static string StandardOleMarshalObjectGetMarshalerFailed
		{
			get
			{
				return SR.GetResourceString("StandardOleMarshalObjectGetMarshalerFailed");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000F1C83 File Offset: 0x000F0E83
		internal static string Security_CannotReadRegistryData
		{
			get
			{
				return SR.GetResourceString("Security_CannotReadRegistryData");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000F1C8F File Offset: 0x000F0E8F
		internal static string Security_RegistryPermission
		{
			get
			{
				return SR.GetResourceString("Security_RegistryPermission");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000F1C9B File Offset: 0x000F0E9B
		internal static string SemaphoreSlim_ctor_InitialCountWrong
		{
			get
			{
				return SR.GetResourceString("SemaphoreSlim_ctor_InitialCountWrong");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000F1CA7 File Offset: 0x000F0EA7
		internal static string SemaphoreSlim_ctor_MaxCountWrong
		{
			get
			{
				return SR.GetResourceString("SemaphoreSlim_ctor_MaxCountWrong");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000F1CB3 File Offset: 0x000F0EB3
		internal static string SemaphoreSlim_Disposed
		{
			get
			{
				return SR.GetResourceString("SemaphoreSlim_Disposed");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x000F1CBF File Offset: 0x000F0EBF
		internal static string SemaphoreSlim_Release_CountWrong
		{
			get
			{
				return SR.GetResourceString("SemaphoreSlim_Release_CountWrong");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x000F1CCB File Offset: 0x000F0ECB
		internal static string SemaphoreSlim_Wait_TimeoutWrong
		{
			get
			{
				return SR.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x000F1CD7 File Offset: 0x000F0ED7
		internal static string Serialization_BadParameterInfo
		{
			get
			{
				return SR.GetResourceString("Serialization_BadParameterInfo");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000F1CE3 File Offset: 0x000F0EE3
		internal static string Serialization_CorruptField
		{
			get
			{
				return SR.GetResourceString("Serialization_CorruptField");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000F1CEF File Offset: 0x000F0EEF
		internal static string Serialization_DateTimeTicksOutOfRange
		{
			get
			{
				return SR.GetResourceString("Serialization_DateTimeTicksOutOfRange");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000F1CFB File Offset: 0x000F0EFB
		internal static string Serialization_DelegatesNotSupported
		{
			get
			{
				return SR.GetResourceString("Serialization_DelegatesNotSupported");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x000F1D07 File Offset: 0x000F0F07
		internal static string Serialization_InsufficientState
		{
			get
			{
				return SR.GetResourceString("Serialization_InsufficientState");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000F1D13 File Offset: 0x000F0F13
		internal static string Serialization_InvalidData
		{
			get
			{
				return SR.GetResourceString("Serialization_InvalidData");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x000F1D1F File Offset: 0x000F0F1F
		internal static string Serialization_InvalidEscapeSequence
		{
			get
			{
				return SR.GetResourceString("Serialization_InvalidEscapeSequence");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x000F1D2B File Offset: 0x000F0F2B
		internal static string Serialization_InvalidOnDeser
		{
			get
			{
				return SR.GetResourceString("Serialization_InvalidOnDeser");
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x000F1D37 File Offset: 0x000F0F37
		internal static string Serialization_InvalidType
		{
			get
			{
				return SR.GetResourceString("Serialization_InvalidType");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000F1D43 File Offset: 0x000F0F43
		internal static string Serialization_KeyValueDifferentSizes
		{
			get
			{
				return SR.GetResourceString("Serialization_KeyValueDifferentSizes");
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x000F1D4F File Offset: 0x000F0F4F
		internal static string Serialization_MissingDateTimeData
		{
			get
			{
				return SR.GetResourceString("Serialization_MissingDateTimeData");
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x000F1D5B File Offset: 0x000F0F5B
		internal static string Serialization_MissingKeys
		{
			get
			{
				return SR.GetResourceString("Serialization_MissingKeys");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x000F1D67 File Offset: 0x000F0F67
		internal static string Serialization_MissingValues
		{
			get
			{
				return SR.GetResourceString("Serialization_MissingValues");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x000F1D73 File Offset: 0x000F0F73
		internal static string Serialization_NoParameterInfo
		{
			get
			{
				return SR.GetResourceString("Serialization_NoParameterInfo");
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x000F1D7F File Offset: 0x000F0F7F
		internal static string Serialization_NotFound
		{
			get
			{
				return SR.GetResourceString("Serialization_NotFound");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x000F1D8B File Offset: 0x000F0F8B
		internal static string Serialization_NullKey
		{
			get
			{
				return SR.GetResourceString("Serialization_NullKey");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x000F1D97 File Offset: 0x000F0F97
		internal static string Serialization_OptionalFieldVersionValue
		{
			get
			{
				return SR.GetResourceString("Serialization_OptionalFieldVersionValue");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x000F1DA3 File Offset: 0x000F0FA3
		internal static string Serialization_SameNameTwice
		{
			get
			{
				return SR.GetResourceString("Serialization_SameNameTwice");
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x000F1DAF File Offset: 0x000F0FAF
		internal static string Serialization_StringBuilderCapacity
		{
			get
			{
				return SR.GetResourceString("Serialization_StringBuilderCapacity");
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x000F1DBB File Offset: 0x000F0FBB
		internal static string Serialization_StringBuilderMaxCapacity
		{
			get
			{
				return SR.GetResourceString("Serialization_StringBuilderMaxCapacity");
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x000F1DC7 File Offset: 0x000F0FC7
		internal static string SpinLock_Exit_SynchronizationLockException
		{
			get
			{
				return SR.GetResourceString("SpinLock_Exit_SynchronizationLockException");
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x000F1DD3 File Offset: 0x000F0FD3
		internal static string SpinLock_IsHeldByCurrentThread
		{
			get
			{
				return SR.GetResourceString("SpinLock_IsHeldByCurrentThread");
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x000F1DDF File Offset: 0x000F0FDF
		internal static string SpinLock_TryEnter_ArgumentOutOfRange
		{
			get
			{
				return SR.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange");
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x000F1DEB File Offset: 0x000F0FEB
		internal static string SpinLock_TryEnter_LockRecursionException
		{
			get
			{
				return SR.GetResourceString("SpinLock_TryEnter_LockRecursionException");
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x000F1DF7 File Offset: 0x000F0FF7
		internal static string SpinLock_TryReliableEnter_ArgumentException
		{
			get
			{
				return SR.GetResourceString("SpinLock_TryReliableEnter_ArgumentException");
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x000F1E03 File Offset: 0x000F1003
		internal static string SpinWait_SpinUntil_ArgumentNull
		{
			get
			{
				return SR.GetResourceString("SpinWait_SpinUntil_ArgumentNull");
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x000F1E0F File Offset: 0x000F100F
		internal static string SpinWait_SpinUntil_TimeoutWrong
		{
			get
			{
				return SR.GetResourceString("SpinWait_SpinUntil_TimeoutWrong");
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000F1E1B File Offset: 0x000F101B
		internal static string Task_ContinueWith_ESandLR
		{
			get
			{
				return SR.GetResourceString("Task_ContinueWith_ESandLR");
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x000F1E27 File Offset: 0x000F1027
		internal static string Task_ContinueWith_NotOnAnything
		{
			get
			{
				return SR.GetResourceString("Task_ContinueWith_NotOnAnything");
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000F1E33 File Offset: 0x000F1033
		internal static string Task_Delay_InvalidDelay
		{
			get
			{
				return SR.GetResourceString("Task_Delay_InvalidDelay");
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000F1E3F File Offset: 0x000F103F
		internal static string Task_Delay_InvalidMillisecondsDelay
		{
			get
			{
				return SR.GetResourceString("Task_Delay_InvalidMillisecondsDelay");
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000F1E4B File Offset: 0x000F104B
		internal static string Task_Dispose_NotCompleted
		{
			get
			{
				return SR.GetResourceString("Task_Dispose_NotCompleted");
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000F1E57 File Offset: 0x000F1057
		internal static string Task_FromAsync_LongRunning
		{
			get
			{
				return SR.GetResourceString("Task_FromAsync_LongRunning");
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000F1E63 File Offset: 0x000F1063
		internal static string Task_FromAsync_PreferFairness
		{
			get
			{
				return SR.GetResourceString("Task_FromAsync_PreferFairness");
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x000F1E6F File Offset: 0x000F106F
		internal static string Task_MultiTaskContinuation_EmptyTaskList
		{
			get
			{
				return SR.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList");
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000F1E7B File Offset: 0x000F107B
		internal static string Task_MultiTaskContinuation_FireOptions
		{
			get
			{
				return SR.GetResourceString("Task_MultiTaskContinuation_FireOptions");
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000F1E87 File Offset: 0x000F1087
		internal static string Task_MultiTaskContinuation_NullTask
		{
			get
			{
				return SR.GetResourceString("Task_MultiTaskContinuation_NullTask");
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x000F1E93 File Offset: 0x000F1093
		internal static string Task_RunSynchronously_AlreadyStarted
		{
			get
			{
				return SR.GetResourceString("Task_RunSynchronously_AlreadyStarted");
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x000F1E9F File Offset: 0x000F109F
		internal static string Task_RunSynchronously_Continuation
		{
			get
			{
				return SR.GetResourceString("Task_RunSynchronously_Continuation");
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x000F1EAB File Offset: 0x000F10AB
		internal static string Task_RunSynchronously_Promise
		{
			get
			{
				return SR.GetResourceString("Task_RunSynchronously_Promise");
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x000F1EB7 File Offset: 0x000F10B7
		internal static string Task_RunSynchronously_TaskCompleted
		{
			get
			{
				return SR.GetResourceString("Task_RunSynchronously_TaskCompleted");
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x000F1EC3 File Offset: 0x000F10C3
		internal static string Task_Start_AlreadyStarted
		{
			get
			{
				return SR.GetResourceString("Task_Start_AlreadyStarted");
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x000F1ECF File Offset: 0x000F10CF
		internal static string Task_Start_ContinuationTask
		{
			get
			{
				return SR.GetResourceString("Task_Start_ContinuationTask");
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x000F1EDB File Offset: 0x000F10DB
		internal static string Task_Start_Promise
		{
			get
			{
				return SR.GetResourceString("Task_Start_Promise");
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x000F1EE7 File Offset: 0x000F10E7
		internal static string Task_Start_TaskCompleted
		{
			get
			{
				return SR.GetResourceString("Task_Start_TaskCompleted");
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x000F1EF3 File Offset: 0x000F10F3
		internal static string Task_ThrowIfDisposed
		{
			get
			{
				return SR.GetResourceString("Task_ThrowIfDisposed");
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x000F1EFF File Offset: 0x000F10FF
		internal static string Task_WaitMulti_NullTask
		{
			get
			{
				return SR.GetResourceString("Task_WaitMulti_NullTask");
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x000F1F0B File Offset: 0x000F110B
		internal static string TaskCanceledException_ctor_DefaultMessage
		{
			get
			{
				return SR.GetResourceString("TaskCanceledException_ctor_DefaultMessage");
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000F1F17 File Offset: 0x000F1117
		internal static string TaskCompletionSourceT_TrySetException_NoExceptions
		{
			get
			{
				return SR.GetResourceString("TaskCompletionSourceT_TrySetException_NoExceptions");
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000F1F23 File Offset: 0x000F1123
		internal static string TaskCompletionSourceT_TrySetException_NullException
		{
			get
			{
				return SR.GetResourceString("TaskCompletionSourceT_TrySetException_NullException");
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000F1F2F File Offset: 0x000F112F
		internal static string TaskExceptionHolder_UnhandledException
		{
			get
			{
				return SR.GetResourceString("TaskExceptionHolder_UnhandledException");
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000F1F3B File Offset: 0x000F113B
		internal static string TaskExceptionHolder_UnknownExceptionType
		{
			get
			{
				return SR.GetResourceString("TaskExceptionHolder_UnknownExceptionType");
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000F1F47 File Offset: 0x000F1147
		internal static string TaskScheduler_ExecuteTask_WrongTaskScheduler
		{
			get
			{
				return SR.GetResourceString("TaskScheduler_ExecuteTask_WrongTaskScheduler");
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000F1F53 File Offset: 0x000F1153
		internal static string TaskScheduler_FromCurrentSynchronizationContext_NoCurrent
		{
			get
			{
				return SR.GetResourceString("TaskScheduler_FromCurrentSynchronizationContext_NoCurrent");
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x000F1F5F File Offset: 0x000F115F
		internal static string TaskScheduler_InconsistentStateAfterTryExecuteTaskInline
		{
			get
			{
				return SR.GetResourceString("TaskScheduler_InconsistentStateAfterTryExecuteTaskInline");
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x000F1F6B File Offset: 0x000F116B
		internal static string TaskSchedulerException_ctor_DefaultMessage
		{
			get
			{
				return SR.GetResourceString("TaskSchedulerException_ctor_DefaultMessage");
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x000F1F77 File Offset: 0x000F1177
		internal static string TaskT_DebuggerNoResult
		{
			get
			{
				return SR.GetResourceString("TaskT_DebuggerNoResult");
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000F1F83 File Offset: 0x000F1183
		internal static string TaskT_TransitionToFinal_AlreadyCompleted
		{
			get
			{
				return SR.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted");
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x000F1F8F File Offset: 0x000F118F
		internal static string Thread_ApartmentState_ChangeFailed
		{
			get
			{
				return SR.GetResourceString("Thread_ApartmentState_ChangeFailed");
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000F1F9B File Offset: 0x000F119B
		internal static string Thread_GetSetCompressedStack_NotSupported
		{
			get
			{
				return SR.GetResourceString("Thread_GetSetCompressedStack_NotSupported");
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x000F1FA7 File Offset: 0x000F11A7
		internal static string Thread_Operation_RequiresCurrentThread
		{
			get
			{
				return SR.GetResourceString("Thread_Operation_RequiresCurrentThread");
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000F1FB3 File Offset: 0x000F11B3
		internal static string Threading_AbandonedMutexException
		{
			get
			{
				return SR.GetResourceString("Threading_AbandonedMutexException");
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x000F1FBF File Offset: 0x000F11BF
		internal static string Threading_WaitHandleCannotBeOpenedException
		{
			get
			{
				return SR.GetResourceString("Threading_WaitHandleCannotBeOpenedException");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x000F1FCB File Offset: 0x000F11CB
		internal static string Threading_WaitHandleCannotBeOpenedException_InvalidHandle
		{
			get
			{
				return SR.GetResourceString("Threading_WaitHandleCannotBeOpenedException_InvalidHandle");
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x000F1FD7 File Offset: 0x000F11D7
		internal static string Threading_WaitHandleTooManyPosts
		{
			get
			{
				return SR.GetResourceString("Threading_WaitHandleTooManyPosts");
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x000F1FE3 File Offset: 0x000F11E3
		internal static string Threading_SemaphoreFullException
		{
			get
			{
				return SR.GetResourceString("Threading_SemaphoreFullException");
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x000F1FEF File Offset: 0x000F11EF
		internal static string ThreadLocal_Disposed
		{
			get
			{
				return SR.GetResourceString("ThreadLocal_Disposed");
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x000F1FFB File Offset: 0x000F11FB
		internal static string ThreadLocal_Value_RecursiveCallsToValue
		{
			get
			{
				return SR.GetResourceString("ThreadLocal_Value_RecursiveCallsToValue");
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x000F2007 File Offset: 0x000F1207
		internal static string ThreadLocal_ValuesNotAvailable
		{
			get
			{
				return SR.GetResourceString("ThreadLocal_ValuesNotAvailable");
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x000F2013 File Offset: 0x000F1213
		internal static string TimeZoneNotFound_MissingData
		{
			get
			{
				return SR.GetResourceString("TimeZoneNotFound_MissingData");
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x000F201F File Offset: 0x000F121F
		internal static string TypeInitialization_Default
		{
			get
			{
				return SR.GetResourceString("TypeInitialization_Default");
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x000F202B File Offset: 0x000F122B
		internal static string TypeInitialization_Type
		{
			get
			{
				return SR.GetResourceString("TypeInitialization_Type");
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x000F2037 File Offset: 0x000F1237
		internal static string TypeLoad_ResolveNestedType
		{
			get
			{
				return SR.GetResourceString("TypeLoad_ResolveNestedType");
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x000F2043 File Offset: 0x000F1243
		internal static string TypeLoad_ResolveType
		{
			get
			{
				return SR.GetResourceString("TypeLoad_ResolveType");
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x000F204F File Offset: 0x000F124F
		internal static string TypeLoad_ResolveTypeFromAssembly
		{
			get
			{
				return SR.GetResourceString("TypeLoad_ResolveTypeFromAssembly");
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x000F205B File Offset: 0x000F125B
		internal static string UnauthorizedAccess_IODenied_NoPathName
		{
			get
			{
				return SR.GetResourceString("UnauthorizedAccess_IODenied_NoPathName");
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x000F2067 File Offset: 0x000F1267
		internal static string UnauthorizedAccess_IODenied_Path
		{
			get
			{
				return SR.GetResourceString("UnauthorizedAccess_IODenied_Path");
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x000F2073 File Offset: 0x000F1273
		internal static string UnauthorizedAccess_MemStreamBuffer
		{
			get
			{
				return SR.GetResourceString("UnauthorizedAccess_MemStreamBuffer");
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x000F207F File Offset: 0x000F127F
		internal static string UnauthorizedAccess_RegistryKeyGeneric_Key
		{
			get
			{
				return SR.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key");
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x000F208B File Offset: 0x000F128B
		internal static string UnknownError_Num
		{
			get
			{
				return SR.GetResourceString("UnknownError_Num");
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000F2097 File Offset: 0x000F1297
		internal static string Verification_Exception
		{
			get
			{
				return SR.GetResourceString("Verification_Exception");
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000F20A3 File Offset: 0x000F12A3
		internal static string DebugAssertBanner
		{
			get
			{
				return SR.GetResourceString("DebugAssertBanner");
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000F20AF File Offset: 0x000F12AF
		internal static string DebugAssertLongMessage
		{
			get
			{
				return SR.GetResourceString("DebugAssertLongMessage");
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000F20BB File Offset: 0x000F12BB
		internal static string DebugAssertShortMessage
		{
			get
			{
				return SR.GetResourceString("DebugAssertShortMessage");
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000F20C7 File Offset: 0x000F12C7
		internal static string LockRecursionException_ReadAfterWriteNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_ReadAfterWriteNotAllowed");
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x000F20D3 File Offset: 0x000F12D3
		internal static string LockRecursionException_RecursiveReadNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_RecursiveReadNotAllowed");
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000F20DF File Offset: 0x000F12DF
		internal static string LockRecursionException_RecursiveWriteNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_RecursiveWriteNotAllowed");
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x000F20EB File Offset: 0x000F12EB
		internal static string LockRecursionException_RecursiveUpgradeNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_RecursiveUpgradeNotAllowed");
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x000F20F7 File Offset: 0x000F12F7
		internal static string LockRecursionException_WriteAfterReadNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_WriteAfterReadNotAllowed");
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x000F2103 File Offset: 0x000F1303
		internal static string SynchronizationLockException_MisMatchedUpgrade
		{
			get
			{
				return SR.GetResourceString("SynchronizationLockException_MisMatchedUpgrade");
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000F210F File Offset: 0x000F130F
		internal static string SynchronizationLockException_MisMatchedRead
		{
			get
			{
				return SR.GetResourceString("SynchronizationLockException_MisMatchedRead");
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x000F211B File Offset: 0x000F131B
		internal static string SynchronizationLockException_IncorrectDispose
		{
			get
			{
				return SR.GetResourceString("SynchronizationLockException_IncorrectDispose");
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000F2127 File Offset: 0x000F1327
		internal static string LockRecursionException_UpgradeAfterReadNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_UpgradeAfterReadNotAllowed");
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000F2133 File Offset: 0x000F1333
		internal static string LockRecursionException_UpgradeAfterWriteNotAllowed
		{
			get
			{
				return SR.GetResourceString("LockRecursionException_UpgradeAfterWriteNotAllowed");
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000F213F File Offset: 0x000F133F
		internal static string SynchronizationLockException_MisMatchedWrite
		{
			get
			{
				return SR.GetResourceString("SynchronizationLockException_MisMatchedWrite");
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000F214B File Offset: 0x000F134B
		internal static string NotSupported_SignatureType
		{
			get
			{
				return SR.GetResourceString("NotSupported_SignatureType");
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x000F2157 File Offset: 0x000F1357
		internal static string HashCode_HashCodeNotSupported
		{
			get
			{
				return SR.GetResourceString("HashCode_HashCodeNotSupported");
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x000F2163 File Offset: 0x000F1363
		internal static string HashCode_EqualityNotSupported
		{
			get
			{
				return SR.GetResourceString("HashCode_EqualityNotSupported");
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x000F216F File Offset: 0x000F136F
		internal static string Arg_TypeNotSupported
		{
			get
			{
				return SR.GetResourceString("Arg_TypeNotSupported");
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x000F217B File Offset: 0x000F137B
		internal static string IO_InvalidReadLength
		{
			get
			{
				return SR.GetResourceString("IO_InvalidReadLength");
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x000F2187 File Offset: 0x000F1387
		internal static string Arg_BasePathNotFullyQualified
		{
			get
			{
				return SR.GetResourceString("Arg_BasePathNotFullyQualified");
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x000F2193 File Offset: 0x000F1393
		internal static string Arg_ElementsInSourceIsGreaterThanDestination
		{
			get
			{
				return SR.GetResourceString("Arg_ElementsInSourceIsGreaterThanDestination");
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x000F219F File Offset: 0x000F139F
		internal static string Arg_NullArgumentNullRef
		{
			get
			{
				return SR.GetResourceString("Arg_NullArgumentNullRef");
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x000F21AB File Offset: 0x000F13AB
		internal static string Argument_OverlapAlignmentMismatch
		{
			get
			{
				return SR.GetResourceString("Argument_OverlapAlignmentMismatch");
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000F21B7 File Offset: 0x000F13B7
		internal static string Arg_InsufficientNumberOfElements
		{
			get
			{
				return SR.GetResourceString("Arg_InsufficientNumberOfElements");
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000F21C3 File Offset: 0x000F13C3
		internal static string Arg_MustBeNullTerminatedString
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeNullTerminatedString");
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000F21CF File Offset: 0x000F13CF
		internal static string ArgumentOutOfRange_Week_ISO
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Week_ISO");
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x000F21DB File Offset: 0x000F13DB
		internal static string Argument_BadPInvokeMethod
		{
			get
			{
				return SR.GetResourceString("Argument_BadPInvokeMethod");
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000F21E7 File Offset: 0x000F13E7
		internal static string Argument_BadPInvokeOnInterface
		{
			get
			{
				return SR.GetResourceString("Argument_BadPInvokeOnInterface");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000F21F3 File Offset: 0x000F13F3
		internal static string Argument_MethodRedefined
		{
			get
			{
				return SR.GetResourceString("Argument_MethodRedefined");
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000F21FF File Offset: 0x000F13FF
		internal static string Argument_CannotExtractScalar
		{
			get
			{
				return SR.GetResourceString("Argument_CannotExtractScalar");
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000F220B File Offset: 0x000F140B
		internal static string Argument_CannotParsePrecision
		{
			get
			{
				return SR.GetResourceString("Argument_CannotParsePrecision");
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000F2217 File Offset: 0x000F1417
		internal static string Argument_GWithPrecisionNotSupported
		{
			get
			{
				return SR.GetResourceString("Argument_GWithPrecisionNotSupported");
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000F2223 File Offset: 0x000F1423
		internal static string Argument_PrecisionTooLarge
		{
			get
			{
				return SR.GetResourceString("Argument_PrecisionTooLarge");
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000F222F File Offset: 0x000F142F
		internal static string AssemblyDependencyResolver_FailedToLoadHostpolicy
		{
			get
			{
				return SR.GetResourceString("AssemblyDependencyResolver_FailedToLoadHostpolicy");
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000F223B File Offset: 0x000F143B
		internal static string AssemblyDependencyResolver_FailedToResolveDependencies
		{
			get
			{
				return SR.GetResourceString("AssemblyDependencyResolver_FailedToResolveDependencies");
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000F2247 File Offset: 0x000F1447
		internal static string Arg_EnumNotCloneable
		{
			get
			{
				return SR.GetResourceString("Arg_EnumNotCloneable");
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000F2253 File Offset: 0x000F1453
		internal static string InvalidOp_InvalidNewEnumVariant
		{
			get
			{
				return SR.GetResourceString("InvalidOp_InvalidNewEnumVariant");
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000F225F File Offset: 0x000F145F
		internal static string Argument_StructArrayTooLarge
		{
			get
			{
				return SR.GetResourceString("Argument_StructArrayTooLarge");
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000F226B File Offset: 0x000F146B
		internal static string IndexOutOfRange_ArrayWithOffset
		{
			get
			{
				return SR.GetResourceString("IndexOutOfRange_ArrayWithOffset");
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000F2277 File Offset: 0x000F1477
		internal static string Serialization_DangerousDeserialization
		{
			get
			{
				return SR.GetResourceString("Serialization_DangerousDeserialization");
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000F2283 File Offset: 0x000F1483
		internal static string Serialization_DangerousDeserialization_Switch
		{
			get
			{
				return SR.GetResourceString("Serialization_DangerousDeserialization_Switch");
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000F228F File Offset: 0x000F148F
		internal static string Argument_InvalidStartupHookSimpleAssemblyName
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidStartupHookSimpleAssemblyName");
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000F229B File Offset: 0x000F149B
		internal static string Argument_StartupHookAssemblyLoadFailed
		{
			get
			{
				return SR.GetResourceString("Argument_StartupHookAssemblyLoadFailed");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000F22A7 File Offset: 0x000F14A7
		internal static string InvalidOperation_NonStaticComRegFunction
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NonStaticComRegFunction");
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000F22B3 File Offset: 0x000F14B3
		internal static string InvalidOperation_NonStaticComUnRegFunction
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_NonStaticComUnRegFunction");
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000F22BF File Offset: 0x000F14BF
		internal static string InvalidOperation_InvalidComRegFunctionSig
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_InvalidComRegFunctionSig");
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000F22CB File Offset: 0x000F14CB
		internal static string InvalidOperation_InvalidComUnRegFunctionSig
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_InvalidComUnRegFunctionSig");
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000F22D7 File Offset: 0x000F14D7
		internal static string InvalidOperation_MultipleComRegFunctions
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MultipleComRegFunctions");
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000F22E3 File Offset: 0x000F14E3
		internal static string InvalidOperation_MultipleComUnRegFunctions
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_MultipleComUnRegFunctions");
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000F22EF File Offset: 0x000F14EF
		internal static string InvalidOperation_ResetGlobalComWrappersInstance
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ResetGlobalComWrappersInstance");
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000F22FB File Offset: 0x000F14FB
		internal static string Argument_SpansMustHaveSameLength
		{
			get
			{
				return SR.GetResourceString("Argument_SpansMustHaveSameLength");
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000F2307 File Offset: 0x000F1507
		internal static string NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed
		{
			get
			{
				return SR.GetResourceString("NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed");
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000F2313 File Offset: 0x000F1513
		internal static string GenericInvalidData
		{
			get
			{
				return SR.GetResourceString("GenericInvalidData");
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000F231F File Offset: 0x000F151F
		internal static string Argument_ResourceScopeWrongDirection
		{
			get
			{
				return SR.GetResourceString("Argument_ResourceScopeWrongDirection");
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x000F232B File Offset: 0x000F152B
		internal static string ArgumentNull_TypeRequiredByResourceScope
		{
			get
			{
				return SR.GetResourceString("ArgumentNull_TypeRequiredByResourceScope");
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000F2337 File Offset: 0x000F1537
		internal static string Argument_BadResourceScopeTypeBits
		{
			get
			{
				return SR.GetResourceString("Argument_BadResourceScopeTypeBits");
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000F2343 File Offset: 0x000F1543
		internal static string Argument_BadResourceScopeVisibilityBits
		{
			get
			{
				return SR.GetResourceString("Argument_BadResourceScopeVisibilityBits");
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000F234F File Offset: 0x000F154F
		internal static string net_emptystringcall
		{
			get
			{
				return SR.GetResourceString("net_emptystringcall");
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000F235B File Offset: 0x000F155B
		internal static string Argument_EmptyApplicationName
		{
			get
			{
				return SR.GetResourceString("Argument_EmptyApplicationName");
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000F2367 File Offset: 0x000F1567
		internal static string Argument_FrameworkNameInvalid
		{
			get
			{
				return SR.GetResourceString("Argument_FrameworkNameInvalid");
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000F2373 File Offset: 0x000F1573
		internal static string Argument_FrameworkNameInvalidVersion
		{
			get
			{
				return SR.GetResourceString("Argument_FrameworkNameInvalidVersion");
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000F237F File Offset: 0x000F157F
		internal static string Argument_FrameworkNameMissingVersion
		{
			get
			{
				return SR.GetResourceString("Argument_FrameworkNameMissingVersion");
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000F238B File Offset: 0x000F158B
		internal static string Argument_FrameworkNameTooShort
		{
			get
			{
				return SR.GetResourceString("Argument_FrameworkNameTooShort");
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000F2397 File Offset: 0x000F1597
		internal static string Arg_SwitchExpressionException
		{
			get
			{
				return SR.GetResourceString("Arg_SwitchExpressionException");
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000F23A3 File Offset: 0x000F15A3
		internal static string Arg_ContextMarshalException
		{
			get
			{
				return SR.GetResourceString("Arg_ContextMarshalException");
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000F23AF File Offset: 0x000F15AF
		internal static string Arg_AppDomainUnloadedException
		{
			get
			{
				return SR.GetResourceString("Arg_AppDomainUnloadedException");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000F23BB File Offset: 0x000F15BB
		internal static string SwitchExpressionException_UnmatchedValue
		{
			get
			{
				return SR.GetResourceString("SwitchExpressionException_UnmatchedValue");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000F23C7 File Offset: 0x000F15C7
		internal static string Encoding_UTF7_Disabled
		{
			get
			{
				return SR.GetResourceString("Encoding_UTF7_Disabled");
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000F23D3 File Offset: 0x000F15D3
		internal static string IDynamicInterfaceCastable_DoesNotImplementRequested
		{
			get
			{
				return SR.GetResourceString("IDynamicInterfaceCastable_DoesNotImplementRequested");
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000F23DF File Offset: 0x000F15DF
		internal static string IDynamicInterfaceCastable_MissingImplementationAttribute
		{
			get
			{
				return SR.GetResourceString("IDynamicInterfaceCastable_MissingImplementationAttribute");
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000F23EB File Offset: 0x000F15EB
		internal static string IDynamicInterfaceCastable_NotInterface
		{
			get
			{
				return SR.GetResourceString("IDynamicInterfaceCastable_NotInterface");
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000F23F7 File Offset: 0x000F15F7
		internal static string Arg_MustBeHalf
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeHalf");
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000F2403 File Offset: 0x000F1603
		internal static string Arg_MustBeRune
		{
			get
			{
				return SR.GetResourceString("Arg_MustBeRune");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000F240F File Offset: 0x000F160F
		internal static string BinaryFormatter_SerializationDisallowed
		{
			get
			{
				return SR.GetResourceString("BinaryFormatter_SerializationDisallowed");
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000F241B File Offset: 0x000F161B
		internal static string NotSupported_CodeBase
		{
			get
			{
				return SR.GetResourceString("NotSupported_CodeBase");
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000F2428 File Offset: 0x000F1628
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			SR.s_usingResourceKeys = (AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag);
		}

		// Token: 0x0400049B RID: 1179
		private static readonly object _lock = new object();

		// Token: 0x0400049C RID: 1180
		private static List<string> _currentlyLoading;

		// Token: 0x0400049D RID: 1181
		private static int _infinitelyRecursingCount;

		// Token: 0x0400049E RID: 1182
		private static bool _resourceManagerInited;

		// Token: 0x0400049F RID: 1183
		private static readonly bool s_usingResourceKeys;

		// Token: 0x040004A0 RID: 1184
		private static ResourceManager s_resourceManager;
	}
}
