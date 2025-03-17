using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security
{
	// Token: 0x020003BD RID: 957
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class SecurityElement
	{
		// Token: 0x06003159 RID: 12633 RVA: 0x0016911E File Offset: 0x0016831E
		[NullableContext(1)]
		public SecurityElement(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidElementTag, tag));
			}
			this._tag = tag;
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x00169154 File Offset: 0x00168354
		[NullableContext(1)]
		public SecurityElement(string tag, [Nullable(2)] string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidElementTag, tag));
			}
			if (text != null && !SecurityElement.IsValidText(text))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidElementText, text));
			}
			this._tag = tag;
			this._text = text;
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x001691B8 File Offset: 0x001683B8
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x001691C0 File Offset: 0x001683C0
		[Nullable(1)]
		public string Tag
		{
			[NullableContext(1)]
			get
			{
				return this._tag;
			}
			[NullableContext(1)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidElementTag, value));
				}
				this._tag = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x001691F0 File Offset: 0x001683F0
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x00169260 File Offset: 0x00168460
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null || this._attributes.Count == 0)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this._attributes.Count / 2);
				int count = this._attributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					hashtable.Add(this._attributes[i], this._attributes[i + 1]);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this._attributes = null;
					return;
				}
				ArrayList arrayList = new ArrayList(value.Count);
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string text2 = (string)enumerator.Value;
					if (!SecurityElement.IsValidAttributeName(text))
					{
						throw new ArgumentException(SR.Format(SR.Argument_InvalidElementName, text));
					}
					if (!SecurityElement.IsValidAttributeValue(text2))
					{
						throw new ArgumentException(SR.Format(SR.Argument_InvalidElementValue, text2));
					}
					arrayList.Add(text);
					arrayList.Add(text2);
				}
				this._attributes = arrayList;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x001692FE File Offset: 0x001684FE
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x0016930B File Offset: 0x0016850B
		public string Text
		{
			get
			{
				return SecurityElement.Unescape(this._text);
			}
			set
			{
				if (value == null)
				{
					this._text = null;
					return;
				}
				if (!SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidElementTag, value));
				}
				this._text = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x00169338 File Offset: 0x00168538
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x00169340 File Offset: 0x00168540
		public ArrayList Children
		{
			get
			{
				return this._children;
			}
			set
			{
				if (value != null && value.Contains(null))
				{
					throw new ArgumentException(SR.ArgumentNull_Child);
				}
				this._children = value;
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00169360 File Offset: 0x00168560
		internal void AddAttributeSafe(string name, string value)
		{
			if (this._attributes == null)
			{
				this._attributes = new ArrayList(8);
			}
			else
			{
				int count = this._attributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string a = (string)this._attributes[i];
					if (string.Equals(a, name))
					{
						throw new ArgumentException(SR.Argument_AttributeNamesMustBeUnique);
					}
				}
			}
			this._attributes.Add(name);
			this._attributes.Add(value);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x001693DC File Offset: 0x001685DC
		[NullableContext(1)]
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!SecurityElement.IsValidAttributeName(name))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidElementName, name));
			}
			if (!SecurityElement.IsValidAttributeValue(value))
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidElementValue, value));
			}
			this.AddAttributeSafe(name, value);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0016943F File Offset: 0x0016863F
		[NullableContext(1)]
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this._children == null)
			{
				this._children = new ArrayList(1);
			}
			this._children.Add(child);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00169470 File Offset: 0x00168670
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this._tag, other._tag))
			{
				return false;
			}
			if (!string.Equals(this._text, other._text))
			{
				return false;
			}
			if (this._attributes == null || other._attributes == null)
			{
				if (this._attributes != other._attributes)
				{
					return false;
				}
			}
			else
			{
				int count = this._attributes.Count;
				if (count != other._attributes.Count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					string a = (string)this._attributes[i];
					string b = (string)other._attributes[i];
					if (!string.Equals(a, b))
					{
						return false;
					}
				}
			}
			if (this._children == null || other._children == null)
			{
				if (this._children != other._children)
				{
					return false;
				}
			}
			else
			{
				if (this._children.Count != other._children.Count)
				{
					return false;
				}
				IEnumerator enumerator = this._children.GetEnumerator();
				IEnumerator enumerator2 = other._children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator2.MoveNext();
					SecurityElement securityElement = (SecurityElement)enumerator.Current;
					SecurityElement other2 = (SecurityElement)enumerator2.Current;
					if (securityElement == null || !securityElement.Equal(other2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x001695B8 File Offset: 0x001687B8
		[NullableContext(1)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this._tag, this._text)
			{
				_children = ((this._children == null) ? null : new ArrayList(this._children)),
				_attributes = ((this._attributes == null) ? null : new ArrayList(this._attributes))
			};
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00169610 File Offset: 0x00168810
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x00169625 File Offset: 0x00168825
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x0016963A File Offset: 0x0016883A
		public static bool IsValidAttributeName(string name)
		{
			return SecurityElement.IsValidTag(name);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00169642 File Offset: 0x00168842
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00169658 File Offset: 0x00168858
		private static string GetEscapeSequence(char c)
		{
			int num = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num; i += 2)
			{
				string text = SecurityElement.s_escapeStringPairs[i];
				string result = SecurityElement.s_escapeStringPairs[i + 1];
				if (text[0] == c)
				{
					return result;
				}
			}
			return c.ToString();
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x001696A0 File Offset: 0x001688A0
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOfAny(SecurityElement.s_escapeChars, num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetEscapeSequence(str[num2]));
				num = num2 + 1;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00169714 File Offset: 0x00168914
		private static string GetUnescapeSequence(string str, int index, out int newIndex)
		{
			int num = str.Length - index;
			int num2 = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num2; i += 2)
			{
				string result = SecurityElement.s_escapeStringPairs[i];
				string text = SecurityElement.s_escapeStringPairs[i + 1];
				int length = text.Length;
				if (length <= num && string.Compare(text, 0, str, index, length, StringComparison.Ordinal) == 0)
				{
					newIndex = index + text.Length;
					return result;
				}
			}
			newIndex = index + 1;
			return str[index].ToString();
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00169790 File Offset: 0x00168990
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOf('&', num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, num2, out num));
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x001697FC File Offset: 0x001689FC
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString(stringBuilder, delegate(object obj, string str)
			{
				((StringBuilder)obj).Append(str);
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0016983C File Offset: 0x00168A3C
		private void ToString(object obj, Action<object, string> write)
		{
			write(obj, "<");
			write(obj, this._tag);
			if (this._attributes != null && this._attributes.Count > 0)
			{
				write(obj, " ");
				int count = this._attributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string arg = (string)this._attributes[i];
					string arg2 = (string)this._attributes[i + 1];
					write(obj, arg);
					write(obj, "=\"");
					write(obj, arg2);
					write(obj, "\"");
					if (i != this._attributes.Count - 2)
					{
						write(obj, "\r\n");
					}
				}
			}
			if (this._text == null && (this._children == null || this._children.Count == 0))
			{
				write(obj, "/>");
				write(obj, "\r\n");
				return;
			}
			write(obj, ">");
			write(obj, this._text);
			if (this._children != null)
			{
				write(obj, "\r\n");
				for (int j = 0; j < this._children.Count; j++)
				{
					((SecurityElement)this._children[j]).ToString(obj, write);
				}
			}
			write(obj, "</");
			write(obj, this._tag);
			write(obj, ">");
			write(obj, "\r\n");
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x001699D4 File Offset: 0x00168BD4
		[NullableContext(1)]
		[return: Nullable(2)]
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._attributes == null)
			{
				return null;
			}
			int count = this._attributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string a = (string)this._attributes[i];
				if (string.Equals(a, name))
				{
					string str = (string)this._attributes[i + 1];
					return SecurityElement.Unescape(str);
				}
			}
			return null;
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00169A48 File Offset: 0x00168C48
		[NullableContext(1)]
		[return: Nullable(2)]
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this._children == null)
			{
				return null;
			}
			foreach (object obj in this._children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement != null && string.Equals(securityElement.Tag, tag))
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x00169ACC File Offset: 0x00168CCC
		[NullableContext(1)]
		[return: Nullable(2)]
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (string.Equals(this._tag, tag))
			{
				return SecurityElement.Unescape(this._text);
			}
			if (this._children == null)
			{
				return null;
			}
			foreach (object obj in this.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				string text = (securityElement != null) ? securityElement.SearchForTextOfTag(tag) : null;
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x00169B6C File Offset: 0x00168D6C
		[NullableContext(1)]
		[return: Nullable(2)]
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			return null;
		}

		// Token: 0x04000D88 RID: 3464
		internal string _tag;

		// Token: 0x04000D89 RID: 3465
		internal string _text;

		// Token: 0x04000D8A RID: 3466
		private ArrayList _children;

		// Token: 0x04000D8B RID: 3467
		internal ArrayList _attributes;

		// Token: 0x04000D8C RID: 3468
		private static readonly char[] s_tagIllegalCharacters = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04000D8D RID: 3469
		private static readonly char[] s_textIllegalCharacters = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x04000D8E RID: 3470
		private static readonly char[] s_valueIllegalCharacters = new char[]
		{
			'<',
			'>',
			'"'
		};

		// Token: 0x04000D8F RID: 3471
		private static readonly char[] s_escapeChars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x04000D90 RID: 3472
		private static readonly string[] s_escapeStringPairs = new string[]
		{
			"<",
			"&lt;",
			">",
			"&gt;",
			"\"",
			"&quot;",
			"'",
			"&apos;",
			"&",
			"&amp;"
		};
	}
}
