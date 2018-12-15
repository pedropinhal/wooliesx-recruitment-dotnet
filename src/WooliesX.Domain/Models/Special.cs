using System.Collections.Generic;

namespace WooliesX.Domain.Models
{
    public class Special
    {
        public List<Product> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}