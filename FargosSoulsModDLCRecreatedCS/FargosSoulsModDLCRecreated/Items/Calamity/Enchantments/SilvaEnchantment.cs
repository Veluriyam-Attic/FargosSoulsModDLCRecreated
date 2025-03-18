using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Silva;
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
	// Token: 0x0200009B RID: 155
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class SilvaEnchantment : ModItem
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00013AE0 File Offset: 0x00011CE0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 20000000;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00013B44 File Offset: 0x00011D44
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Boundless life energy cascades from you..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All projectiles spawn healing leaf orbs on enemy hits"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Max run speed and acceleration boosted by 5%"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "If you are reduced to 1 HP you will not die from any further damage for 8 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "This effect has a 5 minute cooldown. The cooldown does not decrement if any bosses or events are active."));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Magic projectiles which cannot pierce will occasionally set off potent blasts of nature energy"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Summons an ancient leaf prism to blast your enemies with life energy"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Taking heavy damage in one hit gives players with the lowest health the Guardian Angel buff for 30 seconds"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of The Amalgam, Godly Soul Artifact, and Yharim's Gift"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Blunder Booster, Godly Soul Artifact, and Dynamo Stem Cells"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(176, 112, 70));
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00013CF0 File Offset: 0x00011EF0
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			bool silvaEffectApplied = AccessoryEffectLoader.AddEffect<SilvaEnchantment.SilvaMinionEffects>(player, base.Item);
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.SilvaEffects>(player, base.Item))
				{
					modPlayer.silvaSet = true;
					modPlayer.silvaMage = true;
					modPlayer.silvaSet = true;
					if (ModLoader.HasMod("CalamityBardHealer"))
					{
						ModPlayer thorlamityPlayer = ModLoader.GetMod("CalamityBardHealer").Find<ModPlayer>("ThorlamityPlayer");
						FieldInfo omniSpeakerField = thorlamityPlayer.GetType().GetField("silvaHealer", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (omniSpeakerField != null)
						{
							omniSpeakerField.SetValue(thorlamityPlayer, true);
						}
						else
						{
							Mod mod = ModLoader.GetMod("YourModName");
							if (mod != null)
							{
								mod.Logger.Warn("silvaHealer field not found in ThorlamityPlayer.");
							}
						}
					}
				}
				if (silvaEffectApplied)
				{
					modPlayer.silvaSummon = true;
					modPlayer.WearingPostMLSummonerSet = true;
				}
				player.setBonus = ILocalizedModTypeExtensions.GetLocalizedValue(this, "SetBonus") + "\n" + CalamityUtils.GetTextValueFromModItem<SilvaArmor>("CommonSetBonus");
				if (silvaEffectApplied)
				{
					if (player.whoAmI == Main.myPlayer)
					{
						IEntitySource source = player.GetSource_ItemUse(base.Item, null);
						if (player.FindBuffIndex(ModContent.BuffType<SilvaCrystalBuff>()) == -1)
						{
							player.AddBuff(ModContent.BuffType<SilvaCrystalBuff>(), 3600, true, false);
						}
						if (player.ownedProjectileCounts[ModContent.ProjectileType<SilvaCrystal>()] < 1)
						{
							int baseDamage = player.ApplyArmorAccDamageBonusesTo(600f);
							int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo((float)baseDamage);
							int p = Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<SilvaCrystal>(), damage, 0f, Main.myPlayer, -20f, 0f, 0f);
							if (Utils.IndexInRange<Projectile>(Main.projectile, p))
							{
								Main.projectile[p].originalDamage = 600;
							}
						}
					}
				}
				else
				{
					for (int i = 0; i < Main.maxProjectiles; i++)
					{
						if (Main.projectile[i].type == ModContent.ProjectileType<SilvaCrystal>() && Main.projectile[i].active)
						{
							Main.projectile[i].Kill();
						}
					}
				}
				*player.GetDamage<SummonDamageClass>() += 0.65f;
				player.maxMinions += 5;
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem theAmalgam;
				if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.AmalgamEffects>(player, base.Item) && calamity.TryFind<ModItem>("TheAmalgam", ref theAmalgam))
				{
					theAmalgam.UpdateAccessory(player, hideVisual);
				}
				ModItem yharimsGift;
				if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.YharimsGiftEffect>(player, base.Item) && calamity.TryFind<ModItem>("YharimsGift", ref yharimsGift))
				{
					yharimsGift.UpdateAccessory(player, hideVisual);
				}
			}
			else
			{
				ModItem bB;
				if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.BlunderBoostEffect>(player, base.Item) && calamity.TryFind<ModItem>("BlunderBooster", ref bB))
				{
					bB.UpdateAccessory(player, hideVisual);
				}
				ModItem dSC;
				if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.DynamoStemCellsEffect>(player, base.Item) && calamity.TryFind<ModItem>("DynamoStemCells", ref dSC))
				{
					dSC.UpdateAccessory(player, hideVisual);
				}
			}
			ModItem auricSoulArtifact;
			if (AccessoryEffectLoader.AddEffect<SilvaEnchantment.AuricSoulArtifactEffects>(player, base.Item) && calamity.TryFind<ModItem>("AuricSoulArtifact", ref auricSoulArtifact))
			{
				auricSoulArtifact.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00014020 File Offset: 0x00012220
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Silva Helmet";
				}, new int[]
				{
					ModContent.ItemType<SilvaHeadMagic>(),
					ModContent.ItemType<SilvaHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Silva Helmet";
				}, new int[]
				{
					ModContent.ItemType<SilvaHeadMagic>(),
					ModContent.ItemType<SilvaHeadSummon>(),
					calamityBardHealer.Find<ModItem>("SilvaGuardianHelmet").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnySilvaHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnySilvaHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<SilvaArmor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SilvaLeggings>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<TheAmalgam>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<BlunderBooster>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<AuricSoulArtifact>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<YharimsGift>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<DynamoStemCells>(), 1);
			}
			recipe.AddTile(this.calamity, "DraedonsForge");
			recipe.Register();
		}

		// Token: 0x0400005D RID: 93
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x0200015F RID: 351
		public class SilvaEffects : AccessoryEffect
		{
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000506 RID: 1286 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000507 RID: 1287 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000160 RID: 352
		public class SilvaMinionEffects : AccessoryEffect
		{
			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000509 RID: 1289 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600050A RID: 1290 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000161 RID: 353
		public class AmalgamEffects : AccessoryEffect
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600050E RID: 1294 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000162 RID: 354
		public class AuricSoulArtifactEffects : AccessoryEffect
		{
			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000163 RID: 355
		public class YharimsGiftEffect : AccessoryEffect
		{
			// Token: 0x06000513 RID: 1299 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000514 RID: 1300 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000164 RID: 356
		public class BlunderBoostEffect : AccessoryEffect
		{
			// Token: 0x06000517 RID: 1303 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000518 RID: 1304 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000519 RID: 1305 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}

		// Token: 0x02000165 RID: 357
		public class DynamoStemCellsEffect : AccessoryEffect
		{
			// Token: 0x0600051B RID: 1307 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x0600051C RID: 1308 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x0600051D RID: 1309 RVA: 0x00018FC8 File Offset: 0x000171C8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilvaEnchantment>();
				}
			}
		}
	}
}
