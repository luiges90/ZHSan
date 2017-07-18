using GameFreeText;
using GameGlobal;
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
        internal Texture2D ButtonSelectedTexture;
        internal Texture2D ButtonTexture;
        internal bool CancelButtonSelected;
        internal FreeText CancelButtonText;
        internal Point DisplayOffset;
        private bool dragging;
        internal IGameFrame iGameFrame;
        private bool isShowing;
        internal ITabList iTabList;
        internal Texture2D ItemInListTexture;
        internal Texture2D ItemSelectedTexture;
        internal MapViewSelectorKind Kind;
        internal bool MultiSelecting;
        internal bool OKButtonSelected;
        internal FreeText OKButtonText;
        internal GameDelegates.VoidFunction OKFunction;
        internal bool ReturnToListButtonSelected;
        internal FreeText ReturnToListButtonText;
        private Screen screen;
        internal GameObjectList SelectingGameObjectList;
        internal Point TitleSize;
        internal Texture2D TitleTexture;

        internal void AddDisableRects()
        {
            this.screen.AddDisableRectangle(this.screen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.TitleTexture, this.TitleDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.179f);
            this.ReturnToListButtonText.Draw(spriteBatch, 0.1797f);
            if (this.ReturnToListButtonSelected)
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonSelectedTexture, this.ReturnToListButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonTexture, this.ReturnToListButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            this.OKButtonText.Draw(spriteBatch, 0.1797f);
            if (this.OKButtonSelected)
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonSelectedTexture, this.OKButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonTexture, this.OKButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            this.CancelButtonText.Draw(spriteBatch, 0.1797f);
            if (this.CancelButtonSelected)
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonSelectedTexture, this.CancelButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.ButtonTexture, this.CancelButtonText.DisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1798f);
            }
            if (this.Kind == MapViewSelectorKind.建筑)
            {
                foreach (Architecture architecture in this.SelectingGameObjectList)
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        if (this.screen.TileInScreen(point))
                        {
                            if (architecture.Selected)
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(this.ItemSelectedTexture, this.screen.GetDestination(point), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
                            }
                            else
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(this.ItemInListTexture, this.screen.GetDestination(point), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18f);
                            }
                        }
                    }
                }
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        internal void RemoveDisableRects()
        {
            this.screen.RemoveDisableRectangle(this.screen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
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
                    Point positionByPoint = this.screen.GetPositionByPoint(position);
                    if (this.screen.TileInScreen(positionByPoint) && (this.Kind == MapViewSelectorKind.建筑))
                    {
                        Architecture architectureByPosition = this.screen.Scenario.GetArchitectureByPosition(positionByPoint);
                        if ((architectureByPosition != null) && this.SelectingGameObjectList.HasGameObject(architectureByPosition.ID))
                        {
                            architectureByPosition.Selected = !architectureByPosition.Selected;
                            if (!this.MultiSelecting && architectureByPosition.Selected)
                            {
                                this.SelectingGameObjectList.SetOtherUnSelected(architectureByPosition);
                                if (GlobalVariables.SingleSelectionOneClick)
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
                this.DisplayOffset = new Point(this.DisplayOffset.X + this.screen.MouseOffset.X, this.DisplayOffset.Y + this.screen.MouseOffset.Y);
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
            Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
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
                    this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.MapViewSelector, UndoneWorkSubKind.None));
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.AddDisableRects();
                }
                else
                {
                    if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.MapViewSelector)
                    {
                        throw new Exception("The UndoneWork is not a MapViewSelector Dialog.");
                    }
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
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

