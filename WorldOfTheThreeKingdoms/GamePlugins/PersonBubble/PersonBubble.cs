using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PersonBubble
{

    internal class PersonBubble
    {
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal List<Bubble> Bubbles = new List<Bubble>();
        internal Rectangle ClientPosition;
        private int count = 0;
        internal Color DefaultTextColor;
        private bool isShowing = true;
        internal Color PersonSpecialTextColor;
        internal int PersonSpecialTextTimeLast;
        internal Point PopoutOffset;
        internal Rectangle PortraitClient;
        private Screen screen;

        internal Font TextBuilder = new Font();

        internal int TextClientHeight;
        internal int TextClientWidth;
        internal int TextRowMargin;
        internal GameObjectTextTree TextTree = new GameObjectTextTree();

        internal void AddPerson(Person person, Point p, string branchName)
        {
            if (this.screen.TileInScreen(p))
            {
                Point pointByPosition = this.screen.GetPointByPosition(p);
                Bubble item = new Bubble {
                    SpeakingPerson = person,
                    Position = new Point(pointByPosition.X, pointByPosition.Y + (this.PositionCount(person) * this.BackgroundSize.Y))
                };
                GameObjectTextBranch branch = this.TextTree.GetBranch(branchName);
                if (branch != null)
                {
                    item.LastingTime = branch.Time;
                    item.RichText = new FreeRichText();  // this.TextBuilder);
                    item.RichText.DefaultColor = this.DefaultTextColor;
                    item.RichText.ClientWidth = this.TextClientWidth;
                    item.RichText.ClientHeight = this.TextClientHeight;
                    item.RichText.RowMargin = this.TextRowMargin;
                    item.RichText.DisplayOffset = this.RichTextDisplayOffset(item.Position);
                    item.RichText.SetGameObjectTextBranch(person, branch);
                }
                this.Bubbles.Add(item);
            }
        }

        internal void AddPersonText(Person person, Point p, string text)
        {
            if (this.screen.TileInScreen(p))
            {
                Point pointByPosition = this.screen.GetPointByPosition(p);
                Bubble item = new Bubble {
                    SpeakingPerson = person,
                    Position = new Point(pointByPosition.X, pointByPosition.Y + (this.PositionCount(person) * this.BackgroundSize.Y))
                };
                if (text != null)
                {
                    item.LastingTime = this.PersonSpecialTextTimeLast;
                    item.RichText = new FreeRichText();  // this.TextBuilder);
                    item.RichText.DefaultColor = this.PersonSpecialTextColor;
                    item.RichText.ClientWidth = this.TextClientWidth;
                    item.RichText.ClientHeight = this.TextClientHeight;
                    item.RichText.RowMargin = this.TextRowMargin;
                    item.RichText.DisplayOffset = this.RichTextDisplayOffset(item.Position);

                    item.RichText.AddText(text);
                    item.RichText.ResortTexts();
                }
                this.Bubbles.Add(item);
            }
        }

        private Rectangle BackgroundDisplayPosition(Point position)
        {
            return new Rectangle(position.X - this.PopoutOffset.X, position.Y - this.PopoutOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            DateTime now = DateTime.Now;
            float layerDepth = 0.45f;
            for (int i = 0; i < this.Bubbles.Count; i++)
            {
                if (!this.Bubbles[i].DrawingStarted)
                {
                    this.Bubbles[i].DrawingStarted = true;
                    this.Bubbles[i].StartingTime = now;
                }
                if (this.screen.TileInScreen(this.Bubbles[i].SpeakingPerson.Position))
                {
                    layerDepth += -1E-06f;
                    Rectangle? sourceRectangle = null;
                    spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition(this.Bubbles[i].Position), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
                    layerDepth += -1E-06f;
                    sourceRectangle = null;

                    //try
                    //{
                    //    spriteBatch.Draw(this.Bubbles[i].SpeakingPerson.SmallPortrait, this.PortraitDisplayPosition(this.Bubbles[i].Position), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
                    //}
                    //catch
                    //{

                    //}

                    CacheManager.DrawZhsanAvatar(this.Bubbles[i].SpeakingPerson, "s", this.PortraitDisplayPosition(this.Bubbles[i].Position), Color.White, layerDepth);

                    layerDepth += -1E-06f;
                    if (this.Bubbles[i].RichText != null)
                    {
                        this.Bubbles[i].RichText.Draw(spriteBatch, layerDepth);
                    }
                }
            }
        }

        private int HasPerson(Person person)
        {
            for (int i = 0; i < this.Bubbles.Count; i++)
            {
                if (this.Bubbles[i].SpeakingPerson == person)
                {
                    return i;
                }
            }
            return -1;
        }

        private int IndexOfPosition(List<PositionCount> positionCounts, Point point)
        {
            int num = -1;
            foreach (PositionCount count in positionCounts)
            {
                num++;
                if (count.Position == point)
                {
                    return num;
                }
            }
            return -1;
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private Rectangle PortraitDisplayPosition(Point position)
        {
            return new Rectangle((this.PortraitClient.X + position.X) - this.PopoutOffset.X, (this.PortraitClient.Y + position.Y) - this.PopoutOffset.Y, this.PortraitClient.Width, this.PortraitClient.Height);
        }

        private int PositionCount(Person person)
        {
            int num = 0;
            for (int i = 0; i < this.Bubbles.Count; i++)
            {
                if (this.Bubbles[i].SpeakingPerson == person)
                {
                    return num;
                }
                if (this.Bubbles[i].SpeakingPerson.Position == person.Position)
                {
                    num++;
                }
            }
            return num;
        }

        private Point RichTextDisplayOffset(Point position)
        {
            return new Point((position.X - this.PopoutOffset.X) + this.ClientPosition.X, (position.Y - this.PopoutOffset.Y) + this.ClientPosition.Y);
        }

        internal void Update(GameTime gameTime)
        {
            if ((this.count++ % 2) <= 0)
            {
                int num;
                DateTime now = DateTime.Now;
                List<int> list = new List<int>();
                List<PositionCount> positionCounts = new List<PositionCount>();
                for (num = 0; num < this.Bubbles.Count; num++)
                {
                    if (this.Bubbles[num].DrawingStarted)
                    {
                        TimeSpan span = (TimeSpan) (now - this.Bubbles[num].StartingTime);
                        if (span.TotalMilliseconds >= this.Bubbles[num].LastingTime)
                        {
                            list.Add(num);
                        }
                        else
                        {
                            Point position = this.Bubbles[num].SpeakingPerson.Position;
                            int num2 = this.IndexOfPosition(positionCounts, position);
                            if (num2 < 0)
                            {
                                positionCounts.Add(new PositionCount(position, 1));
                                this.Bubbles[num].Position = this.screen.GetPointByPosition(position);
                            }
                            else
                            {
                                this.Bubbles[num].Position = this.screen.GetPointByPosition(position);
                                this.Bubbles[num].Position = new Point(this.Bubbles[num].Position.X, this.Bubbles[num].Position.Y + (positionCounts[num2].Count * this.BackgroundSize.Y));
                                PositionCount local1 = positionCounts[num2];
                                local1.Count++;
                            }
                            if (((this.Bubbles[num].Position.X - this.PopoutOffset.X) + this.BackgroundSize.X) > this.screen.viewportSize.X)
                            {
                                this.Bubbles[num].Position.X = (this.screen.viewportSize.X - this.BackgroundSize.X) + this.PopoutOffset.X;
                            }
                            this.Bubbles[num].RichText.DisplayOffset = this.RichTextDisplayOffset(this.Bubbles[num].Position);
                        }
                    }
                }
                for (num = list.Count - 1; num >= 0; num--)
                {
                    this.Bubbles.RemoveAt(list[num]);
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
                if (value)
                {
                }
            }
        }
    }
}

