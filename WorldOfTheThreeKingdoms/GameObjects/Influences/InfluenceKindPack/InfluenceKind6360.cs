using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6360 : InfluenceKind
    {
        private int chance;
        private int fixAt;

        public override void ApplyInfluenceKind(Person person)
        {
            person.childrenLoyalty += this.fixAt;
            person.childrenLoyaltyRate += this.chance;
        }


        public override void PurifyInfluenceKind(Person person)
        {
            person.childrenLoyalty -= this.fixAt;
            person.childrenLoyaltyRate -= this.chance;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.chance = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.fixAt = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

