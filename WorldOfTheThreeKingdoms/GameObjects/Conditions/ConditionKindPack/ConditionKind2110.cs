using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2110 : ConditionKind
    {
        private double val = 0;

        public override bool CheckConditionKind(Architecture a)
        {
            return (double) a.Commerce / a.CommerceCeiling >= val;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.val = double.Parse(parameter);
            }
            catch
            {
            }
        }

    }
}

