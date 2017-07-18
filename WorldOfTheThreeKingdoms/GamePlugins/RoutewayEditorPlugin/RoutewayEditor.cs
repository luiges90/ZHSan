using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.MapDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RoutewayEditorPlugin
{

    internal class RoutewayEditor
    {
        internal Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal Texture2D BuildButtonDownTexture;
        internal Rectangle BuildButtonPosition;
        internal Texture2D BuildButtonSelectedTexture;
        private ButtonState BuildButtonState;
        internal Texture2D BuildButtonTexture;
        internal FreeRichText Comment = new FreeRichText();
        internal Texture2D CommentBackgroundTexture;
        internal int CommentClientWidth;
        private bool commentDrawing;
        private ArchitectureList CurrentPositionArchitectures;
        internal Texture2D CutButtonDisabledTexture;
        internal Texture2D CutButtonDownTexture;
        internal Rectangle CutButtonPosition;
        internal Texture2D CutButtonSelectedTexture;
        private ButtonState CutButtonState;
        internal Texture2D CutButtonTexture;
        internal Texture2D CutDisabledMouseArrowTexture;
        internal Point CutMouseArrowSize;
        internal Texture2D CutMouseArrowTexture;
        private Texture2D DefaultMouseArrowTexture;
        internal Texture2D DirectionSwitchDisabledTexture;
        internal Rectangle DirectionSwitchPosition;
        internal Texture2D DirectionSwitchSelectedTexture;
        private bool DirectionSwitchSpinning;
        private ButtonState DirectionSwitchState;
        internal Texture2D DirectionSwitchTexture;
        internal Point DisplayOffset;
        private bool draging;
        private Routeway EditingRouteway;
        internal Texture2D EndButtonDownTexture;
        internal Rectangle EndButtonPosition;
        internal Texture2D EndButtonSelectedTexture;
        private ButtonState EndButtonState;
        internal Texture2D EndButtonTexture;
        internal Texture2D ExtendButtonDisabledTexture;
        internal Texture2D ExtendButtonDownTexture;
        internal Rectangle ExtendButtonPosition;
        internal Texture2D ExtendButtonSelectedTexture;
        private ButtonState ExtendButtonState;
        internal Texture2D ExtendButtonTexture;
        internal Texture2D ExtendDisabledMouseArrowTexture;
        private bool extending;
        internal Point ExtendMouseArrowSize;
        internal Texture2D ExtendMouseArrowTexture;
        internal Texture2D ExtendPointEndTexture;
        internal List<FreeText> ExtendPointTexts = new List<FreeText>();
        internal Texture2D ExtendPointTexture;
        private bool isShowing;
        private Screen screen;
        internal FreeText TitleText;

        internal void AddDisableRects()
        {
            this.screen.AddDisableRectangle(this.screen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
            this.screen.AddDisableRectangle(this.screen.SelectingDisableRects, this.BackgroundDisplayPosition);
        }

        private void ClearExtendPointTexts()
        {
            foreach (FreeText text in this.ExtendPointTexts)
            {
                text.Text = "";
            }
        }

        private bool CutRouteway(Point p)
        {
            if (this.EditingRouteway.CutAt(p))
            {
                return true;
            }
            ArchitectureList routewayArchitecturesByPosition = this.screen.Scenario.GetRoutewayArchitecturesByPosition(this.EditingRouteway, this.EditingRouteway.LastPoint.Position);
            if (routewayArchitecturesByPosition.Count > 0)
            {
                if (routewayArchitecturesByPosition.Count > 1)
                {
                    routewayArchitecturesByPosition.PropertyName = "Food";
                    routewayArchitecturesByPosition.IsNumber = true;
                    routewayArchitecturesByPosition.SmallToBig = true;
                    routewayArchitecturesByPosition.ReSort();
                }
                Architecture architecture = routewayArchitecturesByPosition[0] as Architecture;
                if (architecture != this.EditingRouteway.EndArchitecture)
                {
                    this.EditingRouteway.EndArchitecture = architecture;
                }
            }
            else if (!((this.EditingRouteway.EndArchitecture == null) || this.EditingRouteway.HasPointInArchitectureRoutewayStartArea(this.EditingRouteway.EndArchitecture)))
            {
                this.EditingRouteway.EndArchitecture = null;
            }
            this.RefreshDirectionSwitchState();
            return false;
        }

        private void Drag()
        {
            this.RemoveDisableRects();
            this.DisplayOffset = new Point(this.DisplayOffset.X + this.screen.MouseOffset.X, this.DisplayOffset.Y + this.screen.MouseOffset.Y);
            this.AddDisableRects();
            this.TitleText.DisplayOffset = this.DisplayOffset;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.EditingRouteway.RoutePoints.Count != 0)
            {
                Rectangle? sourceRectangle = null;
                spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.25f);
                this.TitleText.Draw(spriteBatch, 0.2499f);
                switch (this.ExtendButtonState)
                {
                    case ButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ExtendButtonTexture, this.ExtendButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ExtendButtonSelectedTexture, this.ExtendButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Down:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ExtendButtonDownTexture, this.ExtendButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Disabled:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ExtendButtonDisabledTexture, this.ExtendButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;
                }
                switch (this.CutButtonState)
                {
                    case ButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.CutButtonTexture, this.CutButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.CutButtonSelectedTexture, this.CutButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Down:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.CutButtonDownTexture, this.CutButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Disabled:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.CutButtonDisabledTexture, this.CutButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;
                }
                switch (this.DirectionSwitchState)
                {
                    case ButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.DirectionSwitchTexture, this.DirectionSwitchDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.DirectionSwitchSelectedTexture, this.DirectionSwitchDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Disabled:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.DirectionSwitchDisabledTexture, this.DirectionSwitchDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;
                }
                switch (this.BuildButtonState)
                {
                    case ButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.BuildButtonTexture, this.BuildButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.BuildButtonSelectedTexture, this.BuildButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Down:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.BuildButtonDownTexture, this.BuildButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;
                }
                switch (this.EndButtonState)
                {
                    case ButtonState.Normal:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.EndButtonTexture, this.EndButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Selected:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.EndButtonSelectedTexture, this.EndButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;

                    case ButtonState.Down:
                        sourceRectangle = null;
                        spriteBatch.Draw(this.EndButtonDownTexture, this.EndButtonDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.249f);
                        break;
                }
                if (this.ExtendButtonState == ButtonState.Down)
                {
                    this.ClearExtendPointTexts();
                    GameArea currentExtendArea = this.EditingRouteway.CurrentExtendArea;
                    for (int i = 0; i < currentExtendArea.Count; i++)
                    {
                        if (this.screen.TileInScreen(currentExtendArea[i]))
                        {
                            Rectangle destination = this.screen.GetDestination(currentExtendArea[i]);
                            if (this.screen.Scenario.GetRoutewayArchitecturesByPosition(this.EditingRouteway, currentExtendArea[i]).Count > 0)
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(this.ExtendPointEndTexture, destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8498001f);
                            }
                            else
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(this.ExtendPointTexture, destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8498001f);
                            }
                            TerrainDetail terrainDetailByPosition = this.screen.Scenario.GetTerrainDetailByPosition(currentExtendArea[i]);
                            this.ExtendPointTexts[i].Position = new Rectangle(0, 0, destination.Width, destination.Height);
                            this.ExtendPointTexts[i].Text = StaticMethods.GetPercentString((terrainDetailByPosition.RoutewayConsumptionRate * this.EditingRouteway.BelongedFaction.RateOfRoutewayConsumption) + this.EditingRouteway.LastPoint.ConsumptionRate, 1);
                            this.ExtendPointTexts[i].DisplayOffset = new Point(destination.X, destination.Y);
                        }
                    }
                    foreach (FreeText text in this.ExtendPointTexts)
                    {
                        text.Draw(spriteBatch, 0.8499f);
                    }
                }
                if (!((!this.commentDrawing || this.draging) || this.extending))
                {
                    spriteBatch.Draw(this.CommentBackgroundTexture, this.CommentDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.25f);
                    this.Comment.Draw(spriteBatch, 0.2499f);
                }
            }
        }

        private void ExtendRouteway(Point p)
        {
            this.EditingRouteway.Extend(p);
            if (this.CurrentPositionArchitectures != null && this.CurrentPositionArchitectures.Count > 0)
            {
                if (this.CurrentPositionArchitectures.Count > 1)
                {
                    this.CurrentPositionArchitectures.PropertyName = "Food";
                    this.CurrentPositionArchitectures.IsNumber = true;
                    this.CurrentPositionArchitectures.SmallToBig = true;
                    this.CurrentPositionArchitectures.ReSort();
                }
                this.EditingRouteway.EndArchitecture = this.CurrentPositionArchitectures[0] as Architecture;
            }
            else if (!((this.EditingRouteway.EndArchitecture == null) || this.EditingRouteway.HasPointInArchitectureRoutewayStartArea(this.EditingRouteway.EndArchitecture)))
            {
                this.EditingRouteway.EndArchitecture = null;
            }
            this.RefreshDirectionSwitchState();
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void RefreshDirectionSwitchState()
        {
            if (((this.EditingRouteway.StartArchitecture != null) && (this.EditingRouteway.EndArchitecture != null)) && this.EditingRouteway.EndedInArchitectureRoutewayStartArea(this.EditingRouteway.EndArchitecture))
            {
                this.DirectionSwitchState = ButtonState.Normal;
            }
            else
            {
                this.DirectionSwitchState = ButtonState.Disabled;
            }
        }

        internal void RemoveDisableRects()
        {
            this.screen.RemoveDisableRectangle(this.screen.LaterMouseEventDisableRects, this.BackgroundDisplayPosition);
            this.screen.RemoveDisableRectangle(this.screen.SelectingDisableRects, this.BackgroundDisplayPosition);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.BackgroundDisplayPosition))
            {
                if (StaticMethods.PointInRectangle(position, this.ExtendButtonDisplayPosition))
                {
                    if ((this.ExtendButtonState == ButtonState.Selected) || (this.ExtendButtonState == ButtonState.Normal))
                    {
                        this.ExtendButtonState = ButtonState.Down;
                    }
                    else if (this.ExtendButtonState == ButtonState.Down)
                    {
                        this.ExtendButtonState = ButtonState.Selected;
                    }
                    if (this.CutButtonState == ButtonState.Down)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.CutButtonDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Down)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if ((this.CutButtonState == ButtonState.Normal) || (this.CutButtonState == ButtonState.Selected))
                    {
                        this.CutButtonState = ButtonState.Down;
                    }
                    else if (this.CutButtonState == ButtonState.Down)
                    {
                        this.CutButtonState = ButtonState.Selected;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.DirectionSwitchDisplayPosition))
                {
                    if ((this.DirectionSwitchState == ButtonState.Normal) || (this.DirectionSwitchState == ButtonState.Selected))
                    {
                        if (this.ExtendButtonState == ButtonState.Down)
                        {
                            this.ExtendButtonState = ButtonState.Normal;
                        }
                        if (this.CutButtonState == ButtonState.Down)
                        {
                            this.CutButtonState = ButtonState.Normal;
                        }
                        this.EditingRouteway.ReverseDirection();
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.BuildButtonDisplayPosition))
                {
                    this.BuildButtonState = ButtonState.Down;
                }
                else if (StaticMethods.PointInRectangle(position, this.EndButtonDisplayPosition))
                {
                    this.EndButtonState = ButtonState.Down;
                }
            }
            else
            {
                Point positionByPoint;
                if (this.ExtendButtonState == ButtonState.Down)
                {
                    GameArea currentExtendArea = this.EditingRouteway.CurrentExtendArea;
                    if (currentExtendArea != null)
                    {
                        positionByPoint = this.screen.GetPositionByPoint(position);
                        if (currentExtendArea.HasPoint(positionByPoint))
                        {
                            TerrainDetail terrainDetailByPosition = this.screen.Scenario.GetTerrainDetailByPosition(positionByPoint);
                            if ((terrainDetailByPosition != null) && ((terrainDetailByPosition.RoutewayConsumptionRate + this.EditingRouteway.LastPoint.ConsumptionRate) < 1f))
                            {
                                this.ExtendRouteway(positionByPoint);
                                this.extending = true;
                            }
                        }
                    }
                }
                else if (this.CutButtonState == ButtonState.Down)
                {
                    positionByPoint = this.screen.GetPositionByPoint(position);
                    if (this.CutRouteway(positionByPoint))
                    {
                        this.screen.Scenario.RemoveRouteway(this.EditingRouteway);
                        this.IsShowing = false;
                    }
                }
            }
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            this.draging = false;
            this.extending = false;
            if ((StaticMethods.PointInRectangle(position, this.BackgroundDisplayPosition) && !StaticMethods.PointInRectangle(position, this.ExtendButtonDisplayPosition)) && (!StaticMethods.PointInRectangle(position, this.CutButtonDisplayPosition) && !StaticMethods.PointInRectangle(position, this.DirectionSwitchDisplayPosition)))
            {
                if (StaticMethods.PointInRectangle(position, this.BuildButtonDisplayPosition))
                {
                    this.EditingRouteway.Building = true;
                    this.IsShowing = false;
                }
                else if (StaticMethods.PointInRectangle(position, this.EndButtonDisplayPosition))
                {
                    this.IsShowing = false;
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            this.commentDrawing = false;
            if (StaticMethods.PointInRectangle(position, this.BackgroundDisplayPosition))
            {
                this.screen.DefaultMouseArrowTexture = this.DefaultMouseArrowTexture;
                if (StaticMethods.PointInRectangle(position, this.ExtendButtonDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Normal)
                    {
                        this.ExtendButtonState = ButtonState.Selected;
                    }
                    if (this.CutButtonState == ButtonState.Selected)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                    if (this.DirectionSwitchState == ButtonState.Selected)
                    {
                        this.DirectionSwitchState = ButtonState.Normal;
                    }
                    if (this.EndButtonState == ButtonState.Selected)
                    {
                        this.EndButtonState = ButtonState.Normal;
                    }
                    this.commentDrawing = true;
                    this.Comment.Clear();
                    this.Comment.AddText("延伸粮道", this.Comment.TitleColor);
                    this.Comment.AddNewLine();
                    this.Comment.AddText("请根据实际情况设计粮道的路径。");
                    this.Comment.AddNewLine();
                    this.Comment.AddText("提示", this.Comment.SubTitleColor);
                    this.Comment.AddText("：您可以在延伸点按下鼠标之后进行拖拽。");
                    this.Comment.ResortTexts();
                    this.Comment.DisplayOffset = new Point(this.ExtendButtonDisplayPosition.Right, this.ExtendButtonDisplayPosition.Top);
                }
                else if (StaticMethods.PointInRectangle(position, this.CutButtonDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Selected)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if (this.CutButtonState == ButtonState.Normal)
                    {
                        this.CutButtonState = ButtonState.Selected;
                    }
                    if (this.DirectionSwitchState == ButtonState.Selected)
                    {
                        this.DirectionSwitchState = ButtonState.Normal;
                    }
                    if (this.EndButtonState == ButtonState.Selected)
                    {
                        this.EndButtonState = ButtonState.Normal;
                    }
                    this.commentDrawing = true;
                    this.Comment.Clear();
                    this.Comment.AddText("截断粮道", this.Comment.TitleColor);
                    this.Comment.AddNewLine();
                    this.Comment.AddText("用以删除某一点之后的粮道。");
                    this.Comment.AddNewLine();
                    this.Comment.AddText("提示", this.Comment.SubTitleColor);
                    this.Comment.AddText("：如果选择粮道的起点则删除整条粮道。请小心操作。");
                    this.Comment.ResortTexts();
                    this.Comment.DisplayOffset = new Point(this.CutButtonDisplayPosition.Right, this.CutButtonDisplayPosition.Top);
                }
                else if (StaticMethods.PointInRectangle(position, this.DirectionSwitchDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Selected)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if (this.CutButtonState == ButtonState.Selected)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                    if (this.DirectionSwitchState == ButtonState.Normal)
                    {
                        this.DirectionSwitchState = ButtonState.Selected;
                    }
                    if (this.EndButtonState == ButtonState.Selected)
                    {
                        this.EndButtonState = ButtonState.Normal;
                    }
                    this.commentDrawing = true;
                    this.Comment.Clear();
                    this.Comment.AddText("转换方向",this.Comment.TitleColor);
                    this.Comment.AddNewLine();
                    this.Comment.AddText("用以转换粮道的运输方向。");
                    this.Comment.AddNewLine();
                    this.Comment.AddText("提示", this.Comment.SubTitleColor);
                    this.Comment.AddText("：仅在粮道两端都为我方建筑时有效。未完全疏通的粮道将被关闭。");
                    this.Comment.ResortTexts();
                    this.Comment.DisplayOffset = new Point(this.DirectionSwitchDisplayPosition.Right, this.DirectionSwitchDisplayPosition.Top);
                }
                else if (StaticMethods.PointInRectangle(position, this.BuildButtonDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Selected)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if (this.CutButtonState == ButtonState.Selected)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                    if (this.DirectionSwitchState == ButtonState.Selected)
                    {
                        this.DirectionSwitchState = ButtonState.Normal;
                    }
                    if (this.BuildButtonState == ButtonState.Normal)
                    {
                        if (leftDown)
                        {
                            this.BuildButtonState = ButtonState.Down;
                        }
                        else
                        {
                            this.BuildButtonState = ButtonState.Selected;
                        }
                    }
                    if (this.EndButtonState == ButtonState.Selected)
                    {
                        this.EndButtonState = ButtonState.Normal;
                    }
                    this.commentDrawing = true;
                    this.Comment.Clear();
                    this.Comment.AddText("疏通粮道", this.Comment.TitleColor);
                    this.Comment.AddNewLine();
                    this.Comment.AddText("结束粮道的编辑工作并且开始疏通粮道。");
                    this.Comment.AddNewLine();
                    this.Comment.ResortTexts();
                    this.Comment.DisplayOffset = new Point(this.BuildButtonDisplayPosition.Right, this.BuildButtonDisplayPosition.Top);
                }
                else if (StaticMethods.PointInRectangle(position, this.EndButtonDisplayPosition))
                {
                    if (this.ExtendButtonState == ButtonState.Selected)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if (this.CutButtonState == ButtonState.Selected)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                    if (this.DirectionSwitchState == ButtonState.Selected)
                    {
                        this.DirectionSwitchState = ButtonState.Normal;
                    }
                    if (this.BuildButtonState == ButtonState.Selected)
                    {
                        this.BuildButtonState = ButtonState.Normal;
                    }
                    if (this.EndButtonState == ButtonState.Normal)
                    {
                        if (leftDown)
                        {
                            this.EndButtonState = ButtonState.Down;
                        }
                        else
                        {
                            this.EndButtonState = ButtonState.Selected;
                        }
                    }
                    this.commentDrawing = true;
                    this.Comment.Clear();
                    this.Comment.AddText("结束编辑", this.Comment.TitleColor);
                    this.Comment.AddNewLine();
                    this.Comment.AddText("结束粮道的编辑工作。");
                    this.Comment.AddNewLine();
                    this.Comment.ResortTexts();
                    this.Comment.DisplayOffset = new Point(this.EndButtonDisplayPosition.Right, this.EndButtonDisplayPosition.Top);
                }
                else
                {
                    if (this.ExtendButtonState == ButtonState.Selected)
                    {
                        this.ExtendButtonState = ButtonState.Normal;
                    }
                    if (this.CutButtonState == ButtonState.Selected)
                    {
                        this.CutButtonState = ButtonState.Normal;
                    }
                    if (this.DirectionSwitchState == ButtonState.Selected)
                    {
                        this.DirectionSwitchState = ButtonState.Normal;
                    }
                    if ((this.EndButtonState == ButtonState.Selected) || (this.EndButtonState == ButtonState.Down))
                    {
                        this.EndButtonState = ButtonState.Normal;
                    }
                    this.draging = leftDown && !this.extending;
                }
            }
            else
            {
                if (this.ExtendButtonState == ButtonState.Selected)
                {
                    this.ExtendButtonState = ButtonState.Normal;
                }
                if (this.CutButtonState == ButtonState.Selected)
                {
                    this.CutButtonState = ButtonState.Normal;
                }
                if (this.DirectionSwitchState == ButtonState.Selected)
                {
                    this.DirectionSwitchState = ButtonState.Normal;
                }
                if ((this.BuildButtonState == ButtonState.Selected) || (this.BuildButtonState == ButtonState.Down))
                {
                    this.BuildButtonState = ButtonState.Normal;
                }
                if ((this.EndButtonState == ButtonState.Selected) || (this.EndButtonState == ButtonState.Down))
                {
                    this.EndButtonState = ButtonState.Normal;
                }
                Point positionByPoint = this.screen.GetPositionByPoint(position);
                if (this.ExtendButtonState == ButtonState.Down)
                {
                    if (this.EditingRouteway.CurrentExtendArea.HasPoint(positionByPoint))
                    {
                        this.CurrentPositionArchitectures = this.screen.Scenario.GetRoutewayArchitecturesByPosition(this.EditingRouteway, positionByPoint);
                        if (this.extending)
                        {
                            this.ExtendRouteway(positionByPoint);
                        }
                        else if (this.CurrentPositionArchitectures.Count > 0)
                        {
                            if (this.CurrentPositionArchitectures.Count > 1)
                            {
                                this.CurrentPositionArchitectures.PropertyName = "Food";
                                this.CurrentPositionArchitectures.IsNumber = true;
                                this.CurrentPositionArchitectures.ReSort();
                            }
                            this.commentDrawing = true;
                            this.Comment.Clear();
                            this.Comment.AddText("连接到", this.Comment.TitleColor);
                            this.Comment.AddText(this.CurrentPositionArchitectures[0].Name, this.Comment.SubTitleColor);
                            this.Comment.ResortTexts();
                            this.Comment.DisplayOffset = new Point(this.screen.GetDestination(positionByPoint).Right, this.screen.GetDestination(positionByPoint).Top);
                        }
                        this.screen.DefaultMouseArrowTexture = this.ExtendMouseArrowTexture;
                    }
                    else
                    {
                        if ((this.extending && (this.EditingRouteway.RoutePoints.Last.Previous != null)) && (positionByPoint == this.EditingRouteway.RoutePoints.Last.Previous.Value.Position))
                        {
                            this.CutRouteway(this.EditingRouteway.LastPoint.Position);
                        }
                        this.screen.DefaultMouseArrowTexture = this.ExtendDisabledMouseArrowTexture;
                    }
                }
                else if (this.CutButtonState == ButtonState.Down)
                {
                    if (this.EditingRouteway.ContainsPoint(positionByPoint))
                    {
                        this.screen.DefaultMouseArrowTexture = this.CutMouseArrowTexture;
                    }
                    else
                    {
                        this.screen.DefaultMouseArrowTexture = this.CutDisabledMouseArrowTexture;
                    }
                }
                if (!this.draging && !this.extending)
                {
                    RoutePoint point = this.EditingRouteway.GetPoint(positionByPoint);
                    if (point != null)
                    {
                        this.commentDrawing = true;
                        this.Comment.Clear();
                        this.Comment.AddText("当前坐标粮道信息", this.Comment.TitleColor);
                        this.Comment.AddNewLine();
                        this.Comment.AddText("消耗率：");
                        this.Comment.AddText(StaticMethods.GetPercentString(point.ConsumptionRate, 1), this.Comment.SubTitleColor2);
                        this.Comment.AddNewLine();
                        this.Comment.AddText("开通费用：");
                        this.Comment.AddText(point.BuildFundCost.ToString());
                        this.Comment.AddNewLine();
                        this.Comment.AddText("维持费用：");
                        this.Comment.AddText(point.ActiveFundCost.ToString());
                        this.Comment.AddNewLine();
                        this.Comment.ResortTexts();
                        this.Comment.DisplayOffset = new Point(this.screen.GetDestination(positionByPoint).Right, this.screen.GetDestination(positionByPoint).Top);
                    }
                }
            }
            if (this.draging)
            {
                this.Drag();
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
        }

        internal void SetRouteway(Routeway routeway)
        {
            if (this.EditingRouteway != null)
            {
                this.EditingRouteway.Selected = false;
            }
            this.EditingRouteway = routeway;
            this.EditingRouteway.Selected = true;
        }

        private void Show()
        {
            Point pointByPosition = this.screen.GetPointByPosition(this.screen.GetPositionByPoint(this.screen.MousePosition));
            int x = pointByPosition.X;
            int y = pointByPosition.Y - this.BackgroundSize.Y;
            if ((x + this.BackgroundSize.X) > this.screen.viewportSize.X)
            {
                x = this.screen.viewportSize.X - this.BackgroundSize.X;
            }
            if (y < 0)
            {
                y = 0;
            }
            this.DisplayOffset = new Point(x, y);
            this.TitleText.DisplayOffset = this.DisplayOffset;
            GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
            this.ExtendButtonState = ButtonState.Down;
            this.CutButtonState = ButtonState.Normal;
            this.RefreshDirectionSwitchState();
            this.EndButtonState = ButtonState.Normal;
        }

        public void Update()
        {
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle BuildButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.BuildButtonPosition.X, this.DisplayOffset.Y + this.BuildButtonPosition.Y, this.BuildButtonPosition.Width, this.BuildButtonPosition.Height);
            }
        }

        private Rectangle CommentDisplayPosition
        {
            get
            {
                return new Rectangle(this.Comment.DisplayOffset.X, this.Comment.DisplayOffset.Y, this.CommentClientWidth, this.Comment.RealHeight);
            }
        }

        private Rectangle CutButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.CutButtonPosition.X, this.DisplayOffset.Y + this.CutButtonPosition.Y, this.CutButtonPosition.Width, this.CutButtonPosition.Height);
            }
        }

        private Rectangle DirectionSwitchDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.DirectionSwitchPosition.X, this.DisplayOffset.Y + this.DirectionSwitchPosition.Y, this.DirectionSwitchPosition.Width, this.DirectionSwitchPosition.Height);
            }
        }

        private Rectangle EndButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.EndButtonPosition.X, this.DisplayOffset.Y + this.EndButtonPosition.Y, this.EndButtonPosition.Width, this.EndButtonPosition.Height);
            }
        }

        private Rectangle ExtendButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.ExtendButtonPosition.X, this.DisplayOffset.Y + this.ExtendButtonPosition.Y, this.ExtendButtonPosition.Width, this.ExtendButtonPosition.Height);
            }
        }

        internal bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                if (this.isShowing != value)
                {
                    this.isShowing = value;
                    if (value)
                    {
                        this.Show();
                        this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.liangdaobianji , DialogKind.liangdaobianji ));

                        this.DefaultMouseArrowTexture = this.screen.DefaultMouseArrowTexture;
                        this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        this.AddDisableRects();
                    }
                    else
                    {
                        if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.liangdaobianji )
                        {
                            throw new Exception("The UndoneWork is not a liangdaobianji.");
                        }

                        this.EditingRouteway.Selected = false;
                        this.screen.DefaultMouseArrowTexture = this.DefaultMouseArrowTexture;
                        this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        this.screen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        this.RemoveDisableRects();
                    }
                }
            }
        }
    }
}

