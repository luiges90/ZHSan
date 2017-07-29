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

namespace ConfirmationDialogPlugin
{

    public class ConfirmationDialogPlugin : GameObject, IConfirmationDialog, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private ConfirmationDialog confirmationDialog = new ConfirmationDialog();
        private const string DataPath = @"Content\Textures\GameComponents\ConfirmationDialog\Data\";
        private string description = "确认对话框";
        private const string Path = @"Content\Textures\GameComponents\ConfirmationDialog\";
        private string pluginName = "ConfirmationDialogPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "ConfirmationDialogData.xml";

        public void AddNoFunction(GameDelegates.VoidFunction noFunction)
        {
            this.confirmationDialog.NoFunction = noFunction;
        }

        public void AddYesFunction(GameDelegates.VoidFunction yesFunction)
        {
            this.confirmationDialog.YesFunction = yesFunction;
        }

        public void ClearFunctions()
        {
            this.confirmationDialog.YesFunction = null;
            this.confirmationDialog.NoFunction = null;
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.confirmationDialog.IsShowing)
            {
                this.confirmationDialog.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            XmlDocument document = new XmlDocument();

            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.confirmationDialog.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ConfirmationDialog\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.confirmationDialog.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.confirmationDialog.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.confirmationDialog.YesTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ConfirmationDialog\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.confirmationDialog.YesSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ConfirmationDialog\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.confirmationDialog.YesPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.confirmationDialog.YesSoundFile = @"Content\Sound\" + node.Attributes.GetNamedItem("SoundFile").Value;
            node = nextSibling.ChildNodes.Item(2);
            this.confirmationDialog.NoTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ConfirmationDialog\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.confirmationDialog.NoSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\ConfirmationDialog\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.confirmationDialog.NoPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.confirmationDialog.NoSoundFile = @"Content\Sound\" + node.Attributes.GetNamedItem("SoundFile").Value;
        }

        public void SetDescriptionText(string Text)
        {
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\ConfirmationDialogData.xml");
        }

        public void SetPersonTextDialog(Itupianwenzi iPersonTextDialog)
        {
            this.confirmationDialog.SetPersonTextDialog(iPersonTextDialog);
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.confirmationDialog.SetPosition(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.confirmationDialog.Initialize();
        }

        public void SetSimpleTextDialog(ISimpleTextDialog iSimpleTextDialog)
        {
            this.confirmationDialog.SetSimpleTextDialog(iSimpleTextDialog);
            iSimpleTextDialog.SetConfirmationDialog(this);
        }

        public void Update(GameTime gameTime)
        {
            if (this.confirmationDialog.IsShowing)
            {
                this.confirmationDialog.Update();
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
                return this.confirmationDialog.IsShowing;
            }
            set
            {
                this.confirmationDialog.IsShowing = value;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public DialogResult Result
        {
            get
            {
                return this.confirmationDialog.Result;
            }
            set
            {
                this.confirmationDialog.Result = value;
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

