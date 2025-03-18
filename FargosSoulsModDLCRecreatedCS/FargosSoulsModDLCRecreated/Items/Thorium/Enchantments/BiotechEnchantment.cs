using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200006A RID: 106
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BiotechEnchantment : ModItem
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 6;
			base.Item.value = 150000;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			bool biotechEffectApplied = AccessoryEffectLoader.AddEffect<BiotechEnchantment.BiotechEffect>(player, base.Item);
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).bioTechSet = true;
				if (Main.myPlayer == player.whoAmI)
				{
					if (biotechEffectApplied)
					{
						int num = ModContent.ProjectileType<BiotechProbe>();
						if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[num] < 1)
						{
							Projectile.NewProjectile(player.GetSource_FromThis("bioTechSet"), player.Center, Vector2.Zero, num, 0, 0f, player.whoAmI, 0f, 0f, 0f);
						}
					}
					else
					{
						for (int i = 0; i < Main.maxProjectiles; i++)
						{
							if (Main.projectile[i].type == ModContent.ProjectileType<BiotechProbe>() && Main.projectile[i].active)
							{
								Main.projectile[i].Kill();
							}
						}
					}
				}
			}
			ModItem hSD;
			if (thorium.TryFind<ModItem>("HightechSonarDevice", ref hSD))
			{
				hSD.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D944 File Offset: 0x0000BB44
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BioTechHood>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BioTechGarment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BioTechLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HightechSonarDevice>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeEssenceApparatus>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NullZoneStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BarrierGenerator>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0200013A RID: 314
		public class BiotechEffect : AccessoryEffect
		{
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x00018B90 File Offset: 0x00016D90
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<AlfheimForceHeader>();
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x00018EB8 File Offset: 0x000170B8
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BiotechEnchantment>();
				}
			}
		}
	}
}
