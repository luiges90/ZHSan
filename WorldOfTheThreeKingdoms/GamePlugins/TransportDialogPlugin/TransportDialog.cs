using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;
using System.Collections.Generic;

namespace TransportDialogPlugin
{

    public class TransportDialog
    {
        internal Point BackgroundSize;
        internal PlatformTexture BackgroundTexture;
        internal int Days;
        private Architecture destinationArchitecture;
        internal PlatformTexture DestinationButtonDisplayTexture;
        internal Rectangle DestinationButtonPosition;
        internal PlatformTexture DestinationButtonSelectedTexture;
        internal PlatformTexture DestinationButtonTexture;
        internal FreeText DestinationCommentText;
        internal FreeText DestinationText;
        internal Point DisplayOffset;
        internal IGameFrame GameFramePlugin;
        internal PlatformTexture InputNumberButtonDisplayTexture;
        internal Rectangle InputNumberButtonPosition;
        internal PlatformTexture InputNumberButtonSelectedTexture;
        internal PlatformTexture InputNumberButtonTexture;
        internal FreeText InputNumberText;
        private bool isShowing;
        internal TransportKind Kind;
        internal List<LabelText> LabelTexts = new List<LabelText>();
        private int number;
        internal INumberInputer NumberInputerPlugin;
        internal IGameRecord GameRecordPlugin;
        
        internal Architecture SourceArchitecture;
        internal PlatformTexture StartButtonDisabledTexture;
        internal PlatformTexture StartButtonDisplayTexture;
        internal bool StartButtonEnabled;
        internal Rectangle StartButtonPosition;
        internal PlatformTexture StartButtonSelectedTexture;
        internal PlatformTexture StartButtonTexture;
#pragma warning disable CS0649 // Field 'TransportDialog.SurplusRate' is never assigned to, and will always have its default value 0
        internal float SurplusRate;
#pragma warning restore CS0649 // Field 'TransportDialog.SurplusRate' is never assigned to, and will always have its default value 0
        internal ITabList TabListPlugin;
        internal FreeText TitleText;

        internal PlatformTexture EmperorDestinationButtonDisplayTexture;
        internal Rectangle EmperorDestinationButtonPosition;
        internal PlatformTexture EmperorDestinationButtonSelectedTexture;
        internal PlatformTexture EmperorDestinationButtonTexture;
        internal FreeText EmperorDestinationCommentText;
        internal FreeText EmperorDestinationText;
        internal PlatformTexture EmperorInputNumberButtonDisplayTexture;
        internal Rectangle EmperorInputNumberButtonPosition;
        internal PlatformTexture EmperorInputNumberButtonSelectedTexture;
        internal PlatformTexture EmperorInputNumberButtonTexture;
        internal FreeText EmperorInputNumberText;
        internal PlatformTexture EmperorStartButtonDisabledTexture;
        internal PlatformTexture EmperorStartButtonDisplayTexture;
        internal Rectangle EmperorStartButtonPosition;
        internal PlatformTexture EmperorStartButtonSelectedTexture;
        internal PlatformTexture EmperorStartButtonTexture;

        internal PlatformTexture FundDestinationButtonDisplayTexture;
        internal Rectangle FundDestinationButtonPosition;
        internal PlatformTexture FundDestinationButtonSelectedTexture;
        internal PlatformTexture FundDestinationButtonTexture;
        internal FreeText FundDestinationCommentText;
        internal FreeText FundDestinationText;
        internal PlatformTexture FundInputNumberButtonDisplayTexture;
        internal Rectangle FundInputNumberButtonPosition;
        internal PlatformTexture FundInputNumberButtonSelectedTexture;
        internal PlatformTexture FundInputNumberButtonTexture;
        internal FreeText FundInputNumberText;
        internal PlatformTexture FundStartButtonDisabledTexture;
        internal PlatformTexture FundStartButtonDisplayTexture;
        internal Rectangle FundStartButtonPosition;
        internal PlatformTexture FundStartButtonSelectedTexture;
        internal PlatformTexture FundStartButtonTexture;

