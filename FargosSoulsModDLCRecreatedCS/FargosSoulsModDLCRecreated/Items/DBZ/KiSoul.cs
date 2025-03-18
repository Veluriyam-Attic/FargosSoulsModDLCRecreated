using System;
using DBZMODPORT;
using DBZMODPORT.Items.Accessories;
using DBZMODPORT.Items.Accessories.Infusers;
using DBZMODPORT.Items.Weapons.Tier_6;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.DBZ
{
	// Token: 0x020000AA RID: 170
	[JITWhenModsEnabled(new string[]
	{
		"DBZMODPORT"
	})]
	[ExtendsFromMod(new string[]
	{
		"DBZMODPORT"
	})]
	public class KiSoul : ModItem
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x000186B4 File Offset: 0x000168B4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 20000000;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00018718 File Offset: 0x00016918
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("DBZMODPORT"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("DBZMODPORT");
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			modPlayer.KiDamage += 0.35f;
			modPlayer.kiCrit += 20;
			modPlayer.chargeMoveSpeed = Math.Max(modPlayer.chargeMoveSpeed, 2f);
			modPlayer.kiKbAddition += 0.3f;
			modPlayer.kiDrainMulti -= 0.4f;
			modPlayer.kiMaxMult += 0.3f;
			modPlayer.kiRegen += 4;
			modPlayer.orbGrabRange += 6;
			modPlayer.orbHealAmount += 100;
			modPlayer.chargeLimitAdd += 5;
			modPlayer.flightSpeedAdd += 0.5f;
			modPlayer.flightUsageAdd += 2;
			modPlayer.zenkaiCharm = true;
			ModItem cA;
			if (mod.TryFind<ModItem>("CrystalliteAlleviate", ref cA))
			{
				cA.UpdateAccessory(player, hideVisual);
			}
			ModItem bDS;
			if (mod.TryFind<ModItem>("BlackDiamondShell", ref bDS))
			{
				bDS.UpdateAccessory(player, hideVisual);
			}
			ModItem buldariumSigmite;
			if (mod.TryFind<ModItem>("BuldariumSigmite", ref buldariumSigmite))
			{
				buldariumSigmite.UpdateAccessory(player, hideVisual);
			}
			ModItem dCI;
			if (mod.TryFind<ModItem>("DragonCrystalInfuser", ref dCI))
			{
				dCI.UpdateAccessory(player, hideVisual);
			}
			ModItem eA;
			if (mod.TryFind<ModItem>("EarthenArcanium", ref eA))
			{
				eA.UpdateAccessory(player, hideVisual);
			}
			ModItem sT6;
			if (mod.TryFind<ModItem>("ScouterT6", ref sT6))
			{
				sT6.UpdateAccessory(player, hideVisual);
			}
			ModItem sC;
			if (mod.TryFind<ModItem>("SpiritCharm", ref sC))
			{
				sC.UpdateAccessory(player, hideVisual);
			}
			ModItem kC;
			if (mod.TryFind<ModItem>("KaioCrystal", ref kC))
			{
				kC.UpdateAccessory(player, hideVisual);
			}
			ModItem mN;
			if (mod.TryFind<ModItem>("MaginNucleus", ref mN))
			{
				mN.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000188E4 File Offset: 0x00016AE4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CrystalliteAlleviate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlackDiamondShell>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlackBlitz>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BuldariumSigmite>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CandyLaser>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfuserRainbow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EarthenArcanium>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FinalShine>(), 1);
			recipe.AddIngredient(ModContent.ItemType<KaioCrystal>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MajinNucleus>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritCharm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SuperSpiritBomb>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ScouterT6>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ZenkaiCharm>(), 1);
			if (ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
			}
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
