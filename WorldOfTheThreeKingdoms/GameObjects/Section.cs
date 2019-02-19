using GameManager;
using GameObjects.ArchitectureDetail;
using GameObjects.SectionDetail;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class Section : GameObject
    {
        private SectionAIDetail aiDetail;

        [DataMember]
        public string CitiesControlledString { get; set; }

        public ArchitectureList Architectures = new ArchitectureList();
        public Faction BelongedFaction;
        public Faction OrientationFaction;
        [DataMember]
        public int OrientationFactionID;
        public Section OrientationSection;
        [DataMember]
        public int OrientationSectionID;
        public State OrientationState;
        [DataMember]
        public int OrientationStateID;
        public Architecture OrientationArchitecture;
        [DataMember]
        public int OrientationArchitectureID;

        public void Init()
        {
            Architectures = new ArchitectureList();

        }

        public void EnsureSectionArchitecture()
        {
            foreach (Architecture a in Session.Current.Scenario.Architectures)
            {
                if (a.BelongedSection == this && !this.Architectures.GameObjects.Contains(a))
                {
                    this.Architectures.Add(a);
                }
            }
        }

        public void AddArchitecture(Architecture architecture)
        {
            this.Architectures.Add(architecture);
            architecture.BelongedSection = this;
        }

        public void AI()
        {
        }

        public void AIIntraTransfer()
        {
            if (this.Architectures.Count > 1)
            {
                this.BelongedFaction.AITransferPlanning(this.Architectures);
            }
        }

        public void AIInterTransfer()
        {
            if (this.OrientationSection != null &&
                (this.AIDetail.AllowFoodTransfer || this.AIDetail.AllowFundTransfer || this.AIDetail.AllowMilitaryTransfer))
            {
                this.BelongedFaction.AllocationTransfer(this.Architectures, this.OrientationSection.Architectures,
                    (this.AIDetail.AllowFoodTransfer || this.AIDetail.AllowFundTransfer), false, this.AIDetail.AllowMilitaryTransfer);
                if (GameObject.Chance(10))
                {
                    this.BelongedFaction.FullTransfer(this.Architectures, this.OrientationSection.Architectures,
                        (this.AIDetail.AllowFoodTransfer || this.AIDetail.AllowFundTransfer), false, this.AIDetail.AllowMilitaryTransfer);
                }
            }
        }

        public int GetFrontScale()
        {
            if (this.ArchitectureCount == 0)
            {
                return 0;
            }
            int num = 0;
            foreach (Architecture architecture in this.Architectures)
            {
                if (architecture.FrontLine)
                {
                    num++;
                }
            }
            return ((num * 100) / this.ArchitectureCount);
        }

        public int GetHostileScale()
        {
            if (this.ArchitectureCount == 0)
            {
                return 0;
            }
            int num = 0;
            foreach (Architecture architecture in this.Architectures)
            {
                if (architecture.HostileLine)
                {
                    num++;
                }
            }
            return ((num * 100) / this.ArchitectureCount);
        }

        public ArchitectureList GetOtherArchitectureList(Architecture architecture)
        {
            ArchitectureList list = new ArchitectureList();
            foreach (Architecture architecture2 in this.Architectures)
            {
                if (architecture2 != architecture)
                {
                    list.Add(architecture2);
                }
            }
            return list;
        }

        public bool HasArchitecture(Architecture a)
        {
            return this.Architectures.HasGameObject(a);
        }

        public List<string> LoadArchitecturesFromString(ArchitectureList architectures, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Architectures.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Architecture gameObject = architectures.GetGameObject(int.Parse(str)) as Architecture;
                    if (gameObject != null)
                    {
                        this.AddArchitecture(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("建筑ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("建筑列表应为半型空格分隔的建筑ID");
            }
            if (this.ArchitectureCount == 0)
            {
                errorMsg.Add("没有建筑");
            }
            return errorMsg;
        }

        public void RefreshSectionName()
        {
            if (this.ArchitectureCount > 0)
            {
                if (this.ArchitectureCount > 1)
                {
                    this.Architectures.PropertyName = "Population";
                    this.Architectures.IsNumber = true;
                    this.Architectures.ReSort();
                }
                base.Name = this.Architectures[0].Name + "军区";
            }
            else
            {
                base.Name = "----";
            }
        }

        public void RemoveArchitecture(Architecture architecture)
        {
            this.Architectures.Remove(architecture);
            architecture.BelongedSection = null;
        }

        [DataMember]
        public int AIDetailIDString { get; set; }

        public SectionAIDetail AIDetail
        {
            get
            {
                return this.aiDetail;
            }
            set
            {
                this.aiDetail = value;
            }
        }

        public string AIDetailString
        {
            get
            {
                return this.aiDetail == null ? "————" : this.aiDetail.Name;
            }
        }

        public int ArchitectureCount
        {
            get
            {
                return this.Architectures.Count;
            }
        }

        public int ArchitectureScale
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.AreaCount;
                }
                return num;
            }
        }

        public int Army
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.ArmyQuantity;
                }
                foreach (Troop troop in this.BelongedFaction.Troops)
                {
                    if (!(troop.Destroyed || !this.HasArchitecture(troop.StartingArchitecture)))
                    {
                        num += troop.Quantity;
                    }
                }
                return num;
            }
        }

        public int ArmyScale
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.ArmyScale;
                }
                foreach (Troop troop in this.BelongedFaction.Troops)
                {
                    if (!(troop.Destroyed || !this.HasArchitecture(troop.StartingArchitecture)))
                    {
                        num += troop.Army.Scales;
                    }
                }
                return num;
            }
        }

        public string FactionString
        {
            get
            {
                return this.BelongedFaction.Name;
            }
        }

        public int Food
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Food;
                }
                return num;
            }
        }

        public int Fund
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Fund;
                }
                return num;
            }
        }

        public Architecture MaxPopulationArchitecture
        {
            get
            {
                if (this.ArchitectureCount == 0)
                {
                    return null;
                }
                if (this.ArchitectureCount != 1)
                {
                    this.Architectures.PropertyName = "Population";
                    this.Architectures.IsNumber = true;
                    this.Architectures.ReSort();
                }
                return (this.Architectures[0] as Architecture);
            }
        }

        public int MilitaryCount
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.MilitaryCount;
                }
                return num + this.TroopCount;
            }
        }

        public string OrientationString
        {
            get
            {
                if (this.AIDetail != null)
                {
                    switch (this.AIDetail.OrientationKind)
                    {
                        case SectionOrientationKind.无:
                            return "----";

                        case SectionOrientationKind.军区:
                            return ((this.OrientationSection != null) ? this.OrientationSection.Name : "----");

                        case SectionOrientationKind.势力:
                            return ((this.OrientationFaction != null) ? this.OrientationFaction.Name : "----");

                        case SectionOrientationKind.州域:
                            return ((this.OrientationState != null) ? this.OrientationState.Name : "----");

                        case SectionOrientationKind.建筑:
                            return ((this.OrientationArchitecture != null) ? this.OrientationArchitecture.Name : "----");
                    }
                }
                return "----";
            }
        }

        public PersonList Persons
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Architecture architecture in this.Architectures)
                {
                    foreach (Person p in architecture.Persons)
                    {
                        result.Add(p);
                    }
                }
                foreach (Troop troop in this.BelongedFaction.Troops)
                {
                    if (troop.StartingArchitecture.BelongedSection == this)
                    {
                        foreach (Person p in troop.Persons)
                        {
                            result.Add(p);
                        }
                    }
                }
                return result;
            }
        }

        public int PersonCount
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.GetAllPersons().Count;
                }
                foreach (Troop troop in this.BelongedFaction.Troops)
                {
                    if (troop.StartingArchitecture.BelongedSection == this)
                    {
                        num += troop.PersonCount;
                    }
                }
                return num;
            }
        }

        public int Population
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Population;
                }
                return num;
            }
        }

        public int TroopCount
        {
            get
            {
                int num = 0;
                foreach (Troop troop in this.BelongedFaction.Troops)
                {
                    if (!troop.Destroyed && troop.StartingArchitecture.BelongedSection == this)
                    {
                        num++;
                    }
                }
                return num;
            }
        }
    }
}

