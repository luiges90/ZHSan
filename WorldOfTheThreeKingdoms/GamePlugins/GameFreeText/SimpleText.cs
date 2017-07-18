using GameGlobal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameFreeText
{

    public class SimpleText
    {
        public Font Builder = new Font()
        {
            Size = 14
        };

        public bool NewLine;
        public int Row;
        public string Text;
        public Color TextColor;
        public Rectangle TextPosition;
        //public Texture2D TextTexture;
                
        static Vector2 OneWidthHeight = new Vector2(28, 30);

        public int Height
        {
            get
            {
                return Convert.ToInt32(OneWidthHeight.Y * Builder.Scale);  // ((this.TextTexture != null) ? this.TextTexture.Height : 0);
            }
        }

        public int Width
        {
            get
            {
                return Convert.ToInt32(OneWidthHeight.X * Text.Length * Builder.Scale);  // ((this.TextTexture != null) ? this.TextTexture.Width : 0);
            }
        }
    }
}

