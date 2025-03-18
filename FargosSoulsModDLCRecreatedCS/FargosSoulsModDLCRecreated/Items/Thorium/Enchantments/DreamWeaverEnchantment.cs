using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials;
using ThoriumMod.Items.BossThePrimordials.Dream;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002C RID: 44
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DreamWeaverEnchantment : ModItem
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00005D9C File Offset: 0x00003F9C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005E00 File Offset: 0x00004000
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

		// Token: 0x06000097 RID: 151 RVA: 0x00005E88 File Offset: 0x00004088
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<DreamWeaverEnchantment.DreamWeaverEffects>(player, base.Item))
			{
				ModItem dreamWeaversHelmet;
				if (thorium.TryFind<ModItem>("DreamWeaversHelmet", ref dreamWeaversHelmet))
				{
					PlayerHelper.GetThoriumPlayer(player).setDreamWeaversMask.Set(dreamWeaversHelmet.Item);
				}
				PlayerHelper.GetThoriumPlayer(player).setDreamWeaversHood = true;
			}
			ModItem tOC;
			if (AccessoryEffectLoader.AddEffect<DreamWeaverEnchantment.TheOmegaCoreEffects>(player, base.Item) && thorium.TryFind<ModItem>("TheOmegaCore", ref tOC))
			{
				tOC.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005F20 File Offset: 0x00004120
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DreamWeaversHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreamWeaversHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreamWeaversTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreamWeaversTreads>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheOmegaCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonHeartWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SnackLantern>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChristmasCheer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MolecularStabilizer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreamCatcher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitanicTrioPaint>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x020000C1 RID: 193
		public class DreamWeaverEffects : AccessoryEffect
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000325 RID: 805 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000326 RID: 806 RVA: 0x00018B4E File Offset: 0x00016D4E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DreamWeaverEnchantment>();
				}
			}
		}

		// Token: 0x020000C2 RID: 194
		public class TheOmegaCoreEffects : AccessoryEffect
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000328 RID: 808 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000329 RID: 809 RVA: 0x00018B4E File Offset: 0x00016D4E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DreamWeaverEnchantment>();
				}
			}
		}
	}
}
