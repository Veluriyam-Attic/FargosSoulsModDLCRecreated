using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004B RID: 75
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class AssassinEnchantment : ModItem
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00009EE8 File Offset: 0x000080E8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009F4C File Offset: 0x0000814C
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(255, 128, 0));
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009FD4 File Offset: 0x000081D4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<AssassinEnchantment.AssassinEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).masterArbalestHoodSet = true;
				PlayerHelper.GetThoriumPlayer(player).masterMarksmansScouterSet = true;
			}
			ModItem dartPouch;
			if (AccessoryEffectLoader.AddEffect<AssassinEnchantment.DartPouchEffects>(player, base.Item) && thorium.TryFind<ModItem>("DartPouch", ref dartPouch))
			{
				dartPouch.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A050 File Offset: 0x00008250
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Assassin Helmet";
			}, new int[]
			{
				ModContent.ItemType<MasterArbalestHood>(),
				ModContent.ItemType<MasterMarksmansScouter>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyAssassinHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyAssassinHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<AssassinsGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AssassinsWalkers>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DartPouch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheBlackBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WyrmDecimator>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x02000106 RID: 262
		public class AssassinEffects : AccessoryEffect
		{
			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00018D77 File Offset: 0x00016F77
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AssassinEnchantment>();
				}
			}
		}

		// Token: 0x02000107 RID: 263
		public class DartPouchEffects : AccessoryEffect
		{
			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00018D77 File Offset: 0x00016F77
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AssassinEnchantment>();
				}
			}
		}
	}
}
