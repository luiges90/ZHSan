using GameObjects;
using GameObjects.Conditions;
using GameGlobal;
using System;


using System.Runtime.Serialization;
using GameManager;

namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind670 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return (person.Age < this.number || !((bool)Session.GlobalVariables.PersonNaturalDeath));
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

