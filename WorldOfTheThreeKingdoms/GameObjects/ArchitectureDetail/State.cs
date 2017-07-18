using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class State : GameObject
    {
        public ArchitectureList Architectures = new ArchitectureList();

        public StateList ContactStates = new StateList();

        [DataMember]
        public string ContactStatesString;

        public Region LinkedRegion;

        public Architecture StateAdmin;

        [DataMember]
        public int StateAdminID;

        public void Init()
        {
            Architectures = new ArchitectureList();

            ContactStates = new StateList();
        }

        public int GetFactionScale(Faction faction)
        {
            if (this.Architectures.Count <= 0)
            {
                return 0;
            }
            int num = 0;
            foreach (Architecture architecture in this.Architectures)
            {
                if ((architecture.BelongedFaction == null) || (faction == architecture.BelongedFaction))
                {
                    num++;
                }
            }
            return ((num * 100) / this.Architectures.Count);
        }

        public int GetSectionScale(Section section)
        {
            if ((this.Architectures.Count <= 0) || (section.ArchitectureCount <= 0))
            {
                return 0;
            }
            int num = 0;
            foreach (Architecture architecture in this.Architectures)
            {
                if (architecture.BelongedSection == section)
                {
                    num++;
                }
                if (num >= section.ArchitectureCount)
                {
                    return 100;
                }
            }
            return ((num * 100) / section.ArchitectureCount);
        }

        public List<string> LoadContactStatesFromString(StateList contactStates, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.ContactStates.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    State gameObject = contactStates.GetGameObject(int.Parse(str)) as State;
                    if (gameObject != null)
                    {
                        this.ContactStates.Add(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("州域ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("连接州域一栏应为半型空格分隔的州域ID");
            }
            return errorMsg;
        }

        public override string ToString()
        {
            return (base.Name + " " + this.LinkedRegionString);
        }

        public string ContactStatesDisplayString
        {
            get
            {
                string str = "";
                foreach (State state in this.ContactStates)
                {
                    str = str + state.Name + " ";
                }
                return str;
            }
        }

        public string LinkedRegionString
        {
            get
            {
                return ((this.LinkedRegion != null) ? this.LinkedRegion.Name : "----");
            }
        }

        public string StateAdminString
        {
            get
            {
                return ((this.StateAdmin != null) ? this.StateAdmin.Name : "----");
            }
        }
    }
}

