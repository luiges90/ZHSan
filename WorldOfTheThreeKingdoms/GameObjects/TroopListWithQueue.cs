using GameGlobal;
using GameManager;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;


namespace GameObjects
{
    [DataContract]
    public class TroopListWithQueue : TroopList
    {
        public TroopList AmbushList = new TroopList();
        public Queue<Troop> CurrentQueue = new Queue<Troop>();
        public Troop CurrentTroop;
        private bool queueEnded = true;
        private Queue<Troop> troopQueue = new Queue<Troop>();

        public void Init()
        {
            queueEnded = true;
            AmbushList = new TroopList();
            CurrentQueue = new Queue<Troop>();
            troopQueue = new Queue<Troop>();
        }

        public void BuildQueue()
        {
            this.queueEnded = false;
            if (this.troopQueue.Count != 0)
            {
                throw new Exception("troopQueue is not empty before building");
            }
            this.AmbushList.Clear();
            GameObjectList randomList = base.GetRandomList();
            if (Session.GlobalVariables.MilitaryKindSpeedValid && (randomList.Count > 1))
            {
                randomList.PropertyName = "Speed";
                randomList.IsNumber = true;
                randomList.SmallToBig = false;
                randomList.ReSort();
            }
            foreach (Troop troop in randomList)
            {
                if (troop.CanMoveAnyway())
                {
                    troop.InitializeInQueue();
                    if (troop.Status == TroopStatus.埋伏)
                    {
                        this.AmbushList.Add(troop);
                    }
                    else
                    {
                        this.troopQueue.Enqueue(troop);
                    }
                }
                troop.Operated = false;
                troop.SelectedMove = false;
                troop.SelectedAttack = false;
                troop.Controllable = true;
            }
        }

        private bool CheckAmbushList()
        {
            Troop gameObject = null;
            int count = this.AmbushList.Count;
            foreach (Troop troop2 in this.AmbushList)
            {
                if (troop2.ToDoCombatAction())
                {
                    troop2.DoCombatAction();
                    if (troop2.OperationDone)
                    {
                        gameObject = troop2;
                        break;
                    }
                }
            }
            this.AmbushList.Remove(gameObject);
            return (gameObject != null);
        }

#pragma warning disable CS0108 // 'TroopListWithQueue.Clear()' hides inherited member 'GameObjectList.Clear()'. Use the new keyword if hiding was intended.
        public void Clear()
#pragma warning restore CS0108 // 'TroopListWithQueue.Clear()' hides inherited member 'GameObjectList.Clear()'. Use the new keyword if hiding was intended.
        {
            base.GameObjects.Clear();
            this.troopQueue.Clear();
            this.CurrentQueue.Clear();
            this.AmbushList.Clear();
        }

