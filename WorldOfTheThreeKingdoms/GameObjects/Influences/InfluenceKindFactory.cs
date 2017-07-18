using System;


namespace GameObjects.Influences
{

    public static class InfluenceKindFactory
    {
        public static InfluenceKind CreateInfluenceKindByID(int id)
        {
            try
            {
                return (Activator.CreateInstance(Type.GetType("GameObjects.Influences.InfluenceKindPack.InfluenceKind" + id.ToString())) as InfluenceKind);
            }
            catch
            {
                return null;
            }
        }
    }
}

