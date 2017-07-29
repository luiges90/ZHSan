using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;

namespace MapViewSelectorPlugin
{

    internal class MapViewSelector
    {
        internal Point BackgroundSize;
        internal PlatformTexture ButtonSelectedTexture;
        internal PlatformTexture ButtonTexture;
        internal bool CancelButtonSelected;
        internal FreeText CancelButtonText;
        internal Point DisplayOffset;
        private bool dragging;
        internal IGameFrame iGameFrame;
        private bool isShowing;
        internal ITabList iTabList;
        internal PlatformTexture ItemInListTexture;
        internal PlatformTexture ItemSelectedTexture;
        internal MapViewSelectorKind Kind;
        internal bool MultiSelecting;
        internal bool OKButtonSelected;
        internal FreeText OKButtonText;
        internal GameDelegates.VoidFunction OKFunction;
        internal bool ReturnToListButtonSelected;
        internal FreeText ReturnToListButtonText;
        
        internal GameObjectList SelectingGameObjectList;
        internal Point TitleSize;
        internal PlatformTexture TitleTexture;

        internal void AddDisableRects()
        {
            Session.MainGame.mainGameScreen.AddDisableRectangle(Session.MainGame.mainGameScreen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
        }

        public void Draw()
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.TitleTexture, this.TitleDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.179f);
            this.ReturnToListButtonText.Draw(0.1797f);
            if (this.ReturnToListButtonSelected)
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonSelectedTexture, this.ReturnToListButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonTexture, this.ReturnToListButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            this.OKButtonText.Draw(0.1797f);
            if (this.OKButtonSelected)
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonSelectedTexture, this.OKButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonTexture, this.OKButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            this.CancelButtonText.Draw(0.1797f);
            if (this.CancelButtonSelected)
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonSelectedTexture, this.CancelButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.ButtonTexture, this.CancelButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            if (this.Kind == MapViewSelectorKind.建筑)
            {
                foreach (Architecture architecture in this.SelectingGameObjectList)
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        if (Session.MainGame.mainGameScreen.TileInScreen(point))
                        {
                            if (architecture.Selected)
                            {
                                sourceRectangle = null;
                                CacheManager.Draw(this.ItemSelectedTexture, Session.MainGame.mainGameScreen.GetDestination(point), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
                            }
                            else
                            {
                                sourceRectangle = null;
                                CacheManager.Draw(this.ItemInListTexture, Session.MainGame.mainGameScreen.GetDestination(point), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
                            }
                        }
                    }
                }
            }
        }

        internal void Initialize()
        {
            
        }

        internal void RemoveDisableRects()
        {
            Session.MainGame.mainGameScreen.RemoveDisableRectangle(Session.MainGame.mainGameScreen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
        }

        private void screen_OnMouseLeftDown(Point position)
        {

        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (!StaticMethods.PointInRectangle(position, this.TitleDisplayPosition))
            {
                if (this.ReturnToListButtonSelected)
                {
                    this.IsShowing = false;
                }
                else if (this.OKButtonSelected)
                {
                    this.IsShowing = false;
                    if (this.OKFunction != null)
                    {
                        this.OKFunction();
                        this.OKFunction = null;
                    }
                    this.iGameFrame.OK();

                }
                else if (this.CancelButtonSelected)
                {
                    this.IsShowing = false;
                    this.iGameFrame.Cancel();
                }
                else
                {
                    Point positionByPoint = Session.MainGame.mainGameScreen.GetPositionByPoint(position);
                    if (Session.MainGame.mainGameScreen.TileInScreen(positionByPoint) && (this.Kind == MapViewSelectorKind.建筑))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(positionByPoint);
                        if ((architectureByPosition != null) && this.SelectingGameObjectList.HasGameObject(architectureByPosition.ID))
                        {
                            architectureByPosition.Selected = !architectureByPosition.Selected;
                            if (!this.MultiSelecting && architectureByPosition.Selected)
                            {
                                this.SelectingGameObjectList.SetOtherUnSelected(architectureByPosition);
                                if (Session.GlobalVariables.SingleSelectionOneClick)
                                {
                                    this.iTabList.RefreshEditable();
                                    this.IsShowing = false;
                                    this.iGameFrame.OK();
                                    return;
                                }
                            }
                            this.iTabList.RefreshEditable();
                        }
                    }
                }
            }

            ///////////////////////////////////////////////////////////////////
            this.dragging = false;
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            this.ReturnToListButtonSelected = false;
            this.OKButtonSelected = false;
            this.CancelButtonSelected = false;
            if (StaticMethods.PointInRectangle(position, this.TitleDisplayPosition))
            {
                this.dragging = leftDown;
            }
            else if (StaticMethods.PointInRectangle(position, this.ReturnToListButtonText.DisplayPosition))
            {
                this.ReturnToListButtonSelected = true;
            }
            else if (StaticMethods.PointInRectangle(position, this.OKButtonText.DisplayPosition))
            {
                this.OKButtonSelected = true;
            }
            else if (StaticMethods.PointInRectangle(position, this.CancelButtonText.DisplayPosition))
            {
                this.CancelButtonSelected = true;
            }
            if (this.dragging)
            {
                this.RemoveDisableRects();
                this.DisplayOffset = new Point(this.DisplayOffset.X + Session.MainGame.mainGameScreen.MouseOffset.X, this.DisplayOffset.Y + Session.MainGame.mainGameScreen.MouseOffset.Y);
                this.ReturnToListButtonText.DisplayOffset = this.DisplayOffset;
                this.OKButtonText.DisplayOffset = this.DisplayOffset;
                this.CancelButtonText.DisplayOffset = this.DisplayOffset;
                this.AddDisableRects();
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            this.IsShowing = false;
            this.iGameFrame.Cancel();
        }

        internal void SetDisplayOffset(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y);
            int num = 0;
            num += this.ReturnToListButtonText.Position.Height;
            num += this.OKButtonText.Position.Height;
            num += this.CancelButtonText.Position.Height;
            Rectangle rect = new Rectangle(0, 0, this.TitleSize.X, this.TitleSize.Y + num);
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
            this.ReturnToListButtonText.DisplayOffset = this.DisplayOffset;
            this.OKButtonText.DisplayOffset = this.DisplayOffset;
            this.CancelButtonText.DisplayOffset = this.DisplayOffset;
        }

        public void Update()
        {
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
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
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.MapViewSelector, UndoneWorkSubKind.None));
                    Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.AddDisableRects();
                }
                else
                {
                    if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.MapViewSelector)
                    {
                        throw new Exception("The UndoneWork is not a MapViewSelector Dialog.");
                    }
                    Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.RemoveDisableRects();
                }
            }
        }

        private Rectangle TitleDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.TitleSize.X, this.TitleSize.Y);
            }
        }
    }
}

