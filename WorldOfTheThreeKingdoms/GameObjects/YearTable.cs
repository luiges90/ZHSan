using GameObjects;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Platforms;
using Tools;
using GameManager;

namespace GameObjects
{
    [DataContract]
    public class YearTable : GameObjectList
    {
        [DataMember]
        private Dictionary<String, String> yearTableStrings;

        public void Init()
        {
            //TextReader tr = new StreamReader("Content/Data/yearTableStrings.txt");

            yearTableStrings = new Dictionary<String, String>();

            var fileName = "Content/Data/yearTableStrings.txt";

            var lines = Platform.Current.LoadTexts(fileName).NullToEmptyList();

            foreach (var line in lines)
            {
                yearTableStrings[line.Substring(0, line.IndexOf(' '))] = line.Substring(line.IndexOf(' ') + 1);
            }

            //while (tr.Peek() >= 0)
            //{
            //    String line = tr.ReadLine();
            //    yearTableStrings[line.Substring(0, line.IndexOf(' '))] = line.Substring(line.IndexOf(' ') + 1);
            //}
        }
        
        public void addTableEntry(GameDate date, FactionList faction, string content, bool global)
        {
            this.Add(new YearTableEntry(this.GetFreeGameObjectID(), date, faction, content, global) as GameObject);
        }

        public void addTableEntry(int id, GameDate date, FactionList faction, string content, bool global)
        {
            this.Add(new YearTableEntry(id, date, faction, content, global) as GameObject);
        }

        public void AddTableEntry(YearTableEntry entry)
        {
            this.Add(entry as GameObject);
        }

        public void addPersonInGameBiography(Person p, GameDate date, string content)
        {
            if (p.PersonBiography == null)
            {
                p.PersonBiography = new PersonDetail.Biography();
                p.PersonBiography.FactionColor = 52;
                p.PersonBiography.MilitaryKinds.AddBasicMilitaryKinds();
                p.PersonBiography.Brief = "";
                p.PersonBiography.History = "";
                p.PersonBiography.Romance = "";
                p.PersonBiography.InGame = "";
                p.PersonBiography.ID = p.ID;
                Session.Current.Scenario.AllBiographies.AddBiography(p.PersonBiography);
            }
            p.PersonBiography.InGame = date.Year + "年" + date.Month + "月：" + content + '\n' + p.PersonBiography.InGame;
        }

        public static FactionList composeFactionList(params Faction[] f)
        {
            FactionList r = new FactionList();
            foreach (Faction i in f)
            {
                if (i != null)
                {
                    r.Add(i);
                }
            }
            return r;
        }

        public void addOccupyEntry(GameDate date, Troop occupier, Architecture occupied)
        {
            if (occupied.BelongedFaction != null)
            {
                this.addTableEntry(date, composeFactionList(occupier.BelongedFaction, occupied.BelongedFaction),
                    String.Format(yearTableStrings["occupy"], occupier.BelongedFaction.Name, occupier.DisplayName, occupied.BelongedFaction.Name,
                        occupied.Name), true);
                this.addPersonInGameBiography(occupier.Leader, date, String.Format(yearTableStrings["occupy_p"], occupier.BelongedFaction.Name,
                    occupier.DisplayName, occupied.BelongedFaction.Name, occupied.Name));
            }
            else
            {
                this.addTableEntry(date, composeFactionList(occupier.BelongedFaction, occupied.BelongedFaction),
                    String.Format(yearTableStrings["occupyEmpty"], occupier.BelongedFaction.Name, occupier.DisplayName,
                        occupied.Name), true);
                this.addPersonInGameBiography(occupier.Leader, date, String.Format(yearTableStrings["occupyEmpty_p"], occupier.BelongedFaction.Name,
                    occupier.DisplayName, occupied.Name));
            }
        }

        public void addFactionTechniqueCompletedEntry(GameDate date, Faction f, FactionDetail.Technique t)
        {
            /*
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["upgradeTechniqueCompleted"], f.Name, t.Name), false);
             * */
        }

        public void addKingDeathEntry(GameDate date, Person p, Faction oldFaction)
        {
            this.addTableEntry(date, composeFactionList(oldFaction),
                String.Format(yearTableStrings["kingDeath"], oldFaction.Name, p.Name, p.Age), true);
            this.addPersonInGameBiography(oldFaction.Leader, date,
                String.Format(yearTableStrings["kingDeath_p"], oldFaction.Name, p.Name, p.Age));
        }

