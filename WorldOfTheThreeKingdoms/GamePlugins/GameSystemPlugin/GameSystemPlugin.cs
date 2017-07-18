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


namespace GameSystemPlugin
{

    public class GameSystemPlugin : GameObject, IGameSystem, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\GameSystem\Data\";
        private string description = "系统工具";
        public GameSystem gameSystem = new GameSystem();
        private GraphicsDevice graphicsDevice;
        private const string Path = @"Content\Textures\GameComponents\GameSystem\";
        private string pluginName = "GameSystemPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "GameSystemData.xml";

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.gameSystem.Draw(spriteBatch);
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
            this.gameSystem.Align = (ToolAlign) Enum.Parse(typeof(ToolAlign), node.Attributes.GetNamedItem("Align").Value);
            this.gameSystem.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.gameSystem.SystemTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameSystem\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.gameSystem.SystemSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameSystem\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.gameSystem.SystemDisplayTexture = this.gameSystem.SystemTexture;
            this.gameSystem.SystemPosition = StaticMethods.LoadRectangleFromXMLNode(node);
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\GameSystemData.xml");
        }

        public void SetOptionDialog(IOptionDialog iOptionDialog)
        {
            this.gameSystem.SetOptionDialog(iOptionDialog);
        }

        public void SetScreen(object screen)
        {
            this.gameSystem.Initialize(screen as Screen);
        }

        public void ShowOptionDialog(ShowPosition showPosition)
        {
            this.gameSystem.ShowOptionDialog(showPosition);
        }

        public void Update(GameTime gameTime)
        {
            this.gameSystem.Update();
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
                return this.gameSystem;
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

