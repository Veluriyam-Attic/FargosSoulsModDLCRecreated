using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000583 RID: 1411
	internal sealed class RuntimeResourceSet : ResourceSet, IEnumerable
	{
		// Token: 0x0600486D RID: 18541 RVA: 0x00181A44 File Offset: 0x00180C44
		internal RuntimeResourceSet(string fileName) : base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this._defaultReader = new ResourceReader(stream, this._resCache, false);
			this.Reader = this._defaultReader;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x00181A91 File Offset: 0x00180C91
		internal RuntimeResourceSet(Stream stream, bool permitDeserialization = false) : base(false)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._defaultReader = new ResourceReader(stream, this._resCache, permitDeserialization);
			this.Reader = this._defaultReader;
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x00181ACC File Offset: 0x00180CCC
		protected override void Dispose(bool disposing)
		{
			if (this.Reader == null)
			{
				return;
			}
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				lock (reader)
				{
					this._resCache = null;
					if (this._defaultReader != null)
					{
						this._defaultReader.Close();
						this._defaultReader = null;
					}
					this._caseInsensitiveTable = null;
					base.Dispose(disposing);
					return;
				}
			}
			this._resCache = null;
			this._caseInsensitiveTable = null;
			this._defaultReader = null;
			base.Dispose(disposing);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x00181B60 File Offset: 0x00180D60
		public override IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00181B60 File Offset: 0x00180D60
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00181B68 File Offset: 0x00180D68
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			IResourceReader reader = this.Reader;
			if (reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
			}
			return reader.GetEnumerator();
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x00181B9C File Offset: 0x00180D9C
		public override string GetString(string key)
		{
			object @object = this.GetObject(key, false, true);
			return (string)@object;
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00181BBC File Offset: 0x00180DBC
		public override string GetString(string key, bool ignoreCase)
		{
			object @object = this.GetObject(key, ignoreCase, true);
			return (string)@object;
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00181BD9 File Offset: 0x00180DD9
		public override object GetObject(string key)
		{
			return this.GetObject(key, false, false);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00181BE4 File Offset: 0x00180DE4
		public override object GetObject(string key, bool ignoreCase)
		{
			return this.GetObject(key, ignoreCase, false);
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x00181BF0 File Offset: 0x00180DF0
		private object GetObject(string key, bool ignoreCase, bool isString)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.Reader == null || this._resCache == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
			}
			object obj = null;
			IResourceReader reader = this.Reader;
			object result;
			lock (reader)
			{
				if (this.Reader == null)
				{
					throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
				}
				ResourceLocator resourceLocator;
				if (this._defaultReader != null)
				{
					int num = -1;
					if (this._resCache.TryGetValue(key, out resourceLocator))
					{
						obj = resourceLocator.Value;
						num = resourceLocator.DataPosition;
					}
					if (num == -1 && obj == null)
					{
						num = this._defaultReader.FindPosForResource(key);
					}
					if (num != -1 && obj == null)
					{
						ResourceTypeCode value;
						if (isString)
						{
							obj = this._defaultReader.LoadString(num);
							value = ResourceTypeCode.String;
						}
						else
						{
							obj = this._defaultReader.LoadObject(num, out value);
						}
						resourceLocator = new ResourceLocator(num, ResourceLocator.CanCache(value) ? obj : null);
						Dictionary<string, ResourceLocator> resCache = this._resCache;
						lock (resCache)
						{
							this._resCache[key] = resourceLocator;
						}
					}
					if (obj != null || !ignoreCase)
					{
						return obj;
					}
				}
				if (!this._haveReadFromReader)
				{
					if (ignoreCase && this._caseInsensitiveTable == null)
					{
						this._caseInsensitiveTable = new Dictionary<string, ResourceLocator>(StringComparer.OrdinalIgnoreCase);
					}
					if (this._defaultReader == null)
					{
						IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
						while (enumerator.MoveNext())
						{
							DictionaryEntry entry = enumerator.Entry;
							string key2 = (string)entry.Key;
							ResourceLocator value2 = new ResourceLocator(-1, entry.Value);
							this._resCache.Add(key2, value2);
							if (ignoreCase)
							{
								this._caseInsensitiveTable.Add(key2, value2);
							}
						}
						if (!ignoreCase)
						{
							this.Reader.Close();
						}
					}
					else
					{
						ResourceReader.ResourceEnumerator enumeratorInternal = this._defaultReader.GetEnumeratorInternal();
						while (enumeratorInternal.MoveNext())
						{
							string key3 = (string)enumeratorInternal.Key;
							int dataPosition = enumeratorInternal.DataPosition;
							ResourceLocator value3 = new ResourceLocator(dataPosition, null);
							this._caseInsensitiveTable.Add(key3, value3);
						}
					}
					this._haveReadFromReader = true;
				}
				object obj2 = null;
				bool flag3 = false;
				bool keyInWrongCase = false;
				if (this._defaultReader != null && this._resCache.TryGetValue(key, out resourceLocator))
				{
					flag3 = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, keyInWrongCase);
				}
				if (!flag3 && ignoreCase && this._caseInsensitiveTable.TryGetValue(key, out resourceLocator))
				{
					keyInWrongCase = true;
					obj2 = this.ResolveResourceLocator(resourceLocator, key, this._resCache, keyInWrongCase);
				}
				result = obj2;
			}
			return result;
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x00181EAC File Offset: 0x001810AC
		private object ResolveResourceLocator(ResourceLocator resLocation, string key, Dictionary<string, ResourceLocator> copyOfCache, bool keyInWrongCase)
		{
			object obj = resLocation.Value;
			if (obj == null)
			{
				IResourceReader reader = this.Reader;
				ResourceTypeCode value;
				lock (reader)
				{
					obj = this._defaultReader.LoadObject(resLocation.DataPosition, out value);
				}
				if (!keyInWrongCase && ResourceLocator.CanCache(value))
				{
					resLocation.Value = obj;
					copyOfCache[key] = resLocation;
				}
			}
			return obj;
		}

		// Token: 0x040011AF RID: 4527
		private Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x040011B0 RID: 4528
		private ResourceReader _defaultReader;

		// Token: 0x040011B1 RID: 4529
		private Dictionary<string, ResourceLocator> _caseInsensitiveTable;

		// Token: 0x040011B2 RID: 4530
		private bool _haveReadFromReader;
	}
}
