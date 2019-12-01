using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using		GameFreeText;
using		GameGlobal;
using		GameObjects;
using		Microsoft.Xna.Framework;
using		PluginInterface;
using		System.Xml;
using TroopTitlePlugin;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface.BaseInterface;
//using System.Drawing;
using WorldOfTheThreeKingdoms;
using Platforms;
using GameManager;

namespace TroopTitlePlugin
{
    public class TroopTitlePlugin : GameObject, ITroopTitle, IBasePlugin, IPluginXML, IPluginGraphics
    {
        private string author = "clip_on";
        private const string DataPath = @"Content\Textures\GameComponents\TroopTitle\Data\";
        private string description = "用来显示部队的标题";
        
        private const string Path = @"Content\Textures\GameComponents\TroopTitle\";
        private string pluginName = "TroopTitlePlugin";
        private TroopTitle troopTitle = new TroopTitle();
        private string version = "1.0.0";
        private const string XMLFilename = "TroopTitleData.xml";

        public void Dispose()
        {
        }

        public void Draw()
        {
        }

        public void DrawTroop(object troop, bool playerControlling)
        {
            if (this.IsShowing)
            {
                this.troopTitle.DrawTroop( troop as Troop, playerControlling);
                /*
                if (troopTitle.Switch11 == "on" && this.troopTitle.HasPersonPicture == true)
                {
                    this.troopTitle.ThePersonPicture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\PersonPicture\" + this.troopTitle.PersonID.ToString() + ".png");
                }
                if (troopTitle.Switch21 == "on" && this.troopTitle.HasFactionPicture == true)
                {
                    this.troopTitle.TheFactionPicture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + this.troopTitle.FactionID.ToString() + ".png");
                }
                /*
                if (troopTitle.Switch31 == "on")
                {
                    this.troopTitle.TheTroopKindPicture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + this.troopTitle.TheTroopKind.ToString() + ".png");
                }
                 */ 
            }
        }

        public void Initialize(Screen screen)
        {
        }

