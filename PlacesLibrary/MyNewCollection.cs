using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlacesLibrary.CollectionHandlerEventArgs;

namespace PlacesLibrary
{
    public class MyNewCollection : MyCollection
    {
        public string Name { get; set; }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyNewCollection(string name) : base()
        {
            Name = name;
        }

        public MyNewCollection(string name, int capacity) : base(capacity)
        {
            Name = name;
        }

        protected virtual void OnCollectionCountChanged(string changeType, object item)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(Name, changeType, item));
        }

        protected virtual void OnCollectionReferenceChanged(string changeType, object item)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(Name, changeType, item));
        }

        public void AddDefaults()
        {
            Place defaultPlace = new Place("Место 1");

            base.Add(defaultPlace);

            OnCollectionCountChanged("Добавлен элемент по умолчанию", defaultPlace);
        }

        public void Add(object[] items)
        {
            if (items == null) return;

            foreach (var obj in items)
            {
                if (obj is Place item)
                {
                    base.Add(item);
                    OnCollectionCountChanged("Добавлен элемент из массива", item);
                }
                else
                {
                    throw new ArgumentException("Элемент массива не является объектом класса Place или его наследником.");
                }
            }
        }

        public bool Remove(int index)
        {
            if (index < 0 || index >= Count) return false;

            Node current = Head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            Place itemToRemoval = current.Data;

            base.Remove(itemToRemoval);

            OnCollectionCountChanged("Удален элемент по индексу", itemToRemoval);
            return true;
        }

        public Place this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException("Индекс вне диапазона коллекции.");

                Node current = Head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException("Индекс вне диапазона коллекции.");

                Node current = Head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                current.Data = value;

                OnCollectionReferenceChanged("Заменен элемент в коллекции", value);
            }
        }
    }
}
