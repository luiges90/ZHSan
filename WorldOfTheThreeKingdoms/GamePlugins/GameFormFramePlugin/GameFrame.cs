using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFormFramePlugin
{

    internal class GameFrame
    {
        private Rectangle TopLeftRectangle;
        internal PlatformTexture TopLeftTexture;
        //internal int TopLeftWidth;
        private Rectangle TopRightRectangle;
        internal PlatformTexture TopRightTexture;
        //internal int TopRightWidth;
        private Rectangle BottomLeftRectangle;
        internal PlatformTexture BottomLeftTexture;
        //internal int BottomLeftWidth;
        private Rectangle BottomRightRectangle;
        internal PlatformTexture BottomRightTexture;
        //internal int BottomRightWidth;

        private Rectangle backgroundRectangle;
        internal PlatformTexture backgroundTexture;
        private Rectangle bottomedgeRectangle;
        internal PlatformTexture bottomedgeTexture;
        internal int bottomedgeWidth;
        private float buttonDepthOffset = -0.01f;
        internal PlatformTexture cancelbuttonDisabledTexture;
        private Point cancelbuttonPosition;
        internal PlatformTexture cancelbuttonPressedTexture;
        private Rectangle cancelbuttonRectangle;
        internal PlatformTexture cancelbuttonSelectedTexture;
        internal Point cancelbuttonSize;
        private FrameButtonState CancelButtonState;
        internal PlatformTexture cancelbuttonTexture;
        internal string CancelSoundFile;
        private bool Draging;
        private FrameContent frameContent = null;
        internal FrameFunction Function;
        internal FrameKind Kind;
        private Rectangle leftedgeRectangle;
        internal PlatformTexture leftedgeTexture;
        internal int leftedgeWidth;
        private Point mapviewselectorbuttonPosition;
        private Rectangle mapviewselectorButtonRectangle;
        private bool MapViewSelectorButtonSelected;
        internal PlatformTexture MapViewSelectorButtonSelectedTexture;
        internal Point mapviewselectorbuttonSize;
        internal PlatformTexture MapViewSelectorButtonTexture;
        internal PlatformTexture okbuttonDisabledTexture;
        private Point okbuttonPosition;
        internal PlatformTexture okbuttonPressedTexture;
        private Rectangle okbuttonRectangle;
        internal PlatformTexture okbuttonSelectedTexture;
        internal Point okbuttonSize;
        private FrameButtonState OKButtonState;
        internal PlatformTexture okbuttonTexture;
        internal string OKSoundFile;
        //全选
#pragma warning disable CS0649 // Field 'GameFrame.selectallbuttonDisabledTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture selectallbuttonDisabledTexture;
#pragma warning restore CS0649 // Field 'GameFrame.selectallbuttonDisabledTexture' is never assigned to, and will always have its default value null
#pragma warning disable CS0169 // The field 'GameFrame.selectallbuttonPosition' is never used
        private Point selectallbuttonPosition;
#pragma warning restore CS0169 // The field 'GameFrame.selectallbuttonPosition' is never used
#pragma warning disable CS0649 // Field 'GameFrame.selectallbuttonPressedTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture selectallbuttonPressedTexture;
#pragma warning restore CS0649 // Field 'GameFrame.selectallbuttonPressedTexture' is never assigned to, and will always have its default value null
#pragma warning disable CS0169 // The field 'GameFrame.selectallbuttonRectangle' is never used
        private Rectangle selectallbuttonRectangle;
#pragma warning restore CS0169 // The field 'GameFrame.selectallbuttonRectangle' is never used
#pragma warning disable CS0649 // Field 'GameFrame.selectallbuttonSelectedTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture selectallbuttonSelectedTexture;
#pragma warning restore CS0649 // Field 'GameFrame.selectallbuttonSelectedTexture' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'GameFrame.selectallbuttonSize' is never assigned to, and will always have its default value
        internal Point selectallbuttonSize;
#pragma warning restore CS0649 // Field 'GameFrame.selectallbuttonSize' is never assigned to, and will always have its default value
#pragma warning disable CS0169 // The field 'GameFrame.SelectAllButtonState' is never used
        private FrameButtonState SelectAllButtonState;
#pragma warning restore CS0169 // The field 'GameFrame.SelectAllButtonState' is never used
#pragma warning disable CS0649 // Field 'GameFrame.selectallbuttonTexture' is never assigned to, and will always have its default value null
        internal PlatformTexture selectallbuttonTexture;
#pragma warning restore CS0649 // Field 'GameFrame.selectallbuttonTexture' is never assigned to, and will always have its default value null
#pragma warning disable CS0649 // Field 'GameFrame.SelectAllSoundFile' is never assigned to, and will always have its default value null
        internal string SelectAllSoundFile;
#pragma warning restore CS0649 // Field 'GameFrame.SelectAllSoundFile' is never assigned to, and will always have its default value null
        internal Rectangle Position;
        internal FrameResult Result = FrameResult.Cancel;
        private Rectangle rightedgeRectangle;
        internal PlatformTexture rightedgeTexture;
        internal int rightedgeWidth;
        
        internal int titleHeight;
        private Rectangle titleRectangle;
        internal FreeText TitleText;
        internal PlatformTexture titleTexture;
        internal int titleWidth;
        private Rectangle topedgeRectangle;
        internal PlatformTexture topedgeTexture;
        internal int topedgeWidth;

        float depth = 0.04f;

        internal void DoCancel()
        {
            Session.MainGame.mainGameScreen.PlayNormalSound(this.CancelSoundFile);
            this.Result = FrameResult.Cancel;
            this.IsShowing = false;
        }

        internal void DoOK()
        {
            if (this.OKButtonEnabled)
            {
                Session.MainGame.mainGameScreen.PlayNormalSound(this.OKSoundFile);
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
                Session.MainGame.mainGameScreen.PlayNormalSound(this.SelectAllSoundFile);
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

        internal void Draw()
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.titleTexture, this.titleRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            this.TitleText.Draw(0.03999f);
            sourceRectangle = null;
            CacheManager.Draw(this.leftedgeTexture, this.leftedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            CacheManager.Draw(this.rightedgeTexture, this.rightedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            CacheManager.Draw(this.topedgeTexture, this.topedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;
            CacheManager.Draw(this.bottomedgeTexture, this.bottomedgeRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            sourceRectangle = null;

            CacheManager.Draw(this.TopLeftTexture, this.TopLeftRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            CacheManager.Draw(this.TopRightTexture, this.TopRightRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            CacheManager.Draw(this.BottomLeftTexture, this.BottomLeftRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            CacheManager.Draw(this.BottomRightTexture, this.BottomRightRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);

            CacheManager.Draw(this.backgroundTexture, this.backgroundRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);

            if (this.OKButtonEnabled)
            {
                switch (this.OKButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        CacheManager.Draw(this.okbuttonTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        CacheManager.Draw(this.okbuttonSelectedTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        CacheManager.Draw(this.okbuttonPressedTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.okbuttonDisabledTexture, this.okbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
        //Label_024F:
            /*if (this.SelectAllButtonEnabled)
            {
                switch (this.SelectAllButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        CacheManager.Draw(this.selectallbuttonTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        CacheManager.Draw(this.selectallbuttonSelectedTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        CacheManager.Draw(this.selectallbuttonPressedTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.selectallbuttonDisabledTexture, this.selectallbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
             */
            if (this.CancelButtonEnabled)
            {
                switch (this.CancelButtonState)
                {
                    case FrameButtonState.Normal:
                        sourceRectangle = null;
                        CacheManager.Draw(this.cancelbuttonTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Selected:
                        sourceRectangle = null;
                        CacheManager.Draw(this.cancelbuttonSelectedTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;

                    case FrameButtonState.Pressed:
                        sourceRectangle = null;
                        CacheManager.Draw(this.cancelbuttonPressedTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                        break;
                }
            }
            else
            {
                sourceRectangle = null;
                CacheManager.Draw(this.cancelbuttonDisabledTexture, this.cancelbuttonRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
            }
        //Label_0365:
            if (this.frameContent.MapViewSelectorButtonEnabled)
            {
                if (this.MapViewSelectorButtonSelected)
                {
                    CacheManager.Draw(this.MapViewSelectorButtonSelectedTexture, this.mapviewselectorButtonRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                }
                else
                {
                    CacheManager.Draw(this.MapViewSelectorButtonTexture, this.mapviewselectorButtonRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth + this.buttonDepthOffset);
                }
            }
            if (this.frameContent != null)
            {
                this.frameContent.Draw();
            }
        }

        private void frameContent_OnItemClick()
        {
            if (this.Function == FrameFunction.Jump)
            {
                this.IsShowing = false;
            }
        }

        internal void Initialize()
        {
            
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
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
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
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
            {
                this.Draging = false;
                if ((this.OKButtonEnabled && (this.OKButtonState == FrameButtonState.Pressed)) && StaticMethods.PointInRectangle(position, this.okbuttonRectangle))
                {
                    Session.MainGame.mainGameScreen.PlayNormalSound(this.OKSoundFile);
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
                    Session.MainGame.mainGameScreen.PlayNormalSound(this.CancelSoundFile);
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
                    Session.MainGame.mainGameScreen.PlayNormalSound(this.SelectAllSoundFile);
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
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Frame)
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
                        this.frameContent.FramePosition = new Rectangle(this.frameContent.FramePosition.X + Session.MainGame.mainGameScreen.MouseOffset.X, this.frameContent.FramePosition.Y + Session.MainGame.mainGameScreen.MouseOffset.Y, this.frameContent.FramePosition.Width, this.frameContent.FramePosition.Height);
                        this.SetPosition(this.frameContent.FramePosition);
                        this.frameContent.ReCalculate();
                    }
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if ((Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Frame) && (this.CancelButtonEnabled && this.frameContent.CanClose))
            {
                Session.MainGame.mainGameScreen.PlayNormalSound(this.CancelSoundFile);
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
                            Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Frame, UndoneWorkSubKind.None));
                            Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                            Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                            Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                            Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        }
                    }
                    else
                    {
                        this.frameContent.IsShowing = false;
                        if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.Frame)
                        {
                            throw new Exception("The UndoneWork is not a Frame.");
                        }
                        Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
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

