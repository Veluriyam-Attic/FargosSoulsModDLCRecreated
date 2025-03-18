using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Aerospec;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Summon;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x0200009C RID: 156
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class AerospecEnchantment : ModItem
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0001419C File Offset: 0x0001239C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 200000;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014200 File Offset: 0x00012400
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(159, 112, 112));
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00014284 File Offset: 0x00012484
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer modPlayer = player.Calamity();
				if (AccessoryEffectLoader.AddEffect<AerospecEnchantment.AerospecEffects>(player, base.Item))
				{
					modPlayer.aeroSet = true;
				}
				modPlayer.valkyrie = true;
				player.noFallDmg = true;
			}
			int minionType = ModContent.ProjectileType<Valkyrie>();
			if (AccessoryEffectLoader.AddEffect<AerospecEnchantment.AerospecMinion>(player, base.Item))
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
				if (player.whoAmI == Main.myPlayer && !player.HasBuff(calamity.Find<ModBuff>("ValkyrieBuff").Type))
				{
					player.AddBuff(calamity.Find<ModBuff>("ValkyrieBuff").Type, 3600, true, false);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == ModContent.ProjectileType<Valkyrie>() && Main.projectile[i].active)
					{
						Main.projectile[i].Kill();
					}
				}
			}
			ModItem gladiatorsLocket;
			if (AccessoryEffectLoader.AddEffect<AerospecEnchantment.GladiatorsLocketEffect>(player, base.Item) && calamity.TryFind<ModItem>("GladiatorsLocket", ref gladiatorsLocket))
			{
				gladiatorsLocket.UpdateAccessory(player, hideVisual);
			}
			ModItem unstableGraniteCore;
			if (AccessoryEffectLoader.AddEffect<AerospecEnchantment.UnstableGraniteCoreEffect>(player, base.Item) && calamity.TryFind<ModItem>("UnstableGraniteCore", ref unstableGraniteCore))
			{
				unstableGraniteCore.UpdateAccessory(player, hideVisual);
			}
			ModItem aeroStone;
			if (calamity.TryFind<ModItem>("AeroStone", ref aeroStone))
			{
				aeroStone.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00014428 File Offset: 0x00012628
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Aerospec Helmet";
				}, new int[]
				{
					ModContent.ItemType<AerospecHat>(),
					ModContent.ItemType<AerospecHeadgear>(),
					ModContent.ItemType<AerospecHelm>(),
					ModContent.ItemType<AerospecHood>(),
					ModContent.ItemType<AerospecHelmet>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Aerospec Helmet";
				}, new int[]
				{
					ModContent.ItemType<AerospecHat>(),
					ModContent.ItemType<AerospecHeadgear>(),
					ModContent.ItemType<AerospecHelm>(),
					ModContent.ItemType<AerospecHood>(),
					ModContent.ItemType<AerospecHelmet>(),
					calamityBardHealer.Find<ModItem>("AerospecBiretta").Type,
					calamityBardHealer.Find<ModItem>("AerospecHeadphones").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyAerospecHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyAerospecHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<AerospecBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AerospecLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GladiatorsLocket>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UnstableGraniteCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AeroStone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StormSurge>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x02000167 RID: 359
		public class AerospecMinion : AccessoryEffect
		{
			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000523 RID: 1315 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000524 RID: 1316 RVA: 0x00018FFB File Offset: 0x000171FB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AerospecEnchantment>();
				}
			}
		}

		// Token: 0x02000168 RID: 360
		public class AerospecEffects : AccessoryEffect
		{
			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000526 RID: 1318 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000527 RID: 1319 RVA: 0x00018FFB File Offset: 0x000171FB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AerospecEnchantment>();
				}
			}
		}

		// Token: 0x02000169 RID: 361
		public class GladiatorsLocketEffect : AccessoryEffect
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000529 RID: 1321 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x0600052A RID: 1322 RVA: 0x00018FFB File Offset: 0x000171FB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AerospecEnchantment>();
				}
			}
		}

		// Token: 0x0200016A RID: 362
		public class UnstableGraniteCoreEffect : AccessoryEffect
		{
			// Token: 0x17000163 RID: 355
			// (get) Token: 0x0600052C RID: 1324 RVA: 0x00018EC6 File Offset: 0x000170C6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AnnihilationForceHeader>();
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x0600052D RID: 1325 RVA: 0x00018FFB File Offset: 0x000171FB
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AerospecEnchantment>();
				}
			}
		}
	}
}
