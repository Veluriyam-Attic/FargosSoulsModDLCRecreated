using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000068 RID: 104
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SpiritTrapperEnchantment : ModItem
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000D394 File Offset: 0x0000B594
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				if (AccessoryEffectLoader.AddEffect<SpiritTrapperEnchantment.SpiritTrapperEffects>(player, base.Item))
				{
					PlayerHelper.GetThoriumPlayer(player).setSpiritTrapper = true;
				}
				player.maxMinions += SpiritTrapperEnchantment.SetMaxMinions;
				player.maxTurrets += SpiritTrapperEnchantment.SetMaxSentries;
			}
			ModItem innerFlame;
			if (AccessoryEffectLoader.AddEffect<SpiritTrapperEnchantment.InnerFlameEffects>(player, base.Item) && thorium.TryFind<ModItem>("InnerFlame", ref innerFlame))
			{
				innerFlame.UpdateAccessory(player, hideVisual);
			}
			ModItem sGlass;
			if (AccessoryEffectLoader.AddEffect<SpiritTrapperEnchantment.ScryingGlassEffects>(player, base.Item) && thorium.TryFind<ModItem>("ScryingGlass", ref sGlass))
			{
				sGlass.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Spirit Trapper Helmet";
			}, new int[]
			{
				ModContent.ItemType<SpiritTrapperCowl>(),
				ModContent.ItemType<SpiritTrapperMask>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnySpiritTrapperHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnySpiritTrapperHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritTrapperCuirass>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritTrapperGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InnerFlame>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ScryingGlass>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TabooWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritBlastWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TotemCaller>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LoudFootstepsPaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400003E RID: 62
		public static readonly int MaxSentries = 1;

		// Token: 0x0400003F RID: 63
		public static readonly int SetMaxSentries = 1;

		// Token: 0x04000040 RID: 64
		public static readonly int MaxMinions = 1;

		// Token: 0x04000041 RID: 65
		public static readonly int SetMaxMinions = 1;

		// Token: 0x02000134 RID: 308
		public class SpiritTrapperEffects : AccessoryEffect
		{
			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x00018E7E File Offset: 0x0001707E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SpiritTrapperEnchantment>();
				}
			}
		}

		// Token: 0x02000135 RID: 309
		public class InnerFlameEffects : AccessoryEffect
		{
			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x00018E7E File Offset: 0x0001707E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SpiritTrapperEnchantment>();
				}
			}
		}

		// Token: 0x02000136 RID: 310
		public class ScryingGlassEffects : AccessoryEffect
		{
			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000484 RID: 1156 RVA: 0x00018E7E File Offset: 0x0001707E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SpiritTrapperEnchantment>();
				}
			}
		}
	}
}
