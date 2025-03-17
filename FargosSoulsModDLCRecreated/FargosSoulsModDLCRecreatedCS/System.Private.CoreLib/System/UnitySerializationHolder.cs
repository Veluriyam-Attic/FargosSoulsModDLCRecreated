using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public sealed class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x06001ADA RID: 6874 RVA: 0x000FCF40 File Offset: 0x000FC140
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", null, typeof(string));
			info.AddValue("UnityType", unityType);
			info.AddValue("AssemblyName", string.Empty);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000FCF8F File Offset: 0x000FC18F
		[NullableContext(1)]
		public UnitySerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._unityType = info.GetInt32("UnityType");
			this._data = info.GetString("Data");
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000FCFC7 File Offset: 0x000FC1C7
		[NullableContext(1)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(SR.NotSupported_UnitySerHolder);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000FCFD3 File Offset: 0x000FC1D3
		[NullableContext(1)]
		public object GetRealObject(StreamingContext context)
		{
			if (this._unityType != 2)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidUnity, this._data ?? "UnityType"));
			}
			return DBNull.Value;
		}

		// Token: 0x040005D8 RID: 1496
		private readonly int _unityType;

		// Token: 0x040005D9 RID: 1497
		private readonly string _data;
	}
}
