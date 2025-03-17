using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200062D RID: 1581
	internal class SymWrapperCore
	{
		// Token: 0x0200062E RID: 1582
		private class SymDocumentWriter : ISymbolDocumentWriter
		{
			// Token: 0x06005088 RID: 20616 RVA: 0x00193384 File Offset: 0x00192584
			internal unsafe SymDocumentWriter(PunkSafeHandle pDocumentWriterSafeHandle)
			{
				this.m_pDocumentWriterSafeHandle = pDocumentWriterSafeHandle;
				this.m_pDocWriter = (SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriter*)((void*)this.m_pDocumentWriterSafeHandle.DangerousGetHandle());
				this.m_vtable = (SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriterVTable)Marshal.PtrToStructure(this.m_pDocWriter->m_unmanagedVTable, typeof(SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriterVTable));
			}

			// Token: 0x06005089 RID: 20617 RVA: 0x001933D9 File Offset: 0x001925D9
			internal PunkSafeHandle GetUnmanaged()
			{
				return this.m_pDocumentWriterSafeHandle;
			}

			// Token: 0x0600508A RID: 20618 RVA: 0x000C279F File Offset: 0x000C199F
			void ISymbolDocumentWriter.SetSource(byte[] source)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600508B RID: 20619 RVA: 0x001933E4 File Offset: 0x001925E4
			void ISymbolDocumentWriter.SetCheckSum(Guid algorithmId, byte[] checkSum)
			{
				int num = this.m_vtable.SetCheckSum(this.m_pDocWriter, algorithmId, (uint)checkSum.Length, checkSum);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x04001494 RID: 5268
			private PunkSafeHandle m_pDocumentWriterSafeHandle;

			// Token: 0x04001495 RID: 5269
			private unsafe SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriter* m_pDocWriter;

			// Token: 0x04001496 RID: 5270
			private SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriterVTable m_vtable;

			// Token: 0x0200062F RID: 1583
			// (Invoke) Token: 0x0600508D RID: 20621
			private unsafe delegate int DSetCheckSum(SymWrapperCore.SymDocumentWriter.ISymUnmanagedDocumentWriter* pThis, Guid algorithmId, uint checkSumSize, [In] byte[] checkSum);

			// Token: 0x02000630 RID: 1584
			private struct ISymUnmanagedDocumentWriterVTable
			{
				// Token: 0x04001497 RID: 5271
				internal IntPtr QueryInterface;

				// Token: 0x04001498 RID: 5272
				internal IntPtr AddRef;

				// Token: 0x04001499 RID: 5273
				internal IntPtr Release;

				// Token: 0x0400149A RID: 5274
				internal IntPtr SetSource;

				// Token: 0x0400149B RID: 5275
				internal SymWrapperCore.SymDocumentWriter.DSetCheckSum SetCheckSum;
			}

			// Token: 0x02000631 RID: 1585
			private struct ISymUnmanagedDocumentWriter
			{
				// Token: 0x0400149C RID: 5276
				internal IntPtr m_unmanagedVTable;
			}
		}

		// Token: 0x02000632 RID: 1586
		internal class SymWriter : ISymbolWriter
		{
			// Token: 0x0600508E RID: 20622 RVA: 0x00193418 File Offset: 0x00192618
			internal static ISymbolWriter CreateSymWriter()
			{
				return new SymWrapperCore.SymWriter();
			}

			// Token: 0x0600508F RID: 20623 RVA: 0x000ABD27 File Offset: 0x000AAF27
			private SymWriter()
			{
			}

			// Token: 0x06005090 RID: 20624 RVA: 0x00193420 File Offset: 0x00192620
			ISymbolDocumentWriter ISymbolWriter.DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
			{
				PunkSafeHandle punkSafeHandle;
				int num = this.m_vtable.DefineDocument(this.m_pWriter, url, ref language, ref languageVendor, ref documentType, out punkSafeHandle);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
				if (punkSafeHandle.IsInvalid)
				{
					return null;
				}
				return new SymWrapperCore.SymDocumentWriter(punkSafeHandle);
			}

			// Token: 0x06005091 RID: 20625 RVA: 0x00193468 File Offset: 0x00192668
			void ISymbolWriter.OpenMethod(SymbolToken method)
			{
				int num = this.m_vtable.OpenMethod(this.m_pWriter, method.GetToken());
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x06005092 RID: 20626 RVA: 0x001934A0 File Offset: 0x001926A0
			void ISymbolWriter.CloseMethod()
			{
				int num = this.m_vtable.CloseMethod(this.m_pWriter);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x06005093 RID: 20627 RVA: 0x001934D0 File Offset: 0x001926D0
			void ISymbolWriter.DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns)
			{
				int num = 0;
				if (offsets != null)
				{
					num = offsets.Length;
				}
				else if (lines != null)
				{
					num = lines.Length;
				}
				else if (columns != null)
				{
					num = columns.Length;
				}
				else if (endLines != null)
				{
					num = endLines.Length;
				}
				else if (endColumns != null)
				{
					num = endColumns.Length;
				}
				if (num == 0)
				{
					return;
				}
				if ((offsets != null && offsets.Length != num) || (lines != null && lines.Length != num) || (columns != null && columns.Length != num) || (endLines != null && endLines.Length != num) || (endColumns != null && endColumns.Length != num))
				{
					throw new ArgumentException();
				}
				SymWrapperCore.SymDocumentWriter symDocumentWriter = (SymWrapperCore.SymDocumentWriter)document;
				int num2 = this.m_vtable.DefineSequencePoints(this.m_pWriter, symDocumentWriter.GetUnmanaged(), num, offsets, lines, columns, endLines, endColumns);
				if (num2 < 0)
				{
					throw Marshal.GetExceptionForHR(num2);
				}
			}

			// Token: 0x06005094 RID: 20628 RVA: 0x00193588 File Offset: 0x00192788
			int ISymbolWriter.OpenScope(int startOffset)
			{
				int result;
				int num = this.m_vtable.OpenScope(this.m_pWriter, startOffset, out result);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
				return result;
			}

			// Token: 0x06005095 RID: 20629 RVA: 0x001935BC File Offset: 0x001927BC
			void ISymbolWriter.CloseScope(int endOffset)
			{
				int num = this.m_vtable.CloseScope(this.m_pWriter, endOffset);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x06005096 RID: 20630 RVA: 0x001935EC File Offset: 0x001927EC
			void ISymbolWriter.DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset)
			{
				int num = this.m_vtable.DefineLocalVariable(this.m_pWriter, name, (int)attributes, signature.Length, signature, (int)addrKind, addr1, addr2, addr3, startOffset, endOffset);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x06005097 RID: 20631 RVA: 0x00193630 File Offset: 0x00192830
			void ISymbolWriter.UsingNamespace(string name)
			{
				int num = this.m_vtable.UsingNamespace(this.m_pWriter, name);
				if (num < 0)
				{
					throw Marshal.GetExceptionForHR(num);
				}
			}

			// Token: 0x06005098 RID: 20632 RVA: 0x00193660 File Offset: 0x00192860
			internal unsafe void InternalSetUnderlyingWriter(IntPtr ppUnderlyingWriter)
			{
				this.m_pWriter = *(IntPtr*)((void*)ppUnderlyingWriter);
				this.m_vtable = (SymWrapperCore.SymWriter.ISymUnmanagedWriterVTable)Marshal.PtrToStructure(this.m_pWriter->m_unmanagedVTable, typeof(SymWrapperCore.SymWriter.ISymUnmanagedWriterVTable));
			}

			// Token: 0x0400149D RID: 5277
			private unsafe SymWrapperCore.SymWriter.ISymUnmanagedWriter* m_pWriter;

			// Token: 0x0400149E RID: 5278
			private SymWrapperCore.SymWriter.ISymUnmanagedWriterVTable m_vtable;

			// Token: 0x02000633 RID: 1587
			// (Invoke) Token: 0x0600509A RID: 20634
			private unsafe delegate int DInitialize(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, IntPtr emitter, [MarshalAs(UnmanagedType.LPWStr)] string filename, IntPtr pIStream, [MarshalAs(UnmanagedType.Bool)] bool fFullBuild);

			// Token: 0x02000634 RID: 1588
			// (Invoke) Token: 0x0600509C RID: 20636
			private unsafe delegate int DDefineDocument(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, [MarshalAs(UnmanagedType.LPWStr)] string url, [In] ref Guid language, [In] ref Guid languageVender, [In] ref Guid documentType, out PunkSafeHandle ppsymUnmanagedDocumentWriter);

			// Token: 0x02000635 RID: 1589
			// (Invoke) Token: 0x0600509E RID: 20638
			private unsafe delegate int DSetUserEntryPoint(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int entryMethod);

			// Token: 0x02000636 RID: 1590
			// (Invoke) Token: 0x060050A0 RID: 20640
			private unsafe delegate int DOpenMethod(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int entryMethod);

			// Token: 0x02000637 RID: 1591
			// (Invoke) Token: 0x060050A2 RID: 20642
			private unsafe delegate int DCloseMethod(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis);

			// Token: 0x02000638 RID: 1592
			// (Invoke) Token: 0x060050A4 RID: 20644
			private unsafe delegate int DDefineSequencePoints(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, PunkSafeHandle document, int spCount, [In] int[] offsets, [In] int[] lines, [In] int[] columns, [In] int[] endLines, [In] int[] endColumns);

			// Token: 0x02000639 RID: 1593
			// (Invoke) Token: 0x060050A6 RID: 20646
			private unsafe delegate int DOpenScope(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int startOffset, out int pretval);

			// Token: 0x0200063A RID: 1594
			// (Invoke) Token: 0x060050A8 RID: 20648
			private unsafe delegate int DCloseScope(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int endOffset);

			// Token: 0x0200063B RID: 1595
			// (Invoke) Token: 0x060050AA RID: 20650
			private unsafe delegate int DSetScopeRange(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int scopeID, int startOffset, int endOffset);

			// Token: 0x0200063C RID: 1596
			// (Invoke) Token: 0x060050AC RID: 20652
			private unsafe delegate int DDefineLocalVariable(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, [MarshalAs(UnmanagedType.LPWStr)] string name, int attributes, int cSig, [In] byte[] signature, int addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

			// Token: 0x0200063D RID: 1597
			// (Invoke) Token: 0x060050AE RID: 20654
			private unsafe delegate int DClose(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis);

			// Token: 0x0200063E RID: 1598
			// (Invoke) Token: 0x060050B0 RID: 20656
			private unsafe delegate int DSetSymAttribute(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, int parent, [MarshalAs(UnmanagedType.LPWStr)] string name, int cData, [In] byte[] data);

			// Token: 0x0200063F RID: 1599
			// (Invoke) Token: 0x060050B2 RID: 20658
			private unsafe delegate int DOpenNamespace(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, [MarshalAs(UnmanagedType.LPWStr)] string name);

			// Token: 0x02000640 RID: 1600
			// (Invoke) Token: 0x060050B4 RID: 20660
			private unsafe delegate int DCloseNamespace(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis);

			// Token: 0x02000641 RID: 1601
			// (Invoke) Token: 0x060050B6 RID: 20662
			private unsafe delegate int DUsingNamespace(SymWrapperCore.SymWriter.ISymUnmanagedWriter* pthis, [MarshalAs(UnmanagedType.LPWStr)] string name);

			// Token: 0x02000642 RID: 1602
			private struct ISymUnmanagedWriterVTable
			{
				// Token: 0x0400149F RID: 5279
				internal IntPtr QueryInterface;

				// Token: 0x040014A0 RID: 5280
				internal IntPtr AddRef;

				// Token: 0x040014A1 RID: 5281
				internal IntPtr Release;

				// Token: 0x040014A2 RID: 5282
				internal SymWrapperCore.SymWriter.DDefineDocument DefineDocument;

				// Token: 0x040014A3 RID: 5283
				internal SymWrapperCore.SymWriter.DSetUserEntryPoint SetUserEntryPoint;

				// Token: 0x040014A4 RID: 5284
				internal SymWrapperCore.SymWriter.DOpenMethod OpenMethod;

				// Token: 0x040014A5 RID: 5285
				internal SymWrapperCore.SymWriter.DCloseMethod CloseMethod;

				// Token: 0x040014A6 RID: 5286
				internal SymWrapperCore.SymWriter.DOpenScope OpenScope;

				// Token: 0x040014A7 RID: 5287
				internal SymWrapperCore.SymWriter.DCloseScope CloseScope;

				// Token: 0x040014A8 RID: 5288
				internal SymWrapperCore.SymWriter.DSetScopeRange SetScopeRange;

				// Token: 0x040014A9 RID: 5289
				internal SymWrapperCore.SymWriter.DDefineLocalVariable DefineLocalVariable;

				// Token: 0x040014AA RID: 5290
				internal IntPtr DefineParameter;

				// Token: 0x040014AB RID: 5291
				internal IntPtr DefineField;

				// Token: 0x040014AC RID: 5292
				internal IntPtr DefineGlobalVariable;

				// Token: 0x040014AD RID: 5293
				internal SymWrapperCore.SymWriter.DClose Close;

				// Token: 0x040014AE RID: 5294
				internal SymWrapperCore.SymWriter.DSetSymAttribute SetSymAttribute;

				// Token: 0x040014AF RID: 5295
				internal SymWrapperCore.SymWriter.DOpenNamespace OpenNamespace;

				// Token: 0x040014B0 RID: 5296
				internal SymWrapperCore.SymWriter.DCloseNamespace CloseNamespace;

				// Token: 0x040014B1 RID: 5297
				internal SymWrapperCore.SymWriter.DUsingNamespace UsingNamespace;

				// Token: 0x040014B2 RID: 5298
				internal IntPtr SetMethodSourceRange;

				// Token: 0x040014B3 RID: 5299
				internal SymWrapperCore.SymWriter.DInitialize Initialize;

				// Token: 0x040014B4 RID: 5300
				internal IntPtr GetDebugInfo;

				// Token: 0x040014B5 RID: 5301
				internal SymWrapperCore.SymWriter.DDefineSequencePoints DefineSequencePoints;
			}

			// Token: 0x02000643 RID: 1603
			private struct ISymUnmanagedWriter
			{
				// Token: 0x040014B6 RID: 5302
				internal IntPtr m_unmanagedVTable;
			}
		}
	}
}
