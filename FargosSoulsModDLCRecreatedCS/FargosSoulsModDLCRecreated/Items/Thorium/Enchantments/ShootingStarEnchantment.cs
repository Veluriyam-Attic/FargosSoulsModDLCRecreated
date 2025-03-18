using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000059 RID: 89
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ShootingStarEnchantment : ModItem
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000B814 File Offset: 0x00009A14
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 250000;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B878 File Offset: 0x00009A78
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				thoriumPlayer.bardBuffDuration += (short)(ShootingStarEnchantment.SetEmpowermentDuration * 60);
				thoriumPlayer.setShootingStar = true;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ShootingStarHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShootingStarShirt>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShootingStarBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Headset>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AcousticGuitar>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SunflareGuitar>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x04000034 RID: 52
		public static readonly int SetEmpowermentDuration = 6;

		// Token: 0x04000035 RID: 53
		public static readonly int SetSymphonicDamageBonus = 5;

		// Token: 0x04000036 RID: 54
		public static readonly int SetInspirationRegenBonus = 2;
	}
}
