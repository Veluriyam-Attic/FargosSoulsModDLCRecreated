using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002E RID: 46
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class OrnateEnchantment : ModItem
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000061A0 File Offset: 0x000043A0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006204 File Offset: 0x00004404
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<OrnateEnchantment.OrnateEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).setOrnate = true;
			}
			ModItem concertTickets;
			if (AccessoryEffectLoader.AddEffect<OrnateEnchantment.ConcertTicketsEffect>(player, base.Item) && thorium.TryFind<ModItem>("ConcertTickets", ref concertTickets))
			{
				concertTickets.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006274 File Offset: 0x00004474
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<OrnateHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OrnateJerkin>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OrnateLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConcertTickets>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OrichalcumSlideWhistle>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheGreenTambourine>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000C6 RID: 198
		public class OrnateEffects : AccessoryEffect
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000334 RID: 820 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000335 RID: 821 RVA: 0x00018B74 File Offset: 0x00016D74
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OrnateEnchantment>();
				}
			}
		}

		// Token: 0x020000C7 RID: 199
		public class ConcertTicketsEffect : AccessoryEffect
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000337 RID: 823 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x06000338 RID: 824 RVA: 0x00018B74 File Offset: 0x00016D74
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<OrnateEnchantment>();
				}
			}
		}
	}
}
