using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Weapons.Melee;
using FargosSoulsModDLCRecreated.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Enchantments
{
	// Token: 0x020000A9 RID: 169
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class AuricEnchantment : ModItem
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00017ED4 File Offset: 0x000160D4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip1", "Your strength rivals that of the Jungle Tyrant..."));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip2", "+130 maximum stealth"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip3", "28% increased melee speed"));
			tooltips.Add(new TooltipLine(base.Mod, "Tooltip4", "+6 max minions and 75% increased summon damage"));
			if (ModLoader.HasMod("CalamityBardHealer"))
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "40% increased radiant damage, 20% increased radiant critical strike chance, and 16% increased healing and radiant casting speed"));
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip15", "Increased empowerment duration by 5 seconds, increases inspiration regeneration rate by 20%, and increases the chance for inspiration notes to drop by 45%"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Heart of the Elements and The Sponge"));
			}
			else
			{
				tooltips.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Daawnlight Spirit Origian, The Transformer, and Dragon Scales"));
			}
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.OverrideColor = new Color?(new Color(217, 142, 67));
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00018050 File Offset: 0x00016250
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 10;
			base.Item.value = 10000000;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000180B4 File Offset: 0x000162B4
		public unsafe override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			Mod calamity = ModLoader.GetMod("CalamityMod");
			player.Calamity();
			if (ModLoader.HasMod("CalamityMod"))
			{
				CalamityPlayer calamityPlayer = player.Calamity();
				player.Calamity().auricBoost = true;
				calamityPlayer.rogueStealthMax += 1.3f;
				calamityPlayer.auricSet = true;
				*player.GetDamage<SummonDamageClass>() += 0.75f;
				player.maxMinions += 6;
				*player.GetAttackSpeed<MeleeDamageClass>() += 0.28f;
				if (ModLoader.HasMod("CalamityBardHealer"))
				{
					Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
					if (thoriumMod != null)
					{
						AuricEnchantment.<>c__DisplayClass6_0 CS$<>8__locals1;
						CS$<>8__locals1.thoriumPlayer = thoriumMod.Find<ModPlayer>("ThoriumPlayer");
						if (CS$<>8__locals1.thoriumPlayer != null)
						{
							AuricEnchantment.<UpdateAccessory>g__ModifyField|6_0<int>("bardBuffDuration", 300, ref CS$<>8__locals1);
							AuricEnchantment.<UpdateAccessory>g__ModifyField|6_0<float>("inspirationRegenBonus", 0.2f, ref CS$<>8__locals1);
							AuricEnchantment.<UpdateAccessory>g__ModifyField|6_0<float>("bardResourceDropBoost", 0.45f, ref CS$<>8__locals1);
						}
						DamageClass healerDamageClass = thoriumMod.Find<DamageClass>("HealerDamage");
						DamageClass healerToolClass = thoriumMod.Find<DamageClass>("HealerTool");
						if (healerDamageClass != null)
						{
							*player.GetDamage(healerDamageClass) += 0.4f;
							*player.GetCritChance(healerDamageClass) += 20f;
							*player.GetAttackSpeed(healerDamageClass) += 0.16f;
						}
						if (healerToolClass != null)
						{
							*player.GetAttackSpeed(healerToolClass) += 0.16f;
						}
					}
				}
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				ModItem hote;
				if (AccessoryEffectLoader.AddEffect<AuricEnchantment.HeartOfTheElementsEffects>(player, base.Item) && calamity.TryFind<ModItem>("HeartoftheElements", ref hote))
				{
					hote.UpdateAccessory(player, hideVisual);
				}
				ModItem theSponge;
				if (calamity.TryFind<ModItem>("TheSponge", ref theSponge))
				{
					theSponge.UpdateAccessory(player, hideVisual);
				}
			}
			else
			{
				ModItem hote2;
				if (AccessoryEffectLoader.AddEffect<AuricEnchantment.DaawnlightSpiritOriginEffects>(player, base.Item) && calamity.TryFind<ModItem>("DaawnlightSpiritOrigin", ref hote2))
				{
					hote2.UpdateAccessory(player, hideVisual);
				}
				ModItem theTransformer;
				if (calamity.TryFind<ModItem>("TheTransformer", ref theTransformer))
				{
					theTransformer.UpdateAccessory(player, hideVisual);
				}
			}
			ModItem dragonScales;
			if (ModLoader.HasMod("FargowiltasCrossmod") && AccessoryEffectLoader.AddEffect<AuricEnchantment.DragonScalesEffects>(player, base.Item) && calamity.TryFind<ModItem>("DragonScales", ref dragonScales))
			{
				dragonScales.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000182E8 File Offset: 0x000164E8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			RecipeGroup group;
			if (!ModLoader.HasMod("CalamityBardHealer"))
			{
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Auric Helmet";
				}, new int[]
				{
					ModContent.ItemType<AuricTeslaHoodedFacemask>(),
					ModContent.ItemType<AuricTeslaPlumedHelm>(),
					ModContent.ItemType<AuricTeslaRoyalHelm>(),
					ModContent.ItemType<AuricTeslaSpaceHelmet>(),
					ModContent.ItemType<AuricTeslaWireHemmedVisage>()
				});
			}
			else
			{
				Mod calamityBardHealer = ModLoader.GetMod("CalamityBardHealer");
				group = new RecipeGroup(delegate()
				{
					LocalizedText localizedText = Lang.misc[37];
					return ((localizedText != null) ? localizedText.ToString() : null) + " Auric Helmet";
				}, new int[]
				{
					ModContent.ItemType<AuricTeslaHoodedFacemask>(),
					ModContent.ItemType<AuricTeslaPlumedHelm>(),
					ModContent.ItemType<AuricTeslaRoyalHelm>(),
					ModContent.ItemType<AuricTeslaSpaceHelmet>(),
					ModContent.ItemType<AuricTeslaWireHemmedVisage>(),
					calamityBardHealer.Find<ModItem>("AuricTeslaFeatheredHeadwear").Type,
					calamityBardHealer.Find<ModItem>("AuricTeslaValkyrieVisage").Type
				});
			}
			RecipeGroup.RegisterGroup("FargosSoulsModDLCRecreated:AnyAuricHelmet", group);
			recipe.AddRecipeGroup("FargosSoulsModDLCRecreated:AnyAuricHelmet", 1);
			recipe.AddIngredient(ModContent.ItemType<AuricTeslaBodyArmor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AuricTeslaCuisses>(), 1);
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<HeartoftheElements>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<DaawnlightSpiritOrigin>(), 1);
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<TheSponge>(), 1);
			}
			else
			{
				recipe.AddIngredient(ModContent.ItemType<TheTransformer>(), 1);
			}
			recipe.AddIngredient(ModContent.ItemType<ArkoftheCosmos>(), 1);
			if (ModLoader.HasMod("FargowiltasCrossmod"))
			{
				recipe.AddIngredient(ModContent.ItemType<DragonScales>(), 1);
			}
			recipe.AddTile(this.calamity, "DraedonsForge");
			recipe.Register();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000184C4 File Offset: 0x000166C4
		[CompilerGenerated]
		internal static void <UpdateAccessory>g__ModifyField|6_0<T>(string fieldName, T increment, ref AuricEnchantment.<>c__DisplayClass6_0 A_2)
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
				if (AuricEnchantment.<>o__0|6<T>.<>p__1 == null)
				{
					AuricEnchantment.<>o__0|6<T>.<>p__1 = CallSite<Action<CallSite, FieldInfo, ModPlayer, object>>.Create(Binder.InvokeMember(256, "SetValue", null, typeof(AuricEnchantment), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(1, null),
						CSharpArgumentInfo.Create(1, null),
						CSharpArgumentInfo.Create(0, null)
					}));
				}
				Action<CallSite, FieldInfo, ModPlayer, object> target = AuricEnchantment.<>o__0|6<T>.<>p__1.Target;
				CallSite <>p__ = AuricEnchantment.<>o__0|6<T>.<>p__1;
				FieldInfo arg = field;
				ModPlayer thoriumPlayer = A_2.thoriumPlayer;
				if (AuricEnchantment.<>o__0|6<T>.<>p__0 == null)
				{
					AuricEnchantment.<>o__0|6<T>.<>p__0 = CallSite<Func<CallSite, object, T, object>>.Create(Binder.BinaryOperation(0, 0, typeof(AuricEnchantment), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(0, null),
						CSharpArgumentInfo.Create(1, null)
					}));
				}
				target(<>p__, arg, thoriumPlayer, AuricEnchantment.<>o__0|6<T>.<>p__0.Target(AuricEnchantment.<>o__0|6<T>.<>p__0, typedValue, increment));
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

		// Token: 0x04000065 RID: 101
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

		// Token: 0x020001A0 RID: 416
		public class HeartOfTheElementsEffects : AccessoryEffect
		{
			// Token: 0x060005D4 RID: 1492 RVA: 0x00018F39 File Offset: 0x00017139
			public override bool IsLoadingEnabled(Mod mod)
			{
				return !ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001919B File Offset: 0x0001739B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AuricEnchantment>();
				}
			}
		}

		// Token: 0x020001A1 RID: 417
		public class DaawnlightSpiritOriginEffects : AccessoryEffect
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001919B File Offset: 0x0001739B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AuricEnchantment>();
				}
			}
		}

		// Token: 0x020001A2 RID: 418
		public class DragonScalesEffects : AccessoryEffect
		{
			// Token: 0x060005DC RID: 1500 RVA: 0x00018F13 File Offset: 0x00017113
			public override bool IsLoadingEnabled(Mod mod)
			{
				return ModLoader.HasMod("FargowiltasCrossmod");
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x00018F74 File Offset: 0x00017174
			public override Header ToggleHeader
			{
				get
				{
					return (Header)Header.GetHeader<ExaltationForceHeader>();
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001919B File Offset: 0x0001739B
			public override int ToggleItemType
			{
				get
				{
					return ModContent.ItemType<AuricEnchantment>();
				}
			}
		}
	}
}
