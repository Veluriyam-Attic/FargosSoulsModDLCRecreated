using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000041 RID: 65
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class CrierEnchantment : ModItem
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00008B7C File Offset: 0x00006D7C
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 40000;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				thoriumPlayer.bardBuffDuration += (short)(CrierEnchantment.SetEmpowermentDuration * 60);
			}
			ModItem wR;
			if (mod.TryFind<ModItem>("WaxyRosin", ref wR))
			{
				wR.UpdateAccessory(player, hideVisual);
			}
			ModItem luckyRabbitsFoot;
			if (mod.TryFind<ModItem>("LuckyRabbitsFoot", ref luckyRabbitsFoot))
			{
				luckyRabbitsFoot.UpdateAccessory(player, hideVisual);
			}
			ModItem tB;
			if (mod.TryFind<ModItem>("TravelersBoots", ref tB))
			{
				tB.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008C70 File Offset: 0x00006E70
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CriersCap>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CriersSash>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CriersLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LuckyRabbitsFoot>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WoodenWhistle>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WaxyRosin>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TravelersBoots>(), 1);
			RecipeGroup group = new RecipeGroup(delegate()
			{
				LocalizedText localizedText = Lang.misc[37];
				return ((localizedText != null) ? localizedText.ToString() : null) + " Bugle Horn";
			}, new int[]
			{
				ModContent.ItemType<GoldBugleHorn>(),
				ModContent.ItemType<PlatinumBugleHorn>()
			});
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyBugleHorn", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyBugleHorn", 1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x0400002F RID: 47
		public static readonly int SetEmpowermentDuration = 3;
	}
}
