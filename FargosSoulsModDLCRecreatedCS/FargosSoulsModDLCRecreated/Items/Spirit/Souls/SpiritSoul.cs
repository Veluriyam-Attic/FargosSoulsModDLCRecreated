using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.Items.Spirit.Forces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Souls
{
	// Token: 0x02000070 RID: 112
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class SpiritSoul : ModItem
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000E024 File Offset: 0x0000C224
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip1", "All armor bonuses from Driftwood, Botanist, and Floran"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Wayfarers and Sunflower"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3", "All armor bonuses from Rogue, Chitin, Apostle, and Marble Chunk"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip4", "All armor bonuses from Astralite, Seraph, and Runic"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "All armor bonuses from Bismite, Cascade, and Granite Chunk"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip6", "All armor bonuses from Stream Surfer, Spirit, and Primalstone"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip7", "All armor bonuses from Bloodcourt, Cryolite, Dusk, and Frigid"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip8", "All armor bonuses from Marksman, Painmonger, and Slag Tyrant"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip9", "Effects of Metal Band, Explorer Treads, Reach Brooch, and Floran Charm"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip10", "Effects of Rogue Crest, Swift Rune, and High Gravity Boots"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip11", "Effects of Seraphim Bulwark, Fallen Angel, and Rune Wizard Scroll"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Bismuth Shield, Pendant Of The Ocean, Tech Boots, and Shrunken Launcher"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip13", "Effects of Flying Fish Fin, Shadow Singe Fang, and Titanbound Bulwark"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip14", "Effects of Bloody Ward, Grisly Tongue, Winter Hat, and Four Of A Kind"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip15", "Effects of Electric Guitar, Frigid Gloves, Hell Eater, and Cimmerian Scepter"));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000E1C6 File Offset: 0x0000C3C6
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 20000000;
			base.Item.accessory = true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000E204 File Offset: 0x0000C404
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(base.Mod.Name, "AdventurerForce").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "AtlantisForce").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "FrostburnForce").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "HurricaneForce").UpdateAccessory(player, false);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000E284 File Offset: 0x0000C484
		public override void AddRecipes()
		{
			Recipe obj = base.CreateRecipe(1);
			obj.AddIngredient<AdventurerForce>(1);
			obj.AddIngredient<AtlantisForce>(1);
			obj.AddIngredient<FrostburnForce>(1);
			obj.AddIngredient<HurricaneForce>(1);
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
						obj.AddIngredient(ashesID, 5);
					}
					if (exoPrismID > 0)
					{
						obj.AddIngredient(exoPrismID, 5);
					}
				}
			}
			obj.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
			obj.Register();
		}
	}
}
