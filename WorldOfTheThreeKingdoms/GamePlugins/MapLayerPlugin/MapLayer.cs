using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfTheThreeKingdoms.GameScreens;

namespace MapLayerPlugin
{

    internal class MapLayer : Tool
    {
        internal PlatformTexture NormalLayerActiveTexture;
        internal Rectangle NormalLayerPosition;
        internal PlatformTexture NormalLayerTexture;
        internal PlatformTexture RoutewayLayerActiveTexture;
        internal Rectangle RoutewayLayerPosition;
        internal PlatformTexture RoutewayLayerTexture;
        

        public override void Draw()
        {
#pragma warning disable CS0219 // The variable 'nullable' is assigned but its value is never used
            Rectangle? nullable;
#pragma warning restore CS0219 // The variable 'nullable' is assigned but its value is never used
            switch (Session.GlobalVariables.CurrentMapLayer)
            {
                case MapLayerKind.Normal:
                    nullable = null;
                    CacheManager.Draw(this.RoutewayLayerTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;

                case MapLayerKind.Routeway:
                    nullable = null;
                    CacheManager.Draw(this.RoutewayLayerActiveTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;
            }
            if (Session.GlobalVariables.ShowGrid)
            {
                CacheManager.Draw(this.NormalLayerActiveTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
            else
            {
                CacheManager.Draw(this.NormalLayerTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
        }

        internal void Initialize(MainGameScreen screen)
        {            
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.NormalLayerDisplayPosition))
                {
                    Session.GlobalVariables.ShowGrid = !Session.GlobalVariables.ShowGrid;
                    
                }
                else if (StaticMethods.PointInRectangle(position, this.RoutewayLayerDisplayPosition))
                {
                    if (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Normal)
                    {
                        Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
                    }
                    else 
                    {
                        Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Normal;
                    }
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.Enabled)
            {
            }
        }

        public override void Update()
        {
        }

        private Rectangle NormalLayerDisplayPosition
        {
            get
            {
                return new Rectangle(this.NormalLayerPosition.X + this.DisplayOffset.X, this.NormalLayerPosition.Y + this.DisplayOffset.Y, this.NormalLayerPosition.Width, this.NormalLayerPosition.Height);
            }
        }

        private Rectangle RoutewayLayerDisplayPosition
        {
            get
            {
                return new Rectangle(this.RoutewayLayerPosition.X + this.DisplayOffset.X, this.RoutewayLayerPosition.Y + this.DisplayOffset.Y, this.RoutewayLayerPosition.Width, this.RoutewayLayerPosition.Height);
            }
        }
    }
}

