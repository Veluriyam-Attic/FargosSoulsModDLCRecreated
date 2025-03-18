using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003E RID: 62
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class NobleEnchantment : ModItem
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000086BC File Offset: 0x000068BC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008720 File Offset: 0x00006920
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setNoble = true;
			}
			ModItem ringofUnity;
			if (thorium.TryFind<ModItem>("RingofUnity", ref ringofUnity))
			{
				ringofUnity.UpdateAccessory(player, hideVisual);
			}
			ModItem brassCap;
			if (thorium.TryFind<ModItem>("BrassCap", ref brassCap))
			{
				brassCap.UpdateAccessory(player, hideVisual);
			}
			ModItem waxyRosin;
			if (thorium.TryFind<ModItem>("WaxyRosin", ref waxyRosin))
			{
				waxyRosin.UpdateAccessory(player, hideVisual);
			}
			ModItem mT;
			if (AccessoryEffectLoader.AddEffect<NobleEnchantment.MixtapeEffects>(player, base.Item) && thorium.TryFind<ModItem>("MixTape", ref mT))
			{
				mT.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000087C8 File Offset: 0x000069C8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<NoblesHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NoblesJerkin>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NoblesLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RingofUnity>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BrassCap>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WaxyRosin>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MixTape>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000EA RID: 234
		public class MixtapeEffects : AccessoryEffect
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600039B RID: 923 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600039C RID: 924 RVA: 0x00018C4B File Offset: 0x00016E4B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NobleEnchantment>();
				}
			}
		}
	}
}
