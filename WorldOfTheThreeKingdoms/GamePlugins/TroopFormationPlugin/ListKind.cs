using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Xml;
using System.Linq;


namespace TroopFormationPlugin
{
    public class ListKind
    {
        public List<Column> AllColumns;
        private int columnsTop;
        internal string DisplayName;
        internal Microsoft.Xna.Framework.Rectangle HorizontalScrollBar;
        internal int HorizontalScrollTrackLength;
        internal int ID;
        internal Microsoft.Xna.Framework.Rectangle LeftScrollTrack;
        internal Microsoft.Xna.Framework.Rectangle LowerScrollTrack;
        internal string Name;
        internal Microsoft.Xna.Framework.Rectangle RightScrollTrack;
        internal Tab SelectedTab;
        internal bool ShowPortrait;
        private TabListInFrame tabList;
        private int tabMargin;
        public List<Tab> Tabs;
        internal Microsoft.Xna.Framework.Rectangle UpperScrollTrack;
        internal Microsoft.Xna.Framework.Rectangle VerticalScrollBar;
        internal int VerticalScrollTrackLength;

        internal ListKind(TabListInFrame tabList)
        {
            this.tabList = tabList;
            this.AllColumns = new List<Column>();
            this.Tabs = new List<Tab>();
        }

        internal void AddCheckBoxColumn()
        {
            Column item = new Column(this.tabList) {
                ID = 0,
                Name = this.tabList.checkboxName,
                IsNumber = false,
                SmallToBig = false,
                DisplayName = this.tabList.checkboxDisplayName,
                MinWidth = this.tabList.checkboxWidth,
                Editable = true,
                ColumnTextList = new FreeTextList()
            };
            item.ColumnTextList.Align = TextAlign.Middle;
            item.Text.Text = this.tabList.checkboxDisplayName;
            item.Text.Position = new Microsoft.Xna.Framework.Rectangle(item.Text.Position.X, item.Text.Position.Y, this.tabList.checkboxWidth, item.Text.Position.Height);
            this.AllColumns.Add(item);
            foreach (Tab tab in this.Tabs)
            {
                if (!tab.Columns[0].Editable)
                {
                    tab.Columns.Insert(0, item);
                    tab.ReCalculate(tab.CurrentYOffset);
                }
            }
        }

        public void ClearData()
        {
            foreach (Column column in this.AllColumns)
            {
                column.ClearData();
            }
        }

