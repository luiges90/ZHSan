using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Xml;


namespace ContextMenuPlugin
{

    public class ContextMenu
    {
        public Vector2 Scale = new Vector2(1.3f, 1.3f);

        public string ClickSoundFile;
        public object CurrentGameObject;
        public int CurrentParamID;
        public string FoldSoundFile;
        public PlatformTexture HasChildTexture;
        public IHelp HelpPlugin;
        private bool isShowing;
        public const int ItemMoveMargin = 10;
        public ContextMenuKind Kind;
        public int left;

        public Font LeftClickFreeTextBuilder = new Font();
        //public FreeTextBuilder LeftClickFreeTextBuilder = new FreeTextBuilder();

        public PlatformTexture LeftClickItemSelectedTexture;
        public PlatformTexture LeftClickItemTexture;
        public Color LeftClickTextColor;
        public List<MenuKind> MenuKinds = new List<MenuKind>();
        private MenuKind menuToDisplay;
        public string OpenSoundFile;
        public ContextMenuResult Result;

        public Font RightClickFreeTextBuilder = new Font();
        //public FreeTextBuilder RightClickFreeTextBuilder = new FreeTextBuilder();

        public PlatformTexture RightClickItemSelectedTexture;
        public PlatformTexture RightClickItemTexture;
        public Color RightClickTextColor;

        public Font DisabledFreeTextBuilder = new Font();
        //public FreeTextBuilder DisabledFreeTextBuilder = new FreeTextBuilder();

        public PlatformTexture DisabledItemSelectedTexture;
        public PlatformTexture DisabledItemTexture;
        public Color DisabledTextColor;

        public Font RightDisabledFreeTextBuilder = new Font();
        //public FreeTextBuilder RightDisabledFreeTextBuilder = new FreeTextBuilder();

        public PlatformTexture RightDisabledItemSelectedTexture;
        public PlatformTexture RightDisabledItemTexture;
        public Color RightDisabledTextColor;
        
        public int top;
        public Point ViewportSize;
        public bool BianduiLiebiaoXianshi=false ;
        public Rectangle BianduiLiebiaoWeizhi=new Rectangle (0,0,0,0);


        public void Draw()
        {
            CacheManager.Scale = Scale;
            if (this.menuToDisplay != null)
            {
                this.menuToDisplay.Draw();
            }
            CacheManager.Scale = Vector2.One;
        }

        private MenuKind GetMenuKindByID(int ID)
        {
            foreach (MenuKind kind in this.MenuKinds)
            {
                if (kind.ID == ID)
                {
                    return kind;
                }
            }
            return null;
        }

        private MenuKind GetMenuKindByName(string Name)
        {
            foreach (MenuKind kind in this.MenuKinds)
            {
                if (kind.Name == Name)
                {
                    return kind;
                }
            }
            return null;
        }

        public void Initialize()
        {
            if (Session.LargeContextMenu)
            {
                Scale = new Vector2(1.3f, 1.3f);
            }
            else
            {
                Scale = Vector2.One;
            }   
        }

        public void LoadFromXmlNode(XmlNode rootNode)
        {
            foreach (XmlNode node in rootNode)
            {
                MenuKind item = new MenuKind(this);
                item.LoadFromXmlNode(node);
                this.MenuKinds.Add(item);
            }
        }

        private void Prepare()
        {
            this.Result = ContextMenuResult.None;
            this.menuToDisplay.Prepare();
        }

        public void Prepare(int Left, int Top, Point viewportSize)
        {
            if (this.menuToDisplay != null)
            {
                this.ViewportSize = viewportSize;
                Rectangle rect = new Rectangle(Left, Top, this.menuToDisplay.ItemWidth, this.Height);
                StaticMethods.AdjustRectangleInViewport(ref rect, viewportSize);
                this.left = rect.Left;
                this.top = rect.Top;
                if (this.top < 0)
                {
                    this.top = 0;
                }
                this.Prepare();
            }
        }

        public void RefreshAllItemsDisplayName()
        {
            foreach (MenuItem item in this.menuToDisplay.MenuItems)
            {
                item.RefreshItemDisplayName();
            }
        }

        public void RefreshAllItemsVisible()
        {
            if ((this.menuToDisplay.DisplayIfTrue != null) && !StaticMethods.GetBoolMethodValue(this.CurrentGameObject, this.menuToDisplay.DisplayIfTrue, new object[0]))
            {
                foreach (MenuItem item in this.menuToDisplay.MenuItems)
                {
                    item.Visible = false;
                }
            }
            else
            {
                foreach (MenuItem item in this.menuToDisplay.MenuItems)
                {
                    item.RefreshItemVisible();
                }
            }
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (this.menuToDisplay != null)
            {
                CacheManager.Scale = Scale;
                /*if (this.BianduiLiebiaoXianshi == true && StaticMethods.PointInRectangle(position, this.BianduiLiebiaoWeizhi)) //光标在编队列表里点击时不关闭菜单
                {

                }*/

                if ((this.HelpPlugin != null) && (this.HelpPlugin.IsButtonShowing && StaticMethods.PointInRectangle(position, this.HelpPlugin.ButtonDisplayPosition)))
                {
                    this.Result = ContextMenuResult.None;
                    this.IsShowing = false;
                    this.HelpPlugin.IsButtonShowing = false;
                    this.HelpPlugin.IsShowing = true;
                }
                else
                {
                    MenuItem itemByPosition = this.menuToDisplay.GetItemByPosition(position);
                    if (itemByPosition != null && itemByPosition.Enabled)
                    {
                        bool open = itemByPosition.Open;
                        itemByPosition.Open = !itemByPosition.Open;
                        if (open || itemByPosition.Open )
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound(this.OpenSoundFile);
                            this.Result = ContextMenuResult.KeepShowing;
                        }
                        else if (itemByPosition.Name == "减小音量")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound(this.ClickSoundFile);
                            Session.MainGame.mainGameScreen.ReduceSound();
                        }
                        else if (itemByPosition.Name == "增加音量")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound(this.ClickSoundFile);
                            Session.MainGame.mainGameScreen.IncreaseSound();
                        }
                        else if (itemByPosition.Name == "返回初始菜单")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound(this.ClickSoundFile);
                            Session.MainGame.mainGameScreen.ReturnMainMenu();
                        }

