using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000063 RID: 99
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class YewWoodEnchantment : ModItem
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C928 File Offset: 0x0000AB28
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<YewWoodEnchantment.YewWoodEffect>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).yewCharging = true;
			}
			ModItem goblinWarshield;
			if (AccessoryEffectLoader.AddEffect<YewWoodEnchantment.GoblinWarShieldEffect>(player, base.Item) && thorium.TryFind<ModItem>("GoblinWarshield", ref goblinWarshield))
			{
				goblinWarshield.UpdateAccessory(player, hideVisual);
			}
			ModItem tR;
			if (thorium.TryFind<ModItem>("ThumbRing", ref tR))
			{
				tR.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodBreastguard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GoblinWarshield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThumbRing>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FeatherFoe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodLute>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YewWoodFlintlock>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadowWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpikeBomb>(), 300);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200012B RID: 299
		public class YewWoodEffect : AccessoryEffect
		{
			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000462 RID: 1122 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00018E62 File Offset: 0x00017062
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<YewWoodEnchantment>();
				}
			}
		}

		// Token: 0x0200012C RID: 300
		public class GoblinWarShieldEffect : AccessoryEffect
		{
			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000465 RID: 1125 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000466 RID: 1126 RVA: 0x00018E62 File Offset: 0x00017062
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<YewWoodEnchantment>();
				}
			}
		}
	}
}
