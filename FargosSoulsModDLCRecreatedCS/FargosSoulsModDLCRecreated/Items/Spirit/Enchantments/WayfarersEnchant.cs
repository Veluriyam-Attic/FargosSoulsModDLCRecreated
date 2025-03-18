using System;
using SpiritMod;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Armor.WayfarerSet;
using SpiritMod.Items.Sets.ToolsMisc.BrilliantHarvester;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000076 RID: 118
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class WayfarersEnchant : ModItem
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E844 File Offset: 0x0000CA44
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MyPlayer>().explorerTreads = true;
			SpiritUtils.GetSpiritPlayer(player).metalBand = true;
			ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerHead").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerBody").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "WayfarerLegs").UpdateArmorSet(player);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<WayfarerHead>(1);
			recipe.AddIngredient<WayfarerBody>(1);
			recipe.AddIngredient<WayfarerLegs>(1);
			recipe.AddIngredient<GemPickaxe>(1);
			recipe.AddIngredient<MetalBand>(1);
			recipe.AddIngredient<ExplorerTreads>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000047 RID: 71
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
