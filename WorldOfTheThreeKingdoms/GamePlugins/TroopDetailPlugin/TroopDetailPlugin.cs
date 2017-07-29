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
////using System.Drawing;
using System.Xml;
using WorldOfTheThreeKingdoms;

namespace TroopDetailPlugin
{
	public class TroopDetailPlugin : GameObject, ITroopDetail, IBasePlugin, IPluginXML, IPluginGraphics
	{
		private const string Path = @"Content\Textures\GameComponents\TroopDetail\";

		private const string DataPath = @"Content\Textures\GameComponents\TroopDetail\Data\";

		private const string XMLFilename = "TroopDetailData.xml";

		private string pluginName;

		private string description;

		private string author;

		private string version;

		private TroopDetail troopDetail;

		

		public string Author
		{
			get
			{
				string str = this.author;
				return str;
			}
		}

		public string Description
		{
			get
			{
				string str = this.description;
				return str;
			}
		}

		public object Instance
		{
			get
			{
				object obj = this;
				return obj;
			}
		}

		public bool IsShowing
		{
			get
			{
				bool isShowing = this.troopDetail.IsShowing;
				return isShowing;
			}
			set
			{
				this.troopDetail.IsShowing = value;
			}
		}

		public string PluginName
		{
			get
			{
				string str = this.pluginName;
				return str;
			}
		}

		public string Version
		{
			get
			{
				string str = this.version;
				return str;
			}
		}

		public TroopDetailPlugin()
		{
			this.pluginName = "TroopDetailPlugin";
			this.description = "部队细节显示";
			this.author = "clip_on";
			this.version = "1.0.0";
			this.troopDetail = new TroopDetail();
		}

		public void Dispose()
		{
		}

		public void Draw()
		{
			bool isShowing = !this.troopDetail.IsShowing;
			if (!isShowing)
			{
				this.troopDetail.Draw();
			}
		}

		public void Initialize(Screen screen)
		{
		}

