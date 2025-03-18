using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005A RID: 90
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class EbonEnchantment : ModItem
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000B954 File Offset: 0x00009B54
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).darkAura = true;
			}
			ModItem darkHeart;
			if (mod.TryFind<ModItem>("DarkHeart", ref darkHeart))
			{
				darkHeart.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<EbonHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EbonCloak>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EbonLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkHeart>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LeechBolt>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadowWand>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
