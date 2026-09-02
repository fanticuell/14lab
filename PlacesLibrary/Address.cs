using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class Address : Place, IInit
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public Address() : base()
        {
            Street = "Неизвестно";
            HouseNumber = 0;
        }

        public Address(string name, string street, int house) : base(name)
        {
            Street = street;
            HouseNumber = house;
        }

        public Address(Address other) : base(other)
        {
            Street = other.Street;
            HouseNumber = other.HouseNumber;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Улица: {Street}, Дом: {HouseNumber}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите улицу: ");
            Street = Console.ReadLine();
            Console.Write("Введите номер дома: ");
            HouseNumber = int.Parse(Console.ReadLine());
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] streets = { "Ленина", "Пушкина", "Гагарина" };
            Street = streets[rnd.Next(streets.Length)];
            HouseNumber = rnd.Next(1, 200);
        }

        public override bool Equals(object obj)
        {
            if (obj is Address other)
                return base.Equals(obj) &&
                       Street == other.Street &&
                       HouseNumber == other.HouseNumber;
            return false;
        }
    }
}
