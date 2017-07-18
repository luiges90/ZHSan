using System;


namespace GameObjects.Conditions
{

    public static class ConditionKindFactory
    {
        public static ConditionKind CreateConditionKindByID(int id)
        {
            try
            {
                return (Activator.CreateInstance(Type.GetType("GameObjects.Conditions.ConditionKindPack.ConditionKind" + id.ToString())) as ConditionKind);
            }
            catch
            {
                return null;
            }
        }
    }
}

