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
                item.TextColor.PackedValue = uint.Parse(node.Attributes.GetNamedItem("Color").Value);
                this.Leaves.Add(item);
            }
        }
    }
}

