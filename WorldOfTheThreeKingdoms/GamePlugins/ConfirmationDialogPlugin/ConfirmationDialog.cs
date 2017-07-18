using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;

namespace ConfirmationDialogPlugin
{

    internal class ConfirmationDialog
    {
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        private Point DisplayOffset;
        private Itupianwenzi iPersonTextDialog;
        private ISimpleTextDialog iSimpleTextDialog;
        private bool isShowing;
        private Texture2D NoDisplayTexture;
        internal GameDelegates.VoidFunction NoFunction;
        internal Rectangle NoPosition;
        internal Texture2D NoSelectedTexture;
        internal string NoSoundFile;
        internal Texture2D NoTexture;
        internal DialogResult Result;
        private Screen screen;
        private Texture2D YesDisplayTexture;
        internal GameDelegates.VoidFunction YesFunction;
        internal Rectangle YesPosition;
        internal Texture2D YesSelectedTexture;
        internal string YesSoundFile;
        internal Texture2D YesTexture;

        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            sourceRectangle = null;
            spriteBatch.Draw(this.YesDisplayTexture, this.YesDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            spriteBatch.Draw(this.NoDisplayTexture, this.NoDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.YesDisplayPosition))
            {
                this.Result = DialogResult.Yes;
                this.screen.PlayNormalSound(this.YesSoundFile);
                if (this.YesFunction != null)
                {
                    this.YesFunction();
                    this.YesFunction = null;
                }
                this.IsShowing = false;
            }
            else if (StaticMethods.PointInRectangle(position, this.NoDisplayPosition))
            {
                this.Result = DialogResult.No;
                this.screen.PlayNormalSound(this.NoSoundFile);
                if (this.NoFunction != null)
                {
                    this.NoFunction();
                    this.NoFunction = null;
                }
                this.IsShowing = false;
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (StaticMethods.PointInRectangle(position, this.YesDisplayPosition))
            {
                this.YesDisplayTexture = this.YesSelectedTexture;
            }
            else
            {
                this.YesDisplayTexture = this.YesTexture;
            }
            if (StaticMethods.PointInRectangle(position, this.NoDisplayPosition))
            {
                this.NoDisplayTexture = this.NoSelectedTexture;
            }
            else
            {
                this.NoDisplayTexture = this.NoTexture;
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            this.Result = DialogResult.No;
            this.screen.PlayNormalSound(this.NoSoundFile);
            if (this.NoFunction != null)
            {
                this.NoFunction();
                this.NoFunction = null;
            }
            this.IsShowing = false;
        }

        internal void SetPersonTextDialog(Itupianwenzi iPersonTextDialog)
        {
            this.iPersonTextDialog = iPersonTextDialog;
            this.iSimpleTextDialog = null;
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
        }

        internal void SetSimpleTextDialog(ISimpleTextDialog iSimpleTextDialog)
        {
            this.iSimpleTextDialog = iSimpleTextDialog;
            this.iPersonTextDialog = null;
        }

        internal void Update()
        {
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
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
                        this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Dialog, DialogKind.Confirmation));
                        this.YesDisplayTexture = this.YesTexture;
                        this.NoDisplayTexture = this.NoTexture;
                        this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        if (this.iSimpleTextDialog != null)
                        {
                            this.iSimpleTextDialog.SetPosition(ShowPosition.Bottom);
                            this.iSimpleTextDialog.IsShowing = true;
                        }
                        else if (this.iPersonTextDialog != null)
                        {
                            this.iPersonTextDialog.SetPosition(ShowPosition.Bottom);
                        }
                    }
                    else
                    {
                       // if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.Dialog)
                        if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.tupianwenzi)
                        {
                          //  throw new Exception("The UndoneWork is not a Dialog.");
                        }
                        this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        if (this.iSimpleTextDialog != null)
                        {
                            this.iSimpleTextDialog.IsShowing = false;
                            this.iSimpleTextDialog = null;
                        }
                        else if (this.iPersonTextDialog != null)
                        {
                            this.iPersonTextDialog.Close();
                        }
                    }
                }
            }
        }

        private Rectangle NoDisplayPosition
        {
            get
            {
                return new Rectangle(this.NoPosition.X + this.DisplayOffset.X, this.NoPosition.Y + this.DisplayOffset.Y, this.NoPosition.Width, this.NoPosition.Height);
            }
        }

        private Rectangle YesDisplayPosition
        {
            get
            {
                return new Rectangle(this.YesPosition.X + this.DisplayOffset.X, this.YesPosition.Y + this.DisplayOffset.Y, this.YesPosition.Width, this.YesPosition.Height);
            }
        }
    }
}

