using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000062 RID: 98
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class WhiteDwarfEnchantment : ModItem
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000C77C File Offset: 0x0000A97C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 300000;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<WhiteDwarfEnchantment.WhiteDwarfEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setWhiteDwarf = true;
			}
			ModItem tGV3;
			if (mod.TryFind<ModItem>("ThrowingGuideVolume3", ref tGV3))
			{
				tGV3.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C840 File Offset: 0x0000AA40
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<WhiteDwarfMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThrowingGuideVolume3>(), 1);
			recipe.AddIngredient(ModContent.ItemType<EbonHammer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteDwarfPickaxe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AngelsEnd>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0200012A RID: 298
		public class WhiteDwarfEffects : AccessoryEffect
		{
			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600045F RID: 1119 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000460 RID: 1120 RVA: 0x00018E5B File Offset: 0x0001705B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<WhiteDwarfEnchantment>();
				}
			}
		}
	}
}
