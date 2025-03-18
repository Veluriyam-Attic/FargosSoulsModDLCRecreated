using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.SlagSet;
using SpiritMod.Items.Sets.SlagSet.FieryArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000081 RID: 129
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class SlagTyrantEnchant : ModItem
	{
		// Token: 0x06000217 RID: 535 RVA: 0x0000EC53 File Offset: 0x0000CE53
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 30000;
			base.Item.accessory = true;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000F220 File Offset: 0x0000D420
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "CimmerianScepter").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusHelm").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusGreaves").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusPlate").UpdateArmorSet(player);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000F29C File Offset: 0x0000D49C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<ObsidiusHelm>(1);
			recipe.AddIngredient<ObsidiusGreaves>(1);
			recipe.AddIngredient<ObsidiusPlate>(1);
			recipe.AddIngredient<Blasphemer>(1);
			recipe.AddIngredient<FierySummonStaff>(1);
			recipe.AddIngredient<CimmerianScepter>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000051 RID: 81
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
