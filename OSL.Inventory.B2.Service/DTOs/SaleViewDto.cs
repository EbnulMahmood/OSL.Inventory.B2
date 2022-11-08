using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SaleViewDto
    {
        public string SaleCode { get; set; } = string.Empty;
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleAmountPaid { get; set; }
        public DateTime AmountPaidTime { get; set; }
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
