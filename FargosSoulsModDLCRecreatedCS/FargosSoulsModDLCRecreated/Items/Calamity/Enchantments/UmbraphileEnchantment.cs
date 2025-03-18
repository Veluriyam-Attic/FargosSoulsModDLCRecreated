using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Umbraphile;
using CalamityMod.Items.Pets;
using CalamityMod.Projectiles.Pets;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A2 RID: 162
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class UmbraphileEnchantment : ModItem
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00016294 File Offset: 0x00014494
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 300000;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000162F8 File Offset: 0x000144F8
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(70, 63, 69));
				}
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001637C File Offset: 0x0001457C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			player.GetModPlayer<CalDLCPlayer>().umbraphileActive = true;
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				calamityPlayer.umbraphileSet = true;
				calamityPlayer.thiefsDime = true;
				calamityPlayer.rogueStealthMax += 1.1f;
				player.Calamity().wearingRogueArmor = true;
				int minionType = ModContent.ProjectileType<GoldiePet>();
				if (AccessoryEffectLoader.AddEffect<UmbraphileEnchantment.GoldiePetEffect>(player, base.Item))
				{
					if (player.ownedProjectileCounts[minionType] < 1)
					{
						Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
					}
				}
				else
				{
					for (int i = 0; i < Main.maxProjectiles; i++)
					{
						if (Main.projectile[i].type == ModContent.ProjectileType<GoldiePet>() && Main.projectile[i].active && player.miscEquips[1].type != ModContent.ItemType<ThiefsDime>())
						{
							Main.projectile[i].Kill();
						}
					}
				}
			}
			ModItem thiefsDime;
			if (calamity.TryFind<ModItem>("ThiefsDime", ref thiefsDime))
			{
				thiefsDime.UpdateAccessory(player, hideVisual);
			}
			ModItem vampiricTalisman;
			if (calamity.TryFind<ModItem>("VampiricTalisman", ref vampiricTalisman))
			{
				vampiricTalisman.UpdateAccessory(player, hideVisual);
			}
			ModItem evasionScarf;
			if (calamity.TryFind<ModItem>("EvasionScarf", ref evasionScarf))
			{
				evasionScarf.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000164EC File Offset: 0x000146EC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<UmbraphileHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UmbraphileRegalia>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UmbraphileBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThiefsDime>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VampiricTalisman>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EvasionScarf>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MomentumCapacitor>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000188 RID: 392
		public class GoldiePetEffect : AccessoryEffect
		{
			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000589 RID: 1417 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600058A RID: 1418 RVA: 0x000190C1 File Offset: 0x000172C1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<UmbraphileEnchantment>();
				}
			}
		}
	}
}
