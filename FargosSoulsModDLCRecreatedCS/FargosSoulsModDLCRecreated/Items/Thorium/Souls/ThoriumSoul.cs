using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Souls
{
	// Token: 0x02000017 RID: 23
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumSoul : ModItem
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000040A4 File Offset: 0x000022A4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.value = 5000000;
			base.Item.rare = 13;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004108 File Offset: 0x00002308
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip1", "The true might of the 9 realms is yours!"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Sandstone, Danger, Flight, and Fungus"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3", "All armor bonuses from Living Wood, Bulb, and Life Bloom"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip4", "All armor bonuses from Depth Diver, Yew Wood, and Tide Hunter"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "All armor bonuses from Naga Skin, Icy, Cryomancer, and Whispering"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip6", "All armor bonuses from Novice Cleric, Sacred, Warlock, and Biotech"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip7", "All armor bonuses from Iridescent, Life Binder, Templar, and Fallen Paladin"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip8", "All armor bonuses from Crier, Noble, Cyber Punk, Ornate, and Maestro"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip9", "All armor bonuses from Granite, Bronze, and Darksteel"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip10", "All armor bonuses from Durasteel, Titan, and Conduit"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip11", "All armor bonuses from Lich, Plague Doctor, and White Dwarf"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip12", "All armor bonuses from Celestial and Shooting Star"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip13", "All armor bonuses from Lodestone, Valadium, Illumite, and Shade Master"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip14", "All armor bonuses from Jester, Thorium, and Terrarium"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip15", "All armor bonuses from Spirit Trapper, Malignant, Dragon, Dread, Flesh, and Demon Blood"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip16", "All armor bonuses from Magma, Berserker, White Knight, and Harbinger"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip17", "All armor bonuses from Tide Turner, Assassin, and Pyromancer"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip18", "All armor bonuses from Dream Weaver and Rhapsodist"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip19", "Effects of Blooming Shield, Bee Booties, Kick Petal, Fragrant Corsage, and Heart of the Jungle"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip20", "Effects of the Faberge Egg, Gardener's Sheath, Sweet Vengeance, Seed of Hope, Air Walkers, Leeching Sheath, and Nightshade Flower"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip21", "Effects of Sea Breeze Pendant, Ocean's Retaliation, Survivalist's Boots, and Lizahrd's Tail"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip22", "Effects of Serpent's Shield, Titan Slayer Sheath, Mermaid's Canteen, Bubble Magnet, and Goblin War Shield"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip23", "Effects of Agnor's Bowl, Thumb Ring, Drowned Doubloon, Blizzard Pouch, and Ice Bound Strider Hide"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip236", "Effects of Ghastly Carapance, Gut Wrencher's Gauntlet, and Writhing Sheath"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip24", "Effects of Demon Tongue, Dark Effigy, Flawless Chrysalis, High-tech Sonar Device, Dew Collector, Equalizer, Mantle of the Protector, and Life Quartz Shield"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip25", "Effects of Dark Heart, Karmic Holder, Nurse Purse, Sandweaver's Tiara, Spirit's Grace, Prydwen, Blast Shield, Yuma's Pendant, and Nirvana Statuette"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip26", "Effects of Ring of Unity, Metronome, Full Score, and Conductor's Baton"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip27", "Effects of Brass Cap, Mixtape, Lucky Rabbit's Foot, and Waxy Rosin"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip28", "Effects of Auto Tuner, Metal Music Player, Diss Track, Traveler's Boots, and Concert Tickets"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2821", "Effects of Jetstream Sheath, Steamkeeper Watch, and Magneto Grip"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip29", "Effects of Shock Absorber, Champion's Rebuttal, Rock Music Player, and Spiked Bracers"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip30", "Effects of Crystal Spear Tip, Mask of the Crystal Eye, Jet Boots, and Abyssal Shell"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip31", "Effects of Olympic Torch, Ogre Sandal, Weighted Winglets, and Spartan Sandals"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip32", "Effects of Crietz, Band of Replenishment, Up Donw Balloon, The Ring, and Fan Letter"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip33", "Effects of Astro-Beetle Husk, Heart of Stone, Obsidian Scale, and Mirror of the Beholder"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip34", "Effects of Shinobi Sigil, Crystal Scorpion, and Terrarium Surround Sound"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3543", "Effects of Jazz Music Player, Concussive Warhead, Terrarium Particle Sprinters, Terrarium Defender, and The Nuclear Option"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip35", "Effects of Phylactery, Hungering Blossom, Monster Charm, Guide to Expert Throwing : Volume 3, and Necrotic Skull"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip351", "Effects of Cape of the Survivor, Greedy Goblet, Pirate's Purse, and Proof of Avarice"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip361", "Effects of Vampire Gland, Beholder Gaze, Rapier Badge, Metabolic Pills, Spring Steps, and Slag Stompers"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip36", "Effects of Smothering Band, Disco Music Player, Cursed-Flaire Core, and Vile-Flail Core"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip37", "Effect of Artificer's Shield, Artificer's Rocketeers, and Artificer's Focus"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip38", "Effects of Plague Lord's Flask, Pocket Fusion Generator, and The Omega Core"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip39", "Summons several pets"));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000045BC File Offset: 0x000027BC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem alfheimForce;
			if (mod.TryFind<ModItem>("AlfheimForce", ref alfheimForce))
			{
				alfheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem asgardForce;
			if (mod.TryFind<ModItem>("AsgardForce", ref asgardForce))
			{
				asgardForce.UpdateAccessory(player, hideVisual);
			}
			ModItem helheimForce;
			if (mod.TryFind<ModItem>("HelheimForce", ref helheimForce))
			{
				helheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem jotunheimForce;
			if (mod.TryFind<ModItem>("JotunheimForce", ref jotunheimForce))
			{
				jotunheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem midgardForce;
			if (mod.TryFind<ModItem>("MidgardForce", ref midgardForce))
			{
				midgardForce.UpdateAccessory(player, hideVisual);
			}
			ModItem muspelheimForce;
			if (mod.TryFind<ModItem>("MuspelheimForce", ref muspelheimForce))
			{
				muspelheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem niflheimForce;
			if (mod.TryFind<ModItem>("NiflheimForce", ref niflheimForce))
			{
				niflheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem svartalfheimForce;
			if (mod.TryFind<ModItem>("SvartalfheimForce", ref svartalfheimForce))
			{
				svartalfheimForce.UpdateAccessory(player, hideVisual);
			}
			ModItem vanaheimForce;
			if (mod.TryFind<ModItem>("VanaheimForce", ref vanaheimForce))
			{
				vanaheimForce.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000046B4 File Offset: 0x000028B4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<AlfheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AsgardForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HelheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JotunheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MidgardForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MuspelheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NiflheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SvartalfheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VanaheimForce>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
			if (ModLoader.HasMod("FargowiltasCrossmod") && ModLoader.HasMod("CalamityMod"))
			{
				Mod calamity = ModLoader.GetMod("CalamityMod");
				if (calamity != null)
				{
					ModItem modItem = calamity.Find<ModItem>("AshesofAnnihilation");
					int ashesID = (modItem != null) ? modItem.Type : 0;
					ModItem modItem2 = calamity.Find<ModItem>("ExoPrism");
					int exoPrismID = (modItem2 != null) ? modItem2.Type : 0;
					if (ashesID > 0)
					{
						recipe.AddIngredient(ashesID, 5);
					}
					if (exoPrismID > 0)
					{
						recipe.AddIngredient(exoPrismID, 5);
					}
				}
			}
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
