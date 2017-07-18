using GameObjects.TroopDetail.EventEffect;
using System;


namespace GameObjects
{

    public class TroopEffectPerson
    {
        public GameObjects.TroopDetail.EventEffect.EventEffect Effect;
        public Person EffectPerson;

        public override string ToString()
        {
            return (this.EffectPerson.Name + " " + this.Effect.Name);
        }
    }
}

