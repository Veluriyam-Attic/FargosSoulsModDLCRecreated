using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Painting;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003C RID: 60
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class OceanEnchantment : ModItem
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00008384 File Offset: 0x00006584
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000083E8 File Offset: 0x000065E8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<OceanEnchantment.CoralEffects>(player, base.Item))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				thoriumPlayer.setCoral = true;
				thoriumPlayer.shieldHealthTimerStop = true;
			}
			ModItem sBP;
			if (mod.TryFind<ModItem>("SeaBreezePendant", ref sBP))
			{
				sBP.UpdateAccessory(player, hideVisual);
			}
			ModItem bM;
			if (mod.TryFind<ModItem>("BubbleMagnet", ref bM))
			{
				bM.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008468 File Offset: 0x00006668
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CoralHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CoralChestGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CoralGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SeaBreezePendant>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BubbleMagnet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CoralSlasher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CoralPolearm>(), 1);
			recipe.AddIngredient(2332, 1);
			recipe.AddIngredient(ModContent.ItemType<JellyintheWaterPaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000E7 RID: 231
		public class CoralEffects : AccessoryEffect
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000392 RID: 914 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000393 RID: 915 RVA: 0x00018C3D File Offset: 0x00016E3D
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OceanEnchantment>();
				}
			}
		}
	}
}
