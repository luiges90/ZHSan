using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;
using System.Collections.Generic;
//using System.Runtime.InteropServices;
using System.Xml;

namespace youcelanPlugin
{

    public class TabListInFrame : FrameContent
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


        internal Point TopLeftPosition = new Point();
        internal string checkboxDisplayName;
        internal string checkboxName;
        internal PlatformTexture checkboxSelectedTexture;
        internal PlatformTexture checkboxTexture;
        internal int checkboxWidth;
        internal int columnheaderHeight;
        internal PlatformTexture columnheaderTexture;
        internal int columnspliterHeight;
        internal PlatformTexture columnspliterTexture;
        internal int columnspliterWidth;
        internal TextAlign ColumnTextAlign;

        internal Font ColumnTextBuilder = new Font();

        internal Color ColumnTextColor;
        internal bool DrawFocused = false;
        private bool firstTimeMapViewSelector = true;
        internal int Focused;
        internal GameObject FocusedObject;
        internal PlatformTexture focusTrackTexture;
        internal Rectangle FullLowerClient;
        internal GameObjectList gameObjectList;
        private bool HeightCanShrink = true;
        internal IArchitectureDetail iArchitectureDetail;
        internal IFactionTechniques iFactionTechniques;
        internal IGameFrame iGameFrame;
        internal IMapViewSelector iMapViewSelector;
        internal IPersonDetail iPersonDetail;
        internal ITreasureDetail iTreasureDetail;
        internal ITroopDetail iTroopDetail;
        internal PlatformTexture leftArrowTexture;
        internal List<ListKind> ListKinds;
        private ListKind listKindToDisplay;
        internal bool MovingHorizontalScrollBar = false;
        internal bool MovingVerticalScrollBar = false;
        internal bool MultiSelecting = false;
        private Point oldMousePosition;
        internal int oldScrollValue;
        internal int PortraitHeight;
        internal int PortraitWidth;
        internal PlatformTexture rightArrowTexture;
        private bool RightClickClose = true;
        internal SubKind RootListKind;
        internal PlatformTexture roundcheckboxSelectedTexture;
        internal PlatformTexture roundcheckboxTexture;
        internal int rowHeight;
        internal List<Rectangle> RowRectangles;
        
        internal PlatformTexture scrollbuttonTexture;
        internal int scrollbuttonWidth;
        internal PlatformTexture scrolltrackTexture;
        internal int scrolltrackWidth;
        internal GameObject SelectedItem;
        internal GameObjectList SelectedItemList;
        internal int SelectedItemMaxCount;
        internal bool SelectingBool = false;
        internal bool SelectingRows = false;
        internal string SelectSoundFile;
        internal bool ShowCheckBox = false;
        internal bool ShowHorizontalScrollBar = false;
        internal bool ShowVerticalScrollBar = false;
        private Stack<SubKind> SubKinds = new Stack<SubKind>();
        internal int tabbuttonHeight;
        internal PlatformTexture tabbuttonselectedTexture;
        internal PlatformTexture tabbuttonTexture;
        internal int tabbuttonWidth;
        internal TextAlign TabTextAlign;

        internal Font TabTextBuilder = new Font();

        internal Color TabTextColor;
        internal string Title = "";
        internal Rectangle VisibleLowerClient;

        internal PlatformTexture ToolDisplayTexture;
        internal Rectangle ToolPosition;
        internal PlatformTexture ToolSelectedTexture;
        internal PlatformTexture ToolTexture;

        private bool WidthCanShrink = true;


        public  bool xianshiyoucelan=true;


        private Rectangle backgroundRectangle;
        internal PlatformTexture backgroundTexture;

        private Rectangle bottomedgeRectangle;
        internal PlatformTexture bottomedgeTexture;
        internal int bottomedgeWidth;


    
        private Rectangle leftedgeRectangle;
        internal PlatformTexture leftedgeTexture;
        internal int leftedgeWidth;
    
   
        internal Rectangle Position;
     
        private Rectangle rightedgeRectangle;
        internal PlatformTexture rightedgeTexture;
        internal int rightedgeWidth;
    
      
        private Rectangle topedgeRectangle;
        internal PlatformTexture topedgeTexture;
        internal int topedgeWidth;
        
