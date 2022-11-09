using System;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SalesPerDayDto
    {
        public DateTime SaleDate { get; set; }
        public decimal Quantity { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductDto product { get; set; }
    }
}
