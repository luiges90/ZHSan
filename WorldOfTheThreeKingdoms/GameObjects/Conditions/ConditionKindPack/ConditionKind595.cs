using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind595 : ConditionKind
    {
        private float rate = 0f;

        public override bool CheckConditionKind(Person person)
        {
            return (((person.TroopsDefeatedCount + person.RoutedCount) > 0) && ((((float) person.TroopsDefeatedCount) / ((float) (person.RoutedCount))) < this.rate));
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

