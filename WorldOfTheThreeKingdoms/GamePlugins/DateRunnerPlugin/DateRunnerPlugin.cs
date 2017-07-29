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
using WorldOfTheThreeKingdoms.GameScreens;

namespace DateRunnerPlugin
{

    public class DateRunnerPlugin : GameObject, IDateRunner, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\DateRunner\Data\";
        private DateRunner dateRunner = new DateRunner();
        private string description = "日期进行工具";
        private const string Path = @"Content\Textures\GameComponents\DateRunner\";
        private string pluginName = "DateRunnerPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "DateRunnerData.xml";

        public void DateGo()
        {
            this.dateRunner.DateGo();
        }

        public void DateStop()
        {
            this.dateRunner.DateStop();
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            this.dateRunner.Draw();
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
            this.dateRunner.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.Align = (ToolAlign) Enum.Parse(typeof(ToolAlign), node.Attributes.GetNamedItem("Align").Value);
            this.dateRunner.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.dateRunner.UpperArrowTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.UpperArrowSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(2);
            this.dateRunner.LowerArrowTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.LowerArrowSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(3);
            this.dateRunner.PlayTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.PlaySelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(4);
            this.dateRunner.PauseTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.PauseSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(5);
            this.dateRunner.StopTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.dateRunner.StopSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\DateRunner\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(6);
            this.dateRunner.FirstDigitUpperArrowPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(7);
            this.dateRunner.FirstDigitLowerArrowPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(8);
            this.dateRunner.DaysToGoFirstDigitTextPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(9);
            this.dateRunner.SecondDigitUpperArrowPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(10);
            this.dateRunner.SecondDigitLowerArrowPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(11);
            this.dateRunner.DaysToGoSecondDigitTextPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(12);
            this.dateRunner.PlayPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(13);
            this.dateRunner.StopPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(14);
            this.dateRunner.DaysLeftTextPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(15);
            this.dateRunner.DaysToGo = int.Parse(node.Attributes.GetNamedItem("DefaultDays").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.dateRunner.DaysToGoTextBuilder = font;

            //this.dateRunner.DaysToGoTextBuilder.SetFreeTextBuilder(font);
            this.dateRunner.DaysToGoTextColor = color;

            StaticMethods.LoadFontAndColorFromXMLNode(nextSibling.ChildNodes.Item(0x10), out font, out color);

            this.dateRunner.DaysLeftTextBuilder = font;
            //this.dateRunner.DaysLeftTextBuilder.SetFreeTextBuilder(font);

            this.dateRunner.DaysLeftTextColor = color;
        }

        public void Pause()
        {
            this.dateRunner.Pause();
        }

        public void Reset()
        {
            this.dateRunner.Reset();
        }

        public void Run()
        {
            this.dateRunner.Run();
        }

        public void RunDays(int Days)
        {
            this.dateRunner.RunDays(Days);
        }

        public void SetGameDate(object gameDate)
        {
            this.dateRunner.Date = gameDate as GameDate;
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\DateRunnerData.xml");
            this.dateRunner.ResetDisplayTextures();
        }

        public void SetScreen(Screen screen)
        {
            this.dateRunner.Initialize(screen);
        }

        public void Stop()
        {
            this.dateRunner.Stop();
        }

        public void Update(GameTime gameTime)
        {
            this.dateRunner.Update();
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
                return this.dateRunner;
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

