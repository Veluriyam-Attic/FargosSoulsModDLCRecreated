using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Titan;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004E RID: 78
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class TitanEnchantment : ModItem
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000A4B0 File Offset: 0x000086B0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 6;
			base.Item.value = 200000;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A514 File Offset: 0x00008714
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				*player.GetDamage(DamageClass.Generic) += 0.18f;
			}
			ModItem maskOfTheCrystalEye;
			if (AccessoryEffectLoader.AddEffect<TitanEnchantment.MaskOfTheCrystalEyeEffects>(player, base.Item) && thorium.TryFind<ModItem>("MaskoftheCrystalEye", ref maskOfTheCrystalEye))
			{
				maskOfTheCrystalEye.UpdateAccessory(player, hideVisual);
			}
			ModItem abyssalShell;
			if (AccessoryEffectLoader.AddEffect<TitanEnchantment.AbyssalShellEffects>(player, base.Item) && thorium.TryFind<ModItem>("AbyssalShell", ref abyssalShell))
			{
				abyssalShell.UpdateAccessory(player, hideVisual);
			}
			ModItem rockMusicPlayer;
			if (thorium.TryFind<ModItem>("TunePlayerDamageReduction", ref rockMusicPlayer))
			{
				rockMusicPlayer.UpdateAccessory(player, hideVisual);
			}
			ModItem spiritBand;
			if (AccessoryEffectLoader.AddEffect<TitanEnchantment.SpiritBandEffects>(player, base.Item) && thorium.TryFind<ModItem>("SpiritBand", ref spiritBand))
			{
				spiritBand.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A5EC File Offset: 0x000087EC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Titan Helmet";
			}, new int[]
			{
				ModContent.ItemType<TitanHelmet>(),
				ModContent.ItemType<TitanHeadgear>(),
				ModContent.ItemType<TitanMask>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyTitanHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyTitanHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<TitanBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitanGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MaskoftheCrystalEye>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AbyssalShell>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritBand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TunePlayerDamageReduction>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitanBoomerang>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TranquilizerGun>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TetherDart>(), 300);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0200010C RID: 268
		public class MaskOfTheCrystalEyeEffects : AccessoryEffect
		{
			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x00018DB8 File Offset: 0x00016FB8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TitanEnchantment>();
				}
			}
		}

		// Token: 0x0200010D RID: 269
		public class AbyssalShellEffects : AccessoryEffect
		{
			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x00018DB8 File Offset: 0x00016FB8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TitanEnchantment>();
				}
			}
		}

		// Token: 0x0200010E RID: 270
		public class SpiritBandEffects : AccessoryEffect
		{
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600040B RID: 1035 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x00018DB8 File Offset: 0x00016FB8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TitanEnchantment>();
				}
			}
		}
	}
}
