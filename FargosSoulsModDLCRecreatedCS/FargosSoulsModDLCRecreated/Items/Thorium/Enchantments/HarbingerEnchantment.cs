using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Tracker;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004C RID: 76
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HarbingerEnchantment : ModItem
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000A110 File Offset: 0x00008310
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A174 File Offset: 0x00008374
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<HarbingerEnchantment.HarbingerEffects>(player, base.Item))
			{
				player.statManaMax2 += (int)((double)player.statManaMax2 * 0.5);
				if (player.statMana > (int)((double)player.statManaMax2 * 0.75) || player.statMana > 300)
				{
					this.overCharge = true;
					*player.GetDamage<MagicDamageClass>() += 0.5f;
					*player.GetCritChance(DamageClass.Magic) += 26f;
					if (this.overCharge && player.statLife >= (int)((float)player.statLifeMax2 * 0.5f))
					{
						if (player.lifeRegen > 0)
						{
							player.lifeRegen = 0;
						}
						player.lifeRegenTime = 0f;
						player.lifeRegen -= 3;
					}
				}
			}
			ModItem whiteKnightEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("WhiteKnightEnchantment", ref whiteKnightEnchantment))
			{
				whiteKnightEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A29C File Offset: 0x0000849C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerChestguard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlackholeCannon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GodKiller>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpiritBendersStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteKnightEnchantment>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000032 RID: 50
		public static readonly int SetHealBonus = 5;

		// Token: 0x04000033 RID: 51
		public bool overCharge;

		// Token: 0x02000109 RID: 265
		public class HarbingerEffects : AccessoryEffect
		{
			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003FC RID: 1020 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003FD RID: 1021 RVA: 0x00018DAA File Offset: 0x00016FAA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<HarbingerEnchantment>();
				}
			}
		}
	}
}
