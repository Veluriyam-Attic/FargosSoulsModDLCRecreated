using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Weapons.Melee;
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
	// Token: 0x020000A6 RID: 166
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class GodSlayerEnchantment : ModItem
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0001717C File Offset: 0x0001537C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip1", "The power to slay gods resides within you..."));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2", "Allows you to dash for an immense distance in 8 directions"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3", "Press H while holding down the movement keys in the direction you want to dash"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip4", "Enemies you dash through take massive damage"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "During the dash you are immune to most debuffs"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip6", "The dash has a 45 second cooldown"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip7", "Taking over 80 damage in one hit will cause you to release a swarm of high-damage god killer darts"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip8", "Enemies take a lot of damage when they hit you"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip9", "You fire a god killer shrapnel round while firing ranged weapons every 2.5 seconds"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip10", "While at full HP all of your rogue stats are boosted by 10%"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip11", "If you take over 80 damage in one hit you will be given extra immunity frames"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip121", "A calamity bell floats above your head and occasionally rings while attacking"));
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip122", "When the bell rings, enemies around you take 7500 true damage"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of the Nebulous Core, Dimensional Soul Artifact, and Draedon's Heart"));
			}
			else
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Dimensional Soul Artifact and Venetrated Locket"));
			}
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(100, 108, 156));
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000173B0 File Offset: 0x000155B0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 10000000;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00017414 File Offset: 0x00015614
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod") && AccessoryEffectLoader.AddEffect<GodSlayerEnchantment.GodSlayerEffects>(player, base.Item))
			{
				modPlayer.godSlayer = true;
				modPlayer.godSlayerDamage = true;
				modPlayer.godSlayerRanged = true;
				modPlayer.godSlayerThrowing = true;
				if (ModLoader.HasMod("CalamityBardHealer"))
				{
					int alluringSongBuffType = ModLoader.GetMod("CalamityBardHealer").Find<ModBuff>("CalamityBell").Type;
					if (Main.myPlayer == player.whoAmI && !player.HasBuff(alluringSongBuffType))
					{
						player.AddBuff(alluringSongBuffType, 2, true, false);
					}
				}
				if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
				{
					modPlayer.DeferredDashID = GodslayerArmorDash.ID;
					player.dash = 0;
				}
			}
			ModItem nebulousCore;
			if (!ModLoader.HasMod("FargowiltasCrossmod") && AccessoryEffectLoader.AddEffect<GodSlayerEnchantment.NebulousCoreEffects>(player, base.Item) && calamity.TryFind<ModItem>("NebulousCore", ref nebulousCore))
			{
				nebulousCore.UpdateAccessory(player, hideVisual);
			}
			ModItem draedonsHeart;
			if (!ModLoader.HasMod("FargowiltasCrossmod") && calamity.TryFind<ModItem>("DraedonsHeart", ref draedonsHeart))
			{
				draedonsHeart.UpdateAccessory(player, hideVisual);
			}
			ModItem dSA;
			if (calamity.TryFind<ModItem>("DimensionalSoulArtifact", ref dSA))
			{
				dSA.UpdateAccessory(player, hideVisual);
			}
			ModItem vL;
			if (ModLoader.HasMod("FargowiltasCrossmod") && AccessoryEffectLoader.AddEffect<GodSlayerEnchantment.VenetratedLocketEffects>(player, base.Item) && calamity.TryFind<ModItem>("VeneratedLocket", ref vL))
			{
				vL.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00017598 File Offset: 0x00015798
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Godslayer Helmet";
				}, new int[]
				{
					ModContent.ItemType<GodSlayerHeadMelee>(),
					ModContent.ItemType<GodSlayerHeadRogue>(),
					ModContent.ItemType<GodSlayerHeadRanged>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Godslayer Helmet";
				}, new int[]
				{
					ModContent.ItemType<GodSlayerHeadMelee>(),
					ModContent.ItemType<GodSlayerHeadRogue>(),
					ModContent.ItemType<GodSlayerHeadRanged>(),
					calamityBardHealer.Find<ModItem>("GodSlayerDeathsingerCowl").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyGodslayerHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyGodslayerHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<GodSlayerChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GodSlayerLeggings>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<NebulousCore>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<Excelsus>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<DimensionalSoulArtifact>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<DraedonsHeart>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<Exoblade>(), 1);
			}
			if (ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<VeneratedLocket>(), 1);
			}
			recipe.AddTile(this.calamity, "DraedonsForge");
			recipe.Register();
		}

		// Token: 0x04000064 RID: 100
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x02000191 RID: 401
		public class GodSlayerEffects : AccessoryEffect
		{
			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00019102 File Offset: 0x00017302
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GodSlayerEnchantment>();
				}
			}
		}

		// Token: 0x02000192 RID: 402
		public class NebulousCoreEffects : AccessoryEffect
		{
			// Token: 0x060005A6 RID: 1446 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00019102 File Offset: 0x00017302
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GodSlayerEnchantment>();
				}
			}
		}

		// Token: 0x02000193 RID: 403
		public class VenetratedLocketEffects : AccessoryEffect
		{
			// Token: 0x060005AA RID: 1450 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x00019102 File Offset: 0x00017302
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GodSlayerEnchantment>();
				}
			}
		}
	}
}
