using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class Node
    {
        public Place Data { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }

        public Node(Place data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
