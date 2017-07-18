using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MapLayerPlugin
{

    internal class MapLayer : Tool
    {
        internal Texture2D NormalLayerActiveTexture;
        internal Rectangle NormalLayerPosition;
        internal Texture2D NormalLayerTexture;
        internal Texture2D RoutewayLayerActiveTexture;
        internal Rectangle RoutewayLayerPosition;
        internal Texture2D RoutewayLayerTexture;
        private Screen screen;

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? nullable;
            switch (GlobalVariables.CurrentMapLayer)
            {
                case MapLayerKind.Normal:
                    nullable = null;
                    spriteBatch.Draw(this.RoutewayLayerTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;

                case MapLayerKind.Routeway:
                    nullable = null;
                    spriteBatch.Draw(this.RoutewayLayerActiveTexture, this.RoutewayLayerDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                    break;
            }
            if (GlobalVariables.ShowGrid)
            {
                spriteBatch.Draw(this.NormalLayerActiveTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
            else
            {
                spriteBatch.Draw(this.NormalLayerTexture, this.NormalLayerDisplayPosition, null , Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.NormalLayerDisplayPosition))
                {
                    GlobalVariables.ShowGrid = !GlobalVariables.ShowGrid;
                    
                }
                else if (StaticMethods.PointInRectangle(position, this.RoutewayLayerDisplayPosition))
                {
                    if (GlobalVariables.CurrentMapLayer == MapLayerKind.Normal)
                    {
                        GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
                    }
                    else 
                    {
                        GlobalVariables.CurrentMapLayer = MapLayerKind.Normal;
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

