using GameGlobal;
using GameManager;
using GameObjects.FactionDetail;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace GameObjects
{

    public class Screen  // : DrawableGameComponent
    {
        //private SpriteBatch batch;
        public Point BottomRightPosition;
        private Texture2D defaultMouseArrowTexture;
        public bool EnableLaterMouseEvent;
        public bool EnableMouseEvent;
        public bool EnableScroll;
        public bool EnableSelecting;
        public bool EnableUpdate;
        public List<int> InitializationFactionIDs;
        public string InitializationFileName;
        public bool IsHolding;
        protected KeyboardState keyState;
        public List<Rectangle> LaterMouseEventDisableRects;
        public bool LoadScenarioInInitialization;
        protected Texture2D MouseArrowTexture;
        protected Microsoft.Xna.Framework.Input.MouseState mouseState;
        public GameObjectList PluginList;
        protected Microsoft.Xna.Framework.Input.MouseState previousMouseState;

        public GameScenario Scenario
        {
            get
            {
                return Session.Current.Scenario;
            }
        }

        public List<Rectangle> ScrollDisableRects;
        public List<Rectangle> SelectingDisableRects;
        public Point TopLeftPosition;
        public Stack<UndoneWorkItem> UndoneWorks;
        public Point viewportSize, viewportSizeFull;

        public event MouseLeftDown OnMouseLeftDown;

        public event MouseLeftUp OnMouseLeftUp;

        public event MouseMove OnMouseMove;

        public event MouseRightDown OnMouseRightDown;

        public event MouseRightUp OnMouseRightUp;

        public event MouseScroll OnMouseScroll;

        public Screen(Game game) // : base(game)
        {
            this.PluginList = new GameObjectList();
            this.EnableUpdate = true;
            this.EnableMouseEvent = true;
            this.EnableScroll = true;
            this.EnableLaterMouseEvent = true;
            this.EnableSelecting = true;
            this.ScrollDisableRects = new List<Rectangle>();
            this.LaterMouseEventDisableRects = new List<Rectangle>();
            this.SelectingDisableRects = new List<Rectangle>();
            this.UndoneWorks = new Stack<UndoneWorkItem>();
            this.UndoneWorks.Push(new UndoneWorkItem(UndoneWorkKind.None, UndoneWorkSubKind.None));
        }

        public virtual void FullScreen() { }


        public virtual void DisposeMapTileMemory()
        {
        }

        public virtual void Activate()
        {
            this.EnableUpdate = true;
            this.RemoveDisableRectangle(this.LaterMouseEventDisableRects, new Rectangle(0, 0, this.viewportSize.X, this.viewportSize.Y));
        }

        public void AddDisableRectangle(List<Rectangle> disablerects, Rectangle rect)
        {
            if (rect != Rectangle.Empty)
            {
                disablerects.Add(rect);
            }
        }

        public virtual void ArchitectureBeginRecentlyAttacked(Architecture architecture)
        {
        }

        public virtual void ArchitectureFacilityCompleted(Architecture architecture, Facility facility)
        {
        }

        public virtual void Architecturefashengzainan(Architecture architecture, int  zainanID)
        {
        }


        public virtual void ArchitectureHirePerson(PersonList personList)
        {
        }

        public virtual void haizizhangdachengren(Person father, Person person, bool showChildTalk)
        {
        }


        public virtual void ArchitecturePopulationEnter(Architecture a, int quantity)
        {
        }

        public virtual void ArchitecturePopulationEscape(Architecture a, int quantity)
        {
        }

        public virtual void ArchitectureReleaseCaptiveAfterOccupied(Architecture architecture, PersonList persons)
        {
        }

        public virtual void ArchitectureRewardPersons(Architecture architecture, GameObjectList personlist)
        {
        }

        public virtual void CaptivePlayerRelease(Faction from, Faction to, Captive captive)
        {
        }

        public virtual void CaptiveRelease(bool success, Faction from, Faction to, Person person)
        {
        }

        public void ClearDisableRects()
        {
            this.ScrollDisableRects.Clear();
            this.LaterMouseEventDisableRects.Clear();
            this.SelectingDisableRects.Clear();
        }

        public virtual void Deactivate()
        {
            this.EnableUpdate = false;
            //base.Game.IsMouseVisible = true;
            this.AddDisableRectangle(this.LaterMouseEventDisableRects, new Rectangle(0, 0, this.viewportSize.X, this.viewportSize.Y));
        }

        public virtual void EarlyMouseLeftDown()
        {
            if (this.OnMouseLeftDown != null)
            {
                this.OnMouseLeftDown(new Point(Convert.ToInt32(InputManager.Position.X), Convert.ToInt32(InputManager.Position.Y)));  //   this.mouseState.X, this.mouseState.Y));
            }
        }

        public virtual void EarlyMouseLeftUp()
        {
            if (this.OnMouseLeftUp != null)
            {
                this.OnMouseLeftUp(new Point(Convert.ToInt32(InputManager.Position.X), Convert.ToInt32(InputManager.Position.Y)));   //  this.mouseState.X, this.mouseState.Y
            }
        }

        public virtual void EarlyMouseMove()
        {
            if (this.OnMouseMove != null)
            {
                this.OnMouseMove(new Point(Convert.ToInt32(InputManager.Position.X), Convert.ToInt32(InputManager.Position.Y)), InputManager.IsMoved);
                //this.OnMouseMove(new Point(this.mouseState.X, this.mouseState.Y), this.mouseState.LeftButton == ButtonState.Pressed);
            }
        }

        public virtual void EarlyMouseRightDown()
        {
            if (this.OnMouseRightDown != null)
            {
                this.OnMouseRightDown(new Point(this.mouseState.X, this.mouseState.Y));
            }
        }

        public virtual void EarlyMouseRightUp()
        {
            if (this.OnMouseRightUp != null)
            {
                this.OnMouseRightUp(new Point(this.mouseState.X, this.mouseState.Y));
            }
        }

        public virtual void EarlyMouseScroll()
        {
            if (this.OnMouseScroll != null)
            {
                this.OnMouseScroll(new Point(this.mouseState.X, this.mouseState.Y), this.mouseState.ScrollWheelValue);
            }
        }

        public virtual void FactionAfterCatchLeader(Person leader, Faction faction)
        {
        }

        public virtual void FactionDestroy(Faction faction)
        {
        }

        public virtual void FactionForcedChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
        }

        public virtual void FactionGetControl(Faction faction)
        {
        }

        public virtual void FactionInitialtiveChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
        }

        public virtual void FactionTechniqueFinished(Faction faction, Technique technique)
        {
        }

        public virtual void FactionUpgradeTechnique(Faction faction, Technique technique, Architecture architecture)
        {
        }

        public virtual void GameEndWithUnite(Faction faction)
        {
        }

        public virtual void GameGo(GameTime gameTime)
        {
        }

        protected void GenerateEarlyMouseEvent()
        {
            if (this.EnableMouseEvent)
            {
                if (InputManager.SleepTime == 0f)
                {
                    if (InputManager.IsDown)
                    {
                        this.EarlyMouseLeftDown();
                    }

                    if (InputManager.IsReleased)  // || Platform.IsMobilePlatForm && InputManager.IsPressed)
                    {
                        this.EarlyMouseLeftUp();
                    }
                }

                if (this.previousMouseState.RightButton != this.mouseState.RightButton)
                {
                    if (this.mouseState.RightButton == ButtonState.Pressed)
                    {
                        this.EarlyMouseRightDown();
                    }
                    else if (this.mouseState.RightButton == ButtonState.Released)
                    {
                        this.EarlyMouseRightUp();
                    }
                }

                if (InputManager.IsPosChanged)
                {
                    this.EarlyMouseMove();
                }

                if (this.mouseState.ScrollWheelValue != this.previousMouseState.ScrollWheelValue)
                {
                    this.EarlyMouseScroll();
                }
            }
        }

        //protected void GenerateEarlyMouseEventOld()
        //{
        //    if (this.EnableMouseEvent)
        //    {
        //        if (this.previousMouseState.LeftButton != this.mouseState.LeftButton)
        //        {
        //            if (this.mouseState.LeftButton == ButtonState.Pressed)
        //            {
        //                this.EarlyMouseLeftDown();
        //            }
        //            else if (this.mouseState.LeftButton == ButtonState.Released)
        //            {
        //                this.EarlyMouseLeftUp();
        //            }
        //        }
        //        else if (this.previousMouseState.RightButton != this.mouseState.RightButton)
        //        {
        //            if (this.mouseState.RightButton == ButtonState.Pressed)
        //            {
        //                this.EarlyMouseRightDown();
        //            }
        //            else if (this.mouseState.RightButton == ButtonState.Released)
        //            {
        //                this.EarlyMouseRightUp();
        //            }
        //        }
        //        if ((this.mouseState.X != this.previousMouseState.X) || (this.mouseState.Y != this.previousMouseState.Y))
        //        {
        //            this.EarlyMouseMove();
        //        }
        //        if (this.mouseState.ScrollWheelValue != this.previousMouseState.ScrollWheelValue)
        //        {
        //            this.EarlyMouseScroll();
        //        }
        //    }
        //}

        public virtual Rectangle GetDestination(Point mapPosition)
        {
            return Rectangle.Empty;
        }

        public virtual Point GetPointByPosition(Point mapPosition)
        {
            return Point.Zero;
        }

        public virtual Texture2D GetPortrait(float id)
        {
            return null;
        }

        public virtual Point GetPositionByPoint(Point mousePoint)
        {
            return Point.Zero;
        }

        //[DllImport("Kernel32", CharSet=CharSet.Auto)]
        //private static extern int GetShortPathName(string path, StringBuilder shortPath, int shortPathLength);
        public virtual Texture2D GetSmallPortrait(float id)
        {
            return null;
        }

        public virtual Texture2D GetTroopPortrait(float id)
        {
            return null;
        }
        public virtual Texture2D GetFullPortrait(float id)
        {
            return null;
        }
        //public override void Initialize()
        //{
        //    base.Initialize();
        //}

        public bool IsPositionEnable(List<Rectangle> disablerects, Point position)
        {
            foreach (Rectangle rectangle in disablerects)
            {
                if (StaticMethods.PointInRectangle(position, rectangle))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void JumpTo(Point mapPosition)
        {
        }

        protected void LoadContent()
        {
            //base.LoadContent();
            //IGraphicsDeviceService service = base.Game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            //this.batch = new SpriteBatch(service.GraphicsDevice);
        }

        public virtual void LoadGame()
        {
        }

        public virtual void LoadScenario()
        {
        }

        //[DllImport("winmm.dll")]
        //public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);
        
        //protected void OnVisibleChanged(object sender, EventArgs args)
        //{            
        //    base.OnVisibleChanged(sender, args);
        //    if (!base.Visible)
        //    {
        //    }
        //}

        public virtual UndoneWorkItem PeekUndoneWork()
        {
            return this.UndoneWorks.Peek();
        }

        public virtual void PersonBeAwardedTreasure(Person person, Treasure t)
        {
        }

        public virtual void PersonBeConfiscatedTreasure(Person person, Treasure t)
        {
        }

        public virtual void PersonBeKilled(Person person, Architecture location)
        {
        }
        /*
        public virtual void QuanXiangFailed(Person source, Faction targetFaction)
        {
        }
        */
        public virtual void PersonChangeLeader(Faction faction, Person leader, bool changeName, string oldName)
        {
        }

        public virtual void PersonConvinceFailed(Person source, Person destination)
        {
        }

        public virtual void PersonConvinceSuccess(Person source, Person destination, Faction oldFaction)
        {
        }

        public virtual void PersonDeath(Person person, Person killerInBattle, Architecture location, Troop troopLocation)
        {
        }

        public virtual void PersonDeathInChallenge(Person person, Troop troop)
        {
        }


        public virtual void PersonDeathChangeFaction(Person dead, Person leader, string oldName)
        {
        }

        public virtual void PersonDestroyFailed(Person person, Architecture architecture)
        {
        }

        public virtual void PersonDestroySuccess(Person person, Architecture architecture, int down)
        {
        }

        public virtual void PersonGossipFailed(Person person, Architecture architecture)
        {
        }

        public virtual void PersonGossipSuccess(Person person, Architecture architecture)
        {
        }

        public virtual void PersonInformationObtained(Person person, Information information)
        {
        }

        public virtual void qingbaoshibai(Person person)
        {
        }

        public virtual void PersonInstigateFailed(Person person, Architecture architecture)
        {
        }

        public virtual void PersonInstigateSuccess(Person person, Architecture architecture, int down)
        {
        }

        public virtual void PersonLeave(Person person, Architecture location)
        {
        }

        public virtual void PersonSearchFinished(Person person, Architecture architecture, SearchResultPack resultPack)
        {
        }
        /*
        public virtual void PersonShowMessage(Person person, PersonMessage message)
        {
        }
        
        public virtual void PersonSpyFailed(Person person, Architecture architecture)
        {
        }

        public virtual void PersonSpyFound(Person person, Person spy)
        {
        }

        public virtual void PersonSpySuccess(Person person, Architecture architecture)
        {
        }
        */
        public virtual void PersonStudySkillFinished(Person person, string skillString, bool success)
        {
        }

        public virtual void PersonStudyStuntFinished(Person person, Stunt stunt, bool success)
        {
        }

        public virtual void PersonStudyTitleFinished(Person person, Title title, bool success)
        {
        }

        public virtual void PersonTreasureFound(Person person, Treasure treasure)
        {
        }

        public virtual void PersonCapturedByArchitecture(Person person, Architecture architecture)
        {
        }

        public virtual void PersonJailBreak(Person person, Captive captive)
        {
        }

        public virtual void PersonJailBreakFailed(Person person, Architecture target)
        {
        }

        public virtual void PersonAssassinateSuccess(Person killer, Person killed, Architecture a)
        {
        }

        public virtual void PersonAssassinateSuccessKilled(Person killer, Person killed, Architecture a)
        {
        }

        public virtual void PersonAssassinateSuccessCaptured(Person killer, Person killed, Architecture a)
        {
        }

        public virtual void PersonAssassinateFailed(Person killer, Person killed, Architecture a)
        {
        }

        public virtual void PersonAssassinateFailedKilled(Person killer, Person killed, Architecture a)
        {
        }

        public void PlayImportantSound(string soundFileLocation)
        {
            if (GlobalVariables.PlayBattleSound)  // && File.Exists(soundFileLocation))
            {
                Platform.Current.PlayEffect(soundFileLocation);
            }
        }

        public virtual void PlayMusic(string musicFileLocation)
        {
        }

        public void PlayNormalSound(string soundFileLocation)
        {
            if (GlobalVariables.PlayNormalSound)  // && File.Exists(soundFileLocation))
            {
                Platform.Current.PlayEffect(soundFileLocation);
                //PlaySound(soundFileLocation);
            }
        }

        //public static void PlaySound(string name)
        //{
        //    StringBuilder shortPath = new StringBuilder(80);
        //    int num = GetShortPathName(name, shortPath, shortPath.Capacity);
        //    name = shortPath.ToString();
        //    string str = string.Empty;
        //    mciSendString("play " + name, str, str.Length, 0);
        //}

        public virtual UndoneWorkItem PopUndoneWork()
        {
            return this.UndoneWorks.Pop();
        }

        public bool PositionOutOfScreen(Point position)
        {
            return ((((position.X < 0) || (position.Y < 0)) || (position.X > this.viewportSize.X)) || (position.Y > this.viewportSize.Y));
        }

        public virtual void PushUndoneWork(UndoneWorkItem undoneWork)
        {
            this.UndoneWorks.Push(undoneWork);
        }

        private void RefreshEnables()
        {
            this.EnableScroll = this.IsPositionEnable(this.ScrollDisableRects, new Point(InputManager.PoX, InputManager.PoY));
            this.EnableLaterMouseEvent = this.IsPositionEnable(this.LaterMouseEventDisableRects, new Point(InputManager.PoX, InputManager.PoY));
            this.EnableSelecting = this.IsPositionEnable(this.SelectingDisableRects, new Point(InputManager.PoX, InputManager.PoY));
        }

        public void RemoveDisableRectangle(List<Rectangle> disablerects, Rectangle rect)
        {
            disablerects.Remove(rect);
        }

        public virtual void ResetMouse()
        {
        }

        public virtual void SaveGame()
        {
        }

        public virtual void SelfCaptiveRelease(Captive captive)
        {
        }

        public virtual void SetTroopTextures(Troop troop)
        {
        }

        public virtual void TechniqueComplete(Faction f, Technique t) { }

        public virtual void Shutdown()
        {
            //if (this.batch != null)
            //{
            //    this.batch.Dispose();
            //    this.batch = null;
            //}
        }

        public bool TileInScreen(Point tile)
        {
            return ((((tile.X >= this.TopLeftPosition.X) && (tile.Y >= this.TopLeftPosition.Y)) && (tile.X <= this.BottomRightPosition.X)) && (tile.Y <= this.BottomRightPosition.Y)
                && !this.Scenario.PositionOutOfRange(tile));
        }

        public virtual void TroopAmbush(Troop troop)
        {
        }

        public virtual void TroopAntiArrowAttack(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopAntiAttack(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopApplyStunt(Troop troop, Stunt stunt)
        {
        }

        public virtual void TroopApplyTroopEvent(TroopEvent te, Troop troop)
        {
        }

        public virtual void TroopBreakWall(Troop troop, Architecture architecture)
        {
        }

        public virtual void TroopCastDeepChaos(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopCastStratagem(Troop sending, Troop receiving, Stratagem stratagem)
        {
        }

        public virtual void TroopChaos(Troop troop, bool deepChaos)
        {
        }

        public virtual void TroopRumour(Troop troop)
        {
        }

        public virtual void TroopAttract(Troop troop, Troop caster)
        {
        }

        public virtual void TroopCombatMethodAttack(Troop sending, Troop receiving, CombatMethod combatMethod)
        {
        }

        public virtual void TroopCreate(Troop troop)
        {
        }

        public virtual void TroopCriticalStrike(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopDiscoverAmbush(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopEndCutRouteway(Troop troop, bool success)
        {
        }

        public virtual void TroopEndPath(Troop troop)
        {
        }

        public virtual void TroopGetNewCaptive(Troop troop, PersonList personlist)
        {
        }

        public virtual void TroopGetSpreadBurnt(Troop troop)
        {
        }

        public virtual void TroopLevyFieldFood(Troop troop, int food)
        {
        }

        public virtual void TroopNormalAttack(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopOccupyArchitecture(Troop troop, Architecture architecture)
        {
        }

        public virtual void xianshishijiantupian(Person p, string TextResultString, string shijian, string tupian, string shengyin, bool zongshixianshi)
        {

        }

        public virtual void xianshishijiantupian(Person p, string TextResultString, TextMessageKind msgKind, string shijian, string tupian, string shengyin, bool zongshixianshi)
        {
           
        }

        public virtual void xianshishijiantupian(Person p, string TextResultString, string shijian, string tupian, string shengyin, string TextDestinationString, bool zongshixianshi)
        {
        }

        public virtual void xianshishijiantupian(Person p, string TextResultString, TextMessageKind msgKind, string shijian, string tupian, string shengyin, string TextDestinationString, bool zongshixianshi)
        {
        }

        public virtual void xianshishijiantupian(object person, object gameObject, string branchName, bool zongshixianshi)
        {
        }

        public virtual void TroopOutburst(Troop troop, OutburstKind kind)
        {
        }

        public virtual void TroopPathNotFound(Troop troop)
        {
        }

        public virtual void TroopPersonChallenge(int  win, Troop sourceTroop, Person source, Troop destinationTroop, Person destination)
        {
        }

        public virtual void TroopPersonControversy(bool win, Troop sourceTroop, Person source, Troop destinationTroop, Person destination)
        {
        }

        public virtual void TroopReceiveCriticalStrike(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopReceiveWaylay(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopRecoverFromChaos(Troop troop)
        {
        }

        public virtual void TroopReleaseCaptive(Troop troop, PersonList personlist)
        {
        }

        public virtual void TroopResistStratagem(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
        }

        public virtual void TroopRout(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopRouted(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopSetCombatMethod(Troop troop, CombatMethod combatMethod)
        {
        }

        public virtual void TroopSetStratagem(Troop troop, Stratagem stratagem)
        {
        }

        public virtual void TroopStartCutRouteway(Troop troop, int days)
        {
        }

        public virtual void TroopStopAmbush(Troop troop)
        {
        }

        public virtual void TroopStratagemSuccess(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
        }

        public virtual void TroopSurround(Troop sending, Troop receiving)
        {
        }

        public virtual void TroopWaylay(Troop sending, Troop receiving)
        {
        }

        public virtual void ObtainMilitaryKind(Faction f, Person giver, MilitaryKind m)
        {
        }

        public virtual void AutoLearnTitle(Person p, Person courier, Title title)
        {
        }
        /*
        public virtual void AutoAwardGuanzhi(Person p, Person courier, Guanzhi guanzhi)
        {
        }
        */
        public virtual void ApplyEvent(Event e, Architecture a)
        {
        }

        public virtual void TryToExit()
        {
        }

        public virtual void Zhaoxian(Person p, Person q) { }

        public virtual void Appointmayor(Person p, Person q) { }

        public virtual void Selectprince(Person p, Person q) { }

        public virtual void CreateSpouse(Person p, Person q) { }

        public virtual void CreateBrother(Person p, Person q) { }

        public virtual void CreateSister(Person p, Person q) { }

        public virtual void MakeMarriage(Person p, Person q) { }

        public virtual void Update(GameTime gameTime)
        {
            if (this.EnableUpdate)
            {
                //base.Update(gameTime);
                //base.Game.IsMouseVisible = false;

                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
                {
                    this.previousMouseState = this.mouseState;
                    this.mouseState = Mouse.GetState();
                    this.keyState = Keyboard.GetState();
                }
                else
                {

                }

                this.GenerateEarlyMouseEvent();

                this.RefreshEnables();
            }
        }

        public Texture2D DefaultMouseArrowTexture
        {
            get
            {
                return this.defaultMouseArrowTexture;
            }
            set
            {
                this.defaultMouseArrowTexture = value;
                this.MouseArrowTexture = value;
            }
        }

        public KeyboardState KeyState
        {
            get
            {
                return this.keyState;
            }
        }

        public Point MouseOffset
        {
            get
            {
                return new Point(InputManager.PoXMove, InputManager.PoY);
                //return new Point(this.mouseState.X - this.previousMouseState.X, this.mouseState.Y - this.previousMouseState.Y);
            }
        }

        public Point MousePosition
        {
            get
            {
                return new Point(InputManager.PoX, InputManager.PoY);
            }
        }

        public Microsoft.Xna.Framework.Input.MouseState MouseState
        {
            get
            {
                return this.mouseState;
            }
        }

        public Point RealViewportSize
        {
            get
            {
                return viewportSize;
                //return new Point(Platform.GraphicsDevice.Viewport.X, Platform.GraphicsDevice.Viewport.Y);
            }
        }

        public SpriteBatch spriteBatch
        {
            get
            {
                return Session.Current.SpriteBatch;
            }
        }

        public virtual bool DrawingSelector
        {
            get;
            set;
        }

        public delegate void MouseLeftDown(Point position);

        public delegate void MouseLeftUp(Point position);

        public delegate void MouseMove(Point position, bool leftDown);

        public delegate void MouseRightDown(Point position);

        public delegate void MouseRightUp(Point position);

        public delegate void MouseScroll(Point position, int scrollValue);

        public virtual void selfFoundPregnant(Person person)
        {
        }

        public virtual void coupleFoundPregnant(Person person, Person reporter)
        {
        }

        public virtual void faxianhuaiyun(Person person)
        {
        }

        public virtual void xiaohaichusheng(Person father, Person mother, Person person)
        {
        }

        public virtual void shilijingong(Faction faction,int jingongzijin,string jingongzhonglei)
        {
            
        }

        public virtual void xiejinxingjilu(string shijian, string TextResultString, string TextDestinationString, Point point)
        {
            
        }

        public virtual void renwudaodatishi(Person person, Architecture architecture)
        {
            
        }

        public virtual void renwukaishitishi(Person person, Architecture architecture)
        {

        }

        public virtual void SwichMusic(GameSeason season)
        {
            
        }

        public virtual void AskWhenTransportArrived(Troop transport, Architecture destination)
        {
        }

        public virtual void NoFactionPersonArrivesAtArchitecture(Person p, Architecture a)
        {
        }

        public virtual void TransferMilitaryArrivesAtArchitecture(Military m, Architecture a)
        {
        }

        public virtual void ReloadScreenData()
        {
        }

        public virtual void OnOfficerInjured(Person p) { }

        public virtual void OnOfficerSick(Person p) { }

        public virtual void OnOfficerRecovered(Person p) { }

        public virtual void OnExecute(Person executor, Person executed) { }

        public virtual void OnTroopRout(Troop router, Troop routed) { }

        public virtual void OnAIMergeAgainstPlayer(Faction strongestPlayer, Faction merger, Faction merged) { }

        public virtual void 减小音量()
        {
        }
        public virtual void 增加音量()
        {
        }
        public virtual void 返回初始菜单()
        {
        }
    }
}

