using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.Granite;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.ThrownItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000056 RID: 86
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class GraniteEnchantment : ModItem
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000B3DC File Offset: 0x000095DC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B440 File Offset: 0x00009640
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<GraniteEnchantment.GraniteArmorEffects>(player, base.Item))
			{
				player.fireWalk = true;
				player.lavaImmune = true;
				player.buffImmune[24] = true;
				player.buffImmune[ModContent.BuffType<Singed>()] = true;
				player.noKnockback = true;
				player.moveSpeed -= 0.2f;
				player.accRunSpeed = player.maxRunSpeed;
			}
			ModItem eyeOfTheStorm;
			if (AccessoryEffectLoader.AddEffect<GraniteEnchantment.EyeOfTheStormEffects>(player, base.Item) && thorium.TryFind<ModItem>("ShockAbsorber", ref eyeOfTheStorm))
			{
				eyeOfTheStorm.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<GraniteHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GraniteChestGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GraniteGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EnergyProjector>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BoulderProbeStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShockAbsorber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ObsidianStriker>(), 300);
			recipe.AddIngredient(ModContent.ItemType<EarthenEnergyPaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200011C RID: 284
		public class GraniteArmorEffects : AccessoryEffect
		{
			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x00018E1C File Offset: 0x0001701C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GraniteEnchantment>();
				}
			}
		}

		// Token: 0x0200011D RID: 285
		public class EyeOfTheStormEffects : AccessoryEffect
		{
			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x00018E1C File Offset: 0x0001701C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<GraniteEnchantment>();
				}
			}
		}
	}
}
