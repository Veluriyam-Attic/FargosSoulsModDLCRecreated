using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000147 RID: 327
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000DB6F8 File Offset: 0x000DA8F8
		public static bool EnableUnsafeUTF7Encoding
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("System.Text.Encoding.EnableUnsafeUTF7Encoding", ref LocalAppContextSwitches.s_enableUnsafeUTF7Encoding);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x000DB709 File Offset: 0x000DA909
		public static bool EnforceJapaneseEraYearRanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.EnforceJapaneseEraYearRanges", ref LocalAppContextSwitches.s_enforceJapaneseEraYearRanges);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x000DB71A File Offset: 0x000DA91A
		public static bool FormatJapaneseFirstYearAsANumber
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.FormatJapaneseFirstYearAsANumber", ref LocalAppContextSwitches.s_formatJapaneseFirstYearAsANumber);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x000DB72B File Offset: 0x000DA92B
		public static bool EnforceLegacyJapaneseDateParsing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.EnforceLegacyJapaneseDateParsing", ref LocalAppContextSwitches.s_enforceLegacyJapaneseDateParsing);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x000DB73C File Offset: 0x000DA93C
		public static bool PreserveEventListnerObjectIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity", ref LocalAppContextSwitches.s_preserveEventListnerObjectIdentity);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x000DB74D File Offset: 0x000DA94D
		public static bool SerializationGuard
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return LocalAppContextSwitches.GetCachedSwitchValue("Switch.System.Runtime.Serialization.SerializationGuard", ref LocalAppContextSwitches.s_serializationGuard);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x000DB75E File Offset: 0x000DA95E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int cachedSwitchValue)
		{
			return cachedSwitchValue >= 0 && (cachedSwitchValue > 0 || LocalAppContextSwitches.GetCachedSwitchValueInternal(switchName, ref cachedSwitchValue));
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000DB778 File Offset: 0x000DA978
		private static bool GetCachedSwitchValueInternal(string switchName, ref int cachedSwitchValue)
		{
			bool switchDefaultValue;
			if (!AppContext.TryGetSwitch(switchName, out switchDefaultValue))
			{
				switchDefaultValue = LocalAppContextSwitches.GetSwitchDefaultValue(switchName);
			}
			bool flag;
			AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out flag);
			if (!flag)
			{
				cachedSwitchValue = (switchDefaultValue ? 1 : -1);
			}
			return switchDefaultValue;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000DB7B2 File Offset: 0x000DA9B2
		private static bool GetSwitchDefaultValue(string switchName)
		{
			return switchName == "Switch.System.Runtime.Serialization.SerializationGuard" || switchName == "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization";
		}

		// Token: 0x04000414 RID: 1044
		private static int s_enableUnsafeUTF7Encoding;

		// Token: 0x04000415 RID: 1045
		private static int s_enforceJapaneseEraYearRanges;

		// Token: 0x04000416 RID: 1046
		private static int s_formatJapaneseFirstYearAsANumber;

		// Token: 0x04000417 RID: 1047
		private static int s_enforceLegacyJapaneseDateParsing;

		// Token: 0x04000418 RID: 1048
		private static int s_preserveEventListnerObjectIdentity;

		// Token: 0x04000419 RID: 1049
		private static int s_serializationGuard;
	}
}
