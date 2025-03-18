using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004A RID: 74
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class TideHunterEnchantment : ModItem
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00009D44 File Offset: 0x00007F44
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<TideHunterEnchantment.TideHunterEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).tideHunterSet = true;
			}
			ModItem anglerBowl;
			if (AccessoryEffectLoader.AddEffect<TideHunterEnchantment.AnglerBowlEffects>(player, base.Item) && thorium.TryFind<ModItem>("AnglerBowl", ref anglerBowl))
			{
				anglerBowl.UpdateAccessory(player, hideVisual);
			}
			ModItem yewWoodEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("YewWoodEnchantment", ref yewWoodEnchantment))
			{
				yewWoodEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009E38 File Offset: 0x00008038
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TideHunterCap>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideHunterChestpiece>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideHunterLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AnglerBowl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BuccaneerBlunderBuss>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PearlPike>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HydroPump>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MarineLauncher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JollyRogerPaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000031 RID: 49
		public static readonly int SetHealBonus = 5;

		// Token: 0x02000104 RID: 260
		public class TideHunterEffects : AccessoryEffect
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x00018D70 File Offset: 0x00016F70
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TideHunterEnchantment>();
				}
			}
		}

		// Token: 0x02000105 RID: 261
		public class AnglerBowlEffects : AccessoryEffect
		{
			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00018D70 File Offset: 0x00016F70
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TideHunterEnchantment>();
				}
			}
		}
	}
}
