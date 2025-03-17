using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000A2 RID: 162
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x000C45E4 File Offset: 0x000C37E4
		protected __ComObject()
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000C45EC File Offset: 0x000C37EC
		internal object GetData(object key)
		{
			object result = null;
			lock (this)
			{
				if (this.m_ObjectToDataMap != null)
				{
					result = this.m_ObjectToDataMap[key];
				}
			}
			return result;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000C463C File Offset: 0x000C383C
		internal bool SetData(object key, object data)
		{
			bool result = false;
			lock (this)
			{
				if (this.m_ObjectToDataMap == null)
				{
					this.m_ObjectToDataMap = new Hashtable();
				}
				if (this.m_ObjectToDataMap[key] == null)
				{
					this.m_ObjectToDataMap[key] = data;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000C46A4 File Offset: 0x000C38A4
		internal void ReleaseAllData()
		{
			lock (this)
			{
				if (this.m_ObjectToDataMap != null)
				{
					foreach (object obj in this.m_ObjectToDataMap.Values)
					{
						IDisposable disposable = obj as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
						__ComObject _ComObject = obj as __ComObject;
						if (_ComObject != null)
						{
							Marshal.ReleaseComObject(_ComObject);
						}
					}
					this.m_ObjectToDataMap = null;
				}
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000C4754 File Offset: 0x000C3954
		internal object GetEventProvider(RuntimeType t)
		{
			object data = this.GetData(t);
			if (data != null)
			{
				return data;
			}
			return this.CreateEventProvider(t);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000C4775 File Offset: 0x000C3975
		internal int ReleaseSelf()
		{
			return Marshal.InternalReleaseComObject(this);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000C477D File Offset: 0x000C397D
		internal void FinalReleaseSelf()
		{
			Marshal.InternalFinalReleaseComObject(this);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000C4788 File Offset: 0x000C3988
		private object CreateEventProvider(RuntimeType t)
		{
			object obj = Activator.CreateInstance(t, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[]
			{
				this
			}, null);
			if (!this.SetData(t, obj))
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				obj = this.GetData(t);
			}
			return obj;
		}

		// Token: 0x04000226 RID: 550
		private Hashtable m_ObjectToDataMap;
	}
}
