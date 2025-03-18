using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000061 RID: 97
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class LifeBinderEnchantment : ModItem
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C654 File Offset: 0x0000A854
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setLifeBinder = true;
			}
			ModItem dewCollector;
			if (AccessoryEffectLoader.AddEffect<LifeBinderEnchantment.AloeLeafEffects>(player, base.Item) && thorium.TryFind<ModItem>("DewCollector", ref dewCollector))
			{
				dewCollector.UpdateAccessory(player, hideVisual);
			}
			ModItem fC;
			if (AccessoryEffectLoader.AddEffect<LifeBinderEnchantment.FlawlessChrysalisEffects>(player, base.Item) && thorium.TryFind<ModItem>("FlawlessChrysalis", ref fC))
			{
				fC.UpdateAccessory(player, hideVisual);
			}
			ModItem iridescentEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("IridescentEnchantment", ref iridescentEnchantment))
			{
				iridescentEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LifeBinderMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBinderBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBinderGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IridescentEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DewCollector>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FlawlessChrysalis>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SunrayStaff>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000128 RID: 296
		public class AloeLeafEffects : AccessoryEffect
		{
			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x00018E54 File Offset: 0x00017054
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LifeBinderEnchantment>();
				}
			}
		}

		// Token: 0x02000129 RID: 297
		public class FlawlessChrysalisEffects : AccessoryEffect
		{
			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600045D RID: 1117 RVA: 0x00018E54 File Offset: 0x00017054
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LifeBinderEnchantment>();
				}
			}
		}
	}
}
