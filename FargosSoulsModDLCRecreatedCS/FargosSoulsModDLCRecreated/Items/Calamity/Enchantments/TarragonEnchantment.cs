using System;
using System.Collections.Generic;
using System.Reflection;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Weapons.Melee;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x02000097 RID: 151
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class TarragonEnchantment : ModItem
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00012790 File Offset: 0x00010990
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 3000000;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000127F4 File Offset: 0x000109F4
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "Braelor's undying might flows through you..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Reduces enemy spawn rates"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Increased heart pickup range"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Taking damage gives the Tarra Life buff, which grants +1.5 HP/s life regen"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Press Y to cloak yourself in life energy that heavily reduces contact damage for 10 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "This has a 30 second cooldown"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "Enemies have a chance to drop extra hearts on death"));
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "Ranged projectiles split into homing life energy and leaves on death"));
			list.Add(new TooltipLine(base.Mod, "Tooltip9", "On every 5th critical strike you will fire a leaf storm"));
			list.Add(new TooltipLine(base.Mod, "Tooltip10", "Magic projectiles heal you on enemy hits"));
			list.Add(new TooltipLine(base.Mod, "Tooltip11", "Amount healed is based on projectile damage"));
			list.Add(new TooltipLine(base.Mod, "Tooltip12", "Summons a life aura around you that damages nearby enemies"));
			list.Add(new TooltipLine(base.Mod, "Tooltip13", "After every 50 rogue critical hits you will gain 2.5 seconds of damage immunity"));
			list.Add(new TooltipLine(base.Mod, "Tooltip14", "This effect has a cooldown of 25 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip15", "While under effects of a debuff you gain 10% increased rogue damage"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip151", "Occasionally emit a healing pulse that heals and shields allies by 8 points"));
				list.Add(new TooltipLine(base.Mod, "Tooltip152", "You spawn quarter rests around you that heal inspiration upon hitting enemies"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip16", "Effects of Blazing Core and Dark Sun Ring"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip16", "Effects of Blazing Core, Warbanner of the Sun, and Dark Sun Ring"));
			}
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(169, 106, 52));
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00012A94 File Offset: 0x00010C94
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod") && AccessoryEffectLoader.AddEffect<TarragonEnchantment.TarragonEffects>(player, base.Item))
			{
				modPlayer.tarraSet = true;
				modPlayer.tarraMage = true;
				modPlayer.tarraMelee = true;
				modPlayer.tarraRanged = true;
				modPlayer.tarraThrowing = true;
				modPlayer.tarraSummon = true;
				if (ModLoader.HasMod("CalamityBardHealer") && Main.myPlayer == player.whoAmI)
				{
					Mod calamityMod = ModLoader.GetMod("CalamityBardHealer");
					ModProjectile modProj;
					if (calamityMod != null && calamityMod.TryFind<ModProjectile>("TarragonQuarterRest", ref modProj))
					{
						ModPlayer thorlamityPlayer = player.GetModPlayer<ModPlayer>(calamityMod.Find<ModPlayer>("ThorlamityPlayer"));
						if (thorlamityPlayer == null)
						{
							return;
						}
						FieldInfo tarragonBeatField = thorlamityPlayer.GetType().GetField("tarragonBeat", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (tarragonBeatField == null)
						{
							return;
						}
						object value = tarragonBeatField.GetValue(thorlamityPlayer);
						if (!(value is int))
						{
							Main.NewText("Debug: tarragonBeat is NOT an int!", new Color?(Color.Red));
							return;
						}
						int tarragonBeat = (int)value;
						tarragonBeat++;
						tarragonBeatField.SetValue(thorlamityPlayer, tarragonBeat);
						if (tarragonBeat > 60)
						{
							Vector2 spawnPosition = player.MountedCenter + Utils.NextVector2Circular(Main.rand, 160f, 160f);
							int proj = Projectile.NewProjectile(player.GetSource_Misc("Tarragon Chapeau"), spawnPosition, player.velocity, modProj.Type, player.statDefense * (1f + player.endurance) * 2f, 10f, player.whoAmI, 0f, 0f, 0f);
							if (proj >= 0 && proj < Main.maxProjectiles)
							{
								if (Main.netMode != 0)
								{
									NetMessage.SendData(27, -1, -1, null, proj, 0f, 0f, 0f, 0, 0, 0);
								}
								Vector2 spawnPosition2 = Main.projectile[proj].Center + Utils.NextVector2Circular(Main.rand, 160f, 160f);
								int proj2 = Projectile.NewProjectile(player.GetSource_Misc("Tarragon Chapeau"), spawnPosition2, player.velocity, modProj.Type, player.statDefense * (1f + player.endurance) * 2f, 10f, player.whoAmI, 0f, 0f, 0f);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(27, -1, -1, null, proj2, 0f, 0f, 0f, 0, 0, 0);
								}
							}
							else
							{
								Main.NewText("Failed to spawn first projectile! Aborting second projectile.", new Color?(Color.Red));
							}
							tarragonBeatField.SetValue(thorlamityPlayer, 0);
						}
					}
					WeakReference<ModProjectile> weakReference = new WeakReference<ModProjectile>(calamityMod.Find<ModProjectile>("TarragonHeartbeat"));
					ModPlayer thorlamityPlayer2 = player.GetModPlayer<ModPlayer>(calamityMod.Find<ModPlayer>("ThorlamityPlayer"));
					ModProjectile modProj2;
					if (weakReference.TryGetTarget(out modProj2))
					{
						FieldInfo tarragonHeartbeatField = thorlamityPlayer2.GetType().GetField("tarragonHeartbeat", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (tarragonHeartbeatField == null)
						{
							return;
						}
						object value2 = tarragonHeartbeatField.GetValue(thorlamityPlayer2);
						if (value2 is int)
						{
							int tarragonHeartbeat = (int)value2;
							tarragonHeartbeat++;
							tarragonHeartbeatField.SetValue(thorlamityPlayer2, tarragonHeartbeat);
							if (Main.myPlayer == player.whoAmI && tarragonHeartbeat > 240)
							{
								tarragonHeartbeatField.SetValue(thorlamityPlayer2, 0);
								int tarragonHeartbeatProj = modProj2.Type;
								IEntitySource source = player.GetSource_Misc("Tarragon Paragon Crown");
								if (source == null)
								{
									Main.NewText("Debug: Projectile source is NULL!", new Color?(Color.Red));
									return;
								}
								int proj3 = Projectile.NewProjectile(source, player.MountedCenter, player.velocity, tarragonHeartbeatProj, 0, 0f, player.whoAmI, 7f, 0f, 0f);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(27, -1, -1, null, proj3, 0f, 0f, 0f, 0, 0, 0);
								}
							}
						}
					}
				}
			}
			ModItem blazingCore;
			if (AccessoryEffectLoader.AddEffect<TarragonEnchantment.BlazingCoreEffects>(player, base.Item) && calamity.TryFind<ModItem>("BlazingCore", ref blazingCore))
			{
				blazingCore.UpdateAccessory(player, hideVisual);
			}
			ModItem darkSunRing;
			if (calamity.TryFind<ModItem>("DarkSunRing", ref darkSunRing))
			{
				darkSunRing.UpdateAccessory(player, hideVisual);
			}
			ModItem warbannerOfTheSun;
			if (ModLoader.HasMod("FargowiltasCrossmod") && calamity.TryFind<ModItem>("WarbanneroftheSun", ref warbannerOfTheSun))
			{
				warbannerOfTheSun.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00012EFC File Offset: 0x000110FC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Tarragon Helmet";
				}, new int[]
				{
					ModContent.ItemType<TarragonHeadMagic>(),
					ModContent.ItemType<TarragonHeadMelee>(),
					ModContent.ItemType<TarragonHeadRanged>(),
					ModContent.ItemType<TarragonHeadRogue>(),
					ModContent.ItemType<TarragonHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Tarragon Helmet";
				}, new int[]
				{
					ModContent.ItemType<TarragonHeadMagic>(),
					ModContent.ItemType<TarragonHeadMelee>(),
					ModContent.ItemType<TarragonHeadRanged>(),
					ModContent.ItemType<TarragonHeadRogue>(),
					ModContent.ItemType<TarragonHeadSummon>(),
					calamityBardHealer.Find<ModItem>("TarragonChapeau").Type,
					calamityBardHealer.Find<ModItem>("TarragonParagonCrown").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyTarragonHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyTarragonHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<TarragonBreastplate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TarragonLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BlazingCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DarkSunRing>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<DefiledGreatsword>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<WarbanneroftheSun>(), 1);
			}
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0200014F RID: 335
		public class TarragonEffects : AccessoryEffect
		{
			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00018F80 File Offset: 0x00017180
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TarragonEnchantment>();
				}
			}
		}

		// Token: 0x02000150 RID: 336
		public class BlazingCoreEffects : AccessoryEffect
		{
			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00018F80 File Offset: 0x00017180
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TarragonEnchantment>();
				}
			}
		}
	}
}
