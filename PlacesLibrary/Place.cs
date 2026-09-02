using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class Place : IInit, IComparable, ICloneable
    {
        protected static Random rnd = new Random();

        protected string name;
        public Address Addr { get; set; }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название не может быть пустым");
                name = value;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is Place other)
                return this.Name.CompareTo(other.Name);

            throw new ArgumentException("Некорректный объект");
        }

        public Place() { Name = "Неизвестно"; }

        public Place(string name)
        {
            Name = name;
        }

        public Place(Place other)
        {
            Name = other.Name;
        }

        public virtual void Show()
        {
            Console.WriteLine($"Место: {Name}");
        }

        public virtual void Init()
        {
            Console.Write("Введите название: ");
            Name = Console.ReadLine();
        }

        public virtual void RandomInit()
        {
            string[] names = { "Место1", "Место2", "Место3" };
            Name = names[rnd.Next(names.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj is Place other)
                return Name == other.Name;
            return false;
        }

        public object Clone()
        {
            Place copy = new Place(this);
            copy.Addr = new Address(this.Addr);
            return copy;
        }

        public Place ShallowCopy()
        {
            return (Place)this.MemberwiseClone();
        }
    }

    public interface IInit
    {
        void Init();
        void RandomInit();
    }

    public class PopulationComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            int pop1 = (x is City c1) ? c1.Population : 0;
            int pop2 = (y is City c2) ? c2.Population : 0;

            return pop1.CompareTo(pop2);
        }
    }
}
