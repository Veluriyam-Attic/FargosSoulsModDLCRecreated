using System;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000003 RID: 3
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class OrbitalLayerBack : OrbitalLayerBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000208C File Offset: 0x0000028C
		public override bool Front
		{
			get
			{
				return false;
			}
		}
	}
}
