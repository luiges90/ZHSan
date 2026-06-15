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
    using Platforms;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        internal Rectangle TreasureTextClient;
        internal FreeRichText TreasureText = new FreeRichText();

        internal FreeText statusEffectText;

        internal Rectangle MoreMessageBGClient;
        internal Point MoreMessageBGSize;
        internal PlatformTexture MoreMessageBGTexture;
        internal Rectangle MoreMessageClient;
        internal FreeRichText MoreMessageText = new FreeRichText();
        public string Switch_DisplayFamily = "";
        public string Switch_MoreMessage = "";
        public string Switch_PersonPortraitL = "";
        internal Rectangle PortraitL;
        public string Switch_PersonBG = "";
        public string Switch_PersonBG2 = "";
        public string Switch_EnlargeBG = "";
        //public string Switch_Fuhuo = "";

        internal PlatformTexture SkillBG;
        internal Rectangle SkillBGClient;
        internal Point SkillBGSize;
        internal Rectangle Stunt0ID0Client;
        internal Rectangle Stunt9ID9Client;
        internal PlatformTexture StuntBG;
        internal Rectangle StuntBGClient;
        internal Point StuntBGSize;
        internal PlatformTexture TitleBG;
        internal Rectangle TitleBGClient;
        internal Point TitleBGSize;
        internal PlatformTexture TreasureBG;
        internal Rectangle TreasureBGClient;
        internal Point TreasureBGSize;
        private List<int> TreasureGroup;

        internal PlatformTexture FuhuoBG;
        internal Rectangle FuhuoBGClient;
        internal Point FuhuoBGSize;

        internal PlatformTexture PersonBG;
        internal Rectangle PersonBGClient;
        internal Point PersonBGSize;

        internal Rectangle Treasure10group70Client;
        internal Rectangle Treasure11group90Client;
        internal Rectangle Treasure12group100Client;
        internal Rectangle Treasure1group10Client;
        internal Rectangle Treasure2group15Client;
        internal Rectangle Treasure3group20Client;
        internal Rectangle Treasure4group25Client;
        internal Rectangle Treasure5group30Client;
        internal Rectangle Treasure6group40Client;
        internal Rectangle Treasure7group50Client;
        internal Rectangle Treasure8group55Client;
        internal Rectangle Treasure9group60Client;

        internal Person IDN;
        private string ThePersonSound;
        internal string Switch3;
        private int CurrentTreasureGID;


        public PersonDetail()
        {
            List<int> list = new List<int> {
            4000,
            4100,
            3214,
            1100,
            4700,
            3200,
            1200,
            1300,
            1700,
            300,
            2900,
            2001,
            };
            this.TreasureGroup = list;

        }


        internal void Draw()
        {
            if (this.ShowingPerson != null)
            {
                List<int>.Enumerator enumerator3;//TreasureGroup
                Rectangle? sourceRectangle = null;
                CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                try
                {
                    CacheManager.DrawZhsanAvatar(this.ShowingPerson, this.PortraitDisplayPosition, 0.199f);
                }
                catch
                {
                }
                this.SurNameText.Draw(0.1999f);
                this.GivenNameText.Draw(0.1999f);
                this.CalledNameText.Draw(0.1999f);
                statusEffectText.Draw(0.1999f);

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
                this.TreasureText.Draw(0.1999f);
                CacheManager.Draw(this.TreasureBG, this.TreasureBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1988f);
                CacheManager.Draw(this.TitleBG, this.TitleBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1988f);
               // CacheManager.Draw(this.StuntBG, this.StuntBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);
               // CacheManager.Draw(this.SkillBG, this.SkillBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);            
                CacheManager.Draw(this.PersonBG, this.PersonBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.21f);
                if (this.ShowingPerson.Alive == false)
                {
                    CacheManager.Draw(this.FuhuoBG, this.FuhuoBGClientDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.19f);
                }
                
                if (this.Switch_MoreMessage == "on")
                {
                    this.MoreMessageText.Draw(0.199f);
                    CacheManager.Draw(this.MoreMessageBGTexture, this.MoreMessageBGDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1997f);
                }
                foreach (Treasure treasure in this.ShowingPerson.AllEffectiveTreasures)
                {
                    using (enumerator3 = this.TreasureGroup.GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            int current = enumerator3.Current;
                            if (treasure.TreasureGroup == current)
                            {
                                CacheManager.DrawTreasure(treasure, this.TreasuresClientDisplayPosition(current), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1987f);
                                
                            }
                        }
                    }
                }
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
            
            if ((this.Switch_MoreMessage == "off") && StaticMethods.PointInRectangle(position, this.MoreMessageBGDisplayPosition))
            {
                this.ShowingPerson.PictureIndex = Convert.ToInt32(this.ShowingPerson.PictureIndex);
                //this.ShowingPerson.PictureIndexString = null;
                this.MoreMessageText.Clear();
                this.MoreMessageText.AddText("兵科经验", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("步兵：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.BubingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("弓弩：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.NubingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("骑兵：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.QibingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("水军：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.ShuijunExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("器械：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.QixieExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("其他经验", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("计略：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.StratagemExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("策略：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.TacticsExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("内政：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.InternalExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();

                this.MoreMessageText.AddText("疲劳：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.Tiredness.ToString(), Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("善名：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.Karma.ToString(), Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("功绩：", this.MoreMessageText.TitleColor);               
                this.MoreMessageText.AddText(this.ShowingPerson.OfficerMerit.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("ID图片：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.ID + "-" + this.ShowingPerson.PictureIndex, Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                if (this.Switch_DisplayFamily == "on")
                {
                    this.MoreMessageText.AddText("生卒年：", this.MoreMessageText.TitleColor);
                    this.MoreMessageText.AddText(this.ShowingPerson.YearBorn + "-" + this.ShowingPerson.YearDead, Color.WhiteSmoke);
                    this.MoreMessageText.AddNewLine();
                }
                this.MoreMessageText.AddText("家族 ", this.MoreMessageText.TitleColor);
                //if (this.Switch_DisplayFamily == "on")
                //{
                //    this.MoreMessageText.AddText(this.ShowingPerson.PersonFamily, Color.Orange);
                //}
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("父：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.FatherName, Color.SkyBlue);
                this.MoreMessageText.AddText(" 母：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.MotherName, Color.SandyBrown);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("配偶：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.SpouseName, Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("兄弟姐妹：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[0], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[1], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("子女：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[2], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[3], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("孙：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[4], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[5], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("亲近：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.ClosePersonsName, Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.ResortTexts();
                this.Switch_MoreMessage = "on";

            }
            else if ((this.Switch_MoreMessage == "on") && StaticMethods.PointInRectangle(position, this.MoreMessageBGDisplayPosition))
            {                
                this.MoreMessageText.Clear();
                this.Switch_MoreMessage = "off";
               
            }
           
            if (StaticMethods.PointInRectangle(position, this.PersonBGClientDisplayPosition) && this.Switch_PersonBG == "off")
            {
                this.Switch_PersonBG = "on";
                ThePersonSound = Platform.Current.GetPersonVioce(IDN, "");
                //p = person;              
                if (this.ThePersonSound.Length > 0 && Switch3 == "on")
                { 
                    this.screen.PlayNormalSound(this.ThePersonSound); 
                }

            }
            else if (this.Switch_PersonBG == "on" && StaticMethods.PointInRectangle(position, this.PersonBGClientDisplayPosition))
            {
                this.Switch_PersonBG = "off";
            }

            if (StaticMethods.PointInRectangle(position, this.FuhuoBGClientDisplayPosition)&& !this.ShowingPerson.Alive)
            {
                this.ShowingPerson.Alive = true; 
                this.ShowingPerson.YearDead = Session.Current.Scenario.Date.Year + 10;
                this.ShowingPerson.BeAvailable();
                this.ShowingPerson.PersonBiography.InGame=Session.Current.Scenario.Date.ToString()+"，因奇怪的力量复活了。"+ "\u000a"+ this.ShowingPerson.PersonBiography.InGame;
            }
            if (StaticMethods.PointInRectangle(position, this.TreasureBGClientDisplayPosition))
            {
                for (int num = 0; num < this.TreasureGroup.Count; num++)
                {
                    if (this.ShowingPerson.BelongedFaction != null && StaticMethods.PointInRectangle(position, this.TreasuresClientDisplayPosition(this.TreasureGroup[num])))
                    {
                       
                        this.CurrentTreasureGID = (TreasureGroup[num]);
                        this.TreasureText.Clear();
                        //this.TreasureText.AddText("可赏赐宝物列表", Color.Khaki);
                        //this.TreasureText.AddNewLine();
                        foreach (Treasure t in this.ShowingPerson.BelongedFaction.Leader.Treasures)
                        {
                            if (t.TreasureGroup == TreasureGroup[num])
                            {
                                this.TreasureText.AddText(t.Name, Color.Khaki);
                                this.TreasureText.AddText(t.InfluenceString, Color.SkyBlue);
                                this.TreasureText.AddNewLine();
                            }
                        }
                        this.TreasureText.ResortTexts();

                    }
                }
            }
            if (StaticMethods.PointInRectangle(position, this.MoreMessageBGDisplayPosition2) && this.TreasureText.RowHeight > 0)
            {
                int num2 = (position.Y - this.MoreMessageBGDisplayPosition2.Y) / this.TreasureText.RowHeight;
                if (num2 > -1)
                {
                    int num3 = num2;
                    if (this.ShowingPerson.BelongedFaction != null && this.ShowingPerson.BelongedFaction.Leader.TreasureListforGroup(CurrentTreasureGID).Count > num3)
                    {
                        Treasure treasure = this.ShowingPerson.BelongedFaction.Leader.TreasureListforGroup(CurrentTreasureGID)[num3] as Treasure;
                        foreach (Treasure t in this.ShowingPerson.TreasureListforGroup(CurrentTreasureGID))
                        {
                            this.ShowingPerson.LoseTreasure(t);
                            this.ShowingPerson.BelongedFaction.Leader.ReceiveTreasure(t);
                        }
                        this.ShowingPerson.BelongedFaction.Leader.LoseTreasure(treasure);
                        this.ShowingPerson.ReceiveTreasure(treasure);
                        this.TreasureText.Clear();
                        //this.TreasureText.AddText("可赏赐宝物列表", Color.Khaki);
                        //this.TreasureText.AddNewLine();
                        foreach (Treasure t in this.ShowingPerson.BelongedFaction.Leader.TreasureListforGroup(CurrentTreasureGID))
                        {                            
                                this.TreasureText.AddText(t.Name, Color.Khaki);
                                this.TreasureText.AddText(t.InfluenceString, Color.SkyBlue);
                                this.TreasureText.AddNewLine();                           
                        }
                        this.TreasureText.ResortTexts();
                    }
                }
        }
        }
        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            bool flag = false;
            if (!flag)
            {              
                for (int num = 0; num < this.TreasureGroup.Count; num++)              
                {
                    if (StaticMethods.PointInRectangle(position, this.TreasuresClientDisplayPosition(this.TreasureGroup[num])))
                    {
                        foreach (Treasure treasure in this.ShowingPerson.AllEffectiveTreasures)
                        {
                            if (treasure.TreasureGroup == this.TreasureGroup[num])
                            {
                                if (this.current != treasure)
                                {
                                    this.BiographyText.Clear();
                                    this.InfluenceText.Clear();
                                    this.InfluenceText.AddText(treasure.Name, Color.WhiteSmoke);
                                    this.InfluenceText.AddNewLine();
                                    this.InfluenceText.AddText("价值：" + treasure.Worth, Color.Orange);
                                    //this.InfluenceText.AddNewLine();
                                    //this.InfluenceText.AddText("类别：" + treasure.TreasureGroupName, Color.Orange);
                                    this.InfluenceText.AddNewLine();
                                    this.InfluenceText.AddText(treasure.Description, Color.Turquoise);
                                    this.InfluenceText.AddNewLine();
                                    foreach (var influence in treasure.Influences.Values)
                                    {
                                        if ((((influence.Kind.ID == 280) || (influence.Kind.ID == 0x119)) || ((influence.Kind.ID == 0x11d) || (influence.Kind.ID == 290))) || (influence.Kind.ID == 300))
                                        {
                                            this.InfluenceText.AddText(influence.Description, Color.Moccasin);
                                        }
                                        else
                                        {
                                            this.InfluenceText.AddText(influence.Description, Color.DeepSkyBlue);
                                        }
                                        this.InfluenceText.AddNewLine();
                                    }
                                    this.InfluenceText.ResortTexts();
                                    this.ConditionText.Clear();
                                    this.ConditionText.ResortTexts();
                                    this.current = treasure;
                                }
                                flag = true;
                            }
                        }
                    }
                }
            }
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
                                foreach (var influence in title.Influences.Values)
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
                                if (title.FundForHolder > 0)
                                {
                                    this.ConditionText.AddText("薪金：" + title.FundForHolder + "/月\n");
                                }
                                this.ConditionText.AddText("修习条件", this.ConditionText.TitleColor);
                                this.ConditionText.AddNewLine();
                                foreach (Condition condition in title.Conditions)
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
                                foreach (Condition condition in title.ArchitectureConditions)
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
                                foreach (Condition condition in title.FactionConditions)
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
                                    foreach (Condition condition in stunt.CastConditions)
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
                                    foreach (Condition condition in stunt.CastConditions)
                                    {
                                        this.ConditionText.AddText(condition.Name);
                                        this.ConditionText.AddNewLine();
                                    }
                                }
                                this.ConditionText.AddNewLine();
                                this.ConditionText.AddText("修习条件", this.ConditionText.SubTitleColor);
                                this.ConditionText.AddNewLine();
                                foreach (Condition condition in stunt.LearnConditions)
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
                                foreach (Influence influence in this.LinkedSkills[i].Influences.Values)
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
                                foreach (Condition condition in this.LinkedSkills[i].Conditions)
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
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Brief);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Romance);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                        this.BiographyText.AddNewLine();
                        this.BiographyText.AddText(this.ShowingPerson.PersonBiography.History);
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

            if (!flag && StaticMethods.PointInRectangle(position, this.GivenNameText.AlignedPosition) && this.Switch_PersonBG == "on")
            {
                this.Switch_EnlargeBG = "on";
                flag = true;
            }
            else { this.Switch_EnlargeBG = "off"; }
            if (!flag && StaticMethods.PointInRectangle(position, this.SurNameText.AlignedPosition) && this.Switch_PersonBG == "on")
            {
                this.Switch_PersonBG2 = "on";
                flag = true;
            }
            else { this.Switch_PersonBG2 = "off"; }

        }

        private void screen_OnMouseRightUp(Point position)
        {
            this.IsShowing = false;
        }

        internal void SetPerson(Person person)
        {
            this.AllSkillTexts.SimpleClear();
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                Rectangle position = new Rectangle(this.SkillDisplayOffset.X + (skill.DisplayCol * this.SkillBlockSize.X), this.SkillDisplayOffset.Y + (skill.DisplayRow * this.SkillBlockSize.Y), this.SkillBlockSize.X, this.SkillBlockSize.Y);
                this.AllSkillTexts.AddText(skill.Name, position);
                this.LinkedSkills.Add(skill);
            }
            this.AllSkillTexts.ResetAllAlignedPositions();

            this.ShowingPerson = person;
            this.SurNameText.Text = person.SurName;
            this.GivenNameText.Text = person.GivenName;
            this.CalledNameText.Text = person.CalledName;
            statusEffectText.Text = string.Join(" ", person.GetStatusEffects());

            foreach (LabelText text in this.LabelTexts)
            {
                text.Text.Text = StaticMethods.GetPropertyValue(person, text.PropertyName).ToString();
                int value = -1;
                switch (text.PropertyName)
                {
                    case "Strength":
                        value = person.Strength;
                        break;
                    case "Command":
                        value = person.Command;
                        break;
                    case "Intelligence":
                        value = person.Intelligence;
                        break;
                    case "Politics":
                        value = person.Politics;
                        break;
                    case "Glamour":
                        value = person.Glamour;
                        break;
                }
                if (value >= 0 && value <60)
                {
                    text.Text.TextColor = Color.White;
                }
                else if (value >= 60 && value < 70)
                {
                    text.Text.TextColor = Color.YellowGreen;
                }
                else if (value >= 70 && value < 80)
                {
                    text.Text.TextColor = Color.LightSkyBlue;
                }
                else if (value >= 80 && value < 90)
                {
                    text.Text.TextColor = Color.Violet;
                }
                else if (value >= 90)
                {
                    text.Text.TextColor = Color.Orange;
                }
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
            this.TreasureText.Clear();
            //this.TreasureText.AddText("君主宝物列表", Color.Khaki);
            //this.TreasureText.AddNewLine();
            //foreach (Treasure t in person.BelongedFaction.Leader.Treasures)
            //{
                
            //        this.TreasureText.AddText(t.Name, Color.Khaki);
            //        this.TreasureText.AddText(t.InfluencesString, Color.SkyBlue);
            //        this.TreasureText.AddNewLine();
               
            //}
            //this.TreasureText.ResortTexts();
            if (person.PersonBiography != null)
            {
                this.BiographyText.Clear();
                this.BiographyText.AddText("列传", this.BiographyText.TitleColor);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Brief);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Romance);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                this.BiographyText.AddNewLine();
                this.BiographyText.AddText(this.ShowingPerson.PersonBiography.History);
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

                this.MoreMessageText.Clear();
                this.MoreMessageText.AddText("兵科经验", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("步兵：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.BubingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("弓弩：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.NubingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("骑兵：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.QibingExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("水军：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.ShuijunExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("器械：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.QixieExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("其他经验", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("计略：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.StratagemExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("策略：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.TacticsExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("内政：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.InternalExperience.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddNewLine();

                this.MoreMessageText.AddText("疲劳：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.Tiredness.ToString(), Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("善名：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.Karma.ToString(), Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("功绩：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.OfficerMerit.ToString(), Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("ID图片：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.ID + "-" + this.ShowingPerson.PictureIndex, Color.WhiteSmoke);
                this.MoreMessageText.AddNewLine();
                if (this.Switch_DisplayFamily == "on")
                {
                    this.MoreMessageText.AddText("生卒年：", this.MoreMessageText.TitleColor);
                    this.MoreMessageText.AddText(this.ShowingPerson.YearBorn + "-" + this.ShowingPerson.YearDead, Color.WhiteSmoke);
                    this.MoreMessageText.AddNewLine();
                }
                this.MoreMessageText.AddText("家族 ", this.MoreMessageText.TitleColor);
                //if (this.Switch_DisplayFamily == "on")
                //{
                //    this.MoreMessageText.AddText(this.ShowingPerson.PersonFamily, Color.Orange);
                //}
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("父：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.FatherName, Color.SkyBlue);
                this.MoreMessageText.AddText(" 母：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.MotherName, Color.SandyBrown);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("配偶：", Color.WhiteSmoke);
                this.MoreMessageText.AddText(this.ShowingPerson.SpouseName, Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("兄弟姐妹：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[0], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[1], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("子女：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[2], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[3], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                //this.MoreMessageText.AddText("孙：", Color.WhiteSmoke);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[4], Color.SkyBlue);
                //this.MoreMessageText.AddText(this.ShowingPerson.FamilyName[5], Color.SandyBrown);
                //this.MoreMessageText.AddNewLine();
                this.MoreMessageText.AddText("亲近：", this.MoreMessageText.TitleColor);
                this.MoreMessageText.AddText(this.ShowingPerson.ClosePersonsName, Color.Turquoise);
                this.MoreMessageText.AddNewLine();
                this.MoreMessageText.ResortTexts();
           
            }
            IDN = person;
            if (!Session.LargeContextMenu) {
                ThePersonSound = Platform.Current.GetPersonVioce(IDN, "");
                if (this.ThePersonSound.EndsWith(".wav")&& Switch3 == "on")
                {
                    this.screen.PlayNormalSound(this.ThePersonSound);
                }
            }
        }

        internal void SetPosition(ShowPosition showPosition)
        {
            //Rectangle rectDes = new Rectangle(0, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            //Rectangle rect = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);           
            Rectangle rectDes = new Rectangle(0 + 100, 0, this.screen.viewportSize.X, this.screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0 + 200, 0, this.BackgroundSize.X + 200, this.BackgroundSize.Y);
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
            statusEffectText.DisplayOffset = DisplayOffset;

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
            this.MoreMessageText.DisplayOffset = new Point(this.DisplayOffset.X + this.MoreMessageClient.X, this.DisplayOffset.Y + this.MoreMessageClient.Y);
            this.TreasureText.DisplayOffset = new Point(this.DisplayOffset.X + 861, this.DisplayOffset.Y + this.MoreMessageClient.Y);
            //this.TreasureText.DisplayOffset = new Point(this.DisplayOffset.X + this.TitleClient.X, this.DisplayOffset.Y + this.TitleClient.Y);
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
        private Rectangle PortraitLDisplayPosition
        {
            get
            {
                return new Rectangle(this.PortraitL.X + this.DisplayOffset.X, this.PortraitL.Y + this.DisplayOffset.Y, this.PortraitL.Width, this.PortraitL.Height);
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
        private Rectangle SkillBGClientDisplayPosition =>
               new Rectangle(this.SkillBGClient.X + this.DisplayOffset.X, this.SkillBGClient.Y + this.DisplayOffset.Y, this.SkillBGClient.Width, this.SkillBGClient.Height);
        private Rectangle TitleBGClientDisplayPosition =>
            new Rectangle(this.TitleBGClient.X + this.DisplayOffset.X, this.TitleBGClient.Y + this.DisplayOffset.Y, this.TitleBGClient.Width, this.TitleBGClient.Height);
        private Rectangle TreasureBGClientDisplayPosition =>
            new Rectangle(this.TreasureBGClient.X + this.DisplayOffset.X, this.TreasureBGClient.Y + this.DisplayOffset.Y, this.TreasureBGClient.Width, this.TreasureBGClient.Height);
        private Rectangle StuntBGClientDisplayPosition =>
           new Rectangle(this.StuntBGClient.X + this.DisplayOffset.X, this.StuntBGClient.Y + this.DisplayOffset.Y, this.StuntBGClient.Width, this.StuntBGClient.Height);
        private Rectangle FuhuoBGClientDisplayPosition =>
         new Rectangle(this.FuhuoBGClient.X + this.DisplayOffset.X, this.FuhuoBGClient.Y + this.DisplayOffset.Y, this.FuhuoBGClient.Width, this.FuhuoBGClient.Height);
        private Rectangle PersonBGClientDisplayPosition =>
          new Rectangle(this.PersonBGClient.X + this.DisplayOffset.X, this.PersonBGClient.Y + this.DisplayOffset.Y, this.PersonBGClient.Width, this.PersonBGClient.Height);
        private Rectangle MoreMessageBGDisplayPosition
        {
           get
            {
                return  new Rectangle(this.MoreMessageBGClient.X + this.DisplayOffset.X, this.MoreMessageBGClient.Y + this.DisplayOffset.Y, this.MoreMessageBGSize.X, this.MoreMessageBGSize.Y);
            }
         }
        private Rectangle MoreMessageBGDisplayPosition2
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + 861, this.DisplayOffset.Y + this.MoreMessageClient.Y, this.MoreMessageBGSize.X, this.MoreMessageBGSize.Y);
            }
        }
        private Rectangle StuntDisplayPosition
        {
            get
            {
                return new Rectangle(this.StuntText.DisplayOffset.X, this.StuntText.DisplayOffset.Y, this.StuntText.ClientWidth, this.StuntText.ClientHeight);
            }
        }

        private Rectangle TreasuresClientDisplayPosition(int TreasureGroup)
        {
            switch (TreasureGroup)
            {
                case 4000:
                    return new Rectangle(this.Treasure4group25Client.X + this.DisplayOffset.X, this.Treasure4group25Client.Y + this.DisplayOffset.Y, this.Treasure4group25Client.Width, this.Treasure4group25Client.Height);

                case 4100:
                    return new Rectangle(this.Treasure5group30Client.X + this.DisplayOffset.X, this.Treasure5group30Client.Y + this.DisplayOffset.Y, this.Treasure5group30Client.Width, this.Treasure5group30Client.Height);

                case 3214:
                    return new Rectangle(this.Treasure6group40Client.X + this.DisplayOffset.X, this.Treasure6group40Client.Y + this.DisplayOffset.Y, this.Treasure6group40Client.Width, this.Treasure6group40Client.Height);

                case 1100:
                    return new Rectangle(this.Treasure1group10Client.X + this.DisplayOffset.X, this.Treasure1group10Client.Y + this.DisplayOffset.Y, this.Treasure1group10Client.Width, this.Treasure1group10Client.Height);

                case 4700:
                    return new Rectangle(this.Treasure2group15Client.X + this.DisplayOffset.X, this.Treasure2group15Client.Y + this.DisplayOffset.Y, this.Treasure2group15Client.Width, this.Treasure2group15Client.Height);

                case 3200:
                    return new Rectangle(this.Treasure3group20Client.X + this.DisplayOffset.X, this.Treasure3group20Client.Y + this.DisplayOffset.Y, this.Treasure3group20Client.Width, this.Treasure3group20Client.Height);

                case 1200:
                    return new Rectangle(this.Treasure7group50Client.X + this.DisplayOffset.X, this.Treasure7group50Client.Y + this.DisplayOffset.Y, this.Treasure7group50Client.Width, this.Treasure7group50Client.Height);

                case 1300:
                    return new Rectangle(this.Treasure8group55Client.X + this.DisplayOffset.X, this.Treasure8group55Client.Y + this.DisplayOffset.Y, this.Treasure8group55Client.Width, this.Treasure8group55Client.Height);

                case 1700:
                    return new Rectangle(this.Treasure9group60Client.X + this.DisplayOffset.X, this.Treasure9group60Client.Y + this.DisplayOffset.Y, this.Treasure9group60Client.Width, this.Treasure9group60Client.Height);

                case 300:
                    return new Rectangle(this.Treasure10group70Client.X + this.DisplayOffset.X, this.Treasure10group70Client.Y + this.DisplayOffset.Y, this.Treasure10group70Client.Width, this.Treasure10group70Client.Height);

                case 2900:
                    return new Rectangle(this.Treasure11group90Client.X + this.DisplayOffset.X, this.Treasure11group90Client.Y + this.DisplayOffset.Y, this.Treasure11group90Client.Width, this.Treasure11group90Client.Height);

                case 2001:
                    return new Rectangle(this.Treasure12group100Client.X + this.DisplayOffset.X, this.Treasure12group100Client.Y + this.DisplayOffset.Y, this.Treasure12group100Client.Width, this.Treasure12group100Client.Height);            
            }
            return new Rectangle(this.Treasure3group20Client.X + this.DisplayOffset.X, this.Treasure3group20Client.Y + this.DisplayOffset.Y, this.Treasure3group20Client.Width, this.Treasure3group20Client.Height);
        }
    }
}

