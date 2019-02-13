namespace PersonDetailPlugin
{
    using GameFreeText;
    using GameGlobal;
    using GameManager;
    using GameObjects;
    using GameObjects.Conditions;
    using GameObjects.Influences;
    using GameObjects.PersonDetail;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    internal class PersonDetail
    {
        internal FreeTextList AllSkillTexts;
        internal Point BackgroundSize;
        internal PlatformTexture BackgroundTexture;
        internal Rectangle BiographyClient;
        internal FreeRichText BiographyText = new FreeRichText();
        internal FreeText CalledNameText;
        internal Rectangle ConditionClient;
        internal FreeRichText ConditionText = new FreeRichText();
        private object current;
        private Point DisplayOffset;
        internal FreeText GivenNameText;
        internal Rectangle InfluenceClient;
        internal FreeRichText InfluenceText = new FreeRichText();
        private bool isShowing;
        internal List<LabelText> LabelTexts = new List<LabelText>();
        internal FreeTextList LearnableSkillTexts;
        internal List<Skill> LinkedSkills = new List<Skill>();
        internal Rectangle TitleClient;
        // internal Rectangle GuanzhiClient; //官职
        //internal FreeRichText GuanzhiText = new FreeRichText();
        internal FreeRichText TitleText = new FreeRichText();
        internal FreeTextList PersonSkillTexts;
        internal Rectangle PortraitClient;
        internal Screen screen;
        internal Person ShowingPerson;
        internal Point SkillBlockSize;
        internal Point SkillDisplayOffset;
        internal Rectangle StuntClient;
        internal FreeRichText StuntText = new FreeRichText();
        internal FreeText SurNameText;


        internal void Draw()
        {
            if (this.ShowingPerson != null)
            {
                Rectangle? sourceRectangle = null;
                CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                try
                {
                    CacheManager.DrawZhsanAvatar(this.ShowingPerson, "", this.PortraitDisplayPosition, Color.White, 0.199f);
                }
                catch
                {
                }
                this.SurNameText.Draw(0.1999f);
                this.GivenNameText.Draw(0.1999f);
                this.CalledNameText.Draw(0.1999f);
                foreach (LabelText text in this.LabelTexts)
                {
                    text.Label.Draw(0.1999f);
                    text.Text.Draw(0.1999f);
                }
                this.TitleText.Draw(0.1999f);
                //this.GuanzhiText.Draw(spriteBatch, 0.1999f);
                this.AllSkillTexts.Draw((float)0.1999f);
                this.PersonSkillTexts.Draw((float)0.1998f);
                this.LearnableSkillTexts.Draw((float)0.1998f);
                this.StuntText.Draw(0.1999f);
                this.InfluenceText.Draw(0.1999f);
                this.ConditionText.Draw(0.1999f);
                this.BiographyText.Draw(0.1999f);
            }
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (StaticMethods.PointInRectangle(position, new Rectangle(this.BiographyText.DisplayOffset.X, this.BiographyText.DisplayOffset.Y, this.BiographyClient.Width, this.BiographyClient.Height)))
            {
                if (this.BiographyText.CurrentPageIndex < (this.BiographyText.PageCount - 1))
                {
                    this.BiographyText.NextPage();
                }
                else if (this.BiographyText.CurrentPageIndex == (this.BiographyText.PageCount - 1))
                {
                    this.BiographyText.FirstPage();
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            bool flag = false;
            if (!flag && StaticMethods.PointInRectangle(position, this.TitleDisplayPosition) && this.TitleText.RowHeight > 0)
            {
                int num2 = (position.Y - this.TitleText.DisplayOffset.Y) / this.TitleText.RowHeight;
                if (num2 >= 0)
                {
                    int num3 = num2;
                    if (this.ShowingPerson.Titles.Count > num3)
                    {
                        Title title = this.ShowingPerson.Titles[num3] as Title;
                        if (title != null)
                        {
                            if (this.current != title)
                            {
                                this.BiographyText.Clear();
                                this.InfluenceText.Clear();
                                //阿柒:增加根据称号等级设定不同字体颜色
                                Color titleColor = Color.White;
                                if (title.Level < 4)
                                {
                                    titleColor = Color.AliceBlue;
                                }
                                else if (title.Level >= 4 && title.Level < 7)
                                {
                                    titleColor = Color.YellowGreen;
                                }
                                else if (title.Level >= 7 && title.Level < 10)
                                {
                                    titleColor = Color.LightSkyBlue;
                                }
                                else if (title.Level >= 10 && title.Level < 13)
                                {
                                    titleColor = Color.Violet;
                                }
                                else
                                {
                                    titleColor = Color.Orange;
                                }
                                this.InfluenceText.AddText(title.DetailedName, titleColor);
                                this.InfluenceText.AddNewLine();
                                foreach (Influence influence in title.Influences.Influences.Values)
                                {
                                    //阿柒:根据影响种类设定不同颜色
                                    if (influence.Kind.ID == 280 || influence.Kind.ID == 281 || influence.Kind.ID == 285 || influence.Kind.ID == 290 || influence.Kind.ID == 300)
                                    {
                                        this.InfluenceText.AddText(influence.Description, Color.Moccasin);
                                    }
                                    else
                                    {
                                        this.InfluenceText.AddText(influence.Description);
                                    }

                                    this.InfluenceText.AddNewLine();
                                }
                                this.InfluenceText.ResortTexts();
                                this.ConditionText.Clear();
                                this.ConditionText.AddText("修习条件", this.ConditionText.TitleColor);
                                this.ConditionText.AddNewLine();
                                foreach (Condition condition in title.Conditions.Conditions.Values)
                                {
                                    if (condition.CheckCondition(this.ShowingPerson))
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                    }
                                    this.ConditionText.AddNewLine();
                                }
                                foreach (Condition condition in title.ArchitectureConditions.Conditions.Values)
                                {
                                    if (this.ShowingPerson.LocationArchitecture != null && condition.CheckCondition(this.ShowingPerson.LocationArchitecture))
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                    }
                                    this.ConditionText.AddNewLine();
                                }
                                foreach (Condition condition in title.FactionConditions.Conditions.Values)
                                {
                                    if (this.ShowingPerson.BelongedFaction != null && condition.CheckCondition(this.ShowingPerson.BelongedFaction))
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                    }
                                    this.ConditionText.AddNewLine();
                                }

                                this.ConditionText.ResortTexts();
                                this.current = title;
                            }
                            flag = true;
                        }
                    }
                }
            }
            /* if (!flag && StaticMethods.PointInRectangle(position, this.GuanzhiDisplayPosition))
             {
                 int num2 = (position.Y - this.GuanzhiText.DisplayOffset.Y / this.GuanzhiText.RowHeight);
                 if (num2 > 1)
                 {
                     int num3 = num2 - 2;
                     if (this.ShowingPerson.Guanzhis.Count > num3)
                     {
                         Guanzhi guanzhi = this.ShowingPerson.Guanzhis[num3] as Guanzhi;
                         if (guanzhi != null)
                         {
                             if (this.current != guanzhi)
                             {
                                 this.BiographyText.Clear();
                                 this.InfluenceText.Clear();
                                 this.InfluenceText.AddText(guanzhi.DetailedName, this.InfluenceText.TitleColor);
                                 this.InfluenceText.AddNewLine();
                                 foreach (Influence influence in guanzhi.Influences.Influences.Values)
                                 {
                                     this.InfluenceText.AddText(influence.Description);
                                     this.InfluenceText.AddNewLine();
                                 }
                                 this.InfluenceText.ResortTexts();
                                 this.ConditionText.Clear();
                                 this.ConditionText.AddText("授予条件", this.ConditionText.TitleColor);
                                 this.ConditionText.AddNewLine();
                                 foreach (Condition condition in guanzhi.Conditions.Conditions.Values)
                                 {
                                     if (condition.CheckCondition(this.ShowingPerson))
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                     }
                                     else
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                     }
                                     this.ConditionText.AddNewLine();
                                 }
                                 foreach (Condition condition in guanzhi.LoseConditions.Conditions.Values)
                                 {
                                     if (condition.CheckCondition(this.ShowingPerson))
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                     }
                                     else
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                     }
                                     this.ConditionText.AddNewLine();
                                 }
                                 foreach (Condition condition in guanzhi.FactionConditions.Conditions.Values)
                                 {
                                     if (this.ShowingPerson.BelongedFaction != null && condition.CheckCondition(this.ShowingPerson.BelongedFaction))
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                     }
                                     else
                                     {
                                         this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                     }
                                     this.ConditionText.AddNewLine();
                                 }

                                 this.ConditionText.ResortTexts();
                                 this.current = guanzhi;
                             }
                             flag = true;
                         }
                     }
                 }
             }*/

            if (!flag && StaticMethods.PointInRectangle(position, this.StuntDisplayPosition) && this.StuntText.RowHeight > 0)
            {
                int num2 = (position.Y - this.StuntText.DisplayOffset.Y) / this.StuntText.RowHeight;
                if (num2 > -1)
                {
                    int num3 = num2;
                    if (this.ShowingPerson.Stunts.Count > num3)
                    {
                        Stunt stunt = this.ShowingPerson.Stunts.GetStuntList()[num3] as Stunt;
                        if (stunt != null)
                        {
                            if (this.current != stunt)
                            {
                                this.BiographyText.Clear();
                                this.InfluenceText.Clear();
                                this.InfluenceText.AddText("战斗特技", this.InfluenceText.TitleColor);
                                this.InfluenceText.AddText(stunt.Name, this.InfluenceText.SubTitleColor);
                                this.InfluenceText.AddNewLine();
                                this.InfluenceText.AddText("持续天数", this.InfluenceText.SubTitleColor2);
                                this.InfluenceText.AddText((stunt.Period * Session.Parameters.DayInTurn).ToString(), this.InfluenceText.SubTitleColor3);
                                this.InfluenceText.AddText("天", this.InfluenceText.SubTitleColor2);
                                this.InfluenceText.AddNewLine();
                                foreach (Influence influence in stunt.Influences.Influences.Values)
                                {
                                    this.InfluenceText.AddText(influence.Description);
                                    this.InfluenceText.AddNewLine();
                                }
                                this.InfluenceText.ResortTexts();
                                this.ConditionText.Clear();
                                this.ConditionText.AddText("使用条件", this.ConditionText.TitleColor);
                                this.ConditionText.AddNewLine();
                                if ((this.ShowingPerson.LocationTroop != null) && (this.ShowingPerson == this.ShowingPerson.LocationTroop.Leader))
                                {
                                    foreach (Condition condition in stunt.CastConditions.Conditions.Values)
                                    {
                                        if (condition.CheckCondition(this.ShowingPerson.LocationTroop))
                                        {
                                            this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                        }
                                        this.ConditionText.AddNewLine();
                                    }
                                }
                                else
                                {
                                    foreach (Condition condition in stunt.CastConditions.Conditions.Values)
                                    {
                                        this.ConditionText.AddText(condition.Name);
                                        this.ConditionText.AddNewLine();
                                    }
                                }
                                this.ConditionText.AddNewLine();
                                this.ConditionText.AddText("修习条件", this.ConditionText.SubTitleColor);
                                this.ConditionText.AddNewLine();
                                foreach (Condition condition in stunt.LearnConditions.Conditions.Values)
                                {
                                    if (condition.CheckCondition(this.ShowingPerson))
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                    }
                                    this.ConditionText.AddNewLine();
                                }
                                this.ConditionText.ResortTexts();
                                this.current = stunt;
                            }
                            flag = true;
                        }
                    }
                }
            }
            if (!flag)
            {
                for (int i = 0; i < this.AllSkillTexts.Count; i++)
                {
                    if (StaticMethods.PointInRectangle(position, this.AllSkillTexts[i].AlignedPosition))
                    {
                        if (this.current != this.LinkedSkills[i])
                        {
                            this.BiographyText.Clear();
                            this.InfluenceText.Clear();
                            if (this.LinkedSkills[i].InfluenceCount > 0)
                            {
                                this.InfluenceText.AddText("技能", this.InfluenceText.TitleColor);
                                this.InfluenceText.AddText(this.LinkedSkills[i].Name, this.InfluenceText.SubTitleColor);
                                this.InfluenceText.AddNewLine();
                                foreach (Influence influence in this.LinkedSkills[i].Influences.Influences.Values)
                                {
                                    //阿柒:根据影响种类设定不同颜色
                                    if (influence.Kind.ID == 280 || influence.Kind.ID == 281 || influence.Kind.ID == 285 || influence.Kind.ID == 290 || influence.Kind.ID == 300)
                                    {
                                        this.InfluenceText.AddText(influence.Description, Color.Moccasin);
                                    }
                                    else
                                    {
                                        this.InfluenceText.AddText(influence.Description);
                                    }
                                    this.InfluenceText.AddNewLine();
                                }
                                this.InfluenceText.ResortTexts();
                                this.ConditionText.Clear();
                                this.ConditionText.AddText("修习条件", this.ConditionText.TitleColor);
                                this.ConditionText.AddNewLine();
                                foreach (Condition condition in this.LinkedSkills[i].Conditions.Conditions.Values)
                                {
                                    if (condition.CheckCondition(this.ShowingPerson))
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                    }
                                    else
                                    {
                                        this.ConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                    }
                                    this.ConditionText.AddNewLine();
                                }
                                this.ConditionText.ResortTexts();
                            }
                            this.current = this.LinkedSkills[i];
                        }
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                if (this.current != null)
                {
                    this.current = null;
                    this.InfluenceText.Clear();
                    this.ConditionText.Clear();
                    if (this.ShowingPerson.PersonBiography != null)
                    {
                        this.BiographyText.Clear();
                        this.BiographyText.AddText("列传", this.BiographyText.TitleColor);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.BriefIntro);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.RomancingIntro);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.HistoricalIntro);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText("剧本", this.BiographyText.SubTitleColor2);
                        this.BiographyText.AddText("：");
                        String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                        foreach (String s in lineBrokenText)
                        {
                            this.BiographyText.AddText(s);
                            this.BiographyText.AddNewLine();
                        }
                        this.BiographyText.ResortTexts();
                    }
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            this.IsShowing = false;
        }

        internal void SetPerson(Person person)
        {
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                Rectangle position = new Rectangle(this.SkillDisplayOffset.X + (skill.DisplayCol * this.SkillBlockSize.X), this.SkillDisplayOffset.Y + (skill.DisplayRow * this.SkillBlockSize.Y), this.SkillBlockSize.X, this.SkillBlockSize.Y);
                this.AllSkillTexts.AddText(skill.Name, position);
                this.LinkedSkills.Add(skill);
            }
            this.AllSkillTexts.ResetAllAlignedPositions();

            this.ShowingPerson = person;
            this.SurNameText.Text = person.LastName;
            this.GivenNameText.Text = person.FirstName;
            this.CalledNameText.Text = person.CalledName;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(person, text.PropertyName).ToString();
            }
            this.TitleText.Clear();
            foreach (Title title in person.Titles)
            {
                if (title != null)
                {
                    //阿柒:根据称号等级设定不同颜色
                    if (title.Level < 4)
                    {
                        this.TitleText.AddText("  " + title.DetailedName, Color.AliceBlue);
                    }
                    else if (title.Level >= 4 && title.Level < 7)
                    {
                        this.TitleText.AddText("  " + title.DetailedName, Color.YellowGreen);
                    }
                    else if (title.Level >= 7 && title.Level < 10)
                    {
                        this.TitleText.AddText("  " + title.DetailedName, Color.LightSkyBlue);
                    }
                    else if (title.Level >= 10 && title.Level < 13)
                    {
                        this.TitleText.AddText(title.DetailedName, Color.Violet);
                    }
                    else
                    {
                        this.TitleText.AddText(title.DetailedName, Color.Orange);
                    }

                }
                //this.TitleText.AddText(title.DetailedName, Color.DarkSlateBlue);
                this.TitleText.AddNewLine();
            }
            this.TitleText.ResortTexts();

            // this.GuanzhiText.Clear();
            /* foreach (Guanzhi guanzhi in person.Guanzhis)
             {
                 this.GuanzhiText.AddText(guanzhi.DetailedName, Color.Lime);
                 this.GuanzhiText.AddNewLine();
             }
             this.GuanzhiText.ResortTexts();
             */
            this.PersonSkillTexts.SimpleClear();
            this.LearnableSkillTexts.SimpleClear();
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                Rectangle position = new Rectangle(this.SkillDisplayOffset.X + (skill.DisplayCol * this.SkillBlockSize.X), this.SkillDisplayOffset.Y + (skill.DisplayRow * this.SkillBlockSize.Y), this.SkillBlockSize.X, this.SkillBlockSize.Y);
                if (person.Skills.GetSkill(skill.ID) != null)
                {
                    this.PersonSkillTexts.AddText(skill.Name, position);
                }
                else if (skill.CanLearn(person))
                {
                    this.LearnableSkillTexts.AddText(skill.Name, position);
                }
            }
            this.PersonSkillTexts.ResetAllAlignedPositions();
            this.LearnableSkillTexts.ResetAllAlignedPositions();
            this.StuntText.Clear();
            //阿柒:特技显示效果修改,去掉多余的字
            //this.StuntText.AddText("战斗特技", Color.Yellow);
            //this.StuntText.AddNewLine();
            //this.StuntText.AddText(person.Stunts.Count.ToString() + "种", Color.Lime);
            //this.StuntText.AddNewLine();
            foreach (Stunt stunt in person.Stunts.Stunts.Values)
            {
                this.StuntText.AddText(stunt.Name, Color.Khaki);
                this.StuntText.AddText(" 战意消耗" + stunt.Combativity.ToString(), Color.SkyBlue);
                this.StuntText.AddNewLine();
            }
            this.StuntText.ResortTexts();
            this.BiographyText.Clear();
            if (person.PersonBiography != null)
            {
                this.BiographyText.Clear();
                this.BiographyText.AddText("列传", this.BiographyText.TitleColor);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.BriefIntro);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.RomancingIntro);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.HistoricalIntro);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText("剧本", this.BiographyText.SubTitleColor2);
                this.BiographyText.AddText("：");
                String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                foreach (String s in lineBrokenText)
                {
                    this.BiographyText.AddText(s);
                    this.BiographyText.AddNewLine();
                }
                this.BiographyText.ResortTexts();
            }
        }

        internal void SetPosition(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
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
            this.DisplayOffset = new Point(rect.X, rect.Y);
            this.SurNameText.DisplayOffset = this.DisplayOffset;
            this.GivenNameText.DisplayOffset = this.DisplayOffset;
            this.CalledNameText.DisplayOffset = this.DisplayOffset;
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.TitleText.DisplayOffset = new Point(this.DisplayOffset.X + this.TitleClient.X, this.DisplayOffset.Y + this.TitleClient.Y);
            // this.GuanzhiText.DisplayOffset = new Point(this.DisplayOffset.X + this.GuanzhiClient.X, this.DisplayOffset.Y + this.GuanzhiClient.Y);
            this.AllSkillTexts.DisplayOffset = this.DisplayOffset;
            this.PersonSkillTexts.DisplayOffset = this.DisplayOffset;
            this.LearnableSkillTexts.DisplayOffset = this.DisplayOffset;
            this.StuntText.DisplayOffset = new Point(this.DisplayOffset.X + this.StuntClient.X, this.DisplayOffset.Y + this.StuntClient.Y);
            this.InfluenceText.DisplayOffset = new Point(this.DisplayOffset.X + this.InfluenceClient.X, this.DisplayOffset.Y + this.InfluenceClient.Y);
            this.ConditionText.DisplayOffset = new Point(this.DisplayOffset.X + this.ConditionClient.X, this.DisplayOffset.Y + this.ConditionClient.Y);
            this.BiographyText.DisplayOffset = new Point(this.DisplayOffset.X + this.BiographyClient.X, this.DisplayOffset.Y + this.BiographyClient.Y);
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
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
                    this.screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.PersonDetail));
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                }
                else
                {
                    if (this.screen.PopUndoneWork().Kind != UndoneWorkKind.SubDialog)
                    {
                        throw new Exception("The UndoneWork is not a SubDialog.");
                    }
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.current = null;
                    this.InfluenceText.Clear();
                    this.ConditionText.Clear();
                }
            }
        }

        private Rectangle PortraitDisplayPosition
        {
            get
            {
                return new Rectangle(this.PortraitClient.X + this.DisplayOffset.X, this.PortraitClient.Y + this.DisplayOffset.Y, this.PortraitClient.Width, this.PortraitClient.Height);
            }
        }

        private Rectangle TitleDisplayPosition
        {
            get
            {
                return new Rectangle(this.TitleText.DisplayOffset.X, this.TitleText.DisplayOffset.Y, this.TitleText.ClientWidth, this.TitleText.ClientHeight);
            }
        }
        /*
        private Rectangle GuanzhiDisplayPosition
        {
            get
            {
                return new Rectangle(this.GuanzhiText.DisplayOffset.X, this.GuanzhiText.DisplayOffset.Y, this.GuanzhiText.ClientWidth, this.GuanzhiText.ClientHeight);
            }
        }
        */
        private Rectangle StuntDisplayPosition
        {
            get
            {
                return new Rectangle(this.StuntText.DisplayOffset.X, this.StuntText.DisplayOffset.Y, this.StuntText.ClientWidth, this.StuntText.ClientHeight);
            }
        }
    }
}

