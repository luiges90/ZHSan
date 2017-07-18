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

namespace TabListPlugin
{

    public class TabListPlugin : GameObject, ITabList, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\TabList\Data\";
        private string description = "可选择类别的详细列表";
        private const string Path = @"Content\Textures\GameComponents\TabList\";
        private string pluginName = "TabListPlugin";
        private TabListInFrame tabList = new TabListInFrame();
        private string version = "1.0.1";
        private const string XMLFilename = "TabListData.xml";

        public void Dispose()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.tabList.Draw(spriteBatch);
        }

        public void Initialize()
        {
        }

        public void InitialValues(object gameObjectList, object selectedObjectList, int scrollValue, string title)
        {
            this.tabList.InitialValues(gameObjectList as GameObjectList, selectedObjectList as GameObjectList, scrollValue, title);
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Font font;
            Microsoft.Xna.Framework.Color color;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.tabList.tabbuttonWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.tabbuttonHeight = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.tabList.tabbuttonTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.tabList.tabbuttonselectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("SelectedFileName").Value);
            node = nextSibling.ChildNodes.Item(1);
            this.tabList.columnheaderHeight = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.tabList.columnheaderTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(2);
            this.tabList.columnspliterWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.columnspliterHeight = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.tabList.columnspliterTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(3);
            this.tabList.scrollbuttonWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.scrollbuttonTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(4);
            this.tabList.scrolltrackWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.scrolltrackTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(5);
            this.tabList.leftArrowTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("LeftFileName").Value);
            this.tabList.rightArrowTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("RightFileName").Value);
            node = nextSibling.ChildNodes.Item(6);
            this.tabList.focusTrackTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            node = nextSibling.ChildNodes.Item(7);
            this.tabList.checkboxName = node.Attributes.GetNamedItem("Name").Value;
            this.tabList.checkboxDisplayName = node.Attributes.GetNamedItem("DisplayName").Value;
            this.tabList.checkboxWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.checkboxTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.tabList.checkboxSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("SelectedFileName").Value);
            this.tabList.roundcheckboxTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("RoundFileName").Value);
            this.tabList.roundcheckboxSelectedTexture = CacheManager.LoadTempTexture(@"Content\Textures\GameComponents\TabList\Data\" + node.Attributes.GetNamedItem("RoundSelectedFileName").Value);
            node = nextSibling.ChildNodes.Item(8);
            this.tabList.PortraitWidth = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.tabList.PortraitHeight = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            node = nextSibling.ChildNodes.Item(9);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.tabList.TabTextBuilder.SetFreeTextBuilder(this.tabList.graphicsDevice, font);
            this.tabList.TabTextColor = color;
            this.tabList.TabTextAlign = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(10);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.tabList.ColumnTextBuilder.SetFreeTextBuilder(this.tabList.graphicsDevice, font);
            this.tabList.ColumnTextColor = color;
            this.tabList.ColumnTextAlign = (TextAlign) Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(11);
            this.tabList.SelectSoundFile = @"Content\Sound\" + node.Attributes.GetNamedItem("Select").Value;
            this.tabList.LoadFromXMLNode(nextSibling.ChildNodes.Item(12));
        }

        public void RefreshEditable()
        {
            this.tabList.RefreshEditable();
        }

        public void SetArchitectureDetailDialog(IArchitectureDetail iArchitectureDetail)
        {
            this.tabList.iArchitectureDetail = iArchitectureDetail;
        }

        public void SetFactionTechniquesDialog(IFactionTechniques iFactionTechniques)
        {
            this.tabList.iFactionTechniques = iFactionTechniques;
        }

        public void SetGameFrame(IGameFrame iGameFrame)
        {
            this.tabList.iGameFrame = iGameFrame;
        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            this.tabList.graphicsDevice = device;
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\TabListData.xml");
        }

        public void SetListKindByName(string Name, bool ShowCheckBox, bool MultiSelecting)
        {
            this.tabList.SetListKindByName(Name, ShowCheckBox, MultiSelecting);
        }

        public void SetMapViewSelector(IMapViewSelector iMapViewSelector)
        {
            this.tabList.iMapViewSelector = iMapViewSelector;
            iMapViewSelector.SetTabList(this);
        }

        public void SetPersonDetailDialog(IPersonDetail iPersonDetail)
        {
            this.tabList.iPersonDetail = iPersonDetail;
        }

        public void SetScreen(object screen)
        {
            this.tabList.Initialize(screen as Screen);
        }

        public void SetSelectedItemMaxCount(int max)
        {
            this.tabList.SelectedItemMaxCount = max;
        }

        public void SetSelectedTab(string tabName)
        {
            this.tabList.SetSelectedTab(tabName);
        }

        public void SetTreasureDetailDialog(ITreasureDetail iTreasureDetail)
        {
            this.tabList.iTreasureDetail = iTreasureDetail;
        }

        public void SetTroopDetailDialog(ITroopDetail iTroopDetail)
        {
            this.tabList.iTroopDetail = iTroopDetail;
        }

        public void Update(GameTime gameTime)
        {
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
                return this.tabList.IsShowing;
            }
            set
            {
                this.tabList.IsShowing = value;
            }
        }

        public string PluginName
        {
            get
            {
                return this.pluginName;
            }
        }

        public object SelectedItem
        {
            get
            {
                return this.tabList.SelectedItem;
            }
        }

        public object SelectedItemList
        {
            get
            {
                return this.tabList.SelectedItemList;
            }
        }

        public object TabList
        {
            get
            {
                return this.tabList;
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

