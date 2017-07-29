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

namespace MapViewSelectorPlugin
{

    public class MapViewSelectorPlugin : GameObject, IMapViewSelector, IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\MapViewSelector\Data\";
        private string description = "地图视角选择器";
        
        private MapViewSelector mapViewSelector = new MapViewSelector();
        private const string Path = @"Content\Textures\GameComponents\MapViewSelector\";
        private string pluginName = "MapViewSelectorPlugin";
        private string version = "1.0.1";
        private const string XMLFilename = "MapViewSelectorData.xml";

        public void AddDisableRects()
        {
            this.mapViewSelector.AddDisableRects();
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.IsShowing)
            {
                this.mapViewSelector.Draw();
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
            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node2 = nextSibling.ChildNodes.Item(0);
            this.mapViewSelector.BackgroundSize.X = int.Parse(node2.Attributes.GetNamedItem("Width").Value);
            this.mapViewSelector.BackgroundSize.Y = int.Parse(node2.Attributes.GetNamedItem("Height").Value);
            node2 = nextSibling.ChildNodes.Item(1);
            this.mapViewSelector.TitleTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\MapViewSelector\Data\" + node2.Attributes.GetNamedItem("FileName").Value);
            this.mapViewSelector.TitleSize.X = int.Parse(node2.Attributes.GetNamedItem("Width").Value);
            this.mapViewSelector.TitleSize.Y = int.Parse(node2.Attributes.GetNamedItem("Height").Value);
            node2 = nextSibling.ChildNodes.Item(2);
            this.mapViewSelector.ButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\MapViewSelector\Data\" + node2.Attributes.GetNamedItem("FileName").Value);
            this.mapViewSelector.ButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\MapViewSelector\Data\" + node2.Attributes.GetNamedItem("Selected").Value);
            node2 = nextSibling.ChildNodes.Item(3);
            XmlNode node = node2.ChildNodes.Item(0);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.mapViewSelector.ReturnToListButtonText = new FreeText(font, color);
            this.mapViewSelector.ReturnToListButtonText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.mapViewSelector.ReturnToListButtonText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.mapViewSelector.ReturnToListButtonText.Text = node.Attributes.GetNamedItem("Label").Value;
            node = node2.ChildNodes.Item(1);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.mapViewSelector.OKButtonText = new FreeText(font, color);
            this.mapViewSelector.OKButtonText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.mapViewSelector.OKButtonText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.mapViewSelector.OKButtonText.Text = node.Attributes.GetNamedItem("Label").Value;
            node = node2.ChildNodes.Item(2);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.mapViewSelector.CancelButtonText = new FreeText(font, color);
            this.mapViewSelector.CancelButtonText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.mapViewSelector.CancelButtonText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.mapViewSelector.CancelButtonText.Text = node.Attributes.GetNamedItem("Label").Value;
            node2 = nextSibling.ChildNodes.Item(4);
            this.mapViewSelector.ItemInListTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\MapViewSelector\Data\" + node2.Attributes.GetNamedItem("InList").Value);
            this.mapViewSelector.ItemSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\MapViewSelector\Data\" + node2.Attributes.GetNamedItem("Selected").Value);
        }

        public void RemoveDisableRects()
        {
            this.mapViewSelector.RemoveDisableRects();
        }

        public void SetGameFrame(IGameFrame iGameFrame)
        {
            this.mapViewSelector.iGameFrame = iGameFrame;
        }

        public void SetGameObjectList(object gameObjectList)
        {
            this.mapViewSelector.SelectingGameObjectList = gameObjectList as GameObjectList;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\MapViewSelectorData.xml");
        }

        public void SetMapPosition(ShowPosition showPosition)
        {
            this.mapViewSelector.SetDisplayOffset(showPosition);
        }

        public void SetMultiSelecting(bool multiSelecting)
        {
            this.mapViewSelector.MultiSelecting = multiSelecting;
        }

        public void SetOKFunction(GameDelegates.VoidFunction function)
        {
            this.mapViewSelector.OKFunction = function;
        }

        public void SetScreen(Screen screen)
        {
            this.mapViewSelector.Initialize();
        }

        public void SetTabList(ITabList iTabList)
        {
            this.mapViewSelector.iTabList = iTabList;
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsShowing)
            {
                this.mapViewSelector.Update();
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

        public bool IsShowing
        {
            get
            {
                return this.mapViewSelector.IsShowing;
            }
            set
            {
                this.mapViewSelector.IsShowing = value;
            }
        }

        public MapViewSelectorKind Kind
        {
            get
            {
                return this.mapViewSelector.Kind;
            }
            set
            {
                this.mapViewSelector.Kind = value;
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

