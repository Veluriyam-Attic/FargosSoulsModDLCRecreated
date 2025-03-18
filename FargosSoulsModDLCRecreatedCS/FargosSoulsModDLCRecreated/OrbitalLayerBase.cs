using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated
{
	// Token: 0x02000004 RID: 4
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public abstract class OrbitalLayerBase : PlayerDrawLayer
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		public abstract bool Front { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002097 File Offset: 0x00000297
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			if (!this.Front)
			{
				return PlayerDrawLayers.BeforeFirstVanillaLayer;
			}
			return PlayerDrawLayers.AfterLastVanillaLayer;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer.dead || drawInfo.shadow != 0f)
			{
				return false;
			}
			DLCPlayer thoriumPlayer = drawPlayer.GetModPlayer<DLCPlayer>();
			return thoriumPlayer.orbital && thoriumPlayer.berserkStage > 0;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F4 File Offset: 0x000002F4
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			DLCPlayer modPlayer = drawPlayer.GetModPlayer<DLCPlayer>();
			Color color = Lighting.GetColor((int)((drawInfo.Position.X + (float)(drawPlayer.width / 2)) / 16f), (int)((drawInfo.Position.Y + (float)(drawPlayer.height / 2)) / 16f));
			if (modPlayer.berserkStage > 0)
			{
				Texture2D texture = ModContent.Request<Texture2D>("FargosSoulsModDLCRecreated/Items/BerserkerMask_Texture", 2).Value;
				Vector2 orbitalRotation = modPlayer.orbitalRotation3;
				float rotationStep = 6.2831855f / (float)modPlayer.berserkStage;
				DrawData item = default(DrawData);
				for (int i = 0; i < modPlayer.berserkStage; i++)
				{
					if (i > 0)
					{
						orbitalRotation = Utils.RotatedBy(orbitalRotation, (double)rotationStep, default(Vector2));
					}
					if ((!this.Front && orbitalRotation.Y < 0f) || (this.Front && orbitalRotation.Y >= 0f))
					{
						Vector2 position = drawInfo.Position + drawPlayer.Size * 0.5f + new Vector2(-2f + orbitalRotation.X * ((float)drawPlayer.width + (float)texture.Width * 0.3f), (float)(drawPlayer.mount.PlayerOffsetHitbox * 2));
						float scale = 0.87f + orbitalRotation.Y * 0.13f;
						Vector2 origin = Utils.Size(texture) * 0.5f * scale;
						SpriteEffects spriteEffects = (drawPlayer.gravDir == -1f) ? 2 : 0;
						Color alphaColor = Color.Lerp(color, color * 0.7f, Math.Abs(orbitalRotation.Y - 1f) * 0.5f);
						alphaColor.A = 190;
						item..ctor(texture, position - Main.screenPosition, null, alphaColor, 0f, origin, scale, spriteEffects, 0f);
						drawInfo.DrawDataCache.Add(item);
					}
				}
			}
		}
	}
}
