using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.SnowRuffian;
using CalamityMod.Items.Mounts;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x0200009D RID: 157
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class SnowRuffianEnchantment : ModItem
	{
		// Token: 0x0600029D RID: 669 RVA: 0x000145A0 File Offset: 0x000127A0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 10000;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00014604 File Offset: 0x00012804
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(191, 68, 59));
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00014688 File Offset: 0x00012888
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (AccessoryEffectLoader.AddEffect<SnowRuffianEnchantment.SnowRuffianEffect>(player, base.Item))
				{
					modPlayer.snowRuffianSet = true;
				}
				modPlayer.rogueStealthMax += 0.5f;
				*player.GetDamage<ThrowingDamageClass>() += 0.05f;
				player.Calamity().wearingRogueArmor = true;
				if (player.controlJump)
				{
					player.noFallDmg = true;
					player.UpdateJumpHeight();
					if (this.shouldBoost && !player.mount.Active)
					{
						player.velocity.X = player.velocity.X * 1.1f;
						this.shouldBoost = false;
					}
				}
				else if (!this.shouldBoost && player.velocity.Y == 0f)
				{
					this.shouldBoost = true;
				}
			}
			ModItem scuttlersJewel;
			if (AccessoryEffectLoader.AddEffect<SnowRuffianEnchantment.ScuttlersJewelEffect>(player, base.Item) && calamity.TryFind<ModItem>("ScuttlersJewel", ref scuttlersJewel))
			{
				scuttlersJewel.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000147A0 File Offset: 0x000129A0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SnowRuffianMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SnowRuffianChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SnowRuffianGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ScuttlersJewel>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TundraLeash>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400005E RID: 94
		private bool shouldBoost;

		// Token: 0x0200016C RID: 364
		public class ScuttlersJewelEffect : AccessoryEffect
		{
			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000533 RID: 1331 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001902E File Offset: 0x0001722E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SnowRuffianEnchantment>();
				}
			}
		}

		// Token: 0x0200016D RID: 365
		public class SnowRuffianEffect : AccessoryEffect
		{
			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000536 RID: 1334 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001902E File Offset: 0x0001722E
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SnowRuffianEnchantment>();
				}
			}
		}
	}
}
