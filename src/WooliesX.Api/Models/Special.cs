
using System.Collections.Generic;

namespace WooliesX.Api.Models
{
    public class Special
    {
        public List<Quantity> Quantities { get; set; }
        public decimal Total { get; set; }

        public Special()
        {
            Quantities = new List<Quantity>();
        }
    }
}
