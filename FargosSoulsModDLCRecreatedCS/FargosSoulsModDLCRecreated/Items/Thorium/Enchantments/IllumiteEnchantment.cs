using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Illumite;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004F RID: 79
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class IllumiteEnchantment : ModItem
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A74C File Offset: 0x0000894C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<IllumiteEnchantment.IllumiteEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setIllumite = true;
			}
			ModItem tPLR;
			if (thorium.TryFind<ModItem>("TunePlayerLifeRegen", ref tPLR))
			{
				tPLR.UpdateAccessory(player, hideVisual);
			}
			ModItem cW;
			if (AccessoryEffectLoader.AddEffect<IllumiteEnchantment.ConcussiveWarheadEffects>(player, base.Item) && thorium.TryFind<ModItem>("ConcussiveWarhead", ref cW))
			{
				cW.UpdateAccessory(player, hideVisual);
			}
			ModItem tNO;
			if (thorium.TryFind<ModItem>("TheNuclearOption", ref tNO))
			{
				tNO.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A7EC File Offset: 0x000089EC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TunePlayerLifeRegen>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConcussiveWarhead>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheNuclearOption>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PinkPhasesaber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HandCannon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteBlaster>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteBarrage>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlobhornCoralStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LargeOpal>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000110 RID: 272
		public class IllumiteEffects : AccessoryEffect
		{
			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x00018DEB File Offset: 0x00016FEB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IllumiteEnchantment>();
				}
			}
		}

		// Token: 0x02000111 RID: 273
		public class ConcussiveWarheadEffects : AccessoryEffect
		{
			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x00018DEB File Offset: 0x00016FEB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<IllumiteEnchantment>();
				}
			}
		}
	}
}
