using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using FargosSoulsModDLCRecreated.Items.Calamity.Enchantments;
using Fargowiltas.Items.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Forces
{
	// Token: 0x0200008E RID: 142
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class DevastationForce : ModItem
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00010368 File Offset: 0x0000E568
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Rain hell down on those who resist your power"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Reaver, Plague Reaper, and Demonshade"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "+5 defense when below 50% life"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Trinket of Chi, Rotten Dogtooth, Wulfrum Acrobatics Pack, and Plague Hive"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Plagued Fuel Pack, The Camper, and Profaned Soul Crystal"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Alchemical Flask, The Camper, and Profaned Soul Crystal"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Bloom Stone and Necklace of Vexation"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Glove of Recklessness, Glove of Precision, and The Bee"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Corrupt Flask and Crimson Flask"));
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Angelic Alliance, Cirrus Dress, and Shattered Community"));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010494 File Offset: 0x0000E694
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000104F8 File Offset: 0x0000E6F8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem wulfrumEnchantment;
			if (mod.TryFind<ModItem>("WulfrumEnchantment", ref wulfrumEnchantment))
			{
				wulfrumEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem reaverEnchantment;
			if (mod.TryFind<ModItem>("ReaverEnchantment", ref reaverEnchantment))
			{
				reaverEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem plagueReaperEnchantment;
			if (mod.TryFind<ModItem>("PlagueReaperEnchantment", ref plagueReaperEnchantment))
			{
				plagueReaperEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem demonShadeEnchantment;
			if (mod.TryFind<ModItem>("DemonShadeEnchantment", ref demonShadeEnchantment))
			{
				demonShadeEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010584 File Offset: 0x0000E784
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WulfrumEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ReaverEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueReaperEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonShadeEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 4);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
