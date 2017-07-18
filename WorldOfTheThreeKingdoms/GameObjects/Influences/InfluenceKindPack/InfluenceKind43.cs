using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind43 : InfluenceKind
    {
        private float multiple = 1;

        public override void ApplyInfluenceKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.MultipleOfStratagemTechniquePoint += this.multiple - 1;
            }
        }

        public override void PurifyInfluenceKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.MultipleOfStratagemTechniquePoint -= this.multiple - 1;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.multiple = float.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

