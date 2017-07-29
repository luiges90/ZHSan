using GameGlobal;
using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tools;
//using System.Drawing;


namespace GameFreeText
{

    //  //using System.Drawing;

    public class FreeText
    {
        private TextAlign align;
        private Microsoft.Xna.Framework.Rectangle alignedPosition;
        public Font Builder = new Font();
        private Microsoft.Xna.Framework.Point displayOffset;
        private Microsoft.Xna.Framework.Rectangle position;
        private string text;
        private Microsoft.Xna.Framework.Color textColor;
        //private Texture2D textTexture;

        public FreeText(Font builder)
        {
            this.position = Microsoft.Xna.Framework.Rectangle.Empty;
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.align = TextAlign.None;
            this.textColor = Microsoft.Xna.Framework.Color.Black;
            //this.textTexture = null;
            this.Builder = builder;
        }

        public FreeText(Font font, Microsoft.Xna.Framework.Color color)
        {
            this.position = Microsoft.Xna.Framework.Rectangle.Empty;
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.align = TextAlign.None;
            this.textColor = Microsoft.Xna.Framework.Color.Black;
            //this.textTexture = null;
            this.Builder = font;
            //this.Builder = new FreeTextBuilder();
            //this.Builder.SetFreeTextBuilder(device, font);
            this.textColor = color;
        }

        public void Draw(float Depth, Microsoft.Xna.Framework.Rectangle weizhijuxing)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(weizhijuxing.X, weizhijuxing.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, this.TextColor, 0f, Vector2.Zero, Builder.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        public void Draw()
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(this.alignedPosition.X, this.alignedPosition.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, this.TextColor, 0f, Vector2.Zero, Builder.Scale, SpriteEffects.None, 1f);
            }
        }

        public void Draw(float Depth)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(this.alignedPosition.X, this.alignedPosition.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, this.TextColor, 0f, Vector2.Zero, Builder.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Color color, float Depth)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(this.alignedPosition.X, this.alignedPosition.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, color, 0f, Vector2.Zero, Builder.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        public void Draw(float Rotation, float? Depth, float scale)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(this.alignedPosition.X, this.alignedPosition.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, this.textColor, Rotation, Vector2.Zero, scale, SpriteEffects.None, Depth == null ? 1f : ((float)Depth + -0.0001f));
            }
        }

        public void Draw(float Rotation, float Depth)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                var pos = new Vector2(this.alignedPosition.X, this.alignedPosition.Y);
                CacheManager.DrawString(Session.Current.Font, Text, pos, this.textColor, Rotation, Vector2.Zero, Builder.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        private void ResetAlignedPosition()
        {
            if (((this.align != TextAlign.None) && !String.IsNullOrEmpty(this.Text)) && !(this.position == Microsoft.Xna.Framework.Rectangle.Empty))
            {
                
                switch (this.align)
                {
                    case TextAlign.Left:
                        this.alignedPosition = StaticMethods.LeftRectangle(this.DisplayPosition, new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width, this.Height));
                        break;

                    case TextAlign.Middle:
                        this.alignedPosition = StaticMethods.CenterRectangle(this.DisplayPosition, new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width, this.Height));
                        break;

                    case TextAlign.Right:
                        this.alignedPosition = StaticMethods.RightRectangle(this.DisplayPosition, new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width, this.Height));
                        break;
                }
            }
        }

        //private void ResetTextTexture()
        //{
        //    if (this.text != null)
        //    {
        //        this.TextTexture = this.Builder.CreateTextTexture(this.text);
        //    }
        //}

        public TextAlign Align
        {
            get
            {
                return this.align;
            }
            set
            {
                this.align = value;
                this.ResetAlignedPosition();
            }
        }

        public Microsoft.Xna.Framework.Rectangle AlignedPosition
        {
            get
            {
                return this.alignedPosition;
            }
        }

        public Microsoft.Xna.Framework.Point DisplayOffset
        {
            get
            {
                return this.displayOffset;
            }
            set
            {
                this.displayOffset = value;
                this.ResetAlignedPosition();
            }
        }

        public Microsoft.Xna.Framework.Rectangle DisplayPosition
        {
            get
            {
                return new Microsoft.Xna.Framework.Rectangle(this.position.X + this.displayOffset.X, this.position.Y + this.displayOffset.Y, this.position.Width, this.position.Height);
            }
        }

        public Microsoft.Xna.Framework.Rectangle Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
                this.ResetAlignedPosition();
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.ResetAlignedPosition();
                //this.ResetTextTexture();
            }
        }

        public Microsoft.Xna.Framework.Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                //this.ResetTextTexture();
            }
        }

        //public Texture2D TextTexture
        //{
        //    get
        //    {
        //        return this.textTexture;
        //    }
        //    set
        //    {
        //        this.textTexture = value;
        //        this.ResetAlignedPosition();
        //    }
        //}

        public int Width
        {
            get
            {
                return Convert.ToInt32(Builder.GetWidthHeight(Text.NullToString("")).X);
                //return ((this.textTexture == null) ? 0 : this.textTexture.Width);
            }

        }

        public int Height
        {
            get
            {
                return Convert.ToInt32(Builder.GetWidthHeight(Text.NullToString("")).Y);
                //return ((this.textTexture == null) ? 0 : this.textTexture.Height);
            }
        }
    }
}

