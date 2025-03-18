using System;
using SpiritMod.Items.Armor.Daybloom;
using SpiritMod.Items.Placeable.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000075 RID: 117
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class SunflowerEnchant : ModItem
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E774 File Offset: 0x0000C974
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomHead").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomBody").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomLegs").UpdateArmorSet(player);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<DaybloomHead>(1);
			recipe.AddIngredient<DaybloomBody>(1);
			recipe.AddIngredient<DaybloomLegs>(1);
			recipe.AddIngredient<BriarFlowerItem>(1);
			recipe.AddIngredient<HangingSunPot>(3);
			recipe.AddIngredient(4429, 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000046 RID: 70
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
