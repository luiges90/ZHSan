using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
//using System.Drawing;
using System.Xml;

namespace CommentTextPlugin
{

    public class CommentTextPlugin : GameObject, IConmentText, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private CommentText conmentText = new CommentText();
        private const string DataPath = @"Content\Textures\GameComponents\CommentText\Data\";
        private string description = "用来设置主界面注释的各个属性";
        private const string Path = @"Content\Textures\GameComponents\CommentText\";
        private string pluginName = "CommentTextPlugin";
        private float ScaleHeight;
        private float ScaleWidth;
        private float ScaleX;
        private float ScaleY;
        private string version = "1.0.0";
        private const string XMLFilename = "CommentTextData.xml";

        public string BuildFirstText(string text, bool decoration)
        {
            this.conmentText.DecorateFirst = decoration;
            return this.conmentText.BuildFirstString(text);
        }

        public string BuildSecondText(string text, bool decoration)
        {
            this.conmentText.DecorateSecond = decoration;
            return this.conmentText.BuildSecondString(text);
        }

        public string BuildThirdText(string text, bool decoration)
        {
            this.conmentText.DecorateThird = decoration;
            return this.conmentText.BuildThirdString(text);
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            this.conmentText.Draw();
        }

        public void Initialize(Screen screen)
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
            this.ScaleX = float.Parse(node.Attributes.GetNamedItem("ScaleX").Value);
            this.ScaleY = float.Parse(node.Attributes.GetNamedItem("ScaleY").Value);
            this.ScaleWidth = float.Parse(node.Attributes.GetNamedItem("ScaleWidth").Value);
            this.ScaleHeight = float.Parse(node.Attributes.GetNamedItem("ScaleHeight").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.conmentText.FirstTextDecorationLeft = node.Attributes.GetNamedItem("Left").Value;
            this.conmentText.FirstTextDecorationRight = node.Attributes.GetNamedItem("Right").Value;
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.conmentText.FirstText = new FreeText(font, color);
            this.conmentText.FirstText.Align = TextAlign.Left;
            node = nextSibling.ChildNodes.Item(2);
            this.conmentText.SecondTextDecorationLeft = node.Attributes.GetNamedItem("Left").Value;
            this.conmentText.SecondTextDecorationRight = node.Attributes.GetNamedItem("Right").Value;
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.conmentText.SecondText = new FreeText(font, color);
            this.conmentText.SecondText.Align = TextAlign.Left;
            node = nextSibling.ChildNodes.Item(3);
            this.conmentText.ThirdTextDecorationLeft = node.Attributes.GetNamedItem("Left").Value;
            this.conmentText.ThirdTextDecorationRight = node.Attributes.GetNamedItem("Right").Value;
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.conmentText.ThirdText = new FreeText(font, color);
            this.conmentText.ThirdText.Align = TextAlign.Left;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\CommentTextData.xml");
        }

        public void SetView(int width, int height)
        {
            this.conmentText.Position = new Microsoft.Xna.Framework.Rectangle((int) (width * this.ScaleX), (int) (height * this.ScaleY), (int) (width * this.ScaleWidth), (int) (height * this.ScaleHeight));
        }

        public void Update(GameTime gameTime)
        {
            this.conmentText.Update();
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

        public string Version
        {
            get
            {
                return this.version;
            }
        }
    }
}

