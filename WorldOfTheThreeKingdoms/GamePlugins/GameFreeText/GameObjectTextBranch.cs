using System;
using System.Collections.Generic;
using System.Xml;


namespace GameFreeText
{

    public class GameObjectTextBranch
    {
        public string BranchName;
        public List<GameObjectTextLeaf> Leaves = new List<GameObjectTextLeaf>();
        public int Time;

        public void LoadFromXmlNode(XmlNode rootNode)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                GameObjectTextLeaf item = new GameObjectTextLeaf();
                XmlNode namedItem = node.Attributes.GetNamedItem("Property");
                if (namedItem != null)
                {
                    item.Property = namedItem.Value;
                }
                namedItem = node.Attributes.GetNamedItem("Text");
                if (namedItem != null)
                {
                    item.Text = namedItem.Value;
                }
                item.TextColor.PackedValue = uint.Parse(node.Attributes.GetNamedItem("Color").Value);
                this.Leaves.Add(item);
            }
        }

        public void AddLeaf(String text, uint color)
        {
            GameObjectTextLeaf item = new GameObjectTextLeaf();
            item.Text = text;
            item.TextColor.PackedValue = color;
            this.Leaves.Add(item);
        }
    }
}