        public void LoadDataFromXMLDocument(string filename)
        {
            Font font;
            Microsoft.Xna.Framework.Color color;
            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filename);document.LoadXml(xml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            XmlNode node = nextSibling.ChildNodes.Item(0);
            this.troopTitle.BackgroundTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.PictureNull = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("PictureNull").Value);
            this.troopTitle.BackgroundSize = new  Microsoft.Xna.Framework.Point(int.Parse(node.Attributes.GetNamedItem("Width").Value), int.Parse(node.Attributes.GetNamedItem("Height").Value));
            node = nextSibling.ChildNodes.Item(1);
            this.troopTitle.PortraitPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(2);
            this.troopTitle.FactionTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.FactionPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(3);
            Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.NameText = new FreeText(font, color);
            this.troopTitle.NameText.Position = rectangle;
            this.troopTitle.NameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            node = nextSibling.ChildNodes.Item(4);
            this.troopTitle.ActionDoneTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Done").Value);
            this.troopTitle.ActionUndoneTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Undone").Value);
            this.troopTitle.ActionAutoTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Auto").Value);
            this.troopTitle.ActionAutoDoneTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("AutoDone").Value);
            this.troopTitle.ActionAttackedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Attacked").Value);
            this.troopTitle.ActionMovedTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Moved").Value);
            this.troopTitle.ActionIconPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(5);
            this.troopTitle.FoodNormalTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Normal").Value);
            this.troopTitle.FoodShortageTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Shortage").Value);
            this.troopTitle.FoodIconPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(6);
            this.troopTitle.NoControlTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("NoControl").Value);
            this.troopTitle.NoControlIconPosition = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(7);
            this.troopTitle.StuntTexture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Stunt").Value);
            this.troopTitle.StuntIconPosition = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(8);
            Microsoft.Xna.Framework.Rectangle rectangle1 = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.binglitext = new FreeText(font, color);
            this.troopTitle.binglitext.Position = rectangle1;
            this.troopTitle.binglitext.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);

            node = nextSibling.ChildNodes.Item(9);
            this.troopTitle.shiqicaotupian  = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.shiqicaoweizhi  = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(10);
            this.troopTitle.shiqitiaotupian = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.shiqitiaoweizhi = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(15);
            this.troopTitle.Switch1 = node.Attributes.GetNamedItem("UIKind").Value;
            /*
            this.troopTitle.Switch11 = node.Attributes.GetNamedItem("PersonPicture").Value;
            this.troopTitle.Switch11 = node.Attributes.GetNamedItem("PersonPortrait").Value;
            this.troopTitle.Switch21 = node.Attributes.GetNamedItem("FactionPicture").Value;
            this.troopTitle.Switch22 = node.Attributes.GetNamedItem("FactionName").Value;
            this.troopTitle.Switch23 = node.Attributes.GetNamedItem("FactionColour").Value;
            this.troopTitle.Switch31 = node.Attributes.GetNamedItem("TroopKind").Value;
            this.troopTitle.Switch36 = node.Attributes.GetNamedItem("Food").Value;
            this.troopTitle.Switch37 = node.Attributes.GetNamedItem("Stunt").Value;
            this.troopTitle.Switch38 = node.Attributes.GetNamedItem("Action").Value;
            this.troopTitle.Switch41 = node.Attributes.GetNamedItem("shiqi").Value;
            this.troopTitle.Switch42 = node.Attributes.GetNamedItem("zhanyi").Value;
            */
            node = nextSibling.ChildNodes.Item(21);
            this.troopTitle.TheBackground1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheBackground1 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.troopTitle.TheMask11 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.troopTitle.TheMask12 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            this.troopTitle.TheMask13 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask3").Value);
            this.troopTitle.TheMask14 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask4").Value);
            node = nextSibling.ChildNodes.Item(22);
            this.troopTitle.ThePortrait1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(26);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.FactionName1Text = new FreeText(font, color);
            this.troopTitle.FactionName1Text.Position = rectangle;
            this.troopTitle.FactionName1Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.troopTitle.FactionName1Position.X = int.Parse(node.Attributes.GetNamedItem("X").Value);
            this.troopTitle.FactionName1Position.Y = int.Parse(node.Attributes.GetNamedItem("Y").Value);
            this.troopTitle.FactionName1Position.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.troopTitle.FactionName1Position.Height = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.troopTitle.FactionName1Kind = node.Attributes.GetNamedItem("FactionNameKind").Value;
            this.troopTitle.ShowFactionName1Background = node.Attributes.GetNamedItem("ShowFactionNameBackground").Value;
            this.troopTitle.FactionName1Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("Background").Value);            
            node = nextSibling.ChildNodes.Item(27);
            this.troopTitle.FactionColor1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.FactionColor1Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("ColorPicture").Value);
            this.troopTitle.FactionColor1Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("ColorBackground").Value);
            node = nextSibling.ChildNodes.Item(31);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.TroopName1Text = new FreeText(font, color);
            this.troopTitle.TroopName1Text.Position = rectangle;
            this.troopTitle.TroopName1Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);

            node = nextSibling.ChildNodes.Item(36);
            this.troopTitle.TheTroopKind1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheTroopKind11Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind1").Value);
            this.troopTitle.TheTroopKind12Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind2").Value);
            this.troopTitle.TheTroopKind13Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind3").Value);
            this.troopTitle.TheTroopKind14Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind4").Value);
            this.troopTitle.TheTroopKind15Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind5").Value);
           
            node = nextSibling.ChildNodes.Item(37);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.Thebingli1Text = new FreeText(font, color);
            this.troopTitle.Thebingli1Text.Position = rectangle;
            this.troopTitle.Thebingli1Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);

            node = nextSibling.ChildNodes.Item(41);
            this.troopTitle.TheActionIcon1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheActionDone1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Done").Value);
            this.troopTitle.TheActionUndone1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Undone").Value);
            this.troopTitle.TheActionAuto1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Auto").Value);
            this.troopTitle.TheActionAutoDone1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("AutoDone").Value);
            this.troopTitle.TheActionAttacked1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Attacked").Value);
            this.troopTitle.TheActionMoved1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Moved").Value);
            node = nextSibling.ChildNodes.Item(42);
            this.troopTitle.TheFoodIcon1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheFoodNormal1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Normal").Value);
            this.troopTitle.TheFoodShortage1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Shortage").Value);            
            node = nextSibling.ChildNodes.Item(43);
            this.troopTitle.TheNoControl1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("NoControl").Value);
            this.troopTitle.TheNoControlIcon1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(44);
            this.troopTitle.TheStunt1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Stunt").Value);
            this.troopTitle.TheStuntIcon1Position = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(46);
            this.troopTitle.Theshiqi1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.Theshiqi1Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(47);
            this.troopTitle.Thezhanyi1Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.Thezhanyi1Position = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(51);
            this.troopTitle.TheBackground2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheBackground2 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Background").Value);
            this.troopTitle.TheMask21 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask1").Value);
            this.troopTitle.TheMask22 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask2").Value);
            this.troopTitle.TheMask23 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask3").Value);
            this.troopTitle.TheMask24 = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\" + node.Attributes.GetNamedItem("Mask4").Value);
            node = nextSibling.ChildNodes.Item(52);
            this.troopTitle.ThePortrait2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(56);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.FactionName2Text = new FreeText(font, color);
            this.troopTitle.FactionName2Text.Position = rectangle;
            this.troopTitle.FactionName2Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);
            this.troopTitle.FactionName2Position.X = int.Parse(node.Attributes.GetNamedItem("X").Value);
            this.troopTitle.FactionName2Position.Y = int.Parse(node.Attributes.GetNamedItem("Y").Value);
            this.troopTitle.FactionName2Position.Width = int.Parse(node.Attributes.GetNamedItem("Width").Value);
            this.troopTitle.FactionName2Position.Height = int.Parse(node.Attributes.GetNamedItem("Height").Value);
            this.troopTitle.FactionName2Kind = node.Attributes.GetNamedItem("FactionNameKind").Value;
            this.troopTitle.ShowFactionName2Background = node.Attributes.GetNamedItem("ShowFactionNameBackground").Value;
            this.troopTitle.FactionName2Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("Background").Value);
            node = nextSibling.ChildNodes.Item(57);
            this.troopTitle.FactionColor2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.FactionColor2Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("ColorPicture").Value);
            this.troopTitle.FactionColor2Background = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + node.Attributes.GetNamedItem("ColorBackground").Value);
            node = nextSibling.ChildNodes.Item(61);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.TroopName2Text = new FreeText(font, color);
            this.troopTitle.TroopName2Text.Position = rectangle;
            this.troopTitle.TroopName2Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);

            node = nextSibling.ChildNodes.Item(66);
            this.troopTitle.TheTroopKind2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheTroopKind21Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind1").Value);
            this.troopTitle.TheTroopKind22Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind2").Value);
            this.troopTitle.TheTroopKind23Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind3").Value);
            this.troopTitle.TheTroopKind24Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind4").Value);
            this.troopTitle.TheTroopKind25Picture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\TroopKind\" + node.Attributes.GetNamedItem("TroopKind5").Value);

            node = nextSibling.ChildNodes.Item(67);
            rectangle = StaticMethods.LoadRectangleFromXMLNode(node);
            StaticMethods.LoadFontAndColorFromXMLNode(node, out font, out color);
            this.troopTitle.Thebingli2Text = new FreeText(font, color);
            this.troopTitle.Thebingli2Text.Position = rectangle;
            this.troopTitle.Thebingli2Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), node.Attributes.GetNamedItem("Align").Value);

            node = nextSibling.ChildNodes.Item(71);
            this.troopTitle.TheActionIcon2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheActionDone2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Done").Value);
            this.troopTitle.TheActionUndone2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Undone").Value);
            this.troopTitle.TheActionAuto2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Auto").Value);
            this.troopTitle.TheActionAutoDone2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("AutoDone").Value);
            this.troopTitle.TheActionAttacked2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Attacked").Value);
            this.troopTitle.TheActionMoved2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Moved").Value);

            node = nextSibling.ChildNodes.Item(72);
            this.troopTitle.TheFoodIcon2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            this.troopTitle.TheFoodNormal2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Normal").Value);
            this.troopTitle.TheFoodShortage2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Shortage").Value);
            node = nextSibling.ChildNodes.Item(73);
            this.troopTitle.TheNoControl2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("NoControl").Value);
            this.troopTitle.TheNoControlIcon2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(74);
            this.troopTitle.TheStunt2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("Stunt").Value);
            this.troopTitle.TheStuntIcon2Position = StaticMethods.LoadRectangleFromXMLNode(node);

            node = nextSibling.ChildNodes.Item(76);
            this.troopTitle.Theshiqi2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.Theshiqi2Position = StaticMethods.LoadRectangleFromXMLNode(node);
            node = nextSibling.ChildNodes.Item(77);
            this.troopTitle.Thezhanyi2Texture = CacheManager.GetTempTexture(@"Content\Textures\GameComponents\TroopTitle\Data\Icon\" + node.Attributes.GetNamedItem("FileName").Value);
            this.troopTitle.Thezhanyi2Position = StaticMethods.LoadRectangleFromXMLNode(node);

        }

        public void SetGraphicsDevice()
        {
            this.LoadDataFromXMLDocument(@"Content\Data\Plugins\TroopTitleData.xml");
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
                return this.troopTitle.IsShowing;
            }
            set
            {
                this.troopTitle.IsShowing = value;
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
