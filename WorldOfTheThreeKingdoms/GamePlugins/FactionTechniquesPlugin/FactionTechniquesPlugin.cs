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

namespace FactionTechniquesPlugin
{

    public class FactionTechniquesPlugin : GameObject, IFactionTechniques, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\FactionTechniques\Data\";
        private string description = "势力技巧界面";
        private FactionTechniques factionTechniques = new FactionTechniques();
        
        private const string Path = @"Content\Textures\GameComponents\FactionTechniques\";
        private string pluginName = "FactionTechniquesPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "FactionTechniquesData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.factionTechniques.IsShowing)
            {
                this.factionTechniques.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Font font;
            Microsoft.Xna.Framework.Color color;
            XmlDocument document = new XmlDocument();

            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.factionTechniques.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.factionTechniques.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.factionTechniques.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\FactionTechniques\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(1);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                XmlNode node3 = node.ChildNodes.Item(i);
                Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(node3);
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
                this.factionTechniques.LabelTexts.Add(item);
            }
            node = nextSibling.ChildNodes.Item(2);
            this.factionTechniques.ButtonStartPosition.X = int.Parse(node.Attributes.GetNamedItem("StartX").Value);
            this.factionTechniques.ButtonStartPosition.Y = int.Parse(node.Attributes.GetNamedItem("StartY").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.factionTechniques.ButtonTextFont = font;
            this.factionTechniques.ButtonTextColor = color;
            this.factionTechniques.ButtonTextAlign = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(3);
            this.factionTechniques.ButtonSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.factionTechniques.ButtonSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.factionTechniques.ButtonBasicTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\FactionTechniques\Data\" + node.Attributes.GetNamedItem("Basic").Value);
            this.factionTechniques.ButtonAvailableTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\FactionTechniques\Data\" + node.Attributes.GetNamedItem("Available").Value);
            this.factionTechniques.ButtonUpgradingTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\FactionTechniques\Data\" + node.Attributes.GetNamedItem("Upgrading").Value);
            this.factionTechniques.ButtonUpgradedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\FactionTechniques\Data\" + node.Attributes.GetNamedItem("Upgraded").Value);
            node = nextSibling.ChildNodes.Item(4);
            this.factionTechniques.CommentsClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.factionTechniques.CommentsText.ClientWidth = this.factionTechniques.CommentsClient.Width;
            this.factionTechniques.CommentsText.ClientHeight = this.factionTechniques.CommentsClient.Height;
            this.factionTechniques.CommentsText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.factionTechniques.CommentsText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.factionTechniques.CommentsText.PositiveColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("PositiveColor").Value);
            this.factionTechniques.CommentsText.NegativeColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("NegativeColor").Value);
            this.factionTechniques.CommentsText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.factionTechniques.CommentsText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.factionTechniques.CommentsText.Builder = font;  //.SetFreeTextBuilder(font);
            this.factionTechniques.CommentsText.DefaultColor = color;
        }

        public void SetArchitecture(object architecture)
        {
            this.factionTechniques.SetArchitecture(architecture as Architecture);
        }

        public void SetFaction(object faction, bool control)
        {
            this.factionTechniques.SetFaction(faction as Faction, control);
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\FactionTechniquesData.xml");
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.factionTechniques.SetPosition(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.factionTechniques.Initialize();
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
                return this.factionTechniques.IsShowing;
            }
            set
            {
                this.factionTechniques.IsShowing = value;
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

