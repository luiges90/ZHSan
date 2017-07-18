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

namespace ScreenBlindPlugin
{

    public class ScreenBlindPlugin : GameObject, IScreenBlind, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\ScreenBlind\Data\";
        private string description = "屏幕窗帘";
        private GraphicsDevice graphicsDevice;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.screenBlind.IsShowing)
            {
                this.screenBlind.Draw(spriteBatch);
            }
        }

        public void Initialize()
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
            this.screenBlind.BackgroundTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.screenBlind.BackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(1);
            this.screenBlind.SpringTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Spring").Value);
            this.screenBlind.SummerTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Summer").Value);
            this.screenBlind.AutumnTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Autumn").Value);
            this.screenBlind.WinterTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ScreenBlind\Data\" + node.Attributes.GetNamedItem("Winter").Value);
            this.screenBlind.SeasonTexture = this.screenBlind.SpringTexture;
            node = nextSibling.ChildNodes.Item(2);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.DateText = new FreeText(this.graphicsDevice, font, color);
            this.screenBlind.DateText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.DateText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.DateText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);
            node = nextSibling.ChildNodes.Item(3);
            this.screenBlind.FactionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.screenBlind.FactionText = new FreeText(this.graphicsDevice, font, color);
            this.screenBlind.FactionText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.screenBlind.FactionText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.screenBlind.FactionText.DisplayOffset = new Microsoft.Xna.Framework.Point(0, 0);
            node = nextSibling.ChildNodes.Item(4);
            this.screenBlind.SeasonClient = StaticMethods.LoadRectangleFromXMLNode(node);
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\ScreenBlindData.xml");
        }

        public void SetScreen(object screen)
        {
            this.screenBlind.Initialize(screen as Screen);
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

