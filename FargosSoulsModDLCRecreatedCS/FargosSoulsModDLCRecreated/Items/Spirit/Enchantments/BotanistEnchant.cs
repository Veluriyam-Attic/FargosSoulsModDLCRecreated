using System;
using SpiritMod.Items.Armor.BotanistSet;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.BriarChestLoot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007F RID: 127
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class BotanistEnchant : ModItem
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000F030 File Offset: 0x0000D230
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ReachBrooch").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistHat").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistBody").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "BotanistLegs").UpdateArmorSet(player);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<BotanistHat>(1);
			recipe.AddIngredient<BotanistBody>(1);
			recipe.AddIngredient<BotanistLegs>(1);
			recipe.AddIngredient<ReachBrooch>(1);
			recipe.AddIngredient<ForagerTableItem>(1);
			recipe.AddIngredient<SunPot>(5);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400004F RID: 79
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
