using GameManager;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class FactionListWithQueue : FactionList
    {
        private Queue<Faction> factionQueue = new Queue<Faction>();
        public Faction RunningFaction;

        [DataMember]
        public string FactionQueue { get; set; }

        public void BuildQueue(bool preUserControlFinished)
        {
            factionQueue = new Queue<Faction>();

            GameObjectList list = base.GetList();
            list.PropertyName = "Power";
            list.IsNumber = true;
            list.SmallToBig = true;
            list.ReSort();
            foreach (Faction faction in list)
            {
                this.SetFactionInQueue(faction, preUserControlFinished);
            }
        }

        public bool HasFactionInQueue(FactionList list)
        {
            foreach (Faction faction in list)
            {
                if (this.IsFactionInQueue(faction))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFactionInQueue(Faction faction)
        {
            foreach (Faction faction2 in this.factionQueue)
            {
                if (faction2 == faction)
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadQueueFromString(string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.factionQueue.Clear();
            foreach (string str in strArray)
            {
                Faction gameObject = base.GetGameObject(int.Parse(str)) as Faction;
                if (gameObject != null)
                {
                    this.factionQueue.Enqueue(gameObject);
                }
            }
        }

        public void RunQueue()
        {
            if (this.RunningFaction != null)
            {
                if (this.RunningFaction.Run())
                {
                    this.RunningFaction = null;
                }
            }
            else if (!this.QueueEmpty)
            {
                this.RunningFaction = this.factionQueue.Dequeue();
                if (this.RunningFaction != null)
                {
                    if (this.RunningFaction.Leader.BelongedFaction == null)
                    {
                        this.RunningFaction = null;
                    }
                    else
                    {
                        Session.Current.Scenario.CurrentFaction = this.RunningFaction;
                        if (this.RunningFaction.Run())
                        {
                            this.RunningFaction = null;
                        }
                    }
                }
            }
        }

        public string SaveQueueToString()
        {
            string str = "";
            if (this.factionQueue != null)
            { 
                foreach (Faction faction in this.factionQueue)
                {
                    str = str + " " + faction.ID.ToString();
                }
            }
            return str;
        }

        public void SetControlling(bool controlling)
        {
            foreach (Faction faction in base.GameObjects)
            {
                faction.Controlling = controlling;
            }
        }

        private void SetFactionInQueue(Faction faction, bool preUserControlFinished)
        {
            this.factionQueue.Enqueue(faction);
            faction.Passed = false;
            faction.PreUserControlFinished = preUserControlFinished;
            faction.AIFinished = false;
        }

        public bool QueueEmpty
        {
            get
            {
                return (this.factionQueue.Count == 0);
            }
        }
    }
}

