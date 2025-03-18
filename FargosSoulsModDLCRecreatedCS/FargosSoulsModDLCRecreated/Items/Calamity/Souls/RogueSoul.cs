using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Rogue;
using Fargowiltas.Items.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Souls
{
	// Token: 0x02000091 RID: 145
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class RogueSoul : ModItem
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000FB7B File Offset: 0x0000DD7B
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !ModLoader.HasMod("FargowiltasCrossmod");
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00011050 File Offset: 0x0000F250
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.value = 1000000;
			base.Item.rare = 11;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000110A0 File Offset: 0x0000F2A0
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

		// Token: 0x06000261 RID: 609 RVA: 0x00011128 File Offset: 0x0000F328
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				*player.GetDamage<ThrowingDamageClass>() += 0.3f;
				calamityPlayer.rogueVelocity += 0.15f;
				*player.GetCritChance(DamageClass.Throwing) += 15f;
			}
			ModItem eclipseMirror;
			if (mod.TryFind<ModItem>("EclipseMirror", ref eclipseMirror))
			{
				eclipseMirror.UpdateAccessory(player, hideVisual);
			}
			ModItem nanotech;
			if (mod.TryFind<ModItem>("Nanotech", ref nanotech))
			{
				nanotech.UpdateAccessory(player, hideVisual);
			}
			ModItem veneratedLocket;
			if (mod.TryFind<ModItem>("VeneratedLocket", ref veneratedLocket))
			{
				veneratedLocket.UpdateAccessory(player, hideVisual);
			}
			ModItem dragonScales;
			if (mod.TryFind<ModItem>("DragonScales", ref dragonScales))
			{
				dragonScales.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000111FC File Offset: 0x0000F3FC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<RogueEssence>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EclipseMirror>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Nanotech>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VeneratedLocket>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonScales>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HellsSun>(), 10);
			recipe.AddIngredient(ModContent.ItemType<JawsOfOblivion>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DeepSeaDumbbell>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TimeBolt>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Eradicator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EclipsesFall>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Celestus>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ScarletDevil>(), 1);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
