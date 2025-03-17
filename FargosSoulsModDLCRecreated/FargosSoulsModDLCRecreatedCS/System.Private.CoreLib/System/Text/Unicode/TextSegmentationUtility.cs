using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Text.Unicode
{
	// Token: 0x02000394 RID: 916
	internal static class TextSegmentationUtility
	{
		// Token: 0x06003059 RID: 12377 RVA: 0x00165164 File Offset: 0x00164364
		private static int GetLengthOfFirstExtendedGraphemeCluster<T>(ReadOnlySpan<T> input, TextSegmentationUtility.DecodeFirstRune<T> decoder)
		{
			TextSegmentationUtility.Processor<T> processor = new TextSegmentationUtility.Processor<T>(input, decoder);
			processor.MoveNext();
			while (processor.CurrentType == GraphemeClusterBreakType.Prepend)
			{
				processor.MoveNext();
			}
			if (processor.CurrentCodeUnitOffset <= 0 || (processor.CurrentType != GraphemeClusterBreakType.Control && processor.CurrentType != GraphemeClusterBreakType.CR && processor.CurrentType != GraphemeClusterBreakType.LF))
			{
				GraphemeClusterBreakType currentType = processor.CurrentType;
				processor.MoveNext();
				switch (currentType)
				{
				case GraphemeClusterBreakType.CR:
					if (processor.CurrentType == GraphemeClusterBreakType.LF)
					{
						processor.MoveNext();
						goto IL_1BC;
					}
					goto IL_1BC;
				case GraphemeClusterBreakType.LF:
				case GraphemeClusterBreakType.Control:
					goto IL_1BC;
				case GraphemeClusterBreakType.Extend:
				case GraphemeClusterBreakType.ZWJ:
				case GraphemeClusterBreakType.Prepend:
				case GraphemeClusterBreakType.SpacingMark:
					goto IL_19E;
				case GraphemeClusterBreakType.Regional_Indicator:
					if (processor.CurrentType == GraphemeClusterBreakType.Regional_Indicator)
					{
						processor.MoveNext();
						goto IL_19E;
					}
					goto IL_19E;
				case GraphemeClusterBreakType.L:
					while (processor.CurrentType == GraphemeClusterBreakType.L)
					{
						processor.MoveNext();
					}
					if (processor.CurrentType == GraphemeClusterBreakType.V)
					{
						processor.MoveNext();
					}
					else if (processor.CurrentType == GraphemeClusterBreakType.LV)
					{
						processor.MoveNext();
					}
					else
					{
						if (processor.CurrentType == GraphemeClusterBreakType.LVT)
						{
							processor.MoveNext();
							goto IL_13A;
						}
						goto IL_19E;
					}
					break;
				case GraphemeClusterBreakType.V:
				case GraphemeClusterBreakType.LV:
					break;
				case GraphemeClusterBreakType.T:
				case GraphemeClusterBreakType.LVT:
					goto IL_13A;
				case GraphemeClusterBreakType.Extended_Pictograph:
					for (;;)
					{
						if (processor.CurrentType != GraphemeClusterBreakType.Extend)
						{
							if (processor.CurrentType != GraphemeClusterBreakType.ZWJ)
							{
								goto IL_19E;
							}
							processor.MoveNext();
							if (processor.CurrentType != GraphemeClusterBreakType.Extended_Pictograph)
							{
								goto IL_19E;
							}
							processor.MoveNext();
						}
						else
						{
							processor.MoveNext();
						}
					}
					break;
				default:
					goto IL_19E;
				}
				while (processor.CurrentType == GraphemeClusterBreakType.V)
				{
					processor.MoveNext();
				}
				if (processor.CurrentType != GraphemeClusterBreakType.T)
				{
					goto IL_19E;
				}
				processor.MoveNext();
				IL_13A:
				while (processor.CurrentType == GraphemeClusterBreakType.T)
				{
					processor.MoveNext();
				}
				IL_19E:
				while (processor.CurrentType == GraphemeClusterBreakType.Extend || processor.CurrentType == GraphemeClusterBreakType.ZWJ || processor.CurrentType == GraphemeClusterBreakType.SpacingMark)
				{
					processor.MoveNext();
				}
			}
			IL_1BC:
			return processor.CurrentCodeUnitOffset;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x00165334 File Offset: 0x00164534
		public static int GetLengthOfFirstUtf16ExtendedGraphemeCluster(ReadOnlySpan<char> input)
		{
			return TextSegmentationUtility.GetLengthOfFirstExtendedGraphemeCluster<char>(input, TextSegmentationUtility._utf16Decoder);
		}

		// Token: 0x04000D55 RID: 3413
		private static readonly TextSegmentationUtility.DecodeFirstRune<char> _utf16Decoder = new TextSegmentationUtility.DecodeFirstRune<char>(Rune.DecodeFromUtf16);

		// Token: 0x02000395 RID: 917
		// (Invoke) Token: 0x0600305D RID: 12381
		private delegate OperationStatus DecodeFirstRune<T>(ReadOnlySpan<T> input, out Rune rune, out int elementsConsumed);

		// Token: 0x02000396 RID: 918
		[StructLayout(LayoutKind.Auto)]
		private ref struct Processor<T>
		{
			// Token: 0x0600305E RID: 12382 RVA: 0x00165354 File Offset: 0x00164554
			internal Processor(ReadOnlySpan<T> buffer, TextSegmentationUtility.DecodeFirstRune<T> decoder)
			{
				this._buffer = buffer;
				this._decoder = decoder;
				this._codeUnitLengthOfCurrentScalar = 0;
				this.CurrentType = GraphemeClusterBreakType.Other;
				this.CurrentCodeUnitOffset = 0;
			}

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x0600305F RID: 12383 RVA: 0x00165379 File Offset: 0x00164579
			// (set) Token: 0x06003060 RID: 12384 RVA: 0x00165381 File Offset: 0x00164581
			public int CurrentCodeUnitOffset { readonly get; private set; }

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x06003061 RID: 12385 RVA: 0x0016538A File Offset: 0x0016458A
			// (set) Token: 0x06003062 RID: 12386 RVA: 0x00165392 File Offset: 0x00164592
			public GraphemeClusterBreakType CurrentType { readonly get; private set; }

			// Token: 0x06003063 RID: 12387 RVA: 0x0016539C File Offset: 0x0016459C
			public void MoveNext()
			{
				this.CurrentCodeUnitOffset += this._codeUnitLengthOfCurrentScalar;
				Rune rune;
				this._decoder(this._buffer.Slice(this.CurrentCodeUnitOffset), out rune, out this._codeUnitLengthOfCurrentScalar);
				this.CurrentType = CharUnicodeInfo.GetGraphemeClusterBreakType(rune);
			}

			// Token: 0x04000D56 RID: 3414
			private readonly ReadOnlySpan<T> _buffer;

			// Token: 0x04000D57 RID: 3415
			private readonly TextSegmentationUtility.DecodeFirstRune<T> _decoder;

			// Token: 0x04000D58 RID: 3416
			private int _codeUnitLengthOfCurrentScalar;
		}
	}
}
