using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000038 RID: 56
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class FallenPaladinEnchantment : ModItem
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00007C84 File Offset: 0x00005E84
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 200000;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).fallenPaladinSet = true;
			}
			ModItem prydwen;
			if (thorium.TryFind<ModItem>("Prydwen", ref prydwen))
			{
				prydwen.UpdateAccessory(player, hideVisual);
			}
			ModItem nirvanaStatuette;
			if (AccessoryEffectLoader.AddEffect<FallenPaladinEnchantment.NirvanaStatuetteEffects>(player, base.Item) && thorium.TryFind<ModItem>("NirvanaStatuette", ref nirvanaStatuette))
			{
				nirvanaStatuette.UpdateAccessory(player, hideVisual);
			}
			ModItem blastShield;
			if (thorium.TryFind<ModItem>("BlastShield", ref blastShield))
			{
				blastShield.UpdateAccessory(player, hideVisual);
			}
			ModItem mOTP;
			if (thorium.TryFind<ModItem>("MantleoftheProtector", ref mOTP))
			{
				mOTP.UpdateAccessory(player, hideVisual);
			}
			ModItem yP;
			if (AccessoryEffectLoader.AddEffect<FallenPaladinEnchantment.YumasPendantEffects>(player, base.Item) && thorium.TryFind<ModItem>("YumasPendant", ref yP))
			{
				yP.UpdateAccessory(player, hideVisual);
			}
			ModItem templarEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("TemplarEnchantment", ref templarEnchantment))
			{
				templarEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FallenPaladinFaceguard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FallenPaladinCuirass>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FallenPaladinGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TemplarEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlastShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MantleoftheProtector>(), 1);
			recipe.AddIngredient(ModContent.ItemType<YumasPendant>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Prydwen>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NirvanaStatuette>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000DF RID: 223
		public class NirvanaStatuetteEffects : AccessoryEffect
		{
			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600037A RID: 890 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600037B RID: 891 RVA: 0x00018C09 File Offset: 0x00016E09
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FallenPaladinEnchantment>();
				}
			}
		}

		// Token: 0x020000E0 RID: 224
		public class YumasPendantEffects : AccessoryEffect
		{
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x0600037D RID: 893 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600037E RID: 894 RVA: 0x00018C09 File Offset: 0x00016E09
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FallenPaladinEnchantment>();
				}
			}
		}
	}
}
