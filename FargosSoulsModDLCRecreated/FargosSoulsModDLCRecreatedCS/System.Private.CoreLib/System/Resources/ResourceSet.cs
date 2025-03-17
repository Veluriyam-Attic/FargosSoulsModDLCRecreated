using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000581 RID: 1409
	[NullableContext(1)]
	[Nullable(0)]
	public class ResourceSet : IDisposable, IEnumerable
	{
		// Token: 0x06004859 RID: 18521 RVA: 0x00181775 File Offset: 0x00180975
		protected ResourceSet()
		{
			this.Table = new Hashtable();
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x000ABD27 File Offset: 0x000AAF27
		internal ResourceSet(bool junk)
		{
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00181788 File Offset: 0x00180988
		public ResourceSet(string fileName) : this()
		{
			this.Reader = new ResourceReader(fileName);
			this.ReadResources();
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x001817A2 File Offset: 0x001809A2
		public ResourceSet(Stream stream) : this()
		{
			this.Reader = new ResourceReader(stream);
			this.ReadResources();
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x001817BC File Offset: 0x001809BC
		public ResourceSet(IResourceReader reader) : this()
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.ReadResources();
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x001817E0 File Offset: 0x001809E0
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x001817EC File Offset: 0x001809EC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x001817E0 File Offset: 0x001809E0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00181828 File Offset: 0x00180A28
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x00181834 File Offset: 0x00180A34
		[DynamicDependency(DynamicallyAccessedMemberTypes.PublicConstructors, "System.Resources.ResourceWriter", "System.Resources.Writer")]
		public virtual Type GetDefaultWriter()
		{
			return Type.GetType("System.Resources.ResourceWriter, System.Resources.Writer", true);
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x00181841 File Offset: 0x00180A41
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x00181841 File Offset: 0x00180A41
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x0018184C File Offset: 0x00180A4C
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
			}
			return table.GetEnumerator();
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x00181878 File Offset: 0x00180A78
		[return: Nullable(2)]
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string result;
			try
			{
				result = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotString_Name, name));
			}
			return result;
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x001818BC File Offset: 0x00180ABC
		[return: Nullable(2)]
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotString_Name, name));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string result;
			try
			{
				result = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotString_Name, name));
			}
			return result;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x00181934 File Offset: 0x00180B34
		[return: Nullable(2)]
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x00181940 File Offset: 0x00180B40
		[return: Nullable(2)]
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00181964 File Offset: 0x00180B64
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x001819A0 File Offset: 0x00180BA0
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
			}
			return table[name];
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x001819D8 File Offset: 0x00180BD8
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, SR.ObjectDisposed_ResourceSet);
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		// Token: 0x04001196 RID: 4502
		protected IResourceReader Reader;

		// Token: 0x04001197 RID: 4503
		internal Hashtable Table;

		// Token: 0x04001198 RID: 4504
		private Hashtable _caseInsensitiveTable;
	}
}
