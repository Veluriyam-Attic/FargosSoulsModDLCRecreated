using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Hydrothermic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Summon;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000096 RID: 150
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class AtaxiaEnchantment : ModItem
	{
		// Token: 0x06000278 RID: 632 RVA: 0x000120B0 File Offset: 0x000102B0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 1000000;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00012114 File Offset: 0x00010314
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Inferno effect when below 50% life"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "You emit a blazing explosion when you are hit"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Melee attacks and projectiles cause chaos flames to erupt on enemy hits"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "You fire a homing chaos flare when using ranged weapons every 0.33 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Magic attacks summon damaging and healing flare orbs on hit"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Summons a hydrothermic vent to protect you"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Rogue weapons unleash a volley of homing chaos flames around the player every 2 seconds"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip71", "Symphonic critical strikes cause targets to erupt in sulfide flames"));
				list.Add(new TooltipLine(base.Mod, "Tooltip72", "Only one eruption can occur and have a 3 second cooldown per trigger"));
				list.Add(new TooltipLine(base.Mod, "Tooltip73", "Taking more than 75 damage spawns an oasis that lasts 3 seconds"));
				list.Add(new TooltipLine(base.Mod, "Tooltip74", "The oasis heals 6 life every 1/3 of a second"));
				list.Add(new TooltipLine(base.Mod, "Tooltip75", "Only one oasis can be active at a time"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Hallowed Rune, Regenator, and Ethereal Extorter"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Flesh Totem, Regenator, and Ethereal Extorter"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(194, 89, 89));
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00012330 File Offset: 0x00010530
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			bool ataxariaEffectApplied = AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.AtaxariaVentSpirit>(player, base.Item);
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer modPlayer = player.Calamity();
				if (AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.AtaxiaEffects>(player, base.Item))
				{
					modPlayer.ataxiaBlaze = true;
					modPlayer.ataxiaGeyser = true;
					modPlayer.ataxiaBolt = true;
					modPlayer.ataxiaMage = true;
					if (ModLoader.HasMod("CalamityBardHealer"))
					{
						ModPlayer thorlamityPlayer = ModLoader.GetMod("CalamityBardHealer").Find<ModPlayer>("ThorlamityPlayer");
						FieldInfo omniSpeakerField = thorlamityPlayer.GetType().GetField("ataxiaBard", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (omniSpeakerField != null)
						{
							omniSpeakerField.SetValue(thorlamityPlayer, true);
						}
						else
						{
							Mod mod = ModLoader.GetMod("YourModName");
							if (mod != null)
							{
								mod.Logger.Warn("ataxiaBard field not found in ThorlamityPlayer.");
							}
						}
						FieldInfo omniSpeakerField2 = thorlamityPlayer.GetType().GetField("ataxiaHealer", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (omniSpeakerField2 != null)
						{
							omniSpeakerField2.SetValue(thorlamityPlayer, true);
						}
						else
						{
							Mod mod2 = ModLoader.GetMod("YourModName");
							if (mod2 != null)
							{
								mod2.Logger.Warn("ataxiaHealer field not found in ThorlamityPlayer.");
							}
						}
					}
					modPlayer.ataxiaVolley = true;
				}
				modPlayer.chaosSpirit = true;
			}
			int minionType = ModContent.ProjectileType<HydrothermicVent>();
			if (ataxariaEffectApplied)
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, ModContent.ProjectileType<HydrothermicVent>(), 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
				if (player.whoAmI == Main.myPlayer && !player.HasBuff(calamity.Find<ModBuff>("HydrothermicVentBuff").Type))
				{
					player.AddBuff(calamity.Find<ModBuff>("HydrothermicVentBuff").Type, 3600, true, false);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == ModContent.ProjectileType<HydrothermicVent>() && Main.projectile[i].active)
					{
						Main.projectile[i].Kill();
					}
				}
			}
			ModItem fleshTotem;
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem hallowedRune;
				if (AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.HallowedRuneEffects>(player, base.Item) && calamity.TryFind<ModItem>("HallowedRune", ref hallowedRune))
				{
					hallowedRune.UpdateAccessory(player, hideVisual);
				}
			}
			else if (AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.FleshTotemEffects>(player, base.Item) && calamity.TryFind<ModItem>("FleshTotem", ref fleshTotem))
			{
				fleshTotem.UpdateAccessory(player, hideVisual);
			}
			ModItem etherealExtorter;
			if (AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.EtherealExtorterEffects>(player, base.Item) && calamity.TryFind<ModItem>("EtherealExtorter", ref etherealExtorter))
			{
				etherealExtorter.UpdateAccessory(player, hideVisual);
			}
			ModItem regenator;
			if (AccessoryEffectLoader.AddEffect<AtaxiaEnchantment.RegenatorEffects>(player, base.Item) && calamity.TryFind<ModItem>("Regenator", ref regenator))
			{
				regenator.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000125FC File Offset: 0x000107FC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Hydrothermic Helmet";
				}, new int[]
				{
					ModContent.ItemType<HydrothermicHeadMagic>(),
					ModContent.ItemType<HydrothermicHeadMelee>(),
					ModContent.ItemType<HydrothermicHeadRanged>(),
					ModContent.ItemType<HydrothermicHeadRogue>(),
					ModContent.ItemType<HydrothermicHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Hydrothermic Helmet";
				}, new int[]
				{
					ModContent.ItemType<HydrothermicHeadMagic>(),
					ModContent.ItemType<HydrothermicHeadMelee>(),
					ModContent.ItemType<HydrothermicHeadRanged>(),
					ModContent.ItemType<HydrothermicHeadRogue>(),
					ModContent.ItemType<HydrothermicHeadSummon>(),
					calamityBardHealer.Find<ModItem>("HydrothermicGasMask").Type,
					calamityBardHealer.Find<ModItem>("HydrothermicHat").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyHydrothermicHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyHydrothermicHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<HydrothermicArmor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HydrothermicSubligar>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<HallowedRune>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<FleshTotem>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<EtherealExtorter>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Regenator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BarracudaGun>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000148 RID: 328
		public class AtaxariaVentSpirit : AccessoryEffect
		{
			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}

		// Token: 0x02000149 RID: 329
		public class AtaxiaEffects : AccessoryEffect
		{
			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}

		// Token: 0x0200014A RID: 330
		public class HallowedRuneEffects : AccessoryEffect
		{
			// Token: 0x060004C1 RID: 1217 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}

		// Token: 0x0200014B RID: 331
		public class FleshTotemEffects : AccessoryEffect
		{
			// Token: 0x060004C5 RID: 1221 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}

		// Token: 0x0200014C RID: 332
		public class EtherealExtorterEffects : AccessoryEffect
		{
			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}

		// Token: 0x0200014D RID: 333
		public class RegenatorEffects : AccessoryEffect
		{
			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x00018F32 File Offset: 0x00017132
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AtaxiaEnchantment>();
				}
			}
		}
	}
}
