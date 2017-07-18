using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class SectionList : GameObjectList
    {
        public void AddSectionWithEvent(Section section)
        {
            base.Add(section);
        }

        public void RemoveSection(Section section)
        {
            base.Remove(section);
        }
    }
}

