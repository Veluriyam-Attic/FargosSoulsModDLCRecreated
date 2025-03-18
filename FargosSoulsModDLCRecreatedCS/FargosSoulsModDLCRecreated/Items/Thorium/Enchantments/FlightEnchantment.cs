using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200006B RID: 107
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class FlightEnchantment : ModItem
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setFlight = true;
				player.jumpBoost = true;
				player.noFallDmg = true;
			}
			ModItem fabergeEgg;
			if (AccessoryEffectLoader.AddEffect<FlightEnchantment.FabergeEggEffect>(player, base.Item) && thorium.TryFind<ModItem>("FabergeEgg", ref fabergeEgg))
			{
				fabergeEgg.UpdateAccessory(player, hideVisual);
			}
			ModItem aW;
			if (AccessoryEffectLoader.AddEffect<FlightEnchantment.AirWalkersEffect>(player, base.Item) && thorium.TryFind<ModItem>("AirWalkers", ref aW))
			{
				aW.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FlightMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FlightMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FlightBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChampionWing>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AirWalkers>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FabergeEgg>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Bolas>(), 300);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200013B RID: 315
		public class FabergeEggEffect : AccessoryEffect
		{
			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000492 RID: 1170 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x00018EBF File Offset: 0x000170BF
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FlightEnchantment>();
				}
			}
		}

		// Token: 0x0200013C RID: 316
		public class AirWalkersEffect : AccessoryEffect
		{
			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000496 RID: 1174 RVA: 0x00018EBF File Offset: 0x000170BF
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FlightEnchantment>();
				}
			}
		}
	}
}
