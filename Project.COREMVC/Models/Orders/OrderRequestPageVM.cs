using Project.COREMVC.Models.OuterRequestModels;
using Project.ENTITIES.Models;

namespace Project.COREMVC.Models.Orders
{
    public class OrderRequestPageVM
    {
        //Refactor
        public Order Order { get; set; }

        public PaymentRequestModel PaymentRequestModel { get; set; }
    }
}
