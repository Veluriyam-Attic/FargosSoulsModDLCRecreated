using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.RunicSet;
using SpiritMod.Items.Sets.RunicSet.RunicArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000084 RID: 132
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class RunicEnchant : ModItem
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000E64F File Offset: 0x0000C84F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 40000;
			base.Item.accessory = true;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000F530 File Offset: 0x0000D730
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RuneWizardScroll").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicHood").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicPlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicGreaves").UpdateArmorSet(player);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<RunicHood>(1);
			recipe.AddIngredient<RunicPlate>(1);
			recipe.AddIngredient<RunicGreaves>(1);
			recipe.AddIngredient<SpiritRune>(1);
			recipe.AddIngredient<RuneWizardScroll>(1);
			recipe.AddIngredient<SpiritBiomePainting>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000054 RID: 84
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
