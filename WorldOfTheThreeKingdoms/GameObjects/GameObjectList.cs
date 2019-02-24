using GameGlobal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;
using GameObjects.TroopDetail;
using GameObjects.FactionDetail;
using GameObjects.PersonDetail;

namespace GameObjects
{
    [DataContract]
    [KnownType(typeof(AttackDefaultKindList))]
    [KnownType(typeof(AttackTargetKindList))]
    [KnownType(typeof(CastDefaultKindList))]
    [KnownType(typeof(CastTargetKindList))]
    [KnownType(typeof(InformationKindList))]
    [KnownType(typeof(PersonGeneratorTypeList))]
    [KnownType(typeof(TrainPolicyList))]

    public class GameObjectList : IEnumerable
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        [DataMember]
        public bool IsNumber;
        [DataMember]
        public string PropertyName;
        [DataMember]
        public bool SmallToBig;

        private bool immutable = false;

        [DataMember]
        public List<GameObject> GameObjects
        {
            get
            {
                return this.gameObjects;
            }
            set
            {
                this.gameObjects = value;
            }
        }

        public void SetImmutable()
        {
            immutable = true;
        }

        public void Add(GameObject t)
        {
            if (immutable)
                throw new Exception("Trying to add things to an immutable list");
            this.gameObjects.Add(t);
        }

        public void AddRange(GameObjectList t)
        {
            if (immutable)
                throw new Exception("Trying to add things to an immutable list");
            this.gameObjects.AddRange(t.GameObjects);
        }

        public void Clear()
        {
            if (immutable)
                throw new Exception("Trying to clear an immutable list");
            this.gameObjects.Clear();
        }

        public void ClearSelected()
        {
            foreach (GameObject obj2 in this.gameObjects)
            {
                obj2.Selected = false;
            }
        }

        public List<int> GenerateRandomIndexList()
        {
            int num;
            List<int> list = new List<int>();
            for (num = 0; num < this.Count; num++)
            {
                list.Add(num);
            }
            for (num = 0; num < this.Count; num++)
            {
                int num2 = num + GameObject.Random(this.Count - num);
                int num3 = list[num];
                list[num] = list[num2];
                list[num2] = num3;
            }
            return list;
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetRealEnumerator();
        }

        public int GetFreeGameObjectID()
        {
            for (int i = this.Count; i >= 0; i--)
            {
                if (!this.HasGameObject(i))
                {
                    return i;
                }
            }
            throw new Exception("GetFreeGameObjectID Error.");
        }

        public GameObject GetGameObject(int ID)
        {
            if (ID >= 0)
            {
                return this.gameObjects.FirstOrDefault(ga => ga != null && ga.ID == ID);
                //foreach (GameObject obj2 in this.gameObjects)
                //{
                //    if (obj2.ID == ID)
                //    {
                //        return obj2;
                //    }
                //}
            }
            return null;
        }

        public GameObject GetGameObject(string Name)
        {
            foreach (GameObject obj2 in this.gameObjects)
            {
                if (obj2.Name == Name)
                {
                    return obj2;
                }
            }
            return null;
        }

        public GameObjectList GetList()
        {
            GameObjectList list = new GameObjectList();
            foreach (GameObject obj2 in this.gameObjects)
            {
                list.Add(obj2);
            }
            return list;
        }

