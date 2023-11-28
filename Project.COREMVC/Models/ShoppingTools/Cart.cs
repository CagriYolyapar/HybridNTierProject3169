namespace Project.COREMVC.Models.ShoppingTools
{
    public class Cart
    {
        Dictionary<int, CartItem> _myCart;

        public Cart()
        {
            _myCart = new Dictionary<int, CartItem>();
        }

        public List<CartItem> GetCartItems()
        {
            return _myCart.Values.ToList();
        }


        public void AddToCart(CartItem ci)
        {
            if (_myCart.ContainsKey(ci.ID))
            {
                _myCart[ci.ID].Amount++;
                return;
            }
            _myCart.Add(ci.ID, ci);
        }

        public void Decrease(int id) 
        {
            //if (_myCart[id].Amount > 1)
            //{
            //    _myCart[id].Amount--;
            //    return;
            //}

            //_myCart.Remove(id);

           
           _myCart[id].Amount--;
           if (_myCart[id].Amount == 0) _myCart.Remove(id);
         
            
        }

        public void RemoveFromCart(int id)
        {
            _myCart.Remove(id);
        }


        public decimal TotalPrice()
        {
            return _myCart.Values.Sum(x => x.SubTotal);
        }

       
    }
}
