namespace Project.COREMVC.Models.ShoppingTools
{
    public class CartItem
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get { return Amount * UnitPrice; } }

    }
}
