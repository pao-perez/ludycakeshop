namespace LudyCakeShop.Controllers
{
    public class OrderItemDTO
    {
        public int OrderNumber { get; set; }
        public int ProductID { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
