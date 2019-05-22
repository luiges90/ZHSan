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

namespace tupianwenziPlugin
{

    public class tupianwenziPlugin : GameObject, Itupianwenzi, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\tupianwenzi\Data\";
        private string description = "图片文字插件";
        
        private const string Path = @"Content\Textures\GameComponents\tupianwenzi\";
        public tupianwenzilei tupianwenzi = new tupianwenzilei();
        private string pluginName = "tupianwenziPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "tupianwenziData.xml";

        public void Close(Screen screen)
        {
            this.tupianwenzi.Close(screen);
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.tupianwenzi.IsShowing)
            {
                this.tupianwenzi.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
            this.tupianwenzi.iPersonTextDialog = this;
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Microsoft.Xna.Framework.Color color;
            Font font;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.tupianwenzi.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.tupianwenzi.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tupianwenzi.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.tupianwenzi.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.tupianwenzi.ClientPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.tupianwenzi.RichText.ClientWidth = this.tupianwenzi.ClientPosition.Width;
            this.tupianwenzi.RichText.ClientHeight = this.tupianwenzi.ClientPosition.Height;
            this.tupianwenzi.RichText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.tupianwenzi.RichText.Builder = font;
            //this.tupianwenzi.RichText.Builder.SetFreeTextBuilder(font);

            this.tupianwenzi.RichText.DefaultColor = color;
            this.tupianwenzi.BuildingRichText.ClientWidth = this.tupianwenzi.RichText.ClientWidth;
            this.tupianwenzi.BuildingRichText.ClientHeight = this.tupianwenzi.RichText.ClientHeight;
            this.tupianwenzi.BuildingRichText.RowMargin = this.tupianwenzi.RichText.RowMargin;

            this.tupianwenzi.BuildingRichText.Builder = font;
            //this.tupianwenzi.BuildingRichText.Builder.SetFreeTextBuilder(font);

            this.tupianwenzi.BuildingRichText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(3);
            this.tupianwenzi.FirstPageButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.tupianwenzi.FirstPageButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.tupianwenzi.FirstPageButtonDisabledTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.tupianwenzi.FirstPageButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(4);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.tupianwenzi.NameText = new FreeText(font, color);
            this.tupianwenzi.NameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.tupianwenzi.NameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            //node = nextSibling.ChildNodes.Item(5);
            //this.tupianwenzi.ShowingSeconds = int.Parse(node.Attributes.GetNamedItem("Time").Value);
            //this.tupianwenzi.ShowingSeconds = Session.GlobalVariables.DialogShowTime;
            this.tupianwenzi.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\tupianwenziTextTree.xml");

        }

        public void SetCloseFunction(GameDelegates.VoidFunction closeFunction)
        {
            this.tupianwenzi.CloseFunction += closeFunction;
        }

        public void SetConfirmationDialog(IConfirmationDialog iConfirmationDialog, GameDelegates.VoidFunction yesFunction, GameDelegates.VoidFunction noFunction)
        {
            this.tupianwenzi.iConfirmationDialog = iConfirmationDialog;
            this.tupianwenzi.YesFunction = yesFunction;
            this.tupianwenzi.NoFunction = noFunction;
            this.tupianwenzi.HasConfirmationDialog = true;
        }

        public void SetContextMenu(IGameContextMenu iContextMenu)
        {
            this.tupianwenzi.iContextMenu = iContextMenu;
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

        public void SetGameObjectBranch(object person, object gameObject, string branchName, string tupian, string shengyin ,string TryToShowString="")
        {
            string shijianshengyin;
            PlatformTexture shijiantupian;
            Microsoft.Xna.Framework.Rectangle shijiantupianjuxing;

            if (!(Session.Current.Scenario.SkyEyeSimpleNotification(gameObject as GameObject) && Session.GlobalVariables.SkyEye))
            {

                this.tupianwenzi.SetGameObjectBranch(person as GameObject, gameObject as GameObject, branchName, TryToShowString );

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
                        string[] files = Platform.Current.GetFiles(@"Content\Textures\GameComponents\tupianwenzi\Data\meinvtupian\" + tupian + "\\").NullToEmptyArray();
                        string suijitupianwenjianming = files[GameObject.Random(files.Length)];
                        shijiantupian = CacheManager.GetTempTexture(suijitupianwenjianming);
                    }
                    catch
                    {
                        try
                        {
                            string[] files = Platform.Current.GetFiles(@"Content\Textures\GameComponents\tupianwenzi\Data\meinvtupian\").NullToEmptyArray();

                            string suijitupianwenjianming = files[GameObject.Random(files.Length)];
                            shijiantupian = CacheManager.GetTempTexture(suijitupianwenjianming);
                        }
                        catch
                        {
                            shijiantupian = null;
                        }

                    }
                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 286, 400);

                }
                else if (branchName == "renwusiwang")
                {
                    //shijiantupian = ((this.tupianwenzi.screen.Scenario.Persons.GetGameObject(Convert.ToInt32(tupian))) as Person).Portrait ;
                    //shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 240, 240);

                    //shijiantupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\tupian\" + "renwusiwang.jpg");
                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 512, 384);
                    shijiantupian = null;
                }
                else
                {
                    if (!String.IsNullOrEmpty(tupian))
                    {
                        shijiantupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\tupian\" + tupian);
                    }
                    else
                    {
                        shijiantupian = null;
                    }

                    shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 512, 384);

                }

                this.tupianwenzi.shijiantupianduilie.Enqueue(shijiantupian);
                this.tupianwenzi.juxingduilie.Enqueue(shijiantupianjuxing);
                this.tupianwenzi.shijianshengyinduilie.Enqueue(shijianshengyin);
            }

        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\tupianwenziData.xml");
        }

        public void SetPosition(ShowPosition showPosition, Screen screen)
        {
            this.tupianwenzi.SetPosition(showPosition, screen);
        }

        public void SetScreen(Screen screen)
        {
            this.tupianwenzi.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (this.tupianwenzi.IsShowing)
            {
                this.tupianwenzi.Update();
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
                return this.tupianwenzi.IsShowing;
            }
            set
            {
                this.tupianwenzi.IsShowing = value;
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
                return this.tupianwenzi.RichText;
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

