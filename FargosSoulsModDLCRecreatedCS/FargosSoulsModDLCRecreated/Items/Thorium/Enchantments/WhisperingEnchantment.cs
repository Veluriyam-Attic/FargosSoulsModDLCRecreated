using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000042 RID: 66
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class WhisperingEnchantment : ModItem
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00008D4C File Offset: 0x00006F4C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 250000;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<WhisperingEnchantment.WhisperingEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).whisperingSet = true;
			}
			ModItem gWG;
			if (AccessoryEffectLoader.AddEffect<WhisperingEnchantment.GutWrenchersSheathEffects>(player, base.Item) && thorium.TryFind<ModItem>("GutWrenchersGauntlet", ref gWG))
			{
				gWG.UpdateAccessory(player, hideVisual);
			}
			ModItem gC;
			if (AccessoryEffectLoader.AddEffect<WhisperingEnchantment.GhastlyCarapanceEffects>(player, base.Item) && thorium.TryFind<ModItem>("GhastlyCarapance", ref gC))
			{
				gC.UpdateAccessory(player, hideVisual);
			}
			ModItem wS;
			if (thorium.TryFind<ModItem>("WrithingSheath", ref wS))
			{
				wS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008E5C File Offset: 0x0000705C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WhisperingHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhisperingTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhisperingLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GhastlyCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GutWrenchersGauntlet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WrithingSheath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RottenCod>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheStalker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SamsaraLotus>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WildUmbra>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MindMelter>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhisperingDagger>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CuriousSeaLifePaint>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000EE RID: 238
		public class WhisperingEffects : AccessoryEffect
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x00018C8C File Offset: 0x00016E8C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhisperingEnchantment>();
				}
			}
		}

		// Token: 0x020000EF RID: 239
		public class GhastlyCarapanceEffects : AccessoryEffect
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060003AA RID: 938 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060003AB RID: 939 RVA: 0x00018C8C File Offset: 0x00016E8C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhisperingEnchantment>();
				}
			}
		}

		// Token: 0x020000F0 RID: 240
		public class GutWrenchersSheathEffects : AccessoryEffect
		{
			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060003AD RID: 941 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060003AE RID: 942 RVA: 0x00018C8C File Offset: 0x00016E8C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhisperingEnchantment>();
				}
			}
		}
	}
}
