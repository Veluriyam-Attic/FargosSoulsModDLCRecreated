using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003B RID: 59
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class IridescentEnchantment : ModItem
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000081F4 File Offset: 0x000063F4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008258 File Offset: 0x00006458
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<IridescentEnchantment.IridescentEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).iridescentSet = true;
			}
			ModItem equalizer;
			if (AccessoryEffectLoader.AddEffect<IridescentEnchantment.EqualizerEffects>(player, base.Item) && thorium.TryFind<ModItem>("Equalizer", ref equalizer))
			{
				equalizer.UpdateAccessory(player, hideVisual);
			}
			ModItem lifeQuartzShield;
			if (AccessoryEffectLoader.AddEffect<IridescentEnchantment.LifeQuartzShieldEffects>(player, base.Item) && thorium.TryFind<ModItem>("LifeQuartzShield", ref lifeQuartzShield))
			{
				lifeQuartzShield.UpdateAccessory(player, hideVisual);
			}
			ModItem sG;
			if (thorium.TryFind<ModItem>("SpiritsGrace", ref sG))
			{
				sG.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008304 File Offset: 0x00006504
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<IridescentHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IridescentMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IridescentGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritsGrace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Equalizer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeQuartzShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HereticBreaker>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000E4 RID: 228
		public class IridescentEffects : AccessoryEffect
		{
			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000389 RID: 905 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600038A RID: 906 RVA: 0x00018C36 File Offset: 0x00016E36
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IridescentEnchantment>();
				}
			}
		}

		// Token: 0x020000E5 RID: 229
		public class EqualizerEffects : AccessoryEffect
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600038C RID: 908 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600038D RID: 909 RVA: 0x00018C36 File Offset: 0x00016E36
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IridescentEnchantment>();
				}
			}
		}

		// Token: 0x020000E6 RID: 230
		public class LifeQuartzShieldEffects : AccessoryEffect
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600038F RID: 911 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000390 RID: 912 RVA: 0x00018C36 File Offset: 0x00016E36
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IridescentEnchantment>();
				}
			}
		}
	}
}
