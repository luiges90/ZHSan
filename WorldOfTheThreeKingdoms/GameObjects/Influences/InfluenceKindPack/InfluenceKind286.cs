using GameObjects;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;
using GameManager;

namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind286 : InfluenceKind
    {
        private int conditionID;

       
        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.conditionID = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override bool IsVaild(Person person)
        {
            Condition t = Session.Current.Scenario.GameCommonData.AllConditions.GetCondition(conditionID);
            if (t.CheckCondition(person))
            {
                return true;
            }
            return false;
        }
    }
}
        
        
         




    
