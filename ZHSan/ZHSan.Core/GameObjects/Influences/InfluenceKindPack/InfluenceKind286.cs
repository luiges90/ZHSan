using GameObjects.Conditions;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects.Influences.InfluenceKindPack;

[DataContract]
public class InfluenceKind286 : InfluenceKind
{
    public override bool IsVaild(Influence influence, Person person)
    {
        Condition condition = Session.Current.Scenario.GameCommonData.AllConditions.Get(influence.GetIntParam());
        
        return condition.CheckCondition(person);
    }
}