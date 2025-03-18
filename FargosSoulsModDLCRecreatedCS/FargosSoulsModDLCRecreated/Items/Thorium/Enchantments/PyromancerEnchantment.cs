using System;
using System.Collections.Generic;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000064 RID: 100
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PyromancerEnchantment : ModItem
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 400000;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(255, 128, 0));
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<PyromancerEnchantment.PyromancerEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).napalm = true;
				PlayerHelper.GetThoriumPlayer(player).setPyromancer = true;
				Lighting.AddLight(player.Center, 0.5f, 0.35f, 0f);
			}
			ModItem plasmaGenerator;
			if (AccessoryEffectLoader.AddEffect<PyromancerEnchantment.PlasmaGeneratorEffects>(player, base.Item) && thorium.TryFind<ModItem>("PlasmaGenerator", ref plasmaGenerator))
			{
				plasmaGenerator.UpdateAccessory(player, hideVisual);
			}
			ModItem pocketFusionGenerator;
			if (AccessoryEffectLoader.AddEffect<PyromancerEnchantment.PocketFusionGeneratorEffects>(player, base.Item) && thorium.TryFind<ModItem>("PocketFusionGenerator", ref pocketFusionGenerator))
			{
				pocketFusionGenerator.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MagmaSeersMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PyromancerCowl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PyromancerTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PyromancerLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlasmaGenerator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PocketFusionGenerator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Stalagmite>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DevilDagger>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MortarStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AncientFlame>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AlmanacofAgony>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0200012D RID: 301
		public class PyromancerEffects : AccessoryEffect
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000468 RID: 1128 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x00018E69 File Offset: 0x00017069
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PyromancerEnchantment>();
				}
			}
		}

		// Token: 0x0200012E RID: 302
		public class PlasmaGeneratorEffects : AccessoryEffect
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600046B RID: 1131 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600046C RID: 1132 RVA: 0x00018E69 File Offset: 0x00017069
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PyromancerEnchantment>();
				}
			}
		}

		// Token: 0x0200012F RID: 303
		public class PocketFusionGeneratorEffects : AccessoryEffect
		{
			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x00018B42 File Offset: 0x00016D42
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AsgardForceHeader>();
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x00018E69 File Offset: 0x00017069
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PyromancerEnchantment>();
				}
			}
		}
	}
}
