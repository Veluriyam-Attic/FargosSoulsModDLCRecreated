using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000040 RID: 64
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class PlagueDoctorEnchantment : ModItem
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000089E4 File Offset: 0x00006BE4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008A48 File Offset: 0x00006C48
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setPlague = true;
			}
			ModItem hB;
			if (AccessoryEffectLoader.AddEffect<PlagueDoctorEnchantment.HungeringBlossomEffects>(player, base.Item) && thorium.TryFind<ModItem>("HungeringBlossom", ref hB))
			{
				hB.UpdateAccessory(player, hideVisual);
			}
			ModItem mC;
			if (thorium.TryFind<ModItem>("MonsterCharm", ref mC))
			{
				mC.UpdateAccessory(player, hideVisual);
			}
			ModItem nS;
			if (thorium.TryFind<ModItem>("NecroticSkull", ref nS))
			{
				nS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsGarb>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HungeringBlossom>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MonsterCharm>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NecroticSkull>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GasContainer>(), 300);
			recipe.AddIngredient(ModContent.ItemType<CombustionFlask>(), 300);
			recipe.AddIngredient(ModContent.ItemType<NitrogenVial>(), 300);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000EC RID: 236
		public class HungeringBlossomEffects : AccessoryEffect
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x00018C59 File Offset: 0x00016E59
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<PlagueDoctorEnchantment>();
				}
			}
		}
	}
}
