using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.HealerItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000065 RID: 101
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class TemplarEnchantment : ModItem
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (Main.myPlayer != player.whoAmI)
				{
					return;
				}
				for (int i = 0; i < 255; i++)
				{
					Player val = Main.player[i];
					if (i != Main.myPlayer && val.active && !val.dead && val.statLife < val.statLifeMax2 / 2)
					{
						player.AddBuff(ModContent.BuffType<TemplarSetBuff>(), 120, true, false);
						return;
					}
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TemplarsCirclet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TemplarsTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TemplarsLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifesGift>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TemplarsGrace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Prophecy>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
