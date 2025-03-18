using System;
using Terraria;
using Terraria.ModLoader;

// Token: 0x02000002 RID: 2
public class DLCGlobalNPC : GlobalNPC
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public override bool InstancePerEntity
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
	public override void AI(NPC npc)
	{
		if (!npc.onFire)
		{
			this.magma = false;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (this.magma && npc.onFire)
		{
			npc.lifeRegen -= 8;
		}
	}

	// Token: 0x04000001 RID: 1
	public bool magma;
}
