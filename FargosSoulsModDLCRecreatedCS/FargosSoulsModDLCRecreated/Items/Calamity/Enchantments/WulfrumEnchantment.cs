using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Wulfrum;
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
	// Token: 0x020000A5 RID: 165
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class WulfrumEnchantment : ModItem
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00016F1C File Offset: 0x0001511C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
			base.Item.defense = 3;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00016F8C File Offset: 0x0001518C
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(129, 168, 109));
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00017014 File Offset: 0x00015214
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				player.GetModPlayer<WulfrumArmorPlayer>().wulfrumSet = true;
				if (player.statLife <= (int)((double)player.statLifeMax2 * 0.5))
				{
					player.statDefense += 5;
				}
			}
			ModItem trinketOfChi;
			if (calamity.TryFind<ModItem>("TrinketofChi", ref trinketOfChi))
			{
				trinketOfChi.UpdateAccessory(player, hideVisual);
			}
			ModItem rD;
			if (calamity.TryFind<ModItem>("RottenDogtooth", ref rD))
			{
				rD.UpdateAccessory(player, hideVisual);
			}
			ModItem wulfrumAcrobaticsPack;
			if (AccessoryEffectLoader.AddEffect<WulfrumEnchantment.WulfrumAcrobaticsEffect>(player, base.Item) && calamity.TryFind<ModItem>("WulfrumAcrobaticsPack", ref wulfrumAcrobaticsPack))
			{
				wulfrumAcrobaticsPack.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000170D8 File Offset: 0x000152D8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WulfrumHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WulfrumJacket>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WulfrumOveralls>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TrinketofChi>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RottenDogtooth>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WulfrumAcrobaticsPack>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SparkSpreader>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Pumpler>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000063 RID: 99
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x02000190 RID: 400
		public class WulfrumAcrobaticsEffect : AccessoryEffect
		{
			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00019068 File Offset: 0x00017268
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DevastationForceHeader>();
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000190FB File Offset: 0x000172FB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WulfrumEnchantment>();
				}
			}
		}
	}
}
