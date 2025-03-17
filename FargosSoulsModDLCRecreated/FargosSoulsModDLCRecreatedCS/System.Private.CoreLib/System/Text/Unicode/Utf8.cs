using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.Text.Unicode
{
	// Token: 0x02000398 RID: 920
	public static class Utf8
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x00165A78 File Offset: 0x00164C78
		public unsafe static OperationStatus FromUtf16(ReadOnlySpan<char> source, Span<byte> destination, out int charsRead, out int bytesWritten, bool replaceInvalidSequences = true, bool isFinalBlock = true)
		{
			int length = source.Length;
			int length2 = destination.Length;
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* ptr2 = reference2;
					OperationStatus operationStatus = OperationStatus.Done;
					char* ptr3 = ptr;
					byte* ptr4 = ptr2;
					while (!source.IsEmpty)
					{
						operationStatus = Utf8Utility.TranscodeToUtf8((char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(source)), source.Length, (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(destination)), destination.Length, out ptr3, out ptr4);
						if (operationStatus <= OperationStatus.DestinationTooSmall || (operationStatus == OperationStatus.NeedMoreData && !isFinalBlock))
						{
							break;
						}
						if (!replaceInvalidSequences)
						{
							operationStatus = OperationStatus.InvalidData;
							break;
						}
						destination = destination.Slice((int)((long)((byte*)ptr4 - (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(destination)))));
						if (2 >= destination.Length)
						{
							operationStatus = OperationStatus.DestinationTooSmall;
							break;
						}
						*destination[0] = 239;
						*destination[1] = 191;
						*destination[2] = 189;
						destination = destination.Slice(3);
						source = source.Slice((int)((long)(((byte*)ptr3 - (byte*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(source))) / 2)) + 1);
						operationStatus = OperationStatus.Done;
						ptr3 = (char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(source));
						ptr4 = (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(destination));
					}
					charsRead = (int)((long)(ptr3 - ptr));
					bytesWritten = (int)((long)(ptr4 - ptr2));
					return operationStatus;
				}
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x00165BC0 File Offset: 0x00164DC0
		public unsafe static OperationStatus ToUtf16(ReadOnlySpan<byte> source, Span<char> destination, out int bytesRead, out int charsWritten, bool replaceInvalidSequences = true, bool isFinalBlock = true)
		{
			int length = source.Length;
			int length2 = destination.Length;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(destination))
				{
					char* ptr2 = reference2;
					OperationStatus operationStatus = OperationStatus.Done;
					byte* ptr3 = ptr;
					char* ptr4 = ptr2;
					while (!source.IsEmpty)
					{
						operationStatus = Utf8Utility.TranscodeToUtf16((byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(source)), source.Length, (char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(destination)), destination.Length, out ptr3, out ptr4);
						if (operationStatus <= OperationStatus.DestinationTooSmall || (operationStatus == OperationStatus.NeedMoreData && !isFinalBlock))
						{
							break;
						}
						if (!replaceInvalidSequences)
						{
							operationStatus = OperationStatus.InvalidData;
							break;
						}
						destination = destination.Slice((int)((long)(((byte*)ptr4 - (byte*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(destination))) / 2)));
						if (destination.IsEmpty)
						{
							operationStatus = OperationStatus.DestinationTooSmall;
							break;
						}
						*destination[0] = '�';
						destination = destination.Slice(1);
						source = source.Slice((int)((long)((byte*)ptr3 - (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(source)))));
						Rune rune;
						int start;
						Rune.DecodeFromUtf8(source, out rune, out start);
						source = source.Slice(start);
						operationStatus = OperationStatus.Done;
						ptr3 = (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(source));
						ptr4 = (char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(destination));
					}
					bytesRead = (int)((long)(ptr3 - ptr));
					charsWritten = (int)((long)(ptr4 - ptr2));
					return operationStatus;
				}
			}
		}
	}
}
