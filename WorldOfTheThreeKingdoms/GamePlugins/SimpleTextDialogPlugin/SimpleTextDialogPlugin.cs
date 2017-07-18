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

namespace SimpleTextDialogPlugin
{

    public class SimpleTextDialogPlugin : GameObject, ISimpleTextDialog, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\SimpleTextDialog\Data\";
        private string description = "简单文本对话框";
        private GraphicsDevice graphicsDevice;
        private const string Path = @"Content\Textures\GameComponents\SimpleTextDialog\";
        private string pluginName = "SimpleTextDialogPlugin";
        private SimpleTextDialog simpleTextDialog = new SimpleTextDialog();
        private string version = "1.0.0";
        private const string XMLFilename = "SimpleTextDialogData.xml";

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.simpleTextDialog.IsShowing)
            {
                this.simpleTextDialog.Draw(spriteBatch);
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
            this.simpleTextDialog.BackgroundTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\SimpleTextDialog\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.simpleTextDialog.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.simpleTextDialog.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.simpleTextDialog.ClientPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.simpleTextDialog.RichText.ClientWidth = this.simpleTextDialog.ClientPosition.Width;
            this.simpleTextDialog.RichText.ClientHeight = this.simpleTextDialog.ClientPosition.Height;
            this.simpleTextDialog.RichText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.simpleTextDialog.RichText.Builder = font; //.SetFreeTextBuilder(this.graphicsDevice, font);

            this.simpleTextDialog.RichText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(2);
            this.simpleTextDialog.FirstPageButtonTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\SimpleTextDialog\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.simpleTextDialog.FirstPageButtonSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\SimpleTextDialog\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.simpleTextDialog.FirstPageButtonDisabledTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\SimpleTextDialog\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.simpleTextDialog.FirstPageButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(3);
            this.simpleTextDialog.ShowingSeconds = int.Parse(node.Attributes.GetNamedItem("Time").Value);
            this.simpleTextDialog.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\SimpleTextTree.xml");
        }

        public void SetBranch(string branchName)
        {
            this.simpleTextDialog.RichText.SetGameObjectTextBranch(null, this.simpleTextDialog.TextTree.GetBranch(branchName));
        }

        public void SetConfirmationDialog(IConfirmationDialog iConfirmationDialog)
        {
            this.simpleTextDialog.iConfirmationDialog = iConfirmationDialog;
        }

        public void SetGameObjectBranch(object gameObject, string branchName)
        {
            this.simpleTextDialog.RichText.SetGameObjectTextBranch(gameObject as GameObject, this.simpleTextDialog.TextTree.GetBranch(branchName));
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\SimpleTextDialogData.xml");
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.simpleTextDialog.SetPosition(showPosition);
        }

        public void SetScreen(object screen)
        {
            this.simpleTextDialog.Initialize(screen as Screen);
        }

        public void Update(GameTime gameTime)
        {
            if (this.simpleTextDialog.IsShowing)
            {
                this.simpleTextDialog.Update();
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
                return this.simpleTextDialog.IsShowing;
            }
            set
            {
                this.simpleTextDialog.IsShowing = value;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public FreeRichText RichText
        {
            get
            {
                return this.simpleTextDialog.RichText;
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

