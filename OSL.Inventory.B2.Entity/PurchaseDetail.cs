namespace OSL.Inventory.B2.Entity
{
    public class PurchaseDetail
    {
        public long Id { get; set; }
        public decimal QuantityPurchased { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
