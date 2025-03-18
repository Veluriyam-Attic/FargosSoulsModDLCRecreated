using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005E RID: 94
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class LodestoneEnchantment : ModItem
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 150000;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000C03C File Offset: 0x0000A23C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				if (AccessoryEffectLoader.AddEffect<LodestoneEnchantment.LodestoneEffect>(player, base.Item))
				{
					thoriumPlayer.orbital = true;
					thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.05000000074505806, default(Vector2));
					int num = (int)(4f - (float)player.statLife / ((float)player.statLifeMax2 * 0.25f));
					thoriumPlayer.thoriumEndurance += (float)num * 0.06f;
					thoriumPlayer.lodestoneStage = num;
				}
			}
			ModItem aBM;
			if (mod.TryFind<ModItem>("AstroBeetleHusk", ref aBM))
			{
				aBM.UpdateAccessory(player, hideVisual);
			}
			ModItem obsidianScale;
			if (mod.TryFind<ModItem>("ObsidianScale", ref obsidianScale))
			{
				obsidianScale.UpdateAccessory(player, hideVisual);
			}
			ModItem hOS;
			if (mod.TryFind<ModItem>("HeartOfStone", ref hOS))
			{
				hOS.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000C12C File Offset: 0x0000A32C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneFaceGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneChestGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneShinGuards>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AstroBeetleHusk>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ObsidianScale>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HeartOfStone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheJuggernaut>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneClaymore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneBreaker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodeStoneStaff>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000123 RID: 291
		public class LodestoneEffect : AccessoryEffect
		{
			// Token: 0x170000DF RID: 223
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x00018E3F File Offset: 0x0001703F
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<LodestoneEnchantment>();
				}
			}
		}
	}
}
