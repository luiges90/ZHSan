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
using WorldOfTheThreeKingdoms.GameScreens;

namespace ScreenBlindPlugin
{

    public class ScreenBlindPlugin : GameObject, IScreenBlind, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\ScreenBlind\Data\";
        private string description = "屏幕窗帘";
        
        private const string Path = @"Content\Textures\GameComponents\ScreenBlind\";
        private string pluginName = "ScreenBlindPlugin";
        private ScreenBlind screenBlind = new ScreenBlind();
        private string version = "1.0.0";
        private const string XMLFilename = "ScreenBlindData.xml";

        public bool IsShowing
        {
            get
            {
                return screenBlind.IsShowing;
            }
            set
            {
                screenBlind.IsShowing = value;
            }
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.screenBlind.IsShowing)
            {
                this.screenBlind.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Microsoft.Xna.Framework.Color color;
            Font font;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.screenBlind.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.screenBlind.BackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(1);
            this.screenBlind.SpringTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Spring").Value);
            this.screenBlind.SummerTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Summer").Value);
            this.screenBlind.AutumnTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Autumn").Value);
            this.screenBlind.WinterTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Winter").Value);
            this.screenBlind.SeasonTexture = this.screenBlind.SpringTexture;
            node = nextSibling.ChildNodes.Item(2);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.DateText = new FreeText(font, color);
            this.screenBlind.DateText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.DateText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.DateText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);
            node = nextSibling.ChildNodes.Item(3);
            this.screenBlind.FactionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FactionText = new FreeText(font, color);
            this.screenBlind.FactionText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FactionText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FactionText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);
            node = nextSibling.ChildNodes.Item(4);
            this.screenBlind.SeasonClient = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(5);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FactionTechText = new FreeText(font, color);
            this.screenBlind.FactionTechText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FactionTechText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FactionTechText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            //阿柒:新增势力信息XML设置
            node = nextSibling.ChildNodes.Item(6);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.LeaderNameText = new FreeText(font, color);
            this.screenBlind.LeaderNameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.LeaderNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.LeaderNameText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(7);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.PrinceNameText = new FreeText(font, color);
            this.screenBlind.PrinceNameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.PrinceNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.PrinceNameText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(8);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.CounsellorNameText = new FreeText(font, color);
            this.screenBlind.CounsellorNameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.CounsellorNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.CounsellorNameText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(9);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.PersonCountText = new FreeText(font, color);
            this.screenBlind.PersonCountText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.PersonCountText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.PersonCountText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(10);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.PopulationText = new FreeText(font, color);
            this.screenBlind.PopulationText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.PopulationText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.PopulationText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(11);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.ArmyText = new FreeText(font, color);
            this.screenBlind.ArmyText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.ArmyText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.ArmyText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(12);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FundText = new FreeText(font, color);
            this.screenBlind.FundText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FundText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FundText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(13);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FoodText = new FreeText(font, color);
            this.screenBlind.FoodText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FoodText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FoodText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(14);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.CapitalNameText = new FreeText(font, color);
            this.screenBlind.CapitalNameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.CapitalNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.CapitalNameText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(15);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.guanjuezifuchuanText = new FreeText(font, color);
            this.screenBlind.guanjuezifuchuanText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.guanjuezifuchuanText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.guanjuezifuchuanText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(16);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.chaotinggongxianduText = new FreeText(font, color);
            this.screenBlind.chaotinggongxianduText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.chaotinggongxianduText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.chaotinggongxianduText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(17);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.CityCountText = new FreeText(font, color);
            this.screenBlind.CityCountText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.CityCountText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.CityCountText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(18);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FiveTigerText = new FreeText(font, color);
            this.screenBlind.FiveTigerText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FiveTigerText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FiveTigerText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);

            node = nextSibling.ChildNodes.Item(19);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.GovernorNameText = new FreeText(font, color);
            this.screenBlind.GovernorNameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.GovernorNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.GovernorNameText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\ScreenBlindData.xml");
        }

        public void SetScreen(Screen screen)
        {
            this.screenBlind.Initialize(screen as MainGameScreen);
        }

        public void Update(GameTime gameTime)
        {
            if (this.screenBlind.IsShowing)
            {
                this.screenBlind.Update();
            }
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

