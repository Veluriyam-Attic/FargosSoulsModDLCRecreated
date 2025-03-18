using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Darksteel;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003F RID: 63
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SteelEnchantment : ModItem
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00008848 File Offset: 0x00006A48
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000088AC File Offset: 0x00006AAC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).thoriumEndurance += 0.08f;
			}
			ModItem spikedBracer;
			if (thorium.TryFind<ModItem>("SpikedBracer", ref spikedBracer))
			{
				spikedBracer.UpdateAccessory(player, hideVisual);
			}
			ModItem weightedWinglets;
			if (AccessoryEffectLoader.AddEffect<SteelEnchantment.WeightedWingletsEffect>(player, base.Item) && thorium.TryFind<ModItem>("WeightedWinglets", ref weightedWinglets))
			{
				weightedWinglets.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008930 File Offset: 0x00006B30
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SteelHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteelChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteelGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WeightedWinglets>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpikedBracer>(), 1);
			recipe.AddIngredient(2273, 1);
			recipe.AddIngredient(ModContent.ItemType<SteelAxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<dDarksteelMallet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteelBlade>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WarForger>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000EB RID: 235
		public class WeightedWingletsEffect : AccessoryEffect
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600039E RID: 926 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600039F RID: 927 RVA: 0x00018C52 File Offset: 0x00016E52
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SteelEnchantment>();
				}
			}
		}
	}
}
