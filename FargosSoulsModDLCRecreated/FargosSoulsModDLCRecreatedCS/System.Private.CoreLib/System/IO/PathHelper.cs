using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020006C1 RID: 1729
	internal static class PathHelper
	{
		// Token: 0x06005836 RID: 22582 RVA: 0x001AF77C File Offset: 0x001AE97C
		internal unsafe static string Normalize(string path)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			PathHelper.GetFullPathName(path.AsSpan(), ref valueStringBuilder);
			string result = (valueStringBuilder.AsSpan().IndexOf('~') >= 0) ? PathHelper.TryExpandShortFileName(ref valueStringBuilder, path) : (valueStringBuilder.AsSpan().Equals(path.AsSpan(), StringComparison.Ordinal) ? path : valueStringBuilder.ToString());
			valueStringBuilder.Dispose();
			return result;
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x001AF7FC File Offset: 0x001AE9FC
		internal unsafe static string Normalize(ref ValueStringBuilder path)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			PathHelper.GetFullPathName(path.AsSpan(true), ref valueStringBuilder);
			string result = (valueStringBuilder.AsSpan().IndexOf('~') >= 0) ? PathHelper.TryExpandShortFileName(ref valueStringBuilder, null) : valueStringBuilder.ToString();
			valueStringBuilder.Dispose();
			return result;
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x001AF864 File Offset: 0x001AEA64
		private static void GetFullPathName(ReadOnlySpan<char> path, ref ValueStringBuilder builder)
		{
			uint fullPathNameW;
			while ((ulong)(fullPathNameW = Interop.Kernel32.GetFullPathNameW(MemoryMarshal.GetReference<char>(path), (uint)builder.Capacity, builder.GetPinnableReference(), IntPtr.Zero)) > (ulong)((long)builder.Capacity))
			{
				builder.EnsureCapacity(checked((int)fullPathNameW));
			}
			if (fullPathNameW == 0U)
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 0)
				{
					num = 161;
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, path.ToString());
			}
			builder.Length = (int)fullPathNameW;
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x001AF8D0 File Offset: 0x001AEAD0
		internal static int PrependDevicePathChars(ref ValueStringBuilder content, bool isDosUnc, ref ValueStringBuilder buffer)
		{
			int num = content.Length;
			num += (isDosUnc ? 6 : 4);
			buffer.EnsureCapacity(num + 1);
			buffer.Length = 0;
			if (isDosUnc)
			{
				buffer.Append("\\\\?\\UNC\\");
				buffer.Append(content.AsSpan(2));
				return 6;
			}
			buffer.Append("\\\\?\\");
			buffer.Append(content.AsSpan());
			return 4;
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001AF934 File Offset: 0x001AEB34
		internal unsafe static string TryExpandShortFileName(ref ValueStringBuilder outputBuilder, string originalPath)
		{
			int num = PathInternal.GetRootLength(outputBuilder.AsSpan());
			bool flag = PathInternal.IsDevice(outputBuilder.AsSpan());
			ValueStringBuilder valueStringBuilder = default(ValueStringBuilder);
			bool flag2 = false;
			int num2 = 0;
			bool flag3 = false;
			if (flag)
			{
				valueStringBuilder.Append(outputBuilder.AsSpan());
				if (*outputBuilder[2] == '.')
				{
					flag3 = true;
					*valueStringBuilder[2] = '?';
				}
			}
			else
			{
				flag2 = (!PathInternal.IsDevice(outputBuilder.AsSpan()) && outputBuilder.Length > 1 && *outputBuilder[0] == '\\' && *outputBuilder[1] == '\\');
				num2 = PathHelper.PrependDevicePathChars(ref outputBuilder, flag2, ref valueStringBuilder);
			}
			num += num2;
			int length = valueStringBuilder.Length;
			bool flag4 = false;
			int num3 = valueStringBuilder.Length - 1;
			while (!flag4)
			{
				uint longPathNameW = Interop.Kernel32.GetLongPathNameW(valueStringBuilder.GetPinnableReference(true), outputBuilder.GetPinnableReference(), (uint)outputBuilder.Capacity);
				if (*valueStringBuilder[num3] == '\0')
				{
					*valueStringBuilder[num3] = '\\';
				}
				if (longPathNameW == 0U)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 2 && lastWin32Error != 3)
					{
						break;
					}
					num3--;
					while (num3 > num && *valueStringBuilder[num3] != '\\')
					{
						num3--;
					}
					if (num3 == num)
					{
						break;
					}
					*valueStringBuilder[num3] = '\0';
				}
				else if ((ulong)longPathNameW > (ulong)((long)outputBuilder.Capacity))
				{
					outputBuilder.EnsureCapacity(checked((int)longPathNameW));
				}
				else
				{
					flag4 = true;
					outputBuilder.Length = checked((int)longPathNameW);
					if (num3 < length - 1)
					{
						outputBuilder.Append(valueStringBuilder.AsSpan(num3, valueStringBuilder.Length - num3));
					}
				}
			}
			ref ValueStringBuilder ptr = ref flag4 ? ref outputBuilder : ref valueStringBuilder;
			if (flag3)
			{
				*ptr[2] = '.';
			}
			if (flag2)
			{
				*ptr[6] = '\\';
			}
			ReadOnlySpan<char> span = ptr.AsSpan(num2);
			string result = (originalPath != null && span.Equals(originalPath.AsSpan(), StringComparison.Ordinal)) ? originalPath : span.ToString();
			valueStringBuilder.Dispose();
			return result;
		}
	}
}
