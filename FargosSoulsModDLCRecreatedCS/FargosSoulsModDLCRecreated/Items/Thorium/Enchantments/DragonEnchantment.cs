using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Dragon;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Pets;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000046 RID: 70
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DragonEnchantment : ModItem
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00009610 File Offset: 0x00007810
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009674 File Offset: 0x00007874
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			player.GetModPlayer<DLCPlayer>().dreadActive = true;
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<DragonEnchantment.DragonEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).dragonSet = true;
			}
			int minionType = ModContent.ProjectileType<WyvernPet>();
			if (AccessoryEffectLoader.AddEffect<DragonEnchantment.WyvernPetEffect>(player, base.Item))
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
					if (Main.projectile[i].type == minionType && Main.projectile[i].active && player.miscEquips[0].type != ModContent.ItemType<CloudyChewToy>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
			ModItem tPMS;
			if (thorium.TryFind<ModItem>("TunePlayerMovementSpeed", ref tPMS))
			{
				tPMS.UpdateAccessory(player, hideVisual);
			}
			ModItem dTN;
			if (AccessoryEffectLoader.AddEffect<DragonEnchantment.DragonTallonNecklaceEffects>(player, base.Item) && thorium.TryFind<ModItem>("DragonTalonNecklace", ref dTN))
			{
				dTN.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000097B4 File Offset: 0x000079B4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DragonMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonTalonNecklace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TunePlayerMovementSpeed>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonsBreath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EbonyTail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CorrupterBalloon>(), 300);
			recipe.AddIngredient(ModContent.ItemType<CloudyChewToy>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000F9 RID: 249
		public class WyvernPetEffect : AccessoryEffect
		{
			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060003CC RID: 972 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060003CD RID: 973 RVA: 0x00018D54 File Offset: 0x00016F54
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DragonEnchantment>();
				}
			}
		}

		// Token: 0x020000FA RID: 250
		public class DragonEffects : AccessoryEffect
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003CF RID: 975 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x00018D54 File Offset: 0x00016F54
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DragonEnchantment>();
				}
			}
		}

		// Token: 0x020000FB RID: 251
		public class DragonTallonNecklaceEffects : AccessoryEffect
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060003D3 RID: 979 RVA: 0x00018D54 File Offset: 0x00016F54
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DragonEnchantment>();
				}
			}
		}
	}
}
