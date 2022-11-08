namespace OSL.Inventory.B2.Entity
{
    public class SaleDetail
    {
        public long Id { get; set; }
        public decimal QuantitySold { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
