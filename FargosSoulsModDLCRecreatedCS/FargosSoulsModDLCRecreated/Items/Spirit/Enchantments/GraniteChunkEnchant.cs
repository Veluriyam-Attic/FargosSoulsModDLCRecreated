using System;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Accessory.ShurikenLauncher;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.GraniteSet.GraniteArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000079 RID: 121
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class GraniteChunkEnchant : ModItem
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteHelm").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteChest").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "GraniteLegs").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "TechBoots").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "ShurikenLauncher").UpdateAccessory(player, false);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<GraniteHelm>(1);
			recipe.AddIngredient<GraniteChest>(1);
			recipe.AddIngredient<GraniteLegs>(1);
			recipe.AddIngredient<ShurikenLauncher>(1);
			recipe.AddIngredient<RageBlazeDecapitator>(1);
			recipe.AddIngredient<TechBoots>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400004A RID: 74
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
