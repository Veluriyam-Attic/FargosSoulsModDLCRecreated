using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Victide;
using CalamityMod.Items.Weapons.Melee;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A8 RID: 168
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class VictideEnchantment : ModItem
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 80000;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00017C38 File Offset: 0x00015E38
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(67, 92, 191));
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00017CBC File Offset: 0x00015EBC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod") && AccessoryEffectLoader.AddEffect<VictideEnchantment.VictideEffects>(player, base.Item))
			{
				modPlayer.victideSet = true;
				player.ignoreWater = true;
			}
			ModItem oceanCrest;
			if (calamity.TryFind<ModItem>("OceanCrest", ref oceanCrest))
			{
				oceanCrest.UpdateAccessory(player, hideVisual);
			}
			ModItem luxorsGift;
			if (AccessoryEffectLoader.AddEffect<VictideEnchantment.LuxorsGiftEffect>(player, base.Item) && calamity.TryFind<ModItem>("LuxorsGift", ref luxorsGift))
			{
				luxorsGift.UpdateAccessory(player, hideVisual);
			}
			ModItem oceanShield;
			if (AccessoryEffectLoader.AddEffect<VictideEnchantment.ShieldOfTheOceanEffects>(player, base.Item) && calamity.TryFind<ModItem>("ShieldoftheOcean", ref oceanShield))
			{
				oceanShield.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017D74 File Offset: 0x00015F74
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Victide Helmet";
				}, new int[]
				{
					ModContent.ItemType<VictideHeadMagic>(),
					ModContent.ItemType<VictideHeadMelee>(),
					ModContent.ItemType<VictideHeadRanged>(),
					ModContent.ItemType<VictideHeadRogue>(),
					ModContent.ItemType<VictideHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Victide Helmet";
				}, new int[]
				{
					ModContent.ItemType<VictideHeadMagic>(),
					ModContent.ItemType<VictideHeadMelee>(),
					ModContent.ItemType<VictideHeadRanged>(),
					ModContent.ItemType<VictideHeadRogue>(),
					ModContent.ItemType<VictideHeadSummon>(),
					calamityBardHealer.Find<ModItem>("VictideAmmoniteHat").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyVictideHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyVictideHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<VictideBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VictideGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OceanCrest>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LuxorsGift>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShieldoftheOcean>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TeardropCleaver>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200019C RID: 412
		public class LuxorsGiftEffect : AccessoryEffect
		{
			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00019168 File Offset: 0x00017368
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<VictideEnchantment>();
				}
			}
		}

		// Token: 0x0200019D RID: 413
		public class VictideEffects : AccessoryEffect
		{
			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x060005CA RID: 1482 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x060005CB RID: 1483 RVA: 0x00019168 File Offset: 0x00017368
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<VictideEnchantment>();
				}
			}
		}

		// Token: 0x0200019E RID: 414
		public class ShieldOfTheOceanEffects : AccessoryEffect
		{
			// Token: 0x170001BB RID: 443
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x060005CE RID: 1486 RVA: 0x00019168 File Offset: 0x00017368
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<VictideEnchantment>();
				}
			}
		}
	}
}