                        else
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound(this.ClickSoundFile);
                            if (itemByPosition.IsParamIDItem)
                            {
                                this.CurrentParamID = int.Parse(itemByPosition.Param);
                                this.Result = StaticMethods.GetContextMenuResultByName(itemByPosition.ResultDecludeParam);
                            }
                            else
                            {
                                this.Result = StaticMethods.GetContextMenuResultByName(itemByPosition.Result);
                            }
                            this.IsShowing = false;
                        }
                    }
                    else if (!this.menuToDisplay.HasOpenItem)
                    {
                        Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                        this.Result = ContextMenuResult.None;
                        this.IsShowing = false;
                    }
                    else
                    {
                        Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                        this.menuToDisplay.FoldOpenedItem();
                        this.Result = ContextMenuResult.KeepShowing;
                    }
                }
                CacheManager.Scale = Vector2.One;
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.menuToDisplay != null)
            {
                CacheManager.Scale = Scale;
                MenuItem itemByPosition = this.menuToDisplay.GetItemByPosition(position);
                if (itemByPosition != null)
                {
                    itemByPosition.Selected = true;
                }
                else
                {
                    this.menuToDisplay.ResetAllItemsSelected();
                }

                CacheManager.Scale = Vector2.One;
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            if (!this.menuToDisplay.IsLeft)
            {
                if (!this.menuToDisplay.HasOpenItem)
                {
                    Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                    this.Result = ContextMenuResult.None;
                    this.IsShowing = false;
                }
            }
            else if (this.menuToDisplay.HasOpenItem)
            {
                Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                this.menuToDisplay.FoldOpenedItem();
                this.Result = ContextMenuResult.KeepShowing;
            }
            else
            {
                Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                this.Result = ContextMenuResult.None;
                this.IsShowing = false;
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (!this.menuToDisplay.IsLeft && this.menuToDisplay.HasOpenItem)
            {
                Session.MainGame.mainGameScreen.PlayNormalSound(this.FoldSoundFile);
                this.menuToDisplay.FoldOpenedItem();
                this.Result = ContextMenuResult.KeepShowing;
            }
        }

        public void SetMenuKindByID(int ID)
        {
            this.menuToDisplay = this.GetMenuKindByID(ID);
            if (this.menuToDisplay != null)
            {
                this.RefreshAllItemsVisible();
                if (this.menuToDisplay.VisibleCount == 0)
                {
                    this.Result = ContextMenuResult.None;
                    this.IsShowing = false;
                }
                this.RefreshAllItemsDisplayName();
                if (this.HelpPlugin != null)
                {
                    this.HelpPlugin.SetButtonSize(new Point(this.menuToDisplay.ItemHeight, this.menuToDisplay.ItemHeight));
                }
            }
        }

        public void SetMenuKindByName(string Name)
        {
            this.menuToDisplay = this.GetMenuKindByName(Name);
            if (this.menuToDisplay != null)
            {
                this.RefreshAllItemsVisible();
                if (this.menuToDisplay.VisibleCount == 0)
                {
                    this.Result = ContextMenuResult.None;
                    this.IsShowing = false;
                }
                this.RefreshAllItemsDisplayName();
                if (this.HelpPlugin != null)
                {
                    this.HelpPlugin.SetButtonSize(new Point(this.menuToDisplay.ItemHeight, this.menuToDisplay.ItemHeight));
                }
            }
        }

        public int Height
        {
            get
            {
                if (this.menuToDisplay != null)
                {
                    return (this.menuToDisplay.VisibleCount * this.menuToDisplay.ItemHeight);
                }
                return 0;
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
                if (this.IsShowing != value)
                {
                    this.isShowing = value;
                    if (value )
                    {
                        Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.ContextMenu, UndoneWorkSubKind.None));
                        Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        Session.MainGame.mainGameScreen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        Session.MainGame.mainGameScreen.PlayNormalSound(this.OpenSoundFile);
                    }
                    else
                    {
                        if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.ContextMenu)
                        {
                            throw new Exception("The UndoneWork is not a ContextMenu.");
                        }
                        Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        Session.MainGame.mainGameScreen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        if (this.HelpPlugin != null)
                        {
                            this.HelpPlugin.IsButtonShowing = false;
                        }
                    }
                }
            }
        }

        public Rectangle Position
        {
            get
            {
                if (this.menuToDisplay != null)
                {
                    return new Rectangle(this.left, this.top, this.menuToDisplay.ItemWidth, this.Height);
                }
                return Rectangle.Empty;
            }
        }
    }
}

