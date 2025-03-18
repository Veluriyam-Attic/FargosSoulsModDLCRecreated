using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Souls;
using FargosSoulsModDLCRecreated.Items.DBZ;
using FargosSoulsModDLCRecreated.Items.Spirit.Souls;
using FargosSoulsModDLCRecreated.Items.Thorium.Souls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items
{
	// Token: 0x0200000A RID: 10
	public class DLCRecipeChanges : ModSystem
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002910 File Offset: 0x00000B10
		public override void PostAddRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<CalamitySoul>() && ModLoader.HasMod("CalamityMod"))
				{
					recipe.AddIngredient<CalamitySoul>(1);
				}
				if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<ThoriumSoul>() && ModLoader.HasMod("ThoriumMod"))
				{
					recipe.AddIngredient<ThoriumSoul>(1);
				}
				if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SpiritSoul>() && ModLoader.HasMod("SpiritMod"))
				{
					recipe.AddIngredient<SpiritSoul>(1);
				}
				if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<GuardianAngelsSoul>() && ModLoader.HasMod("ThoriumMod"))
				{
					recipe.AddIngredient<GuardianAngelsSoul>(1);
				}
				if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<BardSoul>() && ModLoader.HasMod("ThoriumMod"))
				{
					recipe.AddIngredient<BardSoul>(1);
				}
				if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<RogueSoul>() && ModLoader.HasMod("CalamityMod") && !ModLoader.HasMod("FargowiltasCrossmod"))
				{
					recipe.AddIngredient<RogueSoul>(1);
				}
				if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<KiSoul>() && ModLoader.HasMod("DBZMODPORT"))
				{
					recipe.AddIngredient<KiSoul>(1);
				}
			}
		}
	}
}