        internal void Draw()
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            this.TitleText.Draw(0.1999f);
            int index = 0;
            foreach (LabelText text in this.LabelTexts)
            {
                index++;
                //if (index == 1 && (this.Kind == TransportKind.Resource)) continue;
                if (index == 1 && (this.Kind == TransportKind.Food || this.Kind == TransportKind.Fund)) continue;
                if (index == 2 && (this.Kind == TransportKind.EmperorFood || this.Kind == TransportKind.EmperorFund)) continue;
                text.Label.Draw(0.1999f);
                text.Text.Draw(0.1999f);
            }

            PlatformTexture destinationTexture, inputNumberTexture, startTexture;
            Rectangle destinationPosition, inputNumberPosition, startPosition;

           // Texture2D destinationTexture2, inputNumberTexture2, startTexture2;
            //Rectangle destinationPosition2, inputNumberPosition2, startPosition2;

            switch (this.Kind)
            {
                case TransportKind.EmperorFood:
                case TransportKind.EmperorFund:
                    {
                        destinationTexture = this.EmperorDestinationButtonTexture;
                        destinationPosition = this.EmperorDestinationButtonPosition;
                        inputNumberTexture = this.EmperorInputNumberButtonTexture;
                        inputNumberPosition = this.EmperorInputNumberButtonPosition;
                        startTexture = this.StartButtonEnabled ? this.EmperorStartButtonTexture : this.EmperorStartButtonDisabledTexture;
                        startPosition = this.EmperorStartButtonPosition;
                        break;
                    }
                    /*
                case TransportKind.Resource:
                    {

                        destinationTexture = this.FundDestinationButtonTexture;
                        destinationPosition = this.FundDestinationButtonPosition;
                        inputNumberTexture = this.FundInputNumberButtonTexture;
                        inputNumberPosition = this.FundInputNumberButtonPosition;
                        startTexture = this.StartButtonEnabled ? this.FundStartButtonTexture : this.FundStartButtonDisabledTexture;
                        startPosition = this.FundStartButtonPosition;

                        break;
                    }*/
                    
                case TransportKind.Food:
                    {
                        destinationTexture = this.DestinationButtonTexture;
                        destinationPosition = this.DestinationButtonPosition;
                        inputNumberTexture = this.InputNumberButtonTexture;
                        inputNumberPosition = this.InputNumberButtonPosition;
                        startTexture = this.StartButtonEnabled ? this.StartButtonTexture : this.StartButtonDisabledTexture;
                        startPosition = this.StartButtonPosition;
                        break;
                    }
                case TransportKind.Fund:
                    {
                        destinationTexture = this.FundDestinationButtonTexture;
                        destinationPosition = this.FundDestinationButtonPosition;
                        inputNumberTexture = this.FundInputNumberButtonTexture;
                        inputNumberPosition = this.FundInputNumberButtonPosition;
                        startTexture = this.StartButtonEnabled ? this.FundStartButtonTexture : this.FundStartButtonDisabledTexture;
                        startPosition = this.FundStartButtonPosition;
                        break;
                    }
                     
                default:
                    throw new Exception("should not happen");
            }
            //new
            /*
            destinationPosition2 = new Rectangle(destinationPosition2.X + BackgroundDisplayPosition.X, destinationPosition2.Y + BackgroundDisplayPosition.Y,
                destinationPosition2.Width, destinationPosition2.Height);
            inputNumberPosition2 = new Rectangle(inputNumberPosition2.X + BackgroundDisplayPosition.X, inputNumberPosition2.Y + BackgroundDisplayPosition.Y,
                inputNumberPosition2.Width, inputNumberPosition2.Height);
            startPosition2 = new Rectangle(startPosition2.X + BackgroundDisplayPosition.X, startPosition2.Y + BackgroundDisplayPosition.Y,
                startPosition2.Width, startPosition2.Height);
            CacheManager.Draw(destinationTexture2, destinationPosition2, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            CacheManager.Draw(inputNumberTexture2, inputNumberPosition2, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            CacheManager.Draw(startTexture2, startPosition2, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
             */
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            destinationPosition = new Rectangle(destinationPosition.X + BackgroundDisplayPosition.X, destinationPosition.Y + BackgroundDisplayPosition.Y,
                destinationPosition.Width, destinationPosition.Height);
            inputNumberPosition = new Rectangle(inputNumberPosition.X + BackgroundDisplayPosition.X, inputNumberPosition.Y + BackgroundDisplayPosition.Y,
                inputNumberPosition.Width, inputNumberPosition.Height);
            startPosition = new Rectangle(startPosition.X + BackgroundDisplayPosition.X, startPosition.Y + BackgroundDisplayPosition.Y,
                startPosition.Width, startPosition.Height);

