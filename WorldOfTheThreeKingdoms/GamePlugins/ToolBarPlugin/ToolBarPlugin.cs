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

namespace ToolBarPlugin
{

    public class ToolBarPlugin : GameObject, IToolBar, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\ToolBar\Data\";
        private string description = "工具栏";
        private GraphicsDevice graphicsDevice;
        private const string Path = @"Content\Textures\GameComponents\ToolBar\";
        private string pluginName = "ToolBarPlugin";
        private ToolBar toolBar = new ToolBar();
        private string version = "1.0.0";
        private const string XMLFilename = "ToolBarData.xml";

        public WorldOfTheThreeKingdoms.GamePlugins.ToolBarPlugin.BackTool backTool = null;

        public void AddTool(object tool)
        {
            this.toolBar.Tools.Add(tool as Tool);
        }

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this.IsShowing)
            {
                this.toolBar.Draw(spriteBatch,gameTime);
            }
        }

        public void Initialize()
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node2 = nextSibling.ChildNodes.Item(0);
            this.toolBar.BackgroundTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ToolBar\Data\" + node2.Attributes.GetNamedItem("FileName").Value);
            this.toolBar.BackgroundHeight = int.Parse(node2.Attributes.GetNamedItem("Height").Value);
            node2 = nextSibling.ChildNodes.Item(1);
            this.toolBar.SpliterTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\ToolBar\Data\" + node2.Attributes.GetNamedItem("FileName").Value);
            this.toolBar.SpliterWidth = int.Parse(node2.Attributes.GetNamedItem("Width").Value);
            
            this.backTool = new WorldOfTheThreeKingdoms.GamePlugins.ToolBarPlugin.BackTool();
        }

        public void RemoveTool(object tool)
        {
            this.toolBar.Tools.Remove(tool as Tool);
        }

        public void SetContextMenuPlugin(IGameContextMenu contextMenuPlugin)
        {
            this.toolBar.ContextMenuPlugin = contextMenuPlugin;
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\ToolBarData.xml");
        }

        public void SetRealViewportSize(Point realViewportSize)
        {
            //this.toolBar.BackgroundPosition = new Rectangle(0, realViewportSize.Y - this.toolBar.BackgroundHeight, realViewportSize.X, this.toolBar.BackgroundHeight);

            this.toolBar.BackgroundPosition = new Rectangle(0, realViewportSize.Y, realViewportSize.X, this.toolBar.BackgroundHeight);

            this.toolBar.ResetToolsOffset();

            var rec = (this.toolBar.Tools[3] as GameSystemPlugin.GameSystem).SystemDisplayPosition;

            backTool.Position = new Vector2(rec.X + 40, rec.Y - 10);
        }

        public void SetScreen(object screen)
        {
            this.toolBar.Initialize(screen as Screen);
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsShowing)
            {
                this.toolBar.Update();
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

        public bool DrawTools
        {
            get
            {
                return this.toolBar.DrawTools;
            }
            set
            {
                this.toolBar.DrawTools = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.toolBar.Enabled;
            }
            set
            {
                this.toolBar.Enabled = value;
                this.toolBar.SetToolsEnabled(value);
            }
        }

        public int Height
        {
            get
            {
                return (this.IsShowing ? this.toolBar.BackgroundHeight : 0);
            }
            set
            {
                this.toolBar.BackgroundHeight = value;
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
                return this.toolBar.IsShowing;
            }
            set
            {
                this.toolBar.IsShowing = value;
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

