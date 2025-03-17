using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004ED RID: 1261
	internal static class CastHelpers
	{
		// Token: 0x060045ED RID: 17901 RVA: 0x0017A364 File Offset: 0x00179564
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int KeyToBucket(ref int tableData, [NativeInteger] UIntPtr source, [NativeInteger] UIntPtr target)
		{
			int num = CastHelpers.HashShift(ref tableData);
			ulong num2 = ((ulong)source << 32 | (ulong)source >> 32) ^ (ulong)target;
			return (int)(num2 * 11400714819323198485UL >> num);
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x0017A398 File Offset: 0x00179598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ref int TableData(int[] table)
		{
			return MemoryMarshal.GetArrayDataReference<int>(table);
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x0017A3A0 File Offset: 0x001795A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ref CastHelpers.CastCacheEntry Element(ref int tableData, int index)
		{
			return Unsafe.Add<CastHelpers.CastCacheEntry>(Unsafe.As<int, CastHelpers.CastCacheEntry>(ref tableData), index + 1);
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000DA911 File Offset: 0x000D9B11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int HashShift(ref int tableData)
		{
			return tableData;
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x0017A3B0 File Offset: 0x001795B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int TableMask(ref int tableData)
		{
			return *Unsafe.Add<int>(ref tableData, 1);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x0017A3BC File Offset: 0x001795BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static CastHelpers.CastResult TryGet([NativeInteger] UIntPtr source, [NativeInteger] UIntPtr target)
		{
			ref int tableData = ref CastHelpers.TableData(CastHelpers.s_table);
			int num = CastHelpers.KeyToBucket(ref tableData, source, target);
			int i = 0;
			while (i < 8)
			{
				ref CastHelpers.CastCacheEntry ptr = ref CastHelpers.Element(ref tableData, num);
				int num2 = Volatile.Read(ref ptr._version);
				UIntPtr source2 = ptr._source;
				num2 &= -2;
				if (source2 == source)
				{
					UIntPtr uintPtr = ptr._targetAndResult;
					uintPtr ^= target;
					if (uintPtr <= (UIntPtr)((IntPtr)1))
					{
						Interlocked.ReadMemoryBarrier();
						if (num2 == ptr._version)
						{
							return (CastHelpers.CastResult)uintPtr;
						}
						break;
					}
				}
				if (num2 == 0)
				{
					break;
				}
				i++;
				num = (num + i & CastHelpers.TableMask(ref tableData));
			}
			return CastHelpers.CastResult.MaybeCast;
		}

		// Token: 0x060045F3 RID: 17907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern object IsInstanceOfAny_NoCacheLookup(void* toTypeHnd, object obj);

		// Token: 0x060045F4 RID: 17908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern object ChkCastAny_NoCacheLookup(void* toTypeHnd, object obj);

		// Token: 0x060045F5 RID: 17909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static ref extern byte Unbox_Helper(void* toTypeHnd, object obj);

		// Token: 0x060045F6 RID: 17910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteBarrier(ref object dst, object obj);

		// Token: 0x060045F7 RID: 17911 RVA: 0x0017A44C File Offset: 0x0017964C
		[StackTraceHidden]
		[DebuggerHidden]
		[DebuggerStepThrough]
		private unsafe static object IsInstanceOfAny(void* toTypeHnd, object obj)
		{
			if (obj != null)
			{
				void* methodTable = (void*)RuntimeHelpers.GetMethodTable(obj);
				if (methodTable != toTypeHnd)
				{
					CastHelpers.CastResult castResult = CastHelpers.TryGet(methodTable, toTypeHnd);
					if (castResult != CastHelpers.CastResult.CanCast)
					{
						if (castResult != CastHelpers.CastResult.CannotCast)
						{
							return CastHelpers.IsInstanceOfAny_NoCacheLookup(toTypeHnd, obj);
						}
						obj = null;
					}
				}
			}
			return obj;
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x0017A484 File Offset: 0x00179684
		[StackTraceHidden]
		[DebuggerHidden]
		[DebuggerStepThrough]
		private unsafe static object IsInstanceOfInterface(void* toTypeHnd, object obj)
		{
			if (obj != null)
			{
				MethodTable* methodTable = RuntimeHelpers.GetMethodTable(obj);
				UIntPtr uintPtr = (UIntPtr)methodTable->InterfaceCount;
				if (uintPtr != 0)
				{
					MethodTable** interfaceMap = methodTable->InterfaceMap;
					UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
					while (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)0)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) != toTypeHnd)
					{
						if (--uintPtr == 0)
						{
							goto IL_95;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)1)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_95;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)2)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_95;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)3)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_95;
						}
						uintPtr2 += (UIntPtr)((IntPtr)4);
					}
					return obj;
				}
				IL_95:
				if (methodTable->NonTrivialInterfaceCast)
				{
					return CastHelpers.IsInstance_Helper(toTypeHnd, obj);
				}
				obj = null;
			}
			return obj;
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x0017A53C File Offset: 0x0017973C
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static object IsInstanceOfClass(void* toTypeHnd, object obj)
		{
			if (obj == null || RuntimeHelpers.GetMethodTable(obj) == (MethodTable*)toTypeHnd)
			{
				return obj;
			}
			MethodTable* parentMethodTable = RuntimeHelpers.GetMethodTable(obj)->ParentMethodTable;
			while (parentMethodTable != (MethodTable*)toTypeHnd)
			{
				if (parentMethodTable != null)
				{
					parentMethodTable = parentMethodTable->ParentMethodTable;
					if (parentMethodTable == (MethodTable*)toTypeHnd)
					{
						break;
					}
					if (parentMethodTable != null)
					{
						parentMethodTable = parentMethodTable->ParentMethodTable;
						if (parentMethodTable == (MethodTable*)toTypeHnd)
						{
							break;
						}
						if (parentMethodTable != null)
						{
							parentMethodTable = parentMethodTable->ParentMethodTable;
							if (parentMethodTable == (MethodTable*)toTypeHnd)
							{
								break;
							}
							if (parentMethodTable != null)
							{
								parentMethodTable = parentMethodTable->ParentMethodTable;
								continue;
							}
						}
					}
				}
				if (!RuntimeHelpers.GetMethodTable(obj)->HasTypeEquivalence)
				{
					obj = null;
					break;
				}
				return CastHelpers.IsInstance_Helper(toTypeHnd, obj);
			}
			return obj;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x0017A5C0 File Offset: 0x001797C0
		[StackTraceHidden]
		[DebuggerHidden]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static object IsInstance_Helper(void* toTypeHnd, object obj)
		{
			CastHelpers.CastResult castResult = CastHelpers.TryGet(RuntimeHelpers.GetMethodTable(obj), toTypeHnd);
			if (castResult == CastHelpers.CastResult.CanCast)
			{
				return obj;
			}
			if (castResult == CastHelpers.CastResult.CannotCast)
			{
				return null;
			}
			return CastHelpers.IsInstanceOfAny_NoCacheLookup(toTypeHnd, obj);
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x0017A5EC File Offset: 0x001797EC
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static object ChkCastAny(void* toTypeHnd, object obj)
		{
			if (obj != null)
			{
				void* methodTable = (void*)RuntimeHelpers.GetMethodTable(obj);
				if (methodTable != toTypeHnd)
				{
					CastHelpers.CastResult castResult = CastHelpers.TryGet(methodTable, toTypeHnd);
					if (castResult != CastHelpers.CastResult.CanCast)
					{
						return CastHelpers.ChkCastAny_NoCacheLookup(toTypeHnd, obj);
					}
				}
			}
			return obj;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x0017A620 File Offset: 0x00179820
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static object ChkCast_Helper(void* toTypeHnd, object obj)
		{
			CastHelpers.CastResult castResult = CastHelpers.TryGet(RuntimeHelpers.GetMethodTable(obj), toTypeHnd);
			if (castResult == CastHelpers.CastResult.CanCast)
			{
				return obj;
			}
			return CastHelpers.ChkCastAny_NoCacheLookup(toTypeHnd, obj);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x0017A648 File Offset: 0x00179848
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static object ChkCastInterface(void* toTypeHnd, object obj)
		{
			if (obj != null)
			{
				MethodTable* methodTable = RuntimeHelpers.GetMethodTable(obj);
				UIntPtr uintPtr = (UIntPtr)methodTable->InterfaceCount;
				if (uintPtr != 0)
				{
					MethodTable** interfaceMap = methodTable->InterfaceMap;
					UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
					while (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)0)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) != toTypeHnd)
					{
						if (--uintPtr == 0)
						{
							goto IL_97;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)1)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_97;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)2)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_97;
						}
						if (*(IntPtr*)(interfaceMap + (ulong)(uintPtr2 + (UIntPtr)((IntPtr)3)) * (ulong)((long)sizeof(MethodTable*)) / (ulong)sizeof(MethodTable*)) == toTypeHnd)
						{
							break;
						}
						if (--uintPtr == 0)
						{
							goto IL_97;
						}
						uintPtr2 += (UIntPtr)((IntPtr)4);
					}
					return obj;
				}
				IL_97:
				return CastHelpers.ChkCast_Helper(toTypeHnd, obj);
			}
			return obj;
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x0017A6F3 File Offset: 0x001798F3
		[DebuggerStepThrough]
		[StackTraceHidden]
		[DebuggerHidden]
		private unsafe static object ChkCastClass(void* toTypeHnd, object obj)
		{
			if (obj == null || RuntimeHelpers.GetMethodTable(obj) == (MethodTable*)toTypeHnd)
			{
				return obj;
			}
			return CastHelpers.ChkCastClassSpecial(toTypeHnd, obj);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x0017A70C File Offset: 0x0017990C
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static object ChkCastClassSpecial(void* toTypeHnd, object obj)
		{
			MethodTable* ptr = RuntimeHelpers.GetMethodTable(obj);
			do
			{
				ptr = ptr->ParentMethodTable;
				if (ptr == (MethodTable*)toTypeHnd)
				{
					return obj;
				}
				if (ptr == null)
				{
					break;
				}
				ptr = ptr->ParentMethodTable;
				if (ptr == (MethodTable*)toTypeHnd)
				{
					return obj;
				}
				if (ptr == null)
				{
					break;
				}
				ptr = ptr->ParentMethodTable;
				if (ptr == (MethodTable*)toTypeHnd)
				{
					return obj;
				}
				if (ptr == null)
				{
					break;
				}
				ptr = ptr->ParentMethodTable;
				if (ptr == (MethodTable*)toTypeHnd)
				{
					return obj;
				}
			}
			while (ptr != null);
			return CastHelpers.ChkCast_Helper(toTypeHnd, obj);
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x0017A76B File Offset: 0x0017996B
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static ref byte Unbox(void* toTypeHnd, object obj)
		{
			if (RuntimeHelpers.GetMethodTable(obj) == (MethodTable*)toTypeHnd)
			{
				return obj.GetRawData();
			}
			return CastHelpers.Unbox_Helper(toTypeHnd, obj);
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000F2A70 File Offset: 0x000F1C70
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private static ref object ThrowArrayMismatchException()
		{
			throw new ArrayTypeMismatchException();
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x0017A784 File Offset: 0x00179984
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private unsafe static ref object LdelemaRef(Array array, int index, void* type)
		{
			ref object result = ref Unsafe.As<CastHelpers.ArrayElement[]>(array)[index].Value;
			void* elementType = RuntimeHelpers.GetMethodTable(array)->ElementType;
			if (elementType == type)
			{
				return ref result;
			}
			return CastHelpers.ThrowArrayMismatchException();
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x0017A7BC File Offset: 0x001799BC
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		private unsafe static void StelemRef(Array array, int index, object obj)
		{
			ref object ptr = ref Unsafe.As<CastHelpers.ArrayElement[]>(array)[index].Value;
			void* elementType = RuntimeHelpers.GetMethodTable(array)->ElementType;
			if (obj == null)
			{
				ptr = null;
				return;
			}
			if (elementType != (void*)RuntimeHelpers.GetMethodTable(obj) && !(array.GetType() == typeof(object[])))
			{
				CastHelpers.StelemRef_Helper(ref ptr, elementType, obj);
				return;
			}
			CastHelpers.WriteBarrier(ref ptr, obj);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0017A820 File Offset: 0x00179A20
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void StelemRef_Helper(ref object element, void* elementType, object obj)
		{
			CastHelpers.CastResult castResult = CastHelpers.TryGet(RuntimeHelpers.GetMethodTable(obj), elementType);
			if (castResult == CastHelpers.CastResult.CanCast)
			{
				CastHelpers.WriteBarrier(ref element, obj);
				return;
			}
			CastHelpers.StelemRef_Helper_NoCacheLookup(ref element, elementType, obj);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x0017A84E File Offset: 0x00179A4E
		[DebuggerHidden]
		[StackTraceHidden]
		[DebuggerStepThrough]
		private unsafe static void StelemRef_Helper_NoCacheLookup(ref object element, void* elementType, object obj)
		{
			obj = CastHelpers.IsInstanceOfAny_NoCacheLookup(elementType, obj);
			if (obj != null)
			{
				CastHelpers.WriteBarrier(ref element, obj);
				return;
			}
			throw new ArrayTypeMismatchException();
		}

		// Token: 0x040010B0 RID: 4272
		private static int[] s_table;

		// Token: 0x020004EE RID: 1262
		[DebuggerDisplay("Source = {_source}; Target = {_targetAndResult & ~1}; Result = {_targetAndResult & 1}; VersionNum = {_version & ((1 << 29) - 1)}; Distance = {_version >> 29};")]
		private struct CastCacheEntry
		{
			// Token: 0x040010B1 RID: 4273
			internal int _version;

			// Token: 0x040010B2 RID: 4274
			[NativeInteger]
			internal UIntPtr _source;

			// Token: 0x040010B3 RID: 4275
			[NativeInteger]
			internal UIntPtr _targetAndResult;
		}

		// Token: 0x020004EF RID: 1263
		private enum CastResult
		{
			// Token: 0x040010B5 RID: 4277
			CannotCast,
			// Token: 0x040010B6 RID: 4278
			CanCast,
			// Token: 0x040010B7 RID: 4279
			MaybeCast
		}

		// Token: 0x020004F0 RID: 1264
		internal struct ArrayElement
		{
			// Token: 0x040010B8 RID: 4280
			public object Value;
		}
	}
}
