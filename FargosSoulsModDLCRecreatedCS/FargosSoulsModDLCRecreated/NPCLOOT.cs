using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000006 RID: 6
	internal class NPCLOOT : GlobalNPC
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000026A0 File Offset: 0x000008A0
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == 498 || npc.type == 499 || npc.type == 500 || npc.type == 501 || npc.type == 502 || npc.type == 503 || npc.type == 504 || npc.type == 505 || npc.type == 506 || npc.type == 494 || npc.type == 495 || npc.type == 496 || npc.type == 497)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DangerShard>(), 1, 3, 5));
			}
		}
	}
}
