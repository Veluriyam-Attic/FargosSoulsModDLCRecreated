using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.MageTree;
using SpiritMod.Items.Sets.SeraphSet;
using SpiritMod.Items.Sets.SeraphSet.SeraphArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000082 RID: 130
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class SeraphEnchant : ModItem
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000F308 File Offset: 0x0000D508
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FallenAngel").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphimBulwark").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphHelm").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphArmor").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SeraphLegs").UpdateArmorSet(player);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<SeraphHelm>(1);
			recipe.AddIngredient<SeraphArmor>(1);
			recipe.AddIngredient<SeraphLegs>(1);
			recipe.AddIngredient<GlowSting>(1);
			recipe.AddIngredient<SeraphimBulwark>(1);
			recipe.AddIngredient<FallenAngel>(1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000052 RID: 82
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
