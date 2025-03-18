using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Icy;
using ThoriumMod.Projectiles;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000037 RID: 55
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class IcyEnchantment : ModItem
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007B48 File Offset: 0x00005D48
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<IcyEnchantment.IcyEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setIcy = true;
				int num = ModContent.ProjectileType<IcyArmorEffect1>();
				if (Main.myPlayer == player.whoAmI && player.ownedProjectileCounts[num] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis("setIcy"), player.Center, Vector2.Zero, num, 0, 0f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<IcyHeadgear>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IcyMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IcyGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IcyShard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FrostFury>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Flurry>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IceLance>(), 300);
			recipe.AddIngredient(ModContent.ItemType<IceShaver>(), 1);
			recipe.AddIngredient(670, 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000DE RID: 222
		public class IcyEffects : AccessoryEffect
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000377 RID: 887 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000378 RID: 888 RVA: 0x00018C02 File Offset: 0x00016E02
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IcyEnchantment>();
				}
			}
		}
	}
}
