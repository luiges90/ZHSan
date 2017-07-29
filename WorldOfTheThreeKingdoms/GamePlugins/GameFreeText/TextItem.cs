using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFreeText
{
    public class TextItem
    {
        public Rectangle AlignedPosition;
        public int MaxWidth;
        public Rectangle Position;
        public string Text;
        public PlatformTexture TextTexture;

        public TextItem(string text)
        {
            this.Text = text;
            this.TextTexture = null;
            this.Position = Rectangle.Empty;
            this.AlignedPosition = Rectangle.Empty;
        }

        public TextItem(string text, Rectangle position)
        {
            this.Text = text;
            this.TextTexture = null;
            this.Position = position;
            this.AlignedPosition = Rectangle.Empty;
        }
    }
}

