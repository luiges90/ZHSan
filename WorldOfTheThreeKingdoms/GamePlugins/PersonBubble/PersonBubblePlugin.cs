using GameGlobal;
using GameObjects;
using GameObjects.PersonDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using PluginInterface.BaseInterface;
using System;
//using System.Drawing;
using System.Xml;
using System.Collections.Generic;
using WorldOfTheThreeKingdoms;
using Platforms;
using GameManager;

namespace PersonBubble
{

    public class PersonBubblePlugin : GameObject, IPersonBubble, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\PersonBubble\Data\";
        private string description = "人物气泡";
        
        private const string Path = @"Content\Textures\GameComponents\PersonBubble\";
        private PersonBubble personBubble = new PersonBubble();
        private string pluginName = "PersonBubblePlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "PersonBubbleData.xml";

        public void AddPerson(object person, Microsoft.Xna.Framework.Point position, string branchName)
        {
            this.personBubble.AddPerson(person as Person, position, branchName);
        }

        public void AddPersonText(object person, Microsoft.Xna.Framework.Point position, string text)
        {
            this.personBubble.AddPersonText(person as Person, position, text);
        }

        public void AddPerson(object person, Microsoft.Xna.Framework.Point position, Enum kind, string fallback)
        {
            Person p = (Person)person;
            TextMessageKind k = (TextMessageKind)kind;
            List<string> msg = Session.Current.Scenario.GameCommonData.AllTextMessages.GetTextMessage(p.ID, k);
            if (msg.Count > 0)
            {
                this.AddPersonText(person, position, msg[GameObject.Random(msg.Count)]);
            }
            else
            {
                this.AddPerson(person, position, fallback);
            }
        }

        public void Dispose()
        {
        }

        public void Draw()
        {
            if (this.personBubble.IsShowing)
            {
                this.personBubble.Draw();
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
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.personBubble.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\PersonBubble\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.personBubble.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.personBubble.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.personBubble.PopoutOffset.X = int.Parse(node.Attributes.GetNamedItem("PopoutX").Value);
            this.personBubble.PopoutOffset.Y = int.Parse(node.Attributes.GetNamedItem("PopoutY").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.personBubble.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.personBubble.ClientPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            this.personBubble.TextClientWidth = this.personBubble.ClientPosition.Width;
            this.personBubble.TextClientHeight = this.personBubble.ClientPosition.Height;
            this.personBubble.TextRowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.personBubble.TextBuilder.SetFreeTextBuilder(font);

            this.personBubble.DefaultTextColor = color;
            node = nextSibling.ChildNodes.Item(3);
            this.personBubble.PersonSpecialTextTimeLast = int.Parse(node.Attributes.GetNamedItem("TimeLast").Value);
            color = StaticMethods.LoadColor(node.Attributes.GetNamedItem("Color").Value);
            this.personBubble.PersonSpecialTextColor = color;
            this.personBubble.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\PersonBubbleTree.xml");
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\PersonBubbleData.xml");
        }

        public void SetScreen(Screen screen)
        {
            this.personBubble.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (this.personBubble.IsShowing)
            {
                this.personBubble.Update(gameTime);
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
                return this.personBubble.IsShowing;
            }
            set
            {
                this.personBubble.IsShowing = value;
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

