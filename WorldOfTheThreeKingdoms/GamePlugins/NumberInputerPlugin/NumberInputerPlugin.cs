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

namespace NumberInputerPlugin
{

    public class NumberInputerPlugin : GameObject, INumberInputer, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\NumberInputer\Data\";
        private string description = "数字输入器";
        
        private NumberInputer numberInputer = new NumberInputer();
        private const string Path = @"Content\Textures\GameComponents\NumberInputer\";
        private string pluginName = "NumberInputerPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "NumberInputerData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.IsShowing)
            {
                this.numberInputer.Draw();
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Microsoft.Xna.Framework.Color color;
            Font font;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);
            document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.numberInputer.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.numberInputer.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.numberInputer.RangeText = new FreeText(font, color);
            this.numberInputer.RangeText.Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.numberInputer.RangeText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(2);
            this.numberInputer.FrameTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.FramePosition = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.numberInputer.FrameText = new FreeText(font, color);
            this.numberInputer.FrameText.Position = this.numberInputer.FramePosition;
            this.numberInputer.FrameText.Align = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(3);
            foreach (XmlNode node3 in node.ChildNodes)
            {
                Number item = new Number {
                    Position = StaticMethods.LoadRectangleFromXMLNode(node3),
                    Num = int.Parse(node3.Attributes.GetNamedItem("Num").Value),
                    Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node3.Attributes.GetNamedItem("FileName").Value)
                };
                this.numberInputer.Numbers.Add(item);
            }
            node = nextSibling.ChildNodes.Item(4);
            this.numberInputer.ClearTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.ClearPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(5);
            this.numberInputer.EnterTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.EnterPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(6);
            this.numberInputer.BackspaceTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.BackspacePosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(7);
            this.numberInputer.MaxTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.MaxPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(8);
            this.numberInputer.ExitTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.ExitPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(9);
            this.numberInputer.SelectionTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            //阿柒:万字按钮相关
            node = nextSibling.ChildNodes.Item(10);
            this.numberInputer.TenThousandTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\NumberInputer\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.numberInputer.TenThousandPosition = StaticMethods.LoadRectangleFromXMLNode(node);
        }

        public void SetDepthOffset(float offset)
        {
            this.numberInputer.DepthOffset = offset;
        }

        public void SetEnterFunction(GameDelegates.VoidFunction function)
        {
            this.numberInputer.EnterFunction = function;
        }

        public void SetUnit(string unit)
        {
            this.numberInputer.unit = unit;
        }

        public void SetScale(int scale)
        {
            this.numberInputer.Scale = scale;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\NumberInputerData.xml");
        }

        public void SetMapPosition(ShowPosition showPosition)
        {
            this.numberInputer.SetDisplayOffset(showPosition);
        }

        public void SetMax(int max)
        {
            this.numberInputer.Max = max / Scale;
        }

        public void SetScreen(Screen screen)
        {
            this.numberInputer.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsShowing)
            {
                this.numberInputer.Update();
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
                return this.numberInputer.IsShowing;
            }
            set
            {
                this.numberInputer.IsShowing = value;
            }
        }

#pragma warning disable CS0108 // 'NumberInputerPlugin.Scale' hides inherited member 'GameObject.Scale'. Use the new keyword if hiding was intended.
        public int Scale
#pragma warning restore CS0108 // 'NumberInputerPlugin.Scale' hides inherited member 'GameObject.Scale'. Use the new keyword if hiding was intended.
        {
            get
            {
                return this.numberInputer.Scale;
            }
        }

        public int Number
        {
            get
            {
                return this.numberInputer.Num * Scale;
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

