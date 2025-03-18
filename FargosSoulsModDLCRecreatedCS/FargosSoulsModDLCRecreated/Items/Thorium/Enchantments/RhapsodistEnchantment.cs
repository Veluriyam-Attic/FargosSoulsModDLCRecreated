using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000054 RID: 84
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class RhapsodistEnchantment : ModItem
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000B098 File Offset: 0x00009298
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B0FC File Offset: 0x000092FC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(255, 128, 0));
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B184 File Offset: 0x00009384
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<RhapsodistEnchantment.RhapsodistEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setSoloistsHat = true;
				PlayerHelper.GetThoriumPlayer(player).setInspiratorsHelmet = true;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B1DC File Offset: 0x000093DC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SoloistHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InspiratorsHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RhapsodistChestWoofer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RhapsodistBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SirensLyre>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JingleBells>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Sousaphone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Holophonor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EdgeofImagination>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0200011A RID: 282
		public class RhapsodistEffects : AccessoryEffect
		{
			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000430 RID: 1072 RVA: 0x00018E0E File Offset: 0x0001700E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<RhapsodistEnchantment>();
				}
			}
		}
	}
}
