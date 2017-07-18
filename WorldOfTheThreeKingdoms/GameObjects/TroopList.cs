using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class TroopList : GameObjectList
    {
        public void AddTroopWithEvent(Troop troop, bool add = true)
        {
            if (add)
            {
                base.Add(troop);
            }
            if (troop.Scenario.GameScreen != null)
            {
                troop.OnTroopCreate += new Troop.TroopCreate(this.troop_OnTroopCreate);
                troop.OnEndPath += new Troop.EndPath(this.troop_OnEndPath);
                troop.OnPathNotFound += new Troop.PathNotFound(this.troop_OnPathNotFound);
                troop.OnNormalAttack += new Troop.NormalAttack(this.troop_OnNormalAttack);
                troop.OnCombatMethodAttack += new Troop.CombatMethodAttack(this.troop_OnCombatMethodAttack);
                troop.OnCastStratagem += new Troop.CastStratagem(this.troop_OnCastStratagem);
                troop.OnCriticalStrike += new Troop.CriticalStrike(this.troop_OnCriticalStrike);
                troop.OnReceiveCriticalStrike += new Troop.ReceiveCriticalStrike(this.troop_OnReceiveCriticalStrike);
                troop.OnWaylay += new Troop.Waylay(this.troop_OnWaylay);
                troop.OnReceiveWaylay += new Troop.ReceiveWaylay(this.troop_OnReceiveWaylay);
                troop.OnSurround += new Troop.Surround(this.troop_OnSurround);
                troop.OnSetCombatMethod += new Troop.SetCombatMethod(this.troop_OnSetCombatMethod);
                troop.OnSetStratagem += new Troop.SetStratagem(this.troop_OnSetStratagem);
                troop.OnStratagemSuccess += new Troop.StratagemSuccess(this.troop_OnStratagemSuccess);
                troop.OnChaos += new Troop.Chaos(this.troop_OnChaos);
                troop.OnRumour += new Troop.Rumour(this.troop_OnRumour);
                troop.OnAttract += new Troop.Attract(this.troop_OnAttract);
                troop.OnRecoverFromChaos += new Troop.RecoverFromChaos(this.troop_OnRecoverFromChaos);
                troop.OnCastDeepChaos += new Troop.CastDeepChaos(this.troop_OnCastDeepChaos);
                troop.OnResistStratagem += new Troop.ResistStratagem(this.troop_OnResistStratagem);
                troop.OnAmbush += new Troop.Ambush(this.troop_OnAmbush);
                troop.OnStopAmbush += new Troop.StopAmbush(this.troop_OnStopAmbush);
                troop.OnDiscoverAmbush += new Troop.DiscoverAmbush(this.troop_OnDiscoverAmbush);
                troop.OnRout += new Troop.Rout(this.troop_OnRout);
                troop.OnRouted += new Troop.Routed(this.troop_OnRouted);
                troop.OnBreakWall += new Troop.BreakWall(this.troop_OnBreakWall);
                troop.OnGetSpreadBurnt += new Troop.GetSpreadBurnt(this.troop_OnGetSpreadBurnt);
                troop.OnOccupyArchitecture += new Troop.OccupyArchitecture(this.troop_OnOccupyArchitecture);
                troop.OnAntiAttack += new Troop.AntiAttack(this.troop_OnAntiAttack);
                troop.OnAntiArrowAttack += new Troop.AntiArrowAttack(this.troop_OnAntiArrowAttack);
                troop.OnLevyFieldFood += new Troop.LevyFieldFood(this.troop_OnLevyFieldFood);
                troop.OnStartCutRouteway += new Troop.StartCutRouteway(this.troop_OnStartCutRouteway);
                troop.OnEndCutRouteway += new Troop.EndCutRouteway(this.troop_OnEndCutRouteway);
                troop.OnGetNewCaptive += new Troop.GetNewCaptive(this.troop_OnGetNewCaptive);
                troop.OnReleaseCaptive += new Troop.ReleaseCaptive(this.troop_OnReleaseCaptive);
                troop.OnPersonChallenge += new Troop.PersonChallenge(this.troop_OnPersonChallenge);
                troop.OnPersonControversy += new Troop.PersonControversy(this.troop_OnPersonControversy);
                troop.OnOutburst += new Troop.Outburst(this.troop_OnOutburst);
                troop.OnApplyStunt += new Troop.ApplyStunt(this.troop_OnApplyStunt);
                troop.OnTransportArrived += new Troop.TransportArrived(this.troop_OnTransportArrived);
            }
        }

        public Troop GetMaxAntiCriticalAttackTroop(Troop target)
        {
            int antiCriticalStrikeChance = -1;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.AntiCriticalStrikeChance > antiCriticalStrikeChance)
                {
                    antiCriticalStrikeChance = troop2.AntiCriticalStrikeChance;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMaxDefenceTroop(Troop target)
        {
            int defence = -1;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Defence > defence)
                {
                    defence = troop2.Defence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMaxIntelligenceTroop(Troop target)
        {
            int troopIntelligence = -1;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.TroopIntelligence > troopIntelligence)
                {
                    troopIntelligence = troop2.TroopIntelligence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMaxMoraleTroop(Troop target)
        {
            int morale = -1;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Morale > morale)
                {
                    morale = troop2.Morale;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMaxOffenceTroop(Troop target)
        {
            int offence = -1;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Offence > offence)
                {
                    offence = troop2.Offence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMinAntiCriticalAttackTroop(Troop target)
        {
            int antiCriticalStrikeChance = 0x7fffffff;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.AntiCriticalStrikeChance < antiCriticalStrikeChance)
                {
                    antiCriticalStrikeChance = troop2.AntiCriticalStrikeChance;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMinDefenceTroop(Troop target)
        {
            int defence = 0x7fffffff;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Defence < defence)
                {
                    defence = troop2.Defence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMinIntelligenceTroop(Troop target)
        {
            int troopIntelligence = 0x7fffffff;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.TroopIntelligence < troopIntelligence)
                {
                    troopIntelligence = troop2.TroopIntelligence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMinMoraleTroop(Troop target)
        {
            int morale = 0x7fffffff;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Morale < morale)
                {
                    morale = troop2.Morale;
                    troop = troop2;
                }
            }
            return troop;
        }

        public Troop GetMinOffenceTroop(Troop target)
        {
            int offence = 0x7fffffff;
            Troop troop = null;
            foreach (Troop troop2 in base.GameObjects)
            {
                if (target == troop2)
                {
                    return troop2;
                }
                if (troop2.Offence < offence)
                {
                    offence = troop2.Offence;
                    troop = troop2;
                }
            }
            return troop;
        }

        public void RemoveTroop(Troop troop)
        {
            base.Remove(troop);
        }

        private void troop_OnAmbush(Troop troop)
        {
            troop.Scenario.GameScreen.TroopAmbush(troop);
        }

        private void troop_OnAntiArrowAttack(Troop sending, Troop receiving)
        {
            receiving.Scenario.GameScreen.TroopAntiArrowAttack(sending, receiving);
        }

        private void troop_OnAntiAttack(Troop sending, Troop receiving)
        {
            receiving.Scenario.GameScreen.TroopAntiAttack(sending, receiving);
        }

        private void troop_OnApplyStunt(Troop troop, Stunt stunt)
        {
            troop.Scenario.GameScreen.TroopApplyStunt(troop, stunt);
        }

        private void troop_OnBreakWall(Troop troop, Architecture architecture)
        {
            troop.Scenario.GameScreen.TroopBreakWall(troop, architecture);
        }

        private void troop_OnCastDeepChaos(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopCastDeepChaos(sending, receiving);
        }

        private void troop_OnCastStratagem(Troop sending, Troop receiving, Stratagem stratagem)
        {
            sending.Scenario.GameScreen.TroopCastStratagem(sending, receiving, stratagem);
        }

        private void troop_OnChaos(Troop troop, bool deepChaos)
        {
            troop.Scenario.GameScreen.TroopChaos(troop, deepChaos);
        }

        private void troop_OnAttract(Troop troop, Troop caster)
        {
            troop.Scenario.GameScreen.TroopAttract(troop, caster);
        }

        private void troop_OnRumour(Troop troop)
        {
            troop.Scenario.GameScreen.TroopRumour(troop);
        }

        private void troop_OnCombatMethodAttack(Troop sending, Troop receiving, CombatMethod combatMethod)
        {
            sending.Scenario.GameScreen.TroopCombatMethodAttack(sending, receiving, combatMethod);
        }

        private void troop_OnCriticalStrike(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopCriticalStrike(sending, receiving);
        }

        private void troop_OnDiscoverAmbush(Troop sending, Troop receiving)
        {
            receiving.Scenario.GameScreen.TroopDiscoverAmbush(sending, receiving);
        }

        private void troop_OnEndCutRouteway(Troop troop, bool success)
        {
            troop.Scenario.GameScreen.TroopEndCutRouteway(troop, success);
        }

        private void troop_OnEndPath(Troop troop)
        {
            troop.Scenario.GameScreen.TroopEndPath(troop);
        }

        private void troop_OnGetNewCaptive(Troop troop, PersonList personlist)
        {
            troop.Scenario.GameScreen.TroopGetNewCaptive(troop, personlist);
        }

        private void troop_OnGetSpreadBurnt(Troop troop)
        {
            troop.Scenario.GameScreen.TroopGetSpreadBurnt(troop);
        }

        private void troop_OnLevyFieldFood(Troop troop, int food)
        {
            troop.Scenario.GameScreen.TroopLevyFieldFood(troop, food);
        }

        private void troop_OnNormalAttack(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopNormalAttack(sending, receiving);
        }

        private void troop_OnOccupyArchitecture(Troop troop, Architecture architecture)
        {
            troop.Scenario.GameScreen.TroopOccupyArchitecture(troop, architecture);
        }

        private void troop_OnOutburst(Troop troop, OutburstKind kind)
        {
            troop.Scenario.GameScreen.TroopOutburst(troop, kind);
        }

        private void troop_OnPathNotFound(Troop troop)
        {
            troop.Scenario.GameScreen.TroopPathNotFound(troop);
        }

        private void troop_OnPersonChallenge(int  win, Troop sourceTroop, Person source, Troop destinationTroop, Person destination)
        {
            source.Scenario.GameScreen.TroopPersonChallenge(win, sourceTroop, source, destinationTroop, destination);
        }

        private void troop_OnPersonControversy(bool win, Troop sourceTroop, Person source, Troop destinationTroop, Person destination)
        {
            source.Scenario.GameScreen.TroopPersonControversy(win, sourceTroop, source, destinationTroop, destination);
        }

        private void troop_OnReceiveCriticalStrike(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopReceiveCriticalStrike(sending, receiving);
        }

        private void troop_OnReceiveWaylay(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopReceiveWaylay(sending, receiving);
        }

        private void troop_OnRecoverFromChaos(Troop troop)
        {
            troop.Scenario.GameScreen.TroopRecoverFromChaos(troop);
        }

        private void troop_OnReleaseCaptive(Troop troop, PersonList personlist)
        {
            troop.Scenario.GameScreen.TroopReleaseCaptive(troop, personlist);
        }

        private void troop_OnResistStratagem(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
            sending.Scenario.GameScreen.TroopResistStratagem(sending, receiving, stratagem, isHarmful);
        }

        private void troop_OnRout(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopRout(sending, receiving);
        }

        private void troop_OnRouted(Troop sending, Troop receiving)
        {
            receiving.Scenario.GameScreen.TroopRouted(sending, receiving);
        }

        private void troop_OnSetCombatMethod(Troop troop, CombatMethod combatMethod)
        {
            troop.Scenario.GameScreen.TroopSetCombatMethod(troop, combatMethod);
        }

        private void troop_OnSetStratagem(Troop troop, Stratagem stratagem)
        {
            troop.Scenario.GameScreen.TroopSetStratagem(troop, stratagem);
        }

        private void troop_OnStartCutRouteway(Troop troop, int days)
        {
            troop.Scenario.GameScreen.TroopStartCutRouteway(troop, days);
        }

        private void troop_OnStopAmbush(Troop troop)
        {
            troop.Scenario.GameScreen.TroopStopAmbush(troop);
        }

        private void troop_OnStratagemSuccess(Troop sending, Troop receiving, Stratagem stratagem, bool isHarmful)
        {
            sending.Scenario.GameScreen.TroopStratagemSuccess(sending, receiving, stratagem, isHarmful);
        }

        private void troop_OnSurround(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopSurround(sending, receiving);
        }

        private void troop_OnTroopCreate(Troop troop)
        {
            troop.Scenario.GameScreen.TroopCreate(troop);
        }

        private void troop_OnWaylay(Troop sending, Troop receiving)
        {
            sending.Scenario.GameScreen.TroopWaylay(sending, receiving);
        }

        private void troop_OnTransportArrived(Troop troop, Architecture destination)
        {
            troop.Scenario.GameScreen.AskWhenTransportArrived(troop, destination);
        }

        public bool HasAnimatingTroop
        {
            get
            {
                foreach (Troop troop in base.GameObjects)
                {
                    if ((((troop.Action != TroopAction.Stop) || troop.ShowNumber) || (troop.PreAction != TroopPreAction.无)) || (troop.WaitForDeepChaosFrameCount > 0))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

