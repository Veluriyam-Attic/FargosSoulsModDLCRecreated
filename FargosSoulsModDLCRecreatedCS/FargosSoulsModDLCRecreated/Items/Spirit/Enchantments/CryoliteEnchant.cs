using System;
using SpiritMod.Items.Accessory.AceCardsSet;
using SpiritMod.Items.Armor.Masks;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.CryoliteSet.CryoliteArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000088 RID: 136
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class CryoliteEnchant : ModItem
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 40000;
			base.Item.accessory = true;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000F904 File Offset: 0x0000DB04
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "WinterHat").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FourOfAKind").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteHead").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteBody").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CryoliteLegs").UpdateArmorSet(player);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000F99C File Offset: 0x0000DB9C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<CryoliteHead>(1);
			recipe.AddIngredient<CryoliteBody>(1);
			recipe.AddIngredient<CryoliteLegs>(1);
			recipe.AddIngredient<SlowCodex>(1);
			recipe.AddIngredient<WinterHat>(1);
			recipe.AddIngredient<FourOfAKind>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000058 RID: 88
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
