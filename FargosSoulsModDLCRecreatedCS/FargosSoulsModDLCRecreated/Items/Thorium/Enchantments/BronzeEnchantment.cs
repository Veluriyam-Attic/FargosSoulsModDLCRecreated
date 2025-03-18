using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.Bronze;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000049 RID: 73
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BronzeEnchantment : ModItem
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00009AEC File Offset: 0x00007CEC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009B50 File Offset: 0x00007D50
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

		// Token: 0x06000128 RID: 296 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<BronzeEnchantment.BronzeEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setBronze = true;
			}
			ModItem championsRebuttal;
			if (AccessoryEffectLoader.AddEffect<BronzeEnchantment.ChampionsRebuttalEffects>(player, base.Item) && thorium.TryFind<ModItem>("ChampionsRebuttal", ref championsRebuttal))
			{
				championsRebuttal.UpdateAccessory(player, hideVisual);
			}
			ModItem olympicTorch;
			if (AccessoryEffectLoader.AddEffect<BronzeEnchantment.OlympicTorchEffects>(player, base.Item) && thorium.TryFind<ModItem>("OlympicTorch", ref olympicTorch))
			{
				olympicTorch.UpdateAccessory(player, hideVisual);
			}
			ModItem spartanSandles;
			if (AccessoryEffectLoader.AddEffect<BronzeEnchantment.SpartanSandalEffects>(player, base.Item) && thorium.TryFind<ModItem>("SpartanSandles", ref spartanSandles))
			{
				spartanSandles.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00009C94 File Offset: 0x00007E94
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BronzeHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BronzeBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BronzeGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OlympicTorch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChampionsRebuttal>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpartanSandles>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChampionSwiftBlade>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpikyCaltrop>(), 300);
			recipe.AddIngredient(ModContent.ItemType<BronzeThrowingAxe>(), 300);
			recipe.AddIngredient(ModContent.ItemType<AncientDrachma>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x02000100 RID: 256
		public class BronzeEffects : AccessoryEffect
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x00018D69 File Offset: 0x00016F69
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BronzeEnchantment>();
				}
			}
		}

		// Token: 0x02000101 RID: 257
		public class ChampionsRebuttalEffects : AccessoryEffect
		{
			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x00018D69 File Offset: 0x00016F69
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BronzeEnchantment>();
				}
			}
		}

		// Token: 0x02000102 RID: 258
		public class SpartanSandalEffects : AccessoryEffect
		{
			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00018D69 File Offset: 0x00016F69
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BronzeEnchantment>();
				}
			}
		}

		// Token: 0x02000103 RID: 259
		public class OlympicTorchEffects : AccessoryEffect
		{
			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x00018D69 File Offset: 0x00016F69
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BronzeEnchantment>();
				}
			}
		}
	}
}
