using GameObjects;
using GameObjects.Conditions;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind631 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return Session.Current.Scenario.GameCommonData.AllTitles.GetTitle(this.number).CanLearn(person);
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

