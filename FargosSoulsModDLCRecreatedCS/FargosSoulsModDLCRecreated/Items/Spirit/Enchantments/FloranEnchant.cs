using System;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.FloranSet;
using SpiritMod.Items.Sets.FloranSet.FloranArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007D RID: 125
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class FloranEnchant : ModItem
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000EE60 File Offset: 0x0000D060
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FHelmet").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FPlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FLegs").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FloranCharm").UpdateAccessory(player, false);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<FHelmet>(1);
			recipe.AddIngredient<FPlate>(1);
			recipe.AddIngredient<FLegs>(1);
			recipe.AddIngredient<StaminaCodex>(1);
			recipe.AddIngredient<FloranBludgeon>(1);
			recipe.AddIngredient<FloranCharm>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400004D RID: 77
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
