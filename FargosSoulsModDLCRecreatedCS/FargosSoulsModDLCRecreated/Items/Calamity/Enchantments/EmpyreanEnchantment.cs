using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Empyrean;
using CalamityMod.Items.Weapons.Ranged;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000094 RID: 148
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class EmpyreanEnchantment : ModItem
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00011A00 File Offset: 0x0000FC00
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 9;
			base.Item.value = 1000000;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011A64 File Offset: 0x0000FC64
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip1", "The power of an ancient god at your command…"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2", "9% increased rogue damage and velocity"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3", "+115 maximum stealth"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip4", "Rogue projectiles have special effects on enemy hits"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "Imbued with cosmic wrath and rage when you are damaged"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of The Community, The Absorber and Shield of the High Ruler"));
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(171, 19, 33));
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00011B8C File Offset: 0x0000FD8C
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
				if (AccessoryEffectLoader.AddEffect<EmpyreanEnchantment.EmpyreanEffects>(player, base.Item))
				{
					modPlayer.xerocSet = true;
					if (player.statLife <= (int)((double)player.statLifeMax2 * 0.5))
					{
						player.AddBuff(ModContent.BuffType<EmpyreanWrath>(), 2, true, false);
						player.AddBuff(ModContent.BuffType<EmpyreanRage>(), 2, true, false);
					}
				}
				*player.GetDamage<ThrowingDamageClass>() += 0.09f;
				modPlayer.rogueVelocity += 0.09f;
				modPlayer.rogueStealthMax += 1.15f;
				modPlayer.wearingRogueArmor = true;
			}
			ModItem theCommunity;
			if (calamity.TryFind<ModItem>("TheCommunity", ref theCommunity))
			{
				theCommunity.UpdateAccessory(player, hideVisual);
			}
			ModItem theAbsorber;
			if (AccessoryEffectLoader.AddEffect<EmpyreanEnchantment.TheAbsorberEffects>(player, base.Item) && calamity.TryFind<ModItem>("TheAbsorber", ref theAbsorber))
			{
				theAbsorber.UpdateAccessory(player, hideVisual);
			}
			ModItem linkShield;
			if (AccessoryEffectLoader.AddEffect<EmpyreanEnchantment.ShieldOfTheHighRulerEffects>(player, base.Item) && calamity.TryFind<ModItem>("ShieldoftheHighRuler", ref linkShield))
			{
				linkShield.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<EmpyreanMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EmpyreanCloak>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EmpyreanCuisses>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheCommunity>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheAbsorber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShieldoftheHighRuler>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ElementalBlaster>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x02000141 RID: 321
		public class EmpyreanEffects : AccessoryEffect
		{
			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00018F0C File Offset: 0x0001710C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<EmpyreanEnchantment>();
				}
			}
		}

		// Token: 0x02000142 RID: 322
		public class TheAbsorberEffects : AccessoryEffect
		{
			// Token: 0x060004A8 RID: 1192 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060004AA RID: 1194 RVA: 0x00018F0C File Offset: 0x0001710C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<EmpyreanEnchantment>();
				}
			}
		}

		// Token: 0x02000143 RID: 323
		public class ShieldOfTheHighRulerEffects : AccessoryEffect
		{
			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060004AC RID: 1196 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060004AD RID: 1197 RVA: 0x00018F0C File Offset: 0x0001710C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<EmpyreanEnchantment>();
				}
			}
		}
	}
}
