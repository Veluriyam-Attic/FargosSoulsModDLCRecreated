using System;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000007 RID: 7
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class OrbitalLayerFront : OrbitalLayerBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002050 File Offset: 0x00000250
		public override bool Front
		{
			get
			{
				return true;
			}
		}
	}
}