		public void LoadDataFromXMLDocument(string filename)
		{
			Font font = null;
            Microsoft.Xna.Framework.Color color;
			XmlDocument xmlDocument = new XmlDocument();

            var xml = Platform.Current.LoadText(filename);
            xmlDocument.LoadXml(xml);

            XmlNode nextSibling = xmlDocument.FirstChild.NextSibling;
			XmlNode xmlNodes = nextSibling.ChildNodes.Item(0);
			this.troopDetail.BackgroundSize.X = int.Parse(xmlNodes.Attributes.GetNamedItem("Width").Value);
			this.troopDetail.BackgroundSize.Y = int.Parse(xmlNodes.Attributes.GetNamedItem("Height").Value);
			this.troopDetail.BackgroundTexture = CacheManager.GetTempTexture(string.Concat(@"Content\Textures\GameComponents\TroopDetail\Data\", xmlNodes.Attributes.GetNamedItem("FileName").Value));
			xmlNodes = nextSibling.ChildNodes.Item(1);
            Microsoft.Xna.Framework.Rectangle rectangle = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
            StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes, out font, out color);
			this.troopDetail.TroopNameText = new FreeText(font, color);
			this.troopDetail.TroopNameText.Position = rectangle;
			this.troopDetail.TroopNameText.Align = (TextAlign)Enum.Parse(typeof(TextAlign), xmlNodes.Attributes.GetNamedItem("Align").Value);
			xmlNodes = nextSibling.ChildNodes.Item(2);
			this.troopDetail.PortraitClient = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
			xmlNodes = nextSibling.ChildNodes.Item(3);
			int num = 0;
			while (true)
			{
				bool count = num < xmlNodes.ChildNodes.Count;
				if (!count)
				{
					break;
				}
				LabelText labelText = new LabelText();
				XmlNode xmlNodes1 = xmlNodes.ChildNodes.Item(num);
				rectangle = StaticMethods.LoadRectangleFromXMLNode(xmlNodes1);
                StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes1, out font, out color);
				labelText.Label = new FreeText(font, color);
				labelText.Label.Position = rectangle;
				labelText.Label.Align = (TextAlign)Enum.Parse(typeof(TextAlign), xmlNodes1.Attributes.GetNamedItem("Align").Value);
				labelText.Label.Text = xmlNodes1.Attributes.GetNamedItem("Label").Value;
				xmlNodes1 = xmlNodes.ChildNodes.Item(num + 1);
				rectangle = StaticMethods.LoadRectangleFromXMLNode(xmlNodes1);
                StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes1, out font, out color);
				labelText.Text = new FreeText(font, color);
				labelText.Text.Position = rectangle;
				labelText.Text.Align = (TextAlign)Enum.Parse(typeof(TextAlign), xmlNodes1.Attributes.GetNamedItem("Align").Value);
				labelText.PropertyName = xmlNodes1.Attributes.GetNamedItem("PropertyName").Value;
				this.troopDetail.LabelTexts.Add(labelText);
				num = num + 2;
			}
			xmlNodes = nextSibling.ChildNodes.Item(4);
			this.troopDetail.OtherPersonClient = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
			this.troopDetail.OtherPersonText.ClientWidth = this.troopDetail.OtherPersonClient.Width;
			this.troopDetail.OtherPersonText.ClientHeight = this.troopDetail.OtherPersonClient.Height;
			this.troopDetail.OtherPersonText.RowMargin = int.Parse(xmlNodes.Attributes.GetNamedItem("RowMargin").Value);
            this.troopDetail.OtherPersonText.TitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("TitleColor").Value);
            this.troopDetail.OtherPersonText.SubTitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes, out font, out color);

			//this.troopDetail.OtherPersonText.Builder.SetFreeTextBuilder(font);

			this.troopDetail.OtherPersonText.DefaultColor = color;
			xmlNodes = nextSibling.ChildNodes.Item(5);
			this.troopDetail.CombatMethodClient = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
			this.troopDetail.CombatMethodText.ClientWidth = this.troopDetail.CombatMethodClient.Width;
			this.troopDetail.CombatMethodText.ClientHeight = this.troopDetail.CombatMethodClient.Height;
			this.troopDetail.CombatMethodText.RowMargin = int.Parse(xmlNodes.Attributes.GetNamedItem("RowMargin").Value);
            this.troopDetail.CombatMethodText.TitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("TitleColor").Value);
            this.troopDetail.CombatMethodText.SubTitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor").Value);
            this.troopDetail.CombatMethodText.SubTitleColor2 = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor2").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes, out font, out color);

            this.troopDetail.CombatMethodText.Builder = font;
            //this.troopDetail.CombatMethodText.Builder.SetFreeTextBuilder(font);

			this.troopDetail.CombatMethodText.DefaultColor = color;
			xmlNodes = nextSibling.ChildNodes.Item(6);
			this.troopDetail.StuntClient = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
			this.troopDetail.StuntText.ClientWidth = this.troopDetail.CombatMethodClient.Width;
			this.troopDetail.StuntText.ClientHeight = this.troopDetail.CombatMethodClient.Height;
			this.troopDetail.StuntText.RowMargin = int.Parse(xmlNodes.Attributes.GetNamedItem("RowMargin").Value);
            this.troopDetail.StuntText.TitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("TitleColor").Value);
            this.troopDetail.StuntText.SubTitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor").Value);
            this.troopDetail.StuntText.SubTitleColor2 = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.troopDetail.StuntText.SubTitleColor3 = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes, out font, out color);

            this.troopDetail.StuntText.Builder = font;
            //this.troopDetail.StuntText.Builder.SetFreeTextBuilder(font);

			this.troopDetail.StuntText.DefaultColor = color;
			xmlNodes = nextSibling.ChildNodes.Item(7);
			this.troopDetail.InfluenceClient = StaticMethods.LoadRectangleFromXMLNode(xmlNodes);
			this.troopDetail.InfluenceText.ClientWidth = this.troopDetail.InfluenceClient.Width;
			this.troopDetail.InfluenceText.ClientHeight = this.troopDetail.InfluenceClient.Height;
			this.troopDetail.InfluenceText.RowMargin = int.Parse(xmlNodes.Attributes.GetNamedItem("RowMargin").Value);
            this.troopDetail.InfluenceText.TitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("TitleColor").Value);
            this.troopDetail.InfluenceText.SubTitleColor = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor").Value);
            this.troopDetail.InfluenceText.SubTitleColor2 = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor2").Value);
            this.troopDetail.InfluenceText.SubTitleColor3 = StaticMethods.LoadColor(xmlNodes.Attributes.GetNamedItem("SubTitleColor3").Value);
            StaticMethods.LoadFontAndColorFromXMLNode(xmlNodes, out font, out color);

            this.troopDetail.InfluenceText.Builder = font;
            //this.troopDetail.InfluenceText.Builder.SetFreeTextBuilder(font);

			this.troopDetail.InfluenceText.DefaultColor = color;
		}

		public void SetGraphicsDevice()
		{
			this.LoadDataFromXMLDocument(@"Content\Data\Plugins\TroopDetailData.xml");
		}

		public void SetPosition(ShowPosition showPosition)
		{
			this.troopDetail.SetPosition(showPosition);
		}

		public void SetScreen(Screen screen)
		{
			this.troopDetail.Initialize();
		}

		public void SetTroop(object troop)
		{
			this.troopDetail.SetTroop(troop as Troop);
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}
