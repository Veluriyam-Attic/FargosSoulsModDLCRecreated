using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000052 RID: 82
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MarchingBandEnchantment : ModItem
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				if (AccessoryEffectLoader.AddEffect<MarchingBandEnchantment.MarchingBandEffect>(player, base.Item))
				{
					ModItem marchingBandCap;
					if (thorium.TryFind<ModItem>("MarchingBandShako", ref marchingBandCap))
					{
						PlayerHelper.GetThoriumPlayer(player).setMarchingBand.Set(marchingBandCap.Item);
					}
					thoriumPlayer.needsOutOfCombatSync = true;
				}
			}
			ModItem fullScore;
			if (thorium.TryFind<ModItem>("FullScore", ref fullScore))
			{
				fullScore.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000AD6C File Offset: 0x00008F6C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MarchingBandShako>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MarchingBandUniform>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MarchingBandLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FullScore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FrostwindCymbals>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadowflameWarhorn>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000117 RID: 279
		public class MarchingBandEffect : AccessoryEffect
		{
			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x00018E00 File Offset: 0x00017000
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MarchingBandEnchantment>();
				}
			}
		}
	}
}
