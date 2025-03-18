using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Flesh;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Pets;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000050 RID: 80
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class FleshEnchantment : ModItem
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A910 File Offset: 0x00008B10
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			player.GetModPlayer<DLCPlayer>().demonBloodActive = true;
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<FleshEnchantment.FleshEffects>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).Symbiotic = true;
			}
			ModItem vmpireGlnd;
			if (AccessoryEffectLoader.AddEffect<FleshEnchantment.VampiricGlandEffects>(player, base.Item) && thorium.TryFind<ModItem>("VampireGland", ref vmpireGlnd))
			{
				vmpireGlnd.UpdateAccessory(player, hideVisual);
			}
			int minionType = ModContent.ProjectileType<BlisterPet>();
			if (AccessoryEffectLoader.AddEffect<FleshEnchantment.BlisterPetEffects>(player, base.Item))
			{
				if (player.ownedProjectileCounts[minionType] < 1)
				{
					Projectile.NewProjectile(player.GetSource_Accessory(base.Item, null), player.Center, Vector2.Zero, minionType, 20, 2f, Main.myPlayer, 0f, 0f, 0f);
					return;
				}
			}
			else
			{
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					if (Main.projectile[i].type == minionType && Main.projectile[i].active && player.miscEquips[0].type != ModContent.ItemType<BlisterSack>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AA38 File Offset: 0x00008C38
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FleshMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FleshBody>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FleshLegs>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VampireGland>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ToothOfTheConsumer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FleshMace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloodBelcher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StalkersSnippers>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloodTransfusion>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlisterSack>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000112 RID: 274
		public class FleshEffects : AccessoryEffect
		{
			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x00018DF2 File Offset: 0x00016FF2
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FleshEnchantment>();
				}
			}
		}

		// Token: 0x02000113 RID: 275
		public class BlisterPetEffects : AccessoryEffect
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x00018DF2 File Offset: 0x00016FF2
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FleshEnchantment>();
				}
			}
		}

		// Token: 0x02000114 RID: 276
		public class VampiricGlandEffects : AccessoryEffect
		{
			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x00018DF2 File Offset: 0x00016FF2
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<FleshEnchantment>();
				}
			}
		}
	}
}
