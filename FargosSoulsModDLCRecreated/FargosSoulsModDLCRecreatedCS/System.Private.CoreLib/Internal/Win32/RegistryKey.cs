using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security;
using Internal.Win32.SafeHandles;

namespace Internal.Win32
{
	// Token: 0x02000813 RID: 2067
	internal sealed class RegistryKey : IDisposable
	{
		// Token: 0x06006241 RID: 25153 RVA: 0x001D373F File Offset: 0x001D293F
		private RegistryKey(SafeRegistryHandle hkey)
		{
			this._hkey = hkey;
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x001D374E File Offset: 0x001D294E
		void IDisposable.Dispose()
		{
			if (this._hkey != null)
			{
				this._hkey.Dispose();
			}
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x001D3764 File Offset: 0x001D2964
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			int num = Interop.Advapi32.RegDeleteValue(this._hkey, name);
			if (num == 2 || num == 206)
			{
				if (throwOnMissingValue)
				{
					throw new ArgumentException(SR.Arg_RegSubKeyValueAbsent);
				}
			}
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x001D379A File Offset: 0x001D299A
		internal static RegistryKey OpenBaseKey(IntPtr hKey)
		{
			return new RegistryKey(new SafeRegistryHandle(hKey, false));
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x001D37A8 File Offset: 0x001D29A8
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x001D37B4 File Offset: 0x001D29B4
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			SafeRegistryHandle safeRegistryHandle;
			int num = Interop.Advapi32.RegOpenKeyEx(this._hkey, name, 0, writable ? 131103 : 131097, out safeRegistryHandle);
			if (num == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle);
			}
			if (num == 5 || num == 1346)
			{
				throw new SecurityException(SR.Security_RegistryPermission);
			}
			return null;
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x001D380C File Offset: 0x001D2A0C
		public string[] GetSubKeyNames()
		{
			List<string> list = new List<string>();
			char[] array = ArrayPool<char>.Shared.Rent(256);
			try
			{
				int length = array.Length;
				int num;
				while ((num = Interop.Advapi32.RegEnumKeyEx(this._hkey, list.Count, array, ref length, null, null, null, null)) != 259)
				{
					if (num == 0)
					{
						list.Add(new string(array, 0, length));
						length = array.Length;
					}
					else
					{
						RegistryKey.Win32Error(num, null);
					}
				}
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return list.ToArray();
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x001D389C File Offset: 0x001D2A9C
		public string[] GetValueNames()
		{
			List<string> list = new List<string>();
			char[] array = ArrayPool<char>.Shared.Rent(100);
			try
			{
				int length = array.Length;
				int num;
				while ((num = Interop.Advapi32.RegEnumValue(this._hkey, list.Count, array, ref length, IntPtr.Zero, null, null, null)) != 259)
				{
					if (num != 0)
					{
						if (num != 234)
						{
							RegistryKey.Win32Error(num, null);
						}
						else
						{
							char[] array2 = array;
							int num2 = array2.Length;
							array = null;
							ArrayPool<char>.Shared.Return(array2, false);
							array = ArrayPool<char>.Shared.Rent(checked(num2 * 2));
						}
					}
					else
					{
						list.Add(new string(array, 0, length));
					}
					length = array.Length;
				}
			}
			finally
			{
				if (array != null)
				{
					ArrayPool<char>.Shared.Return(array, false);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x001D3960 File Offset: 0x001D2B60
		public object GetValue(string name)
		{
			return this.GetValue(name, null);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x001D396C File Offset: 0x001D2B6C
		[return: NotNullIfNotNull("defaultValue")]
		public object GetValue(string name, object defaultValue)
		{
			object obj = defaultValue;
			int num = 0;
			int num2 = 0;
			int num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, null, ref num2);
			if (num3 != 0 && num3 != 234)
			{
				return obj;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			switch (num)
			{
			case 0:
			case 3:
			case 5:
				break;
			case 1:
			{
				char[] array;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException innerException)
						{
							throw new IOException(SR.Arg_RegGetOverflowBug, innerException);
						}
					}
					array = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array, ref num2);
				}
				if (array.Length != 0)
				{
					char[] array2 = array;
					if (array2[array2.Length - 1] == '\0')
					{
						return new string(array, 0, array.Length - 1);
					}
				}
				return new string(array);
			}
			case 2:
			{
				char[] array3;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException innerException2)
						{
							throw new IOException(SR.Arg_RegGetOverflowBug, innerException2);
						}
					}
					array3 = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array3, ref num2);
				}
				if (array3.Length != 0)
				{
					char[] array4 = array3;
					if (array4[array4.Length - 1] == '\0')
					{
						obj = new string(array3, 0, array3.Length - 1);
						goto IL_1A6;
					}
				}
				obj = new string(array3);
				IL_1A6:
				return Environment.ExpandEnvironmentVariables((string)obj);
			}
			case 4:
				if (num2 <= 4)
				{
					int num4 = 0;
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, ref num4, ref num2);
					return num4;
				}
				goto IL_8B;
			case 6:
			case 8:
			case 9:
			case 10:
				return obj;
			case 7:
			{
				char[] array5;
				checked
				{
					if (num2 % 2 == 1)
					{
						try
						{
							num2++;
						}
						catch (OverflowException innerException3)
						{
							throw new IOException(SR.Arg_RegGetOverflowBug, innerException3);
						}
					}
					array5 = new char[num2 / 2];
					num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array5, ref num2);
				}
				if (array5.Length != 0)
				{
					char[] array6 = array5;
					if (array6[array6.Length - 1] != '\0')
					{
						Array.Resize<char>(ref array5, array5.Length + 1);
					}
				}
				string[] array7 = Array.Empty<string>();
				int num5 = 0;
				int num6 = 0;
				int num7 = array5.Length;
				while (num3 == 0 && num6 < num7)
				{
					int num8 = num6;
					while (num8 < num7 && array5[num8] != '\0')
					{
						num8++;
					}
					string text = null;
					if (num8 < num7)
					{
						if (num8 - num6 > 0)
						{
							text = new string(array5, num6, num8 - num6);
						}
						else if (num8 != num7 - 1)
						{
							text = string.Empty;
						}
					}
					else
					{
						text = new string(array5, num6, num7 - num6);
					}
					num6 = num8 + 1;
					if (text != null)
					{
						if (array7.Length == num5)
						{
							Array.Resize<string>(ref array7, (num5 > 0) ? (num5 * 2) : 4);
						}
						array7[num5++] = text;
					}
				}
				Array.Resize<string>(ref array7, num5);
				return array7;
			}
			case 11:
				goto IL_8B;
			default:
				return obj;
			}
			IL_67:
			byte[] array8 = new byte[num2];
			num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, array8, ref num2);
			return array8;
			IL_8B:
			if (num2 > 8)
			{
				goto IL_67;
			}
			long num9 = 0L;
			num3 = Interop.Advapi32.RegQueryValueEx(this._hkey, name, null, ref num, ref num9, ref num2);
			obj = num9;
			return obj;
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x001D3C6C File Offset: 0x001D2E6C
		internal void SetValue(string name, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null && name.Length > 16383)
			{
				throw new ArgumentException(SR.Arg_RegValStrLenBug, "name");
			}
			int num = Interop.Advapi32.RegSetValueEx(this._hkey, name, 0, 1, value, checked(value.Length * 2 + 2));
			if (num != 0)
			{
				RegistryKey.Win32Error(num, null);
			}
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001D3CCB File Offset: 0x001D2ECB
		internal static void Win32Error(int errorCode, string str)
		{
			if (errorCode == 2)
			{
				throw new IOException(SR.Arg_RegKeyNotFound, errorCode);
			}
			if (errorCode != 5)
			{
				throw new IOException(Interop.Kernel32.GetMessage(errorCode), errorCode);
			}
			if (str != null)
			{
				throw new UnauthorizedAccessException(SR.Format(SR.UnauthorizedAccess_RegistryKeyGeneric_Key, str));
			}
			throw new UnauthorizedAccessException();
		}

		// Token: 0x04001D52 RID: 7506
		private readonly SafeRegistryHandle _hkey;
	}
}
