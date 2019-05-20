using GameGlobal;
using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Reflection;


namespace GameFreeText
{

    public class FreeTextList
    {
        private TextAlign align;
        //public FreeTextBuilder Builder;

        Font Font;

        private Microsoft.Xna.Framework.Point displayOffset;
        private Microsoft.Xna.Framework.Color textColor;
        public List<TextItem> TextList;

        public FreeTextList()
        {
            this.TextList = new List<TextItem>();
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.align = TextAlign.None;
            this.textColor = Microsoft.Xna.Framework.Color.Black;
            Font = new Font();
            //this.Builder = new FreeTextBuilder();
        }

        public FreeTextList(Font font)
        {
            this.TextList = new List<TextItem>();
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.align = TextAlign.None;
            this.textColor = Microsoft.Xna.Framework.Color.Black;
            Font = font;
            //this.Builder = new FreeTextBuilder();
            //this.Builder.SetFreeTextBuilder(device, font);
        }

        public FreeTextList(Font font, Microsoft.Xna.Framework.Color color)
        {
            this.TextList = new List<TextItem>();
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.align = TextAlign.None;
            this.textColor = Microsoft.Xna.Framework.Color.Black;
            Font = font;
            //this.Builder = new FreeTextBuilder();
            //this.Builder.SetFreeTextBuilder(device, font);
            this.textColor = color;
        }

        public void AddText(string text)
        {
            this.TextList.Add(new TextItem(text));
        }

        public void AddText(string text, Microsoft.Xna.Framework.Rectangle position)
        {
            this.TextList.Add(new TextItem(text, position));
        }

        public void Clear()
        {
            this.displayOffset = Microsoft.Xna.Framework.Point.Zero;
            this.TextList.Clear();
        }

        public Microsoft.Xna.Framework.Rectangle DisplayPosition(int index)
        {
            if (index >= this.TextList.Count || index < 0) return new Microsoft.Xna.Framework.Rectangle(0, 0, 0, 0);
            return new Microsoft.Xna.Framework.Rectangle(this.TextList[index].Position.X + this.displayOffset.X, this.TextList[index].Position.Y + this.displayOffset.Y, this.TextList[index].Position.Width, this.TextList[index].Position.Height);
        }

        public void Draw()
        {
            for (int i = 0; i < this.Count; i++)
            {                
                var pos = new Vector2(this.TextList[i].AlignedPosition.X, this.TextList[i].AlignedPosition.Y);

                CacheManager.DrawString(Session.Current.Font, this.TextList[i].Text, pos, this.textColor, 0f, Vector2.Zero, Font.Scale, SpriteEffects.None, 1f);
            }
        }

        public void Draw(int index)
        {            
            var pos = new Vector2(this.TextList[index].AlignedPosition.X, this.TextList[index].AlignedPosition.Y);

            CacheManager.DrawString(Session.Current.Font, this.TextList[index].Text, pos, this.textColor, 0f, Vector2.Zero, Font.Scale, SpriteEffects.None, 1f);
        }

        public void Draw(float Depth)
        {
            for (int i = 0; i < this.Count; i++)
            {
                var pos = new Vector2(this.TextList[i].AlignedPosition.X, this.TextList[i].AlignedPosition.Y);

                CacheManager.DrawString(Session.Current.Font, this.TextList[i].Text, pos, this.textColor, 0f, Vector2.Zero, Font.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        public void Draw(int index, float Depth)
        {
            var pos = new Vector2(this.TextList[index].AlignedPosition.X, this.TextList[index].AlignedPosition.Y);

            CacheManager.DrawString(Session.Current.Font, this.TextList[index].Text, pos, this.textColor, 0f, Vector2.Zero, Font.Scale, SpriteEffects.None, Depth + -0.0001f);
        }

        public void Draw(float Rotation, float Depth)
        {
            for (int i = 0; i < this.Count; i++)
            {
                float scale = Font.Size / 20f;

                var pos = new Vector2(this.TextList[i].AlignedPosition.X, this.TextList[i].AlignedPosition.Y);

                CacheManager.DrawString(Session.Current.Font, this.TextList[i].Text, pos, this.textColor, Rotation, Vector2.Zero, Font.Scale, SpriteEffects.None, Depth + -0.0001f);
            }
        }

        public void Draw(int index, float Rotation, float Depth)
        {

            var pos = new Vector2(this.TextList[index].AlignedPosition.X, this.TextList[index].AlignedPosition.Y);

            CacheManager.DrawString(Session.Current.Font, this.TextList[index].Text, pos, this.textColor, Rotation, Vector2.Zero, Font.Scale, SpriteEffects.None, Depth + -0.0001f);

        }

        private void ResetAlignedPosition(int index)
        {
            switch (this.align)
            {
                case TextAlign.Left:
                    this.TextList[index].AlignedPosition = StaticMethods.LeftRectangle(this.DisplayPosition(index), new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width(index), this.Height(index)));
                    break;

                case TextAlign.Middle:
                    this.TextList[index].AlignedPosition = StaticMethods.CenterRectangle(this.DisplayPosition(index), new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width(index), this.Height(index)));
                    break;

                case TextAlign.Right:
                    this.TextList[index].AlignedPosition = StaticMethods.RightRectangle(this.DisplayPosition(index), new Microsoft.Xna.Framework.Rectangle(0, 0, this.Width(index), this.Height(index)));
                    break;
            }
        }

        public void ResetAllAlignedPositions()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.ResetAlignedPosition(i);
            }
        }

        //public void ResetAllTextTextures()
        //{
        //    for (int i = 0; i < this.Count; i++)
        //    {
        //        this.ResetTextTexture(i);
        //    }
        //}

        //private void ResetTextTexture(int index)
        //{
        //    this.TextList[index].TextTexture = this.Builder.CreateTextTexture(this.TextList[index].Text);
        //}

        public void SimpleClear()
        {
            this.TextList.Clear();
        }

        public int Width(int index)
        {
            var item = this.TextList[index];
            int width = Convert.ToInt32(Font.GetWidthHeight(item.Text).X);
            return width;
            //return ((item.TextTexture == null) ? 0 : ((item.MaxWidth > 0) ? ((item.TextTexture.Width > item.MaxWidth) ? item.MaxWidth : item.TextTexture.Width) : item.TextTexture.Width));
        }

        public int Height(int index)
        {
            var item = this.TextList[index];
            int height = Convert.ToInt32(Font.GetWidthHeight(item.Text).Y);
            return height;
            //return ((this.TextList[index].TextTexture == null) ? 0 : this.TextList[index].TextTexture.Height);
        }

        public TextAlign Align
        {
            get
            {
                return this.align;
            }
            set
            {
                this.align = value;
                this.ResetAllAlignedPositions();
            }
        }

        public int Count
        {
            get
            {
                return this.TextList.Count;
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
                this.ResetAllAlignedPositions();
            }
        }

        public TextItem this[int index]
        {
            get
            {
                return this.TextList[index];
            }
            set
            {
                this.TextList[index] = value;
            }
        }

        public int MaxWidth
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    int num2 = this.Width(i);
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
                return num;
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
            }
        }
    }
}

