using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BossStarScouter;
using ThoriumMod.Items.BossViscount;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.HealerItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Essences
{
	// Token: 0x02000015 RID: 21
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HealerEssence : ModItem
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000039E0 File Offset: 0x00001BE0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.rare = 4;
			base.Item.value = 150000;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003A30 File Offset: 0x00001C30
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(255, 30, 247));
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ThoriumPlayer>();
			*player.GetDamage((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 0.18f;
			*player.GetCritChance((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 5f;
			*player.GetAttackSpeed<HealerDamage>() += 0.05f;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003B1C File Offset: 0x00001D1C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ClericEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheGoodBook>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HeartWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FeatherBarrierRod>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloomingStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LargePopcorn>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkMageStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BatScythe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineLotus>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SentinelWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeDisperser>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedeemerStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DeepStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StarRod>(), 1);
			if (ModLoader.HasMod("SpiritBardHealer"))
			{
				Mod spiritBardHealer = ModLoader.GetMod("SpiritBardHealer");
				if (spiritBardHealer != null)
				{
					ModItem modItem = spiritBardHealer.Find<ModItem>("AvianBeak");
					int screamOfTerrorID = (modItem != null) ? modItem.Type : 0;
					ModItem modItem2 = spiritBardHealer.Find<ModItem>("CryoliteFridgerator");
					int thornBlowID = (modItem2 != null) ? modItem2.Type : 0;
					ModItem modItem3 = spiritBardHealer.Find<ModItem>("StarScythe");
					int starScytheID = (modItem3 != null) ? modItem3.Type : 0;
					if (screamOfTerrorID > 0)
					{
						recipe.AddIngredient(screamOfTerrorID, 1);
					}
					if (thornBlowID > 0)
					{
						recipe.AddIngredient(thornBlowID, 1);
					}
					if (starScytheID > 0)
					{
						recipe.AddIngredient(starScytheID, 1);
					}
				}
			}
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer != null)
				{
					ModItem modItem4 = calamityBardHealer.Find<ModItem>("DryMouth");
					int screamOfTerrorID2 = (modItem4 != null) ? modItem4.Type : 0;
					ModItem modItem5 = calamityBardHealer.Find<ModItem>("Duality");
					int thornBlowID2 = (modItem5 != null) ? modItem5.Type : 0;
					ModItem modItem6 = calamityBardHealer.Find<ModItem>("TheWindmill");
					int starScytheID2 = (modItem6 != null) ? modItem6.Type : 0;
					if (screamOfTerrorID2 > 0)
					{
						recipe.AddIngredient(screamOfTerrorID2, 1);
					}
					if (thornBlowID2 > 0)
					{
						recipe.AddIngredient(thornBlowID2, 1);
					}
					if (starScytheID2 > 0)
					{
						recipe.AddIngredient(starScytheID2, 1);
					}
				}
			}
			recipe.AddIngredient(1225, 5);
			recipe.AddTile(114);
			recipe.Register();
		}
	}
}
