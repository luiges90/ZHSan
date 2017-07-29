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

namespace TreasureDetailPlugin
{

    public class TreasureDetailPlugin : GameObject, ITreasureDetail, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\TreasureDetail\Data\";
        private string description = "宝物细节显示";
        
        private const string Path = @"Content\Textures\GameComponents\TreasureDetail\";
        private string pluginName = "TreasureDetailPlugin";
        private TreasureDetail treasureDetail = new TreasureDetail();
        private string version = "1.0.0";
        private const string XMLFilename = "TreasureDetailData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.treasureDetail.IsShowing)
            {
                this.treasureDetail.Draw();
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
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.treasureDetail.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.treasureDetail.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.treasureDetail.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TreasureDetail\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.treasureDetail.PictureClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
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
                this.treasureDetail.LabelTexts.Add(item);
            }
            node = nextSibling.ChildNodes.Item(3);
            this.treasureDetail.DescriptionClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.treasureDetail.DescriptionText.ClientWidth = this.treasureDetail.DescriptionClient.Width;
            this.treasureDetail.DescriptionText.ClientHeight = this.treasureDetail.DescriptionClient.Height;
            this.treasureDetail.DescriptionText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.treasureDetail.DescriptionText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.treasureDetail.DescriptionText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.treasureDetail.DescriptionText.Builder = font;  //.SetFreeTextBuilder(font);

            this.treasureDetail.DescriptionText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(4);
            this.treasureDetail.InfluenceClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.treasureDetail.InfluenceText.ClientWidth = this.treasureDetail.InfluenceClient.Width;
            this.treasureDetail.InfluenceText.ClientHeight = this.treasureDetail.InfluenceClient.Height;
            this.treasureDetail.InfluenceText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);


            this.treasureDetail.InfluenceText.Builder = font; //.SetFreeTextBuilder(font);

            this.treasureDetail.InfluenceText.DefaultColor = color;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\TreasureDetailData.xml");
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.treasureDetail.SetPosition(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.treasureDetail.Initialize();
        }

        public void SetTreasure(object treasure)
        {
            this.treasureDetail.SetTreasure(treasure as Treasure);
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
                return this.treasureDetail.IsShowing;
            }
            set
            {
                this.treasureDetail.IsShowing = value;
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

