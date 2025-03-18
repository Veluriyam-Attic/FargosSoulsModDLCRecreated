using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.NPCItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002F RID: 47
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BerserkerEnchantment : ModItem
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000062E8 File Offset: 0x000044E8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000634C File Offset: 0x0000454C
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

		// Token: 0x060000A4 RID: 164 RVA: 0x000063D4 File Offset: 0x000045D4
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				DLCPlayer thoriumPlayer = player.GetModPlayer<DLCPlayer>();
				int num = 4 - (int)(((float)player.statLife + 1f) / (float)player.statLifeMax2 * 4f);
				if (num < 1)
				{
					num = 1;
				}
				*player.GetDamage<GenericDamageClass>() += 0.15f * (float)num;
				thoriumPlayer.berserkStage = num;
				if (AccessoryEffectLoader.AddEffect<BerserkerEnchantment.BerserkerEffect>(player, base.Item))
				{
					thoriumPlayer.orbital = true;
					thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.07500000298023224, default(Vector2));
				}
				else
				{
					thoriumPlayer.berserkStage = 0;
				}
			}
			ModItem bG;
			if (AccessoryEffectLoader.AddEffect<BerserkerEnchantment.BeholderGazeEffect>(player, base.Item) && thorium.TryFind<ModItem>("BeholderGaze", ref bG))
			{
				bG.UpdateAccessory(player, hideVisual);
			}
			ModItem mP;
			if (thorium.TryFind<ModItem>("MetabolicPills", ref mP))
			{
				mP.UpdateAccessory(player, hideVisual);
			}
			ModItem magmaEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("MagmaEnchantment", ref magmaEnchantment))
			{
				magmaEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006500 File Offset: 0x00004700
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BeholderGaze>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MetabolicPills>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ExileHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MagmaEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DoomFireAxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HellishHalberd>(), 1);
			recipe.AddIngredient(426, 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000C8 RID: 200
		public class BerserkerEffect : AccessoryEffect
		{
			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600033A RID: 826 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600033B RID: 827 RVA: 0x00018B7B File Offset: 0x00016D7B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BerserkerEnchantment>();
				}
			}
		}

		// Token: 0x020000C9 RID: 201
		public class BeholderGazeEffect : AccessoryEffect
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600033D RID: 829 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x0600033E RID: 830 RVA: 0x00018B7B File Offset: 0x00016D7B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BerserkerEnchantment>();
				}
			}
		}
	}
}
