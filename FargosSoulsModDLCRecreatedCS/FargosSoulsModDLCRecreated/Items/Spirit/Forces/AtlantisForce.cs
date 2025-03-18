using System;
using FargosSoulsModDLCRecreated.Items.Spirit.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Forces
{
	// Token: 0x0200006D RID: 109
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class AtlantisForce : ModItem
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000DB42 File Offset: 0x0000BD42
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 400000;
			base.Item.accessory = true;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(base.Mod.Name, "BismiteEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "CascadeEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "GraniteChunkEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "StreamSurferEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "SpiritEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "PrimalstoneEnchant").UpdateAccessory(player, false);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<BismiteEnchant>(1);
			recipe.AddIngredient<CascadeEnchant>(1);
			recipe.AddIngredient<GraniteChunkEnchant>(1);
			recipe.AddIngredient<StreamSurferEnchant>(1);
			recipe.AddIngredient<SpiritEnchant>(1);
			recipe.AddIngredient<PrimalstoneEnchant>(1);
			recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
			recipe.Register();
		}
	}
}
