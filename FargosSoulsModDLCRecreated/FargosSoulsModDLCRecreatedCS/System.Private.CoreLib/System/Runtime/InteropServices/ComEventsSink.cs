using System;
using System.Runtime.InteropServices.ComTypes;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200045D RID: 1117
	internal class ComEventsSink : IDispatch, ICustomQueryInterface
	{
		// Token: 0x060043FE RID: 17406 RVA: 0x00177ECD File Offset: 0x001770CD
		public ComEventsSink(object rcw, Guid iid)
		{
			this._iidSourceItf = iid;
			this.Advise(rcw);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00177EE4 File Offset: 0x001770E4
		public static ComEventsSink Find(ComEventsSink sinks, ref Guid iid)
		{
			ComEventsSink comEventsSink = sinks;
			while (comEventsSink != null && comEventsSink._iidSourceItf != iid)
			{
				comEventsSink = comEventsSink._next;
			}
			return comEventsSink;
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00177F13 File Offset: 0x00177113
		public static ComEventsSink Add(ComEventsSink sinks, ComEventsSink sink)
		{
			sink._next = sinks;
			return sink;
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x00177F1D File Offset: 0x0017711D
		public static ComEventsSink RemoveAll(ComEventsSink sinks)
		{
			while (sinks != null)
			{
				sinks.Unadvise();
				sinks = sinks._next;
			}
			return null;
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x00177F34 File Offset: 0x00177134
		public static ComEventsSink Remove(ComEventsSink sinks, ComEventsSink sink)
		{
			ComEventsSink result = sinks;
			if (sink == sinks)
			{
				result = sinks._next;
			}
			else
			{
				ComEventsSink comEventsSink = sinks;
				while (comEventsSink != null && comEventsSink._next != sink)
				{
					comEventsSink = comEventsSink._next;
				}
				if (comEventsSink != null)
				{
					comEventsSink._next = sink._next;
				}
			}
			sink.Unadvise();
			return result;
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x00177F7D File Offset: 0x0017717D
		public ComEventsMethod RemoveMethod(ComEventsMethod method)
		{
			this._methods = ComEventsMethod.Remove(this._methods, method);
			return this._methods;
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x00177F97 File Offset: 0x00177197
		public ComEventsMethod FindMethod(int dispid)
		{
			return ComEventsMethod.Find(this._methods, dispid);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x00177FA8 File Offset: 0x001771A8
		public ComEventsMethod AddMethod(int dispid)
		{
			ComEventsMethod comEventsMethod = new ComEventsMethod(dispid);
			this._methods = ComEventsMethod.Add(this._methods, comEventsMethod);
			return comEventsMethod;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000AC09B File Offset: 0x000AB29B
		int IDispatch.GetTypeInfoCount()
		{
			return 0;
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00177FCF File Offset: 0x001771CF
		ITypeInfo IDispatch.GetTypeInfo(int iTInfo, int lcid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00177FCF File Offset: 0x001771CF
		void IDispatch.GetIDsOfNames(ref Guid iid, string[] names, int cNames, int lcid, int[] rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x00177FD8 File Offset: 0x001771D8
		private static ref Variant GetVariant(ref Variant pSrc)
		{
			if (pSrc.VariantType == (VarEnum)16396)
			{
				Span<Variant> span = new Span<Variant>(pSrc.AsByRefVariant.ToPointer(), 1);
				if ((span[0].VariantType & (VarEnum)20479) == (VarEnum)16396)
				{
					return span[0];
				}
			}
			return ref pSrc;
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x0017802C File Offset: 0x0017722C
		unsafe void IDispatch.Invoke(int dispid, ref Guid riid, int lcid, InvokeFlags wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ComEventsMethod comEventsMethod = this.FindMethod(dispid);
			if (comEventsMethod == null)
			{
				return;
			}
			object[] array = new object[pDispParams.cArgs];
			int[] array2 = new int[pDispParams.cArgs];
			bool[] array3 = new bool[pDispParams.cArgs];
			int length = pDispParams.cNamedArgs + pDispParams.cArgs;
			Span<Variant> span = new Span<Variant>(pDispParams.rgvarg.ToPointer(), length);
			Span<int> span2 = new Span<int>(pDispParams.rgdispidNamedArgs.ToPointer(), length);
			int i;
			int num;
			for (i = 0; i < pDispParams.cNamedArgs; i++)
			{
				num = *span2[i];
				ref Variant variant = ref ComEventsSink.GetVariant(span[i]);
				array[num] = variant.ToObject();
				array3[num] = true;
				int num2 = -1;
				if (variant.IsByRef)
				{
					num2 = i;
				}
				array2[num] = num2;
			}
			num = 0;
			while (i < pDispParams.cArgs)
			{
				while (array3[num])
				{
					num++;
				}
				ref Variant variant2 = ref ComEventsSink.GetVariant(span[pDispParams.cArgs - 1 - i]);
				array[num] = variant2.ToObject();
				int num3 = -1;
				if (variant2.IsByRef)
				{
					num3 = pDispParams.cArgs - 1 - i;
				}
				array2[num] = num3;
				num++;
				i++;
			}
			object obj = comEventsMethod.Invoke(array);
			if (pVarResult != IntPtr.Zero)
			{
				Marshal.GetNativeVariantForObject(obj, pVarResult);
			}
			for (i = 0; i < pDispParams.cArgs; i++)
			{
				int num4 = array2[i];
				if (num4 != -1)
				{
					ref Variant variant3 = ref ComEventsSink.GetVariant(span[num4]);
					variant3.CopyFromIndirect(array[i]);
				}
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x001781C8 File Offset: 0x001773C8
		CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == this._iidSourceItf || iid == typeof(IDispatch).GUID)
			{
				ppv = Marshal.GetComInterfaceForObject(this, typeof(IDispatch), CustomQueryInterfaceMode.Ignore);
				return CustomQueryInterfaceResult.Handled;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x00178224 File Offset: 0x00177424
		private void Advise(object rcw)
		{
			IConnectionPointContainer connectionPointContainer = (IConnectionPointContainer)rcw;
			IConnectionPoint connectionPoint;
			connectionPointContainer.FindConnectionPoint(ref this._iidSourceItf, out connectionPoint);
			connectionPoint.Advise(this, out this._cookie);
			this._connectionPoint = connectionPoint;
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x0017825C File Offset: 0x0017745C
		private void Unadvise()
		{
			if (this._connectionPoint == null)
			{
				return;
			}
			try
			{
				this._connectionPoint.Unadvise(this._cookie);
				Marshal.ReleaseComObject(this._connectionPoint);
			}
			catch
			{
			}
			finally
			{
				this._connectionPoint = null;
			}
		}

		// Token: 0x04000ED9 RID: 3801
		private Guid _iidSourceItf;

		// Token: 0x04000EDA RID: 3802
		private IConnectionPoint _connectionPoint;

		// Token: 0x04000EDB RID: 3803
		private int _cookie;

		// Token: 0x04000EDC RID: 3804
		private ComEventsMethod _methods;

		// Token: 0x04000EDD RID: 3805
		private ComEventsSink _next;
	}
}
