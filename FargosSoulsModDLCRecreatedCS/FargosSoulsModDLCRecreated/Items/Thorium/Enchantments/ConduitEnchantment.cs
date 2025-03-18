using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossStarScouter;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000039 RID: 57
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ConduitEnchantment : ModItem
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00007E70 File Offset: 0x00006070
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 250000;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007ED4 File Offset: 0x000060D4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			bool conduitEffect = AccessoryEffectLoader.AddEffect<ConduitEnchantment.ConduitShield>(player, base.Item);
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
				if (conduitEffect)
				{
					thoriumPlayer.conduitSet = true;
					thoriumPlayer.orbital = true;
					thoriumPlayer.orbitalRotation1 = Utils.RotatedBy(thoriumPlayer.orbitalRotation1, -0.10000000149011612, default(Vector2));
					Lighting.AddLight(player.Center, 0.2f, 0.35f, 0.7f);
					if (player.velocity.X != 0f && thoriumPlayer.circuitStage <= ConduitEnchantment.BubbleMax)
					{
						thoriumPlayer.circuitCharge++;
						int num = Dust.NewDust(player.position - player.velocity * 0.5f, player.width, player.height, 185, 0f, 0f, 100, default(Color), 1f);
						Main.dust[num].noGravity = true;
					}
				}
				ModItem jB;
				if (AccessoryEffectLoader.AddEffect<ConduitEnchantment.JetBootsEffect>(player, base.Item) && thorium.TryFind<ModItem>("JetBoots", ref jB))
				{
					jB.UpdateAccessory(player, hideVisual);
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000801C File Offset: 0x0000621C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<ConduitHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConduitSuit>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConduitLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JetBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<VegaPhaser>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LivewireCrasher>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ElectroRebounder>(), 300);
			recipe.AddIngredient(ModContent.ItemType<TheTriangle>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Turntable>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AncientSpark>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OmegaDrive>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x0400002D RID: 45
		public static readonly int BubbleMax = 5;

		// Token: 0x020000E1 RID: 225
		public class ConduitShield : AccessoryEffect
		{
			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000380 RID: 896 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000381 RID: 897 RVA: 0x00018C1C File Offset: 0x00016E1C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ConduitEnchantment>();
				}
			}
		}

		// Token: 0x020000E2 RID: 226
		public class JetBootsEffect : AccessoryEffect
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000383 RID: 899 RVA: 0x00018C10 File Offset: 0x00016E10
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<SvartalfheimForceHeader>();
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000384 RID: 900 RVA: 0x00018C1C File Offset: 0x00016E1C
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<ConduitEnchantment>();
				}
			}
		}
	}
}
