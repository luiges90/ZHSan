using Platforms;
using System;
using System.Collections.Generic;
using System.Xml;


namespace GameFreeText
{

    public class GameObjectTextTree
    {
        private Dictionary<string, GameObjectTextBranch> Branches = new Dictionary<string, GameObjectTextBranch>();

        public GameObjectTextBranch GetBranch(string branchName)
        {
            GameObjectTextBranch branch;
            if (this.Branches.TryGetValue(branchName, out branch))
            {
                return branch;
            }
            return null;
        }

        public void LoadFromXmlFile(string filePath)
        {
            XmlDocument document = new XmlDocument();
            
            string xml = Platform.Current.LoadText(filePath);
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            foreach (XmlNode node2 in nextSibling.ChildNodes)
            {
                GameObjectTextBranch branch = new GameObjectTextBranch {
                    BranchName = node2.Attributes.GetNamedItem("Name").Value
                };
                if (node2.Attributes.GetNamedItem("Time") != null)
                {
                    branch.Time = int.Parse(node2.Attributes.GetNamedItem("Time").Value);
                }
                branch.LoadFromXmlNode(node2);
                this.Branches.Add(branch.BranchName, branch);
            }
        }

        public Boolean HasItem(string branchName)
        {
            return Branches.ContainsKey(branchName);
        }

        public void AddItem(string branchName, GameObjectTextBranch content)
        {
            Branches.Add(branchName, content);
        }

    }
}

