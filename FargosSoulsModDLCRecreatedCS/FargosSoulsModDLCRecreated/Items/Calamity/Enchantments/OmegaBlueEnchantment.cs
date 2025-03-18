using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.OmegaBlue;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x0200009A RID: 154
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class OmegaBlueEnchantment : ModItem
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0001371C File Offset: 0x0001191C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 13;
			base.Item.value = 1000000;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00013780 File Offset: 0x00011980
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "The darkness of the Abyss has overwhelmed you..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Increases armor penetration by 15"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "10% increased damage and critical strike chance and +2 max minions"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Short-ranged tentacles heal you by sucking enemy life"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Press Y to activate abyssal madness for 5 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Abyssal madness increases damage, critical strike chance, and tentacle aggression/range"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "This effect has a 25 second cooldown"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of the Abyssal Diving Suit and Reaper Tooth Necklace"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Old Duke Scales and Reaper Tooth Necklace"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(35, 95, 161));
				}
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00013908 File Offset: 0x00011B08
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (AccessoryEffectLoader.AddEffect<OmegaBlueEnchantment.OmegaBlueTentaclesEffect>(player, base.Item))
				{
					modPlayer.omegaBlueSet = true;
					calamity.Call(new object[]
					{
						"SetSetBonus",
						player,
						"omegablue",
						true
					});
				}
				else
				{
					modPlayer.omegaBlueSet = false;
					calamity.Call(new object[]
					{
						"SetSetBonus",
						player,
						"omegablue",
						false
					});
				}
				*player.GetArmorPenetration<GenericDamageClass>() += 15f;
				player.maxMinions += 2;
			}
			ModItem oDS;
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem abyssalDivingSuit;
				if (AccessoryEffectLoader.AddEffect<OmegaBlueEnchantment.AbyssalDivingSuitEffect>(player, base.Item) && calamity.TryFind<ModItem>("AbyssalDivingSuit", ref abyssalDivingSuit))
				{
					abyssalDivingSuit.UpdateAccessory(player, hideVisual);
				}
			}
			else if (AccessoryEffectLoader.AddEffect<OmegaBlueEnchantment.OldDukeScalesEffect>(player, base.Item) && calamity.TryFind<ModItem>("OldDukeScales", ref oDS))
			{
				oDS.UpdateAccessory(player, hideVisual);
			}
			ModItem reaperToothNecklace;
			if (calamity.TryFind<ModItem>("ReaperToothNecklace", ref reaperToothNecklace))
			{
				reaperToothNecklace.UpdateAccessory(player, hideVisual);
			}
			ModItem mutatedTruffle;
			if (calamity.TryFind<ModItem>("MutatedTruffle", ref mutatedTruffle))
			{
				mutatedTruffle.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00013A5C File Offset: 0x00011C5C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<OmegaBlueHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OmegaBlueChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OmegaBlueTentacles>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<AbyssalDivingSuit>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<OldDukeScales>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<ReaperToothNecklace>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0200015C RID: 348
		public class OmegaBlueTentaclesEffect : AccessoryEffect
		{
			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004FB RID: 1275 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x00018FC1 File Offset: 0x000171C1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OmegaBlueEnchantment>();
				}
			}
		}

		// Token: 0x0200015D RID: 349
		public class AbyssalDivingSuitEffect : AccessoryEffect
		{
			// Token: 0x060004FE RID: 1278 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000500 RID: 1280 RVA: 0x00018FC1 File Offset: 0x000171C1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OmegaBlueEnchantment>();
				}
			}
		}

		// Token: 0x0200015E RID: 350
		public class OldDukeScalesEffect : AccessoryEffect
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000504 RID: 1284 RVA: 0x00018FC1 File Offset: 0x000171C1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OmegaBlueEnchantment>();
				}
			}
		}
	}
}
