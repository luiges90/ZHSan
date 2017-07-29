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

        public virtual void Draw()
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void DrawBackground(Rectangle Position)
        {
        }

        public virtual void Update()
        {
        }
    }
}

