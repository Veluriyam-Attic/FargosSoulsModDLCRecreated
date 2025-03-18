using System;
using SpiritMod;
using SpiritMod.Items.BossLoot.AvianDrops;
using SpiritMod.Items.BossLoot.AvianDrops.ApostleArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x0200007C RID: 124
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class ApostleEnchant : ModItem
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000EE07 File Offset: 0x0000D007
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetJumpState<ApostleJump>().Enable();
			SpiritUtils.GetSpiritPlayer(player).talonSet = true;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000EE20 File Offset: 0x0000D020
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<TalonHeaddress>(1);
			recipe.AddIngredient<TalonGarb>(1);
			recipe.AddIngredient<TalonPiercer>(1);
			recipe.AddIngredient<TalonBlade>(1);
			recipe.AddIngredient<Talonginus>(1);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
