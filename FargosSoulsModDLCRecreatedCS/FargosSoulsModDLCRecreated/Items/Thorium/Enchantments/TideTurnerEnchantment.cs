using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Misc;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000030 RID: 48
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class TideTurnerEnchantment : ModItem
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000065A8 File Offset: 0x000047A8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000660C File Offset: 0x0000480C
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00006694 File Offset: 0x00004894
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (AccessoryEffectLoader.AddEffect<TideTurnerEnchantment.TideTurnerEffects>(player, base.Item))
				{
					PlayerHelper.GetThoriumPlayer(player).setTideTurner = true;
				}
				if (AccessoryEffectLoader.AddEffect<TideTurnerEnchantment.TideGlobules>(player, base.Item))
				{
					PlayerHelper.GetThoriumPlayer(player).setTideCrown = true;
				}
			}
			ModItem pLF;
			if (AccessoryEffectLoader.AddEffect<TideTurnerEnchantment.PlagueLordFlaskEffects>(player, base.Item) && thorium.TryFind<ModItem>("PlagueLordFlask", ref pLF))
			{
				pLF.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006720 File Offset: 0x00004920
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TideTurnerHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideTurnersGaze>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideTurnerBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideTurnerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueLordFlask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PoseidonCharge>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MantisShrimpPunch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OceansJudgement>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DeitysTrefork>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TidalWave>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FishEgg>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x020000CA RID: 202
		public class TideGlobules : AccessoryEffect
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000340 RID: 832 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000341 RID: 833 RVA: 0x00018B82 File Offset: 0x00016D82
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TideTurnerEnchantment>();
				}
			}
		}

		// Token: 0x020000CB RID: 203
		public class TideTurnerEffects : AccessoryEffect
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000343 RID: 835 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000344 RID: 836 RVA: 0x00018B82 File Offset: 0x00016D82
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TideTurnerEnchantment>();
				}
			}
		}

		// Token: 0x020000CC RID: 204
		public class PlagueLordFlaskEffects : AccessoryEffect
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000346 RID: 838 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000347 RID: 839 RVA: 0x00018B82 File Offset: 0x00016D82
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TideTurnerEnchantment>();
				}
			}
		}
	}
}
