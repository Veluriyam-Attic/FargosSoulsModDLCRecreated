using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Dread;
using ThoriumMod.Items.SummonItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000067 RID: 103
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DreadEnchantment : ModItem
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000D048 File Offset: 0x0000B248
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 7;
			base.Item.value = 200000;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<DreadEnchantment.DreadEffect>(player, base.Item))
			{
				player.maxRunSpeed += 3f;
				player.runAcceleration += 0.08f;
				if (player.velocity.X > 0f || player.velocity.X < 0f)
				{
					*player.GetDamage(DamageClass.Melee) += 0.35f;
					*player.GetCritChance(DamageClass.Melee) += 26f;
					for (int i = 0; i < 2; i++)
					{
						int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
						int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
						Main.dust[num].noGravity = true;
						Main.dust[num2].noGravity = true;
						Main.dust[num].noLight = true;
						Main.dust[num2].noLight = true;
					}
				}
			}
			ModItem cFC;
			if (AccessoryEffectLoader.AddEffect<DreadEnchantment.CursedFlailCoreEffects>(player, base.Item) && thorium.TryFind<ModItem>("CursedFlailCore", ref cFC))
			{
				cFC.UpdateAccessory(player, hideVisual);
			}
			ModItem crashBoots;
			if (AccessoryEffectLoader.AddEffect<DreadEnchantment.CrashBootsEffects>(player, base.Item) && thorium.TryFind<ModItem>("CrashBoots", ref crashBoots))
			{
				crashBoots.UpdateAccessory(player, hideVisual);
			}
			ModItem dragonEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("DragonEnchantment", ref dragonEnchantment))
			{
				dragonEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DreadSkull>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadChestPlate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DragonEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CrashBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CursedFlailCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadFork>(), 1);
			recipe.AddIngredient(3012, 1);
			recipe.AddIngredient(ModContent.ItemType<VoidLance>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadDrill>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400003D RID: 61
		public static readonly int SetHealBonus = 5;

		// Token: 0x02000131 RID: 305
		public class DreadEffect : AccessoryEffect
		{
			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000474 RID: 1140 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x00018E77 File Offset: 0x00017077
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DreadEnchantment>();
				}
			}
		}

		// Token: 0x02000132 RID: 306
		public class CursedFlailCoreEffects : AccessoryEffect
		{
			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000477 RID: 1143 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000478 RID: 1144 RVA: 0x00018E77 File Offset: 0x00017077
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DreadEnchantment>();
				}
			}
		}

		// Token: 0x02000133 RID: 307
		public class CrashBootsEffects : AccessoryEffect
		{
			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600047A RID: 1146 RVA: 0x00018B20 File Offset: 0x00016D20
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<HelheimForceHeader>();
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x00018E77 File Offset: 0x00017077
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<DreadEnchantment>();
				}
			}
		}
	}
}