            CacheManager.Draw(destinationTexture, destinationPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            CacheManager.Draw(inputNumberTexture, inputNumberPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
            this.DestinationText.Draw(0.1999f);
            this.DestinationCommentText.Draw(0.1999f);
            this.InputNumberText.Draw(0.1999f);
            CacheManager.Draw(startTexture, startPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
        }

        internal void Initialize()
        {

        }

        private void RefreshStartButton()
        {
            this.StartButtonEnabled = (this.DestinationArchitecture != null) && (this.Number > 0);
        }

        private void screen_OnMouseLeftup(Point position)
        {
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                if (StaticMethods.PointInRectangle(position, this.DestinationButtonDisplayPosition))
                {
                    this.ShowDestinationFrame();
                }
                else if (StaticMethods.PointInRectangle(position, this.InputNumberButtonDisplayPosition))
                {
                    this.ShowInputNumberDialog();
                }
                else if (StaticMethods.PointInRectangle(position, this.StartButtonDisplayPosition) && this.StartButtonEnabled)
                {
                    this.StartTransport();
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                if (StaticMethods.PointInRectangle(position, this.DestinationButtonDisplayPosition))
                {
                    this.DestinationButtonDisplayTexture = this.DestinationButtonSelectedTexture;
                    this.InputNumberButtonDisplayTexture = this.InputNumberButtonTexture;
                    if (this.StartButtonEnabled)
                    {
                        this.StartButtonDisplayTexture = this.StartButtonTexture;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.InputNumberButtonDisplayPosition))
                {
                    this.DestinationButtonDisplayTexture = this.DestinationButtonTexture;
                    this.InputNumberButtonDisplayTexture = this.InputNumberButtonSelectedTexture;
                    if (this.StartButtonEnabled)
                    {
                        this.StartButtonDisplayTexture = this.StartButtonTexture;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.StartButtonDisplayPosition))
                {
                    this.DestinationButtonDisplayTexture = this.DestinationButtonTexture;
                    this.InputNumberButtonDisplayTexture = this.InputNumberButtonTexture;
                    if (this.StartButtonEnabled)
                    {
                        this.StartButtonDisplayTexture = this.StartButtonSelectedTexture;
                    }
                }
                else
                {
                    this.DestinationButtonDisplayTexture = this.DestinationButtonTexture;
                    this.InputNumberButtonDisplayTexture = this.InputNumberButtonTexture;
                    if (this.StartButtonEnabled)
                    {
                        this.StartButtonDisplayTexture = this.StartButtonTexture;
                    }
                }
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.Dialog)
            {
                this.IsShowing = false;
            }
        }

        internal void SetDisplayOffset(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y);
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
            this.TitleText.DisplayOffset = this.DisplayOffset;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.DestinationText.DisplayOffset = this.DisplayOffset;
            this.DestinationCommentText.DisplayOffset = this.DisplayOffset;
            this.InputNumberText.DisplayOffset = this.DisplayOffset;
        }

        internal void SetKind(TransportKind kind)
        {
            this.Kind = kind;
            switch (kind)
            {
                case TransportKind.EmperorFund:
                    this.TitleText.Text = "进贡资金";

                    if (this.SourceArchitecture.jingongjianzhuliebiao() != null)
                    {
                        this.DestinationArchitecture = this.SourceArchitecture.jingongjianzhuliebiao()[0] as Architecture;
                    }
                    break;

                case TransportKind.EmperorFood:
                    this.TitleText.Text = "进贡粮草";
                    if (this.SourceArchitecture.jingongjianzhuliebiao() != null)
                    {
                        this.DestinationArchitecture = this.SourceArchitecture.jingongjianzhuliebiao()[0] as Architecture;

                    }
                    break;
                   /*
                case TransportKind.Resource:
                    this.TitleText.Text = "运输钱粮";
                    break;
                    */
                case TransportKind.Fund:
                    this.TitleText.Text = "运输资金";
                    break;

                case TransportKind.Food:
                    this.TitleText.Text = "运输粮草";
                    break;
                     
            }
        }

        internal void SetSourceArchiecture(Architecture architecture)
        {
            this.SourceArchitecture = architecture;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(architecture, text.PropertyName).ToString();
            }
        }

