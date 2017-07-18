using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TreasureDetailPlugin
{

    public class TreasureDetail
    {
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal Rectangle DescriptionClient;
        internal FreeRichText DescriptionText = new FreeRichText();
        private Point DisplayOffset;
        internal Rectangle InfluenceClient;
        internal FreeRichText InfluenceText = new FreeRichText();
        private bool isShowing;
        internal List<LabelText> LabelTexts = new List<LabelText>();
        internal Rectangle PictureClient;
        internal Screen screen;
        internal Treasure ShowingTreasure;

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (this.ShowingTreasure != null)
            {
                Rectangle? sourceRectangle = null;
                spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);

                if (this.ShowingTreasure.Picture != null)
                {
                    spriteBatch.Draw(this.ShowingTreasure.Picture, this.PictureClientDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
                }

                foreach (LabelText text in this.LabelTexts)
                {
                    text.Label.Draw(spriteBatch, 0.1999f);
                    text.Text.Draw(spriteBatch, 0.1999f);
                }
                this.DescriptionText.Draw(spriteBatch, 0.1999f);
                this.InfluenceText.Draw(spriteBatch, 0.1999f);
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.InfluenceClientDisplayPosition))
            {
                if (this.InfluenceText.CurrentPageIndex < (this.InfluenceText.PageCount - 1))
                {
                    this.InfluenceText.NextPage();
                }
                else if (this.InfluenceText.CurrentPageIndex == (this.InfluenceText.PageCount - 1))
                {
                    this.InfluenceText.FirstPage();
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
        }

        private void screen_OnMouseRightUp(Point position)
        {
            this.IsShowing = false;
        }

        internal void SetPosition(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
            switch (showPosition)
            {
                case ShowPosition.Center:
                    rect = StaticMethods.GetCenterRectangle(rectDes, rect);
                    break;

                case ShowPosition.Top:
                    rect = StaticMethods.GetTopRectangle(rectDes, rect);
                    break;

                case ShowPosition.Left:
                    rect = StaticMethods.GetLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.Right:
                    rect = StaticMethods.GetRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.Bottom:
                    rect = StaticMethods.GetBottomRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopLeft:
                    rect = StaticMethods.GetTopLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopRight:
                    rect = StaticMethods.GetTopRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomLeft:
                    rect = StaticMethods.GetBottomLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomRight:
                    rect = StaticMethods.GetBottomRightRectangle(rectDes, rect);
                    break;
            }
            this.DisplayOffset = new Point(rect.X, rect.Y);
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.DescriptionText.DisplayOffset = new Point(this.DisplayOffset.X + this.DescriptionClient.X, this.DisplayOffset.Y + this.DescriptionClient.Y);
            this.InfluenceText.DisplayOffset = new Point(this.DisplayOffset.X + this.InfluenceClient.X, this.DisplayOffset.Y + this.InfluenceClient.Y);
        }

        internal void SetTreasure(Treasure treasure)
        {
            this.ShowingTreasure = treasure;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(treasure, text.PropertyName).ToString();
            }
            this.DescriptionText.AddText("简介", this.DescriptionText.TitleColor);
            this.DescriptionText.AddNewLine();
            this.DescriptionText.AddText(this.ShowingTreasure.Description, this.DescriptionText.DefaultColor);
            this.DescriptionText.AddNewLine();
            this.DescriptionText.ResortTexts();
            this.InfluenceText.AddText("效果", this.DescriptionText.TitleColor);
            this.InfluenceText.AddNewLine();
            foreach (Influence influence in this.ShowingTreasure.Influences.Influences.Values)
            {
                this.InfluenceText.AddText(influence.Description, this.DescriptionText.SubTitleColor);
                this.InfluenceText.AddNewLine();
            }
            this.InfluenceText.ResortTexts();
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle InfluenceClientDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.InfluenceClient.X, this.DisplayOffset.Y + this.InfluenceClient.Y, this.InfluenceClient.Width, this.InfluenceClient.Height);
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                this.isShowing = value;
                if (value)
                {
                    this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.TreasureDetail));
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                }
                else
                {
                    if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.SubDialog)
                    {
                        throw new Exception("The UndoneWork is not a SubDialog.");
                    }
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.DescriptionText.Clear();
                    this.InfluenceText.Clear();
                }
            }
        }

        private Rectangle PictureClientDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.PictureClient.X, this.DisplayOffset.Y + this.PictureClient.Y, this.PictureClient.Width, this.PictureClient.Height);
            }
        }
    }
}

