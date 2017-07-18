using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
using System.Xml;
using WorldOfTheThreeKingdoms;

namespace MapLayerPlugin
{

    public class MapLayerPlugin : GameObject, IMapLayer, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\MapLayer\Data\";
        private string description = "地图层次";
        private GraphicsDevice graphicsDevice;
        private MapLayer mapLayer = new MapLayer();
        private const string Path = @"Content\Textures\GameComponents\MapLayer\";
        private string pluginName = "MapLayerPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "MapLayerData.xml";

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.mapLayer.Draw(spriteBatch);
        }

        public void Initialize()
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.mapLayer.Align = (ToolAlign) Enum.Parse(typeof(ToolAlign), node.Attributes.GetNamedItem("Align").Value);
            this.mapLayer.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.mapLayer.NormalLayerTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\MapLayer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.mapLayer.NormalLayerActiveTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\MapLayer\Data\" + node.Attributes.GetNamedItem("Active").Value);
            this.mapLayer.NormalLayerPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.mapLayer.RoutewayLayerTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\MapLayer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.mapLayer.RoutewayLayerActiveTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\MapLayer\Data\" + node.Attributes.GetNamedItem("Active").Value);
            this.mapLayer.RoutewayLayerPosition = StaticMethods.LoadRectangleFromXMLNode(node);
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\MapLayerData.xml");
        }

        public void SetScreen(object screen)
        {
            this.mapLayer.Initialize(screen as Screen);
        }

        public void Update(GameTime gameTime)
        {
            this.mapLayer.Update();
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

        public object ToolInstance
        {
            get
            {
                return this.mapLayer;
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

