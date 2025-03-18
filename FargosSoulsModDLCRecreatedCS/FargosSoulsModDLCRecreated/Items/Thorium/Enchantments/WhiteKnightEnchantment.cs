using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Projectiles.Pets;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002A RID: 42
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class WhiteKnightEnchantment : ModItem
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000599C File Offset: 0x00003B9C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 150000;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005A00 File Offset: 0x00003C00
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			player.GetModPlayer<DLCPlayer>().harbingerActive = true;
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<WhiteKnightEnchantment.WhiteKnightEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setWhiteKnight = true;
			}
			ModItem sB;
			if (AccessoryEffectLoader.AddEffect<WhiteKnightEnchantment.SmotheringBandEffects>(player, base.Item) && thorium.TryFind<ModItem>("SmotheringBand", ref sB))
			{
				sB.UpdateAccessory(player, hideVisual);
			}
			int minionType = ModContent.ProjectileType<LilMog>();
			if (AccessoryEffectLoader.AddEffect<WhiteKnightEnchantment.LilMogPetEffects>(player, base.Item))
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
					return;
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == minionType && Main.projectile[i].active && player.miscEquips[0].type != ModContent.ItemType<DelectableNut>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005B28 File Offset: 0x00003D28
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WhiteKnightMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteKnightTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteKnightLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SmotheringBand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PrismStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VileSpitter>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DelectableNut>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BoreanFangStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitaniumStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DynastyWarFan>(), 1);
			recipe.AddIngredient(3787, 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000BC RID: 188
		public class WhiteKnightEffects : AccessoryEffect
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000316 RID: 790 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000317 RID: 791 RVA: 0x00018B2C File Offset: 0x00016D2C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhiteKnightEnchantment>();
				}
			}
		}

		// Token: 0x020000BD RID: 189
		public class SmotheringBandEffects : AccessoryEffect
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000319 RID: 793 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600031A RID: 794 RVA: 0x00018B2C File Offset: 0x00016D2C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhiteKnightEnchantment>();
				}
			}
		}

		// Token: 0x020000BE RID: 190
		public class LilMogPetEffects : AccessoryEffect
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600031C RID: 796 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600031D RID: 797 RVA: 0x00018B2C File Offset: 0x00016D2C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhiteKnightEnchantment>();
				}
			}
		}
	}
}
