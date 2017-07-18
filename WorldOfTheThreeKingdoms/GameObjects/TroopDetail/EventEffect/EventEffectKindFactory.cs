using System;


namespace GameObjects.TroopDetail.EventEffect
{

    public class EventEffectKindFactory
    {
        public static EventEffectKind CreateEventEffectKindByID(int id)
        {
            try
            {
                return (Activator.CreateInstance(Type.GetType("GameObjects.TroopDetail.EventEffect.EventEffectKindPack.EventEffectKind" + id.ToString())) as EventEffectKind);
            }
            catch
            {
                return null;
            }
        }
    }
}

