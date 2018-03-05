using System;
using System.Collections;
using System.Collections.Generic;

namespace WGP
{
    /// <summary>
    /// A basic linked list.
    /// </summary>
    /// <typeparam name="T">Type of the content.</typeparam>
    public class LinkedList<T> : IEnumerable<T>
    {
        private static long ID = 0;

        internal class ElementException : System.Exception
        {
            public ElementException(string str = "") : base(str) { }
        }


        /// <summary>
        /// An element of the linked list.
        /// </summary>
        /// <typeparam name="U">Type of the element.</typeparam>
        public class Element<U>
        {
            /// <summary>
            /// The value.
            /// </summary>
            /// <value>The value.</value>
            public U Value { get; set; }
            /// <summary>
            /// Returns the previous element in the linked list. if the current element is at the beginning, it will return null.
            /// </summary>
            /// <value>Previous element.</value>
            public Element<U> Previous { get; internal set; }
            /// <summary>
            /// Returns the next element in the linked list. if the current element is at the end, it will return null.
            /// </summary>
            /// <value>Next element.</value>
            public Element<U> Next { get; internal set; }
            internal long Id { get; set; }

            /// <summary>
            /// Implicit cast to the value.
            /// </summary>
            /// <param name="v">Element to cast.</param>
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

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        /// <summary>
        /// First element of the list. Is null if the list is empty.
        /// </summary>
        /// <value>First element.</value>
        public Element<T> First { get; private set; }
        /// <summary>
        /// Last element of the list. Is null if the list is empty.
        /// </summary>
        /// <value>Last element.</value>
        public Element<T> Last { get; private set; }
        private readonly long InternID;
        /// <summary>
        /// Number of elements in the list.
        /// </summary>
        /// <value>Size of the list.</value>
        public uint Count { get; private set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public LinkedList()
        {
            InternID = ID;
            ID++;
            Count = 0;
            First = null;
            Last = null;
        }
        private LinkedList(long id) : this()
        {
            ID--;
            InternID = id;
        }
        /// <summary>
        /// Constructor. Copy the elements from a range.
        /// </summary>
        /// <param name="beg">First element of the range.</param>
        /// <param name="end">Last element of the range.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="beg"/> and <paramref name="end"/> doesn't belong to the same list.</exception>
        /// <exception cref="ElementException">Thrown when <paramref name="end"/> doesn't follow <paramref name="beg"/>.</exception>
        public LinkedList(Element<T> beg, Element<T> end) : this()
        {
            Insert(null, beg, end);
        }
        /// <summary>
        /// Constructor. Copy the elements from an array.
        /// </summary>
        /// <param name="array">Array to copy.</param>
        public LinkedList(T[] array) : this()
        {
            Insert(null, array);
        }
        /// <summary>
        /// Constructor. Copy the elements from an array.
        /// </summary>
        /// <param name="array">Array to add.</param>
        /// <param name="pos">Position of the first element of the range in the array.</param>
        /// <param name="count">Number of elements to add.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the range overflows from the array.</exception>
        public LinkedList(T[] array, uint pos, uint count)
        {
            Insert(null, array, pos, count);
        }
        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">List to copy.</param>
        public LinkedList(LinkedList<T> copy) : this(copy.First, copy.Last) { }
        /// <summary>
        /// Adds a new value at the end of the list.
        /// </summary>
        /// <param name="value">Value to add.</param>
        public void Add(T value)
        {
            Insert(null, value);
        }
        /// <summary>
        /// Inserts a new value in the list.
        /// </summary>
        /// <param name="at">Position of the new element, or null to add at the end of the list.</param>
        /// <param name="value">Value to add.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="at"/> doesn't belong to the list.</exception>
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
                    throw new ElementException("The element doesn't belong to the list.");
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
        /// <summary>
        /// Insert a range in the list.
        /// </summary>
        /// <param name="at">Position of the range in the list, or null to add at the end.</param>
        /// <param name="beg">First element of the range.</param>
        /// <param name="end">Last element of the range.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="at"/> doesn't belong to the list.</exception>
        /// <exception cref="ElementException">Thrown when <paramref name="beg"/> and <paramref name="end"/> doesn't belong to the same list.</exception>
        /// <exception cref="ElementException">Thrown when <paramref name="end"/> doesn't follow <paramref name="beg"/>.</exception>
        public void Insert(Element<T> at, Element<T> beg, Element<T> end)
        {
            if (beg.Id != end.Id)
                throw new ElementException("The elements beg and end doesn't belong to the same list.");
            Element<T> tmp = beg;
            while (tmp != null)
            {
                Insert(at, tmp);
                if (tmp == end)
                    return;
                tmp = tmp.Next;
                if (tmp == null)
                    throw new ElementException("The element end doesn't follow beg.");
            }
        }
        /// <summary>
        /// Inserts an array in the list.
        /// </summary>
        /// <param name="at">Position of the new element, or null to add at the end of the list.</param>
        /// <param name="array">array to add.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="at"/> doesn't belong to the list.</exception>
        public void Insert(Element<T> at, T[] array)
        {
            foreach (var item in array)
            {
                Insert(at, item);
            }
        }
        /// <summary>
        /// Inserts an array in the list.
        /// </summary>
        /// <param name="at">Position of the new element, or null to add at the end of the list.</param>
        /// <param name="array">Array to add.</param>
        /// <param name="pos">Position of the first element of the range in the array.</param>
        /// <param name="count">Number of elements to add.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="at"/> doesn't belong to the list.</exception>
        /// <exception cref="IndexOutOfRangeException">Thrown when the range overflows from the array.</exception>
        public void Insert(Element<T> at, T[] array, uint pos, uint count)
        {
            if (count + pos > array.Length)
                throw new IndexOutOfRangeException("The range overflows from the array.");
            for (int i = (int)pos;i<count + pos;i++)
            {
                Insert(at, array[i]);
            }
        }
        /// <summary>
        /// Removes an element of the list.
        /// </summary>
        /// <param name="at">Element to remove.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="at"/> doesn't belong to the list.</exception>
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
                throw new ElementException("The element doesn't belong to the list.");
        }
        /// <summary>
        /// Removes a range of elements from the list.
        /// </summary>
        /// <param name="beg">First element of the range.</param>
        /// <param name="end">Last element of the range.</param>
        /// <exception cref="ElementException">Thrown when <paramref name="beg"/> or <paramref name="end"/> doesn't belong to the list.</exception>
        /// <exception cref="ElementException">Thrown when <paramref name="end"/> doesn't follow <paramref name="beg"/>.</exception>
        public void Remove(Element<T> beg, Element<T> end)
        {
            if (beg.Id != InternID || end.Id != InternID)
                throw new ElementException("The elements beg and end doesn't belong to the same list.");

            List<Element<T>> list = new List<Element<T>>();
            Element<T> tmp = beg;
            while (tmp != end)
            {
                if (tmp != null)
                    list.Add(tmp);
                else
                    throw new IndexOutOfRangeException("The element end doesn't follow beg.");
                tmp = tmp.Next;
            }
            list.Add(tmp);
            foreach (var item in list)
            {
                Remove(item);
            }
        }
        /// <summary>
        /// Remove all elements.
        /// </summary>
        public void Clear()
        {
            First = null;
            Last = null;
            Count = 0;
        }
        /// <summary>
        /// Returns a copy of the list as an array.
        /// </summary>
        /// <returns>Copy of the list.</returns>
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
        /// <summary>
        /// Returns a copy of a range of elements from the list as an array.
        /// </summary>
        /// <param name="beg">First element of the range.</param>
        /// <param name="end">Last element of the range.</param>
        /// <returns>Copy of the range of elements from the list.</returns>
        /// <exception cref="ElementException">Thrown when <paramref name="beg"/> or <paramref name="end"/> doesn't belong to the list.</exception>
        /// <exception cref="ElementException">Thrown when <paramref name="end"/> doesn't follow <paramref name="beg"/>.</exception>
        public T[] ToArray(Element<T> beg, Element<T> end)
        {
            if (beg.Id != InternID || end.Id != InternID)
                throw new ElementException("The elements beg and end doesn't belong to the same list.");

            List<Element<T>> list = new List<Element<T>>();
            Element<T> tmp = beg;
            while (tmp != end)
            {
                if (tmp != null)
                    list.Add(tmp);
                else
                    throw new IndexOutOfRangeException("The element end doesn't follow beg.");
                tmp = tmp.Next;
            }
            list.Add(tmp);
            T[] result = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
                result[i] = list[i];
            return result;
        }
        /// <summary>
        /// Sorts the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the type T isn't comparable.</exception>
        public void Sort()
        {
            if (Count > 1)
            {
                if (First.Value is IComparable)
                {
                    LinkedList<T> buffer = new LinkedList<T>(InternID);
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
                    throw new InvalidOperationException("The type " + First.Value.GetType() + " isn't comparable.");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var item = First; item != null; item = item.Next)
                yield return item.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            string result = "[ ";
            for (var it = First; it != null; it = it.Next)
            {
                result += it;
                if (it.Next != null)
                    result += ", ";
            }
            result += " ]";
            return result;
        }
    }
}
