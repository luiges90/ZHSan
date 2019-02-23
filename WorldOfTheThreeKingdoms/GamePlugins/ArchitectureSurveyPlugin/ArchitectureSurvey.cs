using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameGlobal;
using Microsoft.Xna.Framework.Graphics;
using GameFreeText;
using GameObjects;
using GameManager;

namespace ArchitectureSurveyPlugin
{
    internal class ArchitectureSurvey
    {
        public FreeText AgricultureText;
        public Architecture ArchitectureToSurvey;
        public FreeText ArmyText;
        public Point BackgroundSize;
        public PlatformTexture BackgroundTexture;
        public FreeText CommerceText;
        private bool Controlling;
        public Point ControllingBackgroundSize;
        public PlatformTexture ControllingBackgroundTexture;
        public Point CurrentPosition;
        private Point displayOffset;
        public FreeText DominationText;
        public FreeText EnduranceText;
        public Color FactionColor;
        public Rectangle FactionPosition;
        public FreeText FactionText;
        public PlatformTexture FactionTexture;
        public FreeText FoodText;
        public FreeText FundText;
        public FreeText KindText;
        public int Left;
        public InformationLevel Level;
        public FreeText MoraleText;
        public FreeText NameText;
        public FreeText NoFactionPersonCountText;
        public FreeText PersonCountText;
        public FreeText PopulationText;
        public FreeText MilitaryPopulationText;
        public FreeText FacilityCountText;
        public FreeText BuildingDaysLeftText;
        public FreeText MayorNameText;
        public FreeText TechnologyText;
        public int Top;
        public Faction ViewingFaction;

        //↓功能开关定义
        internal string Switch1;//建筑种类图片
        internal string Switch2;//进度条图片

        //public Point NewControllingBackgroundSize;
        public PlatformTexture NewControllingBackgroundTexture;
        public PlatformTexture NewControllingMaskTexture;
        //↓建筑种类相关定义
        internal string AKinds;
        internal string AKindfor1;
        internal string AKindfor2;
        internal string AKindfor3;
        internal string AKindfor4;
        internal string AKindfor5;
        internal string AKindfor6;
        internal string AKindfor7;
        internal string AKindfor8;
        internal string AKindfor9;
        internal string AKindfor10;
        internal string AKindfor11;
        internal string AKindfor12;
        internal string AKindfor13;
        internal string AKindfor14;
        internal string AKindfor15;
        internal string AKindfor16;
        internal string AKindfor17;
        internal string AKindfor18;
        internal string AKindfor19;
        internal string AKindfor20;
        internal string AKindfor21;
        internal string AKindfor22;
        internal string AKindfor23;
        internal string AKindfor24;
        internal string AKindfor25;
        internal string AKindfor26;
        internal string AKindfor27;
        internal string AKindfor28;
        //↓建筑种类文理及尺寸相关定义
        internal PlatformTexture AKBackgroundTexture;
        internal PlatformTexture AKBackground0Texture;
        internal PlatformTexture AKBackground1Texture;
        internal PlatformTexture AKBackground2Texture;
        internal PlatformTexture AKBackground3Texture;
        internal PlatformTexture AKBackground4Texture;
        internal PlatformTexture AKBackground5Texture;
        internal PlatformTexture AKBackground6Texture;
        internal PlatformTexture AKBackground7Texture;
        internal PlatformTexture AKBackground8Texture;
        internal PlatformTexture AKBackground9Texture;
        internal PlatformTexture AKBackground10Texture;
        internal PlatformTexture AKBackground11Texture;
        internal PlatformTexture AKBackground12Texture;
        internal PlatformTexture AKBackground13Texture;
        internal PlatformTexture AKBackground14Texture;
        internal PlatformTexture AKBackground15Texture;
        internal PlatformTexture AKBackground16Texture;
        internal PlatformTexture AKBackground17Texture;
        internal PlatformTexture AKBackground18Texture;
        internal PlatformTexture AKBackground19Texture;
        internal PlatformTexture AKBackground20Texture;
        internal PlatformTexture AKBackground21Texture;
        internal PlatformTexture AKBackground22Texture;
        internal PlatformTexture AKBackground23Texture;
        internal PlatformTexture AKBackground24Texture;
        internal PlatformTexture AKBackground25Texture;
        internal PlatformTexture AKBackground26Texture;
        internal PlatformTexture AKBackground27Texture;
        internal PlatformTexture AKBackground28Texture;
        internal Rectangle AKBackground0Client; 
        //进度条相关定义
       
