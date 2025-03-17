using System;

namespace System.IO
{
	// Token: 0x020006C2 RID: 1730
	internal struct DisableMediaInsertionPrompt : IDisposable
	{
		// Token: 0x0600583B RID: 22587 RVA: 0x001AFB1C File Offset: 0x001AED1C
		public static DisableMediaInsertionPrompt Create()
		{
			DisableMediaInsertionPrompt result = default(DisableMediaInsertionPrompt);
			result._disableSuccess = Interop.Kernel32.SetThreadErrorMode(1U, out result._oldMode);
			return result;
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x001AFB48 File Offset: 0x001AED48
		public void Dispose()
		{
			if (this._disableSuccess)
			{
				uint num;
				Interop.Kernel32.SetThreadErrorMode(this._oldMode, out num);
			}
		}

		// Token: 0x0400193D RID: 6461
		private bool _disableSuccess;

		// Token: 0x0400193E RID: 6462
		private uint _oldMode;
	}
}
