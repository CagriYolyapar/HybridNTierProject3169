using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Project.COREMVC.Models.ShoppingTools
{
    [Serializable]
    public class CartItem
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [JsonProperty("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("SubTotal")]
        public decimal SubTotal { get { return Amount * UnitPrice; } }

    }
}




//Session'a atmak => Value => Serialize => Json Formatta string  

//Session'dan almak => Value => Deserialize => 