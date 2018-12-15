
using Newtonsoft.Json;

namespace WooliesX.Api.Models
{
    public class Quantity
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Quantity")]
        public int QuantityValue { get; set; }
    }
}
