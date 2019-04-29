using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tools;

namespace GameObjects.Conditions
{
    [DataContract]
    public class Condition : GameObject
    {
        [DataMember]
        public ConditionKind Kind;
        private string parameter;
        private string parameter2;

        public Condition Clone()
        {
            return this.MemberwiseClone() as Condition;
        }

        public static bool CheckConditionList(ICollection<Condition> list, Architecture a, Event e = null)
        {
            if (a == null) return false;
            bool flag = true;
            bool negate = false;
            foreach (Condition condition in list)
            {
                if (condition.Kind.ID == 996)
                {
                    negate = true;
                }
                else if (condition.Kind.ID == 997)
                {
                    if (flag) return true;
                    flag = true;
                }
                else
                {
                    if (negate)
                    {
                        if (e == null)
                        {
                            if (condition.CheckCondition(a))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (condition.CheckCondition(a, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        if (e == null)
                        {
                            if (!condition.CheckCondition(a))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (!condition.CheckCondition(a, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    negate = false;
                }
            }
            return flag;
        }

        public static bool CheckConditionList(ICollection<Condition> list, Person p, Event e = null)
        {
            if (p == null) return false;
            bool flag = true;
            bool negate = false;
            foreach (Condition condition in list)
            {
                //why Kind is null sometimes?
                if (condition.Kind == null)
                {
                    flag = false;
                    continue;
                }
                if (condition.Kind.ID == 996)
                {
                    negate = true;
                }
                else if (condition.Kind.ID == 997)
                {
                    if (flag) return true;
                    flag = true;
                }
                else
                {
                    if (negate)
                    {
                        if (e == null)
                        {
                            if (condition.CheckCondition(p))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (condition.CheckCondition(p, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        if (e == null)
                        {
                            if (!condition.CheckCondition(p))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (!condition.CheckCondition(p, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    negate = false;
                }
            }
            return flag;
        }

        public static bool CheckConditionList(ICollection<Condition> list, Faction f, Event e = null)
        {
            if (f == null) return false;
            bool flag = true;
            bool negate = false;
            foreach (Condition condition in list)
            {
                if (condition.Kind.ID == 996)
                {
                    negate = true;
                }
                else if (condition.Kind.ID == 997)
                {
                    if (flag) return true;
                    flag = true;
                }
                else
                {
                    if (negate)
                    {
                        if (e == null)
                        {
                            if (condition.CheckCondition(f))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (condition.CheckCondition(f, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        if (e == null)
                        {
                            if (!condition.CheckCondition(f))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (!condition.CheckCondition(f, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    negate = false;
                }
            }
            return flag;
        }

        public static bool CheckConditionList(ICollection<Condition> list, Troop t, Event e = null)
        {
            if (t == null) return false;
            bool flag = true;
            bool negate = false;
            foreach (Condition condition in list)
            {
                if (condition.Kind.ID == 996)
                {
                    negate = true;
                }
                else if (condition.Kind.ID == 997)
                {
                    if (flag) return true;
                    flag = true;
                }
                else
                {
                    if (negate)
                    {
                        if (e == null)
                        {
                            if (condition.CheckCondition(t))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (condition.CheckCondition(t, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        if (e == null)
                        {
                            if (!condition.CheckCondition(t))
                            {
                                flag = false;
                            }
                        }
                        else
                        {
                            if (!condition.CheckCondition(t, e))
                            {
                                flag = false;
                            }
                        }
                    }
                    negate = false;
                }
            }
            return flag;
        }

        public bool CheckCondition(Architecture architecture, Event e)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(architecture, e) || this.Kind.CheckConditionKind(architecture);
        }

        public bool CheckCondition(Faction faction, Event e)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(faction, e) || this.Kind.CheckConditionKind(faction);
        }

        public bool CheckCondition(Person person, Event e)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(person, e) || this.Kind.CheckConditionKind(person);
        }

        public bool CheckCondition(Troop troop, Event e)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(troop, e) || this.Kind.CheckConditionKind(troop);
        }

        public bool CheckCondition(Architecture architecture)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(architecture);
        }

        public bool CheckCondition(Faction faction)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);

            if (this.Kind.ID >= 2100 && this.Kind.ID < 3000)
            {
                foreach (Architecture a in faction.Architectures)
                {
                    if (this.Kind.CheckConditionKind(faction))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return this.Kind.CheckConditionKind(faction);
            }
        }

        public bool CheckCondition(Person person)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(person);
        }

        public bool CheckCondition(Troop troop)
        {
            if (this.Kind == null) return false;
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            return this.Kind.CheckConditionKind(troop);
        }
        [DataMember]
        public string Parameter
        {
            get
            {
                return this.parameter;
            }
            set
            {
                this.parameter = value;
            }
        }
        [DataMember]
        public string Parameter2
        {
            get
            {
                return this.parameter2;
            }
            set
            {
                this.parameter2 = value;
            }
        }

        public static List<string> LoadConditionWeightFromString(ConditionTable conditions, string str, out Dictionary<Condition, float> result)
        {
            result = new Dictionary<Condition, float>();

            str = str.NullToString("");

            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            Condition condition = null;
            List<string> errorMsg = new List<string>();
            try
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    if (conditions.Conditions.TryGetValue(int.Parse(strArray[i]), out condition))
                    {
                        result.Add(condition, float.Parse(strArray[i + 1]));
                    }
                    else
                    {
                        errorMsg.Add("条件ID" + int.Parse(strArray[i]) + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("条件AI应为半型空格分隔的条件ID及数值相间");
            }
            return errorMsg;
        }
    }
}

