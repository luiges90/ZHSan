using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.ArchitectureDetail;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Platforms;
using GameManager;

namespace ArchitectureDetail
{
    public class ArchitectureDetail
    {
        public Vector2 Scale = new Vector2(0.94f, 0.94f);

        internal Point BackgroundSize;
        internal PlatformTexture BackgroundTexture;
        internal PlatformTexture PictureNull;
        internal Rectangle CharacteristicClient;
        internal FreeRichText CharacteristicText = new FreeRichText();
        private Point DisplayOffset;
        internal Rectangle FacilityClient;
        internal FreeRichText FacilityText = new FreeRichText();
        private bool isShowing;
        internal List<LabelText> LabelTexts = new List<LabelText>();

        internal Architecture ShowingArchitecture;
        /////以下添加
        ////↓开关
        internal string Switch1;//UI类型
         
        internal string Switch3;//界面音效

        internal string Switch21;//建筑信息
        internal string Switch22;//信息进度条
        internal string Switch23;//特色图片
        internal string Switch24;//建筑特色Text
        internal string Switch25;//建筑设施Text
        internal string Switch26;//指定建筑种类或建筑图片
 
        internal string Switch41;//建筑设施        
        internal string Switch42;//建筑信息Text
        internal string Switch43;//建筑种类设施页
        internal string Switch44;//建筑特色设施页
        internal string Switch45;//设施图片B
        internal string Switch46;//设施位置图片
        internal string Switch47;//建筑设施Text
        ////↓建筑类型图片
        internal int ArchitectureKind;        
        internal int ArchitectureID;
       
        ////↓界面按钮
        internal PlatformTexture InformationButtonTexture;
        internal PlatformTexture InformationPressedTexture;
        internal Rectangle InformationButtonClient;
       
        internal PlatformTexture FacilityButtonTexture;
        internal PlatformTexture FacilityPressedTexture;
        internal Rectangle FacilityButtonClient;

        ////↓建筑信息页       
        //↓背景图
        internal PlatformTexture InformationMask1;
        internal PlatformTexture InformationMask2;
        internal PlatformTexture InformationBackground;
        internal Rectangle InformationBackgroundClient;
        //↓文字信息
        internal List<LabelText> ArchitectureInInformationTexts = new List<LabelText>();
        //↓建筑特色Text
        internal Rectangle TheCharacteristic1Client;
        internal FreeRichText TheCharacteristic1Text = new FreeRichText();
        //↓建筑设施Text
        internal Rectangle TheFacility1Client;
        internal FreeRichText TheFacility1Text = new FreeRichText();
        //↓进度条
        private int Integration;
        private int AllIntegration;
        internal PlatformTexture IntegrationBarTexture;
        internal PlatformTexture Integration1BarTexture;
        internal PlatformTexture Integration2BarTexture;
        internal PlatformTexture Integration3BarTexture;
        internal PlatformTexture Integration4BarTexture;
        internal PlatformTexture Integration5BarTexture;
        internal PlatformTexture Integration6BarTexture;
        internal Rectangle IntegrationBar1Client;

        internal PlatformTexture DominationBarTexture;
        internal PlatformTexture Domination1BarTexture;
        internal PlatformTexture Domination2BarTexture;
        internal PlatformTexture Domination3BarTexture;
        internal PlatformTexture Domination4BarTexture;
        internal PlatformTexture Domination5BarTexture;
        internal PlatformTexture Domination6BarTexture;
        internal Rectangle DominationBar1Client;

        internal PlatformTexture EnduranceBarTexture;
        internal PlatformTexture Endurance1BarTexture;
        internal PlatformTexture Endurance2BarTexture;
        internal PlatformTexture Endurance3BarTexture;
        internal PlatformTexture Endurance4BarTexture;
        internal PlatformTexture Endurance5BarTexture;
        internal PlatformTexture Endurance6BarTexture;
        internal Rectangle EnduranceBar1Client;

        internal PlatformTexture AgricultureBarTexture;
        internal PlatformTexture Agriculture1BarTexture;
        internal PlatformTexture Agriculture2BarTexture;
        internal PlatformTexture Agriculture3BarTexture;
        internal PlatformTexture Agriculture4BarTexture;
        internal PlatformTexture Agriculture5BarTexture;
        internal PlatformTexture Agriculture6BarTexture;
        internal Rectangle AgricultureBar1Client;

        internal PlatformTexture CommerceBarTexture;
        internal PlatformTexture Commerce1BarTexture;
        internal PlatformTexture Commerce2BarTexture;
        internal PlatformTexture Commerce3BarTexture;
        internal PlatformTexture Commerce4BarTexture;
        internal PlatformTexture Commerce5BarTexture;
        internal PlatformTexture Commerce6BarTexture;
        internal Rectangle CommerceBar1Client;

        internal PlatformTexture TechnologyBarTexture;
        internal PlatformTexture Technology1BarTexture;
        internal PlatformTexture Technology2BarTexture;
        internal PlatformTexture Technology3BarTexture;
        internal PlatformTexture Technology4BarTexture;
        internal PlatformTexture Technology5BarTexture;
        internal PlatformTexture Technology6BarTexture;
        internal Rectangle TechnologyBar1Client;

        internal PlatformTexture MoraleBarTexture;
        internal PlatformTexture Morale1BarTexture;
        internal PlatformTexture Morale2BarTexture;
        internal PlatformTexture Morale3BarTexture;
        internal PlatformTexture Morale4BarTexture;
        internal PlatformTexture Morale5BarTexture;
        internal PlatformTexture Morale6BarTexture;
        internal Rectangle MoraleBar1Client;

        internal PlatformTexture FacilityCountBarTexture;
        internal PlatformTexture FacilityCount1BarTexture;
        internal PlatformTexture FacilityCount2BarTexture;
        internal PlatformTexture FacilityCount3BarTexture;
        internal PlatformTexture FacilityCount4BarTexture;
        internal PlatformTexture FacilityCount5BarTexture;
        internal PlatformTexture FacilityCount6BarTexture;
        internal Rectangle FacilityCountBar1Client;
        //↓Kind&ID图片
        internal string PictureShowforAKind;
        internal string PictureShowforAID;
        internal bool HasPictureforAKind
        {
            get
            {
                return true;
                //if (File.Exists(@"GameComponents\ArchitectureDetail\Data\TheInformationPage\ArchitectureKindPicture\" + this.ArchitectureKind.ToString() + ".png"))
                //{ return true; }
                //else
                //{ return false; }
            }
        }
        internal PlatformTexture PictureforAKind
        {
            get
            {
                if (HasPictureforAKind == true)
                { return this.ThePictureforAKind; }
                else
                { return this.PictureNull; }
            }
        }
        internal PlatformTexture ThePictureforAKind;
        internal Rectangle PictureforAKindClient;
        internal bool HasPictureforAID
        {
            get
            {
                return true;
                //if (File.Exists(@"GameComponents\ArchitectureDetail\Data\TheInformationPage\ArchitecturePicture\" + this.ArchitectureID.ToString() + ".png"))
                //{ return true; }
                //else
                //{ return false; }
            }
        }
        internal PlatformTexture PictureforAID
        {
            get
            {
                if (HasPictureforAID == true)
                { return this.ThePictureforAID; }
                else
                { return this.PictureNull; }
            }
        }
        internal PlatformTexture ThePictureforAID;
        internal Rectangle PictureforAIDClient;
        //↓特色图片        
        internal string ShowNullCharacteristicPicture;
        private int TheCharacteristicCount;
        private int TheMaxCharacteristicCount;
        private int TheMaxShowCharacteristicCount;
        internal PlatformTexture CharacteristicShowMask;
        internal PlatformTexture CharacteristicShowBackground;
        internal Rectangle CharacteristicShowBackgroundClient;
        internal int TheCharacteristicShowID1;
        internal int TheCharacteristicShowID2;
        internal int TheCharacteristicShowID3;
        internal int TheCharacteristicShowID4;
        internal int TheCharacteristicShowID5;
        internal int TheCharacteristicShowID6;
        internal int TheCharacteristicShowID7;
        internal int TheCharacteristicShowID8;
        internal int TheCharacteristicShowID9;
        internal int TheCharacteristicShowID10;
        internal int TheCharacteristicShowID11;
        internal int TheCharacteristicShowID12;
        internal int TheCharacteristicShowID13;
        internal int TheCharacteristicShowID14;
        internal int TheCharacteristicShowID15;
        internal int TheCharacteristicShowID16;
        internal int TheCharacteristicShowID17;
        internal int TheCharacteristicShowID18;
        internal int TheCharacteristicShowID19;
        internal int TheCharacteristicShowID20;
        internal int TheCharacteristicShowID21;
        internal int TheCharacteristicShowID22;
        internal int TheCharacteristicShowID23;
        internal int TheCharacteristicShowID24;
        internal int TheCharacteristicShowID25;
        internal int TheCharacteristicShowID26;
        internal int TheCharacteristicShowID27;
        internal int TheCharacteristicShowID28;
        internal int TheCharacteristicShowID29;
        internal int TheCharacteristicShowID30;
        internal PlatformTexture TheCharacteristicShow1;
        internal PlatformTexture TheCharacteristicShow2;
        internal PlatformTexture TheCharacteristicShow3;
        internal PlatformTexture TheCharacteristicShow4;
        internal PlatformTexture TheCharacteristicShow5;
        internal PlatformTexture TheCharacteristicShow6;
        internal PlatformTexture TheCharacteristicShow7;
        internal PlatformTexture TheCharacteristicShow8;
        internal PlatformTexture TheCharacteristicShow9;
        internal PlatformTexture TheCharacteristicShow10;
        internal PlatformTexture TheCharacteristicShow11;
        internal PlatformTexture TheCharacteristicShow12;
        internal PlatformTexture TheCharacteristicShow13;
        internal PlatformTexture TheCharacteristicShow14;
        internal PlatformTexture TheCharacteristicShow15;
        internal PlatformTexture TheCharacteristicShow16;
        internal PlatformTexture TheCharacteristicShow17;
        internal PlatformTexture TheCharacteristicShow18;
        internal PlatformTexture TheCharacteristicShow19;
        internal PlatformTexture TheCharacteristicShow20;
        internal PlatformTexture TheCharacteristicShow21;
        internal PlatformTexture TheCharacteristicShow22;
        internal PlatformTexture TheCharacteristicShow23;
        internal PlatformTexture TheCharacteristicShow24;
        internal PlatformTexture TheCharacteristicShow25;
        internal PlatformTexture TheCharacteristicShow26;
        internal PlatformTexture TheCharacteristicShow27;
        internal PlatformTexture TheCharacteristicShow28;
        internal PlatformTexture TheCharacteristicShow29;
        internal PlatformTexture TheCharacteristicShow30;
        internal PlatformTexture TheCharacteristicShowMask;
        internal PlatformTexture TheCharacteristicShowBackground;
        internal Rectangle TheCharacteristicShowClient;
        internal int TheCharacteristicShowXNumber;
        internal int TheCharacteristicShowYNumber;
        internal int TheCharacteristicShowXSpacing;
        internal int TheCharacteristicShowYSpacing;   
 
        ////↓建筑设施页
        private int TheFacilityPositionCount;
        private int TheAllFacilityNumber;
        internal PlatformTexture FacilityMask1;
        internal PlatformTexture FacilityMask2;
        internal PlatformTexture FacilityBackground;
        internal Rectangle FacilityBackgroundClient;
        internal int AllFacilityNumber;
        private string PageForFacilityPositionCount;
        private string PageForArchitectureKind;
        private string PageForArchitectureCharacteristic;
        //↓文字信息
        internal List<LabelText> ArchitectureInFacilityTexts = new List<LabelText>();
        //↓建筑设施Text
        internal Rectangle TheFacility3Client;
        internal FreeRichText TheFacility3Text = new FreeRichText();
        internal string ShowFacilityAllCount;
        internal string ShowFacilityCount;
        internal string ShowPositionOccupied;
        internal string ShowMaintenanceCost;
        internal string ShowFacilityDescription;
        internal string TheFacility3Text1;
        internal string TheFacility3Text2;
        internal string TheFacility3Text3;
        internal string TheFacility3Text4;
        internal string TheFacility3Text5;
        internal string TheFacility3Text6;
        internal string TheFacility3Text7;
        internal string TheFacility3Text8;
        internal string TheFacility3Text9;
        internal string TheFacility3Text10;
        internal string TheFacility3Text11;
        internal string TheFacility3Text12;
        internal string TheFacility3Text13;
        internal string TheFacility3Text14;
        internal string TheFacility3Text15;
        internal string TheFacility3Text16;
        internal string TheFacility3Text17;
        internal string TheFacility3Text18;
        internal string TheFacility3Text19;
        //↓页面条件
        internal int TheFirstFacilityPage;
        internal int PageForFacilityPositionCountNumber;
        internal int PageForFacilityPositionCount1;
        internal int PageForFacilityPositionCount2;
        internal int PageForFacilityPositionCount3;
        internal int PageForFacilityPositionCount4;
        internal int PageForFacilityPositionCount5;
        internal int PageForFacilityPositionCount6;
        internal int PageForFacilityPositionCount7;
        internal int PageForFacilityPositionCount8;
        internal int PageForFacilityPositionCount9;
        internal int PageForFacilityPositionCount10;
        internal int PageForArchitectureKindNumber;
        internal int PageForArchitectureKind1;
        internal int PageForArchitectureKind2;
        internal int PageForArchitectureKind3;
        internal int PageForArchitectureKind4;
        internal int PageForArchitectureKind5;
        internal int PageForArchitectureKind6;
        internal int PageForArchitectureKind7;
        internal int PageForArchitectureKind8;
        internal int PageForArchitectureKind9;
        internal int PageForArchitectureKind10;
        internal int PageForArchitectureCharacteristicNumber;
        internal int PageForArchitectureCharacteristic1;
        internal int PageForArchitectureCharacteristic2;
        internal int PageForArchitectureCharacteristic3;
        internal int PageForArchitectureCharacteristic4;
        internal int PageForArchitectureCharacteristic5;
        internal int PageForArchitectureCharacteristic6;
        internal int PageForArchitectureCharacteristic7;
        internal int PageForArchitectureCharacteristic8;
        internal int PageForArchitectureCharacteristic9;
        internal int PageForArchitectureCharacteristic10;
        //↓页面图片
        internal PlatformTexture PageForFacilityMask1;
        internal PlatformTexture PageForFacilityMask2;
        internal PlatformTexture PageForFacilityMask3;
        internal PlatformTexture PageForFacilityMask4;
        internal PlatformTexture PageForFacilityMask5;
        internal PlatformTexture PageForFacilityMask6;
        internal PlatformTexture PageForFacilityMask7;
        internal PlatformTexture PageForFacilityMask8;
        internal PlatformTexture PageForFacilityMask9;
        internal PlatformTexture PageForFacilityMask10;
        internal PlatformTexture PageForFacilityMask11;
        internal PlatformTexture PageForFacilityMask12;
        internal PlatformTexture PageForFacilityMask13;
        internal PlatformTexture PageForFacilityMask14;
        internal PlatformTexture PageForFacilityMask15;
        internal PlatformTexture PageForFacilityMask16;
        internal PlatformTexture PageForFacilityMask17;
        internal PlatformTexture PageForFacilityMask18;
        internal PlatformTexture PageForFacilityMask19;
        internal PlatformTexture PageForFacilityMask20;
        internal PlatformTexture PageForFacilityMask21;
        internal PlatformTexture PageForFacilityMask22;
        internal PlatformTexture PageForFacilityMask23;
        internal PlatformTexture PageForFacilityMask24;
        internal PlatformTexture PageForFacilityMask25;
        internal PlatformTexture PageForFacilityMask26;
        internal PlatformTexture PageForFacilityMask27;
        internal PlatformTexture PageForFacilityMask28;
        internal PlatformTexture PageForFacilityMask29;
        internal PlatformTexture PageForFacilityMask30;
        internal PlatformTexture PageForFacilityBackground1;
        internal PlatformTexture PageForFacilityBackground2;
        internal PlatformTexture PageForFacilityBackground3;
        internal PlatformTexture PageForFacilityBackground4;
        internal PlatformTexture PageForFacilityBackground5;
        internal PlatformTexture PageForFacilityBackground6;
        internal PlatformTexture PageForFacilityBackground7;
        internal PlatformTexture PageForFacilityBackground8;
        internal PlatformTexture PageForFacilityBackground9;
        internal PlatformTexture PageForFacilityBackground10;
        internal PlatformTexture PageForFacilityBackground11;
        internal PlatformTexture PageForFacilityBackground12;
        internal PlatformTexture PageForFacilityBackground13;
        internal PlatformTexture PageForFacilityBackground14;
        internal PlatformTexture PageForFacilityBackground15;
        internal PlatformTexture PageForFacilityBackground16;
        internal PlatformTexture PageForFacilityBackground17;
        internal PlatformTexture PageForFacilityBackground18;
        internal PlatformTexture PageForFacilityBackground19;
        internal PlatformTexture PageForFacilityBackground20;
        internal PlatformTexture PageForFacilityBackground21;
        internal PlatformTexture PageForFacilityBackground22;
        internal PlatformTexture PageForFacilityBackground23;
        internal PlatformTexture PageForFacilityBackground24;
        internal PlatformTexture PageForFacilityBackground25;
        internal PlatformTexture PageForFacilityBackground26;
        internal PlatformTexture PageForFacilityBackground27;
        internal PlatformTexture PageForFacilityBackground28;
        internal PlatformTexture PageForFacilityBackground29;
        internal PlatformTexture PageForFacilityBackground30;
        internal PlatformTexture PageForFacilityMask;
        internal PlatformTexture PageForFacilityBackground;
        internal Rectangle PageForFacilityBackgroundClient;
        //↓按钮
        internal PlatformTexture FacilityforPage1ButtonTexture;
        internal PlatformTexture FacilityforPage2ButtonTexture;
        internal PlatformTexture FacilityforPage3ButtonTexture;
        internal PlatformTexture FacilityforPage4ButtonTexture;
        internal PlatformTexture FacilityforPage5ButtonTexture;
        internal PlatformTexture FacilityforPage6ButtonTexture;
        internal PlatformTexture FacilityforPage7ButtonTexture;
        internal PlatformTexture FacilityforPage8ButtonTexture;
        internal PlatformTexture FacilityforPage9ButtonTexture;
        internal PlatformTexture FacilityforPage10ButtonTexture;
        internal PlatformTexture FacilityforPage11ButtonTexture;
        internal PlatformTexture FacilityforPage12ButtonTexture;
        internal PlatformTexture FacilityforPage13ButtonTexture;
        internal PlatformTexture FacilityforPage14ButtonTexture;
        internal PlatformTexture FacilityforPage15ButtonTexture;
        internal PlatformTexture FacilityforPage16ButtonTexture;
        internal PlatformTexture FacilityforPage17ButtonTexture;
        internal PlatformTexture FacilityforPage18ButtonTexture;
        internal PlatformTexture FacilityforPage19ButtonTexture;
        internal PlatformTexture FacilityforPage20ButtonTexture;
        internal PlatformTexture FacilityforPage21ButtonTexture;
        internal PlatformTexture FacilityforPage22ButtonTexture;
        internal PlatformTexture FacilityforPage23ButtonTexture;
        internal PlatformTexture FacilityforPage24ButtonTexture;
        internal PlatformTexture FacilityforPage25ButtonTexture;
        internal PlatformTexture FacilityforPage26ButtonTexture;
        internal PlatformTexture FacilityforPage27ButtonTexture;
        internal PlatformTexture FacilityforPage28ButtonTexture;
        internal PlatformTexture FacilityforPage29ButtonTexture;
        internal PlatformTexture FacilityforPage30ButtonTexture;

        internal PlatformTexture FacilityforPage1PressedTexture;
        internal PlatformTexture FacilityforPage2PressedTexture;
        internal PlatformTexture FacilityforPage3PressedTexture;
        internal PlatformTexture FacilityforPage4PressedTexture;
        internal PlatformTexture FacilityforPage5PressedTexture;
        internal PlatformTexture FacilityforPage6PressedTexture;
        internal PlatformTexture FacilityforPage7PressedTexture;
        internal PlatformTexture FacilityforPage8PressedTexture;
        internal PlatformTexture FacilityforPage9PressedTexture;
        internal PlatformTexture FacilityforPage10PressedTexture;
        internal PlatformTexture FacilityforPage11PressedTexture;
        internal PlatformTexture FacilityforPage12PressedTexture;
        internal PlatformTexture FacilityforPage13PressedTexture;
        internal PlatformTexture FacilityforPage14PressedTexture;
        internal PlatformTexture FacilityforPage15PressedTexture;
        internal PlatformTexture FacilityforPage16PressedTexture;
        internal PlatformTexture FacilityforPage17PressedTexture;
        internal PlatformTexture FacilityforPage18PressedTexture;
        internal PlatformTexture FacilityforPage19PressedTexture;
        internal PlatformTexture FacilityforPage20PressedTexture;
        internal PlatformTexture FacilityforPage21PressedTexture;
        internal PlatformTexture FacilityforPage22PressedTexture;
        internal PlatformTexture FacilityforPage23PressedTexture;
        internal PlatformTexture FacilityforPage24PressedTexture;
        internal PlatformTexture FacilityforPage25PressedTexture;
        internal PlatformTexture FacilityforPage26PressedTexture;
        internal PlatformTexture FacilityforPage27PressedTexture;
        internal PlatformTexture FacilityforPage28PressedTexture;
        internal PlatformTexture FacilityforPage29PressedTexture;
        internal PlatformTexture FacilityforPage30PressedTexture;

        internal Rectangle FacilityforPage1ButtonClient;
        internal Rectangle FacilityforPage2ButtonClient;
        internal Rectangle FacilityforPage3ButtonClient;
        internal Rectangle FacilityforPage4ButtonClient;
        internal Rectangle FacilityforPage5ButtonClient;
        internal Rectangle FacilityforPage6ButtonClient;
        internal Rectangle FacilityforPage7ButtonClient;
        internal Rectangle FacilityforPage8ButtonClient;
        internal Rectangle FacilityforPage9ButtonClient;
        internal Rectangle FacilityforPage10ButtonClient;
        internal Rectangle FacilityforPage11ButtonClient;
        internal Rectangle FacilityforPage12ButtonClient;
        internal Rectangle FacilityforPage13ButtonClient;
        internal Rectangle FacilityforPage14ButtonClient;
        internal Rectangle FacilityforPage15ButtonClient;
        internal Rectangle FacilityforPage16ButtonClient;
        internal Rectangle FacilityforPage17ButtonClient;
        internal Rectangle FacilityforPage18ButtonClient;
        internal Rectangle FacilityforPage19ButtonClient;
        internal Rectangle FacilityforPage20ButtonClient;
        internal Rectangle FacilityforPage21ButtonClient;
        internal Rectangle FacilityforPage22ButtonClient;
        internal Rectangle FacilityforPage23ButtonClient;
        internal Rectangle FacilityforPage24ButtonClient;
        internal Rectangle FacilityforPage25ButtonClient;
        internal Rectangle FacilityforPage26ButtonClient;
        internal Rectangle FacilityforPage27ButtonClient;
        internal Rectangle FacilityforPage28ButtonClient;
        internal Rectangle FacilityforPage29ButtonClient;
        internal Rectangle FacilityforPage30ButtonClient;
        //↓设施说明
        internal FreeRichText FacilityDescriptionText = new FreeRichText();
        internal Rectangle FacilityDescriptionTextClient;
        internal PlatformTexture FacilityDescriptionTextMask;
        internal PlatformTexture FacilityDescriptionTextBackground;
        internal Rectangle FacilityDescriptionTextBackgroundClient;
        internal string TheFacilityDescriptionTextFrom;
        internal string FacilityDescriptionTextFollowTheMouse;
        internal string FacilityDescriptionText1;
        internal string FacilityDescriptionText2;
        internal string FacilityDescriptionText3;
        internal string FacilityDescriptionText4;
        internal string FacilityDescriptionText5;
        internal string FacilityDescriptionText6;
        internal string FacilityDescriptionText7;
        internal string FacilityDescriptionText8;
        internal string FacilityDescriptionText9;
        internal string FacilityDescriptionText10;
        internal string FacilityDescriptionText11;
        internal string FacilityDescriptionText12;
        internal string FacilityDescriptionText13;
        internal string FacilityDescriptionText14;
        internal string FacilityDescriptionText15;
        internal string FacilityDescriptionText16;
        internal string FacilityDescriptionText17;
        internal string FacilityDescriptionText18;
        internal string FacilityDescriptionText19;

        private int TheFacilityIDForDescription;
        //↓在建设施
        internal PlatformTexture TheBuildingFacilityPictureA;
        internal PlatformTexture TheBuildingFacilityPictureB;
        internal PlatformTexture TheBuildingFacilityTextPicture;
        private int TheBuildingFacilityID;
        private string TheBuildingFacilityName;
        private string TheBuildingFacilityDay;
        private bool HasBuildingFacility;
        //↓设施
        internal string Facility1ID;
        internal string Facility2ID;
        internal string Facility3ID;
        internal string Facility4ID;
        internal string Facility5ID;
        internal string Facility6ID;
        internal string Facility7ID;
        internal string Facility8ID;
        internal string Facility9ID;
        internal string Facility10ID;
        internal string Facility11ID;
        internal string Facility12ID;
        internal string Facility13ID;
        internal string Facility14ID;
        internal string Facility15ID;
        internal string Facility16ID;
        internal string Facility17ID;
        internal string Facility18ID;
        internal string Facility19ID;
        internal string Facility20ID;
        internal string Facility21ID;
        internal string Facility22ID;
        internal string Facility23ID;
        internal string Facility24ID;
        internal string Facility25ID;
        internal string Facility26ID;
        internal string Facility27ID;
        internal string Facility28ID;
        internal string Facility29ID;
        internal string Facility30ID;
        internal string Facility31ID;
        internal string Facility32ID;
        internal string Facility33ID;
        internal string Facility34ID;
        internal string Facility35ID;
        internal string Facility36ID;
        internal string Facility37ID;
        internal string Facility38ID;
        internal string Facility39ID;
        internal string Facility40ID;
        internal string Facility41ID;
        internal string Facility42ID;
        internal string Facility43ID;
        internal string Facility44ID;
        internal string Facility45ID;
        internal string Facility46ID;
        internal string Facility47ID;
        internal string Facility48ID;
        internal string Facility49ID;
        internal string Facility50ID;
        internal string Facility51ID;
        internal string Facility52ID;
        internal string Facility53ID;
        internal string Facility54ID;
        internal string Facility55ID;
        internal string Facility56ID;
        internal string Facility57ID;
        internal string Facility58ID;
        internal string Facility59ID;
        internal string Facility60ID;
        internal string Facility61ID;
        internal string Facility62ID;
        internal string Facility63ID;
        internal string Facility64ID;
        internal string Facility65ID;
        internal string Facility66ID;
        internal string Facility67ID;
        internal string Facility68ID;
        internal string Facility69ID;
        internal string Facility70ID;
        internal string Facility71ID;
        internal string Facility72ID;
        internal string Facility73ID;
        internal string Facility74ID;
        internal string Facility75ID;
        internal string Facility76ID;
        internal string Facility77ID;
        internal string Facility78ID;
        internal string Facility79ID;
        internal string Facility80ID;
        internal string Facility81ID;
        internal string Facility82ID;
        internal string Facility83ID;
        internal string Facility84ID;
        internal string Facility85ID;
        internal string Facility86ID;
        internal string Facility87ID;
        internal string Facility88ID;
        internal string Facility89ID;
        internal string Facility90ID;
        internal string Facility91ID;
        internal string Facility92ID;
        internal string Facility93ID;
        internal string Facility94ID;
        internal string Facility95ID;
        internal string Facility96ID;
        internal string Facility97ID;
        internal string Facility98ID;
        internal string Facility99ID;
        internal string Facility100ID;
        internal string Facility101ID;
        internal string Facility102ID;
        internal string Facility103ID;
        internal string Facility104ID;
        internal string Facility105ID;
        internal string Facility106ID;
        internal string Facility107ID;
        internal string Facility108ID;
        internal string Facility109ID;
        internal string Facility110ID;
        internal string Facility111ID;
        internal string Facility112ID;
        internal string Facility113ID;
        internal string Facility114ID;
        internal string Facility115ID;
        internal string Facility116ID;
        internal string Facility117ID;
        internal string Facility118ID;
        internal string Facility119ID;
        internal string Facility120ID;
        internal string Facility121ID;
        internal string Facility122ID;
        internal string Facility123ID;
        internal string Facility124ID;
        internal string Facility125ID;
        internal string Facility126ID;
        internal string Facility127ID;
        internal string Facility128ID;
        internal string Facility129ID;
        internal string Facility130ID;
        internal string Facility131ID;
        internal string Facility132ID;
        internal string Facility133ID;
        internal string Facility134ID;
        internal string Facility135ID;
        internal string Facility136ID;
        internal string Facility137ID;
        internal string Facility138ID;
        internal string Facility139ID;
        internal string Facility140ID;
        internal string Facility141ID;
        internal string Facility142ID;
        internal string Facility143ID;
        internal string Facility144ID;
        internal string Facility145ID;
        internal string Facility146ID;
        internal string Facility147ID;
        internal string Facility148ID;
        internal string Facility149ID;
        internal string Facility150ID;
        internal string Facility151ID;
        internal string Facility152ID;
        internal string Facility153ID;
        internal string Facility154ID;
        internal string Facility155ID;
        internal string Facility156ID;
        internal string Facility157ID;
        internal string Facility158ID;
        internal string Facility159ID;
        internal string Facility160ID;
        internal string Facility161ID;
        internal string Facility162ID;
        internal string Facility163ID;
        internal string Facility164ID;
        internal string Facility165ID;
        internal string Facility166ID;
        internal string Facility167ID;
        internal string Facility168ID;
        internal string Facility169ID;
        internal string Facility170ID;
        internal string Facility171ID;
        internal string Facility172ID;
        internal string Facility173ID;
        internal string Facility174ID;
        internal string Facility175ID;
        internal string Facility176ID;
        internal string Facility177ID;
        internal string Facility178ID;
        internal string Facility179ID;
        internal string Facility180ID;
        internal string Facility181ID;
        internal string Facility182ID;
        internal string Facility183ID;
        internal string Facility184ID;
        internal string Facility185ID;
        internal string Facility186ID;
        internal string Facility187ID;
        internal string Facility188ID;
        internal string Facility189ID;
        internal string Facility190ID;
        internal string Facility191ID;
        internal string Facility192ID;
        internal string Facility193ID;
        internal string Facility194ID;
        internal string Facility195ID;
        internal string Facility196ID;
        internal string Facility197ID;
        internal string Facility198ID;
        internal string Facility199ID;
        internal string Facility200ID;
        internal string Facility1Name;
        internal string Facility2Name;
        internal string Facility3Name;
        internal string Facility4Name;
        internal string Facility5Name;
        internal string Facility6Name;
        internal string Facility7Name;
        internal string Facility8Name;
        internal string Facility9Name;
        internal string Facility10Name;
        internal string Facility11Name;
        internal string Facility12Name;
        internal string Facility13Name;
        internal string Facility14Name;
        internal string Facility15Name;
        internal string Facility16Name;
        internal string Facility17Name;
        internal string Facility18Name;
        internal string Facility19Name;
        internal string Facility20Name;
        internal string Facility21Name;
        internal string Facility22Name;
        internal string Facility23Name;
        internal string Facility24Name;
        internal string Facility25Name;
        internal string Facility26Name;
        internal string Facility27Name;
        internal string Facility28Name;
        internal string Facility29Name;
        internal string Facility30Name;
        internal string Facility31Name;
        internal string Facility32Name;
        internal string Facility33Name;
        internal string Facility34Name;
        internal string Facility35Name;
        internal string Facility36Name;
        internal string Facility37Name;
        internal string Facility38Name;
        internal string Facility39Name;
        internal string Facility40Name;
        internal string Facility41Name;
        internal string Facility42Name;
        internal string Facility43Name;
        internal string Facility44Name;
        internal string Facility45Name;
        internal string Facility46Name;
        internal string Facility47Name;
        internal string Facility48Name;
        internal string Facility49Name;
        internal string Facility50Name;
        internal string Facility51Name;
        internal string Facility52Name;
        internal string Facility53Name;
        internal string Facility54Name;
        internal string Facility55Name;
        internal string Facility56Name;
        internal string Facility57Name;
        internal string Facility58Name;
        internal string Facility59Name;
        internal string Facility60Name;
        internal string Facility61Name;
        internal string Facility62Name;
        internal string Facility63Name;
        internal string Facility64Name;
        internal string Facility65Name;
        internal string Facility66Name;
        internal string Facility67Name;
        internal string Facility68Name;
        internal string Facility69Name;
        internal string Facility70Name;
        internal string Facility71Name;
        internal string Facility72Name;
        internal string Facility73Name;
        internal string Facility74Name;
        internal string Facility75Name;
        internal string Facility76Name;
        internal string Facility77Name;
        internal string Facility78Name;
        internal string Facility79Name;
        internal string Facility80Name;
        internal string Facility81Name;
        internal string Facility82Name;
        internal string Facility83Name;
        internal string Facility84Name;
        internal string Facility85Name;
        internal string Facility86Name;
        internal string Facility87Name;
        internal string Facility88Name;
        internal string Facility89Name;
        internal string Facility90Name;
        internal string Facility91Name;
        internal string Facility92Name;
        internal string Facility93Name;
        internal string Facility94Name;
        internal string Facility95Name;
        internal string Facility96Name;
        internal string Facility97Name;
        internal string Facility98Name;
        internal string Facility99Name;
        internal string Facility100Name;
        internal string Facility101Name;
        internal string Facility102Name;
        internal string Facility103Name;
        internal string Facility104Name;
        internal string Facility105Name;
        internal string Facility106Name;
        internal string Facility107Name;
        internal string Facility108Name;
        internal string Facility109Name;
        internal string Facility110Name;
        internal string Facility111Name;
        internal string Facility112Name;
        internal string Facility113Name;
        internal string Facility114Name;
        internal string Facility115Name;
        internal string Facility116Name;
        internal string Facility117Name;
        internal string Facility118Name;
        internal string Facility119Name;
        internal string Facility120Name;
        internal string Facility121Name;
        internal string Facility122Name;
        internal string Facility123Name;
        internal string Facility124Name;
        internal string Facility125Name;
        internal string Facility126Name;
        internal string Facility127Name;
        internal string Facility128Name;
        internal string Facility129Name;
        internal string Facility130Name;
        internal string Facility131Name;
        internal string Facility132Name;
        internal string Facility133Name;
        internal string Facility134Name;
        internal string Facility135Name;
        internal string Facility136Name;
        internal string Facility137Name;
        internal string Facility138Name;
        internal string Facility139Name;
        internal string Facility140Name;
        internal string Facility141Name;
        internal string Facility142Name;
        internal string Facility143Name;
        internal string Facility144Name;
        internal string Facility145Name;
        internal string Facility146Name;
        internal string Facility147Name;
        internal string Facility148Name;
        internal string Facility149Name;
        internal string Facility150Name;
        internal string Facility151Name;
        internal string Facility152Name;
        internal string Facility153Name;
        internal string Facility154Name;
        internal string Facility155Name;
        internal string Facility156Name;
        internal string Facility157Name;
        internal string Facility158Name;
        internal string Facility159Name;
        internal string Facility160Name;
        internal string Facility161Name;
        internal string Facility162Name;
        internal string Facility163Name;
        internal string Facility164Name;
        internal string Facility165Name;
        internal string Facility166Name;
        internal string Facility167Name;
        internal string Facility168Name;
        internal string Facility169Name;
        internal string Facility170Name;
        internal string Facility171Name;
        internal string Facility172Name;
        internal string Facility173Name;
        internal string Facility174Name;
        internal string Facility175Name;
        internal string Facility176Name;
        internal string Facility177Name;
        internal string Facility178Name;
        internal string Facility179Name;
        internal string Facility180Name;
        internal string Facility181Name;
        internal string Facility182Name;
        internal string Facility183Name;
        internal string Facility184Name;
        internal string Facility185Name;
        internal string Facility186Name;
        internal string Facility187Name;
        internal string Facility188Name;
        internal string Facility189Name;
        internal string Facility190Name;
        internal string Facility191Name;
        internal string Facility192Name;
        internal string Facility193Name;
        internal string Facility194Name;
        internal string Facility195Name;
        internal string Facility196Name;
        internal string Facility197Name;
        internal string Facility198Name;
        internal string Facility199Name;
        internal string Facility200Name;
        internal string Facility1Description;
        internal string Facility2Description;
        internal string Facility3Description;
        internal string Facility4Description;
        internal string Facility5Description;
        internal string Facility6Description;
        internal string Facility7Description;
        internal string Facility8Description;
        internal string Facility9Description;
        internal string Facility10Description;
        internal string Facility11Description;
        internal string Facility12Description;
        internal string Facility13Description;
        internal string Facility14Description;
        internal string Facility15Description;
        internal string Facility16Description;
        internal string Facility17Description;
        internal string Facility18Description;
        internal string Facility19Description;
        internal string Facility20Description;
        internal string Facility21Description;
        internal string Facility22Description;
        internal string Facility23Description;
        internal string Facility24Description;
        internal string Facility25Description;
        internal string Facility26Description;
        internal string Facility27Description;
        internal string Facility28Description;
        internal string Facility29Description;
        internal string Facility30Description;
        internal string Facility31Description;
        internal string Facility32Description;
        internal string Facility33Description;
        internal string Facility34Description;
        internal string Facility35Description;
        internal string Facility36Description;
        internal string Facility37Description;
        internal string Facility38Description;
        internal string Facility39Description;
        internal string Facility40Description;
        internal string Facility41Description;
        internal string Facility42Description;
        internal string Facility43Description;
        internal string Facility44Description;
        internal string Facility45Description;
        internal string Facility46Description;
        internal string Facility47Description;
        internal string Facility48Description;
        internal string Facility49Description;
        internal string Facility50Description;
        internal string Facility51Description;
        internal string Facility52Description;
        internal string Facility53Description;
        internal string Facility54Description;
        internal string Facility55Description;
        internal string Facility56Description;
        internal string Facility57Description;
        internal string Facility58Description;
        internal string Facility59Description;
        internal string Facility60Description;
        internal string Facility61Description;
        internal string Facility62Description;
        internal string Facility63Description;
        internal string Facility64Description;
        internal string Facility65Description;
        internal string Facility66Description;
        internal string Facility67Description;
        internal string Facility68Description;
        internal string Facility69Description;
        internal string Facility70Description;
        internal string Facility71Description;
        internal string Facility72Description;
        internal string Facility73Description;
        internal string Facility74Description;
        internal string Facility75Description;
        internal string Facility76Description;
        internal string Facility77Description;
        internal string Facility78Description;
        internal string Facility79Description;
        internal string Facility80Description;
        internal string Facility81Description;
        internal string Facility82Description;
        internal string Facility83Description;
        internal string Facility84Description;
        internal string Facility85Description;
        internal string Facility86Description;
        internal string Facility87Description;
        internal string Facility88Description;
        internal string Facility89Description;
        internal string Facility90Description;
        internal string Facility91Description;
        internal string Facility92Description;
        internal string Facility93Description;
        internal string Facility94Description;
        internal string Facility95Description;
        internal string Facility96Description;
        internal string Facility97Description;
        internal string Facility98Description;
        internal string Facility99Description;
        internal string Facility100Description;
        internal string Facility101Description;
        internal string Facility102Description;
        internal string Facility103Description;
        internal string Facility104Description;
        internal string Facility105Description;
        internal string Facility106Description;
        internal string Facility107Description;
        internal string Facility108Description;
        internal string Facility109Description;
        internal string Facility110Description;
        internal string Facility111Description;
        internal string Facility112Description;
        internal string Facility113Description;
        internal string Facility114Description;
        internal string Facility115Description;
        internal string Facility116Description;
        internal string Facility117Description;
        internal string Facility118Description;
        internal string Facility119Description;
        internal string Facility120Description;
        internal string Facility121Description;
        internal string Facility122Description;
        internal string Facility123Description;
        internal string Facility124Description;
        internal string Facility125Description;
        internal string Facility126Description;
        internal string Facility127Description;
        internal string Facility128Description;
        internal string Facility129Description;
        internal string Facility130Description;
        internal string Facility131Description;
        internal string Facility132Description;
        internal string Facility133Description;
        internal string Facility134Description;
        internal string Facility135Description;
        internal string Facility136Description;
        internal string Facility137Description;
        internal string Facility138Description;
        internal string Facility139Description;
        internal string Facility140Description;
        internal string Facility141Description;
        internal string Facility142Description;
        internal string Facility143Description;
        internal string Facility144Description;
        internal string Facility145Description;
        internal string Facility146Description;
        internal string Facility147Description;
        internal string Facility148Description;
        internal string Facility149Description;
        internal string Facility150Description;
        internal string Facility151Description;
        internal string Facility152Description;
        internal string Facility153Description;
        internal string Facility154Description;
        internal string Facility155Description;
        internal string Facility156Description;
        internal string Facility157Description;
        internal string Facility158Description;
        internal string Facility159Description;
        internal string Facility160Description;
        internal string Facility161Description;
        internal string Facility162Description;
        internal string Facility163Description;
        internal string Facility164Description;
        internal string Facility165Description;
        internal string Facility166Description;
        internal string Facility167Description;
        internal string Facility168Description;
        internal string Facility169Description;
        internal string Facility170Description;
        internal string Facility171Description;
        internal string Facility172Description;
        internal string Facility173Description;
        internal string Facility174Description;
        internal string Facility175Description;
        internal string Facility176Description;
        internal string Facility177Description;
        internal string Facility178Description;
        internal string Facility179Description;
        internal string Facility180Description;
        internal string Facility181Description;
        internal string Facility182Description;
        internal string Facility183Description;
        internal string Facility184Description;
        internal string Facility185Description;
        internal string Facility186Description;
        internal string Facility187Description;
        internal string Facility188Description;
        internal string Facility189Description;
        internal string Facility190Description;
        internal string Facility191Description;
        internal string Facility192Description;
        internal string Facility193Description;
        internal string Facility194Description;
        internal string Facility195Description;
        internal string Facility196Description;
        internal string Facility197Description;
        internal string Facility198Description;
        internal string Facility199Description;
        internal string Facility200Description;
        internal string Facility1Page;
        internal string Facility2Page;
        internal string Facility3Page;
        internal string Facility4Page;
        internal string Facility5Page;
        internal string Facility6Page;
        internal string Facility7Page;
        internal string Facility8Page;
        internal string Facility9Page;
        internal string Facility10Page;
        internal string Facility11Page;
        internal string Facility12Page;
        internal string Facility13Page;
        internal string Facility14Page;
        internal string Facility15Page;
        internal string Facility16Page;
        internal string Facility17Page;
        internal string Facility18Page;
        internal string Facility19Page;
        internal string Facility20Page;
        internal string Facility21Page;
        internal string Facility22Page;
        internal string Facility23Page;
        internal string Facility24Page;
        internal string Facility25Page;
        internal string Facility26Page;
        internal string Facility27Page;
        internal string Facility28Page;
        internal string Facility29Page;
        internal string Facility30Page;
        internal string Facility31Page;
        internal string Facility32Page;
        internal string Facility33Page;
        internal string Facility34Page;
        internal string Facility35Page;
        internal string Facility36Page;
        internal string Facility37Page;
        internal string Facility38Page;
        internal string Facility39Page;
        internal string Facility40Page;
        internal string Facility41Page;
        internal string Facility42Page;
        internal string Facility43Page;
        internal string Facility44Page;
        internal string Facility45Page;
        internal string Facility46Page;
        internal string Facility47Page;
        internal string Facility48Page;
        internal string Facility49Page;
        internal string Facility50Page;
        internal string Facility51Page;
        internal string Facility52Page;
        internal string Facility53Page;
        internal string Facility54Page;
        internal string Facility55Page;
        internal string Facility56Page;
        internal string Facility57Page;
        internal string Facility58Page;
        internal string Facility59Page;
        internal string Facility60Page;
        internal string Facility61Page;
        internal string Facility62Page;
        internal string Facility63Page;
        internal string Facility64Page;
        internal string Facility65Page;
        internal string Facility66Page;
        internal string Facility67Page;
        internal string Facility68Page;
        internal string Facility69Page;
        internal string Facility70Page;
        internal string Facility71Page;
        internal string Facility72Page;
        internal string Facility73Page;
        internal string Facility74Page;
        internal string Facility75Page;
        internal string Facility76Page;
        internal string Facility77Page;
        internal string Facility78Page;
        internal string Facility79Page;
        internal string Facility80Page;
        internal string Facility81Page;
        internal string Facility82Page;
        internal string Facility83Page;
        internal string Facility84Page;
        internal string Facility85Page;
        internal string Facility86Page;
        internal string Facility87Page;
        internal string Facility88Page;
        internal string Facility89Page;
        internal string Facility90Page;
        internal string Facility91Page;
        internal string Facility92Page;
        internal string Facility93Page;
        internal string Facility94Page;
        internal string Facility95Page;
        internal string Facility96Page;
        internal string Facility97Page;
        internal string Facility98Page;
        internal string Facility99Page;
        internal string Facility100Page;
        internal string Facility101Page;
        internal string Facility102Page;
        internal string Facility103Page;
        internal string Facility104Page;
        internal string Facility105Page;
        internal string Facility106Page;
        internal string Facility107Page;
        internal string Facility108Page;
        internal string Facility109Page;
        internal string Facility110Page;
        internal string Facility111Page;
        internal string Facility112Page;
        internal string Facility113Page;
        internal string Facility114Page;
        internal string Facility115Page;
        internal string Facility116Page;
        internal string Facility117Page;
        internal string Facility118Page;
        internal string Facility119Page;
        internal string Facility120Page;
        internal string Facility121Page;
        internal string Facility122Page;
        internal string Facility123Page;
        internal string Facility124Page;
        internal string Facility125Page;
        internal string Facility126Page;
        internal string Facility127Page;
        internal string Facility128Page;
        internal string Facility129Page;
        internal string Facility130Page;
        internal string Facility131Page;
        internal string Facility132Page;
        internal string Facility133Page;
        internal string Facility134Page;
        internal string Facility135Page;
        internal string Facility136Page;
        internal string Facility137Page;
        internal string Facility138Page;
        internal string Facility139Page;
        internal string Facility140Page;
        internal string Facility141Page;
        internal string Facility142Page;
        internal string Facility143Page;
        internal string Facility144Page;
        internal string Facility145Page;
        internal string Facility146Page;
        internal string Facility147Page;
        internal string Facility148Page;
        internal string Facility149Page;
        internal string Facility150Page;
        internal string Facility151Page;
        internal string Facility152Page;
        internal string Facility153Page;
        internal string Facility154Page;
        internal string Facility155Page;
        internal string Facility156Page;
        internal string Facility157Page;
        internal string Facility158Page;
        internal string Facility159Page;
        internal string Facility160Page;
        internal string Facility161Page;
        internal string Facility162Page;
        internal string Facility163Page;
        internal string Facility164Page;
        internal string Facility165Page;
        internal string Facility166Page;
        internal string Facility167Page;
        internal string Facility168Page;
        internal string Facility169Page;
        internal string Facility170Page;
        internal string Facility171Page;
        internal string Facility172Page;
        internal string Facility173Page;
        internal string Facility174Page;
        internal string Facility175Page;
        internal string Facility176Page;
        internal string Facility177Page;
        internal string Facility178Page;
        internal string Facility179Page;
        internal string Facility180Page;
        internal string Facility181Page;
        internal string Facility182Page;
        internal string Facility183Page;
        internal string Facility184Page;
        internal string Facility185Page;
        internal string Facility186Page;
        internal string Facility187Page;
        internal string Facility188Page;
        internal string Facility189Page;
        internal string Facility190Page;
        internal string Facility191Page;
        internal string Facility192Page;
        internal string Facility193Page;
        internal string Facility194Page;
        internal string Facility195Page;
        internal string Facility196Page;
        internal string Facility197Page;
        internal string Facility198Page;
        internal string Facility199Page;
        internal string Facility200Page;
        internal PlatformTexture Facility1PictureA;
        internal PlatformTexture Facility2PictureA;
        internal PlatformTexture Facility3PictureA;
        internal PlatformTexture Facility4PictureA;
        internal PlatformTexture Facility5PictureA;
        internal PlatformTexture Facility6PictureA;
        internal PlatformTexture Facility7PictureA;
        internal PlatformTexture Facility8PictureA;
        internal PlatformTexture Facility9PictureA;
        internal PlatformTexture Facility10PictureA;
        internal PlatformTexture Facility11PictureA;
        internal PlatformTexture Facility12PictureA;
        internal PlatformTexture Facility13PictureA;
        internal PlatformTexture Facility14PictureA;
        internal PlatformTexture Facility15PictureA;
        internal PlatformTexture Facility16PictureA;
        internal PlatformTexture Facility17PictureA;
        internal PlatformTexture Facility18PictureA;
        internal PlatformTexture Facility19PictureA;
        internal PlatformTexture Facility20PictureA;
        internal PlatformTexture Facility21PictureA;
        internal PlatformTexture Facility22PictureA;
        internal PlatformTexture Facility23PictureA;
        internal PlatformTexture Facility24PictureA;
        internal PlatformTexture Facility25PictureA;
        internal PlatformTexture Facility26PictureA;
        internal PlatformTexture Facility27PictureA;
        internal PlatformTexture Facility28PictureA;
        internal PlatformTexture Facility29PictureA;
        internal PlatformTexture Facility30PictureA;
        internal PlatformTexture Facility31PictureA;
        internal PlatformTexture Facility32PictureA;
        internal PlatformTexture Facility33PictureA;
        internal PlatformTexture Facility34PictureA;
        internal PlatformTexture Facility35PictureA;
        internal PlatformTexture Facility36PictureA;
        internal PlatformTexture Facility37PictureA;
        internal PlatformTexture Facility38PictureA;
        internal PlatformTexture Facility39PictureA;
        internal PlatformTexture Facility40PictureA;
        internal PlatformTexture Facility41PictureA;
        internal PlatformTexture Facility42PictureA;
        internal PlatformTexture Facility43PictureA;
        internal PlatformTexture Facility44PictureA;
        internal PlatformTexture Facility45PictureA;
        internal PlatformTexture Facility46PictureA;
        internal PlatformTexture Facility47PictureA;
        internal PlatformTexture Facility48PictureA;
        internal PlatformTexture Facility49PictureA;
        internal PlatformTexture Facility50PictureA;
        internal PlatformTexture Facility51PictureA;
        internal PlatformTexture Facility52PictureA;
        internal PlatformTexture Facility53PictureA;
        internal PlatformTexture Facility54PictureA;
        internal PlatformTexture Facility55PictureA;
        internal PlatformTexture Facility56PictureA;
        internal PlatformTexture Facility57PictureA;
        internal PlatformTexture Facility58PictureA;
        internal PlatformTexture Facility59PictureA;
        internal PlatformTexture Facility60PictureA;
        internal PlatformTexture Facility61PictureA;
        internal PlatformTexture Facility62PictureA;
        internal PlatformTexture Facility63PictureA;
        internal PlatformTexture Facility64PictureA;
        internal PlatformTexture Facility65PictureA;
        internal PlatformTexture Facility66PictureA;
        internal PlatformTexture Facility67PictureA;
        internal PlatformTexture Facility68PictureA;
        internal PlatformTexture Facility69PictureA;
        internal PlatformTexture Facility70PictureA;
        internal PlatformTexture Facility71PictureA;
        internal PlatformTexture Facility72PictureA;
        internal PlatformTexture Facility73PictureA;
        internal PlatformTexture Facility74PictureA;
        internal PlatformTexture Facility75PictureA;
        internal PlatformTexture Facility76PictureA;
        internal PlatformTexture Facility77PictureA;
        internal PlatformTexture Facility78PictureA;
        internal PlatformTexture Facility79PictureA;
        internal PlatformTexture Facility80PictureA;
        internal PlatformTexture Facility81PictureA;
        internal PlatformTexture Facility82PictureA;
        internal PlatformTexture Facility83PictureA;
        internal PlatformTexture Facility84PictureA;
        internal PlatformTexture Facility85PictureA;
        internal PlatformTexture Facility86PictureA;
        internal PlatformTexture Facility87PictureA;
        internal PlatformTexture Facility88PictureA;
        internal PlatformTexture Facility89PictureA;
        internal PlatformTexture Facility90PictureA;
        internal PlatformTexture Facility91PictureA;
        internal PlatformTexture Facility92PictureA;
        internal PlatformTexture Facility93PictureA;
        internal PlatformTexture Facility94PictureA;
        internal PlatformTexture Facility95PictureA;
        internal PlatformTexture Facility96PictureA;
        internal PlatformTexture Facility97PictureA;
        internal PlatformTexture Facility98PictureA;
        internal PlatformTexture Facility99PictureA;
        internal PlatformTexture Facility100PictureA;
        internal PlatformTexture Facility101PictureA;
        internal PlatformTexture Facility102PictureA;
        internal PlatformTexture Facility103PictureA;
        internal PlatformTexture Facility104PictureA;
        internal PlatformTexture Facility105PictureA;
        internal PlatformTexture Facility106PictureA;
        internal PlatformTexture Facility107PictureA;
        internal PlatformTexture Facility108PictureA;
        internal PlatformTexture Facility109PictureA;
        internal PlatformTexture Facility110PictureA;
        internal PlatformTexture Facility111PictureA;
        internal PlatformTexture Facility112PictureA;
        internal PlatformTexture Facility113PictureA;
        internal PlatformTexture Facility114PictureA;
        internal PlatformTexture Facility115PictureA;
        internal PlatformTexture Facility116PictureA;
        internal PlatformTexture Facility117PictureA;
        internal PlatformTexture Facility118PictureA;
        internal PlatformTexture Facility119PictureA;
        internal PlatformTexture Facility120PictureA;
        internal PlatformTexture Facility121PictureA;
        internal PlatformTexture Facility122PictureA;
        internal PlatformTexture Facility123PictureA;
        internal PlatformTexture Facility124PictureA;
        internal PlatformTexture Facility125PictureA;
        internal PlatformTexture Facility126PictureA;
        internal PlatformTexture Facility127PictureA;
        internal PlatformTexture Facility128PictureA;
        internal PlatformTexture Facility129PictureA;
        internal PlatformTexture Facility130PictureA;
        internal PlatformTexture Facility131PictureA;
        internal PlatformTexture Facility132PictureA;
        internal PlatformTexture Facility133PictureA;
        internal PlatformTexture Facility134PictureA;
        internal PlatformTexture Facility135PictureA;
        internal PlatformTexture Facility136PictureA;
        internal PlatformTexture Facility137PictureA;
        internal PlatformTexture Facility138PictureA;
        internal PlatformTexture Facility139PictureA;
        internal PlatformTexture Facility140PictureA;
        internal PlatformTexture Facility141PictureA;
        internal PlatformTexture Facility142PictureA;
        internal PlatformTexture Facility143PictureA;
        internal PlatformTexture Facility144PictureA;
        internal PlatformTexture Facility145PictureA;
        internal PlatformTexture Facility146PictureA;
        internal PlatformTexture Facility147PictureA;
        internal PlatformTexture Facility148PictureA;
        internal PlatformTexture Facility149PictureA;
        internal PlatformTexture Facility150PictureA;
        internal PlatformTexture Facility151PictureA;
        internal PlatformTexture Facility152PictureA;
        internal PlatformTexture Facility153PictureA;
        internal PlatformTexture Facility154PictureA;
        internal PlatformTexture Facility155PictureA;
        internal PlatformTexture Facility156PictureA;
        internal PlatformTexture Facility157PictureA;
        internal PlatformTexture Facility158PictureA;
        internal PlatformTexture Facility159PictureA;
        internal PlatformTexture Facility160PictureA;
        internal PlatformTexture Facility161PictureA;
        internal PlatformTexture Facility162PictureA;
        internal PlatformTexture Facility163PictureA;
        internal PlatformTexture Facility164PictureA;
        internal PlatformTexture Facility165PictureA;
        internal PlatformTexture Facility166PictureA;
        internal PlatformTexture Facility167PictureA;
        internal PlatformTexture Facility168PictureA;
        internal PlatformTexture Facility169PictureA;
        internal PlatformTexture Facility170PictureA;
        internal PlatformTexture Facility171PictureA;
        internal PlatformTexture Facility172PictureA;
        internal PlatformTexture Facility173PictureA;
        internal PlatformTexture Facility174PictureA;
        internal PlatformTexture Facility175PictureA;
        internal PlatformTexture Facility176PictureA;
        internal PlatformTexture Facility177PictureA;
        internal PlatformTexture Facility178PictureA;
        internal PlatformTexture Facility179PictureA;
        internal PlatformTexture Facility180PictureA;
        internal PlatformTexture Facility181PictureA;
        internal PlatformTexture Facility182PictureA;
        internal PlatformTexture Facility183PictureA;
        internal PlatformTexture Facility184PictureA;
        internal PlatformTexture Facility185PictureA;
        internal PlatformTexture Facility186PictureA;
        internal PlatformTexture Facility187PictureA;
        internal PlatformTexture Facility188PictureA;
        internal PlatformTexture Facility189PictureA;
        internal PlatformTexture Facility190PictureA;
        internal PlatformTexture Facility191PictureA;
        internal PlatformTexture Facility192PictureA;
        internal PlatformTexture Facility193PictureA;
        internal PlatformTexture Facility194PictureA;
        internal PlatformTexture Facility195PictureA;
        internal PlatformTexture Facility196PictureA;
        internal PlatformTexture Facility197PictureA;
        internal PlatformTexture Facility198PictureA;
        internal PlatformTexture Facility199PictureA;
        internal PlatformTexture Facility200PictureA;
        internal PlatformTexture Facility1PictureB;
        internal PlatformTexture Facility2PictureB;
        internal PlatformTexture Facility3PictureB;
        internal PlatformTexture Facility4PictureB;
        internal PlatformTexture Facility5PictureB;
        internal PlatformTexture Facility6PictureB;
        internal PlatformTexture Facility7PictureB;
        internal PlatformTexture Facility8PictureB;
        internal PlatformTexture Facility9PictureB;
        internal PlatformTexture Facility10PictureB;
        internal PlatformTexture Facility11PictureB;
        internal PlatformTexture Facility12PictureB;
        internal PlatformTexture Facility13PictureB;
        internal PlatformTexture Facility14PictureB;
        internal PlatformTexture Facility15PictureB;
        internal PlatformTexture Facility16PictureB;
        internal PlatformTexture Facility17PictureB;
        internal PlatformTexture Facility18PictureB;
        internal PlatformTexture Facility19PictureB;
        internal PlatformTexture Facility20PictureB;
        internal PlatformTexture Facility21PictureB;
        internal PlatformTexture Facility22PictureB;
        internal PlatformTexture Facility23PictureB;
        internal PlatformTexture Facility24PictureB;
        internal PlatformTexture Facility25PictureB;
        internal PlatformTexture Facility26PictureB;
        internal PlatformTexture Facility27PictureB;
        internal PlatformTexture Facility28PictureB;
        internal PlatformTexture Facility29PictureB;
        internal PlatformTexture Facility30PictureB;
        internal PlatformTexture Facility31PictureB;
        internal PlatformTexture Facility32PictureB;
        internal PlatformTexture Facility33PictureB;
        internal PlatformTexture Facility34PictureB;
        internal PlatformTexture Facility35PictureB;
        internal PlatformTexture Facility36PictureB;
        internal PlatformTexture Facility37PictureB;
        internal PlatformTexture Facility38PictureB;
        internal PlatformTexture Facility39PictureB;
        internal PlatformTexture Facility40PictureB;
        internal PlatformTexture Facility41PictureB;
        internal PlatformTexture Facility42PictureB;
        internal PlatformTexture Facility43PictureB;
        internal PlatformTexture Facility44PictureB;
        internal PlatformTexture Facility45PictureB;
        internal PlatformTexture Facility46PictureB;
        internal PlatformTexture Facility47PictureB;
        internal PlatformTexture Facility48PictureB;
        internal PlatformTexture Facility49PictureB;
        internal PlatformTexture Facility50PictureB;
        internal PlatformTexture Facility51PictureB;
        internal PlatformTexture Facility52PictureB;
        internal PlatformTexture Facility53PictureB;
        internal PlatformTexture Facility54PictureB;
        internal PlatformTexture Facility55PictureB;
        internal PlatformTexture Facility56PictureB;
        internal PlatformTexture Facility57PictureB;
        internal PlatformTexture Facility58PictureB;
        internal PlatformTexture Facility59PictureB;
        internal PlatformTexture Facility60PictureB;
        internal PlatformTexture Facility61PictureB;
        internal PlatformTexture Facility62PictureB;
        internal PlatformTexture Facility63PictureB;
        internal PlatformTexture Facility64PictureB;
        internal PlatformTexture Facility65PictureB;
        internal PlatformTexture Facility66PictureB;
        internal PlatformTexture Facility67PictureB;
        internal PlatformTexture Facility68PictureB;
        internal PlatformTexture Facility69PictureB;
        internal PlatformTexture Facility70PictureB;
        internal PlatformTexture Facility71PictureB;
        internal PlatformTexture Facility72PictureB;
        internal PlatformTexture Facility73PictureB;
        internal PlatformTexture Facility74PictureB;
        internal PlatformTexture Facility75PictureB;
        internal PlatformTexture Facility76PictureB;
        internal PlatformTexture Facility77PictureB;
        internal PlatformTexture Facility78PictureB;
        internal PlatformTexture Facility79PictureB;
        internal PlatformTexture Facility80PictureB;
        internal PlatformTexture Facility81PictureB;
        internal PlatformTexture Facility82PictureB;
        internal PlatformTexture Facility83PictureB;
        internal PlatformTexture Facility84PictureB;
        internal PlatformTexture Facility85PictureB;
        internal PlatformTexture Facility86PictureB;
        internal PlatformTexture Facility87PictureB;
        internal PlatformTexture Facility88PictureB;
        internal PlatformTexture Facility89PictureB;
        internal PlatformTexture Facility90PictureB;
        internal PlatformTexture Facility91PictureB;
        internal PlatformTexture Facility92PictureB;
        internal PlatformTexture Facility93PictureB;
        internal PlatformTexture Facility94PictureB;
        internal PlatformTexture Facility95PictureB;
        internal PlatformTexture Facility96PictureB;
        internal PlatformTexture Facility97PictureB;
        internal PlatformTexture Facility98PictureB;
        internal PlatformTexture Facility99PictureB;
        internal PlatformTexture Facility100PictureB;
        internal PlatformTexture Facility101PictureB;
        internal PlatformTexture Facility102PictureB;
        internal PlatformTexture Facility103PictureB;
        internal PlatformTexture Facility104PictureB;
        internal PlatformTexture Facility105PictureB;
        internal PlatformTexture Facility106PictureB;
        internal PlatformTexture Facility107PictureB;
        internal PlatformTexture Facility108PictureB;
        internal PlatformTexture Facility109PictureB;
        internal PlatformTexture Facility110PictureB;
        internal PlatformTexture Facility111PictureB;
        internal PlatformTexture Facility112PictureB;
        internal PlatformTexture Facility113PictureB;
        internal PlatformTexture Facility114PictureB;
        internal PlatformTexture Facility115PictureB;
        internal PlatformTexture Facility116PictureB;
        internal PlatformTexture Facility117PictureB;
        internal PlatformTexture Facility118PictureB;
        internal PlatformTexture Facility119PictureB;
        internal PlatformTexture Facility120PictureB;
        internal PlatformTexture Facility121PictureB;
        internal PlatformTexture Facility122PictureB;
        internal PlatformTexture Facility123PictureB;
        internal PlatformTexture Facility124PictureB;
        internal PlatformTexture Facility125PictureB;
        internal PlatformTexture Facility126PictureB;
        internal PlatformTexture Facility127PictureB;
        internal PlatformTexture Facility128PictureB;
        internal PlatformTexture Facility129PictureB;
        internal PlatformTexture Facility130PictureB;
        internal PlatformTexture Facility131PictureB;
        internal PlatformTexture Facility132PictureB;
        internal PlatformTexture Facility133PictureB;
        internal PlatformTexture Facility134PictureB;
        internal PlatformTexture Facility135PictureB;
        internal PlatformTexture Facility136PictureB;
        internal PlatformTexture Facility137PictureB;
        internal PlatformTexture Facility138PictureB;
        internal PlatformTexture Facility139PictureB;
        internal PlatformTexture Facility140PictureB;
        internal PlatformTexture Facility141PictureB;
        internal PlatformTexture Facility142PictureB;
        internal PlatformTexture Facility143PictureB;
        internal PlatformTexture Facility144PictureB;
        internal PlatformTexture Facility145PictureB;
        internal PlatformTexture Facility146PictureB;
        internal PlatformTexture Facility147PictureB;
        internal PlatformTexture Facility148PictureB;
        internal PlatformTexture Facility149PictureB;
        internal PlatformTexture Facility150PictureB;
        internal PlatformTexture Facility151PictureB;
        internal PlatformTexture Facility152PictureB;
        internal PlatformTexture Facility153PictureB;
        internal PlatformTexture Facility154PictureB;
        internal PlatformTexture Facility155PictureB;
        internal PlatformTexture Facility156PictureB;
        internal PlatformTexture Facility157PictureB;
        internal PlatformTexture Facility158PictureB;
        internal PlatformTexture Facility159PictureB;
        internal PlatformTexture Facility160PictureB;
        internal PlatformTexture Facility161PictureB;
        internal PlatformTexture Facility162PictureB;
        internal PlatformTexture Facility163PictureB;
        internal PlatformTexture Facility164PictureB;
        internal PlatformTexture Facility165PictureB;
        internal PlatformTexture Facility166PictureB;
        internal PlatformTexture Facility167PictureB;
        internal PlatformTexture Facility168PictureB;
        internal PlatformTexture Facility169PictureB;
        internal PlatformTexture Facility170PictureB;
        internal PlatformTexture Facility171PictureB;
        internal PlatformTexture Facility172PictureB;
        internal PlatformTexture Facility173PictureB;
        internal PlatformTexture Facility174PictureB;
        internal PlatformTexture Facility175PictureB;
        internal PlatformTexture Facility176PictureB;
        internal PlatformTexture Facility177PictureB;
        internal PlatformTexture Facility178PictureB;
        internal PlatformTexture Facility179PictureB;
        internal PlatformTexture Facility180PictureB;
        internal PlatformTexture Facility181PictureB;
        internal PlatformTexture Facility182PictureB;
        internal PlatformTexture Facility183PictureB;
        internal PlatformTexture Facility184PictureB;
        internal PlatformTexture Facility185PictureB;
        internal PlatformTexture Facility186PictureB;
        internal PlatformTexture Facility187PictureB;
        internal PlatformTexture Facility188PictureB;
        internal PlatformTexture Facility189PictureB;
        internal PlatformTexture Facility190PictureB;
        internal PlatformTexture Facility191PictureB;
        internal PlatformTexture Facility192PictureB;
        internal PlatformTexture Facility193PictureB;
        internal PlatformTexture Facility194PictureB;
        internal PlatformTexture Facility195PictureB;
        internal PlatformTexture Facility196PictureB;
        internal PlatformTexture Facility197PictureB;
        internal PlatformTexture Facility198PictureB;
        internal PlatformTexture Facility199PictureB;
        internal PlatformTexture Facility200PictureB;
        internal Rectangle Facility1Client;
        internal Rectangle Facility2Client;
        internal Rectangle Facility3Client;
        internal Rectangle Facility4Client;
        internal Rectangle Facility5Client;
        internal Rectangle Facility6Client;
        internal Rectangle Facility7Client;
        internal Rectangle Facility8Client;
        internal Rectangle Facility9Client;
        internal Rectangle Facility10Client;
        internal Rectangle Facility11Client;
        internal Rectangle Facility12Client;
        internal Rectangle Facility13Client;
        internal Rectangle Facility14Client;
        internal Rectangle Facility15Client;
        internal Rectangle Facility16Client;
        internal Rectangle Facility17Client;
        internal Rectangle Facility18Client;
        internal Rectangle Facility19Client;
        internal Rectangle Facility20Client;
        internal Rectangle Facility21Client;
        internal Rectangle Facility22Client;
        internal Rectangle Facility23Client;
        internal Rectangle Facility24Client;
        internal Rectangle Facility25Client;
        internal Rectangle Facility26Client;
        internal Rectangle Facility27Client;
        internal Rectangle Facility28Client;
        internal Rectangle Facility29Client;
        internal Rectangle Facility30Client;
        internal Rectangle Facility31Client;
        internal Rectangle Facility32Client;
        internal Rectangle Facility33Client;
        internal Rectangle Facility34Client;
        internal Rectangle Facility35Client;
        internal Rectangle Facility36Client;
        internal Rectangle Facility37Client;
        internal Rectangle Facility38Client;
        internal Rectangle Facility39Client;
        internal Rectangle Facility40Client;
        internal Rectangle Facility41Client;
        internal Rectangle Facility42Client;
        internal Rectangle Facility43Client;
        internal Rectangle Facility44Client;
        internal Rectangle Facility45Client;
        internal Rectangle Facility46Client;
        internal Rectangle Facility47Client;
        internal Rectangle Facility48Client;
        internal Rectangle Facility49Client;
        internal Rectangle Facility50Client;
        internal Rectangle Facility51Client;
        internal Rectangle Facility52Client;
        internal Rectangle Facility53Client;
        internal Rectangle Facility54Client;
        internal Rectangle Facility55Client;
        internal Rectangle Facility56Client;
        internal Rectangle Facility57Client;
        internal Rectangle Facility58Client;
        internal Rectangle Facility59Client;
        internal Rectangle Facility60Client;
        internal Rectangle Facility61Client;
        internal Rectangle Facility62Client;
        internal Rectangle Facility63Client;
        internal Rectangle Facility64Client;
        internal Rectangle Facility65Client;
        internal Rectangle Facility66Client;
        internal Rectangle Facility67Client;
        internal Rectangle Facility68Client;
        internal Rectangle Facility69Client;
        internal Rectangle Facility70Client;
        internal Rectangle Facility71Client;
        internal Rectangle Facility72Client;
        internal Rectangle Facility73Client;
        internal Rectangle Facility74Client;
        internal Rectangle Facility75Client;
        internal Rectangle Facility76Client;
        internal Rectangle Facility77Client;
        internal Rectangle Facility78Client;
        internal Rectangle Facility79Client;
        internal Rectangle Facility80Client;
        internal Rectangle Facility81Client;
        internal Rectangle Facility82Client;
        internal Rectangle Facility83Client;
        internal Rectangle Facility84Client;
        internal Rectangle Facility85Client;
        internal Rectangle Facility86Client;
        internal Rectangle Facility87Client;
        internal Rectangle Facility88Client;
        internal Rectangle Facility89Client;
        internal Rectangle Facility90Client;
        internal Rectangle Facility91Client;
        internal Rectangle Facility92Client;
        internal Rectangle Facility93Client;
        internal Rectangle Facility94Client;
        internal Rectangle Facility95Client;
        internal Rectangle Facility96Client;
        internal Rectangle Facility97Client;
        internal Rectangle Facility98Client;
        internal Rectangle Facility99Client;
        internal Rectangle Facility100Client;
        internal Rectangle Facility101Client;
        internal Rectangle Facility102Client;
        internal Rectangle Facility103Client;
        internal Rectangle Facility104Client;
        internal Rectangle Facility105Client;
        internal Rectangle Facility106Client;
        internal Rectangle Facility107Client;
        internal Rectangle Facility108Client;
        internal Rectangle Facility109Client;
        internal Rectangle Facility110Client;
        internal Rectangle Facility111Client;
        internal Rectangle Facility112Client;
        internal Rectangle Facility113Client;
        internal Rectangle Facility114Client;
        internal Rectangle Facility115Client;
        internal Rectangle Facility116Client;
        internal Rectangle Facility117Client;
        internal Rectangle Facility118Client;
        internal Rectangle Facility119Client;
        internal Rectangle Facility120Client;
        internal Rectangle Facility121Client;
        internal Rectangle Facility122Client;
        internal Rectangle Facility123Client;
        internal Rectangle Facility124Client;
        internal Rectangle Facility125Client;
        internal Rectangle Facility126Client;
        internal Rectangle Facility127Client;
        internal Rectangle Facility128Client;
        internal Rectangle Facility129Client;
        internal Rectangle Facility130Client;
        internal Rectangle Facility131Client;
        internal Rectangle Facility132Client;
        internal Rectangle Facility133Client;
        internal Rectangle Facility134Client;
        internal Rectangle Facility135Client;
        internal Rectangle Facility136Client;
        internal Rectangle Facility137Client;
        internal Rectangle Facility138Client;
        internal Rectangle Facility139Client;
        internal Rectangle Facility140Client;
        internal Rectangle Facility141Client;
        internal Rectangle Facility142Client;
        internal Rectangle Facility143Client;
        internal Rectangle Facility144Client;
        internal Rectangle Facility145Client;
        internal Rectangle Facility146Client;
        internal Rectangle Facility147Client;
        internal Rectangle Facility148Client;
        internal Rectangle Facility149Client;
        internal Rectangle Facility150Client;
        internal Rectangle Facility151Client;
        internal Rectangle Facility152Client;
        internal Rectangle Facility153Client;
        internal Rectangle Facility154Client;
        internal Rectangle Facility155Client;
        internal Rectangle Facility156Client;
        internal Rectangle Facility157Client;
        internal Rectangle Facility158Client;
        internal Rectangle Facility159Client;
        internal Rectangle Facility160Client;
        internal Rectangle Facility161Client;
        internal Rectangle Facility162Client;
        internal Rectangle Facility163Client;
        internal Rectangle Facility164Client;
        internal Rectangle Facility165Client;
        internal Rectangle Facility166Client;
        internal Rectangle Facility167Client;
        internal Rectangle Facility168Client;
        internal Rectangle Facility169Client;
        internal Rectangle Facility170Client;
        internal Rectangle Facility171Client;
        internal Rectangle Facility172Client;
        internal Rectangle Facility173Client;
        internal Rectangle Facility174Client;
        internal Rectangle Facility175Client;
        internal Rectangle Facility176Client;
        internal Rectangle Facility177Client;
        internal Rectangle Facility178Client;
        internal Rectangle Facility179Client;
        internal Rectangle Facility180Client;
        internal Rectangle Facility181Client;
        internal Rectangle Facility182Client;
        internal Rectangle Facility183Client;
        internal Rectangle Facility184Client;
        internal Rectangle Facility185Client;
        internal Rectangle Facility186Client;
        internal Rectangle Facility187Client;
        internal Rectangle Facility188Client;
        internal Rectangle Facility189Client;
        internal Rectangle Facility190Client;
        internal Rectangle Facility191Client;
        internal Rectangle Facility192Client;
        internal Rectangle Facility193Client;
        internal Rectangle Facility194Client;
        internal Rectangle Facility195Client;
        internal Rectangle Facility196Client;
        internal Rectangle Facility197Client;
        internal Rectangle Facility198Client;
        internal Rectangle Facility199Client;
        internal Rectangle Facility200Client;
        //↓设施介绍        
        internal PlatformTexture Facility1TextPicture;
        internal PlatformTexture Facility2TextPicture;
        internal PlatformTexture Facility3TextPicture;
        internal PlatformTexture Facility4TextPicture;
        internal PlatformTexture Facility5TextPicture;
        internal PlatformTexture Facility6TextPicture;
        internal PlatformTexture Facility7TextPicture;
        internal PlatformTexture Facility8TextPicture;
        internal PlatformTexture Facility9TextPicture;
        internal PlatformTexture Facility10TextPicture;
        internal PlatformTexture Facility11TextPicture;
        internal PlatformTexture Facility12TextPicture;
        internal PlatformTexture Facility13TextPicture;
        internal PlatformTexture Facility14TextPicture;
        internal PlatformTexture Facility15TextPicture;
        internal PlatformTexture Facility16TextPicture;
        internal PlatformTexture Facility17TextPicture;
        internal PlatformTexture Facility18TextPicture;
        internal PlatformTexture Facility19TextPicture;
        internal PlatformTexture Facility20TextPicture;
        internal PlatformTexture Facility21TextPicture;
        internal PlatformTexture Facility22TextPicture;
        internal PlatformTexture Facility23TextPicture;
        internal PlatformTexture Facility24TextPicture;
        internal PlatformTexture Facility25TextPicture;
        internal PlatformTexture Facility26TextPicture;
        internal PlatformTexture Facility27TextPicture;
        internal PlatformTexture Facility28TextPicture;
        internal PlatformTexture Facility29TextPicture;
        internal PlatformTexture Facility30TextPicture;
        internal PlatformTexture Facility31TextPicture;
        internal PlatformTexture Facility32TextPicture;
        internal PlatformTexture Facility33TextPicture;
        internal PlatformTexture Facility34TextPicture;
        internal PlatformTexture Facility35TextPicture;
        internal PlatformTexture Facility36TextPicture;
        internal PlatformTexture Facility37TextPicture;
        internal PlatformTexture Facility38TextPicture;
        internal PlatformTexture Facility39TextPicture;
        internal PlatformTexture Facility40TextPicture;
        internal PlatformTexture Facility41TextPicture;
        internal PlatformTexture Facility42TextPicture;
        internal PlatformTexture Facility43TextPicture;
        internal PlatformTexture Facility44TextPicture;
        internal PlatformTexture Facility45TextPicture;
        internal PlatformTexture Facility46TextPicture;
        internal PlatformTexture Facility47TextPicture;
        internal PlatformTexture Facility48TextPicture;
        internal PlatformTexture Facility49TextPicture;
        internal PlatformTexture Facility50TextPicture;
        internal PlatformTexture Facility51TextPicture;
        internal PlatformTexture Facility52TextPicture;
        internal PlatformTexture Facility53TextPicture;
        internal PlatformTexture Facility54TextPicture;
        internal PlatformTexture Facility55TextPicture;
        internal PlatformTexture Facility56TextPicture;
        internal PlatformTexture Facility57TextPicture;
        internal PlatformTexture Facility58TextPicture;
        internal PlatformTexture Facility59TextPicture;
        internal PlatformTexture Facility60TextPicture;
        internal PlatformTexture Facility61TextPicture;
        internal PlatformTexture Facility62TextPicture;
        internal PlatformTexture Facility63TextPicture;
        internal PlatformTexture Facility64TextPicture;
        internal PlatformTexture Facility65TextPicture;
        internal PlatformTexture Facility66TextPicture;
        internal PlatformTexture Facility67TextPicture;
        internal PlatformTexture Facility68TextPicture;
        internal PlatformTexture Facility69TextPicture;
        internal PlatformTexture Facility70TextPicture;
        internal PlatformTexture Facility71TextPicture;
        internal PlatformTexture Facility72TextPicture;
        internal PlatformTexture Facility73TextPicture;
        internal PlatformTexture Facility74TextPicture;
        internal PlatformTexture Facility75TextPicture;
        internal PlatformTexture Facility76TextPicture;
        internal PlatformTexture Facility77TextPicture;
        internal PlatformTexture Facility78TextPicture;
        internal PlatformTexture Facility79TextPicture;
        internal PlatformTexture Facility80TextPicture;
        internal PlatformTexture Facility81TextPicture;
        internal PlatformTexture Facility82TextPicture;
        internal PlatformTexture Facility83TextPicture;
        internal PlatformTexture Facility84TextPicture;
        internal PlatformTexture Facility85TextPicture;
        internal PlatformTexture Facility86TextPicture;
        internal PlatformTexture Facility87TextPicture;
        internal PlatformTexture Facility88TextPicture;
        internal PlatformTexture Facility89TextPicture;
        internal PlatformTexture Facility90TextPicture;
        internal PlatformTexture Facility91TextPicture;
        internal PlatformTexture Facility92TextPicture;
        internal PlatformTexture Facility93TextPicture;
        internal PlatformTexture Facility94TextPicture;
        internal PlatformTexture Facility95TextPicture;
        internal PlatformTexture Facility96TextPicture;
        internal PlatformTexture Facility97TextPicture;
        internal PlatformTexture Facility98TextPicture;
        internal PlatformTexture Facility99TextPicture;
        internal PlatformTexture Facility100TextPicture;
        internal PlatformTexture Facility101TextPicture;
        internal PlatformTexture Facility102TextPicture;
        internal PlatformTexture Facility103TextPicture;
        internal PlatformTexture Facility104TextPicture;
        internal PlatformTexture Facility105TextPicture;
        internal PlatformTexture Facility106TextPicture;
        internal PlatformTexture Facility107TextPicture;
        internal PlatformTexture Facility108TextPicture;
        internal PlatformTexture Facility109TextPicture;
        internal PlatformTexture Facility110TextPicture;
        internal PlatformTexture Facility111TextPicture;
        internal PlatformTexture Facility112TextPicture;
        internal PlatformTexture Facility113TextPicture;
        internal PlatformTexture Facility114TextPicture;
        internal PlatformTexture Facility115TextPicture;
        internal PlatformTexture Facility116TextPicture;
        internal PlatformTexture Facility117TextPicture;
        internal PlatformTexture Facility118TextPicture;
        internal PlatformTexture Facility119TextPicture;
        internal PlatformTexture Facility120TextPicture;
        internal PlatformTexture Facility121TextPicture;
        internal PlatformTexture Facility122TextPicture;
        internal PlatformTexture Facility123TextPicture;
        internal PlatformTexture Facility124TextPicture;
        internal PlatformTexture Facility125TextPicture;
        internal PlatformTexture Facility126TextPicture;
        internal PlatformTexture Facility127TextPicture;
        internal PlatformTexture Facility128TextPicture;
        internal PlatformTexture Facility129TextPicture;
        internal PlatformTexture Facility130TextPicture;
        internal PlatformTexture Facility131TextPicture;
        internal PlatformTexture Facility132TextPicture;
        internal PlatformTexture Facility133TextPicture;
        internal PlatformTexture Facility134TextPicture;
        internal PlatformTexture Facility135TextPicture;
        internal PlatformTexture Facility136TextPicture;
        internal PlatformTexture Facility137TextPicture;
        internal PlatformTexture Facility138TextPicture;
        internal PlatformTexture Facility139TextPicture;
        internal PlatformTexture Facility140TextPicture;
        internal PlatformTexture Facility141TextPicture;
        internal PlatformTexture Facility142TextPicture;
        internal PlatformTexture Facility143TextPicture;
        internal PlatformTexture Facility144TextPicture;
        internal PlatformTexture Facility145TextPicture;
        internal PlatformTexture Facility146TextPicture;
        internal PlatformTexture Facility147TextPicture;
        internal PlatformTexture Facility148TextPicture;
        internal PlatformTexture Facility149TextPicture;
        internal PlatformTexture Facility150TextPicture;
        internal PlatformTexture Facility151TextPicture;
        internal PlatformTexture Facility152TextPicture;
        internal PlatformTexture Facility153TextPicture;
        internal PlatformTexture Facility154TextPicture;
        internal PlatformTexture Facility155TextPicture;
        internal PlatformTexture Facility156TextPicture;
        internal PlatformTexture Facility157TextPicture;
        internal PlatformTexture Facility158TextPicture;
        internal PlatformTexture Facility159TextPicture;
        internal PlatformTexture Facility160TextPicture;
        internal PlatformTexture Facility161TextPicture;
        internal PlatformTexture Facility162TextPicture;
        internal PlatformTexture Facility163TextPicture;
        internal PlatformTexture Facility164TextPicture;
        internal PlatformTexture Facility165TextPicture;
        internal PlatformTexture Facility166TextPicture;
        internal PlatformTexture Facility167TextPicture;
        internal PlatformTexture Facility168TextPicture;
        internal PlatformTexture Facility169TextPicture;
        internal PlatformTexture Facility170TextPicture;
        internal PlatformTexture Facility171TextPicture;
        internal PlatformTexture Facility172TextPicture;
        internal PlatformTexture Facility173TextPicture;
        internal PlatformTexture Facility174TextPicture;
        internal PlatformTexture Facility175TextPicture;
        internal PlatformTexture Facility176TextPicture;
        internal PlatformTexture Facility177TextPicture;
        internal PlatformTexture Facility178TextPicture;
        internal PlatformTexture Facility179TextPicture;
        internal PlatformTexture Facility180TextPicture;
        internal PlatformTexture Facility181TextPicture;
        internal PlatformTexture Facility182TextPicture;
        internal PlatformTexture Facility183TextPicture;
        internal PlatformTexture Facility184TextPicture;
        internal PlatformTexture Facility185TextPicture;
        internal PlatformTexture Facility186TextPicture;
        internal PlatformTexture Facility187TextPicture;
        internal PlatformTexture Facility188TextPicture;
        internal PlatformTexture Facility189TextPicture;
        internal PlatformTexture Facility190TextPicture;
        internal PlatformTexture Facility191TextPicture;
        internal PlatformTexture Facility192TextPicture;
        internal PlatformTexture Facility193TextPicture;
        internal PlatformTexture Facility194TextPicture;
        internal PlatformTexture Facility195TextPicture;
        internal PlatformTexture Facility196TextPicture;
        internal PlatformTexture Facility197TextPicture;
        internal PlatformTexture Facility198TextPicture;
        internal PlatformTexture Facility199TextPicture;
        internal PlatformTexture Facility200TextPicture;
        internal Rectangle Facility1TextClient;
        internal Rectangle Facility2TextClient;
        internal Rectangle Facility3TextClient;
        internal Rectangle Facility4TextClient;
        internal Rectangle Facility5TextClient;
        internal Rectangle Facility6TextClient;
        internal Rectangle Facility7TextClient;
        internal Rectangle Facility8TextClient;
        internal Rectangle Facility9TextClient;
        internal Rectangle Facility10TextClient;
        internal Rectangle Facility11TextClient;
        internal Rectangle Facility12TextClient;
        internal Rectangle Facility13TextClient;
        internal Rectangle Facility14TextClient;
        internal Rectangle Facility15TextClient;
        internal Rectangle Facility16TextClient;
        internal Rectangle Facility17TextClient;
        internal Rectangle Facility18TextClient;
        internal Rectangle Facility19TextClient;
        internal Rectangle Facility20TextClient;
        internal Rectangle Facility21TextClient;
        internal Rectangle Facility22TextClient;
        internal Rectangle Facility23TextClient;
        internal Rectangle Facility24TextClient;
        internal Rectangle Facility25TextClient;
        internal Rectangle Facility26TextClient;
        internal Rectangle Facility27TextClient;
        internal Rectangle Facility28TextClient;
        internal Rectangle Facility29TextClient;
        internal Rectangle Facility30TextClient;
        internal Rectangle Facility31TextClient;
        internal Rectangle Facility32TextClient;
        internal Rectangle Facility33TextClient;
        internal Rectangle Facility34TextClient;
        internal Rectangle Facility35TextClient;
        internal Rectangle Facility36TextClient;
        internal Rectangle Facility37TextClient;
        internal Rectangle Facility38TextClient;
        internal Rectangle Facility39TextClient;
        internal Rectangle Facility40TextClient;
        internal Rectangle Facility41TextClient;
        internal Rectangle Facility42TextClient;
        internal Rectangle Facility43TextClient;
        internal Rectangle Facility44TextClient;
        internal Rectangle Facility45TextClient;
        internal Rectangle Facility46TextClient;
        internal Rectangle Facility47TextClient;
        internal Rectangle Facility48TextClient;
        internal Rectangle Facility49TextClient;
        internal Rectangle Facility50TextClient;
        internal Rectangle Facility51TextClient;
        internal Rectangle Facility52TextClient;
        internal Rectangle Facility53TextClient;
        internal Rectangle Facility54TextClient;
        internal Rectangle Facility55TextClient;
        internal Rectangle Facility56TextClient;
        internal Rectangle Facility57TextClient;
        internal Rectangle Facility58TextClient;
        internal Rectangle Facility59TextClient;
        internal Rectangle Facility60TextClient;
        internal Rectangle Facility61TextClient;
        internal Rectangle Facility62TextClient;
        internal Rectangle Facility63TextClient;
        internal Rectangle Facility64TextClient;
        internal Rectangle Facility65TextClient;
        internal Rectangle Facility66TextClient;
        internal Rectangle Facility67TextClient;
        internal Rectangle Facility68TextClient;
        internal Rectangle Facility69TextClient;
        internal Rectangle Facility70TextClient;
        internal Rectangle Facility71TextClient;
        internal Rectangle Facility72TextClient;
        internal Rectangle Facility73TextClient;
        internal Rectangle Facility74TextClient;
        internal Rectangle Facility75TextClient;
        internal Rectangle Facility76TextClient;
        internal Rectangle Facility77TextClient;
        internal Rectangle Facility78TextClient;
        internal Rectangle Facility79TextClient;
        internal Rectangle Facility80TextClient;
        internal Rectangle Facility81TextClient;
        internal Rectangle Facility82TextClient;
        internal Rectangle Facility83TextClient;
        internal Rectangle Facility84TextClient;
        internal Rectangle Facility85TextClient;
        internal Rectangle Facility86TextClient;
        internal Rectangle Facility87TextClient;
        internal Rectangle Facility88TextClient;
        internal Rectangle Facility89TextClient;
        internal Rectangle Facility90TextClient;
        internal Rectangle Facility91TextClient;
        internal Rectangle Facility92TextClient;
        internal Rectangle Facility93TextClient;
        internal Rectangle Facility94TextClient;
        internal Rectangle Facility95TextClient;
        internal Rectangle Facility96TextClient;
        internal Rectangle Facility97TextClient;
        internal Rectangle Facility98TextClient;
        internal Rectangle Facility99TextClient;
        internal Rectangle Facility100TextClient;
        internal Rectangle Facility101TextClient;
        internal Rectangle Facility102TextClient;
        internal Rectangle Facility103TextClient;
        internal Rectangle Facility104TextClient;
        internal Rectangle Facility105TextClient;
        internal Rectangle Facility106TextClient;
        internal Rectangle Facility107TextClient;
        internal Rectangle Facility108TextClient;
        internal Rectangle Facility109TextClient;
        internal Rectangle Facility110TextClient;
        internal Rectangle Facility111TextClient;
        internal Rectangle Facility112TextClient;
        internal Rectangle Facility113TextClient;
        internal Rectangle Facility114TextClient;
        internal Rectangle Facility115TextClient;
        internal Rectangle Facility116TextClient;
        internal Rectangle Facility117TextClient;
        internal Rectangle Facility118TextClient;
        internal Rectangle Facility119TextClient;
        internal Rectangle Facility120TextClient;
        internal Rectangle Facility121TextClient;
        internal Rectangle Facility122TextClient;
        internal Rectangle Facility123TextClient;
        internal Rectangle Facility124TextClient;
        internal Rectangle Facility125TextClient;
        internal Rectangle Facility126TextClient;
        internal Rectangle Facility127TextClient;
        internal Rectangle Facility128TextClient;
        internal Rectangle Facility129TextClient;
        internal Rectangle Facility130TextClient;
        internal Rectangle Facility131TextClient;
        internal Rectangle Facility132TextClient;
        internal Rectangle Facility133TextClient;
        internal Rectangle Facility134TextClient;
        internal Rectangle Facility135TextClient;
        internal Rectangle Facility136TextClient;
        internal Rectangle Facility137TextClient;
        internal Rectangle Facility138TextClient;
        internal Rectangle Facility139TextClient;
        internal Rectangle Facility140TextClient;
        internal Rectangle Facility141TextClient;
        internal Rectangle Facility142TextClient;
        internal Rectangle Facility143TextClient;
        internal Rectangle Facility144TextClient;
        internal Rectangle Facility145TextClient;
        internal Rectangle Facility146TextClient;
        internal Rectangle Facility147TextClient;
        internal Rectangle Facility148TextClient;
        internal Rectangle Facility149TextClient;
        internal Rectangle Facility150TextClient;
        internal Rectangle Facility151TextClient;
        internal Rectangle Facility152TextClient;
        internal Rectangle Facility153TextClient;
        internal Rectangle Facility154TextClient;
        internal Rectangle Facility155TextClient;
        internal Rectangle Facility156TextClient;
        internal Rectangle Facility157TextClient;
        internal Rectangle Facility158TextClient;
        internal Rectangle Facility159TextClient;
        internal Rectangle Facility160TextClient;
        internal Rectangle Facility161TextClient;
        internal Rectangle Facility162TextClient;
        internal Rectangle Facility163TextClient;
        internal Rectangle Facility164TextClient;
        internal Rectangle Facility165TextClient;
        internal Rectangle Facility166TextClient;
        internal Rectangle Facility167TextClient;
        internal Rectangle Facility168TextClient;
        internal Rectangle Facility169TextClient;
        internal Rectangle Facility170TextClient;
        internal Rectangle Facility171TextClient;
        internal Rectangle Facility172TextClient;
        internal Rectangle Facility173TextClient;
        internal Rectangle Facility174TextClient;
        internal Rectangle Facility175TextClient;
        internal Rectangle Facility176TextClient;
        internal Rectangle Facility177TextClient;
        internal Rectangle Facility178TextClient;
        internal Rectangle Facility179TextClient;
        internal Rectangle Facility180TextClient;
        internal Rectangle Facility181TextClient;
        internal Rectangle Facility182TextClient;
        internal Rectangle Facility183TextClient;
        internal Rectangle Facility184TextClient;
        internal Rectangle Facility185TextClient;
        internal Rectangle Facility186TextClient;
        internal Rectangle Facility187TextClient;
        internal Rectangle Facility188TextClient;
        internal Rectangle Facility189TextClient;
        internal Rectangle Facility190TextClient;
        internal Rectangle Facility191TextClient;
        internal Rectangle Facility192TextClient;
        internal Rectangle Facility193TextClient;
        internal Rectangle Facility194TextClient;
        internal Rectangle Facility195TextClient;
        internal Rectangle Facility196TextClient;
        internal Rectangle Facility197TextClient;
        internal Rectangle Facility198TextClient;
        internal Rectangle Facility199TextClient;
        internal Rectangle Facility200TextClient;

        ////↓按钮判定
        bool InformationButton;
        
        bool FacilityButton;

        bool FacilityforPage1Button;
        bool FacilityforPage2Button;
        bool FacilityforPage3Button;
        bool FacilityforPage4Button;
        bool FacilityforPage5Button;
        bool FacilityforPage6Button;
        bool FacilityforPage7Button;
        bool FacilityforPage8Button;
        bool FacilityforPage9Button;
        bool FacilityforPage10Button;
        bool FacilityforPage11Button;
        bool FacilityforPage12Button;
        bool FacilityforPage13Button;
        bool FacilityforPage14Button;
        bool FacilityforPage15Button;
        bool FacilityforPage16Button;
        bool FacilityforPage17Button;
        bool FacilityforPage18Button;
        bool FacilityforPage19Button;
        bool FacilityforPage20Button;
        bool FacilityforPage21Button;
        bool FacilityforPage22Button;
        bool FacilityforPage23Button;
        bool FacilityforPage24Button;
        bool FacilityforPage25Button;
        bool FacilityforPage26Button;
        bool FacilityforPage27Button;
        bool FacilityforPage28Button;
        bool FacilityforPage29Button;
        bool FacilityforPage30Button;

        bool FacilityDescriptionTextIng;
        bool FacilityDescriptionTexting;
        //////以上添加
        internal void Draw()
        {
            CacheManager.Scale = Scale;
            if (this.ShowingArchitecture != null)
            {
                if (Switch1 == "off")
                {
                    CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    foreach (LabelText text in this.LabelTexts)
                    {
                        text.Label.Draw(0.1999f);
                        text.Text.Draw(0.1999f);
                    }
                    this.CharacteristicText.Draw(0.1999f);
                    this.FacilityText.Draw(0.1999f);
                }
                if (Switch1 == "on")
                {                  
                    if (Switch21 == "on")
                    {
                        CacheManager.Draw(this.InformationButtonTexture, this.InformationButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018f);
                        CacheManager.Draw(this.InformationPressedTexture, this.InformationPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018f);
                        if (InformationButton == true)
                        {
                            CacheManager.Draw(this.InformationMask1, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0192f);
                            CacheManager.Draw(this.InformationMask2, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);
                            CacheManager.Draw(this.InformationBackground, this.InformationBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);
                            foreach (LabelText text in this.ArchitectureInInformationTexts)
                            {
                                text.Label.Draw(0.0194f);
                                text.Text.Draw(0.0194f);
                            }
                            if (Switch26 == "on")
                            {
                                try
                                {
                                    if (PictureShowforAKind == "on")
                                    {
                                        CacheManager.Draw(this.PictureforAKind, this.PictureforAKindDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0197f);
                                    }
                                    if (PictureShowforAID == "on")
                                    {
                                        CacheManager.Draw(this.PictureforAID, this.PictureforAIDDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.01975f);
                                    }
                                }
                                catch { }
                            }
                            if (Switch22 == "on")
                            {
                                try
                                {
                                    CacheManager.Draw(this.IntegrationBarTexture, this.IntegrationBar1DisplayPosition, this.Integration1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.DominationBarTexture, this.DominationBar1DisplayPosition, this.Domination1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.EnduranceBarTexture, this.EnduranceBar1DisplayPosition, this.Endurance1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.AgricultureBarTexture, this.AgricultureBar1DisplayPosition, this.Agriculture1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.CommerceBarTexture, this.CommerceBar1DisplayPosition, this.Commerce1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.TechnologyBarTexture, this.TechnologyBar1DisplayPosition, this.Technology1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.MoraleBarTexture, this.MoraleBar1DisplayPosition, this.Morale1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                    CacheManager.Draw(this.FacilityCountBarTexture, this.FacilityCountBar1DisplayPosition, this.FacilityCount1DisplayPosition, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                }
                                catch { }
                            }
                            if (Switch23 == "on" && TheCharacteristicCount >0)
                            {
                                CacheManager.Draw(this.CharacteristicShowMask, this.CharacteristicShowBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0185f);                                
                                try
                                {
                                    for (int c = 1; c <= TheMaxShowCharacteristicCount; c++)
                                    {
                                        CacheManager.Draw(this.TheCharacteristicShowMask, this.TheCharacteristicShowDisplayPosition(c), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0186f);                               
                                        CacheManager.Draw(this.TheCharacteristicShow(c), this.TheCharacteristicShowDisplayPosition(c), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0188f);
                                    }
                                    if (ShowNullCharacteristicPicture == "on")
                                    {
                                        for (int c = 1; c <= TheMaxShowCharacteristicCount; c++)
                                        {
                                            CacheManager.Draw(this.TheCharacteristicShowBackground, this.TheCharacteristicShowDisplayPosition(c), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0188f);
                                        }
                                    }
                                }
                                catch { }
                                CacheManager.Draw(this.CharacteristicShowBackground, this.CharacteristicShowBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.019f); 
                            }
                            if (Switch24 == "on")
                            {
                                this.TheCharacteristic1Text.Draw(0.0194f);
                            }
                            if (Switch25 == "on")
                            {
                                this.TheFacility1Text.Draw(0.194f);
                            }
                        }                        
                    }                   
                    if (Switch41 == "on")
                    {
                        CacheManager.Draw(this.FacilityButtonTexture, this.FacilityButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018f);
                        CacheManager.Draw(this.FacilityPressedTexture, this.FacilityPressedDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.018f);
                        if (FacilityButton == true)
                        {
                            CacheManager.Draw(this.FacilityMask1, this.FacilityBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0192f);
                            CacheManager.Draw(this.FacilityMask2, this.FacilityBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0199f);
                            CacheManager.Draw(this.FacilityBackground, this.FacilityBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.02f);
                            if (Switch47 == "on")
                            {
                                this.TheFacility3Text.Draw(0.0198f);
                            }
                            if (Switch42 == "on")
                            {
                                foreach (LabelText text in this.ArchitectureInFacilityTexts)
                                {
                                    text.Label.Draw(0.0194f);
                                    text.Text.Draw(0.0194f);
                                }
                            }
                            
                            if (TheFacilityPositionCount == 0)
                            {
                                CacheManager.Draw(this.PageForFacilityMask, this.PageForFacilityBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0194f);
                                CacheManager.Draw(this.PageForFacilityBackground, this.PageForFacilityBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                            }
                            else 
                            {
                                try
                                {
                                    for (int i = 1; i <= 30; i++)
                                    {
                                        if (ShowTheFacilityPageButton(i) == true)
                                        {
                                            CacheManager.Draw(this.FacilityforPageButtonTexture(i), this.TheFacilityforPageButtonDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0181f);
                                            CacheManager.Draw(this.FacilityforPagePressedTexture(i), this.TheFacilityforPagePressedDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0181f);
                                            CacheManager.Draw(this.ThePageForFacilityMask(i), this.ThePageForFacilityBackgroundDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0193f);
                                            CacheManager.Draw(this.ThePageForFacilityBackground(i), this.ThePageForFacilityBackgroundDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0197f);
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    for (int i = 1; i <= TheAllFacilityNumber; i++)
                                    {
                                        CacheManager.Draw(this.TheFacilityPictureA(i), this.TheFacilityAShowDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                        if (Switch45 == "on")
                                        {
                                            CacheManager.Draw(this.TheFacilityPictureB(i), this.TheFacilityBShowDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0195f);
                                        }
                                        if (Switch46 == "on")
                                        {
                                            CacheManager.Draw(this.TheFacilityTextPicture(i), this.TheFacilityTextShowDisplayPosition(i), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0194f);
                                        }
                                    }
                                    if (HasBuildingFacility == true)
                                    {
                                        CacheManager.Draw(this.TheBuildingFacilityPictureA, this.TheBuildingFacilityPictureADisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0196f);
                                        if (Switch45 == "on")
                                        {
                                            CacheManager.Draw(this.TheBuildingFacilityPictureB, this.TheBuildingFacilityPictureBDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0195f);
                                        }
                                        if (Switch46 == "on")
                                        {
                                            CacheManager.Draw(this.TheBuildingFacilityTextPicture, this.TheBuildingFacilityTextDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0194f);
                                        }
                                    }                                    
                                }
                                catch { }
                                CacheManager.Draw(this.FacilityDescriptionTextMask, this.FacilityDescriptionTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0185f);
                                CacheManager.Draw(this.FacilityDescriptionTextBackground, this.FacilityDescriptionTextBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.019f);
                                this.FacilityDescriptionText.Draw(0.0186f);                                   
                                
                            }                           
                        }
                    }                     
                }
            }
            CacheManager.Scale = Vector2.One;
        }

        internal void Initialize()
        {
            
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            CacheManager.Scale = Scale;
            if (Switch1 == "off")
            {
                if (StaticMethods.PointInRectangle(position, this.CharacteristicDisplayPosition))
                {
                    if (this.CharacteristicText.CurrentPageIndex < (this.CharacteristicText.PageCount - 1))
                    {
                        this.CharacteristicText.NextPage();
                    }
                    else if (this.CharacteristicText.CurrentPageIndex == (this.CharacteristicText.PageCount - 1))
                    {
                        this.CharacteristicText.FirstPage();
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.FacilityDisplayPosition))
                {
                    if (this.FacilityText.CurrentPageIndex < (this.FacilityText.PageCount - 1))
                    {
                        this.FacilityText.NextPage();
                    }
                    else if (this.FacilityText.CurrentPageIndex == (this.FacilityText.PageCount - 1))
                    {
                        this.FacilityText.FirstPage();
                    }
                }
            }

            ///////↓以下添加
            if (Switch1 == "on")
            {
                //
                if (Switch21 == "on")
                {
                    if (Switch24 == "on" && StaticMethods.PointInRectangle(position, this.TheCharacteristic1DisplayPosition))
                    {
                        if (this.TheCharacteristic1Text.CurrentPageIndex < (this.TheCharacteristic1Text.PageCount - 1))
                        {
                            this.TheCharacteristic1Text.NextPage();
                        }
                        else if (this.TheCharacteristic1Text.CurrentPageIndex == (this.TheCharacteristic1Text.PageCount - 1))
                        {
                            this.TheCharacteristic1Text.FirstPage();
                        }
                    }
                    else if (Switch25 == "on" && StaticMethods.PointInRectangle(position, this.TheFacility1DisplayPosition))
                    {
                        if (this.TheFacility1Text.CurrentPageIndex < (this.TheFacility1Text.PageCount - 1))
                        {
                            this.TheFacility1Text.NextPage();
                        }
                        else if (this.TheFacility1Text.CurrentPageIndex == (this.TheFacility1Text.PageCount - 1))
                        {
                            this.TheFacility1Text.FirstPage();
                        }
                    }
                }                
                if (Switch41 == "on" && Switch47 == "on")
                {
                    if (StaticMethods.PointInRectangle(position, this.TheFacility3DisplayPosition))
                    {
                        if (this.TheFacility3Text.CurrentPageIndex < (this.TheFacility3Text.PageCount - 1))
                        {
                            this.TheFacility3Text.NextPage();
                        }
                        else if (this.TheFacility3Text.CurrentPageIndex == (this.TheFacility3Text.PageCount - 1))
                        {
                            this.TheFacility3Text.FirstPage();
                        }
                    }                    
                }
                //
                bool AllFacilityButtonFalse = false;
                if (InformationButton == false && StaticMethods.PointInRectangle(position, this.InformationButtonDisplayPosition))
                {
                    if (Switch3 == "on")
                    {
                        Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                    }
                    InformationButton = true;
                    
                    FacilityButton = false;
                    if (Switch41 == "on") { AllFacilityButtonFalse = true; }
                }                
                else if (FacilityButton == false && StaticMethods.PointInRectangle(position, this.FacilityButtonDisplayPosition))
                {
                    if (Switch3 == "on")
                    {
                        Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Open");
                    }
                    InformationButton = false;
                     
                    FacilityButton = true;
                    if (TheFirstFacilityPage == 1) { FacilityforPage1Button = true; }
                    else if (TheFirstFacilityPage == 2) { FacilityforPage2Button = true; }
                    else if (TheFirstFacilityPage == 3) { FacilityforPage3Button = true; }
                    else if (TheFirstFacilityPage == 4) { FacilityforPage4Button = true; }
                    else if (TheFirstFacilityPage == 5) { FacilityforPage5Button = true; }
                    else if (TheFirstFacilityPage == 6) { FacilityforPage6Button = true; }
                    else if (TheFirstFacilityPage == 7) { FacilityforPage7Button = true; }
                    else if (TheFirstFacilityPage == 8) { FacilityforPage8Button = true; }
                    else if (TheFirstFacilityPage == 9) { FacilityforPage9Button = true; }
                    else if (TheFirstFacilityPage == 10) { FacilityforPage10Button = true; }
                    else if (TheFirstFacilityPage == 11) { FacilityforPage11Button = true; }
                    else if (TheFirstFacilityPage == 12) { FacilityforPage12Button = true; }
                    else if (TheFirstFacilityPage == 13) { FacilityforPage13Button = true; }
                    else if (TheFirstFacilityPage == 14) { FacilityforPage14Button = true; }
                    else if (TheFirstFacilityPage == 15) { FacilityforPage15Button = true; }
                    else if (TheFirstFacilityPage == 16) { FacilityforPage16Button = true; }
                    else if (TheFirstFacilityPage == 17) { FacilityforPage17Button = true; }
                    else if (TheFirstFacilityPage == 18) { FacilityforPage18Button = true; }
                    else if (TheFirstFacilityPage == 19) { FacilityforPage19Button = true; }
                    else if (TheFirstFacilityPage == 20) { FacilityforPage20Button = true; }
                    else if (TheFirstFacilityPage == 21) { FacilityforPage21Button = true; }
                    else if (TheFirstFacilityPage == 22) { FacilityforPage22Button = true; }
                    else if (TheFirstFacilityPage == 23) { FacilityforPage23Button = true; }
                    else if (TheFirstFacilityPage == 24) { FacilityforPage24Button = true; }
                    else if (TheFirstFacilityPage == 25) { FacilityforPage25Button = true; }
                    else if (TheFirstFacilityPage == 26) { FacilityforPage26Button = true; }
                    else if (TheFirstFacilityPage == 27) { FacilityforPage27Button = true; }
                    else if (TheFirstFacilityPage == 28) { FacilityforPage28Button = true; }
                    else if (TheFirstFacilityPage == 29) { FacilityforPage29Button = true; }
                    else if (TheFirstFacilityPage == 30) { FacilityforPage30Button = true; }
                }
                if (FacilityButton == true)
                {
                    bool TheButtonKind01False = false;
                    bool TheButtonKind11False = false;
                    bool TheButtonKind12False = false;
                    bool TheButtonKind02False = false;
                    bool TheButtonKind21False = false;
                    bool TheButtonKind22False = false;
                    bool TheButtonKind03False = false;
                    bool TheButtonKind31False = false;
                    bool TheButtonKind32False = false;
                    if (PageForFacilityPositionCount == "on")
                    {
                        if (FacilityforPage1Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(1)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage1Button = true;
                            FacilityforPage2Button = false;
                            FacilityforPage3Button = false;
                            FacilityforPage4Button = false;
                            FacilityforPage5Button = false;                          
                            TheButtonKind12False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage2Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(2)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage1Button = false;
                            FacilityforPage2Button = true;
                            FacilityforPage3Button = false;
                            FacilityforPage4Button = false;
                            FacilityforPage5Button = false;
                            TheButtonKind12False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage3Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(3)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage1Button = false;
                            FacilityforPage2Button = false;
                            FacilityforPage3Button = true;
                            FacilityforPage4Button = false;
                            FacilityforPage5Button = false;
                            TheButtonKind12False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage4Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(4)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage1Button = false;
                            FacilityforPage2Button = false;
                            FacilityforPage3Button = false;
                            FacilityforPage4Button = true;
                            FacilityforPage5Button = false;
                            TheButtonKind12False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage5Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(5)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage1Button = false;
                            FacilityforPage2Button = false;
                            FacilityforPage3Button = false;
                            FacilityforPage4Button = false;
                            FacilityforPage5Button = true;
                            TheButtonKind12False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage6Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(6)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage6Button = true;
                            FacilityforPage7Button = false;
                            FacilityforPage8Button = false;
                            FacilityforPage9Button = false;
                            FacilityforPage10Button = false;
                            TheButtonKind11False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage7Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(7)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage6Button = false;
                            FacilityforPage7Button = true;
                            FacilityforPage8Button = false;
                            FacilityforPage9Button = false;
                            FacilityforPage10Button = false;
                            TheButtonKind11False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage8Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(8)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage6Button = false;
                            FacilityforPage7Button = false;
                            FacilityforPage8Button = true;
                            FacilityforPage9Button = false;
                            FacilityforPage10Button = false;
                            TheButtonKind11False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage9Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(9)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage6Button = false;
                            FacilityforPage7Button = true;
                            FacilityforPage8Button = false;
                            FacilityforPage9Button = true;
                            FacilityforPage10Button = false;
                            TheButtonKind11False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage10Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(10)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage6Button = false;
                            FacilityforPage7Button = false;
                            FacilityforPage8Button = false;
                            FacilityforPage9Button = false;
                            FacilityforPage10Button = true;
                            TheButtonKind11False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                    }
                    if (PageForArchitectureKind == "on")
                    {
                        if (FacilityforPage11Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(11)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage11Button = true;
                            FacilityforPage12Button = false;
                            FacilityforPage13Button = false;
                            FacilityforPage14Button = false;
                            FacilityforPage15Button = false;
                            TheButtonKind22False = true;
                            TheButtonKind01False = true; 
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage12Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(12)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage11Button = false;
                            FacilityforPage12Button = true;
                            FacilityforPage13Button = false;
                            FacilityforPage14Button = false;
                            FacilityforPage15Button = false;
                            TheButtonKind22False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage13Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(13)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage11Button = false;
                            FacilityforPage12Button = false;
                            FacilityforPage13Button = true;
                            FacilityforPage14Button = false;
                            FacilityforPage15Button = false;
                            TheButtonKind22False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage14Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(14)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage11Button = false;
                            FacilityforPage12Button = true;
                            FacilityforPage13Button = false;
                            FacilityforPage14Button = true;
                            FacilityforPage15Button = false;
                            TheButtonKind22False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage15Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(15)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage11Button = false;
                            FacilityforPage12Button = false;
                            FacilityforPage13Button = false;
                            FacilityforPage14Button = false;
                            FacilityforPage15Button = true;
                            TheButtonKind22False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage16Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(16)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage16Button = true;
                            FacilityforPage17Button = false;
                            FacilityforPage18Button = false;
                            FacilityforPage19Button = false;
                            FacilityforPage20Button = false;
                            TheButtonKind21False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage17Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(17)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage16Button = false;
                            FacilityforPage17Button = true;
                            FacilityforPage18Button = false;
                            FacilityforPage19Button = false;
                            FacilityforPage20Button = false;
                            TheButtonKind21False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage18Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(18)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage16Button = false;
                            FacilityforPage17Button = false;
                            FacilityforPage18Button = true;
                            FacilityforPage19Button = false;
                            FacilityforPage20Button = false;
                            TheButtonKind21False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage19Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(19)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage16Button = false;
                            FacilityforPage17Button = true;
                            FacilityforPage18Button = false;
                            FacilityforPage19Button = true;
                            FacilityforPage20Button = false;
                            TheButtonKind21False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                        else if (FacilityforPage20Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(20)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage16Button = false;
                            FacilityforPage17Button = false;
                            FacilityforPage18Button = false;
                            FacilityforPage19Button = false;
                            FacilityforPage20Button = true;
                            TheButtonKind21False = true;
                            TheButtonKind01False = true;
                            if (Switch44 == "on") { TheButtonKind03False = true; }
                        }
                    }
                    if (PageForArchitectureCharacteristic == "on")
                    {
                        if (FacilityforPage21Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(21)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage21Button = true;
                            FacilityforPage22Button = false;
                            FacilityforPage23Button = false;
                            FacilityforPage24Button = false;
                            FacilityforPage25Button = false;
                            TheButtonKind32False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage22Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(22)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage21Button = false;
                            FacilityforPage22Button = true;
                            FacilityforPage23Button = false;
                            FacilityforPage24Button = false;
                            FacilityforPage25Button = false;
                            TheButtonKind32False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage23Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(23)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage21Button = false;
                            FacilityforPage22Button = false;
                            FacilityforPage23Button = true;
                            FacilityforPage24Button = false;
                            FacilityforPage25Button = false;
                            TheButtonKind32False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage24Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(24)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage21Button = false;
                            FacilityforPage22Button = true;
                            FacilityforPage23Button = false;
                            FacilityforPage24Button = true;
                            FacilityforPage25Button = false;
                            TheButtonKind32False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage25Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(25)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage21Button = false;
                            FacilityforPage22Button = false;
                            FacilityforPage23Button = false;
                            FacilityforPage24Button = false;
                            FacilityforPage25Button = true;
                            TheButtonKind32False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage26Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(26)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage26Button = true;
                            FacilityforPage27Button = false;
                            FacilityforPage28Button = false;
                            FacilityforPage29Button = false;
                            FacilityforPage30Button = false;
                            TheButtonKind31False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage27Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(27)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage26Button = false;
                            FacilityforPage27Button = true;
                            FacilityforPage28Button = false;
                            FacilityforPage29Button = false;
                            FacilityforPage30Button = false;
                            TheButtonKind31False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage28Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(28)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage26Button = false;
                            FacilityforPage27Button = false;
                            FacilityforPage28Button = true;
                            FacilityforPage29Button = false;
                            FacilityforPage30Button = false;
                            TheButtonKind31False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage29Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(29)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage26Button = false;
                            FacilityforPage27Button = true;
                            FacilityforPage28Button = false;
                            FacilityforPage29Button = true;
                            FacilityforPage30Button = false;
                            TheButtonKind31False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                        else if (FacilityforPage30Button == false && StaticMethods.PointInRectangle(position, this.TheFacilityforPageButtonDisplayPosition(30)))
                        {
                            if (Switch3 == "on")
                            {
                                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Select");
                            }
                            FacilityforPage26Button = false;
                            FacilityforPage27Button = false;
                            FacilityforPage28Button = false;
                            FacilityforPage29Button = false;
                            FacilityforPage30Button = true;
                            TheButtonKind31False = true;
                            TheButtonKind01False = true;
                            if (Switch43 == "on") { TheButtonKind02False = true; }
                        }
                    }
                    if (TheButtonKind01False == true)
                    {
                        FacilityforPage11Button = false;
                        FacilityforPage12Button = false;
                        FacilityforPage13Button = false;
                        FacilityforPage14Button = false;
                        FacilityforPage15Button = false;
                        FacilityforPage16Button = false;
                        FacilityforPage17Button = false;
                        FacilityforPage18Button = false;
                        FacilityforPage19Button = false;
                        FacilityforPage20Button = false;
                        FacilityforPage21Button = false;
                        FacilityforPage22Button = false;
                        FacilityforPage23Button = false;
                        FacilityforPage24Button = false;
                        FacilityforPage25Button = false;
                        FacilityforPage26Button = false;
                        FacilityforPage27Button = false;
                        FacilityforPage28Button = false;
                        FacilityforPage29Button = false;
                        FacilityforPage30Button = false;
                    }
                    if (TheButtonKind02False == true)
                    {
                        FacilityforPage1Button = false;
                        FacilityforPage2Button = false;
                        FacilityforPage3Button = false;
                        FacilityforPage4Button = false;
                        FacilityforPage5Button = false;
                        FacilityforPage6Button = false;
                        FacilityforPage7Button = false;
                        FacilityforPage8Button = false;
                        FacilityforPage9Button = false;
                        FacilityforPage10Button = false;
                        FacilityforPage21Button = false;
                        FacilityforPage22Button = false;
                        FacilityforPage23Button = false;
                        FacilityforPage24Button = false;
                        FacilityforPage25Button = false;
                        FacilityforPage26Button = false;
                        FacilityforPage27Button = false;
                        FacilityforPage28Button = false;
                        FacilityforPage29Button = false;
                        FacilityforPage30Button = false;
                    }
                    if (TheButtonKind03False == true)
                    {
                        FacilityforPage1Button = false;
                        FacilityforPage2Button = false;
                        FacilityforPage3Button = false;
                        FacilityforPage4Button = false;
                        FacilityforPage5Button = false;
                        FacilityforPage6Button = false;
                        FacilityforPage7Button = false;
                        FacilityforPage8Button = false;
                        FacilityforPage9Button = false;
                        FacilityforPage10Button = false;
                        FacilityforPage11Button = false;
                        FacilityforPage12Button = false;
                        FacilityforPage13Button = false;
                        FacilityforPage14Button = false;
                        FacilityforPage15Button = false;
                        FacilityforPage16Button = false;
                        FacilityforPage17Button = false;
                        FacilityforPage18Button = false;
                        FacilityforPage19Button = false;
                        FacilityforPage20Button = false;
                    }
                    if (TheButtonKind11False == true)
                    {
                        FacilityforPage1Button = false;
                        FacilityforPage2Button = false;
                        FacilityforPage3Button = false;
                        FacilityforPage4Button = false;
                        FacilityforPage5Button = false;
                    }
                    if (TheButtonKind12False == true)
                    {
                        FacilityforPage6Button = false;
                        FacilityforPage7Button = false;
                        FacilityforPage8Button = false;
                        FacilityforPage9Button = false;
                        FacilityforPage10Button = false;
                    }
                    if (TheButtonKind21False == true)
                    {
                        FacilityforPage11Button = false;
                        FacilityforPage12Button = false;
                        FacilityforPage13Button = false;
                        FacilityforPage14Button = false;
                        FacilityforPage15Button = false;
                    }
                    if (TheButtonKind22False == true)
                    {
                        FacilityforPage16Button = false;
                        FacilityforPage17Button = false;
                        FacilityforPage18Button = false;
                        FacilityforPage19Button = false;
                        FacilityforPage20Button = false;
                    }
                    if (TheButtonKind31False == true)
                    {
                        FacilityforPage21Button = false;
                        FacilityforPage22Button = false;
                        FacilityforPage23Button = false;
                        FacilityforPage24Button = false;
                        FacilityforPage25Button = false;
                    }
                    if (TheButtonKind32False == true)
                    {
                        FacilityforPage26Button = false;
                        FacilityforPage27Button = false;
                        FacilityforPage28Button = false;
                        FacilityforPage29Button = false;
                        FacilityforPage30Button = false;
                    }
                }
                if (AllFacilityButtonFalse == true)
                {
                    FacilityforPage1Button = false;
                    FacilityforPage2Button = false;
                    FacilityforPage3Button = false;
                    FacilityforPage4Button = false;
                    FacilityforPage5Button = false;
                    FacilityforPage6Button = false;
                    FacilityforPage7Button = false;
                    FacilityforPage8Button = false;
                    FacilityforPage9Button = false;
                    FacilityforPage10Button = false;
                    FacilityforPage11Button = false;
                    FacilityforPage12Button = false;
                    FacilityforPage13Button = false;
                    FacilityforPage14Button = false;
                    FacilityforPage15Button = false;
                    FacilityforPage16Button = false;
                    FacilityforPage17Button = false;
                    FacilityforPage18Button = false;
                    FacilityforPage19Button = false;
                    FacilityforPage20Button = false;
                    FacilityforPage21Button = false;
                    FacilityforPage22Button = false;
                    FacilityforPage23Button = false;
                    FacilityforPage24Button = false;
                    FacilityforPage25Button = false;
                    FacilityforPage26Button = false;
                    FacilityforPage27Button = false;
                    FacilityforPage28Button = false;
                    FacilityforPage29Button = false;
                    FacilityforPage30Button = false;
                }
            }
            CacheManager.Scale = Vector2.One;
            ////////
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            CacheManager.Scale = Scale;
            if (FacilityButton == true)
            {
                bool flag1 = false;
                if (!flag1)
                {
                    if (FacilityDescriptionTextIng == false)
                    {
                        for (int t = 1; t <= TheAllFacilityNumber; t++)
                        {
                            if (FacilityButton == true && StaticMethods.PointInRectangle(position, this.TheFacilityTextShowDisplayPosition(t)))
                            {
                                
                                if (FacilityDescriptionTextFollowTheMouse == "on")
                                {
                                    this.FacilityDescriptionText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.FacilityDescriptionTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.FacilityDescriptionTextClient.Y);
                                }                                
                                this.FacilityDescriptionText.Clear();
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText1, this.FacilityDescriptionText.TitleColor);
                                this.FacilityDescriptionText.AddNewLine();
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText2);
                                this.FacilityDescriptionText.AddText(this.TheFacilityName(t), this.FacilityDescriptionText.SubTitleColor);
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText3);
                                this.FacilityDescriptionText.AddNewLine();
                                if (this.TheFacilityCount(t) >= 2)
                                {
                                    this.FacilityDescriptionText.AddText(FacilityDescriptionText4, this.FacilityDescriptionText.TitleColor);
                                    this.FacilityDescriptionText.AddText(FacilityDescriptionText5);
                                    this.FacilityDescriptionText.AddText(this.TheFacilityCount(t).ToString(), this.FacilityDescriptionText.SubTitleColor2);
                                    this.FacilityDescriptionText.AddText(FacilityDescriptionText6);
                                    this.FacilityDescriptionText.AddNewLine();
                                }
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText7, this.FacilityDescriptionText.TitleColor);
                                this.FacilityDescriptionText.AddNewLine();
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText8);
                                this.FacilityDescriptionText.AddText(this.TheFacilityDescription(t), this.FacilityDescriptionText.SubTitleColor3);
                                this.FacilityDescriptionText.AddText(FacilityDescriptionText9);
                                this.FacilityDescriptionText.AddNewLine();
                                this.FacilityDescriptionText.ResortTexts();
                                TheFacilityIDForDescription = this.TheFacilityKind(t);
                                FacilityDescriptionTextIng = true;
                                FacilityDescriptionTexting = true;
                            }
                        }
                        if (FacilityButton == true && HasBuildingFacility == true && StaticMethods.PointInRectangle(position, this.TheBuildingFacilityTextDisplayPosition))
                        {
                            if (FacilityDescriptionTextFollowTheMouse == "on")
                            {
                                this.FacilityDescriptionText.DisplayOffset = new Point(Session.MainGame.mainGameScreen.MousePosition.X + this.FacilityDescriptionTextClient.X, Session.MainGame.mainGameScreen.MousePosition.Y + this.FacilityDescriptionTextClient.Y);
                            }
                            this.FacilityDescriptionText.Clear();
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText1, this.FacilityDescriptionText.TitleColor);
                            this.FacilityDescriptionText.AddNewLine();
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText2);
                            this.FacilityDescriptionText.AddText(this.TheBuildingFacilityName, this.FacilityDescriptionText.SubTitleColor);
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText3);
                            this.FacilityDescriptionText.AddNewLine();
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText7, this.FacilityDescriptionText.TitleColor);
                            this.FacilityDescriptionText.AddNewLine();
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText10, this.FacilityDescriptionText.SubTitleColor3);
                            this.FacilityDescriptionText.AddNewLine();
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText11);
                            this.FacilityDescriptionText.AddText(this.TheBuildingFacilityDay, this.FacilityDescriptionText.SubTitleColor2);
                            this.FacilityDescriptionText.AddText(FacilityDescriptionText12);
                            this.FacilityDescriptionText.AddNewLine();
                            this.FacilityDescriptionText.ResortTexts();
                            TheFacilityIDForDescription = this.TheBuildingFacilityID;
                            FacilityDescriptionTextIng = true;
                            FacilityDescriptionTexting = true;
                        }
                        flag1 = true;
                    }
                }
                if (!flag1)
                {
                    if (FacilityDescriptionTexting == true)
                    {
                        FacilityDescriptionTextIng = false;
                        FacilityDescriptionTexting = false;
                        TheFacilityIDForDescription = -1;
                        FacilityDescriptionText.Clear();
                    }
                }
            }
            CacheManager.Scale = Vector2.One;
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (Switch3 == "on")
            {
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Close");
            }
            this.IsShowing = false;
        }

        internal void SetArchitecture(Architecture architecture)
        {
            this.ShowingArchitecture = architecture;
            if (Switch1 == "off")
            {
                foreach (LabelText text in this.LabelTexts)
                {
                    text.Text.Text = StaticMethods.GetPropertyValue(architecture, text.PropertyName).ToString();
                }
                this.CharacteristicText.AddText("特色", this.CharacteristicText.TitleColor);
                this.CharacteristicText.AddNewLine();
                foreach (Influence influence in this.ShowingArchitecture.Characteristics.Influences.Values)
                {
                    this.CharacteristicText.AddText(influence.Description, this.CharacteristicText.SubTitleColor);
                    this.CharacteristicText.AddNewLine();
                }
                this.CharacteristicText.ResortTexts();
                this.FacilityText.AddText("设施", this.FacilityText.TitleColor);
                this.FacilityText.AddNewLine();
                if (this.ShowingArchitecture.BuildingFacility >= 0)
                {
                    FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.ShowingArchitecture.BuildingFacility);
                    if (facilityKind != null)
                    {
                        this.FacilityText.AddText("建造中：");
                        this.FacilityText.AddText(facilityKind.Name, this.FacilityText.SubTitleColor);
                        this.FacilityText.AddText("，剩余时间：");
                        this.FacilityText.AddText((this.ShowingArchitecture.BuildingDaysLeft * Session.Parameters.DayInTurn).ToString(), this.FacilityText.SubTitleColor2);
                        this.FacilityText.AddText("天");
                        this.FacilityText.AddNewLine();
                    }
                }
                this.FacilityText.AddText("已有设施" + this.ShowingArchitecture.Facilities.Count + "个", this.FacilityText.SubTitleColor3);
                this.FacilityText.AddNewLine();
                foreach (Facility facility in this.ShowingArchitecture.Facilities)
                {
                    this.FacilityText.AddText(facility.Name, this.FacilityText.SubTitleColor);
                    this.FacilityText.AddText("，占用空间：");
                    this.FacilityText.AddText(facility.PositionOccupied.ToString(), this.FacilityText.SubTitleColor2);
                    this.FacilityText.AddText("，维持费用：");
                    this.FacilityText.AddText(facility.MaintenanceCost.ToString(), this.FacilityText.SubTitleColor2);
                    this.FacilityText.AddNewLine();
                }
                this.FacilityText.ResortTexts();
            }
            if (Switch1 == "on")
            {
                TheFacilityPositionCount = this.ShowingArchitecture.FacilityPositionCount;
                ArchitectureKind = this.ShowingArchitecture.Kind.ID;
                ArchitectureID = this.ShowingArchitecture.ID;
                TheCharacteristicCount = this.ShowingArchitecture.Characteristics.Count;
                TheMaxCharacteristicCount = TheCharacteristicShowXNumber * TheCharacteristicShowYNumber;
                if (TheCharacteristicCount <= TheMaxCharacteristicCount) { TheMaxShowCharacteristicCount = TheCharacteristicCount; }
                else { TheMaxShowCharacteristicCount = TheMaxCharacteristicCount; }
                if (TheMaxShowCharacteristicCount > 30) { TheMaxShowCharacteristicCount = 30; }
                InformationButton = true;                 
                FacilityButton = false;                
                
                if (Switch21 == "on")
                {
                    foreach (LabelText text in this.ArchitectureInInformationTexts)
                    {
                        text.Text.Text = StaticMethods.GetPropertyValue(architecture, text.PropertyName).ToString();
                    }
                    if (Switch23 == "on")
                    {
                        TheCharacteristicShowID1 = TheCharacteristicShowID(1);
                        TheCharacteristicShowID2 = TheCharacteristicShowID(2);
                        TheCharacteristicShowID3 = TheCharacteristicShowID(3);
                        TheCharacteristicShowID4 = TheCharacteristicShowID(4);
                        TheCharacteristicShowID5 = TheCharacteristicShowID(5);
                        TheCharacteristicShowID6 = TheCharacteristicShowID(6);
                        TheCharacteristicShowID7 = TheCharacteristicShowID(7);
                        TheCharacteristicShowID8 = TheCharacteristicShowID(8);
                        TheCharacteristicShowID9 = TheCharacteristicShowID(9);
                        TheCharacteristicShowID10 = TheCharacteristicShowID(10);
                        TheCharacteristicShowID11 = TheCharacteristicShowID(11);
                        TheCharacteristicShowID12 = TheCharacteristicShowID(12);
                        TheCharacteristicShowID13 = TheCharacteristicShowID(13);
                        TheCharacteristicShowID14 = TheCharacteristicShowID(14);
                        TheCharacteristicShowID15 = TheCharacteristicShowID(15);
                        TheCharacteristicShowID16 = TheCharacteristicShowID(16);
                        TheCharacteristicShowID17 = TheCharacteristicShowID(17);
                        TheCharacteristicShowID18 = TheCharacteristicShowID(18);
                        TheCharacteristicShowID19 = TheCharacteristicShowID(19);
                        TheCharacteristicShowID20 = TheCharacteristicShowID(20);
                        TheCharacteristicShowID21 = TheCharacteristicShowID(21);
                        TheCharacteristicShowID22 = TheCharacteristicShowID(22);
                        TheCharacteristicShowID23 = TheCharacteristicShowID(23);
                        TheCharacteristicShowID24 = TheCharacteristicShowID(24);
                        TheCharacteristicShowID25 = TheCharacteristicShowID(25);
                        TheCharacteristicShowID26 = TheCharacteristicShowID(26);
                        TheCharacteristicShowID27 = TheCharacteristicShowID(27);
                        TheCharacteristicShowID28 = TheCharacteristicShowID(28);
                        TheCharacteristicShowID29 = TheCharacteristicShowID(29);
                        TheCharacteristicShowID30 = TheCharacteristicShowID(30);
                    }
                    if (Switch24 == "on")
                    {
                        this.TheCharacteristic1Text.AddText("特色", this.TheCharacteristic1Text.TitleColor);
                        this.TheCharacteristic1Text.AddNewLine();
                        foreach (Influence influence in this.ShowingArchitecture.Characteristics.Influences.Values)
                        {
                            this.TheCharacteristic1Text.AddText(influence.Description, this.TheCharacteristic1Text.SubTitleColor);
                            this.TheCharacteristic1Text.AddNewLine();
                        }
                        this.TheCharacteristic1Text.ResortTexts();
                    }
                    if (Switch25 == "on")
                    {
                        this.TheFacility1Text.AddText("设施", this.TheFacility1Text.TitleColor);
                        this.TheFacility1Text.AddNewLine();
                        if (this.ShowingArchitecture.BuildingFacility >= 0)
                        {
                            FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.ShowingArchitecture.BuildingFacility);
                            if (facilityKind != null)
                            {
                                this.TheFacility1Text.AddText("建造中：");
                                this.TheFacility1Text.AddText(facilityKind.Name, this.TheFacility1Text.SubTitleColor);
                                this.TheFacility1Text.AddText("，剩余时间：");
                                this.TheFacility1Text.AddText((this.ShowingArchitecture.BuildingDaysLeft * Session.Parameters.DayInTurn).ToString(), this.TheFacility1Text.SubTitleColor2);
                                this.TheFacility1Text.AddText("天");
                                this.TheFacility1Text.AddNewLine();
                            }
                        }
                        this.TheFacility1Text.AddText("已有设施" + this.ShowingArchitecture.Facilities.Count + "个", this.TheFacility1Text.SubTitleColor3);
                        this.TheFacility1Text.AddNewLine();
                        foreach (Facility facility in this.ShowingArchitecture.Facilities)
                        {
                            this.TheFacility1Text.AddText(facility.Name, this.TheFacility1Text.SubTitleColor);
                            this.TheFacility1Text.AddText("，占用空间：");
                            this.TheFacility1Text.AddText(facility.PositionOccupied.ToString(), this.TheFacility1Text.SubTitleColor2);
                            this.TheFacility1Text.AddText("，维持费用：");
                            this.TheFacility1Text.AddText(facility.MaintenanceCost.ToString(), this.TheFacility1Text.SubTitleColor2);
                            this.TheFacility1Text.AddNewLine();
                        }
                        this.TheFacility1Text.ResortTexts();
                    }
                }                
                if (Switch22 == "on")
                {
                    int Bar1; int Bar2; int Bar3; int Bar4; int Bar5; int Bar6; int Bar7; int Bar8;
                    int A2; int A3; int A4; int A5; int A6; int A7; 
                    if (this.ShowingArchitecture.Domination == 0 || this.ShowingArchitecture.DominationCeiling == 0) { Bar2 = 0; A2 = 0; }
                    else { Bar2 = 100 * this.ShowingArchitecture.Domination / this.ShowingArchitecture.DominationCeiling; A2 = 100; }
                    if (this.ShowingArchitecture.Endurance == 0 || this.ShowingArchitecture.EnduranceCeiling == 0) { Bar3 = 0; A3 = 0; }
                    else { Bar3 = 100 * this.ShowingArchitecture.Domination / this.ShowingArchitecture.EnduranceCeiling; A3 = 100; }
                    if (this.ShowingArchitecture.Agriculture == 0 || this.ShowingArchitecture.AgricultureCeiling == 0) { Bar4 = 0; A4 = 0; }
                    else { Bar4 = 100 * this.ShowingArchitecture.Agriculture / this.ShowingArchitecture.AgricultureCeiling; A4 = 100; }
                    if (this.ShowingArchitecture.Commerce == 0 || this.ShowingArchitecture.CommerceCeiling == 0) { Bar5 = 0; A5 = 0; }
                    else { Bar5 = 100 * this.ShowingArchitecture.Commerce / this.ShowingArchitecture.CommerceCeiling; A5 = 100; }
                    if (this.ShowingArchitecture.Technology == 0 || this.ShowingArchitecture.TechnologyCeiling == 0) { Bar6 = 0; A6 = 0; }
                    else { Bar6 = 100 * this.ShowingArchitecture.Technology / this.ShowingArchitecture.TechnologyCeiling; A6 = 100; }
                    if (this.ShowingArchitecture.Morale == 0 || this.ShowingArchitecture.MoraleCeiling == 0) { Bar7 = 0; A7 = 0; }
                    else { Bar7 = 100 * this.ShowingArchitecture.Morale / this.ShowingArchitecture.MoraleCeiling; A7 = 100; }
                    if (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft == 0 || this.ShowingArchitecture.FacilityPositionCount == 0) { Bar8 = 0; }
                    else { Bar8 = 100 * (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft) / this.ShowingArchitecture.FacilityPositionCount; }
                    Integration = Bar2 + Bar3 + Bar4 + Bar5 + Bar6 + Bar7;
                    AllIntegration = A2 + A3 + A4 + A5 + A6 + A7;
                    if (AllIntegration == 0) { Bar1 = 0; }
                    else { Bar1 = 100 * Integration / AllIntegration; }
                    IntegrationBarTexture = Integration6BarTexture;
                    if (Bar1 < 10) { IntegrationBarTexture = Integration1BarTexture; }
                    else if (Bar1 < 20) { IntegrationBarTexture = Integration2BarTexture; }
                    else if (Bar1 < 50) { IntegrationBarTexture = Integration3BarTexture; }
                    else if (Bar1 < 80) { IntegrationBarTexture = Integration4BarTexture; }
                    else if (Bar1 < 100) { IntegrationBarTexture = Integration5BarTexture; }
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
                    else if (Bar3 < 80) { EnduranceBarTexture = Endurance4BarTexture;  }
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
                if (Switch41 == "on")
                {                    
                    FacilityforPage1Button = false;
                    FacilityforPage2Button = false;
                    FacilityforPage3Button = false;
                    FacilityforPage4Button = false;
                    FacilityforPage5Button = false;
                    FacilityforPage6Button = false;
                    FacilityforPage7Button = false;
                    FacilityforPage8Button = false;
                    FacilityforPage9Button = false;
                    FacilityforPage10Button = false;
                    FacilityforPage11Button = false;
                    FacilityforPage12Button = false;
                    FacilityforPage13Button = false;
                    FacilityforPage14Button = false;
                    FacilityforPage15Button = false;
                    FacilityforPage16Button = false;
                    FacilityforPage17Button = false;
                    FacilityforPage18Button = false;
                    FacilityforPage19Button = false;
                    FacilityforPage20Button = false;
                    FacilityforPage21Button = false;
                    FacilityforPage22Button = false;
                    FacilityforPage23Button = false;
                    FacilityforPage24Button = false;
                    FacilityforPage25Button = false;
                    FacilityforPage26Button = false;
                    FacilityforPage27Button = false;
                    FacilityforPage28Button = false;
                    FacilityforPage29Button = false;
                    FacilityforPage30Button = false;
                    if (TheFirstFacilityPage == 1) { FacilityforPage1Button = true; }
                    else if (TheFirstFacilityPage == 2) { FacilityforPage2Button = true; }
                    else if (TheFirstFacilityPage == 3) { FacilityforPage3Button = true; }
                    else if (TheFirstFacilityPage == 4) { FacilityforPage4Button = true; }
                    else if (TheFirstFacilityPage == 5) { FacilityforPage5Button = true; }
                    else if (TheFirstFacilityPage == 6) { FacilityforPage6Button = true; }
                    else if (TheFirstFacilityPage == 7) { FacilityforPage7Button = true; }
                    else if (TheFirstFacilityPage == 8) { FacilityforPage8Button = true; }
                    else if (TheFirstFacilityPage == 9) { FacilityforPage9Button = true; }
                    else if (TheFirstFacilityPage == 10) { FacilityforPage10Button = true; }
                    else if (TheFirstFacilityPage == 11) { FacilityforPage11Button = true; }
                    else if (TheFirstFacilityPage == 12) { FacilityforPage12Button = true; }
                    else if (TheFirstFacilityPage == 13) { FacilityforPage13Button = true; }
                    else if (TheFirstFacilityPage == 14) { FacilityforPage14Button = true; }
                    else if (TheFirstFacilityPage == 15) { FacilityforPage15Button = true; }
                    else if (TheFirstFacilityPage == 16) { FacilityforPage16Button = true; }
                    else if (TheFirstFacilityPage == 17) { FacilityforPage17Button = true; }
                    else if (TheFirstFacilityPage == 18) { FacilityforPage18Button = true; }
                    else if (TheFirstFacilityPage == 19) { FacilityforPage19Button = true; }
                    else if (TheFirstFacilityPage == 20) { FacilityforPage20Button = true; }
                    else if (TheFirstFacilityPage == 21) { FacilityforPage21Button = true; }
                    else if (TheFirstFacilityPage == 22) { FacilityforPage22Button = true; }
                    else if (TheFirstFacilityPage == 23) { FacilityforPage23Button = true; }
                    else if (TheFirstFacilityPage == 24) { FacilityforPage24Button = true; }
                    else if (TheFirstFacilityPage == 25) { FacilityforPage25Button = true; }
                    else if (TheFirstFacilityPage == 26) { FacilityforPage26Button = true; }
                    else if (TheFirstFacilityPage == 27) { FacilityforPage27Button = true; }
                    else if (TheFirstFacilityPage == 28) { FacilityforPage28Button = true; }
                    else if (TheFirstFacilityPage == 29) { FacilityforPage29Button = true; }
                    else if (TheFirstFacilityPage == 30) { FacilityforPage30Button = true; }
                    HasBuildingFacility = false;
                    TheAllFacilityNumber = AllFacilityNumber;
                    PageForFacilityPositionCount = "false";
                    if (TheFacilityPositionCount > 0) { PageForFacilityPositionCount = "on"; }
                    PageForArchitectureKind = Switch43;
                    PageForArchitectureCharacteristic = Switch44;
                    if (this.ShowingArchitecture.BuildingFacility >= 0)
                    {                        
                        FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.ShowingArchitecture.BuildingFacility);
                        if (facilityKind != null)
                        {
                            HasBuildingFacility = true;
                            TheBuildingFacilityID=facilityKind.ID;
                            TheBuildingFacilityName = facilityKind.Name;
                            TheBuildingFacilityDay = (this.ShowingArchitecture.BuildingDaysLeft * Session.Parameters.DayInTurn).ToString();
                        }
                    }
                    if (Switch42 == "on")
                    {
                        foreach (LabelText text in this.ArchitectureInFacilityTexts)
                        {
                            text.Text.Text = StaticMethods.GetPropertyValue(architecture, text.PropertyName).ToString();
                        }                        
                    }
                    if (Switch47 == "on")
                    {                                               
                        if (this.ShowingArchitecture.BuildingFacility >= 0)
                        {
                            FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.ShowingArchitecture.BuildingFacility);
                            if (facilityKind != null)
                            {
                                this.TheFacility3Text.AddText(this.TheFacility3Text1,this.TheFacility3Text.TitleColor);
                                this.TheFacility3Text.AddNewLine();
                                this.TheFacility3Text.AddText(this.TheFacility3Text2);
                                this.TheFacility3Text.AddText(facilityKind.Name, this.TheFacility3Text.SubTitleColor);
                                this.TheFacility3Text.AddText(this.TheFacility3Text3);
                                this.TheFacility3Text.AddText((this.ShowingArchitecture.BuildingDaysLeft * Session.Parameters.DayInTurn).ToString(), this.TheFacility3Text.SubTitleColor2);
                                this.TheFacility3Text.AddText(this.TheFacility3Text4);
                                this.TheFacility3Text.AddNewLine();
                            }
                        }
                        this.TheFacility3Text.AddText(this.TheFacility3Text5, this.TheFacility3Text.TitleColor);
                        if (ShowFacilityAllCount == "on")
                        {
                            this.TheFacility3Text.AddText(this.TheFacility3Text6);
                            this.TheFacility3Text.AddText(this.ShowingArchitecture.Facilities.Count.ToString(), this.TheFacility3Text.SubTitleColor3);
                            this.TheFacility3Text.AddText(this.TheFacility3Text7);
                        }
                        this.TheFacility3Text.AddNewLine();
                        this.TheFacility3Text.AddText(this.TheFacility3Text8);
                        this.TheFacility3Text.AddNewLine();
                        for (int i = 1; i <= TheAllFacilityNumber; i++)
                        {
                            if (this.HasTheFacilityKind(TheFacilityKind(i)) == true) 
                            {
                                this.TheFacility3Text.AddText(this.TheFacilityName(TheFacilityKind(i)), this.TheFacility3Text.SubTitleColor);
                                if (ShowFacilityCount == "on")
                                {
                                    this.TheFacility3Text.AddText(this.TheFacility3Text10);
                                    this.TheFacility3Text.AddText(this.TheFacilityCount(TheFacilityKind(i)).ToString(), this.TheFacility3Text.SubTitleColor3);
                                    this.TheFacility3Text.AddText(this.TheFacility3Text11);
                                }
                                if (ShowPositionOccupied == "on")
                                {
                                    this.TheFacility3Text.AddText(this.TheFacility3Text12);
                                    this.TheFacility3Text.AddText(this.TheFacilityPositionOccupied(TheFacilityKind(i)), this.TheFacility3Text.SubTitleColor3);
                                    this.TheFacility3Text.AddText(this.TheFacility3Text13);
                                }
                                if (ShowMaintenanceCost == "on")
                                {
                                    this.TheFacility3Text.AddText(this.TheFacility3Text14);
                                    this.TheFacility3Text.AddText(this.TheFacilityMaintenanceCost(TheFacilityKind(i)), this.TheFacility3Text.SubTitleColor3);
                                    this.TheFacility3Text.AddText(this.TheFacility3Text15);
                                }
                                if (ShowFacilityDescription == "on")
                                {
                                    this.TheFacility3Text.AddText(this.TheFacility3Text16);
                                    this.TheFacility3Text.AddText(this.TheFacilityDescription(TheFacilityKind(i)), this.TheFacility3Text.SubTitleColor3);
                                    this.TheFacility3Text.AddText(this.TheFacility3Text17);
                                }
                                this.TheFacility3Text.AddNewLine();
                            }
                            
                        }

                        /*
                        foreach (Facility facility in this.ShowingArchitecture.Facilities)
                        {
                            this.TheFacility3Text.AddText(facility.Name, this.TheFacility2Text.SubTitleColor);
                            if (ShowFacilityCount == "on")
                            {
                                int ID = facility.KindID;
                                this.TheFacility3Text.AddText(this.TheFacility3Text10);
                                this.TheFacility3Text.AddText(this.ShowingArchitecture.GetFacilityCountForKind(ID).ToString(), this.TheFacility3Text.SubTitleColor3);
                                this.TheFacility3Text.AddText(this.TheFacility3Text11);
                            }
                            if (ShowPositionOccupied == "on")
                            {   
                                this.TheFacility3Text.AddText(this.TheFacility3Text12);
                                this.TheFacility3Text.AddText(facility.PositionOccupied.ToString(), this.TheFacility3Text.SubTitleColor3);
                                this.TheFacility3Text.AddText(this.TheFacility3Text13);
                            }
                            if (ShowMaintenanceCost == "on")
                            {
                                this.TheFacility3Text.AddText(this.TheFacility3Text14);
                                this.TheFacility3Text.AddText(facility.MaintenanceCost.ToString(), this.TheFacility3Text.SubTitleColor3);
                                this.TheFacility3Text.AddText(this.TheFacility3Text15);
                            }
                            if (ShowFacilityDescription == "on")
                            {
                                this.TheFacility3Text.AddText(this.TheFacility3Text16);
                                this.TheFacility3Text.AddText(facility.Description, this.TheFacility3Text.SubTitleColor3);
                                this.TheFacility3Text.AddText(this.TheFacility3Text17);
                            }
                            this.TheFacility3Text.AddNewLine();
                        }
                         */ 
                        this.TheFacility3Text.AddText(this.TheFacility3Text9);
                        this.TheFacility3Text.ResortTexts();
                    }
                }
                
            }
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
            this.DisplayOffset = new Point(rect.X, rect.Y + 34);

            //this.DisplayOffset = new Point(Convert.ToInt32(rect.X / InputManager.Scale2.X), Convert.ToInt32(rect.Y / InputManager.Scale2.Y));
            foreach (LabelText text in this.LabelTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.CharacteristicText.DisplayOffset = new Point(this.DisplayOffset.X + this.CharacteristicClient.X, this.DisplayOffset.Y + this.CharacteristicClient.Y);
            this.FacilityText.DisplayOffset = new Point(this.DisplayOffset.X + this.FacilityClient.X, this.DisplayOffset.Y + this.FacilityClient.Y);
            /////以下添加
            foreach (LabelText text in this.ArchitectureInInformationTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.TheCharacteristic1Text.DisplayOffset = new Point(this.DisplayOffset.X + this.TheCharacteristic1Client.X, this.DisplayOffset.Y + this.TheCharacteristic1Client.Y);
            this.TheFacility1Text.DisplayOffset = new Point(this.DisplayOffset.X + this.TheFacility1Client.X, this.DisplayOffset.Y + this.TheFacility1Client.Y);
            
            foreach (LabelText text in this.ArchitectureInFacilityTexts)
            {
                text.Label.DisplayOffset = this.DisplayOffset;
                text.Text.DisplayOffset = this.DisplayOffset;
            }
            this.FacilityDescriptionText.DisplayOffset = new Point(this.DisplayOffset.X + this.FacilityDescriptionTextClient.X, this.DisplayOffset.Y + this.FacilityDescriptionTextClient.Y);
            this.TheFacility3Text.DisplayOffset = new Point(this.DisplayOffset.X + this.TheFacility3Client.X, this.DisplayOffset.Y + this.TheFacility3Client.Y);   
            /////以上添加
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle CharacteristicDisplayPosition
        {
            get
            {
                return new Rectangle(this.CharacteristicText.DisplayOffset.X, this.CharacteristicText.DisplayOffset.Y, this.CharacteristicText.ClientWidth, this.CharacteristicText.ClientHeight);
            }
        }

        private Rectangle FacilityDisplayPosition
        {
            get
            {
                return new Rectangle(this.FacilityText.DisplayOffset.X, this.FacilityText.DisplayOffset.Y, this.FacilityText.ClientWidth, this.FacilityText.ClientHeight);
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
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.ArchitectureDetail));
                    Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                }
                else
                {
                    if (Session.MainGame.mainGameScreen.PopUndoneWork().Kind != UndoneWorkKind.SubDialog)
                    {
                        throw new Exception("The UndoneWork is not a SubDialog.");
                    }
                    Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.CharacteristicText.Clear();
                    this.FacilityText.Clear();
                    this.TheCharacteristic1Text.Clear();
                    this.TheFacility1Text.Clear();
                   
                    this.TheFacility3Text.Clear();
                }
            }
        }
        //////////以下添加
        ////
        private Rectangle TheCharacteristic1DisplayPosition
        {
            get
            {
                return new Rectangle(this.TheCharacteristic1Text.DisplayOffset.X, this.TheCharacteristic1Text.DisplayOffset.Y, this.TheCharacteristic1Text.ClientWidth, this.TheCharacteristic1Text.ClientHeight);
            }
        }
        private Rectangle TheFacility1DisplayPosition
        {
            get
            {
                return new Rectangle(this.TheFacility1Text.DisplayOffset.X, this.TheFacility1Text.DisplayOffset.Y, this.TheFacility1Text.ClientWidth, this.TheFacility1Text.ClientHeight);
            }
        }
   
        private Rectangle TheFacility3DisplayPosition
        {
            get
            {
                return new Rectangle(this.TheFacility3Text.DisplayOffset.X, this.TheFacility3Text.DisplayOffset.Y, this.TheFacility3Text.ClientWidth, this.TheFacility3Text.ClientHeight);
            }
        }
        private Rectangle FacilityDescriptionTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.FacilityDescriptionText.DisplayOffset.X, this.FacilityDescriptionText.DisplayOffset.Y, this.FacilityDescriptionText.ClientWidth, this.FacilityDescriptionText.ClientHeight);
            }
        }
        ////
        private Rectangle NullDisplayPosition
        {
            get
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }       
        ////
        private Rectangle InformationButtonDisplayPosition
        {
            get
            {
                if (Switch21 == "on" && InformationButton == false)
                {
                    return new Rectangle(this.InformationButtonClient.X + this.DisplayOffset.X, this.InformationButtonClient.Y + this.DisplayOffset.Y, this.InformationButtonClient.Width, this.InformationButtonClient.Height);
                }
                return new Rectangle(0, 0, 0, 0);               
            }
        }
        private Rectangle InformationPressedDisplayPosition
        {
            get
            {
                if (Switch21 == "on" && InformationButton == true)
                {
                    return new Rectangle(this.InformationButtonClient.X + this.DisplayOffset.X, this.InformationButtonClient.Y + this.DisplayOffset.Y, this.InformationButtonClient.Width, this.InformationButtonClient.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
       
        private Rectangle FacilityButtonDisplayPosition
        {
            get
            {
                if (Switch21 == "on" && FacilityButton == false)
                {
                    return new Rectangle(this.FacilityButtonClient.X + this.DisplayOffset.X, this.FacilityButtonClient.Y + this.DisplayOffset.Y, this.FacilityButtonClient.Width, this.FacilityButtonClient.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle FacilityPressedDisplayPosition
        {
            get
            {
                if (Switch21 == "on" && FacilityButton == true)
                {
                    return new Rectangle(this.FacilityButtonClient.X + this.DisplayOffset.X, this.FacilityButtonClient.Y + this.DisplayOffset.Y, this.FacilityButtonClient.Width, this.FacilityButtonClient.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        ////
        private Rectangle InformationBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.InformationBackgroundClient.X + this.DisplayOffset.X, this.InformationBackgroundClient.Y + this.DisplayOffset.Y, this.InformationBackgroundClient.Width, this.InformationBackgroundClient.Height);
            }
        }
       
        private Rectangle FacilityBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.FacilityBackgroundClient.X + this.DisplayOffset.X, this.FacilityBackgroundClient.Y + this.DisplayOffset.Y, this.FacilityBackgroundClient.Width, this.FacilityBackgroundClient.Height);
            }
        }
        ////
        private Rectangle IntegrationBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.AllIntegration == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.IntegrationBar1Client.X + this.DisplayOffset.X, this.IntegrationBar1Client.Y + this.DisplayOffset.Y, this.IntegrationBar1Client.Width * this.Integration / this.AllIntegration, this.IntegrationBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Integration1DisplayPosition
        {
            get
            {
                if (this.AllIntegration == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.IntegrationBar1Client.Width * this.Integration / this.AllIntegration, this.IntegrationBar1Client.Height);                 
            }
        }
        private Rectangle DominationBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Domination == 0 || this.ShowingArchitecture.DominationCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.DominationBar1Client.X + this.DisplayOffset.X, this.DominationBar1Client.Y + this.DisplayOffset.Y, this.DominationBar1Client.Width * this.ShowingArchitecture.Domination / this.ShowingArchitecture.DominationCeiling, this.DominationBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Domination1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Domination == 0 || this.ShowingArchitecture.DominationCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.DominationBar1Client.Width * this.ShowingArchitecture.Domination / this.ShowingArchitecture.DominationCeiling, this.DominationBar1Client.Height);
                
            }
        }
        private Rectangle EnduranceBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Endurance == 0 || this.ShowingArchitecture.EnduranceCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.EnduranceBar1Client.X + this.DisplayOffset.X, this.EnduranceBar1Client.Y + this.DisplayOffset.Y, this.EnduranceBar1Client.Width * this.ShowingArchitecture.Endurance / this.ShowingArchitecture.EnduranceCeiling, this.EnduranceBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Endurance1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Endurance == 0 || this.ShowingArchitecture.EnduranceCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.EnduranceBar1Client.Width * this.ShowingArchitecture.Endurance / this.ShowingArchitecture.EnduranceCeiling, this.EnduranceBar1Client.Height);                
            }
        }
        private Rectangle AgricultureBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Agriculture == 0 || this.ShowingArchitecture.AgricultureCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.AgricultureBar1Client.X + this.DisplayOffset.X, this.AgricultureBar1Client.Y + this.DisplayOffset.Y, this.AgricultureBar1Client.Width * this.ShowingArchitecture.Agriculture / this.ShowingArchitecture.AgricultureCeiling, this.AgricultureBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Agriculture1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Agriculture == 0 || this.ShowingArchitecture.AgricultureCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.AgricultureBar1Client.Width * this.ShowingArchitecture.Agriculture / this.ShowingArchitecture.AgricultureCeiling, this.AgricultureBar1Client.Height);                
            }
        }
        private Rectangle CommerceBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Commerce == 0 || this.ShowingArchitecture.CommerceCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.CommerceBar1Client.X + this.DisplayOffset.X, this.CommerceBar1Client.Y + this.DisplayOffset.Y, this.CommerceBar1Client.Width * this.ShowingArchitecture.Commerce / this.ShowingArchitecture.CommerceCeiling, this.CommerceBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Commerce1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Commerce == 0 || this.ShowingArchitecture.CommerceCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.CommerceBar1Client.Width * this.ShowingArchitecture.Commerce / this.ShowingArchitecture.CommerceCeiling, this.CommerceBar1Client.Height);              
            }
        }
        private Rectangle TechnologyBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Technology == 0 || this.ShowingArchitecture.TechnologyCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.TechnologyBar1Client.X + this.DisplayOffset.X, this.TechnologyBar1Client.Y + this.DisplayOffset.Y, this.TechnologyBar1Client.Width * this.ShowingArchitecture.Technology / this.ShowingArchitecture.TechnologyCeiling, this.TechnologyBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Technology1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Technology == 0 || this.ShowingArchitecture.TechnologyCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.TechnologyBar1Client.Width * this.ShowingArchitecture.Technology / this.ShowingArchitecture.TechnologyCeiling, this.TechnologyBar1Client.Height);              
            }
        }
        private Rectangle MoraleBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.Morale == 0 || this.ShowingArchitecture.MoraleCeiling == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.MoraleBar1Client.X + this.DisplayOffset.X, this.MoraleBar1Client.Y + this.DisplayOffset.Y, this.MoraleBar1Client.Width * this.ShowingArchitecture.Morale / this.ShowingArchitecture.MoraleCeiling, this.MoraleBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle Morale1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.Morale == 0 || this.ShowingArchitecture.MoraleCeiling == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.MoraleBar1Client.Width * this.ShowingArchitecture.Morale / this.ShowingArchitecture.MoraleCeiling, this.MoraleBar1Client.Height);                
            }
        }
        private Rectangle FacilityCountBar1DisplayPosition
        {
            get
            {
                if (Switch22 == "on" && InformationButton == true)
                {
                    if (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft == 0 || this.ShowingArchitecture.FacilityPositionCount == 0)
                    {
                        return new Rectangle(0, 0, 0, 0);
                    }
                    return new Rectangle(this.FacilityCountBar1Client.X + this.DisplayOffset.X, this.FacilityCountBar1Client.Y + this.DisplayOffset.Y, this.FacilityCountBar1Client.Width * (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft) / this.ShowingArchitecture.FacilityPositionCount, this.FacilityCountBar1Client.Height);
                }
                return new Rectangle(0, 0, 0, 0);
            }
        }
        private Rectangle FacilityCount1DisplayPosition
        {
            get
            {
                if (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft == 0 || this.ShowingArchitecture.FacilityPositionCount == 0)
                {
                    return new Rectangle(0, 0, 0, 0);
                }
                return new Rectangle(0, 0, this.FacilityCountBar1Client.Width * (this.ShowingArchitecture.FacilityPositionCount - this.ShowingArchitecture.FacilityPositionLeft) / this.ShowingArchitecture.FacilityPositionCount, this.FacilityCountBar1Client.Height);               
            }
        }
        //
        private Rectangle CharacteristicShowBackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.CharacteristicShowBackgroundClient.X + this.DisplayOffset.X, this.CharacteristicShowBackgroundClient.Y + this.DisplayOffset.Y, this.CharacteristicShowBackgroundClient.Width, this.CharacteristicShowBackgroundClient.Height);
            }
        }
        //
        private Rectangle PictureforAKindDisplayPosition
        {
            get
            {
                return new Rectangle(this.PictureforAKindClient.X + this.DisplayOffset.X, this.PictureforAKindClient.Y + this.DisplayOffset.Y, PictureforAKindClient.Width, this.PictureforAKindClient.Height);
            }
        }

        private Rectangle PictureforAIDDisplayPosition
        {
            get
            {
                return new Rectangle(this.PictureforAIDClient.X + this.DisplayOffset.X, this.PictureforAIDClient.Y + this.DisplayOffset.Y, this.PictureforAIDClient.Width, this.PictureforAIDClient.Height);
            }
        }       
        //
        private int TheCharacteristicShowID(int c)
        {
            int id=0;
            int ID = 0;
            int n = 1;
            foreach (Influence influence in this.ShowingArchitecture.Characteristics.Influences.Values)
            {
                if (n == c) { ID = influence.ID; break; } n++;
            }
            //if (File.Exists(@"GameComponents\ArchitectureDetail\Data\TheInformationPage\Characteristic\" + ID.ToString() + ".png"))
            //{
                id = ID;
            //}
            return id;
        }
        private PlatformTexture TheCharacteristicShow(int c)
        {
            PlatformTexture P = PictureNull;
            if (c == 1) { P = TheCharacteristicShow1; }
            else if (c == 2) { P = TheCharacteristicShow2; }
            else if (c == 3) { P = TheCharacteristicShow3; }
            else if (c == 4) { P = TheCharacteristicShow4; }
            else if (c == 5) { P = TheCharacteristicShow5; }
            else if (c == 6) { P = TheCharacteristicShow6; }
            else if (c == 7) { P = TheCharacteristicShow7; }
            else if (c == 8) { P = TheCharacteristicShow8; }
            else if (c == 9) { P = TheCharacteristicShow9; }
            else if (c == 10) { P = TheCharacteristicShow10; }
            else if (c == 11) { P = TheCharacteristicShow11; }
            else if (c == 12) { P = TheCharacteristicShow12; }
            else if (c == 13) { P = TheCharacteristicShow13; }
            else if (c == 14) { P = TheCharacteristicShow14; }
            else if (c == 15) { P = TheCharacteristicShow15; }
            else if (c == 16) { P = TheCharacteristicShow16; }
            else if (c == 17) { P = TheCharacteristicShow17; }
            else if (c == 18) { P = TheCharacteristicShow18; }
            else if (c == 19) { P = TheCharacteristicShow19; }
            else if (c == 20) { P = TheCharacteristicShow20; }
            else if (c == 21) { P = TheCharacteristicShow21; }
            else if (c == 22) { P = TheCharacteristicShow22; }
            else if (c == 23) { P = TheCharacteristicShow23; }
            else if (c == 24) { P = TheCharacteristicShow24; }
            else if (c == 25) { P = TheCharacteristicShow25; }
            else if (c == 26) { P = TheCharacteristicShow26; }
            else if (c == 27) { P = TheCharacteristicShow27; }
            else if (c == 28) { P = TheCharacteristicShow28; }
            else if (c == 29) { P = TheCharacteristicShow29; }
            else if (c == 30) { P = TheCharacteristicShow30; }
            return P;           
        }
        private Rectangle TheCharacteristicShowDisplayPosition(int c)
        {
            Rectangle D = this.NullDisplayPosition;
            int nX = 0;
            int nY = 0;
            nX = (c - 1) * (this.TheCharacteristicShowXSpacing + this.TheCharacteristicShowClient.Height);
            nY = (c - 1) * (this.TheCharacteristicShowYSpacing + this.TheCharacteristicShowClient.Height);
            for (int n = 1; n <= TheCharacteristicShowYNumber; n++)
            {
                if (c <= n * TheCharacteristicShowXNumber)
                {
                    nX = (c - (n - 1) * TheCharacteristicShowXNumber - 1) * (this.TheCharacteristicShowXSpacing + this.TheCharacteristicShowClient.Width);
                    nY = (n - 1) * (this.TheCharacteristicShowYSpacing + this.TheCharacteristicShowClient.Height);
                    D = new Rectangle(nX + this.TheCharacteristicShowClient.X + this.DisplayOffset.X, nY + this.TheCharacteristicShowClient.Y + this.DisplayOffset.Y, this.TheCharacteristicShowClient.Width, this.TheCharacteristicShowClient.Height);
                    break;
                }
            }
            return D;
        }       
        ////      
        
        private bool ShowTheFacilityPageButton(int i)
        {
            bool H=false;
            if(PageForFacilityPositionCount=="on")
            {
                 if(i==1){if(PageForFacilityPositionCountNumber>=1 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount1){H=true;}}
            else if(i==2){if(PageForFacilityPositionCountNumber>=2 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount2){H=true;}}
            else if(i==3){if(PageForFacilityPositionCountNumber>=3 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount3){H=true;}}
            else if(i==4){if(PageForFacilityPositionCountNumber>=4 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount4){H=true;}}
            else if(i==5){if(PageForFacilityPositionCountNumber>=5 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount5){H=true;}}
            else if(i==6){if(PageForFacilityPositionCountNumber>=6 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount6){H=true;}}
            else if(i==7){if(PageForFacilityPositionCountNumber>=7 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount7){H=true;}}
            else if(i==8){if(PageForFacilityPositionCountNumber>=8 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount8){H=true;}}
            else if(i==9){if(PageForFacilityPositionCountNumber>=9 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount9){H=true;}}
            else if(i==10){if(PageForFacilityPositionCountNumber>=10 && this.ShowingArchitecture.FacilityPositionCount>=this.PageForFacilityPositionCount10){H=true;}}  
            }
            if(PageForArchitectureKind=="on")
            {
                 if(i==11){if(PageForArchitectureKindNumber>=1 && this.ArchitectureKind==this.PageForArchitectureKind1){H=true;}}
            else if(i==12){if(PageForArchitectureKindNumber>=2 && this.ArchitectureKind==this.PageForArchitectureKind2){H=true;}}
            else if(i==13){if(PageForArchitectureKindNumber>=3 && this.ArchitectureKind==this.PageForArchitectureKind3){H=true;}}
            else if(i==14){if(PageForArchitectureKindNumber>=4 && this.ArchitectureKind==this.PageForArchitectureKind4){H=true;}}
            else if(i==15){if(PageForArchitectureKindNumber>=5 && this.ArchitectureKind==this.PageForArchitectureKind5){H=true;}}
            else if(i==16){if(PageForArchitectureKindNumber>=6 && this.ArchitectureKind==this.PageForArchitectureKind6){H=true;}}
            else if(i==17){if(PageForArchitectureKindNumber>=7 && this.ArchitectureKind==this.PageForArchitectureKind7){H=true;}}
            else if(i==18){if(PageForArchitectureKindNumber>=8 && this.ArchitectureKind==this.PageForArchitectureKind8){H=true;}}
            else if(i==19){if(PageForArchitectureKindNumber>=9 && this.ArchitectureKind==this.PageForArchitectureKind9){H=true;}}
            else if(i==20){if(PageForArchitectureKindNumber>=10 && this.ArchitectureKind==this.PageForArchitectureKind10){H=true;}}
            }
            if(PageForArchitectureCharacteristic=="on")
            {  
                 if(i==21){if(PageForArchitectureCharacteristicNumber>=1 && HasTheCharacteristicID(PageForArchitectureCharacteristic1)==true ){H=true;}}
            else if(i==22){if(PageForArchitectureCharacteristicNumber>=2 && HasTheCharacteristicID(PageForArchitectureCharacteristic2)==true){H=true;}}
            else if(i==23){if(PageForArchitectureCharacteristicNumber>=3 && HasTheCharacteristicID(PageForArchitectureCharacteristic3)==true){H=true;}}
            else if(i==24){if(PageForArchitectureCharacteristicNumber>=4 && HasTheCharacteristicID(PageForArchitectureCharacteristic4)==true){H=true;}}
            else if(i==25){if(PageForArchitectureCharacteristicNumber>=5 && HasTheCharacteristicID(PageForArchitectureCharacteristic5)==true){H=true;}}
            else if(i==26){if(PageForArchitectureCharacteristicNumber>=6 && HasTheCharacteristicID(PageForArchitectureCharacteristic6)==true){H=true;}}
            else if(i==27){if(PageForArchitectureCharacteristicNumber>=7 && HasTheCharacteristicID(PageForArchitectureCharacteristic7)==true){H=true;}}
            else if(i==28){if(PageForArchitectureCharacteristicNumber>=8 && HasTheCharacteristicID(PageForArchitectureCharacteristic8)==true){H=true;}}
            else if(i==29){if(PageForArchitectureCharacteristicNumber>=9 && HasTheCharacteristicID(PageForArchitectureCharacteristic9)==true){H=true;}}
            else if(i==30){if(PageForArchitectureCharacteristicNumber>=10 && HasTheCharacteristicID(PageForArchitectureCharacteristic10)==true){H=true;}}
            }
            return H;
        }
         private bool HasTheCharacteristicID(int i)
        {
            bool H = false;
            int ID = -1;
            foreach (Influence influence in this.ShowingArchitecture.Characteristics.Influences.Values)
            {
                ID = influence.ID;
                if (ID == i)
                {
                    H = true;
                    break;
                }
            }
            return H;
        }       
         private bool TheFacilityPageButton(int i)
         {
             bool H = false;
             if(i==1){H=FacilityforPage1Button;}
             else if(i==2){H=FacilityforPage2Button;}
             else if(i==3){H=FacilityforPage3Button;}
             else if(i==4){H=FacilityforPage4Button;}
             else if(i==5){H=FacilityforPage5Button;}
             else if(i==6){H=FacilityforPage6Button;}
             else if(i==7){H=FacilityforPage7Button;}
             else if(i==8){H=FacilityforPage8Button;}
             else if(i==9){H=FacilityforPage9Button;}
             else if(i==10){H=FacilityforPage10Button;}
             else if(i==11){H=FacilityforPage11Button;}
             else if(i==12){H=FacilityforPage12Button;}
             else if(i==13){H=FacilityforPage13Button;}
             else if(i==14){H=FacilityforPage14Button;}
             else if(i==15){H=FacilityforPage15Button;}
             else if(i==16){H=FacilityforPage16Button;}
             else if(i==17){H=FacilityforPage17Button;}
             else if(i==18){H=FacilityforPage18Button;}
             else if(i==19){H=FacilityforPage19Button;}
             else if(i==20){H=FacilityforPage20Button;}
             else if(i==21){H=FacilityforPage21Button;}
             else if(i==22){H=FacilityforPage22Button;}
             else if(i==23){H=FacilityforPage23Button;}
             else if(i==24){H=FacilityforPage24Button;}
             else if(i==25){H=FacilityforPage25Button;}
             else if(i==26){H=FacilityforPage26Button;}
             else if(i==27){H=FacilityforPage27Button;}
             else if(i==28){H=FacilityforPage28Button;}
             else if(i==29){H=FacilityforPage29Button;}
             else if(i==30){H=FacilityforPage30Button;}
             return H;
         }
         private PlatformTexture FacilityforPageButtonTexture(int i)
         {
             PlatformTexture P = PictureNull;
             if (i == 1) { P = FacilityforPage1ButtonTexture; }
             else if (i == 2) { P = FacilityforPage2ButtonTexture; }
             else if (i == 3) { P = FacilityforPage3ButtonTexture; }
             else if (i == 4) { P = FacilityforPage4ButtonTexture; }
             else if (i == 5) { P = FacilityforPage5ButtonTexture; }
             else if (i == 6) { P = FacilityforPage6ButtonTexture; }
             else if (i == 7) { P = FacilityforPage7ButtonTexture; }
             else if (i == 8) { P = FacilityforPage8ButtonTexture; }
             else if (i == 9) { P = FacilityforPage9ButtonTexture; }
             else if (i == 10) { P = FacilityforPage10ButtonTexture; }
             else if (i == 11) { P = FacilityforPage11ButtonTexture; }
             else if (i == 12) { P = FacilityforPage12ButtonTexture; }
             else if (i == 13) { P = FacilityforPage13ButtonTexture; }
             else if (i == 14) { P = FacilityforPage14ButtonTexture; }
             else if (i == 15) { P = FacilityforPage15ButtonTexture; }
             else if (i == 16) { P = FacilityforPage16ButtonTexture; }
             else if (i == 17) { P = FacilityforPage17ButtonTexture; }
             else if (i == 18) { P = FacilityforPage18ButtonTexture; }
             else if (i == 19) { P = FacilityforPage19ButtonTexture; }
             else if (i == 20) { P = FacilityforPage20ButtonTexture; }
             else if (i == 21) { P = FacilityforPage21ButtonTexture; }
             else if (i == 22) { P = FacilityforPage22ButtonTexture; }
             else if (i == 23) { P = FacilityforPage23ButtonTexture; }
             else if (i == 24) { P = FacilityforPage24ButtonTexture; }
             else if (i == 25) { P = FacilityforPage25ButtonTexture; }
             else if (i == 26) { P = FacilityforPage26ButtonTexture; }
             else if (i == 27) { P = FacilityforPage27ButtonTexture; }
             else if (i == 28) { P = FacilityforPage28ButtonTexture; }
             else if (i == 29) { P = FacilityforPage29ButtonTexture; }
             else if (i == 30) { P = FacilityforPage30ButtonTexture; }
             return P;
         }
         private PlatformTexture FacilityforPagePressedTexture(int i)
         {
             PlatformTexture P = PictureNull;
             if (i == 1) { P = FacilityforPage1PressedTexture; }
             else if (i == 2) { P = FacilityforPage2PressedTexture; }
             else if (i == 3) { P = FacilityforPage3PressedTexture; }
             else if (i == 4) { P = FacilityforPage4PressedTexture; }
             else if (i == 5) { P = FacilityforPage5PressedTexture; }
             else if (i == 6) { P = FacilityforPage6PressedTexture; }
             else if (i == 7) { P = FacilityforPage7PressedTexture; }
             else if (i == 8) { P = FacilityforPage8PressedTexture; }
             else if (i == 9) { P = FacilityforPage9PressedTexture; }
             else if (i == 10) { P = FacilityforPage10PressedTexture; }
             else if (i == 11) { P = FacilityforPage11PressedTexture; }
             else if (i == 12) { P = FacilityforPage12PressedTexture; }
             else if (i == 13) { P = FacilityforPage13PressedTexture; }
             else if (i == 14) { P = FacilityforPage14PressedTexture; }
             else if (i == 15) { P = FacilityforPage15PressedTexture; }
             else if (i == 16) { P = FacilityforPage16PressedTexture; }
             else if (i == 17) { P = FacilityforPage17PressedTexture; }
             else if (i == 18) { P = FacilityforPage18PressedTexture; }
             else if (i == 19) { P = FacilityforPage19PressedTexture; }
             else if (i == 20) { P = FacilityforPage20PressedTexture; }
             else if (i == 21) { P = FacilityforPage21PressedTexture; }
             else if (i == 22) { P = FacilityforPage22PressedTexture; }
             else if (i == 23) { P = FacilityforPage23PressedTexture; }
             else if (i == 24) { P = FacilityforPage24PressedTexture; }
             else if (i == 25) { P = FacilityforPage25PressedTexture; }
             else if (i == 26) { P = FacilityforPage26PressedTexture; }
             else if (i == 27) { P = FacilityforPage27PressedTexture; }
             else if (i == 28) { P = FacilityforPage28PressedTexture; }
             else if (i == 29) { P = FacilityforPage29PressedTexture; }
             else if (i == 30) { P = FacilityforPage30PressedTexture; }
             return P;
         }
         private Rectangle FacilityforPageButtonDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             if (i == 1) { D = new Rectangle(this.FacilityforPage1ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage1ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage1ButtonClient.Width, this.FacilityforPage1ButtonClient.Height); }
             else if (i == 2) { D = new Rectangle(this.FacilityforPage2ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage2ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage2ButtonClient.Width, this.FacilityforPage2ButtonClient.Height); }
             else if (i == 3) { D = new Rectangle(this.FacilityforPage3ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage3ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage3ButtonClient.Width, this.FacilityforPage3ButtonClient.Height); }
             else if (i == 4) { D = new Rectangle(this.FacilityforPage4ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage4ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage4ButtonClient.Width, this.FacilityforPage4ButtonClient.Height); }
             else if (i == 5) { D = new Rectangle(this.FacilityforPage5ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage5ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage5ButtonClient.Width, this.FacilityforPage5ButtonClient.Height); }
             else if (i == 6) { D = new Rectangle(this.FacilityforPage6ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage6ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage6ButtonClient.Width, this.FacilityforPage6ButtonClient.Height); }
             else if (i == 7) { D = new Rectangle(this.FacilityforPage7ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage7ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage7ButtonClient.Width, this.FacilityforPage7ButtonClient.Height); }
             else if (i == 8) { D = new Rectangle(this.FacilityforPage8ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage8ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage8ButtonClient.Width, this.FacilityforPage8ButtonClient.Height); }
             else if (i == 9) { D = new Rectangle(this.FacilityforPage9ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage9ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage9ButtonClient.Width, this.FacilityforPage9ButtonClient.Height); }
             else if (i == 10) { D = new Rectangle(this.FacilityforPage10ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage10ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage10ButtonClient.Width, this.FacilityforPage10ButtonClient.Height); }
             else if (i == 11) { D = new Rectangle(this.FacilityforPage11ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage11ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage11ButtonClient.Width, this.FacilityforPage11ButtonClient.Height); }
             else if (i == 12) { D = new Rectangle(this.FacilityforPage12ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage12ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage12ButtonClient.Width, this.FacilityforPage12ButtonClient.Height); }
             else if (i == 13) { D = new Rectangle(this.FacilityforPage13ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage13ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage13ButtonClient.Width, this.FacilityforPage13ButtonClient.Height); }
             else if (i == 14) { D = new Rectangle(this.FacilityforPage14ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage14ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage14ButtonClient.Width, this.FacilityforPage14ButtonClient.Height); }
             else if (i == 15) { D = new Rectangle(this.FacilityforPage15ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage15ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage15ButtonClient.Width, this.FacilityforPage15ButtonClient.Height); }
             else if (i == 16) { D = new Rectangle(this.FacilityforPage16ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage16ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage16ButtonClient.Width, this.FacilityforPage16ButtonClient.Height); }
             else if (i == 17) { D = new Rectangle(this.FacilityforPage17ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage17ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage17ButtonClient.Width, this.FacilityforPage17ButtonClient.Height); }
             else if (i == 18) { D = new Rectangle(this.FacilityforPage18ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage18ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage18ButtonClient.Width, this.FacilityforPage18ButtonClient.Height); }
             else if (i == 19) { D = new Rectangle(this.FacilityforPage19ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage19ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage19ButtonClient.Width, this.FacilityforPage19ButtonClient.Height); }
             else if (i == 20) { D = new Rectangle(this.FacilityforPage20ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage20ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage20ButtonClient.Width, this.FacilityforPage20ButtonClient.Height); }
             else if (i == 21) { D = new Rectangle(this.FacilityforPage21ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage21ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage21ButtonClient.Width, this.FacilityforPage21ButtonClient.Height); }
             else if (i == 22) { D = new Rectangle(this.FacilityforPage22ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage22ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage22ButtonClient.Width, this.FacilityforPage22ButtonClient.Height); }
             else if (i == 23) { D = new Rectangle(this.FacilityforPage23ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage23ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage23ButtonClient.Width, this.FacilityforPage23ButtonClient.Height); }
             else if (i == 24) { D = new Rectangle(this.FacilityforPage24ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage24ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage24ButtonClient.Width, this.FacilityforPage24ButtonClient.Height); }
             else if (i == 25) { D = new Rectangle(this.FacilityforPage25ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage25ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage25ButtonClient.Width, this.FacilityforPage25ButtonClient.Height); }
             else if (i == 26) { D = new Rectangle(this.FacilityforPage26ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage26ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage26ButtonClient.Width, this.FacilityforPage26ButtonClient.Height); }
             else if (i == 27) { D = new Rectangle(this.FacilityforPage27ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage27ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage27ButtonClient.Width, this.FacilityforPage27ButtonClient.Height); }
             else if (i == 28) { D = new Rectangle(this.FacilityforPage28ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage28ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage28ButtonClient.Width, this.FacilityforPage28ButtonClient.Height); }
             else if (i == 29) { D = new Rectangle(this.FacilityforPage29ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage29ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage29ButtonClient.Width, this.FacilityforPage29ButtonClient.Height); }
             else if (i == 30) { D = new Rectangle(this.FacilityforPage30ButtonClient.X + this.DisplayOffset.X, this.FacilityforPage30ButtonClient.Y + this.DisplayOffset.Y, this.FacilityforPage30ButtonClient.Width, this.FacilityforPage30ButtonClient.Height); }
             return D;
         }
         private Rectangle TheFacilityforPageButtonDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             if (ShowTheFacilityPageButton(i)==true && TheFacilityPageButton(i)==false) 
             { D = FacilityforPageButtonDisplayPosition(i); }           
             return D;
         }
         private Rectangle TheFacilityforPagePressedDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             if (ShowTheFacilityPageButton(i) == true && TheFacilityPageButton(i) == true)
             { D = FacilityforPageButtonDisplayPosition(i); }
             return D;
         }
        // 
         private PlatformTexture ThePageForFacilityMask(int i)
         {
             PlatformTexture P = PictureNull;
             if (i == 1) { P = PageForFacilityMask1; }
             else if (i == 2) { P = PageForFacilityMask2; }
             else if (i == 3) { P = PageForFacilityMask3; }
             else if (i == 4) { P = PageForFacilityMask4; }
             else if (i == 5) { P = PageForFacilityMask5; }
             else if (i == 6) { P = PageForFacilityMask6; }
             else if (i == 7) { P = PageForFacilityMask7; }
             else if (i == 8) { P = PageForFacilityMask8; }
             else if (i == 9) { P = PageForFacilityMask9; }
             else if (i == 10) { P = PageForFacilityMask10; }
             else if (i == 11) { P = PageForFacilityMask11; }
             else if (i == 12) { P = PageForFacilityMask12; }
             else if (i == 13) { P = PageForFacilityMask13; }
             else if (i == 14) { P = PageForFacilityMask14; }
             else if (i == 15) { P = PageForFacilityMask15; }
             else if (i == 16) { P = PageForFacilityMask16; }
             else if (i == 17) { P = PageForFacilityMask17; }
             else if (i == 18) { P = PageForFacilityMask18; }
             else if (i == 19) { P = PageForFacilityMask19; }
             else if (i == 20) { P = PageForFacilityMask20; }
             else if (i == 21) { P = PageForFacilityMask21; }
             else if (i == 22) { P = PageForFacilityMask22; }
             else if (i == 23) { P = PageForFacilityMask23; }
             else if (i == 24) { P = PageForFacilityMask24; }
             else if (i == 25) { P = PageForFacilityMask25; }
             else if (i == 26) { P = PageForFacilityMask26; }
             else if (i == 27) { P = PageForFacilityMask27; }
             else if (i == 28) { P = PageForFacilityMask28; }
             else if (i == 29) { P = PageForFacilityMask29; }
             else if (i == 30) { P = PageForFacilityMask30; }
             return P;
         }
         private PlatformTexture ThePageForFacilityBackground(int i)
         {
             PlatformTexture P = PictureNull;
             if (i == 1) { P = PageForFacilityBackground1; }
             else if (i == 2) { P = PageForFacilityBackground2; }
             else if (i == 3) { P = PageForFacilityBackground3; }
             else if (i == 4) { P = PageForFacilityBackground4; }
             else if (i == 5) { P = PageForFacilityBackground5; }
             else if (i == 6) { P = PageForFacilityBackground6; }
             else if (i == 7) { P = PageForFacilityBackground7; }
             else if (i == 8) { P = PageForFacilityBackground8; }
             else if (i == 9) { P = PageForFacilityBackground9; }
             else if (i == 10) { P = PageForFacilityBackground10; }
             else if (i == 11) { P = PageForFacilityBackground11; }
             else if (i == 12) { P = PageForFacilityBackground12; }
             else if (i == 13) { P = PageForFacilityBackground13; }
             else if (i == 14) { P = PageForFacilityBackground14; }
             else if (i == 15) { P = PageForFacilityBackground15; }
             else if (i == 16) { P = PageForFacilityBackground16; }
             else if (i == 17) { P = PageForFacilityBackground17; }
             else if (i == 18) { P = PageForFacilityBackground18; }
             else if (i == 19) { P = PageForFacilityBackground19; }
             else if (i == 20) { P = PageForFacilityBackground20; }
             else if (i == 21) { P = PageForFacilityBackground21; }
             else if (i == 22) { P = PageForFacilityBackground22; }
             else if (i == 23) { P = PageForFacilityBackground23; }
             else if (i == 24) { P = PageForFacilityBackground24; }
             else if (i == 25) { P = PageForFacilityBackground25; }
             else if (i == 26) { P = PageForFacilityBackground26; }
             else if (i == 27) { P = PageForFacilityBackground27; }
             else if (i == 28) { P = PageForFacilityBackground28; }
             else if (i == 29) { P = PageForFacilityBackground29; }
             else if (i == 30) { P = PageForFacilityBackground30; }
             return P;
         }
         private Rectangle PageForFacilityBackgroundDisplayPosition
         {
             get
             {
                 return new Rectangle(this.PageForFacilityBackgroundClient.X + this.DisplayOffset.X, this.PageForFacilityBackgroundClient.Y + this.DisplayOffset.Y, this.PageForFacilityBackgroundClient.Width, this.PageForFacilityBackgroundClient.Height); 
             }
         }
         private Rectangle ThePageForFacilityBackgroundDisplayPosition(int i)
         {
             Rectangle D = this.NullDisplayPosition;
             if (TheFacilityPageButton(i) == true)
             { D = PageForFacilityBackgroundDisplayPosition; }
             return D;
         }
        //
        private int TheFacilityKind(int i)
        {
            int ID=-1;
            if(i==1){ID=int.Parse(Facility1ID);}
            else if (i == 2) { ID = int.Parse(Facility2ID); }
            else if (i == 3) { ID = int.Parse(Facility3ID); }
            else if (i == 4) { ID = int.Parse(Facility4ID); }
            else if (i == 5) { ID = int.Parse(Facility5ID); }
            else if (i == 6) { ID = int.Parse(Facility6ID); }
            else if (i == 7) { ID = int.Parse(Facility7ID); }
            else if (i == 8) { ID = int.Parse(Facility8ID); }
            else if (i == 9) { ID = int.Parse(Facility9ID); }
            else if (i == 10) { ID = int.Parse(Facility10ID); }
            else if (i == 11) { ID = int.Parse(Facility11ID); }
            else if (i == 12) { ID = int.Parse(Facility12ID); }
            else if (i == 13) { ID = int.Parse(Facility13ID); }
            else if (i == 14) { ID = int.Parse(Facility14ID); }
            else if (i == 15) { ID = int.Parse(Facility15ID); }
            else if (i == 16) { ID = int.Parse(Facility16ID); }
            else if (i == 17) { ID = int.Parse(Facility17ID); }
            else if (i == 18) { ID = int.Parse(Facility18ID); }
            else if (i == 19) { ID = int.Parse(Facility19ID); }
            else if (i == 20) { ID = int.Parse(Facility20ID); }
            else if (i == 21) { ID = int.Parse(Facility21ID); }
            else if (i == 22) { ID = int.Parse(Facility22ID); }
            else if (i == 23) { ID = int.Parse(Facility23ID); }
            else if (i == 24) { ID = int.Parse(Facility24ID); }
            else if (i == 25) { ID = int.Parse(Facility25ID); }
            else if (i == 26) { ID = int.Parse(Facility26ID); }
            else if (i == 27) { ID = int.Parse(Facility27ID); }
            else if (i == 28) { ID = int.Parse(Facility28ID); }
            else if (i == 29) { ID = int.Parse(Facility29ID); }
            else if (i == 30) { ID = int.Parse(Facility30ID); }
            else if (i == 31) { ID = int.Parse(Facility31ID); }
            else if (i == 32) { ID = int.Parse(Facility32ID); }
            else if (i == 33) { ID = int.Parse(Facility33ID); }
            else if (i == 34) { ID = int.Parse(Facility34ID); }
            else if (i == 35) { ID = int.Parse(Facility35ID); }
            else if (i == 36) { ID = int.Parse(Facility36ID); }
            else if (i == 37) { ID = int.Parse(Facility37ID); }
            else if (i == 38) { ID = int.Parse(Facility38ID); }
            else if (i == 39) { ID = int.Parse(Facility39ID); }
            else if (i == 40) { ID = int.Parse(Facility40ID); }
            else if (i == 41) { ID = int.Parse(Facility41ID); }
            else if (i == 42) { ID = int.Parse(Facility42ID); }
            else if (i == 43) { ID = int.Parse(Facility43ID); }
            else if (i == 44) { ID = int.Parse(Facility44ID); }
            else if (i == 45) { ID = int.Parse(Facility45ID); }
            else if (i == 46) { ID = int.Parse(Facility46ID); }
            else if (i == 47) { ID = int.Parse(Facility47ID); }
            else if (i == 48) { ID = int.Parse(Facility48ID); }
            else if (i == 49) { ID = int.Parse(Facility49ID); }
            else if (i == 50) { ID = int.Parse(Facility50ID); }
            else if (i == 51) { ID = int.Parse(Facility51ID); }
            else if (i == 52) { ID = int.Parse(Facility52ID); }
            else if (i == 53) { ID = int.Parse(Facility53ID); }
            else if (i == 54) { ID = int.Parse(Facility54ID); }
            else if (i == 55) { ID = int.Parse(Facility55ID); }
            else if (i == 56) { ID = int.Parse(Facility56ID); }
            else if (i == 57) { ID = int.Parse(Facility57ID); }
            else if (i == 58) { ID = int.Parse(Facility58ID); }
            else if (i == 59) { ID = int.Parse(Facility59ID); }
            else if (i == 60) { ID = int.Parse(Facility60ID); }
            else if (i == 61) { ID = int.Parse(Facility61ID); }
            else if (i == 62) { ID = int.Parse(Facility62ID); }
            else if (i == 63) { ID = int.Parse(Facility63ID); }
            else if (i == 64) { ID = int.Parse(Facility64ID); }
            else if (i == 65) { ID = int.Parse(Facility65ID); }
            else if (i == 66) { ID = int.Parse(Facility66ID); }
            else if (i == 67) { ID = int.Parse(Facility67ID); }
            else if (i == 68) { ID = int.Parse(Facility68ID); }
            else if (i == 69) { ID = int.Parse(Facility69ID); }
            else if (i == 70) { ID = int.Parse(Facility70ID); }
            else if (i == 71) { ID = int.Parse(Facility71ID); }
            else if (i == 72) { ID = int.Parse(Facility72ID); }
            else if (i == 73) { ID = int.Parse(Facility73ID); }
            else if (i == 74) { ID = int.Parse(Facility74ID); }
            else if (i == 75) { ID = int.Parse(Facility75ID); }
            else if (i == 76) { ID = int.Parse(Facility76ID); }
            else if (i == 77) { ID = int.Parse(Facility77ID); }
            else if (i == 78) { ID = int.Parse(Facility78ID); }
            else if (i == 79) { ID = int.Parse(Facility79ID); }
            else if (i == 80) { ID = int.Parse(Facility80ID); }
            else if (i == 81) { ID = int.Parse(Facility81ID); }
            else if (i == 82) { ID = int.Parse(Facility82ID); }
            else if (i == 83) { ID = int.Parse(Facility83ID); }
            else if (i == 84) { ID = int.Parse(Facility84ID); }
            else if (i == 85) { ID = int.Parse(Facility85ID); }
            else if (i == 86) { ID = int.Parse(Facility86ID); }
            else if (i == 87) { ID = int.Parse(Facility87ID); }
            else if (i == 88) { ID = int.Parse(Facility88ID); }
            else if (i == 89) { ID = int.Parse(Facility89ID); }
            else if (i == 90) { ID = int.Parse(Facility90ID); }
            else if (i == 91) { ID = int.Parse(Facility91ID); }
            else if (i == 92) { ID = int.Parse(Facility92ID); }
            else if (i == 93) { ID = int.Parse(Facility93ID); }
            else if (i == 94) { ID = int.Parse(Facility94ID); }
            else if (i == 95) { ID = int.Parse(Facility95ID); }
            else if (i == 96) { ID = int.Parse(Facility96ID); }
            else if (i == 97) { ID = int.Parse(Facility97ID); }
            else if (i == 98) { ID = int.Parse(Facility98ID); }
            else if (i == 99) { ID = int.Parse(Facility99ID); }
            else if (i == 100) { ID = int.Parse(Facility100ID); }
            else if (i == 101) { ID = int.Parse(Facility101ID); }
            else if (i == 102) { ID = int.Parse(Facility102ID); }
            else if (i == 103) { ID = int.Parse(Facility103ID); }
            else if (i == 104) { ID = int.Parse(Facility104ID); }
            else if (i == 105) { ID = int.Parse(Facility105ID); }
            else if (i == 106) { ID = int.Parse(Facility106ID); }
            else if (i == 107) { ID = int.Parse(Facility107ID); }
            else if (i == 108) { ID = int.Parse(Facility108ID); }
            else if (i == 109) { ID = int.Parse(Facility109ID); }
            else if (i == 110) { ID = int.Parse(Facility110ID); }
            else if (i == 111) { ID = int.Parse(Facility111ID); }
            else if (i == 112) { ID = int.Parse(Facility112ID); }
            else if (i == 113) { ID = int.Parse(Facility113ID); }
            else if (i == 114) { ID = int.Parse(Facility114ID); }
            else if (i == 115) { ID = int.Parse(Facility115ID); }
            else if (i == 116) { ID = int.Parse(Facility116ID); }
            else if (i == 117) { ID = int.Parse(Facility117ID); }
            else if (i == 118) { ID = int.Parse(Facility118ID); }
            else if (i == 119) { ID = int.Parse(Facility119ID); }
            else if (i == 120) { ID = int.Parse(Facility120ID); }
            else if (i == 121) { ID = int.Parse(Facility121ID); }
            else if (i == 122) { ID = int.Parse(Facility122ID); }
            else if (i == 123) { ID = int.Parse(Facility123ID); }
            else if (i == 124) { ID = int.Parse(Facility124ID); }
            else if (i == 125) { ID = int.Parse(Facility125ID); }
            else if (i == 126) { ID = int.Parse(Facility126ID); }
            else if (i == 127) { ID = int.Parse(Facility127ID); }
            else if (i == 128) { ID = int.Parse(Facility128ID); }
            else if (i == 129) { ID = int.Parse(Facility129ID); }
            else if (i == 130) { ID = int.Parse(Facility130ID); }
            else if (i == 131) { ID = int.Parse(Facility131ID); }
            else if (i == 132) { ID = int.Parse(Facility132ID); }
            else if (i == 133) { ID = int.Parse(Facility133ID); }
            else if (i == 134) { ID = int.Parse(Facility134ID); }
            else if (i == 135) { ID = int.Parse(Facility135ID); }
            else if (i == 136) { ID = int.Parse(Facility136ID); }
            else if (i == 137) { ID = int.Parse(Facility137ID); }
            else if (i == 138) { ID = int.Parse(Facility138ID); }
            else if (i == 139) { ID = int.Parse(Facility139ID); }
            else if (i == 140) { ID = int.Parse(Facility140ID); }
            else if (i == 141) { ID = int.Parse(Facility141ID); }
            else if (i == 142) { ID = int.Parse(Facility142ID); }
            else if (i == 143) { ID = int.Parse(Facility143ID); }
            else if (i == 144) { ID = int.Parse(Facility144ID); }
            else if (i == 145) { ID = int.Parse(Facility145ID); }
            else if (i == 146) { ID = int.Parse(Facility146ID); }
            else if (i == 147) { ID = int.Parse(Facility147ID); }
            else if (i == 148) { ID = int.Parse(Facility148ID); }
            else if (i == 149) { ID = int.Parse(Facility149ID); }
            else if (i == 150) { ID = int.Parse(Facility150ID); }
            else if (i == 151) { ID = int.Parse(Facility151ID); }
            else if (i == 152) { ID = int.Parse(Facility152ID); }
            else if (i == 153) { ID = int.Parse(Facility153ID); }
            else if (i == 154) { ID = int.Parse(Facility154ID); }
            else if (i == 155) { ID = int.Parse(Facility155ID); }
            else if (i == 156) { ID = int.Parse(Facility156ID); }
            else if (i == 157) { ID = int.Parse(Facility157ID); }
            else if (i == 158) { ID = int.Parse(Facility158ID); }
            else if (i == 159) { ID = int.Parse(Facility159ID); }
            else if (i == 160) { ID = int.Parse(Facility160ID); }
            else if (i == 161) { ID = int.Parse(Facility161ID); }
            else if (i == 162) { ID = int.Parse(Facility162ID); }
            else if (i == 163) { ID = int.Parse(Facility163ID); }
            else if (i == 164) { ID = int.Parse(Facility164ID); }
            else if (i == 165) { ID = int.Parse(Facility165ID); }
            else if (i == 166) { ID = int.Parse(Facility166ID); }
            else if (i == 167) { ID = int.Parse(Facility167ID); }
            else if (i == 168) { ID = int.Parse(Facility168ID); }
            else if (i == 169) { ID = int.Parse(Facility169ID); }
            else if (i == 170) { ID = int.Parse(Facility170ID); }
            else if (i == 171) { ID = int.Parse(Facility171ID); }
            else if (i == 172) { ID = int.Parse(Facility172ID); }
            else if (i == 173) { ID = int.Parse(Facility173ID); }
            else if (i == 174) { ID = int.Parse(Facility174ID); }
            else if (i == 175) { ID = int.Parse(Facility175ID); }
            else if (i == 176) { ID = int.Parse(Facility176ID); }
            else if (i == 177) { ID = int.Parse(Facility177ID); }
            else if (i == 178) { ID = int.Parse(Facility178ID); }
            else if (i == 179) { ID = int.Parse(Facility179ID); }
            else if (i == 180) { ID = int.Parse(Facility180ID); }
            else if (i == 181) { ID = int.Parse(Facility181ID); }
            else if (i == 182) { ID = int.Parse(Facility182ID); }
            else if (i == 183) { ID = int.Parse(Facility183ID); }
            else if (i == 184) { ID = int.Parse(Facility184ID); }
            else if (i == 185) { ID = int.Parse(Facility185ID); }
            else if (i == 186) { ID = int.Parse(Facility186ID); }
            else if (i == 187) { ID = int.Parse(Facility187ID); }
            else if (i == 188) { ID = int.Parse(Facility188ID); }
            else if (i == 189) { ID = int.Parse(Facility189ID); }
            else if (i == 190) { ID = int.Parse(Facility190ID); }
            else if (i == 191) { ID = int.Parse(Facility191ID); }
            else if (i == 192) { ID = int.Parse(Facility192ID); }
            else if (i == 193) { ID = int.Parse(Facility193ID); }
            else if (i == 194) { ID = int.Parse(Facility194ID); }
            else if (i == 195) { ID = int.Parse(Facility195ID); }
            else if (i == 196) { ID = int.Parse(Facility196ID); }
            else if (i == 197) { ID = int.Parse(Facility197ID); }
            else if (i == 198) { ID = int.Parse(Facility198ID); }
            else if (i == 199) { ID = int.Parse(Facility199ID); }
            else if (i == 200) { ID = int.Parse(Facility200ID); }
            return ID;
        } 
       private bool HasTheFacilityKind(int i)
       {
           bool H = false;
           if (this.ShowingArchitecture.HasTheFacilityForKind(TheFacilityKind(i)) == true) { H = true; }
           return H;
       }      
        private string TheFacilityName(int i)
        {
            string N="";
            if(TheFacilityDescriptionTextFrom=="xml")
            {
                 if(i==1){N=Facility1Name;}
                 else if (i == 2) { N = Facility2Name; }
                 else if (i == 3) { N = Facility3Name; }
                 else if (i == 4) { N = Facility4Name; }
                 else if (i == 5) { N = Facility5Name; }
                 else if (i == 6) { N = Facility6Name; }
                 else if (i == 7) { N = Facility7Name; }
                 else if (i == 8) { N = Facility8Name; }
                 else if (i == 9) { N = Facility9Name; }
                 else if (i == 10) { N = Facility10Name; }
                 else if (i == 11) { N = Facility11Name; }
                 else if (i == 12) { N = Facility12Name; }
                 else if (i == 13) { N = Facility13Name; }
                 else if (i == 14) { N = Facility14Name; }
                 else if (i == 15) { N = Facility15Name; }
                 else if (i == 16) { N = Facility16Name; }
                 else if (i == 17) { N = Facility17Name; }
                 else if (i == 18) { N = Facility18Name; }
                 else if (i == 19) { N = Facility19Name; }
                 else if (i == 20) { N = Facility20Name; }
                 else if (i == 21) { N = Facility21Name; }
                 else if (i == 22) { N = Facility22Name; }
                 else if (i == 23) { N = Facility23Name; }
                 else if (i == 24) { N = Facility24Name; }
                 else if (i == 25) { N = Facility25Name; }
                 else if (i == 26) { N = Facility26Name; }
                 else if (i == 27) { N = Facility27Name; }
                 else if (i == 28) { N = Facility28Name; }
                 else if (i == 29) { N = Facility29Name; }
                 else if (i == 30) { N = Facility30Name; }
                 else if (i == 31) { N = Facility31Name; }
                 else if (i == 32) { N = Facility32Name; }
                 else if (i == 33) { N = Facility33Name; }
                 else if (i == 34) { N = Facility34Name; }
                 else if (i == 35) { N = Facility35Name; }
                 else if (i == 36) { N = Facility36Name; }
                 else if (i == 37) { N = Facility37Name; }
                 else if (i == 38) { N = Facility38Name; }
                 else if (i == 39) { N = Facility39Name; }
                 else if (i == 40) { N = Facility40Name; }
                 else if (i == 41) { N = Facility41Name; }
                 else if (i == 42) { N = Facility42Name; }
                 else if (i == 43) { N = Facility43Name; }
                 else if (i == 44) { N = Facility44Name; }
                 else if (i == 45) { N = Facility45Name; }
                 else if (i == 46) { N = Facility46Name; }
                 else if (i == 47) { N = Facility47Name; }
                 else if (i == 48) { N = Facility48Name; }
                 else if (i == 49) { N = Facility49Name; }
                 else if (i == 50) { N = Facility50Name; }
                 else if (i == 51) { N = Facility51Name; }
                 else if (i == 52) { N = Facility52Name; }
                 else if (i == 53) { N = Facility53Name; }
                 else if (i == 54) { N = Facility54Name; }
                 else if (i == 55) { N = Facility55Name; }
                 else if (i == 56) { N = Facility56Name; }
                 else if (i == 57) { N = Facility57Name; }
                 else if (i == 58) { N = Facility58Name; }
                 else if (i == 59) { N = Facility59Name; }
                 else if (i == 60) { N = Facility60Name; }
                 else if (i == 61) { N = Facility61Name; }
                 else if (i == 62) { N = Facility62Name; }
                 else if (i == 63) { N = Facility63Name; }
                 else if (i == 64) { N = Facility64Name; }
                 else if (i == 65) { N = Facility65Name; }
                 else if (i == 66) { N = Facility66Name; }
                 else if (i == 67) { N = Facility67Name; }
                 else if (i == 68) { N = Facility68Name; }
                 else if (i == 69) { N = Facility69Name; }
                 else if (i == 70) { N = Facility70Name; }
                 else if (i == 71) { N = Facility71Name; }
                 else if (i == 72) { N = Facility72Name; }
                 else if (i == 73) { N = Facility73Name; }
                 else if (i == 74) { N = Facility74Name; }
                 else if (i == 75) { N = Facility75Name; }
                 else if (i == 76) { N = Facility76Name; }
                 else if (i == 77) { N = Facility77Name; }
                 else if (i == 78) { N = Facility78Name; }
                 else if (i == 79) { N = Facility79Name; }
                 else if (i == 80) { N = Facility80Name; }
                 else if (i == 81) { N = Facility81Name; }
                 else if (i == 82) { N = Facility82Name; }
                 else if (i == 83) { N = Facility83Name; }
                 else if (i == 84) { N = Facility84Name; }
                 else if (i == 85) { N = Facility85Name; }
                 else if (i == 86) { N = Facility86Name; }
                 else if (i == 87) { N = Facility87Name; }
                 else if (i == 88) { N = Facility88Name; }
                 else if (i == 89) { N = Facility89Name; }
                 else if (i == 90) { N = Facility90Name; }
                 else if (i == 91) { N = Facility91Name; }
                 else if (i == 92) { N = Facility92Name; }
                 else if (i == 93) { N = Facility93Name; }
                 else if (i == 94) { N = Facility94Name; }
                 else if (i == 95) { N = Facility95Name; }
                 else if (i == 96) { N = Facility96Name; }
                 else if (i == 97) { N = Facility97Name; }
                 else if (i == 98) { N = Facility98Name; }
                 else if (i == 99) { N = Facility99Name; }
                 else if (i == 100) { N = Facility100Name; }
                 else if (i == 101) { N = Facility101Name; }
                 else if (i == 102) { N = Facility102Name; }
                 else if (i == 103) { N = Facility103Name; }
                 else if (i == 104) { N = Facility104Name; }
                 else if (i == 105) { N = Facility105Name; }
                 else if (i == 106) { N = Facility106Name; }
                 else if (i == 107) { N = Facility107Name; }
                 else if (i == 108) { N = Facility108Name; }
                 else if (i == 109) { N = Facility109Name; }
                 else if (i == 110) { N = Facility110Name; }
                 else if (i == 111) { N = Facility111Name; }
                 else if (i == 112) { N = Facility112Name; }
                 else if (i == 113) { N = Facility113Name; }
                 else if (i == 114) { N = Facility114Name; }
                 else if (i == 115) { N = Facility115Name; }
                 else if (i == 116) { N = Facility116Name; }
                 else if (i == 117) { N = Facility117Name; }
                 else if (i == 118) { N = Facility118Name; }
                 else if (i == 119) { N = Facility119Name; }
                 else if (i == 120) { N = Facility120Name; }
                 else if (i == 121) { N = Facility121Name; }
                 else if (i == 122) { N = Facility122Name; }
                 else if (i == 123) { N = Facility123Name; }
                 else if (i == 124) { N = Facility124Name; }
                 else if (i == 125) { N = Facility125Name; }
                 else if (i == 126) { N = Facility126Name; }
                 else if (i == 127) { N = Facility127Name; }
                 else if (i == 128) { N = Facility128Name; }
                 else if (i == 129) { N = Facility129Name; }
                 else if (i == 130) { N = Facility130Name; }
                 else if (i == 131) { N = Facility131Name; }
                 else if (i == 132) { N = Facility132Name; }
                 else if (i == 133) { N = Facility133Name; }
                 else if (i == 134) { N = Facility134Name; }
                 else if (i == 135) { N = Facility135Name; }
                 else if (i == 136) { N = Facility136Name; }
                 else if (i == 137) { N = Facility137Name; }
                 else if (i == 138) { N = Facility138Name; }
                 else if (i == 139) { N = Facility139Name; }
                 else if (i == 140) { N = Facility140Name; }
                 else if (i == 141) { N = Facility141Name; }
                 else if (i == 142) { N = Facility142Name; }
                 else if (i == 143) { N = Facility143Name; }
                 else if (i == 144) { N = Facility144Name; }
                 else if (i == 145) { N = Facility145Name; }
                 else if (i == 146) { N = Facility146Name; }
                 else if (i == 147) { N = Facility147Name; }
                 else if (i == 148) { N = Facility148Name; }
                 else if (i == 149) { N = Facility149Name; }
                 else if (i == 150) { N = Facility150Name; }
                 else if (i == 151) { N = Facility151Name; }
                 else if (i == 152) { N = Facility152Name; }
                 else if (i == 153) { N = Facility153Name; }
                 else if (i == 154) { N = Facility154Name; }
                 else if (i == 155) { N = Facility155Name; }
                 else if (i == 156) { N = Facility156Name; }
                 else if (i == 157) { N = Facility157Name; }
                 else if (i == 158) { N = Facility158Name; }
                 else if (i == 159) { N = Facility159Name; }
                 else if (i == 160) { N = Facility160Name; }
                 else if (i == 161) { N = Facility161Name; }
                 else if (i == 162) { N = Facility162Name; }
                 else if (i == 163) { N = Facility163Name; }
                 else if (i == 164) { N = Facility164Name; }
                 else if (i == 165) { N = Facility165Name; }
                 else if (i == 166) { N = Facility166Name; }
                 else if (i == 167) { N = Facility167Name; }
                 else if (i == 168) { N = Facility168Name; }
                 else if (i == 169) { N = Facility169Name; }
                 else if (i == 170) { N = Facility170Name; }
                 else if (i == 171) { N = Facility171Name; }
                 else if (i == 172) { N = Facility172Name; }
                 else if (i == 173) { N = Facility173Name; }
                 else if (i == 174) { N = Facility174Name; }
                 else if (i == 175) { N = Facility175Name; }
                 else if (i == 176) { N = Facility176Name; }
                 else if (i == 177) { N = Facility177Name; }
                 else if (i == 178) { N = Facility178Name; }
                 else if (i == 179) { N = Facility179Name; }
                 else if (i == 180) { N = Facility180Name; }
                 else if (i == 181) { N = Facility181Name; }
                 else if (i == 182) { N = Facility182Name; }
                 else if (i == 183) { N = Facility183Name; }
                 else if (i == 184) { N = Facility184Name; }
                 else if (i == 185) { N = Facility185Name; }
                 else if (i == 186) { N = Facility186Name; }
                 else if (i == 187) { N = Facility187Name; }
                 else if (i == 188) { N = Facility188Name; }
                 else if (i == 189) { N = Facility189Name; }
                 else if (i == 190) { N = Facility190Name; }
                 else if (i == 191) { N = Facility191Name; }
                 else if (i == 192) { N = Facility192Name; }
                 else if (i == 193) { N = Facility193Name; }
                 else if (i == 194) { N = Facility194Name; }
                 else if (i == 195) { N = Facility195Name; }
                 else if (i == 196) { N = Facility196Name; }
                 else if (i == 197) { N = Facility197Name; }
                 else if (i == 198) { N = Facility198Name; }
                 else if (i == 199) { N = Facility199Name; }
                 else if (i == 200) { N = Facility200Name; }
            }
            else
            {
              N = this.ShowingArchitecture.GetFacilityNameForKind(TheFacilityKind(i));           
            }
            return N;
        }
        private int TheFacilityCount(int i)
        {
            int C = -1;
            C = this.ShowingArchitecture.GetFacilityCountForKind(TheFacilityKind(i)); 
            return C;
        }
        private string TheFacilityPositionOccupied(int i)
        {
            string S = "";
            S = this.ShowingArchitecture.GetFacilityPositionOccupiedForKind(TheFacilityKind(i)).ToString();
            return S;
        }
        private string TheFacilityMaintenanceCost(int i)
        {
            string S = "";
            S = this.ShowingArchitecture.GetFacilityMaintenanceCostForKind(TheFacilityKind(i)).ToString();
            return S;
        }
        private string TheFacilityDescription(int i)
        {
            string D="";
            if(TheFacilityDescriptionTextFrom=="xml")
            {
                 if(i==1){D=Facility1Description;}            
                 else if (i == 2) { D = Facility2Description; }
                 else if (i == 3) { D = Facility3Description; }
                 else if (i == 4) { D = Facility4Description; }
                 else if (i == 5) { D = Facility5Description; }
                 else if (i == 6) { D = Facility6Description; }
                 else if (i == 7) { D = Facility7Description; }
                 else if (i == 8) { D = Facility8Description; }
                 else if (i == 9) { D = Facility9Description; }
                 else if (i == 10) { D = Facility10Description; }
                 else if (i == 11) { D = Facility11Description; }
                 else if (i == 12) { D = Facility12Description; }
                 else if (i == 13) { D = Facility13Description; }
                 else if (i == 14) { D = Facility14Description; }
                 else if (i == 15) { D = Facility15Description; }
                 else if (i == 16) { D = Facility16Description; }
                 else if (i == 17) { D = Facility17Description; }
                 else if (i == 18) { D = Facility18Description; }
                 else if (i == 19) { D = Facility19Description; }
                 else if (i == 20) { D = Facility20Description; }
                 else if (i == 21) { D = Facility21Description; }
                 else if (i == 22) { D = Facility22Description; }
                 else if (i == 23) { D = Facility23Description; }
                 else if (i == 24) { D = Facility24Description; }
                 else if (i == 25) { D = Facility25Description; }
                 else if (i == 26) { D = Facility26Description; }
                 else if (i == 27) { D = Facility27Description; }
                 else if (i == 28) { D = Facility28Description; }
                 else if (i == 29) { D = Facility29Description; }
                 else if (i == 30) { D = Facility30Description; }
                 else if (i == 31) { D = Facility31Description; }
                 else if (i == 32) { D = Facility32Description; }
                 else if (i == 33) { D = Facility33Description; }
                 else if (i == 34) { D = Facility34Description; }
                 else if (i == 35) { D = Facility35Description; }
                 else if (i == 36) { D = Facility36Description; }
                 else if (i == 37) { D = Facility37Description; }
                 else if (i == 38) { D = Facility38Description; }
                 else if (i == 39) { D = Facility39Description; }
                 else if (i == 40) { D = Facility40Description; }
                 else if (i == 41) { D = Facility41Description; }
                 else if (i == 42) { D = Facility42Description; }
                 else if (i == 43) { D = Facility43Description; }
                 else if (i == 44) { D = Facility44Description; }
                 else if (i == 45) { D = Facility45Description; }
                 else if (i == 46) { D = Facility46Description; }
                 else if (i == 47) { D = Facility47Description; }
                 else if (i == 48) { D = Facility48Description; }
                 else if (i == 49) { D = Facility49Description; }
                 else if (i == 50) { D = Facility50Description; }
                 else if (i == 51) { D = Facility51Description; }
                 else if (i == 52) { D = Facility52Description; }
                 else if (i == 53) { D = Facility53Description; }
                 else if (i == 54) { D = Facility54Description; }
                 else if (i == 55) { D = Facility55Description; }
                 else if (i == 56) { D = Facility56Description; }
                 else if (i == 57) { D = Facility57Description; }
                 else if (i == 58) { D = Facility58Description; }
                 else if (i == 59) { D = Facility59Description; }
                 else if (i == 60) { D = Facility60Description; }
                 else if (i == 61) { D = Facility61Description; }
                 else if (i == 62) { D = Facility62Description; }
                 else if (i == 63) { D = Facility63Description; }
                 else if (i == 64) { D = Facility64Description; }
                 else if (i == 65) { D = Facility65Description; }
                 else if (i == 66) { D = Facility66Description; }
                 else if (i == 67) { D = Facility67Description; }
                 else if (i == 68) { D = Facility68Description; }
                 else if (i == 69) { D = Facility69Description; }
                 else if (i == 70) { D = Facility70Description; }
                 else if (i == 71) { D = Facility71Description; }
                 else if (i == 72) { D = Facility72Description; }
                 else if (i == 73) { D = Facility73Description; }
                 else if (i == 74) { D = Facility74Description; }
                 else if (i == 75) { D = Facility75Description; }
                 else if (i == 76) { D = Facility76Description; }
                 else if (i == 77) { D = Facility77Description; }
                 else if (i == 78) { D = Facility78Description; }
                 else if (i == 79) { D = Facility79Description; }
                 else if (i == 80) { D = Facility80Description; }
                 else if (i == 81) { D = Facility81Description; }
                 else if (i == 82) { D = Facility82Description; }
                 else if (i == 83) { D = Facility83Description; }
                 else if (i == 84) { D = Facility84Description; }
                 else if (i == 85) { D = Facility85Description; }
                 else if (i == 86) { D = Facility86Description; }
                 else if (i == 87) { D = Facility87Description; }
                 else if (i == 88) { D = Facility88Description; }
                 else if (i == 89) { D = Facility89Description; }
                 else if (i == 90) { D = Facility90Description; }
                 else if (i == 91) { D = Facility91Description; }
                 else if (i == 92) { D = Facility92Description; }
                 else if (i == 93) { D = Facility93Description; }
                 else if (i == 94) { D = Facility94Description; }
                 else if (i == 95) { D = Facility95Description; }
                 else if (i == 96) { D = Facility96Description; }
                 else if (i == 97) { D = Facility97Description; }
                 else if (i == 98) { D = Facility98Description; }
                 else if (i == 99) { D = Facility99Description; }
                 else if (i == 100) { D = Facility100Description; }
                 else if (i == 101) { D = Facility101Description; }
                 else if (i == 102) { D = Facility102Description; }
                 else if (i == 103) { D = Facility103Description; }
                 else if (i == 104) { D = Facility104Description; }
                 else if (i == 105) { D = Facility105Description; }
                 else if (i == 106) { D = Facility106Description; }
                 else if (i == 107) { D = Facility107Description; }
                 else if (i == 108) { D = Facility108Description; }
                 else if (i == 109) { D = Facility109Description; }
                 else if (i == 110) { D = Facility110Description; }
                 else if (i == 111) { D = Facility111Description; }
                 else if (i == 112) { D = Facility112Description; }
                 else if (i == 113) { D = Facility113Description; }
                 else if (i == 114) { D = Facility114Description; }
                 else if (i == 115) { D = Facility115Description; }
                 else if (i == 116) { D = Facility116Description; }
                 else if (i == 117) { D = Facility117Description; }
                 else if (i == 118) { D = Facility118Description; }
                 else if (i == 119) { D = Facility119Description; }
                 else if (i == 120) { D = Facility120Description; }
                 else if (i == 121) { D = Facility121Description; }
                 else if (i == 122) { D = Facility122Description; }
                 else if (i == 123) { D = Facility123Description; }
                 else if (i == 124) { D = Facility124Description; }
                 else if (i == 125) { D = Facility125Description; }
                 else if (i == 126) { D = Facility126Description; }
                 else if (i == 127) { D = Facility127Description; }
                 else if (i == 128) { D = Facility128Description; }
                 else if (i == 129) { D = Facility129Description; }
                 else if (i == 130) { D = Facility130Description; }
                 else if (i == 131) { D = Facility131Description; }
                 else if (i == 132) { D = Facility132Description; }
                 else if (i == 133) { D = Facility133Description; }
                 else if (i == 134) { D = Facility134Description; }
                 else if (i == 135) { D = Facility135Description; }
                 else if (i == 136) { D = Facility136Description; }
                 else if (i == 137) { D = Facility137Description; }
                 else if (i == 138) { D = Facility138Description; }
                 else if (i == 139) { D = Facility139Description; }
                 else if (i == 140) { D = Facility140Description; }
                 else if (i == 141) { D = Facility141Description; }
                 else if (i == 142) { D = Facility142Description; }
                 else if (i == 143) { D = Facility143Description; }
                 else if (i == 144) { D = Facility144Description; }
                 else if (i == 145) { D = Facility145Description; }
                 else if (i == 146) { D = Facility146Description; }
                 else if (i == 147) { D = Facility147Description; }
                 else if (i == 148) { D = Facility148Description; }
                 else if (i == 149) { D = Facility149Description; }
                 else if (i == 150) { D = Facility150Description; }
                 else if (i == 151) { D = Facility151Description; }
                 else if (i == 152) { D = Facility152Description; }
                 else if (i == 153) { D = Facility153Description; }
                 else if (i == 154) { D = Facility154Description; }
                 else if (i == 155) { D = Facility155Description; }
                 else if (i == 156) { D = Facility156Description; }
                 else if (i == 157) { D = Facility157Description; }
                 else if (i == 158) { D = Facility158Description; }
                 else if (i == 159) { D = Facility159Description; }
                 else if (i == 160) { D = Facility160Description; }
                 else if (i == 161) { D = Facility161Description; }
                 else if (i == 162) { D = Facility162Description; }
                 else if (i == 163) { D = Facility163Description; }
                 else if (i == 164) { D = Facility164Description; }
                 else if (i == 165) { D = Facility165Description; }
                 else if (i == 166) { D = Facility166Description; }
                 else if (i == 167) { D = Facility167Description; }
                 else if (i == 168) { D = Facility168Description; }
                 else if (i == 169) { D = Facility169Description; }
                 else if (i == 170) { D = Facility170Description; }
                 else if (i == 171) { D = Facility171Description; }
                 else if (i == 172) { D = Facility172Description; }
                 else if (i == 173) { D = Facility173Description; }
                 else if (i == 174) { D = Facility174Description; }
                 else if (i == 175) { D = Facility175Description; }
                 else if (i == 176) { D = Facility176Description; }
                 else if (i == 177) { D = Facility177Description; }
                 else if (i == 178) { D = Facility178Description; }
                 else if (i == 179) { D = Facility179Description; }
                 else if (i == 180) { D = Facility180Description; }
                 else if (i == 181) { D = Facility181Description; }
                 else if (i == 182) { D = Facility182Description; }
                 else if (i == 183) { D = Facility183Description; }
                 else if (i == 184) { D = Facility184Description; }
                 else if (i == 185) { D = Facility185Description; }
                 else if (i == 186) { D = Facility186Description; }
                 else if (i == 187) { D = Facility187Description; }
                 else if (i == 188) { D = Facility188Description; }
                 else if (i == 189) { D = Facility189Description; }
                 else if (i == 190) { D = Facility190Description; }
                 else if (i == 191) { D = Facility191Description; }
                 else if (i == 192) { D = Facility192Description; }
                 else if (i == 193) { D = Facility193Description; }
                 else if (i == 194) { D = Facility194Description; }
                 else if (i == 195) { D = Facility195Description; }
                 else if (i == 196) { D = Facility196Description; }
                 else if (i == 197) { D = Facility197Description; }
                 else if (i == 198) { D = Facility198Description; }
                 else if (i == 199) { D = Facility199Description; }
                 else if (i == 200) { D = Facility200Description; }
            }
            else
            {
              D = this.ShowingArchitecture.GetFacilityDescriptionForKind(TheFacilityKind(i));           
            }
            return D;
        }
         private int TheFacilityPage(int i)
        {
            int p=-1;
            if(i==1){p=int.Parse(Facility1Page);}
            else if (i == 2) { p = int.Parse(Facility2Page); }
            else if (i == 3) { p = int.Parse(Facility3Page); }
            else if (i == 4) { p = int.Parse(Facility4Page); }
            else if (i == 5) { p = int.Parse(Facility5Page); }
            else if (i == 6) { p = int.Parse(Facility6Page); }
            else if (i == 7) { p = int.Parse(Facility7Page); }
            else if (i == 8) { p = int.Parse(Facility8Page); }
            else if (i == 9) { p = int.Parse(Facility9Page); }
            else if (i == 10) { p = int.Parse(Facility10Page); }
            else if (i == 11) { p = int.Parse(Facility11Page); }
            else if (i == 12) { p = int.Parse(Facility12Page); }
            else if (i == 13) { p = int.Parse(Facility13Page); }
            else if (i == 14) { p = int.Parse(Facility14Page); }
            else if (i == 15) { p = int.Parse(Facility15Page); }
            else if (i == 16) { p = int.Parse(Facility16Page); }
            else if (i == 17) { p = int.Parse(Facility17Page); }
            else if (i == 18) { p = int.Parse(Facility18Page); }
            else if (i == 19) { p = int.Parse(Facility19Page); }
            else if (i == 20) { p = int.Parse(Facility20Page); }
            else if (i == 21) { p = int.Parse(Facility21Page); }
            else if (i == 22) { p = int.Parse(Facility22Page); }
            else if (i == 23) { p = int.Parse(Facility23Page); }
            else if (i == 24) { p = int.Parse(Facility24Page); }
            else if (i == 25) { p = int.Parse(Facility25Page); }
            else if (i == 26) { p = int.Parse(Facility26Page); }
            else if (i == 27) { p = int.Parse(Facility27Page); }
            else if (i == 28) { p = int.Parse(Facility28Page); }
            else if (i == 29) { p = int.Parse(Facility29Page); }
            else if (i == 30) { p = int.Parse(Facility30Page); }
            else if (i == 31) { p = int.Parse(Facility31Page); }
            else if (i == 32) { p = int.Parse(Facility32Page); }
            else if (i == 33) { p = int.Parse(Facility33Page); }
            else if (i == 34) { p = int.Parse(Facility34Page); }
            else if (i == 35) { p = int.Parse(Facility35Page); }
            else if (i == 36) { p = int.Parse(Facility36Page); }
            else if (i == 37) { p = int.Parse(Facility37Page); }
            else if (i == 38) { p = int.Parse(Facility38Page); }
            else if (i == 39) { p = int.Parse(Facility39Page); }
            else if (i == 40) { p = int.Parse(Facility40Page); }
            else if (i == 41) { p = int.Parse(Facility41Page); }
            else if (i == 42) { p = int.Parse(Facility42Page); }
            else if (i == 43) { p = int.Parse(Facility43Page); }
            else if (i == 44) { p = int.Parse(Facility44Page); }
            else if (i == 45) { p = int.Parse(Facility45Page); }
            else if (i == 46) { p = int.Parse(Facility46Page); }
            else if (i == 47) { p = int.Parse(Facility47Page); }
            else if (i == 48) { p = int.Parse(Facility48Page); }
            else if (i == 49) { p = int.Parse(Facility49Page); }
            else if (i == 50) { p = int.Parse(Facility50Page); }
            else if (i == 51) { p = int.Parse(Facility51Page); }
            else if (i == 52) { p = int.Parse(Facility52Page); }
            else if (i == 53) { p = int.Parse(Facility53Page); }
            else if (i == 54) { p = int.Parse(Facility54Page); }
            else if (i == 55) { p = int.Parse(Facility55Page); }
            else if (i == 56) { p = int.Parse(Facility56Page); }
            else if (i == 57) { p = int.Parse(Facility57Page); }
            else if (i == 58) { p = int.Parse(Facility58Page); }
            else if (i == 59) { p = int.Parse(Facility59Page); }
            else if (i == 60) { p = int.Parse(Facility60Page); }
            else if (i == 61) { p = int.Parse(Facility61Page); }
            else if (i == 62) { p = int.Parse(Facility62Page); }
            else if (i == 63) { p = int.Parse(Facility63Page); }
            else if (i == 64) { p = int.Parse(Facility64Page); }
            else if (i == 65) { p = int.Parse(Facility65Page); }
            else if (i == 66) { p = int.Parse(Facility66Page); }
            else if (i == 67) { p = int.Parse(Facility67Page); }
            else if (i == 68) { p = int.Parse(Facility68Page); }
            else if (i == 69) { p = int.Parse(Facility69Page); }
            else if (i == 70) { p = int.Parse(Facility70Page); }
            else if (i == 71) { p = int.Parse(Facility71Page); }
            else if (i == 72) { p = int.Parse(Facility72Page); }
            else if (i == 73) { p = int.Parse(Facility73Page); }
            else if (i == 74) { p = int.Parse(Facility74Page); }
            else if (i == 75) { p = int.Parse(Facility75Page); }
            else if (i == 76) { p = int.Parse(Facility76Page); }
            else if (i == 77) { p = int.Parse(Facility77Page); }
            else if (i == 78) { p = int.Parse(Facility78Page); }
            else if (i == 79) { p = int.Parse(Facility79Page); }
            else if (i == 80) { p = int.Parse(Facility80Page); }
            else if (i == 81) { p = int.Parse(Facility81Page); }
            else if (i == 82) { p = int.Parse(Facility82Page); }
            else if (i == 83) { p = int.Parse(Facility83Page); }
            else if (i == 84) { p = int.Parse(Facility84Page); }
            else if (i == 85) { p = int.Parse(Facility85Page); }
            else if (i == 86) { p = int.Parse(Facility86Page); }
            else if (i == 87) { p = int.Parse(Facility87Page); }
            else if (i == 88) { p = int.Parse(Facility88Page); }
            else if (i == 89) { p = int.Parse(Facility89Page); }
            else if (i == 90) { p = int.Parse(Facility90Page); }
            else if (i == 91) { p = int.Parse(Facility91Page); }
            else if (i == 92) { p = int.Parse(Facility92Page); }
            else if (i == 93) { p = int.Parse(Facility93Page); }
            else if (i == 94) { p = int.Parse(Facility94Page); }
            else if (i == 95) { p = int.Parse(Facility95Page); }
            else if (i == 96) { p = int.Parse(Facility96Page); }
            else if (i == 97) { p = int.Parse(Facility97Page); }
            else if (i == 98) { p = int.Parse(Facility98Page); }
            else if (i == 99) { p = int.Parse(Facility99Page); }
            else if (i == 100) { p = int.Parse(Facility100Page); }
            else if (i == 101) { p = int.Parse(Facility101Page); }
            else if (i == 102) { p = int.Parse(Facility102Page); }
            else if (i == 103) { p = int.Parse(Facility103Page); }
            else if (i == 104) { p = int.Parse(Facility104Page); }
            else if (i == 105) { p = int.Parse(Facility105Page); }
            else if (i == 106) { p = int.Parse(Facility106Page); }
            else if (i == 107) { p = int.Parse(Facility107Page); }
            else if (i == 108) { p = int.Parse(Facility108Page); }
            else if (i == 109) { p = int.Parse(Facility109Page); }
            else if (i == 110) { p = int.Parse(Facility110Page); }
            else if (i == 111) { p = int.Parse(Facility111Page); }
            else if (i == 112) { p = int.Parse(Facility112Page); }
            else if (i == 113) { p = int.Parse(Facility113Page); }
            else if (i == 114) { p = int.Parse(Facility114Page); }
            else if (i == 115) { p = int.Parse(Facility115Page); }
            else if (i == 116) { p = int.Parse(Facility116Page); }
            else if (i == 117) { p = int.Parse(Facility117Page); }
            else if (i == 118) { p = int.Parse(Facility118Page); }
            else if (i == 119) { p = int.Parse(Facility119Page); }
            else if (i == 120) { p = int.Parse(Facility120Page); }
            else if (i == 121) { p = int.Parse(Facility121Page); }
            else if (i == 122) { p = int.Parse(Facility122Page); }
            else if (i == 123) { p = int.Parse(Facility123Page); }
            else if (i == 124) { p = int.Parse(Facility124Page); }
            else if (i == 125) { p = int.Parse(Facility125Page); }
            else if (i == 126) { p = int.Parse(Facility126Page); }
            else if (i == 127) { p = int.Parse(Facility127Page); }
            else if (i == 128) { p = int.Parse(Facility128Page); }
            else if (i == 129) { p = int.Parse(Facility129Page); }
            else if (i == 130) { p = int.Parse(Facility130Page); }
            else if (i == 131) { p = int.Parse(Facility131Page); }
            else if (i == 132) { p = int.Parse(Facility132Page); }
            else if (i == 133) { p = int.Parse(Facility133Page); }
            else if (i == 134) { p = int.Parse(Facility134Page); }
            else if (i == 135) { p = int.Parse(Facility135Page); }
            else if (i == 136) { p = int.Parse(Facility136Page); }
            else if (i == 137) { p = int.Parse(Facility137Page); }
            else if (i == 138) { p = int.Parse(Facility138Page); }
            else if (i == 139) { p = int.Parse(Facility139Page); }
            else if (i == 140) { p = int.Parse(Facility140Page); }
            else if (i == 141) { p = int.Parse(Facility141Page); }
            else if (i == 142) { p = int.Parse(Facility142Page); }
            else if (i == 143) { p = int.Parse(Facility143Page); }
            else if (i == 144) { p = int.Parse(Facility144Page); }
            else if (i == 145) { p = int.Parse(Facility145Page); }
            else if (i == 146) { p = int.Parse(Facility146Page); }
            else if (i == 147) { p = int.Parse(Facility147Page); }
            else if (i == 148) { p = int.Parse(Facility148Page); }
            else if (i == 149) { p = int.Parse(Facility149Page); }
            else if (i == 150) { p = int.Parse(Facility150Page); }
            else if (i == 151) { p = int.Parse(Facility151Page); }
            else if (i == 152) { p = int.Parse(Facility152Page); }
            else if (i == 153) { p = int.Parse(Facility153Page); }
            else if (i == 154) { p = int.Parse(Facility154Page); }
            else if (i == 155) { p = int.Parse(Facility155Page); }
            else if (i == 156) { p = int.Parse(Facility156Page); }
            else if (i == 157) { p = int.Parse(Facility157Page); }
            else if (i == 158) { p = int.Parse(Facility158Page); }
            else if (i == 159) { p = int.Parse(Facility159Page); }
            else if (i == 160) { p = int.Parse(Facility160Page); }
            else if (i == 161) { p = int.Parse(Facility161Page); }
            else if (i == 162) { p = int.Parse(Facility162Page); }
            else if (i == 163) { p = int.Parse(Facility163Page); }
            else if (i == 164) { p = int.Parse(Facility164Page); }
            else if (i == 165) { p = int.Parse(Facility165Page); }
            else if (i == 166) { p = int.Parse(Facility166Page); }
            else if (i == 167) { p = int.Parse(Facility167Page); }
            else if (i == 168) { p = int.Parse(Facility168Page); }
            else if (i == 169) { p = int.Parse(Facility169Page); }
            else if (i == 170) { p = int.Parse(Facility170Page); }
            else if (i == 171) { p = int.Parse(Facility171Page); }
            else if (i == 172) { p = int.Parse(Facility172Page); }
            else if (i == 173) { p = int.Parse(Facility173Page); }
            else if (i == 174) { p = int.Parse(Facility174Page); }
            else if (i == 175) { p = int.Parse(Facility175Page); }
            else if (i == 176) { p = int.Parse(Facility176Page); }
            else if (i == 177) { p = int.Parse(Facility177Page); }
            else if (i == 178) { p = int.Parse(Facility178Page); }
            else if (i == 179) { p = int.Parse(Facility179Page); }
            else if (i == 180) { p = int.Parse(Facility180Page); }
            else if (i == 181) { p = int.Parse(Facility181Page); }
            else if (i == 182) { p = int.Parse(Facility182Page); }
            else if (i == 183) { p = int.Parse(Facility183Page); }
            else if (i == 184) { p = int.Parse(Facility184Page); }
            else if (i == 185) { p = int.Parse(Facility185Page); }
            else if (i == 186) { p = int.Parse(Facility186Page); }
            else if (i == 187) { p = int.Parse(Facility187Page); }
            else if (i == 188) { p = int.Parse(Facility188Page); }
            else if (i == 189) { p = int.Parse(Facility189Page); }
            else if (i == 190) { p = int.Parse(Facility190Page); }
            else if (i == 191) { p = int.Parse(Facility191Page); }
            else if (i == 192) { p = int.Parse(Facility192Page); }
            else if (i == 193) { p = int.Parse(Facility193Page); }
            else if (i == 194) { p = int.Parse(Facility194Page); }
            else if (i == 195) { p = int.Parse(Facility195Page); }
            else if (i == 196) { p = int.Parse(Facility196Page); }
            else if (i == 197) { p = int.Parse(Facility197Page); }
            else if (i == 198) { p = int.Parse(Facility198Page); }
            else if (i == 199) { p = int.Parse(Facility199Page); }
            else if (i == 200) { p = int.Parse(Facility200Page); }
            return p;
        }
        private bool TheFacilityButton(int i)
       {
           bool H = false;
           H=TheFacilityPageButton(TheFacilityPage(i));
           return H;
       }
        private bool ShowTheFacilityKind(int i)
       {            
           bool H = false;
           if (HasTheFacilityKind(i) == true && TheFacilityButton(i) == true) { H = true; }           
           return H;
       }
        private PlatformTexture TheFacilityPictureA(int i)
        {
            PlatformTexture P=PictureNull;
            if(i==1){P=Facility1PictureA;}
            else if (i == 2) { P = Facility2PictureA; }
            else if (i == 3) { P = Facility3PictureA; }
            else if (i == 4) { P = Facility4PictureA; }
            else if (i == 5) { P = Facility5PictureA; }
            else if (i == 6) { P = Facility6PictureA; }
            else if (i == 7) { P = Facility7PictureA; }
            else if (i == 8) { P = Facility8PictureA; }
            else if (i == 9) { P = Facility9PictureA; }
            else if (i == 10) { P = Facility10PictureA; }
            else if (i == 11) { P = Facility11PictureA; }
            else if (i == 12) { P = Facility12PictureA; }
            else if (i == 13) { P = Facility13PictureA; }
            else if (i == 14) { P = Facility14PictureA; }
            else if (i == 15) { P = Facility15PictureA; }
            else if (i == 16) { P = Facility16PictureA; }
            else if (i == 17) { P = Facility17PictureA; }
            else if (i == 18) { P = Facility18PictureA; }
            else if (i == 19) { P = Facility19PictureA; }
            else if (i == 20) { P = Facility20PictureA; }
            else if (i == 21) { P = Facility21PictureA; }
            else if (i == 22) { P = Facility22PictureA; }
            else if (i == 23) { P = Facility23PictureA; }
            else if (i == 24) { P = Facility24PictureA; }
            else if (i == 25) { P = Facility25PictureA; }
            else if (i == 26) { P = Facility26PictureA; }
            else if (i == 27) { P = Facility27PictureA; }
            else if (i == 28) { P = Facility28PictureA; }
            else if (i == 29) { P = Facility29PictureA; }
            else if (i == 30) { P = Facility30PictureA; }
            else if (i == 31) { P = Facility31PictureA; }
            else if (i == 32) { P = Facility32PictureA; }
            else if (i == 33) { P = Facility33PictureA; }
            else if (i == 34) { P = Facility34PictureA; }
            else if (i == 35) { P = Facility35PictureA; }
            else if (i == 36) { P = Facility36PictureA; }
            else if (i == 37) { P = Facility37PictureA; }
            else if (i == 38) { P = Facility38PictureA; }
            else if (i == 39) { P = Facility39PictureA; }
            else if (i == 40) { P = Facility40PictureA; }
            else if (i == 41) { P = Facility41PictureA; }
            else if (i == 42) { P = Facility42PictureA; }
            else if (i == 43) { P = Facility43PictureA; }
            else if (i == 44) { P = Facility44PictureA; }
            else if (i == 45) { P = Facility45PictureA; }
            else if (i == 46) { P = Facility46PictureA; }
            else if (i == 47) { P = Facility47PictureA; }
            else if (i == 48) { P = Facility48PictureA; }
            else if (i == 49) { P = Facility49PictureA; }
            else if (i == 50) { P = Facility50PictureA; }
            else if (i == 51) { P = Facility51PictureA; }
            else if (i == 52) { P = Facility52PictureA; }
            else if (i == 53) { P = Facility53PictureA; }
            else if (i == 54) { P = Facility54PictureA; }
            else if (i == 55) { P = Facility55PictureA; }
            else if (i == 56) { P = Facility56PictureA; }
            else if (i == 57) { P = Facility57PictureA; }
            else if (i == 58) { P = Facility58PictureA; }
            else if (i == 59) { P = Facility59PictureA; }
            else if (i == 60) { P = Facility60PictureA; }
            else if (i == 61) { P = Facility61PictureA; }
            else if (i == 62) { P = Facility62PictureA; }
            else if (i == 63) { P = Facility63PictureA; }
            else if (i == 64) { P = Facility64PictureA; }
            else if (i == 65) { P = Facility65PictureA; }
            else if (i == 66) { P = Facility66PictureA; }
            else if (i == 67) { P = Facility67PictureA; }
            else if (i == 68) { P = Facility68PictureA; }
            else if (i == 69) { P = Facility69PictureA; }
            else if (i == 70) { P = Facility70PictureA; }
            else if (i == 71) { P = Facility71PictureA; }
            else if (i == 72) { P = Facility72PictureA; }
            else if (i == 73) { P = Facility73PictureA; }
            else if (i == 74) { P = Facility74PictureA; }
            else if (i == 75) { P = Facility75PictureA; }
            else if (i == 76) { P = Facility76PictureA; }
            else if (i == 77) { P = Facility77PictureA; }
            else if (i == 78) { P = Facility78PictureA; }
            else if (i == 79) { P = Facility79PictureA; }
            else if (i == 80) { P = Facility80PictureA; }
            else if (i == 81) { P = Facility81PictureA; }
            else if (i == 82) { P = Facility82PictureA; }
            else if (i == 83) { P = Facility83PictureA; }
            else if (i == 84) { P = Facility84PictureA; }
            else if (i == 85) { P = Facility85PictureA; }
            else if (i == 86) { P = Facility86PictureA; }
            else if (i == 87) { P = Facility87PictureA; }
            else if (i == 88) { P = Facility88PictureA; }
            else if (i == 89) { P = Facility89PictureA; }
            else if (i == 90) { P = Facility90PictureA; }
            else if (i == 91) { P = Facility91PictureA; }
            else if (i == 92) { P = Facility92PictureA; }
            else if (i == 93) { P = Facility93PictureA; }
            else if (i == 94) { P = Facility94PictureA; }
            else if (i == 95) { P = Facility95PictureA; }
            else if (i == 96) { P = Facility96PictureA; }
            else if (i == 97) { P = Facility97PictureA; }
            else if (i == 98) { P = Facility98PictureA; }
            else if (i == 99) { P = Facility99PictureA; }
            else if (i == 100) { P = Facility100PictureA; }
            else if (i == 101) { P = Facility101PictureA; }
            else if (i == 102) { P = Facility102PictureA; }
            else if (i == 103) { P = Facility103PictureA; }
            else if (i == 104) { P = Facility104PictureA; }
            else if (i == 105) { P = Facility105PictureA; }
            else if (i == 106) { P = Facility106PictureA; }
            else if (i == 107) { P = Facility107PictureA; }
            else if (i == 108) { P = Facility108PictureA; }
            else if (i == 109) { P = Facility109PictureA; }
            else if (i == 110) { P = Facility110PictureA; }
            else if (i == 111) { P = Facility111PictureA; }
            else if (i == 112) { P = Facility112PictureA; }
            else if (i == 113) { P = Facility113PictureA; }
            else if (i == 114) { P = Facility114PictureA; }
            else if (i == 115) { P = Facility115PictureA; }
            else if (i == 116) { P = Facility116PictureA; }
            else if (i == 117) { P = Facility117PictureA; }
            else if (i == 118) { P = Facility118PictureA; }
            else if (i == 119) { P = Facility119PictureA; }
            else if (i == 120) { P = Facility120PictureA; }
            else if (i == 121) { P = Facility121PictureA; }
            else if (i == 122) { P = Facility122PictureA; }
            else if (i == 123) { P = Facility123PictureA; }
            else if (i == 124) { P = Facility124PictureA; }
            else if (i == 125) { P = Facility125PictureA; }
            else if (i == 126) { P = Facility126PictureA; }
            else if (i == 127) { P = Facility127PictureA; }
            else if (i == 128) { P = Facility128PictureA; }
            else if (i == 129) { P = Facility129PictureA; }
            else if (i == 130) { P = Facility130PictureA; }
            else if (i == 131) { P = Facility131PictureA; }
            else if (i == 132) { P = Facility132PictureA; }
            else if (i == 133) { P = Facility133PictureA; }
            else if (i == 134) { P = Facility134PictureA; }
            else if (i == 135) { P = Facility135PictureA; }
            else if (i == 136) { P = Facility136PictureA; }
            else if (i == 137) { P = Facility137PictureA; }
            else if (i == 138) { P = Facility138PictureA; }
            else if (i == 139) { P = Facility139PictureA; }
            else if (i == 140) { P = Facility140PictureA; }
            else if (i == 141) { P = Facility141PictureA; }
            else if (i == 142) { P = Facility142PictureA; }
            else if (i == 143) { P = Facility143PictureA; }
            else if (i == 144) { P = Facility144PictureA; }
            else if (i == 145) { P = Facility145PictureA; }
            else if (i == 146) { P = Facility146PictureA; }
            else if (i == 147) { P = Facility147PictureA; }
            else if (i == 148) { P = Facility148PictureA; }
            else if (i == 149) { P = Facility149PictureA; }
            else if (i == 150) { P = Facility150PictureA; }
            else if (i == 151) { P = Facility151PictureA; }
            else if (i == 152) { P = Facility152PictureA; }
            else if (i == 153) { P = Facility153PictureA; }
            else if (i == 154) { P = Facility154PictureA; }
            else if (i == 155) { P = Facility155PictureA; }
            else if (i == 156) { P = Facility156PictureA; }
            else if (i == 157) { P = Facility157PictureA; }
            else if (i == 158) { P = Facility158PictureA; }
            else if (i == 159) { P = Facility159PictureA; }
            else if (i == 160) { P = Facility160PictureA; }
            else if (i == 161) { P = Facility161PictureA; }
            else if (i == 162) { P = Facility162PictureA; }
            else if (i == 163) { P = Facility163PictureA; }
            else if (i == 164) { P = Facility164PictureA; }
            else if (i == 165) { P = Facility165PictureA; }
            else if (i == 166) { P = Facility166PictureA; }
            else if (i == 167) { P = Facility167PictureA; }
            else if (i == 168) { P = Facility168PictureA; }
            else if (i == 169) { P = Facility169PictureA; }
            else if (i == 170) { P = Facility170PictureA; }
            else if (i == 171) { P = Facility171PictureA; }
            else if (i == 172) { P = Facility172PictureA; }
            else if (i == 173) { P = Facility173PictureA; }
            else if (i == 174) { P = Facility174PictureA; }
            else if (i == 175) { P = Facility175PictureA; }
            else if (i == 176) { P = Facility176PictureA; }
            else if (i == 177) { P = Facility177PictureA; }
            else if (i == 178) { P = Facility178PictureA; }
            else if (i == 179) { P = Facility179PictureA; }
            else if (i == 180) { P = Facility180PictureA; }
            else if (i == 181) { P = Facility181PictureA; }
            else if (i == 182) { P = Facility182PictureA; }
            else if (i == 183) { P = Facility183PictureA; }
            else if (i == 184) { P = Facility184PictureA; }
            else if (i == 185) { P = Facility185PictureA; }
            else if (i == 186) { P = Facility186PictureA; }
            else if (i == 187) { P = Facility187PictureA; }
            else if (i == 188) { P = Facility188PictureA; }
            else if (i == 189) { P = Facility189PictureA; }
            else if (i == 190) { P = Facility190PictureA; }
            else if (i == 191) { P = Facility191PictureA; }
            else if (i == 192) { P = Facility192PictureA; }
            else if (i == 193) { P = Facility193PictureA; }
            else if (i == 194) { P = Facility194PictureA; }
            else if (i == 195) { P = Facility195PictureA; }
            else if (i == 196) { P = Facility196PictureA; }
            else if (i == 197) { P = Facility197PictureA; }
            else if (i == 198) { P = Facility198PictureA; }
            else if (i == 199) { P = Facility199PictureA; }
            else if (i == 200) { P = Facility200PictureA; }
            return P;
        }
        private PlatformTexture TheFacilityPictureB(int i)
        {
            PlatformTexture P=PictureNull;
            if(i==1){P=Facility1PictureB;}
            else if (i == 2) { P = Facility2PictureB; }
            else if (i == 3) { P = Facility3PictureB; }
            else if (i == 4) { P = Facility4PictureB; }
            else if (i == 5) { P = Facility5PictureB; }
            else if (i == 6) { P = Facility6PictureB; }
            else if (i == 7) { P = Facility7PictureB; }
            else if (i == 8) { P = Facility8PictureB; }
            else if (i == 9) { P = Facility9PictureB; }
            else if (i == 10) { P = Facility10PictureB; }
            else if (i == 11) { P = Facility11PictureB; }
            else if (i == 12) { P = Facility12PictureB; }
            else if (i == 13) { P = Facility13PictureB; }
            else if (i == 14) { P = Facility14PictureB; }
            else if (i == 15) { P = Facility15PictureB; }
            else if (i == 16) { P = Facility16PictureB; }
            else if (i == 17) { P = Facility17PictureB; }
            else if (i == 18) { P = Facility18PictureB; }
            else if (i == 19) { P = Facility19PictureB; }
            else if (i == 20) { P = Facility20PictureB; }
            else if (i == 21) { P = Facility21PictureB; }
            else if (i == 22) { P = Facility22PictureB; }
            else if (i == 23) { P = Facility23PictureB; }
            else if (i == 24) { P = Facility24PictureB; }
            else if (i == 25) { P = Facility25PictureB; }
            else if (i == 26) { P = Facility26PictureB; }
            else if (i == 27) { P = Facility27PictureB; }
            else if (i == 28) { P = Facility28PictureB; }
            else if (i == 29) { P = Facility29PictureB; }
            else if (i == 30) { P = Facility30PictureB; }
            else if (i == 31) { P = Facility31PictureB; }
            else if (i == 32) { P = Facility32PictureB; }
            else if (i == 33) { P = Facility33PictureB; }
            else if (i == 34) { P = Facility34PictureB; }
            else if (i == 35) { P = Facility35PictureB; }
            else if (i == 36) { P = Facility36PictureB; }
            else if (i == 37) { P = Facility37PictureB; }
            else if (i == 38) { P = Facility38PictureB; }
            else if (i == 39) { P = Facility39PictureB; }
            else if (i == 40) { P = Facility40PictureB; }
            else if (i == 41) { P = Facility41PictureB; }
            else if (i == 42) { P = Facility42PictureB; }
            else if (i == 43) { P = Facility43PictureB; }
            else if (i == 44) { P = Facility44PictureB; }
            else if (i == 45) { P = Facility45PictureB; }
            else if (i == 46) { P = Facility46PictureB; }
            else if (i == 47) { P = Facility47PictureB; }
            else if (i == 48) { P = Facility48PictureB; }
            else if (i == 49) { P = Facility49PictureB; }
            else if (i == 50) { P = Facility50PictureB; }
            else if (i == 51) { P = Facility51PictureB; }
            else if (i == 52) { P = Facility52PictureB; }
            else if (i == 53) { P = Facility53PictureB; }
            else if (i == 54) { P = Facility54PictureB; }
            else if (i == 55) { P = Facility55PictureB; }
            else if (i == 56) { P = Facility56PictureB; }
            else if (i == 57) { P = Facility57PictureB; }
            else if (i == 58) { P = Facility58PictureB; }
            else if (i == 59) { P = Facility59PictureB; }
            else if (i == 60) { P = Facility60PictureB; }
            else if (i == 61) { P = Facility61PictureB; }
            else if (i == 62) { P = Facility62PictureB; }
            else if (i == 63) { P = Facility63PictureB; }
            else if (i == 64) { P = Facility64PictureB; }
            else if (i == 65) { P = Facility65PictureB; }
            else if (i == 66) { P = Facility66PictureB; }
            else if (i == 67) { P = Facility67PictureB; }
            else if (i == 68) { P = Facility68PictureB; }
            else if (i == 69) { P = Facility69PictureB; }
            else if (i == 70) { P = Facility70PictureB; }
            else if (i == 71) { P = Facility71PictureB; }
            else if (i == 72) { P = Facility72PictureB; }
            else if (i == 73) { P = Facility73PictureB; }
            else if (i == 74) { P = Facility74PictureB; }
            else if (i == 75) { P = Facility75PictureB; }
            else if (i == 76) { P = Facility76PictureB; }
            else if (i == 77) { P = Facility77PictureB; }
            else if (i == 78) { P = Facility78PictureB; }
            else if (i == 79) { P = Facility79PictureB; }
            else if (i == 80) { P = Facility80PictureB; }
            else if (i == 81) { P = Facility81PictureB; }
            else if (i == 82) { P = Facility82PictureB; }
            else if (i == 83) { P = Facility83PictureB; }
            else if (i == 84) { P = Facility84PictureB; }
            else if (i == 85) { P = Facility85PictureB; }
            else if (i == 86) { P = Facility86PictureB; }
            else if (i == 87) { P = Facility87PictureB; }
            else if (i == 88) { P = Facility88PictureB; }
            else if (i == 89) { P = Facility89PictureB; }
            else if (i == 90) { P = Facility90PictureB; }
            else if (i == 91) { P = Facility91PictureB; }
            else if (i == 92) { P = Facility92PictureB; }
            else if (i == 93) { P = Facility93PictureB; }
            else if (i == 94) { P = Facility94PictureB; }
            else if (i == 95) { P = Facility95PictureB; }
            else if (i == 96) { P = Facility96PictureB; }
            else if (i == 97) { P = Facility97PictureB; }
            else if (i == 98) { P = Facility98PictureB; }
            else if (i == 99) { P = Facility99PictureB; }
            else if (i == 100) { P = Facility100PictureB; }
            else if (i == 101) { P = Facility101PictureB; }
            else if (i == 102) { P = Facility102PictureB; }
            else if (i == 103) { P = Facility103PictureB; }
            else if (i == 104) { P = Facility104PictureB; }
            else if (i == 105) { P = Facility105PictureB; }
            else if (i == 106) { P = Facility106PictureB; }
            else if (i == 107) { P = Facility107PictureB; }
            else if (i == 108) { P = Facility108PictureB; }
            else if (i == 109) { P = Facility109PictureB; }
            else if (i == 110) { P = Facility110PictureB; }
            else if (i == 111) { P = Facility111PictureB; }
            else if (i == 112) { P = Facility112PictureB; }
            else if (i == 113) { P = Facility113PictureB; }
            else if (i == 114) { P = Facility114PictureB; }
            else if (i == 115) { P = Facility115PictureB; }
            else if (i == 116) { P = Facility116PictureB; }
            else if (i == 117) { P = Facility117PictureB; }
            else if (i == 118) { P = Facility118PictureB; }
            else if (i == 119) { P = Facility119PictureB; }
            else if (i == 120) { P = Facility120PictureB; }
            else if (i == 121) { P = Facility121PictureB; }
            else if (i == 122) { P = Facility122PictureB; }
            else if (i == 123) { P = Facility123PictureB; }
            else if (i == 124) { P = Facility124PictureB; }
            else if (i == 125) { P = Facility125PictureB; }
            else if (i == 126) { P = Facility126PictureB; }
            else if (i == 127) { P = Facility127PictureB; }
            else if (i == 128) { P = Facility128PictureB; }
            else if (i == 129) { P = Facility129PictureB; }
            else if (i == 130) { P = Facility130PictureB; }
            else if (i == 131) { P = Facility131PictureB; }
            else if (i == 132) { P = Facility132PictureB; }
            else if (i == 133) { P = Facility133PictureB; }
            else if (i == 134) { P = Facility134PictureB; }
            else if (i == 135) { P = Facility135PictureB; }
            else if (i == 136) { P = Facility136PictureB; }
            else if (i == 137) { P = Facility137PictureB; }
            else if (i == 138) { P = Facility138PictureB; }
            else if (i == 139) { P = Facility139PictureB; }
            else if (i == 140) { P = Facility140PictureB; }
            else if (i == 141) { P = Facility141PictureB; }
            else if (i == 142) { P = Facility142PictureB; }
            else if (i == 143) { P = Facility143PictureB; }
            else if (i == 144) { P = Facility144PictureB; }
            else if (i == 145) { P = Facility145PictureB; }
            else if (i == 146) { P = Facility146PictureB; }
            else if (i == 147) { P = Facility147PictureB; }
            else if (i == 148) { P = Facility148PictureB; }
            else if (i == 149) { P = Facility149PictureB; }
            else if (i == 150) { P = Facility150PictureB; }
            else if (i == 151) { P = Facility151PictureB; }
            else if (i == 152) { P = Facility152PictureB; }
            else if (i == 153) { P = Facility153PictureB; }
            else if (i == 154) { P = Facility154PictureB; }
            else if (i == 155) { P = Facility155PictureB; }
            else if (i == 156) { P = Facility156PictureB; }
            else if (i == 157) { P = Facility157PictureB; }
            else if (i == 158) { P = Facility158PictureB; }
            else if (i == 159) { P = Facility159PictureB; }
            else if (i == 160) { P = Facility160PictureB; }
            else if (i == 161) { P = Facility161PictureB; }
            else if (i == 162) { P = Facility162PictureB; }
            else if (i == 163) { P = Facility163PictureB; }
            else if (i == 164) { P = Facility164PictureB; }
            else if (i == 165) { P = Facility165PictureB; }
            else if (i == 166) { P = Facility166PictureB; }
            else if (i == 167) { P = Facility167PictureB; }
            else if (i == 168) { P = Facility168PictureB; }
            else if (i == 169) { P = Facility169PictureB; }
            else if (i == 170) { P = Facility170PictureB; }
            else if (i == 171) { P = Facility171PictureB; }
            else if (i == 172) { P = Facility172PictureB; }
            else if (i == 173) { P = Facility173PictureB; }
            else if (i == 174) { P = Facility174PictureB; }
            else if (i == 175) { P = Facility175PictureB; }
            else if (i == 176) { P = Facility176PictureB; }
            else if (i == 177) { P = Facility177PictureB; }
            else if (i == 178) { P = Facility178PictureB; }
            else if (i == 179) { P = Facility179PictureB; }
            else if (i == 180) { P = Facility180PictureB; }
            else if (i == 181) { P = Facility181PictureB; }
            else if (i == 182) { P = Facility182PictureB; }
            else if (i == 183) { P = Facility183PictureB; }
            else if (i == 184) { P = Facility184PictureB; }
            else if (i == 185) { P = Facility185PictureB; }
            else if (i == 186) { P = Facility186PictureB; }
            else if (i == 187) { P = Facility187PictureB; }
            else if (i == 188) { P = Facility188PictureB; }
            else if (i == 189) { P = Facility189PictureB; }
            else if (i == 190) { P = Facility190PictureB; }
            else if (i == 191) { P = Facility191PictureB; }
            else if (i == 192) { P = Facility192PictureB; }
            else if (i == 193) { P = Facility193PictureB; }
            else if (i == 194) { P = Facility194PictureB; }
            else if (i == 195) { P = Facility195PictureB; }
            else if (i == 196) { P = Facility196PictureB; }
            else if (i == 197) { P = Facility197PictureB; }
            else if (i == 198) { P = Facility198PictureB; }
            else if (i == 199) { P = Facility199PictureB; }
            else if (i == 200) { P = Facility200PictureB; }
            return P;
        }
        private Rectangle TheFacilityDisplayPosition(int i)
        {
            Rectangle D = this.NullDisplayPosition;
            if (i == 1) { D = new Rectangle(this.Facility1Client.X + this.DisplayOffset.X, this.Facility1Client.Y + this.DisplayOffset.Y, this.Facility1Client.Width, this.Facility1Client.Height); }
            else if (i == 2) { D = new Rectangle(this.Facility2Client.X + this.DisplayOffset.X, this.Facility2Client.Y + this.DisplayOffset.Y, this.Facility2Client.Width, this.Facility2Client.Height); }
            else if (i == 3) { D = new Rectangle(this.Facility3Client.X + this.DisplayOffset.X, this.Facility3Client.Y + this.DisplayOffset.Y, this.Facility3Client.Width, this.Facility3Client.Height); }
            else if (i == 4) { D = new Rectangle(this.Facility4Client.X + this.DisplayOffset.X, this.Facility4Client.Y + this.DisplayOffset.Y, this.Facility4Client.Width, this.Facility4Client.Height); }
            else if (i == 5) { D = new Rectangle(this.Facility5Client.X + this.DisplayOffset.X, this.Facility5Client.Y + this.DisplayOffset.Y, this.Facility5Client.Width, this.Facility5Client.Height); }
            else if (i == 6) { D = new Rectangle(this.Facility6Client.X + this.DisplayOffset.X, this.Facility6Client.Y + this.DisplayOffset.Y, this.Facility6Client.Width, this.Facility6Client.Height); }
            else if (i == 7) { D = new Rectangle(this.Facility7Client.X + this.DisplayOffset.X, this.Facility7Client.Y + this.DisplayOffset.Y, this.Facility7Client.Width, this.Facility7Client.Height); }
            else if (i == 8) { D = new Rectangle(this.Facility8Client.X + this.DisplayOffset.X, this.Facility8Client.Y + this.DisplayOffset.Y, this.Facility8Client.Width, this.Facility8Client.Height); }
            else if (i == 9) { D = new Rectangle(this.Facility9Client.X + this.DisplayOffset.X, this.Facility9Client.Y + this.DisplayOffset.Y, this.Facility9Client.Width, this.Facility9Client.Height); }
            else if (i == 10) { D = new Rectangle(this.Facility10Client.X + this.DisplayOffset.X, this.Facility10Client.Y + this.DisplayOffset.Y, this.Facility10Client.Width, this.Facility10Client.Height); }
            else if (i == 11) { D = new Rectangle(this.Facility11Client.X + this.DisplayOffset.X, this.Facility11Client.Y + this.DisplayOffset.Y, this.Facility11Client.Width, this.Facility11Client.Height); }
            else if (i == 12) { D = new Rectangle(this.Facility12Client.X + this.DisplayOffset.X, this.Facility12Client.Y + this.DisplayOffset.Y, this.Facility12Client.Width, this.Facility12Client.Height); }
            else if (i == 13) { D = new Rectangle(this.Facility13Client.X + this.DisplayOffset.X, this.Facility13Client.Y + this.DisplayOffset.Y, this.Facility13Client.Width, this.Facility13Client.Height); }
            else if (i == 14) { D = new Rectangle(this.Facility14Client.X + this.DisplayOffset.X, this.Facility14Client.Y + this.DisplayOffset.Y, this.Facility14Client.Width, this.Facility14Client.Height); }
            else if (i == 15) { D = new Rectangle(this.Facility15Client.X + this.DisplayOffset.X, this.Facility15Client.Y + this.DisplayOffset.Y, this.Facility15Client.Width, this.Facility15Client.Height); }
            else if (i == 16) { D = new Rectangle(this.Facility16Client.X + this.DisplayOffset.X, this.Facility16Client.Y + this.DisplayOffset.Y, this.Facility16Client.Width, this.Facility16Client.Height); }
            else if (i == 17) { D = new Rectangle(this.Facility17Client.X + this.DisplayOffset.X, this.Facility17Client.Y + this.DisplayOffset.Y, this.Facility17Client.Width, this.Facility17Client.Height); }
            else if (i == 18) { D = new Rectangle(this.Facility18Client.X + this.DisplayOffset.X, this.Facility18Client.Y + this.DisplayOffset.Y, this.Facility18Client.Width, this.Facility18Client.Height); }
            else if (i == 19) { D = new Rectangle(this.Facility19Client.X + this.DisplayOffset.X, this.Facility19Client.Y + this.DisplayOffset.Y, this.Facility19Client.Width, this.Facility19Client.Height); }
            else if (i == 20) { D = new Rectangle(this.Facility20Client.X + this.DisplayOffset.X, this.Facility20Client.Y + this.DisplayOffset.Y, this.Facility20Client.Width, this.Facility20Client.Height); }
            else if (i == 21) { D = new Rectangle(this.Facility21Client.X + this.DisplayOffset.X, this.Facility21Client.Y + this.DisplayOffset.Y, this.Facility21Client.Width, this.Facility21Client.Height); }
            else if (i == 22) { D = new Rectangle(this.Facility22Client.X + this.DisplayOffset.X, this.Facility22Client.Y + this.DisplayOffset.Y, this.Facility22Client.Width, this.Facility22Client.Height); }
            else if (i == 23) { D = new Rectangle(this.Facility23Client.X + this.DisplayOffset.X, this.Facility23Client.Y + this.DisplayOffset.Y, this.Facility23Client.Width, this.Facility23Client.Height); }
            else if (i == 24) { D = new Rectangle(this.Facility24Client.X + this.DisplayOffset.X, this.Facility24Client.Y + this.DisplayOffset.Y, this.Facility24Client.Width, this.Facility24Client.Height); }
            else if (i == 25) { D = new Rectangle(this.Facility25Client.X + this.DisplayOffset.X, this.Facility25Client.Y + this.DisplayOffset.Y, this.Facility25Client.Width, this.Facility25Client.Height); }
            else if (i == 26) { D = new Rectangle(this.Facility26Client.X + this.DisplayOffset.X, this.Facility26Client.Y + this.DisplayOffset.Y, this.Facility26Client.Width, this.Facility26Client.Height); }
            else if (i == 27) { D = new Rectangle(this.Facility27Client.X + this.DisplayOffset.X, this.Facility27Client.Y + this.DisplayOffset.Y, this.Facility27Client.Width, this.Facility27Client.Height); }
            else if (i == 28) { D = new Rectangle(this.Facility28Client.X + this.DisplayOffset.X, this.Facility28Client.Y + this.DisplayOffset.Y, this.Facility28Client.Width, this.Facility28Client.Height); }
            else if (i == 29) { D = new Rectangle(this.Facility29Client.X + this.DisplayOffset.X, this.Facility29Client.Y + this.DisplayOffset.Y, this.Facility29Client.Width, this.Facility29Client.Height); }
            else if (i == 30) { D = new Rectangle(this.Facility30Client.X + this.DisplayOffset.X, this.Facility30Client.Y + this.DisplayOffset.Y, this.Facility30Client.Width, this.Facility30Client.Height); }
            else if (i == 31) { D = new Rectangle(this.Facility31Client.X + this.DisplayOffset.X, this.Facility31Client.Y + this.DisplayOffset.Y, this.Facility31Client.Width, this.Facility31Client.Height); }
            else if (i == 32) { D = new Rectangle(this.Facility32Client.X + this.DisplayOffset.X, this.Facility32Client.Y + this.DisplayOffset.Y, this.Facility32Client.Width, this.Facility32Client.Height); }
            else if (i == 33) { D = new Rectangle(this.Facility33Client.X + this.DisplayOffset.X, this.Facility33Client.Y + this.DisplayOffset.Y, this.Facility33Client.Width, this.Facility33Client.Height); }
            else if (i == 34) { D = new Rectangle(this.Facility34Client.X + this.DisplayOffset.X, this.Facility34Client.Y + this.DisplayOffset.Y, this.Facility34Client.Width, this.Facility34Client.Height); }
            else if (i == 35) { D = new Rectangle(this.Facility35Client.X + this.DisplayOffset.X, this.Facility35Client.Y + this.DisplayOffset.Y, this.Facility35Client.Width, this.Facility35Client.Height); }
            else if (i == 36) { D = new Rectangle(this.Facility36Client.X + this.DisplayOffset.X, this.Facility36Client.Y + this.DisplayOffset.Y, this.Facility36Client.Width, this.Facility36Client.Height); }
            else if (i == 37) { D = new Rectangle(this.Facility37Client.X + this.DisplayOffset.X, this.Facility37Client.Y + this.DisplayOffset.Y, this.Facility37Client.Width, this.Facility37Client.Height); }
            else if (i == 38) { D = new Rectangle(this.Facility38Client.X + this.DisplayOffset.X, this.Facility38Client.Y + this.DisplayOffset.Y, this.Facility38Client.Width, this.Facility38Client.Height); }
            else if (i == 39) { D = new Rectangle(this.Facility39Client.X + this.DisplayOffset.X, this.Facility39Client.Y + this.DisplayOffset.Y, this.Facility39Client.Width, this.Facility39Client.Height); }
            else if (i == 40) { D = new Rectangle(this.Facility40Client.X + this.DisplayOffset.X, this.Facility40Client.Y + this.DisplayOffset.Y, this.Facility40Client.Width, this.Facility40Client.Height); }
            else if (i == 41) { D = new Rectangle(this.Facility41Client.X + this.DisplayOffset.X, this.Facility41Client.Y + this.DisplayOffset.Y, this.Facility41Client.Width, this.Facility41Client.Height); }
            else if (i == 42) { D = new Rectangle(this.Facility42Client.X + this.DisplayOffset.X, this.Facility42Client.Y + this.DisplayOffset.Y, this.Facility42Client.Width, this.Facility42Client.Height); }
            else if (i == 43) { D = new Rectangle(this.Facility43Client.X + this.DisplayOffset.X, this.Facility43Client.Y + this.DisplayOffset.Y, this.Facility43Client.Width, this.Facility43Client.Height); }
            else if (i == 44) { D = new Rectangle(this.Facility44Client.X + this.DisplayOffset.X, this.Facility44Client.Y + this.DisplayOffset.Y, this.Facility44Client.Width, this.Facility44Client.Height); }
            else if (i == 45) { D = new Rectangle(this.Facility45Client.X + this.DisplayOffset.X, this.Facility45Client.Y + this.DisplayOffset.Y, this.Facility45Client.Width, this.Facility45Client.Height); }
            else if (i == 46) { D = new Rectangle(this.Facility46Client.X + this.DisplayOffset.X, this.Facility46Client.Y + this.DisplayOffset.Y, this.Facility46Client.Width, this.Facility46Client.Height); }
            else if (i == 47) { D = new Rectangle(this.Facility47Client.X + this.DisplayOffset.X, this.Facility47Client.Y + this.DisplayOffset.Y, this.Facility47Client.Width, this.Facility47Client.Height); }
            else if (i == 48) { D = new Rectangle(this.Facility48Client.X + this.DisplayOffset.X, this.Facility48Client.Y + this.DisplayOffset.Y, this.Facility48Client.Width, this.Facility48Client.Height); }
            else if (i == 49) { D = new Rectangle(this.Facility49Client.X + this.DisplayOffset.X, this.Facility49Client.Y + this.DisplayOffset.Y, this.Facility49Client.Width, this.Facility49Client.Height); }
            else if (i == 50) { D = new Rectangle(this.Facility50Client.X + this.DisplayOffset.X, this.Facility50Client.Y + this.DisplayOffset.Y, this.Facility50Client.Width, this.Facility50Client.Height); }
            else if (i == 51) { D = new Rectangle(this.Facility51Client.X + this.DisplayOffset.X, this.Facility51Client.Y + this.DisplayOffset.Y, this.Facility51Client.Width, this.Facility51Client.Height); }
            else if (i == 52) { D = new Rectangle(this.Facility52Client.X + this.DisplayOffset.X, this.Facility52Client.Y + this.DisplayOffset.Y, this.Facility52Client.Width, this.Facility52Client.Height); }
            else if (i == 53) { D = new Rectangle(this.Facility53Client.X + this.DisplayOffset.X, this.Facility53Client.Y + this.DisplayOffset.Y, this.Facility53Client.Width, this.Facility53Client.Height); }
            else if (i == 54) { D = new Rectangle(this.Facility54Client.X + this.DisplayOffset.X, this.Facility54Client.Y + this.DisplayOffset.Y, this.Facility54Client.Width, this.Facility54Client.Height); }
            else if (i == 55) { D = new Rectangle(this.Facility55Client.X + this.DisplayOffset.X, this.Facility55Client.Y + this.DisplayOffset.Y, this.Facility55Client.Width, this.Facility55Client.Height); }
            else if (i == 56) { D = new Rectangle(this.Facility56Client.X + this.DisplayOffset.X, this.Facility56Client.Y + this.DisplayOffset.Y, this.Facility56Client.Width, this.Facility56Client.Height); }
            else if (i == 57) { D = new Rectangle(this.Facility57Client.X + this.DisplayOffset.X, this.Facility57Client.Y + this.DisplayOffset.Y, this.Facility57Client.Width, this.Facility57Client.Height); }
            else if (i == 58) { D = new Rectangle(this.Facility58Client.X + this.DisplayOffset.X, this.Facility58Client.Y + this.DisplayOffset.Y, this.Facility58Client.Width, this.Facility58Client.Height); }
            else if (i == 59) { D = new Rectangle(this.Facility59Client.X + this.DisplayOffset.X, this.Facility59Client.Y + this.DisplayOffset.Y, this.Facility59Client.Width, this.Facility59Client.Height); }
            else if (i == 60) { D = new Rectangle(this.Facility60Client.X + this.DisplayOffset.X, this.Facility60Client.Y + this.DisplayOffset.Y, this.Facility60Client.Width, this.Facility60Client.Height); }
            else if (i == 61) { D = new Rectangle(this.Facility61Client.X + this.DisplayOffset.X, this.Facility61Client.Y + this.DisplayOffset.Y, this.Facility61Client.Width, this.Facility61Client.Height); }
            else if (i == 62) { D = new Rectangle(this.Facility62Client.X + this.DisplayOffset.X, this.Facility62Client.Y + this.DisplayOffset.Y, this.Facility62Client.Width, this.Facility62Client.Height); }
            else if (i == 63) { D = new Rectangle(this.Facility63Client.X + this.DisplayOffset.X, this.Facility63Client.Y + this.DisplayOffset.Y, this.Facility63Client.Width, this.Facility63Client.Height); }
            else if (i == 64) { D = new Rectangle(this.Facility64Client.X + this.DisplayOffset.X, this.Facility64Client.Y + this.DisplayOffset.Y, this.Facility64Client.Width, this.Facility64Client.Height); }
            else if (i == 65) { D = new Rectangle(this.Facility65Client.X + this.DisplayOffset.X, this.Facility65Client.Y + this.DisplayOffset.Y, this.Facility65Client.Width, this.Facility65Client.Height); }
            else if (i == 66) { D = new Rectangle(this.Facility66Client.X + this.DisplayOffset.X, this.Facility66Client.Y + this.DisplayOffset.Y, this.Facility66Client.Width, this.Facility66Client.Height); }
            else if (i == 67) { D = new Rectangle(this.Facility67Client.X + this.DisplayOffset.X, this.Facility67Client.Y + this.DisplayOffset.Y, this.Facility67Client.Width, this.Facility67Client.Height); }
            else if (i == 68) { D = new Rectangle(this.Facility68Client.X + this.DisplayOffset.X, this.Facility68Client.Y + this.DisplayOffset.Y, this.Facility68Client.Width, this.Facility68Client.Height); }
            else if (i == 69) { D = new Rectangle(this.Facility69Client.X + this.DisplayOffset.X, this.Facility69Client.Y + this.DisplayOffset.Y, this.Facility69Client.Width, this.Facility69Client.Height); }
            else if (i == 70) { D = new Rectangle(this.Facility70Client.X + this.DisplayOffset.X, this.Facility70Client.Y + this.DisplayOffset.Y, this.Facility70Client.Width, this.Facility70Client.Height); }
            else if (i == 71) { D = new Rectangle(this.Facility71Client.X + this.DisplayOffset.X, this.Facility71Client.Y + this.DisplayOffset.Y, this.Facility71Client.Width, this.Facility71Client.Height); }
            else if (i == 72) { D = new Rectangle(this.Facility72Client.X + this.DisplayOffset.X, this.Facility72Client.Y + this.DisplayOffset.Y, this.Facility72Client.Width, this.Facility72Client.Height); }
            else if (i == 73) { D = new Rectangle(this.Facility73Client.X + this.DisplayOffset.X, this.Facility73Client.Y + this.DisplayOffset.Y, this.Facility73Client.Width, this.Facility73Client.Height); }
            else if (i == 74) { D = new Rectangle(this.Facility74Client.X + this.DisplayOffset.X, this.Facility74Client.Y + this.DisplayOffset.Y, this.Facility74Client.Width, this.Facility74Client.Height); }
            else if (i == 75) { D = new Rectangle(this.Facility75Client.X + this.DisplayOffset.X, this.Facility75Client.Y + this.DisplayOffset.Y, this.Facility75Client.Width, this.Facility75Client.Height); }
            else if (i == 76) { D = new Rectangle(this.Facility76Client.X + this.DisplayOffset.X, this.Facility76Client.Y + this.DisplayOffset.Y, this.Facility76Client.Width, this.Facility76Client.Height); }
            else if (i == 77) { D = new Rectangle(this.Facility77Client.X + this.DisplayOffset.X, this.Facility77Client.Y + this.DisplayOffset.Y, this.Facility77Client.Width, this.Facility77Client.Height); }
            else if (i == 78) { D = new Rectangle(this.Facility78Client.X + this.DisplayOffset.X, this.Facility78Client.Y + this.DisplayOffset.Y, this.Facility78Client.Width, this.Facility78Client.Height); }
            else if (i == 79) { D = new Rectangle(this.Facility79Client.X + this.DisplayOffset.X, this.Facility79Client.Y + this.DisplayOffset.Y, this.Facility79Client.Width, this.Facility79Client.Height); }
            else if (i == 80) { D = new Rectangle(this.Facility80Client.X + this.DisplayOffset.X, this.Facility80Client.Y + this.DisplayOffset.Y, this.Facility80Client.Width, this.Facility80Client.Height); }
            else if (i == 81) { D = new Rectangle(this.Facility81Client.X + this.DisplayOffset.X, this.Facility81Client.Y + this.DisplayOffset.Y, this.Facility81Client.Width, this.Facility81Client.Height); }
            else if (i == 82) { D = new Rectangle(this.Facility82Client.X + this.DisplayOffset.X, this.Facility82Client.Y + this.DisplayOffset.Y, this.Facility82Client.Width, this.Facility82Client.Height); }
            else if (i == 83) { D = new Rectangle(this.Facility83Client.X + this.DisplayOffset.X, this.Facility83Client.Y + this.DisplayOffset.Y, this.Facility83Client.Width, this.Facility83Client.Height); }
            else if (i == 84) { D = new Rectangle(this.Facility84Client.X + this.DisplayOffset.X, this.Facility84Client.Y + this.DisplayOffset.Y, this.Facility84Client.Width, this.Facility84Client.Height); }
            else if (i == 85) { D = new Rectangle(this.Facility85Client.X + this.DisplayOffset.X, this.Facility85Client.Y + this.DisplayOffset.Y, this.Facility85Client.Width, this.Facility85Client.Height); }
            else if (i == 86) { D = new Rectangle(this.Facility86Client.X + this.DisplayOffset.X, this.Facility86Client.Y + this.DisplayOffset.Y, this.Facility86Client.Width, this.Facility86Client.Height); }
            else if (i == 87) { D = new Rectangle(this.Facility87Client.X + this.DisplayOffset.X, this.Facility87Client.Y + this.DisplayOffset.Y, this.Facility87Client.Width, this.Facility87Client.Height); }
            else if (i == 88) { D = new Rectangle(this.Facility88Client.X + this.DisplayOffset.X, this.Facility88Client.Y + this.DisplayOffset.Y, this.Facility88Client.Width, this.Facility88Client.Height); }
            else if (i == 89) { D = new Rectangle(this.Facility89Client.X + this.DisplayOffset.X, this.Facility89Client.Y + this.DisplayOffset.Y, this.Facility89Client.Width, this.Facility89Client.Height); }
            else if (i == 90) { D = new Rectangle(this.Facility90Client.X + this.DisplayOffset.X, this.Facility90Client.Y + this.DisplayOffset.Y, this.Facility90Client.Width, this.Facility90Client.Height); }
            else if (i == 91) { D = new Rectangle(this.Facility91Client.X + this.DisplayOffset.X, this.Facility91Client.Y + this.DisplayOffset.Y, this.Facility91Client.Width, this.Facility91Client.Height); }
            else if (i == 92) { D = new Rectangle(this.Facility92Client.X + this.DisplayOffset.X, this.Facility92Client.Y + this.DisplayOffset.Y, this.Facility92Client.Width, this.Facility92Client.Height); }
            else if (i == 93) { D = new Rectangle(this.Facility93Client.X + this.DisplayOffset.X, this.Facility93Client.Y + this.DisplayOffset.Y, this.Facility93Client.Width, this.Facility93Client.Height); }
            else if (i == 94) { D = new Rectangle(this.Facility94Client.X + this.DisplayOffset.X, this.Facility94Client.Y + this.DisplayOffset.Y, this.Facility94Client.Width, this.Facility94Client.Height); }
            else if (i == 95) { D = new Rectangle(this.Facility95Client.X + this.DisplayOffset.X, this.Facility95Client.Y + this.DisplayOffset.Y, this.Facility95Client.Width, this.Facility95Client.Height); }
            else if (i == 96) { D = new Rectangle(this.Facility96Client.X + this.DisplayOffset.X, this.Facility96Client.Y + this.DisplayOffset.Y, this.Facility96Client.Width, this.Facility96Client.Height); }
            else if (i == 97) { D = new Rectangle(this.Facility97Client.X + this.DisplayOffset.X, this.Facility97Client.Y + this.DisplayOffset.Y, this.Facility97Client.Width, this.Facility97Client.Height); }
            else if (i == 98) { D = new Rectangle(this.Facility98Client.X + this.DisplayOffset.X, this.Facility98Client.Y + this.DisplayOffset.Y, this.Facility98Client.Width, this.Facility98Client.Height); }
            else if (i == 99) { D = new Rectangle(this.Facility99Client.X + this.DisplayOffset.X, this.Facility99Client.Y + this.DisplayOffset.Y, this.Facility99Client.Width, this.Facility99Client.Height); }
            else if (i == 100) { D = new Rectangle(this.Facility100Client.X + this.DisplayOffset.X, this.Facility100Client.Y + this.DisplayOffset.Y, this.Facility100Client.Width, this.Facility100Client.Height); }
            else if (i == 101) { D = new Rectangle(this.Facility101Client.X + this.DisplayOffset.X, this.Facility101Client.Y + this.DisplayOffset.Y, this.Facility101Client.Width, this.Facility101Client.Height); }
            else if (i == 102) { D = new Rectangle(this.Facility102Client.X + this.DisplayOffset.X, this.Facility102Client.Y + this.DisplayOffset.Y, this.Facility102Client.Width, this.Facility102Client.Height); }
            else if (i == 103) { D = new Rectangle(this.Facility103Client.X + this.DisplayOffset.X, this.Facility103Client.Y + this.DisplayOffset.Y, this.Facility103Client.Width, this.Facility103Client.Height); }
            else if (i == 104) { D = new Rectangle(this.Facility104Client.X + this.DisplayOffset.X, this.Facility104Client.Y + this.DisplayOffset.Y, this.Facility104Client.Width, this.Facility104Client.Height); }
            else if (i == 105) { D = new Rectangle(this.Facility105Client.X + this.DisplayOffset.X, this.Facility105Client.Y + this.DisplayOffset.Y, this.Facility105Client.Width, this.Facility105Client.Height); }
            else if (i == 106) { D = new Rectangle(this.Facility106Client.X + this.DisplayOffset.X, this.Facility106Client.Y + this.DisplayOffset.Y, this.Facility106Client.Width, this.Facility106Client.Height); }
            else if (i == 107) { D = new Rectangle(this.Facility107Client.X + this.DisplayOffset.X, this.Facility107Client.Y + this.DisplayOffset.Y, this.Facility107Client.Width, this.Facility107Client.Height); }
            else if (i == 108) { D = new Rectangle(this.Facility108Client.X + this.DisplayOffset.X, this.Facility108Client.Y + this.DisplayOffset.Y, this.Facility108Client.Width, this.Facility108Client.Height); }
            else if (i == 109) { D = new Rectangle(this.Facility109Client.X + this.DisplayOffset.X, this.Facility109Client.Y + this.DisplayOffset.Y, this.Facility109Client.Width, this.Facility109Client.Height); }
            else if (i == 110) { D = new Rectangle(this.Facility110Client.X + this.DisplayOffset.X, this.Facility110Client.Y + this.DisplayOffset.Y, this.Facility110Client.Width, this.Facility110Client.Height); }
            else if (i == 111) { D = new Rectangle(this.Facility111Client.X + this.DisplayOffset.X, this.Facility111Client.Y + this.DisplayOffset.Y, this.Facility111Client.Width, this.Facility111Client.Height); }
            else if (i == 112) { D = new Rectangle(this.Facility112Client.X + this.DisplayOffset.X, this.Facility112Client.Y + this.DisplayOffset.Y, this.Facility112Client.Width, this.Facility112Client.Height); }
            else if (i == 113) { D = new Rectangle(this.Facility113Client.X + this.DisplayOffset.X, this.Facility113Client.Y + this.DisplayOffset.Y, this.Facility113Client.Width, this.Facility113Client.Height); }
            else if (i == 114) { D = new Rectangle(this.Facility114Client.X + this.DisplayOffset.X, this.Facility114Client.Y + this.DisplayOffset.Y, this.Facility114Client.Width, this.Facility114Client.Height); }
            else if (i == 115) { D = new Rectangle(this.Facility115Client.X + this.DisplayOffset.X, this.Facility115Client.Y + this.DisplayOffset.Y, this.Facility115Client.Width, this.Facility115Client.Height); }
            else if (i == 116) { D = new Rectangle(this.Facility116Client.X + this.DisplayOffset.X, this.Facility116Client.Y + this.DisplayOffset.Y, this.Facility116Client.Width, this.Facility116Client.Height); }
            else if (i == 117) { D = new Rectangle(this.Facility117Client.X + this.DisplayOffset.X, this.Facility117Client.Y + this.DisplayOffset.Y, this.Facility117Client.Width, this.Facility117Client.Height); }
            else if (i == 118) { D = new Rectangle(this.Facility118Client.X + this.DisplayOffset.X, this.Facility118Client.Y + this.DisplayOffset.Y, this.Facility118Client.Width, this.Facility118Client.Height); }
            else if (i == 119) { D = new Rectangle(this.Facility119Client.X + this.DisplayOffset.X, this.Facility119Client.Y + this.DisplayOffset.Y, this.Facility119Client.Width, this.Facility119Client.Height); }
            else if (i == 120) { D = new Rectangle(this.Facility120Client.X + this.DisplayOffset.X, this.Facility120Client.Y + this.DisplayOffset.Y, this.Facility120Client.Width, this.Facility120Client.Height); }
            else if (i == 121) { D = new Rectangle(this.Facility121Client.X + this.DisplayOffset.X, this.Facility121Client.Y + this.DisplayOffset.Y, this.Facility121Client.Width, this.Facility121Client.Height); }
            else if (i == 122) { D = new Rectangle(this.Facility122Client.X + this.DisplayOffset.X, this.Facility122Client.Y + this.DisplayOffset.Y, this.Facility122Client.Width, this.Facility122Client.Height); }
            else if (i == 123) { D = new Rectangle(this.Facility123Client.X + this.DisplayOffset.X, this.Facility123Client.Y + this.DisplayOffset.Y, this.Facility123Client.Width, this.Facility123Client.Height); }
            else if (i == 124) { D = new Rectangle(this.Facility124Client.X + this.DisplayOffset.X, this.Facility124Client.Y + this.DisplayOffset.Y, this.Facility124Client.Width, this.Facility124Client.Height); }
            else if (i == 125) { D = new Rectangle(this.Facility125Client.X + this.DisplayOffset.X, this.Facility125Client.Y + this.DisplayOffset.Y, this.Facility125Client.Width, this.Facility125Client.Height); }
            else if (i == 126) { D = new Rectangle(this.Facility126Client.X + this.DisplayOffset.X, this.Facility126Client.Y + this.DisplayOffset.Y, this.Facility126Client.Width, this.Facility126Client.Height); }
            else if (i == 127) { D = new Rectangle(this.Facility127Client.X + this.DisplayOffset.X, this.Facility127Client.Y + this.DisplayOffset.Y, this.Facility127Client.Width, this.Facility127Client.Height); }
            else if (i == 128) { D = new Rectangle(this.Facility128Client.X + this.DisplayOffset.X, this.Facility128Client.Y + this.DisplayOffset.Y, this.Facility128Client.Width, this.Facility128Client.Height); }
            else if (i == 129) { D = new Rectangle(this.Facility129Client.X + this.DisplayOffset.X, this.Facility129Client.Y + this.DisplayOffset.Y, this.Facility129Client.Width, this.Facility129Client.Height); }
            else if (i == 130) { D = new Rectangle(this.Facility130Client.X + this.DisplayOffset.X, this.Facility130Client.Y + this.DisplayOffset.Y, this.Facility130Client.Width, this.Facility130Client.Height); }
            else if (i == 131) { D = new Rectangle(this.Facility131Client.X + this.DisplayOffset.X, this.Facility131Client.Y + this.DisplayOffset.Y, this.Facility131Client.Width, this.Facility131Client.Height); }
            else if (i == 132) { D = new Rectangle(this.Facility132Client.X + this.DisplayOffset.X, this.Facility132Client.Y + this.DisplayOffset.Y, this.Facility132Client.Width, this.Facility132Client.Height); }
            else if (i == 133) { D = new Rectangle(this.Facility133Client.X + this.DisplayOffset.X, this.Facility133Client.Y + this.DisplayOffset.Y, this.Facility133Client.Width, this.Facility133Client.Height); }
            else if (i == 134) { D = new Rectangle(this.Facility134Client.X + this.DisplayOffset.X, this.Facility134Client.Y + this.DisplayOffset.Y, this.Facility134Client.Width, this.Facility134Client.Height); }
            else if (i == 135) { D = new Rectangle(this.Facility135Client.X + this.DisplayOffset.X, this.Facility135Client.Y + this.DisplayOffset.Y, this.Facility135Client.Width, this.Facility135Client.Height); }
            else if (i == 136) { D = new Rectangle(this.Facility136Client.X + this.DisplayOffset.X, this.Facility136Client.Y + this.DisplayOffset.Y, this.Facility136Client.Width, this.Facility136Client.Height); }
            else if (i == 137) { D = new Rectangle(this.Facility137Client.X + this.DisplayOffset.X, this.Facility137Client.Y + this.DisplayOffset.Y, this.Facility137Client.Width, this.Facility137Client.Height); }
            else if (i == 138) { D = new Rectangle(this.Facility138Client.X + this.DisplayOffset.X, this.Facility138Client.Y + this.DisplayOffset.Y, this.Facility138Client.Width, this.Facility138Client.Height); }
            else if (i == 139) { D = new Rectangle(this.Facility139Client.X + this.DisplayOffset.X, this.Facility139Client.Y + this.DisplayOffset.Y, this.Facility139Client.Width, this.Facility139Client.Height); }
            else if (i == 140) { D = new Rectangle(this.Facility140Client.X + this.DisplayOffset.X, this.Facility140Client.Y + this.DisplayOffset.Y, this.Facility140Client.Width, this.Facility140Client.Height); }
            else if (i == 141) { D = new Rectangle(this.Facility141Client.X + this.DisplayOffset.X, this.Facility141Client.Y + this.DisplayOffset.Y, this.Facility141Client.Width, this.Facility141Client.Height); }
            else if (i == 142) { D = new Rectangle(this.Facility142Client.X + this.DisplayOffset.X, this.Facility142Client.Y + this.DisplayOffset.Y, this.Facility142Client.Width, this.Facility142Client.Height); }
            else if (i == 143) { D = new Rectangle(this.Facility143Client.X + this.DisplayOffset.X, this.Facility143Client.Y + this.DisplayOffset.Y, this.Facility143Client.Width, this.Facility143Client.Height); }
            else if (i == 144) { D = new Rectangle(this.Facility144Client.X + this.DisplayOffset.X, this.Facility144Client.Y + this.DisplayOffset.Y, this.Facility144Client.Width, this.Facility144Client.Height); }
            else if (i == 145) { D = new Rectangle(this.Facility145Client.X + this.DisplayOffset.X, this.Facility145Client.Y + this.DisplayOffset.Y, this.Facility145Client.Width, this.Facility145Client.Height); }
            else if (i == 146) { D = new Rectangle(this.Facility146Client.X + this.DisplayOffset.X, this.Facility146Client.Y + this.DisplayOffset.Y, this.Facility146Client.Width, this.Facility146Client.Height); }
            else if (i == 147) { D = new Rectangle(this.Facility147Client.X + this.DisplayOffset.X, this.Facility147Client.Y + this.DisplayOffset.Y, this.Facility147Client.Width, this.Facility147Client.Height); }
            else if (i == 148) { D = new Rectangle(this.Facility148Client.X + this.DisplayOffset.X, this.Facility148Client.Y + this.DisplayOffset.Y, this.Facility148Client.Width, this.Facility148Client.Height); }
            else if (i == 149) { D = new Rectangle(this.Facility149Client.X + this.DisplayOffset.X, this.Facility149Client.Y + this.DisplayOffset.Y, this.Facility149Client.Width, this.Facility149Client.Height); }
            else if (i == 150) { D = new Rectangle(this.Facility150Client.X + this.DisplayOffset.X, this.Facility150Client.Y + this.DisplayOffset.Y, this.Facility150Client.Width, this.Facility150Client.Height); }
            else if (i == 151) { D = new Rectangle(this.Facility151Client.X + this.DisplayOffset.X, this.Facility151Client.Y + this.DisplayOffset.Y, this.Facility151Client.Width, this.Facility151Client.Height); }
            else if (i == 152) { D = new Rectangle(this.Facility152Client.X + this.DisplayOffset.X, this.Facility152Client.Y + this.DisplayOffset.Y, this.Facility152Client.Width, this.Facility152Client.Height); }
            else if (i == 153) { D = new Rectangle(this.Facility153Client.X + this.DisplayOffset.X, this.Facility153Client.Y + this.DisplayOffset.Y, this.Facility153Client.Width, this.Facility153Client.Height); }
            else if (i == 154) { D = new Rectangle(this.Facility154Client.X + this.DisplayOffset.X, this.Facility154Client.Y + this.DisplayOffset.Y, this.Facility154Client.Width, this.Facility154Client.Height); }
            else if (i == 155) { D = new Rectangle(this.Facility155Client.X + this.DisplayOffset.X, this.Facility155Client.Y + this.DisplayOffset.Y, this.Facility155Client.Width, this.Facility155Client.Height); }
            else if (i == 156) { D = new Rectangle(this.Facility156Client.X + this.DisplayOffset.X, this.Facility156Client.Y + this.DisplayOffset.Y, this.Facility156Client.Width, this.Facility156Client.Height); }
            else if (i == 157) { D = new Rectangle(this.Facility157Client.X + this.DisplayOffset.X, this.Facility157Client.Y + this.DisplayOffset.Y, this.Facility157Client.Width, this.Facility157Client.Height); }
            else if (i == 158) { D = new Rectangle(this.Facility158Client.X + this.DisplayOffset.X, this.Facility158Client.Y + this.DisplayOffset.Y, this.Facility158Client.Width, this.Facility158Client.Height); }
            else if (i == 159) { D = new Rectangle(this.Facility159Client.X + this.DisplayOffset.X, this.Facility159Client.Y + this.DisplayOffset.Y, this.Facility159Client.Width, this.Facility159Client.Height); }
            else if (i == 160) { D = new Rectangle(this.Facility160Client.X + this.DisplayOffset.X, this.Facility160Client.Y + this.DisplayOffset.Y, this.Facility160Client.Width, this.Facility160Client.Height); }
            else if (i == 161) { D = new Rectangle(this.Facility161Client.X + this.DisplayOffset.X, this.Facility161Client.Y + this.DisplayOffset.Y, this.Facility161Client.Width, this.Facility161Client.Height); }
            else if (i == 162) { D = new Rectangle(this.Facility162Client.X + this.DisplayOffset.X, this.Facility162Client.Y + this.DisplayOffset.Y, this.Facility162Client.Width, this.Facility162Client.Height); }
            else if (i == 163) { D = new Rectangle(this.Facility163Client.X + this.DisplayOffset.X, this.Facility163Client.Y + this.DisplayOffset.Y, this.Facility163Client.Width, this.Facility163Client.Height); }
            else if (i == 164) { D = new Rectangle(this.Facility164Client.X + this.DisplayOffset.X, this.Facility164Client.Y + this.DisplayOffset.Y, this.Facility164Client.Width, this.Facility164Client.Height); }
            else if (i == 165) { D = new Rectangle(this.Facility165Client.X + this.DisplayOffset.X, this.Facility165Client.Y + this.DisplayOffset.Y, this.Facility165Client.Width, this.Facility165Client.Height); }
            else if (i == 166) { D = new Rectangle(this.Facility166Client.X + this.DisplayOffset.X, this.Facility166Client.Y + this.DisplayOffset.Y, this.Facility166Client.Width, this.Facility166Client.Height); }
            else if (i == 167) { D = new Rectangle(this.Facility167Client.X + this.DisplayOffset.X, this.Facility167Client.Y + this.DisplayOffset.Y, this.Facility167Client.Width, this.Facility167Client.Height); }
            else if (i == 168) { D = new Rectangle(this.Facility168Client.X + this.DisplayOffset.X, this.Facility168Client.Y + this.DisplayOffset.Y, this.Facility168Client.Width, this.Facility168Client.Height); }
            else if (i == 169) { D = new Rectangle(this.Facility169Client.X + this.DisplayOffset.X, this.Facility169Client.Y + this.DisplayOffset.Y, this.Facility169Client.Width, this.Facility169Client.Height); }
            else if (i == 170) { D = new Rectangle(this.Facility170Client.X + this.DisplayOffset.X, this.Facility170Client.Y + this.DisplayOffset.Y, this.Facility170Client.Width, this.Facility170Client.Height); }
            else if (i == 171) { D = new Rectangle(this.Facility171Client.X + this.DisplayOffset.X, this.Facility171Client.Y + this.DisplayOffset.Y, this.Facility171Client.Width, this.Facility171Client.Height); }
            else if (i == 172) { D = new Rectangle(this.Facility172Client.X + this.DisplayOffset.X, this.Facility172Client.Y + this.DisplayOffset.Y, this.Facility172Client.Width, this.Facility172Client.Height); }
            else if (i == 173) { D = new Rectangle(this.Facility173Client.X + this.DisplayOffset.X, this.Facility173Client.Y + this.DisplayOffset.Y, this.Facility173Client.Width, this.Facility173Client.Height); }
            else if (i == 174) { D = new Rectangle(this.Facility174Client.X + this.DisplayOffset.X, this.Facility174Client.Y + this.DisplayOffset.Y, this.Facility174Client.Width, this.Facility174Client.Height); }
            else if (i == 175) { D = new Rectangle(this.Facility175Client.X + this.DisplayOffset.X, this.Facility175Client.Y + this.DisplayOffset.Y, this.Facility175Client.Width, this.Facility175Client.Height); }
            else if (i == 176) { D = new Rectangle(this.Facility176Client.X + this.DisplayOffset.X, this.Facility176Client.Y + this.DisplayOffset.Y, this.Facility176Client.Width, this.Facility176Client.Height); }
            else if (i == 177) { D = new Rectangle(this.Facility177Client.X + this.DisplayOffset.X, this.Facility177Client.Y + this.DisplayOffset.Y, this.Facility177Client.Width, this.Facility177Client.Height); }
            else if (i == 178) { D = new Rectangle(this.Facility178Client.X + this.DisplayOffset.X, this.Facility178Client.Y + this.DisplayOffset.Y, this.Facility178Client.Width, this.Facility178Client.Height); }
            else if (i == 179) { D = new Rectangle(this.Facility179Client.X + this.DisplayOffset.X, this.Facility179Client.Y + this.DisplayOffset.Y, this.Facility179Client.Width, this.Facility179Client.Height); }
            else if (i == 180) { D = new Rectangle(this.Facility180Client.X + this.DisplayOffset.X, this.Facility180Client.Y + this.DisplayOffset.Y, this.Facility180Client.Width, this.Facility180Client.Height); }
            else if (i == 181) { D = new Rectangle(this.Facility181Client.X + this.DisplayOffset.X, this.Facility181Client.Y + this.DisplayOffset.Y, this.Facility181Client.Width, this.Facility181Client.Height); }
            else if (i == 182) { D = new Rectangle(this.Facility182Client.X + this.DisplayOffset.X, this.Facility182Client.Y + this.DisplayOffset.Y, this.Facility182Client.Width, this.Facility182Client.Height); }
            else if (i == 183) { D = new Rectangle(this.Facility183Client.X + this.DisplayOffset.X, this.Facility183Client.Y + this.DisplayOffset.Y, this.Facility183Client.Width, this.Facility183Client.Height); }
            else if (i == 184) { D = new Rectangle(this.Facility184Client.X + this.DisplayOffset.X, this.Facility184Client.Y + this.DisplayOffset.Y, this.Facility184Client.Width, this.Facility184Client.Height); }
            else if (i == 185) { D = new Rectangle(this.Facility185Client.X + this.DisplayOffset.X, this.Facility185Client.Y + this.DisplayOffset.Y, this.Facility185Client.Width, this.Facility185Client.Height); }
            else if (i == 186) { D = new Rectangle(this.Facility186Client.X + this.DisplayOffset.X, this.Facility186Client.Y + this.DisplayOffset.Y, this.Facility186Client.Width, this.Facility186Client.Height); }
            else if (i == 187) { D = new Rectangle(this.Facility187Client.X + this.DisplayOffset.X, this.Facility187Client.Y + this.DisplayOffset.Y, this.Facility187Client.Width, this.Facility187Client.Height); }
            else if (i == 188) { D = new Rectangle(this.Facility188Client.X + this.DisplayOffset.X, this.Facility188Client.Y + this.DisplayOffset.Y, this.Facility188Client.Width, this.Facility188Client.Height); }
            else if (i == 189) { D = new Rectangle(this.Facility189Client.X + this.DisplayOffset.X, this.Facility189Client.Y + this.DisplayOffset.Y, this.Facility189Client.Width, this.Facility189Client.Height); }
            else if (i == 190) { D = new Rectangle(this.Facility190Client.X + this.DisplayOffset.X, this.Facility190Client.Y + this.DisplayOffset.Y, this.Facility190Client.Width, this.Facility190Client.Height); }
            else if (i == 191) { D = new Rectangle(this.Facility191Client.X + this.DisplayOffset.X, this.Facility191Client.Y + this.DisplayOffset.Y, this.Facility191Client.Width, this.Facility191Client.Height); }
            else if (i == 192) { D = new Rectangle(this.Facility192Client.X + this.DisplayOffset.X, this.Facility192Client.Y + this.DisplayOffset.Y, this.Facility192Client.Width, this.Facility192Client.Height); }
            else if (i == 193) { D = new Rectangle(this.Facility193Client.X + this.DisplayOffset.X, this.Facility193Client.Y + this.DisplayOffset.Y, this.Facility193Client.Width, this.Facility193Client.Height); }
            else if (i == 194) { D = new Rectangle(this.Facility194Client.X + this.DisplayOffset.X, this.Facility194Client.Y + this.DisplayOffset.Y, this.Facility194Client.Width, this.Facility194Client.Height); }
            else if (i == 195) { D = new Rectangle(this.Facility195Client.X + this.DisplayOffset.X, this.Facility195Client.Y + this.DisplayOffset.Y, this.Facility195Client.Width, this.Facility195Client.Height); }
            else if (i == 196) { D = new Rectangle(this.Facility196Client.X + this.DisplayOffset.X, this.Facility196Client.Y + this.DisplayOffset.Y, this.Facility196Client.Width, this.Facility196Client.Height); }
            else if (i == 197) { D = new Rectangle(this.Facility197Client.X + this.DisplayOffset.X, this.Facility197Client.Y + this.DisplayOffset.Y, this.Facility197Client.Width, this.Facility197Client.Height); }
            else if (i == 198) { D = new Rectangle(this.Facility198Client.X + this.DisplayOffset.X, this.Facility198Client.Y + this.DisplayOffset.Y, this.Facility198Client.Width, this.Facility198Client.Height); }
            else if (i == 199) { D = new Rectangle(this.Facility199Client.X + this.DisplayOffset.X, this.Facility199Client.Y + this.DisplayOffset.Y, this.Facility199Client.Width, this.Facility199Client.Height); }
            else if (i == 200) { D = new Rectangle(this.Facility200Client.X + this.DisplayOffset.X, this.Facility200Client.Y + this.DisplayOffset.Y, this.Facility200Client.Width, this.Facility200Client.Height); }           
            return D;
        }
        private Rectangle TheFacilityAShowDisplayPosition(int i)
        {
            Rectangle D = this.NullDisplayPosition;
            if (ShowTheFacilityKind(i) == true)
            {
                D=TheFacilityDisplayPosition(i);
            }
            return D;    
        }
        private Rectangle TheFacilityBShowDisplayPosition(int i)
        {
            Rectangle D = this.NullDisplayPosition;
            if (ShowTheFacilityKind(i) == true && TheFacilityIDForDescription == TheFacilityKind(i))
            {
                D = TheFacilityDisplayPosition(i);
            }
            return D;
        }
        //
        private PlatformTexture TheFacilityTextPicture(int i)
        {
            PlatformTexture P=PictureNull;
            if (i == 1) { P = Facility1TextPicture; }
            else if (i == 2) { P = Facility2TextPicture; }
            else if (i == 2) { P = Facility2TextPicture; }
            else if (i == 3) { P = Facility3TextPicture; }
            else if (i == 4) { P = Facility4TextPicture; }
            else if (i == 5) { P = Facility5TextPicture; }
            else if (i == 6) { P = Facility6TextPicture; }
            else if (i == 7) { P = Facility7TextPicture; }
            else if (i == 8) { P = Facility8TextPicture; }
            else if (i == 9) { P = Facility9TextPicture; }
            else if (i == 10) { P = Facility10TextPicture; }
            else if (i == 11) { P = Facility11TextPicture; }
            else if (i == 12) { P = Facility12TextPicture; }
            else if (i == 13) { P = Facility13TextPicture; }
            else if (i == 14) { P = Facility14TextPicture; }
            else if (i == 15) { P = Facility15TextPicture; }
            else if (i == 16) { P = Facility16TextPicture; }
            else if (i == 17) { P = Facility17TextPicture; }
            else if (i == 18) { P = Facility18TextPicture; }
            else if (i == 19) { P = Facility19TextPicture; }
            else if (i == 20) { P = Facility20TextPicture; }
            else if (i == 21) { P = Facility21TextPicture; }
            else if (i == 22) { P = Facility22TextPicture; }
            else if (i == 23) { P = Facility23TextPicture; }
            else if (i == 24) { P = Facility24TextPicture; }
            else if (i == 25) { P = Facility25TextPicture; }
            else if (i == 26) { P = Facility26TextPicture; }
            else if (i == 27) { P = Facility27TextPicture; }
            else if (i == 28) { P = Facility28TextPicture; }
            else if (i == 29) { P = Facility29TextPicture; }
            else if (i == 30) { P = Facility30TextPicture; }
            else if (i == 31) { P = Facility31TextPicture; }
            else if (i == 32) { P = Facility32TextPicture; }
            else if (i == 33) { P = Facility33TextPicture; }
            else if (i == 34) { P = Facility34TextPicture; }
            else if (i == 35) { P = Facility35TextPicture; }
            else if (i == 36) { P = Facility36TextPicture; }
            else if (i == 37) { P = Facility37TextPicture; }
            else if (i == 38) { P = Facility38TextPicture; }
            else if (i == 39) { P = Facility39TextPicture; }
            else if (i == 40) { P = Facility40TextPicture; }
            else if (i == 41) { P = Facility41TextPicture; }
            else if (i == 42) { P = Facility42TextPicture; }
            else if (i == 43) { P = Facility43TextPicture; }
            else if (i == 44) { P = Facility44TextPicture; }
            else if (i == 45) { P = Facility45TextPicture; }
            else if (i == 46) { P = Facility46TextPicture; }
            else if (i == 47) { P = Facility47TextPicture; }
            else if (i == 48) { P = Facility48TextPicture; }
            else if (i == 49) { P = Facility49TextPicture; }
            else if (i == 50) { P = Facility50TextPicture; }
            else if (i == 51) { P = Facility51TextPicture; }
            else if (i == 52) { P = Facility52TextPicture; }
            else if (i == 53) { P = Facility53TextPicture; }
            else if (i == 54) { P = Facility54TextPicture; }
            else if (i == 55) { P = Facility55TextPicture; }
            else if (i == 56) { P = Facility56TextPicture; }
            else if (i == 57) { P = Facility57TextPicture; }
            else if (i == 58) { P = Facility58TextPicture; }
            else if (i == 59) { P = Facility59TextPicture; }
            else if (i == 60) { P = Facility60TextPicture; }
            else if (i == 61) { P = Facility61TextPicture; }
            else if (i == 62) { P = Facility62TextPicture; }
            else if (i == 63) { P = Facility63TextPicture; }
            else if (i == 64) { P = Facility64TextPicture; }
            else if (i == 65) { P = Facility65TextPicture; }
            else if (i == 66) { P = Facility66TextPicture; }
            else if (i == 67) { P = Facility67TextPicture; }
            else if (i == 68) { P = Facility68TextPicture; }
            else if (i == 69) { P = Facility69TextPicture; }
            else if (i == 70) { P = Facility70TextPicture; }
            else if (i == 71) { P = Facility71TextPicture; }
            else if (i == 72) { P = Facility72TextPicture; }
            else if (i == 73) { P = Facility73TextPicture; }
            else if (i == 74) { P = Facility74TextPicture; }
            else if (i == 75) { P = Facility75TextPicture; }
            else if (i == 76) { P = Facility76TextPicture; }
            else if (i == 77) { P = Facility77TextPicture; }
            else if (i == 78) { P = Facility78TextPicture; }
            else if (i == 79) { P = Facility79TextPicture; }
            else if (i == 80) { P = Facility80TextPicture; }
            else if (i == 81) { P = Facility81TextPicture; }
            else if (i == 82) { P = Facility82TextPicture; }
            else if (i == 83) { P = Facility83TextPicture; }
            else if (i == 84) { P = Facility84TextPicture; }
            else if (i == 85) { P = Facility85TextPicture; }
            else if (i == 86) { P = Facility86TextPicture; }
            else if (i == 87) { P = Facility87TextPicture; }
            else if (i == 88) { P = Facility88TextPicture; }
            else if (i == 89) { P = Facility89TextPicture; }
            else if (i == 90) { P = Facility90TextPicture; }
            else if (i == 91) { P = Facility91TextPicture; }
            else if (i == 92) { P = Facility92TextPicture; }
            else if (i == 93) { P = Facility93TextPicture; }
            else if (i == 94) { P = Facility94TextPicture; }
            else if (i == 95) { P = Facility95TextPicture; }
            else if (i == 96) { P = Facility96TextPicture; }
            else if (i == 97) { P = Facility97TextPicture; }
            else if (i == 98) { P = Facility98TextPicture; }
            else if (i == 99) { P = Facility99TextPicture; }
            else if (i == 100) { P = Facility100TextPicture; }
            else if (i == 101) { P = Facility101TextPicture; }
            else if (i == 102) { P = Facility102TextPicture; }
            else if (i == 103) { P = Facility103TextPicture; }
            else if (i == 104) { P = Facility104TextPicture; }
            else if (i == 105) { P = Facility105TextPicture; }
            else if (i == 106) { P = Facility106TextPicture; }
            else if (i == 107) { P = Facility107TextPicture; }
            else if (i == 108) { P = Facility108TextPicture; }
            else if (i == 109) { P = Facility109TextPicture; }
            else if (i == 110) { P = Facility110TextPicture; }
            else if (i == 111) { P = Facility111TextPicture; }
            else if (i == 112) { P = Facility112TextPicture; }
            else if (i == 113) { P = Facility113TextPicture; }
            else if (i == 114) { P = Facility114TextPicture; }
            else if (i == 115) { P = Facility115TextPicture; }
            else if (i == 116) { P = Facility116TextPicture; }
            else if (i == 117) { P = Facility117TextPicture; }
            else if (i == 118) { P = Facility118TextPicture; }
            else if (i == 119) { P = Facility119TextPicture; }
            else if (i == 120) { P = Facility120TextPicture; }
            else if (i == 121) { P = Facility121TextPicture; }
            else if (i == 122) { P = Facility122TextPicture; }
            else if (i == 123) { P = Facility123TextPicture; }
            else if (i == 124) { P = Facility124TextPicture; }
            else if (i == 125) { P = Facility125TextPicture; }
            else if (i == 126) { P = Facility126TextPicture; }
            else if (i == 127) { P = Facility127TextPicture; }
            else if (i == 128) { P = Facility128TextPicture; }
            else if (i == 129) { P = Facility129TextPicture; }
            else if (i == 130) { P = Facility130TextPicture; }
            else if (i == 131) { P = Facility131TextPicture; }
            else if (i == 132) { P = Facility132TextPicture; }
            else if (i == 133) { P = Facility133TextPicture; }
            else if (i == 134) { P = Facility134TextPicture; }
            else if (i == 135) { P = Facility135TextPicture; }
            else if (i == 136) { P = Facility136TextPicture; }
            else if (i == 137) { P = Facility137TextPicture; }
            else if (i == 138) { P = Facility138TextPicture; }
            else if (i == 139) { P = Facility139TextPicture; }
            else if (i == 140) { P = Facility140TextPicture; }
            else if (i == 141) { P = Facility141TextPicture; }
            else if (i == 142) { P = Facility142TextPicture; }
            else if (i == 143) { P = Facility143TextPicture; }
            else if (i == 144) { P = Facility144TextPicture; }
            else if (i == 145) { P = Facility145TextPicture; }
            else if (i == 146) { P = Facility146TextPicture; }
            else if (i == 147) { P = Facility147TextPicture; }
            else if (i == 148) { P = Facility148TextPicture; }
            else if (i == 149) { P = Facility149TextPicture; }
            else if (i == 150) { P = Facility150TextPicture; }
            else if (i == 151) { P = Facility151TextPicture; }
            else if (i == 152) { P = Facility152TextPicture; }
            else if (i == 153) { P = Facility153TextPicture; }
            else if (i == 154) { P = Facility154TextPicture; }
            else if (i == 155) { P = Facility155TextPicture; }
            else if (i == 156) { P = Facility156TextPicture; }
            else if (i == 157) { P = Facility157TextPicture; }
            else if (i == 158) { P = Facility158TextPicture; }
            else if (i == 159) { P = Facility159TextPicture; }
            else if (i == 160) { P = Facility160TextPicture; }
            else if (i == 161) { P = Facility161TextPicture; }
            else if (i == 162) { P = Facility162TextPicture; }
            else if (i == 163) { P = Facility163TextPicture; }
            else if (i == 164) { P = Facility164TextPicture; }
            else if (i == 165) { P = Facility165TextPicture; }
            else if (i == 166) { P = Facility166TextPicture; }
            else if (i == 167) { P = Facility167TextPicture; }
            else if (i == 168) { P = Facility168TextPicture; }
            else if (i == 169) { P = Facility169TextPicture; }
            else if (i == 170) { P = Facility170TextPicture; }
            else if (i == 171) { P = Facility171TextPicture; }
            else if (i == 172) { P = Facility172TextPicture; }
            else if (i == 173) { P = Facility173TextPicture; }
            else if (i == 174) { P = Facility174TextPicture; }
            else if (i == 175) { P = Facility175TextPicture; }
            else if (i == 176) { P = Facility176TextPicture; }
            else if (i == 177) { P = Facility177TextPicture; }
            else if (i == 178) { P = Facility178TextPicture; }
            else if (i == 179) { P = Facility179TextPicture; }
            else if (i == 180) { P = Facility180TextPicture; }
            else if (i == 181) { P = Facility181TextPicture; }
            else if (i == 182) { P = Facility182TextPicture; }
            else if (i == 183) { P = Facility183TextPicture; }
            else if (i == 184) { P = Facility184TextPicture; }
            else if (i == 185) { P = Facility185TextPicture; }
            else if (i == 186) { P = Facility186TextPicture; }
            else if (i == 187) { P = Facility187TextPicture; }
            else if (i == 188) { P = Facility188TextPicture; }
            else if (i == 189) { P = Facility189TextPicture; }
            else if (i == 190) { P = Facility190TextPicture; }
            else if (i == 191) { P = Facility191TextPicture; }
            else if (i == 192) { P = Facility192TextPicture; }
            else if (i == 193) { P = Facility193TextPicture; }
            else if (i == 194) { P = Facility194TextPicture; }
            else if (i == 195) { P = Facility195TextPicture; }
            else if (i == 196) { P = Facility196TextPicture; }
            else if (i == 197) { P = Facility197TextPicture; }
            else if (i == 198) { P = Facility198TextPicture; }
            else if (i == 199) { P = Facility199TextPicture; }
            else if (i == 200) { P = Facility200TextPicture; }
            return P;
        }
        private Rectangle TheFacilityTextDisplayPosition(int i)
        {
            Rectangle D = this.NullDisplayPosition;
            if (i == 1) { D = new Rectangle(this.Facility1TextClient.X + this.DisplayOffset.X, this.Facility1TextClient.Y + this.DisplayOffset.Y, this.Facility1TextClient.Width, this.Facility1TextClient.Height); }
            else if (i == 2) { D = new Rectangle(this.Facility2TextClient.X + this.DisplayOffset.X, this.Facility2TextClient.Y + this.DisplayOffset.Y, this.Facility2TextClient.Width, this.Facility2TextClient.Height); }
            else if (i == 3) { D = new Rectangle(this.Facility3TextClient.X + this.DisplayOffset.X, this.Facility3TextClient.Y + this.DisplayOffset.Y, this.Facility3TextClient.Width, this.Facility3TextClient.Height); }
            else if (i == 4) { D = new Rectangle(this.Facility4TextClient.X + this.DisplayOffset.X, this.Facility4TextClient.Y + this.DisplayOffset.Y, this.Facility4TextClient.Width, this.Facility4TextClient.Height); }
            else if (i == 5) { D = new Rectangle(this.Facility5TextClient.X + this.DisplayOffset.X, this.Facility5TextClient.Y + this.DisplayOffset.Y, this.Facility5TextClient.Width, this.Facility5TextClient.Height); }
            else if (i == 6) { D = new Rectangle(this.Facility6TextClient.X + this.DisplayOffset.X, this.Facility6TextClient.Y + this.DisplayOffset.Y, this.Facility6TextClient.Width, this.Facility6TextClient.Height); }
            else if (i == 7) { D = new Rectangle(this.Facility7TextClient.X + this.DisplayOffset.X, this.Facility7TextClient.Y + this.DisplayOffset.Y, this.Facility7TextClient.Width, this.Facility7TextClient.Height); }
            else if (i == 8) { D = new Rectangle(this.Facility8TextClient.X + this.DisplayOffset.X, this.Facility8TextClient.Y + this.DisplayOffset.Y, this.Facility8TextClient.Width, this.Facility8TextClient.Height); }
            else if (i == 9) { D = new Rectangle(this.Facility9TextClient.X + this.DisplayOffset.X, this.Facility9TextClient.Y + this.DisplayOffset.Y, this.Facility9TextClient.Width, this.Facility9TextClient.Height); }
            else if (i == 10) { D = new Rectangle(this.Facility10TextClient.X + this.DisplayOffset.X, this.Facility10TextClient.Y + this.DisplayOffset.Y, this.Facility10TextClient.Width, this.Facility10TextClient.Height); }
            else if (i == 11) { D = new Rectangle(this.Facility11TextClient.X + this.DisplayOffset.X, this.Facility11TextClient.Y + this.DisplayOffset.Y, this.Facility11TextClient.Width, this.Facility11TextClient.Height); }
            else if (i == 12) { D = new Rectangle(this.Facility12TextClient.X + this.DisplayOffset.X, this.Facility12TextClient.Y + this.DisplayOffset.Y, this.Facility12TextClient.Width, this.Facility12TextClient.Height); }
            else if (i == 13) { D = new Rectangle(this.Facility13TextClient.X + this.DisplayOffset.X, this.Facility13TextClient.Y + this.DisplayOffset.Y, this.Facility13TextClient.Width, this.Facility13TextClient.Height); }
            else if (i == 14) { D = new Rectangle(this.Facility14TextClient.X + this.DisplayOffset.X, this.Facility14TextClient.Y + this.DisplayOffset.Y, this.Facility14TextClient.Width, this.Facility14TextClient.Height); }
            else if (i == 15) { D = new Rectangle(this.Facility15TextClient.X + this.DisplayOffset.X, this.Facility15TextClient.Y + this.DisplayOffset.Y, this.Facility15TextClient.Width, this.Facility15TextClient.Height); }
            else if (i == 16) { D = new Rectangle(this.Facility16TextClient.X + this.DisplayOffset.X, this.Facility16TextClient.Y + this.DisplayOffset.Y, this.Facility16TextClient.Width, this.Facility16TextClient.Height); }
            else if (i == 17) { D = new Rectangle(this.Facility17TextClient.X + this.DisplayOffset.X, this.Facility17TextClient.Y + this.DisplayOffset.Y, this.Facility17TextClient.Width, this.Facility17TextClient.Height); }
            else if (i == 18) { D = new Rectangle(this.Facility18TextClient.X + this.DisplayOffset.X, this.Facility18TextClient.Y + this.DisplayOffset.Y, this.Facility18TextClient.Width, this.Facility18TextClient.Height); }
            else if (i == 19) { D = new Rectangle(this.Facility19TextClient.X + this.DisplayOffset.X, this.Facility19TextClient.Y + this.DisplayOffset.Y, this.Facility19TextClient.Width, this.Facility19TextClient.Height); }
            else if (i == 20) { D = new Rectangle(this.Facility20TextClient.X + this.DisplayOffset.X, this.Facility20TextClient.Y + this.DisplayOffset.Y, this.Facility20TextClient.Width, this.Facility20TextClient.Height); }
            else if (i == 21) { D = new Rectangle(this.Facility21TextClient.X + this.DisplayOffset.X, this.Facility21TextClient.Y + this.DisplayOffset.Y, this.Facility21TextClient.Width, this.Facility21TextClient.Height); }
            else if (i == 22) { D = new Rectangle(this.Facility22TextClient.X + this.DisplayOffset.X, this.Facility22TextClient.Y + this.DisplayOffset.Y, this.Facility22TextClient.Width, this.Facility22TextClient.Height); }
            else if (i == 23) { D = new Rectangle(this.Facility23TextClient.X + this.DisplayOffset.X, this.Facility23TextClient.Y + this.DisplayOffset.Y, this.Facility23TextClient.Width, this.Facility23TextClient.Height); }
            else if (i == 24) { D = new Rectangle(this.Facility24TextClient.X + this.DisplayOffset.X, this.Facility24TextClient.Y + this.DisplayOffset.Y, this.Facility24TextClient.Width, this.Facility24TextClient.Height); }
            else if (i == 25) { D = new Rectangle(this.Facility25TextClient.X + this.DisplayOffset.X, this.Facility25TextClient.Y + this.DisplayOffset.Y, this.Facility25TextClient.Width, this.Facility25TextClient.Height); }
            else if (i == 26) { D = new Rectangle(this.Facility26TextClient.X + this.DisplayOffset.X, this.Facility26TextClient.Y + this.DisplayOffset.Y, this.Facility26TextClient.Width, this.Facility26TextClient.Height); }
            else if (i == 27) { D = new Rectangle(this.Facility27TextClient.X + this.DisplayOffset.X, this.Facility27TextClient.Y + this.DisplayOffset.Y, this.Facility27TextClient.Width, this.Facility27TextClient.Height); }
            else if (i == 28) { D = new Rectangle(this.Facility28TextClient.X + this.DisplayOffset.X, this.Facility28TextClient.Y + this.DisplayOffset.Y, this.Facility28TextClient.Width, this.Facility28TextClient.Height); }
            else if (i == 29) { D = new Rectangle(this.Facility29TextClient.X + this.DisplayOffset.X, this.Facility29TextClient.Y + this.DisplayOffset.Y, this.Facility29TextClient.Width, this.Facility29TextClient.Height); }
            else if (i == 30) { D = new Rectangle(this.Facility30TextClient.X + this.DisplayOffset.X, this.Facility30TextClient.Y + this.DisplayOffset.Y, this.Facility30TextClient.Width, this.Facility30TextClient.Height); }
            else if (i == 31) { D = new Rectangle(this.Facility31TextClient.X + this.DisplayOffset.X, this.Facility31TextClient.Y + this.DisplayOffset.Y, this.Facility31TextClient.Width, this.Facility31TextClient.Height); }
            else if (i == 32) { D = new Rectangle(this.Facility32TextClient.X + this.DisplayOffset.X, this.Facility32TextClient.Y + this.DisplayOffset.Y, this.Facility32TextClient.Width, this.Facility32TextClient.Height); }
            else if (i == 33) { D = new Rectangle(this.Facility33TextClient.X + this.DisplayOffset.X, this.Facility33TextClient.Y + this.DisplayOffset.Y, this.Facility33TextClient.Width, this.Facility33TextClient.Height); }
            else if (i == 34) { D = new Rectangle(this.Facility34TextClient.X + this.DisplayOffset.X, this.Facility34TextClient.Y + this.DisplayOffset.Y, this.Facility34TextClient.Width, this.Facility34TextClient.Height); }
            else if (i == 35) { D = new Rectangle(this.Facility35TextClient.X + this.DisplayOffset.X, this.Facility35TextClient.Y + this.DisplayOffset.Y, this.Facility35TextClient.Width, this.Facility35TextClient.Height); }
            else if (i == 36) { D = new Rectangle(this.Facility36TextClient.X + this.DisplayOffset.X, this.Facility36TextClient.Y + this.DisplayOffset.Y, this.Facility36TextClient.Width, this.Facility36TextClient.Height); }
            else if (i == 37) { D = new Rectangle(this.Facility37TextClient.X + this.DisplayOffset.X, this.Facility37TextClient.Y + this.DisplayOffset.Y, this.Facility37TextClient.Width, this.Facility37TextClient.Height); }
            else if (i == 38) { D = new Rectangle(this.Facility38TextClient.X + this.DisplayOffset.X, this.Facility38TextClient.Y + this.DisplayOffset.Y, this.Facility38TextClient.Width, this.Facility38TextClient.Height); }
            else if (i == 39) { D = new Rectangle(this.Facility39TextClient.X + this.DisplayOffset.X, this.Facility39TextClient.Y + this.DisplayOffset.Y, this.Facility39TextClient.Width, this.Facility39TextClient.Height); }
            else if (i == 40) { D = new Rectangle(this.Facility40TextClient.X + this.DisplayOffset.X, this.Facility40TextClient.Y + this.DisplayOffset.Y, this.Facility40TextClient.Width, this.Facility40TextClient.Height); }
            else if (i == 41) { D = new Rectangle(this.Facility41TextClient.X + this.DisplayOffset.X, this.Facility41TextClient.Y + this.DisplayOffset.Y, this.Facility41TextClient.Width, this.Facility41TextClient.Height); }
            else if (i == 42) { D = new Rectangle(this.Facility42TextClient.X + this.DisplayOffset.X, this.Facility42TextClient.Y + this.DisplayOffset.Y, this.Facility42TextClient.Width, this.Facility42TextClient.Height); }
            else if (i == 43) { D = new Rectangle(this.Facility43TextClient.X + this.DisplayOffset.X, this.Facility43TextClient.Y + this.DisplayOffset.Y, this.Facility43TextClient.Width, this.Facility43TextClient.Height); }
            else if (i == 44) { D = new Rectangle(this.Facility44TextClient.X + this.DisplayOffset.X, this.Facility44TextClient.Y + this.DisplayOffset.Y, this.Facility44TextClient.Width, this.Facility44TextClient.Height); }
            else if (i == 45) { D = new Rectangle(this.Facility45TextClient.X + this.DisplayOffset.X, this.Facility45TextClient.Y + this.DisplayOffset.Y, this.Facility45TextClient.Width, this.Facility45TextClient.Height); }
            else if (i == 46) { D = new Rectangle(this.Facility46TextClient.X + this.DisplayOffset.X, this.Facility46TextClient.Y + this.DisplayOffset.Y, this.Facility46TextClient.Width, this.Facility46TextClient.Height); }
            else if (i == 47) { D = new Rectangle(this.Facility47TextClient.X + this.DisplayOffset.X, this.Facility47TextClient.Y + this.DisplayOffset.Y, this.Facility47TextClient.Width, this.Facility47TextClient.Height); }
            else if (i == 48) { D = new Rectangle(this.Facility48TextClient.X + this.DisplayOffset.X, this.Facility48TextClient.Y + this.DisplayOffset.Y, this.Facility48TextClient.Width, this.Facility48TextClient.Height); }
            else if (i == 49) { D = new Rectangle(this.Facility49TextClient.X + this.DisplayOffset.X, this.Facility49TextClient.Y + this.DisplayOffset.Y, this.Facility49TextClient.Width, this.Facility49TextClient.Height); }
            else if (i == 50) { D = new Rectangle(this.Facility50TextClient.X + this.DisplayOffset.X, this.Facility50TextClient.Y + this.DisplayOffset.Y, this.Facility50TextClient.Width, this.Facility50TextClient.Height); }
            else if (i == 51) { D = new Rectangle(this.Facility51TextClient.X + this.DisplayOffset.X, this.Facility51TextClient.Y + this.DisplayOffset.Y, this.Facility51TextClient.Width, this.Facility51TextClient.Height); }
            else if (i == 52) { D = new Rectangle(this.Facility52TextClient.X + this.DisplayOffset.X, this.Facility52TextClient.Y + this.DisplayOffset.Y, this.Facility52TextClient.Width, this.Facility52TextClient.Height); }
            else if (i == 53) { D = new Rectangle(this.Facility53TextClient.X + this.DisplayOffset.X, this.Facility53TextClient.Y + this.DisplayOffset.Y, this.Facility53TextClient.Width, this.Facility53TextClient.Height); }
            else if (i == 54) { D = new Rectangle(this.Facility54TextClient.X + this.DisplayOffset.X, this.Facility54TextClient.Y + this.DisplayOffset.Y, this.Facility54TextClient.Width, this.Facility54TextClient.Height); }
            else if (i == 55) { D = new Rectangle(this.Facility55TextClient.X + this.DisplayOffset.X, this.Facility55TextClient.Y + this.DisplayOffset.Y, this.Facility55TextClient.Width, this.Facility55TextClient.Height); }
            else if (i == 56) { D = new Rectangle(this.Facility56TextClient.X + this.DisplayOffset.X, this.Facility56TextClient.Y + this.DisplayOffset.Y, this.Facility56TextClient.Width, this.Facility56TextClient.Height); }
            else if (i == 57) { D = new Rectangle(this.Facility57TextClient.X + this.DisplayOffset.X, this.Facility57TextClient.Y + this.DisplayOffset.Y, this.Facility57TextClient.Width, this.Facility57TextClient.Height); }
            else if (i == 58) { D = new Rectangle(this.Facility58TextClient.X + this.DisplayOffset.X, this.Facility58TextClient.Y + this.DisplayOffset.Y, this.Facility58TextClient.Width, this.Facility58TextClient.Height); }
            else if (i == 59) { D = new Rectangle(this.Facility59TextClient.X + this.DisplayOffset.X, this.Facility59TextClient.Y + this.DisplayOffset.Y, this.Facility59TextClient.Width, this.Facility59TextClient.Height); }
            else if (i == 60) { D = new Rectangle(this.Facility60TextClient.X + this.DisplayOffset.X, this.Facility60TextClient.Y + this.DisplayOffset.Y, this.Facility60TextClient.Width, this.Facility60TextClient.Height); }
            else if (i == 61) { D = new Rectangle(this.Facility61TextClient.X + this.DisplayOffset.X, this.Facility61TextClient.Y + this.DisplayOffset.Y, this.Facility61TextClient.Width, this.Facility61TextClient.Height); }
            else if (i == 62) { D = new Rectangle(this.Facility62TextClient.X + this.DisplayOffset.X, this.Facility62TextClient.Y + this.DisplayOffset.Y, this.Facility62TextClient.Width, this.Facility62TextClient.Height); }
            else if (i == 63) { D = new Rectangle(this.Facility63TextClient.X + this.DisplayOffset.X, this.Facility63TextClient.Y + this.DisplayOffset.Y, this.Facility63TextClient.Width, this.Facility63TextClient.Height); }
            else if (i == 64) { D = new Rectangle(this.Facility64TextClient.X + this.DisplayOffset.X, this.Facility64TextClient.Y + this.DisplayOffset.Y, this.Facility64TextClient.Width, this.Facility64TextClient.Height); }
            else if (i == 65) { D = new Rectangle(this.Facility65TextClient.X + this.DisplayOffset.X, this.Facility65TextClient.Y + this.DisplayOffset.Y, this.Facility65TextClient.Width, this.Facility65TextClient.Height); }
            else if (i == 66) { D = new Rectangle(this.Facility66TextClient.X + this.DisplayOffset.X, this.Facility66TextClient.Y + this.DisplayOffset.Y, this.Facility66TextClient.Width, this.Facility66TextClient.Height); }
            else if (i == 67) { D = new Rectangle(this.Facility67TextClient.X + this.DisplayOffset.X, this.Facility67TextClient.Y + this.DisplayOffset.Y, this.Facility67TextClient.Width, this.Facility67TextClient.Height); }
            else if (i == 68) { D = new Rectangle(this.Facility68TextClient.X + this.DisplayOffset.X, this.Facility68TextClient.Y + this.DisplayOffset.Y, this.Facility68TextClient.Width, this.Facility68TextClient.Height); }
            else if (i == 69) { D = new Rectangle(this.Facility69TextClient.X + this.DisplayOffset.X, this.Facility69TextClient.Y + this.DisplayOffset.Y, this.Facility69TextClient.Width, this.Facility69TextClient.Height); }
            else if (i == 70) { D = new Rectangle(this.Facility70TextClient.X + this.DisplayOffset.X, this.Facility70TextClient.Y + this.DisplayOffset.Y, this.Facility70TextClient.Width, this.Facility70TextClient.Height); }
            else if (i == 71) { D = new Rectangle(this.Facility71TextClient.X + this.DisplayOffset.X, this.Facility71TextClient.Y + this.DisplayOffset.Y, this.Facility71TextClient.Width, this.Facility71TextClient.Height); }
            else if (i == 72) { D = new Rectangle(this.Facility72TextClient.X + this.DisplayOffset.X, this.Facility72TextClient.Y + this.DisplayOffset.Y, this.Facility72TextClient.Width, this.Facility72TextClient.Height); }
            else if (i == 73) { D = new Rectangle(this.Facility73TextClient.X + this.DisplayOffset.X, this.Facility73TextClient.Y + this.DisplayOffset.Y, this.Facility73TextClient.Width, this.Facility73TextClient.Height); }
            else if (i == 74) { D = new Rectangle(this.Facility74TextClient.X + this.DisplayOffset.X, this.Facility74TextClient.Y + this.DisplayOffset.Y, this.Facility74TextClient.Width, this.Facility74TextClient.Height); }
            else if (i == 75) { D = new Rectangle(this.Facility75TextClient.X + this.DisplayOffset.X, this.Facility75TextClient.Y + this.DisplayOffset.Y, this.Facility75TextClient.Width, this.Facility75TextClient.Height); }
            else if (i == 76) { D = new Rectangle(this.Facility76TextClient.X + this.DisplayOffset.X, this.Facility76TextClient.Y + this.DisplayOffset.Y, this.Facility76TextClient.Width, this.Facility76TextClient.Height); }
            else if (i == 77) { D = new Rectangle(this.Facility77TextClient.X + this.DisplayOffset.X, this.Facility77TextClient.Y + this.DisplayOffset.Y, this.Facility77TextClient.Width, this.Facility77TextClient.Height); }
            else if (i == 78) { D = new Rectangle(this.Facility78TextClient.X + this.DisplayOffset.X, this.Facility78TextClient.Y + this.DisplayOffset.Y, this.Facility78TextClient.Width, this.Facility78TextClient.Height); }
            else if (i == 79) { D = new Rectangle(this.Facility79TextClient.X + this.DisplayOffset.X, this.Facility79TextClient.Y + this.DisplayOffset.Y, this.Facility79TextClient.Width, this.Facility79TextClient.Height); }
            else if (i == 80) { D = new Rectangle(this.Facility80TextClient.X + this.DisplayOffset.X, this.Facility80TextClient.Y + this.DisplayOffset.Y, this.Facility80TextClient.Width, this.Facility80TextClient.Height); }
            else if (i == 81) { D = new Rectangle(this.Facility81TextClient.X + this.DisplayOffset.X, this.Facility81TextClient.Y + this.DisplayOffset.Y, this.Facility81TextClient.Width, this.Facility81TextClient.Height); }
            else if (i == 82) { D = new Rectangle(this.Facility82TextClient.X + this.DisplayOffset.X, this.Facility82TextClient.Y + this.DisplayOffset.Y, this.Facility82TextClient.Width, this.Facility82TextClient.Height); }
            else if (i == 83) { D = new Rectangle(this.Facility83TextClient.X + this.DisplayOffset.X, this.Facility83TextClient.Y + this.DisplayOffset.Y, this.Facility83TextClient.Width, this.Facility83TextClient.Height); }
            else if (i == 84) { D = new Rectangle(this.Facility84TextClient.X + this.DisplayOffset.X, this.Facility84TextClient.Y + this.DisplayOffset.Y, this.Facility84TextClient.Width, this.Facility84TextClient.Height); }
            else if (i == 85) { D = new Rectangle(this.Facility85TextClient.X + this.DisplayOffset.X, this.Facility85TextClient.Y + this.DisplayOffset.Y, this.Facility85TextClient.Width, this.Facility85TextClient.Height); }
            else if (i == 86) { D = new Rectangle(this.Facility86TextClient.X + this.DisplayOffset.X, this.Facility86TextClient.Y + this.DisplayOffset.Y, this.Facility86TextClient.Width, this.Facility86TextClient.Height); }
            else if (i == 87) { D = new Rectangle(this.Facility87TextClient.X + this.DisplayOffset.X, this.Facility87TextClient.Y + this.DisplayOffset.Y, this.Facility87TextClient.Width, this.Facility87TextClient.Height); }
            else if (i == 88) { D = new Rectangle(this.Facility88TextClient.X + this.DisplayOffset.X, this.Facility88TextClient.Y + this.DisplayOffset.Y, this.Facility88TextClient.Width, this.Facility88TextClient.Height); }
            else if (i == 89) { D = new Rectangle(this.Facility89TextClient.X + this.DisplayOffset.X, this.Facility89TextClient.Y + this.DisplayOffset.Y, this.Facility89TextClient.Width, this.Facility89TextClient.Height); }
            else if (i == 90) { D = new Rectangle(this.Facility90TextClient.X + this.DisplayOffset.X, this.Facility90TextClient.Y + this.DisplayOffset.Y, this.Facility90TextClient.Width, this.Facility90TextClient.Height); }
            else if (i == 91) { D = new Rectangle(this.Facility91TextClient.X + this.DisplayOffset.X, this.Facility91TextClient.Y + this.DisplayOffset.Y, this.Facility91TextClient.Width, this.Facility91TextClient.Height); }
            else if (i == 92) { D = new Rectangle(this.Facility92TextClient.X + this.DisplayOffset.X, this.Facility92TextClient.Y + this.DisplayOffset.Y, this.Facility92TextClient.Width, this.Facility92TextClient.Height); }
            else if (i == 93) { D = new Rectangle(this.Facility93TextClient.X + this.DisplayOffset.X, this.Facility93TextClient.Y + this.DisplayOffset.Y, this.Facility93TextClient.Width, this.Facility93TextClient.Height); }
            else if (i == 94) { D = new Rectangle(this.Facility94TextClient.X + this.DisplayOffset.X, this.Facility94TextClient.Y + this.DisplayOffset.Y, this.Facility94TextClient.Width, this.Facility94TextClient.Height); }
            else if (i == 95) { D = new Rectangle(this.Facility95TextClient.X + this.DisplayOffset.X, this.Facility95TextClient.Y + this.DisplayOffset.Y, this.Facility95TextClient.Width, this.Facility95TextClient.Height); }
            else if (i == 96) { D = new Rectangle(this.Facility96TextClient.X + this.DisplayOffset.X, this.Facility96TextClient.Y + this.DisplayOffset.Y, this.Facility96TextClient.Width, this.Facility96TextClient.Height); }
            else if (i == 97) { D = new Rectangle(this.Facility97TextClient.X + this.DisplayOffset.X, this.Facility97TextClient.Y + this.DisplayOffset.Y, this.Facility97TextClient.Width, this.Facility97TextClient.Height); }
            else if (i == 98) { D = new Rectangle(this.Facility98TextClient.X + this.DisplayOffset.X, this.Facility98TextClient.Y + this.DisplayOffset.Y, this.Facility98TextClient.Width, this.Facility98TextClient.Height); }
            else if (i == 99) { D = new Rectangle(this.Facility99TextClient.X + this.DisplayOffset.X, this.Facility99TextClient.Y + this.DisplayOffset.Y, this.Facility99TextClient.Width, this.Facility99TextClient.Height); }
            else if (i == 100) { D = new Rectangle(this.Facility100TextClient.X + this.DisplayOffset.X, this.Facility100TextClient.Y + this.DisplayOffset.Y, this.Facility100TextClient.Width, this.Facility100TextClient.Height); }
            else if (i == 101) { D = new Rectangle(this.Facility101TextClient.X + this.DisplayOffset.X, this.Facility101TextClient.Y + this.DisplayOffset.Y, this.Facility101TextClient.Width, this.Facility101TextClient.Height); }
            else if (i == 102) { D = new Rectangle(this.Facility102TextClient.X + this.DisplayOffset.X, this.Facility102TextClient.Y + this.DisplayOffset.Y, this.Facility102TextClient.Width, this.Facility102TextClient.Height); }
            else if (i == 103) { D = new Rectangle(this.Facility103TextClient.X + this.DisplayOffset.X, this.Facility103TextClient.Y + this.DisplayOffset.Y, this.Facility103TextClient.Width, this.Facility103TextClient.Height); }
            else if (i == 104) { D = new Rectangle(this.Facility104TextClient.X + this.DisplayOffset.X, this.Facility104TextClient.Y + this.DisplayOffset.Y, this.Facility104TextClient.Width, this.Facility104TextClient.Height); }
            else if (i == 105) { D = new Rectangle(this.Facility105TextClient.X + this.DisplayOffset.X, this.Facility105TextClient.Y + this.DisplayOffset.Y, this.Facility105TextClient.Width, this.Facility105TextClient.Height); }
            else if (i == 106) { D = new Rectangle(this.Facility106TextClient.X + this.DisplayOffset.X, this.Facility106TextClient.Y + this.DisplayOffset.Y, this.Facility106TextClient.Width, this.Facility106TextClient.Height); }
            else if (i == 107) { D = new Rectangle(this.Facility107TextClient.X + this.DisplayOffset.X, this.Facility107TextClient.Y + this.DisplayOffset.Y, this.Facility107TextClient.Width, this.Facility107TextClient.Height); }
            else if (i == 108) { D = new Rectangle(this.Facility108TextClient.X + this.DisplayOffset.X, this.Facility108TextClient.Y + this.DisplayOffset.Y, this.Facility108TextClient.Width, this.Facility108TextClient.Height); }
            else if (i == 109) { D = new Rectangle(this.Facility109TextClient.X + this.DisplayOffset.X, this.Facility109TextClient.Y + this.DisplayOffset.Y, this.Facility109TextClient.Width, this.Facility109TextClient.Height); }
            else if (i == 110) { D = new Rectangle(this.Facility110TextClient.X + this.DisplayOffset.X, this.Facility110TextClient.Y + this.DisplayOffset.Y, this.Facility110TextClient.Width, this.Facility110TextClient.Height); }
            else if (i == 111) { D = new Rectangle(this.Facility111TextClient.X + this.DisplayOffset.X, this.Facility111TextClient.Y + this.DisplayOffset.Y, this.Facility111TextClient.Width, this.Facility111TextClient.Height); }
            else if (i == 112) { D = new Rectangle(this.Facility112TextClient.X + this.DisplayOffset.X, this.Facility112TextClient.Y + this.DisplayOffset.Y, this.Facility112TextClient.Width, this.Facility112TextClient.Height); }
            else if (i == 113) { D = new Rectangle(this.Facility113TextClient.X + this.DisplayOffset.X, this.Facility113TextClient.Y + this.DisplayOffset.Y, this.Facility113TextClient.Width, this.Facility113TextClient.Height); }
            else if (i == 114) { D = new Rectangle(this.Facility114TextClient.X + this.DisplayOffset.X, this.Facility114TextClient.Y + this.DisplayOffset.Y, this.Facility114TextClient.Width, this.Facility114TextClient.Height); }
            else if (i == 115) { D = new Rectangle(this.Facility115TextClient.X + this.DisplayOffset.X, this.Facility115TextClient.Y + this.DisplayOffset.Y, this.Facility115TextClient.Width, this.Facility115TextClient.Height); }
            else if (i == 116) { D = new Rectangle(this.Facility116TextClient.X + this.DisplayOffset.X, this.Facility116TextClient.Y + this.DisplayOffset.Y, this.Facility116TextClient.Width, this.Facility116TextClient.Height); }
            else if (i == 117) { D = new Rectangle(this.Facility117TextClient.X + this.DisplayOffset.X, this.Facility117TextClient.Y + this.DisplayOffset.Y, this.Facility117TextClient.Width, this.Facility117TextClient.Height); }
            else if (i == 118) { D = new Rectangle(this.Facility118TextClient.X + this.DisplayOffset.X, this.Facility118TextClient.Y + this.DisplayOffset.Y, this.Facility118TextClient.Width, this.Facility118TextClient.Height); }
            else if (i == 119) { D = new Rectangle(this.Facility119TextClient.X + this.DisplayOffset.X, this.Facility119TextClient.Y + this.DisplayOffset.Y, this.Facility119TextClient.Width, this.Facility119TextClient.Height); }
            else if (i == 120) { D = new Rectangle(this.Facility120TextClient.X + this.DisplayOffset.X, this.Facility120TextClient.Y + this.DisplayOffset.Y, this.Facility120TextClient.Width, this.Facility120TextClient.Height); }
            else if (i == 121) { D = new Rectangle(this.Facility121TextClient.X + this.DisplayOffset.X, this.Facility121TextClient.Y + this.DisplayOffset.Y, this.Facility121TextClient.Width, this.Facility121TextClient.Height); }
            else if (i == 122) { D = new Rectangle(this.Facility122TextClient.X + this.DisplayOffset.X, this.Facility122TextClient.Y + this.DisplayOffset.Y, this.Facility122TextClient.Width, this.Facility122TextClient.Height); }
            else if (i == 123) { D = new Rectangle(this.Facility123TextClient.X + this.DisplayOffset.X, this.Facility123TextClient.Y + this.DisplayOffset.Y, this.Facility123TextClient.Width, this.Facility123TextClient.Height); }
            else if (i == 124) { D = new Rectangle(this.Facility124TextClient.X + this.DisplayOffset.X, this.Facility124TextClient.Y + this.DisplayOffset.Y, this.Facility124TextClient.Width, this.Facility124TextClient.Height); }
            else if (i == 125) { D = new Rectangle(this.Facility125TextClient.X + this.DisplayOffset.X, this.Facility125TextClient.Y + this.DisplayOffset.Y, this.Facility125TextClient.Width, this.Facility125TextClient.Height); }
            else if (i == 126) { D = new Rectangle(this.Facility126TextClient.X + this.DisplayOffset.X, this.Facility126TextClient.Y + this.DisplayOffset.Y, this.Facility126TextClient.Width, this.Facility126TextClient.Height); }
            else if (i == 127) { D = new Rectangle(this.Facility127TextClient.X + this.DisplayOffset.X, this.Facility127TextClient.Y + this.DisplayOffset.Y, this.Facility127TextClient.Width, this.Facility127TextClient.Height); }
            else if (i == 128) { D = new Rectangle(this.Facility128TextClient.X + this.DisplayOffset.X, this.Facility128TextClient.Y + this.DisplayOffset.Y, this.Facility128TextClient.Width, this.Facility128TextClient.Height); }
            else if (i == 129) { D = new Rectangle(this.Facility129TextClient.X + this.DisplayOffset.X, this.Facility129TextClient.Y + this.DisplayOffset.Y, this.Facility129TextClient.Width, this.Facility129TextClient.Height); }
            else if (i == 130) { D = new Rectangle(this.Facility130TextClient.X + this.DisplayOffset.X, this.Facility130TextClient.Y + this.DisplayOffset.Y, this.Facility130TextClient.Width, this.Facility130TextClient.Height); }
            else if (i == 131) { D = new Rectangle(this.Facility131TextClient.X + this.DisplayOffset.X, this.Facility131TextClient.Y + this.DisplayOffset.Y, this.Facility131TextClient.Width, this.Facility131TextClient.Height); }
            else if (i == 132) { D = new Rectangle(this.Facility132TextClient.X + this.DisplayOffset.X, this.Facility132TextClient.Y + this.DisplayOffset.Y, this.Facility132TextClient.Width, this.Facility132TextClient.Height); }
            else if (i == 133) { D = new Rectangle(this.Facility133TextClient.X + this.DisplayOffset.X, this.Facility133TextClient.Y + this.DisplayOffset.Y, this.Facility133TextClient.Width, this.Facility133TextClient.Height); }
            else if (i == 134) { D = new Rectangle(this.Facility134TextClient.X + this.DisplayOffset.X, this.Facility134TextClient.Y + this.DisplayOffset.Y, this.Facility134TextClient.Width, this.Facility134TextClient.Height); }
            else if (i == 135) { D = new Rectangle(this.Facility135TextClient.X + this.DisplayOffset.X, this.Facility135TextClient.Y + this.DisplayOffset.Y, this.Facility135TextClient.Width, this.Facility135TextClient.Height); }
            else if (i == 136) { D = new Rectangle(this.Facility136TextClient.X + this.DisplayOffset.X, this.Facility136TextClient.Y + this.DisplayOffset.Y, this.Facility136TextClient.Width, this.Facility136TextClient.Height); }
            else if (i == 137) { D = new Rectangle(this.Facility137TextClient.X + this.DisplayOffset.X, this.Facility137TextClient.Y + this.DisplayOffset.Y, this.Facility137TextClient.Width, this.Facility137TextClient.Height); }
            else if (i == 138) { D = new Rectangle(this.Facility138TextClient.X + this.DisplayOffset.X, this.Facility138TextClient.Y + this.DisplayOffset.Y, this.Facility138TextClient.Width, this.Facility138TextClient.Height); }
            else if (i == 139) { D = new Rectangle(this.Facility139TextClient.X + this.DisplayOffset.X, this.Facility139TextClient.Y + this.DisplayOffset.Y, this.Facility139TextClient.Width, this.Facility139TextClient.Height); }
            else if (i == 140) { D = new Rectangle(this.Facility140TextClient.X + this.DisplayOffset.X, this.Facility140TextClient.Y + this.DisplayOffset.Y, this.Facility140TextClient.Width, this.Facility140TextClient.Height); }
            else if (i == 141) { D = new Rectangle(this.Facility141TextClient.X + this.DisplayOffset.X, this.Facility141TextClient.Y + this.DisplayOffset.Y, this.Facility141TextClient.Width, this.Facility141TextClient.Height); }
            else if (i == 142) { D = new Rectangle(this.Facility142TextClient.X + this.DisplayOffset.X, this.Facility142TextClient.Y + this.DisplayOffset.Y, this.Facility142TextClient.Width, this.Facility142TextClient.Height); }
            else if (i == 143) { D = new Rectangle(this.Facility143TextClient.X + this.DisplayOffset.X, this.Facility143TextClient.Y + this.DisplayOffset.Y, this.Facility143TextClient.Width, this.Facility143TextClient.Height); }
            else if (i == 144) { D = new Rectangle(this.Facility144TextClient.X + this.DisplayOffset.X, this.Facility144TextClient.Y + this.DisplayOffset.Y, this.Facility144TextClient.Width, this.Facility144TextClient.Height); }
            else if (i == 145) { D = new Rectangle(this.Facility145TextClient.X + this.DisplayOffset.X, this.Facility145TextClient.Y + this.DisplayOffset.Y, this.Facility145TextClient.Width, this.Facility145TextClient.Height); }
            else if (i == 146) { D = new Rectangle(this.Facility146TextClient.X + this.DisplayOffset.X, this.Facility146TextClient.Y + this.DisplayOffset.Y, this.Facility146TextClient.Width, this.Facility146TextClient.Height); }
            else if (i == 147) { D = new Rectangle(this.Facility147TextClient.X + this.DisplayOffset.X, this.Facility147TextClient.Y + this.DisplayOffset.Y, this.Facility147TextClient.Width, this.Facility147TextClient.Height); }
            else if (i == 148) { D = new Rectangle(this.Facility148TextClient.X + this.DisplayOffset.X, this.Facility148TextClient.Y + this.DisplayOffset.Y, this.Facility148TextClient.Width, this.Facility148TextClient.Height); }
            else if (i == 149) { D = new Rectangle(this.Facility149TextClient.X + this.DisplayOffset.X, this.Facility149TextClient.Y + this.DisplayOffset.Y, this.Facility149TextClient.Width, this.Facility149TextClient.Height); }
            else if (i == 150) { D = new Rectangle(this.Facility150TextClient.X + this.DisplayOffset.X, this.Facility150TextClient.Y + this.DisplayOffset.Y, this.Facility150TextClient.Width, this.Facility150TextClient.Height); }
            else if (i == 151) { D = new Rectangle(this.Facility151TextClient.X + this.DisplayOffset.X, this.Facility151TextClient.Y + this.DisplayOffset.Y, this.Facility151TextClient.Width, this.Facility151TextClient.Height); }
            else if (i == 152) { D = new Rectangle(this.Facility152TextClient.X + this.DisplayOffset.X, this.Facility152TextClient.Y + this.DisplayOffset.Y, this.Facility152TextClient.Width, this.Facility152TextClient.Height); }
            else if (i == 153) { D = new Rectangle(this.Facility153TextClient.X + this.DisplayOffset.X, this.Facility153TextClient.Y + this.DisplayOffset.Y, this.Facility153TextClient.Width, this.Facility153TextClient.Height); }
            else if (i == 154) { D = new Rectangle(this.Facility154TextClient.X + this.DisplayOffset.X, this.Facility154TextClient.Y + this.DisplayOffset.Y, this.Facility154TextClient.Width, this.Facility154TextClient.Height); }
            else if (i == 155) { D = new Rectangle(this.Facility155TextClient.X + this.DisplayOffset.X, this.Facility155TextClient.Y + this.DisplayOffset.Y, this.Facility155TextClient.Width, this.Facility155TextClient.Height); }
            else if (i == 156) { D = new Rectangle(this.Facility156TextClient.X + this.DisplayOffset.X, this.Facility156TextClient.Y + this.DisplayOffset.Y, this.Facility156TextClient.Width, this.Facility156TextClient.Height); }
            else if (i == 157) { D = new Rectangle(this.Facility157TextClient.X + this.DisplayOffset.X, this.Facility157TextClient.Y + this.DisplayOffset.Y, this.Facility157TextClient.Width, this.Facility157TextClient.Height); }
            else if (i == 158) { D = new Rectangle(this.Facility158TextClient.X + this.DisplayOffset.X, this.Facility158TextClient.Y + this.DisplayOffset.Y, this.Facility158TextClient.Width, this.Facility158TextClient.Height); }
            else if (i == 159) { D = new Rectangle(this.Facility159TextClient.X + this.DisplayOffset.X, this.Facility159TextClient.Y + this.DisplayOffset.Y, this.Facility159TextClient.Width, this.Facility159TextClient.Height); }
            else if (i == 160) { D = new Rectangle(this.Facility160TextClient.X + this.DisplayOffset.X, this.Facility160TextClient.Y + this.DisplayOffset.Y, this.Facility160TextClient.Width, this.Facility160TextClient.Height); }
            else if (i == 161) { D = new Rectangle(this.Facility161TextClient.X + this.DisplayOffset.X, this.Facility161TextClient.Y + this.DisplayOffset.Y, this.Facility161TextClient.Width, this.Facility161TextClient.Height); }
            else if (i == 162) { D = new Rectangle(this.Facility162TextClient.X + this.DisplayOffset.X, this.Facility162TextClient.Y + this.DisplayOffset.Y, this.Facility162TextClient.Width, this.Facility162TextClient.Height); }
            else if (i == 163) { D = new Rectangle(this.Facility163TextClient.X + this.DisplayOffset.X, this.Facility163TextClient.Y + this.DisplayOffset.Y, this.Facility163TextClient.Width, this.Facility163TextClient.Height); }
            else if (i == 164) { D = new Rectangle(this.Facility164TextClient.X + this.DisplayOffset.X, this.Facility164TextClient.Y + this.DisplayOffset.Y, this.Facility164TextClient.Width, this.Facility164TextClient.Height); }
            else if (i == 165) { D = new Rectangle(this.Facility165TextClient.X + this.DisplayOffset.X, this.Facility165TextClient.Y + this.DisplayOffset.Y, this.Facility165TextClient.Width, this.Facility165TextClient.Height); }
            else if (i == 166) { D = new Rectangle(this.Facility166TextClient.X + this.DisplayOffset.X, this.Facility166TextClient.Y + this.DisplayOffset.Y, this.Facility166TextClient.Width, this.Facility166TextClient.Height); }
            else if (i == 167) { D = new Rectangle(this.Facility167TextClient.X + this.DisplayOffset.X, this.Facility167TextClient.Y + this.DisplayOffset.Y, this.Facility167TextClient.Width, this.Facility167TextClient.Height); }
            else if (i == 168) { D = new Rectangle(this.Facility168TextClient.X + this.DisplayOffset.X, this.Facility168TextClient.Y + this.DisplayOffset.Y, this.Facility168TextClient.Width, this.Facility168TextClient.Height); }
            else if (i == 169) { D = new Rectangle(this.Facility169TextClient.X + this.DisplayOffset.X, this.Facility169TextClient.Y + this.DisplayOffset.Y, this.Facility169TextClient.Width, this.Facility169TextClient.Height); }
            else if (i == 170) { D = new Rectangle(this.Facility170TextClient.X + this.DisplayOffset.X, this.Facility170TextClient.Y + this.DisplayOffset.Y, this.Facility170TextClient.Width, this.Facility170TextClient.Height); }
            else if (i == 171) { D = new Rectangle(this.Facility171TextClient.X + this.DisplayOffset.X, this.Facility171TextClient.Y + this.DisplayOffset.Y, this.Facility171TextClient.Width, this.Facility171TextClient.Height); }
            else if (i == 172) { D = new Rectangle(this.Facility172TextClient.X + this.DisplayOffset.X, this.Facility172TextClient.Y + this.DisplayOffset.Y, this.Facility172TextClient.Width, this.Facility172TextClient.Height); }
            else if (i == 173) { D = new Rectangle(this.Facility173TextClient.X + this.DisplayOffset.X, this.Facility173TextClient.Y + this.DisplayOffset.Y, this.Facility173TextClient.Width, this.Facility173TextClient.Height); }
            else if (i == 174) { D = new Rectangle(this.Facility174TextClient.X + this.DisplayOffset.X, this.Facility174TextClient.Y + this.DisplayOffset.Y, this.Facility174TextClient.Width, this.Facility174TextClient.Height); }
            else if (i == 175) { D = new Rectangle(this.Facility175TextClient.X + this.DisplayOffset.X, this.Facility175TextClient.Y + this.DisplayOffset.Y, this.Facility175TextClient.Width, this.Facility175TextClient.Height); }
            else if (i == 176) { D = new Rectangle(this.Facility176TextClient.X + this.DisplayOffset.X, this.Facility176TextClient.Y + this.DisplayOffset.Y, this.Facility176TextClient.Width, this.Facility176TextClient.Height); }
            else if (i == 177) { D = new Rectangle(this.Facility177TextClient.X + this.DisplayOffset.X, this.Facility177TextClient.Y + this.DisplayOffset.Y, this.Facility177TextClient.Width, this.Facility177TextClient.Height); }
            else if (i == 178) { D = new Rectangle(this.Facility178TextClient.X + this.DisplayOffset.X, this.Facility178TextClient.Y + this.DisplayOffset.Y, this.Facility178TextClient.Width, this.Facility178TextClient.Height); }
            else if (i == 179) { D = new Rectangle(this.Facility179TextClient.X + this.DisplayOffset.X, this.Facility179TextClient.Y + this.DisplayOffset.Y, this.Facility179TextClient.Width, this.Facility179TextClient.Height); }
            else if (i == 180) { D = new Rectangle(this.Facility180TextClient.X + this.DisplayOffset.X, this.Facility180TextClient.Y + this.DisplayOffset.Y, this.Facility180TextClient.Width, this.Facility180TextClient.Height); }
            else if (i == 181) { D = new Rectangle(this.Facility181TextClient.X + this.DisplayOffset.X, this.Facility181TextClient.Y + this.DisplayOffset.Y, this.Facility181TextClient.Width, this.Facility181TextClient.Height); }
            else if (i == 182) { D = new Rectangle(this.Facility182TextClient.X + this.DisplayOffset.X, this.Facility182TextClient.Y + this.DisplayOffset.Y, this.Facility182TextClient.Width, this.Facility182TextClient.Height); }
            else if (i == 183) { D = new Rectangle(this.Facility183TextClient.X + this.DisplayOffset.X, this.Facility183TextClient.Y + this.DisplayOffset.Y, this.Facility183TextClient.Width, this.Facility183TextClient.Height); }
            else if (i == 184) { D = new Rectangle(this.Facility184TextClient.X + this.DisplayOffset.X, this.Facility184TextClient.Y + this.DisplayOffset.Y, this.Facility184TextClient.Width, this.Facility184TextClient.Height); }
            else if (i == 185) { D = new Rectangle(this.Facility185TextClient.X + this.DisplayOffset.X, this.Facility185TextClient.Y + this.DisplayOffset.Y, this.Facility185TextClient.Width, this.Facility185TextClient.Height); }
            else if (i == 186) { D = new Rectangle(this.Facility186TextClient.X + this.DisplayOffset.X, this.Facility186TextClient.Y + this.DisplayOffset.Y, this.Facility186TextClient.Width, this.Facility186TextClient.Height); }
            else if (i == 187) { D = new Rectangle(this.Facility187TextClient.X + this.DisplayOffset.X, this.Facility187TextClient.Y + this.DisplayOffset.Y, this.Facility187TextClient.Width, this.Facility187TextClient.Height); }
            else if (i == 188) { D = new Rectangle(this.Facility188TextClient.X + this.DisplayOffset.X, this.Facility188TextClient.Y + this.DisplayOffset.Y, this.Facility188TextClient.Width, this.Facility188TextClient.Height); }
            else if (i == 189) { D = new Rectangle(this.Facility189TextClient.X + this.DisplayOffset.X, this.Facility189TextClient.Y + this.DisplayOffset.Y, this.Facility189TextClient.Width, this.Facility189TextClient.Height); }
            else if (i == 190) { D = new Rectangle(this.Facility190TextClient.X + this.DisplayOffset.X, this.Facility190TextClient.Y + this.DisplayOffset.Y, this.Facility190TextClient.Width, this.Facility190TextClient.Height); }
            else if (i == 191) { D = new Rectangle(this.Facility191TextClient.X + this.DisplayOffset.X, this.Facility191TextClient.Y + this.DisplayOffset.Y, this.Facility191TextClient.Width, this.Facility191TextClient.Height); }
            else if (i == 192) { D = new Rectangle(this.Facility192TextClient.X + this.DisplayOffset.X, this.Facility192TextClient.Y + this.DisplayOffset.Y, this.Facility192TextClient.Width, this.Facility192TextClient.Height); }
            else if (i == 193) { D = new Rectangle(this.Facility193TextClient.X + this.DisplayOffset.X, this.Facility193TextClient.Y + this.DisplayOffset.Y, this.Facility193TextClient.Width, this.Facility193TextClient.Height); }
            else if (i == 194) { D = new Rectangle(this.Facility194TextClient.X + this.DisplayOffset.X, this.Facility194TextClient.Y + this.DisplayOffset.Y, this.Facility194TextClient.Width, this.Facility194TextClient.Height); }
            else if (i == 195) { D = new Rectangle(this.Facility195TextClient.X + this.DisplayOffset.X, this.Facility195TextClient.Y + this.DisplayOffset.Y, this.Facility195TextClient.Width, this.Facility195TextClient.Height); }
            else if (i == 196) { D = new Rectangle(this.Facility196TextClient.X + this.DisplayOffset.X, this.Facility196TextClient.Y + this.DisplayOffset.Y, this.Facility196TextClient.Width, this.Facility196TextClient.Height); }
            else if (i == 197) { D = new Rectangle(this.Facility197TextClient.X + this.DisplayOffset.X, this.Facility197TextClient.Y + this.DisplayOffset.Y, this.Facility197TextClient.Width, this.Facility197TextClient.Height); }
            else if (i == 198) { D = new Rectangle(this.Facility198TextClient.X + this.DisplayOffset.X, this.Facility198TextClient.Y + this.DisplayOffset.Y, this.Facility198TextClient.Width, this.Facility198TextClient.Height); }
            else if (i == 199) { D = new Rectangle(this.Facility199TextClient.X + this.DisplayOffset.X, this.Facility199TextClient.Y + this.DisplayOffset.Y, this.Facility199TextClient.Width, this.Facility199TextClient.Height); }
            else if (i == 200) { D = new Rectangle(this.Facility200TextClient.X + this.DisplayOffset.X, this.Facility200TextClient.Y + this.DisplayOffset.Y, this.Facility200TextClient.Width, this.Facility200TextClient.Height); }
            return D;
        }
        private Rectangle TheFacilityTextShowDisplayPosition(int i)
        {
            Rectangle D = this.NullDisplayPosition;
            if (HasTheFacilityKind(i) == true && TheFacilityButton(i) == true)
            {
                D = TheFacilityTextDisplayPosition(i);
            }
            return D;
        }
        private Rectangle FacilityDescriptionTextBackgroundDisplayPosition
        {
            get
            {
                if (FacilityButton == true && FacilityDescriptionTextIng == true)
                {
                    if (FacilityDescriptionTextFollowTheMouse == "on")
                    {
                        return new Rectangle(this.FacilityDescriptionTextBackgroundClient.X + Session.MainGame.mainGameScreen.MousePosition.X, this.FacilityDescriptionTextBackgroundClient.Y + Session.MainGame.mainGameScreen.MousePosition.Y, this.FacilityDescriptionTextBackgroundClient.Width, this.FacilityDescriptionTextBackgroundClient.Height);
                    }
                    return new Rectangle(this.FacilityDescriptionTextBackgroundClient.X + this.DisplayOffset.X, this.FacilityDescriptionTextBackgroundClient.Y + this.DisplayOffset.Y, this.FacilityDescriptionTextBackgroundClient.Width, this.FacilityDescriptionTextBackgroundClient.Height);
                }
                return NullDisplayPosition;
            }
        }

        private int BuildingFacilityID
        {
            get
            {
                for (int i = 1; i <= TheAllFacilityNumber; i++)
                {
                    if (TheFacilityKind(i) == TheBuildingFacilityID) { return i;}
                }
                return -1;
            }
        }
        private Rectangle TheBuildingFacilityPictureADisplayPosition
        {
            get
            {
                if (TheFacilityButton(BuildingFacilityID) == true)
                {
                    return TheFacilityDisplayPosition(BuildingFacilityID);
                }
                return NullDisplayPosition;
            }
        }
        private Rectangle TheBuildingFacilityPictureBDisplayPosition
        {
            get
            {
                if (TheFacilityButton(BuildingFacilityID) == true && TheFacilityIDForDescription == TheBuildingFacilityID)
                {
                    return TheFacilityDisplayPosition(BuildingFacilityID);
                }
                return NullDisplayPosition;
            }
        }
        private Rectangle TheBuildingFacilityTextDisplayPosition
        {
            get
            {
                if (TheFacilityButton(BuildingFacilityID) == true)
                {
                    return TheFacilityTextDisplayPosition(BuildingFacilityID);
                }
                return NullDisplayPosition;
            }
        }
        ////以上增加
    }
}

