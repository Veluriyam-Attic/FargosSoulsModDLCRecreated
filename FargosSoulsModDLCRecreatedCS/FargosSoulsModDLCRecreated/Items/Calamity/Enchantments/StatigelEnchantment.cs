using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Statigel;
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
	// Token: 0x02000093 RID: 147
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class StatigelEnchantment : ModItem
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00011678 File Offset: 0x0000F878
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 200000;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000116DC File Offset: 0x0000F8DC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Statis’ mystical power surrounds you…"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "When you take over 100 damage in one hit you become immune to damage for an extended period of time"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Grants an extra jump and increased jump height"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Fungal Clump, Mana Polarizer, and Fungal Symbiote"));
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(181, 0, 156));
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000117D0 File Offset: 0x0000F9D0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer modPlayer = player.Calamity();
				if (AccessoryEffectLoader.AddEffect<StatigelEnchantment.StatigelEffects>(player, base.Item))
				{
					modPlayer.statigelSet = true;
					player.GetJumpState<StatigelJump>().Enable();
					player.jumpBoost = true;
					player.jumpSpeedBoost += 0.6f;
				}
			}
			ModItem fungalSymbiote;
			if (AccessoryEffectLoader.AddEffect<StatigelEnchantment.FungalSymbioteEffects>(player, base.Item) && calamity.TryFind<ModItem>("FungalSymbiote", ref fungalSymbiote))
			{
				fungalSymbiote.UpdateAccessory(player, hideVisual);
			}
			ModItem manaOverloader;
			if (calamity.TryFind<ModItem>("ManaPolarizer", ref manaOverloader))
			{
				manaOverloader.UpdateAccessory(player, hideVisual);
			}
			ModItem fC;
			if (calamity.TryFind<ModItem>("FungalClump", ref fC))
			{
				fC.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011898 File Offset: 0x0000FA98
		public override void AddRecipes()
		{
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Statigel Helmet";
				}, new int[]
				{
					ModContent.ItemType<StatigelHeadMagic>(),
					ModContent.ItemType<StatigelHeadMelee>(),
					ModContent.ItemType<StatigelHeadRanged>(),
					ModContent.ItemType<StatigelHeadRogue>(),
					ModContent.ItemType<StatigelHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Statigel Helmet";
				}, new int[]
				{
					ModContent.ItemType<StatigelHeadMagic>(),
					ModContent.ItemType<StatigelHeadMelee>(),
					ModContent.ItemType<StatigelHeadRanged>(),
					ModContent.ItemType<StatigelHeadRogue>(),
					ModContent.ItemType<StatigelHeadSummon>(),
					calamityBardHealer.Find<ModItem>("StatigelEarrings").Type,
					calamityBardHealer.Find<ModItem>("StatigelFoxMask").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyStatigelHelmet", group);
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyStatigelHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<StatigelArmor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StatigelGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FungalClump>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ManaPolarizer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FungalSymbiote>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200013E RID: 318
		public class FungalSymbioteEffects : AccessoryEffect
		{
			// Token: 0x17000113 RID: 275
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x00018ED9 File Offset: 0x000170D9
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<StatigelEnchantment>();
				}
			}
		}

		// Token: 0x0200013F RID: 319
		public class StatigelEffects : AccessoryEffect
		{
			// Token: 0x17000115 RID: 277
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x0600049F RID: 1183 RVA: 0x00018ED9 File Offset: 0x000170D9
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<StatigelEnchantment>();
				}
			}
		}
	}
}
