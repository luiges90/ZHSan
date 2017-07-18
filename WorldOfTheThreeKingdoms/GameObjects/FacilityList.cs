using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class FacilityList : GameObjectList
    {
        public void AddFacility(Facility facility)
        {
            base.GameObjects.Add(facility);
        }

        public void DecreaseEndurance(int decrement)
        {
            if (decrement > 0)
            {
                foreach (Facility facility in base.GameObjects)
                {
                    if (facility.location.CanRemoveFacility(facility))
                    {
                        facility.DecreaseEndurance(decrement);
                    }
                }
            }
        }

        public void RecoverEndurance(int extraInc)
        {
            foreach (Facility facility in base.GameObjects)
            {
                facility.RecoverEndurance(extraInc);
            }
        }
    }
}

