using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlacesLibrary
{
    public class MyCollection : ICollection, IEnumerable, IEnumerator
    {
        public Node Head { get; protected set; }
        public Node Tail { get; protected set; }

        private int capacity;
        public int Capacity
        {
            get => capacity;
            set => capacity = value < 0 ? 0 : value;
        }

        public MyCollection()
        {
            Capacity = 0;
            Clear();
        }

        public MyCollection(int capacity)
        {
            Capacity = capacity;
            Clear();
        }

        public MyCollection(MyCollection c)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            Capacity = c.Capacity;

            Node current = c.Head;
            while (current != null)
            {
                Add((Place)current.Data.Clone());
                current = current.Next;
            }
        }

        public void Add(Place item)
        {
            if (item == null) return;

            if (Capacity > 0 && Count >= Capacity)
            {
                throw new InvalidOperationException("Коллекция переполнена (достигнута максимальная емкость Capacity).");
            }

            Node newNode = new Node(item);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
            Count++;
        }

        public void AddRange(IEnumerable<Place> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public bool Remove(Place item)
        {
            if (Head == null || item == null) return false;

            Node current = Head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    if (current == Head) Head = current.Next;
                    if (current == Tail) Tail = current.Prev;

                    if (current.Prev != null) current.Prev.Next = current.Next;
                    if (current.Next != null) current.Next.Prev = current.Prev;

                    current.Next = null;
                    current.Prev = null;
                    Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void RemoveRange(IEnumerable<Place> items)
        {
            foreach (var item in items)
            {
                Remove(item);
            }
        }

        public Place Find(Place item)
        {
            if (Head == null || item == null) return null;

            Node current = Head;
            while (current != null)
            {
                if (current.Data.Equals(item)) return current.Data;
                current = current.Next;
            }
            return null;
        }

        public MyCollection DeepClone()
        {
            MyCollection clone = new MyCollection(this.Capacity);
            Node current = this.Head;
            while (current != null)
            {
                clone.Add((Place)current.Data.Clone());
                current = current.Next;
            }
            return clone;
        }

        public MyCollection ShallowCopy()
        {
            MyCollection copy = new MyCollection(this.Capacity);
            Node current = this.Head;
            while (current != null)
            {
                copy.Add(current.Data);
                current = current.Next;
            }
            return copy;
        }

        public void Clear()
        {
            Node current = Head;
            while (current != null)
            {
                Node next = current.Next;
                current.Next = null;
                current.Prev = null;
                current.Data = null;
                current = next;
            }
            Head = null;
            Tail = null;
            Count = 0;
        }


        public int Count { get; private set; }
        public bool IsSynchronized => false;
        public object SyncRoot => this;

        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Length) throw new ArgumentOutOfRangeException(nameof(index));

            Node current = Head;
            while (current != null)
            {
                array.SetValue(current.Data, index++);
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        private Node _currentEnumeratorNode;

        public object Current
        {
            get
            {
                if (_currentEnumeratorNode == null) throw new InvalidOperationException();
                return _currentEnumeratorNode.Data;
            }
        }

        public bool MoveNext()
        {
            if (Head == null) return false;

            if (_currentEnumeratorNode == null)
            {
                _currentEnumeratorNode = Head;
                return true;
            }

            if (_currentEnumeratorNode.Next != null)
            {
                _currentEnumeratorNode = _currentEnumeratorNode.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentEnumeratorNode = null;
        }
    }
}
