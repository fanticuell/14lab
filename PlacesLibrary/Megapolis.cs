using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class Megapolis : City, IInit
    {
        public bool IsCapital { get; set; }

        public Megapolis() : base()
        {
            IsCapital = false;
        }

        public Megapolis(string name, string center, int population, bool isCapital)
            : base(name, center, population)
        {
            IsCapital = isCapital;
        }

        public Megapolis(Megapolis other) : base(other)
        {
            IsCapital = other.IsCapital;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Столица: {IsCapital}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Столица? (true/false): ");
            IsCapital = bool.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();

            IsCapital = rnd.Next(2) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Megapolis other)
                return base.Equals(obj) && IsCapital == other.IsCapital;
            return false;
        }
    }
}