        internal PlatformTexture DominationBarTexture;
        internal PlatformTexture Domination1BarTexture;
        internal PlatformTexture Domination2BarTexture;
        internal PlatformTexture Domination3BarTexture;
        internal PlatformTexture Domination4BarTexture;
        internal PlatformTexture Domination5BarTexture;
        internal PlatformTexture Domination6BarTexture;
        internal Rectangle DominationBarClient;

        internal PlatformTexture EnduranceBarTexture;
        internal PlatformTexture Endurance1BarTexture;
        internal PlatformTexture Endurance2BarTexture;
        internal PlatformTexture Endurance3BarTexture;
        internal PlatformTexture Endurance4BarTexture;
        internal PlatformTexture Endurance5BarTexture;
        internal PlatformTexture Endurance6BarTexture;
        internal Rectangle EnduranceBarClient;

        internal PlatformTexture AgricultureBarTexture;
        internal PlatformTexture Agriculture1BarTexture;
        internal PlatformTexture Agriculture2BarTexture;
        internal PlatformTexture Agriculture3BarTexture;
        internal PlatformTexture Agriculture4BarTexture;
        internal PlatformTexture Agriculture5BarTexture;
        internal PlatformTexture Agriculture6BarTexture;
        internal Rectangle AgricultureBarClient;

        internal PlatformTexture CommerceBarTexture;
        internal PlatformTexture Commerce1BarTexture;
        internal PlatformTexture Commerce2BarTexture;
        internal PlatformTexture Commerce3BarTexture;
        internal PlatformTexture Commerce4BarTexture;
        internal PlatformTexture Commerce5BarTexture;
        internal PlatformTexture Commerce6BarTexture;
        internal Rectangle CommerceBarClient;

        internal PlatformTexture TechnologyBarTexture;
        internal PlatformTexture Technology1BarTexture;
        internal PlatformTexture Technology2BarTexture;
        internal PlatformTexture Technology3BarTexture;
        internal PlatformTexture Technology4BarTexture;
        internal PlatformTexture Technology5BarTexture;
        internal PlatformTexture Technology6BarTexture;
        internal Rectangle TechnologyBarClient;

        internal PlatformTexture MoraleBarTexture;
        internal PlatformTexture Morale1BarTexture;
        internal PlatformTexture Morale2BarTexture;
        internal PlatformTexture Morale3BarTexture;
        internal PlatformTexture Morale4BarTexture;
        internal PlatformTexture Morale5BarTexture;
        internal PlatformTexture Morale6BarTexture;
        internal Rectangle MoraleBarClient;

        internal PlatformTexture FacilityCountBarTexture;
        internal PlatformTexture FacilityCount1BarTexture;
        internal PlatformTexture FacilityCount2BarTexture;
        internal PlatformTexture FacilityCount3BarTexture;
        internal PlatformTexture FacilityCount4BarTexture;
        internal PlatformTexture FacilityCount5BarTexture;
        internal PlatformTexture FacilityCount6BarTexture;
        internal Rectangle FacilityCountBarClient;
                
        float Bar2 = 0;
        float Bar3 = 0;
        float Bar4 = 0;
        float Bar5 = 0;
        float Bar6 = 0;
        float Bar7 = 0;
        float Bar8 = 0;
        


