using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Armor.LeatherArmor;
using SpiritMod.Items.Weapon.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000087 RID: 135
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class MarksmanEnchant : ModItem
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			*player.GetCritChance(DamageClass.Generic) += 4f;
			ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherHood").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherPlate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "LeatherLegs").UpdateArmorSet(player);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<LeatherHood>(1);
			recipe.AddIngredient<LeatherPlate>(1);
			recipe.AddIngredient<LeatherLegs>(1);
			recipe.AddIngredient<Kunai_Throwing>(50);
			recipe.AddIngredient<Dartboard>(1);
			recipe.AddIngredient<MagnifyingGlass>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000057 RID: 87
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
