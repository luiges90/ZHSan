using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;




namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class MapVeilLayer
    {
        private Color BlendColorFull = new Color(new Vector4(0.8f, 0.8f, 0.8f, 0f));
        private Color BlendColorHigh = new Color(new Vector4(0.8f, 0.8f, 0.8f, 0.09f));
        private Color BlendColorLow = new Color(new Vector4(0.8f, 0.8f, 0.8f, 0.27f));
        private Color BlendColorMiddle = new Color(new Vector4(0.8f, 0.8f, 0.8f, 0.18f));
        private Color BlendColorNone = new Color(new Vector4(0.8f, 0.8f, 0.8f, 0.6f));
        private MainMapLayer mainMapLayer;
        private GameScenario scenario;
        private Texture2D veilTexture;

        public void Draw(SpriteBatch spriteBatch, Point viewportSize)
        {
            if ((GlobalVariables.DrawMapVeil && !GlobalVariables.SkyEye) && !this.scenario.NoCurrentPlayer)
            {
                foreach (Tile tile in this.mainMapLayer.DisplayingTiles)
                {
                    Rectangle? nullable;
                    switch (this.CurrentPlayer.GetKnownAreaDataNoCheck(tile.Position))
                    {
                        case InformationLevel.无:
                            nullable = null;
                            spriteBatch.Draw(this.veilTexture, tile.Destination, nullable, this.BlendColorNone, 0f, Vector2.Zero, SpriteEffects.None, 0.6f);
                            break;

                        case InformationLevel.低:
                            nullable = null;
                            spriteBatch.Draw(this.veilTexture, tile.Destination, nullable, this.BlendColorLow, 0f, Vector2.Zero, SpriteEffects.None, 0.6f);
                            break;

                        case InformationLevel.中:
                            nullable = null;
                            spriteBatch.Draw(this.veilTexture, tile.Destination, nullable, this.BlendColorMiddle, 0f, Vector2.Zero, SpriteEffects.None, 0.6f);
                            break;

                        case InformationLevel.高:
                            nullable = null;
                            spriteBatch.Draw(this.veilTexture, tile.Destination, nullable, this.BlendColorHigh, 0f, Vector2.Zero, SpriteEffects.None, 0.6f);
                            break;
                    }
                }
            }
        }

        public void Initialize(MainMapLayer mainMapLayer, GameScenario scenario)
        {
            this.mainMapLayer = mainMapLayer;
            this.scenario = scenario;
            this.veilTexture = mainMapLayer.screen.Textures.MapVeilTextures[0];
        }

        private Faction CurrentPlayer
        {
            get
            {
                return this.scenario.CurrentPlayer;
            }
        }
    }


}
