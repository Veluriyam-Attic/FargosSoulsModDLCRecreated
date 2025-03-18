using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000055 RID: 85
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class NoviceClericEnchantment : ModItem
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000B278 File Offset: 0x00009478
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B2DC File Offset: 0x000094DC
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
				if (AccessoryEffectLoader.AddEffect<NoviceClericEnchantment.NoviceClericOrbit>(player, base.Item))
				{
					thoriumPlayer.clericSet = true;
					thoriumPlayer.orbital = true;
					thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.05000000074505806, default(Vector2));
				}
			}
			ModItem nursePurse;
			if (mod.TryFind<ModItem>("NursePurse", ref nursePurse))
			{
				nursePurse.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B368 File Offset: 0x00009568
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<NoviceClericCowl>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NoviceClericTabard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NoviceClericPants>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NursePurse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PalmCross>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Renew>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0200011B RID: 283
		public class NoviceClericOrbit : AccessoryEffect
		{
			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000432 RID: 1074 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x00018E15 File Offset: 0x00017015
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NoviceClericEnchantment>();
				}
			}
		}
	}
}
