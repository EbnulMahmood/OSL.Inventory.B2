using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;
using OSL.Inventory.B2.Entity.BaseEntity;
using System.Collections.Generic;
using System;

namespace OSL.Inventory.B2.Entity
{
    public class Sale : BaseEntity<long>, IEntity
    {
        public string SaleCode { get; set; } = string.Empty;
        public decimal SaleAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal? SaleAmountPaid { get; set; }
        public DateTime? AmountPaidTime { get; set; }
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
