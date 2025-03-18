using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000066 RID: 102
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class LivingWoodEnchantment : ModItem
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000CE30 File Offset: 0x0000B030
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000CE94 File Offset: 0x0000B094
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setLivingWood = true;
			}
			int minionType = ModContent.ProjectileType<LivingWoodAcornPro>();
			if (AccessoryEffectLoader.AddEffect<LivingWoodEnchantment.LivingWoodMinion>(player, base.Item))
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
				if (player.whoAmI == Main.myPlayer && !player.HasBuff(thorium.Find<ModBuff>("LivingWoodAcornBuff").Type))
				{
					player.AddBuff(thorium.Find<ModBuff>("LivingWoodAcornBuff").Type, 3600, true, false);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == ModContent.ProjectileType<LivingWoodAcornPro>() && Main.projectile[i].active)
					{
						Main.projectile[i].Kill();
					}
				}
			}
			ModItem sOH;
			if (thorium.TryFind<ModItem>("SeedofHope", ref sOH))
			{
				sOH.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LivingWoodMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LivingWoodChestguard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LivingWoodBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SeedofHope>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LivingWoodAcorn>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AntlionStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChiTea>(), 5);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x02000130 RID: 304
		public class LivingWoodMinion : AccessoryEffect
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000472 RID: 1138 RVA: 0x00018E70 File Offset: 0x00017070
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LivingWoodEnchantment>();
				}
			}
		}
	}
}
