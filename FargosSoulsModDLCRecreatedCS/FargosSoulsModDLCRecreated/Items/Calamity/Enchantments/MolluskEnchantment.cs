using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Projectiles.Summon;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000095 RID: 149
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class MolluskEnchantment : ModItem
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00011D40 File Offset: 0x0000FF40
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 150000;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(74, 97, 96));
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00011E28 File Offset: 0x00010028
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			bool molluskEffectApplied = AccessoryEffectLoader.AddEffect<MolluskEnchantment.ShellfishMinionEffect>(player, base.Item);
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				*player.GetDamage<GenericDamageClass>() += 0.1f;
				modPlayer.molluskSet = true;
				modPlayer.shellfish = true;
				player.maxMinions += 4;
				if (player.whoAmI == Main.myPlayer)
				{
					if (molluskEffectApplied)
					{
						IEntitySource source = player.GetSource_ItemUse(base.Item, null);
						if (player.FindBuffIndex(ModContent.BuffType<ShellfishBuff>()) == -1)
						{
							player.AddBuff(ModContent.BuffType<ShellfishBuff>(), 3600, true, false);
						}
						if (player.ownedProjectileCounts[ModContent.ProjectileType<Shellfish>()] < 2)
						{
							int baseDamage = player.ApplyArmorAccDamageBonusesTo(140f);
							Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<Shellfish>(), baseDamage, 0f, player.whoAmI, 0f, 0f, 0f).originalDamage = baseDamage;
						}
					}
					else
					{
						for (int i = 0; i < Main.maxProjectiles; i++)
						{
							if (Main.projectile[i].type == ModContent.ProjectileType<Shellfish>() && Main.projectile[i].active)
							{
								Main.projectile[i].Kill();
							}
						}
					}
				}
				player.Calamity().wearingRogueArmor = true;
			}
			ModItem giantPearl;
			if (AccessoryEffectLoader.AddEffect<MolluskEnchantment.GiantPearlEffect>(player, base.Item) && calamity.TryFind<ModItem>("GiantPearl", ref giantPearl))
			{
				giantPearl.UpdateAccessory(player, hideVisual);
			}
			ModItem aquaticEmblem;
			if (AccessoryEffectLoader.AddEffect<MolluskEnchantment.AquaticEmblemEffect>(player, base.Item) && calamity.TryFind<ModItem>("AquaticEmblem", ref aquaticEmblem))
			{
				aquaticEmblem.UpdateAccessory(player, hideVisual);
			}
			ModItem dD;
			if (AccessoryEffectLoader.AddEffect<MolluskEnchantment.DeepDiverEffect>(player, base.Item) && calamity.TryFind<ModItem>("DeepDiver", ref dD))
			{
				dD.UpdateAccessory(player, hideVisual);
			}
			ModItem victideEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("VictideEnchantment", ref victideEnchantment))
			{
				victideEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00012030 File Offset: 0x00010230
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MolluskShellmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MolluskShellplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MolluskShelleggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VictideEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GiantPearl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AquaticEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DeepDiver>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000144 RID: 324
		public class ShellfishMinionEffect : AccessoryEffect
		{
			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00018F2B File Offset: 0x0001712B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MolluskEnchantment>();
				}
			}
		}

		// Token: 0x02000145 RID: 325
		public class GiantPearlEffect : AccessoryEffect
		{
			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00018F2B File Offset: 0x0001712B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MolluskEnchantment>();
				}
			}
		}

		// Token: 0x02000146 RID: 326
		public class AquaticEmblemEffect : AccessoryEffect
		{
			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00018F2B File Offset: 0x0001712B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MolluskEnchantment>();
				}
			}
		}

		// Token: 0x02000147 RID: 327
		public class DeepDiverEffect : AccessoryEffect
		{
			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00018F2B File Offset: 0x0001712B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MolluskEnchantment>();
				}
			}
		}
	}
}
