using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class RandomClass : IInit
    {
        public int Value { get; set; }

        static Random rnd = new Random();

        public void Init()
        {
            Console.Write("Введите число: ");
            Value = int.Parse(Console.ReadLine());
        }

        public void RandomInit()
        {
            Value = rnd.Next(0, 100);
        }

        public void Show()
        {
            Console.WriteLine($"Число: {Value}");
        }
    }
}
