using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.FathomSwarmer;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A4 RID: 164
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class FathomSwarmerEnchantment : ModItem
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00016B68 File Offset: 0x00014D68
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 300000;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00016BCC File Offset: 0x00014DCC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "10% increased summon damage and +2 max minions"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Grants the ability to climb walls"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "20% increased summon damage, +10 defense and +2.5 HP/s life regen while underwater"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Provides a moderate amount of light and moderately reduces breath loss in the abyss"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Corrosive Spine, Lumenous Amulet, and Amadias Pendant"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Sand Cloak, Rusty Medallion, Alluring Bait, Aquatic Heart, and Leviathan Ambergris"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Sand Cloak, Rusty Medallion, Aquatic Heart, and Leviathan Ambergris"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(70, 63, 69));
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00016D18 File Offset: 0x00014F18
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
				modPlayer.fathomSwarmer = true;
				player.spikedBoots = 2;
				player.maxMinions += 2;
				*player.GetDamage<SummonDamageClass>() += 0.1f;
				if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir, false))
				{
					*player.GetDamage<SummonDamageClass>() += 0.2f;
					player.statDefense += 10;
					player.lifeRegen += 5;
				}
			}
			ModItem corrosiveSpine;
			if (AccessoryEffectLoader.AddEffect<FathomSwarmerEnchantment.CorrosiveSpineEffects>(player, base.Item) && calamity.TryFind<ModItem>("CorrosiveSpine", ref corrosiveSpine))
			{
				corrosiveSpine.UpdateAccessory(player, hideVisual);
			}
			ModItem lumenousAmulet;
			if (calamity.TryFind<ModItem>("LumenousAmulet", ref lumenousAmulet))
			{
				lumenousAmulet.UpdateAccessory(player, hideVisual);
			}
			ModItem leviathanAmbergris;
			if (AccessoryEffectLoader.AddEffect<FathomSwarmerEnchantment.LeviathanAmbergrisEffects>(player, base.Item) && calamity.TryFind<ModItem>("LeviathanAmbergris", ref leviathanAmbergris))
			{
				leviathanAmbergris.UpdateAccessory(player, hideVisual);
			}
			ModItem aquaHeart;
			if (AccessoryEffectLoader.AddEffect<FathomSwarmerEnchantment.AquaticHeartEffects>(player, base.Item) && calamity.TryFind<ModItem>("AquaticHeart", ref aquaHeart))
			{
				aquaHeart.UpdateAccessory(player, hideVisual);
			}
			ModItem sulphurousEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("SulphurousEnchantment", ref sulphurousEnchantment))
			{
				sulphurousEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00016E90 File Offset: 0x00015090
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FathomSwarmerVisage>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SulphurousEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CorrosiveSpine>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LumenousAmulet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LeviathanAmbergris>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AquaticHeart>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0200018D RID: 397
		public class CorrosiveSpineEffects : AccessoryEffect
		{
			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x000190F4 File Offset: 0x000172F4
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FathomSwarmerEnchantment>();
				}
			}
		}

		// Token: 0x0200018E RID: 398
		public class LeviathanAmbergrisEffects : AccessoryEffect
		{
			// Token: 0x1700019F RID: 415
			// (get) Token: 0x0600059A RID: 1434 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x000190F4 File Offset: 0x000172F4
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FathomSwarmerEnchantment>();
				}
			}
		}

		// Token: 0x0200018F RID: 399
		public class AquaticHeartEffects : AccessoryEffect
		{
			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x0600059D RID: 1437 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x000190F4 File Offset: 0x000172F4
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FathomSwarmerEnchantment>();
				}
			}
		}
	}
}
