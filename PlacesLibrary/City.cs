using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class City : Region, IInit
    {
        protected int population;

        public int Population
        {
            get => population;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Население не может быть отрицательным");
                population = value;
            }
        }

        public City() : base()
        {
            Population = 0;
        }

        public City(string name, string center, int population) : base(name, center)
        {
            Population = population;
        }

        public City(City other) : base(other)
        {
            Population = other.Population;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Население: {Population}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите население: ");
            Population = int.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Population = rnd.Next(1000, 1000000);
        }

        public override bool Equals(object obj)
        {
            if (obj is City other)
                return base.Equals(obj) && Population == other.Population;
            return false;
        }
    }
}
