using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Buffs
{
	// Token: 0x020000B8 RID: 184
	public class Magma : ModBuff
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00018A8C File Offset: 0x00016C8C
		public override void SetStaticDefaults()
		{
			Texture2D value = ModContent.Request<Texture2D>("FargosSoulsModDLCRecreated/Buffs/Magma", 2).Value;
			Main.debuff[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public override void Update(NPC npc, ref int buffIndex)
		{
			DLCGlobalNPC globalNPC = npc.GetGlobalNPC<DLCGlobalNPC>();
			if (globalNPC != null)
			{
				globalNPC.magma = true;
			}
		}
	}
}
