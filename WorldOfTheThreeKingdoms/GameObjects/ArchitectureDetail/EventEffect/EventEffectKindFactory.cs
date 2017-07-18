using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffectKindFactory
    {
        public static EventEffectKind CreateEventEffectKindByID(int id)
        {
            try
            {
                return (Activator.CreateInstance(Type.GetType("GameObjects.ArchitectureDetail.EventEffect.EventEffect" + id.ToString())) as EventEffectKind);
            }
            catch
            {
                return null;
            }
        }
    }
}

