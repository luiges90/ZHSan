using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;

namespace AirViewPlugin
{
    public class AirView : Tool
    {
#pragma warning disable CS0649 // Field 'AirView.ArchitectureTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture ArchitectureTexture;
#pragma warning restore CS0649 // Field 'AirView.ArchitectureTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture ArchitectureUnitTexture;
        internal FreeText Conment;
        internal PlatformTexture ConmentBackgroundTexture;
        internal int DefaultTileLength;
        private Rectangle framePosition;
        internal PlatformTexture FrameTexture;
        private bool isMapShowing;
        private bool isPreparedToJump = false;
        private Point MapDisplayOffset;
        internal int MapMaxHeight;
        internal int MapMaxWidth;
        internal ShowPosition MapShowPosition = ShowPosition.BottomRight;
        private Point mapSize;
        internal PlatformTexture MapTexture;
        
        internal int TileLength;
        internal int TileLengthMax;
        internal PlatformTexture ToolDisplayTexture;
        internal Rectangle ToolPosition;
        internal PlatformTexture ToolSelectedTexture;
        internal PlatformTexture ToolTexture;

        internal PlatformTexture TroopToolDisplayTexture;
        internal Rectangle TroopToolPosition;
        internal PlatformTexture TroopToolSelectedTexture;
        internal PlatformTexture TroopToolTexture;

        internal float Transparent = 1f;
#pragma warning disable CS0649 // Field 'AirView.TroopTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture TroopTexture;
#pragma warning restore CS0649 // Field 'AirView.TroopTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture TroopFactionColorTexture;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 240;
        bool drawTroopFlag = true;
        bool showTroop = true;

        internal void AddDisableRects()
        {
            Session.MainGame.mainGameScreen.AddDisableRectangle(Session.MainGame.mainGameScreen.LaterMouseEventDisableRects, this.MapPosition);
            Session.MainGame.mainGameScreen.AddDisableRectangle(Session.MainGame.mainGameScreen.SelectingDisableRects, this.MapPosition);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.ToolDisplayTexture, this.ToolDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            if (this.IsMapShowing)
            {
                CacheManager.Draw(this.TroopToolDisplayTexture, this.TroopToolDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

                if (this.MapTexture != null)
                {
                    sourceRectangle = null;
                    CacheManager.Draw(this.MapTexture, this.MapPosition, sourceRectangle, new Color(1f, 1f, 1f, this.Transparent), 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
                }
                /*
                if (this.TroopTexture != null)
                {
                    sourceRectangle = null;
                    CacheManager.Draw(this.TroopTexture, this.MapPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.09999f);
                }
                */
                if (this.showTroop)
                {
                    timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFrame > millisecondsPerFrame)
                    {
                        //timeSinceLastFrame -= millisecondsPerFrame;
                        timeSinceLastFrame = 0;
                        this.drawTroopFlag = !this.drawTroopFlag;
                    }
                    if (this.drawTroopFlag)
                    {
                        this.drawTroop( gameTime);
                    }
                }
                foreach (Architecture architecture in Session.Current.Scenario.Architectures)
                {
                    Color white = Color.White;
                    if (architecture.BelongedFaction != null)
                    {
                        white = architecture.BelongedFaction.FactionColor;
                    }
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        sourceRectangle = null;
                        CacheManager.Draw(this.ArchitectureUnitTexture, new Rectangle(((point.X * this.TileLength) + this.MapDisplayOffset.X) - 1, ((point.Y * this.TileLength) + this.MapDisplayOffset.Y) - 1, this.TileLength + 2, this.TileLength + 2), sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.09999f);
                    }
                }
                sourceRectangle = null;
                //CacheManager.Draw(this.FrameTexture, this.FrameDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0998f);
                CacheManager.Draw(this.FrameTexture, this.frameTopPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0998f);
                CacheManager.Draw(this.FrameTexture, this.frameLeftPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0998f);
                CacheManager.Draw(this.FrameTexture, this.frameBottomPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0998f);
                CacheManager.Draw(this.FrameTexture, this.frameRightPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0998f);
                if (this.Conment.Text != "")
                {
                    CacheManager.Draw(this.ConmentBackgroundTexture, this.Conment.AlignedPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.09999f);
                    this.Conment.Draw(0.0999f);
                }
            }
        }

        private void renderTroop(GameTime gameTime, Troop troop)
        {
            Color color = Color.White;
            if (troop.Destroyed) return;
            if (troop.Status == TroopStatus.埋伏) return;
            if (troop.BelongedFaction != null)
            {
                color = troop.BelongedFaction.FactionColor;
            }
            CacheManager.Draw(TroopFactionColorTexture, 
                new Rectangle(troop.Position.X * this.TileLength + this.MapDisplayOffset.X - 1,
                            troop.Position.Y * this.TileLength + this.MapDisplayOffset.Y - 1, this.TileLength * 4, this.TileLength * 4), 
                            null, color, 0f, Vector2.Zero, SpriteEffects.None, 0.09998f);
        }

