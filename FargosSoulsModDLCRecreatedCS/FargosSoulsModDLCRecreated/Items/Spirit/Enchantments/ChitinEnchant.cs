using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.ScarabeusDrops.ChitinArmor;
using SpiritMod.Items.BossLoot.ScarabeusDrops.LocustCrook;
using SpiritMod.Items.BossLoot.ScarabeusDrops.RadiantCane;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000080 RID: 128
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class ChitinEnchant : ModItem
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000F117 File Offset: 0x0000D317
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F154 File Offset: 0x0000D354
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinHelmet").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ChitinLeggings").UpdateArmorSet(player);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<ChitinHelmet>(1);
			recipe.AddIngredient<ChitinChestplate>(1);
			recipe.AddIngredient<ChitinLeggings>(1);
			recipe.AddIngredient<LocustCrook>(1);
			recipe.AddIngredient<RadiantCane>(1);
			recipe.AddIngredient<DesertSlab>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000050 RID: 80
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
