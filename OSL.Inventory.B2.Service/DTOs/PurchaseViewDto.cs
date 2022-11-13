using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class PurchaseViewDto
    {
        public string PurchaseCode { get; set; } = string.Empty;
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? PurchaseAmountPaid { get; set; }
        public DateTime? AmountPaidTime { get; set; }
        public string ActionLinkHtml { get; set; } = string.Empty;
        public string StatusHtml { get; set; } = string.Empty;
    }
}
