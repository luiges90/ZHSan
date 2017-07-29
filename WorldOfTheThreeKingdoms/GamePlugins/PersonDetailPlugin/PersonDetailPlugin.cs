using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
//using System.Drawing;
using System.Xml;
using WorldOfTheThreeKingdoms;

namespace PersonDetailPlugin
{

    public class PersonDetailPlugin : GameObject, IPersonDetail, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\PersonDetail\Data\";
        private string description = "人物细节显示";
        
        private const string Path = @"Content\Textures\GameComponents\PersonDetail\";
        private PersonDetail personDetail = new PersonDetail();
        private string pluginName = "PersonDetailPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "PersonDetailData.xml";


        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.personDetail.IsShowing)
            {
                this.personDetail.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            XmlNode node3;
            Font font;
            Microsoft.Xna.Framework.Color color;

            //XmlDocument document = new XmlDocument();
            //document.Load(filename);

            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.personDetail.BackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("SizeX").Value);
            this.personDetail.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("SizeY").Value);
            this.personDetail.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.BackgroundMask1 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.BackgroundMask2 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            this.personDetail.PictureNull = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("PictureNull").Value);
            node = nextSibling.ChildNodes.Item(1);
            Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.SurNameText = new FreeText(font, color);
            this.personDetail.SurNameText.Position = rectangle;
            this.personDetail.SurNameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(2);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.GivenNameText = new FreeText(font, color);
            this.personDetail.GivenNameText.Position = rectangle;
            this.personDetail.GivenNameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(3);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.CalledNameText = new FreeText(font, color);
            this.personDetail.CalledNameText.Position = rectangle;
            this.personDetail.CalledNameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(4);
            this.personDetail.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(5);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                node3 = node.ChildNodes.Item(i);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Label = new FreeText(font, color);
                item.Label.Position = rectangle;
                item.Label.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.Label.Text = node3.Attributes.GetNamedItem("Label").Value;
                node3 = node.ChildNodes.Item(i + 1);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Text = new FreeText(font, color);
                item.Text.Position = rectangle;
                item.Text.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.PropertyName = node3.Attributes.GetNamedItem("PropertyName").Value;
                this.personDetail.LabelTexts.Add(item);
            }
            node = nextSibling.ChildNodes.Item(6);
            this.personDetail.TitleClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TitleText.ClientWidth = this.personDetail.TitleClient.Width;
            this.personDetail.TitleText.ClientHeight = this.personDetail.TitleClient.Height;
            this.personDetail.TitleText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TitleText.Builder = font;
            this.personDetail.TitleText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(7);
            this.personDetail.SkillBlockSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.personDetail.SkillBlockSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.personDetail.SkillDisplayOffset.X = int.Parse(node.Attributes.GetNamedItem("OffsetX").Value);
            this.personDetail.SkillDisplayOffset.Y = int.Parse(node.Attributes.GetNamedItem("OffsetY").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.AllSkillTexts = new FreeTextList(font, color);
            this.personDetail.AllSkillTexts.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            Microsoft.Xna.Framework.Color color2 = new Microsoft.Xna.Framework.Color {
                PackedValue = uint.Parse(node.Attributes.GetNamedItem("SkillColor").Value)
            };
            this.personDetail.PersonSkillTexts = new FreeTextList(font, color2);
            this.personDetail.PersonSkillTexts.Align = this.personDetail.AllSkillTexts.Align;
            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color {
                PackedValue = uint.Parse(node.Attributes.GetNamedItem("LearnableColor").Value)
            };
            this.personDetail.LearnableSkillTexts = new FreeTextList(font, color3);
            this.personDetail.LearnableSkillTexts.Align = this.personDetail.AllSkillTexts.Align;
            node = nextSibling.ChildNodes.Item(8);
            this.personDetail.StuntClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.StuntText.ClientWidth = this.personDetail.StuntClient.Width;
            this.personDetail.StuntText.ClientHeight = this.personDetail.StuntClient.Height;
            this.personDetail.StuntText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.StuntText.Builder.SetFreeTextBuilder(font);
            this.personDetail.StuntText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(9);
            this.personDetail.InfluenceClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.InfluenceText.ClientWidth = this.personDetail.InfluenceClient.Width;
            this.personDetail.InfluenceText.ClientHeight = this.personDetail.InfluenceClient.Height;
            this.personDetail.InfluenceText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.InfluenceText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.InfluenceText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.personDetail.InfluenceText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.personDetail.InfluenceText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.InfluenceText.Builder.SetFreeTextBuilder(font);
            this.personDetail.InfluenceText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(10);
            this.personDetail.ConditionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ConditionText.ClientWidth = this.personDetail.ConditionClient.Width;
            this.personDetail.ConditionText.ClientHeight = this.personDetail.ConditionClient.Height;
            this.personDetail.ConditionText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ConditionText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.ConditionText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.personDetail.ConditionText.PositiveColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("PositiveColor").Value);
            this.personDetail.ConditionText.NegativeColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NegativeColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ConditionText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ConditionText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(11);
            this.personDetail.BiographyClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyText.ClientWidth = this.personDetail.BiographyClient.Width;
            this.personDetail.BiographyText.ClientHeight = this.personDetail.BiographyClient.Height;
            this.personDetail.BiographyText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.BiographyText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.BiographyText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.personDetail.BiographyText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.BiographyText.Builder.SetFreeTextBuilder(font);
            this.personDetail.BiographyText.DefaultColor = color;
            /*
            node = nextSibling.ChildNodes.Item(12);
            this.personDetail.GuanzhiClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.GuanzhiText.ClientWidth = this.personDetail.GuanzhiClient.Width;
            this.personDetail.GuanzhiText.ClientHeight = this.personDetail.GuanzhiClient.Height;
            this.personDetail.GuanzhiText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.GuanzhiText.Builder.SetFreeTextBuilder(font);
            this.personDetail.GuanzhiText.DefaultColor = color;
             */
