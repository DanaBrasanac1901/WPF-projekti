using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Model
{
    public class Product
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        
        public string Bidder { get; set; }
        public bool IsClosed { get; set; }

        public DateTime BidExpiration { get; set; }
        public Product() { }

        public Product(int id, int price, string name, string bidder, bool isClosed, DateTime bidExpiration)
        {
            ID = id;
            Price = price;
            Name = name;
            Bidder = bidder;
            IsClosed = isClosed;
            BidExpiration = bidExpiration;
        }
    }
}
