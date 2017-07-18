using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;

namespace SimpleTextDialogPlugin
{

    internal class SimpleTextDialog
    {
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal Rectangle ClientPosition;
        private Point DisplayOffset;
        internal Texture2D FirstPageButtonDisabledTexture;
        private Texture2D FirstPageButtonDisplayTexture;
        internal Rectangle FirstPageButtonPosition;
        internal Texture2D FirstPageButtonSelectedTexture;
        internal Texture2D FirstPageButtonTexture;
        internal IConfirmationDialog iConfirmationDialog;
        private bool isShowing;
        internal FreeRichText RichText = new FreeRichText();
        private Screen screen;
        internal int ShowingSeconds;
        private DateTime startShowingTime;
        internal GameObjectTextTree TextTree = new GameObjectTextTree();

        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            spriteBatch.Draw(this.FirstPageButtonDisplayTexture, this.FirstPageButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            this.RichText.Draw(spriteBatch, 0.1999f);
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.FirstPageButtonDisplayPosition))
            {
                this.RichText.FirstPage();
                this.FirstPageButtonDisplayTexture = this.FirstPageButtonDisabledTexture;
            }
            else if ((this.RichText.CurrentPageIndex >= (this.RichText.PageCount - 1)) && (this.iConfirmationDialog == null))
            {
                this.IsShowing = false;
            }
            else
            {
                this.RichText.NextPage();
                this.startShowingTime = DateTime.Now;
                if (this.RichText.CurrentPageIndex > 0)
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonTexture;
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.RichText.CurrentPageIndex > 0)
            {
                if (StaticMethods.PointInRectangle(position, this.FirstPageButtonDisplayPosition))
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonSelectedTexture;
                }
                else
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonTexture;
                }
            }
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
            this.RichText.DisplayOffset = new Point(rect.X + this.ClientPosition.X, rect.Y + this.ClientPosition.Y);
        }

        internal void Update()
        {
            if ((this.iConfirmationDialog == null) && ((DateTime.Now - this.startShowingTime).Seconds >= this.ShowingSeconds))
            {
                this.IsShowing = false;
            }
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle FirstPageButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.FirstPageButtonPosition.X + this.DisplayOffset.X, this.FirstPageButtonPosition.Y + this.DisplayOffset.Y, this.FirstPageButtonPosition.Width, this.FirstPageButtonPosition.Height);
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
                if (this.isShowing != value)
                {
                    this.isShowing = value;
                    if (value)
                    {
                        this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Dialog, DialogKind.SimpleText));
                        this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        this.FirstPageButtonDisplayTexture = this.FirstPageButtonDisabledTexture;
                        this.screen.EnableLaterMouseEvent = false;
                        this.startShowingTime = DateTime.Now;
                    }
                    else
                    {
                        if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.Dialog)
                        {
                            throw new Exception("The UndoneWork is not a Dialog.");
                        }
                        this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.EnableLaterMouseEvent = true;
                        this.iConfirmationDialog = null;
                    }
                }
            }
        }
    }
}

