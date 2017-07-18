using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFormFramePlugin
{

    internal class GameFrame
    {
        private Rectangle TopLeftRectangle;
        internal Texture2D TopLeftTexture;
        //internal int TopLeftWidth;
        private Rectangle TopRightRectangle;
        internal Texture2D TopRightTexture;
        //internal int TopRightWidth;
        private Rectangle BottomLeftRectangle;
        internal Texture2D BottomLeftTexture;
        //internal int BottomLeftWidth;
        private Rectangle BottomRightRectangle;
        internal Texture2D BottomRightTexture;
        //internal int BottomRightWidth;

        private Rectangle backgroundRectangle;
        internal Texture2D backgroundTexture;
        private Rectangle bottomedgeRectangle;
        internal Texture2D bottomedgeTexture;
        internal int bottomedgeWidth;
        private float buttonDepthOffset = -0.01f;
        internal Texture2D cancelbuttonDisabledTexture;
        private Point cancelbuttonPosition;
        internal Texture2D cancelbuttonPressedTexture;
        private Rectangle cancelbuttonRectangle;
        internal Texture2D cancelbuttonSelectedTexture;
        internal Point cancelbuttonSize;
        private FrameButtonState CancelButtonState;
        internal Texture2D cancelbuttonTexture;
        internal string CancelSoundFile;
        private bool Draging;
        private FrameContent frameContent = null;
        internal FrameFunction Function;
        internal FrameKind Kind;
        private Rectangle leftedgeRectangle;
        internal Texture2D leftedgeTexture;
        internal int leftedgeWidth;
        private Point mapviewselectorbuttonPosition;
        private Rectangle mapviewselectorButtonRectangle;
        private bool MapViewSelectorButtonSelected;
        internal Texture2D MapViewSelectorButtonSelectedTexture;
        internal Point mapviewselectorbuttonSize;
        internal Texture2D MapViewSelectorButtonTexture;
        internal Texture2D okbuttonDisabledTexture;
        private Point okbuttonPosition;
        internal Texture2D okbuttonPressedTexture;
        private Rectangle okbuttonRectangle;
        internal Texture2D okbuttonSelectedTexture;
        internal Point okbuttonSize;
        private FrameButtonState OKButtonState;
        internal Texture2D okbuttonTexture;
        internal string OKSoundFile;
        //全选
        internal Texture2D selectallbuttonDisabledTexture;
        private Point selectallbuttonPosition;
        internal Texture2D selectallbuttonPressedTexture;
        private Rectangle selectallbuttonRectangle;
        internal Texture2D selectallbuttonSelectedTexture;
        internal Point selectallbuttonSize;
        private FrameButtonState SelectAllButtonState;
        internal Texture2D selectallbuttonTexture;
        internal string SelectAllSoundFile;
        internal Rectangle Position;
        internal FrameResult Result = FrameResult.Cancel;
        private Rectangle rightedgeRectangle;
        internal Texture2D rightedgeTexture;
        internal int rightedgeWidth;
        private Screen screen;
        internal int titleHeight;
        private Rectangle titleRectangle;
        internal FreeText TitleText;
        internal Texture2D titleTexture;
        internal int titleWidth;
        private Rectangle topedgeRectangle;
        internal Texture2D topedgeTexture;
        internal int topedgeWidth;

        float depth = 0.04f;

        internal void DoCancel()
        {
            this.screen.PlayNormalSound(this.CancelSoundFile);
            this.Result = FrameResult.Cancel;
            this.IsShowing = false;
        }

        internal void DoOK()
        {
            if (this.OKButtonEnabled)
            {
                this.screen.PlayNormalSound(this.OKSoundFile);
                this.Result = FrameResult.OK;
                if (this.frameContent.OKFunction != null)
                {
                    this.frameContent.OKFunction();
                    this.frameContent.OKFunction = null;
                }
                this.IsShowing = false;
            }
        }
        /*
        internal void DoSelectAll()
        {
            if (this.SelectAllButtonEnabled)
            {
                this.screen.PlayNormalSound(this.SelectAllSoundFile);
                this.Result = FrameResult.SelectAll;
                if (this.frameContent.SelectAllFunction != null)
                {
                    this.frameContent.SelectAllFunction();
                    this.frameContent.SelectAllFunction = null;
                }
                this.IsShowing = false;
            }
        }
        */

        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.titleTexture, this.titleRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            this.TitleText.Draw(spriteBatch, 0.03999f);
            sourceRectangle = null;
            spriteBatch.Draw(this.leftedgeTexture, this.leftedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            spriteBatch.Draw(this.rightedgeTexture, this.rightedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            spriteBatch.Draw(this.topedgeTexture, this.topedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            spriteBatch.Draw(this.bottomedgeTexture, this.bottomedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;

            spriteBatch.Draw(this.TopLeftTexture, this.TopLeftRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(this.TopRightTexture, this.TopRightRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(this.BottomLeftTexture, this.BottomLeftRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            spriteBatch.Draw(this.BottomRightTexture, this.BottomRightRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);

            spriteBatch.Draw(this.backgroundTexture, this.backgroundRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);

            if (this.OKButtonEnabled)
            {
                switch (this.OKButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.okbuttonTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.okbuttonSelectedTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.okbuttonPressedTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.okbuttonDisabledTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
        //Label_024F:
            /*if (this.SelectAllButtonEnabled)
            {
                switch (this.SelectAllButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.selectallbuttonTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.selectallbuttonSelectedTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.selectallbuttonPressedTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.selectallbuttonDisabledTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
             */
            if (this.CancelButtonEnabled)
            {
                switch (this.CancelButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.cancelbuttonTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.cancelbuttonSelectedTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.cancelbuttonPressedTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                spriteBatch.Draw(this.cancelbuttonDisabledTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
        //Label_0365:
            if (this.frameContent.MapViewSelectorButtonEnabled)
            {
                if (this.MapViewSelectorButtonSelected)
                {
                    spriteBatch.Draw(this.MapViewSelectorButtonSelectedTexture, this.mapviewselectorButtonRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                }
                else
                {
                    spriteBatch.Draw(this.MapViewSelectorButtonTexture, this.mapviewselectorButtonRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                }
            }
            if (this.frameContent != null)
            {
                this.frameContent.Draw(spriteBatch);
            }
        }

        private void frameContent_OnItemClick()
        {
            if (this.Function == FrameFunction.Jump)
            {
                this.IsShowing = false;
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void ResetRectangles()
        {
            this.leftedgeRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y, this.leftedgeWidth, this.Position.Height);
            this.rightedgeRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y, this.rightedgeWidth, this.Position.Height);
            this.topedgeRectangle = new Rectangle(this.Position.X, this.Position.Y - this.topedgeWidth, this.Position.Width, this.topedgeWidth);
            this.bottomedgeRectangle = new Rectangle(this.Position.X, this.Position.Y + this.Position.Height, this.Position.Width, this.bottomedgeWidth);
            this.backgroundRectangle = new Rectangle(this.Position.X, this.Position.Y, this.Position.Width, this.Position.Height);
            this.okbuttonRectangle = new Rectangle(this.Position.X + this.okbuttonPosition.X, this.Position.Y + this.okbuttonPosition.Y, this.okbuttonSize.X, this.okbuttonSize.Y);
            this.cancelbuttonRectangle = new Rectangle(this.Position.X + this.cancelbuttonPosition.X, this.Position.Y + this.cancelbuttonPosition.Y, this.cancelbuttonSize.X, this.cancelbuttonSize.Y);
            this.titleRectangle = new Rectangle(this.Position.X, this.Position.Y - this.titleHeight, this.titleWidth, this.titleHeight);
            this.TitleText.Position = this.titleRectangle;
            this.mapviewselectorButtonRectangle = new Rectangle(this.Position.X + this.mapviewselectorbuttonPosition.X, this.Position.Y + this.mapviewselectorbuttonPosition.Y, this.mapviewselectorbuttonSize.X, this.mapviewselectorbuttonSize.Y);

            this.TopLeftRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y - this.topedgeWidth, this.leftedgeWidth, this.topedgeWidth);
            this.TopRightRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y - this.topedgeWidth, this.rightedgeWidth, this.topedgeWidth);
            this.BottomLeftRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y + this.Position.Height, this.leftedgeWidth, this.bottomedgeWidth);
            this.BottomRightRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y + this.Position.Height, this.rightedgeWidth, this.bottomedgeWidth);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
            {
                if (this.OKButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.okbuttonRectangle))
                    {
                        this.OKButtonState = FrameButtonState.Pressed;
                    }
                    else
                    {
                        this.OKButtonState = FrameButtonState.Normal;
                    }
                }
                if (this.CancelButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.cancelbuttonRectangle))
                    {
                        this.CancelButtonState = FrameButtonState.Pressed;
                    }
                    else
                    {
                        this.CancelButtonState = FrameButtonState.Normal;
                    }
                }
                if ((this.frameContent.MapViewSelectorButtonEnabled && StaticMethods.PointInRectangle(position, this.mapviewselectorButtonRectangle)) && (this.frameContent.MapViewSelectorFunction != null))
                {
                    this.frameContent.MapViewSelectorFunction();
                }
                /* if (this.SelectAllButtonEnabled)
                 {
                     if (StaticMethods.PointInRectangle(position, this.selectallbuttonRectangle))
                     {
                         this.SelectAllButtonState = FrameButtonState.Pressed;
                     }
                     else
                     {
                         this.SelectAllButtonState = FrameButtonState.Normal;
                     }
                 }*/
            }
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
            {
                this.Draging = false;
                if ((this.OKButtonEnabled && (this.OKButtonState == FrameButtonState.Pressed)) && StaticMethods.PointInRectangle(position, this.okbuttonRectangle))
                {
                    this.screen.PlayNormalSound(this.OKSoundFile);
                    this.OKButtonState = FrameButtonState.Selected;
                    this.Result = FrameResult.OK;
                    if (this.frameContent.OKFunction != null)
                    {
                        this.frameContent.OKFunction();
                        this.frameContent.OKFunction = null;
                    }
                    this.IsShowing = false;
                }
                if ((this.CancelButtonEnabled && (this.CancelButtonState == FrameButtonState.Pressed)) && StaticMethods.PointInRectangle(position, this.cancelbuttonRectangle))
                {
                    this.screen.PlayNormalSound(this.CancelSoundFile);
                    this.CancelButtonState = FrameButtonState.Selected;
                    this.Result = FrameResult.Cancel;
                    this.IsShowing = false;
                }
                //if ((this.frameContent.MapViewSelectorButtonEnabled && StaticMethods.PointInRectangle(position, this.mapviewselectorButtonRectangle)) && (this.frameContent.MapViewSelectorFunction != null))
                //{
                //    this.frameContent.MapViewSelectorFunction();
                //}
               /* if ((this.SelectAllButtonEnabled && (this.SelectAllButtonState == FrameButtonState.Pressed)) && StaticMethods.PointInRectangle(position, this.selectallbuttonRectangle))
                {
                    this.screen.PlayNormalSound(this.SelectAllSoundFile);
                    this.SelectAllButtonState = FrameButtonState.Selected;
                    this.Result = FrameResult.SelectAll;
                    if (this.frameContent.SelectAllFunction != null)
                    {
                        this.frameContent.SelectAllFunction();
                        this.frameContent.SelectAllFunction = null;
                    }
                    this.IsShowing = false;
                }*/
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
            {
                if (this.OKButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.okbuttonRectangle))
                    {
                        if (leftDown)
                        {
                            this.OKButtonState = FrameButtonState.Pressed;
                        }
                        else
                        {
                            this.OKButtonState = FrameButtonState.Selected;
                        }
                    }
                    else
                    {
                        this.OKButtonState = FrameButtonState.Normal;
                    }
                }
                if (this.CancelButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.cancelbuttonRectangle))
                    {
                        if (leftDown)
                        {
                            this.CancelButtonState = FrameButtonState.Pressed;
                        }
                        else
                        {
                            this.CancelButtonState = FrameButtonState.Selected;
                        }
                    }
                    else
                    {
                        this.CancelButtonState = FrameButtonState.Normal;
                    }
                }
               /* if (this.SelectAllButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.selectallbuttonRectangle))
                    {
                        if (leftDown)
                        {
                            this.SelectAllButtonState = FrameButtonState.Pressed;
                        }
                        else
                        {
                            this.SelectAllButtonState = FrameButtonState.Selected;
                        }
                    }
                    else
                    {
                        this.SelectAllButtonState = FrameButtonState.Normal;
                    }
                }*/
                if (this.frameContent.MapViewSelectorButtonEnabled)
                {
                    if (StaticMethods.PointInRectangle(position, this.mapviewselectorButtonRectangle))
                    {
                        this.MapViewSelectorButtonSelected = true;
                    }
                    else
                    {
                        this.MapViewSelectorButtonSelected = false;
                    }
                }
                if (leftDown)
                {
                    if (StaticMethods.PointInRectangle(position, this.titleRectangle))
                    {
                        this.Draging = true;
                    }
                    if (this.Draging)
                    {
                        this.frameContent.FramePosition = new Rectangle(this.frameContent.FramePosition.X + this.screen.MouseOffset.X, this.frameContent.FramePosition.Y + this.screen.MouseOffset.Y, this.frameContent.FramePosition.Width, this.frameContent.FramePosition.Height);
                        this.SetPosition(this.frameContent.FramePosition);
                        this.frameContent.ReCalculate();
                    }
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if ((this.screen.PeekUndoneWork().Kind == UndoneWorkKind.Frame) && (this.CancelButtonEnabled && this.frameContent.CanClose))
            {
                this.screen.PlayNormalSound(this.CancelSoundFile);
                this.Result = FrameResult.Cancel;
                this.IsShowing = false;
            }
        }

        internal void SetCancelFunction(GameDelegates.VoidFunction cancelfunction)
        {
            this.frameContent.CancelFunction = cancelfunction;
        }
        /*
        internal void SetSelectAllFunction(GameDelegates.VoidFunction selectallfunction)
        {
            this.frameContent.SelectAllFunction = selectallfunction;
        }
        */
        public void SetFrameContent(FrameContent frameContent, Point viewportSize)
        {
            this.Result = FrameResult.Cancel;
            this.frameContent = frameContent;
            this.frameContent.Function = this.Function;
            this.TitleText.Text = frameContent.GetCurrentTitle();
            frameContent.FramePosition = StaticMethods.GetRectangleFitViewport(frameContent.DefaultFrameWidth, frameContent.DefaultFrameHeight, viewportSize);
            this.OKButtonPosition = frameContent.OKButtonPosition;
            //this.SelectAllButtonPosition = frameContent.SelectAllButtonPosition;
            this.CancelButtonPosition = frameContent.CancelButtonPosition;
            this.MapViewSelectorButtonPosition = frameContent.MapViewSelectorButtonPosition;
            this.SetPosition(frameContent.FramePosition);
            frameContent.ReCalculate();
            frameContent.InitializeMapViewSelectorButton();
            frameContent.OnItemClick += new FrameContent.ItemClick(this.frameContent_OnItemClick);
        }

        internal void SetOKFunction(GameDelegates.VoidFunction okfunction)
        {
            this.frameContent.OKFunction = okfunction;
        }

        private void SetPosition(Rectangle position)
        {
            this.Position = position;
            this.ResetRectangles();
        }

        public bool CancelButtonEnabled
        {
            get
            {
                if (this.frameContent != null)
                {
                    return this.frameContent.CancelButtonEnabled;
                }
                return true;
            }
            set
            {
                if (this.frameContent != null)
                {
                    this.frameContent.CancelButtonEnabled = value;
                }
            }
        }

        internal Point CancelButtonPosition
        {
            get
            {
                return this.cancelbuttonPosition;
            }
            set
            {
                this.cancelbuttonPosition = value;
                this.cancelbuttonRectangle = new Rectangle(this.Position.X + this.cancelbuttonPosition.X, this.Position.Y + this.cancelbuttonPosition.Y, this.cancelbuttonSize.X, this.cancelbuttonSize.Y);
            }
        }

        public bool IsShowing
        {
            get
            {
                return ((this.frameContent != null) && this.frameContent.IsShowing);
            }
            set
            {
                if (this.IsShowing != value)
                {
                    if (value)
                    {
                        if (this.frameContent != null)
                        {
                            this.frameContent.IsShowing = true;
                            this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Frame, UndoneWorkSubKind.None));
                            this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                            this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                            this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                            this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        }
                    }
                    else
                    {
                        this.frameContent.IsShowing = false;
                        if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.Frame)
                        {
                            throw new Exception("The UndoneWork is not a Frame.");
                        }
                        this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        this.frameContent.OKFunction = null;
                    }
                }
            }
        }

        internal Point MapViewSelectorButtonPosition
        {
            get
            {
                return this.mapviewselectorbuttonPosition;
            }
            set
            {
                this.mapviewselectorbuttonPosition = value;
                this.mapviewselectorButtonRectangle = new Rectangle(this.Position.X + this.mapviewselectorbuttonPosition.X, this.Position.Y + this.mapviewselectorbuttonPosition.Y, this.mapviewselectorbuttonSize.X, this.mapviewselectorbuttonSize.Y);
            }
        }

        public bool OKButtonEnabled
        {
            get
            {
                if (this.frameContent != null)
                {
                    return this.frameContent.OKButtonEnabled;
                }
                return true;
            }
            set
            {
                if (this.frameContent != null)
                {
                    this.frameContent.OKButtonEnabled = value;
                }
            }
        }

        internal Point OKButtonPosition
        {
            get
            {
                return this.okbuttonPosition;
            }
            set
            {
                this.okbuttonPosition = value;
                this.okbuttonRectangle = new Rectangle(this.Position.X + this.okbuttonPosition.X, this.Position.Y + this.okbuttonPosition.Y, this.okbuttonSize.X, this.okbuttonSize.Y);
            }
        }

        internal string TitleString
        {
            get
            {
                if (this.frameContent != null)
                {
                    return this.frameContent.GetTitleString();
                }
                return "";
            }
        }
        /*
        public bool SelectAllButtonEnabled
        {
            get
            {
                if (this.frameContent != null)
                {
                    return this.frameContent.SelectAllButtonEnabled;
                }
                return true;
            }
            set
            {
                if (this.frameContent != null)
                {
                    this.frameContent.SelectAllButtonEnabled = value;
                }
            }
        }

        internal Point SelectAllButtonPosition
        {
            get
            {
                return this.selectallbuttonPosition;
            }
            set
            {
                this.selectallbuttonPosition = value;
                this.selectallbuttonRectangle = new Rectangle(this.Position.X + this.selectallbuttonPosition.X, this.Position.Y + this.selectallbuttonPosition.Y, this.selectallbuttonSize.X, this.selectallbuttonSize.Y);
            }
        }*/
    }
}

