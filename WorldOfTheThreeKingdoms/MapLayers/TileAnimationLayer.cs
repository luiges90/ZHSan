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
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class TileAnimationLayer
    {
        public void Draw(Point viewportSize)
        {
            foreach (TileAnimation animation in Session.Current.Scenario.GeneratorOfTileAnimation.TileAnimations.Values)
            {
                if ((Session.GlobalVariables.DrawTroopAnimation && Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(animation.Position)) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.CurrentPlayer.IsPositionKnown(animation.Position))))
                {
                    animation.Draw(Session.MainGame.mainGameScreen.mainMapLayer.Tiles[animation.Position.X, animation.Position.Y].Destination);
                }
                else if (!animation.Looping)
                {
                    animation.Drawing = false;
                }
            }
            Session.Current.Scenario.GeneratorOfTileAnimation.ClearFinishedAnimation();
        }

        public void Initialize()
        {
        }
    }

 

}
