using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1425 : ConditionKind
    {
        int number = 0;

        public override bool CheckConditionKind(Troop troop)
        {
            Architecture architectureByPositionNoCheck = troop.Scenario.GetArchitectureByPositionNoCheck(troop.Position);
            return architectureByPositionNoCheck != null && architectureByPositionNoCheck.ID != number;
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

