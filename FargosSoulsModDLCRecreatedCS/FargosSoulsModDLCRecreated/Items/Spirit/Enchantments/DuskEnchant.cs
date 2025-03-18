using System;
using SpiritMod.Items.Accessory.ElectricGuitar;
using SpiritMod.Items.BossLoot.DuskingDrops.DuskArmor;
using SpiritMod.Items.DonatorItems;
using SpiritMod.Items.Placeable.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000074 RID: 116
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class DuskEnchant : ModItem
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x0000E64F File Offset: 0x0000C84F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 40000;
			base.Item.accessory = true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E68C File Offset: 0x0000C88C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskHood").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskPlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskLeggings").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ElectricGuitar").UpdateAccessory(player, false);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E708 File Offset: 0x0000C908
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<DuskHood>(1);
			recipe.AddIngredient<DuskPlate>(1);
			recipe.AddIngredient<DuskLeggings>(1);
			recipe.AddIngredient<BladeofYouKai>(1);
			recipe.AddIngredient<ElectricGuitar>(1);
			recipe.AddIngredient<DuskingPainting>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000045 RID: 69
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
