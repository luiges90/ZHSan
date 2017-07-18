using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class Region : GameObject
    {
        public ArchitectureList Architectures = new ArchitectureList();
        public Architecture RegionCore;

        [DataMember]
        public int RegionCoreID;

        [DataMember]
        public string StatesListString;

        public StateList States = new StateList();

        public void Init()
        {
            Architectures = new ArchitectureList();
            States = new StateList();
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

        public List<string> LoadStatesFromString(StateList states, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.States.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    State gameObject = states.GetGameObject(int.Parse(str)) as State;
                    if (gameObject != null)
                    {
                        this.States.Add(gameObject);
                        gameObject.LinkedRegion = this;
                    }
                    else
                    {
                        errorMsg.Add("州域ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("州域一栏应为半型空格分隔的州域ID");
            }
            return errorMsg;

        }

        public string RegionCoreString
        {
            get
            {
                return ((this.RegionCore != null) ? this.RegionCore.Name : "----");
            }
        }

        public string StatesString
        {
            get
            {
                string str = "";
                foreach (State state in this.States)
                {
                    str = str + state.Name + " ";
                }
                return str;
            }
        }
    }
}

