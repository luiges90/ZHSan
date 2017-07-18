using System;
using System.Collections.Generic;
using System.Linq;



namespace GameObjects
{
	public class SortedBoundedSet<T> : ICollection<T>
	{
        private SortedDictionary<T, T> dict;
        private int bound;

        public SortedBoundedSet(int bound)
        {
            this.bound = bound;
            this.dict = new SortedDictionary<T, T>();
        }

        public SortedBoundedSet(int bound, IComparer<T> comparator)
        {
            this.bound = bound;
            this.dict = new SortedDictionary<T, T>(comparator);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public bool Remove(T item)
        {
            return this.dict.Remove(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dict.Keys.CopyTo(array, arrayIndex);
        }

        public bool Contains(T item)
        {
            return dict.ContainsKey(item);
        }

        public void Clear()
        {
            dict.Clear(); 
        }

        public void Add(T item, out T removed)
        {
            if (!dict.ContainsKey(item))
            {
                dict.Add(item, item);
                if (dict.Count > bound)
                {
                    removed = dict.Keys.Last();
                    dict.Remove(removed);
                }
                else
                {
                    removed = default(T);
                }
            }
            else
            {
                removed = item;
            }
        }

        public void Add(T item)
        {
            T t = default(T);
            this.Add(item, out t);
        }
	}
}
