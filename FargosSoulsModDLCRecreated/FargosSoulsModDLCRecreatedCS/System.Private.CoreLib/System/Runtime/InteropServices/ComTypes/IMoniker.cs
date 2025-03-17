using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C4 RID: 1220
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[NullableContext(1)]
	[ComImport]
	public interface IMoniker
	{
		// Token: 0x06004563 RID: 17763
		void GetClassID(out Guid pClassID);

		// Token: 0x06004564 RID: 17764
		[PreserveSig]
		int IsDirty();

		// Token: 0x06004565 RID: 17765
		void Load(IStream pStm);

		// Token: 0x06004566 RID: 17766
		void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x06004567 RID: 17767
		void GetSizeMax(out long pcbSize);

		// Token: 0x06004568 RID: 17768
		void BindToObject(IBindCtx pbc, [Nullable(2)] IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

		// Token: 0x06004569 RID: 17769
		void BindToStorage(IBindCtx pbc, [Nullable(2)] IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x0600456A RID: 17770
		[NullableContext(2)]
		void Reduce([Nullable(1)] IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

		// Token: 0x0600456B RID: 17771
		void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, [Nullable(2)] out IMoniker ppmkComposite);

		// Token: 0x0600456C RID: 17772
		[NullableContext(2)]
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

		// Token: 0x0600456D RID: 17773
		[PreserveSig]
		int IsEqual(IMoniker pmkOtherMoniker);

		// Token: 0x0600456E RID: 17774
		void Hash(out int pdwHash);

		// Token: 0x0600456F RID: 17775
		[NullableContext(2)]
		[PreserveSig]
		int IsRunning([Nullable(1)] IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

		// Token: 0x06004570 RID: 17776
		void GetTimeOfLastChange(IBindCtx pbc, [Nullable(2)] IMoniker pmkToLeft, out FILETIME pFileTime);

		// Token: 0x06004571 RID: 17777
		void Inverse(out IMoniker ppmk);

		// Token: 0x06004572 RID: 17778
		void CommonPrefixWith(IMoniker pmkOther, [Nullable(2)] out IMoniker ppmkPrefix);

		// Token: 0x06004573 RID: 17779
		void RelativePathTo(IMoniker pmkOther, [Nullable(2)] out IMoniker ppmkRelPath);

		// Token: 0x06004574 RID: 17780
		void GetDisplayName(IBindCtx pbc, [Nullable(2)] IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x06004575 RID: 17781
		void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

		// Token: 0x06004576 RID: 17782
		[PreserveSig]
		int IsSystemMoniker(out int pdwMksys);
	}
}
