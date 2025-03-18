using System;
using SpiritMod.Items.Accessory.SanguineWardTree;
using SpiritMod.Items.Accessory.TalismanTree.GrislyTongue;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.BloodcourtSet.BloodCourt;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200008A RID: 138
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class BloodcourtEnchant : ModItem
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtHead").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtLeggings").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodWard").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "GrislyTongue").UpdateAccessory(player, false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<BloodCourtHead>(1);
			recipe.AddIngredient<BloodCourtChestplate>(1);
			recipe.AddIngredient<BloodCourtLeggings>(1);
			recipe.AddIngredient<HealingCodex>(1);
			recipe.AddIngredient<GrislyTongue>(1);
			recipe.AddIngredient<BloodWard>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000059 RID: 89
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
