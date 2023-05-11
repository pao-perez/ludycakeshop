namespace LudyCakeShop.Core.Domain.Data
{
    public class Order : BaseOrder
    {
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNumber { get; set; }
    }
}
