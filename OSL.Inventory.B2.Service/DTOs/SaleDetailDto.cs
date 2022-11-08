namespace OSL.Inventory.B2.Service.DTOs
{
    public class SaleDetailDto
    {
        public long Id { get; set; }
        public decimal QuantitySold { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public long ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
        public long SaleId { get; set; }
        public virtual SaleDto Sale { get; set; }
    }
}
