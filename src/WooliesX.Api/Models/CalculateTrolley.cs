
using System.Collections.Generic;

namespace WooliesX.Api.Models
{
    public class CalculateTrolley
    {
        public List<Product> Products { get; set; }
        public List<Quantity> Quantities { get; set; }
        public List<Special> Specials { get; set; }
        

        public CalculateTrolley()
        {
            Products = new List<Product>();
            Quantities = new List<Quantity>();
            Specials = new List<Special>();
        }
    }
}
