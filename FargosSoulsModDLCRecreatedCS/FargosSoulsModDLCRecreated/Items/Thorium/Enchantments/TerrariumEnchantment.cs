using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Core.DataClasses;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000035 RID: 53
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class TerrariumEnchantment : ModItem
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00007010 File Offset: 0x00005210
		public static int GetIndexFromName(string fullName)
		{
			return TerrariumEnchantment.Focuses.FindIndex((TerrariumSetClassFocus container) => container.FullName == fullName);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007040 File Offset: 0x00005240
		public static int GetNextIndexFromName(string fullName)
		{
			return (TerrariumEnchantment.GetIndexFromName(fullName) + 1) % TerrariumEnchantment.Focuses.Count;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007055 File Offset: 0x00005255
		public static string GetNextNameFromName(string fullName)
		{
			return TerrariumEnchantment.GetNameFromIndex(TerrariumEnchantment.GetNextIndexFromName(fullName));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007062 File Offset: 0x00005262
		public static string GetNameFromIndex(int index)
		{
			return TerrariumEnchantment.Focuses[index].FullName;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007074 File Offset: 0x00005274
		public static TerrariumSetClassFocus GetFocusFromName(string fullName)
		{
			return TerrariumEnchantment.Focuses[TerrariumEnchantment.GetIndexFromName(fullName)];
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00007086 File Offset: 0x00005286
		public unsafe static void GenericEffect(Player player)
		{
			*player.GetDamage(DamageClass.Generic) += (float)TerrariumEnchantment.GenericFocusDamage / 100f;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000070AF File Offset: 0x000052AF
		public static void MeleeEffect(Player player)
		{
			PlayerHelper.GetThoriumPlayer(player).thoriumEndurance += (float)TerrariumEnchantment.MeleeFocusDR / 100f;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000070CF File Offset: 0x000052CF
		public unsafe static void RangedEffect(Player player)
		{
			*player.GetAttackSpeed(DamageClass.Ranged) += (float)TerrariumEnchantment.RangedFocusFiringSpeed / 100f;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000070EC File Offset: 0x000052EC
		public static void MagicEffect(Player player)
		{
			player.statManaMax2 += TerrariumEnchantment.MagicFocusMaxMana;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007100 File Offset: 0x00005300
		public unsafe static void SummonEffect(Player player)
		{
			player.maxMinions += TerrariumEnchantment.SummonFocusMaxMinions;
			*player.GetDamage(DamageClass.Generic) -= (float)TerrariumEnchantment.SummonFocusDamageLoss / 100f;
			*player.GetDamage(DamageClass.Summon) += (float)TerrariumEnchantment.SummonFocusDamageLoss / 100f;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000716D File Offset: 0x0000536D
		public static void ThrowingEffect(Player player)
		{
			PlayerHelper.GetThoriumPlayer(player).techPointsMax += TerrariumEnchantment.ThrowingFocusTechPoints;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007186 File Offset: 0x00005386
		public static void HealerEffect(Player player)
		{
			PlayerHelper.GetThoriumPlayer(player).healBonus += TerrariumEnchantment.HealerFocusHealBonus;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000719F File Offset: 0x0000539F
		public static void BardEffect(Player player)
		{
			ThoriumPlayer thoriumPlayer = PlayerHelper.GetThoriumPlayer(player);
			thoriumPlayer.bardBuffDuration += (short)(TerrariumEnchantment.BardFocusDuration * 60);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000071C0 File Offset: 0x000053C0
		public static bool TryAddFocus(TerrariumSetClassFocus focus)
		{
			if (TerrariumEnchantment.Focuses.FindIndex((TerrariumSetClassFocus container) => container.FullName == focus.FullName) > -1)
			{
				return false;
			}
			TerrariumEnchantment.Focuses.Add(focus);
			return true;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007206 File Offset: 0x00005406
		public override void Load()
		{
			TerrariumEnchantment.Focuses = new List<TerrariumSetClassFocus>();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007212 File Offset: 0x00005412
		public override void Unload()
		{
			TerrariumEnchantment.Focuses = null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000721C File Offset: 0x0000541C
		public override void SetStaticDefaults()
		{
			DamageClass generic = DamageClass.Generic;
			LocalizedText localization = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Generic.Name", null);
			LocalizedText localizedText = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Generic.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.GenericFocusDamage
			});
			Action<Player> action;
			if ((action = TerrariumEnchantment.<>O.<0>__GenericEffect) == null)
			{
				action = (TerrariumEnchantment.<>O.<0>__GenericEffect = new Action<Player>(TerrariumEnchantment.GenericEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(generic, localization, localizedText, action, new Color(255, 255, 255)));
			DamageClass melee = DamageClass.Melee;
			LocalizedText localization2 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Melee.Name", null);
			LocalizedText localizedText2 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Melee.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.MeleeFocusDR
			});
			Action<Player> action2;
			if ((action2 = TerrariumEnchantment.<>O.<1>__MeleeEffect) == null)
			{
				action2 = (TerrariumEnchantment.<>O.<1>__MeleeEffect = new Action<Player>(TerrariumEnchantment.MeleeEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(melee, localization2, localizedText2, action2, new Color(255, 0, 0)));
			DamageClass ranged = DamageClass.Ranged;
			LocalizedText localization3 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Ranged.Name", null);
			LocalizedText localizedText3 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Ranged.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.RangedFocusFiringSpeed
			});
			Action<Player> action3;
			if ((action3 = TerrariumEnchantment.<>O.<2>__RangedEffect) == null)
			{
				action3 = (TerrariumEnchantment.<>O.<2>__RangedEffect = new Action<Player>(TerrariumEnchantment.RangedEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(ranged, localization3, localizedText3, action3, new Color(50, 255, 0)));
			DamageClass magic = DamageClass.Magic;
			LocalizedText localization4 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Magic.Name", null);
			LocalizedText localizedText4 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Magic.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.MagicFocusMaxMana
			});
			Action<Player> action4;
			if ((action4 = TerrariumEnchantment.<>O.<3>__MagicEffect) == null)
			{
				action4 = (TerrariumEnchantment.<>O.<3>__MagicEffect = new Action<Player>(TerrariumEnchantment.MagicEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(magic, localization4, localizedText4, action4, new Color(50, 180, 255)));
			DamageClass summon = DamageClass.Summon;
			LocalizedText localization5 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Summon.Name", null);
			LocalizedText localizedText5 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Summon.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.SummonFocusMaxMinions,
				TerrariumEnchantment.SummonFocusDamageLoss
			});
			Action<Player> action5;
			if ((action5 = TerrariumEnchantment.<>O.<4>__SummonEffect) == null)
			{
				action5 = (TerrariumEnchantment.<>O.<4>__SummonEffect = new Action<Player>(TerrariumEnchantment.SummonEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(summon, localization5, localizedText5, action5, new Color(180, 50, 255)));
			DamageClass throwing = DamageClass.Throwing;
			LocalizedText localization6 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Throwing.Name", null);
			LocalizedText localizedText6 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Throwing.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.ThrowingFocusTechPoints
			});
			Action<Player> action6;
			if ((action6 = TerrariumEnchantment.<>O.<5>__ThrowingEffect) == null)
			{
				action6 = (TerrariumEnchantment.<>O.<5>__ThrowingEffect = new Action<Player>(TerrariumEnchantment.ThrowingEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(throwing, localization6, localizedText6, action6, new Color(255, 180, 0)));
			DamageClass damageClass = (DamageClass)ThoriumDamageBase<HealerDamage>.Instance;
			LocalizedText localization7 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Healer.Name", null);
			LocalizedText localizedText7 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Healer.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.HealerFocusHealBonus
			});
			Action<Player> action7;
			if ((action7 = TerrariumEnchantment.<>O.<6>__HealerEffect) == null)
			{
				action7 = (TerrariumEnchantment.<>O.<6>__HealerEffect = new Action<Player>(TerrariumEnchantment.HealerEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(damageClass, localization7, localizedText7, action7, new Color(255, 255, 0)));
			DamageClass damageClass2 = (DamageClass)ThoriumDamageBase<BardDamage>.Instance;
			LocalizedText localization8 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Bard.Name", null);
			LocalizedText localizedText8 = ILocalizedModTypeExtensions.GetLocalization((ILocalizedModType)this, "Focus.Bard.Description", null).WithFormatArgs(new object[]
			{
				TerrariumEnchantment.BardFocusDuration
			});
			Action<Player> action8;
			if ((action8 = TerrariumEnchantment.<>O.<7>__BardEffect) == null)
			{
				action8 = (TerrariumEnchantment.<>O.<7>__BardEffect = new Action<Player>(TerrariumEnchantment.BardEffect));
			}
			TerrariumEnchantment.TryAddFocus(new TerrariumSetClassFocus(damageClass2, localization8, localizedText8, action8, new Color(0, 255, 180)));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000075E8 File Offset: 0x000057E8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 250000;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000764C File Offset: 0x0000584C
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000076D8 File Offset: 0x000058D8
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
				if (AccessoryEffectLoader.AddEffect<TerrariumEnchantment.TerrariumEffects>(player, base.Item))
				{
					ModItem tH;
					if (thorium.TryFind<ModItem>("TerrariumHelmet", ref tH))
					{
						thoriumPlayer.setTerrarium.Set(tH.Item);
					}
					TerrariumEnchantment.GetFocusFromName(thoriumPlayer.GetTerrariumFocusValue()).Effect(player);
				}
			}
			ModItem terrariumSurroundSound;
			if (AccessoryEffectLoader.AddEffect<TerrariumEnchantment.TerrariumSurroundSoundEffects>(player, base.Item) && thorium.TryFind<ModItem>("TerrariumSurroundSound", ref terrariumSurroundSound))
			{
				terrariumSurroundSound.UpdateAccessory(player, hideVisual);
			}
			ModItem tPS;
			if (thorium.TryFind<ModItem>("TerrariumParticleSprinters", ref tPS))
			{
				tPS.UpdateAccessory(player, hideVisual);
			}
			ModItem tD;
			if (thorium.TryFind<ModItem>("TerrariumDefender", ref tD))
			{
				tD.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000077AC File Offset: 0x000059AC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumHelmet>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumBreastPlate>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumGreaves>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumSurroundSound>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumParticleSprinters>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumDefender>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumSaber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumAutoharp>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumBomber>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumRippleKnife>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ThoriumCube>(), 1);
			recipe.AddTile(412);
			recipe.Register();
		}

		// Token: 0x0400001F RID: 31
		private static List<TerrariumSetClassFocus> Focuses;

		// Token: 0x04000020 RID: 32
		private static readonly int GenericFocusDamage = 20;

		// Token: 0x04000021 RID: 33
		private static readonly int MeleeFocusDR = 15;

		// Token: 0x04000022 RID: 34
		private static readonly int RangedFocusFiringSpeed = 15;

		// Token: 0x04000023 RID: 35
		private static readonly int MagicFocusMaxMana = 100;

		// Token: 0x04000024 RID: 36
		private static readonly int SummonFocusMaxMinions = 3;

		// Token: 0x04000025 RID: 37
		private static readonly int SummonFocusDamageLoss = 20;

		// Token: 0x04000026 RID: 38
		private static readonly int ThrowingFocusTechPoints = 2;

		// Token: 0x04000027 RID: 39
		private static readonly int HealerFocusHealBonus = 3;

		// Token: 0x04000028 RID: 40
		private static readonly int BardFocusDuration = 6;

		// Token: 0x04000029 RID: 41
		private static readonly float LifeRegen = 1f;

		// Token: 0x0400002A RID: 42
		private static readonly int MaxLife = 10;

		// Token: 0x0400002B RID: 43
		private static readonly int Damage = 10;

		// Token: 0x020000D6 RID: 214
		public class TerrariumEffects : AccessoryEffect
		{
			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000364 RID: 868 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000365 RID: 869 RVA: 0x00018BB1 File Offset: 0x00016DB1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TerrariumEnchantment>();
				}
			}
		}

		// Token: 0x020000D7 RID: 215
		public class TerrariumSurroundSoundEffects : AccessoryEffect
		{
			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000367 RID: 871 RVA: 0x00018B55 File Offset: 0x00016D55
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<MidgardForceHeader>();
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000368 RID: 872 RVA: 0x00018BB1 File Offset: 0x00016DB1
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<TerrariumEnchantment>();
				}
			}
		}

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400006B RID: 107
			public static Action<Player> <0>__GenericEffect;

			// Token: 0x0400006C RID: 108
			public static Action<Player> <1>__MeleeEffect;

			// Token: 0x0400006D RID: 109
			public static Action<Player> <2>__RangedEffect;

			// Token: 0x0400006E RID: 110
			public static Action<Player> <3>__MagicEffect;

			// Token: 0x0400006F RID: 111
			public static Action<Player> <4>__SummonEffect;

			// Token: 0x04000070 RID: 112
			public static Action<Player> <5>__ThrowingEffect;

			// Token: 0x04000071 RID: 113
			public static Action<Player> <6>__HealerEffect;

			// Token: 0x04000072 RID: 114
			public static Action<Player> <7>__BardEffect;
		}
	}
}
