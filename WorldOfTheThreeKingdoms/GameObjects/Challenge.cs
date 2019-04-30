using GameGlobal;
using GameObjects.Animations;
using GameObjects.Influences;
using GameObjects.MapDetail;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using GameObjects.FactionDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using GameManager;
using WorldOfTheThreeKingdoms.GameScreens.ScreenLayers;

namespace GameObjects
{
	public class Challenge
	{
        private bool ChallengeOftenShow = false;  //暴击必然触发单挑，调试单挑程序用，默认为false


        public void ChallgenEvent(Troop sourceTroop, Troop troop, TroopDamage damage)
        {
            if ((!sourceTroop.IsFriendly(troop.BelongedFaction) && !sourceTroop.AirOffence) && (this.ChallengeOftenShow || GameObject.Chance(20)))
            {
                Person maxStrengthPerson = sourceTroop.Persons.GetMaxStrengthPerson();
                Person destination = troop.Persons.GetMaxStrengthPerson();
                if (((maxStrengthPerson != null) && (destination != null)) && (this.ChallengeOftenShow || (GameObject.Random(GameObject.Square(destination.Calmness)) < GameObject.Random(0x19))))
                {
                    if (maxStrengthPerson.IsCivil() || destination.IsCivil())  //文官不单挑
                    {
                        return;
                    }
                    int chance = Person.ChanlengeWinningChance(maxStrengthPerson, destination);
                    if (this.ChallengeOftenShow || (maxStrengthPerson.Character.ChallengeChance + chance) >= 60)
                    {
                        this.challengeHappen(damage, maxStrengthPerson, destination, chance);
                    }
                }
            }
        }

        private void challengeHappen(TroopDamage damage, Person maxStrengthPerson, Person destination, int chance)
        {
            int flag = 0;
            //damage.ChallengeHappened = true;  //单挑发生
            if ((Setting.Current.GlobalVariables.ShowChallengeAnimation) &&   //Session.GlobalVariables.ShowChallengeAnimation
                (Session.Current.Scenario.IsPlayer(maxStrengthPerson.BelongedFaction) || Session.Current.Scenario.IsPlayer(destination.BelongedFaction) || (Session.GlobalVariables.SkyEye && Session.GlobalVariables.SkyEyeSimpleNotification) || this.ChallengeOftenShow))  //单挑双方有玩家的武将才演示
            {
                
                try
                {
                    Session.MainGame.mainGameScreen.EnableUpdate = false;

                    challengeShow(damage, maxStrengthPerson, destination);

                    //int returnValue;
                    //returnValue = this.challengeShow(maxStrengthPerson, destination);
                    //returnValue = 10;

                    //if (returnValue >= -4 && returnValue <= 10 && returnValue != 0)
                    //{
                    //    flag = returnValue;
                    //}
                    //else   //返回值出错时避免跳出
                    //{
                    //    flag = (GameObject.Chance(chance) ? 1 : 2);
                    //}

                }
                catch
                {
                    flag = (GameObject.Chance(chance) ? 1 : 2);
                    damage.ChallengeHappened = true;  //单挑发生
                }
            }
            else
            {
                flag = (GameObject.Chance(chance) ? 1 : 2);
                damage.ChallengeHappened = true;  //单挑发生
            }

            //flag = -4;
            if (damage.ChallengeHappened)
            {
                damage.ChallengeResult = flag;
                damage.ChallengeSourcePerson = maxStrengthPerson;
                damage.ChallengeDestinationPerson = destination;
            }

        }

