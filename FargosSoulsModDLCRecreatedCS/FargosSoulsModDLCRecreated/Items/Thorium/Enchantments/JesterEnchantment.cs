using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000044 RID: 68
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class JesterEnchantment : ModItem
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00009178 File Offset: 0x00007378
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000091DC File Offset: 0x000073DC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<JesterEnchantment.JesterEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setJester = true;
			}
			ModItem fanLetter;
			if (AccessoryEffectLoader.AddEffect<JesterEnchantment.FanLetterEffects>(player, base.Item) && thorium.TryFind<ModItem>("FanLetter", ref fanLetter))
			{
				fanLetter.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000924C File Offset: 0x0000744C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Jester Mask";
			}, new int[]
			{
				ModContent.ItemType<JestersMask>(),
				ModContent.ItemType<JestersMask2>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyJesterMask", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyJesterMask", 1);
			RecipeGroup group2 = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Jester Shirt";
			}, new int[]
			{
				ModContent.ItemType<JestersShirt>(),
				ModContent.ItemType<JestersShirt2>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyJesterShirt", group2);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyJesterShirt", 1);
			RecipeGroup group3 = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Jester Leggings";
			}, new int[]
			{
				ModContent.ItemType<JestersLeggings>(),
				ModContent.ItemType<JestersLeggings2>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyJesterLeggings", group3);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyJesterLeggings", 1);
			RecipeGroup group4 = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Letter";
			}, new int[]
			{
				ModContent.ItemType<FanLetter>(),
				ModContent.ItemType<FanLetter2>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyLetter", group4);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyLetter", 1);
			RecipeGroup group5 = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Tambourine";
			}, new int[]
			{
				ModContent.ItemType<EbonWoodTambourine>(),
				ModContent.ItemType<ShadeWoodTambourine>(),
				ModContent.ItemType<Tambourine>(),
				ModContent.ItemType<TheGreenTambourine>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyTambourine", group5);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyTambourine", 1);
			recipe.AddIngredient(ModContent.ItemType<MeteoriteOboe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SkywareLute>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Panflute>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConchShell>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Alphorn>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000F5 RID: 245
		public class JesterEffects : AccessoryEffect
		{
			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060003BD RID: 957 RVA: 0x00018C9A File Offset: 0x00016E9A
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<JesterEnchantment>();
				}
			}
		}

		// Token: 0x020000F6 RID: 246
		public class FanLetterEffects : AccessoryEffect
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060003BF RID: 959 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x00018C9A File Offset: 0x00016E9A
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<JesterEnchantment>();
				}
			}
		}
	}
}
