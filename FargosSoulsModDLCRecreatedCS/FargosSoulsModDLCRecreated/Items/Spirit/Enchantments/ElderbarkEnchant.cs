using System;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.BriarChestLoot;
using SpiritMod.Items.Sets.BriarDrops;
using SpiritMod.Items.Sets.HuskstalkSet.ElderbarkArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000089 RID: 137
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class ElderbarkEnchant : ModItem
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000FA07 File Offset: 0x0000DC07
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic).Flat += 2f;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<ElderbarkHead>(1);
			recipe.AddIngredient<ElderbarkChest>(1);
			recipe.AddIngredient<ElderbarkLegs>(1);
			recipe.AddIngredient<ReachChestMagic>(1);
			recipe.AddIngredient<SanctifiedStabber>(1);
			recipe.AddIngredient<ReachPainting>(1);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