        private void ShowDestinationFrame()
        {
            switch (this.Kind)
            {
                //case TransportKind.Resource:
                case TransportKind.Fund:
                case TransportKind.Food:
                    this.TabListPlugin.InitialValues(this.SourceArchitecture.BelongedFaction.ArchitecturesExcluding(this.SourceArchitecture), null, 0, this.TitleText.Text);
                    break;

                case TransportKind.EmperorFood:
                case TransportKind.EmperorFund:
                    return;
            }
            this.TabListPlugin.SetListKindByName("Architecture", true, false);
            this.TabListPlugin.SetSelectedTab("");
            this.GameFramePlugin.Kind = FrameKind.Architecture;
            this.GameFramePlugin.Function = FrameFunction.Transport;
            this.GameFramePlugin.SetFrameContent(this.TabListPlugin.TabList, Session.MainGame.mainGameScreen.viewportSizeFull);
            this.GameFramePlugin.OKButtonEnabled = false;
            this.GameFramePlugin.CancelButtonEnabled = true;
            this.GameFramePlugin.SetOKFunction(delegate
            {
                this.DestinationArchitecture = this.TabListPlugin.SelectedItem as Architecture;
            });
            this.GameFramePlugin.IsShowing = true;
        }

        private void ShowInputNumberDialog()
        {
            switch (this.Kind)
            {
                    /*
                case TransportKind.Resource:
                    this.NumberInputerPlugin.SetMax(this.SourceArchitecture.Fund);
                    this.NumberInputerPlugin.SetMax(this.SourceArchitecture.Food);
                    break;
                     */
                case TransportKind.Fund:
                case TransportKind.EmperorFund:
                    this.NumberInputerPlugin.SetMax(this.SourceArchitecture.Fund);
                    break;

                case TransportKind.Food:
                case TransportKind.EmperorFood:
                    this.NumberInputerPlugin.SetMax(this.SourceArchitecture.Food);
                    break;
            }
            this.NumberInputerPlugin.SetDepthOffset(-0.01f);
            this.NumberInputerPlugin.SetMapPosition(ShowPosition.Center);
            this.NumberInputerPlugin.SetEnterFunction(delegate
            {
                this.Number = this.NumberInputerPlugin.Number;
            });
            this.NumberInputerPlugin.IsShowing = true;
        }

