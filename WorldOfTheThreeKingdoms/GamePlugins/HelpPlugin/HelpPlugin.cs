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

namespace HelpPlugin
{

    public class HelpPlugin : GameObject, IHelp, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\Help\Data\";
        private string description = "帮助";
        
        private Help help = new Help();
        private const string Path = @"Content\Textures\GameComponents\Help\";
        private string pluginName = "HelpPlugin";
        private string version = "1.0.0";
        private const string XMLFilename = "HelpData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
            this.help.Draw();
        }

        public void DrawButton(float depth)
        {
            this.help.DrawButton(depth);
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
            this.help.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\Help\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.help.BackgroundSize.X = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.help.BackgroundSize.Y = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.help.ButtonTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\Help\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.help.ButtonSelectedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\Help\Data\" + node.Attributes.GetNamedItem("Selected").Value);
            node = nextSibling.ChildNodes.Item(2);
            this.help.RichText.ClientWidth = this.help.BackgroundSize.X - 10;
            this.help.RichText.ClientHeight = this.help.BackgroundSize.Y - 20;
            this.help.RichText.RowMargin = int.Parse(node.Attributes.GetNamedItem("RowMargin").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);

            this.help.RichText.Builder = font;
            //this.help.RichText.Builder.SetFreeTextBuilder(font);

            this.help.RichText.DefaultColor = color;
            this.help.RichText.TitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("TitleColor").Value);
            this.help.RichText.SubTitleColor = StaticMethods.LoadColor(node.Attributes.GetNamedItem("SubTitleColor").Value);

            //this.help.RichText.Builder.SetCapacity(0x7fffffff);

            this.help.TextTree.LoadFromXmlFile(@"Content\Data\Plugins\HelpTextTree.xml");
        }

        public void SetButtonDisplayOffset(Microsoft.Xna.Framework.Point offset)
        {
            this.help.ButtonDisplayOffset = offset;
        }

        public void SetButtonSize(Microsoft.Xna.Framework.Point size)
        {
            this.help.ButtonSize = size;
        }

        public bool SetCurrentKey(string key)
        {
            this.help.CurrentBranch = this.help.TextTree.GetBranch(key);
            return (this.help.CurrentBranch != null);
        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\HelpData.xml");
        }

        public void SetScenario()
        {
            foreach (GameObjects.TroopDetail.CombatMethod m in Session.Current.Scenario.GameCommonData.AllCombatMethods.CombatMethods.Values)
            {
                if (!this.help.TextTree.HasItem("TroopCombatMethod_" + m.ID))
                {
                    GameObjectTextBranch branch = new GameObjectTextBranch();
                    branch.AddLeaf("战法", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf(m.Name, this.help.RichText.TitleColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    foreach (GameObjects.Influences.Influence i in m.Influences.Influences.Values)
                    {
                        branch.AddLeaf(i.Description, this.help.RichText.SubTitleColor.PackedValue);
                        branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    }
                    this.help.TextTree.AddItem("TroopCombatMethod_" + m.ID, branch);
                }
            }
            foreach (GameObjects.TroopDetail.Stratagem m in Session.Current.Scenario.GameCommonData.AllStratagems.Stratagems.Values)
            {
                if (!this.help.TextTree.HasItem("TroopStratagem_" + m.ID))
                {
                    GameObjectTextBranch branch = new GameObjectTextBranch();
                    branch.AddLeaf("计略", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf(m.Name, this.help.RichText.TitleColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    foreach (GameObjects.Influences.Influence i in m.Influences.Influences.Values)
                    {
                        branch.AddLeaf(i.Description, this.help.RichText.SubTitleColor.PackedValue);
                        branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    }
                    this.help.TextTree.AddItem("TroopStratagem_" + m.ID, branch);
                }
            }
            foreach (GameObjects.PersonDetail.Stunt m in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
            {
                if (!this.help.TextTree.HasItem("TroopStunt_" + m.ID))
                {
                    GameObjectTextBranch branch = new GameObjectTextBranch();
                    branch.AddLeaf("特技", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf(m.Name, this.help.RichText.TitleColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    foreach (GameObjects.Influences.Influence i in m.Influences.Influences.Values)
                    {
                        branch.AddLeaf(i.Description, this.help.RichText.SubTitleColor.PackedValue);
                        branch.AddLeaf("\\n", this.help.RichText.DefaultColor.PackedValue);
                    }
                    this.help.TextTree.AddItem("TroopStunt_" + m.ID, branch);
                }
            }
        }

        public void SetMapPosition(ShowPosition showPosition)
        {
            this.help.SetDisplayOffset(showPosition);
        }

        public void SetScreen(Screen screen)
        {
            this.help.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            this.help.Update();
        }

        public string Author
        {
            get
            {
                return this.author;
            }
        }

        public Microsoft.Xna.Framework.Rectangle ButtonDisplayPosition
        {
            get
            {
                return this.help.ButtonDisplayPosition;
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

        public bool IsButtonShowing
        {
            get
            {
                return this.help.IsButtonShowing;
            }
            set
            {
                this.help.IsButtonShowing = value;
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.help.IsShowing;
            }
            set
            {
                this.help.IsShowing = value;
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
                return this.help.RichText;
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

