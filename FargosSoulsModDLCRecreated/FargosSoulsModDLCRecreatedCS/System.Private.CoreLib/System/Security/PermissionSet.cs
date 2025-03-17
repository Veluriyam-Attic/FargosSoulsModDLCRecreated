using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003B8 RID: 952
	[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[Nullable(0)]
	[NullableContext(2)]
	public class PermissionSet : ICollection, IEnumerable, IDeserializationCallback, ISecurityEncodable, IStackWalk
	{
		// Token: 0x06003117 RID: 12567 RVA: 0x000ABD27 File Offset: 0x000AAF27
		public PermissionSet(PermissionState state)
		{
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000ABD27 File Offset: 0x000AAF27
		public PermissionSet(PermissionSet permSet)
		{
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000AC098 File Offset: 0x000AB298
		[Nullable(1)]
		public virtual object SyncRoot
		{
			[NullableContext(1)]
			get
			{
				return this;
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x001686A2 File Offset: 0x001678A2
		public IPermission AddPermission(IPermission perm)
		{
			return this.AddPermissionImpl(perm);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000C26FD File Offset: 0x000C18FD
		protected virtual IPermission AddPermissionImpl(IPermission perm)
		{
			return null;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void Assert()
		{
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool ContainsNonCodeAccessPermissions()
		{
			return false;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000C7F68 File Offset: 0x000C7168
		[NullableContext(1)]
		[Obsolete]
		public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_CAS);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x001686AB File Offset: 0x001678AB
		[NullableContext(1)]
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(1)]
		public virtual void CopyTo(Array array, int index)
		{
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void Demand()
		{
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000C7F68 File Offset: 0x000C7168
		[Obsolete]
		public void Deny()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_CAS);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x001686B3 File Offset: 0x001678B3
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(1)]
		public virtual void FromXml(SecurityElement et)
		{
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x001686BC File Offset: 0x001678BC
		[NullableContext(1)]
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x001686C4 File Offset: 0x001678C4
		[NullableContext(1)]
		protected virtual IEnumerator GetEnumeratorImpl()
		{
			return Array.Empty<object>().GetEnumerator();
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x001686D0 File Offset: 0x001678D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x001686D8 File Offset: 0x001678D8
		public IPermission GetPermission(Type permClass)
		{
			return this.GetPermissionImpl(permClass);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000C26FD File Offset: 0x000C18FD
		protected virtual IPermission GetPermissionImpl(Type permClass)
		{
			return null;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000C26FD File Offset: 0x000C18FD
		public PermissionSet Intersect(PermissionSet other)
		{
			return null;
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsEmpty()
		{
			return false;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsSubsetOf(PermissionSet target)
		{
			return false;
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsUnrestricted()
		{
			return false;
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000C7F68 File Offset: 0x000C7168
		public void PermitOnly()
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_CAS);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x001686E1 File Offset: 0x001678E1
		public IPermission RemovePermission(Type permClass)
		{
			return this.RemovePermissionImpl(permClass);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000C26FD File Offset: 0x000C18FD
		protected virtual IPermission RemovePermissionImpl(Type permClass)
		{
			return null;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000AB30B File Offset: 0x000AA50B
		public static void RevertAssert()
		{
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x001686EA File Offset: 0x001678EA
		public IPermission SetPermission(IPermission perm)
		{
			return this.SetPermissionImpl(perm);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000C26FD File Offset: 0x000C18FD
		protected virtual IPermission SetPermissionImpl(IPermission perm)
		{
			return null;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000AB30B File Offset: 0x000AA50B
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x001686F3 File Offset: 0x001678F3
		[NullableContext(1)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000C26FD File Offset: 0x000C18FD
		public virtual SecurityElement ToXml()
		{
			return null;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000C26FD File Offset: 0x000C18FD
		public PermissionSet Union(PermissionSet other)
		{
			return null;
		}
	}
}