        private void StartTransport()
        {
            if (this.Number > 0)
            {
                Faction faction = this.SourceArchitecture.BelongedFaction;
                switch (this.Kind)
                {
                    case TransportKind.EmperorFund:
                        this.SourceArchitecture.DecreaseFund(this.Number);
                        if (Session.Current.Scenario.huangdisuozaijianzhu().BelongedFaction != this.SourceArchitecture.BelongedFaction)
                        {
                            this.DestinationArchitecture.AddFundPack(this.Number, this.Days);
                        }
                        this.SourceArchitecture.BelongedFaction.chaotinggongxiandu += this.Number;

                        faction.TextResultString = this.Number.ToString();
                        faction.TextDestinationString = "资金";

                        this.GameRecordPlugin.AddBranch(faction, "shilijingong", faction.Capital.Position);

                        break;

                    case TransportKind.EmperorFood:
                        this.SourceArchitecture.DecreaseFood(this.Number);
                        if (Session.Current.Scenario.huangdisuozaijianzhu().BelongedFaction != this.SourceArchitecture.BelongedFaction)
                        {
                            this.DestinationArchitecture.IncreaseFood(this.Number);
                        }
                        this.SourceArchitecture.BelongedFaction.chaotinggongxiandu += this.Number / 200;

                        faction.TextResultString = this.Number.ToString();
                        faction.TextDestinationString = "粮草";

                        this.GameRecordPlugin.AddBranch(faction, "shilijingong", faction.Capital.Position);

                        break;
                        /*
                    case TransportKind.Resource:
                        this.SourceArchitecture.DecreaseFund(this.Number);
                        this.DestinationArchitecture.AddFundPack((int)(this.Number / this.Session.Current.Scenario.GetResourceConsumptionRate(this.SourceArchitecture, this.destinationArchitecture)), this.Days);
                        this.SourceArchitecture.DecreaseFood(this.Number);
                        this.DestinationArchitecture.AddFoodPack((int)(this.Number / this.Session.Current.Scenario.GetResourceConsumptionRate(this.SourceArchitecture, this.destinationArchitecture)), this.Days);
                        break;
                        */
                    case TransportKind.Fund:
                        this.SourceArchitecture.DecreaseFund(this.Number);
                        this.DestinationArchitecture.AddFundPack((int)(this.Number / Session.Current.Scenario.GetResourceConsumptionRate(this.SourceArchitecture, this.destinationArchitecture)), this.Days);
                        break;

                    case TransportKind.Food:
                        this.SourceArchitecture.DecreaseFood(this.Number);
                        this.DestinationArchitecture.AddFoodPack((int)(this.Number / Session.Current.Scenario.GetResourceConsumptionRate(this.SourceArchitecture, this.destinationArchitecture)), this.Days);
                        break;
                         
                }
            }

            this.IsShowing = false;
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

        internal Architecture DestinationArchitecture
        {
            get
            {
                return this.destinationArchitecture;
            }
            set
            {
                this.destinationArchitecture = value;
                if (this.destinationArchitecture != null)
                {
                    this.DestinationText.Text = this.destinationArchitecture.Name;
                    switch (this.Kind)
                    {
                        case TransportKind.EmperorFood:
                        case TransportKind.EmperorFund:
                            this.Days = 1;
                            this.DestinationCommentText.Text = "";
                            break;

                        //case TransportKind.Resource:
                        case TransportKind.Fund:
                        case TransportKind.Food:
                            this.Days = Session.Current.Scenario.GetTransferFundDays(this.SourceArchitecture, this.DestinationArchitecture);
                            this.DestinationCommentText.Text = "运抵时间：" + this.Days.ToString() + "天";
                            break;

                    }
                }
                else
                {
                    this.DestinationText.Text = "----";
                    this.DestinationCommentText.Text = "";
                }
                //Label_00ED:
                this.RefreshStartButton();
            }
        }

        private Rectangle DestinationButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.DestinationButtonPosition.X, this.DisplayOffset.Y + this.DestinationButtonPosition.Y, this.DestinationButtonPosition.Width, this.DestinationButtonPosition.Height);
            }
        }

        private Rectangle InputNumberButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.InputNumberButtonPosition.X, this.DisplayOffset.Y + this.InputNumberButtonPosition.Y, this.InputNumberButtonPosition.Width, this.InputNumberButtonPosition.Height);
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
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Dialog, UndoneWorkSubKind.None));
                    Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftup);
                    Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                }
                else
                {
                    if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.Dialog)
                    {
                        throw new Exception("The UndoneWork is not a TransportDialog.");
                    }
                    Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftup);
                    Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                    this.DestinationArchitecture = null;
                    this.Number = 0;
                }
            }
        }

        internal int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                this.InputNumberText.Text = this.number.ToString();
                this.RefreshStartButton();
            }
        }

        private Rectangle StartButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.StartButtonPosition.X, this.DisplayOffset.Y + this.StartButtonPosition.Y, this.StartButtonPosition.Width, this.StartButtonPosition.Height);
            }
        }
    }
}

