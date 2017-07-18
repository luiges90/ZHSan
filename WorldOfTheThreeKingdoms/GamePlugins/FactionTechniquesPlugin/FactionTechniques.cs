using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.FactionDetail;
using GameObjects.Conditions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;

namespace FactionTechniquesPlugin
{

    public class FactionTechniques
    {
        internal List<TechniqueItem> AllTechniques = new List<TechniqueItem>();
        internal Microsoft.Xna.Framework.Point BackgroundSize;
        internal Texture2D BackgroundTexture;
        internal Texture2D ButtonAvailableTexture;
        internal Texture2D ButtonBasicTexture;
        internal Microsoft.Xna.Framework.Point ButtonSize;
        internal Microsoft.Xna.Framework.Point ButtonStartPosition;
        internal TextAlign ButtonTextAlign;
        internal Microsoft.Xna.Framework.Color ButtonTextColor;
        internal Font ButtonTextFont;
        internal Texture2D ButtonUpgradedTexture;
        internal Texture2D ButtonUpgradingTexture;
        internal Microsoft.Xna.Framework.Rectangle CommentsClient;
        internal FreeRichText CommentsText = new FreeRichText();
        internal bool Control;
        private object current;
        private Microsoft.Xna.Framework.Point DisplayOffset;
        internal Texture2D FrameTexture;
        internal GraphicsDevice graphicsDevice;
        private bool isShowing;
        internal List<LabelText> LabelTexts = new List<LabelText>();
        internal Screen screen;
        internal Faction ShowingFaction;
        internal Architecture UpgradingArchitecture;

