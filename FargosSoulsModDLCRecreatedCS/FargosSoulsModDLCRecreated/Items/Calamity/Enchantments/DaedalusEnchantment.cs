using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Daedalus;
using CalamityMod.Projectiles.Summon;
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
	// Token: 0x0200009E RID: 158
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class DaedalusEnchantment : ModItem
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00014804 File Offset: 0x00012A04
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 500000;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00014868 File Offset: 0x00012A68
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "TooltipCustom1", "Icy magic envelopes you..."));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom2", "You reflect projectiles back at enemies"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom3", "Reflected projectiles deal 50% less damage to you"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom4", "This reflect has a 90 second cooldown which is shared with all other dodges and reflects"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom5", "Getting hit causes you to emit a blast of crystal shards"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom6", "You have a 10% chance to absorb physical attacks and projectiles when hit"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom7", "If you absorb an attack you are healed for 1/2 of that attack's damage"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom8", "A Daedalus crystal floats above you to protect you"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom9", "Rogue projectiles throw out crystal shards as they travel"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				list.Add(new TooltipLine(base.Mod, "TooltipCustom126", "Symphonic damage inflicts frostburn and frostbite on hit"));
			}
			list.Add(new TooltipLine(base.Mod, "TooltipCustom10", "You can glide to negate fall damage"));
			list.Add(new TooltipLine(base.Mod, "TooltipCustom11", "Effects of Scuttler's Jewel, Howls Heart, Frost Flare, Cryo Stone, Frost Barrier, and Permafrost's Concoction"));
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(64, 115, 164));
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014A3C File Offset: 0x00012C3C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			bool daedalusEffectApplied = AccessoryEffectLoader.AddEffect<DaedalusEnchantment.DaedalusMinionEffect>(player, base.Item);
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (AccessoryEffectLoader.AddEffect<DaedalusEnchantment.DaedalusEffects>(player, base.Item))
				{
					modPlayer.daedalusAbsorb = true;
					modPlayer.daedalusReflect = true;
					modPlayer.daedalusShard = true;
					modPlayer.daedalusSplit = true;
					if (ModLoader.HasMod("CalamityBardHealer"))
					{
						ModPlayer thorlamityPlayer = ModLoader.GetMod("CalamityBardHealer").Find<ModPlayer>("ThorlamityPlayer");
						FieldInfo omniSpeakerField = thorlamityPlayer.GetType().GetField("daedalusBard", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (omniSpeakerField != null)
						{
							omniSpeakerField.SetValue(thorlamityPlayer, true);
						}
					}
				}
				if (daedalusEffectApplied)
				{
					modPlayer.daedalusCrystal = true;
					if (player.whoAmI == Main.myPlayer)
					{
						IEntitySource source = player.GetSource_ItemUse(base.Item, null);
						if (player.FindBuffIndex(ModContent.BuffType<DaedalusCrystalBuff>()) == -1)
						{
							player.AddBuff(ModContent.BuffType<DaedalusCrystalBuff>(), 3600, true, false);
						}
						if (player.ownedProjectileCounts[ModContent.ProjectileType<DaedalusCrystal>()] < 1)
						{
							int baseDamage = player.ApplyArmorAccDamageBonusesTo(95f);
							int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo((float)baseDamage);
							Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<DaedalusCrystal>(), damage, 0f, Main.myPlayer, 50f, 0f, 0f).originalDamage = baseDamage;
						}
					}
				}
				ModItem permafrostConcoction;
				if (calamity.TryFind<ModItem>("PermafrostConcoction", ref permafrostConcoction))
				{
					permafrostConcoction.UpdateAccessory(player, hideVisual);
				}
				ModItem frostFlare;
				if (AccessoryEffectLoader.AddEffect<DaedalusEnchantment.FrostFlareEffect>(player, base.Item) && calamity.TryFind<ModItem>("FrostFlare", ref frostFlare))
				{
					frostFlare.UpdateAccessory(player, hideVisual);
				}
				ModItem frostBarrier;
				if (AccessoryEffectLoader.AddEffect<DaedalusEnchantment.FrostBarrierEffect>(player, base.Item) && calamity.TryFind<ModItem>("FrostBarrier", ref frostBarrier))
				{
					frostBarrier.UpdateAccessory(player, hideVisual);
				}
				ModItem cryoStone;
				if (AccessoryEffectLoader.AddEffect<DaedalusEnchantment.CryoStoneEffect>(player, base.Item) && calamity.TryFind<ModItem>("CryoStone", ref cryoStone))
				{
					cryoStone.UpdateAccessory(player, hideVisual);
				}
				ModItem hH;
				if (AccessoryEffectLoader.AddEffect<DaedalusEnchantment.HowlsHeartEffect>(player, base.Item) && calamity.TryFind<ModItem>("HowlsHeart", ref hH))
				{
					hH.UpdateAccessory(player, hideVisual);
				}
				ModItem snowRuffianEnchantment;
				if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("SnowRuffianEnchantment", ref snowRuffianEnchantment))
				{
					snowRuffianEnchantment.UpdateAccessory(player, hideVisual);
				}
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014C8C File Offset: 0x00012E8C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Daedalus Helmet";
				}, new int[]
				{
					ModContent.ItemType<DaedalusHeadMagic>(),
					ModContent.ItemType<DaedalusHeadMelee>(),
					ModContent.ItemType<DaedalusHeadRanged>(),
					ModContent.ItemType<DaedalusHeadRogue>(),
					ModContent.ItemType<DaedalusHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Daedalus Helmet";
				}, new int[]
				{
					ModContent.ItemType<DaedalusHeadMagic>(),
					ModContent.ItemType<DaedalusHeadMelee>(),
					ModContent.ItemType<DaedalusHeadRanged>(),
					ModContent.ItemType<DaedalusHeadRogue>(),
					ModContent.ItemType<DaedalusHeadSummon>(),
					calamityBardHealer.Find<ModItem>("DaedalusCowl").Type,
					calamityBardHealer.Find<ModItem>("DaedalusHat").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyDaedalusHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyDaedalusHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<DaedalusBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DaedalusLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SnowRuffianEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PermafrostsConcoction>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CryoStone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FrostBarrier>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HowlsHeart>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FrostFlare>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0200016E RID: 366
		public class DaedalusEffects : AccessoryEffect
		{
			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000539 RID: 1337 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x0600053A RID: 1338 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}

		// Token: 0x0200016F RID: 367
		public class DaedalusMinionEffect : AccessoryEffect
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600053C RID: 1340 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x0600053D RID: 1341 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}

		// Token: 0x02000170 RID: 368
		public class FrostFlareEffect : AccessoryEffect
		{
			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600053F RID: 1343 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000540 RID: 1344 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}

		// Token: 0x02000171 RID: 369
		public class CryoStoneEffect : AccessoryEffect
		{
			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000542 RID: 1346 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000543 RID: 1347 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}

		// Token: 0x02000172 RID: 370
		public class FrostBarrierEffect : AccessoryEffect
		{
			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000545 RID: 1349 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}

		// Token: 0x02000173 RID: 371
		public class HowlsHeartEffect : AccessoryEffect
		{
			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x00019035 File Offset: 0x00017235
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DaedalusEnchantment>();
				}
			}
		}
	}
}
