using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind655 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            foreach (GameObjects.PersonDetail.Skill i in person.Skills.Skills.Values)
            {
                if (i.Influences.HasInfluence(this.number)) return false;
            }
            foreach (GameObjects.PersonDetail.Title t in person.Titles)
            {
                if (t.Influences.HasInfluence(this.number)) return false;
            }
            foreach (GameObjects.PersonDetail.Stunt i in person.Stunts.Stunts.Values)
            {
                if (i.Influences.HasInfluence(this.number)) return false;
            }
            return true;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.number = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