        public void CurrentQueueTroopMove()
        {
            
            if (this.CurrentTroop != null)
            {

                this.TroopMoveThread(this.CurrentTroop);
                /*Thread thread;
                
                thread = new Thread(new ThreadStart(this.CurrentTroop.Move));
                thread.Start();
                thread.Join();*/
                
                if (this.CurrentTroop.StepNotFinished || (this.CurrentTroop.MovabilityLeft <= 0))
                {
                    if (!this.CurrentTroop.OperationDone)
                    {
                        this.troopQueue.Enqueue(this.CurrentTroop);
                    }
                    this.CurrentTroop = null;
                }
            }
            else if (!this.CheckAmbushList())
            {
                Queue<Troop> queue = new Queue<Troop>();
                if ((this.CurrentQueue.Count == 0) && (this.troopQueue.Count > 0))
                {
                    this.CurrentQueue.Enqueue(this.troopQueue.Dequeue());
                }
                while (this.CurrentQueue.Count > 0)
                {
                    Troop item = this.CurrentQueue.Dequeue();
                    if (!item.Destroyed)
                    {
                        if (item.Destroyed || 
                            (item.Status != TroopStatus.一般 && item.Status != TroopStatus.伪报 && item.Status != TroopStatus.挑衅))
                        {
                            item.MovabilityLeft = -1;
                            item.OperationDone = true;
                        }

                        if (!item.Destroyed)
                        {
                            if (item.MovabilityLeft > 0)
                            {
                                this.TroopChangeRealDestination(item);
                                this.TroopMoveThread(item);
                            }

                            if (item.MovabilityLeft <= 0)
                            {
                                if (!item.HasToDoCombatAction && item.ToDoCombatAction())
                                {
                                    item.HasToDoCombatAction = true;
                                    this.CurrentQueue.Enqueue(item);
                                    break;
                                }
                                if (item.HasToDoCombatAction)
                                {
                                    item.HasToDoCombatAction = false;
                                    item.DoCombatAction();
                                    this.CurrentQueue.Enqueue(item);
                                    break;
                                }
                            }
                        }

                        if (item.Destroyed ||
                            (item.Status != TroopStatus.一般 && item.Status != TroopStatus.伪报 && item.Status != TroopStatus.挑衅))
                        {
                            item.MovabilityLeft = -1;
                            item.OperationDone = true;
                        }
                       
                        if ((!item.OperationDone && item.OffenceOnlyBeforeMove) && (item.Position != item.PreviousPosition))
                        {
                            item.OperationDone = true;
                        }
                        if ((!item.StepNotFinished || item.chongshemubiaoweizhibiaoji) && item.MovabilityLeft >= 0)
                        {
                            this.CurrentTroop = item;
                            break;
                        }
                        if (!this.queueEnded)
                        {
                            if (item.MovabilityLeft > 0)
                            {
                                item.WaitOnce = true;
                                queue.Enqueue(item);
                            }
                            else if (!(item.OperationDone || item.QueueEnded))
                            {
                                queue.Enqueue(item);
                            }
                        }
                    }
                }
                while (queue.Count > 0)
                {
                    this.troopQueue.Enqueue(queue.Dequeue());
                }
                if (!this.queueEnded && this.TotallyEmpty)
                {
                    this.queueEnded = true;
                    TroopList list = new TroopList();
                    foreach (Troop troop2 in base.GameObjects)
                    {
                        if (troop2.QueueEnded)
                        {
                            list.Add(troop2);
                        }
                    }
                    foreach (Troop troop2 in list.GetRandomList())
                    {
                        this.troopQueue.Enqueue(troop2);
                    }
                }
            }
        }
        private void TroopChangeRealDestination(Troop troop)
        {
            if (troop.mingling == "Attack" || troop.mingling == "Stratagem")
            {
                if (troop.TargetTroop != null)
                {
                    if (!((troop.mingling == "Attack" && troop.CanAttack(troop.TargetTroop)) ||
                    (troop.mingling == "Stratagem" && troop.CanStratagem(troop.TargetTroop))))
                    {
                        troop.RealDestination = troop.TargetTroop.Position;
                    } else
                    {
                        troop.RealDestination = troop.Position;
                    }
                }
                else if (troop.TargetArchitecture != null && troop.mingling == "Attack" && !troop.CanAttack(troop.TargetArchitecture))
                {

                }
                else
                { 
                    troop.RealDestination = troop.Position;
                }
            }
        }


        private void TroopMoveThread(Troop troop)
        {
            troop.Move();
            
            /*Thread thread;

            thread = new Thread(new ThreadStart(troop.Move));
            thread.Start();
            thread.Join();

            thread = null;*/
        }



        public void FinalizeQueue()
        {
            foreach (Troop troop in base.GameObjects)
            {
                troop.FinalizeInQueue();
            }
        }

        public void MoveTroopsFromQueueToCurrentQueue(int n)
        {
            if (this.troopQueue.Count < n)
            {
                n = this.troopQueue.Count;
            }
            for (int i = 0; i < n; i++)
            {
                this.CurrentQueue.Enqueue(this.troopQueue.Dequeue());
            }
        }

        public void StepAnimationIndex(int steps)
        {
            if (this.CurrentTroop != null)
            {
                this.CurrentTroop.AddMoveAnimationIndex(steps);
            }
            foreach (Troop troop in this.CurrentQueue)
            {
                if (troop.Action != TroopAction.Stop)
                {
                    troop.AddMoveAnimationIndex(steps);
                }
            }
        }

        public bool CurrentQueueEmpty
        {
            get
            {
                return ((this.CurrentQueue.Count == 0) && (this.CurrentTroop == null));
            }
        }

        public bool QueueEmpty
        {
            get
            {
                return (this.troopQueue.Count == 0);
            }
        }

        public bool TotallyEmpty
        {
            get
            {
                return (this.QueueEmpty && this.CurrentQueueEmpty);
            }
        }
    }
}

