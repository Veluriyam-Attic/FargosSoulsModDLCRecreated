using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000377 RID: 887
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class EncodingProvider
	{
		// Token: 0x06002EC5 RID: 11973 RVA: 0x000ABD27 File Offset: 0x000AAF27
		public EncodingProvider()
		{
		}

		// Token: 0x06002EC6 RID: 11974
		[return: Nullable(2)]
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06002EC7 RID: 11975
		[NullableContext(2)]
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0015DDD4 File Offset: 0x0015CFD4
		[return: Nullable(2)]
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)encoding.Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0015DE08 File Offset: 0x0015D008
		[return: Nullable(2)]
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)encoding.Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0015DE3B File Offset: 0x0015D03B
		public virtual IEnumerable<EncodingInfo> GetEncodings()
		{
			return Array.Empty<EncodingInfo>();
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0015DE44 File Offset: 0x0015D044
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[]
					{
						provider
					};
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					EncodingProvider[] array2 = array;
					array2[array2.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0015DEF0 File Offset: 0x0015D0F0
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			EncodingProvider[] array = EncodingProvider.s_providers;
			if (array == null)
			{
				return null;
			}
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0015DF30 File Offset: 0x0015D130
		internal static Dictionary<int, EncodingInfo> GetEncodingListFromProviders()
		{
			EncodingProvider[] array = EncodingProvider.s_providers;
			if (array == null)
			{
				return null;
			}
			Dictionary<int, EncodingInfo> dictionary = new Dictionary<int, EncodingInfo>();
			foreach (EncodingProvider encodingProvider in array)
			{
				IEnumerable<EncodingInfo> encodings = encodingProvider.GetEncodings();
				if (encodings != null)
				{
					foreach (EncodingInfo encodingInfo in encodings)
					{
						dictionary.TryAdd(encodingInfo.CodePage, encodingInfo);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0015DFC0 File Offset: 0x0015D1C0
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0015E008 File Offset: 0x0015D208
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0015E050 File Offset: 0x0015D250
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x04000CE8 RID: 3304
		private static readonly object s_InternalSyncObject = new object();

		// Token: 0x04000CE9 RID: 3305
		private static volatile EncodingProvider[] s_providers;
	}
}
