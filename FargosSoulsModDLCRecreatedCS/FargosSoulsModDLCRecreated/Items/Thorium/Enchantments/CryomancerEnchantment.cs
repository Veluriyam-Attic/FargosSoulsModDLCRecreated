using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Blizzard;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Projectiles.Pets;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000053 RID: 83
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class CryomancerEnchantment : ModItem
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000AE44 File Offset: 0x00009044
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			player.GetModPlayer<DLCPlayer>().cryomancerActive = true;
			bool flag = AccessoryEffectLoader.AddEffect<CryomancerEnchantment.SnowyOwlPet>(player, base.Item);
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setCryomancer = true;
				if (AccessoryEffectLoader.AddEffect<CryomancerEnchantment.CryomancerEffect>(player, base.Item))
				{
					PlayerHelper.GetThoriumPlayer(player).orbital = true;
					PlayerHelper.GetThoriumPlayer(player).orbitalRotation8 = Utils.RotatedBy(PlayerHelper.GetThoriumPlayer(player).orbitalRotation8, -0.10000000149011612, default(Vector2));
				}
				else
				{
					PlayerHelper.GetThoriumPlayer(player).orbital = false;
				}
			}
			ModItem iBSH;
			if (mod.TryFind<ModItem>("IceBoundStriderHide", ref iBSH))
			{
				iBSH.UpdateAccessory(player, hideVisual);
			}
			ModItem bP;
			if (mod.TryFind<ModItem>("BlizzardPouch", ref bP))
			{
				bP.UpdateAccessory(player, hideVisual);
			}
			int minionType = ModContent.ProjectileType<ThoriumMod.Projectiles.Pets.SnowyOwlPet>();
			if (flag)
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == minionType && Main.projectile[i].active && player.miscEquips[0].type != ModContent.ItemType<ForgottenLetter>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
			ModItem icyEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("IcyEnchantment", ref icyEnchantment))
			{
				icyEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CryomancersCrown>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CryomancersTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CryomancersLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IcyEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IceBoundStriderHide>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlizzardPouch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IceFairyStaff>(), 1);
			recipe.AddIngredient(726, 1);
			recipe.AddIngredient(ModContent.ItemType<Cryotherapy>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ForgottenLetter>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShroudedbytheStormPaint>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000118 RID: 280
		public class CryomancerEffect : AccessoryEffect
		{
			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x00018E07 File Offset: 0x00017007
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<CryomancerEnchantment>();
				}
			}
		}

		// Token: 0x02000119 RID: 281
		public class SnowyOwlPet : AccessoryEffect
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x00018E07 File Offset: 0x00017007
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<CryomancerEnchantment>();
				}
			}
		}
	}
}
