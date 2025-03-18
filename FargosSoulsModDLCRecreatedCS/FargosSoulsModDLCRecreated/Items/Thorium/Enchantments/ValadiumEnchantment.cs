using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Valadium;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200002D RID: 45
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ValadiumEnchantment : ModItem
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00005FD8 File Offset: 0x000041D8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 150000;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000603C File Offset: 0x0000423C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<ValadiumEnchantment.ValadiumEffects>(player, base.Item))
			{
				player.gravControl = true;
				if (player.gravDir == -1f)
				{
					player.AddBuff(ModContent.BuffType<ValadiumSetBuff>(), 60, true, false);
				}
			}
			ModItem mirrorOfTheBeholder;
			if (AccessoryEffectLoader.AddEffect<ValadiumEnchantment.MirrorOfTheBeholderEffects>(player, base.Item) && thorium.TryFind<ModItem>("MirroroftheBeholder", ref mirrorOfTheBeholder))
			{
				mirrorOfTheBeholder.UpdateAccessory(player, hideVisual);
			}
			ModItem uDBE;
			if (AccessoryEffectLoader.AddEffect<ValadiumEnchantment.UpDownBalloonEffects>(player, base.Item) && thorium.TryFind<ModItem>("UpDownBalloon", ref uDBE))
			{
				uDBE.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000060E8 File Offset: 0x000042E8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumBreastPlate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MirroroftheBeholder>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UpDownBalloon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GlacialSting>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Obliterator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumBow>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TommyGun>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CrystalBalloon>(), 300);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000C3 RID: 195
		public class ValadiumEffects : AccessoryEffect
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600032B RID: 811 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600032C RID: 812 RVA: 0x00018B61 File Offset: 0x00016D61
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ValadiumEnchantment>();
				}
			}
		}

		// Token: 0x020000C4 RID: 196
		public class MirrorOfTheBeholderEffects : AccessoryEffect
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600032E RID: 814 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600032F RID: 815 RVA: 0x00018B61 File Offset: 0x00016D61
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ValadiumEnchantment>();
				}
			}
		}

		// Token: 0x020000C5 RID: 197
		public class UpDownBalloonEffects : AccessoryEffect
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000331 RID: 817 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000332 RID: 818 RVA: 0x00018B61 File Offset: 0x00016D61
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ValadiumEnchantment>();
				}
			}
		}
	}
}
