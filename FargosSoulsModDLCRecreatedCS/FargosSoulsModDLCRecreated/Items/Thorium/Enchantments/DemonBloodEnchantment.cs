using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.DemonBlood;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005D RID: 93
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DemonBloodEnchantment : ModItem
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000BE28 File Offset: 0x0000A028
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				player.statLifeMax2 += 100;
				PlayerHelper.GetThoriumPlayer(player).setDemonBlood = true;
			}
			ModItem rapierBadge;
			if (thorium.TryFind<ModItem>("RapierBadger", ref rapierBadge))
			{
				rapierBadge.UpdateAccessory(player, hideVisual);
			}
			ModItem vFC;
			if (AccessoryEffectLoader.AddEffect<DemonBloodEnchantment.VileFlailCoreEffect>(player, base.Item) && thorium.TryFind<ModItem>("VileFlailCore", ref vFC))
			{
				vFC.UpdateAccessory(player, hideVisual);
			}
			ModItem fleshEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("FleshEnchantment", ref fleshEnchantment))
			{
				fleshEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BF34 File Offset: 0x0000A134
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DemonBloodHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonBloodBreastPlate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonBloodGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FleshEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RapierBadge>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VileFlailCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonBloodRipper>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkContagion>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FesteringBalloon>(), 300);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000039 RID: 57
		public static readonly int SetHealBonus = 5;

		// Token: 0x02000122 RID: 290
		public class VileFlailCoreEffect : AccessoryEffect
		{
			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x00018E38 File Offset: 0x00017038
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DemonBloodEnchantment>();
				}
			}
		}
	}
}
