using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.PlagueReaper;
using CalamityMod.Projectiles.Rogue;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x0200009F RID: 159
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class PlagueReaperEnchantment : ModItem
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00014E1C File Offset: 0x0001301C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 300000;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014E80 File Offset: 0x00013080
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "25% reduced ammo usage and 5% increased flight time"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Enemies receive 10% more damage from ranged projectiles when afflicted by the Plague"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Getting hit causes plague cinders to rain from above"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Press Y to blind yourself for 5 seconds but massively boost your ranged damage"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "This has a 25 second cooldown."));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Plague Hive, Plagued Fuel Pack, and The Camper"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Plague Hive, Alchemical Flask, and The Camper"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Corrupt Flask and Crimson Flask"));
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(70, 63, 69));
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014FE8 File Offset: 0x000131E8
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer modPlayer = player.Calamity();
				if (AccessoryEffectLoader.AddEffect<PlagueReaperEnchantment.PlagueReaperEffects>(player, base.Item))
				{
					modPlayer.plagueReaper = true;
					player.ammoCost75 = true;
					CooldownInstance cd;
					if (modPlayer.cooldowns.TryGetValue(PlagueBlackout.ID, ref cd) && cd.timeLeft > 1500)
					{
						player.blind = true;
						player.headcovered = true;
						player.blackout = true;
						*player.GetDamage<RangedDamageClass>() += 0.6f;
						*player.GetCritChance<RangedDamageClass>() += 20f;
					}
					if (player.whoAmI == Main.myPlayer)
					{
						IEntitySource source = player.GetSource_Accessory(base.Item, null);
						if (player.immune && player.miscCounter % 10 == 0)
						{
							int damage = (int)player.GetTotalDamage<RangedDamageClass>().ApplyTo(40f);
							damage = player.ApplyArmorAccDamageBonusesTo((float)damage);
							Projectile cinder = CalamityUtils.ProjectileRain(source, player.Center, 400f, 100f, 500f, 800f, 22f, ModContent.ProjectileType<TheSyringeCinder>(), damage, 4f, player.whoAmI);
							if (cinder.whoAmI.WithinBounds(Main.maxProjectiles))
							{
								cinder.DamageType = DamageClass.Generic;
							}
						}
					}
				}
			}
			ModItem toxicHeart;
			if (AccessoryEffectLoader.AddEffect<PlagueReaperEnchantment.ToxicHeartEffects>(player, base.Item) && calamity.TryFind<ModItem>("ToxicHeart", ref toxicHeart))
			{
				toxicHeart.UpdateAccessory(player, hideVisual);
			}
			ModItem alchFlask;
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem plaguedFuelPack;
				if (AccessoryEffectLoader.AddEffect<PlagueReaperEnchantment.PlagueFuelPackEffects>(player, base.Item) && calamity.TryFind<ModItem>("PlaguedFuelPack", ref plaguedFuelPack))
				{
					plaguedFuelPack.UpdateAccessory(player, hideVisual);
				}
			}
			else if (AccessoryEffectLoader.AddEffect<PlagueReaperEnchantment.AlchemicalFlaskEffects>(player, base.Item) && calamity.TryFind<ModItem>("AlchemicalFlask", ref alchFlask))
			{
				alchFlask.UpdateAccessory(player, hideVisual);
			}
			ModItem theCamper;
			if (AccessoryEffectLoader.AddEffect<PlagueReaperEnchantment.TheCamperEffects>(player, base.Item) && calamity.TryFind<ModItem>("TheCamper", ref theCamper))
			{
				theCamper.UpdateAccessory(player, hideVisual);
			}
			ModItem iNeedImmuneToBrainRot;
			if (calamity.TryFind<ModItem>("CorruptFlask", ref iNeedImmuneToBrainRot))
			{
				iNeedImmuneToBrainRot.UpdateAccessory(player, hideVisual);
			}
			ModItem crimsonFlask;
			if (calamity.TryFind<ModItem>("CrimsonFlask", ref crimsonFlask))
			{
				crimsonFlask.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00015228 File Offset: 0x00013428
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<PlagueReaperMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueReaperVest>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueReaperStriders>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ToxicHeart>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<PlaguedFuelPack>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<AlchemicalFlask>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<TheCamper>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CorruptFlask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CrimsonFlask>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400005F RID: 95
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x02000175 RID: 373
		public class ToxicHeartEffects : AccessoryEffect
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600054F RID: 1359 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000550 RID: 1360 RVA: 0x00019074 File Offset: 0x00017274
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueReaperEnchantment>();
				}
			}
		}

		// Token: 0x02000176 RID: 374
		public class PlagueFuelPackEffects : AccessoryEffect
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000554 RID: 1364 RVA: 0x00019074 File Offset: 0x00017274
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueReaperEnchantment>();
				}
			}
		}

		// Token: 0x02000177 RID: 375
		public class AlchemicalFlaskEffects : AccessoryEffect
		{
			// Token: 0x06000556 RID: 1366 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000558 RID: 1368 RVA: 0x00019074 File Offset: 0x00017274
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueReaperEnchantment>();
				}
			}
		}

		// Token: 0x02000178 RID: 376
		public class TheCamperEffects : AccessoryEffect
		{
			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600055A RID: 1370 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x0600055B RID: 1371 RVA: 0x00019074 File Offset: 0x00017274
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueReaperEnchantment>();
				}
			}
		}

		// Token: 0x02000179 RID: 377
		public class PlagueReaperEffects : AccessoryEffect
		{
			// Token: 0x1700017D RID: 381
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600055E RID: 1374 RVA: 0x00019074 File Offset: 0x00017274
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueReaperEnchantment>();
				}
			}
		}
	}
}
