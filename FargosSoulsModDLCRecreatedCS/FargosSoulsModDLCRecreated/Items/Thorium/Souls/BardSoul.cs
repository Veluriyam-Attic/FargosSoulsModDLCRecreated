using System;
using System.Collections.Generic;
using System.Reflection;
using FargosSoulsModDLCRecreated.Items.Thorium.Essences;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Souls
{
	// Token: 0x02000019 RID: 25
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BardSoul : ModItem
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.value = 1000000;
			base.Item.rare = 11;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004E14 File Offset: 0x00003014
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				TooltipLine newTooltip = new TooltipLine(base.Mod, "TooltipCustom", "Effects of Omni-Speaker");
				int researchIndex = list.FindIndex((TooltipLine t) => t.Text.Contains("Research"));
				if (researchIndex != -1)
				{
					list.Insert(researchIndex, newTooltip);
				}
				else
				{
					list.Add(newTooltip);
				}
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(230, 248, 34));
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004EF8 File Offset: 0x000030F8
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			ThoriumPlayer modPlayer = player.GetModPlayer<ThoriumPlayer>();
			*player.GetDamage<BardDamage>() += 0.3f;
			*player.GetAttackSpeed<BardDamage>() += 0.2f;
			*player.GetCritChance((DamageClass)ThoriumDamageBase<BardDamage>.Instance) += 15f;
			modPlayer.bardResourceMax2 += 20;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				ModItem digTune;
				if (thorium.TryFind<ModItem>("DigitalTuner", ref digTune))
				{
					digTune.UpdateAccessory(player, hideVisual);
				}
			}
			else
			{
				*player.GetDamage((DamageClass)ThoriumDamageBase<BardDamage>.Instance) += 0.15f;
				*player.GetCritChance((DamageClass)ThoriumDamageBase<BardDamage>.Instance) += 15f;
				*player.GetAttackSpeed((DamageClass)ThoriumDamageBase<BardDamage>.Instance) += 0.15f;
				player.GetModPlayer<ThoriumPlayer>().accBrassMute2 = true;
				player.GetModPlayer<ThoriumPlayer>().accWindHoming = true;
				player.GetModPlayer<ThoriumPlayer>().bardBounceBonus += 3;
				if (ModLoader.HasMod("CalamityBardHealer"))
				{
					ModPlayer thorlamityPlayer = ModLoader.GetMod("CalamityBardHealer").Find<ModPlayer>("ThorlamityPlayer");
					FieldInfo omniSpeakerField = thorlamityPlayer.GetType().GetField("omniSpeaker", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (omniSpeakerField != null)
					{
						omniSpeakerField.SetValue(thorlamityPlayer, true);
					}
					else
					{
						Mod mod = ModLoader.GetMod("YourModName");
						if (mod != null)
						{
							mod.Logger.Warn("omniSpeaker field not found in ThorlamityPlayer.");
						}
					}
				}
				player.GetModPlayer<ThoriumPlayer>().bardRangeBoost += 750;
				ThoriumPlayer modPlayer2 = player.GetModPlayer<ThoriumPlayer>();
				modPlayer2.bardBuffDuration += 120;
				player.GetModPlayer<ThoriumPlayer>().accPercussionTuner2 = true;
			}
			ModItem epicMouthpiece;
			if (thorium.TryFind<ModItem>("EpicMouthpiece", ref epicMouthpiece))
			{
				epicMouthpiece.UpdateAccessory(player, hideVisual);
			}
			ModItem GPC;
			if (thorium.TryFind<ModItem>("GuitarPickClaw", ref GPC))
			{
				GPC.UpdateAccessory(player, hideVisual);
			}
			ModItem sM;
			if (thorium.TryFind<ModItem>("StraightMute", ref sM))
			{
				sM.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005104 File Offset: 0x00003304
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BardEssence>(), 1);
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				recipe.AddIngredient(ModContent.ItemType<DigitalTuner>(), 1);
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer != null)
				{
					ModItem modItem = calamityBardHealer.Find<ModItem>("OmniSpeaker");
					int starScytheID3 = (modItem != null) ? modItem.Type : 0;
					if (starScytheID3 > 0)
					{
						recipe.AddIngredient(starScytheID3, 1);
					}
				}
			}
			recipe.AddIngredient(ModContent.ItemType<EpicMouthpiece>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GuitarPickClaw>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StraightMute>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BandKit>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SteamFlute>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PrimeRoar>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SnowstormBanjo>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Fishbone>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SoundSageLament>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ChronoOcarina>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TheMaw>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SonicAmplifier>(), 1);
			if (ModLoader.HasMod("SpiritBardHealer"))
			{
				Mod spiritBardHealer = ModLoader.GetMod("SpiritBardHealer");
				if (spiritBardHealer != null)
				{
					ModItem modItem2 = spiritBardHealer.Find<ModItem>("DuskSerenade");
					int duskSerenadeID = (modItem2 != null) ? modItem2.Type : 0;
					if (duskSerenadeID > 0)
					{
						recipe.AddIngredient(duskSerenadeID, 1);
					}
				}
			}
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				Mod calamityBardHealer2 = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer2 != null)
				{
					ModItem modItem3 = calamityBardHealer2.Find<ModItem>("ChristmasCarol");
					int screamOfTerrorID = (modItem3 != null) ? modItem3.Type : 0;
					ModItem modItem4 = calamityBardHealer2.Find<ModItem>("DoomsdayCatharsis");
					int thornBlowID = (modItem4 != null) ? modItem4.Type : 0;
					ModItem modItem5 = calamityBardHealer2.Find<ModItem>("ArcticReinforcement");
					int starScytheID4 = (modItem5 != null) ? modItem5.Type : 0;
					if (screamOfTerrorID > 0)
					{
						recipe.AddIngredient(screamOfTerrorID, 1);
					}
					if (thornBlowID > 0)
					{
						recipe.AddIngredient(thornBlowID, 1);
					}
					if (starScytheID4 > 0)
					{
						recipe.AddIngredient(starScytheID4, 1);
					}
				}
			}
			if (ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
			}
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
