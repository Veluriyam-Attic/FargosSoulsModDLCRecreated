using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.BossStarScouter;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.Tracker;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000033 RID: 51
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MalignantEnchantment : ModItem
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006D34 File Offset: 0x00004F34
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			ModLoader.HasMod("ThoriumMod");
			ModItem artRocket;
			if (AccessoryEffectLoader.AddEffect<MalignantEnchantment.ArtificersRocketeersEffects>(player, base.Item) && thorium.TryFind<ModItem>("ArtificersRocketeers", ref artRocket))
			{
				artRocket.UpdateAccessory(player, hideVisual);
			}
			ModItem artShield;
			if (AccessoryEffectLoader.AddEffect<MalignantEnchantment.ArtificersShieldEffects>(player, base.Item) && thorium.TryFind<ModItem>("ArtificersShield", ref artShield))
			{
				artShield.UpdateAccessory(player, hideVisual);
			}
			ModItem silkEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("SilkEnchantment", ref silkEnchantment))
			{
				silkEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MalignantCap>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MalignantRobe>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SilkEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ArtificersRocketeers>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ArtificersShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JellyPondWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkTome>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChampionBomberStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GaussFlinger>(), 1);
			recipe.AddIngredient(1995, 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400001E RID: 30
		public static readonly int SetHealBonus = 5;

		// Token: 0x020000D2 RID: 210
		public class ArtificersRocketeersEffects : AccessoryEffect
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000358 RID: 856 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000359 RID: 857 RVA: 0x00018BA3 File Offset: 0x00016DA3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MalignantEnchantment>();
				}
			}
		}

		// Token: 0x020000D3 RID: 211
		public class ArtificersShieldEffects : AccessoryEffect
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600035B RID: 859 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x0600035C RID: 860 RVA: 0x00018BA3 File Offset: 0x00016DA3
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MalignantEnchantment>();
				}
			}
		}
	}
}