        public void Draw()
        {
            Rectangle? nullable;
            if (!this.Controlling)
            {
                nullable = null;
                CacheManager.Draw(this.BackgroundTexture, new Rectangle(this.displayOffset.X, this.displayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.053f);
                CacheManager.Draw(this.FactionTexture, new Rectangle(this.displayOffset.X + this.FactionPosition.X, this.displayOffset.Y + this.FactionPosition.Y, this.FactionPosition.Width, this.FactionPosition.Height), null, this.FactionColor, 0f, Vector2.Zero, SpriteEffects.None, 0.049f);
                this.NameText.Draw(0.05f);
                this.KindText.Draw(0.05f);
                this.FactionText.Draw(0.05f);
                this.PopulationText.Draw(0.05f);
                this.MilitaryPopulationText.Draw(0.05f);
                this.ArmyText.Draw(0.05f);
                this.DominationText.Draw(0.05f);
                this.EnduranceText.Draw(0.05f);
                this.MayorNameText.Draw(0.05f);
            }
            else
            {
                nullable = null;
                if (Switch1 != "on")
                {
                    CacheManager.Draw(this.ControllingBackgroundTexture, new Rectangle(this.displayOffset.X, this.displayOffset.Y, this.ControllingBackgroundSize.X, this.ControllingBackgroundSize.Y), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.053f);
                }
                    CacheManager.Draw(this.FactionTexture, new Rectangle(this.displayOffset.X + this.FactionPosition.X, this.displayOffset.Y + this.FactionPosition.Y, this.FactionPosition.Width, this.FactionPosition.Height), null, this.FactionColor, 0f, Vector2.Zero, SpriteEffects.None, 0.052f);
                if (Switch1 == "on")
                {
                    CacheManager.Draw(this.AKBackgroundTexture, this.AKBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0491f);
                    CacheManager.Draw(this.NewControllingBackgroundTexture, new Rectangle(this.displayOffset.X, this.displayOffset.Y, this.ControllingBackgroundSize.X, this.ControllingBackgroundSize.Y), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.053f);
                }
                if (Switch2 == "on")
                {
                    try
                    {
                        CacheManager.Draw(this.DominationBarTexture, this.DominationBarDisplayPosition, this.DominationDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.EnduranceBarTexture, this.EnduranceBarDisplayPosition, this.EnduranceDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.AgricultureBarTexture, this.AgricultureBarDisplayPosition, this.AgricultureDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.CommerceBarTexture, this.CommerceBarDisplayPosition, this.CommerceDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.TechnologyBarTexture, this.TechnologyBarDisplayPosition, this.TechnologyDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.MoraleBarTexture, this.MoraleBarDisplayPosition, this.MoraleDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.FacilityCountBarTexture, this.FacilityCountBarDisplayPosition, this.FacilityCountDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.051f);
                        CacheManager.Draw(this.NewControllingMaskTexture, new Rectangle(this.displayOffset.X, this.displayOffset.Y, this.ControllingBackgroundSize.X, this.ControllingBackgroundSize.Y), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.049f);
                    }
                    catch { }
                }

                this.NameText.Draw(0.05f);
                this.KindText.Draw(0.05f);
                this.FactionText.Draw(0.05f);
                this.PopulationText.Draw(0.05f);
                this.MilitaryPopulationText.Draw(0.05f);
                this.ArmyText.Draw(0.05f);
                this.DominationText.Draw(0.05f);
                this.EnduranceText.Draw(0.05f);
                this.FundText.Draw(0.05f);
                this.FoodText.Draw(0.05f);
                this.PersonCountText.Draw(0.05f);
                this.FacilityCountText.Draw(0.05f);
                this.NoFactionPersonCountText.Draw(0.05f);
                this.AgricultureText.Draw(0.05f);
                this.CommerceText.Draw(0.05f);
                this.TechnologyText.Draw(0.05f);
                this.MoraleText.Draw(0.05f);
                this.BuildingDaysLeftText.Draw(0.05f);
                this.MayorNameText.Draw(0.05f);
            }
        }

        private void ResetTextsPosition()
        {
            this.NameText.DisplayOffset = this.displayOffset;
            this.KindText.DisplayOffset = this.displayOffset;
            this.FactionText.DisplayOffset = this.displayOffset;
            this.PopulationText.DisplayOffset = this.displayOffset;
            this.MilitaryPopulationText.DisplayOffset = this.displayOffset;
            this.ArmyText.DisplayOffset = this.displayOffset;
            this.DominationText.DisplayOffset = this.displayOffset;
            this.EnduranceText.DisplayOffset = this.displayOffset;
            this.FundText.DisplayOffset = this.displayOffset;
            this.FoodText.DisplayOffset = this.displayOffset;
            this.PersonCountText.DisplayOffset = this.displayOffset;
            this.FacilityCountText.DisplayOffset = this.displayOffset;
            this.NoFactionPersonCountText.DisplayOffset = this.displayOffset;
            this.AgricultureText.DisplayOffset = this.displayOffset;
            this.CommerceText.DisplayOffset = this.displayOffset;
            this.TechnologyText.DisplayOffset = this.displayOffset;
            this.MoraleText.DisplayOffset = this.displayOffset;
            this.BuildingDaysLeftText.DisplayOffset = this.displayOffset;
            this.MayorNameText.DisplayOffset = this.displayOffset;
        }

        public void Update()
        {
            Rectangle rectangle;
            this.FactionColor = Color.White;
            string meigongzuoderenshuzifuchuan;
            if (this.ArchitectureToSurvey.BelongedFaction != null)
            {
                this.FactionColor = this.ArchitectureToSurvey.BelongedFaction.FactionColor;
            }
            if (((this.ViewingFaction != null) && !Session.GlobalVariables.SkyEye) && (this.ViewingFaction != this.ArchitectureToSurvey.BelongedFaction))
            {
                this.Controlling = false;
                rectangle = new Rectangle(this.Left - this.BackgroundSize.X, this.Top - this.BackgroundSize.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
                StaticMethods.AdjustRectangleInViewport(ref rectangle);
                this.DisplayOffset = new Point(rectangle.X, rectangle.Y);
                this.NameText.Text = this.ArchitectureToSurvey.Name;
                this.KindText.Text = this.ArchitectureToSurvey.KindString;
                this.FactionText.Text = this.ArchitectureToSurvey.FactionString;
                this.PopulationText.Text = this.ArchitectureToSurvey.PopulationInInformationLevel(this.Level);
                this.MilitaryPopulationText.Text = this.ArchitectureToSurvey.MilitaryPopulationInInformationLevel(this.Level);
                //this.ArmyText.Text = this.ArchitectureToSurvey.ArmyQuantityInInformationLevel(this.Level);

                //////////////////////////////////////////////////////////临时代码 ，合理的应该恢复上句并修改GameObjects.Architecture
                if (this.Level == InformationLevel.未知 || this.Level == InformationLevel.无 || this.Level == InformationLevel.低)
                {
                    this.ArmyText.Text = this.ArchitectureToSurvey.ArmyQuantityInInformationLevel(this.Level);
                }
                else
                {
                    this.ArmyText.Text = this.ArchitectureToSurvey.FormationCount.ToString() + "/" + this.ArchitectureToSurvey.ArmyQuantity.ToString();

                }
                /////////////////////////////////////////////////////////////
                this.DominationText.Text = this.ArchitectureToSurvey.DominationInInformationLevel(this.Level);
                this.EnduranceText.Text = this.ArchitectureToSurvey.EnduranceInInformationLevel(this.Level);
                this.MayorNameText.Text = this.ArchitectureToSurvey.MayorName;
            }
            else
            {
                this.Controlling = true;
                rectangle = new Rectangle(this.Left - this.ControllingBackgroundSize.X, this.Top - this.ControllingBackgroundSize.Y, this.ControllingBackgroundSize.X, this.ControllingBackgroundSize.Y);
                StaticMethods.AdjustRectangleInViewport(ref rectangle);
                meigongzuoderenshuzifuchuan = meigongzuoderenshu(this.ArchitectureToSurvey).ToString();

                this.DisplayOffset = new Point(rectangle.X, rectangle.Y);
                this.NameText.Text = this.ArchitectureToSurvey.Name;
                if (Switch1 == "on")
                {
                    if (this.ArchitectureToSurvey.FactionString == "----")
                    {
                        this.KindText.Text = "----";
                    }
                    else
                    {
                        this.KindText.Text = this.ArchitectureToSurvey.BelongedFaction.LeaderName;
                    }
                }
                else 
                { 
                    this.KindText.Text = this.ArchitectureToSurvey.KindString;
                }
                this.FactionText.Text = this.ArchitectureToSurvey.FactionString;
                this.PopulationText.Text = this.ArchitectureToSurvey.Population.ToString();
                this.MilitaryPopulationText.Text = this.ArchitectureToSurvey.ServicePopulation.ToString();
                this.ArmyText.Text = this.ArchitectureToSurvey.FormationCount.ToString() + "/" + this.ArchitectureToSurvey.ArmyQuantity.ToString();
                this.DominationText.Text = this.ArchitectureToSurvey.DominationString;
                this.EnduranceText.Text = this.ArchitectureToSurvey.EnduranceString;
                this.FundText.Text = this.ArchitectureToSurvey.Fund.ToString();
                if (this.ArchitectureToSurvey.Food < 10000)
                {
                    this.FoodText.Text = (this.ArchitectureToSurvey.Food / 10000.0f).ToString("f1") + "万";

                }
                else
                {
                    this.FoodText.Text = Math.Floor(this.ArchitectureToSurvey.Food / 10000.0f).ToString() + "万";
                }
                this.PersonCountText.Text = meigongzuoderenshuzifuchuan + "/" + this.ArchitectureToSurvey.PersonCount.ToString();
                this.FacilityCountText.Text = this.ArchitectureToSurvey.SheshiMiaoshu;
                this.NoFactionPersonCountText.Text = this.ArchitectureToSurvey.NoFactionPersonCount.ToString();
                this.AgricultureText.Text = this.ArchitectureToSurvey.AgricultureString;
                this.CommerceText.Text = this.ArchitectureToSurvey.CommerceString;
                this.TechnologyText.Text = this.ArchitectureToSurvey.TechnologyString;
                this.MoraleText.Text = this.ArchitectureToSurvey.MoraleString;
                this.BuildingDaysLeftText.Text = (this.ArchitectureToSurvey.BuildingDaysLeft * Session.Parameters.DayInTurn).ToString();
                this.MayorNameText.Text = this.ArchitectureToSurvey.MayorName;
                //↓判断建筑种类对应文理
                if (Switch1 == "on")
                {
                    AKinds = this.ArchitectureToSurvey.KindString;
                    if (AKinds == AKindfor1)
                    {
                        AKBackgroundTexture = AKBackground1Texture;
                    }
                    else if (AKinds == AKindfor2)
                    {
                        AKBackgroundTexture = AKBackground2Texture;
                    }
                    else if (AKinds == AKindfor3)
                    {
                        AKBackgroundTexture = AKBackground3Texture;
                    }
                    else if (AKinds == AKindfor4)
                    {
                        AKBackgroundTexture = AKBackground4Texture;
                    }
                    else if (AKinds == AKindfor5)
                    {
                        AKBackgroundTexture = AKBackground5Texture;
                    }
                    else if (AKinds == AKindfor6)
                    {
                        AKBackgroundTexture = AKBackground6Texture;
                    }
                    else if (AKinds == AKindfor7)
                    {
                        AKBackgroundTexture = AKBackground7Texture;
                    }
                    else if (AKinds == AKindfor8)
                    {
                        AKBackgroundTexture = AKBackground8Texture;
                    }
                    else if (AKinds == AKindfor9)
                    {
                        AKBackgroundTexture = AKBackground9Texture;
                    }
                    else if (AKinds == AKindfor10)
                    {
                        AKBackgroundTexture = AKBackground10Texture;
                    }
                    else if (AKinds == AKindfor11)
                    {
                        AKBackgroundTexture = AKBackground11Texture;
                    }
                    else if (AKinds == AKindfor12)
                    {
                        AKBackgroundTexture = AKBackground12Texture;
                    }
                    else if (AKinds == AKindfor13)
                    {
                        AKBackgroundTexture = AKBackground13Texture;
                    }
                    else if (AKinds == AKindfor14)
                    {
                        AKBackgroundTexture = AKBackground14Texture;
                    }
                    else if (AKinds == AKindfor15)
                    {
                        AKBackgroundTexture = AKBackground15Texture;
                    }
                    else if (AKinds == AKindfor16)
                    {
                        AKBackgroundTexture = AKBackground16Texture;
                    }
                    else if (AKinds == AKindfor17)
                    {
                        AKBackgroundTexture = AKBackground17Texture;
                    }
                    else if (AKinds == AKindfor18)
                    {
                        AKBackgroundTexture = AKBackground18Texture;
                    }
                    else if (AKinds == AKindfor19)
                    {
                        AKBackgroundTexture = AKBackground19Texture;
                    }
                    else if (AKinds == AKindfor20)
                    {
                        AKBackgroundTexture = AKBackground20Texture;
                    }
                    else if (AKinds == AKindfor21)
                    {
                        AKBackgroundTexture = AKBackground21Texture;
                    }
                    else if (AKinds == AKindfor22)
                    {
                        AKBackgroundTexture = AKBackground22Texture;
                    }
                    else if (AKinds == AKindfor23)
                    {
                        AKBackgroundTexture = AKBackground23Texture;
                    }
                    else if (AKinds == AKindfor24)
                    {
                        AKBackgroundTexture = AKBackground24Texture;
                    }
                    else if (AKinds == AKindfor25)
                    {
                        AKBackgroundTexture = AKBackground25Texture;
                    }
                    else if (AKinds == AKindfor26)
                    {
                        AKBackgroundTexture = AKBackground26Texture;
                    }
                    else if (AKinds == AKindfor27)
                    {
                        AKBackgroundTexture = AKBackground27Texture;
                    }
                    else if (AKinds == AKindfor28)
                    {
                        AKBackgroundTexture = AKBackground28Texture;
                    }
                    else
                    {
                        AKBackgroundTexture = AKBackground0Texture;
                    }
                }
                //↓进度条对应文理
                if (Switch2 == "on")
                {                    
                    if (this.ArchitectureToSurvey.Domination == 0 || this.ArchitectureToSurvey.DominationCeiling == 0) { Bar2 = 0; }
                    else { Bar2 = 100 * this.ArchitectureToSurvey.Domination / this.ArchitectureToSurvey.DominationCeiling; }
                    if (this.ArchitectureToSurvey.Endurance == 0 || this.ArchitectureToSurvey.EnduranceCeiling == 0) { Bar3 = 0; }
                    else { Bar3 = 100 * this.ArchitectureToSurvey.Endurance / this.ArchitectureToSurvey.EnduranceCeiling; }
                    if (this.ArchitectureToSurvey.Agriculture == 0 || this.ArchitectureToSurvey.AgricultureCeiling == 0) { Bar4 = 0; }
                    else { Bar4 = 100 * this.ArchitectureToSurvey.Agriculture / this.ArchitectureToSurvey.AgricultureCeiling; }
                    if (this.ArchitectureToSurvey.Commerce == 0 || this.ArchitectureToSurvey.CommerceCeiling == 0) { Bar5 = 0; }
                    else { Bar5 = 100 * this.ArchitectureToSurvey.Commerce / this.ArchitectureToSurvey.CommerceCeiling; }
                    if (this.ArchitectureToSurvey.Technology == 0 || this.ArchitectureToSurvey.TechnologyCeiling == 0) { Bar6 = 0; }
                    else { Bar6 = 100 * this.ArchitectureToSurvey.Technology / this.ArchitectureToSurvey.TechnologyCeiling; }
                    if (this.ArchitectureToSurvey.Morale == 0 || this.ArchitectureToSurvey.MoraleCeiling == 0) { Bar7 = 0; }
                    else { Bar7 = 100 * this.ArchitectureToSurvey.Morale / this.ArchitectureToSurvey.MoraleCeiling; }
                    if (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft == 0 || this.ArchitectureToSurvey.FacilityPositionCount == 0) { Bar8 = 0; }
                    else { Bar8 = 100 * (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft) / this.ArchitectureToSurvey.FacilityPositionCount; }
                    DominationBarTexture = Domination6BarTexture;
                    if (Bar2 < 10) { DominationBarTexture = Domination1BarTexture; }
                    else if (Bar2 < 20) { DominationBarTexture = Domination2BarTexture; }
                    else if (Bar2 < 50) { DominationBarTexture = Domination3BarTexture; }
                    else if (Bar2 < 80) { DominationBarTexture = Domination4BarTexture; }
                    else if (Bar2 < 100) { DominationBarTexture = Domination5BarTexture; }
                    EnduranceBarTexture = Endurance6BarTexture;
                    if (Bar3 < 10) { EnduranceBarTexture = Endurance1BarTexture; }
                    else if (Bar3 < 20) { EnduranceBarTexture = Endurance2BarTexture; }
                    else if (Bar3 < 50) { EnduranceBarTexture = Endurance3BarTexture; }
                    else if (Bar3 < 80) { EnduranceBarTexture = Endurance4BarTexture; }
                    else if (Bar3 < 100) { EnduranceBarTexture = Endurance5BarTexture; }
                    AgricultureBarTexture = Agriculture6BarTexture;
                    if (Bar4 < 10) { AgricultureBarTexture = Agriculture1BarTexture; }
                    else if (Bar4 < 20) { AgricultureBarTexture = Agriculture2BarTexture; }
                    else if (Bar4 < 50) { AgricultureBarTexture = Agriculture3BarTexture; }
                    else if (Bar4 < 80) { AgricultureBarTexture = Agriculture4BarTexture; }
                    else if (Bar4 < 100) { AgricultureBarTexture = Agriculture5BarTexture; }
                    CommerceBarTexture = Commerce6BarTexture;
                    if (Bar5 < 10) { CommerceBarTexture = Commerce1BarTexture; }
                    else if (Bar5 < 20) { CommerceBarTexture = Commerce2BarTexture; }
                    else if (Bar5 < 50) { CommerceBarTexture = Commerce3BarTexture; }
                    else if (Bar5 < 80) { CommerceBarTexture = Commerce4BarTexture; }
                    else if (Bar5 < 100) { CommerceBarTexture = Commerce5BarTexture; }
                    TechnologyBarTexture = Technology6BarTexture;
                    if (Bar6 < 10) { TechnologyBarTexture = Technology1BarTexture; }
                    else if (Bar6 < 20) { TechnologyBarTexture = Technology2BarTexture; }
                    else if (Bar6 < 50) { TechnologyBarTexture = Technology3BarTexture; }
                    else if (Bar6 < 80) { TechnologyBarTexture = Technology4BarTexture; }
                    else if (Bar6 < 100) { TechnologyBarTexture = Technology5BarTexture; }
                    MoraleBarTexture = Morale6BarTexture;
                    if (Bar7 < 10) { MoraleBarTexture = Morale1BarTexture; }
                    else if (Bar7 < 20) { MoraleBarTexture = Morale2BarTexture; }
                    else if (Bar7 < 50) { MoraleBarTexture = Morale3BarTexture; }
                    else if (Bar7 < 80) { MoraleBarTexture = Morale4BarTexture; }
                    else if (Bar7 < 100) { MoraleBarTexture = Morale5BarTexture; }
                    FacilityCountBarTexture = FacilityCount6BarTexture;
                    if (Bar8 < 10) { FacilityCountBarTexture = FacilityCount1BarTexture; }
                    else if (Bar8 < 20) { FacilityCountBarTexture = FacilityCount2BarTexture; }
                    else if (Bar8 < 50) { FacilityCountBarTexture = FacilityCount3BarTexture; }
                    else if (Bar8 < 80) { FacilityCountBarTexture = FacilityCount4BarTexture; }
                    else if (Bar8 < 100) { FacilityCountBarTexture = FacilityCount5BarTexture; }     
                }
            }
        }

        private int meigongzuoderenshu(Architecture jianzhu)
        {
            int renshu = 0;
            foreach (Person person in jianzhu.Persons.GetList())
            {
                if (person.WorkKind == ArchitectureWorkKind.无)
                {
                    renshu++;
                }
            }
            return renshu;
        }

        public Point DisplayOffset
        {
            get
            {
                return this.displayOffset;
            }
            set
            {
                this.displayOffset = value;
                this.ResetTextsPosition();
            }
        }
        //以下添加

        private Rectangle AKBackgroundDisplayPosition
        {
            get
            {
                    return new Rectangle(this.AKBackground0Client.X + this.DisplayOffset.X, this.AKBackground0Client.Y + this.DisplayOffset.Y, this.AKBackground0Client.Width, this.AKBackground0Client.Height);
            }
        }        
        private Rectangle DominationBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Domination == 0 || this.ArchitectureToSurvey.DominationCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.DominationBarClient.X + this.DisplayOffset.X, this.DominationBarClient.Y + this.DisplayOffset.Y, this.DominationBarClient.Width * this.ArchitectureToSurvey.Domination / this.ArchitectureToSurvey.DominationCeiling, this.DominationBarClient.Height);
            }
        }
        private Rectangle DominationDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Domination == 0 || this.ArchitectureToSurvey.DominationCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.DominationBarClient.Width * this.ArchitectureToSurvey.Domination / this.ArchitectureToSurvey.DominationCeiling, this.DominationBarClient.Height);
            }
        }
        private Rectangle EnduranceBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Endurance == 0 || this.ArchitectureToSurvey.EnduranceCeiling == 0)
                {
                    return new Rectangle(this.EnduranceBarClient.X + this.DisplayOffset.X, this.EnduranceBarClient.Y + this.DisplayOffset.Y, 0, this.EnduranceBarClient.Height);

                }
                return new Rectangle(this.EnduranceBarClient.X + this.DisplayOffset.X, this.EnduranceBarClient.Y + this.DisplayOffset.Y, this.EnduranceBarClient.Width * this.ArchitectureToSurvey.Endurance / this.ArchitectureToSurvey.EnduranceCeiling, this.EnduranceBarClient.Height);
            }
        }
        private Rectangle EnduranceDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Endurance == 0 || this.ArchitectureToSurvey.EnduranceCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.EnduranceBarClient.Width * this.ArchitectureToSurvey.Endurance / this.ArchitectureToSurvey.EnduranceCeiling, this.EnduranceBarClient.Height);
            }
        }
        private Rectangle AgricultureBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Agriculture == 0 || this.ArchitectureToSurvey.AgricultureCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.AgricultureBarClient.X + this.DisplayOffset.X, this.AgricultureBarClient.Y + this.DisplayOffset.Y, this.AgricultureBarClient.Width * this.ArchitectureToSurvey.Agriculture / this.ArchitectureToSurvey.AgricultureCeiling, this.AgricultureBarClient.Height);
            }
        }
        private Rectangle AgricultureDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Agriculture == 0 || this.ArchitectureToSurvey.AgricultureCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.AgricultureBarClient.Width * this.ArchitectureToSurvey.Agriculture / this.ArchitectureToSurvey.AgricultureCeiling, this.AgricultureBarClient.Height);
            }
        }
        private Rectangle CommerceBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Commerce == 0 || this.ArchitectureToSurvey.CommerceCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.CommerceBarClient.X + this.DisplayOffset.X, this.CommerceBarClient.Y + this.DisplayOffset.Y, this.CommerceBarClient.Width * this.ArchitectureToSurvey.Commerce / this.ArchitectureToSurvey.CommerceCeiling, this.CommerceBarClient.Height);
            }
        }
        private Rectangle CommerceDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Commerce == 0 || this.ArchitectureToSurvey.CommerceCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.CommerceBarClient.Width * this.ArchitectureToSurvey.Commerce / this.ArchitectureToSurvey.CommerceCeiling, this.CommerceBarClient.Height);
            }
        }
        private Rectangle TechnologyBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Technology == 0 || this.ArchitectureToSurvey.TechnologyCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.TechnologyBarClient.X + this.DisplayOffset.X, this.TechnologyBarClient.Y + this.DisplayOffset.Y, this.TechnologyBarClient.Width * this.ArchitectureToSurvey.Technology / this.ArchitectureToSurvey.TechnologyCeiling, this.TechnologyBarClient.Height);
            }
        }
        private Rectangle TechnologyDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Technology == 0 || this.ArchitectureToSurvey.TechnologyCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.TechnologyBarClient.Width * this.ArchitectureToSurvey.Technology / this.ArchitectureToSurvey.TechnologyCeiling, this.TechnologyBarClient.Height);
            }
        }
        private Rectangle MoraleBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Morale == 0 || this.ArchitectureToSurvey.MoraleCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.MoraleBarClient.X + this.DisplayOffset.X, this.MoraleBarClient.Y + this.DisplayOffset.Y, this.MoraleBarClient.Width * this.ArchitectureToSurvey.Morale / this.ArchitectureToSurvey.MoraleCeiling, this.MoraleBarClient.Height);
            }
        }
        private Rectangle MoraleDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.Morale == 0 || this.ArchitectureToSurvey.MoraleCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.MoraleBarClient.Width * this.ArchitectureToSurvey.Morale / this.ArchitectureToSurvey.MoraleCeiling, this.MoraleBarClient.Height);
            }
        }
        private Rectangle FacilityCountBarDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft == 0 || this.ArchitectureToSurvey.FacilityPositionCount == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(this.FacilityCountBarClient.X + this.DisplayOffset.X, this.FacilityCountBarClient.Y + this.DisplayOffset.Y, this.FacilityCountBarClient.Width * (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft) / this.ArchitectureToSurvey.FacilityPositionCount, this.FacilityCountBarClient.Height);
            }
        }
        private Rectangle FacilityCountDisplayPosition
        {
            get
            {
                if (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft == 0 || this.ArchitectureToSurvey.FacilityPositionCount == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.FacilityCountBarClient.Width * (this.ArchitectureToSurvey.FacilityPositionCount - this.ArchitectureToSurvey.FacilityPositionLeft) / this.ArchitectureToSurvey.FacilityPositionCount, this.FacilityCountBarClient.Height);
            }
        }






    }



}