        public void Draw()
        {
            Microsoft.Xna.Framework.Rectangle? nullable;
            foreach (Tab tab in this.Tabs)
            {
                tab.Draw();
            }
            if ((((this.tabList.DrawFocused && !this.tabList.MovingHorizontalScrollBar) && !this.tabList.MovingVerticalScrollBar) && (this.SelectedTab != null)) && ((this.tabList.Focused >= (this.tabList.VisibleLowerClient.Top + this.tabList.columnheaderHeight)) && ((this.tabList.Focused + this.tabList.rowHeight) <= this.tabList.VisibleLowerClient.Bottom)))
            {
                nullable = null;
                CacheManager.Draw(this.tabList.focusTrackTexture, new Microsoft.Xna.Framework.Rectangle(this.tabList.VisibleLowerClient.Left, this.tabList.Focused, this.tabList.VisibleLowerClient.Width - 1, 1), nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.focusTrackTexture, new Microsoft.Xna.Framework.Rectangle(this.tabList.VisibleLowerClient.Left, this.tabList.Focused, 1, this.tabList.rowHeight), nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.focusTrackTexture, new Microsoft.Xna.Framework.Rectangle(this.tabList.VisibleLowerClient.Left, (this.tabList.Focused + this.tabList.rowHeight) - 1, this.tabList.VisibleLowerClient.Width - 1, 1), nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.focusTrackTexture, new Microsoft.Xna.Framework.Rectangle(this.tabList.VisibleLowerClient.Right - 1, this.tabList.Focused, 1, this.tabList.rowHeight), nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                if (this.ShowPortrait)
                {
                    Person person = null;
                    var rec = new Microsoft.Xna.Framework.Rectangle(this.tabList.VisibleLowerClient.Left - this.tabList.PortraitWidth, this.tabList.Focused, this.tabList.PortraitWidth, this.tabList.PortraitHeight);
                    var depth = 0.03498f;
                    if (this.tabList.FocusedObject is Person)
                    {
                        person = this.tabList.FocusedObject as Person;
                    }
                    else if (this.tabList.FocusedObject is Captive)
                    {
                        person = (this.tabList.FocusedObject as Captive).CaptiveCharacter;
                    }
                    else if (this.tabList.FocusedObject is Faction)
                    {
                        person = (this.tabList.FocusedObject as Faction).Leader;
                    }
                    else if (this.tabList.FocusedObject is Troop)
                    {
                        person = (this.tabList.FocusedObject as Troop).Leader;
                    }
                    else if (!(this.tabList.FocusedObject is Architecture))
                    {
                        if (this.tabList.FocusedObject is Military)
                        {
                            if ((this.tabList.FocusedObject as Military).Leader != null)
                            {
                                person = (this.tabList.FocusedObject as Military).Leader;
                            }
                        }
                        else if (this.tabList.FocusedObject is Treasure)
                        {
                        }
                    }
                    CacheManager.DrawZhsanAvatar(person, "s", rec, Color.White, depth);
                }
            }
            if (this.tabList.ShowVerticalScrollBar)
            {
                nullable = null;
                CacheManager.Draw(this.tabList.scrolltrackTexture, this.LeftScrollTrack, nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.scrolltrackTexture, this.RightScrollTrack, nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.scrollbuttonTexture, this.VerticalScrollBar, nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
            }
            if (this.tabList.ShowHorizontalScrollBar)
            {
                nullable = null;
                CacheManager.Draw(this.tabList.scrolltrackTexture, this.UpperScrollTrack, nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                nullable = null;
                CacheManager.Draw(this.tabList.scrolltrackTexture, this.LowerScrollTrack, nullable, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
                CacheManager.Draw(this.tabList.scrollbuttonTexture, this.HorizontalScrollBar, null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.3498f);
            }
        }

        public Column GetColumnByID(int ID)
        {
            return this.AllColumns.FirstOrDefault(column => column.ID == ID);
            //foreach (Column column in this.AllColumns)
            //{
            //    if (column.ID == ID)
            //    {
            //        return column;
            //    }
            //}
            //return null;
        }

        public Tab GetTabByID(int ID)
        {
            foreach (Tab tab in this.Tabs)
            {
                if (tab.ID == ID)
                {
                    return tab;
                }
            }
            return null;
        }

        internal bool IsInEditableColumn(Microsoft.Xna.Framework.Point point)
        {
            foreach (Column column in this.SelectedTab.Columns)
            {
                if (column.Editable)
                {
                    for (int i = 0; i < this.tabList.gameObjectList.Count; i++)
                    {
                        if (StaticMethods.PointInRectangle(point, column.ColumnTextList.DisplayPosition(i)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void LoadFromXMLNode(XmlNode rootNode)
        {
            XmlNode node = rootNode.ChildNodes.Item(0);
            foreach (XmlNode node2 in node.ChildNodes)
            {
                Font font;
                Microsoft.Xna.Framework.Color color;
                Column column;
                column = new Column(this.tabList) {   //读取列
                    ID = int.Parse(node2.Attributes.GetNamedItem("ID").Value),
                    Name = node2.Attributes.GetNamedItem("Name").Value,
                    IsNumber = bool.Parse(node2.Attributes.GetNamedItem("IsNumber").Value),
                    DisplayName = node2.Attributes.GetNamedItem("DisplayName").Value,
                    MinWidth = int.Parse(node2.Attributes.GetNamedItem("MinWidth").Value),
                    //SmallToBig = !column.IsNumber
                    SmallToBig=true
                    //SmallToBig = !(bool.Parse(node2.Attributes.GetNamedItem("IsNumber").Value))

                };
                //column.SmallToBig = !column.IsNumber;   //我添加的

                StaticMethods.LoadFontAndColorFromXMLNode(node2, out font, out color);
                column.ColumnTextList = new FreeTextList(font);
                column.ColumnTextList.TextColor = color;
                //column.ColumnTextList.TextColor =new Microsoft.Xna.Framework.Color (0.5f,0.5f,0.5f);
                column.ColumnTextList.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node2.Attributes.GetNamedItem("Align").Value);
                column.Text.Text = column.DisplayName;
                
                this.AllColumns.Add(column);
            }
            node = rootNode.ChildNodes.Item(1);
            this.tabMargin = int.Parse(node.Attributes.GetNamedItem("Margin").Value);
            foreach (XmlNode node2 in node.ChildNodes)  //读取tab
            {
                Tab item = new Tab(this.tabList, this) {
                    ID = int.Parse(node2.Attributes.GetNamedItem("ID").Value),
                    Name = node2.Attributes.GetNamedItem("Name").Value,
                    DisplayName = node2.Attributes.GetNamedItem("DisplayName").Value
                };
                if (node2.Attributes.GetNamedItem("ListKind") != null)
                {
                    item.ListKind = node2.Attributes.GetNamedItem("ListKind").Value;
                }
                if (node2.Attributes.GetNamedItem("ListMethod") != null)
                {
                    item.ListMethod = node2.Attributes.GetNamedItem("ListMethod").Value;
                }
                item.LoadColumnsFromString(node2.Attributes.GetNamedItem("Columns").Value);
                if (node2.Attributes.GetNamedItem("SortColumnID") != null)
                {
                    item.SortColumnID = int.Parse(node2.Attributes.GetNamedItem("SortColumnID").Value);
                }
                if (node2.Attributes.GetNamedItem("SmallToBig") != null)
                {
                    item.SmallToBig = bool.Parse(node2.Attributes.GetNamedItem("SmallToBig").Value);
                }
                item.Text.Text = item.DisplayName;
                this.Tabs.Add(item);
            }
        }

        public void MoveHorizontal(int offset)
        {
            this.HorizontalScrollBar.X += offset;
            if (this.HorizontalScrollBar.Left < this.tabList.VisibleLowerClient.Left)
            {
                this.HorizontalScrollBar.X = this.tabList.VisibleLowerClient.Left;
            }
            if (this.HorizontalScrollBar.Right > (this.tabList.VisibleLowerClient.Left + this.HorizontalScrollTrackLength))
            {
                this.HorizontalScrollBar.X = (this.tabList.VisibleLowerClient.Left + this.HorizontalScrollTrackLength) - this.HorizontalScrollBar.Width;
            }
            if (this.SelectedTab != null)
            {
                offset = (offset * this.tabList.VisibleLowerClient.Width) / this.HorizontalScrollBar.Width;
                this.SelectedTab.MoveHorizontal(-offset);
            }
        }

        public void MoveVertical(int offset)
        {
            this.VerticalScrollBar.Y += offset;
            if (this.VerticalScrollBar.Top < (this.tabList.VisibleLowerClient.Top + this.tabList.columnheaderHeight))
            {
                this.VerticalScrollBar.Y = this.tabList.VisibleLowerClient.Top + this.tabList.columnheaderHeight;
            }
            if (this.VerticalScrollBar.Bottom > ((this.tabList.VisibleLowerClient.Top + this.tabList.columnheaderHeight) + this.VerticalScrollTrackLength))
            {
                this.VerticalScrollBar.Y = ((this.tabList.VisibleLowerClient.Top + this.tabList.columnheaderHeight) + this.VerticalScrollTrackLength) - this.VerticalScrollBar.Height;
            }
            if (this.SelectedTab != null)
            {
                offset = (offset * (this.tabList.VisibleLowerClient.Height - this.tabList.columnheaderHeight)) / this.VerticalScrollBar.Height;
                this.SelectedTab.MoveVertical(-offset);
            }
        }

        public void ReCalculate()
        {
            Microsoft.Xna.Framework.Rectangle position = new Microsoft.Xna.Framework.Rectangle(this.tabList.RealClient.X + this.tabMargin, this.tabList.RealClient.Y + this.tabMargin, this.tabList.tabbuttonWidth, this.tabList.tabbuttonHeight);
            if (position.Right > (this.tabList.RealClient.Right - this.tabMargin))
            {
                throw new Exception("The tab button size is out of client's range.");
            }
            Tab tab = null;
            foreach (Tab tab2 in this.Tabs)
            {
                if (position.Right > (this.tabList.RealClient.Right - this.tabMargin))
                {
                    position.X = this.tabList.RealClient.X + this.tabMargin;
                    position.Y += position.Height + this.tabMargin;
                    if (position.Bottom > this.tabList.RealClient.Bottom)
                    {
                        throw new Exception("The tab button size is out of client's range.");
                    }
                }
                tab2.SetPosition(position);
                position.X += position.Width + this.tabMargin;
                if (tab2.Selected)
                {
                    tab = tab2;
                }
            }
            this.ColumnsTop = (position.Bottom + this.tabMargin) + 1;
            if (tab != null)
            {
                tab.ReCalculate(tab.CurrentYOffset);
            }
        }

        internal void RemoveCheckBoxColumn()
        {
            foreach (Tab tab in this.Tabs)
            {
                if (tab.Columns[0].Editable)
                {
                    tab.Columns.Remove(tab.Columns[0]);
                    tab.ReCalculate(tab.CurrentYOffset);
                }
            }
        }

        public int ResetAllOtherTabs(Tab tab)
        {
            int currentYOffset = 0;
            foreach (Tab tab2 in this.Tabs)
            {
                if ((tab2 != tab) && tab2.Selected)
                {
                    currentYOffset = tab2.CurrentYOffset;
                    tab2.ResetSelected();
                }
            }
            return currentYOffset;
        }

        public void ResetAllTabs()
        {
            foreach (Tab tab in this.Tabs)
            {
                tab.ResetSelected();
            }
        }

        internal void ResetAllTextures()
        {
            if (this.SelectedTab != null)
            {
                this.SelectedTab.ResetAllTextures();
            }
        }

        internal void ResetEditableTextures()
        {
            if (this.SelectedTab != null)
            {
                this.SelectedTab.ResetEditableTextures();
            }
        }

        public void ResetScrollTracks()
        {
            Microsoft.Xna.Framework.Rectangle realLowerVisibleClient = this.tabList.GetRealLowerVisibleClient();
            if (this.tabList.FullLowerClient.Width > realLowerVisibleClient.Width)
            {
                this.tabList.ShowHorizontalScrollBar = true;
                if (this.tabList.FullLowerClient.Height > realLowerVisibleClient.Height)
                {
                    this.HorizontalScrollTrackLength = (realLowerVisibleClient.Width - (2 * this.tabList.scrolltrackWidth)) - this.tabList.scrollbuttonWidth;
                }
                else
                {
                    this.HorizontalScrollTrackLength = realLowerVisibleClient.Width;
                }
                this.UpperScrollTrack = new Microsoft.Xna.Framework.Rectangle(realLowerVisibleClient.Left, (realLowerVisibleClient.Bottom - (2 * this.tabList.scrolltrackWidth)) - this.tabList.scrollbuttonWidth, this.HorizontalScrollTrackLength, this.tabList.scrolltrackWidth);
                this.LowerScrollTrack = new Microsoft.Xna.Framework.Rectangle(realLowerVisibleClient.Left, realLowerVisibleClient.Bottom - this.tabList.scrolltrackWidth, this.HorizontalScrollTrackLength, this.tabList.scrolltrackWidth);
                this.HorizontalScrollBar = new Microsoft.Xna.Framework.Rectangle(realLowerVisibleClient.Left, (realLowerVisibleClient.Bottom - this.tabList.scrolltrackWidth) - this.tabList.scrollbuttonWidth, (this.HorizontalScrollTrackLength * realLowerVisibleClient.Width) / this.tabList.FullLowerClient.Width, this.tabList.scrollbuttonWidth);
                this.tabList.ShrinkRectanglesHeight();
            }
            else
            {
                this.tabList.ShowHorizontalScrollBar = false;
                this.tabList.EnlargeRectanglesHeight();
            }
            if (this.tabList.FullLowerClient.Height > realLowerVisibleClient.Height)
            {
                this.tabList.ShowVerticalScrollBar = true;
                if (this.tabList.FullLowerClient.Width > realLowerVisibleClient.Width)
                {
                    this.VerticalScrollTrackLength = ((realLowerVisibleClient.Height - (2 * this.tabList.scrolltrackWidth)) - this.tabList.scrollbuttonWidth) - this.tabList.columnheaderHeight;
                }
                else
                {
                    this.VerticalScrollTrackLength = realLowerVisibleClient.Height - this.tabList.columnheaderHeight;
                }
                this.LeftScrollTrack = new Microsoft.Xna.Framework.Rectangle((realLowerVisibleClient.Right - (2 * this.tabList.scrolltrackWidth)) - this.tabList.scrollbuttonWidth, realLowerVisibleClient.Top + this.tabList.columnheaderHeight, this.tabList.scrolltrackWidth, this.VerticalScrollTrackLength);
                this.RightScrollTrack = new Microsoft.Xna.Framework.Rectangle(realLowerVisibleClient.Right - this.tabList.scrolltrackWidth, realLowerVisibleClient.Top + this.tabList.columnheaderHeight, this.tabList.scrolltrackWidth, this.VerticalScrollTrackLength);
                this.VerticalScrollBar = new Microsoft.Xna.Framework.Rectangle((realLowerVisibleClient.Right - this.tabList.scrolltrackWidth) - this.tabList.scrollbuttonWidth, realLowerVisibleClient.Top + this.tabList.columnheaderHeight, this.tabList.scrollbuttonWidth, (this.VerticalScrollTrackLength * realLowerVisibleClient.Height) / this.tabList.FullLowerClient.Height);
                this.tabList.ShrinkRectanglesWidth();
            }
            else
            {
                this.tabList.ShowVerticalScrollBar = false;
                this.tabList.EnlargeRectanglesWidth();
            }
            if (this.SelectedTab != null)
            {
                Microsoft.Xna.Framework.Rectangle visibleLowerClient = this.tabList.VisibleLowerClient;
                if (this.VerticalScrollBar.Bottom < visibleLowerClient.Bottom)
                {
                    int num = (int) ((Math.Abs(this.SelectedTab.CurrentYOffset) * visibleLowerClient.Height) / ((double) this.tabList.FullLowerClient.Height));
                    if ((num + this.VerticalScrollBar.Bottom) > visibleLowerClient.Bottom)
                    {
                        num = visibleLowerClient.Bottom - this.VerticalScrollBar.Bottom;
                    }
                    this.VerticalScrollBar = new Microsoft.Xna.Framework.Rectangle(this.VerticalScrollBar.X, this.VerticalScrollBar.Y + num, this.VerticalScrollBar.Width, this.VerticalScrollBar.Height);
                }
                else if (this.tabList.ShowHorizontalScrollBar)
                {
                    this.VerticalScrollBar = new Microsoft.Xna.Framework.Rectangle(this.VerticalScrollBar.X, ((visibleLowerClient.Bottom - this.VerticalScrollBar.Height) - (2 * this.tabList.scrolltrackWidth)) - this.tabList.scrollbuttonWidth, this.VerticalScrollBar.Width, this.VerticalScrollBar.Height);
                }
                else
                {
                    this.VerticalScrollBar = new Microsoft.Xna.Framework.Rectangle(this.VerticalScrollBar.X, visibleLowerClient.Bottom - this.VerticalScrollBar.Height, this.VerticalScrollBar.Width, this.VerticalScrollBar.Height);
                }
            }
        }

        public void SetSelectedTab(string tabName)
        {
            foreach (Tab tab in this.Tabs)
            {
                if ((tab.Name == tabName) && !tab.Selected)
                {
                    tab.Selected = true;
                }
            }
        }

        internal int ColumnsTop
        {
            get
            {
                return this.columnsTop;
            }
            set
            {
                this.columnsTop = value;
                this.tabList.VisibleLowerClient = this.tabList.RealClient;
                this.tabList.VisibleLowerClient.Y = value;
                this.tabList.VisibleLowerClient.Height -= value - this.tabList.RealClient.Y;
                this.tabList.AddRows();
            }
        }

        public bool HasSelectedTab
        {
            get
            {
                foreach (Tab tab in this.Tabs)
                {
                    if (tab.Selected)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

