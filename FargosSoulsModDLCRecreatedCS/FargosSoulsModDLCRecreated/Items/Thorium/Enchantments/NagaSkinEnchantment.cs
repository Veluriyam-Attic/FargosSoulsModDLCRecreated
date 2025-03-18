using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossMini;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000043 RID: 67
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class NagaSkinEnchantment : ModItem
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00008F28 File Offset: 0x00007128
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 5;
			base.Item.value = 150000;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008F8C File Offset: 0x0000718C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<NagaSkinEnchantment.NagaSkinEffect>(player, base.Item))
			{
				PlayerHelper.GetThoriumPlayer(player).nagaManaEffect = true;
			}
			ModItem oR;
			if (AccessoryEffectLoader.AddEffect<NagaSkinEnchantment.OceanRetaliationEffect>(player, base.Item) && thorium.TryFind<ModItem>("OceanRetaliation", ref oR))
			{
				oR.UpdateAccessory(player, hideVisual);
			}
			ModItem sB;
			if (thorium.TryFind<ModItem>("SurvivalistBoots", ref sB))
			{
				sB.UpdateAccessory(player, hideVisual);
			}
			ModItem lT;
			if (AccessoryEffectLoader.AddEffect<NagaSkinEnchantment.LizhardTailEffect>(player, base.Item) && thorium.TryFind<ModItem>("LizahrdTail", ref lT))
			{
				lT.UpdateAccessory(player, hideVisual);
			}
			ModItem sS;
			if (AccessoryEffectLoader.AddEffect<NagaSkinEnchantment.SerpentShieldEffect>(player, base.Item) && thorium.TryFind<ModItem>("SerpentShield", ref sS))
			{
				sS.UpdateAccessory(player, hideVisual);
			}
			ModItem tSS;
			if (thorium.TryFind<ModItem>("TitanSlayerSheath", ref tSS))
			{
				tSS.UpdateAccessory(player, hideVisual);
			}
			ModItem mC;
			if (thorium.TryFind<ModItem>("MermaidCanteen", ref mC))
			{
				mC.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00009090 File Offset: 0x00007290
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<NagaSkinMask>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NagaSkinSuit>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NagaSkinTail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OceanRetaliation>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SurvivalistBoots>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LihzahrdTail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SerpentShield>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitanSlayerSheath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MermaidCanteen>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Eelrod>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SerpentsBubbleWand>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HydromancerCatalyst>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Leviathan>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OldGodsVision>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UnderseaBountyPaint>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x020000F1 RID: 241
		public class NagaSkinEffect : AccessoryEffect
		{
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060003B1 RID: 945 RVA: 0x00018C93 File Offset: 0x00016E93
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NagaSkinEnchantment>();
				}
			}
		}

		// Token: 0x020000F2 RID: 242
		public class OceanRetaliationEffect : AccessoryEffect
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x00018C93 File Offset: 0x00016E93
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NagaSkinEnchantment>();
				}
			}
		}

		// Token: 0x020000F3 RID: 243
		public class LizhardTailEffect : AccessoryEffect
		{
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060003B7 RID: 951 RVA: 0x00018C93 File Offset: 0x00016E93
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NagaSkinEnchantment>();
				}
			}
		}

		// Token: 0x020000F4 RID: 244
		public class SerpentShieldEffect : AccessoryEffect
		{
			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00018BF6 File Offset: 0x00016DF6
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<JotunheimForceHeader>();
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00018C93 File Offset: 0x00016E93
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<NagaSkinEnchantment>();
				}
			}
		}
	}
}
