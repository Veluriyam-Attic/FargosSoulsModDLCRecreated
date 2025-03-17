using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x020003AB RID: 939
	internal struct AsAnyMarshaler
	{
		// Token: 0x060030CF RID: 12495 RVA: 0x00168031 File Offset: 0x00167231
		private static bool IsIn(int dwFlags)
		{
			return (dwFlags & 268435456) != 0;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0016803D File Offset: 0x0016723D
		private static bool IsOut(int dwFlags)
		{
			return (dwFlags & 536870912) != 0;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x00168049 File Offset: 0x00167249
		private static bool IsAnsi(int dwFlags)
		{
			return (dwFlags & 16711680) != 0;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x00168055 File Offset: 0x00167255
		private static bool IsThrowOn(int dwFlags)
		{
			return (dwFlags & 65280) != 0;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x00168061 File Offset: 0x00167261
		private static bool IsBestFit(int dwFlags)
		{
			return (dwFlags & 255) != 0;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0016806D File Offset: 0x0016726D
		internal AsAnyMarshaler(IntPtr pvArrayMarshaler)
		{
			this.pvArrayMarshaler = pvArrayMarshaler;
			this.backPropAction = AsAnyMarshaler.BackPropAction.None;
			this.layoutType = null;
			this.cleanupWorkList = null;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0016808C File Offset: 0x0016728C
		private unsafe IntPtr ConvertArrayToNative(object pManagedHome, int dwFlags)
		{
			Type elementType = pManagedHome.GetType().GetElementType();
			VarEnum varEnum;
			switch (Type.GetTypeCode(elementType))
			{
			case TypeCode.Object:
				if (elementType == typeof(IntPtr))
				{
					int size = IntPtr.Size;
					varEnum = VarEnum.VT_I8;
					goto IL_FE;
				}
				if (elementType == typeof(UIntPtr))
				{
					int size2 = IntPtr.Size;
					varEnum = VarEnum.VT_UI8;
					goto IL_FE;
				}
				break;
			case TypeCode.Boolean:
				varEnum = (VarEnum)254;
				goto IL_FE;
			case TypeCode.Char:
				varEnum = (AsAnyMarshaler.IsAnsi(dwFlags) ? ((VarEnum)253) : VarEnum.VT_UI2);
				goto IL_FE;
			case TypeCode.SByte:
				varEnum = VarEnum.VT_I1;
				goto IL_FE;
			case TypeCode.Byte:
				varEnum = VarEnum.VT_UI1;
				goto IL_FE;
			case TypeCode.Int16:
				varEnum = VarEnum.VT_I2;
				goto IL_FE;
			case TypeCode.UInt16:
				varEnum = VarEnum.VT_UI2;
				goto IL_FE;
			case TypeCode.Int32:
				varEnum = VarEnum.VT_I4;
				goto IL_FE;
			case TypeCode.UInt32:
				varEnum = VarEnum.VT_UI4;
				goto IL_FE;
			case TypeCode.Int64:
				varEnum = VarEnum.VT_I8;
				goto IL_FE;
			case TypeCode.UInt64:
				varEnum = VarEnum.VT_UI8;
				goto IL_FE;
			case TypeCode.Single:
				varEnum = VarEnum.VT_R4;
				goto IL_FE;
			case TypeCode.Double:
				varEnum = VarEnum.VT_R8;
				goto IL_FE;
			}
			throw new ArgumentException(SR.Arg_NDirectBadObject);
			IL_FE:
			int num = (int)varEnum;
			if (AsAnyMarshaler.IsBestFit(dwFlags))
			{
				num |= 65536;
			}
			if (AsAnyMarshaler.IsThrowOn(dwFlags))
			{
				num |= 16777216;
			}
			MngdNativeArrayMarshaler.CreateMarshaler(this.pvArrayMarshaler, IntPtr.Zero, num, IntPtr.Zero);
			IntPtr result;
			IntPtr pNativeHome = new IntPtr((void*)(&result));
			MngdNativeArrayMarshaler.ConvertSpaceToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				MngdNativeArrayMarshaler.ConvertContentsToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Array;
			}
			return result;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x00168210 File Offset: 0x00167410
		private static IntPtr ConvertStringToNative(string pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				intPtr = CSTRMarshaler.ConvertToNative(dwFlags & 65535, pManagedHome, IntPtr.Zero);
			}
			else
			{
				int num = (pManagedHome.Length + 1) * 2;
				intPtr = Marshal.AllocCoTaskMem(num);
				string.InternalCopy(pManagedHome, intPtr, num);
			}
			return intPtr;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x00168258 File Offset: 0x00167458
		private unsafe IntPtr ConvertStringBuilderToNative(StringBuilder pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				StubHelpers.CheckStringLength(pManagedHome.Capacity);
				int num = checked(pManagedHome.Capacity * Marshal.SystemMaxDBCSCharSize + 4);
				intPtr = Marshal.AllocCoTaskMem(num);
				byte* ptr = (byte*)((void*)intPtr);
				*(ptr + num - 3) = 0;
				*(ptr + num - 2) = 0;
				*(ptr + num - 1) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num2 = Marshal.StringToAnsiString(pManagedHome.ToString(), ptr, num, AsAnyMarshaler.IsBestFit(dwFlags), AsAnyMarshaler.IsThrowOn(dwFlags));
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderAnsi;
				}
			}
			else
			{
				int num3 = checked(pManagedHome.Capacity * 2 + 4);
				intPtr = Marshal.AllocCoTaskMem(num3);
				byte* ptr2 = (byte*)((void*)intPtr);
				*(ptr2 + num3 - 1) = 0;
				*(ptr2 + num3 - 2) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num4 = pManagedHome.Length * 2;
					pManagedHome.InternalCopy(intPtr, num4);
					ptr2[num4] = 0;
					(ptr2 + num4)[1] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderUnicode;
				}
			}
			return intPtr;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x00168348 File Offset: 0x00167548
		private unsafe IntPtr ConvertLayoutToNative(object pManagedHome, int dwFlags)
		{
			int cb = Marshal.SizeOfHelper(pManagedHome.GetType(), false);
			IntPtr intPtr = Marshal.AllocCoTaskMem(cb);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				StubHelpers.FmtClassUpdateNativeInternal(pManagedHome, (byte*)((void*)intPtr), ref this.cleanupWorkList);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Layout;
			}
			this.layoutType = pManagedHome.GetType();
			return intPtr;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x001683A0 File Offset: 0x001675A0
		internal IntPtr ConvertToNative(object pManagedHome, int dwFlags)
		{
			if (pManagedHome == null)
			{
				return IntPtr.Zero;
			}
			if (pManagedHome is ArrayWithOffset)
			{
				throw new ArgumentException(SR.Arg_MarshalAsAnyRestriction);
			}
			IntPtr result;
			if (pManagedHome.GetType().IsArray)
			{
				result = this.ConvertArrayToNative(pManagedHome, dwFlags);
			}
			else
			{
				string text = pManagedHome as string;
				if (text != null)
				{
					result = AsAnyMarshaler.ConvertStringToNative(text, dwFlags);
				}
				else
				{
					StringBuilder stringBuilder = pManagedHome as StringBuilder;
					if (stringBuilder != null)
					{
						result = this.ConvertStringBuilderToNative(stringBuilder, dwFlags);
					}
					else
					{
						if (!pManagedHome.GetType().IsLayoutSequential && !pManagedHome.GetType().IsExplicitLayout)
						{
							throw new ArgumentException(SR.Arg_NDirectBadObject);
						}
						result = this.ConvertLayoutToNative(pManagedHome, dwFlags);
					}
				}
			}
			return result;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0016843C File Offset: 0x0016763C
		internal unsafe void ConvertToManaged(object pManagedHome, IntPtr pNativeHome)
		{
			switch (this.backPropAction)
			{
			case AsAnyMarshaler.BackPropAction.Array:
				MngdNativeArrayMarshaler.ConvertContentsToManaged(this.pvArrayMarshaler, ref pManagedHome, new IntPtr((void*)(&pNativeHome)));
				return;
			case AsAnyMarshaler.BackPropAction.Layout:
				StubHelpers.FmtClassUpdateCLRInternal(pManagedHome, (byte*)((void*)pNativeHome));
				return;
			case AsAnyMarshaler.BackPropAction.StringBuilderAnsi:
			{
				int newLength;
				if (pNativeHome == IntPtr.Zero)
				{
					newLength = 0;
				}
				else
				{
					newLength = string.strlen((byte*)((void*)pNativeHome));
				}
				((StringBuilder)pManagedHome).ReplaceBufferAnsiInternal((sbyte*)((void*)pNativeHome), newLength);
				return;
			}
			case AsAnyMarshaler.BackPropAction.StringBuilderUnicode:
			{
				int newLength2;
				if (pNativeHome == IntPtr.Zero)
				{
					newLength2 = 0;
				}
				else
				{
					newLength2 = string.wcslen((char*)((void*)pNativeHome));
				}
				((StringBuilder)pManagedHome).ReplaceBufferInternal((char*)((void*)pNativeHome), newLength2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x001684EB File Offset: 0x001676EB
		internal void ClearNative(IntPtr pNativeHome)
		{
			if (pNativeHome != IntPtr.Zero)
			{
				if (this.layoutType != null)
				{
					Marshal.DestroyStructure(pNativeHome, this.layoutType);
				}
				Interop.Ole32.CoTaskMemFree(pNativeHome);
			}
			StubHelpers.DestroyCleanupList(ref this.cleanupWorkList);
		}

		// Token: 0x04000D62 RID: 3426
		private IntPtr pvArrayMarshaler;

		// Token: 0x04000D63 RID: 3427
		private AsAnyMarshaler.BackPropAction backPropAction;

		// Token: 0x04000D64 RID: 3428
		private Type layoutType;

		// Token: 0x04000D65 RID: 3429
		private CleanupWorkListElement cleanupWorkList;

		// Token: 0x020003AC RID: 940
		private enum BackPropAction
		{
			// Token: 0x04000D67 RID: 3431
			None,
			// Token: 0x04000D68 RID: 3432
			Array,
			// Token: 0x04000D69 RID: 3433
			Layout,
			// Token: 0x04000D6A RID: 3434
			StringBuilderAnsi,
			// Token: 0x04000D6B RID: 3435
			StringBuilderUnicode
		}
	}
}
