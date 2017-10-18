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
            this.personDetail.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.personDetail.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.personDetail.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonDetail\Data\" + node.Attributes.GetNamedItem("FileName").Value);
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
            Microsoft.Xna.Framework.Color color2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SkillColor").Value);
            this.personDetail.PersonSkillTexts = new FreeTextList(font, color2);
            this.personDetail.PersonSkillTexts.Align = this.personDetail.AllSkillTexts.Align;
            Microsoft.Xna.Framework.Color color3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("LearnableColor").Value);
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
            this.personDetail.Initialize(screen);
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

