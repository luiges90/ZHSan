using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Conditions
{
    [DataContract]
    public class ConditionTable
    {
        [DataMember]
        public Dictionary<int, Condition> Conditions = new Dictionary<int, Condition>();

        public bool AddCondition(Condition influence)
        {
            if (this.Conditions.ContainsKey(influence.ID))
            {
                return false;
            }
            this.Conditions.Add(influence.ID, influence);
            return true;
        }

        public void Clear()
        {
            this.Conditions.Clear();
        }

        public Condition GetCondition(int influenceID)
        {
            Condition condition = null;
            this.Conditions.TryGetValue(influenceID, out condition);
            return condition;
        }

        public GameObjectList GetConditionList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Condition condition in this.Conditions.Values)
            {
                list.Add(condition);
            }
            return list;
        }

        public List<string> LoadFromString(ConditionTable allConditions, string conditionIDs)
        {
            List<string> errorMsg = new List<string>();

            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = conditionIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Condition condition = null;

            for (int i = 0; i < strArray.Length; i++)
            {
                int arrayI;
                if (int.TryParse(strArray[i], out arrayI))
                {
                    if (allConditions.Conditions.TryGetValue(int.Parse(strArray[i]), out condition))
                    {
                        this.AddCondition(condition);
                    }
                    else
                    {
                        errorMsg.Add("条件ID" + int.Parse(strArray[i]) + "不存在");
                    }
                }
                else
                {
                    errorMsg.Add("条件一栏应为半型空格分隔的条件ID");
                }
            }
            //}
            //catch
            //{
            //    errorMsg.Add("条件一栏应为半型空格分隔的条件ID");
            //}
            return errorMsg;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (Condition condition in this.Conditions.Values)
            {
                str = str + condition.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.Conditions.Count;
            }
        }

        public bool CheckCondition(Person p)
        {
            return Condition.CheckConditionList(this.Conditions.Values, p);
        }

        public bool CheckPersonalityCondition(Person p)
        {
            foreach (Condition j in this.Conditions.Values)
            {
                if (j.Kind.ID == 600 || j.Kind.ID == 610) //check personality kind only
                {
                    if (!j.CheckCondition(p))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

