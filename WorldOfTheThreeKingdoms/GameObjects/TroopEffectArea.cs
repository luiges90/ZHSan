using GameObjects.TroopDetail.EventEffect;
using System;


namespace GameObjects
{

    public class TroopEffectArea
    {
        public GameObjects.TroopDetail.EventEffect.EventEffect Effect;
        public EffectAreaKind Kind;

        public override string ToString()
        {
            return (this.Kind + " " + this.Effect.Name);
        }
    }
}

