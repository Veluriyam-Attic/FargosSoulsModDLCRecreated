using System;
using System.Linq;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Painting;
using ThoriumMod.Projectiles.Pets;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200005F RID: 95
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DepthDiverEnchantment : ModItem
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 80000;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C238 File Offset: 0x0000A438
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<DLCPlayer>().depthDiverActive = true;
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			bool isEquipped = player.armor.Any((Item item) => item.type == base.Item.type);
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				player.AddBuff(ModContent.BuffType<DepthDiverAura>(), 30, false, false);
			}
			int minionType = ModContent.ProjectileType<SubterraneanAngler>();
			if (AccessoryEffectLoader.AddEffect<DepthDiverEnchantment.AnglerMinion>(player, base.Item))
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == ModContent.ProjectileType<SubterraneanAngler>() && Main.projectile[i].active && player.miscEquips[0].type != ModContent.ItemType<SubterraneanBulb>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem dD;
			if (thorium.TryFind<ModItem>("DrownedDoubloon", ref dD))
			{
				dD.UpdateAccessory(player, hideVisual);
			}
			ModItem oceanEnchantment;
			if (mod.TryFind<ModItem>("OceanEnchantment", ref oceanEnchantment))
			{
				oceanEnchantment.UpdateAccessory(player, hideVisual);
			}
			this.wasEquipped = isEquipped;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000C394 File Offset: 0x0000A594
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DepthDiverHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DepthDiverChestplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DepthDiverGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OceanEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MagicConch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GeyserStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DrownedDoubloon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SubterraneanBulb>(), 1);
			recipe.AddIngredient(ModContent.ItemType<QueensGlowstick>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AquaticParadisePaint>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400003A RID: 58
		public static readonly int SetHealBonus = 5;

		// Token: 0x0400003B RID: 59
		public bool wasEquipped;

		// Token: 0x02000124 RID: 292
		public class AnglerMinion : AccessoryEffect
		{
			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x00018E46 File Offset: 0x00017046
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DepthDiverEnchantment>();
				}
			}
		}
	}
}
