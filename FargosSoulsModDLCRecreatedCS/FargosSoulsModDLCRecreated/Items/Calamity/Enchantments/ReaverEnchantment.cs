using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Typeless;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A7 RID: 167
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class ReaverEnchantment : ModItem
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0001773C File Offset: 0x0001593C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 400000;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000177A0 File Offset: 0x000159A0
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(54, 164, 66));
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00017824 File Offset: 0x00015A24
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			bool reaverEffectApplied = AccessoryEffectLoader.AddEffect<ReaverEnchantment.ReaverMinionEffects>(player, base.Item);
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (AccessoryEffectLoader.AddEffect<ReaverEnchantment.ReaverEffects>(player, base.Item))
				{
					modPlayer.reaverExplore = true;
					modPlayer.wearingRogueArmor = true;
					modPlayer.reaverDefense = true;
					player.findTreasure = true;
					player.blockRange += 4;
					player.aggro -= 200;
					modPlayer.reaverSpeed = true;
					modPlayer.wearingRogueArmor = true;
					player.autoJump = true;
					if (player.miscCounter % 3 == 2 && player.dashDelay > 0)
					{
						player.dashDelay--;
					}
				}
				if (reaverEffectApplied)
				{
					if (player.whoAmI == Main.myPlayer)
					{
						IEntitySource source = player.GetSource_ItemUse(base.Item, null);
						if (player.FindBuffIndex(ModContent.BuffType<ReaverOrbBuff>()) == -1)
						{
							player.AddBuff(ModContent.BuffType<ReaverOrbBuff>(), 3600, true, false);
						}
						if (player.ownedProjectileCounts[ModContent.ProjectileType<ReaverOrb>()] < 1)
						{
							Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<ReaverOrb>(), 0, 0f, player.whoAmI, 0f, 0f, 0f);
						}
					}
				}
				else
				{
					for (int i = 0; i < Main.maxProjectiles; i++)
					{
						if (Main.projectile[i].type == ModContent.ProjectileType<ReaverOrb>() && Main.projectile[i].active)
						{
							Main.projectile[i].Kill();
						}
					}
				}
				player.thorns += 0.33f;
				player.moveSpeed -= 0.3f;
				player.statDefense += 10;
				player.lifeRegen += 3;
				player.aggro += 600;
				player.noFallDmg = true;
				modPlayer.wearingRogueArmor = true;
				player.Calamity().reaverRegen = true;
			}
			ModItem bloomStone;
			if (AccessoryEffectLoader.AddEffect<ReaverEnchantment.BloomStoneEffects>(player, base.Item) && calamity.TryFind<ModItem>("BloomStone", ref bloomStone))
			{
				bloomStone.UpdateAccessory(player, hideVisual);
			}
			ModItem gloveOfRecklessness;
			if (AccessoryEffectLoader.AddEffect<ReaverEnchantment.GloveOfRecklessnessEffects>(player, base.Item) && calamity.TryFind<ModItem>("GloveOfRecklessness", ref gloveOfRecklessness))
			{
				gloveOfRecklessness.UpdateAccessory(player, hideVisual);
			}
			ModItem gloveOfPrecision;
			if (AccessoryEffectLoader.AddEffect<ReaverEnchantment.GloveOfPrecisionEffects>(player, base.Item) && calamity.TryFind<ModItem>("GloveOfPrecision", ref gloveOfPrecision))
			{
				gloveOfPrecision.UpdateAccessory(player, hideVisual);
			}
			ModItem theBee;
			if (AccessoryEffectLoader.AddEffect<ReaverEnchantment.TheBeeEffects>(player, base.Item) && calamity.TryFind<ModItem>("TheBee", ref theBee))
			{
				theBee.UpdateAccessory(player, hideVisual);
			}
			ModItem nOV;
			if (calamity.TryFind<ModItem>("NecklaceofVexation", ref nOV))
			{
				nOV.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00017ADC File Offset: 0x00015CDC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Reaver Helmet";
			}, new int[]
			{
				ModContent.ItemType<ReaverHeadExplore>(),
				ModContent.ItemType<ReaverHeadMobility>(),
				ModContent.ItemType<ReaverHeadTank>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyReaverHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyReaverHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<ReaverScaleMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ReaverCuisses>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloomStone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NecklaceofVexation>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GloveOfRecklessness>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GloveOfPrecision>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheBee>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SandSharknadoStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ArcNovaDiffuser>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000195 RID: 405
		public class ReaverEffects : AccessoryEffect
		{
			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}

		// Token: 0x02000196 RID: 406
		public class ReaverMinionEffects : AccessoryEffect
		{
			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}

		// Token: 0x02000197 RID: 407
		public class BloomStoneEffects : AccessoryEffect
		{
			// Token: 0x170001AF RID: 431
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}

		// Token: 0x02000198 RID: 408
		public class GloveOfRecklessnessEffects : AccessoryEffect
		{
			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x060005BC RID: 1468 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}

		// Token: 0x02000199 RID: 409
		public class GloveOfPrecisionEffects : AccessoryEffect
		{
			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x060005BE RID: 1470 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x060005BF RID: 1471 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}

		// Token: 0x0200019A RID: 410
		public class TheBeeEffects : AccessoryEffect
		{
			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00019135 File Offset: 0x00017335
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ReaverEnchantment>();
				}
			}
		}
	}
}
