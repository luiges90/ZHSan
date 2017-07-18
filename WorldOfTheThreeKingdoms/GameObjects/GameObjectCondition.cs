using System;


namespace GameObjects
{

    public class GameObjectCondition
    {
        public int LEG;
        public string PropertyName;
        public object PropertyValue;

        public GameObjectCondition(string propertyName, object propertyValue)
        {
            this.PropertyName = "";
            this.PropertyValue = null;
            this.LEG = 0;
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
        }

        public GameObjectCondition(string propertyName, object propertyValue, int leg)
        {
            this.PropertyName = "";
            this.PropertyValue = null;
            this.LEG = 0;
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.LEG = leg;
        }
    }
}

