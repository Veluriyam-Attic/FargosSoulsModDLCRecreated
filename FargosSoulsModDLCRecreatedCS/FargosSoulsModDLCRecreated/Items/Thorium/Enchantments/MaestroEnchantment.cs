using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000051 RID: 81
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MaestroEnchantment : ModItem
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 8;
			base.Item.value = 200000;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AB44 File Offset: 0x00008D44
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			ModItem maestroWig;
			if (ModLoader.HasMod("ThoriumMod") && AccessoryEffectLoader.AddEffect<MaestroEnchantment.MaestroEffects>(player, base.Item) && thorium.TryFind<ModItem>("MaestroWig", ref maestroWig))
			{
				PlayerHelper.GetThoriumPlayer(player).setMaestro.Set(maestroWig.Item);
			}
			ModItem metronome;
			if (AccessoryEffectLoader.AddEffect<MaestroEnchantment.MetronomeEffect>(player, base.Item) && thorium.TryFind<ModItem>("Metronome", ref metronome))
			{
				metronome.UpdateAccessory(player, hideVisual);
			}
			ModItem conductorsBaton;
			if (thorium.TryFind<ModItem>("ConductorsBaton", ref conductorsBaton))
			{
				conductorsBaton.UpdateAccessory(player, hideVisual);
			}
			ModItem marchingBandEnchantment;
			if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("MarchingBandEnchantment", ref marchingBandEnchantment))
			{
				marchingBandEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AC08 File Offset: 0x00008E08
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MaestroWig>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MaestroSuit>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MaestroLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MarchingBandEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Metronome>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ConductorsBaton>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x02000115 RID: 277
		public class MetronomeEffect : AccessoryEffect
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x00018DF9 File Offset: 0x00016FF9
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MaestroEnchantment>();
				}
			}
		}

		// Token: 0x02000116 RID: 278
		public class MaestroEffects : AccessoryEffect
		{
			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x00018DF9 File Offset: 0x00016FF9
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<MaestroEnchantment>();
				}
			}
		}
	}
}
