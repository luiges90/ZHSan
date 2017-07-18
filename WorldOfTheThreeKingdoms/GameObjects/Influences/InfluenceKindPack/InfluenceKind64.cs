using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind64 : InfluenceKind
    {
        private int multiple = 1;

        public override void ApplyInfluenceKind(Person person)
        {
            person.MultipleOfMoraleTechniquePoint += this.multiple - 1;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.MultipleOfMoraleTechniquePoint -= this.multiple - 1;
        }

        public override void InitializeParameter(string parameter)
        {
            if (int.TryParse(parameter, out this.multiple))
            {

            }
        }
    }
}

