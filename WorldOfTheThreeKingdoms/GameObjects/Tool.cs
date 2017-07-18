using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace GameObjects
{

    public class Tool
    {
        public ToolAlign Align;
        public Point DisplayOffset;
        public bool Enabled = true;
        public bool IsDrawing = true;
        public string Name;
        public int Width;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public virtual void DrawBackground(SpriteBatch spriteBatch, Rectangle Position)
        {
        }

        public virtual void Update()
        {
        }
    }
}

