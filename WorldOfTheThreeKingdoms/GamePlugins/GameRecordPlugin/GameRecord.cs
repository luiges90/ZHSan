using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GameRecordPlugin
{

    public class GameRecord : Tool
    {
        private Color ButtonDrawingColor = Color.White;
        private DateTime ButtonDrawingTime;
        private bool isRecordShowing;
        public string PopSoundFile;
        private List<Rectangle> PositionRectangles = new List<Rectangle>();
        private List<Point> Positions = new List<Point>();
        public FreeRichText Record = new FreeRichText();
        public Rectangle RecordBackgroundClient;
        public Texture2D RecordBackgroundTexture;
        private Point RecordDisplayOffset;
        public ShowPosition RecordShowPosition;
        private Screen screen;
        public GameObjectTextTree TextTree = new GameObjectTextTree();
        public Rectangle ToolClient;
        public Texture2D ToolDisplayTexture;
        public Texture2D ToolSelectedTexture;
        public Texture2D ToolTexture;

        private bool isRecord1Showing=false;
        public Rectangle Tool1Client;
        public Texture2D Tool1DisplayTexture;
        public Texture2D Tool1SelectedTexture;
        public Texture2D Tool1Texture;


        public void AddBranch(GameObject gameObject, string branchName, Point position)
        {
            int num = this.Record.TopAddGameObjectTextBranch(gameObject, this.TextTree.GetBranch(branchName));
            this.Positions.Insert(0, position);
            this.MoveRectangleDown(this.Record.RowHeight * num);
            int index = this.Record.ClientHeight / this.Record.RowHeight;
            if (index < this.Positions.Count)
            {
                this.Positions.RemoveRange(index, this.Positions.Count - index);
                this.PositionRectangles.RemoveRange(index, this.PositionRectangles.Count - index);
            }
            this.screen.PlayNormalSound(this.PopSoundFile);
            this.ButtonDrawingColor = Color.Lime;
            this.ButtonDrawingTime = DateTime.Now;
        }

        public void AddDisableRects()
        {
            this.screen.AddDisableRectangle(this.screen.LaterMouseEventDisableRects, this.RecordDisplayPosition);
            this.screen.AddDisableRectangle(this.screen.SelectingDisableRects, this.RecordDisplayPosition);
        }

        public void Clear()
        {
            this.Record.Clear();
            this.Positions.Clear();
            this.PositionRectangles.Clear();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.ToolDisplayTexture, this.ToolDisplayPosition, sourceRectangle, this.ButtonDrawingColor, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            spriteBatch.Draw(this.Tool1DisplayTexture, this.Tool1DisplayPosition, sourceRectangle, this.ButtonDrawingColor, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            if (this.IsRecordShowing)
            {
                spriteBatch.Draw(this.RecordBackgroundTexture, this.RecordDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.102f);
                this.Record.Draw(spriteBatch, 0.101f);
            }

            if (this.IsRecord1Showing)
            {
                 Record.DisplayOffset = new Point(500, 100);
                spriteBatch.Draw(this.RecordBackgroundTexture, new Rectangle(500, 100, 500, 600), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                Record.Draw(spriteBatch, 0.0999f);
            }

        }

        private Rectangle GetPositionRectangle(int i)
        {
            return new Rectangle(this.PositionRectangles[i].X + this.RecordDisplayOffset.X, this.PositionRectangles[i].Y + this.RecordDisplayOffset.Y, this.PositionRectangles[i].Width, this.PositionRectangles[i].Height);
        }
        private Rectangle GetPositionRectangle1(int i)
        {
            return new Rectangle(this.PositionRectangles[i].X + 500, this.PositionRectangles[i].Y + 100, this.PositionRectangles[i].Width, this.PositionRectangles[i].Height);
        }

        public void Initialize(Screen screen)
        {
            this.screen = screen;
            this.Record.OnePage = true;
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
        }

        private void MoveRectangleDown(int height)
        {
            for (int i = 0; i < this.PositionRectangles.Count; i++)
            {
                this.PositionRectangles[i] = new Rectangle(this.PositionRectangles[i].X, this.PositionRectangles[i].Y + height, this.PositionRectangles[i].Width, this.PositionRectangles[i].Height);
            }
            this.PositionRectangles.Insert(0, new Rectangle(this.RecordBackgroundClient.X, this.RecordBackgroundClient.Y, this.RecordBackgroundClient.Width, height));
        }

        public void RemoveDisableRects()
        {
            this.screen.RemoveDisableRectangle(this.screen.LaterMouseEventDisableRects, this.RecordDisplayPosition);
            this.screen.RemoveDisableRectangle(this.screen.SelectingDisableRects, this.RecordDisplayPosition);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.IsDrawing)
            {
                if (StaticMethods.PointInRectangle(position, this.ToolDisplayPosition))
                {
                    this.IsRecordShowing = !this.IsRecordShowing;
                    if (this.IsRecordShowing)
                        this.IsRecord1Showing = false;
                }

                if (StaticMethods.PointInRectangle(position, this.Tool1DisplayPosition))
                {
                    this.IsRecord1Showing = !this.IsRecord1Showing;
                    if (this.IsRecord1Showing)
                        this.IsRecordShowing = false;
                }

                if (this.IsRecordShowing )
                {
                    for (int i = 0; i < this.PositionRectangles.Count; i++)
                    {
                        if (StaticMethods.PointInRectangle(position, this.GetPositionRectangle(i)) && position.Y<screen.viewportSize.Y-48)
                        {
                            this.screen.JumpTo(this.Positions[i]);
                            break;
                        }
                    }
                }
                if (this.IsRecord1Showing)
                {
                    for (int i = 0; i < this.PositionRectangles.Count; i++)
                    {
                        if (StaticMethods.PointInRectangle(position, this.GetPositionRectangle1(i)) && position.Y < screen.viewportSize.Y - 48)
                        {
                            this.screen.JumpTo(this.Positions[i]);
                            break;
                        }
                    }
                }

            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.Enabled)
            {
                if (!this.IsRecordShowing)
                {
                    if (StaticMethods.PointInRectangle(position, this.ToolDisplayPosition))
                    {
                        this.screen.ResetMouse();
                        this.ToolDisplayTexture = this.ToolSelectedTexture;
                    }
                    else
                    {
                        this.ToolDisplayTexture = this.ToolTexture;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.RecordDisplayPosition))
                {
                    this.screen.ResetMouse();
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if ((base.IsDrawing && this.IsRecordShowing) && StaticMethods.PointInRectangle(position, this.RecordDisplayPosition))
            {
                this.IsRecordShowing = false;
                this.ToolDisplayTexture = this.ToolTexture;
            }
        }

        public void SetDisplayOffset(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            if (rectDes != Rectangle.Empty)
            {
                Rectangle recordBackgroundClient = this.RecordBackgroundClient;
                switch (showPosition)
                {
                    case ShowPosition.Center:
                        recordBackgroundClient = StaticMethods.GetCenterRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.Top:
                        recordBackgroundClient = StaticMethods.GetTopRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.Left:
                        recordBackgroundClient = StaticMethods.GetLeftRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.Right:
                        recordBackgroundClient = StaticMethods.GetRightRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.Bottom:
                        recordBackgroundClient = StaticMethods.GetBottomRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.TopLeft:
                        recordBackgroundClient = StaticMethods.GetTopLeftRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.TopRight:
                        recordBackgroundClient = StaticMethods.GetTopRightRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.BottomLeft:
                        recordBackgroundClient = StaticMethods.GetBottomLeftRectangle(rectDes, recordBackgroundClient);
                        break;

                    case ShowPosition.BottomRight:
                        recordBackgroundClient = StaticMethods.GetBottomRightRectangle(rectDes, recordBackgroundClient);
                        break;
                }
                this.RecordDisplayOffset = new Point(recordBackgroundClient.X, recordBackgroundClient.Y);
                this.Record.DisplayOffset = this.RecordDisplayOffset;
            }
        }

        public override void Update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.ButtonDrawingTime);
            if (span.Milliseconds >= 300)
            {
                this.ButtonDrawingColor = Color.White;
            }
        }

        public bool IsRecordShowing
        {
            get
            {
                return this.isRecordShowing;
            }
            set
            {
                this.isRecordShowing = value;
                if (value)
                {
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.SetDisplayOffset(this.RecordShowPosition);
                    this.AddDisableRects();
                }
                else
                {
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.RemoveDisableRects();
                }
            }
        }

        private Rectangle RecordDisplayPosition
        {
            get
            {
                return new Rectangle(this.RecordDisplayOffset.X + this.RecordBackgroundClient.X, this.RecordDisplayOffset.Y + this.RecordBackgroundClient.Y, this.RecordBackgroundClient.Width, this.RecordBackgroundClient.Height);
            }
        }

        private Rectangle ToolDisplayPosition
        {
            get
            {
                return new Rectangle(this.ToolClient.X + this.DisplayOffset.X, this.ToolClient.Y + this.DisplayOffset.Y, this.ToolClient.Width, this.ToolClient.Height);
            }
        }

        public bool IsRecord1Showing
        {
            get
            {
                return this.isRecord1Showing;
            }
            set
            {
                this.isRecord1Showing = value;
            }
        }

        private Rectangle Tool1DisplayPosition
        {
            get
            {
                return new Rectangle(this.Tool1Client.X + this.DisplayOffset.X, this.Tool1Client.Y + this.DisplayOffset.Y, this.Tool1Client.Width, this.Tool1Client.Height);
            }
        }
    }
}

