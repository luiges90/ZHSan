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
        private GraphicsDevice graphicsDevice;
        private const string Path = @"Content\Textures\GameComponents\tupianwenzi\";
        private tupianwenzilei tupianwenzi = new tupianwenzilei();
        private string pluginName = "tupianwenziPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "tupianwenziData.xml";

        public void Close()
        {
            this.tupianwenzi.Close();
        }

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.tupianwenzi.IsShowing)
            {
                this.tupianwenzi.Draw(spriteBatch);
            }
        }

        public void Initialize()
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
            this.tupianwenzi.BackgroundTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("FileName").Value);
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
            //this.tupianwenzi.RichText.Builder.SetFreeTextBuilder(this.graphicsDevice, font);

            this.tupianwenzi.RichText.DefaultColor = color;
            this.tupianwenzi.BuildingRichText.ClientWidth = this.tupianwenzi.RichText.ClientWidth;
            this.tupianwenzi.BuildingRichText.ClientHeight = this.tupianwenzi.RichText.ClientHeight;
            this.tupianwenzi.BuildingRichText.RowMargin = this.tupianwenzi.RichText.RowMargin;

            this.tupianwenzi.BuildingRichText.Builder = font;
            //this.tupianwenzi.BuildingRichText.Builder.SetFreeTextBuilder(this.graphicsDevice, font);

            this.tupianwenzi.BuildingRichText.DefaultColor = color;
            node = nextSibling.ChildNodes.Item(3);
            this.tupianwenzi.FirstPageButtonTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.tupianwenzi.FirstPageButtonSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            this.tupianwenzi.FirstPageButtonDisabledTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\" + node.Attributes.GetNamedItem("Disabled").Value);
            this.tupianwenzi.FirstPageButtonPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(4);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.tupianwenzi.NameText = new FreeText(this.graphicsDevice, font, color);
            this.tupianwenzi.NameText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.tupianwenzi.NameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            //node = nextSibling.ChildNodes.Item(5);
            //this.tupianwenzi.ShowingSeconds = int.Parse(node.Attributes.GetNamedItem("Time").Value);
            //this.tupianwenzi.ShowingSeconds = GlobalVariables.DialogShowTime;
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

            List<String> msg = p.Scenario.GameCommonData.AllTextMessages.GetTextMessage(p.ID, k);
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
            Texture2D shijiantupian;
            Microsoft.Xna.Framework.Rectangle shijiantupianjuxing;

            this.tupianwenzi.SetGameObjectBranch(person as GameObject , gameObject as GameObject, branchName);

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
                if (!String.IsNullOrEmpty(tupian))
                {
                    shijiantupian = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\meinvtupian\" + tupian);
                }
                else
                {
                    try
                    {
                        string[] files = Platform.Current.GetFiles(@"Content\Textures\GameComponents\tupianwenzi\Data\meinvtupian\").NullToEmptyList().Where(fi => fi.StartsWith("B") && fi.EndsWith(".jpg")).NullToEmptyArray();

                        string suijitupianwenjianming = files[GameObject.Random(files.Length)];
                        shijiantupian = CacheManager.LoadTempTexture(suijitupianwenjianming);
                    }
                    catch
                    {
                        // this should not happen, hmm...
                        shijiantupian = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\meinvtupian\B0.jpg");
                    }

                    
                }
                shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 286,400);

            }
            else if (branchName == "renwusiwang")
            {
                //shijiantupian = ((this.tupianwenzi.screen.Scenario.Persons.GetGameObject(Convert.ToInt32(tupian))) as Person).Portrait ;
                //shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 240, 240);

                //shijiantupian = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\tupian\" + "renwusiwang.jpg");
                shijiantupianjuxing = new Microsoft.Xna.Framework.Rectangle(0, 0, 512, 384);
                shijiantupian = null;
            }
            else 
            {
                if (!String.IsNullOrEmpty(tupian))
                {
                    shijiantupian = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\tupianwenzi\Data\tupian\" + tupian);
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

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.graphicsDevice = device;
            this.tupianwenzi.graphicsDevice = device;

            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\tupianwenziData.xml");
        }

        public void SetPosition(ShowPosition showPosition)
        {
            this.tupianwenzi.SetPosition(showPosition);
        }

        public void SetScreen(object screen)
        {
            this.tupianwenzi.Initialize(screen as Screen);
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

