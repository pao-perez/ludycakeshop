
namespace LudyCakeShop.Domain
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductImageID { get; set; }
        public int QuantityAvailable { get; set; }
        public bool Discontinued { get; set; }

    }
}
