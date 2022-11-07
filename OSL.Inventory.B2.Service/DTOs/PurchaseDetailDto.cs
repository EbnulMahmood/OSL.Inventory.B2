
namespace OSL.Inventory.B2.Service.DTOs
{
    public class PurchaseDetailDto
    {
        public long Id { get; set; }
        public decimal QuantityPurchased { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public long ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
        public long PurchaseId { get; set; }
        public virtual PurchaseDto Purchase { get; set; }
    }
}
