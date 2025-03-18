using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000048 RID: 72
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class CelestialEnchantment : ModItem
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000099D4 File Offset: 0x00007BD4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 6;
			base.Item.value = 150000;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009A35 File Offset: 0x00007C35
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<CelestialEnchantment.CelestialEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setCelestial = true;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009A78 File Offset: 0x00007C78
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CelestialCrown>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialVestment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialCarrier>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HeavenlyCloudScepter>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Revivify>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x020000FF RID: 255
		public class CelestialEffects : AccessoryEffect
		{
			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060003DE RID: 990 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060003DF RID: 991 RVA: 0x00018D62 File Offset: 0x00016F62
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<CelestialEnchantment>();
				}
			}
		}
	}
}
