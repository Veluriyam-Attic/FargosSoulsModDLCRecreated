using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Steel;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005B RID: 91
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DurasteelEnchantment : ModItem
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000BA80 File Offset: 0x00009C80
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).thoriumEndurance += 0.08f;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem ogreSandal;
			if (AccessoryEffectLoader.AddEffect<DurasteelEnchantment.OgreSandalEffects>(player, base.Item) && thorium.TryFind<ModItem>("OgreSandal", ref ogreSandal))
			{
				ogreSandal.UpdateAccessory(player, hideVisual);
			}
			ModItem crystalSpearTip;
			if (thorium.TryFind<ModItem>("CrystalSpearTip", ref crystalSpearTip))
			{
				crystalSpearTip.UpdateAccessory(player, hideVisual);
			}
			PlayerHelper.GetThoriumPlayer(player).ballnChain = true;
			ModItem darkSteelEnchantment;
			if (mod.TryFind<ModItem>("DarksteelEnchantment", ref darkSteelEnchantment))
			{
				darkSteelEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000BB94 File Offset: 0x00009D94
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DurasteelHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DurasteelChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DurasteelGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarksteelEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OgreSandal>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CrystalSpearTip>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DurasteelRepeater>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpudBomber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheSeaMine>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000037 RID: 55
		public static readonly int SetHealBonus = 5;

		// Token: 0x0200011F RID: 287
		public class OgreSandalEffects : AccessoryEffect
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x00018E2A File Offset: 0x0001702A
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DurasteelEnchantment>();
				}
			}
		}
	}
}
