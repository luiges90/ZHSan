using GameObjects;
using GameObjects.TroopDetail;
using System;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class Biography : GameObject
    {
        private string brief = "";
        private int factionColor;
        private string history = "";

        [DataMember]
        public string MilitaryKindsString { get; set; }

        public MilitaryKindTable MilitaryKinds = new MilitaryKindTable();
        private string romance = "";
        private string ingame = "";

        public void Init()
        {
            MilitaryKinds = new MilitaryKindTable();
        }

        [DataMember]
        public string Brief
        {
            get
            {
                return this.brief;
            }
            set
            {
                this.brief = value;
            }
        }
        [DataMember]
        public int FactionColor
        {
            get
            {
                return this.factionColor;
            }
            set
            {
                this.factionColor = value;
            }
        }
        [DataMember]
        public string History
        {
            get
            {
                return this.history;
            }
            set
            {
                this.history = value;
            }
        }
        [DataMember]
        public string Romance
        {
            get
            {
                return this.romance;
            }
            set
            {
                this.romance = value;
            }
        }
        [DataMember]
        public string InGame
        {
            get
            {
                return this.ingame;
            }
            set
            {
                this.ingame = value;
            }
        }
    }
}