        private void drawTroop(GameTime gameTime)
        {
            if (Session.GlobalVariables.SkyEye)
            {
                foreach (Faction f in Session.Current.Scenario.Factions)
                {
                    foreach (Troop t in f.GetVisibleTroops())
                    {
                        renderTroop( gameTime, t);
                    }
                }
            }
            else if (Session.Current.Scenario.CurrentPlayer != null)
            {
                foreach (Troop t in Session.Current.Scenario.CurrentPlayer.GetVisibleTroops())
                {
                    renderTroop( gameTime, t);
                }
            }
        }

        private Point GetTranslatedPosition(Point position)
        {
            int num = position.X - this.MapDisplayOffset.X;
            int num2 = position.Y - this.MapDisplayOffset.Y;
            return new Point((Session.Current.Scenario.ScenarioMap.MapDimensions.X * num) / this.mapSize.X, (Session.Current.Scenario.ScenarioMap.MapDimensions.Y * num2) / this.mapSize.Y);
        }

        internal void Initialize(Screen screen)
        {
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
        }

        private void JumpTo(Point position)
        {
            if (this.isPreparedToJump)
            {
                Session.MainGame.mainGameScreen.JumpTo(this.GetTranslatedPosition(position));
            }
        }

        internal void RemoveDisableRects()
        {
            Session.MainGame.mainGameScreen.RemoveDisableRectangle(Session.MainGame.mainGameScreen.LaterMouseEventDisableRects, this.MapPosition);
            Session.MainGame.mainGameScreen.RemoveDisableRectangle(Session.MainGame.mainGameScreen.SelectingDisableRects, this.MapPosition);
        }

        internal void ResetFramePosition(Point viewportSize, int leftEdge, int topEdge, Point totalMapSize)
        {
            if ((this.framePosition.Width + this.framePosition.Height) == 0)
            {
                this.ResetFrameSize(viewportSize, totalMapSize);
            }
            int x = ((((viewportSize.X / 2) - leftEdge) * this.mapSize.X) / totalMapSize.X) - (this.framePosition.Width / 2);
            int y = ((((viewportSize.Y / 2) - topEdge) * this.mapSize.Y) / totalMapSize.Y) - (this.framePosition.Height / 2);
            if (x < 0)
            {
                x = 0;
            }
            if ((x + this.framePosition.Width) > this.mapSize.X)
            {
                x = this.mapSize.X - this.framePosition.Width;
            }
            if (y < 0)
            {
                y = 0;
            }
            if ((y + this.framePosition.Height) > this.mapSize.Y)
            {
                y = this.mapSize.Y - this.framePosition.Height;
            }
            this.framePosition = new Rectangle(x, y, this.framePosition.Width, this.framePosition.Height);
        }

        internal void ResetFrameSize(Point viewportSize, Point totalMapSize)
        {
            int width = (this.mapSize.X * viewportSize.X) / totalMapSize.X;
            int height = (this.mapSize.Y * viewportSize.Y) / totalMapSize.Y;
            this.framePosition = new Rectangle(0, 0, width, height);
        }

