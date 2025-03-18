using System;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Sulphurous;
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
	// Token: 0x02000099 RID: 153
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class SulphurousEnchantment : ModItem
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000133D4 File Offset: 0x000115D4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 50000;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00013438 File Offset: 0x00011638
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Attacking and being attacked by enemies inflicts poison"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Grants an additional jump that summons a sulphurous bubble"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Provides increased underwater mobility and reduces the severity of the sulphuric waters"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Sand Cloak, Old Die, Rusty Medallion, Amadias Pendant, and Alluring Bait"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Sand Cloak, Old Die, Rusty Medallion, and Amadias Pendant"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(70, 63, 69));
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00013550 File Offset: 0x00011750
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod") && AccessoryEffectLoader.AddEffect<SulphurousEnchantment.SulphurousEffect>(player, base.Item))
			{
				modPlayer.sulphurSet = true;
				player.GetJumpState<SulphurJump>().Enable();
				modPlayer.rogueStealthMax += 0.65f;
				modPlayer.wearingRogueArmor = true;
				player.ignoreWater = true;
			}
			ModItem sandCloak;
			if (AccessoryEffectLoader.AddEffect<SulphurousEnchantment.SandCloakEffects>(player, base.Item) && calamity.TryFind<ModItem>("SandCloak", ref sandCloak))
			{
				sandCloak.UpdateAccessory(player, hideVisual);
			}
			ModItem alluringBait;
			if (!ModLoader.HasMod("FargowiltasCrossmod") && calamity.TryFind<ModItem>("AlluringBait", ref alluringBait))
			{
				alluringBait.UpdateAccessory(player, hideVisual);
			}
			ModItem amadiasPendant;
			if (AccessoryEffectLoader.AddEffect<SulphurousEnchantment.AmadiasPendantEffects>(player, base.Item) && calamity.TryFind<ModItem>("AmadiasPendant", ref amadiasPendant))
			{
				amadiasPendant.UpdateAccessory(player, hideVisual);
			}
			ModItem rustyMedallion;
			if (AccessoryEffectLoader.AddEffect<SulphurousEnchantment.RustyMedallionEffects>(player, base.Item) && calamity.TryFind<ModItem>("RustyMedallion", ref rustyMedallion))
			{
				rustyMedallion.UpdateAccessory(player, hideVisual);
			}
			ModItem oldDie;
			if (calamity.TryFind<ModItem>("OldDie", ref oldDie))
			{
				oldDie.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00013674 File Offset: 0x00011874
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SulphurousHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SulphurousBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SulphurousLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SandCloak>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<AlluringBait>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<AmidiasPendant>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OldDie>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RustyMedallion>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CausticCroakerStaff>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x02000158 RID: 344
		public class SulphurousEffect : AccessoryEffect
		{
			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00018FBA File Offset: 0x000171BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SulphurousEnchantment>();
				}
			}
		}

		// Token: 0x02000159 RID: 345
		public class SandCloakEffects : AccessoryEffect
		{
			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00018FBA File Offset: 0x000171BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SulphurousEnchantment>();
				}
			}
		}

		// Token: 0x0200015A RID: 346
		public class AmadiasPendantEffects : AccessoryEffect
		{
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00018FBA File Offset: 0x000171BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SulphurousEnchantment>();
				}
			}
		}

		// Token: 0x0200015B RID: 347
		public class RustyMedallionEffects : AccessoryEffect
		{
			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00018F1F File Offset: 0x0001711F
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<DesolationForceHeader>();
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00018FBA File Offset: 0x000171BA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SulphurousEnchantment>();
				}
			}
		}
	}
}
