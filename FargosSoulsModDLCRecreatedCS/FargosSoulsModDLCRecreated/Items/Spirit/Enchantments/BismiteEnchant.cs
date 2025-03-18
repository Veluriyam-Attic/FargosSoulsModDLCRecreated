using System;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Sets.BismiteSet;
using SpiritMod.Items.Sets.BismiteSet.BismiteArmor;
using SpiritMod.Items.Sets.BriarChestLoot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000085 RID: 133
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class BismiteEnchant : ModItem
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F618 File Offset: 0x0000D818
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteShield").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteHelmet").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteLeggings").UpdateArmorSet(player);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000F694 File Offset: 0x0000D894
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<BismiteHelmet>(1);
			recipe.AddIngredient<BismiteChestplate>(1);
			recipe.AddIngredient<BismiteLeggings>(1);
			recipe.AddIngredient<BismiteShield>(1);
			recipe.AddIngredient<BismiteChakra>(1);
			recipe.AddIngredient<ReachBoomerang>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000055 RID: 85
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
