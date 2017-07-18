using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind980 : ConditionKind
    {
        private string target;

        public override bool CheckConditionKind(Person person)
        {
            return person.SurName == target;
        }

        public override void InitializeParameter(string parameter)
        {
            this.target = parameter;
        }
    }
}

