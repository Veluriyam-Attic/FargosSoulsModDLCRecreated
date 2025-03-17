using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200045B RID: 1115
	internal class ComEventsMethod
	{
		// Token: 0x060043EF RID: 17391 RVA: 0x00177AA2 File Offset: 0x00176CA2
		public ComEventsMethod(int dispid)
		{
			this._dispid = dispid;
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00177ABC File Offset: 0x00176CBC
		public static ComEventsMethod Find(ComEventsMethod methods, int dispid)
		{
			while (methods != null && methods._dispid != dispid)
			{
				methods = methods._next;
			}
			return methods;
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00177AD5 File Offset: 0x00176CD5
		public static ComEventsMethod Add(ComEventsMethod methods, ComEventsMethod method)
		{
			method._next = methods;
			return method;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x00177AE0 File Offset: 0x00176CE0
		public static ComEventsMethod Remove(ComEventsMethod methods, ComEventsMethod method)
		{
			if (methods == method)
			{
				return methods._next;
			}
			ComEventsMethod comEventsMethod = methods;
			while (comEventsMethod != null && comEventsMethod._next != method)
			{
				comEventsMethod = comEventsMethod._next;
			}
			if (comEventsMethod != null)
			{
				comEventsMethod._next = method._next;
			}
			return methods;
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x00177B20 File Offset: 0x00176D20
		public bool Empty
		{
			get
			{
				List<ComEventsMethod.DelegateWrapper> delegateWrappers = this._delegateWrappers;
				bool result;
				lock (delegateWrappers)
				{
					result = (this._delegateWrappers.Count == 0);
				}
				return result;
			}
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x00177B6C File Offset: 0x00176D6C
		public void AddDelegate(Delegate d, bool wrapArgs = false)
		{
			List<ComEventsMethod.DelegateWrapper> delegateWrappers = this._delegateWrappers;
			lock (delegateWrappers)
			{
				foreach (ComEventsMethod.DelegateWrapper delegateWrapper in this._delegateWrappers)
				{
					if (delegateWrapper.Delegate.GetType() == d.GetType() && delegateWrapper.WrapArgs == wrapArgs)
					{
						delegateWrapper.Delegate = Delegate.Combine(delegateWrapper.Delegate, d);
						return;
					}
				}
				ComEventsMethod.DelegateWrapper item = new ComEventsMethod.DelegateWrapper(d, wrapArgs);
				this._delegateWrappers.Add(item);
			}
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00177C30 File Offset: 0x00176E30
		public void RemoveDelegate(Delegate d, bool wrapArgs = false)
		{
			List<ComEventsMethod.DelegateWrapper> delegateWrappers = this._delegateWrappers;
			lock (delegateWrappers)
			{
				int num = -1;
				ComEventsMethod.DelegateWrapper delegateWrapper = null;
				for (int i = 0; i < this._delegateWrappers.Count; i++)
				{
					ComEventsMethod.DelegateWrapper delegateWrapper2 = this._delegateWrappers[i];
					if (delegateWrapper2.Delegate.GetType() == d.GetType() && delegateWrapper2.WrapArgs == wrapArgs)
					{
						num = i;
						delegateWrapper = delegateWrapper2;
						break;
					}
				}
				if (num >= 0)
				{
					Delegate @delegate = Delegate.Remove(delegateWrapper.Delegate, d);
					if (@delegate != null)
					{
						delegateWrapper.Delegate = @delegate;
					}
					else
					{
						this._delegateWrappers.RemoveAt(num);
					}
				}
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00177CF0 File Offset: 0x00176EF0
		public object Invoke(object[] args)
		{
			object result = null;
			List<ComEventsMethod.DelegateWrapper> delegateWrappers = this._delegateWrappers;
			lock (delegateWrappers)
			{
				foreach (ComEventsMethod.DelegateWrapper delegateWrapper in this._delegateWrappers)
				{
					result = delegateWrapper.Invoke(args);
				}
			}
			return result;
		}

		// Token: 0x04000ED1 RID: 3793
		private readonly List<ComEventsMethod.DelegateWrapper> _delegateWrappers = new List<ComEventsMethod.DelegateWrapper>();

		// Token: 0x04000ED2 RID: 3794
		private readonly int _dispid;

		// Token: 0x04000ED3 RID: 3795
		private ComEventsMethod _next;

		// Token: 0x0200045C RID: 1116
		public class DelegateWrapper
		{
			// Token: 0x060043F7 RID: 17399 RVA: 0x00177D70 File Offset: 0x00176F70
			public DelegateWrapper(Delegate d, bool wrapArgs)
			{
				this.Delegate = d;
				this.WrapArgs = wrapArgs;
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x060043F8 RID: 17400 RVA: 0x00177D86 File Offset: 0x00176F86
			// (set) Token: 0x060043F9 RID: 17401 RVA: 0x00177D8E File Offset: 0x00176F8E
			public Delegate Delegate { get; set; }

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x060043FA RID: 17402 RVA: 0x00177D97 File Offset: 0x00176F97
			// (set) Token: 0x060043FB RID: 17403 RVA: 0x00177D9F File Offset: 0x00176F9F
			public bool WrapArgs { get; private set; }

			// Token: 0x060043FC RID: 17404 RVA: 0x00177DA8 File Offset: 0x00176FA8
			public object Invoke(object[] args)
			{
				if (this.Delegate == null)
				{
					return null;
				}
				if (!this._once)
				{
					this.PreProcessSignature();
					this._once = true;
				}
				if (this._cachedTargetTypes != null && this._expectedParamsCount == args.Length)
				{
					for (int i = 0; i < this._expectedParamsCount; i++)
					{
						if (this._cachedTargetTypes[i] != null)
						{
							args[i] = Enum.ToObject(this._cachedTargetTypes[i], args[i]);
						}
					}
				}
				Delegate @delegate = this.Delegate;
				object[] args2;
				if (!this.WrapArgs)
				{
					args2 = args;
				}
				else
				{
					(args2 = new object[1])[0] = args;
				}
				return @delegate.DynamicInvoke(args2);
			}

			// Token: 0x060043FD RID: 17405 RVA: 0x00177E3C File Offset: 0x0017703C
			private void PreProcessSignature()
			{
				ParameterInfo[] parameters = this.Delegate.Method.GetParameters();
				this._expectedParamsCount = parameters.Length;
				Type[] array = null;
				for (int i = 0; i < this._expectedParamsCount; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					if (parameterInfo.ParameterType.IsByRef && parameterInfo.ParameterType.HasElementType && parameterInfo.ParameterType.GetElementType().IsEnum)
					{
						if (array == null)
						{
							array = new Type[this._expectedParamsCount];
						}
						array[i] = parameterInfo.ParameterType.GetElementType();
					}
				}
				if (array != null)
				{
					this._cachedTargetTypes = array;
				}
			}

			// Token: 0x04000ED4 RID: 3796
			private bool _once;

			// Token: 0x04000ED5 RID: 3797
			private int _expectedParamsCount;

			// Token: 0x04000ED6 RID: 3798
			private Type[] _cachedTargetTypes;
		}
	}
}