        private void ResetMapSize()
        {
            int x = Session.Current.Scenario.ScenarioMap.MapDimensions.X;
            int y = Session.Current.Scenario.ScenarioMap.MapDimensions.Y;
            if (x > y)
            {
                if (x > this.MapMaxWidth)
                {
                    y = (y * this.MapMaxWidth) / x;
                    x = this.MapMaxWidth;
                }
                if (y > this.MapMaxHeight)
                {
                    x = (x * this.MapMaxHeight) / y;
                    y = this.MapMaxHeight;
                }
            }
            else
            {
                if (y > this.MapMaxHeight)
                {
                    x = (x * this.MapMaxHeight) / y;
                    y = this.MapMaxHeight;
                }
                if (x > this.MapMaxWidth)
                {
                    y = (y * this.MapMaxWidth) / x;
                    x = this.MapMaxWidth;
                }
            }
            if ((x < this.MapMaxWidth) && (y < this.MapMaxHeight))
            {
                int tileLengthMax = this.MapMaxWidth / x;
                int num4 = this.MapMaxHeight / y;
                if (tileLengthMax < num4)
                {
                    tileLengthMax = num4;
                }
                if (tileLengthMax > this.TileLengthMax)
                {
                    tileLengthMax = this.TileLengthMax;
                }
                this.TileLength = tileLengthMax;
            }
            this.mapSize = new Point(x * this.TileLength, y * this.TileLength);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.IsDrawing)
            {
                if (StaticMethods.PointInRectangle(position, this.ToolDisplayPosition))
                {
                    if (InputManager.IsDownPre == false)
                    {
                        this.IsMapShowing = !this.IsMapShowing;
                    }
                    if (base.Enabled)
                    {
                        if (this.IsMapShowing)
                        {
                                this.ToolDisplayTexture = this.ToolSelectedTexture;
                        }
                        else
                        {
                            this.ToolDisplayTexture = this.ToolTexture;
                        }
                    }
                }
                else if (this.IsMapShowing && StaticMethods.PointInRectangle(position, this.TroopToolDisplayPosition))
                {
                    if (InputManager.IsDownPre == false)
                    {
                        this.showTroop = !this.showTroop;
                    }
                    if (this.showTroop)
                    {
                        this.TroopToolDisplayTexture = this.TroopToolSelectedTexture;
                    }
                    else
                    {
                        this.TroopToolDisplayTexture = this.TroopToolTexture;
                    }
                }
                else if (this.IsMapShowing && StaticMethods.PointInRectangle(position, this.MapPosition))
                {
                    this.JumpTo(position);
                    Session.MainGame.mainGameScreen.cloudLayer.Start();
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.IsDrawing && !Session.MainGame.mainGameScreen.DrawingSelector)
            {
                if (!this.IsMapShowing)
                {
                    if (base.Enabled)
                    {

                            this.ToolDisplayTexture = this.ToolTexture;

                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.MapPosition))
                {
                    this.isPreparedToJump = true;
                    Session.MainGame.mainGameScreen.ResetMouse();
                    if (leftDown)
                    {
                        this.JumpTo(position);
                        Session.MainGame.mainGameScreen.cloudLayer.Start();
                    }
                    else if (this.isPreparedToJump)
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.GetTranslatedPosition(position));
                        if (architectureByPosition != null)
                        {
                            this.Conment.DisplayOffset = position;
                            this.Conment.Text = architectureByPosition.Name + " " + architectureByPosition.FactionString;
                        }
                        else
                        {
                            this.Conment.Text = "";
                        }
                    }
                }
                else
                {
                    this.isPreparedToJump = false;
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if ((base.IsDrawing && this.IsMapShowing) && StaticMethods.PointInRectangle(position, this.MapPosition))
            {
                this.IsMapShowing = false;
                this.ToolDisplayTexture = this.ToolTexture;
            }
        }

        internal void SetDisplayOffset(Screen screen, ShowPosition showPosition)
        {
            this.TileLength = this.DefaultTileLength;
            this.ResetMapSize();
            Rectangle rectDes = new Rectangle(0, 0, screen.viewportSize.X, screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.mapSize.X, this.mapSize.Y);
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
            this.MapDisplayOffset = new Point(rect.X, rect.Y);
        }

        public override void Update()
        {
        }

        private Rectangle FrameDisplayPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X + this.framePosition.X, this.MapDisplayOffset.Y + this.framePosition.Y, this.framePosition.Width, this.framePosition.Height);
            }
        }

        private Rectangle frameTopPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X + this.framePosition.X, this.MapDisplayOffset.Y + this.framePosition.Y, this.framePosition.Width, 2);
            }
        }

        private Rectangle frameLeftPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X + this.framePosition.X, this.MapDisplayOffset.Y + this.framePosition.Y, 2, this.framePosition.Height);
            }
        }

        private Rectangle frameBottomPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X + this.framePosition.X, this.MapDisplayOffset.Y + this.framePosition.Y + this.framePosition.Height-1, this.framePosition.Width+1, 2);
            }
        }

        private Rectangle frameRightPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X + this.framePosition.X + this.framePosition.Width-1, this.MapDisplayOffset.Y + this.framePosition.Y, 2, this.framePosition.Height+1);
            }
        }

        internal bool IsMapShowing
        {
            get
            {
                return this.isMapShowing;
            }
            set
            {
                this.isMapShowing = value;
                if (value)
                {
                    Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    //Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);

                    this.SetDisplayOffset(Session.MainGame.mainGameScreen, this.MapShowPosition);
                    this.AddDisableRects();
                }
                else
                {
                    Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    //Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.RemoveDisableRects();
                }
            }
        }

        internal Rectangle MapPosition
        {
            get
            {
                return new Rectangle(this.MapDisplayOffset.X, this.MapDisplayOffset.Y, this.mapSize.X, this.mapSize.Y);
            }
        }

        private Rectangle ToolDisplayPosition
        {
            get
            {
                return new Rectangle(this.ToolPosition.X + this.DisplayOffset.X, this.ToolPosition.Y + this.DisplayOffset.Y, this.ToolPosition.Width, this.ToolPosition.Height);
            }
        }

        private Rectangle TroopToolDisplayPosition
        {
            get
            {
                return new Rectangle(this.TroopToolPosition.X + this.DisplayOffset.X, this.TroopToolPosition.Y + this.DisplayOffset.Y, this.TroopToolPosition.Width, this.TroopToolPosition.Height);
            }
        }
    }

 

}
