using System;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Prairie;
using SacredTools.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.SOA.Enchantments
{
	// Token: 0x0200000B RID: 11
	[JITWhenModsEnabled(new string[]
	{
		"SacredTools"
	})]
	[ExtendsFromMod(new string[]
	{
		"SacredTools"
	})]
	public class PrairieEnchantment : ModItem
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002A4C File Offset: 0x00000C4C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("SacredTools"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("SacredTools");
			if (ModLoader.HasMod("SacredTools"))
			{
				*player.GetDamage<RangedDamageClass>() += 0.05f;
			}
			ModItem hOTC;
			if (mod.TryFind<ModItem>("HeartOfTheCaverns", ref hOTC))
			{
				hOTC.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B14 File Offset: 0x00000D14
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<PrairieHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PrairieChest>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PrairieLegs>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HeartOfTheCaverns>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WoodJavelin>(), 300);
			recipe.AddIngredient(1809, 300);
			recipe.AddIngredient(ModContent.ItemType<GoldJavelin>(), 300);
			recipe.AddIngredient(55, 1);
			recipe.AddIngredient(287, 300);
			recipe.AddIngredient(162, 1);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
