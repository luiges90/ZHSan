using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind300 : InfluenceKind
    {
        private int militaryKindID;

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.militaryKindID = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override bool IsVaild(Person person)
        {
            return ((person.LocationTroop != null) && (person.LocationTroop.Army != null) && (person.LocationTroop.Army.KindID == this.militaryKindID));
        }
    }
}