        public GameObjectList GetList(params GameObjectCondition[] conditions)
        {
            GameObjectList list = new GameObjectList();
            foreach (GameObject obj2 in this.gameObjects)
            {
                bool flag = true;
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (conditions[i].LEG == 0)
                    {
                        if (!StaticMethods.GetPropertyValue(obj2, conditions[i].PropertyName).Equals(conditions[i].PropertyValue))
                        {
                            flag = false;
                            break;
                        }
                    }
                    else if (conditions[i].LEG > 0)
                    {
                        if (((int)StaticMethods.GetPropertyValue(obj2, conditions[i].PropertyName)) <= ((int)conditions[i].PropertyValue))
                        {
                            flag = false;
                            break;
                        }
                    }
                    else if ((conditions[i].LEG < 0) && (((int)StaticMethods.GetPropertyValue(obj2, conditions[i].PropertyName)) >= ((int)conditions[i].PropertyValue)))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    list.Add(obj2);
                }
            }
            return list;
        }

        public GameObjectList GetMaxObjects(int count)
        {
            int num;
            if (count > this.Count)
            {
                count = this.Count;
            }
            GameObjectList list = new GameObjectList();
            if (!this.SmallToBig)
            {
                for (num = 0; num < count; num++)
                {
                    list.Add(this[num]);
                }
                return list;
            }
            for (num = count - 1; num >= 0; num--)
            {
                list.Add(this[num]);
            }
            return list;
        }

        public GameObjectList GetMinObjects(int count)
        {
            int num;
            if (count > this.Count)
            {
                count = this.Count;
            }
            GameObjectList list = new GameObjectList();
            if (this.SmallToBig)
            {
                for (num = 0; num < count; num++)
                {
                    list.Add(this[num]);
                }
                return list;
            }
            for (num = count - 1; num >= 0; num--)
            {
                list.Add(this[num]);
            }
            return list;
        }

        public GameObjectList GetRandomList()
        {
            GameObjectList list = new GameObjectList();
            foreach (int num in this.GenerateRandomIndexList())
            {
                list.Add(this.gameObjects[num]);
            }
            return list;
        }

        public GameObject GetRandomObject()
        {
            return this.GameObjects[GameObject.Random(this.GameObjects.Count)];
        }

        public IEnumerator GetRealEnumerator()
        {
            foreach (GameObject iteratorVariable0 in this.gameObjects)
            {
                yield return iteratorVariable0;
            }
        }

        public GameObjectList GetSelectedList()
        {
            GameObjectList list = new GameObjectList();
            foreach (GameObject obj2 in this.gameObjects)
            {
                if (obj2.Selected)
                {
                    list.Add(obj2);
                }
            }
            return list;
        }

        public bool HasGameObject(GameObject t)
        {
            return (this.gameObjects.IndexOf(t) >= 0);
        }

        public bool HasGameObject(int ID)
        {
            if (ID >= 0)
            {
                foreach (GameObject obj2 in this.gameObjects)
                {
                    if (obj2.ID == ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasGameObject(string Name)
        {
            foreach (GameObject obj2 in this.gameObjects)
            {
                if (obj2.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSelectedItem()
        {
            foreach (GameObject obj2 in this.gameObjects)
            {
                if (obj2.Selected)
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(GameObject t)
        {
            return this.gameObjects.IndexOf(t);
        }

        public List<string> LoadFromString(GameObjectList list, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    GameObject gameObject = list.GetGameObject(int.Parse(str));
                    if (gameObject != null)
                    {
                        this.Add(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("人物ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("多项人物一栏应为半型空格分隔的称号ID");
            }
            return errorMsg;
        }

        public void Remove(GameObject gameObject)
        {
            if (immutable)
                throw new Exception("Trying to remove things to an immutable list");
            this.gameObjects.RemoveAll(delegate(GameObject o)
            {
                return o == gameObject;
            }
            );
            //this.gameObjects.Remove(gameObject);
        }

        public void RemoveAt(int index)
        {
            if (immutable)
                throw new Exception("Trying to remove things to an immutable list");
            this.gameObjects.RemoveAt(index);
        }

        public void ReSort()
        {
            PropertyComparer comparer = new PropertyComparer(this.PropertyName, this.IsNumber, this.SmallToBig);
            this.gameObjects.Sort(comparer);
        }

        public string SaveToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (GameObject obj2 in this.gameObjects)
            {
                builder.Append(obj2.ID.ToString() + " ");
            }
            return builder.ToString();
        }

        public void SetOtherUnSelected(GameObject selectedT)
        {
            foreach (GameObject obj2 in this.gameObjects)
            {
                if (obj2 != selectedT)
                {
                    obj2.Selected = false;
                }
            }
        }

        public void SetSelected(GameObjectList gameObjectList)
        {
            foreach (GameObject gameObject in this.gameObjects)
            {
                if (gameObjectList.HasGameObject(gameObject))
                {
                    gameObject.Selected = true;
                }
            }
        }

        public void Sort(IComparer<GameObject> comparer)
        {
            this.gameObjects.Sort(comparer);
        }

        public void StableSort(IComparer<GameObject> comparer)
        {
            this.gameObjects = this.gameObjects.OrderBy<GameObject, GameObject>(x => x, comparer).ToList<GameObject>();
        }

        public override string ToString()
        {
            return (base.GetType().Name + ":Count=" + this.Count);
        }

        public int Count
        {
            get
            {
                return this.gameObjects.Count;
            }
        }

        public GameObject this[int index]
        {
            get
            {
                return this.gameObjects[index];
            }
            set
            {
                this.gameObjects[index] = value;
            }
        }

        /// <summary>

        /// </summary>
        /*
        [CompilerGenerated]
        private sealed class GetRealEnumerator__0 : IEnumerator<object>, IEnumerator, IDisposable
        {
            private int a1__state;
            private object a2__current;
            public GameObjectList a4__this;
            public List<GameObject>.Enumerator a7__wrap2;
            public GameObject go5__1;

            [DebuggerHidden]
            public GetRealEnumerator__0(int a1__state)
            {
                this.a1__state = a1__state;
            }

            private void am__Finally3()
            {
                this.a1__state = -1;
                this.a7__wrap2.Dispose();
            }

            //private bool MoveNext()
            public bool MoveNext()
            {
                try
                {
                    /*
                    switch (this.a1__state)
                    {
                        case 0:
                            this.a1__state = -1;
                            this.a7__wrap2 = this.a4__this.gameObjects.GetEnumerator();
                            this.a1__state = 1;
                            while (this.a7__wrap2.MoveNext())
                            {
                                this.go5__1 = this.a7__wrap2.Current;
                                this.a2__current = this.go5__1;
                                this.a1__state = 2;
                                return true;
                            Label_0071:
                                this.a1__state = 1;
                            }
                            this.am__Finally3();
                            break;

                        case 2:
                            goto Label_0071;

                    }*/


        /*

                    switch (this.a1__state)
                    {
                        case 0:
                            this.a1__state = -1;
                            this.a7__wrap2 = this.a4__this.gameObjects.GetEnumerator();
                            this.a1__state = 1;
                            while (this.a7__wrap2.MoveNext())
                            {
                                this.go5__1 = this.a7__wrap2.Current;
                                this.a2__current = this.go5__1;
                                this.a1__state = 2;
                                return true;
                            //Label_0071:
                                //this.a1__state = 1;
                            }
                            this.am__Finally3();
                            break;

                        case 2:
                            //goto Label_0071;

                            this.a1__state = 1;
                            while (this.a7__wrap2.MoveNext())
                            {
                                this.go5__1 = this.a7__wrap2.Current;
                                this.a2__current = this.go5__1;
                                this.a1__state = 2;
                                return true;
                            //Label_0071:
                                //this.a1__state = 1;
                            }
                            this.am__Finally3();
                            break;



                    }





                    return false;
                }
                //fault
                catch
                {
                    //this.System.IDisposable.Dispose();
                    throw new Exception("GameObjectList.cs   private bool MoveNext()     error!"); 
                }
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.a1__state)
                {
                    case 1:
                    case 2:
                        try
                        {
                        }
                        finally
                        {
                            this.am__Finally3();
                        }
                        break;
                }
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.a2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.a2__current;
                }
            }
        }

        */
        //end 
    }
}

