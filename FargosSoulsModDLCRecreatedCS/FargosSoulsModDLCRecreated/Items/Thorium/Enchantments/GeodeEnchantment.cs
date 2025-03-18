using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Geode;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Projectiles.LightPets;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000031 RID: 49
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class GeodeEnchantment : ModItem
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000067D8 File Offset: 0x000049D8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000683C File Offset: 0x00004A3C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			player.GetModPlayer<DLCPlayer>().magicLanternActive = true;
			player.GetModPlayer<DLCPlayer>().davyJonesLockboxActive = true;
			bool flag = AccessoryEffectLoader.AddEffect<GeodeEnchantment.MagicLanternEffect>(player, base.Item);
			bool davyJonesLockBoxEffect = AccessoryEffectLoader.AddEffect<GeodeEnchantment.DavyJonesLockboxEffect>(player, base.Item);
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<GeodeEnchantment.GeodeEffects>(player, base.Item))
			{
				Lighting.AddLight(player.Center, 1.2f, 0.8f, 1.2f);
				PlayerHelper.GetThoriumPlayer(player).setGeode = true;
			}
			int minionType = 492;
			if (flag)
			{
				if (player.ownedProjectileCounts[minionType] < 1 && player.miscEquips[1].type != 3043)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == 492 && Main.projectile[i].active && player.miscEquips[1].type != 3043)
					{
						Main.projectile[i].Kill();
					}
				}
			}
			minionType = ModContent.ProjectileType<DavyJonesLockBoxPro>();
			if (davyJonesLockBoxEffect)
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
			else
			{
				for (int j = 0; j < Main.maxProjectiles; j++)
				{
					if (Main.projectile[j].type == ModContent.ProjectileType<DavyJonesLockBoxPro>() && Main.projectile[j].active && player.miscEquips[1].type != ModContent.ItemType<DavyJonesLockBox>())
					{
						Main.projectile[j].Kill();
					}
				}
			}
			ModItem magLant;
			if (thorium.TryFind<ModItem>("MagicLantern", ref magLant))
			{
				magLant.UpdateAccessory(player, hideVisual);
			}
			ModItem dJLB;
			if (thorium.TryFind<ModItem>("DavyJonesLockBox", ref dJLB))
			{
				dJLB.UpdateAccessory(player, hideVisual);
			}
			ModItem cS;
			if (thorium.TryFind<ModItem>("CrystalScorpion", ref cS))
			{
				cS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006A88 File Offset: 0x00004C88
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<GeodeHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GeodeChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GeodeGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CrystalScorpion>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GeodeSaxophone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EnchantedPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Xylophone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GlitteringScepter>(), 1);
			recipe.AddIngredient(3043, 1);
			recipe.AddIngredient(ModContent.ItemType<DavyJonesLockBox>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000CD RID: 205
		public class GeodeEffects : AccessoryEffect
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000349 RID: 841 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600034A RID: 842 RVA: 0x00018B89 File Offset: 0x00016D89
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GeodeEnchantment>();
				}
			}
		}

		// Token: 0x020000CE RID: 206
		public class MagicLanternEffect : AccessoryEffect
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600034C RID: 844 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600034D RID: 845 RVA: 0x00018B89 File Offset: 0x00016D89
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GeodeEnchantment>();
				}
			}
		}

		// Token: 0x020000CF RID: 207
		public class DavyJonesLockboxEffect : AccessoryEffect
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600034F RID: 847 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000350 RID: 848 RVA: 0x00018B89 File Offset: 0x00016D89
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GeodeEnchantment>();
				}
			}
		}
	}
}
