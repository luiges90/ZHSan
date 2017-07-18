using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1215 : ConditionKind
    {
        private float rate = 0f;

        public override bool CheckConditionKind(Troop troop)
        {
            return ((troop.Quantity > 0) && ((((float) troop.InjuryQuantity) / ((float) troop.Quantity)) < this.rate));
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