        internal Rectangle ToolDisplayPosition;
        //private FrameContent frameContent = null;
        private bool jiancexianshi=false ;
        public void SetyoucelanContent(Point viewportSize)
        {
            //this.Result = FrameResult.Cancel;
            //this.frameContent = frameContent;
            //this.frameContent.Function = this.Function;
            //this.TitleText.Text = frameContent.GetCurrentTitle();

            this.FramePosition = GetRectangleFityoucelan(this.DefaultFrameWidth, this.DefaultFrameHeight, viewportSize);
            //this.OKButtonPosition = frameContent.OKButtonPosition;
            //this.CancelButtonPosition = frameContent.CancelButtonPosition;
            //this.MapViewSelectorButtonPosition = frameContent.MapViewSelectorButtonPosition;
            this.SetPosition(this.FramePosition);
            this.ReCalculate();


            //this.InitializeMapViewSelectorButton();
            //this.OnItemClick += new FrameContent.ItemClick(this.frameContent_OnItemClick);
        }
        /*
        private void frameContent_OnItemClick()
        {
            if (this.Function == FrameFunction.Jump)
            {
                //this.IsShowing = false;
                if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.None)
                {
                    //throw new Exception("The UndoneWork is not a Frame.");  //错误检查
                }

            }
        }*/



        private Rectangle GetRectangleFityoucelan(int width, int height, Point viewportSize)
        {
            int x = width;
            int y = height;
            if (viewportSize.X < width)
            {
                x = viewportSize.X;
            }
            if (viewportSize.Y < height)
            {
                y = viewportSize.Y;
            }
            //return new Rectangle((viewportSize.X - x-this.rightedgeWidth*3), (viewportSize.Y - y) / 2, x, y);
            return new Rectangle(this.TopLeftPosition.X , this.TopLeftPosition.Y , x, y);

            //return new Rectangle((viewportSize.X - x ), (viewportSize.Y - y) / 2, x, y);

        }
        
        private void SetPosition(Rectangle position)
        {
            this.Position = position;
            this.ResetRectangles();
        }

