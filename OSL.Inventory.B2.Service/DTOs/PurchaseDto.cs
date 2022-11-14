using System.Collections.Generic;
using System;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using OSL.Inventory.B2.Service.DTOs.BaseDto;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class PurchaseDto : BaseDto<long>, IDto
    {
        public string PurchaseCode { get; set; } = string.Empty;
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? PurchaseAmountPaid { get; set; }
        public DateTime? AmountPaidTime { get; set; }
        public long SupplierId { get; set; }
        public virtual SupplierDto Supplier { get; set; }
        public virtual IList<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}
 