        public void addChangeKingEntry(GameDate date, Person p, Faction oldFaction, Person oldLeader)
        {
            this.addTableEntry(date, composeFactionList(oldFaction),
                String.Format(yearTableStrings["changeKing"], oldFaction.Name, p.Name, oldLeader.Name), true);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["changeKing_p"], oldFaction.Name, p.Name, oldLeader.Name));
        }

        public void addChangeFactionEntry(GameDate date, Faction oldFaction, Faction newFaction)
        {
            this.addTableEntry(date, composeFactionList(oldFaction, newFaction),
                String.Format(yearTableStrings["changeFaction"], newFaction.Name, oldFaction.Name, oldFaction.Leader.Name), true);
            this.addPersonInGameBiography(newFaction.Leader, date,
                String.Format(yearTableStrings["changeFaction_p"], newFaction.Name, oldFaction.Name, oldFaction.Leader.Name));
        }

        public void addFactionDestroyedEntry(GameDate date, Faction f)
        {
            this.addTableEntry(date, composeFactionList(f), String.Format(yearTableStrings["factionDestroyed"], f.Name), true);
            this.addPersonInGameBiography(f.Leader, date, String.Format(yearTableStrings["factionDestroyed_p"], f.Name));
        }

        public void addChangeCapitalEntry(GameDate date, Faction f, Architecture newCapital)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["changeCapital"], f.Name, newCapital.Name), true);
        }

        public void addNewFactionEntry(GameDate date, Faction oldFaction, Faction newFaction, Architecture foundLocation)
        {
            if (oldFaction != null)
            {
                this.addTableEntry(date, composeFactionList(oldFaction, newFaction),
                    String.Format(yearTableStrings["newFaction"], newFaction.Leader, oldFaction.Name, foundLocation.Name, newFaction.Name), true);
                this.addPersonInGameBiography(newFaction.Leader, date,
                    String.Format(yearTableStrings["newFaction_p"], newFaction.Leader, oldFaction.Name, foundLocation.Name, newFaction.Name));
            }
            else
            {
                this.addTableEntry(date, composeFactionList(oldFaction, newFaction),
                    String.Format(yearTableStrings["newFactionOnEmpty"], newFaction.Leader, foundLocation.Name, newFaction.Name), true);
                this.addPersonInGameBiography(newFaction.Leader, date,
                    String.Format(yearTableStrings["newFactionOnEmpty_p"], newFaction.Leader, foundLocation.Name, newFaction.Name));
            }
        }

        public void addSelfBecomeEmperorEntry(GameDate date, Faction f)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["selfBecomeEmperor"], f.Name, f.Leader.Name), true);
            this.addPersonInGameBiography(f.Leader, date,
                String.Format(yearTableStrings["selfBecomeEmperor_p"], f.Name, f.Leader.Name));
        }

        public void addBecomeEmperorLegallyEntry(GameDate date, Person oldEmperor, Faction f)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["becomeEmperorLegally"], oldEmperor.Name, f.Name, f.Leader.Name), true);
            this.addPersonInGameBiography(f.Leader, date,
                String.Format(yearTableStrings["becomeEmperorLegally_p"], oldEmperor.Name, f.Name, f.Leader.Name));
        }

        public void addExecuteEntry(GameDate date, Person executor, Person executed, Faction oldFaction)
        {
            if (executed.BelongedFaction != null && oldFaction != null)
            {
                this.addTableEntry(date, composeFactionList(executor.BelongedFaction, oldFaction),
                    String.Format(yearTableStrings["execute"], executor.Name, oldFaction.Name, executed.Name, executed.Age), true);
                this.addPersonInGameBiography(executor, date,
                    String.Format(yearTableStrings["execute_p"], executor.Name, oldFaction.Name, executed.Name, executed.Age));
                this.addPersonInGameBiography(executed, date,
                    String.Format(yearTableStrings["execute_q"], executor.Name, oldFaction.Name, executed.Name, executed.Age));
            }
            else
            {
                this.addTableEntry(date, composeFactionList(executor.BelongedFaction),
                    String.Format(yearTableStrings["executeNoFaction"], executor.Name, executed.Name, executed.Age), true);
                this.addPersonInGameBiography(executor, date,
                    String.Format(yearTableStrings["executeNoFaction_p"], executor.Name, executed.Name, executed.Age));
                this.addPersonInGameBiography(executed, date,
                    String.Format(yearTableStrings["executeNoFaction_q"], executor.Name, executed.Name, executed.Age));
            }
        }

        public void addAssassinateEntry(GameDate date, Person killer, Person killed)
        {
            String killedFactionName = killed.BelongedFaction == null ? "" : killed.BelongedFaction.Name;
            this.addTableEntry(date, composeFactionList(killer.BelongedFaction, killed.BelongedFaction),
                String.Format(yearTableStrings["assassinate"], killedFactionName, killed.Name, killed.LocationArchitecture.Name, killedFactionName, killer.Name, killed.Age), true);
            this.addPersonInGameBiography(killer, date,
                String.Format(yearTableStrings["assassinate_p"], killedFactionName, killed.Name, killed.LocationArchitecture.Name, killedFactionName, killer.Name, killed.Age));
            this.addPersonInGameBiography(killed, date,
                String.Format(yearTableStrings["assassinate_q"], killedFactionName, killed.Name, killed.LocationArchitecture.Name, killedFactionName, killer.Name, killed.Age));
        }

        public void addReverseAssassinateEntry(GameDate date, Person killer, Person killed)
        {
            this.addTableEntry(date, composeFactionList(killer.BelongedFaction, killed.BelongedFaction),
                String.Format(yearTableStrings["reverse_assassinate"], killed.BelongedFaction.Name, killed.Name, killed.LocationArchitecture.Name, killer.BelongedFaction.Name, killer.Name, killed.Age), true);
            this.addPersonInGameBiography(killer, date,
                String.Format(yearTableStrings["reverse_assassinate_p"], killed.BelongedFaction.Name, killed.Name, killed.LocationArchitecture.Name, killer.BelongedFaction.Name, killer.Name, killed.Age));
            this.addPersonInGameBiography(killed, date,
                String.Format(yearTableStrings["reverse_assassinate_q"], killed.BelongedFaction.Name, killed.Name, killed.LocationArchitecture.Name, killer.BelongedFaction.Name, killer.Name, killed.Age));
        }

        public void addChildrenBornEntry(GameDate date, Person factionLeader, Person feizi, Person born)
        {
            Faction faction = factionLeader.BelongedFaction;
            if (faction == null)
            {
                if (factionLeader.BelongedArchitecture != null)
                {
                    faction = factionLeader.BelongedArchitecture.BelongedFaction;
                }
                else if (factionLeader.BelongedTroop != null)
                {
                    faction = factionLeader.BelongedTroop.BelongedFaction;
                }
            }
            if (faction != null)
            {
                this.addTableEntry(date, composeFactionList(faction),
                    String.Format(yearTableStrings["childrenBorn"], faction.Name, factionLeader.Name, feizi.Name, (born.Sex ? "女" : "子"), born.Name), false);
                this.addPersonInGameBiography(factionLeader, date,
                    String.Format(yearTableStrings["childrenBorn_p"], faction.Name, factionLeader.Name, feizi.Name, (born.Sex ? "女" : "子"), born.Name));
                this.addPersonInGameBiography(feizi, date,
                    String.Format(yearTableStrings["childrenBorn_q"], faction.Name, factionLeader.Name, feizi.Name, (born.Sex ? "女" : "子"), born.Name));
            }
            else
            {
                this.addPersonInGameBiography(factionLeader, date,
                    String.Format(yearTableStrings["childrenBorn_p"], "", factionLeader.Name, feizi.Name, (born.Sex ? "女" : "子"), born.Name));
                this.addPersonInGameBiography(feizi, date,
                    String.Format(yearTableStrings["childrenBorn_q"], "", factionLeader.Name, feizi.Name, (born.Sex ? "女" : "子"), born.Name));
            }
        }

        public void addGameEndWithUniteEntry(GameDate date, Faction f)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["gameEndWithUnite"], f.Name, f.Leader.Name), true);
            this.addPersonInGameBiography(f.Leader, date,
                String.Format(yearTableStrings["gameEndWithUnite_p"], f.Name, f.Leader.Name));
        }

        public void addAdvanceGuanjueEntry(GameDate date, Faction f, guanjuezhongleilei guanjue)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["advanceGuanjue"], f.Name, guanjue.Name), true);
            this.addPersonInGameBiography(f.Leader, date,
                String.Format(yearTableStrings["advanceGuanjue_p"], f.Name, guanjue.Name));
        }

        public void addSelfAdvanceGuanjueEntry(GameDate date, Faction f, guanjuezhongleilei guanjue)
        {
            this.addTableEntry(date, composeFactionList(f),
                String.Format(yearTableStrings["selfAdvanceGuanjue"], f.Name, guanjue.Name), true);
            this.addPersonInGameBiography(f.Leader, date,
                String.Format(yearTableStrings["selfAdvanceGuanjue_p"], f.Name, guanjue.Name));
        }

        public void addCreateSpouseEntry(GameDate date, Person p1, Person p2)
        {
            this.addTableEntry(date, composeFactionList(p1.BelongedFaction, p2.BelongedFaction),
                String.Format(yearTableStrings["createSpouse"], p1.Name, p2.Name), false);
            this.addPersonInGameBiography(p1, date,
                String.Format(yearTableStrings["createSpouse_p"], p1.Name, p2.Name));
            this.addPersonInGameBiography(p2, date,
                String.Format(yearTableStrings["createSpouse_q"], p1.Name, p2.Name));
        }

        public void addCreateBrotherEntry(GameDate date, Person p1, Person p2)
        {
            this.addTableEntry(date, composeFactionList(p1.BelongedFaction, p2.BelongedFaction),
                String.Format(yearTableStrings["createBrother"], p1.Name, p2.Name), false);
            this.addPersonInGameBiography(p1, date,
                String.Format(yearTableStrings["createBrother_p"], p1.Name, p2.Name));
            this.addPersonInGameBiography(p2, date,
                String.Format(yearTableStrings["createBrother_q"], p1.Name, p2.Name));
        }

        public void addCreateSisterEntry(GameDate date, Person p1, Person p2)
        {
            this.addTableEntry(date, composeFactionList(p1.BelongedFaction, p2.BelongedFaction),
                String.Format(yearTableStrings["createSister"], p1.Name, p2.Name), false);
            this.addPersonInGameBiography(p1, date,
                String.Format(yearTableStrings["createSister_p"], p1.Name, p2.Name));
            this.addPersonInGameBiography(p2, date,
                String.Format(yearTableStrings["createSister_q"], p1.Name, p2.Name));
        }

        public void addPersonDeathEntry(GameDate date, Person p)
        {
            String location = "";
            if (p.BelongedArchitecture != null)
            {
                location = p.BelongedArchitecture.Name;
            }
            else if (p.BelongedTroop != null)
            {
                location = p.BelongedTroop.Name;
            }
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["personDeath"], p.Name, location, p.Age), true);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["personDeath_p"], p.Name, location, p.Age));
        }

        public void addKilledInBattleEntry(GameDate date, Person killer, Person killed)
        {
            String killerFaction = killer.BelongedFaction == null ? "" : killer.BelongedFaction.Name;
            String killedFaction = killed.BelongedFaction == null ? "" : killed.BelongedFaction.Name;
            this.addTableEntry(date, composeFactionList(killer.BelongedFaction, killed.BelongedFaction),
                String.Format(yearTableStrings["personKilledInBattle"], killedFaction, killed.Name, killerFaction, killer.Name, killed.Age), true);
            this.addPersonInGameBiography(killer, date,
                String.Format(yearTableStrings["personKilledInBattle_p"], killedFaction, killed.Name, killerFaction, killer.Name, killed.Age));
            this.addPersonInGameBiography(killed, date,
                String.Format(yearTableStrings["personKilledInBattle_q"], killedFaction, killed.Name, killerFaction, killer.Name, killed.Age));
        }

        public void addDefeatedManyTroopsEntry(GameDate date, Person p, int count)
        {
            if ((count < 100 && count % 10 == 0) || count % 50 == 0)
            {
                this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                    String.Format(yearTableStrings["defeatedManyTroops"], p.Name, count), false);
                this.addPersonInGameBiography(p, date,
                    String.Format(yearTableStrings["defeatedManyTroops_p"], p.Name, count));
            }
        }

        public void addGrownBecomeAvailableEntry(GameDate date, Person p)
        {
            if (p.LocationArchitecture != null)
            {
                if (p.BelongedFaction == null)
                {
                    this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                        String.Format(yearTableStrings["grownBecomeAvailabeNoFaction"], p.Name, p.LocationArchitecture.Name), false);
                    this.addPersonInGameBiography(p, date,
                        String.Format(yearTableStrings["grownBecomeAvailabeNoFaction_p"], p.Name, p.LocationArchitecture.Name));
                }
                else
                {
                    this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                        String.Format(yearTableStrings["grownBecomeAvailable"], p.Name, p.LocationArchitecture.Name, p.BelongedFaction.Name), false);
                    this.addPersonInGameBiography(p, date,
                        String.Format(yearTableStrings["grownBecomeAvailable_p"], p.Name, p.LocationArchitecture.Name, p.BelongedFaction.Name));
                }
            }
        }

        public void addBecomePrincessEntry(GameDate date, Person p, Person leader)
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["becomePrincess"], p.Name, p.BelongedArchitecture.Name, leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["becomePrincess_p"], p.Name, p.BelongedArchitecture.Name, leader.Name));
            this.addPersonInGameBiography(leader, date,
                String.Format(yearTableStrings["becomePrincess_q"], p.Name, p.BelongedArchitecture.Name, leader.Name));
        }

        public void addReleaseFromPrincessEntry(GameDate date, Person p, Person leader)
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["releaseFromPrincess"], p.Name, p.BelongedArchitecture.Name, leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["releaseFromPrincess_p"], p.Name, p.BelongedArchitecture.Name, leader.Name));
            this.addPersonInGameBiography(leader, date,
                String.Format(yearTableStrings["releaseFromPrincess_q"], p.Name, p.BelongedArchitecture.Name, leader.Name));
        }

        public void addSelectPrinceEntry(GameDate date, Person p, Person leader)//立储
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["selectPrince"], p.Name, p.BelongedArchitecture.Name, leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["selectPrince_p"], p.Name, leader.Name));
            this.addPersonInGameBiography(leader, date,
                String.Format(yearTableStrings["selectPrince_q"], p.Name, leader.Name));
        }

        public void addAppointMayorEntry(GameDate date, Person p, Person leader)//太守
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["appointMayor"], p.Name, p.BelongedArchitecture.Name, leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["appointMayor_p"], p.Name, p.BelongedArchitecture.Name, leader.Name));
            //this.addPersonInGameBiography(leader, date,
            // String.Format(yearTableStrings["appointMayor_q"], p.Name, p.BelongedArchitecture.Name, leader.Name));
        }

        public void addZhaoXianEntry(GameDate date, Person p, Person leader)
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction),
                String.Format(yearTableStrings["zhaoXian"], p.Name, p.BelongedArchitecture.Name, leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["zhaoXian_p"], p.Name, p.BelongedArchitecture.Name, leader.Name));
            // this.addPersonInGameBiography(leader, date,
            // String.Format(yearTableStrings["dengYong_q"], p.Name, p.BelongedArchitecture.Name, leader.Name));
        }


        public void addOutOfPrincessEntry(GameDate date, Person p, Faction capturer)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["outOfPrincess_p"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer.Leader.Name));
        }

        public void addOutOfPrincessByLeaderDeathEntry(GameDate date, Person p, Faction capturer)
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction, capturer),
                String.Format(yearTableStrings["outOfPrincessByLeaderDeath"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer.Leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["outOfPrincessByLeaderDeath_p"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer.Leader.Name));
        }

        public void addChangeFactionPrincessEntry(GameDate date, Person p, Faction capturer)
        {
            this.addTableEntry(date, composeFactionList(p.BelongedFaction, capturer),
                String.Format(yearTableStrings["changeFactionPrincess"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer == null ? "贼军" : capturer.Leader.Name), false);
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["changeFactionPrincess_p"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer == null ? "贼军" : capturer.Leader.Name));
            if (capturer != null)
            {
                this.addPersonInGameBiography(capturer.Leader, date,
                    String.Format(yearTableStrings["changeFactionPrincess_q"], p.Name, p.BelongedArchitecture.Name, capturer == null ? "贼军" : capturer.Name, capturer == null ? "贼军" : capturer.Leader.Name));
            }
        }

        public void addBecomeNoFactionEntry(GameDate date, Person p, Faction f)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["becomeNoFaction_p"], f.Name));
        }

        public void addBecomeNoFactionDueToDestructionEntry(GameDate date, Person p, Faction f)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["becomeNoFactionDueToDestruction_p"], f.Name));
        }

        public void addJoinFactionEntry(GameDate date, Person p, Person convincer, Faction f)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["joinFaction_p"], convincer.Name, f.Name));
        }

        public void addChangeFactionEntry(GameDate date, Person p, Person convincer, Faction oldFaction, Faction newFaction)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["changeFaction_p"], convincer.Name, oldFaction.Name, newFaction.Name));
        }

        public void addObtainedTitleEntry(GameDate date, Person p, PersonDetail.Title title)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["obtainTitle_p"], title.Name));
        }

        public void addAwardTitleEntry(GameDate date, Person p, PersonDetail.Title title)
        {
            this.addPersonInGameBiography(p, date,
                String.Format(yearTableStrings["awardTitle_p"], title.Name));
        }

        public void addChallengeEntry(GameDate date, Person winner, Person loser, String loserState)
        {
            this.addPersonInGameBiography(winner, date, String.Format(yearTableStrings["challengeWin_p"], winner.Name, loser.Name, loserState));
            this.addPersonInGameBiography(loser, date, String.Format(yearTableStrings["challengeLose_p"], winner.Name, loser.Name, loserState));
        }

        public void addChallengeDrawEntry(GameDate date, Person p, Person q)
        {
            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["challengeDraw_p"], p.Name, q.Name));
            this.addPersonInGameBiography(q, date, String.Format(yearTableStrings["challengeDraw_q"], p.Name, q.Name));
        }

        public void addChallengeDrawKilledEntry(GameDate date, Person p, Person killed)
        {
            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["challengeDrawKilled_p"], p.Name, killed.Name));
            this.addPersonInGameBiography(killed, date, String.Format(yearTableStrings["challengeDrawKilled_q"], p.Name, killed.Name));
        }

        public void addChallengeDrawBothKilledEntry(GameDate date, Person p, Person q)
        {
            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["challengeDrawBothKilled_p"], p.Name, q.Name));
            this.addPersonInGameBiography(q, date, String.Format(yearTableStrings["challengeDrawBothKilled_q"], p.Name, q.Name));
        }
        /*
        public void addBattleEntry(bool addYearTable, GameDate date, OngoingBattle ob, Person p, ArchitectureList architectures,
            Dictionary<Faction, int> factionDamages)
        {
            
            if (factionDamages.Count < 1) return;

            if (p.BelongedFaction == null) return;

            String allFactionStrings = "";
            FactionList fl = new FactionList();
            foreach (Faction f in factionDamages.Keys)
            {
                allFactionStrings += "、" + (f == null ? "贼军" : f.Name);
                fl.Add(f);
            }
            allFactionStrings = allFactionStrings.Substring(1);

            String architectureStrings = "";
            if (architectures.Count > 0)
            {
                foreach (Architecture a in architectures)
                {
                    architectureStrings += "、" + a.Name;
                }
                architectureStrings = architectureStrings.Substring(1);
            }

            int dayDiff = (date.Year - ob.StartYear) * 360 + (date.Month - ob.StartMonth) * 30 + (date.Day - ob.StartDay) - 5;

            Dictionary<Faction, int> totalDamages = new Dictionary<Faction, int>();
            foreach (Faction f in factionDamages.Keys)
            {
                totalDamages.Add(f, 0);
                foreach (KeyValuePair<Faction, int> pair in factionDamages)
                {
                    if (f != pair.Key)
                    {
                        totalDamages[f] += pair.Value;
                    }
                }
            }

            List<KeyValuePair<Faction, int>> damageList = totalDamages.ToList();
            damageList.Sort((firstPair, nextPair) => -firstPair.Value.CompareTo(nextPair.Value));

            List<KeyValuePair<Faction, int>> selfDamageList = factionDamages.ToList();
            selfDamageList.Sort((firstPair, nextPair) => firstPair.Value.CompareTo(nextPair.Value));

            String victorDescription = "";
            String selfDescription = "";

            if (ob.Skirmish)
            {
                if (damageList.Count > 1)
                {
                    if (damageList[0].Value > damageList[1].Value * 1.5)
                    {
                        victorDescription = (damageList[0].Key == null ? "贼军" : damageList[0].Key.Name) + "大胜";
                    }
                    else if (damageList[0].Value > damageList[1].Value * 1.2)
                    {
                        victorDescription = (damageList[0].Key == null ? "贼军" : damageList[0].Key.Name) + "小胜";
                    }
                    else
                    {
                        victorDescription = "互有胜负";
                    }

                    int rank = 0;
                    foreach (KeyValuePair<Faction, int> i in damageList)
                    {
                        if (i.Key == p.BelongedFaction)
                        {
                            if (rank == 0)
                            {
                                if (damageList[0].Value > damageList[1].Value * 1.5)
                                {
                                    selfDescription = "并大胜敌人";
                                }
                                else if (damageList[0].Value > damageList[1].Value * 1.2)
                                {
                                    selfDescription = "并小胜敌人";
                                }
                                else
                                {
                                    selfDescription = "互有胜负";
                                }
                            }
                            else if (rank == damageList.Count - 1)
                            {
                                if (damageList[0].Value > damageList[rank].Value * 1.5)
                                {
                                    selfDescription = "却遭到大败";
                                }
                                else if (damageList[0].Value > damageList[rank].Value * 1.2)
                                {
                                    selfDescription = "却遭遇小败";
                                }
                                else
                                {
                                    selfDescription = "互有胜负";
                                }
                            }
                        }
                        rank++;
                    }
                }
            }

            if (ob.Skirmish)
            {
                if (addYearTable)
                {
                    this.addTableEntry(date, fl, String.Format(yearTableStrings["battleSkirmish"],
                        p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                        allFactionStrings + "之间",
                        "",
                        architectureStrings,
                        "",
                        "",
                        dayDiff  * Session.Parameters.DayInTurn + "天",
                        victorDescription), false);
                }
                this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleSkirmish_p"],
                    p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                    allFactionStrings + "之间",
                    "",
                    architectureStrings,
                    "",
                    "",
                    dayDiff  * Session.Parameters.DayInTurn + "天",
                    selfDescription));
            }
            else
            {
                foreach (Architecture a in architectures)
                {
                    if (a.BelongedFaction == null
                        || (a.OldFactionName != a.BelongedFaction.Name))
                    {
                        if (addYearTable)
                        {
                            this.addTableEntry(date, fl, String.Format(yearTableStrings["battleOccupy"],
                                p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                allFactionStrings + "之间",
                                a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                a.Name,
                                a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                a.OldFactionName,
                                dayDiff  * Session.Parameters.DayInTurn + "天"), false);
                        }

                        if (p.BelongedFaction == a.BelongedFaction)
                        {
                            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleOccupy_p"],
                                    p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                    allFactionStrings + "之间",
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.Name,
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.OldFactionName,
                                    dayDiff * Session.Parameters.DayInTurn + "天"));
                        }
                        else if (p.BelongedFaction.Name == a.OldFactionName)
                        {
                            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleOccupy_q"],
                                    p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                    allFactionStrings + "之间",
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.Name,
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.OldFactionName,
                                    dayDiff * Session.Parameters.DayInTurn + "天"));
                        }
                        else
                        {
                            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleOccupy_r"],
                                    p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                    allFactionStrings + "之间",
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.Name,
                                    a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                    a.OldFactionName,
                                    dayDiff * Session.Parameters.DayInTurn + "天"));
                        }
                    }
                    else
                    {
                        String offenderString = "";

                        if (fl.Count > 1)
                        {
                            foreach (Faction f in fl)
                            {
                                if (f != a.BelongedFaction)
                                {
                                    offenderString += "、" + f.Name;
                                }
                            }
                            offenderString = offenderString.Substring(1);
                        }

                        if (addYearTable)
                        {
                            this.addTableEntry(date, fl, String.Format(yearTableStrings["battleRetreat"],
                                p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                allFactionStrings + "之间",
                                a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                a.Name,
                                offenderString,
                                a.OldFactionName,
                                dayDiff * Session.Parameters.DayInTurn + "天"), false);
                        }

                        if (a.BelongedFaction == p.BelongedFaction)
                        {
                            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleRetreat_p"],
                                p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                allFactionStrings + "之间",
                                a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                a.Name,
                                offenderString,
                                a.OldFactionName,
                                dayDiff * Session.Parameters.DayInTurn + "天"));
                        }
                        else
                        {
                            this.addPersonInGameBiography(p, date, String.Format(yearTableStrings["battleRetreat_q"],
                                p.BelongedFaction == null ? "贼军" : p.BelongedFaction.Name,
                                allFactionStrings + "之间",
                                a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name,
                                a.Name,
                                offenderString,
                                a.OldFactionName,
                                dayDiff * Session.Parameters.DayInTurn + "天"));
                        }
                    }
                }
            } 
            

        }

    }
    */
    }
}
