using System;
using FargosSoulsModDLCRecreated.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ThoriumMod.Projectiles.LightPets;
using ThoriumMod.Projectiles.Pets;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000005 RID: 5
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DLCPlayer : ModPlayer
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002308 File Offset: 0x00000508
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

		// Token: 0x0600000D RID: 13 RVA: 0x00002350 File Offset: 0x00000550
		public override void ResetEffects()
		{
			this.setMagma = false;
			this.berserkStage = 0;
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (this.depthDiverWasActive && !this.depthDiverActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<SubterraneanAngler>());
				}
				this.depthDiverWasActive = this.depthDiverActive;
				this.depthDiverActive = false;
				if (this.cryomancerWasActive && !this.cryomancerActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<SnowyOwlPet>());
				}
				this.cryomancerWasActive = this.cryomancerActive;
				this.cryomancerActive = false;
				if (this.demonBloodWasActive && !this.demonBloodActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<BlisterPet>());
				}
				this.demonBloodWasActive = this.demonBloodActive;
				this.demonBloodActive = false;
				if (this.dreadWasActive && !this.dreadActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<WyvernPet>());
				}
				this.dreadWasActive = this.dreadActive;
				this.dreadActive = false;
				if (this.harbingerWasActive && !this.harbingerActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<LilMog>());
				}
				this.harbingerWasActive = this.harbingerActive;
				this.harbingerActive = false;
				if (this.magicLanternWasActive && !this.magicLanternActive)
				{
					this.OnAccessoryUnequip(492);
				}
				this.magicLanternWasActive = this.magicLanternActive;
				this.magicLanternActive = false;
				if (this.davyJonesLockboxWasActive && !this.davyJonesLockboxActive)
				{
					this.OnAccessoryUnequip(ModContent.ProjectileType<DavyJonesLockBoxPro>());
				}
				this.davyJonesLockboxWasActive = this.davyJonesLockboxActive;
				this.davyJonesLockboxActive = false;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024BC File Offset: 0x000006BC
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (this.setMagma)
			{
				target.AddBuff(24, 300, false);
				target.AddBuff(ModContent.BuffType<Magma>(), 300, false);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024E5 File Offset: 0x000006E5
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (this.setMagma)
			{
				target.AddBuff(24, 300, false);
				target.AddBuff(ModContent.BuffType<Magma>(), 300, false);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002510 File Offset: 0x00000710
		public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
			Player player = drawInfo.drawPlayer;
			if (this.setMagma && Utils.NextBool(Main.rand, 4))
			{
				int num8 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 6, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.2f);
				int num9 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 55, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 0.7f);
				Main.dust[num8].noGravity = true;
				Main.dust[num8].velocity *= 1.8f;
				Dust dust = Main.dust[num8];
				dust.velocity.Y = dust.velocity.Y - 0.5f;
				Main.dust[num9].noGravity = true;
				Main.dust[num9].velocity *= 1.8f;
				Dust dust2 = Main.dust[num9];
				dust2.velocity.Y = dust2.velocity.Y - 0.5f;
			}
		}

		// Token: 0x04000002 RID: 2
		public bool setMagma;

		// Token: 0x04000003 RID: 3
		public bool orbital;

		// Token: 0x04000004 RID: 4
		public Vector2 orbitalRotation3 = Vector2.UnitY;

		// Token: 0x04000005 RID: 5
		public int berserkStage;

		// Token: 0x04000006 RID: 6
		public bool isInFront;

		// Token: 0x04000007 RID: 7
		public bool depthDiverActive;

		// Token: 0x04000008 RID: 8
		public bool depthDiverWasActive;

		// Token: 0x04000009 RID: 9
		public bool cryomancerActive;

		// Token: 0x0400000A RID: 10
		public bool cryomancerWasActive;

		// Token: 0x0400000B RID: 11
		public bool demonBloodActive;

		// Token: 0x0400000C RID: 12
		public bool demonBloodWasActive;

		// Token: 0x0400000D RID: 13
		public bool dreadActive;

		// Token: 0x0400000E RID: 14
		public bool dreadWasActive;

		// Token: 0x0400000F RID: 15
		public bool harbingerActive;

		// Token: 0x04000010 RID: 16
		public bool harbingerWasActive;

		// Token: 0x04000011 RID: 17
		public bool magicLanternActive;

		// Token: 0x04000012 RID: 18
		public bool magicLanternWasActive;

		// Token: 0x04000013 RID: 19
		public bool davyJonesLockboxActive;

		// Token: 0x04000014 RID: 20
		public bool davyJonesLockboxWasActive;
	}
}
