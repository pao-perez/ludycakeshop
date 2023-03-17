namespace LudyCakeShop.Controllers
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int QuantityAvailable { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public string QuantityPerUnit { get; set; }
        public int CategoryID { get; set; }
        public int ProductImageID { get; set; }
    }
}
