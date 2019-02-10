using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind990 : ConditionKind
    {
        private string target;

        public override bool CheckConditionKind(Person person)
        {
            return person.FirstName == target;
        }

        public override void InitializeParameter(string parameter)
        {
            this.target = parameter;
        }
    }
}

