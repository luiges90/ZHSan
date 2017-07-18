using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Xml;


namespace ContextMenuPlugin
{

    public class ContextMenu
    {
        public string ClickSoundFile;
        public object CurrentGameObject;
        public int CurrentParamID;
        public string FoldSoundFile;
        public Texture2D HasChildTexture;
        public IHelp HelpPlugin;
        private bool isShowing;
        public const int ItemMoveMargin = 10;
        public ContextMenuKind Kind;
        public int left;

        public Font LeftClickFreeTextBuilder = new Font();
        //public FreeTextBuilder LeftClickFreeTextBuilder = new FreeTextBuilder();

        public Texture2D LeftClickItemSelectedTexture;
        public Texture2D LeftClickItemTexture;
        public Color LeftClickTextColor;
        public List<MenuKind> MenuKinds = new List<MenuKind>();
        private MenuKind menuToDisplay;
        public string OpenSoundFile;
        public ContextMenuResult Result;

        public Font RightClickFreeTextBuilder = new Font();
        //public FreeTextBuilder RightClickFreeTextBuilder = new FreeTextBuilder();

        public Texture2D RightClickItemSelectedTexture;
        public Texture2D RightClickItemTexture;
        public Color RightClickTextColor;

        public Font DisabledFreeTextBuilder = new Font();
        //public FreeTextBuilder DisabledFreeTextBuilder = new FreeTextBuilder();

        public Texture2D DisabledItemSelectedTexture;
        public Texture2D DisabledItemTexture;
        public Color DisabledTextColor;

        public Font RightDisabledFreeTextBuilder = new Font();
        //public FreeTextBuilder RightDisabledFreeTextBuilder = new FreeTextBuilder();

        public Texture2D RightDisabledItemSelectedTexture;
        public Texture2D RightDisabledItemTexture;
        public Color RightDisabledTextColor;
        private Screen screen;
        public int top;
        public Point ViewportSize;
        public bool BianduiLiebiaoXianshi=false ;
        public Rectangle BianduiLiebiaoWeizhi=new Rectangle (0,0,0,0);


        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.menuToDisplay != null)
            {
                this.menuToDisplay.Draw(spriteBatch);
            }
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

        public void Initialize(Screen screen)
        {
            this.screen = screen;
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
                            this.screen.PlayNormalSound(this.OpenSoundFile);
                            this.Result = ContextMenuResult.KeepShowing;
                        }
                        else if (itemByPosition.Name == "减小音量")
                        {
                            this.screen.PlayNormalSound(this.ClickSoundFile);
                            this.screen.减小音量();
                        }
                        else if (itemByPosition.Name == "增加音量")
                        {
                            this.screen.PlayNormalSound(this.ClickSoundFile);
                            this.screen.增加音量();
                        }
                        else if (itemByPosition.Name == "返回初始菜单")
                        {
                            this.screen.PlayNormalSound(this.ClickSoundFile);
                            this.screen.返回初始菜单();
                        }

                        else
                        {
                            this.screen.PlayNormalSound(this.ClickSoundFile);
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
                        this.screen.PlayNormalSound(this.FoldSoundFile);
                        this.Result = ContextMenuResult.None;
                        this.IsShowing = false;
                    }
                    else
                    {
                        this.screen.PlayNormalSound(this.FoldSoundFile);
                        this.menuToDisplay.FoldOpenedItem();
                        this.Result = ContextMenuResult.KeepShowing;
                    }
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.menuToDisplay != null)
            {
                MenuItem itemByPosition = this.menuToDisplay.GetItemByPosition(position);
                if (itemByPosition != null)
                {
                    itemByPosition.Selected = true;
                }
                else
                {
                    this.menuToDisplay.ResetAllItemsSelected();
                }
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            if (!this.menuToDisplay.IsLeft)
            {
                if (!this.menuToDisplay.HasOpenItem)
                {
                    this.screen.PlayNormalSound(this.FoldSoundFile);
                    this.Result = ContextMenuResult.None;
                    this.IsShowing = false;
                }
            }
            else if (this.menuToDisplay.HasOpenItem)
            {
                this.screen.PlayNormalSound(this.FoldSoundFile);
                this.menuToDisplay.FoldOpenedItem();
                this.Result = ContextMenuResult.KeepShowing;
            }
            else
            {
                this.screen.PlayNormalSound(this.FoldSoundFile);
                this.Result = ContextMenuResult.None;
                this.IsShowing = false;
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (!this.menuToDisplay.IsLeft && this.menuToDisplay.HasOpenItem)
            {
                this.screen.PlayNormalSound(this.FoldSoundFile);
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
                        this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.ContextMenu, UndoneWorkSubKind.None));
                        this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        this.screen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        this.screen.PlayNormalSound(this.OpenSoundFile);
                    }
                    else
                    {
                        if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.ContextMenu)
                        {
                            throw new Exception("The UndoneWork is not a ContextMenu.");
                        }
                        this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                        this.screen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
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

