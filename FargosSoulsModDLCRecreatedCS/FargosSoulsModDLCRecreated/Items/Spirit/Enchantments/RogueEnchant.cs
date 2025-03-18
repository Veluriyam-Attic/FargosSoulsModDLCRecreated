using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Armor;
using SpiritMod.Items.Weapon.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000078 RID: 120
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class RogueEnchant : ModItem
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RogueHood").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RoguePlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RoguePants").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "RogueCrest").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "SwiftRune").UpdateAccessory(player, false);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<RogueHood>(1);
			recipe.AddIngredient<RoguePlate>(1);
			recipe.AddIngredient<RoguePants>(1);
			recipe.AddIngredient<Kunai_Throwing>(50);
			recipe.AddIngredient<RogueCrest>(1);
			recipe.AddIngredient<SwiftRune>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000049 RID: 73
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
