using GameFreeText;
using GameObjects;
using GameGlobal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TabListPlugin
{

    public class Tab
    {
        public List<Column> Columns;
        public string DisplayName;
        public int ID;
        private ListKind listKind;
        public string ListKind;
        public string ListMethod;
        public string Name;
        public float Scale = 1f;
        private bool selected;
        public bool SmallToBig;
        public int SortColumnID;
        private TabListInFrame tabList;
        internal FreeText Text;

        internal Tab(TabListInFrame tabList, ListKind listKind)
        {
            this.Text = new FreeText(tabList.TabTextBuilder);
            this.Text.TextColor = tabList.TabTextColor;
            this.Text.Align = tabList.TabTextAlign;
            this.tabList = tabList;
            this.listKind = listKind;
            this.Columns = new List<Column>();
        }

        private int LeastDetailLevelCache = -1;
        public int LeastDetailLevel
        {
            get
            {
                int r = 99;
                if (LeastDetailLevelCache < 0){
                    foreach (Column c in this.Columns)
                    {
                        if (c.DetailLevel < r && c.CountToDisplay)
                        {
                            r = c.DetailLevel;
                        }
                    }
                    LeastDetailLevelCache = r;
                }
                return LeastDetailLevelCache;
            }
        }

        public bool Visible
        {
            get
            {
                return this.LeastDetailLevel <= GlobalVariables.TabListDetailLevel;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
            {
                if (this.selected)
                {
                    spriteBatch.Draw(this.tabList.tabbuttonselectedTexture, this.Position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.035f);
                    this.Text.Draw(spriteBatch, Color.White, 0.03499f);
                    foreach (Column column in this.Columns)
                    {
                        if (column.Visible)
                        {
                            column.Draw(spriteBatch);
                        }
                    }
                }
                else
                {
                    spriteBatch.Draw(this.tabList.tabbuttonTexture, this.Position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.035f);
                    this.Text.Draw(spriteBatch, 0.03499f);
                }
            }
        }

        public Column GetColumnByID(int ID)
        {
            foreach (Column column in this.Columns)
            {
                if (column.ID == ID)
                {
                    return column;
                }
            }
            return null;
        }

        public void LoadColumnsFromString(string columnString)
        {
            char[] separator = new char[] { ' ' };
            string[] strArray = columnString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                Column columnByID = this.listKind.GetColumnByID(int.Parse(strArray[i]));
                if (columnByID != null)
                {
                    this.Columns.Add(columnByID);
                }
            }
        }

        public void MoveHorizontal(int offset)
        {
            if ((this.Columns[0].DisplayPosition.Left + offset) > this.tabList.VisibleLowerClient.Left)
            {
                offset = this.tabList.VisibleLowerClient.Left - this.Columns[0].DisplayPosition.Left;
            }
            else if ((this.Columns[this.Columns.Count - 1].DisplayPosition.Right + offset) < this.tabList.VisibleLowerClient.Right)
            {
                offset = this.tabList.VisibleLowerClient.Right - this.Columns[this.Columns.Count - 1].DisplayPosition.Right;
            }
            if (offset != 0)
            {
                foreach (Column column in this.Columns)
                {
                    column.MoveHorizontal(offset);
                }
            }
        }

        public void MoveVertical(int offset)
        {
            foreach (Column column in this.Columns)
            {
                column.MoveVertical(offset);
            }
            if (this.Columns.Count > 0)
            {
                this.Columns[0].AdjustRowRectangles(this.tabList.RowRectangles);
            }
        }

        internal void ReCalculate(int yOffset)
        {
            if (this.selected)
            {
                int x = this.tabList.RealClient.X;
                this.tabList.FullLowerClient.X = x;
                this.tabList.FullLowerClient.Y = this.tabList.VisibleLowerClient.Y;
                foreach (Column column in this.Columns)
                {
                    column.ReCalculate(this.listKind.ColumnsTop, ref x);
                    column.ColumnTextList.DisplayOffset = new Point(0, yOffset);
                }
                this.SortTheKeyColumn();
                this.tabList.FullLowerClient.Width = x - this.tabList.RealClient.X + this.tabList.iGameFrame.LeftEdge + this.tabList.iGameFrame.RightEdge;
                this.listKind.ResetScrollTracks();
                if (this.Columns.Count > 0)
                {
                    this.Columns[0].AdjustRowRectangles(this.tabList.RowRectangles);
                }
                this.ResetAllTextures();
            }
        }

        internal void ResetAllTextures()
        {
            foreach (Column column in this.Columns)
            {
                column.ResetAllTextures();
            }
        }

        internal void ResetEditableTextures()
        {
            foreach (Column column in this.Columns)
            {
                column.ResetEditableTextures();
            }
        }

        public void ResetSelected()
        {
            this.selected = false;
        }

        public void SetPosition(Rectangle position)
        {
            this.Position = position;
        }

        private void SortTheKeyColumn()
        {
            if (this.SortColumnID > 0)
            {
                Column columnByID = this.GetColumnByID(this.SortColumnID);
                if (columnByID != null)
                {
                    PropertyComparer comparer = new PropertyComparer(columnByID.Name, columnByID.IsNumber, this.SmallToBig);
                    this.tabList.gameObjectList.GameObjects.Sort(comparer);
                }
            }
        }

        internal int CurrentYOffset
        {
            get
            {
                return ((this.Columns.Count > 0) ? this.Columns[0].ColumnTextList.DisplayOffset.Y : 0);
            }
        }

        internal Rectangle Position
        {
            get
            {
                return this.Text.Position;
            }
            set
            {
                this.Text.Position = value;
            }
        }

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                if (this.selected != value)
                {
                    this.selected = value;
                    this.ReCalculate(this.listKind.ResetAllOtherTabs(this));
                    this.listKind.SelectedTab = this;
                }
            }
        }
    }
}

