using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Darksteel;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000045 RID: 69
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DarksteelEnchantment : ModItem
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000946C File Offset: 0x0000766C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000094D0 File Offset: 0x000076D0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<DarksteelEnchantment.DarksteelEffects>(player, base.Item))
			{
				player.moveSpeed += 0.1f;
				player.noKnockback = true;
				player.iceSkate = true;
				player.dashType = 1;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem ballNChain;
			if (thorium.TryFind<ModItem>("BallnChain", ref ballNChain))
			{
				ballNChain.UpdateAccessory(player, hideVisual);
			}
			PlayerHelper.GetThoriumPlayer(player).ballnChain = true;
			ModItem steelEnchantment;
			if (mod.TryFind<ModItem>("SteelEnchantment", ref steelEnchantment))
			{
				steelEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000957C File Offset: 0x0000777C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DarksteelFaceGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarksteelBreastPlate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarksteelGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteelEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BallnChain>(), 1);
			recipe.AddIngredient(ModContent.ItemType<gDarkSteelCrossBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ElephantGun>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StrongestLink>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000030 RID: 48
		public static readonly int SetHealBonus = 5;

		// Token: 0x020000F8 RID: 248
		public class DarksteelEffects : AccessoryEffect
		{
			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060003C9 RID: 969 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003CA RID: 970 RVA: 0x00018D4D File Offset: 0x00016F4D
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DarksteelEnchantment>();
				}
			}
		}
	}
}
