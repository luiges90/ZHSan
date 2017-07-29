using GameGlobal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;


namespace ContextMenuPlugin
{

    public class MenuKind
    {
        public ContextMenu contextMenu;
        public string DisplayIfTrue;
        public string DisplayName;
        public int ID;
        public bool IsLeft;
        public int ItemHeight;
        public int ItemWidth;
        public List<MenuItem> MenuItems;
        public string Name;
        public bool ShowLeft = false;
        public bool ShowTop = false;
        public bool DisplayAll = true;

        public MenuKind(ContextMenu contextMenu)
        {
            this.contextMenu = contextMenu;
            this.MenuItems = new List<MenuItem>();
        }

        public void Draw()
        {
            foreach (MenuItem item in this.MenuItems)
            {
                item.Draw();
            }
            if (this.contextMenu.HelpPlugin != null)
            {
                this.contextMenu.HelpPlugin.DrawButton(0.04f);
            }
        }

        public void FoldOpenedItem()
        {
            foreach (MenuItem item in this.MenuItems)
            {
                if (item.Open)
                {
                    item.Open = false;
                    break;
                }
            }
        }

        public MenuItem GetItemByPosition(Point position)
        {
            for (int i = 0; i < this.MenuItems.Count; i++)
            {
                if (this.MenuItems[i].Visible)
                {
                    if (StaticMethods.PointInRectangle(position, this.MenuItems[i].Position))
                    {
                        return this.MenuItems[i];
                    }
                    if (this.MenuItems[i].Open)
                    {
                        MenuItem itemByPosition = this.MenuItems[i].GetItemByPosition(position);
                        if (itemByPosition != null)
                        {
                            return itemByPosition;
                        }
                    }
                }
            }
            return null;
        }

        public void LoadFromXmlNode(XmlNode rootNode)
        {
            this.ID = int.Parse(rootNode.Attributes.GetNamedItem("ID").Value);
            this.Name = rootNode.Attributes.GetNamedItem("Name").Value;
            this.DisplayName = rootNode.Attributes.GetNamedItem("DisplayName").Value;
            this.IsLeft = bool.Parse(rootNode.Attributes.GetNamedItem("IsLeft").Value);
            this.ItemWidth = int.Parse(rootNode.Attributes.GetNamedItem("Width").Value);
            this.ItemHeight = int.Parse(rootNode.Attributes.GetNamedItem("Height").Value);
            if (rootNode.Attributes.GetNamedItem("DisplayIfTrue") != null)
            {
                this.DisplayIfTrue = rootNode.Attributes.GetNamedItem("DisplayIfTrue").Value;
            }
            if (rootNode.Attributes.GetNamedItem("DisplayAll") != null)
            {
                this.DisplayAll = bool.Parse(rootNode.Attributes.GetNamedItem("DisplayAll").Value);
            }
            foreach (XmlNode node in rootNode)
            {
                MenuItem item = new MenuItem(this, this.contextMenu);
                item.IsRootItem = true;
                item.LoadFromXmlNode(node);
                this.MenuItems.Add(item);
            }
        }

        public void Prepare()
        {
            this.ShowLeft = false;
            this.ShowTop = false;
            this.ResetAllItemsSelected();
            this.ResetAllItemsOpen();
            this.RePrepare();
        }

        public void RePrepare()
        {
            for (int i = 0; i < this.MenuItems.Count; i++)
            {
                if (this.MenuItems[i].Visible)
                {
                    this.MenuItems[i].Prepare();
                }
            }
        }

        public void ResetAllItemsOpen()
        {
            foreach (MenuItem item in this.MenuItems)
            {
                item.ResetOpen();
                item.ResetAllItemsOpen();
            }
        }

        public void ResetAllItemsSelected()
        {
            foreach (MenuItem item in this.MenuItems)
            {
                item.ResetSelected();
                if (item.Open)
                {
                    item.ResetAllItemsSelected();
                }
            }
        }

        public void ResetOtherOpen(MenuItem menuItem)
        {
            foreach (MenuItem item in this.MenuItems)
            {
                if (item == menuItem)
                {
                    item.ResetAllItemsOpen();
                }
                else
                {
                    item.ResetOpen();
                    item.ResetAllItemsOpen();
                }
            }
        }

        public void ResetOtherSelected(MenuItem menuItem)
        {
            foreach (MenuItem item in this.MenuItems)
            {
                if (item == menuItem)
                {
                    item.ResetAllItemsSelected();
                }
                else
                {
                    item.ResetSelected();
                    item.ResetAllItemsSelected();
                }
            }
        }

        public void SetAllItemsVisible(bool visible)
        {
            foreach (MenuItem item in this.MenuItems)
            {
                item.Visible = visible;
                item.SetAllItemsVisible(visible);
            }
        }

        public bool HasOpenItem
        {
            get
            {
                foreach (MenuItem item in this.MenuItems)
                {
                    if (item.Open)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int VisibleCount
        {
            get
            {
                int num = 0;
                foreach (MenuItem item in this.MenuItems)
                {
                    if (item.Visible)
                    {
                        num++;
                    }
                }
                return num;
            }
        }
    }
}

