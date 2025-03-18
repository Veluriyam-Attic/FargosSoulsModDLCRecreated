using System;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000008 RID: 8
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class CalDLCPlayer : ModPlayer
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002774 File Offset: 0x00000974
		private void OnAccessoryUnequip(int proj)
		{
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				if (Main.projectile[i].type == proj && Main.projectile[i].active)
				{
					Main.projectile[i].Kill();
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027BC File Offset: 0x000009BC
		public override void ResetEffects()
		{
			this.setMagma = false;
			this.berserkStage = 0;
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (this.umbraphileWasActive && !this.umbraphileActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<GoldiePet>());
				}
				this.umbraphileWasActive = this.umbraphileActive;
				this.umbraphileActive = false;
			}
		}

		// Token: 0x04000015 RID: 21
		public bool setMagma;

		// Token: 0x04000016 RID: 22
		public bool orbital;

		// Token: 0x04000017 RID: 23
		public Vector2 orbitalRotation3 = Vector2.UnitY;

		// Token: 0x04000018 RID: 24
		public int berserkStage;

		// Token: 0x04000019 RID: 25
		public bool isInFront;

		// Token: 0x0400001A RID: 26
		public bool umbraphileActive;

		// Token: 0x0400001B RID: 27
		public bool umbraphileWasActive;

		// Token: 0x0400001C RID: 28
		public bool davyJonesLockboxWasActive;
	}
}
