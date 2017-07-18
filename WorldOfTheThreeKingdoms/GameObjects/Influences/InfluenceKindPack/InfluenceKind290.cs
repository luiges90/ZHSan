using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind290 : InfluenceKind
    {
        private int militaryTypeID;

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.militaryTypeID = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override bool IsVaild(Person person)
        {
            //兼容旧存档？
            return ((person.LocationTroop != null) && (person.LocationTroop.Army != null) && (person.LocationTroop.Army.Kind != null) && ((int)person.LocationTroop.Army.Kind.Type == this.militaryTypeID));
        }
    }
}

