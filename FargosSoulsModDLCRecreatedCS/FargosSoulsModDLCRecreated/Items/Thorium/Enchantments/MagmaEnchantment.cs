using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.Misc;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200004D RID: 77
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MagmaEnchantment : ModItem
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000A324 File Offset: 0x00008524
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A388 File Offset: 0x00008588
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				player.magmaStone = true;
				player.GetModPlayer<DLCPlayer>().setMagma = true;
			}
			ModItem springSteps;
			if (AccessoryEffectLoader.AddEffect<MagmaEnchantment.SpringStepsEffect>(player, base.Item) && thorium.TryFind<ModItem>("SpringSteps", ref springSteps))
			{
				springSteps.UpdateAccessory(player, hideVisual);
			}
			ModItem slagStompers;
			if (AccessoryEffectLoader.AddEffect<MagmaEnchantment.SlagStompersEffect>(player, base.Item) && thorium.TryFind<ModItem>("SlagStompers", ref slagStompers))
			{
				slagStompers.UpdateAccessory(player, hideVisual);
			}
			ModItem mST;
			if (thorium.TryFind<ModItem>("MoltenSpearTip", ref mST))
			{
				mST.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A430 File Offset: 0x00008630
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MagmaHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MagmaChestguard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MagmaGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SpringSteps>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlagStompers>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MoltenSpearTip>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HotChocolate>(), 5);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200010A RID: 266
		public class SpringStepsEffect : AccessoryEffect
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003FF RID: 1023 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000400 RID: 1024 RVA: 0x00018DB1 File Offset: 0x00016FB1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MagmaEnchantment>();
				}
			}
		}

		// Token: 0x0200010B RID: 267
		public class SlagStompersEffect : AccessoryEffect
		{
			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000402 RID: 1026 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x00018DB1 File Offset: 0x00016FB1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MagmaEnchantment>();
				}
			}
		}
	}
}
