using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005C RID: 92
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class LifeBloomEnchantment : ModItem
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000BC34 File Offset: 0x00009E34
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000BC98 File Offset: 0x00009E98
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setLifeBloom = true;
			}
			ModItem hOTJ;
			if (AccessoryEffectLoader.AddEffect<LifeBloomEnchantment.HeartOfTheJungleEffect>(player, base.Item) && thorium.TryFind<ModItem>("HeartOfTheJungle", ref hOTJ))
			{
				hOTJ.UpdateAccessory(player, hideVisual);
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem beeBooties;
			if (AccessoryEffectLoader.AddEffect<LifeBloomEnchantment.BeeBootiesEffect>(player, base.Item) && thorium.TryFind<ModItem>("BeeBooties", ref beeBooties))
			{
				beeBooties.UpdateAccessory(player, hideVisual);
			}
			ModItem gS;
			if (thorium.TryFind<ModItem>("GardenersSheath", ref gS))
			{
				gS.UpdateAccessory(player, hideVisual);
			}
			ModItem sV;
			if (thorium.TryFind<ModItem>("SweetVengeance", ref sV))
			{
				sV.UpdateAccessory(player, hideVisual);
			}
			ModItem livingWoodEnchantment;
			if (mod.TryFind<ModItem>("LivingWoodEnchantment", ref livingWoodEnchantment))
			{
				livingWoodEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem bulbEnchantment;
			if (mod.TryFind<ModItem>("BulbEnchantment", ref bulbEnchantment))
			{
				bulbEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000BD88 File Offset: 0x00009F88
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LifeBloomMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBloomMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBloomLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LivingWoodEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BulbEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HeartOfTheJungle>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BeeBooties>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GardenersSheath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SweetVengeance>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000038 RID: 56
		public static readonly int SetHealBonus = 5;

		// Token: 0x02000120 RID: 288
		public class BeeBootiesEffect : AccessoryEffect
		{
			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00018E31 File Offset: 0x00017031
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LifeBloomEnchantment>();
				}
			}
		}

		// Token: 0x02000121 RID: 289
		public class HeartOfTheJungleEffect : AccessoryEffect
		{
			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x00018E31 File Offset: 0x00017031
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LifeBloomEnchantment>();
				}
			}
		}
	}
}
