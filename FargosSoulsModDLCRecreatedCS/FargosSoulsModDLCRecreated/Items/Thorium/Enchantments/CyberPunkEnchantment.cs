using System;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000069 RID: 105
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class CyberPunkEnchantment : ModItem
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 6;
			base.Item.value = 150000;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000D614 File Offset: 0x0000B814
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
				if (AccessoryEffectLoader.AddEffect<CyberPunkEnchantment.CyberPunkEffect>(player, base.Item))
				{
					thoriumPlayer.setCyberPunk = true;
					string oldSetBonus = player.setBonus;
					ModItem cyberPunkHelmet;
					if (thorium.TryFind<ModItem>("CyberPunkHeadset", ref cyberPunkHelmet))
					{
						cyberPunkHelmet.UpdateArmorSet(player);
					}
					player.setBonus = oldSetBonus;
				}
			}
			ModItem autoTuner;
			if (thorium.TryFind<ModItem>("AutoTuner", ref autoTuner))
			{
				autoTuner.UpdateAccessory(player, hideVisual);
			}
			ModItem tunePlayerDamage;
			if (thorium.TryFind<ModItem>("TunePlayerDamage", ref tunePlayerDamage))
			{
				tunePlayerDamage.UpdateAccessory(player, hideVisual);
			}
			ModItem dissTrack;
			if (AccessoryEffectLoader.AddEffect<CyberPunkEnchantment.DissTrackEffect>(player, base.Item) && thorium.TryFind<ModItem>("DissTrack", ref dissTrack))
			{
				dissTrack.UpdateAccessory(player, hideVisual);
			}
			ModItem jS;
			if (thorium.TryFind<ModItem>("JetstreamSheath", ref jS))
			{
				jS.UpdateAccessory(player, hideVisual);
			}
			ModItem sW;
			if (thorium.TryFind<ModItem>("SteamkeeperWatch", ref sW))
			{
				sW.UpdateAccessory(player, hideVisual);
			}
			ModItem mG;
			if (thorium.TryFind<ModItem>("MagnetoGrip", ref mG))
			{
				mG.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000D728 File Offset: 0x0000B928
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CyberPunkHeadset>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CyberPunkSuit>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CyberPunkLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AutoTuner>(), 1);
			recipe.AddIngredient(ModContent.ItemType<JetstreamSheath>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteamkeeperWatch>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MagnetoGrip>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TunePlayerDamage>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DissTrack>(), 1);
			recipe.AddTile(125);
			recipe.Register();
		}

		// Token: 0x04000042 RID: 66
		public static readonly int SetEmpowermentLevel = 2;

		// Token: 0x02000138 RID: 312
		public class CyberPunkEffect : AccessoryEffect
		{
			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000489 RID: 1161 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x0600048A RID: 1162 RVA: 0x00018EB1 File Offset: 0x000170B1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<CyberPunkEnchantment>();
				}
			}
		}

		// Token: 0x02000139 RID: 313
		public class DissTrackEffect : AccessoryEffect
		{
			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x00018B68 File Offset: 0x00016D68
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<NiflheimForceHeader>();
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600048D RID: 1165 RVA: 0x00018EB1 File Offset: 0x000170B1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<CyberPunkEnchantment>();
				}
			}
		}
	}
}
