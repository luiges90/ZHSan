using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class SkillTable
    {
        [DataMember]
        public Dictionary<int, Skill> Skills = new Dictionary<int, Skill>();

        public bool AddSkill(Skill skill)
        {
            if (this.Skills.ContainsKey(skill.ID))
            {
                return false;
            }
            this.Skills.Add(skill.ID, skill);
            return true;
        }

        public void Clear()
        {
            this.Skills.Clear();
        }

        public Skill GetSkill(int skillID)
        {
            Skill skill = null;
            this.Skills.TryGetValue(skillID, out skill);
            return skill;
        }

        public GameObjectList GetSkillList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Skill skill in this.Skills.Values)
            {
                list.Add(skill);
            }
            return list;
        }

        public void LoadFromString(SkillTable allSkills, string skillIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = skillIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Skill skill = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allSkills.Skills.TryGetValue(int.Parse(strArray[i]), out skill))
                {
                    this.AddSkill(skill);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (Skill skill in this.Skills.Values)
            {
                str = str + skill.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.Skills.Count;
            }
        }
    }
}

