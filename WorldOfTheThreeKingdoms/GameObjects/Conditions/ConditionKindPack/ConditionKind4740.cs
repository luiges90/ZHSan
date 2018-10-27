using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind4740 : ConditionKind
    {
        private int type = 0;
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            PersonDetail.Title tThis = person.getTitleOfKind(Session.Current.Scenario.GameCommonData.AllTitleKinds.GetTitleKind(type));
            PersonDetail.Title tOther = ConditionKind.markedPerson.getTitleOfKind(Session.Current.Scenario.GameCommonData.AllTitleKinds.GetTitleKind(type));

            int t1 = tThis != null ? tThis.Level : 0;
            int t2 = tOther != null ? tOther.Level : 0;

            return t1 - t2 >= number;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
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

