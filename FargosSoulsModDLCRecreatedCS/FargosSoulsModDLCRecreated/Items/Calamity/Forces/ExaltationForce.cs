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
	// Token: 0x0200008D RID: 141
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class ExaltationForce : ModItem
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00010054 File Offset: 0x0000E254
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "All armor bonuses from Tarragon, Bloodflare, and Brimflame"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from God Slayer, Silva, and Auric"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip3", "Effects of Blazing Core, Dark Sun Ring, and Chalice of the Blood God"));
				list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Nebulous Core and Draedon's Heart"));
				list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of The Amalgam and Auric Soul Artifact"));
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Yharim's Gift, Heart of the Elements, and The Sponge"));
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Void Of Extinction, Bloodflare Core, Phantomic Artifact, and Eldritch Soul Artifact"));
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Void of Calamity, Slagsplitter Pauldron, Flame-Licked Shell, and Chaos Stone"));
				return;
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Effects of Blazing Core, Dark Sun Ring, Warbanner of the Sun, and Affliction"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Blunder Booster and Auric Soul Artifact"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Dynamo Stem Cells, Daawnlight Spirit Origin, and The Transformer"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Void Of Extinction, Bloodflare Core, Phantomic Artifact, and Eldritch Soul Artifact"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Void of Calamity, Slagsplitter Pauldron, Flame-Licked Shell, and Chaos Stone"));
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Dragon Scales and Dimensional Soul Artifact"));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010250 File Offset: 0x0000E450
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem tarragonEnchantment;
			if (mod.TryFind<ModItem>("TarragonEnchantment", ref tarragonEnchantment))
			{
				tarragonEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem bloodflareEnchantment;
			if (mod.TryFind<ModItem>("BloodflareEnchantment", ref bloodflareEnchantment))
			{
				bloodflareEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem godSlayerEnchantment;
			if (mod.TryFind<ModItem>("GodSlayerEnchantment", ref godSlayerEnchantment))
			{
				godSlayerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem silvaEnchantment;
			if (mod.TryFind<ModItem>("SilvaEnchantment", ref silvaEnchantment))
			{
				silvaEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem auricEnchantment;
			if (mod.TryFind<ModItem>("AuricEnchantment", ref auricEnchantment))
			{
				auricEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TarragonEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloodflareEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GodSlayerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SilvaEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AuricEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 4);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
