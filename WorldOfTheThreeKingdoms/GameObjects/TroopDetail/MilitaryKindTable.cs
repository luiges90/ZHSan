using GameManager;
using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class MilitaryKindTable
    {
        [DataMember]
        public Dictionary<int, MilitaryKind> MilitaryKinds = new Dictionary<int, MilitaryKind>();

        public bool AddMilitaryKind(MilitaryKind militaryKind)
        {
            if (this.MilitaryKinds.ContainsKey(militaryKind.ID))
            {
                return false;
            }
            this.MilitaryKinds.Add(militaryKind.ID, militaryKind);
            return true;
        }

        public bool AddMilitaryKind(int kind)
        {
            if (this.MilitaryKinds.ContainsKey(kind))
            {
                return false;
            }
            MilitaryKind militaryKind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(kind);
            if (militaryKind != null)
            {
                this.MilitaryKinds.Add(kind, militaryKind);
            }
            return true;
        }

        public bool RemoveMilitaryKind(int kind)
        {
            if (!this.MilitaryKinds.ContainsKey(kind))
            {
                return false;
            }
            MilitaryKind militaryKind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(kind);
            if (militaryKind != null)
            {
                this.MilitaryKinds.Remove(militaryKind.ID);
            }
            return true;
        }

        public void Clear()
        {
            this.MilitaryKinds.Clear();
        }

        public MilitaryKind GetMilitaryKind(int militaryKindID)
        {
            MilitaryKind kind = null;
            this.MilitaryKinds.TryGetValue(militaryKindID, out kind);
            return kind;
        }

        public GameObjectList GetMilitaryKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (MilitaryKind kind in this.MilitaryKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void AddBasicMilitaryKinds()
        {
            this.AddMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList().GetGameObject(0) as MilitaryKind);
            this.AddMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList().GetGameObject(1) as MilitaryKind);
            this.AddMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList().GetGameObject(2) as MilitaryKind);
            this.AddMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList().GetGameObject(30) as MilitaryKind);
        }

        public List<string> LoadFromString(MilitaryKindTable allMilitaryKinds, string militaryKindIDs)
        {
            List<string> errorMsg = new List<string>();

            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = militaryKindIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            MilitaryKind kind = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allMilitaryKinds.MilitaryKinds.TryGetValue(int.Parse(strArray[i]), out kind))
                    {
                        this.AddMilitaryKind(kind);
                    }
                    else
                    {
                        errorMsg.Add("兵种ID" + int.Parse(strArray[i]) + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("兵种一栏应为半型空格分隔的影响ID");
            }

            return errorMsg;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (MilitaryKind kind in this.MilitaryKinds.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }
    }
}

