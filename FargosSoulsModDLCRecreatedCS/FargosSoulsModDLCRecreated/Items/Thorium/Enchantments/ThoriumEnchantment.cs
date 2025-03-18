using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.Thorium;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003D RID: 61
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumEnchantment : ModItem
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00008500 File Offset: 0x00006700
		public override void SetDefaults()
		{
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008554 File Offset: 0x00006754
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				*player.GetDamage(DamageClass.Generic) += 0.1f;
			}
			ModItem crietz;
			if (AccessoryEffectLoader.AddEffect<ThoriumEnchantment.CrietzEffects>(player, base.Item) && thorium.TryFind<ModItem>("Crietz", ref crietz))
			{
				crietz.UpdateAccessory(player, hideVisual);
			}
			ModItem BOR;
			if (AccessoryEffectLoader.AddEffect<ThoriumEnchantment.BandOfReplenishmentEffects>(player, base.Item) && thorium.TryFind<ModItem>("BandofReplenishment", ref BOR))
			{
				BOR.UpdateAccessory(player, hideVisual);
			}
			ModItem tR;
			if (thorium.TryFind<ModItem>("TheRing", ref tR))
			{
				tR.UpdateAccessory(player, hideVisual);
			}
			ModItem jesterEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("JesterEnchantment", ref jesterEnchantment))
			{
				jesterEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008628 File Offset: 0x00006828
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JesterEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Crietz>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheRing>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BandofReplenishment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LastSupperPaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400002E RID: 46
		public static readonly int SetHealBonus = 5;

		// Token: 0x020000E8 RID: 232
		public class BandOfReplenishmentEffects : AccessoryEffect
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000395 RID: 917 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000396 RID: 918 RVA: 0x00018C44 File Offset: 0x00016E44
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ThoriumEnchantment>();
				}
			}
		}

		// Token: 0x020000E9 RID: 233
		public class CrietzEffects : AccessoryEffect
		{
			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000398 RID: 920 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000399 RID: 921 RVA: 0x00018C44 File Offset: 0x00016E44
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ThoriumEnchantment>();
				}
			}
		}
	}
}
