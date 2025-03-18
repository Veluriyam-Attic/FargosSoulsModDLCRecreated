using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.EarlyMagic;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Icy;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002B RID: 43
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SilkEnchantment : ModItem
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00005BDC File Offset: 0x00003DDC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 0;
			base.Item.value = 20000;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005C40 File Offset: 0x00003E40
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<SilkEnchantment.SilkEffects>(player, base.Item))
			{
				if ((float)player.statMana / (float)player.statManaMax2 >= 0.9f)
				{
					PlayerHelper.GetThoriumPlayer(player).setSilk = true;
					*player.GetDamage(DamageClass.Magic) += 0.12f;
				}
				else
				{
					PlayerHelper.GetThoriumPlayer(player).setSilk = false;
				}
			}
			ModItem artificersFocus;
			if (AccessoryEffectLoader.AddEffect<SilkEnchantment.ArtificersFocusEffect>(player, base.Item) && thorium.TryFind<ModItem>("ArtificersFocus", ref artificersFocus))
			{
				artificersFocus.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SilkHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SilkTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SilkLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ArtificersFocus>(), 1);
			recipe.AddIngredient(3069, 1);
			recipe.AddIngredient(ModContent.ItemType<IceCube>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WindGust>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Cure>(), 1);
			recipe.AddIngredient(1997, 1);
			recipe.AddIngredient(3079, 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000BF RID: 191
		public class ArtificersFocusEffect : AccessoryEffect
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600031F RID: 799 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000320 RID: 800 RVA: 0x00018B3B File Offset: 0x00016D3B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilkEnchantment>();
				}
			}
		}

		// Token: 0x020000C0 RID: 192
		public class SilkEffects : AccessoryEffect
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000322 RID: 802 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000323 RID: 803 RVA: 0x00018B3B File Offset: 0x00016D3B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SilkEnchantment>();
				}
			}
		}
	}
}
