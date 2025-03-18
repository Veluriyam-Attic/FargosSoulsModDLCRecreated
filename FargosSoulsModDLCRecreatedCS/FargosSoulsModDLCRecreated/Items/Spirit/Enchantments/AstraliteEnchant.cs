using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.StarplateDrops;
using SpiritMod.Items.BossLoot.StarplateDrops.StarArmor;
using SpiritMod.Items.Placeable.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007A RID: 122
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class AstraliteEnchant : ModItem
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000EC53 File Offset: 0x0000CE53
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 30000;
			base.Item.accessory = true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StarMask").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StarPlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StarLegs").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "HighGravityBoots").UpdateAccessory(player, false);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<StarMask>(1);
			recipe.AddIngredient<StarPlate>(1);
			recipe.AddIngredient<StarLegs>(1);
			recipe.AddIngredient<OrionPistol>(1);
			recipe.AddIngredient<HighGravityBoots>(1);
			recipe.AddIngredient<StarplatePainting>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400004B RID: 75
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