////////////////////////////////////////////////////
            node = nextSibling.ChildNodes.Item(13);
            this.personDetail.PersonTreasuresTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PersonTreasuresText.ClientWidth = this.personDetail.PersonTreasuresTextClient.Width;
            this.personDetail.PersonTreasuresText.ClientHeight = this.personDetail.PersonTreasuresTextClient.Height;
            this.personDetail.PersonTreasuresText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.PersonTreasuresText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor1").Value);
            this.personDetail.PersonTreasuresText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor2").Value);
            this.personDetail.PersonTreasuresText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("WorthColor").Value);
            this.personDetail.PersonTreasuresText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("DescriptionColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.PersonTreasuresText.Builder = font;
            this.personDetail.PersonTreasuresText.DefaultColor = color;
            this.personDetail.ShowPersonTreasuresCount = node.Attributes.GetNamedItem("ShowPersonTreasuresCount").Value;
            this.personDetail.ShowPersonTreasuresWorth = node.Attributes.GetNamedItem("ShowPersonTreasuresWorth").Value;
            this.personDetail.ShowPersonTreasuresDescription = node.Attributes.GetNamedItem("ShowPersonTreasuresDescription").Value;            
            this.personDetail.PersonTreasuresText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.PersonTreasuresText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.PersonTreasuresText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.PersonTreasuresText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.PersonTreasuresText5 = node.Attributes.GetNamedItem("Text5").Value;
            this.personDetail.PersonTreasuresText6 = node.Attributes.GetNamedItem("Text6").Value;
            this.personDetail.PersonTreasuresText7 = node.Attributes.GetNamedItem("Text7").Value;
            this.personDetail.PersonTreasuresText8 = node.Attributes.GetNamedItem("Text8").Value;
            this.personDetail.PersonTreasuresText9 = node.Attributes.GetNamedItem("Text9").Value;
            this.personDetail.PersonTreasuresText10 = node.Attributes.GetNamedItem("Text10").Value;
            this.personDetail.PersonTreasuresText11 = node.Attributes.GetNamedItem("Text11").Value;
            this.personDetail.PersonTreasuresText12 = node.Attributes.GetNamedItem("Text12").Value;
            this.personDetail.PersonTreasuresText13 = node.Attributes.GetNamedItem("Text13").Value;
            this.personDetail.PersonTreasuresText14 = node.Attributes.GetNamedItem("Text14").Value;
            ////////////////////////////////////////////////////
            node = nextSibling.ChildNodes.Item(15);            
            this.personDetail.Switch1 = node.Attributes.GetNamedItem("NewUI").Value;
            
            this.personDetail.Switch3 = node.Attributes.GetNamedItem("UISound").Value;
            this.personDetail.Switch4 = node.Attributes.GetNamedItem("PersonSound").Value;

            this.personDetail.Switch21 = node.Attributes.GetNamedItem("PersonBiography").Value;            
            this.personDetail.Switch24 = node.Attributes.GetNamedItem("BiographySeparate").Value;
            this.personDetail.Switch25 = node.Attributes.GetNamedItem("TheFirstShowOfBiography").Value;
            this.personDetail.Switch26 = node.Attributes.GetNamedItem("BiographyBrief").Value;
            this.personDetail.Switch27 = node.Attributes.GetNamedItem("BiographRomance").Value;
            this.personDetail.Switch28 = node.Attributes.GetNamedItem("BiographyHistory").Value;
            this.personDetail.Switch29 = node.Attributes.GetNamedItem("BiographyInGame").Value;            
           
            this.personDetail.Switch105 = node.Attributes.GetNamedItem("FactionName").Value;
            this.personDetail.Switch106 = node.Attributes.GetNamedItem("FactionColour").Value;
            
            this.personDetail.Switch114 = node.Attributes.GetNamedItem("ImportantTreasure").Value;
            this.personDetail.Switch115 = node.Attributes.GetNamedItem("GeneralTreasure").Value;
            this.personDetail.Switch116 = node.Attributes.GetNamedItem("ThePersonPictureInTreasure").Value;
            this.personDetail.Switch117 = node.Attributes.GetNamedItem("ImportantTitle").Value;
            this.personDetail.Switch118 = node.Attributes.GetNamedItem("ImportantStunt").Value;
                                 
            this.personDetail.Switch121 = node.Attributes.GetNamedItem("Relative").Value;
            this.personDetail.Switch122 = node.Attributes.GetNamedItem("Relation1").Value;
            this.personDetail.Switch123 = node.Attributes.GetNamedItem("Relation2").Value;
            this.personDetail.Switch124 = node.Attributes.GetNamedItem("Standings").Value;
            
            this.personDetail.Switch131 = node.Attributes.GetNamedItem("ConditionAndInfluenceBackground").Value;
            this.personDetail.Switch132 = node.Attributes.GetNamedItem("SkillBackground").Value;
            this.personDetail.Switch133 = node.Attributes.GetNamedItem("TitleBackground").Value;
            this.personDetail.Switch134 = node.Attributes.GetNamedItem("StuntBackground").Value;
            
            node = nextSibling.ChildNodes.Item(22);
            this.personDetail.BiographyButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.BiographyPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
          
            node = nextSibling.ChildNodes.Item(31);
            this.personDetail.InformationBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.InformationBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.InformationMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.InformationMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(32);
            this.personDetail.DetailBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.DetailBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.DetailMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.DetailMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(33);
            this.personDetail.TreasureBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TreasureBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.TreasureMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.TreasureMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(34);
            this.personDetail.TitleBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TitleBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.TitleMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.TitleMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(35);
            this.personDetail.SkillBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.SkillBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.SkillMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.SkillMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);           
            node = nextSibling.ChildNodes.Item(36);
            this.personDetail.StuntBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.StuntBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.StuntMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.StuntMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(37);
            this.personDetail.BiographyBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.BiographyMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.BiographyMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            node = nextSibling.ChildNodes.Item(38);
            this.personDetail.SpecialtyShowBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.SpecialtyShowBackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.SpecialtyShowMask1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.personDetail.SpecialtyShowMask2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
           
            node = nextSibling.ChildNodes.Item(41);
            this.personDetail.PortraitInInformationClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PortraitKindInInformation = node.Attributes.GetNamedItem("PortraitKind").Value;
            
            //
            node = nextSibling.ChildNodes.Item(61);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                node3 = node.ChildNodes.Item(i);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Label = new FreeText(font, color);
                item.Label.Position = rectangle;
                item.Label.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.Label.Text = node3.Attributes.GetNamedItem("Label").Value;
                node3 = node.ChildNodes.Item(i + 1);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Text = new FreeText(font, color);
                item.Text.Position = rectangle;
                item.Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.PropertyName = node3.Attributes.GetNamedItem("PropertyName").Value;
                this.personDetail.PersonInInformationTexts.Add(item);
            }           
            //           
            node = nextSibling.ChildNodes.Item(75);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.FactionNameText = new FreeText(font, color);
            this.personDetail.FactionNameText.Position = rectangle;
            this.personDetail.FactionNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);            
            this.personDetail.FactionNameBackgroundClient.X = int.Parse(node.Attributes.GetNamedItem("X").Value);
            this.personDetail.FactionNameBackgroundClient.Y = int.Parse(node.Attributes.GetNamedItem("Y").Value);
            this.personDetail.FactionNameBackgroundClient.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.personDetail.FactionNameBackgroundClient.Height = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.personDetail.FactionNameKind = node.Attributes.GetNamedItem("FactionNameKind").Value;
            this.personDetail.ShowFactionNameBackground = node.Attributes.GetNamedItem("ShowFactionNameBackground").Value;
            this.personDetail.FactionNameBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Faction\" + node.Attributes.GetNamedItem("Background").Value);
            node = nextSibling.ChildNodes.Item(76);
            this.personDetail.FactionColourClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.FactionColour = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Faction\" + node.Attributes.GetNamedItem("ColourPicture").Value);
            this.personDetail.FactionColourBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Faction\" + node.Attributes.GetNamedItem("ColourBackground").Value);
            //
            node = nextSibling.ChildNodes.Item(111);
            this.personDetail.ImportantTreasureButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasureButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.ImportantTreasurePressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            //
            node = nextSibling.ChildNodes.Item(112);
            this.personDetail.ImportantTreasureBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasureBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTreasureMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Mask").Value);
            this.personDetail.ImportantTreasureShowMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("TreasureMask").Value);
            this.personDetail.MaxImportantTreasureShowNumber = int.Parse(node.Attributes.GetNamedItem("MaxImportantTreasureShowNumber").Value);
            node = nextSibling.ChildNodes.Item(113);
            this.personDetail.GeneralTreasureBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.GeneralTreasureBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.GeneralTreasureMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Mask").Value);
            this.personDetail.ShowNullTreasurePicture = node.Attributes.GetNamedItem("ShowNullTreasurePicture").Value;            
            this.personDetail.MaxGeneralTreasureShowNumber = int.Parse(node.Attributes.GetNamedItem("MaxGeneralTreasureShowNumber").Value);              
            //
            node = nextSibling.ChildNodes.Item(114);
            this.personDetail.ImportantTreasureTextBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasureTextFollowTheMouse = node.Attributes.GetNamedItem("ImportantTreasureTextFollowTheMouse").Value;            
            this.personDetail.ImportantTreasureTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTreasureTextMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Mask").Value);
            node = nextSibling.ChildNodes.Item(115);
            this.personDetail.ImportantTreasureTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasureText.ClientWidth = this.personDetail.ImportantTreasureTextClient.Width;
            this.personDetail.ImportantTreasureText.ClientHeight = this.personDetail.ImportantTreasureTextClient.Height;
            this.personDetail.ImportantTreasureText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ImportantTreasureText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.ImportantTreasureText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor").Value);
            this.personDetail.ImportantTreasureText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("WorthColor").Value);
            this.personDetail.ImportantTreasureText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("DescriptionColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTreasureText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ImportantTreasureText.DefaultColor = color;
            this.personDetail.ImportantTreasureText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.ImportantTreasureText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.ImportantTreasureText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.ImportantTreasureText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.ImportantTreasureText5 = node.Attributes.GetNamedItem("Text5").Value;
            node = nextSibling.ChildNodes.Item(116);
            this.personDetail.ImportantTreasureTextPictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            //
            node = nextSibling.ChildNodes.Item(117);
            this.personDetail.ThePersonPictureInTreasureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ThePersonPictureKindInTreasure = node.Attributes.GetNamedItem("ThePictureKindInTreasure").Value;
            this.personDetail.TheSexPicture1 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Sex\" + node.Attributes.GetNamedItem("SexPicture1").Value);
            this.personDetail.TheSexPicture2 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Sex\" + node.Attributes.GetNamedItem("SexPicture2").Value);
            //
            node = nextSibling.ChildNodes.Item(121);
            this.personDetail.ImportantTreasure1Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure1Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(122);
            this.personDetail.ImportantTreasure2Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure2Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(123);
            this.personDetail.ImportantTreasure3Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure3Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(124);
            this.personDetail.ImportantTreasure4Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure4Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(125);
            this.personDetail.ImportantTreasure5Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure5Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(126);
            this.personDetail.ImportantTreasure6Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure6Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(127);
            this.personDetail.ImportantTreasure7Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure7Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(128);
            this.personDetail.ImportantTreasure8Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure8Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(129);
            this.personDetail.ImportantTreasure9Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure9Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(130);
            this.personDetail.ImportantTreasure10Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure10Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(131);
            this.personDetail.ImportantTreasure11Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure11Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(132);
            this.personDetail.ImportantTreasure12Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure12Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(133);
            this.personDetail.ImportantTreasure13Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure13Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(134);
            this.personDetail.ImportantTreasure14Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure14Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(135);
            this.personDetail.ImportantTreasure15Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure15Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(136);
            this.personDetail.ImportantTreasure16Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure16Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(137);
            this.personDetail.ImportantTreasure17Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure17Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(138);
            this.personDetail.ImportantTreasure18Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure18Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(139);
            this.personDetail.ImportantTreasure19Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure19Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(140);
            this.personDetail.ImportantTreasure20Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTreasure20Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(141);
            this.personDetail.GeneralTreasureShowClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.GeneralTreasureShowBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.GeneralTreasureShowMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTreasure\" + node.Attributes.GetNamedItem("Mask").Value);
            this.personDetail.GeneralTreasureHSpace = int.Parse(node.Attributes.GetNamedItem("HSpace").Value);
            this.personDetail.GeneralTreasureVSpace = int.Parse(node.Attributes.GetNamedItem("VSpace").Value);
            this.personDetail.GeneralTreasureHNumber = int.Parse(node.Attributes.GetNamedItem("HNumber").Value);
            this.personDetail.GeneralTreasureVNumber = int.Parse(node.Attributes.GetNamedItem("VNumber").Value);
            //
            node = nextSibling.ChildNodes.Item(151);
            this.personDetail.ImportantTitleTextBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.MaxImportantTitleShowNumber = int.Parse(node.Attributes.GetNamedItem("MaxImportantTitleShowNumber").Value);
            this.personDetail.ShowImportantTitleName = node.Attributes.GetNamedItem("ShowImportantTitleName").Value;
            this.personDetail.ShowImportantTitleNameBackground = node.Attributes.GetNamedItem("ShowImportantTitleNameBackground").Value;
            this.personDetail.ShowImportantTitlePicture = node.Attributes.GetNamedItem("ShowImportantTitlePicture").Value;
            this.personDetail.ImportantTitleTextFollowTheMouse = node.Attributes.GetNamedItem("ImportantTitleTextFollowTheMouse").Value;
            this.personDetail.ImportantTitleTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(152);
            this.personDetail.ImportantTitleTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitleText.ClientWidth = this.personDetail.ImportantTitleTextClient.Width;
            this.personDetail.ImportantTitleText.ClientHeight = this.personDetail.ImportantTitleTextClient.Height;
            this.personDetail.ImportantTitleText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ImportantTitleText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.ImportantTitleText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("KindColor").Value);
            this.personDetail.ImportantTitleText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("LevelColor").Value);
            this.personDetail.ImportantTitleText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("DescriptionColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitleText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ImportantTitleText.DefaultColor = color;
            this.personDetail.ImportantTitleText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.ImportantTitleText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.ImportantTitleText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.ImportantTitleText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.ImportantTitleText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(161);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle1NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle1NameText.Position = rectangle;
            this.personDetail.ImportantTitle1NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle1Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle1Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle1Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(162);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle2NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle2NameText.Position = rectangle;
            this.personDetail.ImportantTitle2NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle2Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle2Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle2Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);           
            node = nextSibling.ChildNodes.Item(163);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle3NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle3NameText.Position = rectangle;
            this.personDetail.ImportantTitle3NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle3Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle3Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle3Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(164);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle4NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle4NameText.Position = rectangle;
            this.personDetail.ImportantTitle4NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle4Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle4Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle4Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(165);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle5NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle5NameText.Position = rectangle;
            this.personDetail.ImportantTitle5NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle5Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle5Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle5Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(166);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle6NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle6NameText.Position = rectangle;
            this.personDetail.ImportantTitle6NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle6Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle6Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle6Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(167);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle7NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle7NameText.Position = rectangle;
            this.personDetail.ImportantTitle7NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle7Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle7Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle7Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(168);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle8NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle8NameText.Position = rectangle;
            this.personDetail.ImportantTitle8NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle8Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle8Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle8Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(169);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle9NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle9NameText.Position = rectangle;
            this.personDetail.ImportantTitle9NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle9Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle9Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle9Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(170);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle10NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle10NameText.Position = rectangle;
            this.personDetail.ImportantTitle10NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle10Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle10Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle10Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(171);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle11NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle11NameText.Position = rectangle;
            this.personDetail.ImportantTitle11NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle11Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle11Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle11Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(172);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle12NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle12NameText.Position = rectangle;
            this.personDetail.ImportantTitle12NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle12Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle12Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle12Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(173);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle13NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle13NameText.Position = rectangle;
            this.personDetail.ImportantTitle13NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle13Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle13Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle13Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(174);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle14NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle14NameText.Position = rectangle;
            this.personDetail.ImportantTitle14NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle14Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle14Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle14Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(175);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle15NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle15NameText.Position = rectangle;
            this.personDetail.ImportantTitle15NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle15Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle15Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle15Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(176);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle16NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle16NameText.Position = rectangle;
            this.personDetail.ImportantTitle16NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle16Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle16Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle16Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(177);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle17NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle17NameText.Position = rectangle;
            this.personDetail.ImportantTitle17NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle17Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle17Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle17Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(178);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle18NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle18NameText.Position = rectangle;
            this.personDetail.ImportantTitle18NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle18Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle18Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle18Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(179);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle19NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle19NameText.Position = rectangle;
            this.personDetail.ImportantTitle19NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle19Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle19Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle19Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(180);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantTitle20NameText = new FreeText(font, color);
            this.personDetail.ImportantTitle20NameText.Position = rectangle;
            this.personDetail.ImportantTitle20NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantTitle20Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle20Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantTitle20Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            node = nextSibling.ChildNodes.Item(181);
            this.personDetail.ImportantTitle1PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle1Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(182);
            this.personDetail.ImportantTitle2PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle2Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(183);
            this.personDetail.ImportantTitle3PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle3Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(184);
            this.personDetail.ImportantTitle4PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle4Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(185);
            this.personDetail.ImportantTitle5PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle5Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(186);
            this.personDetail.ImportantTitle6PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle6Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(187);
            this.personDetail.ImportantTitle7PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle7Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(188);
            this.personDetail.ImportantTitle8PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle8Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(189);
            this.personDetail.ImportantTitle9PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle9Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(190);
            this.personDetail.ImportantTitle10PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle10Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(191);
            this.personDetail.ImportantTitle11PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle11Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(192);
            this.personDetail.ImportantTitle12PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle12Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(193);
            this.personDetail.ImportantTitle13PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle13Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(194);
            this.personDetail.ImportantTitle14PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle14Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(195);
            this.personDetail.ImportantTitle15PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle15Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(196);
            this.personDetail.ImportantTitle16PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle16Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(197);
            this.personDetail.ImportantTitle17PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle17Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(198);
            this.personDetail.ImportantTitle18PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle18Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(199);
            this.personDetail.ImportantTitle19PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle19Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(200);
            this.personDetail.ImportantTitle20PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantTitle20Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantTitle\" + node.Attributes.GetNamedItem("FileName").Value);
            //
            node = nextSibling.ChildNodes.Item(201);
            this.personDetail.ImportantStuntTextBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.MaxImportantStuntShowNumber = int.Parse(node.Attributes.GetNamedItem("MaxImportantStuntShowNumber").Value);
            this.personDetail.ShowImportantStuntName = node.Attributes.GetNamedItem("ShowImportantStuntName").Value;
            this.personDetail.ShowImportantStuntNameBackground = node.Attributes.GetNamedItem("ShowImportantStuntNameBackground").Value;
            this.personDetail.ShowImportantStuntPicture = node.Attributes.GetNamedItem("ShowImportantStuntPicture").Value;
            this.personDetail.ImportantStuntTextFollowTheMouse = node.Attributes.GetNamedItem("ImportantStuntTextFollowTheMouse").Value;
            this.personDetail.ImportantStuntTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(202);
            this.personDetail.ImportantStuntTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStuntText.ClientWidth = this.personDetail.ImportantStuntTextClient.Width;
            this.personDetail.ImportantStuntText.ClientHeight = this.personDetail.ImportantStuntTextClient.Height;
            this.personDetail.ImportantStuntText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ImportantStuntText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.ImportantStuntText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("KindColor").Value);
            this.personDetail.ImportantStuntText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("LevelColor").Value);
            this.personDetail.ImportantStuntText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("DescriptionColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStuntText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ImportantStuntText.DefaultColor = color;
            this.personDetail.ImportantStuntText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.ImportantStuntText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.ImportantStuntText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.ImportantStuntText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.ImportantStuntText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(211);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt1NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt1NameText.Position = rectangle;
            this.personDetail.ImportantStunt1NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt1Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt1Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt1Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt1Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(212);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt2NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt2NameText.Position = rectangle;
            this.personDetail.ImportantStunt2NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt2Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt2Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt2Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt2Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(213);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt3NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt3NameText.Position = rectangle;
            this.personDetail.ImportantStunt3NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt3Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt3Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt3Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt3Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(214);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt4NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt4NameText.Position = rectangle;
            this.personDetail.ImportantStunt4NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt4Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt4Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt4Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt4Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(215);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt5NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt5NameText.Position = rectangle;
            this.personDetail.ImportantStunt5NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt5Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt5Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt5Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt5Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(216);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt6NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt6NameText.Position = rectangle;
            this.personDetail.ImportantStunt6NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt6Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt6Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt6Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt6Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(217);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt7NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt7NameText.Position = rectangle;
            this.personDetail.ImportantStunt7NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt7Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt7Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt7Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt7Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(218);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt8NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt8NameText.Position = rectangle;
            this.personDetail.ImportantStunt8NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt8Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt8Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt8Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt8Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(219);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt9NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt9NameText.Position = rectangle;
            this.personDetail.ImportantStunt9NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt9Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt9Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt9Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt9Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(220);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt10NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt10NameText.Position = rectangle;
            this.personDetail.ImportantStunt10NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt10Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt10Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt10Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt10Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(221);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt11NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt11NameText.Position = rectangle;
            this.personDetail.ImportantStunt11NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt11Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt11Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt11Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt11Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(222);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt12NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt12NameText.Position = rectangle;
            this.personDetail.ImportantStunt12NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt12Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt12Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt12Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt12Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(223);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt13NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt13NameText.Position = rectangle;
            this.personDetail.ImportantStunt13NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt13Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt13Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt13Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt13Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(224);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt14NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt14NameText.Position = rectangle;
            this.personDetail.ImportantStunt14NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt14Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt14Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt14Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt14Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(225);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt15NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt15NameText.Position = rectangle;
            this.personDetail.ImportantStunt15NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt15Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt15Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt15Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt15Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(226);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt16NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt16NameText.Position = rectangle;
            this.personDetail.ImportantStunt16NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt16Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt16Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt16Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt16Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(227);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt17NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt17NameText.Position = rectangle;
            this.personDetail.ImportantStunt17NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt17Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt17Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt17Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt17Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(228);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt18NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt18NameText.Position = rectangle;
            this.personDetail.ImportantStunt18NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt18Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt18Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt18Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt18Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(229);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt19NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt19NameText.Position = rectangle;
            this.personDetail.ImportantStunt19NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt19Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt19Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt19Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt19Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(230);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ImportantStunt20NameText = new FreeText(font, color);
            this.personDetail.ImportantStunt20NameText.Position = rectangle;
            this.personDetail.ImportantStunt20NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.personDetail.ImportantStunt20Client = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt20Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.ImportantStunt20Group = int.Parse(node.Attributes.GetNamedItem("Group").Value);
            this.personDetail.ImportantStunt20Description = node.Attributes.GetNamedItem("Description").Value;
            node = nextSibling.ChildNodes.Item(231);
            this.personDetail.ImportantStunt1PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt1Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(232);
            this.personDetail.ImportantStunt2PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt2Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(233);
            this.personDetail.ImportantStunt3PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt3Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(234);
            this.personDetail.ImportantStunt4PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt4Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(235);
            this.personDetail.ImportantStunt5PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt5Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(236);
            this.personDetail.ImportantStunt6PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt6Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(237);
            this.personDetail.ImportantStunt7PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt7Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(238);
            this.personDetail.ImportantStunt8PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt8Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(239);
            this.personDetail.ImportantStunt9PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt9Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(240);
            this.personDetail.ImportantStunt10PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt10Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(241);
            this.personDetail.ImportantStunt11PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt11Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(242);
            this.personDetail.ImportantStunt12PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt12Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(243);
            this.personDetail.ImportantStunt13PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt13Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(244);
            this.personDetail.ImportantStunt14PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt14Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(245);
            this.personDetail.ImportantStunt15PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt15Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(246);
            this.personDetail.ImportantStunt16PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt16Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(247);
            this.personDetail.ImportantStunt17PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt17Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(248);
            this.personDetail.ImportantStunt18PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt18Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(249);
            this.personDetail.ImportantStunt19PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt19Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(250);
            this.personDetail.ImportantStunt20PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ImportantStunt20Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\ImportantStunt\" + node.Attributes.GetNamedItem("FileName").Value);
            //
            node = nextSibling.ChildNodes.Item(251);
            this.personDetail.BiographyBriefButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyBriefButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.BiographyBriefPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(252);
            this.personDetail.BiographyRomanceButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyRomanceButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.BiographyRomancePressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(253);
            this.personDetail.BiographyHistoryButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyHistoryButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.BiographyHistoryPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(254);
            this.personDetail.BiographyInGameButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BiographyInGameButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.BiographyInGamePressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(255);
            this.personDetail.PersonBiographyTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PersonBiographyText.ClientWidth = this.personDetail.PersonBiographyTextClient.Width;
            this.personDetail.PersonBiographyText.ClientHeight = this.personDetail.PersonBiographyTextClient.Height;
            this.personDetail.PersonBiographyText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.PersonBiographyText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("BriefColor").Value);
            this.personDetail.PersonBiographyText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("RomanceColor").Value);
            this.personDetail.PersonBiographyText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("HistoryColor").Value);
            this.personDetail.PersonBiographyText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("InGameColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.PersonBiographyText.Builder.SetFreeTextBuilder(font);
            this.personDetail.PersonBiographyText.DefaultColor = color;
            this.personDetail.PersonBiographyText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.PersonBiographyText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.PersonBiographyText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.PersonBiographyText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.PersonBiographyText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(261);
            this.personDetail.RelativeButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.RelativeButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.RelativePressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(262);
            this.personDetail.RelationButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.RelationButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.RelationPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(263);
            this.personDetail.PersonRelationButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PersonRelationButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.PersonRelationPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            node = nextSibling.ChildNodes.Item(264);
            this.personDetail.StandingsButtonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.StandingsButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName0").Value);
            this.personDetail.StandingsPressedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\Button\" + node.Attributes.GetNamedItem("FileName1").Value);
            //
            node = nextSibling.ChildNodes.Item(266);
            this.personDetail.RelativeBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.RelativeBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.RelativeMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Mask").Value);
            node = nextSibling.ChildNodes.Item(267);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.GenerationText = new FreeText(font, color);
            this.personDetail.GenerationText.Position = rectangle;
            this.personDetail.GenerationText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(268);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.StrainText = new FreeText(font, color);
            this.personDetail.StrainText.Position = rectangle;
            this.personDetail.StrainText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(269);
            this.personDetail.SmallPortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(270);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.FatherNameText = new FreeText(font, color);
            this.personDetail.FatherNameText.Position = rectangle;
            this.personDetail.FatherNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(271);
            this.personDetail.FatherSmallPortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(272);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.MotherNameText = new FreeText(font, color);
            this.personDetail.MotherNameText.Position = rectangle;
            this.personDetail.MotherNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(273);
            this.personDetail.MotherSmallPortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(274);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.SpouseNameText = new FreeText(font, color);
            this.personDetail.SpouseNameText.Position = rectangle;
            this.personDetail.SpouseNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(275);
            this.personDetail.SpouseSmallPortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(276);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ChildrenNumberText = new FreeText(font, color);
            this.personDetail.ChildrenNumberText.Position = rectangle;
            this.personDetail.ChildrenNumberText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(277);
            this.personDetail.ChildrenTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ChildrenText.ClientWidth = this.personDetail.ChildrenTextClient.Width;
            this.personDetail.ChildrenText.ClientHeight = this.personDetail.ChildrenTextClient.Height;
            this.personDetail.ChildrenText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ChildrenText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor1").Value);
            this.personDetail.ChildrenText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor2").Value);
            this.personDetail.ChildrenText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("DisplayedAgeColor").Value);
            this.personDetail.ChildrenText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("LocationColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ChildrenText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ChildrenText.DefaultColor = color;
            this.personDetail.ChildrenText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.ChildrenText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.ChildrenText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.ChildrenText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.ChildrenText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(281);
            this.personDetail.RelationBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.RelationBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.RelationMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Mask").Value);
            node = nextSibling.ChildNodes.Item(282);
            this.personDetail.BrothersTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.BrothersText.ClientWidth = this.personDetail.BrothersTextClient.Width;
            this.personDetail.BrothersText.ClientHeight = this.personDetail.BrothersTextClient.Height;
            this.personDetail.BrothersText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.BrothersText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor").Value);
            this.personDetail.BrothersText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text1and2Color").Value);
            this.personDetail.BrothersText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text3Color").Value);
            this.personDetail.BrothersText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text4and5Color").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.BrothersText.Builder.SetFreeTextBuilder(font);
            this.personDetail.BrothersText.DefaultColor = color;
            this.personDetail.VerticalForBrothersText = node.Attributes.GetNamedItem("Vertical").Value;
            this.personDetail.BrothersText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.BrothersText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.BrothersText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.BrothersText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.BrothersText5 = node.Attributes.GetNamedItem("Text5").Value;
            node = nextSibling.ChildNodes.Item(283);
            this.personDetail.ClosePersonsTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ClosePersonsText.ClientWidth = this.personDetail.ClosePersonsTextClient.Width;
            this.personDetail.ClosePersonsText.ClientHeight = this.personDetail.ClosePersonsTextClient.Height;
            this.personDetail.ClosePersonsText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.ClosePersonsText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor").Value);
            this.personDetail.ClosePersonsText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text1and2Color").Value);
            this.personDetail.ClosePersonsText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text3Color").Value);
            this.personDetail.ClosePersonsText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text4and5Color").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.ClosePersonsText.Builder.SetFreeTextBuilder(font);
            this.personDetail.ClosePersonsText.DefaultColor = color;
            this.personDetail.VerticalForClosePersonsText = node.Attributes.GetNamedItem("Vertical").Value;
            this.personDetail.ClosePersonsText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.ClosePersonsText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.ClosePersonsText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.ClosePersonsText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.ClosePersonsText5 = node.Attributes.GetNamedItem("Text5").Value;
            node = nextSibling.ChildNodes.Item(284);
            this.personDetail.HatedPersonsTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.HatedPersonsText.ClientWidth = this.personDetail.HatedPersonsTextClient.Width;
            this.personDetail.HatedPersonsText.ClientHeight = this.personDetail.HatedPersonsTextClient.Height;
            this.personDetail.HatedPersonsText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.HatedPersonsText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NameColor").Value);
            this.personDetail.HatedPersonsText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text1and2Color").Value);
            this.personDetail.HatedPersonsText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text3Color").Value);
            this.personDetail.HatedPersonsText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text4and5Color").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.HatedPersonsText.Builder.SetFreeTextBuilder(font);
            this.personDetail.HatedPersonsText.DefaultColor = color;
            this.personDetail.VerticalForHatedPersonsText = node.Attributes.GetNamedItem("Vertical").Value;
            this.personDetail.HatedPersonsText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.HatedPersonsText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.HatedPersonsText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.HatedPersonsText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.HatedPersonsText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(286);
            this.personDetail.PersonRelationBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PersonRelationBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.PersonRelationMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Mask").Value);
            node = nextSibling.ChildNodes.Item(287);
            this.personDetail.PersonRelationTextClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.PersonRelationText.ClientWidth = this.personDetail.PersonRelationTextClient.Width;
            this.personDetail.PersonRelationText.ClientHeight = this.personDetail.PersonRelationTextClient.Height;
            this.personDetail.PersonRelationText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.PersonRelationText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text1and2Color").Value);
            this.personDetail.PersonRelationText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Relation1Color").Value);
            this.personDetail.PersonRelationText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Text3and4Color").Value);
            this.personDetail.PersonRelationText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Relation2Color").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.PersonRelationText.Builder.SetFreeTextBuilder(font);
            this.personDetail.PersonRelationText.DefaultColor = color;
            this.personDetail.PersonRelationText1 = node.Attributes.GetNamedItem("Text1").Value;
            this.personDetail.PersonRelationText2 = node.Attributes.GetNamedItem("Text2").Value;
            this.personDetail.PersonRelationText3 = node.Attributes.GetNamedItem("Text3").Value;
            this.personDetail.PersonRelationText4 = node.Attributes.GetNamedItem("Text4").Value;
            this.personDetail.PersonRelationText5 = node.Attributes.GetNamedItem("Text5").Value;
            //
            node = nextSibling.ChildNodes.Item(288);
            this.personDetail.StandingsBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.StandingsBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Background").Value);
            this.personDetail.StandingsMask = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("Mask").Value);
            node = nextSibling.ChildNodes.Item(289);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                node3 = node.ChildNodes.Item(i);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Label = new FreeText(font, color);
                item.Label.Position = rectangle;
                item.Label.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.Label.Text = node3.Attributes.GetNamedItem("Label").Value;
                node3 = node.ChildNodes.Item(i + 1);
                rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
                StaticMethods.LoadFontAndColorFromXMLNode(node3, out font, out color);
                item.Text = new FreeText(font, color);
                item.Text.Position = rectangle;
                item.Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node3.Attributes.GetNamedItem("Align").Value);
                item.PropertyName = node3.Attributes.GetNamedItem("PropertyName").Value;
                this.personDetail.StandingsTexts.Add(item);
            }
            //
            node = nextSibling.ChildNodes.Item(291);
            this.personDetail.PersonSkillTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("PersonSkill").Value);
            this.personDetail.LearnableSkillTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("LearnableSkill").Value);
            this.personDetail.AllSkillTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("AllSkill").Value);      
            node = nextSibling.ChildNodes.Item(292);
            this.personDetail.TitleTextBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TitleTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("FileName").Value);
            this.personDetail.TitleTextHeight = int.Parse(node.Attributes.GetNamedItem("TextHeight").Value);
            node = nextSibling.ChildNodes.Item(293);
            this.personDetail.StuntTextBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.StuntTextBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("FileName").Value);
            this.personDetail.StuntTextHeight = int.Parse(node.Attributes.GetNamedItem("TextHeight").Value);
            //            
            node = nextSibling.ChildNodes.Item(294);
            this.personDetail.ConditionBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.ConditionBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(295);
            this.personDetail.InfluenceBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.InfluenceBackground = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\Backgrounds\DetailBackgrounds\" + node.Attributes.GetNamedItem("FileName").Value);
            //
            node = nextSibling.ChildNodes.Item(296);
            this.personDetail.TheSkillBlockSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.personDetail.TheSkillBlockSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.personDetail.TheSkillDisplayOffset.X = int.Parse(node.Attributes.GetNamedItem("OffsetX").Value);
            this.personDetail.TheSkillDisplayOffset.Y = int.Parse(node.Attributes.GetNamedItem("OffsetY").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TheAllSkillTexts = new FreeTextList(font, color);
            this.personDetail.TheAllSkillTexts.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            Microsoft.Xna.Framework.Color color4 = new Microsoft.Xna.Framework.Color
            {
                PackedValue = uint.Parse(node.Attributes.GetNamedItem("SkillColor").Value)
            };
            this.personDetail.ThePersonSkillTexts = new FreeTextList(font, color2);
            this.personDetail.ThePersonSkillTexts.Align = this.personDetail.AllSkillTexts.Align;
            Microsoft.Xna.Framework.Color color5 = new Microsoft.Xna.Framework.Color
            {
                PackedValue = uint.Parse(node.Attributes.GetNamedItem("LearnableColor").Value)
            };
            this.personDetail.TheLearnableSkillTexts = new FreeTextList(font, color3);
            this.personDetail.TheLearnableSkillTexts.Align = this.personDetail.AllSkillTexts.Align;
            node = nextSibling.ChildNodes.Item(297);
            this.personDetail.TheTitleClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TheTitleText.ClientWidth = this.personDetail.TitleClient.Width;
            this.personDetail.TheTitleText.ClientHeight = this.personDetail.TitleClient.Height;
            this.personDetail.TheTitleText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TheTitleText.Builder.SetFreeTextBuilder(font);
            this.personDetail.TheTitleText.DefaultColor = color;            
            node = nextSibling.ChildNodes.Item(298);
            this.personDetail.TheStuntClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TheStuntText.ClientWidth = this.personDetail.StuntClient.Width;
            this.personDetail.TheStuntText.ClientHeight = this.personDetail.StuntClient.Height;
            this.personDetail.TheStuntText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TheStuntText.Builder.SetFreeTextBuilder(font);
            this.personDetail.TheStuntText.DefaultColor = color;   
            //
            node = nextSibling.ChildNodes.Item(299);
            this.personDetail.TheConditionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TheConditionText.ClientWidth = this.personDetail.ConditionClient.Width;
            this.personDetail.TheConditionText.ClientHeight = this.personDetail.ConditionClient.Height;
            this.personDetail.TheConditionText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.TheConditionText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.TheConditionText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.personDetail.TheConditionText.PositiveColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("PositiveColor").Value);
            this.personDetail.TheConditionText.NegativeColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NegativeColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TheConditionText.Builder.SetFreeTextBuilder(font);
            this.personDetail.TheConditionText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(300);
            this.personDetail.TheInfluenceClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personDetail.TheInfluenceText.ClientWidth = this.personDetail.InfluenceClient.Width;
            this.personDetail.TheInfluenceText.ClientHeight = this.personDetail.InfluenceClient.Height;
            this.personDetail.TheInfluenceText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.personDetail.TheInfluenceText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.personDetail.TheInfluenceText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.personDetail.TheInfluenceText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.personDetail.TheInfluenceText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.personDetail.TheInfluenceText.Builder.SetFreeTextBuilder(font);
            this.personDetail.TheInfluenceText.DefaultColor = color;            

//////////////////////////////////////////////////////////////////
        }
        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\PersonDetailData.xml");
        }

        public void SetPerson(object person)
        {
            this.personDetail.SetPerson(person as Person);
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.personDetail.SetPosition(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.personDetail.Initialize();
        }

        public void Update(GameTime gameTime)
        {
        }

        public string Author
        {
            get
            {
                return this.author;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public object Instance
        {
            get
            {
                return this;
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.personDetail.IsShowing;
            }
            set
            {
                this.personDetail.IsShowing = value;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }
    }
}

