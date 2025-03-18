using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.ThrownItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x0200003A RID: 58
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SandstoneEnchantment : ModItem
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000080DC File Offset: 0x000062DC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000813D File Offset: 0x0000633D
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<SandstoneEnchantment.SandstoneEffect>(player, base.Item))
			{
				player.GetJumpState<SandstormInABottleJump>().Enable();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000817C File Offset: 0x0000637C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SandStoneHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SandStoneMail>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SandStoneGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StoneThrowingSpear>(), 300);
			recipe.AddIngredient(ModContent.ItemType<Scorpain>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TalonBurst>(), 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x020000E3 RID: 227
		public class SandstoneEffect : AccessoryEffect
		{
			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000386 RID: 902 RVA: 0x00018C23 File Offset: 0x00016E23
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MuspelheimForceHeader>();
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000387 RID: 903 RVA: 0x00018C2F File Offset: 0x00016E2F
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<SandstoneEnchantment>();
				}
			}
		}
	}
}
