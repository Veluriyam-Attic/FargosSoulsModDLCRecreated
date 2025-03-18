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
	// Token: 0x0200008C RID: 140
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class AnnihilationForce : ModItem
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Where once there was life and light, only ruin remains..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Aerospec, Statigel, and Hydrothermic"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "All armor bonuses from Empyrean and Fearmonger"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Gladiator's Locket, Aero Stone and Unstable Granite Core"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Fungal Clump, Mana Polarizer and Fungal Symbiote"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Hallowed Rune, Ethereal Extorter, Regenator, The Community, and Shield of the High Ruler"));
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil, The Community, and Statis' Void Sash"));
				return;
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Flesh Totem, Ethereal Extorter, Regenator, The Absorber, and Shield of the High Ruler"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil, The Community, and The Evolution"));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000FF3C File Offset: 0x0000E13C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem aerospecEnchantment;
			if (mod.TryFind<ModItem>("AerospecEnchantment", ref aerospecEnchantment))
			{
				aerospecEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem ataxiaEnchantment;
			if (mod.TryFind<ModItem>("AtaxiaEnchantment", ref ataxiaEnchantment))
			{
				ataxiaEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem fearmongerEnchantment;
			if (mod.TryFind<ModItem>("FearmongerEnchantment", ref fearmongerEnchantment))
			{
				fearmongerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem statigelEnchantment;
			if (mod.TryFind<ModItem>("StatigelEnchantment", ref statigelEnchantment))
			{
				statigelEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem empyreanEnchantment;
			if (mod.TryFind<ModItem>("EmpyreanEnchantment", ref empyreanEnchantment))
			{
				empyreanEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<AerospecEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StatigelEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AtaxiaEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EmpyreanEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FearmongerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 4);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
