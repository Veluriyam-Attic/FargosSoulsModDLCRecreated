using System;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.TideDrops.StreamSurfer;
using SpiritMod.Items.Sets.TideDrops.Whirltide;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000086 RID: 134
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class StreamSurferEnchant : ModItem
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 3;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000F700 File Offset: 0x0000D900
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ModContent.Find<ModItem>(this.SpiritMod.Name, "Flying_Fish_Fin").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferHelmet").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferChestplate").UpdateArmorSet(player);
			ModContent.Find<ModItem>(this.SpiritMod.Name, "StreamSurferLeggings").UpdateArmorSet(player);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F77C File Offset: 0x0000D97C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<StreamSurferHelmet>(1);
			recipe.AddIngredient<StreamSurferChestplate>(1);
			recipe.AddIngredient<StreamSurferLeggings>(1);
			recipe.AddIngredient<Whirltide>(1);
			recipe.AddIngredient<BassSlapper>(1);
			recipe.AddIngredient<Flying_Fish_Fin>(1);
			recipe.AddTile(26);
			recipe.Register();
		}

		// Token: 0x04000056 RID: 86
		private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
	}
}
