using OSL.Inventory.B2.Entity.BaseEntity;
using OSL.Inventory.B2.Entity.BaseEntity.Interfaces;
using System;
using System.Collections.Generic;

namespace OSL.Inventory.B2.Entity
{
    public class Purchase : BaseEntity<long>, IEntity
    {
        public string PurchaseCode { get; set; } = string.Empty;
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseAmountPaid { get; set; }
        public DateTime AmountPaidTime { get; set; }
        public long SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseDetail> purchaseDetails { get; set; }
    }
}
