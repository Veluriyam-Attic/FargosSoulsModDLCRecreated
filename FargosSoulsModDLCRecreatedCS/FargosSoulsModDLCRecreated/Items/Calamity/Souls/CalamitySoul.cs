using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using FargosSoulsModDLCRecreated.Items.Calamity.Forces;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Souls
{
	// Token: 0x02000090 RID: 144
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class CalamitySoul : ModItem
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0001095C File Offset: 0x0000EB5C
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "And the land grew quiet once more..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Aerospec, Statigel, and Hydrothermic"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "All armor bonuses from Empyrean and Fearmonger"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "All armor bonuses from Daedalus, Snow Ruffian, Umbraphile, and Astral"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "All armor bonuses from Omega Blue, Mollusk, Victide, Fathom Swarmer, and Sulphurous"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "All armor bonuses from Reaver, Plague Reaper, and Demonshade"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "All armor bonuses from Tarragon, Bloodflare, and Brimflame"));
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "All armor bonuses from God Slayer, Silva, and Auric"));
			list.Add(new TooltipLine(base.Mod, "Tooltip9", "+5 defense when below 50% life"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Gladiator's Locket, Aero Stone and Unstable Granite Core"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Fungal Clump, Mana Polarizer and Fungal Symbiote"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Hallowed Rune, Ethereal Extorter, Regenator, The Community, and Shield of the High Ruler"));
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil, The Absorber and Statis' Void Sash"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Flesh Totem, Ethereal Extorter, Regenator, The Absorber, and Shield of the High Ruler"));
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil, The Community and The Evolution"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Scuttler's Jewel and Permafrost Concoction"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Cryo Stone, Howls Heart, Frost Barrier, and Frost Flare"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Thief's Dime, Vampiric Talisman, and Evasion Scarf"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Radiance, Gravistar Sabaton and Ursa Sergeant"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Hide of Asturm Deus, Gravistar Sabaton and Ursa Sergeant"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of the Abyssal Diving Suit and Reaper Tooth Necklace"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Old Duke Scales and Reaper Tooth Necklace"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip9", "Effects of Aquatic Emblem, Shield of the Ocean and Giant Pearl"));
			list.Add(new TooltipLine(base.Mod, "Tooltip10", "Effects of Ocean's Crest, Deep Diver, and Luxor's Gift"));
			list.Add(new TooltipLine(base.Mod, "Tooltip11", "Effects of Corrosive Spine Lumenous Amulet, and Leviathan Ambergris"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Sand Cloak, Old Die, Aquatic Heart, Amadias Pendant, and Alluring Bait"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Sand Cloak, Old Die, Aquatic Heart, and Amadias Pendant"));
			}
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
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Angelic Alliance and Shattered Community"));
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

		// Token: 0x0600025A RID: 602 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 20000000;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00010F34 File Offset: 0x0000F134
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem annihilationForce;
			if (mod.TryFind<ModItem>("AnnihilationForce", ref annihilationForce))
			{
				annihilationForce.UpdateAccessory(player, hideVisual);
			}
			ModItem desolationForce;
			if (mod.TryFind<ModItem>("DesolationForce", ref desolationForce))
			{
				desolationForce.UpdateAccessory(player, hideVisual);
			}
			ModItem devastationForce;
			if (mod.TryFind<ModItem>("DevastationForce", ref devastationForce))
			{
				devastationForce.UpdateAccessory(player, hideVisual);
			}
			ModItem exaltationForce;
			if (mod.TryFind<ModItem>("ExaltationForce", ref exaltationForce))
			{
				exaltationForce.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<AnnihilationForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DesolationForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DevastationForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ExaltationForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
			if (ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<AshesofAnnihilation>(), 5);
				recipe.AddIngredient(ModContent.ItemType<ExoPrism>(), 5);
			}
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
