using System;
using FargosSoulsModDLCRecreated.Items.Spirit.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Forces
{
	// Token: 0x0200006E RID: 110
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class FrostburnForce : ModItem
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000DB42 File Offset: 0x0000BD42
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 400000;
			base.Item.accessory = true;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(base.Mod.Name, "BloodcourtEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "CryoliteEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "DuskEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "FrigidEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "MarksmanEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "PainMongerEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "SlagTyrantEnchant").UpdateAccessory(player, false);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000DE80 File Offset: 0x0000C080
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<BloodcourtEnchant>(1);
			recipe.AddIngredient<CryoliteEnchant>(1);
			recipe.AddIngredient<DuskEnchant>(1);
			recipe.AddIngredient<FrigidEnchant>(1);
			recipe.AddIngredient<MarksmanEnchant>(1);
			recipe.AddIngredient<PainMongerEnchant>(1);
			recipe.AddIngredient<SlagTyrantEnchant>(1);
			recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
			recipe.Register();
		}
	}
}
