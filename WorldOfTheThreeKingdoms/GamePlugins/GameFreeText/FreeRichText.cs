using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GameFreeText
{

    public class FreeRichText
    {
        //public FreeTextBuilder Builder;

        public Font Builder = new Font();

        public int ClientHeight;
        public int ClientWidth;
        private int currentPageIndex;
        private int currentRow;
        public Color DefaultColor;
        public Point DisplayOffset;
        public bool MultiPage;
        public bool OnePage;
        private List<int> PageIndexs;
        public int RowMargin;
        public List<SimpleText> Texts;

        public Color TitleColor;
        public Color SubTitleColor;
        public Color SubTitleColor2;
        public Color SubTitleColor3;
        public Color PositiveColor;
        public Color NegativeColor;

        public FreeRichText()
        {
            this.Texts = new List<SimpleText>();
            this.PageIndexs = new List<int>();
            this.ClientWidth = 200;
            this.ClientHeight = 600;
            //this.Builder = new FreeTextBuilder();
        }

        public int GetTextWidth(string text)
        {
            float scale = Builder == null ? 1f : Builder.Size / 20;
            return Convert.ToInt32(28 * text.Length * scale);
            //return this.myDrawing.MeasureString(text, this.font).ToSize().Width;
        }

        //public FreeRichText(FreeTextBuilder builder)
        //{
        //    this.Texts = new List<SimpleText>();
        //    this.PageIndexs = new List<int>();
        //    this.ClientWidth = 200;
        //    this.ClientHeight = 600;
        //    //this.Builder = builder;
        //}

        public void AddGameObjectTextBranch(GameObject gameObject, GameObjectTextBranch branch)
        {
            if ((branch != null) && (branch.Leaves.Count != 0))
            {
                foreach (GameObjectTextLeaf leaf in branch.Leaves)
                {
                    if ((gameObject != null) && (leaf.Property != ""))
                    {
                        this.AddText(StaticMethods.GetPropertyValue(gameObject, leaf.Property).ToString(), leaf.TextColor);
                    }
                    else
                    {
                        this.AddText(leaf.Text, leaf.TextColor);
                    }
                }
                this.ResortTexts();
            }
        }

        public void AddNewLine()
        {
            SimpleText item = new SimpleText {
                Text = @"\n",
                Builder = Builder
            };
            this.Texts.Add(item);
        }

        public void AddNewLine(int pos)
        {
            SimpleText item = new SimpleText {
                Text = @"\n",
                Builder = Builder
            };
            this.Texts.Insert(pos, item);
        }

        public void AddText(string text)
        {
            SimpleText item = new SimpleText {
                Text = text,
                Builder = Builder
            };
            this.Texts.Add(item);
        }

        public void AddText(string text, Color color)
        {
            SimpleText item = new SimpleText {
                Text = text,
                TextColor = color,
                Builder = Builder
            };
            this.Texts.Add(item);
        }

        public void AddText(int pos, string text, Color color)
        {
            SimpleText item = new SimpleText {
                Text = text,
                TextColor = color,
                Builder = Builder
            };
            this.Texts.Insert(pos, item);
        }

        private void BuildRow(int index)
        {
            int num = 0;
            for (int i = index; i < this.Texts.Count; i++)
            {
                if (this.Texts[i].NewLine)
                {
                    this.currentRow++;
                    num = 0;
                }
                else
                {
                    num += this.Texts[i].Width;
                }
                this.Texts[i].Row = this.currentRow;
                if (num >= this.ClientWidth)
                {
                    this.currentRow++;
                    if (this.DevideText(i, num - this.Texts[i].Width))
                    {
                        this.BuildRow(i + 1);
                    }
                    else
                    {
                        this.BuildRow(i);
                    }
                    break;
                }
                if (((this.Texts[i].Row + 1) * this.RowHeight) >= this.ClientHeight)
                {
                    this.MultiPage = true;
                    this.Texts[i].Row = 0;
                    this.currentRow = 0;
                    this.PageIndexs.Add(i + 1);
                }
            }
        }

        private void BuildTextTextures()
        {
            foreach (SimpleText text in this.Texts)
            {
                if (text.Text == @"\n")
                {
                    text.NewLine = true;
                }
                else
                {
                    //text.TextTexture = this.Builder.CreateTextTexture(text.Text);
                }
            }
        }

        public void Clear()
        {
            this.Texts.Clear();
            this.PageIndexs.Clear();
        }

        private bool DevideText(int index, int currentWidth)
        {
            SimpleText text;
            string str = this.Texts[index].Text;
            int textWidth = 0;
            int length = 1;
            do
            {
                textWidth = GetTextWidth(str.Substring(0, length));  // this.Builder.GetTextWidth(str.Substring(0, length));
                if ((currentWidth + textWidth) >= this.ClientWidth)
                {
                    break;
                }
                length++;
            }
            while (length <= str.Length);
            if (length > str.Length)
            {
                //暫去掉，待考慮
                //throw new Exception("RichText Width Error.");
            }
            if (length == 1)
            {
                SimpleText local1 = this.Texts[index];
                local1.Row++;
                if (((this.Texts[index].Row + 1) * this.RowHeight) > this.ClientHeight)
                {
                    this.MultiPage = true;
                    this.Texts[index].Row = 0;
                    this.currentRow = 0;
                    this.PageIndexs.Add(index + 1);
                }
                return false;
            }
            text = new SimpleText {
                Text = str.Substring(length - 1),
                TextColor = this.Texts[index].TextColor,
                Row = this.Texts[index].Row + 1,
                //TextTexture = this.Builder.CreateTextTexture(text.Text)
                //TextTexture = this.Builder.CreateTextTexture(str.Substring(length - 1))
            };
            this.Texts.Insert(index + 1, text);
            if (((text.Row + 1) * this.RowHeight) > this.ClientHeight)
            {
                this.MultiPage = true;
                text.Row = 0;
                this.currentRow = 0;
                this.PageIndexs.Add(index + 1);
            }
            this.Texts[index].Text = str.Substring(0, length - 1);
            //this.Texts[index].TextTexture = this.Builder.CreateTextTexture(this.Texts[index].Text);
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, float Depth)
        {
            if (this.PageIndexs.Count > 0)
            {
                for (int i = this.PageIndexs[this.currentPageIndex]; i < this.CurrentPageEndIndex; i++)
                {
                    if (!this.Texts[i].NewLine)  // && (this.Texts[i].TextTexture != null))
                    {
                        //Rectangle? sourceRectangle = null;
                        //spriteBatch.Draw(this.Texts[i].TextTexture, this.TextDisplayPosition(i), sourceRectangle, this.TextDisplayColor(i), 0f, Vector2.Zero, SpriteEffects.None, Depth);

                        var pos = new Vector2(this.TextDisplayPosition(i).X, this.TextDisplayPosition(i).Y);

                        CacheManager.DrawString(Session.Current.Font, this.Texts[i].Text, pos, this.TextDisplayColor(i), 0f, Vector2.Zero, Builder.Scale, SpriteEffects.None, Depth);

                    }
                }
            }
        }

        public void FirstPage()
        {
            this.currentPageIndex = 0;
        }

        public void NextPage()
        {
            if (this.currentPageIndex < (this.PageIndexs.Count - 1))
            {
                this.currentPageIndex++;
            }
        }

        private void RepositionTexts()
        {
            int row = 0;
            int x = 0;
            for (int i = 0; i < this.Texts.Count; i++)
            {
                if (!this.Texts[i].NewLine)
                {
                    if (row != this.Texts[i].Row)
                    {
                        x = 0;
                    }
                    this.Texts[i].TextPosition = new Rectangle(x, this.Texts[i].Row * this.RowHeight, this.Texts[i].Width, this.Texts[i].Height);
                    x += this.Texts[i].Width;
                    row = this.Texts[i].Row;
                }
            }
        }

        public void ResortTexts()
        {
            if ((this.Texts.Count != 0) && ((this.ClientWidth != 0) && (this.ClientHeight != 0)))
            {
                this.MultiPage = false;
                this.currentPageIndex = 0;
                this.PageIndexs.Clear();
                this.PageIndexs.Add(0);
                this.BuildTextTextures();
                this.currentRow = 0;
                this.BuildRow(0);
                if (this.OnePage && (this.PageIndexs.Count > 1))
                {
                    this.Texts.RemoveRange(this.PageIndexs[1], this.Texts.Count - this.PageIndexs[1]);
                }
                this.RepositionTexts();
            }
        }

        public void SetGameObjectTextBranch(GameObject gameObject, GameObjectTextBranch branch)
        {
            if ((branch != null) && (branch.Leaves.Count != 0))
            {
                this.Clear();
                foreach (GameObjectTextLeaf leaf in branch.Leaves)
                {
                    if ((gameObject != null) && (leaf.Property != ""))
                    {
                        this.AddText(StaticMethods.GetPropertyValue(gameObject, leaf.Property).ToString(), leaf.TextColor);
                    }
                    else
                    {
                        this.AddText(leaf.Text, leaf.TextColor);
                    }
                }
                this.ResortTexts();
            }
        }

        private Color TextDisplayColor(int index)
        {
            return ((this.Texts[index].TextColor == new Color()) ? this.DefaultColor : this.Texts[index].TextColor);
        }

        private Rectangle TextDisplayPosition(int index)
        {
            return new Rectangle(this.Texts[index].TextPosition.X + this.DisplayOffset.X, this.Texts[index].TextPosition.Y + this.DisplayOffset.Y, this.Texts[index].TextPosition.Width, this.Texts[index].TextPosition.Height);
        }

        public int TopAddGameObjectTextBranch(GameObject gameObject, GameObjectTextBranch branch)
        {
            if (branch != null)
            {
                if (branch.Leaves.Count == 0)
                {
                    return 0;
                }
                for (int i = branch.Leaves.Count - 1; i >= 0; i--)
                {
                    GameObjectTextLeaf leaf = branch.Leaves[i];
                    if ((gameObject != null) && (leaf.Property != ""))
                    {
                        this.AddText(0, StaticMethods.GetPropertyValue(gameObject, leaf.Property).ToString(), leaf.TextColor);
                    }
                    else
                    {
                        this.AddText(0, leaf.Text, leaf.TextColor);
                    }
                }
                this.ResortTexts();
                foreach (SimpleText text in this.Texts)
                {
                    if (text.NewLine)
                    {
                        return text.Row;
                    }
                }
            }
            return 0;
        }

        private int CurrentPageEndIndex
        {
            get
            {
                return ((this.PageIndexs.Count > (this.currentPageIndex + 1)) ? this.PageIndexs[this.currentPageIndex + 1] : this.Texts.Count);
            }
            set
            {
                //this = value;
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                return this.currentPageIndex;
            }
        }

        public int PageCount
        {
            get
            {
                return this.PageIndexs.Count;
            }
        }

        public int RealHeight
        {
            get
            {
                if (this.PageCount == 1)
                {
                    return (this.RowHeight * (this.Texts[this.Texts.Count - 1].Row + 1));
                }
                return this.ClientHeight;
            }
        }

        public int RowHeight
        {
            get
            {
                if (this.Texts != null && this.Texts.Count > 0)
                {
                    return this.Texts[0].Height;
                }
                else
                {
                    return 0;
                }
                //if (this.Texts.Count > 0 && this.Texts[0].TextTexture != null)
                //{
                //    return (this.Texts[0].TextTexture.Height + this.RowMargin);
                //}
                //return (int) this.Builder.font.Size;
            }
        }
    }
}