        private void ResetRectangles()
        {
            this.leftedgeRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y, this.leftedgeWidth, this.Position.Height);
            this.rightedgeRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y, this.rightedgeWidth, this.Position.Height);
            this.topedgeRectangle = new Rectangle(this.Position.X, this.Position.Y - this.topedgeWidth, this.Position.Width, this.topedgeWidth);
            this.bottomedgeRectangle = new Rectangle(this.Position.X, this.Position.Y + this.Position.Height, this.Position.Width, this.bottomedgeWidth);
            this.backgroundRectangle = new Rectangle(this.Position.X, this.Position.Y, this.Position.Width, this.Position.Height);

            this.ToolDisplayPosition = new Rectangle(this.Position.X+this.ToolPosition.X , this.Position.Y+this.ToolPosition.Y , this.ToolPosition.Width, this.ToolPosition.Height);

            this.TopLeftRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y - this.topedgeWidth, this.leftedgeWidth, this.topedgeWidth);
            this.TopRightRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y - this.topedgeWidth, this.rightedgeWidth, this.topedgeWidth);
            this.BottomLeftRectangle = new Rectangle(this.Position.X - this.leftedgeWidth, this.Position.Y + this.Position.Height, this.leftedgeWidth, this.bottomedgeWidth);
            this.BottomRightRectangle = new Rectangle(this.Position.X + this.Position.Width, this.Position.Y + this.Position.Height, this.rightedgeWidth, this.bottomedgeWidth);


            //this.okbuttonRectangle = new Rectangle(this.Position.X + this.okbuttonPosition.X, this.Position.Y + this.okbuttonPosition.Y, this.okbuttonSize.X, this.okbuttonSize.Y);
            //this.cancelbuttonRectangle = new Rectangle(this.Position.X + this.cancelbuttonPosition.X, this.Position.Y + this.cancelbuttonPosition.Y, this.cancelbuttonSize.X, this.cancelbuttonSize.Y);
            //this.titleRectangle = new Rectangle(this.Position.X, this.Position.Y - this.titleHeight, this.titleWidth, this.titleHeight);
            //this.TitleText.Position = this.titleRectangle;
            //this.mapviewselectorButtonRectangle = new Rectangle(this.Position.X + this.mapviewselectorbuttonPosition.X, this.Position.Y + this.mapviewselectorbuttonPosition.Y, this.mapviewselectorbuttonSize.X, this.mapviewselectorbuttonSize.Y);
        }



        public void AddRows()
        {
            if (this.gameObjectList != null)
            {
                this.RowRectangles.Clear();
                for (int i = 0; i < this.gameObjectList.Count; i++)
                {
                    this.RowRectangles.Add(new Rectangle(this.VisibleLowerClient.X, (this.VisibleLowerClient.Y + this.columnheaderHeight) + (this.rowHeight * i), this.VisibleLowerClient.Width - 1, this.rowHeight));
                }
            }
        }

        public void ClearData()
        {
            this.Title = "";
            this.RowRectangles.Clear();
            this.Focused = 0;
            this.WidthCanShrink = true;
            this.HeightCanShrink = true;
            if (this.listKindToDisplay != null)
            {
                this.listKindToDisplay.ClearData();
            }
        }

        public override void Draw()
        {
            if (this.jiancexianshi)
            {
                CacheManager.Draw(this.ToolDisplayTexture, this.ToolDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
                if (this.xianshiyoucelan)
                {

                    CacheManager.Draw(this.leftedgeTexture, this.leftedgeRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    CacheManager.Draw(this.rightedgeTexture, this.rightedgeRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    CacheManager.Draw(this.topedgeTexture, this.topedgeRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    CacheManager.Draw(this.bottomedgeTexture, this.bottomedgeRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    CacheManager.Draw(this.backgroundTexture, this.backgroundRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);

                    CacheManager.Draw(this.TopLeftTexture, this.TopLeftRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
                    CacheManager.Draw(this.TopRightTexture, this.TopRightRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
                    CacheManager.Draw(this.BottomLeftTexture, this.BottomLeftRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
                    CacheManager.Draw(this.BottomRightTexture, this.BottomRightRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);


                    //base.Draw();
                    //if (this.frameContent != null)
                    //{
                    //    this.frameContent.Draw();
                    //}

                    if (this.listKindToDisplay != null)
                    {
                        this.listKindToDisplay.Draw();
                    }
                }
            }
        }


        /*private Rectangle ToolDisplayPosition
        {
            get
            {
                return new Rectangle(this.ToolPosition.X + 60, this.ToolPosition.Y + 50, this.ToolPosition.Width, this.ToolPosition.Height);
            }
        }*/


        public void EnlargeRectanglesHeight()
        {
            if (!this.HeightCanShrink)
            {
                this.VisibleLowerClient = new Rectangle(this.VisibleLowerClient.X, this.VisibleLowerClient.Y, this.VisibleLowerClient.Width, (this.VisibleLowerClient.Height + (2 * this.scrolltrackWidth)) + this.scrollbuttonWidth);
                this.HeightCanShrink = true;
            }
        }

        public void EnlargeRectanglesWidth()
        {
            if (!this.WidthCanShrink)
            {
                this.VisibleLowerClient = new Rectangle(this.VisibleLowerClient.X, this.VisibleLowerClient.Y, (this.VisibleLowerClient.Width + (2 * this.scrolltrackWidth)) + this.scrollbuttonWidth, this.VisibleLowerClient.Height);
                for (int i = 0; i < this.RowRectangles.Count; i++)
                {
                    this.RowRectangles[i] = new Rectangle(this.RowRectangles[i].X, this.RowRectangles[i].Y, (this.RowRectangles[i].Width + (2 * this.scrolltrackWidth)) + this.scrollbuttonWidth, this.RowRectangles[i].Height);
                }
                this.WidthCanShrink = true;
            }
        }

        public Tab FindTabByPosition(Point position)
        {
            foreach (Tab tab in this.listKindToDisplay.Tabs)
            {
                if (StaticMethods.PointInRectangle(position, tab.Position))
                {
                    return tab;
                }
            }
            return null;
        }

        private Column GetColumnByPosition(Point position)
        {
            if (this.listKindToDisplay.SelectedTab != null)
            {
                foreach (Column column in this.listKindToDisplay.SelectedTab.Columns)
                {
                    if (((column.DisplayPosition.Left >= this.VisibleLowerClient.Left) && (column.DisplayPosition.Right <= this.VisibleLowerClient.Right)) && StaticMethods.PointInRectangle(position, column.DisplayPosition))
                    {
                        return column;
                    }
                }
            }
            return null;
        }

        public override string GetCurrentTitle()
        {
            if ((this.Title == "") && (this.listKindToDisplay != null))
            {
                return this.listKindToDisplay.DisplayName;
            }
            return this.Title;
        }

        private GameObject GetGameObjectByPosition(Point position)
        {
            if (this.RowRectangles.Count != this.gameObjectList.Count) return null;
            for (int i = 0; i < this.RowRectangles.Count; i++)
            {
                Rectangle rectangle = this.RowRectangles[i];
                if (((rectangle.Bottom <= this.VisibleLowerClient.Bottom) && ((rectangle = this.RowRectangles[i]).Top >= (this.VisibleLowerClient.Top + this.columnheaderHeight))) && StaticMethods.PointInRectangle(position, this.RowRectangles[i]))
                {
                    return this.gameObjectList[i];
                }
            }
            return null;
        }

        public ListKind GetListKindByID(int ID)
        {
            foreach (ListKind kind in this.ListKinds)
            {
                if (kind.ID == ID)
                {
                    return kind;
                }
            }
            return null;
        }

        public ListKind GetListKindByName(string Name)
        {
            foreach (ListKind kind in this.ListKinds)
            {
                if (kind.Name == Name)
                {
                    return kind;
                }
            }
            return null;
        }

        public Rectangle GetRealLowerVisibleClient()
        {
            return new Rectangle(this.RealClient.X, this.listKindToDisplay.ColumnsTop, this.RealClient.Width, this.RealClient.Bottom - this.listKindToDisplay.ColumnsTop);
        }

        public int GetRowTopByPosition(Point position)
        {
            foreach (Rectangle rectangle in this.RowRectangles)
            {
                if (((rectangle.Bottom <= this.VisibleLowerClient.Bottom) && (rectangle.Top >= (this.VisibleLowerClient.Top + this.columnheaderHeight))) && StaticMethods.PointInRectangle(position, rectangle))
                {
                    return rectangle.Top;
                }
            }
            return -1;
        }

        public override string GetTitleString()
        {
            if (this.listKindToDisplay != null)
            {
                return this.listKindToDisplay.DisplayName;
            }
            return "未知";
        }

        internal void Initialize()
        {
            this.ListKinds = new List<ListKind>();
            this.RowRectangles = new List<Rectangle>();
        }

        public override void InitializeMapViewSelectorButton()
        {
            GameDelegates.VoidFunction function = null;
            if (this.MapViewSelectorButtonEnabled)
            {
                if (function == null)
                {
                    function = delegate {
                        this.iMapViewSelector.SetMultiSelecting(this.MultiSelecting);
                        this.iMapViewSelector.SetGameObjectList(this.gameObjectList);
                        if (this.firstTimeMapViewSelector)
                        {
                            this.firstTimeMapViewSelector = false;
                            this.iMapViewSelector.SetMapPosition(ShowPosition.Center);
                        }
                        this.iMapViewSelector.IsShowing = true;
                    };
                }
                this.MapViewSelectorFunction = function;
            }
        }

        public void InitialValues(GameObjectList gameObjectList, GameObjectList selectedObjectList, int scrollValue, string title)
        {
            this.SubKinds.Clear();
            this.SetObjectList(gameObjectList);
            //this.SetSelectedObjectList(selectedObjectList); 不能影响建筑连接的改动，建筑列表插件和建筑是否被选中无关
            this.oldScrollValue = scrollValue;
            this.Title = title;
        }

        public void LoadFromXMLNode(XmlNode rootNode)  //读取xml里的列表文件样式，包括tablist 和 listkind
        {
            this.rowHeight = int.Parse(rootNode.Attributes.GetNamedItem("RowHeight").Value);
            this.defaultFrameWidth = int.Parse(rootNode.Attributes.GetNamedItem("FrameWidth").Value);
            this.defaultFrameHeight = int.Parse(rootNode.Attributes.GetNamedItem("FrameHeight").Value);
            this.client.X = int.Parse(rootNode.Attributes.GetNamedItem("ClientX").Value);
            this.client.Y = int.Parse(rootNode.Attributes.GetNamedItem("ClientY").Value);
            this.client.Width = int.Parse(rootNode.Attributes.GetNamedItem("ClientWidth").Value);
            this.client.Height = int.Parse(rootNode.Attributes.GetNamedItem("ClientHeight").Value);
            this.defaultOKButtonPosition.X = int.Parse(rootNode.Attributes.GetNamedItem("OKButtonX").Value);
            this.defaultOKButtonPosition.Y = int.Parse(rootNode.Attributes.GetNamedItem("OKButtonY").Value);
            this.defaultCancelButtonPosition.X = int.Parse(rootNode.Attributes.GetNamedItem("CancelButtonX").Value);
            this.defaultCancelButtonPosition.Y = int.Parse(rootNode.Attributes.GetNamedItem("CancelButtonY").Value);
            this.defaultMapViewSelectorButtonPosition.X = int.Parse(rootNode.Attributes.GetNamedItem("MapViewSelectorButtonX").Value);
            this.defaultMapViewSelectorButtonPosition.Y = int.Parse(rootNode.Attributes.GetNamedItem("MapViewSelectorButtonY").Value);
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                ListKind item = new ListKind(this) {
                    ID = int.Parse(node.Attributes.GetNamedItem("ID").Value),
                    Name = node.Attributes.GetNamedItem("Name").Value,
                    DisplayName = node.Attributes.GetNamedItem("DisplayName").Value,
                    ShowPortrait = bool.Parse(node.Attributes.GetNamedItem("ShowPortrait").Value)
                };
                item.LoadFromXMLNode(node);
                this.ListKinds.Add(item);
            }
        }

        internal void PopSubKind()
        {
            if (this.SubKinds.Count > 0)
            {
                SubKind rootListKind;
                this.SubKinds.Pop();
                if (this.SubKinds.Count > 0)
                {
                    rootListKind = this.SubKinds.Peek();
                }
                else
                {
                    rootListKind = this.RootListKind;
                }
                this.SetObjectList(rootListKind.List);
                this.listKindToDisplay = rootListKind.Kind;
                this.ReCalculate();
            }
            else
            {
                this.RightClickClose = true;
            }
        }

        internal void PushSubKindByName(string name, GameObjectList list)
        {
            if ((list != null) && (list.Count != 0))
            {
                if (this.SubKinds.Count == 0)
                {
                    this.RightClickClose = false;
                    this.RootListKind.Kind = this.listKindToDisplay;
                    this.RootListKind.List = this.gameObjectList;
                }
                this.SetObjectList(list);
                this.listKindToDisplay = this.GetListKindByName(name);
                if (this.listKindToDisplay != null)
                {
                    this.ReCalculate();
                    SubKind item = new SubKind {
                        Kind = this.listKindToDisplay,
                        List = list
                    };
                    this.SubKinds.Push(item);
                }
            }
        }

        public override void ReCalculate()
        {
            //base.ReCalculate();
            if (this.listKindToDisplay != null)
            {
                this.listKindToDisplay.ReCalculate();
            }
            this.SelectDefaultTab();
        }

        internal void RefreshEditable()
        {
            this.ResetEditableTextures();
            this.OKButtonEnabled = this.gameObjectList.HasSelectedItem();
            if (this.MultiSelecting)
            {
                this.SelectedItemList = this.gameObjectList.GetSelectedList();
            }
            else if (this.OKButtonEnabled)
            {
                this.SelectedItem = this.gameObjectList.GetSelectedList()[0];
            }
        }

        internal void ResetEditableTextures()
        {
            if (this.listKindToDisplay != null)
            {
                this.listKindToDisplay.ResetEditableTextures();
            }
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.ToolDisplayPosition))
            {
                this.xianshiyoucelan  = !this.xianshiyoucelan ;
                if (true) //base.Enabled)
                {
                    if (this.xianshiyoucelan)
                    {
                        this.ToolDisplayTexture = this.ToolSelectedTexture;
                    }
                    else
                    {
                        this.ToolDisplayTexture = this.ToolTexture;
                    }
                }
            }

            if (this.xianshiyoucelan && (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None) && StaticMethods.PointInRectangle(position, this.RealClient))
            {
                if (position.Y < this.listKindToDisplay.ColumnsTop)
                {
                    Tab tab = this.FindTabByPosition(position);
                    if (tab != null)
                    {
                        tab.Selected = true;
                    }
                }
                else if (position.Y < (this.listKindToDisplay.ColumnsTop + this.columnheaderHeight))
                {
                    Column columnByPosition = this.GetColumnByPosition(position);
                    if (columnByPosition != null)
                    {
                        PropertyComparer comparer = new PropertyComparer(columnByPosition.Name, columnByPosition.IsNumber, columnByPosition.SmallToBig);
                        this.gameObjectList.Sort(comparer);
                        this.listKindToDisplay.ResetAllTextures();
                        columnByPosition.SmallToBig = !columnByPosition.SmallToBig;
                    }
                }
                else
                {
                    GameObject gameObjectByPosition;
                    if (this.ShowCheckBox)
                    {
                        gameObjectByPosition = this.GetGameObjectByPosition(position);
                        if (gameObjectByPosition != null)
                        {
                            if (this.listKindToDisplay.IsInEditableColumn(position))
                            {
                                if ((gameObjectByPosition.Selected || (this.SelectedItemMaxCount <= 0)) || (this.gameObjectList.GetSelectedList().Count < this.SelectedItemMaxCount))
                                {
                                    gameObjectByPosition.Selected = !gameObjectByPosition.Selected;
                                    if (this.MultiSelecting)
                                    {
                                        this.SelectingRows = true;
                                        this.SelectingBool = gameObjectByPosition.Selected;
                                        this.SelectedItemList = this.gameObjectList.GetSelectedList();
                                    }
                                    else
                                    {
                                        this.gameObjectList.SetOtherUnSelected(gameObjectByPosition);
                                    }
                                    this.OKButtonEnabled = this.gameObjectList.HasSelectedItem() || (gameObjectByPosition is Faction);
                                    this.ResetEditableTextures();
                                    if (gameObjectByPosition.Selected)
                                    {
                                        this.SelectedItem = gameObjectByPosition;
                                        if (!(this.MultiSelecting || !Setting.Current.GlobalVariables.SingleSelectionOneClick))
                                        {
                                            this.iGameFrame.OK();
                                        }
                                        else
                                        {
                                            Session.MainGame.mainGameScreen.PlayNormalSound(this.SelectSoundFile);
                                        }
                                    }
                                    else
                                    {
                                        this.SelectedItem = null;
                                    }
                                }
                            }
                            else
                            {
                                if (gameObjectByPosition is Troop)
                                {
                                    if ((this.iTroopDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iTroopDetail.SetPosition(ShowPosition.Center);
                                        this.iTroopDetail.SetTroop(gameObjectByPosition);
                                        this.iTroopDetail.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Troop).Position);
                                }
                                else if (gameObjectByPosition is Person)
                                {
                                    if ((this.iPersonDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iPersonDetail.SetPosition(ShowPosition.Center);
                                        this.iPersonDetail.SetPerson(gameObjectByPosition);
                                        this.iPersonDetail.IsShowing = true;
                                    }
                                    if (!(gameObjectByPosition as Person).IsCaptive)
                                    {
                                        Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Person).Position);
                                    }
                                }
                                else if (gameObjectByPosition is Architecture)
                                {
                                    if ((this.iArchitectureDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iArchitectureDetail.SetPosition(ShowPosition.Center);
                                        this.iArchitectureDetail.SetArchitecture(gameObjectByPosition);
                                        this.iArchitectureDetail.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Architecture).Position);
                                }
                                else if (gameObjectByPosition is Military)
                                {
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Military).Position);
                                }
                                else if (gameObjectByPosition is Faction)
                                {
                                    if ((this.iFactionTechniques != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iFactionTechniques.SetArchitecture(null);
                                        this.iFactionTechniques.SetFaction(gameObjectByPosition, false);
                                        this.iFactionTechniques.SetPosition(ShowPosition.Center);
                                        this.iFactionTechniques.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Faction).Leader.Position);
                                }
                                else if (gameObjectByPosition is Captive)
                                {
                                    if ((this.iPersonDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iPersonDetail.SetPosition(ShowPosition.Center);
                                        this.iPersonDetail.SetPerson((gameObjectByPosition as Captive).CaptivePerson);
                                        this.iPersonDetail.IsShowing = true;
                                    }
                                }
                                else if (gameObjectByPosition is Treasure)
                                {
                                    if ((this.iTreasureDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iTreasureDetail.SetPosition(ShowPosition.Center);
                                        this.iTreasureDetail.SetTreasure(gameObjectByPosition);
                                        this.iTreasureDetail.IsShowing = true;
                                    }
                                    if ((gameObjectByPosition as Treasure).BelongedPerson != null)
                                    {
                                        Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Treasure).BelongedPerson.Position);
                                    }
                                }
                                if (gameObjectByPosition != null)
                                {
                                    this.TriggerItemClick();
                                }
                            }
                        }
                    }
                    else
                    {
                        gameObjectByPosition = this.GetGameObjectByPosition(position);
                        if (gameObjectByPosition != null)
                        {
                            if (this.listKindToDisplay.SelectedTab.ListMethod != null)
                            {
                                this.PushSubKindByName(this.listKindToDisplay.SelectedTab.ListKind, StaticMethods.GetListMethodValue(gameObjectByPosition, this.listKindToDisplay.SelectedTab.ListMethod) as GameObjectList);
                            }
                            else
                            {
                                if (gameObjectByPosition is Troop)
                                {
                                    if ((this.iTroopDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iTroopDetail.SetPosition(ShowPosition.Center);
                                        this.iTroopDetail.SetTroop(gameObjectByPosition);
                                        this.iTroopDetail.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Troop).Position);
                                }
                                else if (gameObjectByPosition is Person)
                                {
                                    if ((this.iPersonDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iPersonDetail.SetPosition(ShowPosition.Center);
                                        this.iPersonDetail.SetPerson(gameObjectByPosition);
                                        this.iPersonDetail.IsShowing = true;
                                    }
                                    if (!(gameObjectByPosition as Person).IsCaptive)
                                    {
                                        Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Person).Position);
                                    }
                                }
                                else if (gameObjectByPosition is Architecture)
                                {
                                    if ((this.iArchitectureDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iArchitectureDetail.SetPosition(ShowPosition.Center);
                                        this.iArchitectureDetail.SetArchitecture(gameObjectByPosition);
                                        this.iArchitectureDetail.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Architecture).Position);
                                }
                                else if (gameObjectByPosition is Military)
                                {
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Military).Position);
                                }
                                else if (gameObjectByPosition is Faction)
                                {
                                    if ((this.iFactionTechniques != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iFactionTechniques.SetArchitecture(null);
                                        this.iFactionTechniques.SetFaction(gameObjectByPosition, false);
                                        this.iFactionTechniques.SetPosition(ShowPosition.Center);
                                        this.iFactionTechniques.IsShowing = true;
                                    }
                                    Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Faction).Leader.Position);
                                }
                                else if (gameObjectByPosition is Captive)
                                {
                                    if ((this.iPersonDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iPersonDetail.SetPosition(ShowPosition.Center);
                                        this.iPersonDetail.SetPerson((gameObjectByPosition as Captive).CaptivePerson);
                                        this.iPersonDetail.IsShowing = true;
                                    }
                                }
                                else if (gameObjectByPosition is Treasure)
                                {
                                    if ((this.iTreasureDetail != null) && (this.Function != FrameFunction.Jump))
                                    {
                                        this.iTreasureDetail.SetPosition(ShowPosition.Center);
                                        this.iTreasureDetail.SetTreasure(gameObjectByPosition);
                                        this.iTreasureDetail.IsShowing = true;
                                    }
                                    if ((gameObjectByPosition as Treasure).BelongedPerson != null)
                                    {
                                        Session.MainGame.mainGameScreen.JumpTo((gameObjectByPosition as Treasure).BelongedPerson.Position);
                                    }
                                }
                                if (gameObjectByPosition != null)
                                {
                                    this.TriggerItemClick();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (!this.xianshiyoucelan) return;
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None)
            {
                this.MovingHorizontalScrollBar = false;
                this.MovingVerticalScrollBar = false;
                this.SelectingRows = false;
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (!this.xianshiyoucelan) return;
            if ((Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None) && (this.oldMousePosition != position))
            {
                if (leftDown)
                {
                    if (this.ShowCheckBox && ((this.MultiSelecting && !this.MovingHorizontalScrollBar) && !this.MovingVerticalScrollBar))
                    {
                        GameObject gameObjectByPosition = this.GetGameObjectByPosition(position);
                        if (gameObjectByPosition != null)
                        {
                            if (!this.SelectingRows)
                            {
                                this.SelectingRows = true;
                                this.SelectingBool = gameObjectByPosition.Selected;
                            }
                            if (this.SelectingRows)
                            {
                                if (this.SelectingBool)
                                {
                                    if ((this.SelectedItemMaxCount <= 0) || (this.gameObjectList.GetSelectedList().Count < this.SelectedItemMaxCount))
                                    {
                                        gameObjectByPosition.Selected = this.SelectingBool;
                                        this.ResetEditableTextures();
                                    }
                                }
                                else
                                {
                                    gameObjectByPosition.Selected = this.SelectingBool;
                                    this.ResetEditableTextures();
                                }
                            }
                            this.OKButtonEnabled = this.gameObjectList.HasSelectedItem();
                            this.SelectedItemList = this.gameObjectList.GetSelectedList();
                        }
                    }
                    if (this.ShowHorizontalScrollBar && (this.MovingHorizontalScrollBar || StaticMethods.PointInRectangle(position, this.listKindToDisplay.HorizontalScrollBar)))
                    {
                        this.listKindToDisplay.MoveHorizontal(position.X - this.oldMousePosition.X);
                        this.MovingHorizontalScrollBar = true;
                    }
                    if (this.ShowVerticalScrollBar && (this.MovingVerticalScrollBar || StaticMethods.PointInRectangle(position, this.listKindToDisplay.VerticalScrollBar)))
                    {
                        this.listKindToDisplay.MoveVertical(position.Y - this.oldMousePosition.Y);
                        this.MovingVerticalScrollBar = true;
                    }
                }
                else
                {
                    int rowTopByPosition = this.GetRowTopByPosition(position);
                    if (rowTopByPosition >= 0)
                    {
                        this.Focused = rowTopByPosition;
                        this.FocusedObject = this.GetGameObjectByPosition(position);
                        this.DrawFocused = true;
                    }
                    else
                    {
                        this.DrawFocused = false;
                    }
                }
                this.oldMousePosition = position;
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None)
            {
                this.PopSubKind();
            }
        }

        private void screen_OnMouseScroll(Point position, int scrollValue)
        {
            if (((Session.MainGame.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None) && (!this.MovingHorizontalScrollBar && !this.MovingVerticalScrollBar)) && (this.listKindToDisplay != null))
            {
                if (this.ShowVerticalScrollBar)
                {
                    this.listKindToDisplay.MoveVertical((this.oldScrollValue - scrollValue) / 6);
                }
                else if (this.ShowHorizontalScrollBar)
                {
                    this.listKindToDisplay.MoveHorizontal((scrollValue - this.oldScrollValue) / 6);
                }
                this.oldScrollValue = scrollValue;
            }
        }

        private void SelectDefaultTab()
        {
            if (((this.listKindToDisplay != null) && (this.listKindToDisplay.Tabs.Count > 0)) && !this.listKindToDisplay.HasSelectedTab)
            {
                this.listKindToDisplay.Tabs[0].Selected = true;
            }
        }

        public void SetListKindByID(int ID, bool showCheckBox, bool multiSelecting)
        {
            this.listKindToDisplay = this.GetListKindByID(ID);
            this.MultiSelecting = multiSelecting;
            if (this.listKindToDisplay != null)
            {
                this.SetShowCheckBox(showCheckBox);
            }
        }

        public void SetListKindByName(string Name, bool showCheckBox, bool multiSelecting)
        {
            this.listKindToDisplay = this.GetListKindByName(Name);
            this.MultiSelecting = multiSelecting;
            if (this.listKindToDisplay != null)
            {
                this.SetShowCheckBox(showCheckBox);
            }
        }

        public void SetObjectList(GameObjectList gameObjectList)
        {
            this.ClearData();
            this.gameObjectList = gameObjectList;
            foreach (GameObject obj2 in gameObjectList)
            {
                //obj2.Selected = false;  不能影响建筑连接的改动，建筑列表插件和建筑是否被选中无关
            }
            this.FullLowerClient.Height = (gameObjectList.Count * this.rowHeight) + this.columnheaderHeight;
        }

        private void SetSelectedObjectList(GameObjectList selectedObjectList)
        {
            if (selectedObjectList != null)
            {
                foreach (GameObject obj2 in selectedObjectList)
                {
                    obj2.Selected = true;
                }
            }
        }

        public void SetSelectedTab(string tabName)
        {
            if ((this.listKindToDisplay != null) && (this.listKindToDisplay.Tabs.Count > 0))
            {
                this.listKindToDisplay.SetSelectedTab(tabName);
            }
        }

        private void SetShowCheckBox(bool show)
        {
            this.ShowCheckBox = show;
            if (this.listKindToDisplay != null)
            {
                if (show)
                {
                    this.listKindToDisplay.AddCheckBoxColumn();
                }
                else
                {
                    this.listKindToDisplay.RemoveCheckBoxColumn();
                }
            }
        }

        public void ShrinkRectanglesHeight()
        {
            if (this.HeightCanShrink)
            {
                this.VisibleLowerClient = new Rectangle(this.VisibleLowerClient.X, this.VisibleLowerClient.Y, this.VisibleLowerClient.Width, (this.VisibleLowerClient.Height - (2 * this.scrolltrackWidth)) - this.scrollbuttonWidth);
                this.HeightCanShrink = false;
            }
        }

        public void ShrinkRectanglesWidth()
        {
            if (this.WidthCanShrink)
            {
                this.VisibleLowerClient = new Rectangle(this.VisibleLowerClient.X, this.VisibleLowerClient.Y, (this.VisibleLowerClient.Width - (2 * this.scrolltrackWidth)) - this.scrollbuttonWidth, this.VisibleLowerClient.Height);
                for (int i = 0; i < this.RowRectangles.Count; i++)
                {
                    this.RowRectangles[i] = new Rectangle(this.RowRectangles[i].X, this.RowRectangles[i].Y, (this.RowRectangles[i].Width - (2 * this.scrolltrackWidth)) - this.scrollbuttonWidth, this.RowRectangles[i].Height);
                }
                this.WidthCanShrink = false;
            }
        }

        public override bool CanClose
        {
            get
            {
                return this.RightClickClose;
            }
        }

        public override bool IsShowing
        {
            get

            {/*
                bool isShowing = base.isShowing;
                if (isShowing && (this.iPersonDetail != null))
                {
                    isShowing = !this.iPersonDetail.IsShowing;
                }
                if (isShowing && (this.iTroopDetail != null))
                {
                    isShowing = !this.iTroopDetail.IsShowing;
                }
                if (isShowing && (this.iArchitectureDetail != null))
                {
                    isShowing = !this.iArchitectureDetail.IsShowing;
                }
                if (isShowing && (this.iFactionTechniques != null))
                {
                    isShowing = !this.iFactionTechniques.IsShowing;
                }
                return isShowing;
              */

                return this.jiancexianshi ;
            }
            set
            {
                SetMouseEvent(Session.MainGame.mainGameScreen, value);
            }
        }

        public void SetMouseEvent(Screen screen, bool value)
        {
            if (this.jiancexianshi != value)
            {
                //base.isShowing = value;
                this.jiancexianshi = value;
                if (value)
                {
                    //Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.None, UndoneWorkSubKind.None));
                    //this.frameContent.IsShowing = true;
                }
                else
                {
                    //if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.None )
                    {
                        //throw new Exception("The UndoneWork is not a Frame.");  //错误检查
                    }
                    //this.frameContent.IsShowing = false;

                    this.SelectedItemMaxCount = 0;
                    this.OKFunction = null;
                }

            }

            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            //screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
            screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftDown);
            screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
            screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
            //screen.OnMouseScroll += new Screen.MouseScroll(this.screen_OnMouseScroll);
        }

        public override bool MapViewSelectorButtonEnabled
        {
            get
            {
                return ((((this.iMapViewSelector != null) && (this.gameObjectList.Count > 0)) && (this.gameObjectList[0] is Architecture)) && this.ShowCheckBox);
            }
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        internal struct SubKind
        {
            internal ListKind Kind;
            internal GameObjectList List;
        }
    }
}

