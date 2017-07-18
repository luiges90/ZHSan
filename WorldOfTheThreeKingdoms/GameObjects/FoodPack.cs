using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class FoodPack
    {
        [DataMember]
        public int Days;
        [DataMember]
        public int Food;

        public FoodPack(int food, int days)
        {
            this.Food = food;
            this.Days = days;
        }
    }
}

