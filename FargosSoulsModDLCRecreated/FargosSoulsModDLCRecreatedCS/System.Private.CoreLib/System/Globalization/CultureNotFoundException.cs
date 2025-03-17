using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020001F2 RID: 498
	[Nullable(0)]
	[NullableContext(2)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class CultureNotFoundException : ArgumentException
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x001277D9 File Offset: 0x001269D9
		public CultureNotFoundException() : base(CultureNotFoundException.DefaultMessage)
		{
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x001277E6 File Offset: 0x001269E6
		public CultureNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x001277EF File Offset: 0x001269EF
		public CultureNotFoundException(string paramName, string message) : base(message, paramName)
		{
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x001277F9 File Offset: 0x001269F9
		public CultureNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00127803 File Offset: 0x00126A03
		public CultureNotFoundException(string paramName, string invalidCultureName, string message) : base(message, paramName)
		{
			this._invalidCultureName = invalidCultureName;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00127814 File Offset: 0x00126A14
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException) : base(message, innerException)
		{
			this._invalidCultureName = invalidCultureName;
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00127825 File Offset: 0x00126A25
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException) : base(message, innerException)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0012783B File Offset: 0x00126A3B
		public CultureNotFoundException(string paramName, int invalidCultureId, string message) : base(message, paramName)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00127854 File Offset: 0x00126A54
		[NullableContext(1)]
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this._invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x001278AC File Offset: 0x00126AAC
		[NullableContext(1)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvalidCultureId", this._invalidCultureId, typeof(int?));
			info.AddValue("InvalidCultureName", this._invalidCultureName, typeof(string));
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x001278FC File Offset: 0x00126AFC
		public virtual int? InvalidCultureId
		{
			get
			{
				return this._invalidCultureId;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00127904 File Offset: 0x00126B04
		public virtual string InvalidCultureName
		{
			get
			{
				return this._invalidCultureName;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0012790C File Offset: 0x00126B0C
		[Nullable(1)]
		private static string DefaultMessage
		{
			get
			{
				return SR.Argument_CultureNotSupported;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00127914 File Offset: 0x00126B14
		private string FormattedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId == null)
				{
					return this.InvalidCultureName;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0012795C File Offset: 0x00126B5C
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				string message = base.Message;
				if (this._invalidCultureId == null && this._invalidCultureName == null)
				{
					return message;
				}
				string text = SR.Format(SR.Argument_CultureInvalidIdentifier, this.FormattedInvalidCultureId);
				if (message == null)
				{
					return text;
				}
				return message + "\r\n" + text;
			}
		}

		// Token: 0x0400078A RID: 1930
		private readonly string _invalidCultureName;

		// Token: 0x0400078B RID: 1931
		private readonly int? _invalidCultureId;
	}
}
