using Platforms;
using System;
using System.Collections.Generic;
using System.Xml;


namespace GameFreeText
{

    public class SimpleTextTree
    {
        private Dictionary<string, SimpleTextBranch> Branches = new Dictionary<string, SimpleTextBranch>();

        public SimpleTextBranch GetBranch(string branchName)
        {
            SimpleTextBranch branch;
            if (this.Branches.TryGetValue(branchName, out branch))
            {
                return branch;
            }
            return null;
        }

        public void LoadFromXmlFile(string filePath)
        {
            //XmlDocument document = new XmlDocument();
            //document.Load(filePath);

            XmlDocument document = new XmlDocument();
            string xml = Platform.Current.LoadText(filePath);
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            foreach (XmlNode node2 in nextSibling.ChildNodes)
            {
                SimpleTextBranch branch = new SimpleTextBranch {
                    BranchName = node2.Attributes.GetNamedItem("Name").Value
                };
                branch.LoadFromXmlNode(node2);
                this.Branches.Add(branch.BranchName, branch);
            }
        }
    }
}

