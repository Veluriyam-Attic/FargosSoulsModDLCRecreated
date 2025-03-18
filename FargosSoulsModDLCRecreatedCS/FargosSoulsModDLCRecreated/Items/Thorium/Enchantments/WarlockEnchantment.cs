using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000060 RID: 96
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class WarlockEnchantment : ModItem
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000C458 File Offset: 0x0000A658
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<WarlockEnchantment.WarlockEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).warlockSet = true;
			}
			ModItem demonTongue;
			if (AccessoryEffectLoader.AddEffect<WarlockEnchantment.DemonTongueEffects>(player, base.Item) && thorium.TryFind<ModItem>("DemonTongue", ref demonTongue))
			{
				demonTongue.UpdateAccessory(player, hideVisual);
			}
			ModItem darkEffigy;
			if (AccessoryEffectLoader.AddEffect<WarlockEnchantment.DarkEffigyEffects>(player, base.Item) && thorium.TryFind<ModItem>("DarkEffigy", ref darkEffigy))
			{
				darkEffigy.UpdateAccessory(player, hideVisual);
			}
			ModItem ebonEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("EbonEnchantment", ref ebonEnchantment))
			{
				ebonEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000C574 File Offset: 0x0000A774
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WarlockHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WarlockGarb>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WarlockLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EbonEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonTongue>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkEffigy>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400003C RID: 60
		public static readonly int SetHealBonus = 5;

		// Token: 0x02000125 RID: 293
		public class WarlockEffects : AccessoryEffect
		{
			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x00018E4D File Offset: 0x0001704D
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WarlockEnchantment>();
				}
			}
		}

		// Token: 0x02000126 RID: 294
		public class DemonTongueEffects : AccessoryEffect
		{
			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x00018E4D File Offset: 0x0001704D
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WarlockEnchantment>();
				}
			}
		}

		// Token: 0x02000127 RID: 295
		public class DarkEffigyEffects : AccessoryEffect
		{
			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00018E4D File Offset: 0x0001704D
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WarlockEnchantment>();
				}
			}
		}
	}
}
