using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Runtime.Versioning
{
	// Token: 0x02000404 RID: 1028
	public static class VersioningHelper
	{
		// Token: 0x0600329C RID: 12956 RVA: 0x0016B836 File Offset: 0x0016AA36
		[NullableContext(1)]
		public static string MakeVersionSafeName([Nullable(2)] string name, ResourceScope from, ResourceScope to)
		{
			return VersioningHelper.MakeVersionSafeName(name, from, to, null);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x0016B844 File Offset: 0x0016AA44
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
		{
			ResourceScope resourceScope = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			if (resourceScope > resourceScope2)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResourceScopeWrongDirection, resourceScope, resourceScope2), "from");
			}
			SxSRequirements requirements = VersioningHelper.GetRequirements(to, from);
			if ((requirements & (SxSRequirements.AssemblyName | SxSRequirements.TypeName)) != SxSRequirements.None && type == null)
			{
				throw new ArgumentNullException("type", SR.ArgumentNull_TypeRequiredByResourceScope);
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			char value = '_';
			if ((requirements & SxSRequirements.ProcessID) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append('p');
				stringBuilder.Append(Environment.ProcessId);
			}
			if ((requirements & SxSRequirements.CLRInstanceID) != SxSRequirements.None)
			{
				string clrinstanceString = VersioningHelper.GetCLRInstanceString();
				stringBuilder.Append(value);
				stringBuilder.Append('r');
				stringBuilder.Append(clrinstanceString);
			}
			if ((requirements & SxSRequirements.AppDomainID) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("ad");
				stringBuilder.Append(AppDomain.CurrentDomain.Id);
			}
			if ((requirements & SxSRequirements.TypeName) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(type.Name);
			}
			if ((requirements & SxSRequirements.AssemblyName) != SxSRequirements.None)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x0016B96C File Offset: 0x0016AB6C
		private static string GetCLRInstanceString()
		{
			return 3.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x0016B988 File Offset: 0x0016AB88
		private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
		{
			SxSRequirements sxSRequirements = SxSRequirements.None;
			switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
			{
			case ResourceScope.Machine:
				switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
				{
				case ResourceScope.Machine:
					goto IL_8D;
				case ResourceScope.Process:
					sxSRequirements |= SxSRequirements.ProcessID;
					goto IL_8D;
				case ResourceScope.AppDomain:
					sxSRequirements |= (SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID);
					goto IL_8D;
				}
				throw new ArgumentException(SR.Format(SR.Argument_BadResourceScopeTypeBits, consumeAsScope), "consumeAsScope");
			case ResourceScope.Process:
				if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
				{
					sxSRequirements |= (SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID);
					goto IL_8D;
				}
				goto IL_8D;
			case ResourceScope.AppDomain:
				goto IL_8D;
			}
			throw new ArgumentException(SR.Format(SR.Argument_BadResourceScopeTypeBits, calleeScope), "calleeScope");
			IL_8D:
			ResourceScope resourceScope = calleeScope & (ResourceScope.Private | ResourceScope.Assembly);
			if (resourceScope != ResourceScope.None)
			{
				if (resourceScope != ResourceScope.Private)
				{
					if (resourceScope != ResourceScope.Assembly)
					{
						throw new ArgumentException(SR.Format(SR.Argument_BadResourceScopeVisibilityBits, calleeScope), "calleeScope");
					}
					if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
					{
						sxSRequirements |= SxSRequirements.TypeName;
					}
				}
			}
			else
			{
				ResourceScope resourceScope2 = consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly);
				if (resourceScope2 != ResourceScope.None)
				{
					if (resourceScope2 != ResourceScope.Private)
					{
						if (resourceScope2 != ResourceScope.Assembly)
						{
							throw new ArgumentException(SR.Format(SR.Argument_BadResourceScopeVisibilityBits, consumeAsScope), "consumeAsScope");
						}
						sxSRequirements |= SxSRequirements.AssemblyName;
					}
					else
					{
						sxSRequirements |= (SxSRequirements.AssemblyName | SxSRequirements.TypeName);
					}
				}
			}
			return sxSRequirements;
		}
	}
}
