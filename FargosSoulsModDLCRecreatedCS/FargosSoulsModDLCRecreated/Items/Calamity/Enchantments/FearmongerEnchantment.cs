using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Fearmonger;
using CalamityMod.Items.Weapons.Magic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000092 RID: 146
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class FearmongerEnchantment : ModItem
	{
		// Token: 0x06000264 RID: 612 RVA: 0x000112CC File Offset: 0x0000F4CC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 750000;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00011330 File Offset: 0x0000F530
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "20% increased summon damage and +2 max minions"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Minions no longer deal less damage while wielding non-summoner weapons"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Immunity to all forms of frost and flame"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Minion attacks grant +3.5 HP/s life regen and massively accelerate life regen"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "15% increased damage reduction during the Pumpkin and Frost Moons"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "This extra damage reduction ignores the soft cap"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil and Statis' Void Sash"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Spectral Veil and The Evolution"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(70, 63, 69));
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00011498 File Offset: 0x0000F698
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				player.Calamity().wearingRogueArmor = true;
				player.Calamity().WearingPostMLSummonerSet = true;
				*player.GetDamage<SummonDamageClass>() += 0.2f;
				player.maxMinions += 2;
				int[] immuneDebuffs = new int[]
				{
					24,
					44,
					39,
					153,
					189,
					67,
					0,
					0,
					0,
					0,
					46,
					47,
					0
				};
				for (int i = 0; i < immuneDebuffs.Length; i++)
				{
					player.buffImmune[immuneDebuffs[i]] = true;
				}
			}
			ModItem statisVoidSash;
			if (calamity.TryFind<ModItem>("StatisVoidSash", ref statisVoidSash))
			{
				statisVoidSash.UpdateAccessory(player, hideVisual);
			}
			ModItem theEvolution;
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem spectralVeil;
				if (AccessoryEffectLoader.AddEffect<FearmongerEnchantment.SpectralVeilEffects>(player, base.Item) && calamity.TryFind<ModItem>("SpectralVeil", ref spectralVeil))
				{
					spectralVeil.UpdateAccessory(player, hideVisual);
					return;
				}
			}
			else if (calamity.TryFind<ModItem>("TheEvolution", ref theEvolution))
			{
				theEvolution.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FearmongerGreathelm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FearmongerPlateMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FearmongerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpectralVeil>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<StatisVoidSash>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<TheEvolution>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<FaceMelter>(), 1);
			recipe.AddTile(this.calamity, "DraedonsForge");
			recipe.Register();
		}

		// Token: 0x0400005A RID: 90
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x0200013D RID: 317
		public class SpectralVeilEffects : AccessoryEffect
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000499 RID: 1177 RVA: 0x00018ED2 File Offset: 0x000170D2
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FearmongerEnchantment>();
				}
			}
		}
	}
}
