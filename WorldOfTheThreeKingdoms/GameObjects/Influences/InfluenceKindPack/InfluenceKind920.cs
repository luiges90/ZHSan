using GameObjects;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    /*
    [DataContract]public class InfluenceKind920 : InfluenceKind
    {
        private int number = -1;

        public override void ApplyInfluenceKind(Person person)
        {
            if (person.BelongedFaction != null))
            {
                Condition c = person.Scenario.GameCommonData.AllConditions.GetCondition(this.number);
                if (c != null)
                {


            }
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

        public override bool IsVaild(Person person)
        {
            return ((person.BelongedFaction!= null) && (person.LocationTroop.Army != null) && (person.LocationTroop.Army.KindID == this.militaryKindID));
        }
    }*/
}
