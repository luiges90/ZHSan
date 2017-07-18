using GameObjects.PersonDetail;
using GameObjects.Influences;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class PersonList : GameObjectList
    {
        public void Add(Person person)
        {
            base.Add(person);
        }

        public void AddPersonWithEvent(Person person, bool add = true)
        {
            if (add)
            {
                base.Add(person);
            }
            if (person.Scenario.GameScreen != null)
            {
                person.OnJailBreakSuccess += new Person.JailBreakSuccess(person_OnJailBreakSuccess);
                person.OnJailBreakFailed += new Person.JailBreakFailed(person_OnJailBreakFailed);
                person.OnConvinceSuccess += new Person.ConvinceSuccess(this.person_OnConvinceSuccess);
                person.OnConvinceFailed += new Person.ConvinceFailed(this.person_OnConvinceFailed);
                person.OnInformationObtained += new Person.InformationObtained(this.person_OnInformationObtained);
                person.qingbaoshibaishijian +=new Person.qingbaoshibai(this.person_qingbaoshibai);
               // person.OnSpySuccess += new Person.SpySuccess(this.person_OnSpySuccess);
               // person.OnSpyFailed += new Person.SpyFailed(this.person_OnSpyFailed);
                person.OnDestroySuccess += new Person.DestroySuccess(this.person_OnDestroySuccess);
                person.OnDestroyFailed += new Person.DestroyFailed(this.person_OnDestroyFailed);
                person.OnInstigateSuccess += new Person.InstigateSuccess(this.person_OnInstigateSuccess);
                person.OnInstigateFailed += new Person.InstigateFailed(this.person_OnInstigateFailed);
                person.OnGossipSuccess += new Person.GossipSuccess(this.person_OnGossipSuccess);
                person.OnGossipFailed += new Person.GossipFailed(this.person_OnGossipFailed);
                person.OnSearchFinished += new Person.SearchFinished(this.person_OnSearchFinished);
                //person.OnSpyFound += new Person.SpyFound(this.person_OnSpyFound);
                //person.OnTreasureFound += new Person.TreasureFound(this.person_OnTreasureFound);
               // person.OnShowMessage += new Person.ShowMessage(this.person_OnShowMessage);
                person.OnDeath += new Person.Death(this.person_OnDeath);
                person.OnLeave += new Person.Leave(this.person_OnLeave);
                person.OnBeKilled += new Person.BeKilled(this.person_OnBeKilled);
                person.OnChangeLeader += new Person.ChangeLeader(this.person_OnChangeLeader);
                person.OnDeathChangeFaction += new Person.DeathChangeFaction(this.person_OnDeathChangeFaction);
                person.OnStudyTitleFinished += new Person.StudyTitleFinished(this.person_OnStudyTitleFinished);
                person.OnStudySkillFinished += new Person.StudySkillFinished(this.person_OnStudySkillFinished);
                person.OnStudyStuntFinished += new Person.StudyStuntFinished(this.person_OnStudyStuntFinished);
                person.OnBeAwardedTreasure += new Person.BeAwardedTreasure(this.person_OnBeAwardedTreasure);
                person.OnBeConfiscatedTreasure += new Person.BeConfiscatedTreasure(this.person_OnBeConfiscatedTreasure);
                person.OnCapturedByArchitecture += new Person.CapturedByArchitecture(this.person_OnCapturedByArchitecture);
                person.OnCreateBrother += new Person.CreateBrother(this.person_OnCreateBrother);
                person.OnCreateSister += new Person.CreateSister(person_OnCreateSister);
                person.OnCreateSpouse += new Person.CreateSpouse(person_OnCreateSpouse);
            }
        }

        public void PurifyInfluences()
        {
            foreach (Person person in base.GameObjects)
            {
                if (!person.Scenario.Preparing)
                {
                    foreach (Title t in person.Titles)
                    {
                        t.Influences.PurifyInfluence(person, Applier.Title, t.ID, false);
                    }
                    foreach (Skill s in person.Skills.GetSkillList())
                    {
                        s.Influences.PurifyInfluence(person, Applier.Skill, s.ID, false);
                    }
                    foreach (Stunt s in person.Stunts.GetStuntList())
                    {
                        s.Influences.PurifyInfluence(person, Applier.Stunt, 0, false);
                    }
                    person.PurifyAllTreasures(false);
                }
            }
        }

        public void ApplyInfluences()
        {
            this.PurifyInfluences();
            foreach (Person person in base.GameObjects)
            {
                person.ApplyTitles(false);
                person.ApplySkills(false);
                person.ApplyStunts();
                person.ApplyAllTreasures(false);
            }
        }

        public Person GetMaxCommandPerson()
        {
            int command = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.Command > command)
                {
                    command = person2.Command;
                    person = person2;
                }
            }
            return person;
        }

        public Person GetMaxControversyAbilityPerson()
        {
            int controversyAbility = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.ControversyAbility > controversyAbility)
                {
                    controversyAbility = person2.ControversyAbility;
                    person = person2;
                }
            }
            return person;
        }

        public Person GetMaxIntelligencePerson()
        {
            int intelligence = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.Intelligence > intelligence)
                {
                    intelligence = person2.Intelligence;
                    person = person2;
                }
            }
            return person;
        }

        public Person GetMaxMeritPerson()
        {
            int merit = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.Merit > merit)
                {
                    merit = person2.Merit;
                    person = person2;
                }
            }
            return person;
        }

        public Person GetMaxUntiredMeritPerson()
        {
            int merit = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.UntiredMerit > merit)
                {
                    merit = person2.Merit;
                    person = person2;
                }
            }
            return person;
        }

        public Person GetMaxStrengthPerson()
        {
            int strength = -1;
            Person person = null;
            foreach (Person person2 in base.GameObjects)
            {
                if (person2.Strength > strength)
                {
                    strength = person2.Strength;
                    person = person2;
                }
            }
            return person;
        }

        private void person_OnBeAwardedTreasure(Person person, Treasure t)
        {
            person.Scenario.GameScreen.PersonBeAwardedTreasure(person, t);
        }

        private void person_OnBeConfiscatedTreasure(Person person, Treasure t)
        {
            person.Scenario.GameScreen.PersonBeConfiscatedTreasure(person, t);
        }

        private void person_OnBeKilled(Person person, Architecture location)
        {
            person.Scenario.GameScreen.PersonBeKilled(person, location);
        }

        private void person_OnChangeLeader(Faction faction, Person leader, bool changeName, string oldName)
        {
            faction.Scenario.GameScreen.PersonChangeLeader(faction, leader, changeName, oldName);
        }
        /*
        private void person_OnQuanXiangFailed(Person source, Faction targetFaction)
        {
            source.Scenario.GameScreen.QuanXiangFailed(source, targetFaction);
        }
        */
        private void person_OnConvinceFailed(Person source, Person destination)
        {
            source.Scenario.GameScreen.PersonConvinceFailed(source, destination);
        }

        private void person_OnConvinceSuccess(Person source, Person destination, Faction oldFaction)
        {
            source.Scenario.GameScreen.PersonConvinceSuccess(source, destination, oldFaction);
        }

        private void person_OnDeath(Person person, Person killer, Architecture location, Troop locationTroop)
        {
            person.Scenario.GameScreen.PersonDeath(person, killer, location, locationTroop);
        }

        private void person_OnDeathChangeFaction(Person dead, Person leader, string oldName)
        {
            leader.Scenario.GameScreen.PersonDeathChangeFaction(dead, leader, oldName);
        }

        private void person_OnDestroyFailed(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonDestroyFailed(person, architecture);
        }

        private void person_OnDestroySuccess(Person person, Architecture architecture, int down)
        {
            person.Scenario.GameScreen.PersonDestroySuccess(person, architecture, down);
        }

        private void person_OnGossipFailed(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonGossipFailed(person, architecture);
        }

        private void person_OnGossipSuccess(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonGossipSuccess(person, architecture);
        }

        private void person_OnInformationObtained(Person person, Information information)
        {
            person.Scenario.GameScreen.PersonInformationObtained(person, information);
        }

        
        private void person_qingbaoshibai(Person person)
        {
            person.Scenario.GameScreen.qingbaoshibai(person);
        }

        private void person_OnInstigateFailed(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonInstigateFailed(person, architecture);
        }

        private void person_OnInstigateSuccess(Person person, Architecture architecture, int down)
        {
            person.Scenario.GameScreen.PersonInstigateSuccess(person, architecture, down);
        }

        private void person_OnLeave(Person person, Architecture location)
        {
            person.Scenario.GameScreen.PersonLeave(person, location);
        }

        private void person_OnSearchFinished(Person person, Architecture architecture, SearchResultPack resultPack)
        {
            person.Scenario.GameScreen.PersonSearchFinished(person, architecture, resultPack);
        }
        /*
        private void person_OnShowMessage(Person person, PersonMessage personMessage)
        {
            person.Scenario.GameScreen.PersonShowMessage(person, personMessage);
        }
        
        private void person_OnSpyFailed(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonSpyFailed(person, architecture);
        }

        private void person_OnSpyFound(Person person, Person spy)
        {
            person.Scenario.GameScreen.PersonSpyFound(person, spy);
        }

        private void person_OnSpySuccess(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonSpySuccess(person, architecture);
        }
        */
        private void person_OnStudySkillFinished(Person person, string skillString, bool success)
        {
            person.Scenario.GameScreen.PersonStudySkillFinished(person, skillString, success);
        }

        private void person_OnStudyStuntFinished(Person person, Stunt stunt, bool success)
        {
            person.Scenario.GameScreen.PersonStudyStuntFinished(person, stunt, success);
        }

        private void person_OnStudyTitleFinished(Person person, Title title, bool success)
        {
            person.Scenario.GameScreen.PersonStudyTitleFinished(person, title, success);
        }

        private void person_OnTreasureFound(Person person, Treasure treasure)
        {
            person.Scenario.GameScreen.PersonTreasureFound(person, treasure);
        }

        private void person_OnCapturedByArchitecture(Person person, Architecture architecture)
        {
            person.Scenario.GameScreen.PersonCapturedByArchitecture(person, architecture);
        }

        void person_OnJailBreakSuccess(Person source, Captive destination)
        {
            source.Scenario.GameScreen.PersonJailBreak(source, destination);
        }

        void person_OnJailBreakFailed(Person source, Architecture destination)
        {
            source.Scenario.GameScreen.PersonJailBreakFailed(source, destination);
        }

        void person_OnCreateBrother(Person p, Person q)
        {
            p.Scenario.GameScreen.CreateBrother(p, q);
        }

        void person_OnCreateSister(Person p, Person q)
        {
            p.Scenario.GameScreen.CreateSister(p, q);
        }

        void person_OnCreateSpouse(Person p, Person q)
        {
            p.Scenario.GameScreen.CreateSpouse(p, q);
        }
    }
}
