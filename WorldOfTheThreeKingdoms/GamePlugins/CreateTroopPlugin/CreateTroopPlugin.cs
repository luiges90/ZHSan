using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
//using System.Drawing;
using System.Xml;
using WorldOfTheThreeKingdoms;

namespace CreateTroopPlugin
{

    public class CreateTroopPlugin : GameObject, ICreateTroop, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        public CreateTroop createTroop = new CreateTroop();
        private const string DataPath = @"Content\Textures\GameComponents\CreateTroop\Data\";
        private string description = "创建部队对话框";
        private const string Path = @"Content\Textures\GameComponents\CreateTroop\";
        private string pluginName = "CreateTroopPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "CreateTroopData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.createTroop.IsShowing)
            {
                this.createTroop.Draw();
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
            this.createTroop.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.createTroop.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.createTroop.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(1);
            Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.createTroop.TroopNameText = new FreeText(font, color);
            this.createTroop.TroopNameText.Position = rectangle;
            this.createTroop.TroopNameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(2);
            this.createTroop.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(3);
            for (int i = 0; i < node.ChildNodes.Count; i += 2)
            {
                LabelText item = new LabelText();
                XmlNode node3 = node.ChildNodes.Item(i);
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
                this.createTroop.LabelTexts.Add(item);
            }
            node = nextSibling.ChildNodes.Item(4);
            this.createTroop.OtherPersonClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.OtherPersonText.ClientWidth = this.createTroop.OtherPersonClient.Width;
            this.createTroop.OtherPersonText.ClientHeight = this.createTroop.OtherPersonClient.Height;
            this.createTroop.OtherPersonText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.createTroop.OtherPersonText.Builder = font; //.SetFreeTextBuilder(font);
            this.createTroop.OtherPersonText.DefaultColor = color;
            this.createTroop.OtherPersonText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.createTroop.OtherPersonText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            node = nextSibling.ChildNodes.Item(5);
            this.createTroop.CombatMethodClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.CombatMethodText.ClientWidth = this.createTroop.CombatMethodClient.Width;
            this.createTroop.CombatMethodText.ClientHeight = this.createTroop.CombatMethodClient.Height;
            this.createTroop.CombatMethodText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.createTroop.CombatMethodText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.createTroop.CombatMethodText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.createTroop.CombatMethodText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.createTroop.CombatMethodText.Builder = font; //.SetFreeTextBuilder(font);
            this.createTroop.CombatMethodText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(6);
            this.createTroop.StuntClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.StuntText.ClientWidth = this.createTroop.StuntClient.Width;
            this.createTroop.StuntText.ClientHeight = this.createTroop.StuntClient.Height;
            this.createTroop.StuntText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.createTroop.StuntText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.createTroop.StuntText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.createTroop.StuntText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.createTroop.StuntText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.createTroop.StuntText.Builder = font; //.SetFreeTextBuilder(font);
            this.createTroop.StuntText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(7);
            this.createTroop.InfluenceClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.InfluenceText.ClientWidth = this.createTroop.InfluenceClient.Width;
            this.createTroop.InfluenceText.ClientHeight = this.createTroop.InfluenceClient.Height;
            this.createTroop.InfluenceText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            this.createTroop.InfluenceText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.createTroop.InfluenceText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);
            this.createTroop.InfluenceText.SubTitleColor2 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.createTroop.InfluenceText.SubTitleColor3 = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.createTroop.InfluenceText.Builder = font;  //.SetFreeTextBuilder(font);
            this.createTroop.InfluenceText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(8);
            this.createTroop.MilitaryButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.MilitaryButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.MilitaryButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.MilitaryButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.MilitaryButtonDisplayTexture = this.createTroop.MilitaryButtonTexture;
            node = nextSibling.ChildNodes.Item(9);
            this.createTroop.PersonButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.PersonButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.PersonButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.PersonButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.PersonButtonDisplayTexture = this.createTroop.PersonButtonTexture;
            node = nextSibling.ChildNodes.Item(10);
            this.createTroop.LeaderButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.LeaderButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.LeaderButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.LeaderButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.LeaderButtonDisplayTexture = this.createTroop.LeaderButtonTexture;
            node = nextSibling.ChildNodes.Item(11);
            this.createTroop.RationButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.RationButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.RationButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.RationButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.RationButtonDisplayTexture = this.createTroop.RationButtonTexture;
            node = nextSibling.ChildNodes.Item(12);
            this.createTroop.CreateButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.CreateButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.CreateButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.CreateButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.CreateButtonDisplayTexture = this.createTroop.CreateButtonTexture;
            node = nextSibling.ChildNodes.Item(13);
            this.createTroop.zijinButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.createTroop.zijinButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.createTroop.zijinButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\CreateTroop\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.createTroop.zijinButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.createTroop.zijinButtonDisplayTexture = this.createTroop.zijinButtonTexture;
        }

        public void SetArchitecture(object architecture)
        {
            this.createTroop.SetArchitecture(architecture as Architecture);
        }

        public void SetCreateFunction(GameDelegates.VoidFunction function)
        {
            this.createTroop.CreateFunction = function;
        }

        public void SetGameFrame(IGameFrame iGameFrame)
        {
            this.createTroop.GameFramePlugin = iGameFrame;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\CreateTroopData.xml");
        }

        public void SetNumberInputer(INumberInputer iNumberInputer)
        {
            this.createTroop.NumberInputerPlugin = iNumberInputer;
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.createTroop.SetPosition(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.createTroop.Initialize();
        }

        public void SetShellMilitaryKind(object kind)
        {
            this.createTroop.ShellMilitaryKind = kind as MilitaryKind;
        }

        public void SetTabList(ITabList iTabList)
        {
            this.createTroop.TabListPlugin = iTabList;
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

        public object CreatingArchitecture
        {
            get
            {
                return this.createTroop.CreatingArchitecture;
            }
        }

        public int CreatingFood
        {
            get
            {
                return (this.createTroop.RationDays * this.createTroop.CreatingMilitary.FoodCostPerDay);
            }
        }

        public int Creatingzijin
        {
            get
            {
                return this.createTroop.zijin ;
            }
        }

        public object CreatingLeader
        {
            get
            {
                return this.createTroop.CreatingLeader;
            }
        }

        public object CreatingMilitary
        {
            get
            {
                return this.createTroop.CreatingMilitary;
            }
        }

        public object CreatingPersons
        {
            get
            {
                return this.createTroop.CreatingPersons;
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
                return this.createTroop.IsShowing;
            }
            set
            {
                this.createTroop.IsShowing = value;
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

