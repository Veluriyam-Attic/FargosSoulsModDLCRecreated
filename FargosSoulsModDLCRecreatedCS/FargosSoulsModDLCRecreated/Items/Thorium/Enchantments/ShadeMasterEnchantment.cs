using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000034 RID: 52
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ShadeMasterEnchantment : ModItem
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00006E80 File Offset: 0x00005080
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006EE4 File Offset: 0x000050E4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<ShadeMasterEnchantment.ShadeMasterEffect>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setShade = true;
			}
			ModItem sS;
			if (AccessoryEffectLoader.AddEffect<ShadeMasterEnchantment.ShinobiSigilEffect>(player, base.Item) && thorium.TryFind<ModItem>("ShinobiSigil", ref sS))
			{
				sS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006F54 File Offset: 0x00005154
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ShadeMasterMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadeMasterGarb>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadeMasterTreads>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShinobiSigil>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ClockWorkBomb>(), 300);
			recipe.AddIngredient(ModContent.ItemType<BugenkaiShuriken>(), 300);
			recipe.AddIngredient(ModContent.ItemType<ShadeKunai>(), 300);
			recipe.AddIngredient(ModContent.ItemType<Soulslasher>(), 300);
			recipe.AddIngredient(ModContent.ItemType<LihzahrdKukri>(), 300);
			recipe.AddIngredient(ModContent.ItemType<TechniqueShadowDance>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000D4 RID: 212
		public class ShadeMasterEffect : AccessoryEffect
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600035E RID: 862 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600035F RID: 863 RVA: 0x00018BAA File Offset: 0x00016DAA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ShadeMasterEnchantment>();
				}
			}
		}

		// Token: 0x020000D5 RID: 213
		public class ShinobiSigilEffect : AccessoryEffect
		{
			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000361 RID: 865 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000362 RID: 866 RVA: 0x00018BAA File Offset: 0x00016DAA
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ShadeMasterEnchantment>();
				}
			}
		}
	}
}
