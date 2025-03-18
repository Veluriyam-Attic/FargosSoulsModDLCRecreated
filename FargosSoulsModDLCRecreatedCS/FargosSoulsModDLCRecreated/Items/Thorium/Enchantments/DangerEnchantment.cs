using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Tracker;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000058 RID: 88
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DangerEnchantment : ModItem
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000B688 File Offset: 0x00009888
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B6EC File Offset: 0x000098EC
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				player.GetModPlayer<ThoriumPlayer>();
				player.buffImmune[44] = true;
				player.buffImmune[20] = true;
				player.buffImmune[24] = true;
				player.buffImmune[30] = true;
				player.buffImmune[70] = true;
			}
			ModItem nightShadeFlower;
			if (AccessoryEffectLoader.AddEffect<DangerEnchantment.NightShadeEffect>(player, base.Item) && thorium.TryFind<ModItem>("NightShadeFlower", ref nightShadeFlower))
			{
				nightShadeFlower.UpdateAccessory(player, hideVisual);
			}
			ModItem lS;
			if (thorium.TryFind<ModItem>("LeechingSheath", ref lS))
			{
				lS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B794 File Offset: 0x00009994
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DangerHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DangerMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DangerGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LeechingSheath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NightShadeFlower>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TrackersSkinningBlade>(), 1);
			recipe.AddIngredient(3285, 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200011E RID: 286
		public class NightShadeEffect : AccessoryEffect
		{
			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00018E23 File Offset: 0x00017023
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DangerEnchantment>();
				}
			}
		}
	}
}
