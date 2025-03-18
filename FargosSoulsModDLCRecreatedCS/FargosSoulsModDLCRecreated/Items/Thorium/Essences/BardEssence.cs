using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.BossViscount;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Essences
{
	// Token: 0x02000016 RID: 22
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BardEssence : ModItem
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003D30 File Offset: 0x00001F30
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.rare = 4;
			base.Item.value = 150000;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003D80 File Offset: 0x00001F80
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(230, 248, 34));
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E08 File Offset: 0x00002008
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ThoriumPlayer>();
			*player.GetDamage<BardDamage>() += 0.18f;
			*player.GetAttackSpeed<BardDamage>() += 0.05f;
			*player.GetCritChance((DamageClass)ThoriumDamageBase<BardDamage>.Instance) += 5f;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003E60 File Offset: 0x00002060
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BardEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AntlionMaraca>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SeashellCastanettes>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Didgeridoo>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Bagpipe>(), 1);
			if (!ModLoader.HasMod("SpiritBardHealer"))
			{
				recipe.AddIngredient(ModContent.ItemType<SkywareLute>(), 1);
			}
			else
			{
				ModItem modItem = ModLoader.GetMod("SpiritBardHealer").Find<ModItem>("Clairvoyage");
				int clairvoyageID = (modItem != null) ? modItem.Type : 0;
				if (clairvoyageID > 0)
				{
					recipe.AddIngredient(clairvoyageID, 1);
				}
			}
			recipe.AddIngredient(ModContent.ItemType<ForestOcarina>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SonarCannon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Calaveras>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GraniteBoomBox>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TuningFork>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HotHorn>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SongofIceAndFire>(), 1);
			if (ModLoader.HasMod("SpiritBardHealer"))
			{
				Mod spiritBardHealer = ModLoader.GetMod("SpiritBardHealer");
				if (spiritBardHealer != null)
				{
					ModItem modItem2 = spiritBardHealer.Find<ModItem>("ScreamofTerror");
					int screamOfTerrorID = (modItem2 != null) ? modItem2.Type : 0;
					ModItem modItem3 = spiritBardHealer.Find<ModItem>("ThornBlow");
					int thornBlowID = (modItem3 != null) ? modItem3.Type : 0;
					if (screamOfTerrorID > 0)
					{
						recipe.AddIngredient(screamOfTerrorID, 1);
					}
					if (thornBlowID > 0)
					{
						recipe.AddIngredient(thornBlowID, 1);
					}
				}
			}
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer != null)
				{
					ModItem modItem4 = calamityBardHealer.Find<ModItem>("WulfrumMegaphone");
					int screamOfTerrorID2 = (modItem4 != null) ? modItem4.Type : 0;
					ModItem modItem5 = calamityBardHealer.Find<ModItem>("Windward");
					int thornBlowID2 = (modItem5 != null) ? modItem5.Type : 0;
					ModItem modItem6 = calamityBardHealer.Find<ModItem>("Violince");
					int starScytheID = (modItem6 != null) ? modItem6.Type : 0;
					ModItem modItem7 = calamityBardHealer.Find<ModItem>("CrystalHydraulophone");
					int starScytheID2 = (modItem7 != null) ? modItem7.Type : 0;
					if (screamOfTerrorID2 > 0)
					{
						recipe.AddIngredient(screamOfTerrorID2, 1);
					}
					if (thornBlowID2 > 0)
					{
						recipe.AddIngredient(thornBlowID2, 1);
					}
					if (starScytheID > 0)
					{
						recipe.AddIngredient(starScytheID, 1);
					}
					if (starScytheID2 > 0)
					{
						recipe.AddIngredient(starScytheID2, 1);
					}
				}
			}
			recipe.AddIngredient(1225, 5);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
