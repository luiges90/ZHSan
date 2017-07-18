using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.Animations;



namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class TileAnimationLayer
    {
        private MainMapLayer mainMapLayer;
        private GameScenario scenario;

        public void Draw(SpriteBatch spriteBatch, Point viewportSize)
        {
            foreach (TileAnimation animation in this.scenario.GeneratorOfTileAnimation.TileAnimations.Values)
            {
                if ((GlobalVariables.DrawTroopAnimation && this.mainMapLayer.TileInScreen(animation.Position)) && ((GlobalVariables.SkyEye || this.scenario.NoCurrentPlayer) || ((this.scenario.CurrentPlayer != null) && this.scenario.CurrentPlayer.IsPositionKnown(animation.Position))))
                {
                    animation.Draw(spriteBatch, this.mainMapLayer.Tiles[animation.Position.X, animation.Position.Y].Destination);
                }
                else if (!animation.Looping)
                {
                    animation.Drawing = false;
                }
            }
            this.scenario.GeneratorOfTileAnimation.ClearFinishedAnimation();
        }

        public void Initialize(MainMapLayer mainMapLayer, GameScenario scenario)
        {
            this.mainMapLayer = mainMapLayer;
            this.scenario = scenario;
        }
    }

 

}
