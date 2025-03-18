using System;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.MarbleSet;
using SpiritMod.Items.Sets.MarbleSet.MarbleArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007B RID: 123
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class MarbleChunkEnchant : ModItem
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000ED77 File Offset: 0x0000CF77
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "MarbleHelm").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "MarbleChest").UpdateArmorSet(player);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<MarbleHelm>(1);
			recipe.AddIngredient<MarbleChest>(1);
			recipe.AddIngredient<DefenseCodex>(1);
			recipe.AddIngredient<MarbleBident>(1);
			recipe.AddIngredient<MarbleStaff>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400004C RID: 76
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
