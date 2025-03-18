using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Forces
{
	// Token: 0x02000012 RID: 18
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class SvartalfheimForce : ModItem
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000353C File Offset: 0x0000173C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000035A0 File Offset: 0x000017A0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem graniteEnchantment;
			if (mod.TryFind<ModItem>("GraniteEnchantment", ref graniteEnchantment))
			{
				graniteEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem bronzeEnchantment;
			if (mod.TryFind<ModItem>("BronzeEnchantment", ref bronzeEnchantment))
			{
				bronzeEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem durasteelEnchantment;
			if (mod.TryFind<ModItem>("DurasteelEnchantment", ref durasteelEnchantment))
			{
				durasteelEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem titanEnchantment;
			if (mod.TryFind<ModItem>("TitanEnchantment", ref titanEnchantment))
			{
				titanEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem conduitEnchantment;
			if (mod.TryFind<ModItem>("ConduitEnchantment", ref conduitEnchantment))
			{
				conduitEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003638 File Offset: 0x00001838
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<GraniteEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BronzeEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DurasteelEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TitanEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConduitEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
