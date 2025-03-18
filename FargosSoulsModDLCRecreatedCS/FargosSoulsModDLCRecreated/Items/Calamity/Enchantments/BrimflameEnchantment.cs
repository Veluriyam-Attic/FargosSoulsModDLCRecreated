using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000098 RID: 152
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class BrimflameEnchantment : ModItem
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00013084 File Offset: 0x00011284
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 600000;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000130E8 File Offset: 0x000112E8
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

		// Token: 0x06000284 RID: 644 RVA: 0x0001316C File Offset: 0x0001136C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod") && AccessoryEffectLoader.AddEffect<BrimflameEnchantment.BrimflameEffects>(player, base.Item))
			{
				modPlayer.brimflameSet = true;
				player.buffImmune[ModContent.BuffType<BrimstoneFlames>()] = true;
				player.buffImmune[24] = true;
				player.buffImmune[44] = true;
				this.UpdateFrenzy(player);
			}
			ModItem vOE;
			if (AccessoryEffectLoader.AddEffect<BrimflameEnchantment.VoidOfExtinctionEffects>(player, base.Item) && calamity.TryFind<ModItem>("VoidofExtinction", ref vOE))
			{
				vOE.UpdateAccessory(player, hideVisual);
			}
			ModItem vOC;
			if (AccessoryEffectLoader.AddEffect<BrimflameEnchantment.VoidOfCalamityEffects>(player, base.Item) && calamity.TryFind<ModItem>("VoidofCalamity", ref vOC))
			{
				vOC.UpdateAccessory(player, hideVisual);
			}
			ModItem cS;
			if (AccessoryEffectLoader.AddEffect<BrimflameEnchantment.ChaosStoneEffects>(player, base.Item) && calamity.TryFind<ModItem>("ChaosStone", ref cS))
			{
				cS.UpdateAccessory(player, hideVisual);
			}
			ModItem fLS;
			if (AccessoryEffectLoader.AddEffect<BrimflameEnchantment.FlameLickedShellEffects>(player, base.Item) && calamity.TryFind<ModItem>("FlameLickedShell", ref fLS))
			{
				fLS.UpdateAccessory(player, hideVisual);
			}
			ModItem sP;
			if (AccessoryEffectLoader.AddEffect<BrimflameEnchantment.SlagslitterPauldronEffects>(player, base.Item) && calamity.TryFind<ModItem>("SlagsplitterPauldron", ref sP))
			{
				sP.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000132A0 File Offset: 0x000114A0
		private unsafe void UpdateFrenzy(Player player)
		{
			CalamityPlayer modPlayer = player.Calamity();
			if (!this.frenzy)
			{
				if (modPlayer.brimflameFrenzy)
				{
					this.frenzy = true;
				}
			}
			else if (!modPlayer.brimflameFrenzy)
			{
				this.frenzy = false;
				player.AddCooldown(BrimflameFrenzy.ID, BrimflameEnchantment.CooldownLength, true);
			}
			if (this.frenzy)
			{
				*player.GetDamage<MagicDamageClass>() += 0.4f;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00013314 File Offset: 0x00011514
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BrimflameScowl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BrimflameRobes>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BrimflameBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VoidofExtinction>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VoidofCalamity>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlagsplitterPauldron>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChaosStone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FlameLickedShell>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Butcher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IgneousExaltation>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlazingStar>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400005B RID: 91
		private bool frenzy;

		// Token: 0x0400005C RID: 92
		public static int CooldownLength = 1800;

		// Token: 0x02000152 RID: 338
		public class BrimflameEffects : AccessoryEffect
		{
			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060004DD RID: 1245 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060004DE RID: 1246 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}

		// Token: 0x02000153 RID: 339
		public class VoidOfExtinctionEffects : AccessoryEffect
		{
			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}

		// Token: 0x02000154 RID: 340
		public class VoidOfCalamityEffects : AccessoryEffect
		{
			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}

		// Token: 0x02000155 RID: 341
		public class FlameLickedShellEffects : AccessoryEffect
		{
			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}

		// Token: 0x02000156 RID: 342
		public class ChaosStoneEffects : AccessoryEffect
		{
			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060004EA RID: 1258 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}

		// Token: 0x02000157 RID: 343
		public class SlagslitterPauldronEffects : AccessoryEffect
		{
			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060004EC RID: 1260 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060004ED RID: 1261 RVA: 0x00018FB3 File Offset: 0x000171B3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BrimflameEnchantment>();
				}
			}
		}
	}
}
