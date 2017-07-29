using GameManager;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class ArchitectureList : GameObjectList
    {
        public void AddArchitectureWithEvent(Architecture architecture, bool add = true)
        {
            if (add)
            {
                base.Add(architecture);
            }
            //if (Session.MainGame.mainGameScreen != null)
            //{
                architecture.OnHirePerson += new Architecture.HirePerson(this.architecture_OnHirePerson);
                architecture.OnRewardPersons += new Architecture.RewardPersons(this.architecture_OnRewardPersons);
                architecture.OnFacilityCompleted += new Architecture.FacilityCompleted(this.architecture_OnFacilityCompleted);
                architecture.Onfashengzainan  += new Architecture.fashengzainan (this.architecture_Onfashengzainan);
                architecture.OnReleaseCaptiveAfterOccupied += new Architecture.ReleaseCaptiveAfterOccupied(this.architecture_OnReleaseCaptiveAfterOccupied);
                architecture.OnBeginRecentlyAttacked += new Architecture.BeginRecentlyAttacked(this.architecture_OnBeginRecentlyAttacked);
                architecture.OnPopulationEnter += new Architecture.PopulationEnter(this.architecture_OnPopulationEnter);
                architecture.OnPopulationEscape += new Architecture.PopulationEscape(this.architecture_OnPopulationEscape);
                architecture.OnSelectprince += new Architecture.Selectprince(this.architecture_OnSelectprince);
                architecture.OnAppointmayor += new Architecture.Appointmayor(this.architecture_OnAppointmayor);
                architecture.OnZhaoxian += new Architecture.Zhaoxian(this.architecture_OnZhaoxian);
            //}
        }

        public void architecture_OnZhaoxian(Person person, Person leader)
        {
            Session.MainGame.mainGameScreen.Zhaoxian(person, leader);
        }

        public void architecture_OnSelectprince(Person person, Person leader)//立储
        {
            Session.MainGame.mainGameScreen.Selectprince(person, leader);
        }

        public void architecture_OnAppointmayor(Person person, Person leader) //太守
        {
            Session.MainGame.mainGameScreen.Appointmayor(person, leader);
        }

        public void ApplyInfluences()
        {
            foreach (Architecture architecture in base.GameObjects)
            {
                architecture.ApplyInfluences();
            }
        }

        private void architecture_OnBeginRecentlyAttacked(Architecture architecture)
        {
            Session.MainGame.mainGameScreen.ArchitectureBeginRecentlyAttacked(architecture);
        }

        private void architecture_OnFacilityCompleted(Architecture architecture, Facility facility)
        {
            Session.MainGame.mainGameScreen.ArchitectureFacilityCompleted(architecture, facility);
        }

        private void architecture_Onfashengzainan(Architecture architecture, int zainanID)
        {
            Session.MainGame.mainGameScreen.Architecturefashengzainan(architecture, zainanID);
        }

        private void architecture_OnHirePerson(PersonList personList)
        {
            if (personList.Count > 0)
            {
                Session.MainGame.mainGameScreen.ArchitectureHirePerson(personList);
            }
        }

        private void architecture_OnPopulationEnter(Architecture a, int quantity)
        {
            Session.MainGame.mainGameScreen.ArchitecturePopulationEnter(a, quantity);
        }

        private void architecture_OnPopulationEscape(Architecture a, int quantity)
        {
            Session.MainGame.mainGameScreen.ArchitecturePopulationEscape(a, quantity);
        }

        private void architecture_OnReleaseCaptiveAfterOccupied(Architecture architecture, PersonList persons)
        {
            Session.MainGame.mainGameScreen.ArchitectureReleaseCaptiveAfterOccupied(architecture, persons);
        }

        private void architecture_OnRewardPersons(Architecture architecture, GameObjectList personlist)
        {
            Session.MainGame.mainGameScreen.ArchitectureRewardPersons(architecture, personlist);
        }

        public Architecture GetMaxEnduranceArchitecture(Architecture target)
        {
            int endurance = -1;
            Architecture architecture = null;
            foreach (Architecture architecture2 in base.GameObjects)
            {
                if (architecture2 == target)
                {
                    return architecture2;
                }
                if (architecture2.Endurance > endurance)
                {
                    endurance = architecture2.Endurance;
                    architecture = architecture2;
                }
            }
            return architecture;
        }

        public Architecture GetMinEnduranceArchitecture(Architecture target)
        {
            int endurance = 0x7fffffff;
            Architecture architecture = null;
            foreach (Architecture architecture2 in base.GameObjects)
            {
                if (architecture2 == target)
                {
                    return architecture2;
                }
                if (architecture2.Endurance < endurance)
                {
                    endurance = architecture2.Endurance;
                    architecture = architecture2;
                }
            }
            return architecture;
        }

        public void NoFactionDevelop()
        {
            foreach (Architecture architecture in base.GameObjects)
            {
                if (architecture.BelongedFaction == null)
                {
                    architecture.DevelopDayNoFaction();
                }
            }
        }
    }
}

