using GameManager;
using System.Runtime.Serialization;
using GameObjects.Influences;

namespace GameObjects.ArchitectureDetail.EventEffect;

[DataContract]
public class EventEffect330 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Person person, Event e)
    {
        var titleId = eventEffect.GetIntParam();
        var title = Session.Current.Scenario.GameCommonData.AllTitles.GetTitle(titleId);

        foreach (var t in person.RealTitles)
        {
            if (t.Kind.Equals(title.Kind))
            {
                title.Influences.PurifyInfluence(person, Applier.Title, titleId);
                person.RealTitles.Remove(title);
            }
        }
    }
}