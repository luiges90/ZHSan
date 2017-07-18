using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;
using System.Collections.Generic;

namespace ToolBarPlugin
{

    internal class ToolBar
    {
        internal int BackgroundHeight;
        public Rectangle BackgroundPosition;
        internal Texture2D BackgroundTexture;
        internal IGameContextMenu ContextMenuPlugin;
        private bool drawTools = true;
        internal bool Enabled = true;
        private bool isShowing = true;
        private Screen screen;
        internal Texture2D SpliterTexture;
        internal int SpliterWidth;
        internal List<Tool> Tools = new List<Tool>();

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.BackgroundTexture, this.BackgroundPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            if (this.DrawTools)
            {
                foreach (Tool tool in this.Tools)
                {
                    if (tool.Name == "AirViewPlugin")
                    {
                        tool.Draw(spriteBatch,gameTime);
                    }
                    else
                    {
                        tool.Draw(spriteBatch);
                    }
                }
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            screen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
        }

        internal void ResetToolsOffset()
        {
            int left = this.BackgroundPosition.Left;
            int right = this.BackgroundPosition.Right;
            foreach (Tool tool in this.Tools)
            {
                if (tool.Align == ToolAlign.Left)
                {
                    tool.DisplayOffset = new Point(left, this.BackgroundPosition.Y);
                    left += tool.Width + this.SpliterWidth;
                }
                else
                {
                    tool.DisplayOffset = new Point(right - tool.Width, this.BackgroundPosition.Y);
                    right -= tool.Width + this.SpliterWidth;
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.Enabled)
            {
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            if ((this.Enabled && StaticMethods.PointInRectangle(position, this.BackgroundPosition)) && !this.ContextMenuPlugin.IsShowing)
            {
                this.ContextMenuPlugin.IsShowing = true;
                this.ContextMenuPlugin.SetMenuKindByName("ToolBarRightClick");
                this.ContextMenuPlugin.Prepare(position.X, position.Y, this.screen.RealViewportSize);
            }
        }

        internal void SetToolsEnabled(bool enabled)
        {
            foreach (Tool tool in this.Tools)
            {
                tool.Enabled = enabled;
            }
        }

        internal void Update()
        {
            if (this.Enabled)
            {
                foreach (Tool tool in this.Tools)
                {
                    tool.Update();
                }
            }
        }

        internal bool DrawTools
        {
            get
            {
                return this.drawTools;
            }
            set
            {
                this.drawTools = value;
                foreach (Tool tool in this.Tools)
                {
                    tool.IsDrawing = value;
                }
            }
        }

        internal bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                this.isShowing = value;
            }
        }
    }
}

