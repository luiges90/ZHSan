using System;
using System.Collections.Generic;
using System.Xml;


namespace GameFreeText
{

    public class SimpleTextBranch
    {
        public string BranchName;
        public List<SimpleTextLeaf> Leaves = new List<SimpleTextLeaf>();

        public void LoadFromXmlNode(XmlNode rootNode)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                SimpleTextLeaf item = new SimpleTextLeaf {
                    Text = node.Attributes.GetNamedItem("Text").Value
                };
                uint x = uint.Parse(node.Attributes.GetNamedItem("Color").Value);
                item.TextColor.PackedValue = (x & 0xFF000000) | ((x & 0x00FF0000) >> 16) | (x & 0x0000FF00) | ((x & 0x000000FF) << 16);
                this.Leaves.Add(item);
            }
        }
    }
}

