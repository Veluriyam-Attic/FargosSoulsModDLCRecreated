using System;
using SpiritMod.Items.BossLoot.AtlasDrops;
using SpiritMod.Items.BossLoot.AtlasDrops.PrimalstoneArmor;
using SpiritMod.Items.Sets.SwordsMisc.EternalSwordTree;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000083 RID: 131
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class PrimalstoneEnchant : ModItem
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000F40B File Offset: 0x0000D60B
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000F448 File Offset: 0x0000D648
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "TitanboundBulwark").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneFaceplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneBreastplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneLeggings").UpdateArmorSet(player);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<PrimalstoneFaceplate>(1);
			recipe.AddIngredient<PrimalstoneBreastplate>(1);
			recipe.AddIngredient<PrimalstoneLeggings>(1);
			recipe.AddIngredient<DemoncomboSword>(1);
			recipe.AddIngredient<Mountain>(1);
			recipe.AddIngredient<TitanboundBulwark>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000053 RID: 83
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
