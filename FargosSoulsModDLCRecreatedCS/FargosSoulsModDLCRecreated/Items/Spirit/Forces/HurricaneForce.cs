using System;
using FargosSoulsModDLCRecreated.Items.Spirit.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Forces
{
	// Token: 0x0200006F RID: 111
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class HurricaneForce : ModItem
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000DB42 File Offset: 0x0000BD42
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 400000;
			base.Item.accessory = true;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(base.Mod.Name, "RogueEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "ChitinEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "ApostleEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "MarbleChunkEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "AstraliteEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "SeraphEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "RunicEnchant").UpdateAccessory(player, false);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<RogueEnchant>(1);
			recipe.AddIngredient<ChitinEnchant>(1);
			recipe.AddIngredient<ApostleEnchant>(1);
			recipe.AddIngredient<MarbleChunkEnchant>(1);
			recipe.AddIngredient<AstraliteEnchant>(1);
			recipe.AddIngredient<SeraphEnchant>(1);
			recipe.AddIngredient<RunicEnchant>(1);
			recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
			recipe.Register();
		}
	}
}
