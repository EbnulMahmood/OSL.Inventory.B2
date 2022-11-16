using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SaleDatatableViewDto
    {
        public string SaleCode { get; set; } = string.Empty;
        public decimal SaleAmount { get; set; }
        public string SaleDate { get; set; }
        public string SaleAmountPaid { get; set; } = string.Empty;
        public string AmountPaidTime { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
