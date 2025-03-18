using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Souls;
using FargosSoulsModDLCRecreated.Items.DBZ;
using FargosSoulsModDLCRecreated.Items.Spirit.Souls;
using FargosSoulsModDLCRecreated.Items.Thorium.Souls;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items
{
	// Token: 0x02000009 RID: 9
	public class DLCItemChanges : GlobalItem
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002824 File Offset: 0x00000A24
		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			FargoExtensionMethods.FargoSouls(player);
			if (item.type == ModContent.ItemType<EternitySoul>())
			{
				if (ModLoader.HasMod("CalamityMod"))
				{
					ModContent.GetInstance<CalamitySoul>().UpdateAccessory(player, hideVisual);
				}
				if (ModLoader.HasMod("ThoriumMod"))
				{
					ModContent.GetInstance<ThoriumSoul>().UpdateAccessory(player, hideVisual);
				}
				if (ModLoader.HasMod("SpiritMod"))
				{
					ModContent.GetInstance<SpiritSoul>().UpdateAccessory(player, hideVisual);
				}
			}
			if (item.type == ModContent.ItemType<EternitySoul>() || item.type == ModContent.ItemType<UniverseSoul>())
			{
				if (ModLoader.HasMod("ThoriumMod"))
				{
					ModContent.GetInstance<GuardianAngelsSoul>().UpdateAccessory(player, hideVisual);
					ModContent.GetInstance<BardSoul>().UpdateAccessory(player, hideVisual);
				}
				if (ModLoader.HasMod("CalamityMod") && !ModLoader.HasMod("FargowiltasCrossmod"))
				{
					ModContent.GetInstance<RogueSoul>().UpdateAccessory(player, hideVisual);
				}
				if (ModLoader.HasMod("DBZMODPORT"))
				{
					ModContent.GetInstance<KiSoul>().UpdateAccessory(player, hideVisual);
				}
			}
		}
	}
}
