using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2300 : ConditionKind
    {
        private int cnt;
        private int facilityId;

        public override bool CheckConditionKind(Architecture a)
        {
            int i = 0;
            foreach (Facility f in a.Facilities)
            {
                if (f.KindID == facilityId)
                {
                    i++;
                }
            }
            return i >= cnt;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.cnt = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.facilityId = int.Parse(parameter);
            }
            catch
            {
            }
        }

    }
}

