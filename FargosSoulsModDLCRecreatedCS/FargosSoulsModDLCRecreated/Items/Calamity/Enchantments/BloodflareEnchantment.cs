using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Bloodflare;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A0 RID: 160
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class BloodflareEnchantment : ModItem
	{
		// Token: 0x060002AC RID: 684 RVA: 0x000152E8 File Offset: 0x000134E8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 3000000;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001534C File Offset: 0x0001354C
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "The souls of the fallen are at your disposal..."));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "Greatly increased life regen, equivalent to Crimson armor"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "Enemies below 50% life drop a heart when struck"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "This effect has a 5 second cooldown"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "True melee strikes will heal you"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "After striking an enemy 15 times with true melee you will enter a blood frenzy for 5 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip7", "During this you will gain 25% increased melee damage, critical strike chance, and contact damage is halved"));
			list.Add(new TooltipLine(base.Mod, "Tooltip8", "This effect has a 30 second cooldown"));
			list.Add(new TooltipLine(base.Mod, "Tooltip9", "Press Y to unleash the lost souls of Polterghast to destroy your enemies"));
			list.Add(new TooltipLine(base.Mod, "Tooltip10", "This effect has a 30 second cooldown"));
			list.Add(new TooltipLine(base.Mod, "Tooltip11", "Ranged weapons fire bloodsplosion orbs every 2.5 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip12", "Magic weapons fire ghostly bolts every 1.67 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip13", "Magic critical strikes cause flame explosions every 2 seconds"));
			list.Add(new TooltipLine(base.Mod, "Tooltip14", "Summons Polterghast mines to circle you"));
			list.Add(new TooltipLine(base.Mod, "Tooltip15", "At 90% life and above you gain 10% increased summon damage"));
			list.Add(new TooltipLine(base.Mod, "Tooltip16", "At 50% life and below you gain 20 defense and +1 HP/s life regen"));
			list.Add(new TooltipLine(base.Mod, "Tooltip17", "Being over 80% life boosts your defense by 30 and rogue crit by 5%"));
			list.Add(new TooltipLine(base.Mod, "Tooltip18", "Being below 80% life boosts your rogue damage by 10%"));
			list.Add(new TooltipLine(base.Mod, "Tooltip19", "Rogue critical strikes have a 50% chance to heal you"));
			list.Add(new TooltipLine(base.Mod, "Tooltip20", "Press Y to trigger a brimflame frenzy effect"));
			list.Add(new TooltipLine(base.Mod, "Tooltip21", "While under this effect, you get an additional 40% increase to magic damage"));
			list.Add(new TooltipLine(base.Mod, "Tooltip22", "However, this comes at the cost of rapid life loss and no mana regeneration"));
			list.Add(new TooltipLine(base.Mod, "Tooltip23", "This can be toggled off, however, a brimflame frenzy has a 30 second cooldown"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip213", "27% increased radiant damage and 11% increased radiant casting speed"));
				list.Add(new TooltipLine(base.Mod, "Tooltip214", "Press G to use half your life to summon a ritual"));
				list.Add(new TooltipLine(base.Mod, "Tooltip215", "The ritual will heal allies for 12 life rapidly"));
				list.Add(new TooltipLine(base.Mod, "Tooltip216", "Only one ritual can be active at a time"));
				list.Add(new TooltipLine(base.Mod, "Tooltip217", "+4 max inspiration"));
				list.Add(new TooltipLine(base.Mod, "Tooltip218", "Press G to gain Alluring Song buff for 5 seconds"));
				list.Add(new TooltipLine(base.Mod, "Tooltip219", "Alluring song increases regeneration rate and empowerment range by 50% and empowerment duration by 5 seconds"));
				list.Add(new TooltipLine(base.Mod, "Tooltip220", "Taking damage while Alluring Song is active heals you by a flat 200 life but removes Alluring Song"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip24", "Effects of the Chalice of the Blood God, Void Of Extinction, Bloodflare Core, Phantomic Artifact, and Eldritch Soul Artifact"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip24", "Effects of Affliction, Void Of Extinction, Bloodflare Core, Phantomic Artifact, and Eldritch Soul Artifact"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip25", "Effects of Void of Calamity, Flame-Licked Shell, Slagsplitter Pauldron, and Chaos Stone"));
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(191, 68, 59));
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00015784 File Offset: 0x00013984
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			CalamityPlayer modPlayer = player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				if (ModLoader.HasMod("ThoriumMod") && ModLoader.HasMod("CalamityBardHealer"))
				{
					Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
					BloodflareEnchantment.<>c__DisplayClass8_0 CS$<>8__locals1;
					CS$<>8__locals1.thoriumPlayer = thoriumMod.Find<ModPlayer>("ThoriumPlayer");
					if (ModLoader.HasMod("CalamityBardHealer") && AccessoryEffectLoader.AddEffect<BloodflareEnchantment.BloodflareRitualistEffects>(player, base.Item) && Main.myPlayer == player.whoAmI && Main.keyState.IsKeyDown(71) && !Main.oldKeyState.IsKeyDown(71))
					{
						int bloodflareRitualProj = ModLoader.GetMod("CalamityBardHealer").Find<ModProjectile>("BloodflareRitual").Type;
						if (player.ownedProjectileCounts[bloodflareRitualProj] == 0)
						{
							int proj = Projectile.NewProjectile(player.GetSource_Misc("Bloodflare Ritualist Mask"), player.MountedCenter, player.velocity, bloodflareRitualProj, 0, 0f, player.whoAmI, 0f, 0f, 0f);
							NetMessage.SendData(27, -1, -1, null, proj, 0f, 0f, 0f, 0, 0, 0);
							int damage = player.statLifeMax2 / 2;
							Player.HurtInfo hurtInfo2;
							hurtInfo2..ctor();
							hurtInfo2.DamageSource = PlayerDeathReason.ByOther(13, -1);
							hurtInfo2.Damage = damage;
							hurtInfo2.HitDirection = player.direction;
							hurtInfo2.Dodgeable = false;
							Player.HurtInfo hurtInfo = hurtInfo2;
							player.Hurt(hurtInfo, false);
							if (player.statLife <= 0)
							{
								player.KillMe(PlayerDeathReason.ByOther(13, -1), 1.0, 0, false);
							}
							player.lifeRegenCount = 0;
							player.lifeRegenTime = 0f;
							NetMessage.SendPlayerHurt(player.whoAmI, hurtInfo, -1);
						}
					}
					if (CS$<>8__locals1.thoriumPlayer != null)
					{
						BloodflareEnchantment.<UpdateAccessory>g__ModifyField|8_0<int>("bardBuffDuration", 240, ref CS$<>8__locals1);
						BloodflareEnchantment.<UpdateAccessory>g__ModifyField|8_0<int>("bardResourceMax2", 4, ref CS$<>8__locals1);
					}
					DamageClass healerDamageClass = thoriumMod.Find<DamageClass>("HealerDamage");
					if (healerDamageClass != null)
					{
						*player.GetDamage(healerDamageClass) += 0.27f;
						*player.GetAttackSpeed(healerDamageClass) += 0.11f;
					}
				}
				player.crimsonRegen = true;
				if (AccessoryEffectLoader.AddEffect<BloodflareEnchantment.BloodflareEffects>(player, base.Item))
				{
					if (ModLoader.HasMod("CalamityBardHealer"))
					{
						Mod mod = ModLoader.GetMod("CalamityBardHealer");
						int alluringSongBuffType = mod.Find<ModBuff>("AlluringSong").Type;
						int alluringSongBuffType2 = mod.Find<ModBuff>("AlluringSongCD").Type;
						if (Main.myPlayer == player.whoAmI && !player.HasBuff(alluringSongBuffType) && !player.HasBuff(alluringSongBuffType2) && Main.keyState.IsKeyDown(71) && !Main.oldKeyState.IsKeyDown(71))
						{
							player.AddBuff(alluringSongBuffType, 300, true, false);
						}
					}
					modPlayer.bloodflareSet = true;
					modPlayer.bloodflareMage = true;
					modPlayer.bloodflareMelee = true;
					modPlayer.bloodflareRanged = true;
					modPlayer.bloodflareThrowing = true;
					if (AccessoryEffectLoader.AddEffect<BloodflareEnchantment.PolterMinesEffect>(player, base.Item))
					{
						modPlayer.bloodflareSummon = true;
					}
					player.crimsonRegen = true;
				}
				ModItem eldritchSoulArtifact;
				if (calamity.TryFind<ModItem>("EldritchSoulArtifact", ref eldritchSoulArtifact))
				{
					eldritchSoulArtifact.UpdateAccessory(player, hideVisual);
				}
				ModItem affliction;
				if (!ModLoader.HasMod("FargowiltasCrossmod"))
				{
					ModItem chaliceOTBG;
					if (calamity.TryFind<ModItem>("ChaliceOfTheBloodGod", ref chaliceOTBG))
					{
						chaliceOTBG.UpdateAccessory(player, hideVisual);
					}
				}
				else if (AccessoryEffectLoader.AddEffect<BloodflareEnchantment.AfflictionEffects>(player, base.Item) && calamity.TryFind<ModItem>("Affliction", ref affliction))
				{
					affliction.UpdateAccessory(player, hideVisual);
				}
				ModItem bloodflareCore;
				if (AccessoryEffectLoader.AddEffect<BloodflareEnchantment.BloodflareCoreEffects>(player, base.Item) && calamity.TryFind<ModItem>("BloodflareCore", ref bloodflareCore))
				{
					bloodflareCore.UpdateAccessory(player, hideVisual);
				}
				ModItem phantomicArtifact;
				if (AccessoryEffectLoader.AddEffect<BloodflareEnchantment.PhantomicArtifactEffects>(player, base.Item) && calamity.TryFind<ModItem>("PhantomicArtifact", ref phantomicArtifact))
				{
					phantomicArtifact.UpdateAccessory(player, hideVisual);
				}
				ModItem brimflameEnchantment;
				if (ModLoader.GetMod("FargosSoulsModDLCRecreated").TryFind<ModItem>("BrimflameEnchantment", ref brimflameEnchantment))
				{
					brimflameEnchantment.UpdateAccessory(player, hideVisual);
				}
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00015B70 File Offset: 0x00013D70
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Bloodflare Helmet";
				}, new int[]
				{
					ModContent.ItemType<BloodflareHeadMagic>(),
					ModContent.ItemType<BloodflareHeadMelee>(),
					ModContent.ItemType<BloodflareHeadRanged>(),
					ModContent.ItemType<BloodflareHeadRogue>(),
					ModContent.ItemType<BloodflareHeadSummon>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Bloodflare Helmet";
				}, new int[]
				{
					ModContent.ItemType<BloodflareHeadMagic>(),
					ModContent.ItemType<BloodflareHeadMelee>(),
					ModContent.ItemType<BloodflareHeadRanged>(),
					ModContent.ItemType<BloodflareHeadRogue>(),
					ModContent.ItemType<BloodflareHeadSummon>(),
					calamityBardHealer.Find<ModItem>("BloodflareRitualistMask").Type,
					calamityBardHealer.Find<ModItem>("BloodflareSirenSkull").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyBloodflareHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyBloodflareHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<BloodflareBodyArmor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloodflareCuisses>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BrimflameEnchantment>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<ChaliceOfTheBloodGod>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<Affliction>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<EldritchSoulArtifact>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BloodflareCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PhantomicArtifact>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015D14 File Offset: 0x00013F14
		[CompilerGenerated]
		internal static void <UpdateAccessory>g__ModifyField|8_0<T>(string fieldName, T increment, ref BloodflareEnchantment.<>c__DisplayClass8_0 A_2)
		{
			FieldInfo field = A_2.thoriumPlayer.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (!(field != null))
			{
				Main.NewText("Field " + fieldName + " not found.", byte.MaxValue, byte.MaxValue, byte.MaxValue);
				return;
			}
			object value = field.GetValue(A_2.thoriumPlayer);
			if (value is T)
			{
				T typedValue = (T)((object)value);
				if (BloodflareEnchantment.<>o__0|8<T>.<>p__1 == null)
				{
					BloodflareEnchantment.<>o__0|8<T>.<>p__1 = CallSite<Action<CallSite, FieldInfo, ModPlayer, object>>.Create(Binder.InvokeMember(256, "SetValue", null, typeof(BloodflareEnchantment), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(1, null),
						CSharpArgumentInfo.Create(1, null),
						CSharpArgumentInfo.Create(0, null)
					}));
				}
				Action<CallSite, FieldInfo, ModPlayer, object> target = BloodflareEnchantment.<>o__0|8<T>.<>p__1.Target;
				CallSite <>p__ = BloodflareEnchantment.<>o__0|8<T>.<>p__1;
				FieldInfo arg = field;
				ModPlayer thoriumPlayer = A_2.thoriumPlayer;
				if (BloodflareEnchantment.<>o__0|8<T>.<>p__0 == null)
				{
					BloodflareEnchantment.<>o__0|8<T>.<>p__0 = CallSite<Func<CallSite, object, T, object>>.Create(Binder.BinaryOperation(0, 0, typeof(BloodflareEnchantment), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(0, null),
						CSharpArgumentInfo.Create(1, null)
					}));
				}
				target(<>p__, arg, thoriumPlayer, BloodflareEnchantment.<>o__0|8<T>.<>p__0.Target(BloodflareEnchantment.<>o__0|8<T>.<>p__0, typedValue, increment));
				return;
			}
			if (value is short)
			{
				short shortValue = (short)value;
				if (increment is int)
				{
					field.SetValue(A_2.thoriumPlayer, (short)((int)shortValue + (int)((object)increment)));
					return;
				}
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(27, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Field ");
			defaultInterpolatedStringHandler.AppendFormatted(fieldName);
			defaultInterpolatedStringHandler.AppendLiteral(" type is ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>((value != null) ? value.GetType() : null);
			defaultInterpolatedStringHandler.AppendLiteral(", expected ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(typeof(T));
			defaultInterpolatedStringHandler.AppendLiteral(".");
			Main.NewText(defaultInterpolatedStringHandler.ToStringAndClear(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x0200017A RID: 378
		public class BloodflareEffects : AccessoryEffect
		{
			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000560 RID: 1376 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}

		// Token: 0x0200017B RID: 379
		public class PolterMinesEffect : AccessoryEffect
		{
			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}

		// Token: 0x0200017C RID: 380
		public class BloodflareCoreEffects : AccessoryEffect
		{
			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}

		// Token: 0x0200017D RID: 381
		public class PhantomicArtifactEffects : AccessoryEffect
		{
			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}

		// Token: 0x0200017E RID: 382
		public class AfflictionEffects : AccessoryEffect
		{
			// Token: 0x0600056C RID: 1388 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x0600056D RID: 1389 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}

		// Token: 0x0200017F RID: 383
		public class BloodflareRitualistEffects : AccessoryEffect
		{
			// Token: 0x06000570 RID: 1392 RVA: 0x00019082 File Offset: 0x00017282
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("CalamityBardHealer");
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001907B File Offset: 0x0001727B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<BloodflareEnchantment>();
				}
			}
		}
	}
}
