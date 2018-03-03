using System;
using System.Collections;
using System.Collections.Generic;

namespace WGP
{
    public class ChainedList<T>
    {
        private static long ID = 0;

        internal class Exception : System.Exception
        {
            public Exception(string str = "") : base(str) { }
        }

        public class Element<U>
        {
            public U Value;
            public Element<U> Previous { get; internal set; }
            public Element<U> Next { get; internal set; }
            internal long Id { get; set; }

            public static implicit operator U(Element<U> v)
            {
                return v.Value;
            }

            internal Element(U v)
            {
                Value = v;
                Previous = null;
                Next = null;
                ID = -1;
            }
        }

   
        public Element<T> First { get; private set; }
        public Element<T> Last { get; private set; }
        private readonly long InternID;
        public uint Count { get; private set; }

        public ChainedList()
        {
            InternID = ID;
            ID++;
            Count = 0;
            First = null;
            Last = null;
        }

        private ChainedList(long id) : this()
        {
            ID--;
            InternID = id;
        }

        public ChainedList(Element<T> beg, Element<T> end) : this()
        {
            Insert(null, beg, end);
        }

        public ChainedList(ChainedList<T> copy) : this(copy.First, copy.Last) { }

        public void Add(T v)
        {
            Insert(null, v);
        }

        public void Insert(Element<T> at, T value)
        {
            Element<T> tmp = new Element<T>(value);
            tmp.Id = InternID;
            if (at != null)
            {
                if (at.Id == InternID)
                {
                    Count++;
                    if (at != First)
                        at.Previous.Next = tmp;
                    tmp.Previous = at.Previous;
                    tmp.Next = at;
                    at.Previous = tmp;
                }
                else
                    throw new Exception("L'élément fourni n'appartient pas à la liste.");
            }
            else
            {
                Count++;
                tmp.Previous = Last;
                tmp.Next = null;
                if (Last != null)
                    Last.Next = tmp;
                Last = tmp;
            }
            if (at == First)
                First = tmp;
        }

        public void Insert(Element<T> at, Element<T> beg, Element<T> end)
        {
            if (beg.Id != end.Id)
                throw new Exception("Les éléments fournis n'appartiennent pas à la même liste.");
            Element<T> tmp = beg;
            while (tmp != null)
            {
                Insert(at, tmp);
                if (tmp == end)
                    return;
                tmp = tmp.Next;
                if (tmp == null)
                    throw new Exception("L'élément end ne suis pas l'itérateur beg.");
            }
        }

        public void Remove(Element<T> at)
        {
            if (at.Id == InternID)
            {
                if (at == First && Count > 1)
                    First = at.Next;
                if (at == Last && Count > 1)
                    Last = at.Previous;
                Count--;
                if (at.Next != null)
                    at.Next.Previous = at.Previous;
                if (at.Previous != null)
                    at.Previous.Next = at.Next;
                if (Count == 0)
                {
                    First = null;
                    Last = null;
                }
            }
            else
                throw new Exception("Les éléments fournis n'appartient pas à la liste.");
        }
        public void Remove(Element<T> beg, Element<T> end)
        {
            if (beg.Id != InternID || end.Id != InternID)
                throw new Exception("Les éléments n'appartiennent pas à la liste.");

            List<Element<T>> list = new List<Element<T>>();
            Element<T> tmp = beg;
            while (tmp != end)
            {
                if (tmp != null)
                    list.Add(tmp);
                else
                    throw new IndexOutOfRangeException("L'élément end ne suis pas l'itérateur beg.");
                tmp = tmp.Next;
            }
            list.Add(tmp);
            foreach (var item in list)
            {
                Remove(item);
            }
        }
        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
        }
        public T[] ToArray()
        {
            T[] result = new T[Count];
            int i = 0;
            for (var it = First; it != null; it = it.Next)
            {
                result[i] = it;
                i++;
            }
            return result;
        }
        public T[] ToArray(Element<T> beg, Element<T> end)
        {
            if (beg.Id != InternID || end.Id != InternID)
                throw new Exception("Les itérateurs n'appartiennent pas à la liste.");

            List<Element<T>> list = new List<Element<T>>();
            Element<T> tmp = beg;
            while (tmp != end)
            {
                if (tmp != null)
                    list.Add(tmp);
                else
                    throw new IndexOutOfRangeException("L'élément end ne suis pas l'itérateur beg.");
                tmp = tmp.Next;
            }
            list.Add(tmp);
            T[] result = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
                result[i] = list[i];
            return result;
        }
        public void Sort()
        {
            if (Count > 1)
            {
                if (First.Value is IComparable)
                {
                    ChainedList<T> buffer = new ChainedList<T>(InternID);
                    Element<T> min;
                    buffer.Add(First);
                    min = buffer.First;
                    for (var it = First.Next;it!=null;it=it.Next)
                    {
                        IComparable value = (IComparable)it.Value;
                        Element<T> insert = min;
                        bool run = true;
                        while (run)
                        {
                            if (insert == null)
                                run = false;
                            else if (((IComparable)insert.Value).CompareTo(value) == 1)
                                run = false;
                            else
                                insert = insert.Next;
                        }
                        buffer.Insert(insert, it.Value);
                        if (((IComparable)min.Value).CompareTo(value) == 1)
                            min = buffer.First;
                    }
                    First = buffer.First;
                    Last = buffer.Last;
                }
                else
                    throw new System.Exception("Le type " + First.Value.GetType() + "n'est pas comparable.");
            }
        }
    }
}
