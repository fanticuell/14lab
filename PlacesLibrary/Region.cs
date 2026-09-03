using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class Region : Place, IInit
    {
        protected string regionCenter;
        public int RegionId { get; set; }

        public string RegionCenter
        {
            get => regionCenter;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Центр области не может быть пустым");
                regionCenter = value;
            }
        }

        public Region() : base()
        {
            RegionCenter = "Неизвестно";
            RegionId = rnd.Next(1, 100);
        }

        public Region(string name, string center, int id) : base(name)
        {
            RegionCenter = center;
            RegionId = id;
        }

        public Region(Region other) : base(other)
        {
            RegionCenter = other.RegionCenter;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Центр области: {RegionCenter}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите центр области: ");
            RegionCenter = Console.ReadLine();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] centers = { "Центр1", "Центр2", "Центр3" };
            RegionCenter = centers[rnd.Next(centers.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj is Region other)
                return base.Equals(obj) && RegionCenter == other.RegionCenter;
            return false;
        }
    }
}
