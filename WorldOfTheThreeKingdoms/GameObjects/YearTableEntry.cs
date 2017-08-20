using GameManager;
using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class YearTableEntry : GameObject
	{
        private GameDate date;
        private string content;
        private FactionList factions;
        private bool isGloballyKnown;
        
        [DataMember]
        public GameDate Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        [DataMember]
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }

        [DataMember]
        public string FactionsString { get; set; }

        public FactionList Factions
        {
            get
            {
                if (factions == null)
                {
                    factions = new FactionList();
                    string[] ids = FactionsString.Split(' ');
                    foreach (string id in ids)
                    {
                        int iid;
                        if (int.TryParse(id, out iid))
                        {
                            factions.Add(Session.Current.Scenario.Factions.GetGameObject(iid));
                        }
                    }
                }
                return factions;
            }
            set
            {
                factions = value;
            }
        }

        public String FactionName1
        {
            get
            {
                if (factions.Count < 1) return "";
                return factions[0].Name;
            }
        }

        public String FactionName2
        {
            get
            {
                if (factions.Count < 2) return "";
                return factions[1].Name;
            }
        }

        public String FactionName3
        {
            get
            {
                if (factions.Count < 3) return "";
                return factions[2].Name;
            }
        }

        public String FactionName4
        {
            get
            {
                if (factions.Count < 4) return "";
                return factions[3].Name;
            }
        }

        public String FactionName5
        {
            get
            {
                if (factions.Count < 5) return "";
                return factions[4].Name;
            }
        }

        [DataMember]
        public bool IsGloballyKnown
        {
            get
            {
                return isGloballyKnown;
            }
            set
            {
                isGloballyKnown = value;
            }
        }

        public YearTableEntry(int id, GameDate date, FactionList faction, string content, bool isGloballyKnown)
        {
            this.ID = id;
            this.date = new GameDate(date);
            this.content = content;
            this.factions = faction;
            this.isGloballyKnown = isGloballyKnown;
        }

        public override string ToString()
        {
            return this.content;
        }
	}
}
