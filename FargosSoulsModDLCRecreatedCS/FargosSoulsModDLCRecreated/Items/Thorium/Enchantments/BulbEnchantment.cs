using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000047 RID: 71
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BulbEnchantment : ModItem
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00009850 File Offset: 0x00007A50
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 2;
			base.Item.value = 60000;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000098B4 File Offset: 0x00007AB4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setBlooming = true;
			}
			ModItem bloomingShield;
			if (AccessoryEffectLoader.AddEffect<BulbEnchantment.BloomingShieldEffect>(player, base.Item) && thorium.TryFind<ModItem>("BloomingShield", ref bloomingShield))
			{
				bloomingShield.UpdateAccessory(player, hideVisual);
			}
			ModItem fragrantCorsage;
			if (AccessoryEffectLoader.AddEffect<BulbEnchantment.FragrantCorsageEffect>(player, base.Item) && thorium.TryFind<ModItem>("FragrantCorsage", ref fragrantCorsage))
			{
				fragrantCorsage.UpdateAccessory(player, hideVisual);
			}
			ModItem kickPetal;
			if (AccessoryEffectLoader.AddEffect<BulbEnchantment.BulbEffect>(player, base.Item) && thorium.TryFind<ModItem>("KickPetal", ref kickPetal))
			{
				kickPetal.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009960 File Offset: 0x00007B60
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BloomingCrown>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloomingTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloomingLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloomingShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FragrantCorsage>(), 1);
			recipe.AddIngredient(ModContent.ItemType<KickPetal>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000FC RID: 252
		public class BulbEffect : AccessoryEffect
		{
			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x00018D5B File Offset: 0x00016F5B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BulbEnchantment>();
				}
			}
		}

		// Token: 0x020000FD RID: 253
		public class FragrantCorsageEffect : AccessoryEffect
		{
			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x00018D5B File Offset: 0x00016F5B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BulbEnchantment>();
				}
			}
		}

		// Token: 0x020000FE RID: 254
		public class BloomingShieldEffect : AccessoryEffect
		{
			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060003DB RID: 987 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060003DC RID: 988 RVA: 0x00018D5B File Offset: 0x00016F5B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BulbEnchantment>();
				}
			}
		}
	}
}