        private string challengePersonString(Person person)
        {
            //武将ID,姓,名,字,性别(0,女、1,男),头像编号,身份(0 盗贼，1 武将，2君主)
            //生命,体力,力量,敏捷,
            //武艺,统御,智谋,政治,魅力,
            //相性,勇猛,冷静,义理,野心,名声,
            //坐骑(1、没有马；300、赤兔马；301、的卢；302、绝影；303、爪黄飞电；304、大宛马),
            //忠诚度,当前所属势力声望,
            //当前效力君主的魅力(没有君主传-1),当前效力君主的相性(没有君主传-1)


            string para;
            para = person.ID.ToString() + "," + person.SurName + "," + person.GivenName + "," + (person.CalledName == "" ? "无" : person.CalledName) + "," + (person.Sex ? "0" : "1") + "," + person.PictureIndex.ToString() + ","+person.Identity().ToString()+",";
            para += person.ChallengeStrength.ToString() + "," + person.ChallengeStrength.ToString() + "," + person.ChallengeStrength.ToString() + "," + person.ChallengeStrength.ToString() + ",";
            para += person.ChallengeStrength.ToString() + "," + person.Command.ToString() + "," + person.Intelligence.ToString() + "," + person.Politics.ToString() + "," + person.Glamour.ToString() + ",";
            para += person.Ideal.ToString() + "," + person.Braveness.ToString() + "," + person.Calmness.ToString() + "," + person.PersonalLoyalty.ToString() + "," + person.Ambition.ToString() + "," + person.Reputation.ToString() + ",";
            ///////////////////////////////判断有没有宝物马
            if (person.HasHorse() == -1)  //游戏中用-1表示没有马，而单挑程序中用1表示没有马
            {
                para += "1" + ",";
            }
            else
            {
                para += person.HasHorse().ToString() + ",";
            }
            /////////////////////////////////
            para += person.Loyalty.ToString() + "," + (person.BelongedFaction == null ? 0 : person.BelongedFaction.Reputation).ToString()+",";
            para += (person.BelongedFaction == null ? -1 : person.BelongedFaction.Leader.Glamour).ToString() + "," + (person.BelongedFaction == null ? -1 : person.BelongedFaction.Leader.Ideal).ToString();
            para += "\r\n";
            return para;
        }

        private void challengeShow(TroopDamage damage, Person maxStrengthPerson, Person destination)
        {
            DantiaoLayer.Persons = new List<Person>()
            {
                maxStrengthPerson,
                destination
            };

            damage.ChallengeStarted = true;

            Session.MainGame.mainGameScreen.cloudLayer.Reverse = true;

            Session.MainGame.mainGameScreen.cloudLayer.Start();

            Session.MainGame.mainGameScreen.dantiaoLayer = new DantiaoLayer(DantiaoLayer.Persons[DantiaoLayer.Persons.Count - 2], DantiaoLayer.Persons[DantiaoLayer.Persons.Count - 1]);

            Session.MainGame.mainGameScreen.dantiaoLayer.damage = damage;

            //throw new NotImplementedException("跨平台單挑程序尚未實現！");

            /////////////////////////////////////////////////调用单挑程序
            //string fileName = @"Dantiao\start.exe";

            //string para = "";
            //para += this.challengePersonString(maxStrengthPerson);
            //para += this.challengePersonString(destination);

            //Process myProcess = new Process();
            //ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(fileName, para);
            //myProcess.StartInfo = myProcessStartInfo;
            //myProcess.Start();
            //while (!myProcess.HasExited)
            //{

            //    myProcess.WaitForExit();

            //}

            //return myProcess.ExitCode;
        }

