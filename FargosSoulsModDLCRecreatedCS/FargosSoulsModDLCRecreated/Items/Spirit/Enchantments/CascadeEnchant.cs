using System;
using SpiritMod.Items.Sets.CascadeSet.Armor;
using SpiritMod.Items.Sets.ReefhunterSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000077 RID: 119
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class CascadeEnchant : ModItem
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000E928 File Offset: 0x0000CB28
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeHelmet").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CascadeLeggings").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "PendantOfTheOcean").UpdateAccessory(player, false);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<CascadeHelmet>(1);
			recipe.AddIngredient<CascadeChestplate>(1);
			recipe.AddIngredient<CascadeLeggings>(1);
			recipe.AddIngredient<ReefSpear>(1);
			recipe.AddIngredient<UrchinStaff>(1);
			recipe.AddIngredient<PendantOfTheOcean>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000048 RID: 72
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
