using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Astral;
using CalamityMod.Items.Fishing.AstralCatches;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A1 RID: 161
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class AstralEnchantment : ModItem
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00015F04 File Offset: 0x00014104
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 1000000;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00015F68 File Offset: 0x00014168
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "The Astral Infection has consumed you..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "5% increased movement speed and +3 max minions"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "35% increased damage and 25% increased critical strike chance"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Whenever you crit an enemy, a barrage of stars will rain down"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "This effect has a 1 second cooldown before it can trigger again"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Radiance, Gravistar Sabaton, and Ursa Sergeant"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Hide of Astrum Deus, Gravistar Sabaton, and Ursa Sergeant"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(123, 99, 130));
				}
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000160B8 File Offset: 0x000142B8
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
				if (AccessoryEffectLoader.AddEffect<AstralEnchantment.AstralStarsEffect>(player, base.Item))
				{
					modPlayer.astralStarRain = true;
				}
				player.moveSpeed += 0.05f;
				*player.GetDamage<GenericDamageClass>() += 0.35f;
				player.maxMinions += 3;
				*player.GetCritChance<GenericDamageClass>() += 25f;
				player.Calamity().wearingRogueArmor = true;
			}
			ModItem hOAD;
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem radiance;
				if (AccessoryEffectLoader.AddEffect<AstralEnchantment.RadianceEffect>(player, base.Item) && calamity.TryFind<ModItem>("Radiance", ref radiance))
				{
					radiance.UpdateAccessory(player, hideVisual);
				}
			}
			else if (AccessoryEffectLoader.AddEffect<AstralEnchantment.HideOfAstrumDeusEffect>(player, base.Item) && calamity.TryFind<ModItem>("HideofAstrumDeus", ref hOAD))
			{
				hOAD.UpdateAccessory(player, hideVisual);
			}
			ModItem gravistarSabaton;
			if (AccessoryEffectLoader.AddEffect<AstralEnchantment.GravistarSabatonEffect>(player, base.Item) && calamity.TryFind<ModItem>("GravistarSabaton", ref gravistarSabaton))
			{
				gravistarSabaton.UpdateAccessory(player, hideVisual);
			}
			ModItem ursaSergeant;
			if (AccessoryEffectLoader.AddEffect<AstralEnchantment.UrsaSergeantEffect>(player, base.Item) && calamity.TryFind<ModItem>("UrsaSergeant", ref ursaSergeant))
			{
				ursaSergeant.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00016200 File Offset: 0x00014400
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<AstralHelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AstralBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AstralLeggings>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<Radiance>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<HideofAstrumDeus>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<GravistarSabaton>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UrsaSergeant>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x02000183 RID: 387
		public class AstralStarsEffect : AccessoryEffect
		{
			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x000190BA File Offset: 0x000172BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AstralEnchantment>();
				}
			}
		}

		// Token: 0x02000184 RID: 388
		public class RadianceEffect : AccessoryEffect
		{
			// Token: 0x0600057B RID: 1403 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x0600057D RID: 1405 RVA: 0x000190BA File Offset: 0x000172BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AstralEnchantment>();
				}
			}
		}

		// Token: 0x02000185 RID: 389
		public class GravistarSabatonEffect : AccessoryEffect
		{
			// Token: 0x1700018F RID: 399
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000580 RID: 1408 RVA: 0x000190BA File Offset: 0x000172BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AstralEnchantment>();
				}
			}
		}

		// Token: 0x02000186 RID: 390
		public class UrsaSergeantEffect : AccessoryEffect
		{
			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x000190BA File Offset: 0x000172BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AstralEnchantment>();
				}
			}
		}

		// Token: 0x02000187 RID: 391
		public class HideOfAstrumDeusEffect : AccessoryEffect
		{
			// Token: 0x06000585 RID: 1413 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000587 RID: 1415 RVA: 0x000190BA File Offset: 0x000172BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AstralEnchantment>();
				}
			}
		}
	}
}
