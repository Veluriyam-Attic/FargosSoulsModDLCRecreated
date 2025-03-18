using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity
{
	// Token: 0x0200008B RID: 139
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class RogueEssence : ModItem
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000FB7B File Offset: 0x0000DD7B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModLoader.HasMod("FargowiltasCrossmod");
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.rare = 4;
			base.Item.value = 150000;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000FBDC File Offset: 0x0000DDDC
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

		// Token: 0x06000242 RID: 578 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				*player.GetDamage<ThrowingDamageClass>() += 0.18f;
				calamityPlayer.rogueVelocity += 0.05f;
				*player.GetCritChance(DamageClass.Throwing) += 5f;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<RogueEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GildedDagger>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WebBall>(), 300);
			recipe.AddIngredient(ModContent.ItemType<BouncingEyeball>(), 1);
			recipe.AddIngredient(4764, 1);
			recipe.AddIngredient(ModContent.ItemType<MeteorFist>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SludgeSplotch>(), 300);
			recipe.AddIngredient(ModContent.ItemType<SkyStabber>(), 4);
			recipe.AddIngredient(ModContent.ItemType<PoisonPack>(), 3);
			recipe.AddIngredient(ModContent.ItemType<HardenedHoneycomb>(), 300);
			recipe.AddIngredient(ModContent.ItemType<ShinobiBlade>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernalKris>(), 300);
			recipe.AddIngredient(ModContent.ItemType<AshenStalactite>(), 1);
			recipe.AddIngredient(1225, 5);
			recipe.AddTile(114);
			recipe.Register();
		}
	}
}