        public void HandleChallengeResult(TroopDamage damage, int result, Troop sourceTroop, Person sourcePerson, Troop destinationTroop, Person destinationPerson)
        {
            Session.MainGame.mainGameScreen.TroopPersonChallenge(result, sourceTroop, sourcePerson, destinationTroop, destinationPerson);

            switch (result)
            {
                case 1: //P1武将胜利
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson, "被打下馬");
                    damage.SourceMoraleChange += 20;
                    damage.DestinationMoraleChange -= 20;
                    damage.SourceCombativityChange += 20;
                    damage.DestinationCombativityChange -= 20;  //第2只军队战意下降
                    break;
                case 2: //2：P2武将胜利
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson, "被打下馬");
                    damage.SourceMoraleChange -= 20;
                    damage.DestinationMoraleChange += 20;
                    damage.SourceCombativityChange -= 20;
                    damage.DestinationCombativityChange += 20;
                    break;
                case 3: //3：P1武将被杀
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson, "被擊殺");
                    this.challengePersonDie(sourcePerson,sourceTroop, destinationPerson);
                    damage.SourceMoraleChange -= 30;
                    damage.DestinationMoraleChange += 30;
                    damage.SourceCombativityChange -= 30;
                    damage.DestinationCombativityChange += 30;
                    break;
                case 4: //4：P2武将被杀
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson, "被擊殺");
                    this.challengePersonDie(destinationPerson, destinationTroop, sourcePerson);
                    damage.SourceMoraleChange += 30;
                    damage.DestinationMoraleChange -= 30;
                    damage.SourceCombativityChange += 30;
                    damage.DestinationCombativityChange -= 30;
                    break;
                case 5: //5：P1武将逃跑
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson, "逃跑");
                    damage.SourceMoraleChange -= 20;
                    damage.DestinationMoraleChange += 20;
                    break;
                case 6: //6：P2武将逃跑
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson, "逃跑");
                    damage.SourceMoraleChange += 20;
                    damage.DestinationMoraleChange -= 20;
                    break;
                case 7: //7、P1武将被俘虏
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson, "被俘虜");
                    destinationTroop.CatchCaptiveFromTroop(sourcePerson);
                    sourceTroop.RefreshAfterLosePerson();
                    damage.SourceMoraleChange -= 20;
                    damage.DestinationMoraleChange += 20;
                    damage.SourceCombativityChange -= 20;
                    damage.DestinationCombativityChange += 20;
                    break;
                case 8: //8、P2武将被俘虏
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson, "被俘虜");
                    sourceTroop.CatchCaptiveFromTroop(destinationPerson);
                    destinationTroop.RefreshAfterLosePerson();
                    damage.SourceMoraleChange += 20;
                    damage.DestinationMoraleChange -= 20;
                    damage.SourceCombativityChange += 20;
                    damage.DestinationCombativityChange -= 20; 
                    break;
                case 9: //9、P1武将被说服
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson, "被说服");
                    destinationPerson.ConvincePersonSuccess(sourcePerson);
                    damage.SourceCombativityChange -= 30;
                    damage.DestinationCombativityChange += 30;
                    break;
                case 10: //10、P2武将被说服
                    Session.Current.Scenario.YearTable.addChallengeEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson, "被说服");
                    sourcePerson.ConvincePersonSuccess(destinationPerson);
                    damage.SourceCombativityChange += 30;
                    damage.DestinationCombativityChange -= 30; 
                    break;
                case -1: //-1：平局
                    Session.Current.Scenario.YearTable.addChallengeDrawEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson);
                    break;
                case -2: //-2：平局：P1武将被杀
                    Session.Current.Scenario.YearTable.addChallengeDrawKilledEntry(Session.Current.Scenario.Date, destinationPerson, sourcePerson);
                    this.challengePersonDie(sourcePerson, sourceTroop, destinationPerson);
                    break;
                case -3: //-3：平局：P2武将被杀
                    Session.Current.Scenario.YearTable.addChallengeDrawKilledEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson);
                    this.challengePersonDie(destinationPerson, destinationTroop, sourcePerson);
                    break;
                case -4: //-4：平局：双方武将被杀
                    Session.Current.Scenario.YearTable.addChallengeDrawBothKilledEntry(Session.Current.Scenario.Date, sourcePerson, destinationPerson);
                    this.challengePersonDie(sourcePerson, sourceTroop, destinationPerson);
                    this.challengePersonDie(destinationPerson, destinationTroop, sourcePerson);
                    break;
            }


        }



        private void challengePersonDie(Person challengePerson,Troop troop, Person killer)
        {
            if (Session.GlobalVariables.PersonDieInChallenge)
            {
                if (troop == troop.StartingArchitecture.RobberTroop)
                {
                    troop.Persons.Remove(troop.Leader);
                    troop.Leader.LocationTroop = null;
                }
                else
                {
                    challengePerson.KilledInBattle(troop, killer);
                }
            }
        }


    }
}