        internal void Draw(SpriteBatch spriteBatch)
        {
            if (this.ShowingFaction != null)
            {
                Microsoft.Xna.Framework.Rectangle? sourceRectangle = null;
                spriteBatch.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);  // 0.2f);
                foreach (LabelText text in this.LabelTexts)
                {
                    text.Label.Draw(spriteBatch, 0.01999f);
                    text.Text.Draw(spriteBatch, 0.01999f);
                }
                foreach (TechniqueItem item in this.AllTechniques)
                {
                    Microsoft.Xna.Framework.Color white = Microsoft.Xna.Framework.Color.White;
                    Microsoft.Xna.Framework.Color black = Microsoft.Xna.Framework.Color.Black;
                    if (item == this.current)
                    {
                        white = new Microsoft.Xna.Framework.Color((byte) 150, (byte) 150, (byte) 150);
                        black = Microsoft.Xna.Framework.Color.White;
                    }
                    item.Text.Draw(spriteBatch, black, 0.01989f);
                    if (this.ShowingFaction.IsTechniqueUpgrading(item.LinkedTechnique.ID))
                    {
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ButtonUpgradingTexture, item.Position, sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);  // 0.199f);
                    }
                    else if (this.ShowingFaction.HasTechnique(item.LinkedTechnique.ID))
                    {
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ButtonUpgradedTexture, item.Position, sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);
                    }
                    else if (this.Control && this.ShowingFaction.MatchTechnique(item.LinkedTechnique, this.UpgradingArchitecture))
                    {
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ButtonAvailableTexture, item.Position, sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);
                    }
                    else
                    {
                        sourceRectangle = null;
                        spriteBatch.Draw(this.ButtonBasicTexture, item.Position, sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);
                    }
                }
                this.CommentsText.Draw(spriteBatch, 0.01999f);
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void screen_OnMouseLeftDown(Microsoft.Xna.Framework.Point position)
        {
            if (this.Control && this.ShowingFaction.Controlling)
            {
                foreach (TechniqueItem item in this.AllTechniques)
                {
                    if (StaticMethods.PointInRectangle(position, item.Position) && (!this.ShowingFaction.HasTechnique(item.LinkedTechnique.ID) && this.ShowingFaction.MatchTechnique(item.LinkedTechnique, this.UpgradingArchitecture)))
                    {
                        this.ShowingFaction.UpgradeTechnique(item.LinkedTechnique, this.UpgradingArchitecture);
                        foreach (LabelText text in this.LabelTexts)
                        {
                            text.Text.Text = StaticMethods.GetPropertyValue(this, text.PropertyName).ToString();
                        }
                    }
                }
            }
        }

        private void screen_OnMouseMove(Microsoft.Xna.Framework.Point position, bool leftDown)
        {
            bool flag = false;
            foreach (TechniqueItem item in this.AllTechniques)
            {
                if (StaticMethods.PointInRectangle(position, item.Position))
                {
                    if (this.current != item)
                    {
                        this.CommentsText.Clear();
                        this.CommentsText.AddText(item.LinkedTechnique.Name, this.CommentsText.TitleColor);
                        this.CommentsText.AddNewLine();
                        this.CommentsText.AddText(item.LinkedTechnique.Description, this.CommentsText.DefaultColor);
                        this.CommentsText.AddNewLine();
                        if (this.Control)
                        {
                            this.CommentsText.AddNewLine();
                            if (this.ShowingFaction.IsTechniqueUpgrading(item.LinkedTechnique.ID))
                            {
                                this.CommentsText.AddText("正在升级中……剩余", this.CommentsText.SubTitleColor);
                                this.CommentsText.AddText(this.ShowingFaction.UpgradingDaysLeft.ToString(), Microsoft.Xna.Framework.Color.Lime);
                                this.CommentsText.AddText("天", this.CommentsText.SubTitleColor);
                            }
                            else if (this.ShowingFaction.HasTechnique(item.LinkedTechnique.ID))
                            {
                                this.CommentsText.AddText("已拥有", this.CommentsText.SubTitleColor2);
                            }
                            else if (this.ShowingFaction.MatchTechnique(item.LinkedTechnique, this.UpgradingArchitecture))
                            {
                                this.CommentsText.AddText("可升级", this.CommentsText.PositiveColor);
                                this.CommentsText.AddNewLine();
                                this.CommentsText.AddText("升级时间", this.CommentsText.DefaultColor);
                                this.CommentsText.AddText(this.ShowingFaction == null ? item.LinkedTechnique.Days.ToString() : this.ShowingFaction.getTechniqueActualTime(item.LinkedTechnique).ToString(), this.CommentsText.PositiveColor);
                                this.CommentsText.AddText("天", this.CommentsText.DefaultColor);
                                this.CommentsText.AddNewLine();
                                this.CommentsText.AddText("势力声望：" + (this.ShowingFaction == null ? item.LinkedTechnique.Reputation.ToString() : this.ShowingFaction.getTechniqueActualReputation(item.LinkedTechnique).ToString()) + 
                                    "，技巧点数：" + (this.ShowingFaction == null ? item.LinkedTechnique.PointCost.ToString() : this.ShowingFaction.getTechniqueActualPointCost(item.LinkedTechnique).ToString()) + 
                                    "，资金：" + (this.ShowingFaction == null ? item.LinkedTechnique.FundCost.ToString() : this.ShowingFaction.getTechniqueActualFundCost(item.LinkedTechnique).ToString()),
                                    this.CommentsText.DefaultColor);
                                if (item.LinkedTechnique.PreID >= 0)
                                {
                                    this.CommentsText.AddText("，前提条件：" + this.ShowingFaction.Scenario.GameCommonData.AllTechniques.GetTechnique(item.LinkedTechnique.PreID).Name, this.CommentsText.DefaultColor);
                                }
                                this.CommentsText.AddNewLine();
                                this.CommentsText.AddText("请单击鼠标左键开始升级", this.CommentsText.SubTitleColor2);
                            }
                            else
                            {
                                this.CommentsText.AddText("升级时间", this.CommentsText.DefaultColor);
                                this.CommentsText.AddText(this.ShowingFaction == null ? item.LinkedTechnique.Days.ToString() : this.ShowingFaction.getTechniqueActualTime(item.LinkedTechnique).ToString(), this.CommentsText.PositiveColor);
                                this.CommentsText.AddText("天", this.CommentsText.DefaultColor);
                                this.CommentsText.AddNewLine();
                                this.CommentsText.AddNewLine();
                                this.CommentsText.AddText("未达到升级条件：", this.CommentsText.SubTitleColor);
                                this.CommentsText.AddNewLine();
                                if (this.ShowingFaction.Reputation >= (this.ShowingFaction == null ? item.LinkedTechnique.Reputation : this.ShowingFaction.getTechniqueActualReputation(item.LinkedTechnique)))
                                {
                                    this.CommentsText.AddText("势力声望：" + (this.ShowingFaction == null ? item.LinkedTechnique.Reputation.ToString() : this.ShowingFaction.getTechniqueActualReputation(item.LinkedTechnique).ToString()), this.CommentsText.PositiveColor);
                                }
                                else
                                {
                                    this.CommentsText.AddText("势力声望：" + (this.ShowingFaction == null ? item.LinkedTechnique.Reputation.ToString() : this.ShowingFaction.getTechniqueActualReputation(item.LinkedTechnique).ToString()), this.CommentsText.NegativeColor);
                                }
                                this.CommentsText.AddNewLine();
                                if (this.ShowingFaction.TotalTechniquePoint >= (this.ShowingFaction == null ? item.LinkedTechnique.PointCost : this.ShowingFaction.getTechniqueActualPointCost(item.LinkedTechnique)))
                                {
                                    this.CommentsText.AddText("技巧点数：" + (this.ShowingFaction == null ? item.LinkedTechnique.PointCost.ToString() : this.ShowingFaction.getTechniqueActualPointCost(item.LinkedTechnique).ToString()), this.CommentsText.PositiveColor);
                                }
                                else
                                {
                                    this.CommentsText.AddText("技巧点数：" + (this.ShowingFaction == null ? item.LinkedTechnique.PointCost.ToString() : this.ShowingFaction.getTechniqueActualPointCost(item.LinkedTechnique).ToString()), this.CommentsText.NegativeColor);
                                }
                                this.CommentsText.AddNewLine();
                                if (this.UpgradingArchitecture.Fund >= (this.ShowingFaction == null ? item.LinkedTechnique.FundCost : this.ShowingFaction.getTechniqueActualFundCost(item.LinkedTechnique)))
                                {
                                    this.CommentsText.AddText("资金：" + (this.ShowingFaction == null ? item.LinkedTechnique.FundCost.ToString() : this.ShowingFaction.getTechniqueActualFundCost(item.LinkedTechnique).ToString()), this.CommentsText.PositiveColor);
                                }
                                else
                                {
                                    this.CommentsText.AddText("资金：" + (this.ShowingFaction == null ? item.LinkedTechnique.FundCost.ToString() : this.ShowingFaction.getTechniqueActualFundCost(item.LinkedTechnique).ToString()), this.CommentsText.NegativeColor);
                                }
                                if (item.LinkedTechnique.PreID >= 0)
                                {
                                    this.CommentsText.AddNewLine();
                                    if (this.ShowingFaction.HasTechnique(item.LinkedTechnique.PreID))
                                    {
                                        this.CommentsText.AddText("前提条件：" + this.ShowingFaction.Scenario.GameCommonData.AllTechniques.GetTechnique(item.LinkedTechnique.PreID).Name, this.CommentsText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.CommentsText.AddText("前提条件：" + this.ShowingFaction.Scenario.GameCommonData.AllTechniques.GetTechnique(item.LinkedTechnique.PreID).Name, this.CommentsText.NegativeColor);
                                    }
                                }
                                if (item.LinkedTechnique.Conditions.Count > 0)
                                {
                                    foreach (Condition c in item.LinkedTechnique.Conditions.Conditions.Values) 
                                    {
                                        this.CommentsText.AddNewLine();
                                        if (c.CheckCondition(this.ShowingFaction))
                                        {
                                            this.CommentsText.AddText("条件：" + c.Name, this.CommentsText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.CommentsText.AddText("条件：" + c.Name, this.CommentsText.NegativeColor);
                                        }
                                    }

                                }
                                
                                if (this.ShowingFaction.UpgradingTechnique >= 0)
                                {
                                    this.CommentsText.AddNewLine();
                                    this.CommentsText.AddText("已经有技巧正在升级中", this.CommentsText.NegativeColor);
                                }
                            }
                            this.CommentsText.AddNewLine();
                        }
                        this.CommentsText.ResortTexts();
                        this.current = item;
                    }
                    flag = true;
                }
            }
            if (!flag)
            {
                this.current = null;
                this.CommentsText.Clear();
            }
        }

        private void screen_OnMouseRightUp(Microsoft.Xna.Framework.Point position)
        {
            this.IsShowing = false;
        }

        internal void SetArchitecture(Architecture architecture)
        {
            this.UpgradingArchitecture = architecture;
        }

        internal void SetFaction(Faction faction, bool control)
        {
            this.ShowingFaction = faction;
            this.Control = control;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(this, text.PropertyName).ToString();
            }
            this.AllTechniques.Clear();

            Dictionary<Microsoft.Xna.Framework.Point, Technique> showTechniques = new Dictionary<Microsoft.Xna.Framework.Point, Technique>();
            foreach (Technique technique in faction.Scenario.GameCommonData.AllTechniques.Techniques.Values)
            {
                Microsoft.Xna.Framework.Point p = new Microsoft.Xna.Framework.Point(technique.DisplayRow, technique.DisplayCol);
                //try
                //{

                if (showTechniques.ContainsKey(p))
                {
                    Technique existTech = showTechniques[p];
                    if (technique.PreID < 0)
                    {
                        showTechniques[p] = technique;
                    }
                    else if (technique.PreID == existTech.ID && faction.HasTechnique(existTech.ID))
                    {
                        showTechniques[p] = technique;
                    }
                }
                else
                {
                    //}
                    //catch (KeyNotFoundException ex)
                    //{
                    showTechniques[p] = technique;
                    //}
                }

            }

            foreach (Technique technique in showTechniques.Values)
            {
                TechniqueItem item = new TechniqueItem {
                    LinkedTechnique = technique,
                    Text = new FreeText(this.graphicsDevice, this.ButtonTextFont, this.ButtonTextColor)
                };
                item.Text.Position = new Microsoft.Xna.Framework.Rectangle(this.ButtonSize.X * item.Col, this.ButtonSize.Y * item.Row, this.ButtonSize.X, this.ButtonSize.Y);
                item.Text.Align = this.ButtonTextAlign;
                item.Text.Text = technique.Name;
                this.AllTechniques.Add(item);
            }
        }

        internal void SetPosition(ShowPosition showPosition)
        {
            Microsoft.Xna.Framework.Rectangle rectDes = new Microsoft.Xna.Framework.Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
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
            this.DisplayOffset = new Microsoft.Xna.Framework.Point(rect.X, rect.Y);
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            foreach (TechniqueItem item in this.AllTechniques)
            {
                item.Text.DisplayOffset = new Microsoft.Xna.Framework.Point(this.ButtonStartPosition.X + this.DisplayOffset.X, this.ButtonStartPosition.Y + this.DisplayOffset.Y);
            }
            this.CommentsText.DisplayOffset = new Microsoft.Xna.Framework.Point(this.DisplayOffset.X + this.CommentsClient.X, this.DisplayOffset.Y + this.CommentsClient.Y);
        }

        public string ArchitectureFund
        {
            get
            {
                if (this.Control)
                {
                    return this.UpgradingArchitecture.Fund.ToString();
                }
                return "----";
            }
        }

        public string ArchitectureName
        {
            get
            {
                if (UpgradingArchitecture == null)
                {
                    return "";
                }
                else
                {
                    return this.UpgradingArchitecture.Name;
                }
            }
        }

        private Microsoft.Xna.Framework.Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Microsoft.Xna.Framework.Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        public string FactionName
        {
            get
            {
                return this.ShowingFaction.Name;
            }
        }

        public string FactionReputation
        {
            get
            {
                return this.ShowingFaction.Reputation.ToString();
            }
        }

        public string FactionTotalTechniquePoint
        {
            get
            {
                if (this.Control)
                {
                    return this.ShowingFaction.TotalTechniquePoint.ToString();
                }
                return "----";
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
                    this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.FactionTechniques));
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                }
                else
                {
                    if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.SubDialog)
                    {
                        throw new Exception("The UndoneWork is not a SubDialog.");
                    }
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    this.CommentsText.Clear();
                }
            }
        }

        internal class TechniqueItem
        {
            internal Technique LinkedTechnique;
            internal FreeText Text;

            internal int Col
            {
                get
                {
                    return this.LinkedTechnique.DisplayCol;
                }
            }

            internal Microsoft.Xna.Framework.Rectangle Position
            {
                get
                {
                    return this.Text.DisplayPosition;
                }
            }

            internal int Row
            {
                get
                {
                    return this.LinkedTechnique.DisplayRow;
                }
            }
        }
    }
}

