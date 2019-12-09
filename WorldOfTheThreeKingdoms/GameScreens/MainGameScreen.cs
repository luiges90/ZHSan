using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.FactionDetail;
using GameObjects.PersonDetail;
using GameObjects.SectionDetail;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PluginInterface;
using WorldOfTheThreeKingdoms.GameLogic;
using WorldOfTheThreeKingdoms.GameScreens;
using WorldOfTheThreeKingdoms.GameScreens.ScreenLayers;
using WorldOfTheThreeKingdoms.Resources;
using Platforms;
using GameManager;

//using GameObjects.PersonDetail.PersonMessages;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    public partial class MainGameScreen : Screen
    {
        private string bianduiLiebiaoBiaoji;
        private ArchitectureLayer architectureLayer;
        private Keys currentKey;
        private bool drawingSelector;
        private bool tufashijianzantingyinyue=false ;
        private int shangciCundangShijian;
        private int cundangShijianJiange;
        public bool EnableLaterMouseLeftDownEvent;
        public bool EnableLaterMouseLeftUpEvent;
        public bool EnableLaterMouseMoveEvent;
        public bool EnableLaterMouseRightDownEvent;
        public bool EnableLaterMouseRightUpEvent;
        public bool EnableLaterMouseScrollEvent;
        private int frameCounter;
        private int frameRate;
        private Point lastPosition;
        private double lastTime;
        private string LoadFileName;
        public MainMapLayer mainMapLayer;
        private MapVeilLayer mapVeilLayer;
        private int oldScrollWheelValue;

        //public WindowsMediaPlayerClass Player;

        public GamePlugin Plugins;
        private Point position;
        private RoutewayLayer routewayLayer;
        private string SaveFileName;
        private ScreenManager screenManager;
        private float scrollSpeedScale;
        private float scrollSpeedScaleDefault;
        private float scrollSpeedScaleSpeedy;
        private SelectingLayer selectingLayer;
        public Point SelectorStartPosition;
        public TroopList SelectorTroops;
        public GameTextures Textures;
        
        private TileAnimationLayer tileAnimationLayer;
        private TroopLayer troopLayer;
        private int UpdateCount;
        private ViewMove viewMove;
        private bool isKeyScrolling = false;
        //public FreeText qizidezi;
        public bool editMode = false;
        public bool ShowArchitectureConnectedLine = false;
        private int ditukuaidezhi = 1;

        private bool mapEdited = false;

        public CloudLayer cloudLayer = new CloudLayer();

        public DantiaoLayer dantiaoLayer = null;

        public MainGameScreen()
            : base()
        {
            //this.Player = new WindowsMediaPlayerClass();

            this.Textures = new GameTextures();
            this.mainMapLayer = new MainMapLayer();
            this.architectureLayer = new ArchitectureLayer();
            this.mapVeilLayer = new MapVeilLayer();
            this.troopLayer = new TroopLayer();
            this.selectingLayer = new SelectingLayer();
            this.tileAnimationLayer = new TileAnimationLayer();
            this.routewayLayer = new RoutewayLayer();
            this.Plugins = new GamePlugin();
            this.SelectorTroops = new TroopList();
            this.scrollSpeedScale = 1f;
            this.scrollSpeedScaleDefault = 1f;
            this.scrollSpeedScaleSpeedy = 1.7f;
            this.oldScrollWheelValue = 0;
            this.EnableLaterMouseLeftDownEvent = true;
            this.EnableLaterMouseLeftUpEvent = true;
            this.EnableLaterMouseRightDownEvent = true;
            this.EnableLaterMouseRightUpEvent = true;
            this.EnableLaterMouseMoveEvent = true;
            this.EnableLaterMouseScrollEvent = true;
            this.frameRate = 0;
            this.frameCounter = 0;
            this.cundangShijianJiange = 0;
            this.shangciCundangShijian = 0;
            this.UpdateCount = 0;

            this.screenManager = new ScreenManager();
            
            //Session.Current.Scenario = new GameScenario(this);
            //this.LoadCommonData();

            //Platform.MainGame.Window.ClientSizeChanged += this.Window_ClientSizeChanged;  // new EventHandler(this.Window_ClientSizeChanged);

            Platform.MainGame.Activated += this.Game_Activated;  // new EventHandler(this.Game_Activated);
            Platform.MainGame.Deactivated += this.Game_Deactivated;  // new EventHandler(this.Game_Deactivated);
        }        

        private string SaveFileExtension
        {
            get
            {
                return ".json";
#pragma warning disable CS0162 // Unreachable code detected
                if (Session.GlobalVariables.EncryptSave)
#pragma warning restore CS0162 // Unreachable code detected
                {
                    return ".zhs";
                }
                else
                {
                    return ".mdb";
                }
            }
        }


        private void CalculateFrameRate(GameTime gameTime)
        {
        }

        public override void DisposeMapTileMemory(bool gc, bool clearAll)
        {
            this.mainMapLayer.freeTilesMemory(gc, clearAll);
        }

        public void Dispose()
        {
            //Platform.MainGame.Window.ClientSizeChanged -= this.Window_ClientSizeChanged;  // new EventHandler(this.Window_ClientSizeChanged);
            Platform.MainGame.Activated -= this.Game_Activated;  // new EventHandler(this.Game_Activated);
            Platform.MainGame.Deactivated -= this.Game_Deactivated;  // new EventHandler(this.Game_Deactivated);

            this.mainMapLayer.DisplayingMapTiles = null;
            this.mainMapLayer.DisplayingTiles = null;
            this.mainMapLayer = null;
            this.architectureLayer = null;
            this.routewayLayer = null;
            this.screenManager = null;
            this.Plugins = null;
        }


        public bool CurrentPlayerHasArchitecture()
        {
            return ((Session.Current.Scenario.CurrentPlayer != null) && (Session.Current.Scenario.CurrentPlayer.ArchitectureCount > 0));
        }

        public bool CurrentPlayerHasPerson()
        {
            return ((Session.Current.Scenario.CurrentPlayer != null) && (Session.Current.Scenario.CurrentPlayer.PersonCount > 0));
        }

        public bool CurrentPlayerHasTroop()
        {
            return ((Session.Current.Scenario.CurrentPlayer != null) && (Session.Current.Scenario.CurrentPlayer.TroopCount > 0));
        }

        private void DemolishCurrentRouteway()
        {
            Session.Current.Scenario.RemoveRouteway(this.CurrentRouteway);
        }

        public void Draw(GameTime gameTime)
        {            
            //base.Draw(gameTime);
            //Begin(SpriteSortMode.Deferred);
            //Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            this.Drawing(gameTime);
            //End();
        }

        private void DrawAutoSavePicture()
        {
            CacheManager.Draw(this.Textures.zidongcundangtupian, StaticMethods.GetViewportCenterRectangle(this.Textures.zidongcundangtupian.Width, this.Textures.zidongcundangtupian.Height, base.viewportSize), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0001f);            
        }

        private void DrawArchitectureSurvey()   //绘制城市情况表
        {
            if ((this.Plugins.ArchitectureSurveyPlugin.Showing && (this.viewMove == ViewMove.Stop)) && (this.Plugins.ArchitectureSurveyPlugin != null))
            {
                this.Plugins.ArchitectureSurveyPlugin.Draw();
            }
        }

        private void DrawCommentText()  //绘制底部地图注释
        {
            if (this.Plugins.ConmentTextPlugin != null)
            {
                this.Plugins.ConmentTextPlugin.Draw();
            }
        }

        public void DrawContextMenu()         //绘制建筑命令菜单
        {
            if (this.Plugins.ContextMenuPlugin.IsShowing)
            {
                this.Plugins.ContextMenuPlugin.Draw();
            }
        }

        public void DrawDialog()
        {
            this.Plugins.HelpPlugin.Draw();
            this.Plugins.OptionDialogPlugin.Draw();
            this.Plugins.SimpleTextDialogPlugin.Draw();
            this.Plugins.tupianwenziPlugin.Draw();
            //this.Plugins.tupianwenziPlugin.Draw();
            this.Plugins.ConfirmationDialogPlugin.Draw();
            this.Plugins.TransportDialogPlugin.Draw();
            this.Plugins.CreateTroopPlugin.Draw();
            this.Plugins.MarshalSectionDialogPlugin.Draw();
        }

        public void Drawtupianwenzi()
        {

            this.Plugins.tupianwenziPlugin.Draw();

        }

        public void DrawFrameRate()
        {
            this.frameCounter++;
            string str = string.Format("fps: {0}", this.frameRate);
            Platform.MainGame.Window.Title = str;
        }

        public void DrawGameFrame()
        {

            if (this.Plugins.GameFramePlugin.IsShowing)
            {
                this.Plugins.GameFramePlugin.Draw();
            }
        }
        

        private void Drawing(GameTime gameTime)            //绘制游戏屏幕
        {
            this.mainMapLayer.Draw(base.viewportSize);
            this.architectureLayer.Draw(base.viewportSize, gameTime);
            this.routewayLayer.Draw(base.viewportSize);

            this.cloudLayer.Draw();

            if (this.dantiaoLayer != null)
            {
                this.dantiaoLayer.Draw();
            }

            this.tileAnimationLayer.Draw(base.viewportSize);
            
            this.troopLayer.Draw(base.viewportSize, gameTime);

            this.mapVeilLayer.Draw(base.viewportSize);

            switch (base.UndoneWorks.Peek().Kind)
            {
                case UndoneWorkKind.None:
                    if (StaticMethods.PointInViewport(base.MousePosition, base.viewportSize))
                    {
                        this.DrawCommentText();
                        this.DrawArchitectureSurvey();
                        this.DrawTroopSurvey();
                    }
                    this.DrawRoutewayEditor();
                    this.Plugins.ToolBarPlugin.DrawTools = true;
                    //    this.Showyoucelan(UndoneWorkKind.None, FrameKind.Architecture, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Architectures, null, "", "");
                    //this.Plugins.youcelanPlugin.Draw(); 

                    //if (!this.Plugins.youcelanPlugin.IsShowing)
                    //{
                        //this.Showyoucelan(UndoneWorkKind.None, FrameKind.Architecture, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Architectures, null, "", "");
                    //}

                    this.Plugins.youcelanPlugin.Draw(); 


                    break;

                case UndoneWorkKind.ContextMenu:
                    this.DrawContextMenu();
                    if (this.bianduiLiebiaoBiaoji  == "ArchitectureLeftClick")
                    {
                        this.Plugins.BianduiLiebiao.Draw();
                        if (StaticMethods.PointInViewport(base.MousePosition, base.viewportSize))
                        {
                            this.DrawArchitectureSurvey();
                        }
                    }
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;

                case UndoneWorkKind.Frame:
                    this.DrawGameFrame();
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;

                case UndoneWorkKind.Dialog:
                    this.DrawDialog();
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;
                case UndoneWorkKind.tupianwenzi:
                    this.Drawtupianwenzi();
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;
                case UndoneWorkKind.liangdaobianji:
                    if (StaticMethods.PointInViewport(base.MousePosition, base.viewportSize))
                    {
                        this.DrawCommentText();
                        this.DrawArchitectureSurvey();
                        this.DrawTroopSurvey();
                    }
                    this.DrawRoutewayEditor();

                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;
                case UndoneWorkKind.SubDialog:
                    this.DrawSubDialog();
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;

                case UndoneWorkKind.Selecting:
                    this.DrawCommentText();
                    this.selectingLayer.Draw(base.viewportSize);
                    this.Plugins.ToolBarPlugin.DrawTools = true;
                    this.Plugins.youcelanPlugin.Draw(); 
                    break;

                case UndoneWorkKind.Inputer:
                    this.DrawInputer();
                    this.Plugins.ToolBarPlugin.DrawTools = false;
                    break;

                case UndoneWorkKind.Selector:
                    if (StaticMethods.PointInViewport(base.MousePosition, base.viewportSize))
                    {
                        this.DrawCommentText();
                    }
                    this.DrawSelector();

                    this.Plugins.ToolBarPlugin.DrawTools = true;
                    this.Plugins.youcelanPlugin.Draw(); 

                    break;

                case UndoneWorkKind.MapViewSelector:
                    if (StaticMethods.PointInViewport(base.MousePosition, base.viewportSize))
                    {
                        this.DrawCommentText();
                    }
                    if (this.Plugins.MapViewSelectorPlugin.Kind == MapViewSelectorKind.建筑)
                    {
                        this.DrawArchitectureSurvey();
                    }
                    this.DrawMapViewSelector();
                    break;
            }
            this.DrawScreenBlind();
            this.DrawPersonBubble();
            this.DrawToolBar(gameTime);

            if (this.Plugins.ToolBarPlugin != null)
            {
                ((ToolBarPlugin.ToolBarPlugin)this.Plugins.ToolBarPlugin).backTool.Draw();
            }

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.UWP && !Platform.IsMobile)
            {
                this.DrawMouseArrow();
            }

        }

        private void DrawInputer()
        {
            this.Plugins.NumberInputerPlugin.Draw();
        }

        private void DrawMapViewSelector()
        {
            if (this.Plugins.MapViewSelectorPlugin != null)
            {
                this.Plugins.MapViewSelectorPlugin.Draw();
            }
        }

        private void DrawMouseArrow()
        {
            if (base.MouseArrowTexture != null)
            {
                CacheManager.Draw(base.MouseArrowTexture.Name, InputManager.Position, null, Color.White, SpriteEffects.None, 1f);
                //base.CacheManager.Draw(base.MouseArrowTexture, new Vector2((float)InputManager.NowMouse.X, (float)InputManager.NowMouse.Y), null, Color.White, 0f, Vector2.Zero, (float)1f, SpriteEffects.None, 0f);
            }
        }

        private void DrawPersonBubble()
        {
            if (this.Plugins.PersonBubblePlugin != null)
            {
                this.Plugins.PersonBubblePlugin.Draw();
            }
        }

        private void DrawRoutewayEditor()
        {
            if (this.Plugins.RoutewayEditorPlugin != null)
            {
                this.Plugins.RoutewayEditorPlugin.Draw();
            }
        }

        private void DrawScreenBlind()
        {
            this.Plugins.ScreenBlindPlugin.Draw();
        }

        private void DrawSelector()
        {
            if (this.DrawingSelector)
            {
                CacheManager.Draw(this.Textures.SelectorTexture, 
                    new Rectangle(Math.Min(base.MousePosition.X, this.SelectorStartPosition.X), Math.Min(base.MousePosition.Y, this.SelectorStartPosition.Y), 
                        Math.Abs(base.MousePosition.X - this.SelectorStartPosition.X), Math.Abs(base.MousePosition.Y - this.SelectorStartPosition.Y)), 
                    null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.72f);
            }
        }

        public void DrawSubDialog()
        {
            this.Plugins.PersonDetailPlugin.Draw();
            this.Plugins.TroopDetailPlugin.Draw();
            this.Plugins.ArchitectureDetailPlugin.Draw();
            this.Plugins.FactionTechniquesPlugin.Draw();
            this.Plugins.TreasureDetailPlugin.Draw();
        }

        private void DrawToolBar(GameTime gameTime)
        {
            if (this.Plugins.ToolBarPlugin != null)
            {
                //this.Plugins.ToolBarPlugin.Draw();
                this.Plugins.ToolBarPlugin.Draw(gameTime);
            }
        }

        private void DrawTroopSurvey()
        {
            if ((this.Plugins.TroopSurveyPlugin.Showing && (this.viewMove == ViewMove.Stop)) && (this.Plugins.TroopSurveyPlugin != null))
            {
                this.Plugins.TroopSurveyPlugin.Draw();
            }
        }

        private void Game_Activated(object sender, EventArgs e)
        {
            //this.UpdateViewport();
            this.ResumeMusic();
            base.EnableMouseEvent = true;
        }

        private void Game_Deactivated(object sender, EventArgs e)
        {
            this.PauseMusic();
            base.EnableMouseEvent = false;
        }

        public override Rectangle GetDestination(Point mapPosition)
        {
            return this.mainMapLayer.GetDestination(mapPosition);
        }

        public override Point GetPointByPosition(Point mapPosition)
        {
            return this.mainMapLayer.GetTopCenterPoint(mapPosition);
        }
            
        //public override Texture2D GetPortrait(float id)
        //{
        //    return this.Plugins.PersonPortraitPlugin.GetPortrait(id);
        //}

        public override Point GetPositionByPoint(Point mousePoint)
        {
            return this.mainMapLayer.TranslateCoordinateToTilePosition(mousePoint.X, mousePoint.Y);
        }        

        //public override Texture2D GetSmallPortrait(float id)
        //{
        //    return this.Plugins.PersonPortraitPlugin.GetSmallPortrait(id);
        //}

        //public override Texture2D GetTroopPortrait(float id)
        //{
        //    return this.Plugins.PersonPortraitPlugin.GetTroopPortrait(id);
        //}
        
        //public override Texture2D GetFullPortrait(float id)
        //{
        //    return this.Plugins.PersonPortraitPlugin.GetFullPortrait(id);
        //}
        
        private void HandleDialogResult(Enum kind)
        {
            switch (((DialogKind)kind))
            {
                case DialogKind.Confirmation:
                    /*
                    switch (this.Plugins.ConfirmationDialogPlugin.Result)
                    {
                    }
                    */  //原程序，由于警告去掉
                    break;
            }
        }

        private void HandleFrameResult(FrameResult result)
        {
            switch (result)
            {
                case FrameResult.OK:
                    this.screenManager.HandleFrameFunction(this.Plugins.GameFramePlugin.Function);

                    
                    break;
                
            }
        }

        private int oldDialogShowTime = -1;
        private bool? oldEnableCheat = null;
        private bool? oldSkyEye = null;

        private void StartAutoplayMode()
        {
            Session.Current.Scenario.CurrentPlayer = null;
            oldSkyEye = Session.GlobalVariables.SkyEye;
            Session.GlobalVariables.SkyEye = true;
            oldDialogShowTime = Setting.Current.GlobalVariables.DialogShowTime;
            Setting.Current.GlobalVariables.DialogShowTime = 0;
            oldEnableCheat = Session.GlobalVariables.EnableCheat;
            Session.GlobalVariables.EnableCheat = true;
        }

        private void StopAutoplayMode()
        {
            if (oldSkyEye != null)
            {
                Session.GlobalVariables.SkyEye = oldSkyEye.Value;
            }
            if (oldEnableCheat != null)
            {
                Session.GlobalVariables.EnableCheat = oldEnableCheat.Value;
            }
            if (this.oldDialogShowTime >= 0)
            {
                Setting.Current.GlobalVariables.DialogShowTime = this.oldDialogShowTime;
            }
        }

        public void changeFaction()
        {
            GameDelegates.VoidFunction function = null;
            this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Faction, FrameFunction.Browse, true, true, true, true, Session.Current.Scenario.Factions, Session.Current.Scenario.PlayerFactions, "变更控制", "");
            if (function == null)
            {
                function = delegate
                {
                    FacilityList list = new FacilityList();
                    foreach (Faction faction in Session.Current.Scenario.PlayerFactions)
                    {
                        list.Add(faction);
                    }
                    Session.Current.Scenario.SetPlayerFactionList(this.Plugins.TabListPlugin.SelectedItemList as GameObjectList);
                    foreach (Faction faction in list)
                    {
                        if (!Session.Current.Scenario.IsPlayer(faction))
                        {
                            faction.EndControl();
                        }
                    }
                    foreach (Faction faction in Session.Current.Scenario.PlayerFactions)
                    {
                        if (!list.HasGameObject(faction))
                        {
                            foreach (Routeway routeway in faction.Routeways.GetList())
                            {
                                if (!routeway.IsInUsing)
                                {
                                    Session.Current.Scenario.RemoveRouteway(routeway);
                                }
                            }
                        }
                    }
                    if (!Session.Current.Scenario.IsPlayer(Session.Current.Scenario.CurrentPlayer))
                    {
                        if (Session.Current.Scenario.CurrentPlayer != null)
                        {
                            Session.Current.Scenario.CurrentPlayer.Passed = true;
                            Session.Current.Scenario.CurrentPlayer.Controlling = false;
                        }
                        if (!Session.Current.Scenario.Factions.HasFactionInQueue(Session.Current.Scenario.PlayerFactions))
                        {
                            this.Plugins.DateRunnerPlugin.Reset();
                            this.Plugins.DateRunnerPlugin.RunDays(1);
                        }
                    }
                    if (Session.Current.Scenario.PlayerFactions.Count == 0)
                    {
                        this.StartAutoplayMode();
                    }
                    else
                    {
                        this.StopAutoplayMode();
                    }
                };
            }
            this.Plugins.GameFramePlugin.SetOKFunction(function);
        }

        private void HandlePushSelectingUndoneWork(Enum kind)
        {
            switch (((SelectingUndoneWorkKind)kind))
            {
                case SelectingUndoneWorkKind.ArchitectureAvailableContactArea:
                    if ((this.CurrentArchitecture != null) && (this.CurrentMilitary != null))
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.ArchitectureAvailableContactArea;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetAllAvailableArea(false);
                        this.CurrentMilitary.ModifyAreaByTerrainAdaptablity(this.selectingLayer.Area);
                    }
                    break;

                case SelectingUndoneWorkKind.ConvincePersonPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.ConvincePersonPosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetConvincePersonArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.AssassinatePosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.AssassinatePosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetAssassinateArchitectureArea((this.CurrentPersons[0] as Person).BelongedFaction);
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.WujiangDiaodong:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.WujiangDiaodong;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetPersonTransferArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.MoveFeizi:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.MoveFeizi;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetFeiziTransferArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.MoveCaptive: //俘虏可移动
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.MoveCaptive;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetCaptiveTransferArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;
                    /*
                case SelectingUndoneWorkKind.MilitaryTransfer: //运输编队
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.MilitaryTransfer;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetMilitaryTransferArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;
                    */
                

                case SelectingUndoneWorkKind.InformationPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.InformationPosition;
                        this.selectingLayer.Area = new GameArea();
                        this.selectingLayer.EffectingAreaRadius = this.CurrentPerson.RadiusIncrementOfInformation + this.CurrentPerson.CurrentInformationKind.Radius;
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = false;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;
                    /*
                case SelectingUndoneWorkKind.SpyPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.SpyPosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetSpyArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;
                    */

                case SelectingUndoneWorkKind.DestroyPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.DestroyPosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetDestroyArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.InstigatePosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.InstigatePosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetInstigateArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.GossipPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.GossipPosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetGossipArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.JailBreakPosition:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.JailBreakPosition;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetJailBreakArchitectureArea();
                        this.selectingLayer.ShowComment = true;
                        this.selectingLayer.SingleWay = true;
                        this.selectingLayer.FromArea = this.CurrentArchitecture.ArchitectureArea;
                    }
                    break;

                case SelectingUndoneWorkKind.TroopDestination:
                    if (this.CurrentTroop != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.TroopDestination;
                        this.selectingLayer.Area = this.CurrentTroop.GetDayArea(1);
                    }
                    break;

                case SelectingUndoneWorkKind.SelectorTroopsDestination:
                    if (this.SelectorTroops.Count > 0)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.SelectorTroopsDestination;
                        this.selectingLayer.Area = new GameArea();
                    }
                    break;

                case SelectingUndoneWorkKind.TroopTarget:
                    if (this.CurrentTroop != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.TroopTarget;
                        if (this.CurrentTroop.CurrentCombatMethod != null)
                        {
                            this.selectingLayer.Area = this.CurrentTroop.GetTargetArea(false, this.CurrentTroop.CurrentCombatMethod.ArchitectureTarget);
                        }
                        else if (this.CurrentTroop.CurrentStratagem != null)
                        {
                            this.selectingLayer.Area = this.CurrentTroop.GetTargetArea(this.CurrentTroop.CurrentStratagem.Friendly, this.CurrentTroop.CurrentStratagem.ArchitectureTarget);
                        }
                        else
                        {
                            this.selectingLayer.Area = this.CurrentTroop.GetTargetArea(false, true);
                        }
                    }
                    break;

                case SelectingUndoneWorkKind.Trooprucheng :
                    if (this.CurrentTroop != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.Trooprucheng ;

                        this.selectingLayer.Area = this.CurrentTroop.GetruchengArea(true);
                        
                    }
                    break;

                case SelectingUndoneWorkKind.TroopInvestigatePosition:
                    if (this.CurrentTroop != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.TroopInvestigatePosition;
                        this.selectingLayer.Area = Session.Current.Scenario.GetAreaWithinDistance(this.CurrentTroop.Position, this.CurrentTroop.ViewRadius + 1, false);
                        this.selectingLayer.EffectingAreaRadius = this.CurrentTroop.InvestigateRadius;
                    }
                    break;

                case SelectingUndoneWorkKind.TroopSetFirePosition:
                    if (this.CurrentTroop != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.TroopSetFirePosition;
                        this.selectingLayer.Area = this.CurrentTroop.GetSetFireArea();
                    }
                    break;

                case SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint;
                        this.selectingLayer.Area = this.CurrentArchitecture.GetRoutewayStartPoints();
                    }
                    break;

                case SelectingUndoneWorkKind.RoutewayPointShortestNormal:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.RoutewayPointShortestNormal;
                        this.selectingLayer.Area = new GameArea();
                    }
                    break;

                case SelectingUndoneWorkKind.RoutewayPointShortestNoWater:
                    if (this.CurrentArchitecture != null)
                    {
                        this.selectingLayer.AreaFrameKind = SelectingUndoneWorkKind.RoutewayPointShortestNoWater;
                        this.selectingLayer.Area = new GameArea();
                    }
                    break;
            }
            this.selectingLayer.TryToShow();
        }

        private void HandleSelectingResult(Enum kind)
        {
            Architecture targetArchitecture;
            Routeway routeway;


            switch (((SelectingUndoneWorkKind)kind))
            {
                case SelectingUndoneWorkKind.None:
                case SelectingUndoneWorkKind.SearchPosition:
                    return;

                case SelectingUndoneWorkKind.ArchitectureAvailableContactArea:
                    if (!this.selectingLayer.Canceled)
                    {
                        if(this.CurrentMilitaries.Count==1 && this.CurrentMilitary!=null )
                        {
                            this.screenManager.SetCreatingTroopPosition(this.selectingLayer.SelectedPoint);
                        }
                        else if (this.CurrentMilitaries.Count > 1)
                        {
                            this.screenManager.SetTroopsPosition(this.selectingLayer.SelectedPoint);
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.ConvincePersonPosition:
                    if (!this.selectingLayer.Canceled && (this.CurrentPersons.Count > 0))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        if (architectureByPosition != null)
                        {
                            this.CurrentArchitecture = architectureByPosition;
                            foreach (Person person in this.CurrentPersons)
                            {
                                person.OutsideDestination = new Point?(this.selectingLayer.SelectedPoint);
                            }
                            this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetConvinceDestinationPerson, false, true, true, false, architectureByPosition.GetConvinceDestinationPersonList((this.CurrentPersons[0] as Person).BelongedFaction), null, "说服", "Personal");
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.AssassinatePosition:
                    if (!this.selectingLayer.Canceled && (this.CurrentPersons.Count > 0))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        if (architectureByPosition != null)
                        {
                            this.CurrentArchitecture = architectureByPosition;
                            foreach (Person person in this.CurrentPersons)
                            {
                                person.OutsideDestination = new Point?(this.selectingLayer.SelectedPoint);
                            }
                            this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetAssassinatePersonTarget, false, true, true, false, architectureByPosition.GetAssassinatePersonTarget((this.CurrentPersons[0] as Person).BelongedFaction), null, "暗杀", "Personal");
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.WujiangDiaodong:
                    if (!this.selectingLayer.Canceled && (this.CurrentPersons.Count > 0))
                    {
                           Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                           if (architectureByPosition != null)
                           {

                               this.screenManager.FrameFunction_Architecture_AfterGetOneArchitectureBySelecting(architectureByPosition);
                           }
                    }
                    return;

                case SelectingUndoneWorkKind.MoveFeizi:
                    if (!this.selectingLayer.Canceled && (this.CurrentPersons != null))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        if (architectureByPosition != null)
                        {

                            this.screenManager.FrameFunction_Architecture_AfterGetOneArchitectureBySelecting(architectureByPosition);
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.MoveCaptive: //移动俘虏
                    if (!this.selectingLayer.Canceled && (this.CurrentPersons != null))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        if (architectureByPosition != null)
                        {

                            this.screenManager.FrameFunction_Architecture_AfterGetMoveCaptiveArchitectureBySelecting(architectureByPosition);
                        }
                    }
                    return;
                    /*
                case SelectingUndoneWorkKind.MilitaryTransfer: //运输编队
                    if (!this.selectingLayer.Canceled && (this.CurrentMilitaries != null))
                    {
                        Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        if (architectureByPosition != null)
                        {

                            this.screenManager.FrameFunction_Architecture_AfterGetTransferMilitaryArchitectureBySelecting(architectureByPosition);

                            /*foreach (Military military in this.CurrentMilitaries)
                            {
                                architectureByPosition.AddMilitary(military);
                                this.CurrentArchitecture.RemoveMilitary(military);
                            }
                        }
                    }
                    return;
                     */


                case SelectingUndoneWorkKind.InformationPosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        this.CurrentPerson.GoForInformation(this.selectingLayer.SelectedPoint);
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;
                    /*
                case SelectingUndoneWorkKind.SpyPosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        foreach (Person person in this.CurrentPersons)
                        {
                            person.GoForSpy(this.selectingLayer.SelectedPoint);
                        }
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;
                    */
                case SelectingUndoneWorkKind.DestroyPosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        foreach (Person person in this.CurrentPersons)
                        {
                            person.GoForDestroy(this.selectingLayer.SelectedPoint);
                        }
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;

                case SelectingUndoneWorkKind.InstigatePosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        foreach (Person person in this.CurrentPersons)
                        {
                            person.GoForInstigate(this.selectingLayer.SelectedPoint);
                        }
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;

                case SelectingUndoneWorkKind.GossipPosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        foreach (Person person in this.CurrentPersons)
                        {
                            person.GoForGossip(this.selectingLayer.SelectedPoint);
                        }
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;

                case SelectingUndoneWorkKind.JailBreakPosition:
                    if (!this.selectingLayer.Canceled)
                    {
                        foreach (Person person in this.CurrentPersons)
                        {
                            person.GoForJailBreak(this.selectingLayer.SelectedPoint);
                        }
                        base.PlayNormalSound("Content/Sound/Tactics/Outside");
                    }
                    return;

                case SelectingUndoneWorkKind.TroopDestination:   //移动
                    if (this.selectingLayer.Canceled)
                    {
                        return;
                    }
                    targetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                    this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                    this.CurrentTroop.TargetTroop = null;
                    this.CurrentTroop.WillTroop = null;
                    this.CurrentTroop.TargetArchitecture = null;
                    this.CurrentTroop.WillArchitecture = null;

                    if (targetArchitecture != null)
                    {
                        this.CurrentTroop.WillArchitecture = targetArchitecture;
                        this.CurrentTroop.BelongedLegion.WillArchitecture = targetArchitecture;
                        if (this.CurrentTroop.BelongedFaction.IsFriendly(targetArchitecture.BelongedFaction))
                        {
                            this.CurrentTroop.BelongedLegion.Kind = LegionKind.Defensive;
                        }
                        else
                        {
                            this.CurrentTroop.BelongedLegion.Kind = LegionKind.Offensive;
                        }
                    }

                    this.CurrentTroop.SelectedMove = true;
                    this.CurrentTroop.mingling = "Move";

                    break;

                case SelectingUndoneWorkKind.Trooprucheng :   //入城
                    if (this.selectingLayer.Canceled)
                    {
                        return;
                    }

                    targetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);

                    if (this.CurrentTroop.CanEnter() && this.CurrentTroop.EnterList.GameObjects.Contains(targetArchitecture))
                    {
                        this.CurrentTroop.Enter(targetArchitecture);
                        this.CurrentTroop = null;
                        //this.Plugins.AirViewPlugin.ReloadTroopView();
                        Session.Current.Scenario.ClearPersonStatusCache();
                        return;
                    }
                    
                    this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                    this.CurrentTroop.TargetTroop = null;
                    this.CurrentTroop.WillTroop = null;
                    this.CurrentTroop.TargetArchitecture = null;
                    this.CurrentTroop.WillArchitecture = null;
                    this.CurrentTroop.mingling = "Enter";
                    if (targetArchitecture != null)
                    {
                        this.CurrentTroop.TargetArchitecture = targetArchitecture;

                    }

                    this.CurrentTroop.SelectedMove = true;

                    break;


                case SelectingUndoneWorkKind.SelectorTroopsDestination:
                    if (!this.selectingLayer.Canceled)
                    {
                        targetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.selectingLayer.SelectedPoint);
                        Troop targetTroop = Session.Current.Scenario.GetTroopByPosition(this.selectingLayer.SelectedPoint);
                        foreach (Troop troop in this.SelectorTroops)
                        {
                            if (!troop.SelectedMove && !troop.SelectedAttack)
                            {
                                if (targetArchitecture != null)
                                {
                                    if (targetTroop != null && troop.Army.Kind.AirOffence)
                                    {
                                        troop.TargetTroop = targetTroop;
                                    }
                                    else
                                    {
                                        troop.TargetArchitecture = targetArchitecture;
                                    }
                                    troop.WillArchitecture = targetArchitecture;
                                    troop.BelongedLegion.WillArchitecture = targetArchitecture;
                                    if (targetArchitecture.BelongedFaction == troop.BelongedFaction)
                                    {
                                        troop.TargetTroop = null;
                                        troop.WillTroop = null;
                                        troop.mingling = "Enter";
                                    }
                                    else
                                    {
                                        troop.mingling = "Attack";
                                    }

                                    troop.SelectedAttack = true;
                                   
                                }
                                else if (targetTroop != null)
                                {
                                    troop.TargetTroop = targetTroop;
                                    troop.WillTroop = targetTroop;

                                    troop.SelectedAttack = true;
                                    troop.mingling = "Attack";
                                }
                                else
                                {
                                    troop.mingling = "Move";
                                }
                                troop.RealDestination = this.selectingLayer.SelectedPoint;
                                if (!((targetArchitecture == null) || troop.BelongedFaction.IsFriendly(targetArchitecture.BelongedFaction)))
                                {
                                    troop.BelongedLegion.Kind = LegionKind.Offensive;
                                }
                                else
                                {
                                    troop.BelongedLegion.Kind = LegionKind.Defensive;
                                }
                                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.TroopMoveTo, "Destination");

                                troop.SelectedMove = true;
                            }
                        }
                    }
                    this.SelectorTroops.Clear();
                    return;

                case SelectingUndoneWorkKind.TroopTarget:  //选目标
                    {
                        if (this.selectingLayer.Canceled)
                        {
                            this.CurrentTroop.AttackTargetKind = TroopAttackTargetKind.遇敌;
                            if (this.CurrentTroop.CurrentStratagem != null)
                            {
                                this.CurrentTroop.CastTargetKind = TroopCastTargetKind.可能;
                            }
                            
                            if (this.CurrentTroop.Status == TroopStatus.埋伏)
                            {
                                this.CurrentTroop.EndAmbush();
                            }
                            return;
                        }
                        //////////////////////////////////////////////////////////////////////////////
                        
                        Troop troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(this.selectingLayer.SelectedPoint);
                        if ((troopByPositionNoCheck == null) || !this.CurrentTroop.BelongedFaction.IsPositionKnown(this.selectingLayer.SelectedPoint))
                        {
                            this.CurrentTroop.TargetTroop = null;
                            if (!this.CurrentTroop.SelectedMove)
                            {
                                this.CurrentTroop.WillTroop = null;
                                this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                            }
                        }
                        else
                        {
                            this.CurrentTroop.TargetTroop = troopByPositionNoCheck;
                            //this.CurrentTroop.TargetArchitecture = null;
                            if (!this.CurrentTroop.SelectedMove)
                            {
                                this.CurrentTroop.WillTroop = troopByPositionNoCheck;
                                this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                                //this.CurrentTroop.WillArchitecture = null;
                            }
                        }
                        Architecture architectureByPositionNoCheck = Session.Current.Scenario.GetArchitectureByPositionNoCheck(this.selectingLayer.SelectedPoint);
                        if (architectureByPositionNoCheck != null)
                        {
                            this.CurrentTroop.TargetArchitecture = architectureByPositionNoCheck;
                            //this.CurrentTroop.TargetTroop = null;
                            if (!this.CurrentTroop.SelectedMove)
                            {
                                this.CurrentTroop.WillArchitecture = architectureByPositionNoCheck;
                                //this.CurrentTroop.WillTroop = null;
                                if (!this.CurrentTroop.CanAttack(architectureByPositionNoCheck))
                                {
                                    this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                                }
                                else
                                {
                                    this.CurrentTroop.RealDestination = this.CurrentTroop.Position;
                                }
                            }
                            
                        }
                        else
                        {
                            this.CurrentTroop.TargetArchitecture = null;
                            if (!this.CurrentTroop.SelectedMove)
                            {
                                this.CurrentTroop.WillArchitecture = null;
                            }
                        }

                        this.CurrentTroop.SelectedMove = true;
                        this.CurrentTroop.SelectedAttack = true;
                        if (this.CurrentTroop.mingling != "Move" && this.CurrentTroop.mingling != "Stratagem" && this.CurrentTroop.mingling != "Enter")
                        {
                            this.CurrentTroop.mingling = "Attack";
                        }

                        ///////////////////////////////////////////////////////////////////////////////////
                        /*
                        Troop troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(this.selectingLayer.SelectedPoint);
                        Architecture architectureByPositionNoCheck = Session.Current.Scenario.GetArchitectureByPositionNoCheck(this.selectingLayer.SelectedPoint);
                        bool youjianzhu=false ;
                        bool youdijun = false;
                        youjianzhu = (architectureByPositionNoCheck != null && architectureByPositionNoCheck.Endurance > 0);
                        youdijun = troopByPositionNoCheck != null && (this.CurrentTroop.BelongedFaction.IsPositionKnown(this.selectingLayer.SelectedPoint)||Session.GlobalVariables.SkyEye);
                        if (!youjianzhu && !youdijun)
                        {
                            this.CurrentTroop.TargetTroop = null;
                            this.CurrentTroop.TargetArchitecture = null;
                            this.CurrentTroop.mingling = "——";

                            return;
                        }
                        else if (youjianzhu && !youdijun)
                        {
                            this.CurrentTroop.TargetArchitecture = architectureByPositionNoCheck;
                            this.CurrentTroop.TargetTroop = null;
                            this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                            this.CurrentTroop.mingling = "攻击建筑";

                        }
                        else if (!youjianzhu && youdijun)
                        {
                            this.CurrentTroop.TargetTroop = troopByPositionNoCheck;
                            this.CurrentTroop.TargetArchitecture = null;
                            this.CurrentTroop.mingling = "攻击军队";

                        }
                        else if (youjianzhu && youdijun)
                        {
                            //if (this.CurrentTroop.Army.Kind.Type == MilitaryType.弩兵 || this.CurrentTroop.Army.KindID == 26 || troopByPositionNoCheck.BelongedFaction == this.CurrentTroop.BelongedFaction )
                            if (this.CurrentTroop.Army.Kind.AirOffence || this.CurrentTroop.CurrentStratagem != null)

                            {
                                this.CurrentTroop.TargetTroop = troopByPositionNoCheck;
                                this.CurrentTroop.TargetArchitecture = null;
                                this.CurrentTroop.mingling = "攻击军队";

                            }
                            else
                            {
                                this.CurrentTroop.TargetArchitecture = architectureByPositionNoCheck;
                                this.CurrentTroop.TargetTroop = null;
                                this.CurrentTroop.RealDestination = this.selectingLayer.SelectedPoint;
                                this.CurrentTroop.mingling = "攻击建筑";

                            }
                        }
                        */
                        /////////////////////////////////////////////////////////////////////////////////////

                        //this.Plugins.PersonBubblePlugin.AddPerson(this.CurrentTroop.Leader, this.CurrentTroop.Position, "Target");
                        return;
                    }
                case SelectingUndoneWorkKind.TroopInvestigatePosition:  //让军队侦查
                    if (this.selectingLayer.Canceled)
                    {
                        this.CurrentTroop.CurrentStratagem = null;

                        return;
                    }
                    this.CurrentTroop.SelfCastPosition = this.selectingLayer.SelectedPoint;

                    this.CurrentTroop.SelectedAttack = true;
                    this.CurrentTroop.mingling = "Stratagem";

                    return;

                case SelectingUndoneWorkKind.TroopSetFirePosition:
                    if (this.selectingLayer.Canceled)
                    {
                        this.CurrentTroop.CurrentStratagem = null;
                        return;
                    }
                    this.CurrentTroop.SelfCastPosition = this.selectingLayer.SelectedPoint;

                    this.CurrentTroop.SelectedAttack = true;
                    this.CurrentTroop.mingling = "Stratagem";

                    return;

                case SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint:
                    if (!this.selectingLayer.Canceled)
                    {
                        routeway = this.CurrentArchitecture.CreateRouteway(this.selectingLayer.SelectedPoint);
                        if (routeway != null)
                        {
                            this.Plugins.RoutewayEditorPlugin.SetRouteway(routeway);
                            this.Plugins.RoutewayEditorPlugin.IsShowing = true;
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.RoutewayPointShortestNormal:
                    if (!this.selectingLayer.Canceled)
                    {
                        routeway = this.CurrentArchitecture.BuildShortestRouteway(this.selectingLayer.SelectedPoint, false);
                        if (routeway != null)
                        {
                            routeway.Building = true;
                            Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
                        }
                    }
                    return;

                case SelectingUndoneWorkKind.RoutewayPointShortestNoWater:
                    if (!this.selectingLayer.Canceled)
                    {
                        routeway = this.CurrentArchitecture.BuildShortestRouteway(this.selectingLayer.SelectedPoint, true);
                        if (routeway != null)
                        {
                            routeway.Building = true;
                            Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
                        }
                    }
                    return;

                default:
                    return;
            }
            this.Plugins.PersonBubblePlugin.AddPerson(this.CurrentTroop.Leader, this.CurrentTroop.Position, TextMessageKind.TroopMoveTo, "Destination");
        }



        public override void JumpTo(Point mapPosition)    //地图跳转
        {
            int num = (this.mainMapLayer.TileWidth * mapPosition.X) + (this.mainMapLayer.TileWidth / 2);
            int num2 = (this.mainMapLayer.TileHeight * mapPosition.Y) + (this.mainMapLayer.TileHeight / 2);
            this.mainMapLayer.LeftEdge = (this.viewportSize.X / 2) - num;
            if (this.mainMapLayer.LeftEdge > 0)
            {
                this.mainMapLayer.LeftEdge = 0;
            }
            else if (this.mainMapLayer.LeftEdge < (this.viewportSize.X - this.mainMapLayer.TotalTileWidth))
            {
                this.mainMapLayer.LeftEdge = this.viewportSize.X - this.mainMapLayer.TotalTileWidth;
            }
            this.mainMapLayer.TopEdge = (this.viewportSize.Y / 2) - num2;
            if (this.mainMapLayer.TopEdge > 0)
            {
                this.mainMapLayer.TopEdge = 0;
            }
            else if (this.mainMapLayer.TopEdge < (this.viewportSize.Y - this.mainMapLayer.TotalTileHeight))
            {
                this.mainMapLayer.TopEdge = this.viewportSize.Y - this.mainMapLayer.TotalTileHeight;
            }
            this.ResetScreenEdge();
            this.mainMapLayer.ReCalculateTileDestination(this);
            this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, this.mainMapLayer.LeftEdge, this.mainMapLayer.TopEdge, this.mainMapLayer.TotalMapSize);

            if (Session.MainGame.mainGameScreen == null)
            {

            }
            else
            {
                Session.MainGame.mainGameScreen.cloudLayer.Start();
            }
        }


        private bool MoveTheTroops(GameTime gameTime)
        {
            if (!Session.Current.Scenario.Threading)
            {
                if (!Session.Current.Scenario.Animating)
                {
                    Session.Current.Scenario.Troops.CurrentQueueTroopMove();
                    if (Session.Current.Scenario.Troops.TotallyEmpty)
                    {
                        return false;
                    }
                }
                //else if (gameTime.ElapsedRealTime.TotalMilliseconds > Session.GlobalVariables.MaxTimeOfAnimationFrame)
                else if (gameTime.ElapsedGameTime.TotalMilliseconds > Session.GlobalVariables.MaxTimeOfAnimationFrame)
                {
                    Session.Current.Scenario.Troops.StepAnimationIndex(1);
                }
            }
            return true;
        }

        public void PauseMusic()
        {
            try
            {
                if (Session.GlobalVariables.PlayMusic)  // && (this.Player.playState == WMPPlayState.wmppsPlaying))
                {
                    Platform.Current.PauseSong();
                    //this.Player.pause();
                }
            }
            //catch (System.Runtime.InteropServices.COMException ex)
            catch
            {
                //do not let an ignorable exception break the game!
            }
        }



        private void Player_PlayStateChange(int NewState)
        {
            if (Session.GlobalVariables.PlayMusic && (NewState == 1))
            {
                Platform.Current.ResumeSong();
                //this.Player.play();
            }
        }

        //public override void PlayMusic(string musicFileLocation)
        //{
        //    if (Session.GlobalVariables.PlayMusic && File.Exists(musicFileLocation))
        //    {
        //        this.Player.URL = musicFileLocation;
        //    }
        //}

        public override UndoneWorkItem PopUndoneWork()
        {
            UndoneWorkItem item = base.PopUndoneWork();
            if (this.PeekUndoneWork().Kind == UndoneWorkKind.None)
            {
                this.Plugins.ToolBarPlugin.Enabled = true;
            }
            //base.previousMouseState = base.mouseState;
            switch (item.Kind)
            {

                case UndoneWorkKind.ContextMenu:
                    this.HandleContextMenuResult(this.Plugins.ContextMenuPlugin.Result);
                    this.gengxinyoucelan(); 
                    break;

                case UndoneWorkKind.Frame:
                    this.HandleFrameResult(this.Plugins.GameFramePlugin.Result);


                    this.gengxinyoucelan(); 

                    break;

                case UndoneWorkKind.Dialog:
                    this.HandleDialogResult(item.SubKind);
                    break;
                case UndoneWorkKind.tupianwenzi:
                    break;
                case UndoneWorkKind.liangdaobianji :
                    break;
                case UndoneWorkKind.SubDialog:
                    break;

                case UndoneWorkKind.Selecting:
                    this.HandleSelectingResult(item.SubKind);
                    this.gengxinyoucelan(); 

                    break;
                case UndoneWorkKind.MapViewSelector:
                    //this.screenManager.FrameFunction_Architecture_AfterGetOneArchitectureByMapViewSelector();
                    break;

            }

            if (this.tufashijianzantingyinyue && this.Plugins.tupianwenziPlugin.IsShowing == false)
            {
                this.ResumeMusic();
                this.tufashijianzantingyinyue = false;
            }

            return item;
        }

        public override void PushUndoneWork(UndoneWorkItem undoneWork)
        {
            base.PushUndoneWork(undoneWork);
            if (this.PeekUndoneWork().Kind != UndoneWorkKind.None)
            {
                this.Plugins.ToolBarPlugin.Enabled = false;
            }
            switch (undoneWork.Kind)
            {
                case UndoneWorkKind.Selecting:
                    this.HandlePushSelectingUndoneWork(undoneWork.SubKind);
                    break;
            }
        }

        public void RefreshDisableRects()
        {
            base.ClearDisableRects();
            if (this.Plugins.AirViewPlugin.IsMapShowing)
            {
                this.Plugins.AirViewPlugin.ResetMapPosition(Session.MainGame.mainGameScreen);
                this.Plugins.AirViewPlugin.AddDisableRects();
            }
            if (this.Plugins.GameRecordPlugin.IsRecordShowing)
            {
                this.Plugins.GameRecordPlugin.ResetRecordShowPosition();
                this.Plugins.GameRecordPlugin.AddDisableRects();
            }
            if (this.Plugins.RoutewayEditorPlugin.IsShowing)
            {
                this.Plugins.RoutewayEditorPlugin.AddDisableRects();
            }
            if (this.Plugins.MapViewSelectorPlugin.IsShowing)
            {
                this.Plugins.MapViewSelectorPlugin.AddDisableRects();
            }
        }

        private void ResetCurrentStatus()
        {
            this.lastPosition = this.position;
            this.position = this.mainMapLayer.TranslateCoordinateToTilePosition(InputManager.PoX, InputManager.PoY);
        }

        public override void ResetMouse()
        {
            base.MouseArrowTexture = base.DefaultMouseArrowTexture;
            this.viewMove = ViewMove.Stop;
        }

        private void ResetScreenEdge()
        {
            this.TopLeftPosition.X = -this.mainMapLayer.LeftEdge / Session.Current.Scenario.ScenarioMap.TileWidth;
            this.TopLeftPosition.Y = -this.mainMapLayer.TopEdge / Session.Current.Scenario.ScenarioMap.TileHeight;
            this.BottomRightPosition.X = (this.viewportSize.X - this.mainMapLayer.LeftEdge) / Session.Current.Scenario.ScenarioMap.TileWidth;
            this.BottomRightPosition.Y = (this.viewportSize.Y - this.mainMapLayer.TopEdge) / Session.Current.Scenario.ScenarioMap.TileHeight;
        }

        private void ResetTiles()
        {
            if (Session.Current.Scenario.ScenarioMap != null)
            {
                if (this.mainMapLayer.TotalTileWidth < this.viewportSize.X)
                {
                    this.mainMapLayer.TileWidth = (this.viewportSize.X / Session.Current.Scenario.ScenarioMap.MapDimensions.X) + (Session.Current.Scenario.ScenarioMap.TileWidthMin / 5);
                }
                if (this.mainMapLayer.TotalTileHeight < this.viewportSize.Y)
                {
                    this.mainMapLayer.TileWidth = (this.viewportSize.Y / Session.Current.Scenario.ScenarioMap.MapDimensions.Y) + (Session.Current.Scenario.ScenarioMap.TileWidthMin / 5);
                }
            }
        }

        public void ResumeMusic()
        {
            try
            {
                if (Session.GlobalVariables.PlayMusic)  // && (this.Player.playState == WMPPlayState.wmppsPaused))
                {
                    //this.Player.play();
                    Platform.Current.ResumeSong();
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
            }
        }

        private void RoutewayOptionDialogClickCallback(object obj)
        {
            this.CurrentRouteway = obj as Routeway;
        }

        private bool RunTheFactions(GameTime gameTime)
        {
            try
            {
                Session.Current.Scenario.Factions.RunQueue();
                if (Session.Current.Scenario.Factions.QueueEmpty)
                {
                    return false;
                }
                return true;
            }
            catch (OutOfMemoryException)
            {
                Session.Current.Scenario.DisposeLotsOfMemory();
                return false;
            }
        }

        public override void SaveGame()
        {
            this.Plugins.OptionDialogPlugin.SetStyle("SaveAndLoad");
            this.Plugins.OptionDialogPlugin.SetTitle("存储进度");
            this.Plugins.OptionDialogPlugin.Clear();

            //throw new Exception("SaveGame");

            var saves = GameScenario.LoadScenarioSaves();
            for (int i = 1; i <= GameScenario.savemaxcounts; i++)
            {
                string ss = i < 10 ? "0" + i.ToString() : i.ToString();
                GameDelegates.VoidFunction voidFunction = delegate
                {
                    this.SaveFileName = "Save" + ss + this.SaveFileExtension;
                    this.SaveGameToDisk(this.SaveFileName);
                };
                saves[i].ID = ss;
                this.Plugins.OptionDialogPlugin.AddOption(saves[i].Summary, null, voidFunction);
            }
            this.Plugins.OptionDialogPlugin.EndAddOptions();
            this.Plugins.OptionDialogPlugin.ShowOptionDialog(ShowPosition.Center);
        }

        public void SaveGameToDisk(string LoadedFileName)
        {
            Session.Current.Scenario.EnableLoadAndSave = false;

            try
            {
                this.mainMapLayer.freeTilesMemory();

                if (!Platform.Current.UserDirectoryExist("Save"))
                {
                    Platform.Current.UserDirectoryCreate("Save");
                }

                bool saveMap;

                if (Session.Current.Scenario.UsingOwnCommonData)
                {
                    saveMap = false;
                }
                else
                {
                    saveMap = false;
                }

                Session.Current.Scenario.ScenarioMap.JumpPosition = this.mainMapLayer.GetCurrentScreenCenter(base.viewportSize);
                saveMap = saveMap || this.mapEdited;

                Session.Current.Scenario.SaveGameScenario(LoadedFileName, saveMap, saveMap, true);

                this.mainMapLayer.freeTilesMemory();
            }
            finally
            {

                Session.Current.Scenario.EnableLoadAndSave = true;
            }
        }

        public void SaveGameAutoPosition()
        {
            this.SaveFileName = "Save00" + this.SaveFileExtension; //"AutoSave" + this.SaveFileExtension;
            this.SaveGameToDisk(this.SaveFileName);
        }

        private void SaveGameQuitPosition()
        {
            this.SaveFileName = "QuitSave" + this.SaveFileExtension;
            this.SaveGameToDisk(this.SaveFileName);
        }
 
        public void SaveGameWhenCrash(String _savePath)
        {
            this.SaveFileName = _savePath;
            this.SaveGameToDisk(this.SaveFileName);
        }

        private void Scenario_OnNewFactionAppear(Faction faction)
        {
            if ((faction.Leader != null) && (faction.Capital != null))
            {
                faction.Leader.TextDestinationString = faction.Capital.Name;
                this.Plugins.GameRecordPlugin.AddBranch(faction.Leader, "NewFactionAppear", faction.Leader.Position);
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, this);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(faction.Leader, faction.Leader, TextMessageKind.CreateNewFaction, "NewFactionAppear");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        private void ScrollTheMainMap(GameTime gameTime)
        {
            if (base.EnableScroll)
            {
                if (Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.Win) 
                {
                    if (this.viewMove != ViewMove.Stop)
                    {
                        if ((Math.Abs((int)(InputManager.PoX - Convert.ToInt32(InputManager.PositionPre.X))) <= 2) && (Math.Abs((int)(InputManager.PoY - Convert.ToInt32(InputManager.PositionPre.Y))) <= 2)
                            || this.isKeyScrolling)
                        {
                            // 没有200毫秒的延迟，鼠标滚动屏幕效果也不错
                            //
                            //if (!this.isKeyScrolling && (gameTime.TotalGameTime.TotalMilliseconds - this.lastTime) < 200.0)
                            //{
                            //    return;
                            //}

                            int num = (int)((gameTime.ElapsedGameTime.Milliseconds * Session.GlobalVariables.MapScrollSpeed) * this.scrollSpeedScale);
                            switch (this.viewMove)
                            {
                                case ViewMove.Left:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge += num;
                                        if (this.mainMapLayer.LeftEdge > 0)
                                        {
                                            this.mainMapLayer.LeftEdge = 0;
                                        }
                                    }
                                    break;

                                case ViewMove.Right:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge -= num;
                                        if (this.mainMapLayer.LeftEdge < (this.viewportSize.X - this.mainMapLayer.TotalTileWidth))
                                        {
                                            this.mainMapLayer.LeftEdge = this.viewportSize.X - this.mainMapLayer.TotalTileWidth;
                                        }
                                    }
                                    break;

                                case ViewMove.Top:
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge += num;
                                        if (this.mainMapLayer.TopEdge > 0)
                                        {
                                            this.mainMapLayer.TopEdge = 0;
                                        }
                                    }
                                    break;

                                case ViewMove.Bottom:
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge -= num;
                                        if (this.mainMapLayer.TopEdge < (this.viewportSize.Y - this.mainMapLayer.TotalTileHeight))
                                        {
                                            this.mainMapLayer.TopEdge = this.viewportSize.Y - this.mainMapLayer.TotalTileHeight;
                                        }
                                    }
                                    break;

                                case ViewMove.TopLeft:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge += num;
                                        if (this.mainMapLayer.LeftEdge > 0)
                                        {
                                            this.mainMapLayer.LeftEdge = 0;
                                        }
                                    }
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge += num;
                                        if (this.mainMapLayer.TopEdge > 0)
                                        {
                                            this.mainMapLayer.TopEdge = 0;
                                        }
                                    }
                                    break;

                                case ViewMove.TopRight:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge -= num;
                                        if (this.mainMapLayer.LeftEdge < (this.viewportSize.X - this.mainMapLayer.TotalTileWidth))
                                        {
                                            this.mainMapLayer.LeftEdge = this.viewportSize.X - this.mainMapLayer.TotalTileWidth;
                                        }
                                    }
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge += num;
                                        if (this.mainMapLayer.TopEdge > 0)
                                        {
                                            this.mainMapLayer.TopEdge = 0;
                                        }
                                    }
                                    break;

                                case ViewMove.BottomLeft:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge += num;
                                        if (this.mainMapLayer.LeftEdge > 0)
                                        {
                                            this.mainMapLayer.LeftEdge = 0;
                                        }
                                    }
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge -= num;
                                        if (this.mainMapLayer.TopEdge < (this.viewportSize.Y - this.mainMapLayer.TotalTileHeight))
                                        {
                                            this.mainMapLayer.TopEdge = this.viewportSize.Y - this.mainMapLayer.TotalTileHeight;
                                        }
                                    }
                                    break;

                                case ViewMove.BottomRight:
                                    if (this.viewportSize.X < this.mainMapLayer.TotalTileWidth)
                                    {
                                        this.mainMapLayer.LeftEdge -= num;
                                        if (this.mainMapLayer.LeftEdge < (this.viewportSize.X - this.mainMapLayer.TotalTileWidth))
                                        {
                                            this.mainMapLayer.LeftEdge = this.viewportSize.X - this.mainMapLayer.TotalTileWidth;
                                        }
                                    }
                                    if (this.viewportSize.Y < this.mainMapLayer.TotalTileHeight)
                                    {
                                        this.mainMapLayer.TopEdge -= num;
                                        if (this.mainMapLayer.TopEdge < (this.viewportSize.Y - this.mainMapLayer.TotalTileHeight))
                                        {
                                            this.mainMapLayer.TopEdge = this.viewportSize.Y - this.mainMapLayer.TotalTileHeight;
                                        }
                                    }
                                    break;
                            }
                            //goto Label_0647;
                            this.ResetScreenEdge();
                            this.mainMapLayer.ReCalculateTileDestination(this);
                            this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, this.mainMapLayer.LeftEdge, this.mainMapLayer.TopEdge, this.mainMapLayer.TotalMapSize);
                            return;
                        }
                        this.lastTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    return;
                    //Label_0647:
                    //this.ResetScreenEdge();
                    //this.mainMapLayer.ReCalculateTileDestination();
                    //this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, this.mainMapLayer.LeftEdge, this.mainMapLayer.TopEdge, this.mainMapLayer.TotalMapSize);
                }
                else
                {
                    if (InputManager.IsMoved)
                    {
                        this.mainMapLayer.LeftEdge += InputManager.PoXMove;
                        this.mainMapLayer.TopEdge += InputManager.PoYMove;

                        if (this.mainMapLayer.LeftEdge > 0)
                        {
                            this.mainMapLayer.LeftEdge = 0;
                        }

                        if (this.mainMapLayer.LeftEdge < (this.viewportSize.X - this.mainMapLayer.TotalTileWidth))
                        {
                            this.mainMapLayer.LeftEdge = this.viewportSize.X - this.mainMapLayer.TotalTileWidth;
                        }

                        if (this.mainMapLayer.TopEdge > 0)
                        {
                            this.mainMapLayer.TopEdge = 0;
                        }

                        if (this.mainMapLayer.TopEdge < (this.viewportSize.Y - this.mainMapLayer.TotalTileHeight))
                        {
                            this.mainMapLayer.TopEdge = this.viewportSize.Y - this.mainMapLayer.TotalTileHeight;
                        }

                        this.ResetScreenEdge();
                        this.mainMapLayer.ReCalculateTileDestination(this);
                        this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, this.mainMapLayer.LeftEdge, this.mainMapLayer.TopEdge, this.mainMapLayer.TotalMapSize);
                    }
                    else
                    {
                        this.lastTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
            }
        }

        private void SetTroopCombatMethod(int id)
        {
            //this.CurrentTroop.Operated = true;
            this.CurrentTroop.CurrentStratagem = null;
            this.CurrentTroop.CurrentCombatMethod = Session.Current.Scenario.GameCommonData.AllCombatMethods.GetCombatMethod(id);
            if (this.CurrentTroop.CurrentCombatMethod != null)
            {
                if (this.CurrentTroop.CurrentCombatMethod.AttackDefault != null)
                {
                    this.CurrentTroop.AttackDefaultKind = (TroopAttackDefaultKind)this.CurrentTroop.CurrentCombatMethod.AttackDefault.ID;
                }
                if (this.CurrentTroop.CurrentCombatMethod.AttackTarget != null)
                {
                    this.CurrentTroop.AttackTargetKind = (TroopAttackTargetKind)this.CurrentTroop.CurrentCombatMethod.AttackTarget.ID;
                }
                if ((this.CurrentTroop.AttackTargetKind == TroopAttackTargetKind.目标默认) || (this.CurrentTroop.AttackTargetKind == TroopAttackTargetKind.目标))
                {
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopTarget));
                }
            }
        }

        private void SetTroopStratagem(int id)
        {
            //this.CurrentTroop.Operated = true;
            this.CurrentTroop.CurrentCombatMethod = null;
            this.CurrentTroop.CurrentStratagem = Session.Current.Scenario.GameCommonData.AllStratagems.GetStratagem(id);
            if (this.CurrentTroop.CurrentStratagem != null)
            {
                if (this.CurrentTroop.CurrentStratagem.CastDefault != null)
                {
                    this.CurrentTroop.CastDefaultKind = (TroopCastDefaultKind)this.CurrentTroop.CurrentStratagem.CastDefault.ID;
                }
                if (this.CurrentTroop.CurrentStratagem.CastTarget != null)
                {
                    this.CurrentTroop.CastTargetKind = (TroopCastTargetKind)this.CurrentTroop.CurrentStratagem.CastTarget.ID;
                }

                if (id == 2 || id == 3 || id == 6 || id == 8)
                {

                }
                else if ((this.CurrentTroop.CastTargetKind == TroopCastTargetKind.特定默认) || (this.CurrentTroop.CastTargetKind == TroopCastTargetKind.特定))
                {
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopTarget));
                }
            }
        }

        private void SetTroopStunt(int id)
        {
            //this.CurrentTroop.Operated = true;
            this.CurrentTroop.CurrentStunt = Session.Current.Scenario.GameCommonData.AllStunts.GetStunt(id);
            this.CurrentTroop.ApplyCurrentStunt();
        }

        public void ShowFactionTechniques(Faction faction, Architecture architecture)
        {
            this.Plugins.FactionTechniquesPlugin.SetArchitecture(architecture);
            this.Plugins.FactionTechniquesPlugin.SetFaction(faction, Session.Current.Scenario.CurrentPlayer == faction);
            this.Plugins.FactionTechniquesPlugin.SetPosition(ShowPosition.Center);
            this.Plugins.FactionTechniquesPlugin.IsShowing = true;
        }

        public void ShowTabListInFrame(UndoneWorkKind undoneWork, FrameKind kind, FrameFunction function, bool OKEnabled, bool CancelEnabled, bool showCheckBox, bool multiselecting, GameObjectList gameObjectList, GameObjectList selectedObjectList, string title, string tabName)
        {
            if ((gameObjectList != null) && (gameObjectList.Count != 0))
            {
                this.Plugins.GameFramePlugin.Kind = kind;
                this.Plugins.GameFramePlugin.Function = function;
                this.Plugins.TabListPlugin.InitialValues(gameObjectList, selectedObjectList, InputManager.NowMouse.ScrollWheelValue, title);
                this.Plugins.TabListPlugin.SetListKindByName(kind.ToString(), showCheckBox, multiselecting);
                this.Plugins.TabListPlugin.SetSelectedTab(tabName);
                this.Plugins.GameFramePlugin.SetFrameContent(this.Plugins.TabListPlugin.TabList, base.viewportSizeFull);
                
                this.Plugins.GameFramePlugin.OKButtonEnabled = OKEnabled;
                this.Plugins.GameFramePlugin.CancelButtonEnabled = CancelEnabled;
                this.Plugins.GameFramePlugin.IsShowing = true;
            }
        }

        public void SetTabListInFrame(UndoneWorkKind undoneWork, FrameKind kind, FrameFunction function, bool OKEnabled, bool CancelEnabled, bool showCheckBox, bool multiselecting, GameObjectList gameObjectList, GameObjectList selectedObjectList, string title, string tabName)
        {
            if ((gameObjectList != null) && (gameObjectList.Count != 0))
            {
                this.Plugins.GameFramePlugin.Kind = kind;
                this.Plugins.GameFramePlugin.Function = function;
                this.Plugins.TabListPlugin.InitialValues(gameObjectList, selectedObjectList, InputManager.NowMouse.ScrollWheelValue, title);
                this.Plugins.TabListPlugin.SetListKindByName(kind.ToString(), showCheckBox, multiselecting);
                this.Plugins.TabListPlugin.SetSelectedTab(tabName);
                this.Plugins.GameFramePlugin.SetFrameContent(this.Plugins.TabListPlugin.TabList, base.viewportSizeFull);

                this.Plugins.GameFramePlugin.OKButtonEnabled = OKEnabled;
                this.Plugins.GameFramePlugin.CancelButtonEnabled = CancelEnabled;
                //this.Plugins.GameFramePlugin.IsShowing = true;
            }
        }

        public void ShowMapViewSelector(bool multiSelecting, GameObjectList gameObjectList, GameDelegates.VoidFunction function, MapViewSelectorKind mapViewSelectorKind)
        {
                        this.Plugins.MapViewSelectorPlugin.SetMultiSelecting(multiSelecting);
                        this.Plugins.MapViewSelectorPlugin.SetGameObjectList(gameObjectList);
                        //if (this.firstTimeMapViewSelector)
                        {
                            //this.firstTimeMapViewSelector = false;
                            this.Plugins.MapViewSelectorPlugin.SetMapPosition(ShowPosition.Center);
                        }
                        this.Plugins.MapViewSelectorPlugin.SetOKFunction(function);
                        this.Plugins.MapViewSelectorPlugin.Kind =mapViewSelectorKind;
                        //this.Plugins.MapViewSelectorPlugin.SetTabList(this.Plugins.TabListPlugin);
                        this.Plugins.MapViewSelectorPlugin.IsShowing = true;
        }
        

        private void gengxinyoucelan()
        {
            if ((Session.Current.Scenario.CurrentPlayer != null) && (Session.Current.Scenario.CurrentPlayer.FirstSection != null))
            {
                this.Showyoucelan(UndoneWorkKind.None,FrameKind.Architecture, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.FirstSection.Architectures, null, "", "");
            }
        }



        public void Showyoucelan(UndoneWorkKind undoneWork, FrameKind kind, FrameFunction function, bool OKEnabled, bool CancelEnabled, bool showCheckBox, bool multiselecting, GameObjectList gameObjectList, GameObjectList selectedObjectList, string title, string tabName)
        {
            if ((gameObjectList != null) && (gameObjectList.Count != 0))
            {
                this.Plugins.youcelanPlugin.Kind = kind;
                this.Plugins.youcelanPlugin.Function = function;
                this.Plugins.youcelanPlugin.InitialValues(gameObjectList, selectedObjectList, InputManager.NowMouse.ScrollWheelValue,title);
                this.Plugins.youcelanPlugin.SetListKindByName(kind.ToString(), showCheckBox, multiselecting);
                this.Plugins.youcelanPlugin.SetSelectedTab(tabName);

                //this.Plugins.GameFramePlugin.SetFrameyoucelanContent(this.Plugins.youcelanPlugin.TabList, base.viewportSize);  //viewportSize  游戏内容窗口的大小
                this.Plugins.youcelanPlugin.SetyoucelanContent(base.viewportSize);  //viewportSize  游戏内容窗口的大小

                //this.Plugins.GameFramePlugin.shiyoucelan = true;
                //this.Plugins.GameFramePlugin.OKButtonEnabled = OKEnabled;
                //this.Plugins.GameFramePlugin.CancelButtonEnabled = CancelEnabled;
                //this.Plugins.GameFramePlugin.IsShowing = true;
                //this.Plugins.youcelanPlugin.IsShowing = true;

            }
        }

        public void ShowBianduiLiebiao(UndoneWorkKind undoneWork, FrameKind kind, FrameFunction function, bool OKEnabled, bool CancelEnabled, bool showCheckBox, bool multiselecting, GameObjectList gameObjectList, GameObjectList selectedObjectList, string title, string tabName,int bingyi)
        {
            //if ((gameObjectList != null) && (gameObjectList.Count != 0))
            {
                this.Plugins.BianduiLiebiao.Kind = kind;
                this.Plugins.BianduiLiebiao.Function = function;
                this.Plugins.BianduiLiebiao.ShezhiBingyi(bingyi);
                this.Plugins.BianduiLiebiao.InitialValues(gameObjectList, selectedObjectList, InputManager.NowMouse.ScrollWheelValue, title);
                this.Plugins.BianduiLiebiao.SetListKindByName(kind.ToString(), showCheckBox, multiselecting);
                this.Plugins.BianduiLiebiao.SetSelectedTab(tabName);

                //this.Plugins.GameFramePlugin.SetFrameyoucelanContent(this.Plugins.youcelanPlugin.TabList, base.viewportSize);  //viewportSize  游戏内容窗口的大小
                this.Plugins.BianduiLiebiao.SetyoucelanContent(base.viewportSize);  //viewportSize  游戏内容窗口的大小
                
                //this.Plugins.GameFramePlugin.shiyoucelan = true;
                //this.Plugins.GameFramePlugin.OKButtonEnabled = OKEnabled;
                //this.Plugins.GameFramePlugin.CancelButtonEnabled = CancelEnabled;
                //this.Plugins.GameFramePlugin.IsShowing = true;
                //this.Plugins.youcelanPlugin.IsShowing = true;


                this.Plugins.BianduiLiebiao.IsShowing = true;
                this.Plugins.youcelanPlugin.IsShowing = false ;

                this.Plugins.ContextMenuPlugin.ShezhiBianduiLiebiaoXinxi(this.Plugins.BianduiLiebiao.IsShowing, this.Plugins.BianduiLiebiao.Weizhi);

            }
        }


        //public void StopMusic()
        //{
        //    if (this.Player.playState == WMPPlayState.wmppsPlaying)
        //    {
        //        this.Player.stop();
        //    }
        //}

        public void ToggleFullScreen()
        {
            Platform.SetGraphicsWidthHeight(Session.MainGame.Window.ClientBounds.Width, Session.MainGame.Window.ClientBounds.Height);

            Session.MainGame.ToggleFullScreen();
            this.RefreshDisableRects();
        }

        public override void TroopAmbush(Troop troop)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.StartAmbush, "Ambush");
            }
        }

        public override void TroopAntiArrowAttack(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.AntiAttack, "AntiArrowAttack");
            }
        }

        public override void TroopAntiAttack(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.AntiAttack, "AntiAttack");
            }
        }

        public override void TroopApplyStunt(Troop troop, Stunt stunt)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (troop.BelongedFaction != null)
                {
                    troop.Leader.TextDestinationString = stunt.Name;
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.UseStunt, "ApplyStunt");
                }
                else
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.UseStunt, "ApplyStuntBasic");
                }
            }
        }

        public override void TroopApplyTroopEvent(TroopEvent te, Troop troop)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye) && (te.Dialogs.Count > 0))
            {
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, this);
                foreach (PersonDialog dialog in te.Dialogs)
                {
                    dialog.SpeakingPerson = Session.Current.Scenario.Persons.GetGameObject(dialog.SpeakingPersonID) as Person;//修复部队事件未识别说话武将
                    if (dialog.SpeakingPerson !=null)
                    {
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(dialog.SpeakingPerson, null, dialog.Text, te.Image, te.Sound,te.TryToShowString);
                    }
                    else
                    {
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(troop.Leader, null, dialog.Text, te.Image, te.Sound,te.TryToShowString);
                    }
                }
                if (Setting.Current.GlobalVariables.DialogShowTime > 0)
                {
                    this.Plugins.tupianwenziPlugin.SetCloseFunction(new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyTroopEvents));
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }
                else
                {
                    Session.Current.Scenario.ApplyTroopEvents();
                }
            }
            else
            {
                Session.Current.Scenario.ApplyTroopEvents();
            }
        }

        public override void ObtainMilitaryKind(Faction f, Person giver, MilitaryKind m)
        {
            if (Session.Current.Scenario.CurrentPlayer == f || Session.GlobalVariables.SkyEye) 
            {
                giver.TextResultString = m.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(giver, null, "SpyMessageNewFacility");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, this);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }
        /*
        public override void AutoAwardGuanzhi(Person p, Person courier, Guanzhi guanzhi)
        {
            if (Session.Current.Scenario.CurrentPlayer == null || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(p.Position) || Session.GlobalVariables.SkyEye)
            {
                if (guanzhi.AutoLearnTextByCourier.Length > 0 && guanzhi.Level >= 6 )
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(courier, null, guanzhi.AutoLearnTextByCourier.Replace("%0", p.Name));
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }
                if (guanzhi.AutoLearnText.Length > 0 && guanzhi.Level >= 6 )
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, null, guanzhi.AutoLearnText.Replace("%0", p.Name));
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }
            }
        }*/

        public override void AutoLearnTitle(Person p, Person courier, Title title)
        {
            if (Session.Current.Scenario.CurrentPlayer == null || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(p.Position) || Session.GlobalVariables.SkyEye)
            {
                if (title.AutoLearnTextByCourier.Length > 0 && title.Level >= 6)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(courier, null, title.AutoLearnTextByCourier.Replace("%0", p.Name));
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }
                if (title.AutoLearnText.Length > 0 && title.Level >= 6)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, null, title.AutoLearnText.Replace("%0", p.Name));
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }
            }
        }

        public override void ApplyEvent(Event e, Architecture a, Screen screen)
        {


            if ((Session.Current.Scenario.CurrentPlayer == null || Session.Current.Scenario.CurrentPlayer.IsArchitectureKnown(a) || Session.GlobalVariables.SkyEye || e.GloballyDisplayed) 
                && (e.matchedDialog != null && e.matchedDialog.Count > 0 && (!e.Minor || e.InvolveLeader)))
            {
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, screen);
                
                foreach (PersonDialog dialog in e.matchedDialog)
                {
                    if (dialog.SpeakingPerson != null)
                    {
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(dialog.SpeakingPerson, null, dialog.Text, e.Image, e.Sound,e.TryToShowString);
                    }
                    else
                    {
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(a.BelongedFaction.Leader, null, dialog.Text, e.Image, e.Sound,e.TryToShowString);
                    }
                }

                if (e.yesEffect.Count > 0 || e.noEffect.Count > 0 || e.yesArchitectureEffect.Count > 0 || e.noArchitectureEffect.Count > 0)
                {
                    if (Session.Current.Scenario.CurrentPlayer != null)
                    {

                        if (!this.Plugins.ConfirmationDialogPlugin.IsShowing)
                        {
                            //this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyEvents(true), new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyEvents(false)));
                            this.Plugins.ConfirmationDialogPlugin.ClearFunctions();
                            this.Plugins.ConfirmationDialogPlugin.AddYesFunction(new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyYesEvents));
                            this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                            this.Plugins.ConfirmationDialogPlugin.AddNoFunction(new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyNoEvents));



                            this.Plugins.ConfirmationDialogPlugin.IsShowing = true;
                        }
                    }
                    else
                    {
                        if (GameObject.Chance(50))
                        {
                            Session.Current.Scenario.ApplyYesEvents();
                        }
                        else
                        {
                            Session.Current.Scenario.ApplyNoEvents();
                        }
                    }
                }

                if (Setting.Current.GlobalVariables.DialogShowTime > 0)
                {
                    this.Plugins.tupianwenziPlugin.SetCloseFunction(new GameDelegates.VoidFunction(Session.Current.Scenario.ApplyEvents));
                    (this.Plugins.tupianwenziPlugin as tupianwenziPlugin.tupianwenziPlugin).tupianwenzi.SetIsShowing(this, true);
                }
                else
                {
                    Session.Current.Scenario.ApplyEvents();
                }
            }
            else
            {
               
                Session.Current.Scenario.ApplyEvents();
            }
        }

        public override void TroopBreakWall(Troop troop, Architecture architecture)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.BreakWall, "BreakWall");
            }
        }

        public override void TroopCastDeepChaos(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.CastDeepChaos, "CastDeepChaos");
            }
        }

        public override void TroopCastStratagem(Troop sending, Troop receiving, Stratagem stratagem)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (sending.BelongedFaction != null)
                {
                    sending.Leader.TextDestinationString = receiving.Leader.Name;
                    sending.Leader.TextResultString = receiving.DisplayName;
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, (TextMessageKind) (((int) TextMessageKind.UseStratagem0) + stratagem.ID), "Stratagem" + stratagem.ID);
                }
                else if (stratagem.Friendly)
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.NoFactionUseStratagemFriendly, "StratagemFriendly");
                }
                else
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.NoFactionUseStratagemHostile, "StratagemHostile");
                }
            }
        }

        public override void TroopChaos(Troop troop, bool deepChaos)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (deepChaos)
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.DeepChaos, "DeepChaos");
                } 
                else 
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.Chaos, "Chaos");
                }
            }
        }

        public override void TroopAttract(Troop troop, Troop caster)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.Attract, "Attract");
            }
        }

        public override void TroopRumour(Troop troop)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.Rumour, "Rumour");
            }
        }

        public override void TroopCombatMethodAttack(Troop sending, Troop receiving, CombatMethod combatMethod)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (sending.BelongedFaction != null)
                {
                    sending.Leader.TextDestinationString = receiving.Leader.Name;
                    sending.Leader.TextResultString = receiving.DisplayName;
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.UseCombatMethod, "CombatMethod" + combatMethod.ID);
                }
                else
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.UseCombatMethod, "CombatMethod");
                }
            }
        }

        public override void TroopCreate(Troop troop)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye) && !Session.Current.Scenario.IsCurrentPlayer(troop.BelongedFaction))
            {
                troop.TextDestinationString = troop.StartingArchitecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(troop, "TroopCreate", troop.Position);
            }
        }

        public override void TroopCriticalStrike(Troop sending, Troop receiving)
        {
            if (sending.CurrentCombatMethod == null && (Session.Current.Scenario.CurrentPlayer == null || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position) || Session.GlobalVariables.SkyEye))
            {
                if (receiving != null)
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.Critical, "CriticalStrike");
                } 
                else 
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.CriticalArchitecture, "CriticalStrikeOnArchitecture");
                }
            }
        }

        public override void TroopDiscoverAmbush(Troop sending, Troop receiving)
        {
            if ((sending != null) && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye))
            {
                this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.DiscoverAmbush, "DiscoverAmbush");
            }
            if ((receiving != null) && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye))
            {
                this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.BeDiscoverAmbush, "BeDiscoveredAmbush");
            }
        }

        public override void TroopEndCutRouteway(Troop troop, bool success)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (success)
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.CutRoutewaySuccess, "EndCutRoutewaySuccess");
                }
                else
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.CutRoutewayFail, "EndCutRoutewayFail");
                }
            }
        }

        public override void TroopEndPath(Troop troop)
        {
        }

        public override void TroopGetNewCaptive(Troop troop, PersonList personlist)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                Person person = personlist[StaticMethods.Random(personlist.Count)] as Person;
                troop.Leader.TextDestinationString = person.Name;
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.TroopNewCaptive, "TroopGetNewCaptive");
                troop.TextDestinationString = person.Name;
                this.Plugins.GameRecordPlugin.AddBranch(troop, "TroopGetNewCaptive", troop.Position);
            }
        }

        public override void TroopGetSpreadBurnt(Troop troop)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.GetSpreadBurnt, "GetSpreadBurnt");
            }
        }

        public override void TroopLevyFieldFood(Troop troop, int food)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, "LevyFieldFood");
            }
        }

        public override void TroopNormalAttack(Troop sending, Troop receiving)
        {
        }

        public override void TroopOccupyArchitecture(Troop troop, Architecture architecture)
        {
            this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureOccupied", troop.Position);

        }

        public override void TroopOutburst(Troop troop, OutburstKind kind)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                switch (kind)
                {
                    case OutburstKind.愤怒:
                        this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.Angry, "TroopOutburstAngry");
                        break;

                    case OutburstKind.沉静:
                        this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.Calm, "TroopOutburstQuiet");
                        break;
                }
            }
        }

        public override void TroopPathNotFound(Troop troop)
        {
        }



        public override void TroopReceiveCriticalStrike(Troop sending, Troop receiving)
        {
            if (!receiving.Destroyed && ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye) && ((receiving.Status != TroopStatus.混乱) && (GameObject.Chance(receiving.Leader.Braveness * 10)))))
            {
                this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.BeCritical, "ReceiveCriticalStrike");
            }
        }

        public override void TroopReceiveWaylay(Troop sending, Troop receiving)
        {
            if (!receiving.Destroyed && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye))
            {
                this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.BeAmbush, "ReceiveWaylay");
            }
        }

        public override void TroopRecoverFromChaos(Troop troop)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.RecoverChaos, "RecoverFromChaos");
            }
        }

        public override void TroopReleaseCaptive(Troop troop, PersonList personlist)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                Person person = personlist[StaticMethods.Random(personlist.Count)] as Person;
                troop.TextDestinationString = person.Name;
                this.Plugins.GameRecordPlugin.AddBranch(troop, "TroopReleaseCaptive", troop.Position);
            }
        }

        public override void TroopResistStratagem(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (isHarmful)
                {
                    if (GameObject.Random(receiving.TroopIntelligence) > GameObject.Random(sending.TroopIntelligence))
                    {
                        this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.ResistHarmfulStratagem, "ResistHarmfulStratagem");
                    }
                }
                else
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.ResistHelpfulStratagem, "ResistNoHarmStratagem");
                }
            }
        }

        public override void TroopRout(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.Rout, "Rout");
            }
        }

        public override void TroopRouted(Troop sending, Troop receiving)
        {
        }

        public override void TroopSetCombatMethod(Troop troop, CombatMethod combatMethod)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                troop.Leader.TextDestinationString = combatMethod.Name;
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.SetCombatMethod, "SetCombatMethod");
            }
        }

        public override void TroopSetStratagem(Troop troop, Stratagem stratagem)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                troop.Leader.TextDestinationString = stratagem.Name;
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.SetStratagem, "SetStratagem");
            }
        }

        public override void TroopStartCutRouteway(Troop troop, int days)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                troop.Leader.TextDestinationString = days.ToString();
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.StartCutRouteway, "StartCutRouteway");
            }
        }

        public override void TroopStopAmbush(Troop troop)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(troop.Leader, troop.Position, TextMessageKind.StopAmbush, "StopAmbush");
            }
        }

        public override void TroopStratagemSuccess(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(receiving.Position)) || Session.GlobalVariables.SkyEye)
            {
                if (isHarmful)
                {
                    if (GameObject.Chance(0x21))
                    {
                        this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.TrappedByStratagem, "HarmfulStratagemSuccess");
                    }
                }
                else if ((sending != receiving) && GameObject.Chance(0x21))
                {
                    this.Plugins.PersonBubblePlugin.AddPerson(receiving.Leader, receiving.Position, TextMessageKind.HelpedByStratagem, "NoHarmStratagemSuccess");
                }
            }
        }

        public override void TroopSurround(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                sending.Leader.TextDestinationString = receiving.Leader.Name;
                sending.Leader.TextResultString = receiving.DisplayName;
                this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.Surround, "Surround");
            }
        }

        public override void TroopWaylay(Troop sending, Troop receiving)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sending.Position)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(sending.Leader, sending.Position, TextMessageKind.Ambush, "Waylay");
            }
        }

        public void TryToDemolishRouteway()
        {
            if (!this.Plugins.tupianwenziPlugin.IsShowing)
            {
                this.Plugins.ConfirmationDialogPlugin.SetSimpleTextDialog(this.Plugins.SimpleTextDialogPlugin);
                this.Plugins.ConfirmationDialogPlugin.ClearFunctions();
                this.Plugins.ConfirmationDialogPlugin.AddYesFunction(new GameDelegates.VoidFunction(this.DemolishCurrentRouteway));
                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                this.Plugins.SimpleTextDialogPlugin.SetBranch("DemolishRouteway");
                this.Plugins.ConfirmationDialogPlugin.IsShowing = true;
            }
        }

        private void saveBeforeExit()
        {
            this.mainMapLayer.StopThreads();
            if (Session.GlobalVariables.HardcoreMode)
            {
                this.SaveGameAutoPosition();
            }
            
            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.Android)
            {
                Platform.Current.Exit();
            }
            else
            {
                ReturnMainMenu();
            }            
        }

        public override void TryToExit()
        {
            if (!this.Plugins.tupianwenziPlugin.IsShowing)
            {
                this.Plugins.ConfirmationDialogPlugin.SetSimpleTextDialog(this.Plugins.SimpleTextDialogPlugin);
                this.Plugins.ConfirmationDialogPlugin.ClearFunctions();
                this.Plugins.ConfirmationDialogPlugin.AddYesFunction(new GameDelegates.VoidFunction(this.saveBeforeExit));
                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                this.Plugins.SimpleTextDialogPlugin.SetBranch(Session.GlobalVariables.HardcoreMode ? "ExitSaveGame" : (Session.Current.Scenario.JustSaved ? "ExitGameNoReminder" : "ExitGame"));
                this.Plugins.ConfirmationDialogPlugin.IsShowing = true;
            }
        }

        
        private volatile bool roundDone = false;
        private object roundDoneLock = new object();

        public override void GameGo(GameTime gameTime)
        {
            if ((this.viewMove == ViewMove.Stop) && !this.AfterDayPassed(gameTime))
            {
                this.Plugins.DateRunnerPlugin.DateGo();
                if (!this.AfterDayStarting(gameTime))
                {
                    if (Session.GlobalVariables.EnableResposiveThreading)
                    {
                        roundDone = true;
                    }
                    else
                    {
                        this.Plugins.DateRunnerPlugin.DateStop();
                    }
                }
            }
        }

        private void RunAI()
        {
            do
            {
                this.GameGo(new GameTime());
            } while (true);
        }

        private PlatformTask aiThread;

        private bool loaded = false;

        private bool toggleScreen = false;

        private float toggleScreenTime = 0f;

        public override void Update(GameTime gameTime)   //视野内容更新
        {
            if (toggleScreen)
            {
                toggleScreenTime += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
                if (toggleScreenTime >= 1f)
                {
                    toggleScreen = false;
                    int width = Session.MainGame.Window.ClientBounds.Width;
                    int height = Session.MainGame.Window.ClientBounds.Height;
                    Session.RealResolution = Session.Resolution = width + "*" + height;
                    Platform.SetGraphicsWidthHeight(width, height);
                    //Session.MainGame.ToggleFullScreen();
                    Platform.GraphicsApplyChanges();
                    this.UpdateViewport();
                    this.ResetTiles();
                    this.RefreshDisableRects();
                    JumpToFaction();
                }
            }

            if (!loaded)
            {
                loaded = true;
                //全屏的判断放到初始化代码中
                if (Session.GlobalVariables.FullScreen)
                {
                    Platform.Current.SetFullScreen2(true);
                    toggleScreen = true;
                    //Platform.Current.ProcessViewChanged();
                    //this.RefreshDisableRects();
                    //
                }                
            }

            if (cloudLayer.IsVisible)
            {
                if (cloudLayer.IsStart)
                {

                }
                else
                {
                    if (this.mainMapLayer.DisplayingMapTiles.Exists(ma => ma == null || ma.TileTexture == null))
                    {

                    }
                    else
                    {
                        cloudLayer.IsStart = true;
                    }
                }
                cloudLayer.Update(Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (dantiaoLayer == null)
            {

            }
            else
            {
                dantiaoLayer.Update(Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds));

                return;
            }

            if (this.Plugins.ToolBarPlugin != null)
            {
                var btBack = ((ToolBarPlugin.ToolBarPlugin)this.Plugins.ToolBarPlugin).backTool;
                btBack.Update();
                if (btBack.MouseOver)
                {
                    if (InputManager.IsPressed)
                    {
                        InputManager.SleepTime = 1f;
                    }
                }
            }
            if (base.EnableUpdate)
            {
                /*try
                {*/

                this.UpdateCount++;
                base.Update(gameTime);
                this.CalculateFrameRate(gameTime);
                this.Plugins.PersonBubblePlugin.Update(gameTime);

                switch (base.UndoneWorks.Peek().Kind)
                {
                    case UndoneWorkKind.None:

                        this.UpdateToolBar(gameTime);
                        this.UpdateScreenBlind(gameTime);
                        //this.Plugins.youcelanPlugin.Update(gameTime);
                        //this.Plugins.youcelanPlugin.IsShowing = false;
                        this.UpdateViewMove();
                        this.HandleLaterMouseEvent(gameTime);
                        this.ScrollTheMainMap(gameTime);
                        this.HandleKey(gameTime);

                        if (Session.GlobalVariables.EnableResposiveThreading)
                        {
                            if (aiThread == null || !aiThread.IsAlive)
                            {
                                aiThread = null;
                                aiThread = new PlatformTask(() => RunAI()); 
                                    //new PlatformTask(new ThreadStart(RunAI));
                                aiThread.Start();
                            }

                            lock (roundDoneLock)
                            {
                                if (roundDone)
                                {
                                    roundDone = false;
                                    this.Plugins.DateRunnerPlugin.DateStop();
                                }
                            }
                        }
                        else
                        {
                            this.GameGo(gameTime);
                        }

                        if (Session.Current.Scenario.PlayerFactions.Count == 0)
                        {
                            this.DateGo(1);
                        }

                        //if (!this.Plugins.youcelanPlugin.IsShowing)
                        //{
                        //    this.Showyoucelan(UndoneWorkKind.None, FrameKind.Architecture, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Architectures, null, "", "");
                        //}
                        break;

                    case UndoneWorkKind.Dialog:
                        this.UpdateDialog(gameTime);

                        break;
                    case UndoneWorkKind.tupianwenzi:
                        this.Updatetupianwenzi(gameTime);

                        break;

                    case UndoneWorkKind.liangdaobianji:

                        this.HandleLaterMouseEvent(gameTime);
                        this.ScrollTheMainMap(gameTime);

                        break;
                    case UndoneWorkKind.Selecting:
                        if (base.EnableSelecting)
                        {
                            this.ResetCurrentStatus();
                            this.UpdateViewMove();
                            this.HandleLaterMouseScroll();
                            this.ScrollTheMainMap(gameTime);
                            this.UpdateConmentText(gameTime);
                        }
                        break;

                    case UndoneWorkKind.Inputer:
                        this.UpdateInputer(gameTime);
                        break;

                    case UndoneWorkKind.Selector:
                        this.HandleLaterMouseEvent(gameTime);
                        this.ScrollTheMainMap(gameTime);
                        break;

                    case UndoneWorkKind.MapViewSelector:
                        this.ResetCurrentStatus();
                        this.UpdateViewMove();
                        this.HandleLaterMouseScroll();
                        this.ScrollTheMainMap(gameTime);
                        if (base.EnableLaterMouseEvent)
                        {
                            this.UpdateSurvey(gameTime);
                            this.UpdateConmentText(gameTime);
                        }
                        break;
                }

                var optionDialog = Session.MainGame.mainGameScreen.Plugins.OptionDialogPlugin as OptionDialogPlugin.OptionDialogPlugin;
                if(optionDialog.IsShowing)
                {
                    optionDialog.Update(gameTime);
                }
                /*}
                catch (OutOfMemoryException)
                {
                    this.mainMapLayer.freeTilesMemory();
                }
                catch (InvalidOperationException)
                {
                    this.mainMapLayer.freeTilesMemory();
                }*/

            }
        }

        private void UpdateConmentText(GameTime gameTime)
        {
            if ((this.Plugins.ConmentTextPlugin != null) && (this.lastPosition != this.position))
            {
                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.position);
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(this.position);
                this.Plugins.ConmentTextPlugin.BuildThirdText(Session.Current.Scenario.GetCoordinateString(this.position), true);

                if (Session.Current.Scenario.CurrentPlayer != null)
                {
                    this.Plugins.ConmentTextPlugin.BuildSecondText(InformationTile.InformationString(Session.Current.Scenario.CurrentPlayer.GetKnownAreaData(this.position)), true);
                }
                else
                {
                    this.Plugins.ConmentTextPlugin.BuildSecondText("", false);
                }
                if ((troopByPosition != null && troopByPosition.Status != TroopStatus.埋伏) && (Session.GlobalVariables.SkyEye || ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position))))
                {
                    this.Plugins.ConmentTextPlugin.BuildFirstText(troopByPosition.DisplayName + " " + this.mainMapLayer.GetTerrainNameByPosition(this.position), true);
                }
                else if (architectureByPosition != null)
                {
                    this.Plugins.ConmentTextPlugin.BuildFirstText(architectureByPosition.Name + " " + this.mainMapLayer.GetTerrainNameByPosition(this.position), true);
                }
                else
                {
                    this.Plugins.ConmentTextPlugin.BuildFirstText(this.mainMapLayer.GetTerrainNameByPosition(this.position), false);
                }
                this.Plugins.ConmentTextPlugin.SetView(this.viewportSize.X, this.viewportSize.Y - this.Plugins.ToolBarPlugin.Height);
                this.Plugins.ConmentTextPlugin.Update(gameTime);
            }
        }

        private void UpdateDialog(GameTime gameTime)
        {

            if (this.Plugins.SimpleTextDialogPlugin != null)
            {
                this.Plugins.SimpleTextDialogPlugin.Update(gameTime);
            }
            if (this.Plugins.tupianwenziPlugin != null)
            {
                this.Plugins.tupianwenziPlugin.Update(gameTime);
            }


 
        }
        private void Updatetupianwenzi(GameTime gameTime)
        {


            if (this.Plugins.tupianwenziPlugin != null)
            {
                this.Plugins.tupianwenziPlugin.Update(gameTime);
            }


        }
        private void UpdateInputer(GameTime gameTime)
        {
            this.Plugins.NumberInputerPlugin.Update(gameTime);
        }

        private void UpdateScreenBlind(GameTime gameTime)
        {
            if (this.Plugins.ScreenBlindPlugin != null)
            {
                this.Plugins.ScreenBlindPlugin.Update(gameTime);
            }
        }

        private void ShowArchitectureSurveyPlugin(Architecture architectureByPosition) //,GameTime gameTime)       //显示情况表到右侧
        {
            if (this.Plugins.ArchitectureSurveyPlugin != null)
            {
                //Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.position);
                if (architectureByPosition != null)
                {
                    this.Plugins.ArchitectureSurveyPlugin.SetArchitecture(architectureByPosition, this.position);
                    this.Plugins.ArchitectureSurveyPlugin.SetFaction(Session.Current.Scenario.CurrentPlayer);
                    this.Plugins.ArchitectureSurveyPlugin.Showing = true;

                    if (Session.LargeContextMenu)
                    {
                        //if (InputManager.PoX < 670 && InputManager.PoY < 300)  // if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        if (InputManager.PoX < 400)  // && InputManager.PoY < 250)  // if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        {
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(this.viewportSize.X - 100, 20);
                        }
                        else
                        {
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(100, 20);
                            //this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(280, 20);
                        }
                    }
                    else
                    {
                        if (InputManager.PoX < 670 && InputManager.PoY < 300)  // if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        //if (InputManager.PoX < 400)  // && InputManager.PoY < 250)  // if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        {
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(this.viewportSize.X - 100, 20);
                        }
                        else
                        {
                            //this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(100, 20);
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(280, 20);
                        }
                    }

                    this.Plugins.ArchitectureSurveyPlugin.Gengxin();
                }
                else
                {
                    this.Plugins.ArchitectureSurveyPlugin.SetArchitecture(null, this.position);
                    this.Plugins.ArchitectureSurveyPlugin.Showing = false;
                }
            }
        }

        private void UpdateSurvey(GameTime gameTime)       //更新情况表
        {
            if (this.Plugins.ArchitectureSurveyPlugin != null)
            {
                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.position);
                if (this.Plugins.youcelanPlugin.IsShowing && StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.youcelanPlugin.FrameRectangle))
                {
                    architectureByPosition = null;
                }
                if ((architectureByPosition != null) && ((this.CurrentTroop == null) || ((!Session.GlobalVariables.SkyEye && (Session.Current.Scenario.CurrentPlayer != null)) && !Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.CurrentTroop.Position))))
                {
                    this.Plugins.ArchitectureSurveyPlugin.SetArchitecture(architectureByPosition, this.position);
                    this.Plugins.ArchitectureSurveyPlugin.SetFaction(Session.Current.Scenario.CurrentPlayer);
                    this.Plugins.ArchitectureSurveyPlugin.Showing = true;

                    //this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(280, 20);

                    if (Session.LargeContextMenu)
                    {
                        if (InputManager.PoX < 400)  // && InputManager.PoY < 250)  // if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        {
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(this.viewportSize.X - 100, 20);
                        }
                        else
                        {
                            this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(100, 20);
                            //this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(280, 20);
                        }
                    }
                    else
                    {
                        //if (InputManager.NowMouse.X < 670 && InputManager.NowMouse.Y < 300)
                        //{
                        //}
                        //else
                        //{
                        //}

                        this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(InputManager.PoX, InputManager.PoY);
                    }

                    //this.Plugins.ArchitectureSurveyPlugin.SetTopLeftPoint(InputManager.PoX, InputManager.PoY);  // InputManager.NowMouse.X, InputManager.NowMouse.Y);

                    this.Plugins.ArchitectureSurveyPlugin.Update(gameTime);
                }
                else
                {
                    this.Plugins.ArchitectureSurveyPlugin.SetArchitecture(null, this.position);
                    this.Plugins.ArchitectureSurveyPlugin.Showing = false;
                }
            }
            if (this.Plugins.TroopSurveyPlugin != null)
            {
                if (Session.GlobalVariables.SkyEye || ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(this.position);
                    if (this.Plugins.youcelanPlugin.IsShowing && StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.youcelanPlugin.FrameRectangle))
                    {
                        troopByPosition = null;
                    }
                    if (troopByPosition != null && troopByPosition.Status != TroopStatus.埋伏)
                    {
                        this.Plugins.TroopSurveyPlugin.SetTroop(troopByPosition);
                        this.Plugins.TroopSurveyPlugin.SetFaction(Session.Current.Scenario.CurrentPlayer);
                        this.Plugins.TroopSurveyPlugin.Showing = true;
                        this.Plugins.TroopSurveyPlugin.SetTopLeftPoint(InputManager.PoX, InputManager.PoY);  // InputManager.NowMouse.X, InputManager.NowMouse.Y);
                        this.Plugins.TroopSurveyPlugin.Update(gameTime);
                    }
                    else
                    {
                        this.Plugins.TroopSurveyPlugin.SetTroop(null);
                        this.Plugins.TroopSurveyPlugin.Showing = false;
                    }
                }
                else
                {
                    this.Plugins.TroopSurveyPlugin.SetTroop(null);
                    this.Plugins.TroopSurveyPlugin.Showing = false;
                }
            }
        }

        private void UpdateToolBar(GameTime gameTime)
        {
            if (this.Plugins.ToolBarPlugin != null)
            {
                this.Plugins.ToolBarPlugin.Update(gameTime);
            }
        }

        private void UpdateViewMove()          //更新视野移动方向
        {
            this.ResetMouse();

            if (this.Plugins.AirViewPlugin.IsMapShowing)
            {
                if (StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.AirViewPlugin.MapPosition))
                {
                    return;
                }
            }

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
            {
                //if (((Platform.MainGame.IsActive && base.EnableScroll) && (!this.DrawingSelector && (base.viewportSize != Point.Zero))) && ((((InputManager.NowMouse.X >= 0) && (InputManager.NowMouse.Y >= 0)) && (InputManager.NowMouse.X <= this.viewportSize.X)) && (InputManager.NowMouse.Y <= this.viewportSize.Y)))
                if (((Platform.MainGame.IsActive && base.EnableScroll) && (!this.DrawingSelector && (base.viewportSize != Point.Zero))) && ((((InputManager.PoX >= 0) && (InputManager.PoY >= 0)) && (InputManager.PoX <= this.viewportSize.X)) && (InputManager.PoY <= this.viewportSize.Y)))
                {
                    if (InputManager.PoX < 50)
                    {
                        if (InputManager.PoY < 50)
                        {
                            if ((this.mainMapLayer.LeftEdge != 0) || (this.mainMapLayer.TopEdge != 0))
                            {
                                base.MouseArrowTexture = this.Textures.MouseArrowTextures[5];
                                this.viewMove = ViewMove.TopLeft;
                            }
                        }
                        else if ((this.viewportSize.Y - InputManager.PoY) < 50)
                        {
                            if ((this.mainMapLayer.LeftEdge != 0) || ((this.mainMapLayer.TopEdge + this.mainMapLayer.TotalTileHeight) != this.viewportSize.Y))
                            {
                                base.MouseArrowTexture = this.Textures.MouseArrowTextures[7];
                                this.viewMove = ViewMove.BottomLeft;
                            }
                        }
                        else if (this.mainMapLayer.LeftEdge != 0)
                        {
                            base.MouseArrowTexture = this.Textures.MouseArrowTextures[1];
                            this.viewMove = ViewMove.Left;
                        }
                    }
                    else if ((this.viewportSize.X - InputManager.PoX) < 50)
                    {
                        if (InputManager.PoY < 50)
                        {
                            if (((this.mainMapLayer.LeftEdge + this.mainMapLayer.TotalTileWidth) != this.viewportSize.X) || (this.mainMapLayer.TopEdge != 0))
                            {
                                base.MouseArrowTexture = this.Textures.MouseArrowTextures[6];
                                this.viewMove = ViewMove.TopRight;
                            }
                        }
                        else if ((this.viewportSize.Y - InputManager.PoY) < 50)
                        {
                            if (((this.mainMapLayer.LeftEdge + this.mainMapLayer.TotalTileWidth) != this.viewportSize.X) || ((this.mainMapLayer.TopEdge + this.mainMapLayer.TotalTileHeight) != this.viewportSize.Y))
                            {
                                base.MouseArrowTexture = this.Textures.MouseArrowTextures[8];
                                this.viewMove = ViewMove.BottomRight;
                            }
                        }
                        else if ((this.mainMapLayer.LeftEdge + this.mainMapLayer.TotalTileWidth) != this.viewportSize.X)
                        {
                            base.MouseArrowTexture = this.Textures.MouseArrowTextures[2];
                            this.viewMove = ViewMove.Right;
                        }
                    }
                    else if (InputManager.PoY < 50)
                    {
                        if (this.mainMapLayer.TopEdge != 0)
                        {
                            base.MouseArrowTexture = this.Textures.MouseArrowTextures[3];
                            this.viewMove = ViewMove.Top;
                        }
                    }
                    else if (((this.viewportSize.Y - InputManager.PoY) < 50) && ((this.mainMapLayer.TopEdge + this.mainMapLayer.TotalTileHeight) != this.viewportSize.Y))
                    {
                        base.MouseArrowTexture = this.Textures.MouseArrowTextures[4];
                        this.viewMove = ViewMove.Bottom;
                    }

                    if (this.currentKey == Keys.A)
                    {
                        if (this.mainMapLayer.LeftEdge != 0)
                        {
                            this.viewMove = ViewMove.Left;
                            this.isKeyScrolling = true;
                        }
                    }
                    else if (this.currentKey == Keys.D)
                    {
                        if ((this.mainMapLayer.LeftEdge + this.mainMapLayer.TotalTileWidth) != this.viewportSize.X)
                        {
                            this.viewMove = ViewMove.Right;
                            this.isKeyScrolling = true;
                        }
                    }
                    else if (this.currentKey == Keys.W)
                    {
                        if (this.mainMapLayer.TopEdge != 0)
                        {
                            this.viewMove = ViewMove.Top;
                            this.isKeyScrolling = true;
                        }
                    }
                    else if ((this.currentKey == Keys.S && ((this.mainMapLayer.TopEdge + this.mainMapLayer.TotalTileHeight) != this.viewportSize.Y)))
                    {
                        this.viewMove = ViewMove.Bottom;
                        this.isKeyScrolling = true;
                    }
                    else
                    {
                        this.isKeyScrolling = false;
                    }
                }
            }
        }

        private void UpdateViewport()
        {
            if (Platform.GraphicsDevice != null)
            {
                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
                {
                    this.viewportSize.X = Platform.GraphicsDevice.Viewport.Width;
                    this.viewportSize.Y = Platform.GraphicsDevice.Viewport.Height - this.Plugins.ToolBarPlugin.Height;
                }
                else
                {
                    this.viewportSize.X = Session.ResolutionX - 20;  // Platform.GraphicsDevice.Viewport.Width;
                    this.viewportSize.Y = Convert.ToInt32(Session.ResolutionY - this.Plugins.ToolBarPlugin.Height - 10);  // Platform.GraphicsDevice.Viewport.Height - this.Plugins.ToolBarPlugin.Height;
                }

                this.viewportSizeFull.X = Platform.GraphicsDevice.Viewport.Width;
                this.viewportSizeFull.Y = Platform.GraphicsDevice.Viewport.Height;

                this.Plugins.ToolBarPlugin.SetRealViewportSize(new Point(this.viewportSize.X, this.viewportSize.Y));

                //this.Plugins.ToolBarPlugin.SetRealViewportSize(new Point(this.viewportSize.X, this.viewportSize.Y));

                this.ResetScreenEdge();

                this.mainMapLayer.ReCalculateTileDestination(this);

                Session.ChangeStartDisplay(Platform.GraphicsDevice.Viewport.Width, Platform.GraphicsDevice.Viewport.Height);
            }
        }

        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            this.UpdateViewport();
            this.ResetTiles();
            this.RefreshDisableRects();
        }

        public Architecture CurrentArchitecture
        {
            get
            {
                return this.screenManager.CurrentArchitecture;
            }
            set
            {
                this.screenManager.CurrentArchitecture = value;
            }
        }

        public string CurrentArchitectureDisplayName
        {
            get
            {
                return this.CurrentArchitecture.Name;
            }
        }

        public Faction CurrentFaction
        {
            get
            {
                return this.screenManager.CurrentFaction;
            }
            set
            {
                this.screenManager.CurrentFaction = value;
            }
        }

        public GameObjectList CurrentGameObjects
        {
            get
            {
                return this.screenManager.CurrentGameObjects;
            }
            set
            {
                this.screenManager.CurrentGameObjects = value;
            }
        }

        public Military CurrentMilitary
        {
            get
            {
                return this.screenManager.CurrentMilitary;
            }
            set
            {
                this.screenManager.CurrentMilitary = value;
            }
        }

        public int CurrentNumber
        {
            get
            {
                return this.screenManager.CurrentNumber;
            }
            set
            {
                this.screenManager.CurrentNumber = value;
            }
        }

        public int Currentzijin
        {
            get
            {
                return this.screenManager.Currentzijin;
            }
            set
            {
                this.screenManager.Currentzijin = value;
            }
        }

        public Person CurrentPerson
        {
            get
            {
                return this.screenManager.CurrentPerson;
            }
            set
            {
                this.screenManager.CurrentPerson = value;
            }
        }

        public GameObjectList CurrentPersons
        {
            get
            {
                return this.screenManager.CurrentPersons;
            }
        }

        public Routeway CurrentRouteway
        {
            get
            {
                return this.screenManager.CurrentRouteway;
            }
            set
            {
                this.screenManager.CurrentRouteway = value;
            }
        }

        public string CurrentRoutewayDisplayName
        {
            get
            {
                return this.CurrentRouteway.DisplayName;
            }
        }

        public Troop CurrentTroop
        {
            get
            {
                return this.screenManager.CurrentTroop;
            }
            set
            {
                this.screenManager.CurrentTroop = value;
            }
        }

        public string CurrentTroopDisplayName
        {
            get
            {
                return this.CurrentTroop.DisplayName;
            }
        }

        public override bool DrawingSelector
        {
            get
            {
                return this.drawingSelector;
            }
            set
            {
                if (this.drawingSelector != value)
                {
                    this.drawingSelector = value;
                    this.SelectorTroops.Clear();
                    if (!value)
                    {
                        this.PopUndoneWork();
                        if (((this.SelectorStartPosition != base.MousePosition) && (Session.Current.Scenario.CurrentPlayer != null)) && Session.Current.Scenario.CurrentPlayer.Controlling)
                        {
                            Point positionByPoint = this.GetPositionByPoint(this.SelectorStartPosition);
                            Point point2 = this.GetPositionByPoint(base.MousePosition);
                            Rectangle r = new Rectangle(
                                Math.Min(point2.X, positionByPoint.X), Math.Min(point2.Y, positionByPoint.Y),
                                Math.Abs(point2.X - positionByPoint.X), Math.Abs(point2.Y - positionByPoint.Y));
                            foreach (Troop troop in Session.Current.Scenario.CurrentPlayer.Troops.GetList())
                            {
                                if (!troop.Destroyed && troop.Status == TroopStatus.一般 && 
                                    r.Contains(troop.Position) &&
                                    !troop.Operated)
                                {
                                    this.SelectorTroops.Add(troop);
                                }
                            }
                            this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.SelectorTroopsDestination));
                        }
                    }
                    else
                    {
                        this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selector, UndoneWorkSubKind.None ));
                        //this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selector, ViewMove.Stop ));
                    }
                    this.SelectorStartPosition = base.MousePosition;
                }
            }
        }

        public bool IsFullScreen
        {
            get
            {
                return Platform.GraphicsDevice.PresentationParameters.IsFullScreen;  // base.GraphicsDevice.PresentationParameters.IsFullScreen;
            }
        }

        public GameObjectList CurrentMilitaries
        {
            get
            {
                return this.screenManager.CurrentMilitaries;
            }
            set
            {
                this.screenManager.CurrentMilitaries = value;
            }
        }

        public bool IsMultipleResource
        {
            get
            {
                return Session.GlobalVariables.MultipleResource;
            }
        }

        public bool IsPlayingBattleSound
        {
            get
            {
                return Setting.Current.GlobalVariables.PlayBattleSound;
            }
        }

        public bool IsPlayingTroopVoice
        {
            get
            {
                return Setting.Current.GlobalVariables.TroopVoice;
            }
        }

        public bool IsPlayingMusic
        {
            get
            {
                return Session.GlobalVariables.PlayMusic;
            }
        }

        public bool IsPlayingNormalSound
        {
            get
            {
                return Setting.Current.GlobalVariables.PlayNormalSound;
            }
        }

        public bool IsPlayingTroopAnimation
        {
            get
            {
                return Setting.Current.GlobalVariables.DrawTroopAnimation;
            }
        }

        public bool IsShowingSmog
        {
            get
            {
                return Setting.Current.GlobalVariables.DrawMapVeil;
            }
        }

        public bool IsStopOnAttack
        {
            get
            {
                return Setting.Current.GlobalVariables.StopToControlOnAttack;
            }
        }

        public bool IsShowingTroopTitle
        {
            get
            {
                return this.Plugins.TroopTitlePlugin.IsShowing;
            }
        }

        public bool IsSkyEye
        {
            get
            {
                return Session.GlobalVariables.SkyEye;
            }
        }

        public ViewMove ViewMoveDirection
        {
            get
            {
                return this.viewMove;
            }
        }

        public override void ReduceSound()
        {
            Setting.Current.MusicVolume -= 10;

            if (Setting.Current.MusicVolume < 0)
            {
                Setting.Current.MusicVolume = 0;
            }

            Setting.Save();

            Platform.Current.SetMusicVolume((int)Setting.Current.MusicVolume);

            //this.Player.settings.volume -= 10;
        }

        public override void IncreaseSound()
        {
            Setting.Current.MusicVolume += 10;

            if (Setting.Current.MusicVolume > 100)
            {
                Setting.Current.MusicVolume = 100;
            }

            Setting.Save();

            Platform.Current.SetMusicVolume((int)Setting.Current.MusicVolume);

            //Platform.Current.PlayEffect(@"Sound\Move");

            //this.Player.settings.volume += 10;
        }

        public override void ReturnMainMenu()
        {
            this.Plugins.ConfirmationDialogPlugin.SetSimpleTextDialog(this.Plugins.SimpleTextDialogPlugin);
            this.Plugins.ConfirmationDialogPlugin.ClearFunctions();
            this.Plugins.ConfirmationDialogPlugin.AddYesFunction(new GameDelegates.VoidFunction(this.ReturnToMainMenu));
            this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
            this.Plugins.SimpleTextDialogPlugin.SetBranch("退回初始");
            this.Plugins.ConfirmationDialogPlugin.IsShowing = true;
        }

        public void ReturnToMainMenu()
        {
            Session.MainGame.loadingScreen = new LoadingScreen("End", "");
            Session.MainGame.loadingScreen.LoadScreenEvent += (sender0, e0) =>
            {
                Platform.Sleep(1000);
            };

            //System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo myProcessStartInfo = new System.Diagnostics.ProcessStartInfo(@"WorldOfTheThreeKingdoms.exe", "");
            //myProcess.StartInfo = myProcessStartInfo;
            //myProcess.Start();
            //System.Environment.Exit(0);
        }

        public bool SkyEyeSimpleNotification
        {
            get
            {
                return Session.GlobalVariables.SkyEyeSimpleNotification;
            }
        }
        public void updateGameScreenByCurrentTarget()
        {            
            if ((((this.CurrentArchitecture != null) && (this.CurrentTroop != null)) && (this.CurrentTroop.BelongedFaction == Session.Current.Scenario.CurrentPlayer)) && (this.CurrentArchitecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
            {
                if (!(this.Plugins.ContextMenuPlugin.IsShowing || !Session.Current.Scenario.CurrentPlayer.Controlling))
                {
                    this.Plugins.ContextMenuPlugin.IsShowing = true;
                    this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                    this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureTroopLeftClick");
                    this.Plugins.ContextMenuPlugin.Prepare(this.SelectorStartPosition.X, this.SelectorStartPosition.Y, base.viewportSize);
                    this.bianduiLiebiaoBiaoji = "ArchitectureTroopLeftClick";
                }
            }
            else if ((this.CurrentTroop != null) && (this.CurrentTroop.BelongedFaction == Session.Current.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
            {
                if (!this.Plugins.ContextMenuPlugin.IsShowing && Session.Current.Scenario.IsPlayerControlling())
                {
                    this.Plugins.ContextMenuPlugin.IsShowing = true;
                    this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                    this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopLeftClick");
                    this.Plugins.ContextMenuPlugin.Prepare(this.SelectorStartPosition.X, this.SelectorStartPosition.Y, base.viewportSize);
                    this.bianduiLiebiaoBiaoji = "TroopLeftClick";
                    if (!this.Plugins.ContextMenuPlugin.IsShowing && (this.CurrentTroop.CutRoutewayDays > 0))
                    {
                        this.CurrentTroop.Leader.TextDestinationString = this.CurrentTroop.CutRoutewayDays.ToString();
                        this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(this.CurrentTroop.StopCutRouteway), null);
                        this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(this.CurrentTroop.Leader, this.CurrentTroop.Leader, TextMessageKind.StopCutRouteway, "StopCutRouteway");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                    }
                }
            }
            else if (((this.CurrentArchitecture != null) && (this.CurrentArchitecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)) && !(this.Plugins.ContextMenuPlugin.IsShowing || !Session.Current.Scenario.IsPlayerControlling()))
            {
                this.Plugins.ContextMenuPlugin.IsShowing = true;
                this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureLeftClick");
                this.Plugins.ContextMenuPlugin.Prepare(this.SelectorStartPosition.X, this.SelectorStartPosition.Y, base.viewportSize);

                this.bianduiLiebiaoBiaoji = "ArchitectureLeftClick";
                this.ShowBianduiLiebiao(UndoneWorkKind.None, FrameKind.Military, FrameFunction.Browse, false, true, false, true,
                    this.CurrentArchitecture.Militaries, this.CurrentArchitecture.ZhengzaiBuchongDeBiandui(), "", "", this.CurrentArchitecture.MilitaryPopulation);
                this.ShowArchitectureSurveyPlugin(this.CurrentArchitecture);
            }
        }
    }
}
