using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class PurchaseDatatableViewDto
    {
        public string PurchaseCode { get; set; } = string.Empty;
        public decimal PurchaseAmount { get; set; }
        public string PurchaseDate { get; set; }
        public string PurchaseAmountPaid { get; set; } = string.Empty;
        public string AmountPaidTime { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
