using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class PopulationPack
    {
        [DataMember]
        public int Days;
        [DataMember]
        public int Population;

        public PopulationPack(int days, int population)
        {
            this.Days = days;
            this.Population = population;
        }
    }
}

