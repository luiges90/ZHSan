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
    using System.IO;

    internal class PersonDetail
    {
        public Vector2 Scale = new Vector2(0.865f, 0.865f);

        internal FreeTextList AllSkillTexts;
        internal Point BackgroundSize;
        internal Rectangle BackgroundClient;
        internal PlatformTexture BackgroundMask1;
        internal PlatformTexture BackgroundMask2;
        internal PlatformTexture BackgroundTexture;
        internal PlatformTexture PictureNull;
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

        internal Person ShowingPerson;
        internal Point SkillBlockSize;
        internal Point SkillDisplayOffset;
        internal Rectangle StuntClient;
        internal FreeRichText StuntText = new FreeRichText();
        internal FreeText SurNameText;
        //////////下面是添加的内容
        internal Rectangle PersonTreasuresTextClient;//人物宝物
        internal FreeRichText PersonTreasuresText = new FreeRichText();
        internal string ShowPersonTreasuresCount;
        internal string ShowPersonTreasuresWorth;
        internal string ShowPersonTreasuresDescription;
        internal string PersonTreasuresText1;
        internal string PersonTreasuresText2;
        internal string PersonTreasuresText3;
        internal string PersonTreasuresText4;
        internal string PersonTreasuresText5;
        internal string PersonTreasuresText6;
        internal string PersonTreasuresText7;
        internal string PersonTreasuresText8;
        internal string PersonTreasuresText9;
        internal string PersonTreasuresText10;
        internal string PersonTreasuresText11;
        internal string PersonTreasuresText12;
        internal string PersonTreasuresText13;
        internal string PersonTreasuresText14;

        internal int EffectiveTreasuresCount;
        internal int NoEffectiveTreasuresCount;
        internal int TreasuresCount; 
        //////////以下新界面相关
        ////↓功能开关
        internal string Switch1;//新界面
         
        internal string Switch3;//界面音效
        internal string Switch4;//人物声音

        internal string Switch21;//人物列传
        internal string Switch24;//人物列传分开
        internal string Switch25;//人物列传默认显示页
        internal string Switch26;//人物列传
        internal string Switch27;//人物演义
        internal string Switch28;//人物历史
        internal string Switch29;//人物剧本

        internal string Switch105;//人物势力名称
        internal string Switch106;//人物势力颜色 
        
        internal string Switch114;//人物特定宝物
        internal string Switch115;//人物一般宝物
        internal string Switch116;//人物小图像
        internal string Switch117;//人物重要称号
        internal string Switch118;//人物重要特技

        internal string Switch121;//人物亲人
        internal string Switch122;//人物关系
        internal string Switch123;//人物关系表
        internal string Switch124;//人物战绩

        internal string Switch131;//条件影响背景图
        internal string Switch132;//技能背景图
        internal string Switch133;//称号背景图
        internal string Switch134;//特技背景图
        //
        private string ThePersonSound;
        ////↓主页面按钮        
        internal Rectangle BiographyButtonClient;
        internal PlatformTexture BiographyButtonTexture;
        internal PlatformTexture BiographyPressedTexture;
        ////↓主页面UI
        internal Rectangle InformationBackgroundClient;
        internal PlatformTexture InformationBackgroundTexture;
        internal PlatformTexture InformationMask1Texture;
        internal PlatformTexture InformationMask2Texture;
        internal Rectangle DetailBackgroundClient;
        internal PlatformTexture DetailBackgroundTexture;
        internal PlatformTexture DetailMask1Texture;
        internal PlatformTexture DetailMask2Texture;
        internal Rectangle TreasureBackgroundClient;
        internal PlatformTexture TreasureBackgroundTexture;
        internal PlatformTexture TreasureMask1Texture;
        internal PlatformTexture TreasureMask2Texture;
        internal Rectangle TitleBackgroundClient;
        internal PlatformTexture TitleBackgroundTexture;
        internal PlatformTexture TitleMask1Texture;
        internal PlatformTexture TitleMask2Texture;
        internal Rectangle SkillBackgroundClient;
        internal PlatformTexture SkillBackgroundTexture;
        internal PlatformTexture SkillMask1Texture;
        internal PlatformTexture SkillMask2Texture;
        internal Rectangle StuntBackgroundClient;
        internal PlatformTexture StuntBackgroundTexture;
        internal PlatformTexture StuntMask1Texture;
        internal PlatformTexture StuntMask2Texture;
        internal Rectangle BiographyBackgroundClient;
        internal PlatformTexture BiographyBackgroundTexture;
        internal PlatformTexture BiographyMask1Texture;
        internal PlatformTexture BiographyMask2Texture;
        internal Rectangle SpecialtyShowBackgroundClient;
        internal PlatformTexture SpecialtyShowBackgroundTexture;
        internal PlatformTexture SpecialtyShowMask1Texture;
        internal PlatformTexture SpecialtyShowMask2Texture;
        //↓人物头像
        internal string PortraitKindInInformation;
        internal Rectangle PortraitInInformationClient;       
        //↓人物Text信息
        internal List<LabelText> PersonInInformationTexts = new List<LabelText>();        
        ////↓人物信息
        internal int IDN;       
        //↓势力
        internal string FactionName;
        internal string FactionNameKind;
        internal string ShowFactionNameBackground;
        internal PlatformTexture FactionNameBackground;
        internal Rectangle FactionNameBackgroundClient;
        internal FreeText FactionNameText;
        private Color TheFactionColor
        {get
            {
                if (this.ShowingPerson.BelongedFaction != null)
                {return this.ShowingPerson.BelongedFaction.FactionColor;}
                else
                { return Color.White; }
            }
        }
        internal PlatformTexture FactionColour;
        internal PlatformTexture FactionColourBackground;
        internal Rectangle FactionColourClient;  
        //↓重要宝物 
        internal Rectangle ImportantTreasureButtonClient;
        internal PlatformTexture ImportantTreasureButtonTexture;
        internal PlatformTexture ImportantTreasurePressedTexture;

        internal PlatformTexture ImportantTreasureShowMask;
        internal PlatformTexture ImportantTreasureMask;
        internal PlatformTexture ImportantTreasureBackground;
        internal Rectangle ImportantTreasureBackgroundClient;
        internal int MaxImportantTreasureShowNumber;
        internal string ShowNullTreasurePicture;
        internal PlatformTexture GeneralTreasureMask;
        internal PlatformTexture GeneralTreasureBackground;
        internal Rectangle GeneralTreasureBackgroundClient;
        internal int MaxGeneralTreasureShowNumber;
        
        internal string ImportantTreasureTextFollowTheMouse;        
        internal PlatformTexture ImportantTreasureTextBackground;
        internal Rectangle ImportantTreasureTextBackgroundClient;
        internal Rectangle ImportantTreasureTextClient;
        internal PlatformTexture ImportantTreasureTextMask;
        internal FreeRichText ImportantTreasureText = new FreeRichText();
        internal string ImportantTreasureText1;
        internal string ImportantTreasureText2;
        internal string ImportantTreasureText3;
        internal string ImportantTreasureText4;
        internal string ImportantTreasureText5;
        internal PlatformTexture ImportantTreasureTextPicture;
        internal Rectangle ImportantTreasureTextPictureClient;
              
        internal Rectangle ThePersonPictureInTreasureClient;
        private PlatformTexture SexPicture
        { get
            { if (SexN == true)
                {return TheSexPicture1;}
                else
                {return TheSexPicture2;}
            }
        }
        internal PlatformTexture TheSexPicture1;
        internal PlatformTexture TheSexPicture2;
        internal string ThePersonPictureKindInTreasure;
        internal bool SexN;

        internal int ImportantTreasure1Group;
        internal int ImportantTreasure2Group;
        internal int ImportantTreasure3Group;
        internal int ImportantTreasure4Group;
        internal int ImportantTreasure5Group;
        internal int ImportantTreasure6Group;
        internal int ImportantTreasure7Group;
        internal int ImportantTreasure8Group;
        internal int ImportantTreasure9Group;
        internal int ImportantTreasure10Group;
        internal int ImportantTreasure11Group;
        internal int ImportantTreasure12Group;
        internal int ImportantTreasure13Group;
        internal int ImportantTreasure14Group;
        internal int ImportantTreasure15Group;
        internal int ImportantTreasure16Group;
        internal int ImportantTreasure17Group;
        internal int ImportantTreasure18Group;
        internal int ImportantTreasure19Group;
        internal int ImportantTreasure20Group;

        internal Rectangle ImportantTreasure1Client;
        internal Rectangle ImportantTreasure2Client;
        internal Rectangle ImportantTreasure3Client;
        internal Rectangle ImportantTreasure4Client;
        internal Rectangle ImportantTreasure5Client;
        internal Rectangle ImportantTreasure6Client;
        internal Rectangle ImportantTreasure7Client;
        internal Rectangle ImportantTreasure8Client;
        internal Rectangle ImportantTreasure9Client;
        internal Rectangle ImportantTreasure10Client;
        internal Rectangle ImportantTreasure11Client;
        internal Rectangle ImportantTreasure12Client;
        internal Rectangle ImportantTreasure13Client;
        internal Rectangle ImportantTreasure14Client;
        internal Rectangle ImportantTreasure15Client;
        internal Rectangle ImportantTreasure16Client;
        internal Rectangle ImportantTreasure17Client;
        internal Rectangle ImportantTreasure18Client;
        internal Rectangle ImportantTreasure19Client;
        internal Rectangle ImportantTreasure20Client;

        internal PlatformTexture GeneralTreasureShowMask;
        internal PlatformTexture GeneralTreasureShowBackground;
        internal Rectangle GeneralTreasureShowClient;
        internal int GeneralTreasureHSpace;
        internal int GeneralTreasureVSpace;
        internal int GeneralTreasureHNumber;
        internal int GeneralTreasureVNumber;
        //↓重要称号
        internal int MaxImportantTitleShowNumber;
        internal string ShowImportantTitleName;
        internal string ShowImportantTitleNameBackground;
        internal string ShowImportantTitlePicture;
        internal string ImportantTitleTextFollowTheMouse;
        internal PlatformTexture ImportantTitleTextBackground;
        internal Rectangle ImportantTitleTextBackgroundClient;
        internal FreeRichText ImportantTitleText = new FreeRichText();
        internal Rectangle ImportantTitleTextClient;
        internal string ImportantTitleText1;
        internal string ImportantTitleText2;
        internal string ImportantTitleText3;
        internal string ImportantTitleText4;
        internal string ImportantTitleText5;

        internal int ImportantTitle1Group;
        internal int ImportantTitle2Group;
        internal int ImportantTitle3Group;
        internal int ImportantTitle4Group;
        internal int ImportantTitle5Group;
        internal int ImportantTitle6Group;
        internal int ImportantTitle7Group;
        internal int ImportantTitle8Group;
        internal int ImportantTitle9Group;
        internal int ImportantTitle10Group;
        internal int ImportantTitle11Group;
        internal int ImportantTitle12Group;
        internal int ImportantTitle13Group;
        internal int ImportantTitle14Group;
        internal int ImportantTitle15Group;
        internal int ImportantTitle16Group;
        internal int ImportantTitle17Group;
        internal int ImportantTitle18Group;
        internal int ImportantTitle19Group;
        internal int ImportantTitle20Group;

        internal FreeText ImportantTitle1NameText;
        internal FreeText ImportantTitle2NameText;
        internal FreeText ImportantTitle3NameText;
        internal FreeText ImportantTitle4NameText;
        internal FreeText ImportantTitle5NameText;
        internal FreeText ImportantTitle6NameText;
        internal FreeText ImportantTitle7NameText;
        internal FreeText ImportantTitle8NameText;
        internal FreeText ImportantTitle9NameText;
        internal FreeText ImportantTitle10NameText;
        internal FreeText ImportantTitle11NameText;
        internal FreeText ImportantTitle12NameText;
        internal FreeText ImportantTitle13NameText;
        internal FreeText ImportantTitle14NameText;
        internal FreeText ImportantTitle15NameText;
        internal FreeText ImportantTitle16NameText;
        internal FreeText ImportantTitle17NameText;
        internal FreeText ImportantTitle18NameText;
        internal FreeText ImportantTitle19NameText;
        internal FreeText ImportantTitle20NameText;  
        internal PlatformTexture ImportantTitle1Background;
        internal PlatformTexture ImportantTitle2Background;
        internal PlatformTexture ImportantTitle3Background;
        internal PlatformTexture ImportantTitle4Background;
        internal PlatformTexture ImportantTitle5Background;
        internal PlatformTexture ImportantTitle6Background;
        internal PlatformTexture ImportantTitle7Background;
        internal PlatformTexture ImportantTitle8Background;
        internal PlatformTexture ImportantTitle9Background;
        internal PlatformTexture ImportantTitle10Background;
        internal PlatformTexture ImportantTitle11Background;
        internal PlatformTexture ImportantTitle12Background;
        internal PlatformTexture ImportantTitle13Background;
        internal PlatformTexture ImportantTitle14Background;
        internal PlatformTexture ImportantTitle15Background;
        internal PlatformTexture ImportantTitle16Background;
        internal PlatformTexture ImportantTitle17Background;
        internal PlatformTexture ImportantTitle18Background;
        internal PlatformTexture ImportantTitle19Background;
        internal PlatformTexture ImportantTitle20Background;
        internal Rectangle ImportantTitle1Client;
        internal Rectangle ImportantTitle2Client;
        internal Rectangle ImportantTitle3Client;
        internal Rectangle ImportantTitle4Client;
        internal Rectangle ImportantTitle5Client;
        internal Rectangle ImportantTitle6Client;
        internal Rectangle ImportantTitle7Client;
        internal Rectangle ImportantTitle8Client;
        internal Rectangle ImportantTitle9Client;
        internal Rectangle ImportantTitle10Client;
        internal Rectangle ImportantTitle11Client;
        internal Rectangle ImportantTitle12Client;
        internal Rectangle ImportantTitle13Client;
        internal Rectangle ImportantTitle14Client;
        internal Rectangle ImportantTitle15Client;
        internal Rectangle ImportantTitle16Client;
        internal Rectangle ImportantTitle17Client;
        internal Rectangle ImportantTitle18Client;
        internal Rectangle ImportantTitle19Client;
        internal Rectangle ImportantTitle20Client;
        internal string ImportantStunt1Description;
        internal string ImportantStunt2Description;
        internal string ImportantStunt3Description;
        internal string ImportantStunt4Description;
        internal string ImportantStunt5Description;
        internal string ImportantStunt6Description;
        internal string ImportantStunt7Description;
        internal string ImportantStunt8Description;
        internal string ImportantStunt9Description;
        internal string ImportantStunt10Description;
        internal string ImportantStunt11Description;
        internal string ImportantStunt12Description;
        internal string ImportantStunt13Description;
        internal string ImportantStunt14Description;
        internal string ImportantStunt15Description;
        internal string ImportantStunt16Description;
        internal string ImportantStunt17Description;
        internal string ImportantStunt18Description;
        internal string ImportantStunt19Description;
        internal string ImportantStunt20Description;

        internal PlatformTexture ImportantTitle1Picture;
        internal PlatformTexture ImportantTitle2Picture;
        internal PlatformTexture ImportantTitle3Picture;
        internal PlatformTexture ImportantTitle4Picture;
        internal PlatformTexture ImportantTitle5Picture;
        internal PlatformTexture ImportantTitle6Picture;
        internal PlatformTexture ImportantTitle7Picture;
        internal PlatformTexture ImportantTitle8Picture;
        internal PlatformTexture ImportantTitle9Picture;
        internal PlatformTexture ImportantTitle10Picture;
        internal PlatformTexture ImportantTitle11Picture;
        internal PlatformTexture ImportantTitle12Picture;
        internal PlatformTexture ImportantTitle13Picture;
        internal PlatformTexture ImportantTitle14Picture;
        internal PlatformTexture ImportantTitle15Picture;
        internal PlatformTexture ImportantTitle16Picture;
        internal PlatformTexture ImportantTitle17Picture;
        internal PlatformTexture ImportantTitle18Picture;
        internal PlatformTexture ImportantTitle19Picture;
        internal PlatformTexture ImportantTitle20Picture;
        internal Rectangle ImportantTitle1PictureClient;
        internal Rectangle ImportantTitle2PictureClient;
        internal Rectangle ImportantTitle3PictureClient;
        internal Rectangle ImportantTitle4PictureClient;
        internal Rectangle ImportantTitle5PictureClient;
        internal Rectangle ImportantTitle6PictureClient;
        internal Rectangle ImportantTitle7PictureClient;
        internal Rectangle ImportantTitle8PictureClient;
        internal Rectangle ImportantTitle9PictureClient;
        internal Rectangle ImportantTitle10PictureClient;
        internal Rectangle ImportantTitle11PictureClient;
        internal Rectangle ImportantTitle12PictureClient;
        internal Rectangle ImportantTitle13PictureClient;
        internal Rectangle ImportantTitle14PictureClient;
        internal Rectangle ImportantTitle15PictureClient;
        internal Rectangle ImportantTitle16PictureClient;
        internal Rectangle ImportantTitle17PictureClient;
        internal Rectangle ImportantTitle18PictureClient;
        internal Rectangle ImportantTitle19PictureClient;
        internal Rectangle ImportantTitle20PictureClient; 
        
        //↓重要特技
        internal int MaxImportantStuntShowNumber;
        internal string ShowImportantStuntName;
        internal string ShowImportantStuntNameBackground;
        internal string ShowImportantStuntPicture;
        internal string ImportantStuntTextFollowTheMouse;
        internal PlatformTexture ImportantStuntTextBackground;
        internal Rectangle ImportantStuntTextBackgroundClient;
        internal FreeRichText ImportantStuntText = new FreeRichText();
        internal Rectangle ImportantStuntTextClient;
        internal string ImportantStuntText1;
        internal string ImportantStuntText2;
        internal string ImportantStuntText3;
        internal string ImportantStuntText4;
        internal string ImportantStuntText5;

        internal int ImportantStunt1Group;
        internal int ImportantStunt2Group;
        internal int ImportantStunt3Group;
        internal int ImportantStunt4Group;
        internal int ImportantStunt5Group;
        internal int ImportantStunt6Group;
        internal int ImportantStunt7Group;
        internal int ImportantStunt8Group;
        internal int ImportantStunt9Group;
        internal int ImportantStunt10Group;
        internal int ImportantStunt11Group;
        internal int ImportantStunt12Group;
        internal int ImportantStunt13Group;
        internal int ImportantStunt14Group;
        internal int ImportantStunt15Group;
        internal int ImportantStunt16Group;
        internal int ImportantStunt17Group;
        internal int ImportantStunt18Group;
        internal int ImportantStunt19Group;
        internal int ImportantStunt20Group;

        internal FreeText ImportantStunt1NameText;
        internal FreeText ImportantStunt2NameText;
        internal FreeText ImportantStunt3NameText;
        internal FreeText ImportantStunt4NameText;
        internal FreeText ImportantStunt5NameText;
        internal FreeText ImportantStunt6NameText;
        internal FreeText ImportantStunt7NameText;
        internal FreeText ImportantStunt8NameText;
        internal FreeText ImportantStunt9NameText;
        internal FreeText ImportantStunt10NameText;
        internal FreeText ImportantStunt11NameText;
        internal FreeText ImportantStunt12NameText;
        internal FreeText ImportantStunt13NameText;
        internal FreeText ImportantStunt14NameText;
        internal FreeText ImportantStunt15NameText;
        internal FreeText ImportantStunt16NameText;
        internal FreeText ImportantStunt17NameText;
        internal FreeText ImportantStunt18NameText;
        internal FreeText ImportantStunt19NameText;
        internal FreeText ImportantStunt20NameText;
        internal PlatformTexture ImportantStunt1Background;
        internal PlatformTexture ImportantStunt2Background;
        internal PlatformTexture ImportantStunt3Background;
        internal PlatformTexture ImportantStunt4Background;
        internal PlatformTexture ImportantStunt5Background;
        internal PlatformTexture ImportantStunt6Background;
        internal PlatformTexture ImportantStunt7Background;
        internal PlatformTexture ImportantStunt8Background;
        internal PlatformTexture ImportantStunt9Background;
        internal PlatformTexture ImportantStunt10Background;
        internal PlatformTexture ImportantStunt11Background;
        internal PlatformTexture ImportantStunt12Background;
        internal PlatformTexture ImportantStunt13Background;
        internal PlatformTexture ImportantStunt14Background;
        internal PlatformTexture ImportantStunt15Background;
        internal PlatformTexture ImportantStunt16Background;
        internal PlatformTexture ImportantStunt17Background;
        internal PlatformTexture ImportantStunt18Background;
        internal PlatformTexture ImportantStunt19Background;
        internal PlatformTexture ImportantStunt20Background;
        internal Rectangle ImportantStunt1Client;
        internal Rectangle ImportantStunt2Client;
        internal Rectangle ImportantStunt3Client;
        internal Rectangle ImportantStunt4Client;
        internal Rectangle ImportantStunt5Client;
        internal Rectangle ImportantStunt6Client;
        internal Rectangle ImportantStunt7Client;
        internal Rectangle ImportantStunt8Client;
        internal Rectangle ImportantStunt9Client;
        internal Rectangle ImportantStunt10Client;
        internal Rectangle ImportantStunt11Client;
        internal Rectangle ImportantStunt12Client;
        internal Rectangle ImportantStunt13Client;
        internal Rectangle ImportantStunt14Client;
        internal Rectangle ImportantStunt15Client;
        internal Rectangle ImportantStunt16Client;
        internal Rectangle ImportantStunt17Client;
        internal Rectangle ImportantStunt18Client;
        internal Rectangle ImportantStunt19Client;
        internal Rectangle ImportantStunt20Client;

        internal PlatformTexture ImportantStunt1Picture;
        internal PlatformTexture ImportantStunt2Picture;
        internal PlatformTexture ImportantStunt3Picture;
        internal PlatformTexture ImportantStunt4Picture;
        internal PlatformTexture ImportantStunt5Picture;
        internal PlatformTexture ImportantStunt6Picture;
        internal PlatformTexture ImportantStunt7Picture;
        internal PlatformTexture ImportantStunt8Picture;
        internal PlatformTexture ImportantStunt9Picture;
        internal PlatformTexture ImportantStunt10Picture;
        internal PlatformTexture ImportantStunt11Picture;
        internal PlatformTexture ImportantStunt12Picture;
        internal PlatformTexture ImportantStunt13Picture;
        internal PlatformTexture ImportantStunt14Picture;
        internal PlatformTexture ImportantStunt15Picture;
        internal PlatformTexture ImportantStunt16Picture;
        internal PlatformTexture ImportantStunt17Picture;
        internal PlatformTexture ImportantStunt18Picture;
        internal PlatformTexture ImportantStunt19Picture;
        internal PlatformTexture ImportantStunt20Picture;
        internal Rectangle ImportantStunt1PictureClient;
        internal Rectangle ImportantStunt2PictureClient;
        internal Rectangle ImportantStunt3PictureClient;
        internal Rectangle ImportantStunt4PictureClient;
        internal Rectangle ImportantStunt5PictureClient;
        internal Rectangle ImportantStunt6PictureClient;
        internal Rectangle ImportantStunt7PictureClient;
        internal Rectangle ImportantStunt8PictureClient;
        internal Rectangle ImportantStunt9PictureClient;
        internal Rectangle ImportantStunt10PictureClient;
        internal Rectangle ImportantStunt11PictureClient;
        internal Rectangle ImportantStunt12PictureClient;
        internal Rectangle ImportantStunt13PictureClient;
        internal Rectangle ImportantStunt14PictureClient;
        internal Rectangle ImportantStunt15PictureClient;
        internal Rectangle ImportantStunt16PictureClient;
        internal Rectangle ImportantStunt17PictureClient;
        internal Rectangle ImportantStunt18PictureClient;
        internal Rectangle ImportantStunt19PictureClient;
        internal Rectangle ImportantStunt20PictureClient;

        //↓列传        
        internal Rectangle BiographyBriefButtonClient;
        internal PlatformTexture BiographyBriefButtonTexture;
        internal PlatformTexture BiographyBriefPressedTexture;
        internal Rectangle BiographyRomanceButtonClient;
        internal PlatformTexture BiographyRomanceButtonTexture;
        internal PlatformTexture BiographyRomancePressedTexture;
        internal Rectangle BiographyHistoryButtonClient;
        internal PlatformTexture BiographyHistoryButtonTexture;
        internal PlatformTexture BiographyHistoryPressedTexture;
        internal Rectangle BiographyInGameButtonClient;
        internal PlatformTexture BiographyInGameButtonTexture;
        internal PlatformTexture BiographyInGamePressedTexture;

        internal Rectangle PersonBiographyTextClient;
        internal FreeRichText PersonBiographyText = new FreeRichText();
        internal string PersonBiographyText1;
        internal string PersonBiographyText2;
        internal string PersonBiographyText3;
        internal string PersonBiographyText4;
        internal string PersonBiographyText5;

        ////↓人物关系
        internal Rectangle RelativeButtonClient;
        internal PlatformTexture RelativeButtonTexture;
        internal PlatformTexture RelativePressedTexture;
        internal Rectangle RelationButtonClient;
        internal PlatformTexture RelationButtonTexture;
        internal PlatformTexture RelationPressedTexture;
        internal Rectangle StandingsButtonClient;
        internal PlatformTexture StandingsButtonTexture;
        internal PlatformTexture StandingsPressedTexture;
        internal Rectangle PersonRelationButtonClient;
        internal PlatformTexture PersonRelationButtonTexture;
        internal PlatformTexture PersonRelationPressedTexture;
        //↓亲人
        internal PlatformTexture RelativeMask;
        internal PlatformTexture RelativeBackground;
        internal Rectangle RelativeBackgroundClient;

        internal int GenerationN;
        internal FreeText GenerationText;
        internal int StrainN;
        internal FreeText StrainText;
        
        internal Rectangle SmallPortraitClient;
        internal bool HasFather;
        internal String FatherName;
        internal FreeText FatherNameText;
        internal Rectangle FatherSmallPortraitClient;
        internal bool HasMother;
        internal String MotherName;
        internal FreeText MotherNameText;
        internal Rectangle MotherSmallPortraitClient;
        internal bool HasSpouse;
        internal String SpouseName;
        internal FreeText SpouseNameText;
        internal Rectangle SpouseSmallPortraitClient;

        internal int ChildrenN;
        internal FreeText ChildrenNumberText;
        internal Rectangle ChildrenTextClient;
        internal FreeRichText ChildrenText = new FreeRichText();
        internal string ChildrenText1;
        internal string ChildrenText2;
        internal string ChildrenText3;
        internal string ChildrenText4;
        internal string ChildrenText5;

        //↓兄弟喜恶
        internal PlatformTexture RelationMask;
        internal PlatformTexture RelationBackground;
        internal Rectangle RelationBackgroundClient;

        internal Rectangle BrothersTextClient;
        internal FreeRichText BrothersText = new FreeRichText();
        internal string VerticalForBrothersText;
        internal string BrothersText1;
        internal string BrothersText2;
        internal string BrothersText3;
        internal string BrothersText4;
        internal string BrothersText5;

        internal Rectangle ClosePersonsTextClient;
        internal FreeRichText ClosePersonsText = new FreeRichText();
        internal string VerticalForClosePersonsText;
        internal string ClosePersonsText1;
        internal string ClosePersonsText2;
        internal string ClosePersonsText3;
        internal string ClosePersonsText4;
        internal string ClosePersonsText5;

        internal Rectangle HatedPersonsTextClient;
        internal FreeRichText HatedPersonsText = new FreeRichText();
        internal string VerticalForHatedPersonsText;
        internal string HatedPersonsText1;
        internal string HatedPersonsText2;
        internal string HatedPersonsText3;
        internal string HatedPersonsText4;
        internal string HatedPersonsText5;
        //↓人物关系
        internal PlatformTexture PersonRelationMask;
        internal PlatformTexture PersonRelationBackground;
        internal Rectangle PersonRelationBackgroundClient;

        internal Rectangle PersonRelationTextClient;
        internal FreeRichText PersonRelationText = new FreeRichText();
        internal string PersonRelationText1;
        internal string PersonRelationText2;
        internal string PersonRelationText3;
        internal string PersonRelationText4;
        internal string PersonRelationText5;
        //↓战绩
        internal PlatformTexture StandingsMask;
        internal PlatformTexture StandingsBackground;
        internal Rectangle StandingsBackgroundClient;
        internal List<LabelText> StandingsTexts = new List<LabelText>();

        ////↓人物特长        
        internal PlatformTexture ConditionBackground;
        internal Rectangle ConditionBackgroundClient;
        internal PlatformTexture InfluenceBackground;
        internal Rectangle InfluenceBackgroundClient;
        internal int AllSkillTitleCount;
        internal PlatformTexture PersonSkillTextBackground;
        internal PlatformTexture LearnableSkillTextBackground;
        internal PlatformTexture AllSkillTextBackground;
        internal PlatformTexture TitleTextBackground;
        internal int TitleTextHeight;
        internal int TitleCountN;
        internal Rectangle TitleTextBackgroundClient;
        internal PlatformTexture StuntTextBackground;
        internal Rectangle StuntTextBackgroundClient;
        internal int StuntTextHeight;
        internal int StuntCountN;

        internal Rectangle TheConditionClient;
        internal FreeRichText TheConditionText = new FreeRichText();
        internal Rectangle TheInfluenceClient;
        internal FreeRichText TheInfluenceText = new FreeRichText();

        internal FreeTextList TheAllSkillTexts;
        internal FreeTextList TheLearnableSkillTexts;
        internal List<Skill> TheLinkedSkills = new List<Skill>();
        internal Rectangle TheTitleClient;
        internal FreeRichText TheTitleText = new FreeRichText();
        internal FreeTextList ThePersonSkillTexts;
        internal Point TheSkillBlockSize;
        internal Point TheSkillDisplayOffset;
        internal Rectangle TheStuntClient;
        internal FreeRichText TheStuntText = new FreeRichText();
       
        ////↓辅助判断
        bool InformationButton = false;
        bool BiographyButton = false; 

        bool BiographyBriefButton = false;
        bool BiographyRomanceButton = false;
        bool BiographyHistoryButton = false;
        bool BiographyInGameButton = false;

        bool ImportantTreasureButton = false;
        bool ImportantTreasureTextIng = false;
        bool ImportantTreasureTexting = false;
        bool ImportantTitleTextIng = false;
        bool ImportantTitleTexting = false;
        bool ImportantStuntTextIng = false;
        bool ImportantStuntTexting = false;

        bool RelativeButton = false;
        bool RelationButton = false;
        bool PersonRelationButton = false;
        bool StandingsButton = false;

        bool ConditionAndInfluenceText = false;



        //////////上面是添加的内容
        internal void Draw()
        {
            if (this.ShowingPerson != null)
            {
                Rectangle? sourceRectangle = null;

                CacheManager.Scale = Scale;

                if (Switch1 == "on")//新界面打开
                {                    
                    //↓人物信息页内容
                    if (InformationButton == true)
                    {
                        if ((Switch114 == "on" || Switch115 == "on") && BiographyButton == false)//人物宝物
                        {
                            CacheManager.Draw(this.ImportantTreasureButtonTexture, this.ImportantTreasureButtonClientDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);  //0.1821f)
                            CacheManager.Draw(this.ImportantTreasurePressedTexture, this.ImportantTreasurePressedClientDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }
                        if (Switch121 == "on" && BiographyButton == false)//人物亲人
                        {
                            CacheManager.Draw(this.RelativeButtonTexture, this.RelativeButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            CacheManager.Draw(this.RelativePressedTexture, this.RelativeButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }
                        if (Switch122 == "on" && BiographyButton == false)//人物兄弟喜恶
                        {
                            CacheManager.Draw(this.RelationButtonTexture, this.RelationButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            CacheManager.Draw(this.RelationPressedTexture, this.RelationButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }
                        if (Switch123 == "on" && RelationButton == true)//人物关系
                        {
                            CacheManager.Draw(this.PersonRelationButtonTexture, this.PersonRelationButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            CacheManager.Draw(this.PersonRelationPressedTexture, this.PersonRelationButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }
                        if (Switch124 == "on" && BiographyButton == false)//在人物详情页显示人物战绩
                        {
                            CacheManager.Draw(this.StandingsButtonTexture, this.StandingsButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            CacheManager.Draw(this.StandingsPressedTexture, this.StandingsButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }
                        if (Switch21 == "on")//在人物列传
                        {
                            CacheManager.Draw(this.BiographyButtonTexture, this.BiographyButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            CacheManager.Draw(this.BiographyPressedTexture, this.BiographyButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                        }

                        CacheManager.Draw(this.InformationBackgroundTexture, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0185f);
                        CacheManager.Draw(this.InformationMask2Texture, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01849f);

                        try
                        {
                            if (PortraitKindInInformation == "1")//人物半身像
                            {
                                //CacheManager.Draw(this.ShowingPerson.Portrait, this.PortraitInInformationDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1847f);

                                CacheManager.DrawZhsanAvatar(this.ShowingPerson, "", this.PortraitInInformationDisplayPosition, Color.White, 0.01847f);

                            }
                            else if (PortraitKindInInformation == "2")//人物全身像
                            {
                                //CacheManager.Draw(this.ShowingPerson.FullPortrait, this.PortraitInInformationDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1847f);

                                CacheManager.DrawZhsanAvatar(this.ShowingPerson, "f", this.PortraitInInformationDisplayPosition, Color.White, 0.01847f);
                            }

                            //if (PortraitKindInInformation == "1")//人物半身像
                            //{
                            //    CacheManager.Draw(this.ShowingPerson.Portrait, this.PortraitInInformationDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1847f);
                            //}
                            //else if (PortraitKindInInformation == "2")//人物全身像
                            //{
                            //    CacheManager.Draw(this.ShowingPerson.FullPortrait, this.PortraitInInformationDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1847f);

                            //}                               
                        }
                        catch { }

                        if (Switch106 == "on")//势力颜色
                        {
                            CacheManager.Draw(this.FactionColour, this.FactionColourDisplayPosition, null, this.TheFactionColor, 0f, Vector2.Zero, SpriteEffects.None, 0.01845f);
                            CacheManager.Draw(this.FactionColourBackground, this.FactionColourDisplayPosition, null, this.TheFactionColor, 0f, Vector2.Zero, SpriteEffects.None, 0.01846f);
                        }
                        if (Switch105 == "on")//势力名称
                        {
                            this.FactionNameText.Draw(0.01843f);
                        }
                        if (BiographyButton == false)
                        {
                            CacheManager.Draw(this.DetailBackgroundTexture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01844f);
                            CacheManager.Draw(this.DetailMask2Texture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018435f);
                            foreach (LabelText text in this.PersonInInformationTexts)
                            {
                                text.Label.Draw(0.01843f);
                                text.Text.Draw(0.01843f);
                            }
                            CacheManager.Draw(this.DetailMask1Texture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01842f);
                        }
                        this.ThePersonSkillTexts.Draw((float)0.018271f);
                        this.TheLearnableSkillTexts.Draw((float)0.018273f);
                        this.TheAllSkillTexts.Draw((float)0.018275f);
                        if (Switch132 == "on")//技能背景图片
                        {
                            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
                            {
                                Rectangle skillposition = new Rectangle(this.DisplayOffset.X + this.TheSkillDisplayOffset.X + (skill.DisplayCol * this.TheSkillBlockSize.X), this.DisplayOffset.Y + this.TheSkillDisplayOffset.Y + (skill.DisplayRow * this.TheSkillBlockSize.Y), this.TheSkillBlockSize.X, this.TheSkillBlockSize.Y);
                                CacheManager.Draw(this.AllSkillTextBackground, skillposition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018285f);
                                if (ShowingPerson.Skills.GetSkill(skill.ID) != null)
                                {
                                    CacheManager.Draw(this.PersonSkillTextBackground, skillposition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018281f);
                                }
                                else if (skill.CanLearn(ShowingPerson))
                                {
                                    CacheManager.Draw(this.LearnableSkillTextBackground, skillposition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018283f);
                                }
                            }
                        }
                        this.TheInfluenceText.Draw(0.01813f);
                        this.TheConditionText.Draw(0.01813f);
                        if (Switch131 == "on")//条件与影响背景图片
                        {
                            CacheManager.Draw(this.ConditionBackground, this.ConditionBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01815f);
                            CacheManager.Draw(this.InfluenceBackground, this.InfluenceBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01815f);
                        }

                        CacheManager.Draw(this.InformationMask1Texture, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0183f);

                        if (Switch117 == "on" && BiographyButton == false) //重要称号显示
                        {
                            CacheManager.Draw(this.TitleBackgroundTexture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                            CacheManager.Draw(this.TitleMask2Texture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018249f);

                            try
                            {
                                CacheManager.Draw(this.ImportantTitleTextBackground, this.ImportantTitleTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0182f);
                                this.ImportantTitleText.Draw(0.01813f);

                                if (ShowImportantTitlePicture == "on")
                                {
                                    for (int t = 1; t <= MaxImportantTitleShowNumber; t++)
                                    {
                                        if (HasTheImportantTitle(t) == true)
                                        {
                                            CacheManager.Draw(this.TheImportantTitlePicture(t), this.TheImportantTitlePictureDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018247f);
                                        }
                                    }
                                }
                                if (ShowImportantTitleNameBackground == "on")
                                {
                                    for (int t = 1; t <= MaxImportantTitleShowNumber; t++)
                                    {
                                        if (HasTheImportantTitle(t) == true)
                                        {
                                            CacheManager.Draw(this.TheImportantTitleBackground(t), this.TheImportantTitleDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                                        }
                                    }
                                }
                                if (ShowImportantTitleName == "on")
                                {
                                    if (HasTheImportantTitle(1) == true && MaxImportantTitleShowNumber > 0) { this.ImportantTitle1NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(2) == true && MaxImportantTitleShowNumber > 1) { this.ImportantTitle2NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(3) == true && MaxImportantTitleShowNumber > 2) { this.ImportantTitle3NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(4) == true && MaxImportantTitleShowNumber > 3) { this.ImportantTitle4NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(5) == true && MaxImportantTitleShowNumber > 4) { this.ImportantTitle5NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(6) == true && MaxImportantTitleShowNumber > 5) { this.ImportantTitle6NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(7) == true && MaxImportantTitleShowNumber > 6) { this.ImportantTitle7NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(8) == true && MaxImportantTitleShowNumber > 7) { this.ImportantTitle8NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(9) == true && MaxImportantTitleShowNumber > 8) { this.ImportantTitle9NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(10) == true && MaxImportantTitleShowNumber > 9) { this.ImportantTitle10NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(11) == true && MaxImportantTitleShowNumber > 10) { this.ImportantTitle11NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(12) == true && MaxImportantTitleShowNumber > 11) { this.ImportantTitle12NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(13) == true && MaxImportantTitleShowNumber > 12) { this.ImportantTitle13NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(14) == true && MaxImportantTitleShowNumber > 13) { this.ImportantTitle14NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(15) == true && MaxImportantTitleShowNumber > 14) { this.ImportantTitle15NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(16) == true && MaxImportantTitleShowNumber > 15) { this.ImportantTitle16NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(17) == true && MaxImportantTitleShowNumber > 16) { this.ImportantTitle17NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(18) == true && MaxImportantTitleShowNumber > 17) { this.ImportantTitle18NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(19) == true && MaxImportantTitleShowNumber > 18) { this.ImportantTitle19NameText.Draw(0.018243f); }
                                    if (HasTheImportantTitle(20) == true && MaxImportantTitleShowNumber > 19) { this.ImportantTitle20NameText.Draw(0.018243f); }
                                }
                            }
                            catch { }
                            CacheManager.Draw(this.TitleMask1Texture, this.TitleBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);

                        }
                        if (Switch118 == "on" && BiographyButton == false)//重要特技显示
                        {
                            CacheManager.Draw(this.StuntBackgroundTexture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                            CacheManager.Draw(this.StuntMask2Texture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018249f);

                            try
                            {
                                CacheManager.Draw(this.ImportantStuntTextBackground, this.ImportantStuntTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0182f);
                                this.ImportantStuntText.Draw(0.1813f);

                                if (ShowImportantStuntPicture == "on")
                                {
                                    for (int t = 1; t <= MaxImportantStuntShowNumber; t++)
                                    {
                                        if (HasTheImportantStunt(t) == true)
                                        {
                                            CacheManager.Draw(this.TheImportantStuntPicture(t), this.TheImportantStuntPictureDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018247f);
                                        }
                                    }
                                }
                                if (ShowImportantStuntNameBackground == "on")
                                {
                                    for (int t = 1; t <= MaxImportantStuntShowNumber; t++)
                                    {
                                        if (HasTheImportantStunt(t) == true)
                                        {
                                            CacheManager.Draw(this.TheImportantStuntBackground(t), this.TheImportantStuntDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                                        }
                                    }
                                }
                                if (ShowImportantStuntName == "on")
                                {
                                    if (HasTheImportantStunt(1) == true && MaxImportantStuntShowNumber > 0) { this.ImportantStunt1NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(2) == true && MaxImportantStuntShowNumber > 1) { this.ImportantStunt2NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(3) == true && MaxImportantStuntShowNumber > 2) { this.ImportantStunt3NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(4) == true && MaxImportantStuntShowNumber > 3) { this.ImportantStunt4NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(5) == true && MaxImportantStuntShowNumber > 4) { this.ImportantStunt5NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(6) == true && MaxImportantStuntShowNumber > 5) { this.ImportantStunt6NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(7) == true && MaxImportantStuntShowNumber > 6) { this.ImportantStunt7NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(8) == true && MaxImportantStuntShowNumber > 7) { this.ImportantStunt8NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(9) == true && MaxImportantStuntShowNumber > 8) { this.ImportantStunt9NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(10) == true && MaxImportantStuntShowNumber > 9) { this.ImportantStunt10NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(11) == true && MaxImportantStuntShowNumber > 10) { this.ImportantStunt11NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(12) == true && MaxImportantStuntShowNumber > 11) { this.ImportantStunt12NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(13) == true && MaxImportantStuntShowNumber > 12) { this.ImportantStunt13NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(14) == true && MaxImportantStuntShowNumber > 13) { this.ImportantStunt14NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(15) == true && MaxImportantStuntShowNumber > 14) { this.ImportantStunt15NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(16) == true && MaxImportantStuntShowNumber > 15) { this.ImportantStunt16NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(17) == true && MaxImportantStuntShowNumber > 16) { this.ImportantStunt17NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(18) == true && MaxImportantStuntShowNumber > 17) { this.ImportantStunt18NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(19) == true && MaxImportantStuntShowNumber > 18) { this.ImportantStunt19NameText.Draw(0.018243f); }
                                    if (HasTheImportantStunt(20) == true && MaxImportantStuntShowNumber > 19) { this.ImportantStunt20NameText.Draw(0.018243f); }
                                }
                            }
                            catch { }
                            CacheManager.Draw(this.StuntMask1Texture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);

                        }
                        if (ImportantTreasureButton == true)
                        {
                            CacheManager.Draw(this.TreasureBackgroundTexture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                            CacheManager.Draw(this.TreasureMask2Texture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018249f);

                            if (Switch114 == "on")//重要宝物显示
                            {
                                CacheManager.Draw(this.ImportantTreasureBackground, this.ImportantTreasureBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018247f);
                                try
                                {
                                    if (Switch116 == "on")
                                    {
                                        if (ThePersonPictureKindInTreasure == "1")//性别像
                                        {
                                            CacheManager.Draw(this.SexPicture, this.ThePersonPictureInTreasureDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018246f);
                                        }
                                        if (ThePersonPictureKindInTreasure == "2")//全身像
                                        {
                                            CacheManager.DrawZhsanAvatar(this.ShowingPerson, "f", this.ThePersonPictureInTreasureDisplayPosition, Color.White, 0.018246f);
                                            //CacheManager.Draw(this.ShowingPerson.FullPortrait, this.ThePersonPictureInTreasureDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18246f);
                                        }
                                    }

                                    for (int t = 1; t <= MaxImportantTreasureShowNumber; t++)
                                    {
                                        if (HasTheImportantTreasure(t) == true)
                                        {
                                            CacheManager.Draw(this.TheImportantTreasurePicture(t), this.TheImportantTreasureDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                                            CacheManager.Draw(this.ImportantTreasureShowMask, this.TheImportantTreasureDisplayPosition(t), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018244f);
                                        }
                                    }
                                }
                                catch { }
                                CacheManager.Draw(this.ImportantTreasureMask, this.ImportantTreasureBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018243f);
                            }
                            if (Switch115 == "on")
                            {
                                CacheManager.Draw(this.GeneralTreasureBackground, this.GeneralTreasureBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018247f);

                                for (int i = 0; i < MaxGeneralTreasureShowNumber; i++)
                                {
                                    CacheManager.Draw(this.TheGeneralTreasurePicture(i), this.TheGeneralTreasureDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                                    CacheManager.Draw(this.GeneralTreasureShowMask, this.TheGeneralTreasureDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018244f);
                                    if (ShowNullTreasurePicture == "on")
                                    {
                                        CacheManager.Draw(this.GeneralTreasureShowBackground, this.GeneralTreasureDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018246f);
                                    }
                                }

                                CacheManager.Draw(this.GeneralTreasureMask, this.GeneralTreasureBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018243f);
                            }

                            CacheManager.Draw(this.TreasureMask1Texture, this.StuntBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);

                            CacheManager.Draw(this.ImportantTreasureTextPicture, this.ImportantTreasureTextPictureDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01815f);
                            CacheManager.Draw(this.ImportantTreasureTextBackground, this.ImportantTreasureTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0182f);
                            CacheManager.Draw(this.ImportantTreasureTextMask, this.ImportantTreasureTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0181f);
                            this.ImportantTreasureText.Draw(0.1813f);

                        }

                    }
                    if (RelativeButton == true)//人物亲人
                    {
                        CacheManager.Draw(this.RelativeMask, this.RelativeBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);
                        CacheManager.Draw(this.RelativeBackground, this.RelativeBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                        this.GenerationText.Draw(0.018243f);
                        this.StrainText.Draw(0.018243f);

                        Person person = null;
                        float depth = 0.018247f;
                        person = this.ShowingPerson;

                        CacheManager.DrawZhsanAvatar(person, "s", this.SmallPortraitDisplayPosition, Color.White, depth);

                        //CacheManager.Draw(this.ShowingPerson.SmallPortrait, this.SmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        if (HasFather == true)
                        {
                            person = this.ShowingPerson.Father;
                            CacheManager.DrawZhsanAvatar(person, "s", this.FatherSmallPortraitDisplayPosition, Color.White, depth);
                            //CacheManager.Draw(this.ShowingPerson.FatherSmallPortrait, this.FatherSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        }
                        this.FatherNameText.Draw(0.018243f);
                        if (HasMother == true)
                        {
                            person = this.ShowingPerson.Mother;
                            CacheManager.DrawZhsanAvatar(person, "s", this.MotherSmallPortraitDisplayPosition, Color.White, depth);
                            //CacheManager.Draw(this.ShowingPerson.MotherSmallPortrait, this.MotherSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        }
                        this.MotherNameText.Draw(0.18243f);
                        if (HasSpouse == true)
                        {
                            person = this.ShowingPerson.Spouse;
                            CacheManager.DrawZhsanAvatar(person, "s", this.SpouseSmallPortraitDisplayPosition, Color.White, depth);
                            //CacheManager.Draw(this.ShowingPerson.SpouseSmallPortrait, this.SpouseSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        }

                        ////CacheManager.Draw(this.ShowingPerson.SmallPortrait, this.SmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        //if (HasFather == true)
                        //{
                        //    CacheManager.Draw(this.ShowingPerson.FatherSmallPortrait, this.FatherSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        //}
                        //this.FatherNameText.Draw(0.18243f);
                        //if (HasMother == true)
                        //{
                        //    CacheManager.Draw(this.ShowingPerson.MotherSmallPortrait, this.MotherSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        //}
                        //this.MotherNameText.Draw(0.18243f);
                        //if (HasSpouse == true)
                        //{
                        //    CacheManager.Draw(this.ShowingPerson.SpouseSmallPortrait, this.SpouseSmallPortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.18247f);
                        //}
                        this.SpouseNameText.Draw(0.018243f);
                        this.ChildrenNumberText.Draw(0.018243f);
                        if (ChildrenN > 0)
                        {
                            this.ChildrenText.Draw(0.018243f);
                        }
                    }
                    if (RelationButton == true)//人物兄弟喜恶
                    {
                        CacheManager.Draw(this.RelationMask, this.RelationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);
                        CacheManager.Draw(this.RelationBackground, this.RelationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                        this.BrothersText.Draw(0.018243f);
                        this.ClosePersonsText.Draw(0.018243f);
                        this.HatedPersonsText.Draw(0.018243f);
                    }

                    if (PersonRelationButton == true)//人物关系
                    {
                        CacheManager.Draw(this.PersonRelationMask, this.PersonRelationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);
                        CacheManager.Draw(this.PersonRelationBackground, this.PersonRelationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                        this.PersonRelationText.Draw(0.18243f);
                    }


                    if (StandingsButton == true)//人物战绩
                    {
                        CacheManager.Draw(this.StandingsMask, this.StandingsBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);
                        CacheManager.Draw(this.StandingsBackground, this.StandingsBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                        foreach (LabelText text in this.StandingsTexts)
                        {
                            text.Label.Draw(0.018243f);
                            text.Text.Draw(0.018243f);
                        }
                    }
                    /////////
                    if (BiographyButton == true)//↓人物列传
                    {
                        CacheManager.Draw(this.BiographyBackgroundTexture, this.BiographyBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01825f);
                        CacheManager.Draw(this.BiographyMask2Texture, this.BiographyBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018249f);

                        if (Switch24 == "on")//在人物列传分页
                        {
                            if (Switch26 == "on")//列传
                            {
                                CacheManager.Draw(this.BiographyBriefButtonTexture, this.BiographyBriefButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                                CacheManager.Draw(this.BiographyBriefPressedTexture, this.BiographyBriefButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            }
                            if (Switch27 == "on")//演义
                            {
                                CacheManager.Draw(this.BiographyRomanceButtonTexture, this.BiographyRomanceButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                                CacheManager.Draw(this.BiographyRomancePressedTexture, this.BiographyRomanceButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            }
                            if (Switch28 == "on")//历史
                            {
                                CacheManager.Draw(this.BiographyHistoryButtonTexture, this.BiographyHistoryButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                                CacheManager.Draw(this.BiographyHistoryPressedTexture, this.BiographyHistoryButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            }
                            if (Switch29 == "on")//剧本
                            {
                                CacheManager.Draw(this.BiographyInGameButtonTexture, this.BiographyInGameButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                                CacheManager.Draw(this.BiographyInGamePressedTexture, this.BiographyInGameButtonPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01821f);
                            }
                            this.PersonBiographyText.Draw(0.018235f);
                        }
                        else
                        {
                            this.PersonBiographyText.Draw(0.018235f);
                        }
                        if (Switch133 == "on")//称号背景图片
                        {
                            CacheManager.Draw(this.TitleTextBackground, this.TitleTextBackgroundDisplayPosition, this.TitleCountDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                        }
                        this.TheTitleText.Draw(0.018243f);
                        if (Switch134 == "on")//称号背景图片
                        {
                            CacheManager.Draw(this.StuntTextBackground, this.StuntTextBackgroundDisplayPosition, this.StuntCountDisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018245f);
                        }
                        this.TheStuntText.Draw(0.018243f);
                        CacheManager.Draw(this.BiographyMask1Texture, this.BiographyBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01823f);


                    }
                    
                }
                ///////
                if (Switch1 == "off")//旧界面
                {

                    CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.019f);
                    CacheManager.Draw(this.BackgroundMask2, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0189f);
                    //CacheManager.Draw(this.ShowingPerson.Portrait, this.PortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.185f);
                    CacheManager.Draw(this.BackgroundMask1, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0183f);

                    foreach (LabelText text in this.LabelTexts)
                    {
                        text.Label.Draw(0.0184f);
                        text.Text.Draw(0.0184f);
                    }
                    this.TitleText.Draw(0.0184f);
                    //this.GuanzhiText.Draw(0.184f);
                    this.AllSkillTexts.Draw((float)0.01837f);
                    this.PersonSkillTexts.Draw((float)0.01833f);
                    this.LearnableSkillTexts.Draw((float)0.01835f);
                    this.StuntText.Draw(0.0184f);
                    this.InfluenceText.Draw(0.01831f);
                    this.ConditionText.Draw(0.01831f);
                    this.BiographyText.Draw(0.0184f);
                    this.PersonTreasuresText.Draw(0.0184f);//宝物显示
                }

                this.SurNameText.Draw(0.01827f);
                this.GivenNameText.Draw(0.01827f);
                this.CalledNameText.Draw(0.01827f);

                CacheManager.Scale = Vector2.One;
            }
        }

        internal void Initialize()
        {
            
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            CacheManager.Scale = Scale;
            if (Switch1 == "off")
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
            if (StaticMethods.PointInRectangle(position, new Rectangle(this.PersonTreasuresText.DisplayOffset.X, this.PersonTreasuresText.DisplayOffset.Y, this.PersonTreasuresTextClient.Width, this.PersonTreasuresTextClient.Height)))
            {
                    if (this.PersonTreasuresText.CurrentPageIndex < (this.PersonTreasuresText.PageCount - 1))
                    {
                        this.PersonTreasuresText.NextPage();
                    }
                    else if (this.PersonTreasuresText.CurrentPageIndex == (this.PersonTreasuresText.PageCount - 1))
                    {
                        this.PersonTreasuresText.FirstPage();
                    }
            }
            //////////以下新界面相关
            if (Switch1 == "on")
            {
                if (BiographyButton == true && StaticMethods.PointInRectangle(position, new Rectangle(this.PersonBiographyText.DisplayOffset.X, this.PersonBiographyText.DisplayOffset.Y, this.PersonBiographyTextClient.Width, this.PersonBiographyTextClient.Height)))
                {
                        if (this.PersonBiographyText.CurrentPageIndex < (this.PersonBiographyText.PageCount - 1))
                        {
                            this.PersonBiographyText.NextPage();
                        }
                        else if (this.PersonBiographyText.CurrentPageIndex == (this.PersonBiographyText.PageCount - 1))
                        {
                            this.PersonBiographyText.FirstPage();
                        }
                }
                /////  
                bool ImportantTreasure = false;
                if (ImportantTreasureButton == true)
                {
                    ImportantTreasure = true;
                }
                if (ImportantTreasure == false && BiographyButton == false && StaticMethods.PointInRectangle(position, this.ImportantTreasureButtonClientDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        ImportantTreasureButton = true;
                }
                if (ImportantTreasure == true && BiographyButton == false && StaticMethods.PointInRectangle(position, this.ImportantTreasurePressedClientDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        ImportantTreasureButton = false;
                }
                /////            
                if (BiographyBriefButton == false && BiographyButton == true && StaticMethods.PointInRectangle(position, this.BiographyBriefButtonDisplayPosition))
                {
                    if (InputManager.IsPressed)
                    {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                        }
                        BiographyBriefButton = true;
                        BiographyRomanceButton = false;
                        BiographyHistoryButton = false;
                        BiographyInGameButton = false;
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText1, this.PersonBiographyText.TitleColor);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.Brief, this.PersonBiographyText.TitleColor);
                        this.PersonBiographyText.ResortTexts();
                    }
                }
                if (BiographyRomanceButton == false && BiographyButton == true && StaticMethods.PointInRectangle(position, this.BiographyRomanceButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                        }
                        BiographyBriefButton = false;
                        BiographyRomanceButton = true;
                        BiographyHistoryButton = false;
                        BiographyInGameButton = false;
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText2, this.PersonBiographyText.SubTitleColor);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.Romance, this.PersonBiographyText.SubTitleColor);
                        this.PersonBiographyText.ResortTexts();
                }
                if (BiographyHistoryButton == false && BiographyButton == true && StaticMethods.PointInRectangle(position, this.BiographyHistoryButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                        }
                        BiographyBriefButton = false;
                        BiographyRomanceButton = false;
                        BiographyHistoryButton = true;
                        BiographyInGameButton = false;
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText3, this.PersonBiographyText.SubTitleColor2);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.History, this.PersonBiographyText.SubTitleColor2);
                        this.PersonBiographyText.ResortTexts();
                }
                if (BiographyInGameButton == false && BiographyButton == true && StaticMethods.PointInRectangle(position, this.BiographyInGameButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                        }
                        BiographyBriefButton = false;
                        BiographyRomanceButton = false;
                        BiographyHistoryButton = false;
                        BiographyInGameButton = true;
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText4, this.PersonBiographyText.SubTitleColor3);
                        String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                        foreach (String s in lineBrokenText)
                        {
                            this.PersonBiographyText.AddText(s, this.PersonBiographyText.SubTitleColor3);
                            this.PersonBiographyText.AddNewLine();
                        }
                        this.PersonBiographyText.ResortTexts();
                }
                /////
                bool Biography = false;

                bool Relative = false;
                bool Relation = false;
                bool Standings = false;
                bool PersonRelation = false;
                //
                if (BiographyButton == true)
                {
                    Biography = true;
                }
                if (RelativeButton == true)
                {
                    Relative = true;
                }
                if (RelationButton == true)
                {
                    Relation = true;
                }
                if (StandingsButton == true)
                {
                    Standings = true;
                }
                if (PersonRelationButton == true)
                {
                    PersonRelation = true;
                }
                //
                if (Biography == false && StaticMethods.PointInRectangle(position, this.BiographyButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        BiographyButton = true;
                        RelativeButton = false;
                        RelationButton = false;
                        StandingsButton = false;
                        PersonRelationButton = false;
                }
                if (Biography == true && StaticMethods.PointInRectangle(position, this.BiographyButtonPressedDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        BiographyButton = false;
                }
                //
                if (PersonRelation == false && BiographyButton == false && StaticMethods.PointInRectangle(position, this.PersonRelationButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        PersonRelationButton = true;
                }
                if (PersonRelation == true && BiographyButton == false && StaticMethods.PointInRectangle(position, this.PersonRelationButtonPressedDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        PersonRelationButton = false;
                }
                //
                if (Relative == false && BiographyButton == false && StaticMethods.PointInRectangle(position, this.RelativeButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        RelativeButton = true;
                        RelationButton = false;
                        StandingsButton = false;
                        PersonRelationButton = false;
                }
                if (Relation == false && BiographyButton == false && StaticMethods.PointInRectangle(position, this.RelationButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        RelativeButton = false;
                        RelationButton = true;
                        StandingsButton = false;
                        PersonRelationButton = false;
                }
                if (Standings == false && BiographyButton == false && StaticMethods.PointInRectangle(position, this.StandingsButtonDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                        }
                        RelativeButton = false;
                        RelationButton = false;
                        StandingsButton = true;
                        PersonRelationButton = false;
                }
                if (Relative == true && BiographyButton == false && StaticMethods.PointInRectangle(position, this.RelativeButtonPressedDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        RelativeButton = false;
                }
                if (Relation == true && BiographyButton == false && StaticMethods.PointInRectangle(position, this.RelationButtonPressedDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        RelationButton = false;
                }
                if (Standings == true && BiographyButton == false && StaticMethods.PointInRectangle(position, this.StandingsButtonPressedDisplayPosition))
                {
                        if (Switch3 == "on")
                        {
                            Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
                        }
                        StandingsButton = false;
                }
            }
            //////////以上新界面相关
            CacheManager.Scale = Vector2.One;
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            CacheManager.Scale = Scale;
            if (Switch1 == "off")
            {
                bool flag = false;
                if (StaticMethods.PointInRectangle(position, this.TitleDisplayPosition))
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
                                    this.PersonTreasuresText.Clear();
                                    this.InfluenceText.Clear();
                                    this.InfluenceText.AddText(title.DetailedName, this.InfluenceText.TitleColor);
                                    this.InfluenceText.AddNewLine();
                                    foreach (Influence influence in title.Influences.Influences.Values)
                                    {
                                        this.InfluenceText.AddText(influence.Description);
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

                if (StaticMethods.PointInRectangle(position, this.StuntDisplayPosition))
                {
                    int num2 = (position.Y - this.StuntText.DisplayOffset.Y) / this.StuntText.RowHeight;
                    //if (num2 > 1)
                    if (num2 >= 0)
                    {
                        //int num3 = num2 - 2;
                        int num3 = num2;
                        if (this.ShowingPerson.Stunts.Count > num3)
                        {
                            Stunt stunt = this.ShowingPerson.Stunts.GetStuntList()[num3] as Stunt;
                            if (stunt != null)
                            {
                                if (this.current != stunt)
                                {
                                    this.BiographyText.Clear();
                                    this.PersonTreasuresText.Clear();
                                    this.InfluenceText.Clear();
                                    this.InfluenceText.AddText("战斗特技", this.InfluenceText.TitleColor);
                                    this.InfluenceText.AddText(stunt.Name, this.InfluenceText.SubTitleColor);
                                    this.InfluenceText.AddNewLine();
                                    this.InfluenceText.AddText("持续天数", this.InfluenceText.SubTitleColor2);
                                    this.InfluenceText.AddText(stunt.Period.ToString(), this.InfluenceText.SubTitleColor3);
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
                                this.PersonTreasuresText.Clear();
                                this.InfluenceText.Clear();
                                if (this.LinkedSkills[i].InfluenceCount > 0)
                                {
                                    this.InfluenceText.AddText("技能", this.InfluenceText.TitleColor);
                                    this.InfluenceText.AddText(this.LinkedSkills[i].Name, this.InfluenceText.SubTitleColor);
                                    this.InfluenceText.AddNewLine();
                                    foreach (Influence influence in this.LinkedSkills[i].Influences.Influences.Values)
                                    {
                                        this.InfluenceText.AddText(influence.Description);
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
                            this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Brief);
                            this.BiographyText.AddNewLine();
                            this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                            this.BiographyText.AddText("：" + this.ShowingPerson.PersonBiography.Romance);
                            this.BiographyText.AddNewLine();
                            this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                            this.BiographyText.AddText("：" + this.ShowingPerson.PersonBiography.History);
                            this.BiographyText.AddNewLine();
                            this.BiographyText.AddText("剧本", Color.Cyan);
                            this.BiographyText.AddText("：");
                            String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                            foreach (String s in lineBrokenText)
                            {
                                this.BiographyText.AddText(s);
                                this.BiographyText.AddNewLine();
                            }
                            this.BiographyText.ResortTexts();
                        }
                        this.PersonTreasuresText.Clear();
                        if (ShowPersonTreasuresCount == "on")
                        {
                            this.PersonTreasuresText.AddText(PersonTreasuresText1, this.PersonTreasuresText.TitleColor);
                            this.PersonTreasuresText.AddText(PersonTreasuresText2);
                            this.PersonTreasuresText.AddText(EffectiveTreasuresCount.ToString(), Color.Cyan);
                            this.PersonTreasuresText.AddText(PersonTreasuresText3);
                            this.PersonTreasuresText.AddText(TreasuresCount.ToString(), Color.White);
                            this.PersonTreasuresText.AddText(PersonTreasuresText4);
                            this.PersonTreasuresText.AddNewLine();
                            this.PersonTreasuresText.AddText(PersonTreasuresText5);
                            this.PersonTreasuresText.AddNewLine();
                        }
                        if (TreasuresCount > 0)
                        {
                            this.PersonTreasuresText.AddText(PersonTreasuresText6, this.PersonTreasuresText.SubTitleColor);
                            this.PersonTreasuresText.AddNewLine();
                            foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
                            {
                                this.PersonTreasuresText.AddText(PersonTreasuresText11);
                                if (ShowPersonTreasuresWorth == "on" || ShowPersonTreasuresDescription == "on")
                                {
                                    this.PersonTreasuresText.AddText(PersonTreasuresText8, this.PersonTreasuresText.TitleColor);
                                }
                                this.PersonTreasuresText.AddText(t.Name);
                                if (ShowPersonTreasuresWorth == "on")
                                {
                                    this.PersonTreasuresText.AddNewLine();
                                    this.PersonTreasuresText.AddText(PersonTreasuresText9, this.PersonTreasuresText.TitleColor);
                                    this.PersonTreasuresText.AddText(t.Worth.ToString(), this.PersonTreasuresText.SubTitleColor2);
                                }
                                if (ShowPersonTreasuresDescription == "on")
                                {
                                    this.PersonTreasuresText.AddNewLine();
                                    this.PersonTreasuresText.AddText(PersonTreasuresText10, this.PersonTreasuresText.TitleColor);
                                    this.PersonTreasuresText.AddText(t.Description, this.PersonTreasuresText.SubTitleColor3);
                                }
                                this.PersonTreasuresText.AddText(PersonTreasuresText12);
                                this.PersonTreasuresText.AddNewLine();
                            }
                        }
                        if (NoEffectiveTreasuresCount > 0)
                        {
                            this.PersonTreasuresText.AddText(PersonTreasuresText7, this.PersonTreasuresText.SubTitleColor);
                            this.PersonTreasuresText.AddNewLine();
                            foreach (Treasure t in this.ShowingPerson.Treasures)
                            {
                                int ID = 0;
                                ID = t.ID;
                                if (IsTheNoEffectiveTreasure(ID)==true)
                                {
                                    this.PersonTreasuresText.AddText(PersonTreasuresText13);
                                    if (ShowPersonTreasuresWorth == "on" || ShowPersonTreasuresDescription == "on")
                                    {
                                        this.PersonTreasuresText.AddText(PersonTreasuresText8, this.PersonTreasuresText.TitleColor);
                                    }
                                    this.PersonTreasuresText.AddText(t.Name);
                                    if (ShowPersonTreasuresWorth == "on")
                                    {
                                        this.PersonTreasuresText.AddNewLine();
                                        this.PersonTreasuresText.AddText(PersonTreasuresText9, this.PersonTreasuresText.TitleColor);
                                        this.PersonTreasuresText.AddText(t.Worth.ToString(), this.PersonTreasuresText.SubTitleColor2);
                                    }
                                    if (ShowPersonTreasuresDescription == "on")
                                    {
                                        this.PersonTreasuresText.AddNewLine();
                                        this.PersonTreasuresText.AddText(PersonTreasuresText10, this.PersonTreasuresText.TitleColor);
                                        this.PersonTreasuresText.AddText(t.Description, this.PersonTreasuresText.SubTitleColor3);
                                    }
                                    this.PersonTreasuresText.AddText(PersonTreasuresText14);
                                    this.PersonTreasuresText.AddNewLine();
                                }
                            }
                        }
                        this.PersonTreasuresText.ResortTexts();
                    }
                }
            }
            //////////以下添加
            //技能称号特技介绍
           if (Switch1 == "on")
            {
                bool flag0 = false;
                if (InformationButton == true && BiographyButton == true && StaticMethods.PointInRectangle(position, this.NewTitleDisplayPosition))
                {
                    int num2 = 0;
                    if (this.TheTitleText.RowHeight > 0)
                    {
                        num2 = (position.Y - this.TheTitleText.DisplayOffset.Y) / this.TheTitleText.RowHeight;
                    }                     
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
                                    ConditionAndInfluenceText = true;
                                    this.TheInfluenceText.Clear();
                                    this.TheInfluenceText.AddText(title.DetailedName, this.TheInfluenceText.TitleColor);
                                    this.TheInfluenceText.AddNewLine();
                                    foreach (Influence influence in title.Influences.Influences.Values)
                                    {
                                        this.TheInfluenceText.AddText(influence.Description);
                                        this.TheInfluenceText.AddNewLine();
                                    }
                                    this.TheInfluenceText.ResortTexts();
                                    this.TheConditionText.Clear();
                                    this.TheConditionText.AddText("修习条件", this.TheConditionText.TitleColor);
                                    this.TheConditionText.AddNewLine();
                                    foreach (Condition condition in title.Conditions.Conditions.Values)
                                    {
                                        if (condition.CheckCondition(this.ShowingPerson))
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                        }
                                        this.TheConditionText.AddNewLine();
                                    }
                                    foreach (Condition condition in title.ArchitectureConditions.Conditions.Values)
                                    {
                                        if (this.ShowingPerson.LocationArchitecture != null && condition.CheckCondition(this.ShowingPerson.LocationArchitecture))
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                        }
                                        this.TheConditionText.AddNewLine();
                                    }
                                    foreach (Condition condition in title.FactionConditions.Conditions.Values)
                                    {
                                        if (this.ShowingPerson.BelongedFaction != null && condition.CheckCondition(this.ShowingPerson.BelongedFaction))
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                        }
                                        this.TheConditionText.AddNewLine();
                                    }

                                    this.TheConditionText.ResortTexts();
                                    this.current = title;
                                }
                                flag0 = true;
                            }
                        }
                    }
                }
                if (InformationButton == true && BiographyButton == true && StaticMethods.PointInRectangle(position, this.NewStuntDisplayPosition))
                {
                    int num2 = 0;

                    if (this.TheStuntText.RowHeight > 0)
                    {
                        num2 = (position.Y - this.TheStuntText.DisplayOffset.Y) / this.TheStuntText.RowHeight;
                    }
                    //if (num2 > 1)
                    if (num2 >= 0)
                    {
                        //int num3 = num2 - 2;
                        int num3 = num2;
                        if (this.ShowingPerson.Stunts.Count > num3)
                        {
                            Stunt stunt = this.ShowingPerson.Stunts.GetStuntList()[num3] as Stunt;
                            if (stunt != null)
                            {
                                if (this.current != stunt)
                                {
                                    ConditionAndInfluenceText = true;
                                    this.TheInfluenceText.Clear();
                                    this.TheInfluenceText.AddText("战斗特技", this.TheInfluenceText.TitleColor);
                                    this.TheInfluenceText.AddText(stunt.Name, this.TheInfluenceText.SubTitleColor);
                                    this.TheInfluenceText.AddNewLine();
                                    this.TheInfluenceText.AddText("持续天数", this.TheInfluenceText.SubTitleColor2);
                                    this.TheInfluenceText.AddText(stunt.Period.ToString(), this.TheInfluenceText.SubTitleColor3);
                                    this.TheInfluenceText.AddText("天", this.TheInfluenceText.SubTitleColor2);
                                    this.TheInfluenceText.AddNewLine();
                                    foreach (Influence influence in stunt.Influences.Influences.Values)
                                    {
                                        this.TheInfluenceText.AddText(influence.Description);
                                        this.TheInfluenceText.AddNewLine();
                                    }
                                    this.TheInfluenceText.ResortTexts();
                                    this.TheConditionText.Clear();
                                    this.TheConditionText.AddText("使用条件", this.TheConditionText.TitleColor);
                                    this.TheConditionText.AddNewLine();
                                    if ((this.ShowingPerson.LocationTroop != null) && (this.ShowingPerson == this.ShowingPerson.LocationTroop.Leader))
                                    {
                                        foreach (Condition condition in stunt.CastConditions.Conditions.Values)
                                        {
                                            if (condition.CheckCondition(this.ShowingPerson.LocationTroop))
                                            {
                                                this.TheConditionText.AddText(condition.Name, this.ConditionText.PositiveColor);
                                            }
                                            else
                                            {
                                                this.TheConditionText.AddText(condition.Name, this.ConditionText.NegativeColor);
                                            }
                                            this.TheConditionText.AddNewLine();
                                        }
                                    }
                                    else
                                    {
                                        foreach (Condition condition in stunt.CastConditions.Conditions.Values)
                                        {
                                            this.TheConditionText.AddText(condition.Name);
                                            this.TheConditionText.AddNewLine();
                                        }
                                    }
                                    this.TheConditionText.AddNewLine();
                                    this.TheConditionText.AddText("修习条件", this.ConditionText.SubTitleColor);
                                    this.TheConditionText.AddNewLine();
                                    foreach (Condition condition in stunt.LearnConditions.Conditions.Values)
                                    {
                                        if (condition.CheckCondition(this.ShowingPerson))
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.TheConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.TheConditionText.AddText(condition.Name, this.TheConditionText.NegativeColor);
                                        }
                                        this.TheConditionText.AddNewLine();
                                    }
                                    this.TheConditionText.ResortTexts();
                                    this.current = stunt;
                                }
                                flag0 = true;
                            }
                        }
                    }
                }
                if (!flag0)
                {
                    for (int i = 0; i < this.AllSkillTitleCount; i++)//this.AllSkillTexts.Count; i++)
                    {
                        if (InformationButton == true && StaticMethods.PointInRectangle(position, this.TheAllSkillTexts[i].AlignedPosition))
                        {
                            if (this.current != this.TheLinkedSkills[i])
                            {                                
                                ConditionAndInfluenceText = true;
                                this.TheInfluenceText.Clear();
                                if (this.TheLinkedSkills[i].InfluenceCount > 0)
                                {
                                    this.TheInfluenceText.AddText("技能", this.TheInfluenceText.TitleColor);
                                    this.TheInfluenceText.AddText(this.TheLinkedSkills[i].Name, this.TheInfluenceText.SubTitleColor);
                                    this.TheInfluenceText.AddNewLine();
                                    foreach (Influence influence in this.TheLinkedSkills[i].Influences.Influences.Values)
                                    {
                                        this.TheInfluenceText.AddText(influence.Description);
                                        this.TheInfluenceText.AddNewLine();
                                    }
                                    this.TheInfluenceText.ResortTexts();
                                    this.TheConditionText.Clear();
                                    this.TheConditionText.AddText("修习条件", this.TheConditionText.TitleColor);
                                    this.TheConditionText.AddNewLine();
                                    foreach (Condition condition in this.TheLinkedSkills[i].Conditions.Conditions.Values)
                                    {
                                        if (condition.CheckCondition(this.ShowingPerson))
                                        {
                                            this.TheConditionText.AddText(condition.Name, Color.White); // this.ConditionText.PositiveColor);
                                        }
                                        else
                                        {
                                            this.TheConditionText.AddText(condition.Name, Color.White);  // this.ConditionText.NegativeColor);
                                        }
                                        this.TheConditionText.AddNewLine();
                                    }
                                    this.TheConditionText.ResortTexts();
                                }
                                this.current = this.TheLinkedSkills[i];
                            }
                            flag0 = true;
                            break;
                        }
                    }
                }
                if (!flag0)
                {
                    if (this.current != null)
                    {
                        this.current = null;
                        ConditionAndInfluenceText = false;
                        this.TheInfluenceText.Clear();
                        this.TheConditionText.Clear();                        
                    }
                }
                //重要宝物介绍
                if (Switch114 == "on")
                {
                    bool flag1 = false;
                    if (!flag1)
                    {
                        if (ImportantTreasureTextIng == false)
                        {
                            for (int t = 1; t <= MaxImportantTreasureShowNumber; t++)
                            {
                                if (ImportantTreasureButton == true && StaticMethods.PointInRectangle(position, this.TheImportantTreasureDisplayPosition(t)))
                                {
                                    if (ImportantTreasureTextFollowTheMouse == "1")
                                    {
                                        this.ImportantTreasureText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.ImportantTreasureTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.ImportantTreasureTextClient.Y);
                                    }
                                    ImportantTreasureTextPicture = TheImportantTreasurePicture(t);
                                    this.ImportantTreasureText.Clear();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText1, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheImportantTreasureName(t), this.ImportantTreasureText.SubTitleColor);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText2, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheImportantTreasureWorth(t), this.ImportantTreasureText.SubTitleColor2);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText3, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheImportantTreasureDescription(t), this.ImportantTreasureText.SubTitleColor3);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.ResortTexts();
                                    ImportantTreasureTextIng = true;
                                    ImportantTreasureTexting = true;
                                }
                            }
                            for (int t = 0; t < MaxGeneralTreasureShowNumber; t++)
                            {
                                if (ImportantTreasureButton == true && StaticMethods.PointInRectangle(position, this.TheGeneralTreasureDisplayPosition(t)))
                                {
                                    if (ImportantTreasureTextFollowTheMouse == "1")
                                    {
                                        this.ImportantTreasureText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.ImportantTreasureTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.ImportantTreasureTextClient.Y);
                                    }
                                    ImportantTreasureTextPicture = TheGeneralTreasurePicture(t);
                                    this.ImportantTreasureText.Clear();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText1, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheGeneralTreasureName(t), this.ImportantTreasureText.SubTitleColor);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText2, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheGeneralTreasureWorth(t), this.ImportantTreasureText.SubTitleColor2);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.AddText(ImportantTreasureText3, this.ImportantTreasureText.TitleColor);
                                    this.ImportantTreasureText.AddText(this.TheGeneralTreasureDescription(t), this.ImportantTreasureText.SubTitleColor3);
                                    this.ImportantTreasureText.AddNewLine();
                                    this.ImportantTreasureText.ResortTexts();
                                    ImportantTreasureTextIng = true;
                                    ImportantTreasureTexting = true;
                                }
                            }
                            flag1 = true;
                        }
                    }
                    if (!flag1)
                    {
                        if (ImportantTreasureTexting == true)
                        {

                            ImportantTreasureTextIng = false;
                            ImportantTreasureTexting = false;
                            ImportantTreasureText.Clear();

                        }
                    }
                }

                //重要称号介绍
                if (Switch117 == "on")
                {
                    bool flag2 = false;
                    if (!flag2)
                    {
                        if (ImportantTitleTextIng == false)
                        {
                            for (int t = 1; t <= MaxImportantTitleShowNumber; t++)
                            {
                                if (InformationButton == true && StaticMethods.PointInRectangle(position, this.TheImportantTitleDisplayPosition(t)))
                                {
                                    if (ImportantTitleTextFollowTheMouse == "1")
                                    {
                                        this.ImportantTitleText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.ImportantTitleTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.ImportantTitleTextClient.Y);
                                    }
                                    this.ImportantTitleText.Clear();
                                    this.ImportantTitleText.AddText(ImportantTitleText1, this.ImportantTitleText.TitleColor);
                                    this.ImportantTitleText.AddText(this.TheImportantTitleKind(t), this.ImportantTitleText.SubTitleColor);
                                    this.ImportantTitleText.AddNewLine();
                                    this.ImportantTitleText.AddText(ImportantTitleText2, this.ImportantTitleText.TitleColor);
                                    this.ImportantTitleText.AddText(this.TheImportantTitleLevel(t).ToString(), this.ImportantTitleText.SubTitleColor2);
                                    this.ImportantTitleText.AddNewLine();
                                    this.ImportantTitleText.AddText(ImportantTitleText3, this.ImportantTitleText.TitleColor);
                                    this.ImportantTitleText.AddText(this.TheImportantTitleDescription(t), this.ImportantTitleText.SubTitleColor3);
                                    this.ImportantTitleText.AddNewLine();
                                    this.ImportantTitleText.ResortTexts();
                                    ImportantTitleTextIng = true;
                                    ImportantTitleTexting = true;
                                }
                            }
                            flag2 = true;
                        }
                    }
                    if (!flag2)
                    {
                        if (ImportantTitleTexting == true)
                        {
                            ImportantTitleTextIng = false;
                            ImportantTitleTexting = false;
                            ImportantTitleText.Clear();
                        }
                    }
                }
                //重要特技介绍
                if (Switch118 == "on")
                {
                    bool flag3 = false;
                    if (!flag3)
                    {
                        if (ImportantStuntTextIng == false)
                        {
                            for (int t = 1; t <= MaxImportantStuntShowNumber; t++)
                            {
                                if (InformationButton == true && StaticMethods.PointInRectangle(position, this.TheImportantStuntDisplayPosition(t)))
                                {
                                    if (ImportantStuntTextFollowTheMouse == "1")
                                    {
                                        this.ImportantStuntText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.ImportantStuntTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.ImportantStuntTextClient.Y);
                                    }
                                    this.ImportantStuntText.Clear();
                                    this.ImportantStuntText.AddText(ImportantStuntText1, this.ImportantStuntText.TitleColor);
                                    this.ImportantStuntText.AddText(this.TheImportantStuntName(t), this.ImportantStuntText.SubTitleColor);
                                    this.ImportantStuntText.AddNewLine();
                                    this.ImportantStuntText.AddText(ImportantStuntText2, this.ImportantStuntText.TitleColor);
                                    this.ImportantStuntText.AddText(this.TheImportantStuntCombativity(t).ToString(), this.ImportantStuntText.SubTitleColor2);
                                    this.ImportantStuntText.AddNewLine();
                                    this.ImportantStuntText.AddText(ImportantStuntText3, this.ImportantStuntText.TitleColor);
                                    this.ImportantStuntText.AddText(this.TheImportantStuntDescription(t), this.ImportantStuntText.SubTitleColor3);
                                    this.ImportantStuntText.AddNewLine();
                                    this.ImportantStuntText.ResortTexts();
                                    ImportantStuntTextIng = true;
                                    ImportantStuntTexting = true;
                                }
                            }
                            flag3 = true;
                        }
                    }
                    if (!flag3)
                    {
                        if (ImportantStuntTexting == true)
                        {

                            ImportantStuntTextIng = false;
                            ImportantStuntTexting = false;
                            ImportantStuntText.Clear();

                        }
                    }
                }
                
           }
            CacheManager.Scale = Vector2.One;
            //////////////////以上添加
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (Switch3 == "on")
            {
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
            }
            this.IsShowing = false;
        }

        internal void SetPerson(Person person)
        {
            if (Switch1 == "off")//部分修改增加宝物显示的原界面
            {                
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
                    this.AllSkillTexts.AddText(skill.Name, position);
                    this.LinkedSkills.Add(skill);
                }
                //this.AllSkillTexts.ResetAllTextTextures();
                this.AllSkillTexts.ResetAllAlignedPositions();
                //this.PersonSkillTexts.ResetAllTextTextures();
                this.PersonSkillTexts.ResetAllAlignedPositions();
                //this.LearnableSkillTexts.ResetAllTextTextures();
                this.LearnableSkillTexts.ResetAllAlignedPositions();

                this.ShowingPerson = person;
                this.SurNameText.Text = person.SurName;
                this.GivenNameText.Text = person.GivenName;
                this.CalledNameText.Text = person.CalledName;
                EffectiveTreasuresCount = person.EffectiveTreasureCount;
                TreasuresCount = person.TreasureCount;
                NoEffectiveTreasuresCount = TreasuresCount - EffectiveTreasuresCount;
                foreach (LabelText text in this.LabelTexts)
                {
                    text.Text.Text = StaticMethods.GetPropertyValue(person, text.PropertyName).ToString();
                }
                this.TitleText.Clear();
                foreach (Title title in person.Titles)
                {
                    this.TitleText.AddText(title.DetailedName, Color.Lime);
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

                this.StuntText.Clear();
                /*
                this.StuntText.AddText("战斗特技", Color.Yellow);
                this.StuntText.AddNewLine();
                this.StuntText.AddText(person.Stunts.Count.ToString() + "种", Color.Lime);
                this.StuntText.AddNewLine();
                 */
                foreach (Stunt stunt in person.Stunts.Stunts.Values)
                {
                    this.StuntText.AddText(stunt.Name, Color.Red);
                    this.StuntText.AddText(" 战意消耗" + stunt.Combativity.ToString(), Color.LightGreen);
                    this.StuntText.AddNewLine();
                }
                this.StuntText.ResortTexts();
                this.BiographyText.Clear();
                if (person.PersonBiography != null)
                {
                    this.BiographyText.Clear();
                    this.BiographyText.AddText("列传", this.BiographyText.TitleColor);
                    this.BiographyText.AddNewLine();
                    this.BiographyText.AddText(this.ShowingPerson.PersonBiography.Brief);
                    this.BiographyText.AddNewLine();
                    this.BiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                    this.BiographyText.AddText("：" + this.ShowingPerson.PersonBiography.Romance);
                    this.BiographyText.AddNewLine();
                    this.BiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                    this.BiographyText.AddText("：" + this.ShowingPerson.PersonBiography.History);
                    this.BiographyText.AddNewLine();
                    this.BiographyText.AddText("剧本", Color.Cyan);
                    this.BiographyText.AddText("：");
                    String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                    foreach (String s in lineBrokenText)
                    {
                        this.BiographyText.AddText(s);
                        this.BiographyText.AddNewLine();
                    }
                    this.BiographyText.ResortTexts();
                }
                this.PersonTreasuresText.Clear();
                if (ShowPersonTreasuresCount == "on")
                {                    
                    this.PersonTreasuresText.AddText(PersonTreasuresText1, this.PersonTreasuresText.TitleColor);
                    this.PersonTreasuresText.AddText(PersonTreasuresText2);
                    this.PersonTreasuresText.AddText(EffectiveTreasuresCount.ToString(), Color.Cyan);
                    this.PersonTreasuresText.AddText(PersonTreasuresText3);
                    this.PersonTreasuresText.AddText(TreasuresCount.ToString(), Color.White);
                    this.PersonTreasuresText.AddText(PersonTreasuresText4);
                    this.PersonTreasuresText.AddNewLine();
                    this.PersonTreasuresText.AddText(PersonTreasuresText5);
                    this.PersonTreasuresText.AddNewLine();                    
                }                
                if (TreasuresCount > 0)
                {
                    this.PersonTreasuresText.AddText(PersonTreasuresText6, this.PersonTreasuresText.SubTitleColor);
                    this.PersonTreasuresText.AddNewLine();
                    foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
                    {
                        this.PersonTreasuresText.AddText(PersonTreasuresText11);
                        if (ShowPersonTreasuresWorth == "on" || ShowPersonTreasuresDescription == "on")
                        {
                            this.PersonTreasuresText.AddText(PersonTreasuresText8, this.PersonTreasuresText.TitleColor);
                        }
                        this.PersonTreasuresText.AddText(t.Name);
                        if (ShowPersonTreasuresWorth == "on")
                        {
                            this.PersonTreasuresText.AddNewLine();
                            this.PersonTreasuresText.AddText(PersonTreasuresText9, this.PersonTreasuresText.TitleColor);
                            this.PersonTreasuresText.AddText(t.Worth.ToString(), this.PersonTreasuresText.SubTitleColor2);
                        }
                        if (ShowPersonTreasuresDescription == "on")
                        {
                            this.PersonTreasuresText.AddNewLine();
                            this.PersonTreasuresText.AddText(PersonTreasuresText10, this.PersonTreasuresText.TitleColor);
                            this.PersonTreasuresText.AddText(t.Description, this.PersonTreasuresText.SubTitleColor3);
                        }
                        this.PersonTreasuresText.AddText(PersonTreasuresText12);
                        this.PersonTreasuresText.AddNewLine();
                    }
                }
                if (NoEffectiveTreasuresCount > 0)
                {
                    this.PersonTreasuresText.AddText(PersonTreasuresText7, this.PersonTreasuresText.SubTitleColor);
                    this.PersonTreasuresText.AddNewLine();
                    foreach (Treasure t in this.ShowingPerson.Treasures)
                    {
                        int ID =0;
                        ID =t.ID;
                        if (IsTheNoEffectiveTreasure(ID)==true)
                        {
                            this.PersonTreasuresText.AddText(PersonTreasuresText13);
                            if (ShowPersonTreasuresWorth == "on" || ShowPersonTreasuresDescription == "on")
                            {
                                this.PersonTreasuresText.AddText(PersonTreasuresText8, this.PersonTreasuresText.TitleColor);
                            }
                            this.PersonTreasuresText.AddText(t.Name);
                            if (ShowPersonTreasuresWorth == "on")
                            {
                                this.PersonTreasuresText.AddNewLine();
                                this.PersonTreasuresText.AddText(PersonTreasuresText9, this.PersonTreasuresText.TitleColor);
                                this.PersonTreasuresText.AddText(t.Worth.ToString(), this.PersonTreasuresText.SubTitleColor2);
                            }
                            if (ShowPersonTreasuresDescription == "on")
                            {
                                this.PersonTreasuresText.AddNewLine();
                                this.PersonTreasuresText.AddText(PersonTreasuresText10, this.PersonTreasuresText.TitleColor);
                                this.PersonTreasuresText.AddText(t.Description, this.PersonTreasuresText.SubTitleColor3);
                            }
                            this.PersonTreasuresText.AddText(PersonTreasuresText14);
                            this.PersonTreasuresText.AddNewLine();
                        }
                    }
                }
                this.PersonTreasuresText.ResortTexts();
            } 
            ///////////以下新界面
            if (Switch1 == "on")
            {                
                //初始化
                InformationButton = true;
                BiographyButton = false;
                /*
                if (Switch2 == "1") { InformationButton = true; }               
                else if (Switch2 == "7") { BiographyButton = true; }               
              
                */
                BiographyBriefButton = false;
                BiographyRomanceButton = false;
                BiographyHistoryButton = false;
                BiographyInGameButton = false;
                if (Switch24 == "on")
                {
                    if (Switch25 == "1") { BiographyBriefButton = true; }
                    else if (Switch25 == "2") { BiographyRomanceButton = true; }
                    else if (Switch25 == "3") { BiographyHistoryButton = true; }
                    else if (Switch25 == "4") { BiographyInGameButton = true; }
                }
                ImportantTreasureButton = false;
                ImportantTreasureTextIng = false;
                ImportantTreasureTexting = false;
                ImportantTitleTextIng = false;
                ImportantTitleTexting = false;
                ImportantStuntTextIng = false;
                ImportantStuntTexting = false;
                RelativeButton = false;
                RelationButton = false;
                PersonRelationButton = false;
                StandingsButton = false;
                //数据采集及处理
                IDN = person.ID;
                if (Switch3 == "on" && Switch4 == "on")
                {
                    this.ThePersonSound = "Content/Sound/Open";

                    //if (File.Exists(@"GameComponents\PersonDetail\Data\Sound\Person\" + this.IDN.ToString() + ""))
                    //{ this.ThePersonSound = "Content/Sound/Person/" + this.IDN.ToString() + ""; }
                    //else
                    //{ this.ThePersonSound = "Content/Sound/Open"; }

                    Session.MainGame.mainGameScreen.PlayNormalSound(this.ThePersonSound);
                }
                SexN = person.Sex;
                //
                FactionName = "";
                if (Switch105 == "on" && person.Faction != "----")
                {                    
                    if (FactionNameKind == "1") { FactionName = person.Faction; }//势力名
                    else if (FactionNameKind == "2") { FactionName = person.BelongedFaction.Leader.SurName; }//君主姓
                    else if (FactionNameKind == "3") { FactionName = person.BelongedFaction.Leader.Name; }//君主名
                    else if (FactionNameKind == "4")//判断
                    {
                        string factionName;
                        string leaderName;
                        factionName = person.Faction;
                        leaderName = person.BelongedFaction.Leader.Name;
                        if (factionName == leaderName)
                        { FactionName = person.BelongedFaction.Leader.SurName; }
                        else
                        { FactionName = person.Faction; }
                    }
                }
                this.FactionNameText.Text = FactionName;
                //
                GenerationN = person.Generation;
                StrainN = person.Strain;
                FatherName = person.FatherName;
                MotherName = person.MotherName;
                SpouseName = person.SpouseName;
                ChildrenN = person.NumberOfChildren;
                TitleCountN = person.TitleCount;
                StuntCountN = person.StuntCount;
                GenerationText.Text = GenerationN.ToString();
                StrainText.Text = StrainN.ToString();
                //
                foreach (LabelText text in this.PersonInInformationTexts)
                {
                    text.Text.Text = StaticMethods.GetPropertyValue(person, text.PropertyName).ToString();
                }
                //
                if (Switch117 == "on")
                {
                    this.ImportantTitle1NameText.Text = person.TitleNameforGroup(ImportantTitle1Group);
                    this.ImportantTitle2NameText.Text = person.TitleNameforGroup(ImportantTitle2Group);
                    this.ImportantTitle3NameText.Text = person.TitleNameforGroup(ImportantTitle3Group);
                    this.ImportantTitle4NameText.Text = person.TitleNameforGroup(ImportantTitle4Group);
                    this.ImportantTitle5NameText.Text = person.TitleNameforGroup(ImportantTitle5Group);
                    this.ImportantTitle6NameText.Text = person.TitleNameforGroup(ImportantTitle6Group);
                    this.ImportantTitle7NameText.Text = person.TitleNameforGroup(ImportantTitle7Group);
                    this.ImportantTitle8NameText.Text = person.TitleNameforGroup(ImportantTitle8Group);
                    this.ImportantTitle9NameText.Text = person.TitleNameforGroup(ImportantTitle9Group);
                    this.ImportantTitle10NameText.Text = person.TitleNameforGroup(ImportantTitle10Group);
                    this.ImportantTitle11NameText.Text = person.TitleNameforGroup(ImportantTitle11Group);
                    this.ImportantTitle12NameText.Text = person.TitleNameforGroup(ImportantTitle12Group);
                    this.ImportantTitle13NameText.Text = person.TitleNameforGroup(ImportantTitle13Group);
                    this.ImportantTitle14NameText.Text = person.TitleNameforGroup(ImportantTitle14Group);
                    this.ImportantTitle15NameText.Text = person.TitleNameforGroup(ImportantTitle15Group);
                    this.ImportantTitle16NameText.Text = person.TitleNameforGroup(ImportantTitle16Group);
                    this.ImportantTitle17NameText.Text = person.TitleNameforGroup(ImportantTitle17Group);
                    this.ImportantTitle18NameText.Text = person.TitleNameforGroup(ImportantTitle18Group);
                    this.ImportantTitle19NameText.Text = person.TitleNameforGroup(ImportantTitle19Group);
                    this.ImportantTitle20NameText.Text = person.TitleNameforGroup(ImportantTitle20Group);
                }
                if (Switch118 == "on")
                {
                    this.ImportantStunt1NameText.Text = person.StuntNameforGroup(ImportantStunt1Group);
                    this.ImportantStunt2NameText.Text = person.StuntNameforGroup(ImportantStunt2Group);
                    this.ImportantStunt3NameText.Text = person.StuntNameforGroup(ImportantStunt3Group);
                    this.ImportantStunt4NameText.Text = person.StuntNameforGroup(ImportantStunt4Group);
                    this.ImportantStunt5NameText.Text = person.StuntNameforGroup(ImportantStunt5Group);
                    this.ImportantStunt6NameText.Text = person.StuntNameforGroup(ImportantStunt6Group);
                    this.ImportantStunt7NameText.Text = person.StuntNameforGroup(ImportantStunt7Group);
                    this.ImportantStunt8NameText.Text = person.StuntNameforGroup(ImportantStunt8Group);
                    this.ImportantStunt9NameText.Text = person.StuntNameforGroup(ImportantStunt9Group);
                    this.ImportantStunt10NameText.Text = person.StuntNameforGroup(ImportantStunt10Group);
                    this.ImportantStunt11NameText.Text = person.StuntNameforGroup(ImportantStunt11Group);
                    this.ImportantStunt12NameText.Text = person.StuntNameforGroup(ImportantStunt12Group);
                    this.ImportantStunt13NameText.Text = person.StuntNameforGroup(ImportantStunt13Group);
                    this.ImportantStunt14NameText.Text = person.StuntNameforGroup(ImportantStunt14Group);
                    this.ImportantStunt15NameText.Text = person.StuntNameforGroup(ImportantStunt15Group);
                    this.ImportantStunt16NameText.Text = person.StuntNameforGroup(ImportantStunt16Group);
                    this.ImportantStunt17NameText.Text = person.StuntNameforGroup(ImportantStunt17Group);
                    this.ImportantStunt18NameText.Text = person.StuntNameforGroup(ImportantStunt18Group);
                    this.ImportantStunt19NameText.Text = person.StuntNameforGroup(ImportantStunt19Group);
                    this.ImportantStunt20NameText.Text = person.StuntNameforGroup(ImportantStunt20Group);
                }
                //
                HasFather = false;
                HasMother = false;
                HasSpouse = false;
                if (FatherName != "－－－－") { HasFather = true; }
                if (MotherName != "－－－－") { HasMother = true; }
                if (SpouseName != "－－－－") { HasSpouse = true; }


                //
                this.ThePersonSkillTexts.SimpleClear();
                this.TheLearnableSkillTexts.SimpleClear();
                foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
                {
                    Rectangle position = new Rectangle(this.TheSkillDisplayOffset.X + (skill.DisplayCol * this.TheSkillBlockSize.X), this.TheSkillDisplayOffset.Y + (skill.DisplayRow * this.TheSkillBlockSize.Y), this.TheSkillBlockSize.X, this.TheSkillBlockSize.Y);
                    if (person.Skills.GetSkill(skill.ID) != null)
                    {
                        this.ThePersonSkillTexts.AddText(skill.Name, position);
                    }
                    else if (skill.CanLearn(person))
                    {
                        this.TheLearnableSkillTexts.AddText(skill.Name, position);
                    }
                    this.TheAllSkillTexts.AddText(skill.Name, position);
                    this.TheLinkedSkills.Add(skill);
                }
                //this.TheAllSkillTexts.ResetAllTextTextures();
                this.TheAllSkillTexts.ResetAllAlignedPositions();
                //this.ThePersonSkillTexts.ResetAllTextTextures();
                this.ThePersonSkillTexts.ResetAllAlignedPositions();
                //this.TheLearnableSkillTexts.ResetAllTextTextures();
                this.TheLearnableSkillTexts.ResetAllAlignedPositions();
                this.AllSkillTitleCount = this.TheAllSkillTexts.Count;
                this.ShowingPerson = person;
                this.SurNameText.Text = person.SurName;
                this.GivenNameText.Text = person.GivenName;
                this.CalledNameText.Text = person.CalledName;
                this.TheTitleText.Clear();
                foreach (Title title in person.Titles)
                {
                    this.TheTitleText.AddText(title.DetailedName, Color.Lime);
                    this.TheTitleText.AddNewLine();
                }
                this.TheTitleText.ResortTexts();
                this.TheStuntText.Clear();
                foreach (Stunt stunt in person.Stunts.Stunts.Values)
                {
                    this.TheStuntText.AddText(stunt.Name, Color.Red);
                    this.TheStuntText.AddText(" 战意消耗" + stunt.Combativity.ToString(), Color.LightGreen);
                    this.TheStuntText.AddNewLine();
                }
                this.TheStuntText.ResortTexts();

                //                
                if (Switch24 == "on")
                {
                    if (BiographyBriefButton == true)
                    {
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText1, this.PersonBiographyText.TitleColor);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.Brief, this.PersonBiographyText.TitleColor);
                        this.PersonBiographyText.ResortTexts();
                    }
                    else if (BiographyRomanceButton == true)
                    {
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText2, this.PersonBiographyText.SubTitleColor);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.Romance, this.PersonBiographyText.SubTitleColor);
                        this.PersonBiographyText.ResortTexts();
                    }
                    else if (BiographyHistoryButton == true)
                    {
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText3, this.PersonBiographyText.SubTitleColor2);
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.History, this.PersonBiographyText.SubTitleColor2);
                        this.PersonBiographyText.ResortTexts();
                    }
                    else if (BiographyInGameButton == true)
                    {
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText(this.PersonBiographyText4, this.PersonBiographyText.SubTitleColor3);
                        String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                        foreach (String s in lineBrokenText)
                        {
                            this.PersonBiographyText.AddText(s, this.PersonBiographyText.SubTitleColor3);
                            this.PersonBiographyText.AddNewLine();
                        }
                        this.PersonBiographyText.ResortTexts();
                    }
                }
                else
                {
                    if (person.PersonBiography != null)
                    {
                        this.PersonBiographyText.Clear();
                        this.PersonBiographyText.AddText("列传", this.BiographyText.TitleColor);
                        this.PersonBiographyText.AddNewLine();
                        this.PersonBiographyText.AddText(this.ShowingPerson.PersonBiography.Brief, this.PersonBiographyText.TitleColor);
                        this.PersonBiographyText.AddNewLine();
                        this.PersonBiographyText.AddText("演义", this.BiographyText.SubTitleColor);
                        this.PersonBiographyText.AddText("：" + this.ShowingPerson.PersonBiography.Romance, this.PersonBiographyText.SubTitleColor);
                        this.PersonBiographyText.AddNewLine();
                        this.PersonBiographyText.AddText("历史", this.BiographyText.SubTitleColor2);
                        this.PersonBiographyText.AddText("：" + this.ShowingPerson.PersonBiography.History, this.PersonBiographyText.SubTitleColor2);
                        this.PersonBiographyText.AddNewLine();
                        this.PersonBiographyText.AddText("剧本", Color.Cyan);
                        this.PersonBiographyText.AddText("：");
                        String[] lineBrokenText = ShowingPerson.PersonBiography.InGame.Split('\n');
                        foreach (String s in lineBrokenText)
                        {
                            this.PersonBiographyText.AddText(s, this.PersonBiographyText.SubTitleColor3);
                            this.PersonBiographyText.AddNewLine();
                        }
                        this.PersonBiographyText.ResortTexts();
                    }
                }
                //
                if (Switch121 == "on")
                {                    
                    FatherNameText.Text = FatherName;
                    MotherNameText.Text = MotherName;
                    SpouseNameText.Text = SpouseName;
                    ChildrenNumberText.Text = ChildrenN.ToString();
                    if (ChildrenN > 0)
                    {
                        this.ChildrenText.Clear();
                        this.ChildrenText.AddText(ChildrenText4);
                        this.ChildrenText.AddNewLine();
                        foreach (Person p in person.ChildrenList)
                        {
                            if (p.Sex == true)
                            {
                                this.ChildrenText.AddText(ChildrenText1);
                                this.ChildrenText.AddText(p.Name, this.ChildrenText.TitleColor);
                                this.ChildrenText.AddText(ChildrenText2);
                                this.ChildrenText.AddText(p.DisplayedAge, this.ChildrenText.SubTitleColor2);
                                this.ChildrenText.AddText("岁", this.ChildrenText.SubTitleColor2);
                                this.ChildrenText.AddText(ChildrenText2);
                                this.ChildrenText.AddText(p.Location, this.ChildrenText.SubTitleColor3);
                                this.ChildrenText.AddNewLine();
                            }
                            else
                            {
                                this.ChildrenText.AddText(ChildrenText1);
                                this.ChildrenText.AddText(p.Name, this.ChildrenText.SubTitleColor);
                                this.ChildrenText.AddText(ChildrenText2);
                                this.ChildrenText.AddText(p.DisplayedAge, this.ChildrenText.SubTitleColor2);
                                this.ChildrenText.AddText("岁", this.ChildrenText.SubTitleColor2);
                                this.ChildrenText.AddText(ChildrenText3);
                                this.ChildrenText.AddText(p.Location, this.ChildrenText.SubTitleColor3);
                                this.ChildrenText.AddNewLine();
                            }
                        }
                        this.ChildrenText.AddText(ChildrenText5);
                        this.ChildrenText.ResortTexts();
                    }
                }
                if (Switch122 == "on")
                {
                    this.BrothersText.Clear();
                    this.BrothersText.AddText(BrothersText4, this.BrothersText.SubTitleColor3);
                    if (VerticalForBrothersText == "on")
                    {
                        this.BrothersText.AddNewLine();
                    }
                    foreach (Person p in person.Brothers)
                    {
                        this.BrothersText.AddText(BrothersText1, this.BrothersText.SubTitleColor);
                        this.BrothersText.AddText(p.Name, this.BrothersText.TitleColor);
                        this.BrothersText.AddText(BrothersText2, this.BrothersText.SubTitleColor);
                        this.BrothersText.AddText(BrothersText3, this.BrothersText.SubTitleColor2);
                        if (VerticalForBrothersText == "on")
                        {
                            this.BrothersText.AddNewLine();
                        }
                    }
                    this.BrothersText.AddText(BrothersText5, this.BrothersText.SubTitleColor3);
                    this.BrothersText.ResortTexts();

                    this.ClosePersonsText.Clear();
                    this.ClosePersonsText.AddText(ClosePersonsText4, this.ClosePersonsText.SubTitleColor3);
                    if (VerticalForClosePersonsText == "on")
                    {
                        this.ClosePersonsText.AddNewLine();
                    }
                    foreach (Person p in person.ClosePersons)
                    {
                        this.ClosePersonsText.AddText(ClosePersonsText1, this.ClosePersonsText.SubTitleColor);
                        this.ClosePersonsText.AddText(p.Name, this.ClosePersonsText.TitleColor);
                        this.ClosePersonsText.AddText(ClosePersonsText2, this.ClosePersonsText.SubTitleColor);
                        this.ClosePersonsText.AddText(ClosePersonsText3, this.ClosePersonsText.SubTitleColor3);
                        if (VerticalForClosePersonsText == "on")
                        {
                            this.ClosePersonsText.AddNewLine();
                        }
                    }
                    this.ClosePersonsText.AddText(ClosePersonsText5, this.ClosePersonsText.SubTitleColor3);
                    this.ClosePersonsText.ResortTexts();

                    this.HatedPersonsText.Clear();
                    this.HatedPersonsText.AddText(HatedPersonsText4, this.ClosePersonsText.SubTitleColor3);
                    if (VerticalForHatedPersonsText == "on")
                    {
                        this.HatedPersonsText.AddNewLine();
                    }
                    foreach (Person p in person.HatedPersons)
                    {
                        this.HatedPersonsText.AddText(HatedPersonsText1, this.HatedPersonsText.SubTitleColor);
                        this.HatedPersonsText.AddText(p.Name, this.HatedPersonsText.TitleColor);
                        this.HatedPersonsText.AddText(HatedPersonsText2, this.HatedPersonsText.SubTitleColor);
                        this.HatedPersonsText.AddText(HatedPersonsText3, this.HatedPersonsText.SubTitleColor3);
                        if (VerticalForHatedPersonsText == "on")
                        {
                            this.HatedPersonsText.AddNewLine();
                        }
                    }
                    this.HatedPersonsText.AddText(HatedPersonsText5, this.ClosePersonsText.SubTitleColor3);
                    this.HatedPersonsText.ResortTexts();
                }
                if (Switch123 == "on")
                {
                    this.PersonRelationText.Clear();
                    this.PersonRelationText.AddText(PersonRelationText1, this.PersonRelationText.TitleColor);
                    this.PersonRelationText.AddText(ShowingPerson.PersonSpecialRelationString2, this.PersonRelationText.SubTitleColor);
                    this.PersonRelationText.AddText(PersonRelationText2, this.PersonRelationText.TitleColor);
                    this.PersonRelationText.AddNewLine();
                    this.PersonRelationText.AddText(PersonRelationText3, this.PersonRelationText.SubTitleColor2);
                    this.PersonRelationText.AddText(ShowingPerson.PersonSpecialRelationString1, this.PersonRelationText.SubTitleColor3);
                    this.PersonRelationText.AddText(PersonRelationText4, this.PersonRelationText.SubTitleColor2);
                    this.PersonRelationText.AddNewLine();
                    this.PersonRelationText.ResortTexts();
                }
                if (Switch124 == "on")
                {
                    foreach (LabelText text in this.StandingsTexts)
                    {
                        text.Text.Text = StaticMethods.GetPropertyValue(person, text.PropertyName).ToString();
                    }
                }
            }
            //////////以上新页面
        }

        internal void SetPosition(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, Convert.ToInt16(this.BackgroundSize.X * Scale.X), Convert.ToInt16(this.BackgroundSize.Y * Scale.Y));
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
            this.DisplayOffset = new Point(rect.X + 13, rect.Y + 15);  // new Point(rect.X, rect.Y + 15);
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
            this.PersonTreasuresText.DisplayOffset = new Point(this.DisplayOffset.X + this.PersonTreasuresTextClient.X, this.DisplayOffset.Y + this.PersonTreasuresTextClient.Y);
            ///////////////////以下新界面相关
            if (Switch1 == "on")
            {                
                foreach (LabelText text in this.PersonInInformationTexts)
                {
                    text.Label.DisplayOffset = this.DisplayOffset;
                    text.Text.DisplayOffset = this.DisplayOffset;
                }                
                //
                this.TheTitleText.DisplayOffset = new Point(this.DisplayOffset.X + this.TheTitleClient.X, this.DisplayOffset.Y + this.TheTitleClient.Y);
                this.TheAllSkillTexts.DisplayOffset = this.DisplayOffset;
                this.ThePersonSkillTexts.DisplayOffset = this.DisplayOffset;
                this.TheLearnableSkillTexts.DisplayOffset = this.DisplayOffset;
                this.TheStuntText.DisplayOffset = new Point(this.DisplayOffset.X + this.TheStuntClient.X, this.DisplayOffset.Y + this.TheStuntClient.Y);
                this.TheInfluenceText.DisplayOffset = new Point(this.DisplayOffset.X + this.TheInfluenceClient.X, this.DisplayOffset.Y + this.TheInfluenceClient.Y);
                this.TheConditionText.DisplayOffset = new Point(this.DisplayOffset.X + this.TheConditionClient.X, this.DisplayOffset.Y + this.TheConditionClient.Y);
                this.PersonBiographyText.DisplayOffset = new Point(this.DisplayOffset.X + this.PersonBiographyTextClient.X, this.DisplayOffset.Y + this.PersonBiographyTextClient.Y);
                //
                this.FactionNameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTreasureText.DisplayOffset = new Point(this.DisplayOffset.X + this.ImportantTreasureTextClient.X, this.DisplayOffset.Y + this.ImportantTreasureTextClient.Y);
                //
                this.ImportantTitleText.DisplayOffset = new Point(this.DisplayOffset.X + this.ImportantTitleTextClient.X, this.DisplayOffset.Y + this.ImportantTitleTextClient.Y);
                this.ImportantTitle1NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle2NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle3NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle4NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle5NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle6NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle7NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle8NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle9NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle10NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle11NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle12NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle13NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle14NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle15NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle16NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle17NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle18NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle19NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantTitle20NameText.DisplayOffset = this.DisplayOffset;
                //
                this.ImportantStuntText.DisplayOffset = new Point(this.DisplayOffset.X + this.ImportantStuntTextClient.X, this.DisplayOffset.Y + this.ImportantStuntTextClient.Y);
                this.ImportantStunt1NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt2NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt3NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt4NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt5NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt6NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt7NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt8NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt9NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt10NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt11NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt12NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt13NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt14NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt15NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt16NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt17NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt18NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt19NameText.DisplayOffset = this.DisplayOffset;
                this.ImportantStunt20NameText.DisplayOffset = this.DisplayOffset;
                //
                this.GenerationText.DisplayOffset = this.DisplayOffset;
                this.StrainText.DisplayOffset = this.DisplayOffset;
                this.FatherNameText.DisplayOffset = this.DisplayOffset;
                this.MotherNameText.DisplayOffset = this.DisplayOffset;
                this.SpouseNameText.DisplayOffset = this.DisplayOffset;
                this.ChildrenNumberText.DisplayOffset = this.DisplayOffset;
                this.ChildrenText.DisplayOffset = new Point(this.DisplayOffset.X + this.ChildrenTextClient.X, this.DisplayOffset.Y + this.ChildrenTextClient.Y);
                //
                this.BrothersText.DisplayOffset = new Point(this.DisplayOffset.X + this.BrothersTextClient.X, this.DisplayOffset.Y + this.BrothersTextClient.Y);
                this.ClosePersonsText.DisplayOffset = new Point(this.DisplayOffset.X + this.ClosePersonsTextClient.X, this.DisplayOffset.Y + this.ClosePersonsTextClient.Y);
                this.HatedPersonsText.DisplayOffset = new Point(this.DisplayOffset.X + this.HatedPersonsTextClient.X, this.DisplayOffset.Y + this.HatedPersonsTextClient.Y);
                this.PersonRelationText.DisplayOffset = new Point(this.DisplayOffset.X + this.PersonRelationTextClient.X, this.DisplayOffset.Y + this.PersonRelationTextClient.Y);
                //
                foreach (LabelText text in this.StandingsTexts)
                {
                    text.Label.DisplayOffset = this.DisplayOffset;
                    text.Text.DisplayOffset = this.DisplayOffset;
                }
                //
            }
            //////////以上新界面相关
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.BackgroundClient.X + this.DisplayOffset.X, this.BackgroundClient.Y + this.DisplayOffset.Y, this.BackgroundClient.Width, this.BackgroundClient.Height);
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
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.PersonDetail));
                    Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    //Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                }
                else
                {
                    if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.SubDialog)
                    {
                        throw new Exception("The UndoneWork is not a SubDialog.");
                    }
                    Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    //Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.current = null;
                    this.InfluenceText.Clear();
                    this.ConditionText.Clear();
                    if (Switch1 == "on")
                    {
                        this.TheInfluenceText.Clear();
                        this.TheConditionText.Clear();
                    }
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
        //////////以下新增内容
        private Rectangle NullDisplayPosition
        {
            get
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }
        //
        private Rectangle BiographyButtonDisplayPosition
        {
            get
            {
                if (BiographyButton == false && Switch21 == "on")
                {
                    return new Rectangle(this.BiographyButtonClient.X + this.DisplayOffset.X, this.BiographyButtonClient.Y + this.DisplayOffset.Y, this.BiographyButtonClient.Width, this.BiographyButtonClient.Height);
                }
                return new Rectangle(this.BiographyButtonClient.X + this.DisplayOffset.X, this.BiographyButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyButtonPressedDisplayPosition
        {
            get
            {
                if (BiographyButton == true && Switch21 == "on")
                {
                    return new Rectangle(this.BiographyButtonClient.X + this.DisplayOffset.X, this.BiographyButtonClient.Y + this.DisplayOffset.Y, this.BiographyButtonClient.Width, this.BiographyButtonClient.Height);
                }
                return new Rectangle(this.BiographyButtonClient.X + this.DisplayOffset.X, this.BiographyButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
        private Rectangle InformationBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.InformationBackgroundClient.X + this.DisplayOffset.X, this.InformationBackgroundClient.Y + this.DisplayOffset.Y, this.InformationBackgroundClient.Width, this.InformationBackgroundClient.Height);
            }
        }
        private Rectangle DetailBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DetailBackgroundClient.X + this.DisplayOffset.X, this.DetailBackgroundClient.Y + this.DisplayOffset.Y, this.DetailBackgroundClient.Width, this.DetailBackgroundClient.Height);
            }
        }
        private Rectangle TreasureBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.TreasureBackgroundClient.X + this.DisplayOffset.X, this.TreasureBackgroundClient.Y + this.DisplayOffset.Y, this.TreasureBackgroundClient.Width, this.TreasureBackgroundClient.Height);
            }
        }
        private Rectangle SkillBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.SkillBackgroundClient.X + this.DisplayOffset.X, this.SkillBackgroundClient.Y + this.DisplayOffset.Y, this.SkillBackgroundClient.Width, this.SkillBackgroundClient.Height);
            }
        }
        private Rectangle TitleBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.TitleBackgroundClient.X + this.DisplayOffset.X, this.TitleBackgroundClient.Y + this.DisplayOffset.Y, this.TitleBackgroundClient.Width, this.TitleBackgroundClient.Height);
            }
        }
        private Rectangle StuntBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.StuntBackgroundClient.X + this.DisplayOffset.X, this.StuntBackgroundClient.Y + this.DisplayOffset.Y, this.StuntBackgroundClient.Width, this.StuntBackgroundClient.Height);
            }
        }
        private Rectangle BiographyBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.BiographyBackgroundClient.X + this.DisplayOffset.X, this.BiographyBackgroundClient.Y + this.DisplayOffset.Y, this.BiographyBackgroundClient.Width, this.BiographyBackgroundClient.Height);
            }
        }
        private Rectangle SpecialtyShowBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.SpecialtyShowBackgroundClient.X + this.DisplayOffset.X, this.SpecialtyShowBackgroundClient.Y + this.DisplayOffset.Y, this.SpecialtyShowBackgroundClient.Width, this.SpecialtyShowBackgroundClient.Height);
            }
        }
        //
        private Rectangle PortraitInInformationDisplayPosition
        {
            get
            {
                return new Rectangle(this.PortraitInInformationClient.X + this.DisplayOffset.X, this.PortraitInInformationClient.Y + this.DisplayOffset.Y, this.PortraitInInformationClient.Width, this.PortraitInInformationClient.Height);
            }
        }
       
        //
        private Rectangle NewTitleDisplayPosition
        {
            get
            {
                return new Rectangle(this.TheTitleText.DisplayOffset.X, this.TheTitleText.DisplayOffset.Y, this.TheTitleText.ClientWidth, this.TheTitleText.ClientHeight);
            }
        }
        private Rectangle NewStuntDisplayPosition
        {
            get
            {
                return new Rectangle(this.TheStuntText.DisplayOffset.X, this.TheStuntText.DisplayOffset.Y, this.TheStuntText.ClientWidth, this.TheStuntText.ClientHeight);
            }
        }
        //  
        private Rectangle FactionNameBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.FactionNameBackgroundClient.X + this.DisplayOffset.X, this.FactionNameBackgroundClient.Y + this.DisplayOffset.Y, this.FactionNameBackgroundClient.Width, this.FactionNameBackgroundClient.Height);
            }
        }
        private Rectangle FactionColourDisplayPosition
        {
            get
            {
                return new Rectangle(this.FactionColourClient.X + this.DisplayOffset.X, this.FactionColourClient.Y + this.DisplayOffset.Y, this.FactionColourClient.Width, this.FactionColourClient.Height);
            }
        }
        //
        private Rectangle ImportantTreasureButtonClientDisplayPosition
        {
            get
            {
                if ((Switch114 == "on" || Switch115 == "on") && ImportantTreasureButton == false)
                {
                    return new Rectangle(this.ImportantTreasureButtonClient.X + this.DisplayOffset.X, this.ImportantTreasureButtonClient.Y + this.DisplayOffset.Y, this.ImportantTreasureButtonClient.Width, this.ImportantTreasureButtonClient.Height);
                }
                return new Rectangle(this.ImportantTreasureButtonClient.X + this.DisplayOffset.X, this.ImportantTreasureButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasurePressedClientDisplayPosition
        {
            get
            {
                if ((Switch114 == "on" || Switch115 == "on") && ImportantTreasureButton == true)
                {
                    return new Rectangle(this.ImportantTreasureButtonClient.X + this.DisplayOffset.X, this.ImportantTreasureButtonClient.Y + this.DisplayOffset.Y, this.ImportantTreasureButtonClient.Width, this.ImportantTreasureButtonClient.Height);
                }
                return new Rectangle(this.ImportantTreasureButtonClient.X + this.DisplayOffset.X, this.ImportantTreasureButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasureBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.ImportantTreasureBackgroundClient.X + this.DisplayOffset.X, this.ImportantTreasureBackgroundClient.Y + this.DisplayOffset.Y, this.ImportantTreasureBackgroundClient.Width, this.ImportantTreasureBackgroundClient.Height);
            }
        }
        private Rectangle GeneralTreasureBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.GeneralTreasureBackgroundClient.X + this.DisplayOffset.X, this.GeneralTreasureBackgroundClient.Y + this.DisplayOffset.Y, this.GeneralTreasureBackgroundClient.Width, this.GeneralTreasureBackgroundClient.Height);
            }
        }
        private Rectangle ImportantTreasureTextBackgroundDisplayPosition
        {
            get
            {
                if (InformationButton == true && ImportantTreasureButton == true && ImportantTreasureTextIng == true)
                {
                    if (ImportantTreasureTextFollowTheMouse == "1")
                    {
                        return new Rectangle(this.ImportantTreasureTextBackgroundClient.X + Session.MainGame.mainGameScreen.MousePosition.X, this.ImportantTreasureTextBackgroundClient.Y + Session.MainGame.mainGameScreen.MousePosition.Y, this.ImportantTreasureTextBackgroundClient.Width, this.ImportantTreasureTextBackgroundClient.Height);
                    }
                    return new Rectangle(this.ImportantTreasureTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantTreasureTextBackgroundClient.Y + this.DisplayOffset.Y, this.ImportantTreasureTextBackgroundClient.Width, this.ImportantTreasureTextBackgroundClient.Height);
                }
                return new Rectangle(this.ImportantTreasureTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantTreasureTextBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
         private Rectangle ImportantTreasureTextDisplayPosition
        {
            get
            {

                return new Rectangle(this.ImportantTreasureText.DisplayOffset.X, this.ImportantTreasureText.DisplayOffset.Y, this.ImportantTreasureText.ClientWidth, this.ImportantTreasureText.ClientHeight);
            }
        }
        private Rectangle ImportantTreasureTextPictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && ImportantTreasureButton == true && ImportantTreasureTextIng == true)
                {
                    if (ImportantTreasureTextFollowTheMouse == "1")
                    {
                        return new Rectangle(this.ImportantTreasureTextPictureClient.X + Session.MainGame.mainGameScreen.MousePosition.X, this.ImportantTreasureTextPictureClient.Y + Session.MainGame.mainGameScreen.MousePosition.Y, this.ImportantTreasureTextPictureClient.Width, this.ImportantTreasureTextPictureClient.Height);
                    }
                    return new Rectangle(this.ImportantTreasureTextPictureClient.X + this.DisplayOffset.X, this.ImportantTreasureTextPictureClient.Y + this.DisplayOffset.Y, this.ImportantTreasureTextPictureClient.Width, this.ImportantTreasureTextPictureClient.Height);
                }
                return new Rectangle(this.ImportantTreasureTextPictureClient.X + this.DisplayOffset.X, this.ImportantTreasureTextPictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ThePersonPictureInTreasureDisplayPosition
        {
            get
            {
                return new Rectangle(this.ThePersonPictureInTreasureClient.X + this.DisplayOffset.X, this.ThePersonPictureInTreasureClient.Y + this.DisplayOffset.Y, this.ThePersonPictureInTreasureClient.Width, this.ThePersonPictureInTreasureClient.Height);
            }
        }
        private Rectangle TheImportantTreasureDisplayPosition(int t)
        {
            Rectangle D = this.NullDisplayPosition;
            if (t == 1) { D = this.ImportantTreasure1DisplayPosition; }
            else if (t == 2) { D = this.ImportantTreasure2DisplayPosition; }
            else if (t == 3) { D = this.ImportantTreasure3DisplayPosition; }
            else if (t == 4) { D = this.ImportantTreasure4DisplayPosition; }
            else if (t == 5) { D = this.ImportantTreasure5DisplayPosition; }
            else if (t == 6) { D = this.ImportantTreasure6DisplayPosition; }
            else if (t == 7) { D = this.ImportantTreasure7DisplayPosition; }
            else if (t == 8) { D = this.ImportantTreasure8DisplayPosition; }
            else if (t == 9) { D = this.ImportantTreasure9DisplayPosition; }
            else if (t == 10) { D = this.ImportantTreasure10DisplayPosition; }
            else if (t == 11) { D = this.ImportantTreasure11DisplayPosition; }
            else if (t == 12) { D = this.ImportantTreasure12DisplayPosition; }
            else if (t == 13) { D = this.ImportantTreasure13DisplayPosition; }
            else if (t == 14) { D = this.ImportantTreasure14DisplayPosition; }
            else if (t == 15) { D = this.ImportantTreasure15DisplayPosition; }
            else if (t == 16) { D = this.ImportantTreasure16DisplayPosition; }
            else if (t == 17) { D = this.ImportantTreasure17DisplayPosition; }
            else if (t == 18) { D = this.ImportantTreasure18DisplayPosition; }
            else if (t == 19) { D = this.ImportantTreasure19DisplayPosition; }
            else if (t == 20) { D = this.ImportantTreasure20DisplayPosition; }
            return D;
        }
        private PlatformTexture TheImportantTreasurePicture(int t)
        {
            PlatformTexture P = this.PictureNull;
            if (t == 1) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure1Group); }
            else if (t == 2) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure2Group); }
            else if (t == 3) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure3Group); }
            else if (t == 4) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure4Group); }
            else if (t == 5) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure5Group); }
            else if (t == 6) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure6Group); }
            else if (t == 7) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure7Group); }
            else if (t == 8) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure8Group); }
            else if (t == 9) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure9Group); }
            else if (t == 10) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure10Group); }
            else if (t == 11) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure11Group); }
            else if (t == 12) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure12Group); }
            else if (t == 13) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure13Group); }
            else if (t == 14) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure14Group); }
            else if (t == 15) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure15Group); }
            else if (t == 16) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure16Group); }
            else if (t == 17) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure17Group); }
            else if (t == 18) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure18Group); }
            else if (t == 19) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure19Group); }
            else if (t == 20) { P = this.ShowingPerson.TreasurePictureforGroup(ImportantTreasure20Group); }
            return P;
        }
        private string TheImportantTreasureName(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure1Group); }
            else if (t == 2) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure2Group); }
            else if (t == 3) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure3Group); }
            else if (t == 4) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure4Group); }
            else if (t == 5) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure5Group); }
            else if (t == 6) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure6Group); }
            else if (t == 7) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure7Group); }
            else if (t == 8) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure8Group); }
            else if (t == 9) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure9Group); }
            else if (t == 10) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure10Group); }
            else if (t == 11) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure11Group); }
            else if (t == 12) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure12Group); }
            else if (t == 13) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure13Group); }
            else if (t == 14) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure14Group); }
            else if (t == 15) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure15Group); }
            else if (t == 16) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure16Group); }
            else if (t == 17) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure17Group); }
            else if (t == 18) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure18Group); }
            else if (t == 19) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure19Group); }
            else if (t == 20) { s = this.ShowingPerson.TreasureNameforGroup(ImportantTreasure20Group); }
            return s;
        }
        private string TheImportantTreasureWorth(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure1Group).ToString(); }
            else if (t == 2) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure2Group).ToString(); }
            else if (t == 3) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure3Group).ToString(); }
            else if (t == 4) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure4Group).ToString(); }
            else if (t == 5) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure5Group).ToString(); }
            else if (t == 6) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure6Group).ToString(); }
            else if (t == 7) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure7Group).ToString(); }
            else if (t == 8) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure8Group).ToString(); }
            else if (t == 9) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure9Group).ToString(); }
            else if (t == 10) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure10Group).ToString(); }
            else if (t == 11) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure11Group).ToString(); }
            else if (t == 12) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure12Group).ToString(); }
            else if (t == 13) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure13Group).ToString(); }
            else if (t == 14) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure14Group).ToString(); }
            else if (t == 15) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure15Group).ToString(); }
            else if (t == 16) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure16Group).ToString(); }
            else if (t == 17) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure17Group).ToString(); }
            else if (t == 18) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure18Group).ToString(); }
            else if (t == 19) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure19Group).ToString(); }
            else if (t == 20) { s = this.ShowingPerson.TreasureWorthforGroup(ImportantTreasure20Group).ToString(); }
            return s;
        }
        private string TheImportantTreasureDescription(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure1Group); }
            else if (t == 2) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure2Group); }
            else if (t == 3) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure3Group); }
            else if (t == 4) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure4Group); }
            else if (t == 5) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure5Group); }
            else if (t == 6) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure6Group); }
            else if (t == 7) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure7Group); }
            else if (t == 8) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure8Group); }
            else if (t == 9) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure9Group); }
            else if (t == 10) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure10Group); }
            else if (t == 11) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure11Group); }
            else if (t == 12) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure12Group); }
            else if (t == 13) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure13Group); }
            else if (t == 14) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure14Group); }
            else if (t == 15) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure15Group); }
            else if (t == 16) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure16Group); }
            else if (t == 17) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure17Group); }
            else if (t == 18) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure18Group); }
            else if (t == 19) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure19Group); }
            else if (t == 20) { s = this.ShowingPerson.TreasureDescriptionforGroup(ImportantTreasure20Group); }
            return s;
        }
        private bool HasTheImportantTreasure(int t)
        {
            bool H = false;
            if (t == 1) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure1Group); }
            else if (t == 2) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure2Group); }
            else if (t == 3) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure3Group); }
            else if (t == 4) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure4Group); }
            else if (t == 5) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure5Group); }
            else if (t == 6) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure6Group); }
            else if (t == 7) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure7Group); }
            else if (t == 8) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure8Group); }
            else if (t == 9) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure9Group); }
            else if (t == 10) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure10Group); }
            else if (t == 11) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure11Group); }
            else if (t == 12) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure12Group); }
            else if (t == 13) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure13Group); }
            else if (t == 14) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure14Group); }
            else if (t == 15) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure15Group); }
            else if (t == 16) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure16Group); }
            else if (t == 17) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure17Group); }
            else if (t == 18) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure18Group); }
            else if (t == 19) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure19Group); }
            else if (t == 20) { H = this.ShowingPerson.HasTreasureforGroup(ImportantTreasure20Group); }
            return H;
        }
        private Rectangle ImportantTreasure1DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(1) == true && MaxImportantTreasureShowNumber > 0)
                {
                    return new Rectangle(this.ImportantTreasure1Client.X + this.DisplayOffset.X, this.ImportantTreasure1Client.Y + this.DisplayOffset.Y, this.ImportantTreasure1Client.Width, this.ImportantTreasure1Client.Height);
                }
                return new Rectangle(this.ImportantTreasure1Client.X + this.DisplayOffset.X, this.ImportantTreasure1Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure2DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(2) == true && MaxImportantTreasureShowNumber > 1)
                {
                    return new Rectangle(this.ImportantTreasure2Client.X + this.DisplayOffset.X, this.ImportantTreasure2Client.Y + this.DisplayOffset.Y, this.ImportantTreasure2Client.Width, this.ImportantTreasure2Client.Height);
                }
                return new Rectangle(this.ImportantTreasure2Client.X + this.DisplayOffset.X, this.ImportantTreasure2Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure3DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(3) == true && MaxImportantTreasureShowNumber > 2)
                {
                    return new Rectangle(this.ImportantTreasure3Client.X + this.DisplayOffset.X, this.ImportantTreasure3Client.Y + this.DisplayOffset.Y, this.ImportantTreasure3Client.Width, this.ImportantTreasure3Client.Height);
                }
                return new Rectangle(this.ImportantTreasure3Client.X + this.DisplayOffset.X, this.ImportantTreasure3Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure4DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(4) == true && MaxImportantTreasureShowNumber > 3)
                {
                    return new Rectangle(this.ImportantTreasure4Client.X + this.DisplayOffset.X, this.ImportantTreasure4Client.Y + this.DisplayOffset.Y, this.ImportantTreasure4Client.Width, this.ImportantTreasure4Client.Height);
                }
                return new Rectangle(this.ImportantTreasure4Client.X + this.DisplayOffset.X, this.ImportantTreasure4Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure5DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(5) == true && MaxImportantTreasureShowNumber > 4)
                {
                    return new Rectangle(this.ImportantTreasure5Client.X + this.DisplayOffset.X, this.ImportantTreasure5Client.Y + this.DisplayOffset.Y, this.ImportantTreasure5Client.Width, this.ImportantTreasure5Client.Height);
                }
                return new Rectangle(this.ImportantTreasure5Client.X + this.DisplayOffset.X, this.ImportantTreasure5Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure6DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(6) == true && MaxImportantTreasureShowNumber > 5)
                {
                    return new Rectangle(this.ImportantTreasure6Client.X + this.DisplayOffset.X, this.ImportantTreasure6Client.Y + this.DisplayOffset.Y, this.ImportantTreasure6Client.Width, this.ImportantTreasure6Client.Height);
                }
                return new Rectangle(this.ImportantTreasure6Client.X + this.DisplayOffset.X, this.ImportantTreasure6Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure7DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(7) == true && MaxImportantTreasureShowNumber > 6)
                {
                    return new Rectangle(this.ImportantTreasure7Client.X + this.DisplayOffset.X, this.ImportantTreasure7Client.Y + this.DisplayOffset.Y, this.ImportantTreasure7Client.Width, this.ImportantTreasure7Client.Height);
                }
                return new Rectangle(this.ImportantTreasure7Client.X + this.DisplayOffset.X, this.ImportantTreasure7Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure8DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(8) == true && MaxImportantTreasureShowNumber > 7)
                {
                    return new Rectangle(this.ImportantTreasure8Client.X + this.DisplayOffset.X, this.ImportantTreasure8Client.Y + this.DisplayOffset.Y, this.ImportantTreasure8Client.Width, this.ImportantTreasure8Client.Height);
                }
                return new Rectangle(this.ImportantTreasure8Client.X + this.DisplayOffset.X, this.ImportantTreasure8Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure9DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(9) == true && MaxImportantTreasureShowNumber > 8)
                {
                    return new Rectangle(this.ImportantTreasure9Client.X + this.DisplayOffset.X, this.ImportantTreasure9Client.Y + this.DisplayOffset.Y, this.ImportantTreasure9Client.Width, this.ImportantTreasure9Client.Height);
                }
                return new Rectangle(this.ImportantTreasure9Client.X + this.DisplayOffset.X, this.ImportantTreasure9Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure10DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(10) == true && MaxImportantTreasureShowNumber > 9)
                {
                    return new Rectangle(this.ImportantTreasure10Client.X + this.DisplayOffset.X, this.ImportantTreasure10Client.Y + this.DisplayOffset.Y, this.ImportantTreasure10Client.Width, this.ImportantTreasure10Client.Height);
                }
                return new Rectangle(this.ImportantTreasure10Client.X + this.DisplayOffset.X, this.ImportantTreasure10Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure11DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(11) == true && MaxImportantTreasureShowNumber > 10)
                {
                    return new Rectangle(this.ImportantTreasure11Client.X + this.DisplayOffset.X, this.ImportantTreasure11Client.Y + this.DisplayOffset.Y, this.ImportantTreasure11Client.Width, this.ImportantTreasure11Client.Height);
                }
                return new Rectangle(this.ImportantTreasure11Client.X + this.DisplayOffset.X, this.ImportantTreasure11Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure12DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(12) == true && MaxImportantTreasureShowNumber > 11)
                {
                    return new Rectangle(this.ImportantTreasure12Client.X + this.DisplayOffset.X, this.ImportantTreasure12Client.Y + this.DisplayOffset.Y, this.ImportantTreasure12Client.Width, this.ImportantTreasure12Client.Height);
                }
                return new Rectangle(this.ImportantTreasure12Client.X + this.DisplayOffset.X, this.ImportantTreasure12Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure13DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(13) == true && MaxImportantTreasureShowNumber > 12)
                {
                    return new Rectangle(this.ImportantTreasure13Client.X + this.DisplayOffset.X, this.ImportantTreasure13Client.Y + this.DisplayOffset.Y, this.ImportantTreasure13Client.Width, this.ImportantTreasure13Client.Height);
                }
                return new Rectangle(this.ImportantTreasure13Client.X + this.DisplayOffset.X, this.ImportantTreasure13Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure14DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(14) == true && MaxImportantTreasureShowNumber > 13)
                {
                    return new Rectangle(this.ImportantTreasure14Client.X + this.DisplayOffset.X, this.ImportantTreasure14Client.Y + this.DisplayOffset.Y, this.ImportantTreasure14Client.Width, this.ImportantTreasure14Client.Height);
                }
                return new Rectangle(this.ImportantTreasure14Client.X + this.DisplayOffset.X, this.ImportantTreasure14Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure15DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(15) == true && MaxImportantTreasureShowNumber > 14)
                {
                    return new Rectangle(this.ImportantTreasure15Client.X + this.DisplayOffset.X, this.ImportantTreasure15Client.Y + this.DisplayOffset.Y, this.ImportantTreasure15Client.Width, this.ImportantTreasure15Client.Height);
                }
                return new Rectangle(this.ImportantTreasure15Client.X + this.DisplayOffset.X, this.ImportantTreasure15Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure16DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(16) == true && MaxImportantTreasureShowNumber > 15)
                {
                    return new Rectangle(this.ImportantTreasure16Client.X + this.DisplayOffset.X, this.ImportantTreasure16Client.Y + this.DisplayOffset.Y, this.ImportantTreasure16Client.Width, this.ImportantTreasure16Client.Height);
                }
                return new Rectangle(this.ImportantTreasure16Client.X + this.DisplayOffset.X, this.ImportantTreasure16Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure17DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(17) == true && MaxImportantTreasureShowNumber > 16)
                {
                    return new Rectangle(this.ImportantTreasure17Client.X + this.DisplayOffset.X, this.ImportantTreasure17Client.Y + this.DisplayOffset.Y, this.ImportantTreasure17Client.Width, this.ImportantTreasure17Client.Height);
                }
                return new Rectangle(this.ImportantTreasure17Client.X + this.DisplayOffset.X, this.ImportantTreasure17Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure18DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(18) == true && MaxImportantTreasureShowNumber > 17)
                {
                    return new Rectangle(this.ImportantTreasure18Client.X + this.DisplayOffset.X, this.ImportantTreasure18Client.Y + this.DisplayOffset.Y, this.ImportantTreasure18Client.Width, this.ImportantTreasure18Client.Height);
                }
                return new Rectangle(this.ImportantTreasure18Client.X + this.DisplayOffset.X, this.ImportantTreasure18Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure19DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(18) == true && MaxImportantTreasureShowNumber > 18)
                {
                    return new Rectangle(this.ImportantTreasure19Client.X + this.DisplayOffset.X, this.ImportantTreasure19Client.Y + this.DisplayOffset.Y, this.ImportantTreasure19Client.Width, this.ImportantTreasure19Client.Height);
                }
                return new Rectangle(this.ImportantTreasure19Client.X + this.DisplayOffset.X, this.ImportantTreasure19Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTreasure20DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch114 == "on" && ImportantTreasureButton == true && HasTheImportantTreasure(18) == true && MaxImportantTreasureShowNumber > 19)
                {
                    return new Rectangle(this.ImportantTreasure20Client.X + this.DisplayOffset.X, this.ImportantTreasure20Client.Y + this.DisplayOffset.Y, this.ImportantTreasure20Client.Width, this.ImportantTreasure20Client.Height);
                }
                return new Rectangle(this.ImportantTreasure20Client.X + this.DisplayOffset.X, this.ImportantTreasure20Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
        //////////////////////////////
         private bool IsImportantTreasure(int i)
         {
             bool Is = false;
             if (i == ImportantTreasure1Group && MaxImportantTreasureShowNumber >= 1) { Is = true; }
             else if (i == ImportantTreasure2Group && MaxImportantTreasureShowNumber >= 2) { Is = true; }
             else if (i == ImportantTreasure3Group && MaxImportantTreasureShowNumber >= 3) { Is = true; }
             else if (i == ImportantTreasure4Group && MaxImportantTreasureShowNumber >= 4) { Is = true; }
             else if (i == ImportantTreasure5Group && MaxImportantTreasureShowNumber >= 5) { Is = true; }
             else if (i == ImportantTreasure6Group && MaxImportantTreasureShowNumber >= 6) { Is = true; }
             else if (i == ImportantTreasure7Group && MaxImportantTreasureShowNumber >= 7) { Is = true; }
             else if (i == ImportantTreasure8Group && MaxImportantTreasureShowNumber >= 8) { Is = true; }
             else if (i == ImportantTreasure9Group && MaxImportantTreasureShowNumber >= 9) { Is = true; }
             else if (i == ImportantTreasure10Group && MaxImportantTreasureShowNumber >= 10) { Is = true; }
             else if (i == ImportantTreasure11Group && MaxImportantTreasureShowNumber >= 11) { Is = true; }
             else if (i == ImportantTreasure12Group && MaxImportantTreasureShowNumber >= 12) { Is = true; }
             else if (i == ImportantTreasure13Group && MaxImportantTreasureShowNumber >= 13) { Is = true; }
             else if (i == ImportantTreasure14Group && MaxImportantTreasureShowNumber >= 14) { Is = true; }
             else if (i == ImportantTreasure15Group && MaxImportantTreasureShowNumber >= 15) { Is = true; }
             else if (i == ImportantTreasure16Group && MaxImportantTreasureShowNumber >= 16) { Is = true; }
             else if (i == ImportantTreasure17Group && MaxImportantTreasureShowNumber >= 17) { Is = true; }
             else if (i == ImportantTreasure18Group && MaxImportantTreasureShowNumber >= 18) { Is = true; }
             else if (i == ImportantTreasure19Group && MaxImportantTreasureShowNumber >= 19) { Is = true; }
             else if (i == ImportantTreasure20Group && MaxImportantTreasureShowNumber >= 20) { Is = true; }
             return Is;
         }
         private PlatformTexture TheGeneralTreasurePicture(int i)
         {
            PlatformTexture P = this.PictureNull;
             int n = 0;
             foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
             {
                 int g = 0;
                 g = t.TreasureGroup;
                 if (i == 0 && IsImportantTreasure(g) == false)
                 {
                     P = t.Picture;
                     break;
                 }
                 else if (i == n && IsImportantTreasure(g) == false)
                 {
                     P = t.Picture;
                     break;
                 }
                 if (IsImportantTreasure(g) == false) { n++; }
             }
             return P;
         }
         private Rectangle GeneralTreasureDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             int I = 0;
             int H = 0;
             int V = 0;
             int XN = 0;
             int YN = 0;
             I = i + 1;
             XN = this.GeneralTreasureShowClient.Width;
             YN = this.GeneralTreasureShowClient.Height;
             for (int n = 0; n < GeneralTreasureVNumber; n++)
             {
                 if (I <= (n + 1) * GeneralTreasureHNumber)
                 {
                     H = I - n * GeneralTreasureHNumber - 1;
                     V = n;
                     D = new Rectangle(H * this.GeneralTreasureHSpace + (H*XN) + this.GeneralTreasureShowClient.X + this.DisplayOffset.X, V * this.GeneralTreasureVSpace + (V*YN) + this.GeneralTreasureShowClient.Y + this.DisplayOffset.Y, this.GeneralTreasureShowClient.Width, this.GeneralTreasureShowClient.Height);
                     break;
                 }
             }             
             return D;
         }
         private string TheGeneralTreasureName(int i)
         {
             string s = "";
             int n = 0;
             foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
             {
                 int g = 0;
                 g = t.TreasureGroup;
                 if (i == 0 && IsImportantTreasure(g) == false)
                 {
                     s = t.Name;
                     break;
                 }
                 else if (i == n && IsImportantTreasure(g) == false)
                 {
                     s = t.Name;
                     break;
                 }
                 if (IsImportantTreasure(g) == false) { n++; }
             }
             return s;
         }
         private string TheGeneralTreasureWorth(int i)
         {
             string s = "";
             int n = 0;
             foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
             {
                 int g = 0;
                 g = t.TreasureGroup;
                 if (i == 0 && IsImportantTreasure(g) == false)
                 {
                     s = t.Worth.ToString();
                     break;
                 }
                 else if (i == n && IsImportantTreasure(g) == false)
                 {
                     s = t.Worth.ToString();
                     break;
                 }
                 if (IsImportantTreasure(g) == false) { n++; }
             }
             return s;
         }
         private string TheGeneralTreasureDescription(int i)
         {
             string s = "";
             int n = 0;
             foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
             {
                 int g = 0;
                 g = t.TreasureGroup;
                 if (i == 0 && IsImportantTreasure(g) == false)
                 {
                     s = t.Description;
                     break;
                 }
                 else if (i == n && IsImportantTreasure(g) == false)
                 {
                     s = t.Description;
                     break;
                 }
                 if (IsImportantTreasure(g) == false) { n++; }
             }
             return s;
         }
         private Rectangle TheGeneralTreasureDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             if (TheGeneralTreasurePicture(i) != this.PictureNull)
             {
                 D = GeneralTreasureDisplayPosition(i);
             }
             return D;
         }         

        /////////////////////////////

        //  
        private Rectangle ImportantTitleTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.ImportantTitleText.DisplayOffset.X, this.ImportantTitleText.DisplayOffset.Y, this.ImportantTitleText.ClientWidth, this.ImportantTitleText.ClientHeight);
            }
        }
        private Rectangle ImportantTitleTextBackgroundDisplayPosition
        {
            get
            {
                if (InformationButton == true && ImportantTitleTextIng == true)
                {
                    if (ImportantTitleTextFollowTheMouse == "1")
                    {
                        return new Rectangle(this.ImportantTitleTextBackgroundClient.X + Session.MainGame.mainGameScreen.MousePosition.X, this.ImportantTitleTextBackgroundClient.Y + Session.MainGame.mainGameScreen.MousePosition.Y, this.ImportantTitleTextBackgroundClient.Width, this.ImportantTitleTextBackgroundClient.Height);
                    }
                    return new Rectangle(this.ImportantTitleTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantTitleTextBackgroundClient.Y + this.DisplayOffset.Y, this.ImportantTitleTextBackgroundClient.Width, this.ImportantTitleTextBackgroundClient.Height);
                }
                return new Rectangle(this.ImportantTitleTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantTitleTextBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private PlatformTexture TheImportantTitleBackground(int t)
        {
            PlatformTexture P = this.PictureNull;
            if (t == 1) { P = this.ImportantTitle1Background; }
            else if (t == 2) { P = this.ImportantTitle2Background; }
            else if (t == 3) { P = this.ImportantTitle3Background; }
            else if (t == 4) { P = this.ImportantTitle4Background; }
            else if (t == 5) { P = this.ImportantTitle5Background; }
            else if (t == 6) { P = this.ImportantTitle6Background; }
            else if (t == 7) { P = this.ImportantTitle7Background; }
            else if (t == 8) { P = this.ImportantTitle8Background; }
            else if (t == 9) { P = this.ImportantTitle9Background; }
            else if (t == 10) { P = this.ImportantTitle10Background; }
            else if (t == 11) { P = this.ImportantTitle11Background; }
            else if (t == 12) { P = this.ImportantTitle12Background; }
            else if (t == 13) { P = this.ImportantTitle13Background; }
            else if (t == 14) { P = this.ImportantTitle14Background; }
            else if (t == 15) { P = this.ImportantTitle15Background; }
            else if (t == 16) { P = this.ImportantTitle16Background; }
            else if (t == 17) { P = this.ImportantTitle17Background; }
            else if (t == 18) { P = this.ImportantTitle18Background; }
            else if (t == 19) { P = this.ImportantTitle19Background; }
            else if (t == 20) { P = this.ImportantTitle20Background; }
            return P;
        }
        private PlatformTexture TheImportantTitlePicture(int t)
        {
            PlatformTexture P = this.PictureNull;
            if (t == 1) { P = this.ImportantTitle1Picture; }
            else if (t == 2) { P = this.ImportantTitle2Picture; }
            else if (t == 3) { P = this.ImportantTitle3Picture; }
            else if (t == 4) { P = this.ImportantTitle4Picture; }
            else if (t == 5) { P = this.ImportantTitle5Picture; }
            else if (t == 6) { P = this.ImportantTitle6Picture; }
            else if (t == 7) { P = this.ImportantTitle7Picture; }
            else if (t == 8) { P = this.ImportantTitle8Picture; }
            else if (t == 9) { P = this.ImportantTitle9Picture; }
            else if (t == 10) { P = this.ImportantTitle10Picture; }
            else if (t == 11) { P = this.ImportantTitle11Picture; }
            else if (t == 12) { P = this.ImportantTitle12Picture; }
            else if (t == 13) { P = this.ImportantTitle13Picture; }
            else if (t == 14) { P = this.ImportantTitle14Picture; }
            else if (t == 15) { P = this.ImportantTitle15Picture; }
            else if (t == 16) { P = this.ImportantTitle16Picture; }
            else if (t == 17) { P = this.ImportantTitle17Picture; }
            else if (t == 18) { P = this.ImportantTitle18Picture; }
            else if (t == 19) { P = this.ImportantTitle19Picture; }
            else if (t == 20) { P = this.ImportantTitle20Picture; }
            return P;
        }
        private Rectangle TheImportantTitleDisplayPosition(int t)
        {
            Rectangle D = this.NullDisplayPosition;
            if (t == 1) { D = this.ImportantTitle1DisplayPosition; }
            else if (t == 2) { D = this.ImportantTitle2DisplayPosition; }
            else if (t == 3) { D = this.ImportantTitle3DisplayPosition; }
            else if (t == 4) { D = this.ImportantTitle4DisplayPosition; }
            else if (t == 5) { D = this.ImportantTitle5DisplayPosition; }
            else if (t == 6) { D = this.ImportantTitle6DisplayPosition; }
            else if (t == 7) { D = this.ImportantTitle7DisplayPosition; }
            else if (t == 8) { D = this.ImportantTitle8DisplayPosition; }
            else if (t == 9) { D = this.ImportantTitle9DisplayPosition; }
            else if (t == 10) { D = this.ImportantTitle10DisplayPosition; }
            else if (t == 11) { D = this.ImportantTitle11DisplayPosition; }
            else if (t == 12) { D = this.ImportantTitle12DisplayPosition; }
            else if (t == 13) { D = this.ImportantTitle13DisplayPosition; }
            else if (t == 14) { D = this.ImportantTitle14DisplayPosition; }
            else if (t == 15) { D = this.ImportantTitle15DisplayPosition; }
            else if (t == 16) { D = this.ImportantTitle16DisplayPosition; }
            else if (t == 17) { D = this.ImportantTitle17DisplayPosition; }
            else if (t == 18) { D = this.ImportantTitle18DisplayPosition; }
            else if (t == 19) { D = this.ImportantTitle19DisplayPosition; }
            else if (t == 20) { D = this.ImportantTitle20DisplayPosition; }
            return D;
        }
        private Rectangle TheImportantTitlePictureDisplayPosition(int t)
        {
            Rectangle D = this.NullDisplayPosition;
            if (t == 1) { D = this.ImportantTitle1PictureDisplayPosition; }
            else if (t == 2) { D = this.ImportantTitle2PictureDisplayPosition; }
            else if (t == 3) { D = this.ImportantTitle3PictureDisplayPosition; }
            else if (t == 4) { D = this.ImportantTitle4PictureDisplayPosition; }
            else if (t == 5) { D = this.ImportantTitle5PictureDisplayPosition; }
            else if (t == 6) { D = this.ImportantTitle6PictureDisplayPosition; }
            else if (t == 7) { D = this.ImportantTitle7PictureDisplayPosition; }
            else if (t == 8) { D = this.ImportantTitle8PictureDisplayPosition; }
            else if (t == 9) { D = this.ImportantTitle9PictureDisplayPosition; }
            else if (t == 10) { D = this.ImportantTitle10PictureDisplayPosition; }
            else if (t == 11) { D = this.ImportantTitle11PictureDisplayPosition; }
            else if (t == 12) { D = this.ImportantTitle12PictureDisplayPosition; }
            else if (t == 13) { D = this.ImportantTitle13PictureDisplayPosition; }
            else if (t == 14) { D = this.ImportantTitle14PictureDisplayPosition; }
            else if (t == 15) { D = this.ImportantTitle15PictureDisplayPosition; }
            else if (t == 16) { D = this.ImportantTitle16PictureDisplayPosition; }
            else if (t == 17) { D = this.ImportantTitle17PictureDisplayPosition; }
            else if (t == 18) { D = this.ImportantTitle18PictureDisplayPosition; }
            else if (t == 19) { D = this.ImportantTitle19PictureDisplayPosition; }
            else if (t == 20) { D = this.ImportantTitle20PictureDisplayPosition; }
            return D;
        }
        private string TheImportantTitleKind(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle1Group); }
            else if (t == 2) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle2Group); }
            else if (t == 3) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle3Group); }
            else if (t == 4) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle4Group); }
            else if (t == 5) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle5Group); }
            else if (t == 6) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle6Group); }
            else if (t == 7) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle7Group); }
            else if (t == 8) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle8Group); }
            else if (t == 9) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle9Group); }
            else if (t == 10) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle10Group); }
            else if (t == 11) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle11Group); }
            else if (t == 12) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle12Group); }
            else if (t == 13) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle13Group); }
            else if (t == 14) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle14Group); }
            else if (t == 15) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle15Group); }
            else if (t == 16) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle16Group); }
            else if (t == 17) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle17Group); }
            else if (t == 18) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle18Group); }
            else if (t == 19) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle19Group); }
            else if (t == 20) { s = this.ShowingPerson.TitleKindforGroup(ImportantTitle20Group); }
            return s;
        }
        private string TheImportantTitleLevel(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle1Group).ToString(); }
            else if (t == 2) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle2Group).ToString(); }
            else if (t == 3) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle3Group).ToString(); }
            else if (t == 4) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle4Group).ToString(); }
            else if (t == 5) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle5Group).ToString(); }
            else if (t == 6) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle6Group).ToString(); }
            else if (t == 7) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle7Group).ToString(); }
            else if (t == 8) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle8Group).ToString(); }
            else if (t == 9) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle9Group).ToString(); }
            else if (t == 10) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle10Group).ToString(); }
            else if (t == 11) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle11Group).ToString(); }
            else if (t == 12) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle12Group).ToString(); }
            else if (t == 13) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle13Group).ToString(); }
            else if (t == 14) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle14Group).ToString(); }
            else if (t == 15) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle15Group).ToString(); }
            else if (t == 16) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle16Group).ToString(); }
            else if (t == 17) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle17Group).ToString(); }
            else if (t == 18) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle18Group).ToString(); }
            else if (t == 19) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle19Group).ToString(); }
            else if (t == 20) { s = this.ShowingPerson.TitleLevelforGroup(ImportantTitle20Group).ToString(); }
            return s;
        }
        private string TheImportantTitleDescription(int t)
        {
            string s = "";
            if (t == 1) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle1Group); }
            else if (t == 2) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle2Group); }
            else if (t == 3) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle3Group); }
            else if (t == 4) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle4Group); }
            else if (t == 5) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle5Group); }
            else if (t == 6) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle6Group); }
            else if (t == 7) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle7Group); }
            else if (t == 8) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle8Group); }
            else if (t == 9) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle9Group); }
            else if (t == 10) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle10Group); }
            else if (t == 11) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle11Group); }
            else if (t == 12) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle12Group); }
            else if (t == 13) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle13Group); }
            else if (t == 14) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle14Group); }
            else if (t == 15) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle15Group); }
            else if (t == 16) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle16Group); }
            else if (t == 17) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle17Group); }
            else if (t == 18) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle18Group); }
            else if (t == 19) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle19Group); }
            else if (t == 20) { s = this.ShowingPerson.TitleDescriptionforGroup(ImportantTitle20Group); }
            return s;
        }
        private bool HasTheImportantTitle(int t)
        {
            bool H = false;
            if (t == 1) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle1Group); }
            else if (t == 2) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle2Group); }
            else if (t == 3) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle3Group); }
            else if (t == 4) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle4Group); }
            else if (t == 5) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle5Group); }
            else if (t == 6) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle6Group); }
            else if (t == 7) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle7Group); }
            else if (t == 8) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle8Group); }
            else if (t == 9) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle9Group); }
            else if (t == 10) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle10Group); }
            else if (t == 11) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle11Group); }
            else if (t == 12) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle12Group); }
            else if (t == 13) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle13Group); }
            else if (t == 14) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle14Group); }
            else if (t == 15) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle15Group); }
            else if (t == 16) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle16Group); }
            else if (t == 17) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle17Group); }
            else if (t == 18) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle18Group); }
            else if (t == 19) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle19Group); }
            else if (t == 20) { H = this.ShowingPerson.HasTitleforGroup(ImportantTitle20Group); }
            return H;
        }
        private Rectangle ImportantTitle1DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(1) == true && MaxImportantTitleShowNumber > 0)
                {
                    return new Rectangle(this.ImportantTitle1Client.X + this.DisplayOffset.X, this.ImportantTitle1Client.Y + this.DisplayOffset.Y, this.ImportantTitle1Client.Width, this.ImportantTitle1Client.Height);
                }
                return new Rectangle(this.ImportantTitle1Client.X + this.DisplayOffset.X, this.ImportantTitle1Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle2DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(2) == true && MaxImportantTitleShowNumber > 1)
                {
                    return new Rectangle(this.ImportantTitle2Client.X + this.DisplayOffset.X, this.ImportantTitle2Client.Y + this.DisplayOffset.Y, this.ImportantTitle2Client.Width, this.ImportantTitle2Client.Height);
                }
                return new Rectangle(this.ImportantTitle2Client.X + this.DisplayOffset.X, this.ImportantTitle2Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle3DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(3) == true && MaxImportantTitleShowNumber > 2)
                {
                    return new Rectangle(this.ImportantTitle3Client.X + this.DisplayOffset.X, this.ImportantTitle3Client.Y + this.DisplayOffset.Y, this.ImportantTitle3Client.Width, this.ImportantTitle3Client.Height);
                }
                return new Rectangle(this.ImportantTitle3Client.X + this.DisplayOffset.X, this.ImportantTitle3Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle4DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(4) == true && MaxImportantTitleShowNumber > 3)
                {
                    return new Rectangle(this.ImportantTitle4Client.X + this.DisplayOffset.X, this.ImportantTitle4Client.Y + this.DisplayOffset.Y, this.ImportantTitle4Client.Width, this.ImportantTitle4Client.Height);
                }
                return new Rectangle(this.ImportantTitle4Client.X + this.DisplayOffset.X, this.ImportantTitle4Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle5DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(5) == true && MaxImportantTitleShowNumber > 4)
                {
                    return new Rectangle(this.ImportantTitle5Client.X + this.DisplayOffset.X, this.ImportantTitle5Client.Y + this.DisplayOffset.Y, this.ImportantTitle5Client.Width, this.ImportantTitle5Client.Height);
                }
                return new Rectangle(this.ImportantTitle5Client.X + this.DisplayOffset.X, this.ImportantTitle5Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle6DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(6) == true && MaxImportantTitleShowNumber > 5)
                {
                    return new Rectangle(this.ImportantTitle6Client.X + this.DisplayOffset.X, this.ImportantTitle6Client.Y + this.DisplayOffset.Y, this.ImportantTitle6Client.Width, this.ImportantTitle6Client.Height);
                }
                return new Rectangle(this.ImportantTitle6Client.X + this.DisplayOffset.X, this.ImportantTitle6Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle7DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(7) == true && MaxImportantTitleShowNumber > 6)
                {
                    return new Rectangle(this.ImportantTitle7Client.X + this.DisplayOffset.X, this.ImportantTitle7Client.Y + this.DisplayOffset.Y, this.ImportantTitle7Client.Width, this.ImportantTitle7Client.Height);
                }
                return new Rectangle(this.ImportantTitle7Client.X + this.DisplayOffset.X, this.ImportantTitle7Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle8DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(8) == true && MaxImportantTitleShowNumber > 7)
                {
                    return new Rectangle(this.ImportantTitle8Client.X + this.DisplayOffset.X, this.ImportantTitle8Client.Y + this.DisplayOffset.Y, this.ImportantTitle8Client.Width, this.ImportantTitle8Client.Height);
                }
                return new Rectangle(this.ImportantTitle8Client.X + this.DisplayOffset.X, this.ImportantTitle8Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle9DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(9) == true && MaxImportantTitleShowNumber > 8)
                {
                    return new Rectangle(this.ImportantTitle9Client.X + this.DisplayOffset.X, this.ImportantTitle9Client.Y + this.DisplayOffset.Y, this.ImportantTitle9Client.Width, this.ImportantTitle9Client.Height);
                }
                return new Rectangle(this.ImportantTitle9Client.X + this.DisplayOffset.X, this.ImportantTitle9Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle10DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(10) == true && MaxImportantTitleShowNumber > 9)
                {
                    return new Rectangle(this.ImportantTitle10Client.X + this.DisplayOffset.X, this.ImportantTitle10Client.Y + this.DisplayOffset.Y, this.ImportantTitle10Client.Width, this.ImportantTitle10Client.Height);
                }
                return new Rectangle(this.ImportantTitle10Client.X + this.DisplayOffset.X, this.ImportantTitle10Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle11DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(11) == true && MaxImportantTitleShowNumber > 10)
                {
                    return new Rectangle(this.ImportantTitle11Client.X + this.DisplayOffset.X, this.ImportantTitle11Client.Y + this.DisplayOffset.Y, this.ImportantTitle11Client.Width, this.ImportantTitle11Client.Height);
                }
                return new Rectangle(this.ImportantTitle11Client.X + this.DisplayOffset.X, this.ImportantTitle11Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle12DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(12) == true && MaxImportantTitleShowNumber > 11)
                {
                    return new Rectangle(this.ImportantTitle12Client.X + this.DisplayOffset.X, this.ImportantTitle12Client.Y + this.DisplayOffset.Y, this.ImportantTitle12Client.Width, this.ImportantTitle12Client.Height);
                }
                return new Rectangle(this.ImportantTitle12Client.X + this.DisplayOffset.X, this.ImportantTitle12Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle13DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(13) == true && MaxImportantTitleShowNumber > 12)
                {
                    return new Rectangle(this.ImportantTitle13Client.X + this.DisplayOffset.X, this.ImportantTitle13Client.Y + this.DisplayOffset.Y, this.ImportantTitle13Client.Width, this.ImportantTitle13Client.Height);
                }
                return new Rectangle(this.ImportantTitle13Client.X + this.DisplayOffset.X, this.ImportantTitle13Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle14DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(14) == true && MaxImportantTitleShowNumber > 13)
                {
                    return new Rectangle(this.ImportantTitle14Client.X + this.DisplayOffset.X, this.ImportantTitle14Client.Y + this.DisplayOffset.Y, this.ImportantTitle14Client.Width, this.ImportantTitle14Client.Height);
                }
                return new Rectangle(this.ImportantTitle14Client.X + this.DisplayOffset.X, this.ImportantTitle14Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle15DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(15) == true && MaxImportantTitleShowNumber > 14)
                {
                    return new Rectangle(this.ImportantTitle15Client.X + this.DisplayOffset.X, this.ImportantTitle15Client.Y + this.DisplayOffset.Y, this.ImportantTitle15Client.Width, this.ImportantTitle15Client.Height);
                }
                return new Rectangle(this.ImportantTitle15Client.X + this.DisplayOffset.X, this.ImportantTitle15Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle16DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(16) == true && MaxImportantTitleShowNumber > 15)
                {
                    return new Rectangle(this.ImportantTitle16Client.X + this.DisplayOffset.X, this.ImportantTitle16Client.Y + this.DisplayOffset.Y, this.ImportantTitle16Client.Width, this.ImportantTitle16Client.Height);
                }
                return new Rectangle(this.ImportantTitle16Client.X + this.DisplayOffset.X, this.ImportantTitle16Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle17DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(17) == true && MaxImportantTitleShowNumber > 16)
                {
                    return new Rectangle(this.ImportantTitle17Client.X + this.DisplayOffset.X, this.ImportantTitle17Client.Y + this.DisplayOffset.Y, this.ImportantTitle17Client.Width, this.ImportantTitle17Client.Height);
                }
                return new Rectangle(this.ImportantTitle17Client.X + this.DisplayOffset.X, this.ImportantTitle17Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle18DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(18) == true && MaxImportantTitleShowNumber > 17)
                {
                    return new Rectangle(this.ImportantTitle18Client.X + this.DisplayOffset.X, this.ImportantTitle18Client.Y + this.DisplayOffset.Y, this.ImportantTitle18Client.Width, this.ImportantTitle18Client.Height);
                }
                return new Rectangle(this.ImportantTitle18Client.X + this.DisplayOffset.X, this.ImportantTitle18Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle19DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(19) == true && MaxImportantTitleShowNumber > 18)
                {
                    return new Rectangle(this.ImportantTitle19Client.X + this.DisplayOffset.X, this.ImportantTitle19Client.Y + this.DisplayOffset.Y, this.ImportantTitle19Client.Width, this.ImportantTitle19Client.Height);
                }
                return new Rectangle(this.ImportantTitle19Client.X + this.DisplayOffset.X, this.ImportantTitle19Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle20DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(20) == true && MaxImportantTitleShowNumber > 19)
                {
                    return new Rectangle(this.ImportantTitle20Client.X + this.DisplayOffset.X, this.ImportantTitle19Client.Y + this.DisplayOffset.Y, this.ImportantTitle20Client.Width, this.ImportantTitle20Client.Height);
                }
                return new Rectangle(this.ImportantTitle20Client.X + this.DisplayOffset.X, this.ImportantTitle19Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle1PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(1) == true && MaxImportantTitleShowNumber > 0)
                {
                    return new Rectangle(this.ImportantTitle1PictureClient.X + this.DisplayOffset.X, this.ImportantTitle1PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle1PictureClient.Width, this.ImportantTitle1PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle1PictureClient.X + this.DisplayOffset.X, this.ImportantTitle1PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle2PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(2) == true && MaxImportantTitleShowNumber > 1)
                {
                    return new Rectangle(this.ImportantTitle2PictureClient.X + this.DisplayOffset.X, this.ImportantTitle2PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle2PictureClient.Width, this.ImportantTitle2PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle2PictureClient.X + this.DisplayOffset.X, this.ImportantTitle2PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle3PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(3) == true && MaxImportantTitleShowNumber > 2)
                {
                    return new Rectangle(this.ImportantTitle3PictureClient.X + this.DisplayOffset.X, this.ImportantTitle3PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle3PictureClient.Width, this.ImportantTitle3PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle3PictureClient.X + this.DisplayOffset.X, this.ImportantTitle3PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle4PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(4) == true && MaxImportantTitleShowNumber > 3)
                {
                    return new Rectangle(this.ImportantTitle4PictureClient.X + this.DisplayOffset.X, this.ImportantTitle4PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle4PictureClient.Width, this.ImportantTitle4PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle4PictureClient.X + this.DisplayOffset.X, this.ImportantTitle4PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle5PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(5) == true && MaxImportantTitleShowNumber > 4)
                {
                    return new Rectangle(this.ImportantTitle5PictureClient.X + this.DisplayOffset.X, this.ImportantTitle5PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle5PictureClient.Width, this.ImportantTitle5PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle5PictureClient.X + this.DisplayOffset.X, this.ImportantTitle5PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle6PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(6) == true && MaxImportantTitleShowNumber > 5)
                {
                    return new Rectangle(this.ImportantTitle6PictureClient.X + this.DisplayOffset.X, this.ImportantTitle6PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle6PictureClient.Width, this.ImportantTitle6PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle6PictureClient.X + this.DisplayOffset.X, this.ImportantTitle6PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle7PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(7) == true && MaxImportantTitleShowNumber > 6)
                {
                    return new Rectangle(this.ImportantTitle7PictureClient.X + this.DisplayOffset.X, this.ImportantTitle7PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle7PictureClient.Width, this.ImportantTitle7PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle7PictureClient.X + this.DisplayOffset.X, this.ImportantTitle7PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle8PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(8) == true && MaxImportantTitleShowNumber > 7)
                {
                    return new Rectangle(this.ImportantTitle8PictureClient.X + this.DisplayOffset.X, this.ImportantTitle8PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle8PictureClient.Width, this.ImportantTitle8PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle8PictureClient.X + this.DisplayOffset.X, this.ImportantTitle8PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle9PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(9) == true && MaxImportantTitleShowNumber > 8)
                {
                    return new Rectangle(this.ImportantTitle9PictureClient.X + this.DisplayOffset.X, this.ImportantTitle9PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle9PictureClient.Width, this.ImportantTitle9PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle9PictureClient.X + this.DisplayOffset.X, this.ImportantTitle9PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle10PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(10) == true && MaxImportantTitleShowNumber > 9)
                {
                    return new Rectangle(this.ImportantTitle10PictureClient.X + this.DisplayOffset.X, this.ImportantTitle10PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle10PictureClient.Width, this.ImportantTitle10PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle10PictureClient.X + this.DisplayOffset.X, this.ImportantTitle10PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle11PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(11) == true && MaxImportantTitleShowNumber > 10)
                {
                    return new Rectangle(this.ImportantTitle11PictureClient.X + this.DisplayOffset.X, this.ImportantTitle11PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle11PictureClient.Width, this.ImportantTitle11PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle11PictureClient.X + this.DisplayOffset.X, this.ImportantTitle11PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle12PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(12) == true && MaxImportantTitleShowNumber > 11)
                {
                    return new Rectangle(this.ImportantTitle12PictureClient.X + this.DisplayOffset.X, this.ImportantTitle12PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle12PictureClient.Width, this.ImportantTitle12PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle12PictureClient.X + this.DisplayOffset.X, this.ImportantTitle12PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle13PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(13) == true && MaxImportantTitleShowNumber > 12)
                {
                    return new Rectangle(this.ImportantTitle13PictureClient.X + this.DisplayOffset.X, this.ImportantTitle13PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle13PictureClient.Width, this.ImportantTitle13PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle13PictureClient.X + this.DisplayOffset.X, this.ImportantTitle13PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle14PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(14) == true && MaxImportantTitleShowNumber > 13)
                {
                    return new Rectangle(this.ImportantTitle14PictureClient.X + this.DisplayOffset.X, this.ImportantTitle14PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle14PictureClient.Width, this.ImportantTitle14PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle14PictureClient.X + this.DisplayOffset.X, this.ImportantTitle14PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle15PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(15) == true && MaxImportantTitleShowNumber > 14)
                {
                    return new Rectangle(this.ImportantTitle15PictureClient.X + this.DisplayOffset.X, this.ImportantTitle15PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle15PictureClient.Width, this.ImportantTitle15PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle15PictureClient.X + this.DisplayOffset.X, this.ImportantTitle15PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle16PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(16) == true && MaxImportantTitleShowNumber > 15)
                {
                    return new Rectangle(this.ImportantTitle16PictureClient.X + this.DisplayOffset.X, this.ImportantTitle16PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle16PictureClient.Width, this.ImportantTitle16PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle16PictureClient.X + this.DisplayOffset.X, this.ImportantTitle16PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle17PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(17) == true && MaxImportantTitleShowNumber > 16)
                {
                    return new Rectangle(this.ImportantTitle17PictureClient.X + this.DisplayOffset.X, this.ImportantTitle17PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle17PictureClient.Width, this.ImportantTitle17PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle17PictureClient.X + this.DisplayOffset.X, this.ImportantTitle17PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle18PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(18) == true && MaxImportantTitleShowNumber > 17)
                {
                    return new Rectangle(this.ImportantTitle18PictureClient.X + this.DisplayOffset.X, this.ImportantTitle18PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle18PictureClient.Width, this.ImportantTitle18PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle18PictureClient.X + this.DisplayOffset.X, this.ImportantTitle18PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle19PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(19) == true && MaxImportantTitleShowNumber > 18)
                {
                    return new Rectangle(this.ImportantTitle19PictureClient.X + this.DisplayOffset.X, this.ImportantTitle19PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle19PictureClient.Width, this.ImportantTitle19PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle19PictureClient.X + this.DisplayOffset.X, this.ImportantTitle19PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantTitle20PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantTitle(20) == true && MaxImportantTitleShowNumber > 19)
                {
                    return new Rectangle(this.ImportantTitle20PictureClient.X + this.DisplayOffset.X, this.ImportantTitle19PictureClient.Y + this.DisplayOffset.Y, this.ImportantTitle20PictureClient.Width, this.ImportantTitle20PictureClient.Height);
                }
                return new Rectangle(this.ImportantTitle20PictureClient.X + this.DisplayOffset.X, this.ImportantTitle19PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
        private Rectangle ImportantStuntTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.ImportantStuntText.DisplayOffset.X, this.ImportantStuntText.DisplayOffset.Y, this.ImportantStuntText.ClientWidth, this.ImportantStuntText.ClientHeight);
            }
        }
        private Rectangle ImportantStuntTextBackgroundDisplayPosition
        {
            get
            {
                if (InformationButton == true && ImportantStuntTextIng == true)
                {
                    if (ImportantStuntTextFollowTheMouse == "1")
                    {
                        return new Rectangle(this.ImportantStuntTextBackgroundClient.X + Session.MainGame.mainGameScreen.MousePosition.X, this.ImportantStuntTextBackgroundClient.Y + Session.MainGame.mainGameScreen.MousePosition.Y, this.ImportantStuntTextBackgroundClient.Width, this.ImportantStuntTextBackgroundClient.Height);
                    }
                    return new Rectangle(this.ImportantStuntTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantStuntTextBackgroundClient.Y + this.DisplayOffset.Y, this.ImportantStuntTextBackgroundClient.Width, this.ImportantStuntTextBackgroundClient.Height);
                }
                return new Rectangle(this.ImportantStuntTextBackgroundClient.X + this.DisplayOffset.X, this.ImportantStuntTextBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private PlatformTexture TheImportantStuntBackground(int t)
        {
            PlatformTexture P = this.PictureNull;
            if (t == 1) { P = this.ImportantStunt1Background; }
            else if (t == 2) { P = this.ImportantStunt2Background; }
            else if (t == 3) { P = this.ImportantStunt3Background; }
            else if (t == 4) { P = this.ImportantStunt4Background; }
            else if (t == 5) { P = this.ImportantStunt5Background; }
            else if (t == 6) { P = this.ImportantStunt6Background; }
            else if (t == 7) { P = this.ImportantStunt7Background; }
            else if (t == 8) { P = this.ImportantStunt8Background; }
            else if (t == 9) { P = this.ImportantStunt9Background; }
            else if (t == 10) { P = this.ImportantStunt10Background; }
            else if (t == 11) { P = this.ImportantStunt11Background; }
            else if (t == 12) { P = this.ImportantStunt12Background; }
            else if (t == 13) { P = this.ImportantStunt13Background; }
            else if (t == 14) { P = this.ImportantStunt14Background; }
            else if (t == 15) { P = this.ImportantStunt15Background; }
            else if (t == 16) { P = this.ImportantStunt16Background; }
            else if (t == 17) { P = this.ImportantStunt17Background; }
            else if (t == 18) { P = this.ImportantStunt18Background; }
            else if (t == 19) { P = this.ImportantStunt19Background; }
            else if (t == 20) { P = this.ImportantStunt20Background; }
            return P;
        }
        private PlatformTexture TheImportantStuntPicture(int t)
        {
            PlatformTexture P = this.PictureNull;
            if (t == 1) { P = this.ImportantStunt1Picture; }
            else if (t == 2) { P = this.ImportantStunt2Picture; }
            else if (t == 3) { P = this.ImportantStunt3Picture; }
            else if (t == 4) { P = this.ImportantStunt4Picture; }
            else if (t == 5) { P = this.ImportantStunt5Picture; }
            else if (t == 6) { P = this.ImportantStunt6Picture; }
            else if (t == 7) { P = this.ImportantStunt7Picture; }
            else if (t == 8) { P = this.ImportantStunt8Picture; }
            else if (t == 9) { P = this.ImportantStunt9Picture; }
            else if (t == 10) { P = this.ImportantStunt10Picture; }
            else if (t == 11) { P = this.ImportantStunt11Picture; }
            else if (t == 12) { P = this.ImportantStunt12Picture; }
            else if (t == 13) { P = this.ImportantStunt13Picture; }
            else if (t == 14) { P = this.ImportantStunt14Picture; }
            else if (t == 15) { P = this.ImportantStunt15Picture; }
            else if (t == 16) { P = this.ImportantStunt16Picture; }
            else if (t == 17) { P = this.ImportantStunt17Picture; }
            else if (t == 18) { P = this.ImportantStunt18Picture; }
            else if (t == 19) { P = this.ImportantStunt19Picture; }
            else if (t == 20) { P = this.ImportantStunt20Picture; }
            return P;
        }
        private Rectangle TheImportantStuntDisplayPosition(int t)
        {
            Rectangle D = this.NullDisplayPosition;
            if (t == 1) { D = this.ImportantStunt1DisplayPosition; }
            else if (t == 2) { D = this.ImportantStunt2DisplayPosition; }
            else if (t == 3) { D = this.ImportantStunt3DisplayPosition; }
            else if (t == 4) { D = this.ImportantStunt4DisplayPosition; }
            else if (t == 5) { D = this.ImportantStunt5DisplayPosition; }
            else if (t == 6) { D = this.ImportantStunt6DisplayPosition; }
            else if (t == 7) { D = this.ImportantStunt7DisplayPosition; }
            else if (t == 8) { D = this.ImportantStunt8DisplayPosition; }
            else if (t == 9) { D = this.ImportantStunt9DisplayPosition; }
            else if (t == 10) { D = this.ImportantStunt10DisplayPosition; }
            else if (t == 11) { D = this.ImportantStunt11DisplayPosition; }
            else if (t == 12) { D = this.ImportantStunt12DisplayPosition; }
            else if (t == 13) { D = this.ImportantStunt13DisplayPosition; }
            else if (t == 14) { D = this.ImportantStunt14DisplayPosition; }
            else if (t == 15) { D = this.ImportantStunt15DisplayPosition; }
            else if (t == 16) { D = this.ImportantStunt16DisplayPosition; }
            else if (t == 17) { D = this.ImportantStunt17DisplayPosition; }
            else if (t == 18) { D = this.ImportantStunt18DisplayPosition; }
            else if (t == 19) { D = this.ImportantStunt19DisplayPosition; }
            else if (t == 20) { D = this.ImportantStunt20DisplayPosition; }
            return D;
        }
        private Rectangle TheImportantStuntPictureDisplayPosition(int t)
        {
            Rectangle D = this.NullDisplayPosition;
            if (t == 1) { D = this.ImportantStunt1PictureDisplayPosition; }
            else if (t == 2) { D = this.ImportantStunt2PictureDisplayPosition; }
            else if (t == 3) { D = this.ImportantStunt3PictureDisplayPosition; }
            else if (t == 4) { D = this.ImportantStunt4PictureDisplayPosition; }
            else if (t == 5) { D = this.ImportantStunt5PictureDisplayPosition; }
            else if (t == 6) { D = this.ImportantStunt6PictureDisplayPosition; }
            else if (t == 7) { D = this.ImportantStunt7PictureDisplayPosition; }
            else if (t == 8) { D = this.ImportantStunt8PictureDisplayPosition; }
            else if (t == 9) { D = this.ImportantStunt9PictureDisplayPosition; }
            else if (t == 10) { D = this.ImportantStunt10PictureDisplayPosition; }
            else if (t == 11) { D = this.ImportantStunt11PictureDisplayPosition; }
            else if (t == 12) { D = this.ImportantStunt12PictureDisplayPosition; }
            else if (t == 13) { D = this.ImportantStunt13PictureDisplayPosition; }
            else if (t == 14) { D = this.ImportantStunt14PictureDisplayPosition; }
            else if (t == 15) { D = this.ImportantStunt15PictureDisplayPosition; }
            else if (t == 16) { D = this.ImportantStunt16PictureDisplayPosition; }
            else if (t == 17) { D = this.ImportantStunt17PictureDisplayPosition; }
            else if (t == 18) { D = this.ImportantStunt18PictureDisplayPosition; }
            else if (t == 19) { D = this.ImportantStunt19PictureDisplayPosition; }
            else if (t == 20) { D = this.ImportantStunt20PictureDisplayPosition; }
            return D;
        }
        private string TheImportantStuntName(int t)
        {
            string s = "";
            if (t == 1)
            {
                s = this.ShowingPerson.StuntNameforGroup(ImportantStunt1Group);
            }
            else if (t == 2)
            {
                s = this.ShowingPerson.StuntNameforGroup(ImportantStunt2Group);
            }
            else if (t == 3) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt3Group); }
            else if (t == 4) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt4Group); }
            else if (t == 5) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt5Group); }
            else if (t == 6) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt6Group); }
            else if (t == 7) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt7Group); }
            else if (t == 8) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt8Group); }
            else if (t == 9) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt9Group); }
            else if (t == 10) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt10Group); }
            else if (t == 11) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt11Group); }
            else if (t == 12) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt12Group); }
            else if (t == 13) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt13Group); }
            else if (t == 14) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt14Group); }
            else if (t == 15) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt15Group); }
            else if (t == 16) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt16Group); }
            else if (t == 17) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt17Group); }
            else if (t == 18) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt18Group); }
            else if (t == 19) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt19Group); }
            else if (t == 20) { s = this.ShowingPerson.StuntNameforGroup(ImportantStunt20Group); }
            return s;
        }
        private string TheImportantStuntCombativity(int t)
        {
            string s = "";
            if (t == 1)
            {
                s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt1Group).ToString();
            }
            else if (t == 2)
            {
                s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt2Group).ToString();
            }
            else if (t == 3) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt3Group).ToString(); }
            else if (t == 4) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt4Group).ToString(); }
            else if (t == 5) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt5Group).ToString(); }
            else if (t == 6) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt6Group).ToString(); }
            else if (t == 7) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt7Group).ToString(); }
            else if (t == 8) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt8Group).ToString(); }
            else if (t == 9) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt9Group).ToString(); }
            else if (t == 10) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt10Group).ToString(); }
            else if (t == 11) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt11Group).ToString(); }
            else if (t == 12) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt12Group).ToString(); }
            else if (t == 13) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt13Group).ToString(); }
            else if (t == 14) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt14Group).ToString(); }
            else if (t == 15) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt15Group).ToString(); }
            else if (t == 16) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt16Group).ToString(); }
            else if (t == 17) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt17Group).ToString(); }
            else if (t == 18) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt18Group).ToString(); }
            else if (t == 19) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt19Group).ToString(); }
            else if (t == 20) { s = this.ShowingPerson.StuntCombativityforGroup(ImportantStunt20Group).ToString(); }
            return s;
        }
        private string TheImportantStuntDescription(int t)
        {
            string s = "";
            if (t == 1)
            {
                s = this.ImportantStunt1Description;
            }
            else if (t == 2)
            {
                s = this.ImportantStunt2Description;
            }
            else if (t == 3) { s = this.ImportantStunt3Description; }
            else if (t == 4) { s = this.ImportantStunt4Description; }
            else if (t == 5) { s = this.ImportantStunt5Description; }
            else if (t == 6) { s = this.ImportantStunt6Description; }
            else if (t == 7) { s = this.ImportantStunt7Description; }
            else if (t == 8) { s = this.ImportantStunt8Description; }
            else if (t == 9) { s = this.ImportantStunt9Description; }
            else if (t == 10) { s = this.ImportantStunt10Description; }
            else if (t == 11) { s = this.ImportantStunt11Description; }
            else if (t == 12) { s = this.ImportantStunt12Description; }
            else if (t == 13) { s = this.ImportantStunt13Description; }
            else if (t == 14) { s = this.ImportantStunt14Description; }
            else if (t == 15) { s = this.ImportantStunt15Description; }
            else if (t == 16) { s = this.ImportantStunt16Description; }
            else if (t == 17) { s = this.ImportantStunt17Description; }
            else if (t == 18) { s = this.ImportantStunt18Description; }
            else if (t == 19) { s = this.ImportantStunt19Description; }
            else if (t == 20) { s = this.ImportantStunt20Description; }
            return s;
        }
        private bool HasTheImportantStunt(int t)
        {
            bool H = false;
            if (t == 1) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt1Group); }
            else if (t == 2) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt2Group); }
            else if (t == 3) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt3Group); }
            else if (t == 4) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt4Group); }
            else if (t == 5) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt5Group); }
            else if (t == 6) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt6Group); }
            else if (t == 7) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt7Group); }
            else if (t == 8) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt8Group); }
            else if (t == 9) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt9Group); }
            else if (t == 10) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt10Group); }
            else if (t == 11) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt11Group); }
            else if (t == 12) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt12Group); }
            else if (t == 13) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt13Group); }
            else if (t == 14) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt14Group); }
            else if (t == 15) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt15Group); }
            else if (t == 16) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt16Group); }
            else if (t == 17) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt17Group); }
            else if (t == 18) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt18Group); }
            else if (t == 19) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt19Group); }
            else if (t == 20) { H = this.ShowingPerson.HasStuntforGroup(ImportantStunt20Group); }
            return H;
        }
        private Rectangle ImportantStunt1DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(1) == true && MaxImportantStuntShowNumber > 0)
                {
                    return new Rectangle(this.ImportantStunt1Client.X + this.DisplayOffset.X, this.ImportantStunt1Client.Y + this.DisplayOffset.Y, this.ImportantStunt1Client.Width, this.ImportantStunt1Client.Height);
                }
                return new Rectangle(this.ImportantStunt1Client.X + this.DisplayOffset.X, this.ImportantStunt1Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt2DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(2) == true && MaxImportantStuntShowNumber > 1)
                {
                    return new Rectangle(this.ImportantStunt2Client.X + this.DisplayOffset.X, this.ImportantStunt2Client.Y + this.DisplayOffset.Y, this.ImportantStunt2Client.Width, this.ImportantStunt2Client.Height);
                }
                return new Rectangle(this.ImportantStunt2Client.X + this.DisplayOffset.X, this.ImportantStunt2Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt3DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(3) == true && MaxImportantStuntShowNumber > 2)
                {
                    return new Rectangle(this.ImportantStunt3Client.X + this.DisplayOffset.X, this.ImportantStunt3Client.Y + this.DisplayOffset.Y, this.ImportantStunt3Client.Width, this.ImportantStunt3Client.Height);
                }
                return new Rectangle(this.ImportantStunt3Client.X + this.DisplayOffset.X, this.ImportantStunt3Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt4DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(4) == true && MaxImportantStuntShowNumber > 3)
                {
                    return new Rectangle(this.ImportantStunt4Client.X + this.DisplayOffset.X, this.ImportantStunt4Client.Y + this.DisplayOffset.Y, this.ImportantStunt4Client.Width, this.ImportantStunt4Client.Height);
                }
                return new Rectangle(this.ImportantStunt4Client.X + this.DisplayOffset.X, this.ImportantStunt4Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt5DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(5) == true && MaxImportantStuntShowNumber > 4)
                {
                    return new Rectangle(this.ImportantStunt5Client.X + this.DisplayOffset.X, this.ImportantStunt5Client.Y + this.DisplayOffset.Y, this.ImportantStunt5Client.Width, this.ImportantStunt5Client.Height);
                }
                return new Rectangle(this.ImportantStunt5Client.X + this.DisplayOffset.X, this.ImportantStunt5Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt6DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(6) == true && MaxImportantStuntShowNumber > 5)
                {
                    return new Rectangle(this.ImportantStunt6Client.X + this.DisplayOffset.X, this.ImportantStunt6Client.Y + this.DisplayOffset.Y, this.ImportantStunt6Client.Width, this.ImportantStunt6Client.Height);
                }
                return new Rectangle(this.ImportantStunt6Client.X + this.DisplayOffset.X, this.ImportantStunt6Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt7DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(7) == true && MaxImportantStuntShowNumber > 6)
                {
                    return new Rectangle(this.ImportantStunt7Client.X + this.DisplayOffset.X, this.ImportantStunt7Client.Y + this.DisplayOffset.Y, this.ImportantStunt7Client.Width, this.ImportantStunt7Client.Height);
                }
                return new Rectangle(this.ImportantStunt7Client.X + this.DisplayOffset.X, this.ImportantStunt7Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt8DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(8) == true && MaxImportantStuntShowNumber > 7)
                {
                    return new Rectangle(this.ImportantStunt8Client.X + this.DisplayOffset.X, this.ImportantStunt8Client.Y + this.DisplayOffset.Y, this.ImportantStunt8Client.Width, this.ImportantStunt8Client.Height);
                }
                return new Rectangle(this.ImportantStunt8Client.X + this.DisplayOffset.X, this.ImportantStunt8Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt9DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(9) == true && MaxImportantStuntShowNumber > 8)
                {
                    return new Rectangle(this.ImportantStunt9Client.X + this.DisplayOffset.X, this.ImportantStunt9Client.Y + this.DisplayOffset.Y, this.ImportantStunt9Client.Width, this.ImportantStunt9Client.Height);
                }
                return new Rectangle(this.ImportantStunt9Client.X + this.DisplayOffset.X, this.ImportantStunt9Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt10DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(10) == true && MaxImportantStuntShowNumber > 9)
                {
                    return new Rectangle(this.ImportantStunt10Client.X + this.DisplayOffset.X, this.ImportantStunt10Client.Y + this.DisplayOffset.Y, this.ImportantStunt10Client.Width, this.ImportantStunt10Client.Height);
                }
                return new Rectangle(this.ImportantStunt10Client.X + this.DisplayOffset.X, this.ImportantStunt10Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt11DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(11) == true && MaxImportantStuntShowNumber > 10)
                {
                    return new Rectangle(this.ImportantStunt11Client.X + this.DisplayOffset.X, this.ImportantStunt11Client.Y + this.DisplayOffset.Y, this.ImportantStunt11Client.Width, this.ImportantStunt11Client.Height);
                }
                return new Rectangle(this.ImportantStunt11Client.X + this.DisplayOffset.X, this.ImportantStunt11Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt12DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(12) == true && MaxImportantStuntShowNumber > 11)
                {
                    return new Rectangle(this.ImportantStunt12Client.X + this.DisplayOffset.X, this.ImportantStunt12Client.Y + this.DisplayOffset.Y, this.ImportantStunt12Client.Width, this.ImportantStunt12Client.Height);
                }
                return new Rectangle(this.ImportantStunt12Client.X + this.DisplayOffset.X, this.ImportantStunt12Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt13DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(13) == true && MaxImportantStuntShowNumber > 12)
                {
                    return new Rectangle(this.ImportantStunt13Client.X + this.DisplayOffset.X, this.ImportantStunt13Client.Y + this.DisplayOffset.Y, this.ImportantStunt13Client.Width, this.ImportantStunt13Client.Height);
                }
                return new Rectangle(this.ImportantStunt13Client.X + this.DisplayOffset.X, this.ImportantStunt13Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt14DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(14) == true && MaxImportantStuntShowNumber > 13)
                {
                    return new Rectangle(this.ImportantStunt14Client.X + this.DisplayOffset.X, this.ImportantStunt14Client.Y + this.DisplayOffset.Y, this.ImportantStunt14Client.Width, this.ImportantStunt14Client.Height);
                }
                return new Rectangle(this.ImportantStunt14Client.X + this.DisplayOffset.X, this.ImportantStunt14Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt15DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(15) == true && MaxImportantStuntShowNumber > 14)
                {
                    return new Rectangle(this.ImportantStunt15Client.X + this.DisplayOffset.X, this.ImportantStunt15Client.Y + this.DisplayOffset.Y, this.ImportantStunt15Client.Width, this.ImportantStunt15Client.Height);
                }
                return new Rectangle(this.ImportantStunt15Client.X + this.DisplayOffset.X, this.ImportantStunt15Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt16DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(16) == true && MaxImportantStuntShowNumber > 15)
                {
                    return new Rectangle(this.ImportantStunt16Client.X + this.DisplayOffset.X, this.ImportantStunt16Client.Y + this.DisplayOffset.Y, this.ImportantStunt16Client.Width, this.ImportantStunt16Client.Height);
                }
                return new Rectangle(this.ImportantStunt16Client.X + this.DisplayOffset.X, this.ImportantStunt16Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt17DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(17) == true && MaxImportantStuntShowNumber > 16)
                {
                    return new Rectangle(this.ImportantStunt17Client.X + this.DisplayOffset.X, this.ImportantStunt17Client.Y + this.DisplayOffset.Y, this.ImportantStunt17Client.Width, this.ImportantStunt17Client.Height);
                }
                return new Rectangle(this.ImportantStunt17Client.X + this.DisplayOffset.X, this.ImportantStunt17Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt18DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(18) == true && MaxImportantStuntShowNumber > 17)
                {
                    return new Rectangle(this.ImportantStunt18Client.X + this.DisplayOffset.X, this.ImportantStunt18Client.Y + this.DisplayOffset.Y, this.ImportantStunt18Client.Width, this.ImportantStunt18Client.Height);
                }
                return new Rectangle(this.ImportantStunt18Client.X + this.DisplayOffset.X, this.ImportantStunt18Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt19DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(19) == true && MaxImportantStuntShowNumber > 18)
                {
                    return new Rectangle(this.ImportantStunt19Client.X + this.DisplayOffset.X, this.ImportantStunt19Client.Y + this.DisplayOffset.Y, this.ImportantStunt19Client.Width, this.ImportantStunt19Client.Height);
                }
                return new Rectangle(this.ImportantStunt19Client.X + this.DisplayOffset.X, this.ImportantStunt19Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt20DisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch118 == "on" && HasTheImportantStunt(20) == true && MaxImportantStuntShowNumber > 19)
                {
                    return new Rectangle(this.ImportantStunt20Client.X + this.DisplayOffset.X, this.ImportantStunt19Client.Y + this.DisplayOffset.Y, this.ImportantStunt20Client.Width, this.ImportantStunt20Client.Height);
                }
                return new Rectangle(this.ImportantStunt20Client.X + this.DisplayOffset.X, this.ImportantStunt19Client.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt1PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(1) == true && MaxImportantStuntShowNumber > 0)
                {
                    return new Rectangle(this.ImportantStunt1PictureClient.X + this.DisplayOffset.X, this.ImportantStunt1PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt1PictureClient.Width, this.ImportantStunt1PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt1PictureClient.X + this.DisplayOffset.X, this.ImportantStunt1PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt2PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(2) == true && MaxImportantStuntShowNumber > 1)
                {
                    return new Rectangle(this.ImportantStunt2PictureClient.X + this.DisplayOffset.X, this.ImportantStunt2PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt2PictureClient.Width, this.ImportantStunt2PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt2PictureClient.X + this.DisplayOffset.X, this.ImportantStunt2PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt3PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(3) == true && MaxImportantStuntShowNumber > 2)
                {
                    return new Rectangle(this.ImportantStunt3PictureClient.X + this.DisplayOffset.X, this.ImportantStunt3PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt3PictureClient.Width, this.ImportantStunt3PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt3PictureClient.X + this.DisplayOffset.X, this.ImportantStunt3PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt4PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(4) == true && MaxImportantStuntShowNumber > 3)
                {
                    return new Rectangle(this.ImportantStunt4PictureClient.X + this.DisplayOffset.X, this.ImportantStunt4PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt4PictureClient.Width, this.ImportantStunt4PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt4PictureClient.X + this.DisplayOffset.X, this.ImportantStunt4PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt5PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(5) == true && MaxImportantStuntShowNumber > 4)
                {
                    return new Rectangle(this.ImportantStunt5PictureClient.X + this.DisplayOffset.X, this.ImportantStunt5PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt5PictureClient.Width, this.ImportantStunt5PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt5PictureClient.X + this.DisplayOffset.X, this.ImportantStunt5PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt6PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(6) == true && MaxImportantStuntShowNumber > 5)
                {
                    return new Rectangle(this.ImportantStunt6PictureClient.X + this.DisplayOffset.X, this.ImportantStunt6PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt6PictureClient.Width, this.ImportantStunt6PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt6PictureClient.X + this.DisplayOffset.X, this.ImportantStunt6PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt7PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(7) == true && MaxImportantStuntShowNumber > 6)
                {
                    return new Rectangle(this.ImportantStunt7PictureClient.X + this.DisplayOffset.X, this.ImportantStunt7PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt7PictureClient.Width, this.ImportantStunt7PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt7PictureClient.X + this.DisplayOffset.X, this.ImportantStunt7PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt8PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(8) == true && MaxImportantStuntShowNumber > 7)
                {
                    return new Rectangle(this.ImportantStunt8PictureClient.X + this.DisplayOffset.X, this.ImportantStunt8PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt8PictureClient.Width, this.ImportantStunt8PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt8PictureClient.X + this.DisplayOffset.X, this.ImportantStunt8PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt9PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(9) == true && MaxImportantStuntShowNumber > 8)
                {
                    return new Rectangle(this.ImportantStunt9PictureClient.X + this.DisplayOffset.X, this.ImportantStunt9PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt9PictureClient.Width, this.ImportantStunt9PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt9PictureClient.X + this.DisplayOffset.X, this.ImportantStunt9PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt10PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(10) == true && MaxImportantStuntShowNumber > 9)
                {
                    return new Rectangle(this.ImportantStunt10PictureClient.X + this.DisplayOffset.X, this.ImportantStunt10PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt10PictureClient.Width, this.ImportantStunt10PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt10PictureClient.X + this.DisplayOffset.X, this.ImportantStunt10PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt11PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(11) == true && MaxImportantStuntShowNumber > 10)
                {
                    return new Rectangle(this.ImportantStunt11PictureClient.X + this.DisplayOffset.X, this.ImportantStunt11PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt11PictureClient.Width, this.ImportantStunt11PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt11PictureClient.X + this.DisplayOffset.X, this.ImportantStunt11PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt12PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(12) == true && MaxImportantStuntShowNumber > 11)
                {
                    return new Rectangle(this.ImportantStunt12PictureClient.X + this.DisplayOffset.X, this.ImportantStunt12PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt12PictureClient.Width, this.ImportantStunt12PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt12PictureClient.X + this.DisplayOffset.X, this.ImportantStunt12PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt13PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(13) == true && MaxImportantStuntShowNumber > 12)
                {
                    return new Rectangle(this.ImportantStunt13PictureClient.X + this.DisplayOffset.X, this.ImportantStunt13PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt13PictureClient.Width, this.ImportantStunt13PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt13PictureClient.X + this.DisplayOffset.X, this.ImportantStunt13PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt14PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(14) == true && MaxImportantStuntShowNumber > 13)
                {
                    return new Rectangle(this.ImportantStunt14PictureClient.X + this.DisplayOffset.X, this.ImportantStunt14PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt14PictureClient.Width, this.ImportantStunt14PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt14PictureClient.X + this.DisplayOffset.X, this.ImportantStunt14PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt15PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(15) == true && MaxImportantStuntShowNumber > 14)
                {
                    return new Rectangle(this.ImportantStunt15PictureClient.X + this.DisplayOffset.X, this.ImportantStunt15PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt15PictureClient.Width, this.ImportantStunt15PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt15PictureClient.X + this.DisplayOffset.X, this.ImportantStunt15PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt16PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(16) == true && MaxImportantStuntShowNumber > 15)
                {
                    return new Rectangle(this.ImportantStunt16PictureClient.X + this.DisplayOffset.X, this.ImportantStunt16PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt16PictureClient.Width, this.ImportantStunt16PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt16PictureClient.X + this.DisplayOffset.X, this.ImportantStunt16PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt17PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(17) == true && MaxImportantStuntShowNumber > 16)
                {
                    return new Rectangle(this.ImportantStunt17PictureClient.X + this.DisplayOffset.X, this.ImportantStunt17PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt17PictureClient.Width, this.ImportantStunt17PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt17PictureClient.X + this.DisplayOffset.X, this.ImportantStunt17PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt18PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(18) == true && MaxImportantStuntShowNumber > 17)
                {
                    return new Rectangle(this.ImportantStunt18PictureClient.X + this.DisplayOffset.X, this.ImportantStunt18PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt18PictureClient.Width, this.ImportantStunt18PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt18PictureClient.X + this.DisplayOffset.X, this.ImportantStunt18PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt19PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(19) == true && MaxImportantStuntShowNumber > 18)
                {
                    return new Rectangle(this.ImportantStunt19PictureClient.X + this.DisplayOffset.X, this.ImportantStunt19PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt19PictureClient.Width, this.ImportantStunt19PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt19PictureClient.X + this.DisplayOffset.X, this.ImportantStunt19PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle ImportantStunt20PictureDisplayPosition
        {
            get
            {
                if (InformationButton == true && Switch117 == "on" && HasTheImportantStunt(20) == true && MaxImportantStuntShowNumber > 19)
                {
                    return new Rectangle(this.ImportantStunt20PictureClient.X + this.DisplayOffset.X, this.ImportantStunt19PictureClient.Y + this.DisplayOffset.Y, this.ImportantStunt20PictureClient.Width, this.ImportantStunt20PictureClient.Height);
                }
                return new Rectangle(this.ImportantStunt20PictureClient.X + this.DisplayOffset.X, this.ImportantStunt19PictureClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
         private Rectangle BiographyBriefButtonDisplayPosition
        {
            get
            {
                if (BiographyBriefButton == false && Switch26 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyBriefButtonClient.X + this.DisplayOffset.X, this.BiographyBriefButtonClient.Y + this.DisplayOffset.Y, this.BiographyBriefButtonClient.Width, this.BiographyBriefButtonClient.Height);
                }
                return new Rectangle(this.BiographyBriefButtonClient.X + this.DisplayOffset.X, this.BiographyBriefButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyBriefButtonPressedDisplayPosition
        {
            get
            {
                if (BiographyBriefButton == true && Switch26 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyBriefButtonClient.X + this.DisplayOffset.X, this.BiographyBriefButtonClient.Y + this.DisplayOffset.Y, this.BiographyBriefButtonClient.Width, this.BiographyBriefButtonClient.Height);
                }
                return new Rectangle(this.BiographyBriefButtonClient.X + this.DisplayOffset.X, this.BiographyBriefButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyRomanceButtonDisplayPosition
        {
            get
            {
                if (BiographyRomanceButton == false && Switch27 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyRomanceButtonClient.X + this.DisplayOffset.X, this.BiographyRomanceButtonClient.Y + this.DisplayOffset.Y, this.BiographyRomanceButtonClient.Width, this.BiographyRomanceButtonClient.Height);
                }
                return new Rectangle(this.BiographyRomanceButtonClient.X + this.DisplayOffset.X, this.BiographyRomanceButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyRomanceButtonPressedDisplayPosition
        {
            get
            {
                if (BiographyRomanceButton == true && Switch27 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyRomanceButtonClient.X + this.DisplayOffset.X, this.BiographyRomanceButtonClient.Y + this.DisplayOffset.Y, this.BiographyRomanceButtonClient.Width, this.BiographyRomanceButtonClient.Height);
                }
                return new Rectangle(this.BiographyRomanceButtonClient.X + this.DisplayOffset.X, this.BiographyRomanceButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyHistoryButtonDisplayPosition
        {
            get
            {
                if (BiographyHistoryButton == false && Switch28 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyHistoryButtonClient.X + this.DisplayOffset.X, this.BiographyHistoryButtonClient.Y + this.DisplayOffset.Y, this.BiographyHistoryButtonClient.Width, this.BiographyHistoryButtonClient.Height);
                }
                return new Rectangle(this.BiographyHistoryButtonClient.X + this.DisplayOffset.X, this.BiographyHistoryButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyHistoryButtonPressedDisplayPosition
        {
            get
            {
                if (BiographyHistoryButton == true && Switch28 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyHistoryButtonClient.X + this.DisplayOffset.X, this.BiographyHistoryButtonClient.Y + this.DisplayOffset.Y, this.BiographyHistoryButtonClient.Width, this.BiographyHistoryButtonClient.Height);
                }
                return new Rectangle(this.BiographyHistoryButtonClient.X + this.DisplayOffset.X, this.BiographyHistoryButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyInGameButtonDisplayPosition
        {
            get
            {
                if (BiographyInGameButton == false && Switch29 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyInGameButtonClient.X + this.DisplayOffset.X, this.BiographyInGameButtonClient.Y + this.DisplayOffset.Y, this.BiographyInGameButtonClient.Width, this.BiographyInGameButtonClient.Height);
                }
                return new Rectangle(this.BiographyInGameButtonClient.X + this.DisplayOffset.X, this.BiographyInGameButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle BiographyInGameButtonPressedDisplayPosition
        {
            get
            {
                if (BiographyInGameButton == true && Switch29 == "on" && Switch24 == "on")
                {
                    return new Rectangle(this.BiographyInGameButtonClient.X + this.DisplayOffset.X, this.BiographyInGameButtonClient.Y + this.DisplayOffset.Y, this.BiographyInGameButtonClient.Width, this.BiographyInGameButtonClient.Height);
                }
                return new Rectangle(this.BiographyInGameButtonClient.X + this.DisplayOffset.X, this.BiographyInGameButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }      
        //
         private Rectangle RelativeButtonDisplayPosition
        {
            get
            {
                if (RelativeButton == false)
                {
                    return new Rectangle(this.RelativeButtonClient.X + this.DisplayOffset.X, this.RelativeButtonClient.Y + this.DisplayOffset.Y, this.RelativeButtonClient.Width, this.RelativeButtonClient.Height);
                }
                return new Rectangle(this.RelativeButtonClient.X + this.DisplayOffset.X, this.RelativeButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle RelativeButtonPressedDisplayPosition
        {
            get
            {
                if (RelativeButton == true)
                {
                    return new Rectangle(this.RelativeButtonClient.X + this.DisplayOffset.X, this.RelativeButtonClient.Y + this.DisplayOffset.Y, this.RelativeButtonClient.Width, this.RelativeButtonClient.Height);
                }
                return new Rectangle(this.RelativeButtonClient.X + this.DisplayOffset.X, this.RelativeButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle RelationButtonDisplayPosition
        {
            get
            {
                if (RelationButton == false)
                {
                    return new Rectangle(this.RelationButtonClient.X + this.DisplayOffset.X, this.RelationButtonClient.Y + this.DisplayOffset.Y, this.RelationButtonClient.Width, this.RelationButtonClient.Height);
                }
                return new Rectangle(this.RelationButtonClient.X + this.DisplayOffset.X, this.RelationButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle RelationButtonPressedDisplayPosition
        {
            get
            {
                if (RelationButton == true)
                {
                    return new Rectangle(this.RelationButtonClient.X + this.DisplayOffset.X, this.RelationButtonClient.Y + this.DisplayOffset.Y, this.RelationButtonClient.Width, this.RelationButtonClient.Height);
                }
                return new Rectangle(this.RelationButtonClient.X + this.DisplayOffset.X, this.RelationButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle PersonRelationButtonDisplayPosition
        {
            get
            {
                if (PersonRelationButton == false)
                {
                    return new Rectangle(this.PersonRelationButtonClient.X + this.DisplayOffset.X, this.PersonRelationButtonClient.Y + this.DisplayOffset.Y, this.PersonRelationButtonClient.Width, this.PersonRelationButtonClient.Height);
                }
                return new Rectangle(this.PersonRelationButtonClient.X + this.DisplayOffset.X, this.PersonRelationButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle PersonRelationButtonPressedDisplayPosition
        {
            get
            {
                if (PersonRelationButton == true)
                {
                    return new Rectangle(this.PersonRelationButtonClient.X + this.DisplayOffset.X, this.PersonRelationButtonClient.Y + this.DisplayOffset.Y, this.PersonRelationButtonClient.Width, this.PersonRelationButtonClient.Height);
                }
                return new Rectangle(this.PersonRelationButtonClient.X + this.DisplayOffset.X, this.PersonRelationButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle StandingsButtonDisplayPosition
        {
            get
            {
                if (StandingsButton == false)
                {
                    return new Rectangle(this.StandingsButtonClient.X + this.DisplayOffset.X, this.StandingsButtonClient.Y + this.DisplayOffset.Y, this.StandingsButtonClient.Width, this.StandingsButtonClient.Height);
                }
                return new Rectangle(this.StandingsButtonClient.X + this.DisplayOffset.X, this.StandingsButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle StandingsButtonPressedDisplayPosition
        {
            get
            {
                if (StandingsButton == true)
                {
                    return new Rectangle(this.StandingsButtonClient.X + this.DisplayOffset.X, this.StandingsButtonClient.Y + this.DisplayOffset.Y, this.StandingsButtonClient.Width, this.StandingsButtonClient.Height);
                }
                return new Rectangle(this.StandingsButtonClient.X + this.DisplayOffset.X, this.StandingsButtonClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
        private Rectangle RelativeBackgroundDisplayPosition
        {
            get
            {
                if (RelativeButton == true)
                {
                    return new Rectangle(this.RelativeBackgroundClient.X + this.DisplayOffset.X, this.RelativeBackgroundClient.Y + this.DisplayOffset.Y, this.RelativeBackgroundClient.Width, this.RelativeBackgroundClient.Height);
                }
                return new Rectangle(this.RelativeBackgroundClient.X + this.DisplayOffset.X, this.RelativeBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle RelationBackgroundDisplayPosition
        {
            get
            {
                if (RelationButton == true)
                {
                    return new Rectangle(this.RelationBackgroundClient.X + this.DisplayOffset.X, this.RelationBackgroundClient.Y + this.DisplayOffset.Y, this.RelationBackgroundClient.Width, this.RelationBackgroundClient.Height);
                }
                return new Rectangle(this.RelationBackgroundClient.X + this.DisplayOffset.X, this.RelationBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle PersonRelationBackgroundDisplayPosition
        {
            get
            {
                if (PersonRelationButton == true)
                {
                    return new Rectangle(this.PersonRelationBackgroundClient.X + this.DisplayOffset.X, this.PersonRelationBackgroundClient.Y + this.DisplayOffset.Y, this.PersonRelationBackgroundClient.Width, this.PersonRelationBackgroundClient.Height);
                }
                return new Rectangle(this.PersonRelationBackgroundClient.X + this.DisplayOffset.X, this.PersonRelationBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle StandingsBackgroundDisplayPosition
        {
            get
            {
                if (StandingsButton == true)
                {
                    return new Rectangle(this.StandingsBackgroundClient.X + this.DisplayOffset.X, this.StandingsBackgroundClient.Y + this.DisplayOffset.Y, this.StandingsBackgroundClient.Width, this.StandingsBackgroundClient.Height);
                }
                return new Rectangle(this.StandingsBackgroundClient.X + this.DisplayOffset.X, this.StandingsBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        //
        private Rectangle SmallPortraitDisplayPosition
        {
            get
            {
                    return new Rectangle(this.SmallPortraitClient.X + this.DisplayOffset.X, this.SmallPortraitClient.Y + this.DisplayOffset.Y, this.SmallPortraitClient.Width, this.SmallPortraitClient.Height);
               
            }
        }
        private Rectangle FatherSmallPortraitDisplayPosition
        {
            get
            {
                    return new Rectangle(this.FatherSmallPortraitClient.X + this.DisplayOffset.X, this.FatherSmallPortraitClient.Y + this.DisplayOffset.Y, this.FatherSmallPortraitClient.Width, this.FatherSmallPortraitClient.Height);
             
            }
        }
        private Rectangle MotherSmallPortraitDisplayPosition
        {
            get
            {
                    return new Rectangle(this.MotherSmallPortraitClient.X + this.DisplayOffset.X, this.MotherSmallPortraitClient.Y + this.DisplayOffset.Y, this.MotherSmallPortraitClient.Width, this.MotherSmallPortraitClient.Height);
             
            }
        }
        private Rectangle SpouseSmallPortraitDisplayPosition
        {
            get
            {
                    return new Rectangle(this.SpouseSmallPortraitClient.X + this.DisplayOffset.X, this.SpouseSmallPortraitClient.Y + this.DisplayOffset.Y, this.SpouseSmallPortraitClient.Width, this.SpouseSmallPortraitClient.Height);
            }
        }        
        private Rectangle PersonBiographyDisplayPosition
        {
            get
            {
                return new Rectangle(this.PersonBiographyText.DisplayOffset.X, this.PersonBiographyText.DisplayOffset.Y, this.PersonBiographyText.ClientWidth, this.PersonBiographyText.ClientHeight);
            }
        }
        private Rectangle ChildrenDisplayPosition
        {
            get
            {
                return new Rectangle(this.ChildrenText.DisplayOffset.X, this.ChildrenText.DisplayOffset.Y, this.ChildrenText.ClientWidth, this.ChildrenText.ClientHeight);
            }
        }
        private Rectangle BrothersDisplayPosition
        {
            get
            {
                return new Rectangle(this.BrothersText.DisplayOffset.X, this.BrothersText.DisplayOffset.Y, this.BrothersText.ClientWidth, this.BrothersText.ClientHeight);
            }
        }
        private Rectangle ClosePersonsDisplayPosition
        {
            get
            {
                return new Rectangle(this.ClosePersonsText.DisplayOffset.X, this.ClosePersonsText.DisplayOffset.Y, this.ClosePersonsText.ClientWidth, this.ClosePersonsText.ClientHeight);
            }
        }
        private Rectangle HatedPersonsDisplayPosition
        {
            get
            {
                return new Rectangle(this.HatedPersonsText.DisplayOffset.X, this.HatedPersonsText.DisplayOffset.Y, this.HatedPersonsText.ClientWidth, this.HatedPersonsText.ClientHeight);
            }
        }
        private Rectangle PersonRelationDisplayPosition
        {
            get
            {
                return new Rectangle(this.PersonRelationText.DisplayOffset.X, this.PersonRelationText.DisplayOffset.Y, this.PersonRelationText.ClientWidth, this.PersonRelationText.ClientHeight);
            }
        }
       //
        private Rectangle ConditionBackgroundDisplayPosition
        {
            get
            {
                if (ConditionAndInfluenceText == true)
                {
                    return new Rectangle(this.ConditionBackgroundClient.X + this.DisplayOffset.X, this.ConditionBackgroundClient.Y + this.DisplayOffset.Y, this.ConditionBackgroundClient.Width, this.ConditionBackgroundClient.Height);
                }
                return new Rectangle(this.ConditionBackgroundClient.X + this.DisplayOffset.X, this.ConditionBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle InfluenceBackgroundDisplayPosition
        {
            get
            {
                if (ConditionAndInfluenceText == true)
                {
                    return new Rectangle(this.InfluenceBackgroundClient.X + this.DisplayOffset.X, this.InfluenceBackgroundClient.Y + this.DisplayOffset.Y, this.InfluenceBackgroundClient.Width, this.InfluenceBackgroundClient.Height);
                }
                return new Rectangle(this.InfluenceBackgroundClient.X + this.DisplayOffset.X, this.InfluenceBackgroundClient.Y + this.DisplayOffset.Y, 0, 0);
            }
        }
        private Rectangle TitleCountDisplayPosition
        {
            get
            {
                return new Rectangle(0, 0, this.TitleTextBackgroundClient.Width, this.TitleCountN*this.TitleTextHeight);
               
            }
        }
        private Rectangle StuntCountDisplayPosition
        {
            get
            {
                return new Rectangle(0, 0, this.StuntTextBackgroundClient.Width, this.StuntCountN * this.StuntTextHeight);
               
            }
        }
        private Rectangle TitleTextBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.TitleTextBackgroundClient.X + this.DisplayOffset.X, this.TitleTextBackgroundClient.Y + this.DisplayOffset.Y, this.TitleTextBackgroundClient.Width, this.TitleCountN * this.TitleTextHeight);

            }
        }
        private Rectangle StuntTextBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.StuntTextBackgroundClient.X + this.DisplayOffset.X, this.StuntTextBackgroundClient.Y + this.DisplayOffset.Y, this.StuntTextBackgroundClient.Width, this.StuntCountN * this.StuntTextHeight);
                
            }
        }

        private bool IsTheEffectiveTreasure(int i)
        {
            int ID = 0;
            int n = 1;
            foreach (Treasure t in this.ShowingPerson.Treasures)
            {
                if (n == i)
                {
                    ID = t.ID;
                    break;
                }
                n++;
            }
            bool b = false;
            foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
            {
                if (t.ID == ID)
                {
                    b = true;
                }
            }
            return b;
        }
        private bool IsTheNoEffectiveTreasure(int i)
        {
            bool b = true;
            foreach (Treasure t in this.ShowingPerson.effectiveTreasures.Values)
            {
                if (t.ID == i)
                {
                    b = false;
                }
            }
            return b;
        }
        //
       



        ////////以上新增
    }
}

