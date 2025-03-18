using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.InfernonDrops;
using SpiritMod.Items.BossLoot.InfernonDrops.InfernonArmor;
using SpiritMod.Items.Sets.MagicMisc.TerraStaffTree;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000072 RID: 114
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PainMongerEnchant : ModItem
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000E46B File Offset: 0x0000C66B
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 40000;
			base.Item.accessory = true;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalVisor").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalBreastplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalGreaves").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "HellEater").UpdateAccessory(player, false);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000E524 File Offset: 0x0000C724
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<InfernalVisor>(1);
			recipe.AddIngredient<InfernalBreastplate>(1);
			recipe.AddIngredient<InfernalGreaves>(1);
			recipe.AddIngredient<InfernalStaff>(1);
			recipe.AddIngredient<HellStaff>(1);
			recipe.AddIngredient<HellEater>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000044 RID: 68
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
