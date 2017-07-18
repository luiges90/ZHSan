using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind750 : ConditionKind
    {
        private String tag;

        public override bool CheckConditionKind(Person person)
        {
            return person.Tags.Contains(tag + ",");
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.tag = parameter;
            }
            catch
            {
            }
        }
    }
}
