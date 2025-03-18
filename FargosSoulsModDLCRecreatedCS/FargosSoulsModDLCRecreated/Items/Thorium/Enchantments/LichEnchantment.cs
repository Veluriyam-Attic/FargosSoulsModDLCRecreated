using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000036 RID: 54
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class LichEnchantment : ModItem
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000078D0 File Offset: 0x00005AD0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 6;
			base.Item.value = 200000;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007934 File Offset: 0x00005B34
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<LichEnchantment.LichEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setLich = true;
			}
			ModItem phylactery;
			if (AccessoryEffectLoader.AddEffect<LichEnchantment.PhylacteryEffects>(player, base.Item) && thorium.TryFind<ModItem>("Phylactery", ref phylactery))
			{
				phylactery.UpdateAccessory(player, hideVisual);
			}
			ModItem cOTS;
			if (thorium.TryFind<ModItem>("CapeoftheSurvivor", ref cOTS))
			{
				cOTS.UpdateAccessory(player, hideVisual);
			}
			ModItem gG;
			if (thorium.TryFind<ModItem>("GreedyGoblet", ref gG))
			{
				gG.UpdateAccessory(player, hideVisual);
			}
			ModItem piratesPurse;
			if (thorium.TryFind<ModItem>("PiratesPurse", ref piratesPurse))
			{
				piratesPurse.UpdateAccessory(player, hideVisual);
			}
			ModItem pA;
			if (AccessoryEffectLoader.AddEffect<LichEnchantment.ProofOfAvariceEffects>(player, base.Item) && thorium.TryFind<ModItem>("ProofAvarice", ref pA))
			{
				pA.UpdateAccessory(player, hideVisual);
			}
			ModItem plagueDoctorEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("PlagueDoctorEnchantment", ref plagueDoctorEnchantment))
			{
				plagueDoctorEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007A34 File Offset: 0x00005C34
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LichCowl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LichCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LichTalon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PlagueDoctorEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Phylactery>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CapeoftheSurvivor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GreedyGoblet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PiratesPurse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ProofAvarice>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SoulCleaver>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400002C RID: 44
		public static readonly int SetHealBonus = 5;

		// Token: 0x020000DB RID: 219
		public class LichEffects : AccessoryEffect
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600036E RID: 878 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600036F RID: 879 RVA: 0x00018BEF File Offset: 0x00016DEF
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LichEnchantment>();
				}
			}
		}

		// Token: 0x020000DC RID: 220
		public class PhylacteryEffects : AccessoryEffect
		{
			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000371 RID: 881 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000372 RID: 882 RVA: 0x00018BEF File Offset: 0x00016DEF
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LichEnchantment>();
				}
			}
		}

		// Token: 0x020000DD RID: 221
		public class ProofOfAvariceEffects : AccessoryEffect
		{
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000374 RID: 884 RVA: 0x00018BE3 File Offset: 0x00016DE3
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<VanaheimForceHeader>();
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000375 RID: 885 RVA: 0x00018BEF File Offset: 0x00016DEF
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LichEnchantment>();
				}
			}
		}
	}
}
