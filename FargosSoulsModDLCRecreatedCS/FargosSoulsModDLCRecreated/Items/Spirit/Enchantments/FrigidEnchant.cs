using System;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Sets.FrigidSet;
using SpiritMod.Items.Sets.FrigidSet.FrigidArmor;
using SpiritMod.Items.Sets.FrigidSet.Frostbite;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000071 RID: 113
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class FrigidEnchant : ModItem
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000E346 File Offset: 0x0000C546
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 30000;
			base.Item.accessory = true;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000E384 File Offset: 0x0000C584
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidGloves").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidHelm").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidLegs").UpdateArmorSet(player);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000E400 File Offset: 0x0000C600
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<FrigidHelm>(1);
			recipe.AddIngredient<FrigidChestplate>(1);
			recipe.AddIngredient<FrigidLegs>(1);
			recipe.AddIngredient<IcySpear>(1);
			recipe.AddIngredient<HowlingScepter>(1);
			recipe.AddIngredient<FrigidGloves>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000043 RID: 67
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
