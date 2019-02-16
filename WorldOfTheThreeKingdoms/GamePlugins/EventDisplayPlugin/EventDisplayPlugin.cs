using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.PersonDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
////using System.Drawing;
using System.Xml;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using WorldOfTheThreeKingdoms;
using Platforms;
using GameManager;
using Tools;

namespace EventDisplayPlugin
{

    public class EventDisplayPlugin : GameObject, Itupianwenzi, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\EventDisplay\Data\";
        private string description = "图片文字插件";
        
        private const string Path = @"Content\Textures\GameComponents\EventDisplay\";
        public tupianwenzilei EventDisplay = new tupianwenzilei();
        private string pluginName = "EventDisplayPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "EventDisplayData.xml";

        public void Close(Screen screen)
        {
            this.EventDisplay.Close(screen);
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.EventDisplay.IsShowing)
            {
                this.EventDisplay.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
            this.EventDisplay.iPersonTextDialog = this;
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Microsoft.Xna.Framework.Color color;
            Font font;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.EventDisplay.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.EventDisplay.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.EventDisplay.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.EventDisplay.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.EventDisplay.ClientPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.EventDisplay.RichText.ClientWidth = this.EventDisplay.ClientPosition.Width;
            this.EventDisplay.RichText.ClientHeight = this.EventDisplay.ClientPosition.Height;
            this.EventDisplay.RichText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.EventDisplay.RichText.Builder = font;
            //this.EventDisplay.RichText.Builder.SetFreeTextBuilder(font);

            this.EventDisplay.RichText.DefaultColor = color;
            this.EventDisplay.BuildingRichText.ClientWidth = this.EventDisplay.RichText.ClientWidth;
            this.EventDisplay.BuildingRichText.ClientHeight = this.EventDisplay.RichText.ClientHeight;
            this.EventDisplay.BuildingRichText.RowMargin = this.EventDisplay.RichText.RowMargin;

            this.EventDisplay.BuildingRichText.Builder = font;
            //this.EventDisplay.BuildingRichText.Builder.SetFreeTextBuilder(font);

            this.EventDisplay.BuildingRichText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(3);
            this.EventDisplay.FirstPageButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.EventDisplay.FirstPageButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.EventDisplay.FirstPageButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.EventDisplay.FirstPageButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(4);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.EventDisplay.NameText = new FreeText(font, color);
            this.EventDisplay.NameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.EventDisplay.NameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            //node = nextSibling.ChildNodes.Item(5);
            //this.EventDisplay.ShowingSeconds = int.Parse(node.Attributes.GetNamedItem("Time").Value);
            //this.EventDisplay.ShowingSeconds = Session.GlobalVariables.DialogShowTime;
            this.EventDisplay.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\EventDisplayTextTree.xml");

        }

        public void SetCloseFunction(GameDelegates.VoidFunction closeFunction)
        {
            this.EventDisplay.CloseFunction += closeFunction;
        }

        public void SetConfirmationDialog(IConfirmationDialog iConfirmationDialog, GameDelegates.VoidFunction yesFunction, GameDelegates.VoidFunction noFunction)
        {
            this.EventDisplay.iConfirmationDialog = iConfirmationDialog;
            this.EventDisplay.YesFunction = yesFunction;
            this.EventDisplay.NoFunction = noFunction;
            this.EventDisplay.HasConfirmationDialog = true;
        }

        public void SetContextMenu(IGameContextMenu iContextMenu)
        {
            this.EventDisplay.iContextMenu = iContextMenu;
        }

        public void SetGameObjectBranch(object person, object gameObject, string branchName)
        {
            SetGameObjectBranch(person, gameObject, branchName, "", "");
        }

        public void SetGameObjectBranch(object person, object gameObject, Enum kind, string branchName)
        {
            SetGameObjectBranch(person, gameObject, kind, branchName, "", "");
        }

        public void SetGameObjectBranch(object person, object gameObject, Enum kind, string branchName, string tupian, string shengyin)
        {
            GameObject p = (GameObject) person;
            TextMessageKind k = (TextMessageKind) kind;

            List<String> msg = Session.Current.Scenario.GameCommonData.AllTextMessages.GetTextMessage(p.ID, k);
            if (msg.Count > 0)
            {
                SetGameObjectBranch(p, null, msg[GameObject.Random(msg.Count)], tupian, shengyin);
            }
            else
            {
                SetGameObjectBranch(p, gameObject, branchName, tupian, shengyin);
            }
        }

        public void SetGameObjectBranch(object person, object gameObject, string branchName, string tupian, string shengyin)
        {
            string shijianshengyin;
            PlatformTexture shijiantupian;
            Microsoft.Xna.Framework.Rectangle shijiantupianjuxing;

            if (!(Session.Current.Scenario.SkyEyeSimpleNotification(gameObject as GameObject) && Session.GlobalVariables.SkyEye))
            {

                this.EventDisplay.SetGameObjectBranch(person as GameObject, gameObject as GameObject, branchName);

                if (shengyin != "")
                {
                    shijianshengyin = @"Content\Sound\Yinxiao\" + shengyin;
                }
                else
                {
                    shijianshengyin = null;

                }

                if (branchName == "chongxing")
                {
                    try
                    {
                        string[] files = Platform.Current.GetFiles(@"Content\Textures\GameComponents\EventDisplay\Data\BeautyAvatars\" + tupian + "\\").NullToEmptyArray();
                        string suijitupianwenjianming = files[GameObject.Random(files.Length)];
                        shijiantupian = CacheManager.GetTempTexture(suijitupianwenjianming);
                    }
                    catch
                    {
                        try
                        {
                            string[] files = Platform.Current.GetFiles(@"Content\Textures\GameComponents\EventDisplay\Data\BeautyAvatars\").NullToEmptyArray();

                            string suijitupianwenjianming = files[GameObject.Random(files.Length)];
                            shijiantupian = CacheManager.GetTempTexture(suijitupianwenjianming);
                        }
                        catch
                        {
                            // this should not happen, hmm...
                            shijiantupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\BeautyAvatars\B0.jpg");
                        }

                    }
                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 286, 400);

                }
                else if (branchName == "renwusiwang")
                {
                    //shijiantupian = ((this.EventDisplay.screen.Scenario.Persons.GetGameObject(Convert.ToInt32(tupian))) as Person).Portrait ;
                    //shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 240, 240);

                    //shijiantupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\tupian\" + "renwusiwang.jpg");
                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 512, 384);
                    shijiantupian = null;
                }
                else
                {
                    if (!String.IsNullOrEmpty(tupian))
                    {
                        shijiantupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\EventDisplay\Data\tupian\" + tupian);
                    }
                    else
                    {
                        shijiantupian = null;
                    }

                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 512, 384);

                }

                this.EventDisplay.shijiantupianduilie.Enqueue(shijiantupian);
                this.EventDisplay.juxingduilie.Enqueue(shijiantupianjuxing);
                this.EventDisplay.shijianshengyinduilie.Enqueue(shijianshengyin);
            }

        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\EventDisplayData.xml");
        }

        public void SetPosition(ShowPosition showPosition, Screen screen)
        {
            this.EventDisplay.SetPosition(showPosition, screen);
        }

        public void SetScreen(Screen screen)
        {
            this.EventDisplay.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (this.EventDisplay.IsShowing)
            {
                this.EventDisplay.Update();
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
                return this.EventDisplay.IsShowing;
            }
            set
            {
                this.EventDisplay.IsShowing = value;
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
                return this.EventDisplay.RichText;
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

