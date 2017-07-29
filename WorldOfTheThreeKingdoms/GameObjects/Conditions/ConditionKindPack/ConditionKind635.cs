using GameObjects;
using GameObjects.Conditions;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind635 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return !person.RealTitles.Contains(Session.Current.Scenario.GameCommonData.AllTitles.GetTitle(number));
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

