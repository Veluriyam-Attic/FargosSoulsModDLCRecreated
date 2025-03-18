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
	// Token: 0x02000032 RID: 50
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SacredEnchantment : ModItem
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00006B30 File Offset: 0x00004D30
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006B94 File Offset: 0x00004D94
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).healBonus += SacredEnchantment.SetHealBonus;
			}
			ModItem karmicHolder;
			if (AccessoryEffectLoader.AddEffect<SacredEnchantment.KarmicHolderEffect>(player, base.Item) && thorium.TryFind<ModItem>("KarmicHolder", ref karmicHolder))
			{
				karmicHolder.UpdateAccessory(player, hideVisual);
			}
			ModItem sandWeaversTiara;
			if (AccessoryEffectLoader.AddEffect<SacredEnchantment.SandweaversTiaraEffect>(player, base.Item) && thorium.TryFind<ModItem>("SandweaversTiara", ref sandWeaversTiara))
			{
				sandWeaversTiara.UpdateAccessory(player, hideVisual);
			}
			ModItem noviceClericEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("NoviceClericEnchantment", ref noviceClericEnchantment))
			{
				noviceClericEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006C48 File Offset: 0x00004E48
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SacredHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SacredBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SacredLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SandweaversTiara>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NoviceClericEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<KarmicHolder>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Liberation>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400001D RID: 29
		public static readonly int SetHealBonus = 5;

		// Token: 0x020000D0 RID: 208
		public class KarmicHolderEffect : AccessoryEffect
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000352 RID: 850 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000353 RID: 851 RVA: 0x00018B9C File Offset: 0x00016D9C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SacredEnchantment>();
				}
			}
		}

		// Token: 0x020000D1 RID: 209
		public class SandweaversTiaraEffect : AccessoryEffect
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000355 RID: 853 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000356 RID: 854 RVA: 0x00018B9C File Offset: 0x00016D9C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SacredEnchantment>();
				}
			}
		}
	}
}
