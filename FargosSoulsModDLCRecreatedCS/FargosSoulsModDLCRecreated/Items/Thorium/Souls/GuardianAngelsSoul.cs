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
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Souls
{
	// Token: 0x02000018 RID: 24
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class GuardianAngelsSoul : ModItem
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000047D0 File Offset: 0x000029D0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			base.Item.value = 1000000;
			base.Item.rare = 11;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004820 File Offset: 0x00002A20
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				TooltipLine newTooltip = new TooltipLine(base.Mod, "TooltipCustom", "Effects of Elemental Bloom");
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

		// Token: 0x06000056 RID: 86 RVA: 0x00004904 File Offset: 0x00002B04
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			player.GetModPlayer<ThoriumPlayer>();
			*player.GetDamage((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 0.3f;
			*player.GetCritChance((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 20f;
			*player.GetAttackSpeed<HealerDamage>() += 0.2f;
			PlayerHelper.GetThoriumPlayer(player).healBonus += 5;
			ModItem supSas;
			if (thorium.TryFind<ModItem>("SupportSash", ref supSas))
			{
				supSas.UpdateAccessory(player, hideVisual);
			}
			ModItem savingGrace;
			if (thorium.TryFind<ModItem>("SavingGrace", ref savingGrace))
			{
				savingGrace.UpdateAccessory(player, hideVisual);
			}
			ModItem soulGuard;
			if (thorium.TryFind<ModItem>("SoulGuard", ref soulGuard))
			{
				soulGuard.UpdateAccessory(player, hideVisual);
			}
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				ModItem aDC;
				if (thorium.TryFind<ModItem>("ArchDemonCurse", ref aDC))
				{
					aDC.UpdateAccessory(player, hideVisual);
				}
				ModItem aH;
				if (thorium.TryFind<ModItem>("ArchangelHeart", ref aH))
				{
					aH.UpdateAccessory(player, hideVisual);
				}
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				ModItem eB;
				if (calamityBardHealer.TryFind<ModItem>("ElementalBloom", ref eB))
				{
					ModItem modItem = calamityBardHealer.Find<ModItem>("ElementalBloom");
					if (((modItem != null) ? modItem.Type : 0) > 0)
					{
						player.statLifeMax2 += 40;
					}
					player.statManaMax2 += 20;
					*player.GetAttackSpeed((DamageClass)ThoriumDamageBase<HealerTool>.Instance) += 0.15f;
					player.GetModPlayer<ThoriumPlayer>().healBonus += 2;
					player.GetModPlayer<ThoriumPlayer>().accHexingTalisman = true;
					ModPlayer thorlamityPlayer = calamityBardHealer.Find<ModPlayer>("ThorlamityPlayer");
					FieldInfo omniSpeakerField = thorlamityPlayer.GetType().GetField("elementalBloom", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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
					*player.GetDamage((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 0.2f;
					*player.GetAttackSpeed((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 0.1f;
					*player.GetCritChance((DamageClass)ThoriumDamageBase<HealerDamage>.Instance) += 12f;
				}
			}
			ModItem mB;
			if (thorium.TryFind<ModItem>("MedicalBag", ref mB))
			{
				mB.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004B68 File Offset: 0x00002D68
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<HealerEssence>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SupportSash>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SavingGrace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SoulGuard>(), 1);
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer != null)
				{
					ModItem modItem = calamityBardHealer.Find<ModItem>("ElementalBloom");
					int starScytheID = (modItem != null) ? modItem.Type : 0;
					if (starScytheID > 0)
					{
						recipe.AddIngredient(starScytheID, 1);
					}
				}
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<ArchDemonCurse>(), 1);
				recipe.AddIngredient(ModContent.ItemType<ArchangelHeart>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<MedicalBag>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TeslaDefibrillator>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MoonlightStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumHolyScythe>(), 1);
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				recipe.AddIngredient(ModContent.ItemType<TerraScythe>(), 1);
			}
			else
			{
				Mod calamityBardHealer2 = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer2 != null)
				{
					ModItem modItem2 = calamityBardHealer2.Find<ModItem>("MilkyWay");
					int screamOfTerrorID = (modItem2 != null) ? modItem2.Type : 0;
					if (screamOfTerrorID > 0)
					{
						recipe.AddIngredient(screamOfTerrorID, 1);
					}
				}
			}
			recipe.AddIngredient(ModContent.ItemType<PhoenixStaff>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShieldDroneBeacon>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeAndDeath>(), 1);
			if (ModLoader.HasMod("SpiritBardHealer"))
			{
				Mod spiritBardHealer = ModLoader.GetMod("SpiritBardHealer");
				if (spiritBardHealer != null)
				{
					ModItem modItem3 = spiritBardHealer.Find<ModItem>("DawnPhantom");
					int screamOfTerrorID2 = (modItem3 != null) ? modItem3.Type : 0;
					if (screamOfTerrorID2 > 0)
					{
						recipe.AddIngredient(screamOfTerrorID2, 1);
					}
				}
			}
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				Mod calamityBardHealer3 = ModLoader.GetMod("CalamityBardHealer");
				if (calamityBardHealer3 != null)
				{
					ModItem modItem4 = calamityBardHealer3.Find<ModItem>("WilloftheRagnarok");
					int screamOfTerrorID3 = (modItem4 != null) ? modItem4.Type : 0;
					ModItem modItem5 = calamityBardHealer3.Find<ModItem>("DeathAdder");
					int thornBlowID = (modItem5 != null) ? modItem5.Type : 0;
					ModItem modItem6 = calamityBardHealer3.Find<ModItem>("SARS");
					int starScytheID2 = (modItem6 != null) ? modItem6.Type : 0;
					if (screamOfTerrorID3 > 0)
					{
						recipe.AddIngredient(screamOfTerrorID3, 1);
					}
					if (thornBlowID > 0)
					{
						recipe.AddIngredient(thornBlowID, 1);
					}
					if (starScytheID2 > 0)
					{
						recipe.AddIngredient(starScytheID2, 1);
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
