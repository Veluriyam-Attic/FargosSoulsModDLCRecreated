using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A3 RID: 163
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class DemonShadeEnchantment : ModItem
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0001656C File Offset: 0x0001476C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 50000000;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000165DC File Offset: 0x000147DC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(173, 52, 70));
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00016660 File Offset: 0x00014860
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			bool demonShadeEffectApplied = AccessoryEffectLoader.AddEffect<DemonShadeEnchantment.RedDevilMinionEffect>(player, base.Item);
			this.profanedCrystalEffectToggle = AccessoryEffectLoader.AddEffect<DemonShadeEnchantment.ProfanedSoulCrystalEffect>(player, base.Item);
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				bool wearingThisArmorSet = false;
				modPlayer.dsSetBonus = true;
				modPlayer.wearingRogueArmor = true;
				modPlayer.WearingPostMLSummonerSet = true;
				if (demonShadeEffectApplied)
				{
					if (player.whoAmI == Main.myPlayer && !modPlayer.chibii)
					{
						modPlayer.redDevil = true;
						IEntitySource source = player.GetSource_ItemUse(base.Item, null);
						if (player.FindBuffIndex(ModContent.BuffType<DemonshadeSetDevilBuff>()) == -1)
						{
							player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 3600, true, false);
						}
						if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] < 1)
						{
							int baseDamage = player.ApplyArmorAccDamageBonusesTo(10000f);
							int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo((float)baseDamage);
							Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<DemonshadeRedDevil>(), damage, 0f, Main.myPlayer, 0f, 0f, 0f).originalDamage = baseDamage;
						}
					}
				}
				else
				{
					for (int i = 0; i < Main.maxProjectiles; i++)
					{
						if (player.armor[0].type == ModContent.ItemType<DemonshadeHelm>() && player.armor[1].type == ModContent.ItemType<DemonshadeBreastplate>() && player.armor[2].type == ModContent.ItemType<DemonshadeGreaves>())
						{
							wearingThisArmorSet = true;
						}
						if (Main.projectile[i].type == ModContent.ProjectileType<DemonshadeRedDevil>() && Main.projectile[i].active && !wearingThisArmorSet)
						{
							Main.projectile[i].Kill();
						}
					}
				}
				*player.GetDamage<SummonDamageClass>() += 1f;
				player.maxMinions += 10;
			}
			ModItem profannedSoulCrystal;
			if (AccessoryEffectLoader.AddEffect<DemonShadeEnchantment.ProfanedSoulCrystalEffect>(player, base.Item) && calamity.TryFind<ModItem>("ProfanedSoulCrystal", ref profannedSoulCrystal))
			{
				profannedSoulCrystal.UpdateAccessory(player, hideVisual);
				modPlayer = player.Calamity();
				modPlayer.pSoulArtifact = true;
				modPlayer.profanedCrystal = true;
				if (!modPlayer.profanedCrystalPrevious && player.ownedProjectileCounts[ModContent.ProjectileType<PscTransformAnimation>()] == 0)
				{
					modPlayer.pSoulShieldDurability = 1;
					modPlayer.profanedCrystalAnim = 120;
					Projectile.NewProjectile(player.GetSource_FromThis(null), player.Center, Vector2.Zero, ModContent.ProjectileType<PscTransformAnimation>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
				}
				modPlayer.profanedCrystalHide = (hideVisual || modPlayer.profanedCrystalAnim > 0);
				modPlayer.pSoulShieldVisible = !hideVisual;
				DemonShadeEnchantment.DetermineTransformationEligibility(player);
			}
			ModItem angelicAlliance;
			if (AccessoryEffectLoader.AddEffect<DemonShadeEnchantment.AngelicAllianceEffect>(player, base.Item) && calamity.TryFind<ModItem>("AngelicAlliance", ref angelicAlliance))
			{
				angelicAlliance.UpdateAccessory(player, hideVisual);
			}
			ModItem sC;
			if (calamity.TryFind<ModItem>("ShatteredCommunity", ref sC))
			{
				sC.UpdateAccessory(player, hideVisual);
			}
			ModItem cD;
			if (calamity.TryFind<ModItem>("CirrusDress", ref cD))
			{
				cD.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001696C File Offset: 0x00014B6C
		internal static void DetermineTransformationEligibility(Player player)
		{
			if (!player.Calamity().profanedCrystalBuffs && player.Calamity().profanedCrystalAnim == -1 && DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs && (float)player.maxMinions - player.slotsMinions >= 10f && !player.Calamity().profanedCrystalForce && player.HasBuff<ProfanedCrystalBuff>())
			{
				player.Calamity().profanedCrystalBuffs = true;
				player.Calamity().pscState = (int)DemonShadeEnchantment.GetPscStateFor(player, false);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000169EC File Offset: 0x00014BEC
		internal static ProfanedSoulCrystal.ProfanedSoulCrystalState GetPscStateFor(Player player, bool ignoreNoBuffs = false)
		{
			if (!player.Calamity().profanedCrystalBuffs && !ignoreNoBuffs)
			{
				return ProfanedSoulCrystal.ProfanedSoulCrystalState.Vanity;
			}
			if ((ignoreNoBuffs && (!DownedBossSystem.downedCalamitas || !DownedBossSystem.downedExoMechs || (float)player.maxMinions - player.slotsMinions < 10f)) || player.Calamity().profanedCrystalForce || !player.HasBuff<ProfanedCrystalBuff>())
			{
				return ProfanedSoulCrystal.ProfanedSoulCrystalState.Vanity;
			}
			bool flag = player.slotsMinions == 0f;
			bool noSentries = !Main.projectile.Any((Projectile proj) => proj.active && proj.owner == player.whoAmI && proj.sentry);
			if (flag && noSentries)
			{
				return ProfanedSoulCrystal.ProfanedSoulCrystalState.Empowered;
			}
			if (Main.dayTime)
			{
				return ProfanedSoulCrystal.ProfanedSoulCrystalState.Buffs;
			}
			return ProfanedSoulCrystal.ProfanedSoulCrystalState.Enraged;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00016AAC File Offset: 0x00014CAC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DemonshadeHelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonshadeBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonshadeGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ProfanedSoulCrystal>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AngelicAlliance>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShatteredCommunity>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CirrusDress>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Apotheosis>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Eternity>(), 1);
			recipe.AddTile(this.calamity, "DraedonsForge");
			recipe.Register();
		}

		// Token: 0x04000060 RID: 96
		public bool profanedCrystalEffectToggle;

		// Token: 0x04000061 RID: 97
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x04000062 RID: 98
		public const int maxPscAnimTime = 120;

		// Token: 0x02000189 RID: 393
		public class RedDevilMinionEffect : AccessoryEffect
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600058C RID: 1420 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x000190C8 File Offset: 0x000172C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DemonShadeEnchantment>();
				}
			}
		}

		// Token: 0x0200018A RID: 394
		public class ProfanedSoulCrystalEffect : AccessoryEffect
		{
			// Token: 0x17000199 RID: 409
			// (get) Token: 0x0600058F RID: 1423 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000590 RID: 1424 RVA: 0x000190C8 File Offset: 0x000172C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DemonShadeEnchantment>();
				}
			}
		}

		// Token: 0x0200018B RID: 395
		public class AngelicAllianceEffect : AccessoryEffect
		{
			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000592 RID: 1426 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000593 RID: 1427 RVA: 0x000190C8 File Offset: 0x000172C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DemonShadeEnchantment>();
				}
			}
		}
	}
}
