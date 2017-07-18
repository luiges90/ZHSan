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


namespace GameRecordPlugin
{

    public class GameRecordPlugin : GameObject, IGameRecord, IBasePlugin, IPluginXML, IPluginGraphics, IScreenDisableRects
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\GameRecord\Data\";
        private string description = "游戏记录";
        private GameRecord gameRecord = new GameRecord();
        private GraphicsDevice graphicsDevice;
        private const string Path = @"Content\Textures\GameComponents\GameRecord\";
        private string pluginName = "GameRecordPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "GameRecordData.xml";

        public void AddBranch(object gameObject, string branchName, Microsoft.Xna.Framework.Point position)
        {
            this.gameRecord.AddBranch(gameObject as GameObject, branchName, position);
           // this.gameRecord.AddBranch1(gameObject as GameObject, branchName, position);
        }

        public void AddDisableRects()
        {
            this.gameRecord.AddDisableRects();
        }

        public void Clear()
        {
            this.gameRecord.Clear();
        }

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.gameRecord.Draw(spriteBatch);
        }

        public void Initialize()
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
            this.gameRecord.Align = (ToolAlign) Enum.Parse(typeof(ToolAlign), node.Attributes.GetNamedItem("Align").Value);
            this.gameRecord.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.gameRecord.ToolTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameRecord\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.gameRecord.ToolSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameRecord\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.gameRecord.ToolDisplayTexture = this.gameRecord.ToolTexture;
            this.gameRecord.ToolClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.gameRecord.PopSoundFile = @"Content\Sound\" + node.Attributes.GetNamedItem("PopSoundFile").Value;
            this.gameRecord.RecordBackgroundTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameRecord\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.gameRecord.RecordBackgroundClient = StaticMethods.LoadRectangleFromXMLNode(node);
            this.gameRecord.RecordShowPosition = (ShowPosition) Enum.Parse(typeof(ShowPosition), node.Attributes.GetNamedItem("Position").Value);
            node = nextSibling.ChildNodes.Item(3);
            this.gameRecord.Record.ClientWidth = this.gameRecord.RecordBackgroundClient.Width;
            this.gameRecord.Record.ClientHeight = 600;
            this.gameRecord.Record.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.gameRecord.Record.Builder = font;  //.SetFreeTextBuilder(this.graphicsDevice, font);
            this.gameRecord.Record.DefaultColor = color;
            this.gameRecord.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\RecordTextTree.xml");
            node = nextSibling.ChildNodes.Item(4);
            this.gameRecord.Tool1Texture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameRecord\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.gameRecord.Tool1SelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\GameRecord\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.gameRecord.Tool1DisplayTexture = this.gameRecord.Tool1Texture;
            this.gameRecord.Tool1Client = StaticMethods.LoadRectangleFromXMLNode(node);
        }

        public void RemoveDisableRects()
        {
            this.gameRecord.RemoveDisableRects();
        }

        public void ResetRecordShowPosition()
        {
            this.gameRecord.SetDisplayOffset(this.gameRecord.RecordShowPosition);
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\GameRecordData.xml");
        }

        public void SetScreen(object screen)
        {
            this.gameRecord.Initialize(screen as Screen);
        }

        public void Update(GameTime gameTime)
        {
            this.gameRecord.Update();
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

        public bool IsRecordShowing
        {
            get
            {
                return this.gameRecord.IsRecordShowing;
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
                return this.gameRecord;
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

