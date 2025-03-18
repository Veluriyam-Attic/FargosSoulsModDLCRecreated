using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.SeraphSet;
using SpiritMod.Items.Sets.SpiritSet;
using SpiritMod.Items.Sets.SpiritSet.SpiritArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007E RID: 126
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class SpiritEnchant : ModItem
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000EF48 File Offset: 0x0000D148
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ShadowSingeFang").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritHeadgear").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritBodyArmor").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritLeggings").UpdateArmorSet(player);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<SpiritHeadgear>(1);
			recipe.AddIngredient<SpiritBodyArmor>(1);
			recipe.AddIngredient<SpiritLeggings>(1);
			recipe.AddIngredient<SpiritSaber>(1);
			recipe.AddIngredient<GlowSting>(1);
			recipe.AddIngredient<ShadowSingeFang>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400004E RID: 78
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
